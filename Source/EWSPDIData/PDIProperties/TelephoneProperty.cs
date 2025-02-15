//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : TelephoneProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/04/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains the Telephone property class.  It is used with the Personal Data Interchange (PDI) vCard
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
    /// This class is used to represent the Telephone (TEL) property of a vCard.  This specifies the telephone
    /// number for telephony communication for the vCard object.
    /// </summary>
    /// <remarks><para>This property has no special requirements or handling.  It does contain an additional
    /// property to specify a telephone type.  The <see cref="BaseProperty.Value"/> property contains the value.</para>
    /// 
    /// <para>With the use of property grouping, the association can be limited to a group of properties.</para>
    /// 
    /// <para>This type is based on the X.500 Telephone Number attribute.  No validation is performed on the
    /// actual value though.</para></remarks>
    public class TelephoneProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private static readonly Regex reSplit = new(@"(?:^[,])|(?<=(?:[^\\]))[,]");

        // This private array is used to translate parameter names and values to phone types
        private static readonly NameToValue<PhoneTypes>[] ntv =
        [
            new("TYPE", PhoneTypes.None, false),
            new("PREF", PhoneTypes.Preferred, true),
            new("WORK", PhoneTypes.Work, true),
            new("HOME", PhoneTypes.Home, true),
            new("VOICE", PhoneTypes.Voice, true),
            new("FAX", PhoneTypes.Fax, true),
            new("MSG", PhoneTypes.Message, true),
            new("CELL", PhoneTypes.Cell, true),
            new("PAGER", PhoneTypes.Pager, true),
            new("BBS", PhoneTypes.BBS, true),
            new("MODEM", PhoneTypes.Modem, true),
            new("CAR", PhoneTypes.Car, true),
            new("ISDN", PhoneTypes.ISDN, true),
            new("VIDEO", PhoneTypes.Video, true),
            new("PCS", PhoneTypes.PCS, true),
            new("TEXT", PhoneTypes.Text, true),
            new("TEXTPHONE", PhoneTypes.TextPhone, true),

            // This is a non-standard one that pops up every now and then.  When it does, treat it like CELL.
            new NameToValue<PhoneTypes>("MOBILE", PhoneTypes.Cell, true)
        ];

        private short preferredOrder;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports all vCard specifications</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll;

        /// <summary>
        /// This read-only property defines the tag (TEL)
        /// </summary>
        public override string Tag => "TEL";

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Text;

        /// <summary>
        /// This property is used to set or get the phone type flags
        /// </summary>
        public PhoneTypes PhoneTypes { get; set; }

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

        /// <summary>
        /// This is used to get or set the URI prefix if the value location is set to URI
        /// </summary>
        public string? UriPrefix { get; set; }

        /// <summary>
        /// This is overridden to handle the URI prefix on the value if the value location is set to URI
        /// </summary>
        public override string? EncodedValue
        {
            get
            {
                if(this.ValueLocation == ValLocValue.Uri && !String.IsNullOrWhiteSpace(this.UriPrefix))
                    return this.UriPrefix + ":" + base.EncodedValue;

                return base.EncodedValue;
            }
            set
            {
                if(this.ValueLocation == ValLocValue.Uri && !String.IsNullOrWhiteSpace(value) &&
                  value!.IndexOf(':') != -1)
                {
                    int pos = value.IndexOf(':');

                    this.UriPrefix = value.Substring(0, pos);
                    value = value.Substring(pos + 1);
                }

                base.EncodedValue = value;
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Default constructor.  Unless the version is changed, the object will conform to the vCard 3.0
        /// specification.
        /// </summary>
        public TelephoneProperty()
        {
            this.Version = SpecificationVersions.vCard30;
            this.PhoneTypes = PhoneTypes.Voice;
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
            TelephoneProperty o = new();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            var clone = (TelephoneProperty)p;

            this.PhoneTypes = clone.PhoneTypes;
            this.PreferredOrder = clone.PreferredOrder;
            this.UriPrefix = clone.UriPrefix;

            base.Clone(p);
        }

        /// <summary>
        /// This is overridden to provide custom handling of the TYPE parameter
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        public override void SerializeParameters(StringBuilder sb)
        {
            base.SerializeParameters(sb);

            PhoneTypes pt = this.PhoneTypes;

            // Serialize the phone types if necessary
            if(pt != PhoneTypes.None && pt != PhoneTypes.Voice)
            {
                // PCS is only defined in the 3.0 spec
                if(this.Version != SpecificationVersions.vCard30)
                    pt &= ~PhoneTypes.PCS;

                // Text and TextPhone are only defined in the 4.0 spec
                if(this.Version != SpecificationVersions.vCard40)
                    pt &= ~(PhoneTypes.Text | PhoneTypes.TextPhone);

                // The 4.0 spec handles Preferred differently
                if(this.Version == SpecificationVersions.vCard40)
                {
                    if((pt & PhoneTypes.Preferred) != 0 && this.PreferredOrder == 0)
                        this.PreferredOrder = 1;

                    pt &= ~PhoneTypes.Preferred;

                    if(this.PreferredOrder > 0)
                        sb.AppendFormat(";PREF={0}", this.PreferredOrder);
                }

                StringBuilder sbTypes = new(50);

                // This ignores the last entry (MOBILE) as it's a duplicate of CELL
                for(int idx = 1; idx < ntv.Length - 1; idx++)
                {
                    if((pt & ntv[idx].EnumValue) != 0)
                    {
                        if(sbTypes.Length > 0)
                            sbTypes.Append(',');

                        sbTypes.Append(ntv[idx].Name);
                    }
                }

                // The format is different for the 3.0 spec and later
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
        /// <param name="parameters">The parameters for the property.</param>
        public override void DeserializeParameters(StringCollection parameters)
        {
            string[] types;
            int idx, subIdx;

            if(parameters == null || parameters.Count == 0)
                return;

            PhoneTypes pt = PhoneTypes.None;

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
                            {
                                if(ntv[subIdx].IsMatch(s))
                                    break;
                            }

                            // Unrecognized ones are ignored
                            if(subIdx < ntv.Length)
                                pt |= ntv[subIdx].EnumValue;
                        }

                        parameters.RemoveAt(paramIdx);
                    }
                }
                else
                {
                    // Preferred is handled differently in vCard 4.0
                    if(ntv[idx].EnumValue == PhoneTypes.Preferred &&
                      parameters[paramIdx].EndsWith("=", StringComparison.Ordinal))
                    {
                        parameters.RemoveAt(paramIdx);

                        if(Int16.TryParse(parameters[paramIdx], out short order))
                            this.PreferredOrder = order;
                    }
                    else
                        pt |= ntv[idx].EnumValue;

                    // As above, remove the value
                    parameters.RemoveAt(paramIdx);
                }

                paramIdx--;
            }

            if(pt != PhoneTypes.None)
                this.PhoneTypes = pt;

            // Let the base class handle all other parameters
            base.DeserializeParameters(parameters);
        }
        #endregion
    }
}
