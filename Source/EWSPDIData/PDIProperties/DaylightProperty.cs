//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : DaylightProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/21/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Daylight property used by the Personal Data Interchange (PDI) vCalendar classes
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

using System;
using System.Globalization;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the daylight saving time rule (DAYLIGHT) property of a vCalendar object.
    /// This defines the rule used by the "home" calendaring system to represent daylight saving time.
    /// </summary>
    /// <remarks>This property is only valid for use with the vCalendar 1.0 specification.  This property class
    /// parses the <see cref="Value"/> property and allows access to the component parts.  The value consists
    /// of the daylight saving time flag, followed by the daylight saving time offset, followed by the date and
    /// time that the daylight saving time begins, followed by the date and time that the daylight saving time
    /// ends, followed by the standard time designation, followed by the daylight saving time designation.  The
    /// time offset and date/time values are specified in ISO 8601 format.</remarks>
    public class DaylightProperty : BaseProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCalendar 1.0 only</value>
        public override SpecificationVersions VersionsSupported
        {
            get { return SpecificationVersions.vCalendar10; }
        }

        /// <summary>
        /// This read-only property defines the tag (DAYLIGHT)
        /// </summary>
        public override string Tag
        {
            get { return "DAYLIGHT"; }
        }

        /// <summary>
        /// This read-only property defines the default value location as INLINE
        /// </summary>
        public override string DefaultValueLocation
        {
            get { return ValLocValue.Inline; }
        }

        /// <summary>
        /// This property is used to set or get the daylight saving time flag
        /// </summary>
        /// <value>Daylight saving time is used if true, false if not</value>
        public bool UsesDaylightSavingTime { get; set; }

        /// <summary>
        /// This is used to get or set the time offset as a <see cref="System.TimeSpan"/> object
        /// </summary>
        public TimeSpan Offset { get; set; }

        /// <summary>
        /// This is used to get or set the starting date/time of daylight saving time
        /// </summary>
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// This is used to get or set the ending date/time of daylight saving time
        /// </summary>
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// This is used to get or set the standard time designation (i.e. PST, EST)
        /// </summary>
        public string StandardDesignation { get; set; }

        /// <summary>
        /// This is used to get or set the daylight saving time designation (i.e. PDT, EDT)
        /// </summary>
        public string DaylightDesignation { get; set; }

        /// <summary>
        /// This property is overridden to handle parsing the components and concatenating them when requested
        /// </summary>
        public override string Value
        {
            get
            {
                string[] parts = new string[6];

                // If false, don't bother
                if(!this.UsesDaylightSavingTime)
                    return null;

                parts[0] = "TRUE";

                int hours = this.Offset.Hours, mins = this.Offset.Minutes, secs = this.Offset.Seconds;

                if(hours < 0 || mins < 0 || secs < 0)
                    parts[1] = String.Format(CultureInfo.InvariantCulture, "-{0:00}:{1:00}", hours * -1, mins * -1);
                else
                    parts[1] = String.Format(CultureInfo.InvariantCulture, "+{0:00}:{1:00}", hours, mins);

                if(secs != 0)
                {
                    if(secs < 0)
                        secs *= -1;

                    parts[1] += String.Format(CultureInfo.InvariantCulture, ":{0:00}", secs);
                }

                // The spec doesn't say but I'm assuming these should always be in floating format, not universal
                // time.
                parts[2] = this.StartDateTime.ToString(ISO8601Format.BasicDateTimeLocal, CultureInfo.InvariantCulture);
                parts[3] = this.EndDateTime.ToString(ISO8601Format.BasicDateTimeLocal, CultureInfo.InvariantCulture);

                parts[4] = this.StandardDesignation;
                parts[5] = this.DaylightDesignation;

                return String.Join(";", parts);
            }
            set
            {
                string[] parts;

                this.UsesDaylightSavingTime = false;

                if(value != null && value.Length > 0)
                {
                    parts = value.Split(';');

                    if(parts.Length > 0 && String.Compare(parts[0].Trim(), "TRUE", StringComparison.OrdinalIgnoreCase) == 0)
                        this.UsesDaylightSavingTime = true;

                    if(parts.Length > 1)
                        this.Offset = DateUtils.FromISO8601TimeZone(parts[1]);

                    if(parts.Length > 2)
                        this.StartDateTime = DateUtils.FromISO8601String(parts[2], true);

                    if(parts.Length > 3)
                        this.EndDateTime = DateUtils.FromISO8601String(parts[3], true);

                    if(parts.Length > 4)
                        this.StandardDesignation = parts[4];

                    if(parts.Length > 5)
                        this.DaylightDesignation = parts[5];
                }
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the components and concatenating them when requested
        /// </summary>
        public override string EncodedValue
        {
            get { return this.Value; }
            set { this.Value = value; }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public DaylightProperty()
        {
            this.Version = SpecificationVersions.vCalendar10;
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
            DaylightProperty o = new DaylightProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
