//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : TimeZoneRegInfo.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/23/2018
// Note    : Copyright 2003-2018, Eric Woodruff, All rights reserved
// Compiler: Visual C#
//
// This class is used to create a time zone information from the time zone data found in the registry on Windows
// PCs.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 01/05/2005  EFW  Factored out the code to share between applications
//===============================================================================================================

// Ignore Spelling: Dlt

using System;
using System.Runtime.InteropServices;

using Microsoft.Win32;

using EWSoftware.PDI.Objects;
using EWSoftware.PDI.Properties;

namespace EWSoftware.PDI
{
    /// <summary>
	/// This class can be used to create a set of VTimeZone components from time zone information in the registry
	/// </summary>
	public static class TimeZoneRegInfo
	{
        #region Win32 SYSTEMTIME structure
        //=====================================================================

        /// <summary>
        /// This is used to represent a system date/time in the time zone registry structure
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack=2)]
        private struct SYSTEMTIME
        {
            public Int16 wYear;
            public Int16 wMonth;
            public Int16 wDayOfWeek;
            public Int16 wDay;
            public Int16 wHour;
            public Int16 wMinute;
            public Int16 wSecond;
            public Int16 wMilliseconds;

            public DateTime ToDateTime()
            {
                return new DateTime(wYear, wMonth, wDay, wHour, wMinute, wSecond, wMilliseconds);
            }

            public DateTime ToDateTime(int specificYear)
            {
                return DateUtils.CalculateFloatingDate(specificYear, wMonth,
                    (wDay > 4) ? DayOccurrence.Last : (DayOccurrence)wDay, (DayOfWeek)wDayOfWeek, 0).Add(
                        new TimeSpan(0, wHour, wMinute, wSecond, wMilliseconds));
            }

            public DaysOfWeek DayOfWeek()
            {
                switch(wDayOfWeek)
                {
                    case 0:
                        return DaysOfWeek.Sunday;

                    case 1:
                        return DaysOfWeek.Monday;

                    case 2:
                        return DaysOfWeek.Tuesday;

                    case 3:
                        return DaysOfWeek.Wednesday;

                    case 4:
                        return DaysOfWeek.Thursday;

                    case 5:
                        return DaysOfWeek.Friday;

                    default:
                        return DaysOfWeek.Saturday;
                }
            }
        }
        #endregion

        #region Win32 TIMEZONE structure
        //=====================================================================

        /// <summary>
        /// This represents time zone information for area.  The information is retrieved from the registry.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack=2)]
        private struct TIMEZONE
        {
            public Int32 nBias;
            public Int32 nStandardBias;
            public Int32 nDaylightBias;
            public SYSTEMTIME standardDate;
            public SYSTEMTIME daylightDate;

            public static TIMEZONE FromRegistry(object timeZoneInfo)
            {
                byte[] tziBytes = (byte[])timeZoneInfo;
                int size = tziBytes.Length;
                IntPtr buffer = IntPtr.Zero;
                TIMEZONE tzi;

                try
                {
                    buffer = Marshal.AllocHGlobal(size);

                    Marshal.Copy(tziBytes, 0, buffer, size);

                    tzi = (TIMEZONE)Marshal.PtrToStructure(buffer, typeof(TIMEZONE));
                }
                finally
                {
                    if(buffer != IntPtr.Zero)
                        Marshal.FreeHGlobal(buffer);
                }

                return tzi;
            }
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is used to load the VCalendar.TimeZones collection with time zone information from the registry
        /// </summary>
        public static void LoadTimeZoneInfo()
        {
            string keyName, display, standardDesc, dstDesc;
            TIMEZONE tz;

            VCalendar.TimeZones.Clear();

            // To keep things simple, we'll load the time zone data from the settings available in the registry.
            // We could take it a step further and load it from something like a copy of the Olson time zone
            // database but I haven't been that ambitious yet.
            if(Environment.OSVersion.Platform == PlatformID.Win32NT)
                keyName = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Time Zones";
            else
                keyName = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Time Zones";

            using(var rk = Registry.LocalMachine.OpenSubKey(keyName))
            {
                foreach(string s in rk.GetSubKeyNames())
                {
                    using(var rsk = rk.OpenSubKey(s))
                    {
                        display = (string)rsk.GetValue("Display");
                        standardDesc = (string)rsk.GetValue("Std");
                        dstDesc = (string)rsk.GetValue("Dlt");
                        tz = TIMEZONE.FromRegistry(rsk.GetValue("TZI"));
                    }

                    // Create the time zone object
                    VTimeZone vtz = new VTimeZone();
                    vtz.TimeZoneId.Value = display;

                    ObservanceRule or = vtz.ObservanceRules.Add(ObservanceRuleType.Standard);

                    or.OffsetFrom.TimeSpanValue = TimeSpan.FromMinutes(tz.nBias + tz.nDaylightBias).Negate();
                    or.OffsetTo.TimeSpanValue = TimeSpan.FromMinutes(tz.nBias + tz.nStandardBias).Negate();
                    or.TimeZoneNames.Add(standardDesc);

                    // If the standard date month is zero, it doesn't use standard time.  Assume 01/01/1970 and
                    // set it up to return the offset.
                    if(tz.standardDate.wMonth == 0)
                        or.StartDateTime.DateTimeValue = new DateTime(1970, 1, 1);
                    else
                    {
                        // If year is zero, its a recurrence.  If not zero, it's a fixed date.
                        if(tz.standardDate.wYear == 0)
                        {
                            or.StartDateTime.DateTimeValue = tz.standardDate.ToDateTime(1970);

                            RRuleProperty rrule = new RRuleProperty();

                            rrule.Recurrence.RecurYearly(
                                (tz.standardDate.wDay > 4) ? DayOccurrence.Last : (DayOccurrence)tz.standardDate.wDay,
                                tz.standardDate.DayOfWeek(), tz.standardDate.wMonth, 1);

                            or.RecurrenceRules.Add(rrule);
                        }
                        else
                        {
                            or.StartDateTime.DateTimeValue = tz.standardDate.ToDateTime();
                            or.RecurDates.Add(or.StartDateTime.DateTimeValue);
                        }
                    }

                    // If the daylight month is zero, it doesn't use DST.  The standard rule will handle
                    // everything.
                    if(tz.daylightDate.wMonth != 0)
                    {
                        or = vtz.ObservanceRules.Add(ObservanceRuleType.Daylight);

                        or.OffsetFrom.TimeSpanValue = TimeSpan.FromMinutes(tz.nBias + tz.nStandardBias).Negate();
                        or.OffsetTo.TimeSpanValue = TimeSpan.FromMinutes(tz.nBias + tz.nDaylightBias).Negate();
                        or.TimeZoneNames.Add(dstDesc);

                        // If year is zero, its a recurrence.  If not zero, it's a fixed date.
                        if(tz.daylightDate.wYear == 0)
                        {
                            or.StartDateTime.DateTimeValue = tz.daylightDate.ToDateTime(1970);

                            RRuleProperty rrule = new RRuleProperty();

                            rrule.Recurrence.RecurYearly(
                                (tz.daylightDate.wDay > 4) ? DayOccurrence.Last : (DayOccurrence)tz.daylightDate.wDay,
                                tz.daylightDate.DayOfWeek(), tz.daylightDate.wMonth, 1);

                            or.RecurrenceRules.Add(rrule);
                        }
                        else
                        {
                            or.StartDateTime.DateTimeValue = tz.daylightDate.ToDateTime();
                            or.RecurDates.Add(or.StartDateTime.DateTimeValue);
                        }
                    }

                    VCalendar.TimeZones.Add(vtz);
                }
            }

            // Put the time zones in sorted order
            VCalendar.TimeZones.Sort(true);
        }
        #endregion
    }
}
