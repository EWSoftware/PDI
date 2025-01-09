//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : ResourcesProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains the Resources property class used by the Personal Data Interchange (PDI) classes such as
// vCalendar and iCalendar.
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
    /// This class is used to represent the Resources (RESOURCES) property of a vCalendar or iCalendar object
    /// </summary>
    /// <remarks>This property class parses the <see cref="Value"/> property and allows access to the component
    /// parts.  It is used to specify equipment or resources needed in the calendar event.  This property is only
    /// valid for use with the vCalendar 1.0 or iCalendar 2.0 specifications.</remarks>
    public class ResourcesProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private static readonly Regex reSplit = new(@"(?:^[,;])|(?<=(?:[^\\]))[,;]");

        private readonly StringCollection resources;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCalendar 1.0 and iCalendar 2.0</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCalendar10 |
            SpecificationVersions.iCalendar20;

        /// <summary>
        /// This read-only property defines the tag (RESOURCES)
        /// </summary>
        public override string Tag => "RESOURCES";

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Text;

        /// <summary>
        /// This property is used to get the resources string collection
        /// </summary>
        /// <value>Resources can be added to or removed from the returned collection reference</value>
        public StringCollection Resources => resources;

        /// <summary>
        /// This property is used to set or get the resource as a string value
        /// </summary>
        /// <value>The string can contain one or more resources separated by commas or semi-colons.  The string
        /// will be split and loaded into the resources string collection.</value>
        public string ResourcesString
        {
            get => String.Join(", ", resources);
            set
            {
                resources.Clear();

                if(value != null)
                {
                    string[] entries = value.Split(',', ';');

                    foreach(string s in entries)
                    {
                        string tempRes = s.Trim();

                        if(tempRes.Length > 0)
                            resources.Add(tempRes);
                    }
                }
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the resources and concatenating them when requested
        /// </summary>
        /// <value>The resources are escaped as needed</value>
        public override string? Value
        {
            get
            {
                // If empty, nothing will be saved
                if(this.Resources.Count == 0)
                    return null;

                StringBuilder sb = new(256);

                foreach(string s in this.Resources)
                {
                    // The vCalendar 1.0 spec uses a semi-colon
                    if(this.Version == SpecificationVersions.vCalendar10)
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
                this.Resources.Clear();

                if(value != null && value.Length > 0)
                {
                    // Split on all semi-colons and commas except escaped ones
                    string[] entries = reSplit.Split(value);

                    foreach(string s in entries)
                    {
                        string tempRes = EncodingUtils.Unescape(s.Trim())!;

                        if(tempRes.Length > 0)
                            resources.Add(tempRes);
                    }
                }
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the resources and concatenating them when requested
        /// </summary>
        /// <value>The resources are escaped as needed</value>
        public override string? EncodedValue
        {
            get => this.Value;
            set => this.Value = value;
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Default constructor.  Unless the version is changed, the object will conform to the iCalendar 2.0
        /// specification.
        /// </summary>
        public ResourcesProperty()
        {
            this.Version = SpecificationVersions.iCalendar20;
            resources = [];
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
            ResourcesProperty o = new();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
