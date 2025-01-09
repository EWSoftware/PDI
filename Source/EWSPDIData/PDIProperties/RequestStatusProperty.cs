//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : RequestStatusProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains the Request Status property.  It is used with the iCalendar Personal Data Interchange
// (PDI) classes.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 04/05/2004  EFW  Created the code
//===============================================================================================================

using System;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the request status (REQUEST-STATUS) property of an iCalendar object
    /// </summary>
    /// <remarks>This property class parses the <see cref="Value"/> property and allows access to the component
    /// parts.  It is used to define the status code returned for a scheduling request.</remarks>
    public class RequestStatusProperty : BaseProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports iCalendar 2.0 only</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.iCalendar20;

        /// <summary>
        /// This read-only property defines the tag (REQUEST-STATUS)
        /// </summary>
        public override string Tag => "REQUEST-STATUS";

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Text;

        /// <summary>
        /// This is used to get or set the status code value
        /// </summary>
        public string? StatusCode { get; set; }

        /// <summary>
        /// This is used to get or set the status message
        /// </summary>
        public string? StatusMessage { get; set; }

        /// <summary>
        /// This is used to get or set the extended data value
        /// </summary>
        public string? ExtendedData { get; set; }

        /// <summary>
        /// This property is overridden to handle parsing the request status components and concatenating them
        /// when requested.
        /// </summary>
        public override string? Value
        {
            get
            {
                string? statValue = null;

                // Return nothing if undefined
                if(!String.IsNullOrWhiteSpace(this.StatusCode) || !String.IsNullOrWhiteSpace(this.StatusMessage) ||
                   !String.IsNullOrWhiteSpace(this.ExtendedData))
                {
                    statValue = String.Join(";", [this.StatusCode, this.StatusMessage]);

                    if(!String.IsNullOrWhiteSpace(this.ExtendedData))
                        statValue = String.Join(";", [statValue, this.ExtendedData]);
                }

                return statValue;
            }
            set
            {
                this.StatusCode = this.StatusMessage = this.ExtendedData = null;

                if(value != null && value.Length > 0)
                {
                    string[] parts = value.Split(';');

                    if(parts.Length > 0)
                        this.StatusCode = parts[0];

                    if(parts.Length > 1)
                        this.StatusMessage = parts[1];

                    if(parts.Length > 2)
                        this.ExtendedData = parts[2];
                }
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the request status components and concatenating them
        /// when requested.
        /// </summary>
        public override string? EncodedValue
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
        public RequestStatusProperty()
        {
            this.Version = SpecificationVersions.iCalendar20;
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
            RequestStatusProperty o = new();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
