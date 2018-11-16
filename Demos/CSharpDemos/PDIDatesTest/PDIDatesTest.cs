//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : PDIDatesTest.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/15/2018
// Note    : Copyright 2003-2018, Eric Woodruff, All rights reserved
// Compiler: Visual C#
//
// This is a console mode application that runs through a few simple configurations to test the basics in the
// date utility, holiday, and recurrence classes.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 08/19/2003  EFW  Created the code
//===============================================================================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

using EWSoftware.PDI;

namespace PDIDatesTest
{
	/// <summary>
	/// A simple test of the EWSoftware.PDI date and recurrence classes
	/// </summary>
	class PDIDatesTest
	{
		/// <summary>
        /// This is used to test the EWSoftware.PDI namespace date utility and recurrence classes.  It displays
        /// holidays using the first year specified.  It then runs through each year and displays the holidays
        /// found.  It also runs through some simple recurrence configurations.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
            DateTime testDate, nextDate;
            HashSet<DateTime> holidayDates;
            DateTimeCollection recurDates;
            int yearFrom, yearTo, idx;

            if(args.GetUpperBound(0) < 1)
            {
                Console.WriteLine("Specify a starting and ending year");
                return;
            }

            try
            {
                yearFrom = Convert.ToInt32(args[0]);
                yearTo = Convert.ToInt32(args[1]);

                if(yearFrom < 1)
                    yearFrom = 1;

                if(yearFrom > 9999)
                    yearFrom = 9999;

                if(yearTo < 1)
                    yearTo = 1;

                if(yearTo > 9999)
                    yearTo = 9999;

                if(yearFrom > yearTo)
                {
                    idx = yearFrom;
                    yearFrom = yearTo;
                    yearTo = idx;
                }
            }
            catch
            {
                Console.WriteLine("Invalid year specified on command line");
                return;
            }

            // Test DateUtils.CalculateOccurrenceDate
            Console.WriteLine("Fourth weekday in January 2004: {0:d}",
                DateUtils.CalculateOccurrenceDate(2004, 1, DayOccurrence.Fourth, DaysOfWeek.Weekdays, 0));

            Console.WriteLine("Fourth weekday in January 2004 + 2: {0:d}",
                DateUtils.CalculateOccurrenceDate(2004, 1, DayOccurrence.Fourth, DaysOfWeek.Weekdays, 2));

            Console.WriteLine("Last weekend day in January 2004: {0:d}",
                DateUtils.CalculateOccurrenceDate(2004, 1, DayOccurrence.Last, DaysOfWeek.Weekends, 0));

            Console.WriteLine("Last weekend day in January 2004 + 2: {0:d}",
                DateUtils.CalculateOccurrenceDate(2004, 1, DayOccurrence.Last, DaysOfWeek.Weekends, 2));

            // Pause to review output
            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();

            // Test DateUtils.DateFromWeek(), DateUtils.WeeksInYear(), and DateUtils.WeekFromDate()
            DateTime weekFrom, weekTo;
            int year;

            Console.WriteLine("Week start = Monday");
            DayOfWeek dow = DayOfWeek.Monday;

            for(year = 1998; year < 2010; year++)
            {
                for(idx = 1; idx < 54; idx++)
                    if(idx != 53 || DateUtils.WeeksInYear(year, dow) == 53)
                    {
                        weekFrom = DateUtils.DateFromWeek(year, idx, dow, 0);
                        weekTo = DateUtils.DateFromWeek(year, idx, dow, 6);

                        Console.WriteLine("{0} Week {1}: {2:d} - {3:d}  {4}", year, idx, weekFrom, weekTo,
                            DateUtils.WeekFromDate(weekFrom.AddDays(3), dow));
                    }

                // Pause to review output
                Console.WriteLine("Press ENTER to continue");
                Console.ReadLine();
            }

            // Test DateUtils.EasterSunday()
            Console.WriteLine("Easter - Gregorian");

            for(idx = yearFrom; idx <= yearTo; idx += 3)
            {
                testDate = DateUtils.EasterSunday(idx, EasterMethod.Gregorian);
                Console.Write("{0}    {1}/{2:00}            ", idx, testDate.Month, testDate.Day);

                testDate = DateUtils.EasterSunday(idx + 1, EasterMethod.Gregorian);
                Console.Write("{0}    {1}/{2:00}            ", idx + 1, testDate.Month, testDate.Day);

                testDate = DateUtils.EasterSunday(idx + 2, EasterMethod.Gregorian);
                Console.WriteLine("{0}    {1}/{2:00}", idx + 2, testDate.Month, testDate.Day);
            }

            Console.WriteLine("\nEaster - Julian");

            for(idx = yearFrom; idx <= yearTo; idx += 3)
            {
                testDate = DateUtils.EasterSunday(idx, EasterMethod.Julian);
                Console.Write("{0}    {1}/{2:00}            ", idx, testDate.Month, testDate.Day);

                testDate = DateUtils.EasterSunday(idx + 1, EasterMethod.Julian);
                Console.Write("{0}    {1}/{2:00}            ", idx + 1, testDate.Month, testDate.Day);

                testDate = DateUtils.EasterSunday(idx + 2, EasterMethod.Julian);
                Console.WriteLine("{0}    {1}/{2:00}", idx + 2, testDate.Month, testDate.Day);
            }

            Console.WriteLine("\nEaster - Orthodox");

            for(idx = yearFrom; idx <= yearTo; idx += 3)
            {
                testDate = DateUtils.EasterSunday(idx, EasterMethod.Orthodox);
                Console.Write("{0}    {1}/{2:00}            ", idx, testDate.Month, testDate.Day);

                testDate = DateUtils.EasterSunday(idx + 1, EasterMethod.Orthodox);
                Console.Write("{0}    {1}/{2:00}            ", idx + 1, testDate.Month, testDate.Day);

                testDate = DateUtils.EasterSunday(idx + 2, EasterMethod.Orthodox);
                Console.WriteLine("{0}    {1}/{2:00}", idx + 2, testDate.Month, testDate.Day);
            }

            // Pause to review output
            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();

            // Test DateUtils.FromISO8601String and DateUtils.FromISO8601TimeZone
            Console.WriteLine("Expressed in Universal Time");

            Console.WriteLine("20040314 = {0}", DateUtils.FromISO8601String("20040314", false));
            Console.WriteLine("20040314T10 = {0}", DateUtils.FromISO8601String("20040314T10", false));
            Console.WriteLine("20040314T1025 = {0}", DateUtils.FromISO8601String("20040314T1025", false));
            Console.WriteLine("20040314T102531 = {0}", DateUtils.FromISO8601String("20040314T102531", false));
            Console.WriteLine("20040314T102531.123 = {0:d} {0:HH:mm:ss.fff}",
                DateUtils.FromISO8601String("20040314T102531.123", false));
            Console.WriteLine("20040314T102531.98Z = {0:d} {0:HH:mm:ss.fff}",
                DateUtils.FromISO8601String("20040314T102531.98Z", false));
            Console.WriteLine("20040314T102531-04 = {0}", DateUtils.FromISO8601String("20040314T102531-04", false));
            Console.WriteLine("20040314T102531.123+0830 = {0}",
                DateUtils.FromISO8601String("20040314T102531.123+0830", false));

            Console.WriteLine("\n2004-03-14 = {0}", DateUtils.FromISO8601String("2004-03-14", false));
            Console.WriteLine("2004-03-14T10 = {0}", DateUtils.FromISO8601String("2004-03-14T10", false));
            Console.WriteLine("2004-03-14T10:25 = {0}", DateUtils.FromISO8601String("2004-03-14T10:25", false));
            Console.WriteLine("2004-03-14T10:25:31 = {0}", DateUtils.FromISO8601String("2004-03-14T10:25:31", false));
            Console.WriteLine("2004-03-14T10:25:31.123 = {0:d} {0:HH:mm:ss.fff}",
                DateUtils.FromISO8601String("2004-03-14T10:25:31.123", false));
            Console.WriteLine("2004-03-14T10:25:31.98Z = {0:d} {0:HH:mm:ss.fff}",
                DateUtils.FromISO8601String("2004-03-14T10:25:31.98Z", false));
            Console.WriteLine("2004-03-14T10:25:31-04 = {0}",
                DateUtils.FromISO8601String("2004-03-14T10:25:31-04", false));
            Console.WriteLine("2004-03-14T10:25:31+08:30 = {0}",
                DateUtils.FromISO8601String("2004-03-14T10:25:31+08:30", false));

            // Test DateUtils.FromISO8601String and DateUtils.FromISO8601TimeZone
            Console.WriteLine("\nExpressed in Local Time");

            Console.WriteLine("20040314 = {0}", DateUtils.FromISO8601String("20040314", true));
            Console.WriteLine("20040314T10 = {0}", DateUtils.FromISO8601String("20040314T10", true));
            Console.WriteLine("20040314T1025 = {0}", DateUtils.FromISO8601String("20040314T1025", true));
            Console.WriteLine("20040314T102531 = {0}", DateUtils.FromISO8601String("20040314T102531", true));
            Console.WriteLine("20040314T102531.123 = {0:d} {0:HH:mm:ss.fff}",
                DateUtils.FromISO8601String("20040314T102531.123", true));
            Console.WriteLine("20040314T102531.98Z = {0:d} {0:HH:mm:ss.fff}",
                DateUtils.FromISO8601String("20040314T102531.98Z", true));
            Console.WriteLine("20040314T102531-04 = {0}", DateUtils.FromISO8601String("20040314T102531-04", true));
            Console.WriteLine("20040314T102531.123+0830 = {0}",
                DateUtils.FromISO8601String("20040314T102531.123+0830", true));

            Console.WriteLine("\n2004-03-14 = {0}", DateUtils.FromISO8601String("2004-03-14", true));
            Console.WriteLine("2004-03-14T10 = {0}", DateUtils.FromISO8601String("2004-03-14T10", true));
            Console.WriteLine("2004-03-14T10:25 = {0}", DateUtils.FromISO8601String("2004-03-14T10:25", true));
            Console.WriteLine("2004-03-14T10:25:31 = {0}", DateUtils.FromISO8601String("2004-03-14T10:25:31", true));
            Console.WriteLine("2004-03-14T10:25:31.123 = {0:d} {0:HH:mm:ss.fff}",
                DateUtils.FromISO8601String("2004-03-14T10:25:31.123", true));
            Console.WriteLine("2004-03-14T10:25:31.98Z = {0:d} {0:HH:mm:ss.fff}",
                DateUtils.FromISO8601String("2004-03-14T10:25:31.98Z", true));
            Console.WriteLine("2004-03-14T10:25:31-04 = {0}",
                DateUtils.FromISO8601String("2004-03-14T10:25:31-04", true));
            Console.WriteLine("2004-03-14T10:25:31+08:30 = {0}",
                DateUtils.FromISO8601String("2004-03-14T10:25:31+08:30", true));

            TimeSpan ts = DateUtils.FromISO8601TimeZone("+08");

            Console.WriteLine("\n+08 = {0} hours {1} minutes", ts.Hours, ts.Minutes);

            ts = DateUtils.FromISO8601TimeZone("-08");

            Console.WriteLine("-08 = {0} hours {1} minutes", ts.Hours, ts.Minutes);

            ts = DateUtils.FromISO8601TimeZone("-0830");

            Console.WriteLine("-0830 = {0} hours {1} minutes", ts.Hours, ts.Minutes);

            ts = DateUtils.FromISO8601TimeZone("+08:30");

            Console.WriteLine("+08:30 = {0} hours {1} minutes", ts.Hours, ts.Minutes);

            // Restrict bad date part values to their minimum/maximum
            Console.WriteLine("\nBad date values test");

            Console.WriteLine("0000-01-01T10:25:00 = {0}",
                DateUtils.FromISO8601String("0000-01-01T10:25:00", true));
            Console.WriteLine("0000-00-01T10:25:00 = {0}",
                DateUtils.FromISO8601String("0000-00-01T10:25:00", true));
            Console.WriteLine("0000-13-01T10:25:00 = {0}",
                DateUtils.FromISO8601String("0000-13-01T10:25:00", true));
            Console.WriteLine("0000-00-00T10:25:00 = {0}",
                DateUtils.FromISO8601String("0000-00-00T10:25:00", true));
            Console.WriteLine("0000-00-32T10:25:00 = {0}",
                DateUtils.FromISO8601String("0000-00-32T10:25:00", true));

            // Pause to review output
            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();

            // Test the Duration class
            Console.WriteLine("Assumptions: 1 year = {0} days, 1 month = {1} days\n", Duration.DaysInOneYear,
                Duration.DaysInOneMonth);

            Duration d = new Duration("P1Y2M3W4DT5H6M7S");

            Console.WriteLine("P1Y2M3W4DT5H6M7S = {0}", d.ToString());
            Console.WriteLine("P1Y2M3W4DT5H6M7S = {0} (max units = months)", d.ToString(Duration.MaxUnit.Months));

            d = new Duration("P10Y11MT16M12S");

            Console.WriteLine("P10Y11MT16M12S = {0}", d.ToString());

            d = new Duration("P5M2DT16M");

            Console.WriteLine("P5M2DT16M = {0}", d.ToString());

            d = new Duration("P7W");

            Console.WriteLine("P7W = {0}", d.ToString());
            Console.WriteLine("P7W = {0} (max units = weeks)", d.ToString(Duration.MaxUnit.Weeks));
            Console.WriteLine("P7W = {0} (max units = days)", d.ToString(Duration.MaxUnit.Days));

            d = new Duration("P7W2D");

            Console.WriteLine("P7W2D = {0}", d.ToString());
            Console.WriteLine("P7W2D = {0} (max units = weeks)", d.ToString(Duration.MaxUnit.Weeks));
            Console.WriteLine("P7W2D = {0} (max units = days)", d.ToString(Duration.MaxUnit.Days));

            d = new Duration("P5DT2S");

            Console.WriteLine("P5DT2S = {0}", d.ToString());

            d = new Duration("PT24H");

            Console.WriteLine("PT24H = {0}", d.ToString());
            Console.WriteLine("PT24H = {0} (max units = hours)", d.ToString(Duration.MaxUnit.Hours));
            Console.WriteLine("PT24H = {0} (max units = minutes)", d.ToString(Duration.MaxUnit.Minutes));
            Console.WriteLine("PT24H = {0} (max units = seconds)",  d.ToString(Duration.MaxUnit.Seconds));

            d = new Duration("PT24H3M2S");

            Console.WriteLine("PT24H3M2S = {0}", d.ToString());
            Console.WriteLine("PT24H3M2S = {0} (max units = hours)", d.ToString(Duration.MaxUnit.Hours));
            Console.WriteLine("PT24H3M2S = {0} (max units = minutes)", d.ToString(Duration.MaxUnit.Minutes));
            Console.WriteLine("PT24H3M2S = {0} (max units = seconds)", d.ToString(Duration.MaxUnit.Seconds));

            d = new Duration("PT1H10M20S");

            Console.WriteLine("PT1H10M20S = {0}", d.ToString());

            d = new Duration("PT5M20S");

            Console.WriteLine("PT5M20S = {0}", d.ToString());

            d = new Duration("PT5S");

            Console.WriteLine("PT5S = {0}", d.ToString());

            d = new Duration("P0Y0M0W0DT0H0M0S");

            Console.WriteLine("P0Y0M0W0DT0H0M0S = {0}", d.ToString());

            // Pause to review output
            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();

            d = new Duration("-P1Y2M3W4DT5H6M7S");

            Console.WriteLine("\n-P1Y2M3W4DT5H6M7S = {0}", d.ToString());
            Console.WriteLine("-P1Y2M3W4DT5H6M7S = {0} (max units = months)", d.ToString(Duration.MaxUnit.Months));

            d = new Duration("-P10Y11MT16M12S");

            Console.WriteLine("-P10Y11MT16M12S = {0}", d.ToString());

            d = new Duration("-P5M2DT16M");

            Console.WriteLine("-P5M2DT16M = {0}", d.ToString());

            d = new Duration("-P7W");

            Console.WriteLine("-P7W = {0}", d.ToString());
            Console.WriteLine("-P7W = {0} (max units = weeks)", d.ToString(Duration.MaxUnit.Weeks));
            Console.WriteLine("-P7W = {0} (max units = days)", d.ToString(Duration.MaxUnit.Days));

            d = new Duration("-P7W2D");

            Console.WriteLine("-P7W2D = {0}", d.ToString());
            Console.WriteLine("-P7W2D = {0} (max units = weeks)", d.ToString(Duration.MaxUnit.Weeks));
            Console.WriteLine("-P7W2D = {0} (max units = days)", d.ToString(Duration.MaxUnit.Days));

            d = new Duration("-P5DT2S");

            Console.WriteLine("-P5DT2S = {0}", d.ToString());

            d = new Duration("-PT24H");

            Console.WriteLine("-PT24H = {0}", d.ToString());
            Console.WriteLine("-PT24H = {0} (max units = hours)", d.ToString(Duration.MaxUnit.Hours));
            Console.WriteLine("-PT24H = {0} (max units = minutes)", d.ToString(Duration.MaxUnit.Minutes));
            Console.WriteLine("-PT24H = {0} (max units = seconds)", d.ToString(Duration.MaxUnit.Seconds));

            d = new Duration("-PT24H3M2S");

            Console.WriteLine("-PT24H3M2S = {0}", d.ToString());
            Console.WriteLine("-PT24H3M2S = {0} (max units = hours)", d.ToString(Duration.MaxUnit.Hours));
            Console.WriteLine("-PT24H3M2S = {0} (max units = minutes)", d.ToString(Duration.MaxUnit.Minutes));
            Console.WriteLine("-PT24H3M2S = {0} (max units = seconds)", d.ToString(Duration.MaxUnit.Seconds));

            d = new Duration("-PT1H10M20S");

            Console.WriteLine("-PT1H10M20S = {0}", d.ToString());

            d = new Duration("-PT5M20S");

            Console.WriteLine("-PT5M20S = {0}", d.ToString());

            d = new Duration("-PT5S");

            Console.WriteLine("-PT5S = {0}", d.ToString());

            d = new Duration("-P0Y0M0W0DT0H0M0S");

            Console.WriteLine("-P0Y0M0W0DT0H0M0S = {0}", d.ToString());

            // Pause to review output
            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();

            // Test the ToDescription() methods
            d = new Duration("P1Y2M3W4DT5H6M7S");

            Console.WriteLine("P1Y2M3W4DT5H6M7S = {0}", d.ToDescription());
            Console.WriteLine("P1Y2M3W4DT5H6M7S = {0} (max units = months)", d.ToDescription(Duration.MaxUnit.Months));

            d = new Duration("P10Y11MT16M12S");

            Console.WriteLine("P10Y11MT16M12S = {0}", d.ToDescription());

            d = new Duration("P5M2DT16M");

            Console.WriteLine("P5M2DT16M = {0}", d.ToDescription());

            d = new Duration("P7W");

            Console.WriteLine("P7W = {0}", d.ToDescription());
            Console.WriteLine("P7W = {0} (max units = weeks)", d.ToDescription(Duration.MaxUnit.Weeks));
            Console.WriteLine("P7W = {0} (max units = days)", d.ToDescription(Duration.MaxUnit.Days));

            d = new Duration("P7W2D");

            Console.WriteLine("P7W2D = {0}", d.ToDescription());
            Console.WriteLine("P7W2D = {0} (max units = weeks)", d.ToDescription(Duration.MaxUnit.Weeks));
            Console.WriteLine("P7W2D = {0} (max units = days)", d.ToDescription(Duration.MaxUnit.Days));

            d = new Duration("P5DT2S");

            Console.WriteLine("P5DT2S = {0}", d.ToDescription());

            d = new Duration("PT24H");

            Console.WriteLine("PT24H = {0}", d.ToDescription());
            Console.WriteLine("PT24H = {0} (max units = hours)", d.ToDescription(Duration.MaxUnit.Hours));
            Console.WriteLine("PT24H = {0} (max units = minutes)", d.ToDescription(Duration.MaxUnit.Minutes));
            Console.WriteLine("PT24H = {0} (max units = seconds)", d.ToDescription(Duration.MaxUnit.Seconds));

            d = new Duration("PT24H3M2S");

            Console.WriteLine("PT24H3M2S = {0}", d.ToDescription());
            Console.WriteLine("PT24H3M2S = {0} (max units = hours)", d.ToDescription(Duration.MaxUnit.Hours));
            Console.WriteLine("PT24H3M2S = {0} (max units = minutes)", d.ToDescription(Duration.MaxUnit.Minutes));
            Console.WriteLine("PT24H3M2S = {0} (max units = seconds)", d.ToDescription(Duration.MaxUnit.Seconds));

            d = new Duration("PT1H10M20S");

            Console.WriteLine("PT1H10M20S = {0}", d.ToDescription());

            d = new Duration("PT5M20S");

            Console.WriteLine("PT5M20S = {0}", d.ToDescription());

            d = new Duration("PT5S");

            Console.WriteLine("PT5S = {0}", d.ToDescription());

            d = new Duration("P0Y0M0W0DT0H0M0S");

            Console.WriteLine("P0Y0M0W0DT0H0M0S = {0}", d.ToDescription());

            // Pause to review output
            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();

            d = new Duration("-P1Y2M3W4DT5H6M7S");

            Console.WriteLine("\n-P1Y2M3W4DT5H6M7S = {0}", d.ToDescription());
            Console.WriteLine("-P1Y2M3W4DT5H6M7S = {0} (max units = months)", d.ToDescription(Duration.MaxUnit.Months));

            d = new Duration("-P10Y11MT16M12S");

            Console.WriteLine("-P10Y11MT16M12S = {0}", d.ToDescription());

            d = new Duration("-P5M2DT16M");

            Console.WriteLine("-P5M2DT16M = {0}", d.ToDescription());

            d = new Duration("-P7W");

            Console.WriteLine("-P7W = {0}", d.ToDescription());
            Console.WriteLine("-P7W = {0} (max units = weeks)", d.ToDescription(Duration.MaxUnit.Weeks));
            Console.WriteLine("-P7W = {0} (max units = days)", d.ToDescription(Duration.MaxUnit.Days));

            d = new Duration("-P7W2D");

            Console.WriteLine("-P7W2D = {0}", d.ToDescription());
            Console.WriteLine("-P7W2D = {0} (max units = weeks)", d.ToDescription(Duration.MaxUnit.Weeks));
            Console.WriteLine("-P7W2D = {0} (max units = days)", d.ToDescription(Duration.MaxUnit.Days));

            d = new Duration("-P5DT2S");

            Console.WriteLine("-P5DT2S = {0}", d.ToDescription());

            d = new Duration("-PT24H");

            Console.WriteLine("-PT24H = {0}", d.ToDescription());
            Console.WriteLine("-PT24H = {0} (max units = hours)", d.ToDescription(Duration.MaxUnit.Hours));
            Console.WriteLine("-PT24H = {0} (max units = minutes)", d.ToDescription(Duration.MaxUnit.Minutes));
            Console.WriteLine("-PT24H = {0} (max units = seconds)", d.ToDescription(Duration.MaxUnit.Seconds));

            d = new Duration("-PT24H3M2S");

            Console.WriteLine("-PT24H3M2S = {0}", d.ToDescription());
            Console.WriteLine("-PT24H3M2S = {0} (max units = hours)", d.ToDescription(Duration.MaxUnit.Hours));
            Console.WriteLine("-PT24H3M2S = {0} (max units = minutes)", d.ToDescription(Duration.MaxUnit.Minutes));
            Console.WriteLine("-PT24H3M2S = {0} (max units = seconds)", d.ToDescription(Duration.MaxUnit.Seconds));

            d = new Duration("-PT1H10M20S");

            Console.WriteLine("-PT1H10M20S = {0}", d.ToDescription());

            d = new Duration("-PT5M20S");

            Console.WriteLine("-PT5M20S = {0}", d.ToDescription());

            d = new Duration("-PT5S");

            Console.WriteLine("-PT5S = {0}", d.ToDescription());

            d = new Duration("-P0Y0M0W0DT0H0M0S");

            Console.WriteLine("-P0Y0M0W0DT0H0M0S = {0}", d.ToDescription());

            // Pause to review output
            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();

            // Create a set of fixed and floating holidays
            HolidayCollection holidays = new HolidayCollection();

            holidays.AddFixed(1, 1, true, "New Year's Day");
            holidays.AddFloating(DayOccurrence.Third, DayOfWeek.Monday, 1, 0, "Martin Luther King Day");
            holidays.AddFloating(DayOccurrence.Third, DayOfWeek.Monday, 2, 0, "President's Day");
            holidays.AddFloating(DayOccurrence.Last, DayOfWeek.Monday, 5, 0, "Memorial Day");
            holidays.AddFixed(7, 4, true, "Independence Day");
            holidays.AddFloating(DayOccurrence.First, DayOfWeek.Monday, 9, 0, "Labor Day");
            holidays.AddFixed(11, 11, true, "Veteran's Day");
            holidays.AddFloating(DayOccurrence.Fourth, DayOfWeek.Thursday, 11, 0, "Thanksgiving Day");
            holidays.AddFloating(DayOccurrence.Fourth, DayOfWeek.Thursday, 11, 1, "Day After Thanksgiving");
            holidays.AddFixed(12, 25, true, "Christmas Day");

            // Serialize the holidays to a file
            try
            {
                // XML
                using(var fs = new FileStream("Holidays.xml", FileMode.Create))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(HolidayCollection));
                    xs.Serialize(fs, holidays);

                    Console.WriteLine("Holidays saved to Holidays.xml");
                }

                // SOAP
                using(var fs = new FileStream("Holidays.soap", FileMode.Create))
                {
                    SoapFormatter sf = new SoapFormatter();

                    // SOAP doesn't support generics directly so use an array
                    sf.Serialize(fs, holidays.ToArray());

                    Console.WriteLine("Holidays saved to Holidays.soap");
                }

                // Binary
                using(var fs = new FileStream("Holidays.bin", FileMode.Create))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, holidays);

                    Console.WriteLine("Holidays saved to Holidays.bin\n");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to save holidays:\n{0}", ex.Message);

                if(ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);

                    if(ex.InnerException.InnerException != null)
                        Console.WriteLine(ex.InnerException.InnerException.Message);
                }
            }

            // Delete the collection and read it back in
            holidays = null;

            try
            {
                // XML
                using(var fs = new FileStream("Holidays.xml", FileMode.Open))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(HolidayCollection));
                    holidays = (HolidayCollection)xs.Deserialize(fs);

                    Console.WriteLine("Holidays retrieved from Holidays.xml");
                }

                // SOAP
                using(var fs = new FileStream("Holidays.soap", FileMode.Open))
                {
                    SoapFormatter sf = new SoapFormatter();

                    // As noted, SOAP doesn't support generics to an array is used instead
                    holidays = new HolidayCollection((Holiday[])sf.Deserialize(fs));

                    Console.WriteLine("Holidays retrieved from Holidays.soap");
                }

                // Binary
                using(var fs = new FileStream("Holidays.bin", FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    holidays = (HolidayCollection)bf.Deserialize(fs);

                    Console.WriteLine("Holidays retrieved from Holidays.bin\n");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to load holidays:\n{0}", ex.Message);

                if(ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);

                    if(ex.InnerException.InnerException != null)
                        Console.WriteLine(ex.InnerException.InnerException.Message);
                }
            }

            // Display the holidays added to the list
            Console.WriteLine("Holidays on file.  Is Holiday should be true for all.");

            foreach(Holiday hol in holidays)
                Console.WriteLine("Holiday Date: {0:d}   Is Holiday: {1}  Description: {2}",
                    hol.ToDateTime(yearFrom), holidays.IsHoliday(hol.ToDateTime(yearFrom)), hol.Description);

            // Pause to review output
            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();

            // Display holidays found in each year specified using the IsHoliday method
            Console.WriteLine("Looking for holidays using the IsHoliday method");

            testDate = new DateTime(yearFrom, 1, 1);

            while(testDate.Year <= yearTo)
            {
                if(holidays.IsHoliday(testDate))
                    Console.WriteLine("Found holiday: {0:d} {1}", testDate, holidays.HolidayDescription(testDate));

                testDate = testDate.AddDays(1);

                // Stop after each year to review output
                if(testDate.Month == 1 && testDate.Day == 1)
                {
                    Console.WriteLine("Press ENTER to continue");
                    Console.ReadLine();
                }
            }

            // One more time, but use a hash set using the dates returned by the HolidaysBetween() method.  For
            // bulk comparisons, this is faster than the above procedure using the IsHoliday method.
            Console.WriteLine("Looking for holidays using HolidaysBetween");

            holidayDates = new HashSet<DateTime>(holidays.HolidaysBetween(yearFrom, yearTo));

            if(holidayDates.Count != 0)
            {
                testDate = new DateTime(yearFrom, 1, 1);

                while(testDate.Year <= yearTo)
                {
                    if(holidayDates.Contains(testDate))
                        Console.WriteLine("Found holiday: {0:d} {1}", testDate, holidays.HolidayDescription(testDate));

                    testDate = testDate.AddDays(1);

                    // Stop after each year to review output
                    if(testDate.Month == 1 && testDate.Day == 1)
                    {
                        Console.WriteLine("Press ENTER to continue");
                        Console.ReadLine();
                    }
                }
            }

            //=================================================================

            // Test recurrence
            Recurrence rRecur = new Recurrence();
            Recurrence.Holidays.AddRange(holidays);

            // Disallow occurrences on any of the defined holidays
            rRecur.CanOccurOnHoliday = false;

            testDate = new DateTime(yearFrom, 1, 1);

            // Test daily recurrence
            rRecur.StartDateTime = testDate;
            rRecur.RecurUntil = testDate.AddMonths(1);   // For the enumerator
            rRecur.RecurDaily(2);

            // Serialize the recurrence to a file
            try
            {
                // XML
                using(var fs = new FileStream("Recurrence.xml", FileMode.Create))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Recurrence));
                    xs.Serialize(fs, rRecur);

                    Console.WriteLine("Recurrence saved to Recurrence.xml");
                }

                // SOAP
                using(var fs = new FileStream("Recurrence.soap", FileMode.Create))
                {
                    SoapFormatter sf = new SoapFormatter();
                    sf.Serialize(fs, rRecur);

                    Console.WriteLine("Recurrence saved to Recurrence.soap");
                }

                // Binary
                using(var fs = new FileStream("Recurrence.bin", FileMode.Create))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, rRecur);

                    Console.WriteLine("Recurrence saved to Recurrence.bin\n");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to save recurrence:\n{0}", ex.Message);

                if(ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);

                    if(ex.InnerException.InnerException != null)
                        Console.WriteLine(ex.InnerException.InnerException.Message);
                }
            }

            // Deserialize the recurrence from a file
            rRecur = null;

            try
            {
                // XML
                using(var fs = new FileStream("Recurrence.xml", FileMode.Open))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Recurrence));
                    rRecur = (Recurrence)xs.Deserialize(fs);

                    Console.WriteLine("Recurrence restored from Recurrence.xml");
                }

                // SOAP
                using(var fs = new FileStream("Recurrence.soap", FileMode.Open))
                {
                    SoapFormatter sf = new SoapFormatter();
                    rRecur = (Recurrence)sf.Deserialize(fs);

                    Console.WriteLine("Recurrence retrieved from Recurrence.soap");
                }

                // Binary
                using(var fs = new FileStream("Recurrence.bin", FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    rRecur = (Recurrence)bf.Deserialize(fs);

                    Console.WriteLine("Recurrence retrieved from Recurrence.bin\n");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to restore recurrence:\n{0}", ex.Message);

                if(ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);

                    if(ex.InnerException.InnerException != null)
                        Console.WriteLine(ex.InnerException.InnerException.Message);
                }
            }

            recurDates = rRecur.InstancesBetween(testDate, testDate.AddMonths(1));

            Console.WriteLine(rRecur.ToStringWithStartDateTime());

            foreach(DateTime dt in recurDates)
                Console.WriteLine("(Collection) Daily recurrence on: {0:d}", dt);

            foreach(DateTime dt in rRecur)
                Console.WriteLine("(Enumerator) Daily recurrence on: {0:d}", dt);

            // Test NextInstance().  This isn't the most efficient way of searching for lots of dates but is
            // useful for one-off searches.
            nextDate = testDate;

            do
            {
                nextDate = rRecur.NextInstance(nextDate, false);

                if(nextDate != DateTime.MinValue)
                {
                    Console.WriteLine("(NextInstance) Daily recurrence on: {0:d}", nextDate);
                    nextDate = nextDate.AddMinutes(1);
                }

            } while(nextDate != DateTime.MinValue);

            // Pause to review output
            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();

            // Test weekly recurrence
            rRecur.StartDateTime = testDate;
            rRecur.RecurWeekly(1, DaysOfWeek.Monday | DaysOfWeek.Wednesday);
            recurDates = rRecur.InstancesBetween(testDate, testDate.AddMonths(1));

            Console.WriteLine(rRecur.ToStringWithStartDateTime());

            foreach(DateTime dt in recurDates)
                Console.WriteLine("(Collection) Weekly recurrence on: {0:d}", dt);

            foreach(DateTime dt in rRecur)
                Console.WriteLine("(Enumerator) Weekly recurrence on: {0:d}", dt);

            nextDate = testDate;

            do
            {
                nextDate = rRecur.NextInstance(nextDate, false);

                if(nextDate != DateTime.MinValue)
                {
                    Console.WriteLine("(NextInstance) Weekly recurrence on: {0:d}", nextDate);
                    nextDate = nextDate.AddMinutes(1);
                }

            } while(nextDate != DateTime.MinValue);

            // Pause to review output
            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();

            // Test monthly recurrence (option 1)
            rRecur.StartDateTime = testDate;
            rRecur.RecurUntil = testDate.AddMonths(12);   // For the enumerator
            rRecur.RecurMonthly(15, 2);
            recurDates = rRecur.InstancesBetween(testDate, testDate.AddMonths(12));

            Console.WriteLine(rRecur.ToStringWithStartDateTime());

            foreach(DateTime dt in recurDates)
                Console.WriteLine("(Collection) Monthly recurrence on: {0:d}", dt);

            foreach(DateTime dt in rRecur)
                Console.WriteLine("(Enumerator) Monthly recurrence on: {0:d}", dt);

            nextDate = testDate;

            do
            {
                nextDate = rRecur.NextInstance(nextDate, false);

                if(nextDate != DateTime.MinValue)
                {
                    Console.WriteLine("(NextInstance) Monthly recurrence on: {0:d}", nextDate);
                    nextDate = nextDate.AddMinutes(1);
                }

            } while(nextDate != DateTime.MinValue);

            // Pause to review output
            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();

            // Test monthly recurrence (option 2)
            rRecur.RecurMonthly(DayOccurrence.Third, DaysOfWeek.Thursday, 3);
            recurDates = rRecur.InstancesBetween(testDate, testDate.AddMonths(12));

            Console.WriteLine(rRecur.ToStringWithStartDateTime());

            foreach(DateTime dt in recurDates)
                Console.WriteLine("(Collection) Monthly recurrence on: {0:d}", dt);

            foreach(DateTime dt in rRecur)
                Console.WriteLine("(Enumerator) Monthly recurrence on: {0:d}", dt);

            nextDate = testDate;

            do
            {
                nextDate = rRecur.NextInstance(nextDate, false);

                if(nextDate != DateTime.MinValue)
                {
                    Console.WriteLine("(NextInstance) Monthly recurrence on: {0:d}", nextDate);
                    nextDate = nextDate.AddMinutes(1);
                }

            } while(nextDate != DateTime.MinValue);

            // Pause to review output
            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();

            // Test monthly recurrence (option 3 - a variation of option 2)
            rRecur.RecurMonthly(DayOccurrence.Third, DaysOfWeek.Weekends, 2);
            recurDates = rRecur.InstancesBetween(testDate, testDate.AddMonths(12));

            Console.WriteLine(rRecur.ToStringWithStartDateTime());

            foreach(DateTime dt in recurDates)
                Console.WriteLine("(Collection) Monthly recurrence on: {0:d}", dt);

            foreach(DateTime dt in rRecur)
                Console.WriteLine("(Enumerator) Monthly recurrence on: {0:d}", dt);

            nextDate = testDate;

            do
            {
                nextDate = rRecur.NextInstance(nextDate, false);

                if(nextDate != DateTime.MinValue)
                {
                    Console.WriteLine("(NextInstance) Monthly recurrence on: {0:d}", nextDate);
                    nextDate = nextDate.AddMinutes(1);
                }

            } while(nextDate != DateTime.MinValue);

            // Pause to review output
            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();

            // Test yearly recurrence (option 1)
            rRecur.StartDateTime = testDate;
            rRecur.RecurUntil = testDate.AddYears(5);   // For the enumerator
            rRecur.RecurYearly(5, 24, 1);
            recurDates = rRecur.InstancesBetween(testDate, testDate.AddYears(5));

            Console.WriteLine(rRecur.ToStringWithStartDateTime());

            foreach(DateTime dt in recurDates)
                Console.WriteLine("(Collection) Yearly recurrence on: {0:d}", dt);

            foreach(DateTime dt in rRecur)
                Console.WriteLine("(Enumerator) Yearly recurrence on: {0:d}", dt);

            nextDate = testDate;

            do
            {
                nextDate = rRecur.NextInstance(nextDate, false);

                if(nextDate != DateTime.MinValue)
                {
                    Console.WriteLine("(NextInstance) Yearly recurrence on: {0:d}", nextDate);
                    nextDate = nextDate.AddMinutes(1);
                }

            } while(nextDate != DateTime.MinValue);

            // Pause to review output
            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();

            // Test yearly recurrence (option 2)
            rRecur.RecurYearly(DayOccurrence.Last, DaysOfWeek.Sunday, 9, 2);
            recurDates = rRecur.InstancesBetween(testDate, testDate.AddYears(5));

            Console.WriteLine(rRecur.ToStringWithStartDateTime());

            foreach(DateTime dt in recurDates)
                Console.WriteLine("(Collection) Yearly recurrence on: {0:d}", dt);

            foreach(DateTime dt in rRecur)
                Console.WriteLine("(Enumerator) Yearly recurrence on: {0:d}", dt);

            nextDate = testDate;

            do
            {
                nextDate = rRecur.NextInstance(nextDate, false);

                if(nextDate != DateTime.MinValue)
                {
                    Console.WriteLine("(NextInstance) Yearly recurrence on: {0:d}", nextDate);
                    nextDate = nextDate.AddMinutes(1);
                }

            } while(nextDate != DateTime.MinValue);

            // Pause to review output
            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();

            // Test yearly recurrence (option 3 - a variation of option 2)
            rRecur.RecurYearly(DayOccurrence.Last, DaysOfWeek.Weekdays, 7, 1);
            recurDates = rRecur.InstancesBetween(testDate, testDate.AddYears(5));

            Console.WriteLine(rRecur.ToStringWithStartDateTime());

            foreach(DateTime dt in recurDates)
                Console.WriteLine("(Collection) Yearly recurrence on: {0:d}", dt);

            foreach(DateTime dt in rRecur)
                Console.WriteLine("(Enumerator) Yearly recurrence on: {0:d}", dt);

            nextDate = testDate;

            do
            {
                nextDate = rRecur.NextInstance(nextDate, false);

                if(nextDate != DateTime.MinValue)
                {
                    Console.WriteLine("(NextInstance) Yearly recurrence on: {0:d}", nextDate);
                    nextDate = nextDate.AddMinutes(1);
                }

            } while(nextDate != DateTime.MinValue);

            // Pause to review output
            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();
        }
	}
}
