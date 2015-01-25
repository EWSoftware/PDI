//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : NameProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/21/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Name property class used by the Personal Data Interchange (PDI) vCard class
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
using System.Text.RegularExpressions;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Name (N) property of a vCard
    /// </summary>
    /// <remarks>This property class parses the <see cref="Value"/> property and allows access to the component
    /// name parts.  It is based on the semantics of the X.520 individual name attributes.</remarks>
    public class NameProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private static Regex reSplit = new Regex(@"(?:^[;])|(?<=(?:[^\\]))[;]");

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
        /// This read-only property defines the tag (N)
        /// </summary>
        public override string Tag
        {
            get { return "N"; }
        }

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation
        {
            get { return ValLocValue.Text; }
        }

        /// <summary>
        /// This property is used to set or get the family name (last name)
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// This property is used to set or get the given name (first name)
        /// </summary>
        public string GivenName { get; set; }

        /// <summary>
        /// This property is used to set or get additional names such as one or more middle names
        /// </summary>
        public string AdditionalNames { get; set; }

        /// <summary>
        /// This property is used to set or get the name prefix (i.e. Mr., Mrs.)
        /// </summary>
        public string NamePrefix { get; set; }

        /// <summary>
        /// This property is used to set or get the name suffix (i.e. Jr, Sr)
        /// </summary>
        public string NameSuffix { get; set; }

        /// <summary>
        /// This read-only property can be used to get the name in a format suitable for sorting by last name
        /// </summary>
        /// <remarks>The name is returned in a comma-separated format in the order family name, given name,
        /// additional names, name suffix, name prefix.</remarks>
        public string SortableName
        {
            get
            {
                string[] parts = new string[5];
                int count = 0;

                // Only include as much as necessary
                if(!String.IsNullOrWhiteSpace(this.FamilyName))
                    parts[count++] = this.FamilyName;

                if(!String.IsNullOrWhiteSpace(this.GivenName))
                    parts[count++] = this.GivenName;

                if(!String.IsNullOrWhiteSpace(this.AdditionalNames))
                    parts[count++] = this.AdditionalNames;

                if(!String.IsNullOrWhiteSpace(this.NameSuffix))
                    parts[count++] = this.NameSuffix;

                if(!String.IsNullOrWhiteSpace(this.NamePrefix))
                    parts[count++] = this.NamePrefix;

                // If empty, return "Unknown" as it is a required property
                if(count == 0)
                    return "Unknown";

                return String.Join(", ", parts, 0, count);
            }
        }

        /// <summary>
        /// This read-only property can be used to get the full, formatted name
        /// </summary>
        public string FormattedName
        {
            get
            {
                string[] parts = new string[5];
                int count = 0;

                // Only include as much as necessary
                if(!String.IsNullOrWhiteSpace(this.NamePrefix))
                    parts[count++] = this.NamePrefix;

                if(!String.IsNullOrWhiteSpace(this.GivenName))
                    parts[count++] = this.GivenName;

                if(!String.IsNullOrWhiteSpace(this.AdditionalNames))
                    parts[count++] = this.AdditionalNames;

                if(!String.IsNullOrWhiteSpace(this.FamilyName))
                    parts[count++] = this.FamilyName;

                if(!String.IsNullOrWhiteSpace(this.NameSuffix))
                    parts[count++] = this.NameSuffix;

                // If empty, return "Unknown" as it is a required property
                if(count == 0)
                    return "Unknown";

                return String.Join(" ", parts, 0, count);
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the name components and concatenating them when
        /// requested.
        /// </summary>
        /// <value>This is a required property as defined by the 3.0 specification.  It always returns a non-null
        /// value for both the 2.1 and 3.0 specifications.  The parts will be escaped as needed.</value>
        /// <exception cref="ArgumentNullException">This is thrown if an attempt is made to set this property to
        /// null.</exception>
        public override string Value
        {
            get
            {
                SpecificationVersions v = this.Version;
                string[] parts = new string[5];
                int count = 0;

                // Only include as much as necessary
                if(!String.IsNullOrWhiteSpace(this.FamilyName))
                {
                    count = 1;

                    if(v == SpecificationVersions.vCard21)
                        parts[0] = EncodingUtils.RestrictedEscape(this.FamilyName);
                    else
                        parts[0] = EncodingUtils.Escape(this.FamilyName);
                }

                if(!String.IsNullOrWhiteSpace(this.GivenName))
                {
                    count = 2;

                    if(v == SpecificationVersions.vCard21)
                        parts[1] = EncodingUtils.RestrictedEscape(this.GivenName);
                    else
                        parts[1] = EncodingUtils.Escape(this.GivenName);
                }

                if(!String.IsNullOrWhiteSpace(this.AdditionalNames))
                {
                    count = 3;

                    if(v == SpecificationVersions.vCard21)
                        parts[2] = EncodingUtils.RestrictedEscape(this.AdditionalNames);
                    else
                        parts[2] = EncodingUtils.Escape(this.AdditionalNames);
                }

                if(!String.IsNullOrWhiteSpace(this.NamePrefix))
                {
                    count = 4;

                    if(v == SpecificationVersions.vCard21)
                        parts[3] = EncodingUtils.RestrictedEscape(this.NamePrefix);
                    else
                        parts[3] = EncodingUtils.Escape(this.NamePrefix);
                }

                if(!String.IsNullOrWhiteSpace(this.NameSuffix))
                {
                    count = 5;

                    if(v == SpecificationVersions.vCard21)
                        parts[4] = EncodingUtils.RestrictedEscape(this.NameSuffix);
                    else
                        parts[4] = EncodingUtils.Escape(this.NameSuffix);
                }

                // If empty, return "Unknown" as it is a required property
                if(count == 0)
                    return "Unknown";

                return String.Join(";", parts, 0, count);
            }
            set
            {
                string[] parts;

                if(value == null)
                    throw new ArgumentNullException("value", LR.GetString("ExNCannotBeNull"));

                this.FamilyName = this.GivenName = this.AdditionalNames = this.NamePrefix = this.NameSuffix = null;

                if(value.Length > 0)
                {
                    // Split on all semi-colons except escaped ones
                    parts = reSplit.Split(value);

                    if(parts.Length > 0)
                        this.FamilyName = EncodingUtils.Unescape(parts[0]);

                    if(parts.Length > 1)
                        this.GivenName = EncodingUtils.Unescape(parts[1]);

                    if(parts.Length > 2)
                        this.AdditionalNames = EncodingUtils.Unescape(parts[2]);

                    if(parts.Length > 3)
                        this.NamePrefix = EncodingUtils.Unescape(parts[3]);

                    if(parts.Length > 4)
                        this.NameSuffix = EncodingUtils.Unescape(parts[4]);
                }
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the name components and concatenating them when
        /// requested.
        /// </summary>
        /// <value>This is a required property as defined by the 3.0 specification.  It always returns a non-null
        /// value for both the 2.1 and 3.0 specifications.  The parts will be escaped as needed.</value>
        /// <exception cref="ArgumentNullException">This is thrown if an attempt is made to set this property to
        /// null.</exception>
        public override string EncodedValue
        {
            get
            {
                string nameValue = this.Value;

                // Encode using the character set?  If so, unescape the backslashes as they get double-encoded.
                if(this.Version == SpecificationVersions.vCard21)
                    nameValue = base.Encode(nameValue).Replace(@"\\", @"\");

                return nameValue;
            }
            set
            {
                if(value == null)
                    throw new ArgumentNullException("value", LR.GetString("ExNCannotBeNull"));

                this.Value = base.Decode(value);
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public NameProperty()
        {
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
            NameProperty o = new NameProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
