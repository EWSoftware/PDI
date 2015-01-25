//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : CategoriesProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/18/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Categories property class used by the Personal Data Interchange (PDI) classes such as
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
// 08/19/2007  EFW  Added support for vNote objects
//===============================================================================================================

using System;
using System.Text;
using System.Text.RegularExpressions;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Categories (CATEGORIES) property of a vCard, vCalendar, or iCalendar
    /// object.
    /// </summary>
    /// <remarks>This property class parses the <see cref="Value"/> property and allows access to the component
    /// parts.  It is used to specify application category information about the object.  This property is only
    /// valid for use with the vCard 3.0 specification, vNote, vCalendar and iCalendar objects.</remarks>
    public class CategoriesProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private static Regex reSplit = new Regex(@"(?:^[,;])|(?<=(?:[^\\]))[,;]");

        private StringCollection categories;
        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCard 3.0, vCalendar 1.0, iCalendar 2.0, and IrMC 1.1.</value>
        public override SpecificationVersions VersionsSupported
        {
            get { return SpecificationVersions.vCard30 | SpecificationVersions.vCalendar10 |
                SpecificationVersions.iCalendar20 | SpecificationVersions.IrMC11; }
        }

        /// <summary>
        /// This read-only property defines the tag (CATEGORIES)
        /// </summary>
        public override string Tag
        {
            get { return "CATEGORIES"; }
        }

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation
        {
            get { return ValLocValue.Text; }
        }

        /// <summary>
        /// This property is used to get the categories string collection
        /// </summary>
        /// <value>Categories can be added to or removed from the returned collection reference</value>
        public StringCollection Categories
        {
            get { return categories; }
        }

        /// <summary>
        /// This property is used to set or get the categories as a string value
        /// </summary>
        /// <value>The string can contain one or more categories separated by commas or semi-colons.  The string
        /// will be split and loaded into the categories string collection.</value>
        public string CategoriesString
        {
            get { return String.Join(", ", categories); }
            set
            {
                string tempCat;
                string[] entries;

                categories.Clear();

                if(value != null)
                {
                    entries = value.Split(',', ';');

                    foreach(string s in entries)
                    {
                        tempCat = s.Trim();

                        if(tempCat.Length > 0)
                            categories.Add(tempCat);
                    }
                }
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the categories and concatenating them when requested
        /// </summary>
        /// <value>The categories are escaped as needed</value>
        public override string Value
        {
            get
            {
                // If empty, nothing will be saved
                if(this.Categories.Count == 0)
                    return null;

                StringBuilder sb = new StringBuilder(256);

                foreach(string s in this.Categories)
                {
                    // The vCalendar 1.0 and IrMC 1.1 spec use a semi-colon
                    if(this.Version == SpecificationVersions.vCalendar10 || this.Version == SpecificationVersions.IrMC11)
                        sb.Append(';');
                    else
                        sb.Append(',');

                    sb.Append(EncodingUtils.Escape(s));
                }

                sb.Remove(0, 1);

                return sb.ToString();
            }
            set
            {
                string tempCat;
                string[] entries;

                this.Categories.Clear();

                if(value != null && value.Length > 0)
                {
                    // Split on all semi-colons and commas except escaped ones
                    entries = reSplit.Split(value);

                    foreach(string s in entries)
                    {
                        tempCat = EncodingUtils.Unescape(s.Trim());

                        if(tempCat.Length > 0)
                            categories.Add(tempCat);
                    }
                }
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the categories and concatenating them when requested
        /// </summary>
        /// <value>The categories are escaped as needed</value>
        public override string EncodedValue
        {
            get { return this.Value; }
            set { this.Value = value; }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor.  Unless the version is changed, the object will conform to the vCard 3.0/iCalendar 2.0
        /// specification.
        /// </summary>
        public CategoriesProperty()
        {
            this.Version = SpecificationVersions.iCalendar20;
            categories = new StringCollection();
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
            CategoriesProperty o = new CategoriesProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
