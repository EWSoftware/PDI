//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : CalendarScaleProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Calendar Scale property used by the Personal Data Interchange (PDI) iCalendar classes
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/30/2004  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the calendar scale (CALSCALE) property of an iCalendar object.  This
    /// defines the calendar scale used for the calendar information specified in the iCalendar object.
    /// </summary>
    /// <remarks>This property has no special requirements or handling.  The <see cref="BaseProperty.Value"/>
    /// property contains the scale.  This property is only valid for use with the iCalendar 2.0 specification.
    /// </remarks>
    public class CalendarScaleProperty : BaseProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports iCalendar 2.0 only</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.iCalendar20;

        /// <summary>
        /// This read-only property defines the tag (CALSCALE)
        /// </summary>
        public override string Tag => "CALSCALE";

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Text;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public CalendarScaleProperty()
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
            CalendarScaleProperty o = new CalendarScaleProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
