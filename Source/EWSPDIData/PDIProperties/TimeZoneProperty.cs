//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : TimeZoneProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/04/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains Time Zone property class used by the Personal Data Interchange (PDI) classes such as
// vCalendar and vCard.
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
using System.Globalization;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Time Zone (TZ) property of a vCard or vCalendar object
    /// </summary>
    /// <remarks>This property class parses the <see cref="BaseProperty.Value"/> property to allow access to its
    /// content as an actual <see cref="System.TimeSpan"/> object.  The time zone is a string specified in a
    /// manner consistent with ISO 8601. It is an offset from Coordinated Universal Time (UTC).</remarks>
    public class TimeZoneProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private TimeSpan timeSpan;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports all vCard specifications and vCalendar 1.0</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll |
            SpecificationVersions.vCalendar10;

        /// <summary>
        /// This read-only property defines the tag (TZ)
        /// </summary>
        public override string Tag => "TZ";

        /// <summary>
        /// This read-only property defines the default value type as UTC-OFFSET
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.UtcOffset;

        /// <summary>
        /// This is used to get or set the time zone as a <see cref="System.TimeSpan"/> object
        /// </summary>
        /// <value>If set to a time zone value, any underlying text value that includes descriptive time zone
        /// text will be lost (VALUE will be reset from TEXT to UTC-OFFSET).</value>
        public TimeSpan TimeSpanValue
        {
            get => timeSpan;
            set
            {
                timeSpan = value;

                string tz;
                int hours = timeSpan.Hours, mins = timeSpan.Minutes, secs = timeSpan.Seconds;

                this.ValueLocation = ValLocValue.UtcOffset;

                if(hours < 0 || mins < 0 || secs < 0)
                    tz = String.Format(CultureInfo.InvariantCulture, "-{0:00}:{1:00}", hours * -1, mins * -1);
                else
                    tz = String.Format(CultureInfo.InvariantCulture, "+{0:00}:{1:00}", hours, mins);

                if(secs != 0)
                {
                    if(secs < 0)
                        secs *= -1;

                    tz += String.Format(CultureInfo.InvariantCulture, ":{0:00}", secs);
                }

                base.Value = tz;
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the time zone to/from its string form
        /// </summary>
        /// <value>The value assigned can be a time zone value alone or a time zone value with descriptive text
        /// (VALUE=TEXT).</value>
        public override string? Value
        {
            get
            {
                // If empty, nothing is saved.
                if(base.Value == null || base.Value.Length == 0)
                    return null;

                return base.Value;
            }
            set
            {
                // If not a UTC offset, don't set the time span
                if(this.ValueLocation == ValLocValue.UtcOffset && !String.IsNullOrWhiteSpace(value))
                    timeSpan = DateUtils.FromISO8601TimeZone(value!);
                else
                    timeSpan = TimeSpan.MinValue;

                // Store the string too as it may include descriptive text
                base.Value = value;
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the time zone to/from its string form
        /// </summary>
        public override string? EncodedValue
        {
            get
            {
                // If empty, nothing is saved.
                if(base.EncodedValue == null || base.EncodedValue.Length == 0)
                    return null;

                return base.EncodedValue;
            }
            set
            {
                // If not a UTC offset, don't set the time span
                if(this.ValueLocation == ValLocValue.UtcOffset && !String.IsNullOrWhiteSpace(value))
                    timeSpan = DateUtils.FromISO8601TimeZone(value!);
                else
                    timeSpan = TimeSpan.MinValue;

                // Store the string too as it may include descriptive text
                base.EncodedValue = value;
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        public TimeZoneProperty()
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
            TimeZoneProperty o = new();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
