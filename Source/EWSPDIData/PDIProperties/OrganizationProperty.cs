//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : OrganizationProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains the Organization property class used by the Personal Data Interchange (PDI) vCard class
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

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Organization Name and Unit (ORG) property of a vCard
    /// </summary>
    /// <remarks>This property class parses the <see cref="BaseProperty.Value"/> property and allows access to
    /// the component organization name and unit parts.  This property is based on the X.520 Organization Name
    /// attribute and the X.520 Organization Unit attribute.</remarks>
    public class OrganizationProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private static readonly Regex reSplit = new(@"(?:^[;])|(?<=(?:[^\\]))[;]");

        private readonly StringCollection units;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports all vCard specifications</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll;

        /// <summary>
        /// This read-only property defines the tag (ORG)
        /// </summary>
        public override string Tag => "ORG";

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Text;

        /// <summary>
        /// This property is used to set or get the organization name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// This property is used to set or get the string to use when sorting organizations
        /// </summary>
        /// <value>If set, this value should be given precedence over the <see cref="Name"/> property</value>
        public string? SortAs { get; set; }

        /// <summary>
        /// This property is used to get the Organization Units string collection
        /// </summary>
        /// <value>Units can be added to or removed from the returned collection reference</value>
        public StringCollection Units => units;

        /// <summary>
        /// This property is used to set or get the Organization Units as a string value
        /// </summary>
        /// <value>The string can contain one or more unit names separated by commas or semi-colons.  The string
        /// will be split and loaded into the units string collection.</value>
        public string UnitsString
        {
            get => String.Join(", ", units);
            set
            {
                units.Clear();

                if(value != null)
                {
                    string[] entries = value.Split([',', ';'], StringSplitOptions.RemoveEmptyEntries);

                    foreach(string s in entries)
                    {
                        string tempUnit = s.Trim();

                        if(tempUnit.Length > 0)
                            units.Add(tempUnit);
                    }
                }
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the organization components and concatenating them when
        /// requested.
        /// </summary>
        /// <value>The component parts are escaped as needed</value>
        public override string? Value
        {
            get
            {
                SpecificationVersions v = this.Version;

                // If empty, nothing will be saved
                if(String.IsNullOrWhiteSpace(this.Name) && this.Units.Count == 0)
                    return null;

                StringBuilder sb = new(256);

                if(v == SpecificationVersions.vCard21)
                    sb.Append(EncodingUtils.RestrictedEscape(this.Name));
                else
                    sb.Append(EncodingUtils.Escape(this.Name));

                foreach(string s in this.Units)
                {
                    sb.Append(';');

                    if(v == SpecificationVersions.vCard21)
                        sb.Append(EncodingUtils.RestrictedEscape(s));
                    else
                        sb.Append(EncodingUtils.Escape(s));
                }

                return sb.ToString();
            }
            set
            {
                this.Name = null;
                this.Units.Clear();

                if(!String.IsNullOrWhiteSpace(value))
                {
                    // Split on all semi-colons except escaped ones
                    string[] parts = reSplit.Split(value);

                    this.Name = EncodingUtils.Unescape(parts[0]);

                    for(int idx = 1; idx < parts.Length; idx++)
                    {
                        string tempUnit = EncodingUtils.Unescape(parts[idx].Trim())!;

                        if(tempUnit.Length > 0)
                            this.Units.Add(tempUnit);
                    }
                }
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the organization components and concatenating them when
        /// requested.
        /// </summary>
        /// <value>The component parts are escaped as needed</value>
        public override string? EncodedValue
        {
            get
            {
                string? orgValue = this.Value;

                // Encode using the character set?  If so, unescape the backslashes as they get double-encoded.
                if(orgValue != null && this.Version == SpecificationVersions.vCard21)
                    orgValue = base.Encode(orgValue)?.Replace(@"\\", @"\");

                return orgValue;
            }
            set => this.Value = (value == null) ? value : base.Decode(value);
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public OrganizationProperty()
        {
            units = [];
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
            OrganizationProperty o = new();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to provide custom handling of the SORT-AS parameter
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        public override void SerializeParameters(StringBuilder sb)
        {
            base.SerializeParameters(sb);

            if(this.Version == SpecificationVersions.vCard40)
            {
                if(!String.IsNullOrWhiteSpace(this.SortAs))
                {
                    sb.Append(';');
                    sb.Append(ParameterNames.SortAs);
                    sb.Append("=\"");
                    sb.Append(this.SortAs);
                    sb.Append('"');
                }
            }
        }

        /// <summary>
        /// This is overridden to provide custom handling of the SORT-AS parameter
        /// </summary>
        /// <param name="parameters">The parameters for the property</param>
        public override void DeserializeParameters(StringCollection parameters)
        {
            if(parameters == null || parameters.Count == 0)
                return;

            for(int paramIdx = 0; paramIdx < parameters.Count; paramIdx++)
            {
                if(String.Compare(parameters[paramIdx], "SORT-AS=", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    // Remove the parameter name
                    parameters.RemoveAt(paramIdx);

                    if(paramIdx < parameters.Count)
                    {
                        this.SortAs = parameters[paramIdx];

                        // As above, remove the value
                        parameters.RemoveAt(paramIdx);
                    }
                    break;
                }
            }

            // Let the base class handle all other parameters
            base.DeserializeParameters(parameters);
        }
        #endregion
    }
}
