//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : TimeZoneUrlProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Time Zone URL property class used by the Personal Data Interchange (PDI) iCalendar
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

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Time Zone Uniform Resource Locator (TZURL) property of a VTIMEZONE
    /// object.  This URL may be used to obtain real-time or up-to-date information about the time zone object.
    /// </summary>
    /// <remarks>This property has no special requirements or handling.  The <see cref="BaseProperty.Value"/>
    /// property contains the URL.  It is not validated for correctness but should conform to the IETF RFC 1738,
    /// Uniform Resource Locators.</remarks>
    public class TimeZoneUrlProperty : UrlProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports iCalendar 2.0 only</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.iCalendar20;

        /// <summary>
        /// This read-only property defines the tag (TZURL)
        /// </summary>
        public override string Tag => "TZURL";

        /// <summary>
        /// This read-only property defines the default value type as URI
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Uri;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        public TimeZoneUrlProperty()
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
            TimeZoneUrlProperty o = new TimeZoneUrlProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
