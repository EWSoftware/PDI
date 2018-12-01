//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : AddressProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Address property.  It is used with the Personal Data Interchange (PDI) vCard class
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
    /// This class is used to represent the Address (ADR) property of a vCard.  This specifies a structured
    /// representation of the physical delivery address for the vCard object.
    /// </summary>
    /// <remarks><para>This property class parses the <see cref="Value"/> property and allows access to the
    /// component address parts.  It is based on the X.500 Post Office Box attribute, the X.520 Street Address
    /// geographical attribute, the X.520 Locality Name geographical attribute, the X.520 State or Province Name
    /// geographical attribute, the X.520 Postal Code attribute, and the X.520 Country Name geographical
    /// attribute.</para>
    /// 
    /// <para>With the use of property grouping, the association can be limited to a group of properties.</para></remarks>
    public class AddressProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private static Regex reSplitSemiColon = new Regex(@"(?:^[;])|(?<=(?:[^\\]))[;]");
        private static Regex reSplitComma = new Regex(@"(?:^[,])|(?<=(?:[^\\]))[,]");

        //=====================================================================

        // This private array is used to translate parameter names and values to address types.
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
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCard21 |
            SpecificationVersions.vCard30;

        /// <summary>
        /// This read-only property defines the tag (ADR)
        /// </summary>
        public override string Tag => "ADR";

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Text;

        /// <summary>
        /// This property is used to set or get the address type flags
        /// </summary>
        public AddressTypes AddressTypes { get; set; }

        /// <summary>
        /// This property is used to set or get the PO Box
        /// </summary>
        public string POBox { get; set; }

        /// <summary>
        /// This property is used to set or get the extended address
        /// </summary>
        public string ExtendedAddress { get; set; }

        /// <summary>
        /// This property is used to set or get the street address
        /// </summary>
        public string StreetAddress { get; set; }

        /// <summary>
        /// This property is used to set or get the locality (i.e. city)
        /// </summary>
        public string Locality { get; set; }

        /// <summary>
        /// This property is used to set or get the region (i.e. state or province)
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// This property is used to set or get the postal (zip) code
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// This property is used to set or get the country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// This property is overridden to handle parsing the address components and concatenating them when
        /// requested.
        /// </summary>
        /// <value>The component parts are escaped as needed</value>
        public override string Value
        {
            get
            {
                SpecificationVersions v = this.Version;
                string[] parts = new string[8];
                int count = 0;

                // Only include as much as necessary
                if(this.POBox != null && this.POBox.Length > 0)
                {
                    count = 1;

                    if(v == SpecificationVersions.vCard21)
                        parts[0] = EncodingUtils.RestrictedEscape(this.POBox);
                    else
                        parts[0] = EncodingUtils.Escape(this.POBox);
                }

                if(this.ExtendedAddress != null && this.ExtendedAddress.Length > 0)
                {
                    count = 2;

                    if(v == SpecificationVersions.vCard21)
                        parts[1] = EncodingUtils.RestrictedEscape(this.ExtendedAddress);
                    else
                        parts[1] = EncodingUtils.Escape(this.ExtendedAddress);
                }

                if(this.StreetAddress != null && this.StreetAddress.Length > 0)
                {
                    count = 3;

                    if(v == SpecificationVersions.vCard21)
                        parts[2] = EncodingUtils.RestrictedEscape(this.StreetAddress);
                    else
                        parts[2] = EncodingUtils.Escape(this.StreetAddress);
                }

                if(this.Locality != null && this.Locality.Length > 0)
                {
                    count = 4;

                    if(v == SpecificationVersions.vCard21)
                        parts[3] = EncodingUtils.RestrictedEscape(this.Locality);
                    else
                        parts[3] = EncodingUtils.Escape(this.Locality);
                }

                if(this.Region != null && this.Region.Length > 0)
                {
                    count = 5;

                    if(v == SpecificationVersions.vCard21)
                        parts[4] = EncodingUtils.RestrictedEscape(this.Region);
                    else
                        parts[4] = EncodingUtils.Escape(this.Region);
                }

                if(this.PostalCode != null && this.PostalCode.Length > 0)
                {
                    count = 6;

                    if(v == SpecificationVersions.vCard21)
                        parts[5] = EncodingUtils.RestrictedEscape(this.PostalCode);
                    else
                        parts[5] = EncodingUtils.Escape(this.PostalCode);
                }

                if(this.Country != null && this.Country.Length > 0)
                {
                    count = 7;

                    if(v == SpecificationVersions.vCard21)
                        parts[6] = EncodingUtils.RestrictedEscape(this.Country);
                    else
                        parts[6] = EncodingUtils.Escape(this.Country);
                }

                // If empty, nothing is saved
                if(count == 0)
                    return null;

                return String.Join(";", parts, 0, count);
            }
            set
            {
                string[] parts;

                this.POBox = this.ExtendedAddress = this.StreetAddress = this.Locality = this.Region =
                    this.PostalCode = this.Country = null;

                if(value != null && value.Length > 0)
                {
                    // Split on all semi-colons except escaped ones
                    parts = reSplitSemiColon.Split(value);

                    if(parts.Length > 0)
                        this.POBox = EncodingUtils.Unescape(parts[0]);

                    if(parts.Length > 1)
                        this.ExtendedAddress = EncodingUtils.Unescape(parts[1]);

                    if(parts.Length > 2)
                        this.StreetAddress = EncodingUtils.Unescape(parts[2]);

                    if(parts.Length > 3)
                        this.Locality = EncodingUtils.Unescape(parts[3]);

                    if(parts.Length > 4)
                        this.Region = EncodingUtils.Unescape(parts[4]);

                    if(parts.Length > 5)
                        this.PostalCode = EncodingUtils.Unescape(parts[5]);

                    if(parts.Length > 6)
                        this.Country = EncodingUtils.Unescape(parts[6]);
                }
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the address components and concatenating them when
        /// requested.
        /// </summary>
        /// <value>The component parts are escaped as needed</value>
        public override string EncodedValue
        {
            get
            {
                string addrValue = this.Value;

                // Encode using the character set?  If so, unescape the backslashes as they get double-encoded.
                if(addrValue != null && this.Version == SpecificationVersions.vCard21)
                    addrValue = base.Encode(addrValue).Replace(@"\\", @"\");

                return addrValue;
            }
            set => this.Value = (value == null) ? value : base.Decode(value);
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor.  Unless the version is changed, the object will conform to the vCard 3.0
        /// specification.
        /// </summary>
        public AddressProperty()
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
            AddressProperty o = new AddressProperty();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            this.AddressTypes = ((AddressProperty)p).AddressTypes;
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

                    continue;   // Not an address parameter
                }

                // Parameters may appear as a pair (name followed by value) or by value alone
                if(!ntv[idx].IsParameterValue && paramIdx < parameters.Count - 1)
                {
                    // Remove the TYPE parameter name so that the base class won't put it in the custom
                    // parameters.  We'll skip this one and decode the parameter value.
                    parameters.RemoveAt(paramIdx);

                    // If the values contain a comma, split it on the comma and parse the types (i.e. vCard 3.0
                    // spec).  If not, just continue and handle it as normal.
                    if(reSplitComma.IsMatch(parameters[paramIdx]))
                    {
                        types = reSplitComma.Split(parameters[paramIdx]);

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
