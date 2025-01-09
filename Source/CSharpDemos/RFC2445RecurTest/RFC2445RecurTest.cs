//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : RFC2445RecurTest.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/04/2025
// Note    : Copyright 2003-2025, Eric Woodruff, All rights reserved
//
// This creates the example recurrence patterns given in the RFC 2445 iCalendar specification starting on page
// 118 and generates the instances for each one.  Rather than parsing the information from strings, it shows how
// to create the patterns using the API.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 10/22/2004  EFW  Created the code
//===============================================================================================================

#pragma warning disable CA1861

using System;

using EWSoftware.PDI;
using EWSoftware.PDI.Objects;
using EWSoftware.PDI.Properties;

namespace RFC2445RecurTest
{
	/// <summary>
	/// Test the various RFC 2445 iCalendar recurrence patterns found in the spec starting on page 118
	/// </summary>
	sealed class RFC2445RecurTest
	{
		/// <summary>
		/// Create each pattern using the API and generate the instances.
		/// </summary>
		[STAThread]
		static void Main()
		{
            Recurrence r = new();
            int idx;

            Console.WriteLine("The first part will calculate instances using only the Recurrence class.  All " +
                "instances are expressed in local time.\nPress ENTER to continue...");
            Console.ReadLine();

            // NOTE: The examples in the spec assume the US-Eastern time zone.  Here, we're specifying the
            // date/times directly and everything is in local time.  The recurrence engine always generates the
            // date/times in local time when used by itself.  For time zone support, use the calendar objects as
            // demonstrated later on below.

            Console.WriteLine("Daily for 10 occurrences:");

            r.StartDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Daily;
            r.MaximumOccurrences = 10;

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Daily until December 24, 1997:");

            // We'll re-use the same object.  Just reset it first.
            r.Reset();

            r.StartDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Daily;

            // Note that in the spec examples, it looks as if the end times are wrong.  It shows the times in
            // Universal Time as it should be but if you convert the times from Universal Time to US-Eastern time
            // (-05:00), it cuts a few of the patterns off too early.  I've adjusted those where needed.
            r.RecurUntil = new DateTime(1997, 12, 24, 0, 0, 0);

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every other day - forever:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Daily;
            r.Interval = 2;

            Console.WriteLine(r.ToStringWithStartDateTime());

            // We'll limit the output of the "forever" rules by using the InstancesBetween() method to limit the
            // output to a range of dates and enumerate the returned DateTimeCollection.
            foreach(DateTime dt in r.InstancesBetween(r.StartDateTime, new DateTime(1997, 12, 5)))
                Console.WriteLine(dt);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every 10 days, 5 occurrences:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Daily;
            r.Interval = 10;
            r.MaximumOccurrences = 5;

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every day in January, for 3 years:");

            r.Reset();
            r.StartDateTime = new DateTime(1998, 1, 1, 9, 0, 0);
            r.Frequency = RecurFrequency.Yearly;
            r.RecurUntil = new DateTime(2000, 1, 31, 9, 0, 0);
            r.ByMonth.Add(1);

            // When adding days without an instance value, you can use the helper method on the collection that
            // takes an array of DayOfWeek values rather than constructing an array of DayInstance objects.
            r.ByDay.AddRange([ DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
                DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday ]);

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            // Like the last one but using a DAILY frequency
            Console.WriteLine("Everyday in January, for 3 years:");

            r.Reset();
            r.StartDateTime = new DateTime(1998, 1, 1, 9, 0, 0);
            r.Frequency = RecurFrequency.Daily;
            r.RecurUntil = new DateTime(2000, 1, 31, 9, 0, 0);
            r.ByMonth.Add(1);

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Weekly for 10 occurrences:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Weekly;
            r.MaximumOccurrences = 10;

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Weekly until December 24, 1997:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Weekly;
            r.RecurUntil = new DateTime(1997, 12, 24, 0, 0, 0);

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every other week - forever:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Weekly;
            r.Interval = 2;
            r.WeekStart = DayOfWeek.Sunday;

            Console.WriteLine(r.ToStringWithStartDateTime());

            // Not forever for the test
            foreach(DateTime dt in r.InstancesBetween(r.StartDateTime, new DateTime(1999, 2, 10)))
                Console.WriteLine(dt);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Weekly on Tuesday and Thursday for 5 weeks:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Weekly;
            r.RecurUntil = new DateTime(1997, 10, 07, 0, 0, 0);
            r.WeekStart = DayOfWeek.Sunday;
            r.ByDay.AddRange([DayOfWeek.Tuesday, DayOfWeek.Thursday]);

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            // Like the last one but using a count
            Console.WriteLine("Weekly on Tuesday and Thursday for 5 weeks:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Weekly;
            r.MaximumOccurrences = 10;
            r.WeekStart = DayOfWeek.Sunday;
            r.ByDay.AddRange([DayOfWeek.Tuesday, DayOfWeek.Thursday]);

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            // NOTE: The inclusion of DTSTART in a recurrence set is a feature of the calendar objects, NOT the
            // recurrence engine.  To include DTSTART, use a calendar object such as VEvent to generate the set
            // as demonstrated later on below.
            Console.WriteLine("Every other week on Monday, Wednesday and Friday until December 24, 1997, but " +
                "starting on Tuesday, September 2, 1997:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Weekly;
            r.Interval = 2;
            r.WeekStart = DayOfWeek.Sunday;
            r.RecurUntil = new DateTime(1997, 12, 24, 0, 0, 0);
            r.ByDay.AddRange([DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday]);

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every other week on Tuesday and Thursday, for 8 occurrences:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Weekly;
            r.Interval = 2;
            r.WeekStart = DayOfWeek.Sunday;
            r.MaximumOccurrences = 8;
            r.ByDay.AddRange([DayOfWeek.Tuesday, DayOfWeek.Thursday]);

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Monthly on the 1st Friday for ten occurrences:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 5, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.MaximumOccurrences = 10;

            // There are also helper methods to add single instances
            r.ByDay.Add(1, DayOfWeek.Friday);

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Monthly on the 1st Friday until December 24, 1997:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 5, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.RecurUntil = new DateTime(1997, 12, 24, 0, 0, 0);
            r.ByDay.Add(1, DayOfWeek.Friday);

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every other month on the 1st and last Sunday of the month for 10 occurrences:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 7, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.Interval = 2;
            r.MaximumOccurrences = 10;
            r.ByDay.AddRange([new DayInstance(1, DayOfWeek.Sunday), new DayInstance(-1, DayOfWeek.Sunday)]);

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Monthly on the second to last Monday of the month for 6 months:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 22, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.MaximumOccurrences = 6;
            r.ByDay.Add(-2, DayOfWeek.Monday);

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Monthly on the third to the last day of the month, forever:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 28, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.ByMonthDay.Add(-3);

            Console.WriteLine(r.ToStringWithStartDateTime());

            // Not forever for the test
            foreach(DateTime dt in r.InstancesBetween(r.StartDateTime, new DateTime(1998, 2, 28)))
                Console.WriteLine(dt);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Monthly on the 2nd and 15th of the month for 10 occurrences:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.MaximumOccurrences = 10;
            r.ByMonthDay.AddRange([2, 15]);

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Monthly on the first and last day of the month for 10 occurrences:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 30, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.MaximumOccurrences = 10;
            r.ByMonthDay.AddRange([1, -1]);

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every 18 months on the 10th through 15th of the month for 10 occurrences:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 10, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.Interval = 18;
            r.MaximumOccurrences = 10;
            r.ByMonthDay.AddRange([10, 11, 12, 13, 14, 15]);

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every Tuesday, every other month:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.Interval = 2;
            r.ByDay.Add(DayOfWeek.Tuesday);

            Console.WriteLine(r.ToStringWithStartDateTime());

            // Not forever for the test
            foreach(DateTime dt in r.InstancesBetween(r.StartDateTime, new DateTime(1998, 4, 1)))
                Console.WriteLine(dt);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Yearly in June and July for 10 occurrences:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 6, 10, 9, 0, 0);
            r.Frequency = RecurFrequency.Yearly;
            r.MaximumOccurrences = 10;
            r.ByMonth.AddRange([6, 7]);

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every other year in January, February, and March for 10 occurrences:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 3, 10, 9, 0, 0);
            r.Frequency = RecurFrequency.Yearly;
            r.Interval = 2;
            r.MaximumOccurrences = 10;
            r.ByMonth.AddRange([1, 2, 3]);

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every 3rd year on the 1st, 100th and 200th day for 10 occurrences:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 1, 1, 9, 0, 0);
            r.Frequency = RecurFrequency.Yearly;
            r.Interval = 3;
            r.MaximumOccurrences = 10;
            r.ByYearDay.AddRange([1, 100, 200]);

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every 20th Monday of the year, forever:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 5, 19, 9, 0, 0);
            r.Frequency = RecurFrequency.Yearly;
            r.ByDay.Add(20, DayOfWeek.Monday);

            Console.WriteLine(r.ToStringWithStartDateTime());

            // Not forever for the test
            foreach(DateTime dt in r.InstancesBetween(r.StartDateTime, new DateTime(2004, 5, 19)))
                Console.WriteLine(dt);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Monday of week number 20 (where the default start of the week is Monday), forever:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 5, 12, 9, 0, 0);
            r.Frequency = RecurFrequency.Yearly;
            r.ByWeekNo.Add(20);
            r.ByDay.Add(DayOfWeek.Monday);

            Console.WriteLine(r.ToStringWithStartDateTime());

            // Not forever for the test
            foreach(DateTime dt in r.InstancesBetween(r.StartDateTime, new DateTime(2004, 5, 19)))
                Console.WriteLine(dt);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every Thursday in March, forever:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 3, 13, 9, 0, 0);
            r.Frequency = RecurFrequency.Yearly;
            r.ByMonth.Add(3);
            r.ByDay.Add(DayOfWeek.Thursday);

            Console.WriteLine(r.ToStringWithStartDateTime());

            // Not forever for the test
            foreach(DateTime dt in r.InstancesBetween(r.StartDateTime, new DateTime(1999, 4, 1)))
                Console.WriteLine(dt);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every Thursday, but only during June, July, and August, forever:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 6, 5, 9, 0, 0);
            r.Frequency = RecurFrequency.Yearly;
            r.ByMonth.AddRange([6, 7, 8]);
            r.ByDay.Add(DayOfWeek.Thursday);

            Console.WriteLine(r.ToStringWithStartDateTime());

            // Not forever for the test
            foreach(DateTime dt in r.InstancesBetween(r.StartDateTime, new DateTime(1999, 9, 1)))
                Console.WriteLine(dt);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every Friday the 13th, forever:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.ByDay.Add(DayOfWeek.Friday);
            r.ByMonthDay.Add(13);

            Console.WriteLine(r.ToStringWithStartDateTime());

            // Not forever for the test
            foreach(DateTime dt in r.InstancesBetween(r.StartDateTime, new DateTime(2003, 12, 31)))
                Console.WriteLine(dt);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("The first Saturday that follows the first Sunday of the month, forever:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 13, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.ByDay.Add(DayOfWeek.Saturday);
            r.ByMonthDay.AddRange([7, 8, 9, 10, 11, 12, 13]);

            Console.WriteLine(r.ToStringWithStartDateTime());

            // Not forever for the test
            foreach(DateTime dt in r.InstancesBetween(r.StartDateTime, new DateTime(1998, 7, 1)))
                Console.WriteLine(dt);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every four years, the first Tuesday after a Monday in November, forever " +
                "(U.S. Presidential Election day):");

            r.Reset();
            r.StartDateTime = new DateTime(1996, 11, 5, 9, 0, 0);
            r.Frequency = RecurFrequency.Yearly;
            r.Interval = 4;
            r.ByMonth.Add(11);
            r.ByDay.Add(DayOfWeek.Tuesday);
            r.ByMonthDay.AddRange([2, 3, 4, 5, 6, 7, 8]);

            Console.WriteLine(r.ToStringWithStartDateTime());

            // Not forever for the test
            foreach(DateTime dt in r.InstancesBetween(r.StartDateTime, new DateTime(2004, 11, 3)))
                Console.WriteLine(dt);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("The 3rd instance into the month of one of Tuesday, Wednesday or Thursday, for " +
                "the next 3 months:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 4, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.MaximumOccurrences = 3;
            r.BySetPos.Add(3);
            r.ByDay.AddRange([DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday]);

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("The 2nd to last weekday of the month:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 29, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.BySetPos.Add(-2);
            r.ByDay.AddRange([ DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
                DayOfWeek.Thursday, DayOfWeek.Friday ]);

            Console.WriteLine(r.ToStringWithStartDateTime());

            // Not forever for the test
            foreach(DateTime dt in r.InstancesBetween(r.StartDateTime, new DateTime(1998, 9, 29)))
                Console.WriteLine(dt);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every 3 hours from 9:00 AM to 5:00 PM on a specific day:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Hourly;
            r.Interval = 3;
            r.RecurUntil = new DateTime(1997, 9, 2, 17, 0, 0);

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every 15 minutes for 6 occurrences:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Minutely;
            r.Interval = 15;
            r.MaximumOccurrences = 6;

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every hour and a half for 4 occurrences:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Minutely;
            r.Interval = 90;
            r.MaximumOccurrences = 4;

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every 20 minutes from 9:00 AM to 4:40 PM every day:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Daily;
            r.ByHour.AddRange([9, 10, 11, 12, 13, 14, 15, 16]);
            r.ByMinute.AddRange([0, 20, 40]);

            Console.WriteLine(r.ToStringWithStartDateTime());

            // Not forever for the test
            foreach(DateTime dt in r.InstancesBetween(r.StartDateTime, new DateTime(1997, 9, 4)))
                Console.WriteLine(dt);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------
            
            // Like the last one but with a MINUTELY frequency
            Console.WriteLine("Every 20 minutes from 9:00 AM to 4:40 PM every day:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Minutely;
            r.Interval = 20;
            r.ByHour.AddRange([9, 10, 11, 12, 13, 14, 15, 16]);

            Console.WriteLine(r.ToStringWithStartDateTime());

            // Not forever for the test
            foreach(DateTime dt in r.InstancesBetween(r.StartDateTime, new DateTime(1997, 9, 4)))
                Console.WriteLine(dt);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("An example where the days generated makes a difference because of WKST:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 8, 5, 9, 0, 0);
            r.Frequency = RecurFrequency.Weekly;
            r.Interval = 2;
            r.MaximumOccurrences = 4;
            r.ByDay.AddRange([DayOfWeek.Tuesday, DayOfWeek.Sunday]);

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Changing only WKST from the default MO to SU yields different results:");

            r.Reset();
            r.StartDateTime = new DateTime(1997, 8, 5, 9, 0, 0);
            r.Frequency = RecurFrequency.Weekly;
            r.Interval = 2;
            r.MaximumOccurrences = 4;
            r.WeekStart = DayOfWeek.Sunday;
            r.ByDay.AddRange([DayOfWeek.Tuesday, DayOfWeek.Sunday]);

            Console.WriteLine(r.ToStringWithStartDateTime());

            foreach(DateTime dt in r)
                Console.WriteLine(dt);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            // Now do the same one but use the calendar classes to demonstrate the time zone abilities
            Console.WriteLine("\n\nThe next part will calculate instances using the calendar classes with " +
                "time zone support.\nPress ENTER to continue...");
            Console.ReadLine();

            // First, set up the time zone information.  If you don't care about the time zone, you can skip this
            // stuff and everything will be calculated in local time.
            VTimeZone vtz = new();
            vtz.TimeZoneId.Value = "US-Eastern";

            // Set the standard time observance rule
            ObservanceRule obr = vtz.ObservanceRules.Add(ObservanceRuleType.Standard);

            obr.StartDateTime.DateTimeValue = new DateTime(1970, 10, 25, 2, 0, 0);
            obr.OffsetFrom.TimeSpanValue = TimeSpan.FromHours(-4);
            obr.OffsetTo.TimeSpanValue = TimeSpan.FromHours(-5);

            RRuleProperty rrule = new();
            rrule.Recurrence.RecurYearly(DayOccurrence.Last, DaysOfWeek.Sunday, 10, 1);
            obr.RecurrenceRules.Add(rrule);

            obr.TimeZoneNames.Add("Eastern Standard Time");

            // Set the daylight saving time observance rule
            obr = vtz.ObservanceRules.Add(ObservanceRuleType.Daylight);

            obr.StartDateTime.DateTimeValue = new DateTime(1970, 4, 5, 2, 0, 0);
            obr.OffsetFrom.TimeSpanValue = TimeSpan.FromHours(-5);
            obr.OffsetTo.TimeSpanValue = TimeSpan.FromHours(-4);

            rrule = new RRuleProperty();
            rrule.Recurrence.RecurYearly(DayOccurrence.First, DaysOfWeek.Sunday, 4, 1);
            obr.RecurrenceRules.Add(rrule);

            obr.TimeZoneNames.Add("Eastern Daylight Time");

            VCalendar.TimeZones.Add(vtz);

            // Now we'll set up an event to use for the calculations.  We don't need a VCalendar object as we are
            // just generating recurring instances.
            VEvent vevent = new();

            // Add an RRULE property
            rrule = new RRuleProperty();
            vevent.RecurrenceRules.Add(rrule);

            // Get a reference to the recurrence rule property's recurrence and the event's start date property
            // for convenience.
            r = rrule.Recurrence;

            StartDateProperty startDateTime = vevent.StartDateTime;

            // Set the time zone of the event's start date to the same ID used for the time zone component above
            vevent.StartDateTime.TimeZoneId = "US-Eastern";

            //-----------------------------------------------------------------

            Console.WriteLine("Daily for 10 occurrences:");

            // The start date is set on the event.  We use the TimeZoneDateTime property to indicate that the
            // date/time is in the time zone.  Using DateTimeValue instead would convert the value from local
            // time to the time zone.
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Daily;
            r.MaximumOccurrences = 10;

            // Get the instances expressed in the time zone's time and in local time
            DateTimeInstanceCollection dtiTZ = vevent.AllInstances(false);
            DateTimeInstanceCollection dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            // We'll just display the start time from each instance.  End time and duration info is also present
            // in the returned instances.
            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Daily until December 24, 1997:");

            // We'll re-use the same recurrence object.  Just reset it first.
            r.Reset();

            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Daily;

            // Note that the recurrence rules in an event determine the end date of the event, NOT the EndDate
            // (DTEND) property of the event.  The end date property on the event simply determines the duration
            // of each instance.  Another thing to remember is that RecurUntil is specified in Universal Time
            // which ends up in local time when recurrence instances are generated.  If specify times in another
            // time zone, be sure to set the RecurUntil time as a time in that time zone.
            r.RecurUntil = new DateTime(1997, 12, 24, 5, 0, 0).ToLocalTime();

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every other day - forever:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Daily;
            r.Interval = 2;

            // We'll limit the output of the "forever" rules by using the InstancesBetween() method to limit the
            // output to a range of dates and enumerate the returned DateTimeCollection.
            dtiTZ = vevent.InstancesBetween(startDateTime.TimeZoneDateTime, new DateTime(1997, 12, 5), false);

            // For local time, the start and end date must be in local time.  To match the range in the prior
            // call, we use the DateTimeValue property of the start date property and convert a value in
            // Universal Time to local time.
            dtiLocal = vevent.InstancesBetween(startDateTime.DateTimeValue,
                new DateTime(1997, 12, 5, 5, 0, 0).ToLocalTime(), true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every 10 days, 5 occurrences:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Daily;
            r.Interval = 10;
            r.MaximumOccurrences = 5;

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Everyday in January, for 3 years:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1998, 1, 1, 9, 0, 0);
            r.Frequency = RecurFrequency.Yearly;
            r.RecurUntil = new DateTime(2000, 1, 31, 14, 0, 0).ToLocalTime();
            r.ByMonth.Add(1);

            // When adding days without an instance value, you can use the helper method on the collection that
            // takes an array of DayOfWeek values rather than constructing an array of DayInstance objects.
            r.ByDay.AddRange([ DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
                DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday ]);

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            // Like the last one but using a DAILY frequency
            Console.WriteLine("Everyday in January, for 3 years:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1998, 1, 1, 9, 0, 0);
            r.Frequency = RecurFrequency.Daily;
            r.RecurUntil = new DateTime(2000, 1, 31, 14, 0, 0).ToLocalTime();
            r.ByMonth.Add(1);

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Weekly for 10 occurrences:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Weekly;
            r.MaximumOccurrences = 10;

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Weekly until December 24, 1997:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Weekly;
            r.RecurUntil = new DateTime(1997, 12, 24, 5, 0, 0).ToLocalTime();

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every other week - forever:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Weekly;
            r.Interval = 2;
            r.WeekStart = DayOfWeek.Sunday;

            // Not forever for the test
            dtiTZ = vevent.InstancesBetween(startDateTime.TimeZoneDateTime, new DateTime(1999, 2, 10), false);
            dtiLocal = vevent.InstancesBetween(startDateTime.DateTimeValue,
                new DateTime(1999, 2, 10, 5, 0, 0).ToLocalTime(), true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Weekly on Tuesday and Thursday for 5 weeks:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Weekly;
            r.RecurUntil = new DateTime(1997, 10, 07, 4, 0, 0).ToLocalTime();
            r.WeekStart = DayOfWeek.Sunday;
            r.ByDay.AddRange([DayOfWeek.Tuesday, DayOfWeek.Thursday]);

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------
            
            // Like the last one but using a count
            Console.WriteLine("Weekly on Tuesday and Thursday for 5 weeks:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Weekly;
            r.MaximumOccurrences = 10;
            r.WeekStart = DayOfWeek.Sunday;
            r.ByDay.AddRange([DayOfWeek.Tuesday, DayOfWeek.Thursday]);

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            // The inclusion of DTSTART in a recurrence set is a feature of the calendar objects, NOT the
            // recurrence engine.  So, for this example, the start date will be included.
            Console.WriteLine("Every other week on Monday, Wednesday and Friday until December 24, 1997, but " +
                "starting on Tuesday, September 2, 1997:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Weekly;
            r.Interval = 2;
            r.WeekStart = DayOfWeek.Sunday;
            r.RecurUntil = new DateTime(1997, 12, 24, 5, 0, 0).ToLocalTime();
            r.ByDay.AddRange([DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday]);

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every other week on Tuesday and Thursday, for 8 occurrences:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Weekly;
            r.Interval = 2;
            r.WeekStart = DayOfWeek.Sunday;
            r.MaximumOccurrences = 8;
            r.ByDay.AddRange([DayOfWeek.Tuesday, DayOfWeek.Thursday]);

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName,  dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Monthly on the 1st Friday for ten occurrences:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 5, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.MaximumOccurrences = 10;

            // There are also helper methods to add single instances
            r.ByDay.Add(1, DayOfWeek.Friday);

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Monthly on the 1st Friday until December 24, 1997:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 5, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.RecurUntil = new DateTime(1997, 12, 24, 5, 0, 0).ToLocalTime();
            r.ByDay.Add(1, DayOfWeek.Friday);

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every other month on the 1st and last Sunday of the month for 10 occurrences:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 7, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.Interval = 2;
            r.MaximumOccurrences = 10;
            r.ByDay.AddRange([new DayInstance(1, DayOfWeek.Sunday), new DayInstance(-1, DayOfWeek.Sunday)]);

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Monthly on the second to last Monday of the month for 6 months:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 22, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.MaximumOccurrences = 6;
            r.ByDay.Add(-2, DayOfWeek.Monday);

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Monthly on the third to the last day of the month, forever:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 28, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.ByMonthDay.Add(-3);

            // Not forever for the test
            dtiTZ = vevent.InstancesBetween(startDateTime.TimeZoneDateTime, new DateTime(1998, 2, 28), false);
            dtiLocal = vevent.InstancesBetween(startDateTime.DateTimeValue,
                new DateTime(1998, 2, 28, 5, 0, 0).ToLocalTime(), true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Monthly on the 2nd and 15th of the month for 10 occurrences:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.MaximumOccurrences = 10;
            r.ByMonthDay.AddRange([2, 15]);

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Monthly on the first and last day of the month for 10 occurrences:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 30, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.MaximumOccurrences = 10;
            r.ByMonthDay.AddRange([1, -1]);

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every 18 months on the 10th through 15th of the month for 10 occurrences:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 10, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.Interval = 18;
            r.MaximumOccurrences = 10;
            r.ByMonthDay.AddRange([10, 11, 12, 13, 14, 15]);

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every Tuesday, every other month:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.Interval = 2;
            r.ByDay.Add(DayOfWeek.Tuesday);

            // Not forever for the test
            dtiTZ = vevent.InstancesBetween(startDateTime.TimeZoneDateTime, new DateTime(1998, 4, 1), false);
            dtiLocal = vevent.InstancesBetween(startDateTime.DateTimeValue,
                new DateTime(1998, 4, 1, 5, 0, 0).ToLocalTime(), true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Yearly in June and July for 10 occurrences:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 6, 10, 9, 0, 0);
            r.Frequency = RecurFrequency.Yearly;
            r.MaximumOccurrences = 10;
            r.ByMonth.AddRange([6, 7]);

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every other year in January, February, and March for 10 occurrences:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 3, 10, 9, 0, 0);
            r.Frequency = RecurFrequency.Yearly;
            r.Interval = 2;
            r.MaximumOccurrences = 10;
            r.ByMonth.AddRange([1, 2, 3]);

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every 3rd year on the 1st, 100th and 200th day for 10 occurrences:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 1, 1, 9, 0, 0);
            r.Frequency = RecurFrequency.Yearly;
            r.Interval = 3;
            r.MaximumOccurrences = 10;
            r.ByYearDay.AddRange([1, 100, 200]);

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every 20th Monday of the year, forever:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 5, 19, 9, 0, 0);
            r.Frequency = RecurFrequency.Yearly;
            r.ByDay.Add(20, DayOfWeek.Monday);

            // Not forever for the test
            dtiTZ = vevent.InstancesBetween(startDateTime.TimeZoneDateTime, new DateTime(2004, 5, 19), false);
            dtiLocal = vevent.InstancesBetween(startDateTime.DateTimeValue,
                new DateTime(2004, 5, 19, 4, 0, 0).ToLocalTime(), true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Monday of week number 20 (where the default start of the week is Monday), forever:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 5, 12, 9, 0, 0);
            r.Frequency = RecurFrequency.Yearly;
            r.ByWeekNo.Add(20);
            r.ByDay.Add(DayOfWeek.Monday);

            // Not forever for the test
            dtiTZ = vevent.InstancesBetween(startDateTime.TimeZoneDateTime, new DateTime(2004, 5, 19), false);
            dtiLocal = vevent.InstancesBetween(startDateTime.DateTimeValue,
                new DateTime(2004, 5, 19, 4, 0, 0).ToLocalTime(), true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every Thursday in March, forever:");
            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 3, 13, 9, 0, 0);
            r.Frequency = RecurFrequency.Yearly;
            r.ByMonth.Add(3);
            r.ByDay.Add(DayOfWeek.Thursday);

            // Not forever for the test
            dtiTZ = vevent.InstancesBetween(startDateTime.TimeZoneDateTime, new DateTime(1999, 4, 1), false);
            dtiLocal = vevent.InstancesBetween(startDateTime.DateTimeValue,
                new DateTime(1999, 4, 1, 5, 0, 0).ToLocalTime(), true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every Thursday, but only during June, July, and August, forever:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 6, 5, 9, 0, 0);
            r.Frequency = RecurFrequency.Yearly;
            r.ByMonth.AddRange([6, 7, 8]);
            r.ByDay.Add(DayOfWeek.Thursday);

            // Not forever for the test
            dtiTZ = vevent.InstancesBetween(startDateTime.TimeZoneDateTime, new DateTime(1999, 9, 1), false);
            dtiLocal = vevent.InstancesBetween(startDateTime.DateTimeValue,
                new DateTime(1999, 9, 1, 4, 0, 0).ToLocalTime(), true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every Friday the 13th, forever:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.ByDay.Add(DayOfWeek.Friday);
            r.ByMonthDay.Add(13);

            // This one uses an EXDATE property to remove the starting date
            ExDateProperty exdate = vevent.ExceptionDates.Add(startDateTime.TimeZoneDateTime);
            exdate.TimeZoneId = startDateTime.TimeZoneId;

            // Not forever for the test
            dtiTZ = vevent.InstancesBetween(startDateTime.TimeZoneDateTime, new DateTime(2003, 12, 31), false);
            dtiLocal = vevent.InstancesBetween(startDateTime.DateTimeValue,
                new DateTime(2003, 12, 31, 5, 0, 0).ToLocalTime(), true);

            Console.Write(startDateTime.ToString());
            Console.Write(exdate.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            // Clear the exception date
            vevent.ExceptionDates.Clear();

            //-----------------------------------------------------------------

            Console.WriteLine("The first Saturday that follows the first Sunday of the month, forever:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 13, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.ByDay.Add(DayOfWeek.Saturday);
            r.ByMonthDay.AddRange([7, 8, 9, 10, 11, 12, 13]);

            // Not forever for the test
            dtiTZ = vevent.InstancesBetween(startDateTime.TimeZoneDateTime, new DateTime(1998, 7, 1), false);
            dtiLocal = vevent.InstancesBetween(startDateTime.DateTimeValue,
                new DateTime(1998, 7, 1, 4, 0, 0).ToLocalTime(), true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every four years, the first Tuesday after a Monday in November, forever (U.S. " +
                "Presidential Election day):");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1996, 11, 5, 9, 0, 0);
            r.Frequency = RecurFrequency.Yearly;
            r.Interval = 4;
            r.ByMonth.Add(11);
            r.ByDay.Add(DayOfWeek.Tuesday);
            r.ByMonthDay.AddRange([2, 3, 4, 5, 6, 7, 8]);

            // Not forever for the test
            dtiTZ = vevent.InstancesBetween(startDateTime.TimeZoneDateTime, new DateTime(2004, 11, 3), false);
            dtiLocal = vevent.InstancesBetween(startDateTime.DateTimeValue,
                new DateTime(2004, 11, 3, 5, 0, 0).ToLocalTime(), true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("The 3rd instance into the month of one of Tuesday, Wednesday or Thursday, for " +
                "the next 3 months:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 4, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.MaximumOccurrences = 3;
            r.BySetPos.Add(3);
            r.ByDay.AddRange([DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday]);

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("The 2nd to last weekday of the month:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 29, 9, 0, 0);
            r.Frequency = RecurFrequency.Monthly;
            r.BySetPos.Add(-2);
            r.ByDay.AddRange([ DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
                DayOfWeek.Thursday, DayOfWeek.Friday ]);

            // Not forever for the test
            dtiTZ = vevent.InstancesBetween(startDateTime.TimeZoneDateTime, new DateTime(1998, 9, 29), false);
            dtiLocal = vevent.InstancesBetween(startDateTime.DateTimeValue,
                new DateTime(1998, 9, 29, 4, 0, 0).ToLocalTime(), true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every 3 hours from 9:00 AM to 5:00 PM on a specific day:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Hourly;
            r.Interval = 3;
            r.RecurUntil = new DateTime(1997, 9, 2, 21, 0, 0).ToLocalTime();

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every 15 minutes for 6 occurrences:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Minutely;
            r.Interval = 15;
            r.MaximumOccurrences = 6;

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every hour and a half for 4 occurrences:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Minutely;
            r.Interval = 90;
            r.MaximumOccurrences = 4;

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Every 20 minutes from 9:00 AM to 4:40 PM every day:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Daily;
            r.ByHour.AddRange([9, 10, 11, 12, 13, 14, 15, 16]);
            r.ByMinute.AddRange([0, 20, 40]);

            // Not forever for the test
            dtiTZ = vevent.InstancesBetween(startDateTime.TimeZoneDateTime, new DateTime(1997, 9, 4), false);
            dtiLocal = vevent.InstancesBetween(startDateTime.DateTimeValue,
                new DateTime(1997, 9, 4, 4, 0, 0).ToLocalTime(), true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            // Like the last one but with a MINUTELY frequency
            Console.WriteLine("Every 20 minutes from 9:00 AM to 4:40 PM every day:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 9, 2, 9, 0, 0);
            r.Frequency = RecurFrequency.Minutely;
            r.Interval = 20;
            r.ByHour.AddRange([9, 10, 11, 12, 13, 14, 15, 16]);

            // Not forever for the test
            dtiTZ = vevent.InstancesBetween(startDateTime.TimeZoneDateTime, new DateTime(1997, 9, 4), false);
            dtiLocal = vevent.InstancesBetween(startDateTime.DateTimeValue,
                new DateTime(1997, 9, 4, 4, 0, 0).ToLocalTime(), true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("... Forever ...\nPress ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("An example where the days generated makes a difference because of WKST:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 8, 5, 9, 0, 0);
            r.Frequency = RecurFrequency.Weekly;
            r.Interval = 2;
            r.MaximumOccurrences = 4;
            r.ByDay.AddRange([DayOfWeek.Tuesday, DayOfWeek.Sunday]);

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            //-----------------------------------------------------------------

            Console.WriteLine("Changing only WKST from the default MO to SU yields different results:");

            r.Reset();
            startDateTime.TimeZoneDateTime = new DateTime(1997, 8, 5, 9, 0, 0);
            r.Frequency = RecurFrequency.Weekly;
            r.Interval = 2;
            r.MaximumOccurrences = 4;
            r.WeekStart = DayOfWeek.Sunday;
            r.ByDay.AddRange([DayOfWeek.Tuesday, DayOfWeek.Sunday]);

            dtiTZ = vevent.AllInstances(false);
            dtiLocal = vevent.AllInstances(true);

            Console.Write(startDateTime.ToString());
            Console.Write(rrule.ToString());

            for(idx = 0; idx < dtiTZ.Count; idx++)
                Console.WriteLine("TZ: {0:G} {1}    Local: {2:G} {3}", dtiTZ[idx].StartDateTime,
                    dtiTZ[idx].AbbreviatedStartTimeZoneName, dtiLocal[idx].StartDateTime,
                    dtiLocal[idx].AbbreviatedStartTimeZoneName);

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
		}
	}
}
