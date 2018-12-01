//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : ProductIdProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Product ID property class used by the Personal Data Interchange (PDI) classes such as
// vCalendar, iCalendar, and vCard.
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
    /// This class is used to represent the Product ID (PRODID) property of a vCard, vCalendar, or iCalendar
    /// object.
    /// </summary>
    /// <remarks>This property class parses the <see cref="Value"/> property and allows access to the component
    /// parts.  It is used to specify the identifier for the product that created the object.  This property is
    /// only valid for use with the vCard 3.0 specification and vCalendar and iCalendar specifications.</remarks>
    public class ProductIdProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private static Regex reSplit = new Regex("//");

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCard 3.0, vCalendar 1.0, and iCalendar 2.0</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCard30 |
            SpecificationVersions.vCalendar10 | SpecificationVersions.iCalendar20;

        /// <summary>
        /// This read-only property defines the tag (PRODID)
        /// </summary>
        public override string Tag => "PRODID";

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Text;

        /// <summary>
        /// This is used to set or get whether the product ID is registered
        /// </summary>
        public bool IsRegistered { get; set; }

        /// <summary>
        /// This is used to set or get the product owner name
        /// </summary>
        public string OwnerName { get; set; }

        /// <summary>
        /// This is used to set or get the keyword description
        /// </summary>
        public string KeywordDescription { get; set; }

        /// <summary>
        /// This is used to set or get the language
        /// </summary>
        public string ProductLanguage { get; set; }

        /// <summary>
        /// This property is overridden to handle parsing the product ID components and concatenating them when
        /// requested.
        /// </summary>
        /// <value>If some but not all parts are specified, default values are used for the missing parts</value>
        public override string Value
        {
            get
            {
                // Check for missing parts
                if(String.IsNullOrWhiteSpace(this.OwnerName))
                {
                    this.OwnerName = "EWSoftware";
                    this.IsRegistered = false;
                }

                if(String.IsNullOrWhiteSpace(this.KeywordDescription))
                {
                    this.KeywordDescription = "EWSoftware PDI Class Library";
                    this.IsRegistered = false;
                }

                if(String.IsNullOrWhiteSpace(this.ProductLanguage))
                {
                    this.ProductLanguage = "EN";
                    this.IsRegistered = false;
                }

                return String.Join("//", new[] { (this.IsRegistered ? "+" : "-"), this.OwnerName,
                    this.KeywordDescription, this.ProductLanguage });
            }
            set
            {
                this.IsRegistered = false;
                this.OwnerName = this.KeywordDescription = this.ProductLanguage = null;

                if(!String.IsNullOrWhiteSpace(value))
                {
                    string[] parts = reSplit.Split(value);

                    if(parts.Length > 0)
                        this.IsRegistered = (parts[0] == "+");

                    if(parts.Length > 1)
                        this.OwnerName = parts[1];

                    if(parts.Length > 2)
                        this.KeywordDescription = parts[2];

                    if(parts.Length > 3)
                        this.ProductLanguage = parts[3];
                }
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the product ID components and concatenating them when
        /// requested.
        /// </summary>
        /// <value>If some but not all parts are specified, default values are used for the missing parts</value>
        public override string EncodedValue
        {
            get => this.Value;
            set => this.Value = value;
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductIdProperty()
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
            ProductIdProperty o = new ProductIdProperty();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// The specifications do not allow parameters for this property.  Any parameters are ignored.
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        public override void SerializeParameters(StringBuilder sb)
        {
        }

        /// <summary>
        /// The specifications do not allow parameters for this property.  Any parameters are ignored.
        /// </summary>
        /// <param name="parameters">The parameters for the property</param>
        public override void DeserializeParameters(StringCollection parameters)
        {
        }
        #endregion
    }
}
