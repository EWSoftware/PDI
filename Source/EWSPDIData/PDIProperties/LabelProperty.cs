//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : LabelProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/21/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Label property.  It is used with the Personal Data Interchange (PDI) vCard class
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/14/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.Text;
using System.Text.RegularExpressions;

using EWSoftware.PDI.Parser;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Label (LABEL) property of a vCard.  This specifies the formatted text
    /// corresponding to the delivery address of the vCard object.
    /// </summary>
    /// <remarks><para>This property has no special requirements or handling.  It does contain an additional
    /// property to specify an address type.  The <see cref="BaseProperty.Value"/> property contains the value.</para>
    /// 
    /// <para>With the use of property grouping, the association can be limited to a group of properties.</para></remarks>
    public class LabelProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private static Regex reSplit = new Regex(@"(?:^[,])|(?<=(?:[^\\]))[,]");

        // This private array is used to translate parameter names and values to address types
        private static NameToValue<AddressTypes>[] ntv = {
            new NameToValue<AddressTypes>("TYPE", AddressTypes.None, false),
            new NameToValue<AddressTypes>("DOM", AddressTypes.Domestic, true),
            new NameToValue<AddressTypes>("INTL", AddressTypes.International, true),
            new NameToValue<AddressTypes>("POSTAL", AddressTypes.Postal, true),
            new NameToValue<AddressTypes>("PARCEL", AddressTypes.Parcel, true),
            new NameToValue<AddressTypes>("HOME", AddressTypes.Home, true),
            new NameToValue<AddressTypes>("WORK", AddressTypes.Work, true),
            new NameToValue<AddressTypes>("PREF", AddressTypes.Preferred, true)
        };
        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCard 2.1 and vCard 3.0</value>
        public override SpecificationVersions VersionsSupported
        {
            get { return SpecificationVersions.vCard21 | SpecificationVersions.vCard30; }
        }

        /// <summary>
        /// This read-only property defines the tag (LABEL)
        /// </summary>
        public override string Tag
        {
            get { return "LABEL"; }
        }

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation
        {
            get { return ValLocValue.Text; }
        }

        /// <summary>
        /// This is overridden to enforce the correct encoding type when the version changes
        /// </summary>
        /// <remarks>vCard 2.1 defaults to Quoted-Printable.  vCard 3.0 uses 8-bit encoding.</remarks>
        public override SpecificationVersions Version
        {
            get { return base.Version; }
            set
            {
                base.Version = value;

                if(value == SpecificationVersions.vCard21)
                    this.EncodingMethod = EncodingType.QuotedPrintable;
                else
                    if(this.EncodingMethod == EncodingType.QuotedPrintable)
                        this.EncodingMethod = EncodingType.EightBit;
            }
        }

        /// <summary>
        /// This property is used to set or get the address type flags
        /// </summary>
        /// <value>The default is International, Postal, Parcel, and Work</value>
        public AddressTypes AddressTypes { get; set; }

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor.  Unless the version is changed, the object will conform to the vCard 3.0 specification.
        /// </summary>
        public LabelProperty()
        {
            this.Version = SpecificationVersions.vCard30;
            this.AddressTypes = AddressTypes.Default;
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
            LabelProperty o = new LabelProperty();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            this.AddressTypes = ((LabelProperty)p).AddressTypes;
            base.Clone(p);
        }

        /// <summary>
        /// This is overridden to provide custom handling of the TYPE parameter
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        public override void SerializeParameters(StringBuilder sb)
        {
            base.SerializeParameters(sb);

            // Serialize the address types if necessary
            if(this.AddressTypes != AddressTypes.None && this.AddressTypes != AddressTypes.Default)
            {
                StringBuilder sbTypes = new StringBuilder(50);

                for(int idx = 1; idx < ntv.Length; idx++)
                    if((this.AddressTypes & ntv[idx].EnumValue) != 0)
                    {
                        if(sbTypes.Length > 0)
                            sbTypes.Append(',');

                        sbTypes.Append(ntv[idx].Name);
                    }

                // The format is different for the 3.0 spec
                if(this.Version == SpecificationVersions.vCard30)
                {
                    sbTypes.Insert(0, "=");
                    sbTypes.Insert(0, ParameterNames.Type);
                }
                else
                    sbTypes.Replace(',', ';');

                sb.Append(';');
                sb.Append(sbTypes.ToString());
            }
        }

        /// <summary>
        /// This is overridden to provide custom handling of the TYPE parameter
        /// </summary>
        /// <param name="parameters">The parameters for the property</param>
        public override void DeserializeParameters(StringCollection parameters)
        {
            string[] types;
            int idx, subIdx;

            if(parameters == null || parameters.Count == 0)
                return;

            AddressTypes at = AddressTypes.None;

            for(int paramIdx = 0; paramIdx < parameters.Count; paramIdx++)
            {
                for(idx = 0; idx < ntv.Length; idx++)
                    if(ntv[idx].IsMatch(parameters[paramIdx]))
                        break;

                if(idx == ntv.Length)
                {
                    // If it was a parameter name, skip the value too
                    if(parameters[paramIdx].EndsWith("=", StringComparison.Ordinal))
                        paramIdx++;

                    continue;   // Not a label parameter
                }

                // Parameters may appear as a pair (name followed by value) or by value alone
                if(!ntv[idx].IsParameterValue && paramIdx < parameters.Count - 1)
                {
                    // Remove the TYPE parameter name so that the base class won't put it in the custom
                    // parameters.  We'll skip this one and decode the parameter value.
                    parameters.RemoveAt(paramIdx);

                    // If the values contain a comma, split it on the comma and parse the types (i.e. vCard 3.0
                    // spec).  If not, just continue and handle it as normal.
                    if(reSplit.IsMatch(parameters[paramIdx]))
                    {
                        types = reSplit.Split(parameters[paramIdx]);

                        foreach(string s in types)
                        {
                            for(subIdx = 1; subIdx < ntv.Length; subIdx++)
                                if(ntv[subIdx].IsMatch(s))
                                    break;

                            // Unrecognized ones are ignored
                            if(subIdx < ntv.Length)
                                at |= ntv[subIdx].EnumValue;
                        }

                        parameters.RemoveAt(paramIdx);
                    }
                }
                else
                {
                    at |= ntv[idx].EnumValue;

                    // As above, remove the value
                    parameters.RemoveAt(paramIdx);
                }

                paramIdx--;
            }

            if(at != AddressTypes.None)
                this.AddressTypes = at;

            // Let the base class handle all other parameters
            base.DeserializeParameters(parameters);
        }
        #endregion
    }
}
