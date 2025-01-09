//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : RelatedProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2019-2025, Eric Woodruff, All rights reserved
//
// This file contains the Related property.  It is used with the vCard 4.0 Personal Data  Interchange (PDI)
// classes.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 12/20/2019  EFW  Created the code
//===============================================================================================================

using System;
using System.Text;
using System.Text.RegularExpressions;

using EWSoftware.PDI.Parser;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Related (RELATED-TO) property of an iCalendar component.  This is
    /// used to indicate a relationship between the object and another object identified by the unique ID in this
    /// property's value.
    /// </summary>
    /// <remarks>This class parses the <see cref="BaseProperty.Value"/> property to allow setting and getting the
    /// value as an enumerated <see cref="RelationshipType"/> value.</remarks>
    public class RelatedProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private static readonly Regex reSplit = new(@"(?:^[,])|(?<=(?:[^\\]))[,]");

        // This private array is used to translate parameter names and values to phone types
        private static readonly NameToValue<RelatedTypes>[] ntv =
        [
            new("PREF", RelatedTypes.None, false),
            new("TYPE", RelatedTypes.None, false),
            new("ACQUIANTANCE", RelatedTypes.Acquaintance, true),
            new("AGENT", RelatedTypes.Agent, true),
            new("CHILD", RelatedTypes.Child, true),
            new("CO-RESIDENT", RelatedTypes.CoResident, true),
            new("CO-WORKER", RelatedTypes.CoWorker, true),
            new("COLLEAGUE", RelatedTypes.Colleague, true),
            new("CONTACT", RelatedTypes.Contact, true),
            new("CRUSH", RelatedTypes.Crush, true),
            new("DATE", RelatedTypes.Date, true),
            new("EMERGENCY", RelatedTypes.Emergency, true),
            new("FRIEND", RelatedTypes.Friend, true),
            new("KIN", RelatedTypes.Kin, true),
            new("ME", RelatedTypes.Me, true),
            new("MET", RelatedTypes.Met, true),
            new("MUSE", RelatedTypes.Muse, true),
            new("NEIGHBOR", RelatedTypes.Neighbor, true),
            new("PARENT", RelatedTypes.Parent, true),
            new("SIBLING", RelatedTypes.Sibling, true),
            new("SPOUSE", RelatedTypes.Spouse, true),
            new("SWEETHEART", RelatedTypes.Sweetheart, true),
        ];

        private short preferredOrder;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Only supported by the vCard 4.0 specification</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCard40;

        /// <summary>
        /// This read-only property defines the tag (RELATED)
        /// </summary>
        public override string Tag => "RELATED";

        /// <summary>
        /// This read-only property defines the default value type as URI
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Uri;

        /// <summary>
        /// This property is used to set or get the related types (TYPE) parameter
        /// </summary>
        public RelatedTypes RelatedTypes { get; set; }

        /// <summary>
        /// This property is used to get or set the preferred order (vCard 4.0 only)
        /// </summary>
        /// <value>Zero if not set or the preferred usage order between 1 and 100</value>
        public short PreferredOrder
        {
            get => preferredOrder;
            set
            {
                if(value < 0)
                    value = 0;
                else
                    if(value > 100)
                    value = 100;

                preferredOrder = value;
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public RelatedProperty()
        {
            this.Version = SpecificationVersions.vCard40;
            this.RelatedTypes = RelatedTypes.Contact;
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is overridden to allow cloning of a PDI object
        /// </summary>
        /// <returns>A clone of the object</returns>
        public override object Clone()
        {
            RelatedProperty o = new();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            RelatedProperty rp = (RelatedProperty)p;
            this.RelatedTypes = rp.RelatedTypes;
            this.PreferredOrder = rp.PreferredOrder;
            base.Clone(p);
        }

        /// <summary>
        /// This is overridden to provide custom handling of the TYPE and PREF parameters
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        public override void SerializeParameters(StringBuilder sb)
        {
            base.SerializeParameters(sb);

            RelatedTypes rt = this.RelatedTypes;

            if(this.PreferredOrder > 0)
                sb.AppendFormat(";PREF={0}", this.PreferredOrder);

            // Serialize the related types if necessary
            if(rt != RelatedTypes.None)
            {
                StringBuilder sbTypes = new(50);

                for(int idx = 1; idx < ntv.Length; idx++)
                {
                    if((rt & ntv[idx].EnumValue) != 0)
                    {
                        if(sbTypes.Length > 0)
                            sbTypes.Append(',');

                        sbTypes.Append(ntv[idx].Name.ToLowerInvariant());
                    }
                }

                sbTypes.Insert(0, "=");
                sbTypes.Insert(0, ParameterNames.Type);

                sb.Append(';');
                sb.Append(sbTypes.ToString());
            }
        }

        /// <summary>
        /// This is overridden to provide custom handling of the TYPE and PREF parameters
        /// </summary>
        /// <param name="parameters">The parameters for the property</param>
        public override void DeserializeParameters(StringCollection parameters)
        {
            string[] types;
            int idx, subIdx;

            if(parameters == null || parameters.Count == 0)
                return;

            RelatedTypes rt = RelatedTypes.None;

            for(int paramIdx = 0; paramIdx < parameters.Count; paramIdx++)
            {
                for(idx = 0; idx < ntv.Length; idx++)
                {
                    if(ntv[idx].IsMatch(parameters[paramIdx]))
                        break;
                }

                if(idx == ntv.Length)
                {
                    // If it was a parameter name, skip the value too
                    if(parameters[paramIdx].EndsWith("=", StringComparison.Ordinal))
                        paramIdx++;

                    continue;   // Not a phone parameter
                }

                // Parameters may appear as a pair (name followed by value) or by value alone
                if(!ntv[idx].IsParameterValue && paramIdx < parameters.Count - 1)
                {
                    if(ntv[idx].Name == "PREF")
                    {
                        // Remove the parameter name so that the base class won't put it in the custom
                        // parameters.  We'll skip this one and decode the parameter value.
                        parameters.RemoveAt(paramIdx);

                        if(Int16.TryParse(parameters[paramIdx], out short order))
                            this.PreferredOrder = order;

                        parameters.RemoveAt(paramIdx);
                    }
                    else
                    {
                        parameters.RemoveAt(paramIdx);

                        // If the values contain a comma, split it on the comma and parse the types (i.e. vCard 3.0
                        // spec).  If not, just continue and handle it as normal.
                        if(reSplit.IsMatch(parameters[paramIdx]))
                        {
                            types = reSplit.Split(parameters[paramIdx]);

                            foreach(string s in types)
                            {
                                for(subIdx = 1; subIdx < ntv.Length; subIdx++)
                                {
                                    if(ntv[subIdx].IsMatch(s))
                                        break;
                                }

                                // Unrecognized ones are ignored
                                if(subIdx < ntv.Length)
                                    rt |= ntv[subIdx].EnumValue;
                            }

                            parameters.RemoveAt(paramIdx);
                        }
                    }
                }
                else
                {
                    rt |= ntv[idx].EnumValue;

                    // As above, remove the value
                    parameters.RemoveAt(paramIdx);
                }

                paramIdx--;
            }

            if(rt != RelatedTypes.None)
                this.RelatedTypes = rt;

            // Let the base class handle all other parameters
            base.DeserializeParameters(parameters);
        }
        #endregion
    }
}
