//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : DateTimeInstance.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/02/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains the definition for the date/time instance class used by the time zone conversion methods
// in the VCalendar class and the recurring instance generation methods of the RecurringObject class.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 10/23/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.Text;
using System.Text.RegularExpressions;

using EWSoftware.PDI.Objects;

namespace EWSoftware.PDI
{
    /// <summary>
    /// This class is returned by the time zone conversion functions in the <see cref="Objects.VCalendar"/>
    /// class.  It is also used by the <see cref="Objects.RecurringObject"/>-derived classes to return a
    /// collection of recurring instances.
    /// </summary>
    /// <seealso cref="VCalendar.TimeZoneTimeToLocalTime"/>
    /// <seealso cref="VCalendar.LocalTimeToTimeZoneTime"/>
    /// <seealso cref="VCalendar.TimeZoneTimeToUtc"/>
    /// <seealso cref="VCalendar.UtcToTimeZoneTime"/>
    /// <seealso cref="VCalendar.TimeZoneToTimeZone"/>
    /// <seealso cref="RecurringObject"/>
    public class DateTimeInstance
    {
        #region Private data members
        //=====================================================================

        private static readonly Regex reSplit = new(@"\s+");

        private DateTime startDate, endDate;
        private Duration duration;
        private bool startIsDST, endIsDST;
        private string? timeZoneID;
        private string startTZName, endTZName;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to get or set the time zone ID
        /// </summary>
        /// <value>The value can be used to look up a <see cref="VTimeZone"/> object in a
        /// <see cref="VTimeZoneCollection"/>.  If this is null, the times are assumed to be in local time.</value>
        public string? TimeZoneId
        {
            get => timeZoneID;
            set
            {
                if(value == null || value.Length == 0)
                    timeZoneID = null;
                else
                    timeZoneID = value;
            }
        }

        /// <summary>
        /// This is used to get or set the start date/time value
        /// </summary>
        public DateTime StartDateTime
        {
            get => startDate;
            set => startDate = value;
        }

        /// <summary>
        /// This is used to get or set the end date/time value
        /// </summary>
        public DateTime EndDateTime
        {
            get => endDate;
            set => endDate = value;
        }

        /// <summary>
        /// This is used to get or set the instance's duration
        /// </summary>
        public Duration Duration
        {
            get => duration;
            set => duration = value;
        }

        /// <summary>
        /// This is used to get or set whether or not the start date/time is in daylight saving time
        /// </summary>
        public bool StartIsDaylightSavingTime
        {
            get => startIsDST;
            set => startIsDST = value;
        }

        /// <summary>
        /// This is used to get or set whether or not the end date/time is in daylight saving time
        /// </summary>
        public bool EndIsDaylightSavingTime
        {
            get => endIsDST;
            set => endIsDST = value;
        }

        /// <summary>
        /// This is used to get or set the time zone name description for the start date/time value (Eastern
        /// Daylight Time, Pacific Standard Time, etc).
        /// </summary>
        public string StartTimeZoneName
        {
            get => startTZName;
            set
            {
                if(value == null)
                    startTZName = String.Empty;
                else
                    startTZName = value;
            }
        }

        /// <summary>
        /// This is used to get or set the time zone name description for the end date/time value (Eastern
        /// Daylight Time, Pacific Standard Time, etc).
        /// </summary>
        public string EndTimeZoneName
        {
            get => endTZName;
            set
            {
                if(value == null)
                    endTZName = String.Empty;
                else
                    endTZName = value;
            }
        }

        /// <summary>
        /// This read-only property is used to get the start time zone name in its abbreviated form
        /// </summary>
        /// <remarks>This is useful when <see cref="StartTimeZoneName"/> is set to a full description.  The
        /// property simply concatenates the first letter of every word in the description.  For example,
        /// Pacific Standard Time is returned as PST.  If there is only one word, it is assumed to be in
        /// abbreviated form already and that is returned instead.</remarks>
        public string AbbreviatedStartTimeZoneName
        {
            get
            {
                string[] parts = reSplit.Split(startTZName);
                int count = 0;

                if(parts.Length == 1)
                    return parts[0];

                char[] letters = new char[parts.Length];

                for(int idx = 0; idx < parts.Length; idx++)
                {
                    if(Char.IsLetter(parts[idx][0]))
                    {
                        letters[idx] = parts[idx][0];
                        count++;
                    }
                }

                return new String(letters, 0, count);
            }
        }

        /// <summary>
        /// This read-only property is used to get the end time zone name in its abbreviated form
        /// </summary>
        /// <remarks>This is useful when <see cref="EndTimeZoneName"/> is set to a full description.  The
        /// property simply concatenates the first letter of every word in the description.  For example,
        /// Pacific Standard Time is returned as PST.  If there is only one word, it is assumed to be in
        /// abbreviated form already and that is returned instead.</remarks>
        public string AbbreviatedEndTimeZoneName
        {
            get
            {
                string[] parts = reSplit.Split(endTZName);
                int count = 0;

                if(parts.Length == 1)
                    return parts[0];

                char[] letters = new char[parts.Length];

                for(int idx = 0; idx < parts.Length; idx++)
                {
                    if(Char.IsLetter(parts[idx][0]))
                    {
                        letters[idx] = parts[idx][0];
                        count++;
                    }
                }

                return new String(letters, 0, count);
            }
        }
        #endregion

        #region Constructors
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dt">The start date/time for the time zone</param>
        /// <remarks>The duration is set to zero, the Daylight Saving Time flags are set to false and the time
        /// zone names default to an empty string.</remarks>
        /// <overloads>There are three overloads for the constructor</overloads>
        public DateTimeInstance(DateTime dt) : this(dt, Duration.Zero)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dt">The start date/time for the time zone</param>
        /// <param name="dur">The duration of the instance</param>
        /// <remarks>The Daylight Saving Time flags are set to false and the time zone names default to an empty
        /// string.</remarks>
        public DateTimeInstance(DateTime dt, Duration dur)
        {
            startDate = dt;
            endDate = dt.Add(dur.TimeSpan);
            duration = dur;
            startIsDST = endIsDST = false;
            startTZName = endTZName = String.Empty;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="dateTimeInstance">The date/time instance to copy</param>
        public DateTimeInstance(DateTimeInstance? dateTimeInstance)
        {
            if(dateTimeInstance != null)
            {
                timeZoneID = dateTimeInstance.TimeZoneId;
                startDate = dateTimeInstance.StartDateTime;
                endDate = dateTimeInstance.EndDateTime;
                duration = dateTimeInstance.Duration;
                startIsDST = dateTimeInstance.StartIsDaylightSavingTime;
                endIsDST = dateTimeInstance.EndIsDaylightSavingTime;
                startTZName = dateTimeInstance.StartTimeZoneName;
                endTZName = dateTimeInstance.EndTimeZoneName;
            }
            else
                startTZName = endTZName = String.Empty;
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is overridden to allow proper comparison of <c>DateTimeInstance</c> objects
        /// </summary>
        /// <param name="obj">The object to which this instance is compared</param>
        /// <returns>Returns true if the object equals this instance, false if it does not</returns>
        public override bool Equals(object obj)
        {
            if(obj is not DateTimeInstance dti)
                return false;

            return startDate == dti.StartDateTime && endDate == dti.EndDateTime && duration == dti.Duration &&
                  startIsDST == dti.StartIsDaylightSavingTime && endIsDST == dti.EndIsDaylightSavingTime &&
                  startTZName == dti.StartTimeZoneName && endTZName == dti.EndTimeZoneName;
        }

        /// <summary>
        /// Get a hash code for the DateTimeInstance object
        /// </summary>
        /// <returns>Returns the hash code for the object</returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// This converts the date/time info to string form
        /// </summary>
        /// <returns>A string containing the object information</returns>
        public override string ToString()
        {
            StringBuilder sb = new(256);

            sb.AppendFormat("Start Date: {0} {1}\r\n", startDate, startTZName);
            sb.AppendFormat("End Date: {0} {1}\r\n", endDate, endTZName);
            sb.AppendFormat("Duration: {0}\r\n", duration);
            sb.AppendFormat("Start Is DST: {0}\r\n", startIsDST);
            sb.AppendFormat("End Is DST: {0}\r\n", endIsDST);

            return sb.ToString();
        }

        /// <summary>
        /// This converts the date/time info to local time
        /// </summary>
        /// <remarks>This converts the date time instance information from its current time zone identified by
        /// the <see cref="TimeZoneId"/> property to local time.  The time zone ID property will be set to null
        /// after conversion to indicate that the instance is in local time.</remarks>
        public void ToLocalTime()
        {
            DateTimeInstance dti, dtiEnd;

            // Already in local time?
            if(timeZoneID == null)
                return;

            dti = VCalendar.TimeZoneTimeToLocalTime(startDate, timeZoneID);
            dtiEnd = VCalendar.TimeZoneTimeToLocalTime(endDate, timeZoneID);

            timeZoneID = null;
            startDate = dti.StartDateTime;
            startIsDST = dti.StartIsDaylightSavingTime;
            startTZName = dti.StartTimeZoneName;
            endDate = dtiEnd.EndDateTime;
            endIsDST = dtiEnd.EndIsDaylightSavingTime;
            endTZName = dtiEnd.EndTimeZoneName;
        }

        /// <summary>
        /// This converts the date/time info to the specified time zone
        /// </summary>
        /// <param name="tzid">The time zone to which to convert this instance</param>
        /// <remarks>This converts the date time instance information from its current time zone identified by
        /// the <see cref="TimeZoneId"/> property to the specified time zone.  The time zone ID property will be
        /// set to the specified time zone after conversion.</remarks>
        public void ToTimeZone(string? tzid)
        {
            DateTimeInstance dti, dtiEnd;

            // Already in the specified time zone?
            if(timeZoneID == tzid)
                return;

            // Convert from local time?
            if(timeZoneID == null)
            {
                dti = VCalendar.LocalTimeToTimeZoneTime(startDate, tzid!);
                dtiEnd = VCalendar.LocalTimeToTimeZoneTime(endDate, tzid!);
            }
            else
            {
                dti = VCalendar.TimeZoneToTimeZone(startDate, timeZoneID, tzid);
                dtiEnd = VCalendar.TimeZoneToTimeZone(endDate, timeZoneID, tzid);
            }

            timeZoneID = dti.TimeZoneId;
            startDate = dti.StartDateTime;
            startIsDST = dti.StartIsDaylightSavingTime;
            startTZName = dti.StartTimeZoneName;
            endDate = dtiEnd.EndDateTime;
            endIsDST = dtiEnd.EndIsDaylightSavingTime;
            endTZName = dtiEnd.EndTimeZoneName;
        }
        #endregion
    }
}
