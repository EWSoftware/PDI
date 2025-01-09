//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : BaseDateTimeProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/02/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains an abstract base date/time property class used to create the other date/time property
// classes.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/30/2004  EFW  Created the code
// 07/24/2006  EFW  Added UtcDateTime property
//===============================================================================================================

using System;
using System.Globalization;
using System.Text;

using EWSoftware.PDI.Objects;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used as a base class for the various date/time properties
    /// </summary>
    /// <remarks>This property class parses the <see cref="BaseProperty.Value"/> property to allow access to its
    /// content as an actual <see cref="System.DateTime"/> object.  The time zone ID property (TZID) can also be
    /// set or retrieved (iCalendar only).</remarks>
    public abstract class BaseDateTimeProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private DateTime propDate;
        private string? timeZoneId;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to get or set whether or not the date/time is a floating value (assumed to be local
        /// time).
        /// </summary>
        /// <value><para>If false (the default) and there is no time zone ID, the value is considered to be
        /// expressed in universal time.  If the date/time value is retrieved or set via the <see cref="Value"/>
        /// property, it will be converted to/from local time accordingly.</para>
        /// 
        /// <para>If set to true and there is no time zone ID, the date/time is considered to be a floating value
        /// that is not dependent on any particular time zone.  It will be interpreted as a local time regardless
        /// of the system's current time zone.  It will not be converted to/from universal time by the
        /// <see cref="Value"/> property.</para></value>
        public bool IsFloating { get; set; }

        /// <summary>
        /// This is used to get or set the time zone ID (TZID) parameter value
        /// </summary>
        /// <value>This property is only applicable to iCalendar 2.0 date/time objects.  If set to a non-blank
        /// value, the time part of the property value is considered to be expressed in local time for the given
        /// time zone rather than universal time.  The ID should match a time zone ID on a VTIMEZONE component in
        /// the owning calendar.  If set, the <see cref="IsFloating"/> property is ignored.</value>
        public string? TimeZoneId
        {
            get => timeZoneId;
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                    timeZoneId = value;
                else
                    timeZoneId = null;
            }
        }

        /// <summary>
        /// This is used to get or set the calendar scale (CALSCALE) parameter value
        /// </summary>
        /// <value>This property is only applicable to vCard 4.0 date/time objects</value>
        public string? CalendarScale { get; set; }

        /// <summary>
        /// This is used to get or set the value as a <see cref="System.DateTime"/> object expressed in the
        /// specified <see cref="TimeZoneId"/> rather than local time on the current system.
        /// </summary>
        /// <value>For non-iCalendar objects or when a time zone ID has not been specified, the value is always
        /// in local time.</value>
        /// <seealso cref="DateTimeValue"/>
        public virtual DateTime TimeZoneDateTime
        {
            get => propDate;
            set => propDate = value;
        }

        /// <summary>
        /// This is used to get or set the value as a <see cref="System.DateTime"/> object expressed in Universal
        /// Time.
        /// </summary>
        /// <value>If a time zone ID has been specified, an attempt is made to convert the underlying date/time
        /// to/from Universal Time based on the time zone settings.  A VTIMEZONE component with a matching time
        /// zone ID must exist in the owning calendar for this to occur.  If one cannot be found (and for
        /// non-iCalendar objects), the value is not modified and will be treated as local time for the
        /// conversion.</value>
        public virtual DateTime UtcDateTime
        {
            get
            {
                if(propDate == DateTime.MinValue)
                    return propDate;

                if(timeZoneId == null || this.Version != SpecificationVersions.iCalendar20)
                    return propDate.ToUniversalTime();

                // Convert to universal time based on TZID
                return VCalendar.TimeZoneTimeToUtc(propDate, timeZoneId);
            }
            set
            {
                if(value == DateTime.MinValue)
                    propDate = value;
                else
                    if(timeZoneId == null || this.Version != SpecificationVersions.iCalendar20)
                        propDate = value.ToLocalTime();
                    else
                    {
                        // Convert to time zone time based on TZID
                        propDate = VCalendar.UtcToTimeZoneTime(value, timeZoneId).StartDateTime;
                    }
            }
        }

        /// <summary>
        /// This is used to get or set the value as a <see cref="System.DateTime"/> object expressed in local
        /// time.
        /// </summary>
        /// <value>For non-iCalendar objects or if a <see cref="TimeZoneId"/> has not been specified, this acts
        /// the same as <see cref="TimeZoneDateTime"/>.  If a time zone ID has been specified, an attempt is made
        /// to convert the underlying date/time to/from local time based on the time zone settings.  A VTIMEZONE
        /// component with a matching time zone ID must exist in the owning calendar for this to occur.  If one
        /// cannot be found, the value is not modified and will be treated as local time.</value>
        public virtual DateTime DateTimeValue
        {
            get
            {
                if(propDate == DateTime.MinValue || timeZoneId == null || this.Version != SpecificationVersions.iCalendar20)
                    return propDate;

                // Convert to local time based on TZID
                return VCalendar.TimeZoneTimeToLocalTime(propDate, timeZoneId).StartDateTime;
            }
            set
            {
                if(value == DateTime.MinValue || timeZoneId == null || this.Version != SpecificationVersions.iCalendar20)
                {
                    propDate = value;
                    return;
                }

                // Convert to time zone time based on TZID
                propDate = VCalendar.LocalTimeToTimeZoneTime(value, timeZoneId).StartDateTime;
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the date/time to/from its string form
        /// </summary>
        public override string? Value
        {
            get
            {
                if(this.ValueLocation == ValLocValue.Text)
                    return base.Value;

                DateTime dtDate = propDate;
                string format;

                // If equal to minimum date, nothing will be saved
                if(dtDate == DateTime.MinValue)
                    return null;

                // Only save the date part if the value location is DATE
                if(this.ValueLocation == ValLocValue.Date)
                {
                    // vCard 3.0 and later use the extended format.  All others use the basic format.
                    if(this.Version == SpecificationVersions.vCard30 || this.Version == SpecificationVersions.vCard40)
                        format = ISO8601Format.ExtendedDate;
                    else
                        format = ISO8601Format.BasicDate;
                }
                else
                {
                    if(this.Version == SpecificationVersions.vCard30 || this.Version == SpecificationVersions.vCard40)
                    {
                        // Floating uses the local time format
                        if(this.IsFloating)
                            format = ISO8601Format.ExtendedDateTimeLocal;
                        else
                        {
                            dtDate = dtDate.ToUniversalTime();
                            format = ISO8601Format.ExtendedDateTimeUniversal;
                        }
                    }
                    else
                    {
                        // iCalendar with time zone parameter uses local time format.  So does a floating
                        // date/time.
                        if(this.IsFloating || (this.Version == SpecificationVersions.iCalendar20 && timeZoneId != null))
                            format = ISO8601Format.BasicDateTimeLocal;
                        else
                        {
                            dtDate = dtDate.ToUniversalTime();
                            format = ISO8601Format.BasicDateTimeUniversal;
                        }
                    }
                }

                return dtDate.ToString(format, CultureInfo.InvariantCulture);
            }
            set
            {
                this.IsFloating = false;

                if(value != null && value.Length > 0)
                {
                    // We don't handle missing date parts (i.e. --0517).  Default them to text values.
                    if(value.IndexOf("--", StringComparison.Ordinal) != -1)
                        this.ValueLocation = ValLocValue.Text;

                    if(this.ValueLocation == ValLocValue.Text)
                        base.Value = value;
                    else
                    {
                        propDate = DateUtils.FromISO8601String(value, true);

                        if(this.ValueLocation == ValLocValue.DateTime && timeZoneId == null)
                            this.IsFloating = DateUtils.IsFloatingFormat(value);
                    }
                }
                else
                {
                    propDate = DateTime.MinValue;
                    base.Value = null;
                }
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the date/time to/from its string form
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
        protected BaseDateTimeProperty()
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            var clone = (BaseDateTimeProperty)p;

            timeZoneId = clone.TimeZoneId;
            this.CalendarScale = clone.CalendarScale;

            base.Clone(p);
        }

        /// <summary>
        /// This is overridden to provide custom handling of the TZID parameter
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        public override void SerializeParameters(StringBuilder sb)
        {
            base.SerializeParameters(sb);

            // Serialize the time zone ID if necessary.  It is enclosed in quotes if it contains a comma,
            // semi-colon, or a colon.
            if(this.Version == SpecificationVersions.iCalendar20 && timeZoneId != null)
            {
                sb.Append(';');
                sb.Append(ParameterNames.TimeZoneId);
                sb.Append('=');

                if(timeZoneId.IndexOfAny([',', ';', ':']) != -1)
                {
                    sb.Append('\"');
                    sb.Append(timeZoneId);
                    sb.Append('\"');
                }
                else
                    sb.Append(timeZoneId);
            }

            // Serialize the calendar scale if necessary
            if(this.Version == SpecificationVersions.vCard40 && !String.IsNullOrWhiteSpace(this.CalendarScale))
            {
                sb.Append(';');
                sb.Append(ParameterNames.CalendarScale);
                sb.Append('=');
                sb.Append(this.CalendarScale);
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
            {
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

                if(String.Compare(parameters[paramIdx], "CALSCALE=", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    // Remove the parameter name
                    parameters.RemoveAt(paramIdx);

                    if(paramIdx < parameters.Count)
                    {
                        this.CalendarScale = parameters[paramIdx];

                        // As above, remove the value
                        parameters.RemoveAt(paramIdx);
                    }
                    break;
                }
            }

            // Let the base class handle all other parameters
            base.DeserializeParameters(parameters);
        }
        #endregion
    }
}
