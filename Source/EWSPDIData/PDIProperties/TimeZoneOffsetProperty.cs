//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : TimeZoneOffsetProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/04/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains the Time Zone Offset property classes used by the Personal Data Interchange (PDI) iCalendar
// classes.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//   Date      Who  Comments
// ==============================================================================================================
// 09/20/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.Globalization;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent a Time Zone Offset (TZOFFSETFROM or TZOFFSETTO) property of a VTIMEZONE
    /// observance rule component.  This specifies the UTC offset used to adjust a time value from/to universal
    /// time.
    /// </summary>
    /// <remarks>This property class parses the <see cref="BaseProperty.Value"/> property to allow access to its
    /// content as an actual <see cref="System.TimeSpan"/> object.</remarks>
    public class TimeZoneOffsetProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private readonly bool isOffsetFrom;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports iCalendar 2.0 only</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.iCalendar20;

        /// <summary>
        /// This read-only property defines the tag (<c>TZOFFSETFROM</c> or <c>TZOFFSETTO</c>).
        /// </summary>
        public override string Tag => isOffsetFrom ? "TZOFFSETFROM" : "TZOFFSETTO";

        /// <summary>
        /// This read-only property defines the default value type as UTC-OFFSET
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.UtcOffset;

        /// <summary>
        /// This is used to get or set the value as a <see cref="System.TimeSpan"/> object
        /// </summary>
        public virtual TimeSpan TimeSpanValue { get; set; }

        /// <summary>
        /// This property is overridden to handle parsing the time span
        /// to/from its string form.
        /// </summary>
        public override string? Value
        {
            get
            {
                string tz;
                int hours = this.TimeSpanValue.Hours, mins = this.TimeSpanValue.Minutes,
                    secs = this.TimeSpanValue.Seconds;

                if(hours < 0 || mins < 0 || secs < 0)
                    tz = String.Format(CultureInfo.InvariantCulture, "-{0:00}{1:00}", hours * -1, mins * -1);
                else
                    tz = String.Format(CultureInfo.InvariantCulture, "+{0:00}{1:00}", hours, mins);

                if(secs != 0)
                {
                    if(secs < 0)
                        secs *= -1;

                    tz += String.Format(CultureInfo.InvariantCulture, "{0:00}", secs);
                }

                return tz;
            }
            set
            {
                if(value != null && value.Length > 0)
                    this.TimeSpanValue = DateUtils.FromISO8601TimeZone(value);
                else
                    this.TimeSpanValue = new TimeSpan(0);
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the time span to/from its string form
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
        /// <param name="isOffsetFrom">Specify true if this is <c>TZOFFSETFROM</c> property or false if it is a
        /// <c>TZOFFSETTO</c> property.</param>
        public TimeZoneOffsetProperty(bool isOffsetFrom)
        {
            this.Version = SpecificationVersions.iCalendar20;
            this.isOffsetFrom = isOffsetFrom;
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
            TimeZoneOffsetProperty o = new(isOffsetFrom);
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
