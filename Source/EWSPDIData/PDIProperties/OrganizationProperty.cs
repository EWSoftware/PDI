//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : OrganizationProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/21/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
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

        private static Regex reSplit = new Regex(@"(?:^[;])|(?<=(?:[^\\]))[;]");

        private StringCollection units;
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
        /// This read-only property defines the tag (ORG)
        /// </summary>
        public override string Tag
        {
            get { return "ORG"; }
        }

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation
        {
            get { return ValLocValue.Text; }
        }

        /// <summary>
        /// This property is used to set or get the organization name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// This property is used to get the Organization Units string collection
        /// </summary>
        /// <value>Units can be added to or removed from the returned collection reference</value>
        public StringCollection Units
        {
            get { return units; }
        }

        /// <summary>
        /// This property is used to set or get the Organization Units as a string value
        /// </summary>
        /// <value>The string can contain one or more unit names separated by commas or semi-colons.  The string
        /// will be split and loaded into the units string collection.</value>
        public string UnitsString
        {
            get { return String.Join(", ", units); }
            set
            {
                string tempUnit;
                string[] entries;

                units.Clear();

                if(value != null)
                {
                    entries = value.Split(new[] { ',', ';'}, StringSplitOptions.RemoveEmptyEntries);

                    foreach(string s in entries)
                    {
                        tempUnit = s.Trim();

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
        public override string Value
        {
            get
            {
                SpecificationVersions v = this.Version;

                // If empty, nothing will be saved
                if(String.IsNullOrWhiteSpace(this.Name) && this.Units.Count == 0)
                    return null;

                StringBuilder sb = new StringBuilder(256);

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
                string tempUnit;
                string[] parts;

                this.Name = null;
                this.Units.Clear();

                if(!String.IsNullOrWhiteSpace(value))
                {
                    // Split on all semi-colons except escaped ones
                    parts = reSplit.Split(value);

                    this.Name = EncodingUtils.Unescape(parts[0]);

                    for(int idx = 1; idx < parts.Length; idx++)
                    {
                        tempUnit = EncodingUtils.Unescape(parts[idx].Trim());

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
        public override string EncodedValue
        {
            get
            {
                string orgValue = this.Value;

                // Encode using the character set?  If so, unescape the backslashes as they get double-encoded.
                if(orgValue != null && this.Version == SpecificationVersions.vCard21)
                    orgValue = base.Encode(orgValue).Replace(@"\\", @"\");

                return orgValue;
            }
            set
            {
                this.Value = (value == null) ? value : base.Decode(value);
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public OrganizationProperty()
        {
            units = new StringCollection();
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
            OrganizationProperty o = new OrganizationProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
