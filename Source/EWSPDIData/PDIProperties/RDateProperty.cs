//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : RDateProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/21/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class for the RDATE property used by the Personal Data Interchange (PDI) classes such as
// vCalendar and iCalendar.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 09/10/2004  EFW  Created the code
// 07/24/2006  EFW  Added UtcDateTime property
//===============================================================================================================

using System;
using System.Globalization;
using System.Text;

using EWSoftware.PDI.Objects;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent a Recur Date (RDATE) property of a calendar object that supports
    /// recurrence.
    /// </summary>
    /// <remarks>This property class parses the <see cref="BaseProperty.Value"/> property to allow access to its
    /// content as an actual <see cref="System.DateTime"/> or <see cref="PDI.Period"/> object.  The time zone ID
    /// property (TZID) can also be set or retrieved when the value is a date/time (iCalendar only).</remarks>
    public class RDateProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private Period period;
        private string timeZoneId;
        private bool isFloating;
        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCalendar 1.0 and iCalendar 2.0</value>
        public override SpecificationVersions VersionsSupported
        {
            get { return SpecificationVersions.vCalendar10 | SpecificationVersions.iCalendar20; }
        }

        /// <summary>
        /// This read-only property defines the tag (RDATE)
        /// </summary>
        public override string Tag
        {
            get { return "RDATE"; }
        }

        /// <summary>
        /// This read-only property defines the default value type as DATE-TIME
        /// </summary>
        public override string DefaultValueLocation
        {
            get { return ValLocValue.DateTime; }
        }

        /// <summary>
        /// This is used to set or get whether the date/time is floating
        /// </summary>
        /// <value><para>If false (the default) and there is no time zone ID, the value is considered to be
        /// expressed in universal time.  If the date/time value is retrieved or set via the <see cref="Value"/>
        /// property, /// it will be converted to/from local time accordingly.</para>
        /// 
        /// <para>If set to true and there is no time zone ID, the date/time is considered to be a floating value
        /// that is not dependent on any particular time zone.  It will be interpreted as a local time regardless
        /// of the system's current time zone.  It will not be converted to/from universal time by the
        /// <see cref="Value"/> property.</para>
        /// 
        /// <para>This only applies when the value is a date/time, not a period.</para>
        /// </value>
        public bool IsFloating
        {
            get { return isFloating; }
            set { isFloating = value; }
        }

        /// <summary>
        /// This is used to get or set the time zone ID (TZID) parameter value
        /// </summary>
        /// <value>This property is only applicable to iCalendar 2.0 date/time objects.  If set to a non-blank
        /// value, the time part of the property value is considered to be expressed in local time for the given
        /// time zone rather than universal time.  The ID should match a time zone ID on a VTIMEZONE component in
        /// the owning calendar.  If set, the <see cref="IsFloating"/> property is ignored.</value>
        public string TimeZoneId
        {
            get { return timeZoneId; }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                    timeZoneId = value;
                else
                    timeZoneId = null;
            }
        }

        /// <summary>
        /// This is used to get or set the value as a <see cref="System.DateTime"/> object expressed in the
        /// specified <see cref="TimeZoneId"/> rather than local time on the current system.
        /// </summary>
        /// <value>For non-iCalendar objects or when a time zone ID has not been specified, the value is always
        /// in local time.  <p/>If set to a date/time, the value format is forced to date/time if it is currently
        /// set to period.</value>
        /// <seealso cref="DateTimeValue"/>
        public virtual DateTime TimeZoneDateTime
        {
            get { return this.PeriodValue.StartDateTime; }
            set
            {
                if(this.ValueLocation == ValLocValue.Period)
                    this.ValueLocation = ValLocValue.DateTime;

                this.PeriodValue.StartDateTime = this.PeriodValue.EndDateTime = value;
            }
        }

        /// <summary>
        /// This is used to get or set the value as a <see cref="System.DateTime"/> object expressed in Universal
        /// Time.
        /// </summary>
        /// <value>If a time zone ID has been specified, an attempt is made to convert the underlying date/time
        /// to/from Universal Time based on the time zone settings.  A VTIMEZONE component with a matching time
        /// zone ID must exist in the owning calendar for this to occur.  If one cannot be found (and for
        /// non-iCalendar objects or those in which the value format is a period), the value is not modified and
        /// will be treated as local time for the conversion.</value>
        public virtual DateTime UtcDateTime
        {
            get
            {
                DateTime dt = this.PeriodValue.StartDateTime;

                if(dt == DateTime.MinValue || this.ValueLocation == ValLocValue.Period)
                    return dt;

                if(timeZoneId == null || this.Version != SpecificationVersions.iCalendar20)
                    return dt.ToUniversalTime();

                // Convert to universal time based on TZID
                return VCalendar.TimeZoneTimeToUtc(dt, timeZoneId);
            }
            set
            {
                DateTime dt;

                if(this.ValueLocation == ValLocValue.Period)
                    this.ValueLocation = ValLocValue.DateTime;

                if(value == DateTime.MinValue)
                    dt = value;
                else
                    if(timeZoneId == null || this.Version != SpecificationVersions.iCalendar20)
                        dt = value.ToLocalTime();
                    else
                    {
                        // Convert to time zone time based on TZID
                        dt = VCalendar.UtcToTimeZoneTime(value, timeZoneId).StartDateTime;
                    }

                this.PeriodValue.StartDateTime = this.PeriodValue.EndDateTime = dt;
            }
        }

        /// <summary>
        /// This is used to get or set the value as a <see cref="System.DateTime"/> object expressed in local
        /// time.
        /// </summary>
        /// <value><para>For non-iCalendar objects or if a <see cref="TimeZoneId"/> has not been specified or if
        /// the value format is a period, this acts the same as <see cref="TimeZoneDateTime"/>.  If a time zone
        /// ID has been specified, an attempt is made to convert the underlying date/time to/from local time
        /// based on the time zone settings.  A VTIMEZONE component with a matching time zone ID must exist in
        /// the owning calendar for this to occur.  If one cannot be found, the value is not modified and will be
        /// treated as local time.</para>
        /// 
        /// <para>If set to a date/time, the value format is forced to date/time if it is currently set to
        /// period.</para></value>
        public DateTime DateTimeValue
        {
            get
            {
                DateTime dt = this.PeriodValue.StartDateTime;

                if(this.ValueLocation == ValLocValue.Period || dt == DateTime.MinValue || timeZoneId == null ||
                  this.Version != SpecificationVersions.iCalendar20)
                    return dt;

                // Convert to local time based on TZID
                return VCalendar.TimeZoneTimeToLocalTime(dt, timeZoneId).StartDateTime;
            }
            set
            {
                if(this.ValueLocation == ValLocValue.Period)
                    this.ValueLocation = ValLocValue.DateTime;

                if(value == DateTime.MinValue || timeZoneId == null || this.Version != SpecificationVersions.iCalendar20)
                {
                    this.PeriodValue.StartDateTime = this.PeriodValue.EndDateTime = value;
                    return;
                }

                // Convert to time zone time based on TZID
                this.PeriodValue.StartDateTime = this.PeriodValue.EndDateTime =
                    VCalendar.LocalTimeToTimeZoneTime(value, timeZoneId).StartDateTime;
            }
        }

        /// <summary>
        /// This is used to get or set the value as a <see cref="PDI.Period"/> object
        /// </summary>
        /// <value>If retrieved while formatted as a date/time, the duration will be zero.  If set to a period,
        /// the value format is forced to period.</value>
        public Period PeriodValue
        {
            get
            {
                if(period == null)
                    period = new Period();

                return period;
            }
            set
            {
                period = value;
                this.ValueLocation = ValLocValue.Period;
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the date/time or period to/from its string form
        /// </summary>
        public override string Value
        {
            get
            {
                string format;

                // If equal to minimum date or a duration less than 0, nothing will be saved
                if(this.ValueLocation == ValLocValue.Period)
                {
                    if(period == null || period.Duration.Ticks < 0)
                        return null;

                    // Periods are easy, they format themselves
                    return period.ToString();
                }

                if(period == null || period.StartDateTime == DateTime.MinValue)
                    return null;

                DateTime dtDate = period.StartDateTime;

                // Only save the date part if the value location is DATE
                if(this.ValueLocation == ValLocValue.Date)
                    format = ISO8601Format.BasicDate;
                else
                {
                    // iCalendar with time zone parameter uses local time format.  So does a floating date/time.
                    if(isFloating || (this.Version == SpecificationVersions.iCalendar20 && timeZoneId != null))
                        format = ISO8601Format.BasicDateTimeLocal;
                    else
                    {
                        dtDate = dtDate.ToUniversalTime();
                        format = ISO8601Format.BasicDateTimeUniversal;
                    }
                }

                return dtDate.ToString(format, CultureInfo.InvariantCulture);
            }
            set
            {
                isFloating = false;

                if(value != null && value.Length > 0)
                {
                    if(this.ValueLocation == ValLocValue.Period)
                        period = new Period(value);
                    else
                    {
                        DateTime dt = DateUtils.FromISO8601String(value, true);

                        if(this.ValueLocation == ValLocValue.DateTime && timeZoneId == null)
                            isFloating = DateUtils.IsFloatingFormat(value);

                        // We store it as a period internally to keep things simple.  No need to keep a separate
                        // date object in synch with the period start if the format changes.
                        period = new Period(dt, dt);
                    }
                }
                else
                    period = null;
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the date/time to/from its string form
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
        /// Constructor.  Unless the version is changed, the object will conform to the iCalendar 2.0
        /// specification.
        /// </summary>
        public RDateProperty()
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
            RDateProperty o = new RDateProperty();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            timeZoneId = ((RDateProperty)p).TimeZoneId;
            base.Clone(p);
        }

        /// <summary>
        /// This is overridden to provide custom handling of the TZID parameter
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        public override void SerializeParameters(StringBuilder sb)
        {
            base.SerializeParameters(sb);

            // Serialize the time zone ID if necessary
            if(this.ValueLocation == ValLocValue.DateTime && this.Version == SpecificationVersions.iCalendar20 &&
              timeZoneId != null)
            {
                sb.Append(';');
                sb.Append(ParameterNames.TimeZoneId);
                sb.Append('=');

                if(timeZoneId.IndexOfAny(new char[] { ',', ';', ':' }) != -1)
                {
                    sb.Append('\"');
                    sb.Append(timeZoneId);
                    sb.Append('\"');
                }
                else
                    sb.Append(timeZoneId);
            }
        }

        /// <summary>
        /// This is overridden to provide custom handling of the TZID parameter
        /// </summary>
        /// <param name="parameters">The parameters for the property</param>
        public override void DeserializeParameters(StringCollection parameters)
        {
            if(parameters == null || parameters.Count == 0)
                return;

            for(int paramIdx = 0; paramIdx < parameters.Count; paramIdx++)
                if(String.Compare(parameters[paramIdx], "TZID=", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    // Remove the parameter name
                    parameters.RemoveAt(paramIdx);

                    if(paramIdx < parameters.Count)
                    {
                        this.TimeZoneId = parameters[paramIdx];

                        // As above, remove the value
                        parameters.RemoveAt(paramIdx);
                    }
                    break;
                }

            // Let the base class handle all other parameters
            base.DeserializeParameters(parameters);
        }
        #endregion
    }
}
