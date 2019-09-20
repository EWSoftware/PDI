//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : EMailProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2019
// Note    : Copyright 2004-2019, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the e-mail property class.  It is used with the Personal Data Interchange (PDI) vCard
// class.
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
    /// This class is used to represent the E-Mail (EMAIL) property of a vCard.  This specifies the electronic
    /// mail address for communication for the vCard object.
    /// </summary>
    /// <remarks>This property has no special requirements or handling.  It does contain an additional property
    /// to specify an e-mail type.  The <see cref="BaseProperty.Value"/> property contains the value.
    /// 
    /// <p/>With the use of property grouping, the association can be limited to a group of properties.  No
    /// validation is performed to ensure that the e-mail address matches the specified type.</remarks>
    public class EMailProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private static Regex reSplit = new Regex(@"(?:^[,])|(?<=(?:[^\\]))[,]");

        // This private array is used to translate parameter names and values to email types
        private static NameToValue<EMailTypes>[] ntv = {
            new NameToValue<EMailTypes>("TYPE", EMailTypes.None, false),
            new NameToValue<EMailTypes>("PREF", EMailTypes.Preferred, true),
            new NameToValue<EMailTypes>("AOL", EMailTypes.AOL, true),
            new NameToValue<EMailTypes>("AppleLink", EMailTypes.AppleLink, true),
            new NameToValue<EMailTypes>("ATTMail", EMailTypes.ATTMail, true),
            new NameToValue<EMailTypes>("CIS", EMailTypes.CompuServe, true),
            new NameToValue<EMailTypes>("eWorld", EMailTypes.eWorld, true),
            new NameToValue<EMailTypes>("INTERNET", EMailTypes.Internet, true),
            new NameToValue<EMailTypes>("IBMMail", EMailTypes.IBMMail, true),
            new NameToValue<EMailTypes>("MCIMail", EMailTypes.MCIMail, true),
            new NameToValue<EMailTypes>("POWERSHARE", EMailTypes.PowerShare, true),
            new NameToValue<EMailTypes>("PRODIGY", EMailTypes.Prodigy, true),
            new NameToValue<EMailTypes>("TLX", EMailTypes.Telex, true),
            new NameToValue<EMailTypes>("X400", EMailTypes.X400, true)
        };
        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports all vCard specifications</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll;

        /// <summary>
        /// This read-only property defines the tag (EMAIL)
        /// </summary>
        public override string Tag => "EMAIL";

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Text;

        /// <summary>
        /// This property is used to set or get the e-mail type flags
        /// </summary>
        /// <value>The default is Internet</value>
        public EMailTypes EMailTypes { get; set; }

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor.  Unless the version is changed, the object will conform to the vCard 3.0 specification.
        /// </summary>
        public EMailProperty()
        {
            this.Version = SpecificationVersions.vCard30;
            this.EMailTypes = EMailTypes.Internet;
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
            EMailProperty o = new EMailProperty();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            this.EMailTypes = ((EMailProperty)p).EMailTypes;
            base.Clone(p);
        }

        /// <summary>
        /// This is overridden to provide custom handling of the TYPE parameter
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        public override void SerializeParameters(StringBuilder sb)
        {
            base.SerializeParameters(sb);

            // Serialize the e-mail types if necessary
            if(this.EMailTypes != EMailTypes.None && this.EMailTypes != EMailTypes.Internet)
            {
                StringBuilder sbTypes = new StringBuilder(50);

                for(int idx = 1; idx < ntv.Length; idx++)
                    if((this.EMailTypes & ntv[idx].EnumValue) != 0)
                    {
                        if(sbTypes.Length > 0)
                            sbTypes.Append(',');

                        sbTypes.Append(ntv[idx].Name);
                    }

                // The format is different for the 3.0 and later specs
                if(this.Version == SpecificationVersions.vCard21)
                    sbTypes.Replace(',', ';');
                else
                {
                    sbTypes.Insert(0, "=");
                    sbTypes.Insert(0, ParameterNames.Type);
                }

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

            EMailTypes et = EMailTypes.None;

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

                    continue;   // Not an e-mail parameter
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
                                et |= ntv[subIdx].EnumValue;
                        }

                        parameters.RemoveAt(paramIdx);
                    }
                }
                else
                {
                    et |= ntv[idx].EnumValue;

                    // As above, remove the value
                    parameters.RemoveAt(paramIdx);
                }

                paramIdx--;
            }

            if(et != EMailTypes.None)
                this.EMailTypes = et;

            // Let the base class handle all other parameters
            base.DeserializeParameters(parameters);
        }
        #endregion
    }
}
