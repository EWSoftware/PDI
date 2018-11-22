'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : PDIDatesTest.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 11/20/2018
' Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
' Compiler: Visual Basic .NET
'
' This is a console mode application that runs through a few simple configurations to test the basics in the
' date utility, holiday, and recurrence classes.
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 12/01/2004  EFW  Created the code
'================================================================================================================

Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Xml.Serialization

Imports EWSoftware.PDI

Module PDIDatesTest

    ' This is used to test the EWSoftware.PDI namespace date utility and recurrence classes.  It displays
    ' holidays using the first year specified.  It then runs through each year and displays the holidays found.
    ' It also runs through some simple recurrence configurations.
    Sub Main(args As String())
        Dim dt, testDate, nextDate As DateTime
        Dim holidayDates As HashSet(Of DateTime)
        Dim recurDates As DateTimeCollection
        Dim yearFrom, yearTo, idx As Integer

        If args.Length <> 2 Then
            Console.WriteLine("Using current year +/- 4 years for testing" + Environment.NewLine)

            yearFrom = DateTime.Today.Year - 4
            yearTo = yearFrom + 8
        Else
            Try
                yearFrom = Convert.ToInt32(args(0))
                yearTo = Convert.ToInt32(args(1))

                If yearFrom < 1 Then yearFrom = 1
                If yearFrom > 9999 Then yearFrom = 9999
                If yearTo < 1 Then yearTo = 1
                If yearTo > 9999 Then yearTo = 9999

                If yearFrom > yearTo Then
                    idx = yearFrom
                    yearFrom = yearTo
                    yearTo = idx
                End If
            Catch
                Console.WriteLine("Invalid year specified on command line")
                Return
            End Try
        End If

        ' Test DateUtils.CalculateOccurrenceDate
        Console.WriteLine("Fourth weekday in January 2018: {0:d}",
            DateUtils.CalculateOccurrenceDate(2018, 1, DayOccurrence.Fourth, DaysOfWeek.Weekdays, 0))

        Console.WriteLine("Fourth weekday in January 2018 + 2: {0:d}",
            DateUtils.CalculateOccurrenceDate(2018, 1, DayOccurrence.Fourth, DaysOfWeek.Weekdays, 2))

        Console.WriteLine("Last weekend day in January 2018: {0:d}",
            DateUtils.CalculateOccurrenceDate(2018, 1, DayOccurrence.Last, DaysOfWeek.Weekends, 0))

        Console.WriteLine("Last weekend day in January 2018 + 2: {0:d}",
            DateUtils.CalculateOccurrenceDate(2018, 1, DayOccurrence.Last, DaysOfWeek.Weekends, 2))

        ' Pause to review output
        Console.WriteLine("Press ENTER to continue")
        Console.ReadLine()

        ' Test DateUtils.DateFromWeek(), DateUtils.WeeksInYear(), and DateUtils.WeekFromDate()
        Dim weekFrom, weekTo As DateTime
        Dim year As Integer

        Console.WriteLine("Week start = Monday")
        Dim dow As DayOfWeek = DayOfWeek.Monday

        For year = yearFrom To yearTo
            For idx = 1 To 53
                If idx <> 53 Or DateUtils.WeeksInYear(year, dow) = 53 Then
                    weekFrom = DateUtils.DateFromWeek(year, idx, dow, 0)
                    weekTo = DateUtils.DateFromWeek(year, idx, dow, 6)

                    Console.WriteLine("{0} Week {1}: {2:d} - {3:d}  {4}", year, idx, weekFrom, weekTo,
                        DateUtils.WeekFromDate(weekFrom.AddDays(3), dow))
                End If
            Next idx

            ' Pause to review output
            Console.WriteLine("Press ENTER to continue")
            Console.ReadLine()
        Next year

        ' Test DateUtils.EasterSunday()
        Console.WriteLine("Easter - Gregorian")

        For idx = yearFrom To yearTo Step 3
            testDate = DateUtils.EasterSunday(idx, EasterMethod.Gregorian)
            Console.Write("{0}    {1}/{2:00}            ", idx, testDate.Month, testDate.Day)

            testDate = DateUtils.EasterSunday(idx + 1, EasterMethod.Gregorian)
            Console.Write("{0}    {1}/{2:00}            ", idx + 1, testDate.Month, testDate.Day)

            testDate = DateUtils.EasterSunday(idx + 2, EasterMethod.Gregorian)
            Console.WriteLine("{0}    {1}/{2:00}", idx + 2, testDate.Month, testDate.Day)
        Next idx

        Console.WriteLine("{0}Easter - Julian", Environment.NewLine)

        For idx = yearFrom To yearTo Step 3
            testDate = DateUtils.EasterSunday(idx, EasterMethod.Julian)
            Console.Write("{0}    {1}/{2:00}            ", idx, testDate.Month, testDate.Day)

            testDate = DateUtils.EasterSunday(idx + 1, EasterMethod.Julian)
            Console.Write("{0}    {1}/{2:00}            ", idx + 1, testDate.Month, testDate.Day)

            testDate = DateUtils.EasterSunday(idx + 2, EasterMethod.Julian)
            Console.WriteLine("{0}    {1}/{2:00}", idx + 2, testDate.Month, testDate.Day)
        Next idx

        Console.WriteLine("{0}Easter - Orthodox", Environment.NewLine)

        For idx = yearFrom To yearTo Step 3
            testDate = DateUtils.EasterSunday(idx, EasterMethod.Orthodox)
            Console.Write("{0}    {1}/{2:00}            ", idx, testDate.Month, testDate.Day)

            testDate = DateUtils.EasterSunday(idx + 1, EasterMethod.Orthodox)
            Console.Write("{0}    {1}/{2:00}            ", idx + 1, testDate.Month, testDate.Day)

            testDate = DateUtils.EasterSunday(idx + 2, EasterMethod.Orthodox)
            Console.WriteLine("{0}    {1}/{2:00}", idx + 2, testDate.Month, testDate.Day)
        Next idx

        ' Pause to review output
        Console.WriteLine("Press ENTER to continue")
        Console.ReadLine()

        ' Test DateUtils.FromISO8601String and DateUtils.FromISO8601TimeZone
        Console.WriteLine("Expressed in Universal Time")
        Console.WriteLine("20180314 = {0}", DateUtils.FromISO8601String("20180314", False))
        Console.WriteLine("20180314T10 = {0}", DateUtils.FromISO8601String("20180314T10", False))
        Console.WriteLine("20180314T1025 = {0}", DateUtils.FromISO8601String("20180314T1025", False))
        Console.WriteLine("20180314T102531 = {0}", DateUtils.FromISO8601String("20180314T102531", False))
        Console.WriteLine("20180314T102531.123 = {0:d} {0:HH:mm:ss.fff}",
            DateUtils.FromISO8601String("20180314T102531.123", False))
        Console.WriteLine("20180314T102531.98Z = {0:d} {0:HH:mm:ss.fff}",
            DateUtils.FromISO8601String("20180314T102531.98Z", False))
        Console.WriteLine("20180314T102531-04 = {0}", DateUtils.FromISO8601String("20180314T102531-04", False))
        Console.WriteLine("20180314T102531.123+0830 = {0}", DateUtils.FromISO8601String("20180314T102531.123+0830", False))

        Console.WriteLine("{0}2018-03-14 = {1}", Environment.NewLine, DateUtils.FromISO8601String("2018-03-14", False))
        Console.WriteLine("2018-03-14T10 = {0}", DateUtils.FromISO8601String("2018-03-14T10", False))
        Console.WriteLine("2018-03-14T10:25 = {0}", DateUtils.FromISO8601String("2018-03-14T10:25", False))
        Console.WriteLine("2018-03-14T10:25:31 = {0}", DateUtils.FromISO8601String("2018-03-14T10:25:31", False))
        Console.WriteLine("2018-03-14T10:25:31.123 = {0:d} {0:HH:mm:ss.fff}",
            DateUtils.FromISO8601String("2018-03-14T10:25:31.123", False))
        Console.WriteLine("2018-03-14T10:25:31.98Z = {0:d} {0:HH:mm:ss.fff}",
            DateUtils.FromISO8601String("2018-03-14T10:25:31.98Z", False))
        Console.WriteLine("2018-03-14T10:25:31-04 = {0}", DateUtils.FromISO8601String("2018-03-14T10:25:31-04", False))
        Console.WriteLine("2018-03-14T10:25:31+08:30 = {0}", DateUtils.FromISO8601String("2018-03-14T10:25:31+08:30", False))

        ' Test DateUtils.FromISO8601String and DateUtils.FromISO8601TimeZone
        Console.WriteLine("{0}Expressed in Local Time", Environment.NewLine)
        Console.WriteLine("20180314 = {0}", DateUtils.FromISO8601String("20180314", True))
        Console.WriteLine("20180314T10 = {0}", DateUtils.FromISO8601String("20180314T10", True))
        Console.WriteLine("20180314T1025 = {0}", DateUtils.FromISO8601String("20180314T1025", True))
        Console.WriteLine("20180314T102531 = {0}", DateUtils.FromISO8601String("20180314T102531", True))
        Console.WriteLine("20180314T102531.123 = {0:d} {0:HH:mm:ss.fff}",
            DateUtils.FromISO8601String("20180314T102531.123", True))
        Console.WriteLine("20180314T102531.98Z = {0:d} {0:HH:mm:ss.fff}",
            DateUtils.FromISO8601String("20180314T102531.98Z", True))
        Console.WriteLine("20180314T102531-04 = {0}", DateUtils.FromISO8601String("20180314T102531-04", True))
        Console.WriteLine("20180314T102531.123+0830 = {0}", DateUtils.FromISO8601String("20180314T102531.123+0830", True))

        Console.WriteLine("{0}2018-03-14 = {1}", Environment.NewLine, DateUtils.FromISO8601String("2018-03-14", True))
        Console.WriteLine("2018-03-14T10 = {0}", DateUtils.FromISO8601String("2018-03-14T10", True))
        Console.WriteLine("2018-03-14T10:25 = {0}", DateUtils.FromISO8601String("2018-03-14T10:25", True))
        Console.WriteLine("2018-03-14T10:25:31 = {0}", DateUtils.FromISO8601String("2018-03-14T10:25:31", True))
        Console.WriteLine("2018-03-14T10:25:31.123 = {0:d} {0:HH:mm:ss.fff}",
            DateUtils.FromISO8601String("2018-03-14T10:25:31.123", True))
        Console.WriteLine("2018-03-14T10:25:31.98Z = {0:d} {0:HH:mm:ss.fff}",
            DateUtils.FromISO8601String("2018-03-14T10:25:31.98Z", True))
        Console.WriteLine("2018-03-14T10:25:31-04 = {0}", DateUtils.FromISO8601String("2018-03-14T10:25:31-04", True))
        Console.WriteLine("2018-03-14T10:25:31+08:30 = {0}", DateUtils.FromISO8601String("2018-03-14T10:25:31+08:30", True))

        Dim ts As TimeSpan = DateUtils.FromISO8601TimeZone("+08")
        Console.WriteLine("{0}+08 = {1} hours {2} minutes", Environment.NewLine, ts.Hours, ts.Minutes)
        ts = DateUtils.FromISO8601TimeZone("-08")
        Console.WriteLine("-08 = {0} hours {1} minutes", ts.Hours, ts.Minutes)
        ts = DateUtils.FromISO8601TimeZone("-0830")
        Console.WriteLine("-0830 = {0} hours {1} minutes", ts.Hours, ts.Minutes)
        ts = DateUtils.FromISO8601TimeZone("+08:30")
        Console.WriteLine("+08:30 = {0} hours {1} minutes", ts.Hours, ts.Minutes)

        ' Restrict bad date part values to their minimum/maximum
        Console.WriteLine("{0}Bad date values test", Environment.NewLine)

        Console.WriteLine("0000-01-01T10:25:00 = {0}",
            DateUtils.FromISO8601String("0000-01-01T10:25:00", true))
        Console.WriteLine("0000-00-01T10:25:00 = {0}",
            DateUtils.FromISO8601String("0000-00-01T10:25:00", true))
        Console.WriteLine("0000-13-01T10:25:00 = {0}",
            DateUtils.FromISO8601String("0000-13-01T10:25:00", true))
        Console.WriteLine("0000-00-00T10:25:00 = {0}",
            DateUtils.FromISO8601String("0000-00-00T10:25:00", true))
        Console.WriteLine("0000-00-32T10:25:00 = {0}",
            DateUtils.FromISO8601String("0000-00-32T10:25:00", true))

        ' Pause to review output
        Console.WriteLine("Press ENTER to continue")
        Console.ReadLine()

        ' Test the Duration class
        Console.WriteLine("Assumptions: 1 year = {0} days, 1 month = {1} days{2}", Duration.DaysInOneYear,
            Duration.DaysInOneMonth, Environment.NewLine)

        Dim d As New Duration("P1Y2M3W4DT5H6M7S")

        Console.WriteLine("P1Y2M3W4DT5H6M7S = {0}", d.ToString())
        Console.WriteLine("P1Y2M3W4DT5H6M7S = {0} (max units = months)", d.ToString(Duration.MaxUnit.Months))

        d = New Duration("P10Y11MT16M12S")
        Console.WriteLine("P10Y11MT16M12S = {0}", d.ToString())

        d = New Duration("P5M2DT16M")
        Console.WriteLine("P5M2DT16M = {0}", d.ToString())

        d = New Duration("P7W")
        Console.WriteLine("P7W = {0}", d.ToString())
        Console.WriteLine("P7W = {0} (max units = weeks)", d.ToString(Duration.MaxUnit.Weeks))
        Console.WriteLine("P7W = {0} (max units = days)", d.ToString(Duration.MaxUnit.Days))

        d = New Duration("P7W2D")
        Console.WriteLine("P7W2D = {0}", d.ToString())
        Console.WriteLine("P7W2D = {0} (max units = weeks)", d.ToString(Duration.MaxUnit.Weeks))
        Console.WriteLine("P7W2D = {0} (max units = days)", d.ToString(Duration.MaxUnit.Days))

        d = New Duration("P5DT2S")
        Console.WriteLine("P5DT2S = {0}", d.ToString())

        d = New Duration("PT24H")
        Console.WriteLine("PT24H = {0}", d.ToString())
        Console.WriteLine("PT24H = {0} (max units = hours)", d.ToString(Duration.MaxUnit.Hours))
        Console.WriteLine("PT24H = {0} (max units = minutes)", d.ToString(Duration.MaxUnit.Minutes))
        Console.WriteLine("PT24H = {0} (max units = seconds)", d.ToString(Duration.MaxUnit.Seconds))

        d = New Duration("PT24H3M2S")
        Console.WriteLine("PT24H3M2S = {0}", d.ToString())
        Console.WriteLine("PT24H3M2S = {0} (max units = hours)", d.ToString(Duration.MaxUnit.Hours))
        Console.WriteLine("PT24H3M2S = {0} (max units = minutes)", d.ToString(Duration.MaxUnit.Minutes))
        Console.WriteLine("PT24H3M2S = {0} (max units = seconds)", d.ToString(Duration.MaxUnit.Seconds))

        d = New Duration("PT1H10M20S")
        Console.WriteLine("PT1H10M20S = {0}", d.ToString())

        d = New Duration("PT5M20S")
        Console.WriteLine("PT5M20S = {0}", d.ToString())

        d = New Duration("PT5S")
        Console.WriteLine("PT5S = {0}", d.ToString())

        d = New Duration("P0Y0M0W0DT0H0M0S")
        Console.WriteLine("P0Y0M0W0DT0H0M0S = {0}", d.ToString())

        ' Pause to review output
        Console.WriteLine("Press ENTER to continue")
        Console.ReadLine()

        d = New Duration("-P1Y2M3W4DT5H6M7S")
        Console.WriteLine("{0}-P1Y2M3W4DT5H6M7S = {1}", Environment.NewLine, d.ToString())
        Console.WriteLine("-P1Y2M3W4DT5H6M7S = {0} (max units = months)", d.ToString(Duration.MaxUnit.Months))

        d = New Duration("-P10Y11MT16M12S")
        Console.WriteLine("-P10Y11MT16M12S = {0}", d.ToString())

        d = New Duration("-P5M2DT16M")
        Console.WriteLine("-P5M2DT16M = {0}", d.ToString())

        d = New Duration("-P7W")
        Console.WriteLine("-P7W = {0}", d.ToString())
        Console.WriteLine("-P7W = {0} (max units = weeks)", d.ToString(Duration.MaxUnit.Weeks))
        Console.WriteLine("-P7W = {0} (max units = days)", d.ToString(Duration.MaxUnit.Days))

        d = New Duration("-P7W2D")
        Console.WriteLine("-P7W2D = {0}", d.ToString())
        Console.WriteLine("-P7W2D = {0} (max units = weeks)", d.ToString(Duration.MaxUnit.Weeks))
        Console.WriteLine("-P7W2D = {0} (max units = days)", d.ToString(Duration.MaxUnit.Days))

        d = New Duration("-P5DT2S")
        Console.WriteLine("-P5DT2S = {0}", d.ToString())

        d = New Duration("-PT24H")
        Console.WriteLine("-PT24H = {0}", d.ToString())
        Console.WriteLine("-PT24H = {0} (max units = hours)", d.ToString(Duration.MaxUnit.Hours))
        Console.WriteLine("-PT24H = {0} (max units = minutes)", d.ToString(Duration.MaxUnit.Minutes))
        Console.WriteLine("-PT24H = {0} (max units = seconds)", d.ToString(Duration.MaxUnit.Seconds))

        d = New Duration("-PT24H3M2S")
        Console.WriteLine("-PT24H3M2S = {0}", d.ToString())
        Console.WriteLine("-PT24H3M2S = {0} (max units = hours)", d.ToString(Duration.MaxUnit.Hours))
        Console.WriteLine("-PT24H3M2S = {0} (max units = minutes)", d.ToString(Duration.MaxUnit.Minutes))
        Console.WriteLine("-PT24H3M2S = {0} (max units = seconds)", d.ToString(Duration.MaxUnit.Seconds))

        d = New Duration("-PT1H10M20S")
        Console.WriteLine("-PT1H10M20S = {0}", d.ToString())

        d = New Duration("-PT5M20S")
        Console.WriteLine("-PT5M20S = {0}", d.ToString())

        d = New Duration("-PT5S")
        Console.WriteLine("-PT5S = {0}", d.ToString())

        d = New Duration("-P0Y0M0W0DT0H0M0S")
        Console.WriteLine("-P0Y0M0W0DT0H0M0S = {0}", d.ToString())

        ' Pause to review output
        Console.WriteLine("Press ENTER to continue")
        Console.ReadLine()

        ' Test the ToDescription() methods
        d = New Duration("P1Y2M3W4DT5H6M7S")
        Console.WriteLine("P1Y2M3W4DT5H6M7S = {0}", d.ToDescription())
        Console.WriteLine("P1Y2M3W4DT5H6M7S = {0} (max units = months)", d.ToDescription(Duration.MaxUnit.Months))

        d = New Duration("P10Y11MT16M12S")
        Console.WriteLine("P10Y11MT16M12S = {0}", d.ToDescription())

        d = New Duration("P5M2DT16M")
        Console.WriteLine("P5M2DT16M = {0}", d.ToDescription())

        d = New Duration("P7W")
        Console.WriteLine("P7W = {0}", d.ToDescription())
        Console.WriteLine("P7W = {0} (max units = weeks)", d.ToDescription(Duration.MaxUnit.Weeks))
        Console.WriteLine("P7W = {0} (max units = days)", d.ToDescription(Duration.MaxUnit.Days))

        d = New Duration("P7W2D")
        Console.WriteLine("P7W2D = {0}", d.ToDescription())
        Console.WriteLine("P7W2D = {0} (max units = weeks)", d.ToDescription(Duration.MaxUnit.Weeks))
        Console.WriteLine("P7W2D = {0} (max units = days)", d.ToDescription(Duration.MaxUnit.Days))

        d = New Duration("P5DT2S")
        Console.WriteLine("P5DT2S = {0}", d.ToDescription())

        d = New Duration("PT24H")
        Console.WriteLine("PT24H = {0}", d.ToDescription())
        Console.WriteLine("PT24H = {0} (max units = hours)", d.ToDescription(Duration.MaxUnit.Hours))
        Console.WriteLine("PT24H = {0} (max units = minutes)", d.ToDescription(Duration.MaxUnit.Minutes))
        Console.WriteLine("PT24H = {0} (max units = seconds)", d.ToDescription(Duration.MaxUnit.Seconds))

        d = New Duration("PT24H3M2S")
        Console.WriteLine("PT24H3M2S = {0}", d.ToDescription())
        Console.WriteLine("PT24H3M2S = {0} (max units = hours)", d.ToDescription(Duration.MaxUnit.Hours))
        Console.WriteLine("PT24H3M2S = {0} (max units = minutes)", d.ToDescription(Duration.MaxUnit.Minutes))
        Console.WriteLine("PT24H3M2S = {0} (max units = seconds)", d.ToDescription(Duration.MaxUnit.Seconds))

        d = New Duration("PT1H10M20S")
        Console.WriteLine("PT1H10M20S = {0}", d.ToDescription())

        d = New Duration("PT5M20S")
        Console.WriteLine("PT5M20S = {0}", d.ToDescription())

        d = New Duration("PT5S")
        Console.WriteLine("PT5S = {0}", d.ToDescription())

        d = New Duration("P0Y0M0W0DT0H0M0S")
        Console.WriteLine("P0Y0M0W0DT0H0M0S = {0}", d.ToDescription())

        ' Pause to review output
        Console.WriteLine("Press ENTER to continue")
        Console.ReadLine()

        d = New Duration("-P1Y2M3W4DT5H6M7S")
        Console.WriteLine("{0}-P1Y2M3W4DT5H6M7S = {1}", Environment.NewLine, d.ToDescription())
        Console.WriteLine("-P1Y2M3W4DT5H6M7S = {0} (max units = months)", d.ToDescription(Duration.MaxUnit.Months))

        d = New Duration("-P10Y11MT16M12S")
        Console.WriteLine("-P10Y11MT16M12S = {0}", d.ToDescription())

        d = New Duration("-P5M2DT16M")
        Console.WriteLine("-P5M2DT16M = {0}", d.ToDescription())

        d = New Duration("-P7W")
        Console.WriteLine("-P7W = {0}", d.ToDescription())
        Console.WriteLine("-P7W = {0} (max units = weeks)", d.ToDescription(Duration.MaxUnit.Weeks))
        Console.WriteLine("-P7W = {0} (max units = days)", d.ToDescription(Duration.MaxUnit.Days))

        d = New Duration("-P7W2D")
        Console.WriteLine("-P7W2D = {0}", d.ToDescription())
        Console.WriteLine("-P7W2D = {0} (max units = weeks)", d.ToDescription(Duration.MaxUnit.Weeks))
        Console.WriteLine("-P7W2D = {0} (max units = days)", d.ToDescription(Duration.MaxUnit.Days))

        d = New Duration("-P5DT2S")
        Console.WriteLine("-P5DT2S = {0}", d.ToDescription())

        d = New Duration("-PT24H")
        Console.WriteLine("-PT24H = {0}", d.ToDescription())
        Console.WriteLine("-PT24H = {0} (max units = hours)", d.ToDescription(Duration.MaxUnit.Hours))
        Console.WriteLine("-PT24H = {0} (max units = minutes)", d.ToDescription(Duration.MaxUnit.Minutes))
        Console.WriteLine("-PT24H = {0} (max units = seconds)", d.ToDescription(Duration.MaxUnit.Seconds))

        d = New Duration("-PT24H3M2S")
        Console.WriteLine("-PT24H3M2S = {0}", d.ToDescription())
        Console.WriteLine("-PT24H3M2S = {0} (max units = hours)", d.ToDescription(Duration.MaxUnit.Hours))
        Console.WriteLine("-PT24H3M2S = {0} (max units = minutes)", d.ToDescription(Duration.MaxUnit.Minutes))
        Console.WriteLine("-PT24H3M2S = {0} (max units = seconds)", d.ToDescription(Duration.MaxUnit.Seconds))

        d = New Duration("-PT1H10M20S")
        Console.WriteLine("-PT1H10M20S = {0}", d.ToDescription())

        d = New Duration("-PT5M20S")
        Console.WriteLine("-PT5M20S = {0}", d.ToDescription())

        d = New Duration("-PT5S")
        Console.WriteLine("-PT5S = {0}", d.ToDescription())

        d = New Duration("-P0Y0M0W0DT0H0M0S")
        Console.WriteLine("-P0Y0M0W0DT0H0M0S = {0}", d.ToDescription())

        ' Pause to review output
        Console.WriteLine("Press ENTER to continue")
        Console.ReadLine()

        ' Create a set of fixed and floating holidays
        Dim holidays As New HolidayCollection()

        holidays.AddFixed(1, 1, True, "New Year's Day")
        holidays.AddFloating(DayOccurrence.Third, DayOfWeek.Monday, 1, 0, _
            "Martin Luther King Day")
        holidays.AddFloating(DayOccurrence.Third, DayOfWeek.Monday, 2, 0, _
            "President's Day")
        holidays.AddFloating(DayOccurrence.Last, DayOfWeek.Monday, 5, 0, _
            "Memorial Day")
        holidays.AddFixed(7, 4, True, "Independence Day")
        holidays.AddFloating(DayOccurrence.First, DayOfWeek.Monday, 9, 0, _
            "Labor Day")
        holidays.AddFixed(11, 11, True, "Veteran's Day")
        holidays.AddFloating(DayOccurrence.Fourth, DayOfWeek.Thursday, 11, 0, _
            "Thanksgiving Day")
        holidays.AddFloating(DayOccurrence.Fourth, DayOfWeek.Thursday, 11, 1, _
            "Day After Thanksgiving")
        holidays.AddFixed(12, 25, True, "Christmas Day")

        ' Serialize the holidays to a file
        Try
            ' XML
            Using fs As New FileStream("Holidays.xml", FileMode.Create)
                Dim xs As New XmlSerializer(GetType(HolidayCollection))
                xs.Serialize(fs, holidays)
                Console.WriteLine("Holidays saved to Holidays.xml")
            End Using
#If NETFramework
            ' The SOAP formatter is only supported on the full .NET Framework
            ' SOAP
            Using fs As New FileStream("Holidays.soap", FileMode.Create)
                Dim sf As New System.Runtime.Serialization.Formatters.Soap.SoapFormatter()

                ' SOAP doesn't support generics directly so use an array
                sf.Serialize(fs, holidays.ToArray())
                Console.WriteLine("Holidays saved to Holidays.soap")
            End Using
#End If
            ' Binary
            Using fs As New FileStream("Holidays.bin", FileMode.Create)
                Dim bf As New BinaryFormatter()
                bf.Serialize(fs, holidays)
                Console.WriteLine("Holidays saved to Holidays.bin{0}", Environment.NewLine)
            End Using

        Catch ex As Exception
            Console.WriteLine("Unable to save holidays:{0}{1}", Environment.NewLine, ex.Message)

            If Not (ex.InnerException Is Nothing) Then
                Console.WriteLine(ex.InnerException.Message)

                If Not (ex.InnerException.InnerException Is Nothing) Then
                    Console.WriteLine(ex.InnerException.InnerException.Message)
                End If
            End If

        End Try

        ' Delete the collection and read it back in
        holidays = Nothing

        Try
            ' XML
            Using fs AS New FileStream("Holidays.xml", FileMode.Open)
                Dim xs As New XmlSerializer(GetType(HolidayCollection))
                holidays = DirectCast(xs.Deserialize(fs), HolidayCollection)
                Console.WriteLine("Holidays retrieved from Holidays.xml")
            End Using
#If NETFramework
            ' SOAP
            Using fs As New FileStream("Holidays.soap", FileMode.Open)
                Dim sf As New System.Runtime.Serialization.Formatters.Soap.SoapFormatter()

                ' As noted, SOAP doesn't support generics to an array is used instead
                holidays = New HolidayCollection(DirectCast(sf.Deserialize(fs), Holiday()))

                Console.WriteLine("Holidays retrieved from Holidays.soap")
            End Using
#End If
            ' Binary
            Using fs As New FileStream("Holidays.bin", FileMode.Open)
                Dim bf As New BinaryFormatter()
                holidays = DirectCast(bf.Deserialize(fs), HolidayCollection)
                Console.WriteLine("Holidays retrieved from Holidays.bin{0}", Environment.NewLine)
            End Using

        Catch ex As Exception
            Console.WriteLine("Unable to load holidays:{0}{1}", Environment.NewLine, ex.Message)

            If Not (ex.InnerException Is Nothing) Then
                Console.WriteLine(ex.InnerException.Message)

                If Not (ex.InnerException.InnerException Is Nothing) Then
                    Console.WriteLine(ex.InnerException.InnerException.Message)
                End If
            End If

        End Try

        ' Display the holidays added to the list
        Console.WriteLine("Holidays on file.  Is Holiday should be true for all.")

        For Each hol As Holiday In holidays
            Console.WriteLine("Holiday Date: {0:d}   Is Holiday: {1}  Description: {2}", hol.ToDateTime(yearFrom),
                holidays.IsHoliday(hol.ToDateTime(yearFrom)), hol.Description)
        Next

        ' Pause to review output
        Console.WriteLine("Press ENTER to continue")
        Console.ReadLine()

        ' Display holidays found in each year specified using the IsHoliday method
        Console.WriteLine("Looking for holidays using the IsHoliday method")

        testDate = New DateTime(yearFrom, 1, 1)

        Do While testDate.Year <= yearTo
            If holidays.IsHoliday(testDate) Then
                Console.WriteLine("Found holiday: {0:d} {1}", testDate, holidays.HolidayDescription(testDate))
            End If

            testDate = testDate.AddDays(1)

            ' Stop after each year to review output
            If testDate.Month = 1 And testDate.Day = 1 Then
                Console.WriteLine("Press ENTER to continue")
                Console.ReadLine()
            End If
        Loop

        ' One more time, but use a hash set returned by the HolidaysBetween method.  For bulk comparisons, this
        ' is faster than the above procedure using the IsHoliday method.
        Console.WriteLine("Looking for holidays using a collection from HolidayList")

        holidayDates = New HashSet(Of DateTime)(holidays.HolidaysBetween(yearFrom, yearTo))

        If holidayDates.Count <> 0
            testDate = New DateTime(yearFrom, 1, 1)

            Do While testDate.Year <= yearTo
                If holidayDates.Contains(testDate) Then
                    Console.WriteLine("Found holiday: {0:d} {1}", testDate, holidays.HolidayDescription(testDate))
                End If

                testDate = testDate.AddDays(1)

                ' Stop after each year to review output
                If testDate.Month = 1 And testDate.Day = 1 Then
                    Console.WriteLine("Press ENTER to continue")
                    Console.ReadLine()
                End If
            Loop
        End If

        '=============================
        ' Test recurrence
        Dim rRecur As New Recurrence()
        Recurrence.Holidays.AddRange(holidays)

        ' Disallow occurrences on any of the defined holidays
        rRecur.CanOccurOnHoliday = False

        testDate = New DateTime(yearFrom, 1, 1)

        ' Test daily recurrence
        rRecur.StartDateTime = testDate
        rRecur.RecurUntil = testDate.AddMonths(1)   ' For the enumerator
        rRecur.RecurDaily(2)

        ' Serialize the recurrence to a file
        Try
            ' XML
            Using fs As New FileStream("Recurrence.xml", FileMode.Create)
                Dim xs As New XmlSerializer(GetType(Recurrence))
                xs.Serialize(fs, rRecur)
                Console.WriteLine("Recurrence saved to Recurrence.xml")
            End Using
#If NETFramework
            ' SOAP
            Using fs As New FileStream("Recurrence.soap", FileMode.Create)
                Dim sf As New System.Runtime.Serialization.Formatters.Soap.SoapFormatter()
                sf.Serialize(fs, rRecur)
                Console.WriteLine("Recurrence saved to Recurrence.soap")
            End Using
#End If
            ' Binary
            Using fs As New FileStream("Recurrence.bin", FileMode.Create)
                Dim bf As New BinaryFormatter()
                bf.Serialize(fs, rRecur)
                Console.WriteLine("Recurrence saved to Recurrence.bin{0}", Environment.NewLine)
            End Using

        Catch ex As Exception
            Console.WriteLine("Unable to save recurrence:{0}{1}", Environment.NewLine, ex.Message)

            If Not (ex.InnerException Is Nothing) Then
                Console.WriteLine(ex.InnerException.Message)

                If Not (ex.InnerException.InnerException Is Nothing) Then
                    Console.WriteLine(ex.InnerException.InnerException.Message)
                End If
            End If

        End Try

        ' Deserialize the recurrence from a file
        rRecur = Nothing

        Try
            ' XML
            Using fs As New FileStream("Recurrence.xml", FileMode.Open)
                Dim xs As New XmlSerializer(GetType(Recurrence))
                rRecur = DirectCast(xs.Deserialize(fs), Recurrence)
                Console.WriteLine("Recurrence restored from Recurrence.xml")
            End Using
#If NETFramework
            ' SOAP
            Using fs As New FileStream("Recurrence.soap", FileMode.Open)
                Dim sf As New System.Runtime.Serialization.Formatters.Soap.SoapFormatter()
                rRecur = DirectCast(sf.Deserialize(fs), Recurrence)
                Console.WriteLine("Recurrence retrieved from Recurrence.soap")
            End Using
#End If
            ' Binary
            Using fs As New FileStream("Recurrence.bin", FileMode.Open)
                Dim bf As New BinaryFormatter()
                rRecur = DirectCast(bf.Deserialize(fs), Recurrence)
                Console.WriteLine("Recurrence retrieved from Recurrence.bin{0}", Environment.NewLine)
            End Using

        Catch ex As Exception
            Console.WriteLine("Unable to restore recurrence:{0}{1}", Environment.NewLine, ex.Message)

            If Not (ex.InnerException Is Nothing) Then
                Console.WriteLine(ex.InnerException.Message)

                If Not (ex.InnerException.InnerException Is Nothing) Then
                    Console.WriteLine(ex.InnerException.InnerException.Message)
                End If
            End If

        End Try

        recurDates = rRecur.InstancesBetween(testDate, testDate.AddMonths(1))

        Console.WriteLine(rRecur.ToStringWithStartDateTime())

        For Each dt In recurDates
            Console.WriteLine("(Collection) Daily recurrence on: {0:d}", dt)
        Next

        For Each dt In rRecur
            Console.WriteLine("(Enumerator) Daily recurrence on: {0:d}", dt)
        Next

        ' Test NextInstance().  This isn't the most efficient way of searching for lots of dates but is useful
        ' for one-off searches.
        nextDate = testDate

        Do
            nextDate = rRecur.NextInstance(nextDate, False)

            If nextDate <> DateTime.MinValue Then
                Console.WriteLine("(NextInstance) Daily recurrence on: {0:d}", nextDate)
                nextDate = nextDate.AddMinutes(1)
            End If

        Loop While nextDate <> DateTime.MinValue

        ' Pause to review output
        Console.WriteLine("Press ENTER to continue")
        Console.ReadLine()

        ' Test weekly recurrence
        rRecur.StartDateTime = testDate
        rRecur.RecurWeekly(1, DaysOfWeek.Monday Or DaysOfWeek.Wednesday)
        recurDates = rRecur.InstancesBetween(testDate, testDate.AddMonths(1))

        Console.WriteLine(rRecur.ToStringWithStartDateTime())

        For Each dt In recurDates
            Console.WriteLine("(Collection) Weekly recurrence on: {0:d}", dt)
        Next

        For Each dt In rRecur
            Console.WriteLine("(Enumerator) Weekly recurrence on: {0:d}", dt)
        Next

        nextDate = testDate

        Do
            nextDate = rRecur.NextInstance(nextDate, False)

            If nextDate <> DateTime.MinValue Then
                Console.WriteLine("(NextInstance) Weekly recurrence on: {0:d}", nextDate)
                nextDate = nextDate.AddMinutes(1)
            End If

        Loop While nextDate <> DateTime.MinValue

        ' Pause to review output
        Console.WriteLine("Press ENTER to continue")
        Console.ReadLine()

        ' Test monthly recurrence (option 1)
        rRecur.StartDateTime = testDate
        rRecur.RecurUntil = testDate.AddMonths(12)    ' For the enumerator
        rRecur.RecurMonthly(15, 2)
        recurDates = rRecur.InstancesBetween(testDate, testDate.AddMonths(12))

        Console.WriteLine(rRecur.ToStringWithStartDateTime())

        For Each dt In recurDates
            Console.WriteLine("(Collection) Monthly recurrence on: {0:d}", dt)
        Next

        For Each dt In rRecur
            Console.WriteLine("(Enumerator) Monthly recurrence on: {0:d}", dt)
        Next

        nextDate = testDate

        Do
            nextDate = rRecur.NextInstance(nextDate, False)

            If nextDate <> DateTime.MinValue Then
                Console.WriteLine("(NextInstance) Monthly recurrence on: {0:d}", nextDate)
                nextDate = nextDate.AddMinutes(1)
            End If

        Loop While nextDate <> DateTime.MinValue

        ' Pause to review output
        Console.WriteLine("Press ENTER to continue")
        Console.ReadLine()

        ' Test monthly recurrence (option 2)
        rRecur.RecurMonthly(DayOccurrence.Third, DaysOfWeek.Thursday, 3)
        recurDates = rRecur.InstancesBetween(testDate, testDate.AddMonths(12))

        Console.WriteLine(rRecur.ToStringWithStartDateTime())

        For Each dt In recurDates
            Console.WriteLine("(Collection) Monthly recurrence on: {0:d}", dt)
        Next

        For Each dt In rRecur
            Console.WriteLine("(Enumerator) Monthly recurrence on: {0:d}", dt)
        Next

        nextDate = testDate

        Do
            nextDate = rRecur.NextInstance(nextDate, False)

            If nextDate <> DateTime.MinValue Then
                Console.WriteLine("(NextInstance) Monthly recurrence on: {0:d}", nextDate)
                nextDate = nextDate.AddMinutes(1)
            End If

        Loop While nextDate <> DateTime.MinValue

        ' Pause to review output
        Console.WriteLine("Press ENTER to continue")
        Console.ReadLine()

        ' Test monthly recurrence (option 3 - a variation of option 2)
        rRecur.RecurMonthly(DayOccurrence.Third, DaysOfWeek.Weekends, 2)
        recurDates = rRecur.InstancesBetween(testDate, testDate.AddMonths(12))

        Console.WriteLine(rRecur.ToStringWithStartDateTime())

        For Each dt In recurDates
            Console.WriteLine("(Collection) Monthly recurrence on: {0:d}", dt)
        Next

        For Each dt In rRecur
            Console.WriteLine("(Enumerator) Monthly recurrence on: {0:d}", dt)
        Next

        nextDate = testDate

        Do
            nextDate = rRecur.NextInstance(nextDate, False)

            If nextDate <> DateTime.MinValue Then
                Console.WriteLine("(NextInstance) Monthly recurrence on: {0:d}", nextDate)
                nextDate = nextDate.AddMinutes(1)
            End If

        Loop While nextDate <> DateTime.MinValue

        ' Pause to review output
        Console.WriteLine("Press ENTER to continue")
        Console.ReadLine()

        ' Test yearly recurrence (option 1)
        rRecur.StartDateTime = testDate
        rRecur.RecurUntil = testDate.AddYears(5)  ' For the enumerator
        rRecur.RecurYearly(5, 24, 1)
        recurDates = rRecur.InstancesBetween(testDate, testDate.AddYears(5))

        Console.WriteLine(rRecur.ToStringWithStartDateTime())

        For Each dt In recurDates
            Console.WriteLine("(Collection) Yearly recurrence on: {0:d}", dt)
        Next

        For Each dt In rRecur
            Console.WriteLine("(Enumerator) Yearly recurrence on: {0:d}", dt)
        Next

        nextDate = testDate

        Do
            nextDate = rRecur.NextInstance(nextDate, False)

            If nextDate <> DateTime.MinValue Then
                Console.WriteLine("(NextInstance) Yearly recurrence on: {0:d}", nextDate)
                nextDate = nextDate.AddMinutes(1)
            End If

        Loop While nextDate <> DateTime.MinValue

        ' Pause to review output
        Console.WriteLine("Press ENTER to continue")
        Console.ReadLine()

        ' Test yearly recurrence (option 2)
        rRecur.RecurYearly(DayOccurrence.Last, DaysOfWeek.Sunday, 9, 2)
        recurDates = rRecur.InstancesBetween(testDate, testDate.AddYears(5))

        Console.WriteLine(rRecur.ToStringWithStartDateTime())

        For Each dt In recurDates
            Console.WriteLine("(Collection) Yearly recurrence on: {0:d}", dt)
        Next

        For Each dt In rRecur
            Console.WriteLine("(Enumerator) Yearly recurrence on: {0:d}", dt)
        Next

        nextDate = testDate

        Do
            nextDate = rRecur.NextInstance(nextDate, False)

            If nextDate <> DateTime.MinValue Then
                Console.WriteLine("(NextInstance) Yearly recurrence on: {0:d}", nextDate)
                nextDate = nextDate.AddMinutes(1)
            End If

        Loop While nextDate <> DateTime.MinValue

        ' Pause to review output
        Console.WriteLine("Press ENTER to continue")
        Console.ReadLine()

        ' Test yearly recurrence (option 3 - a variation of option 2)
        rRecur.RecurYearly(DayOccurrence.Last, DaysOfWeek.Weekdays, 7, 1)
        recurDates = rRecur.InstancesBetween(testDate, testDate.AddYears(5))

        Console.WriteLine(rRecur.ToStringWithStartDateTime())

        For Each dt In recurDates
            Console.WriteLine("(Collection) Yearly recurrence on: {0:d}", dt)
        Next

        For Each dt In rRecur
            Console.WriteLine("(Enumerator) Yearly recurrence on: {0:d}", dt)
        Next

        nextDate = testDate

        Do
            nextDate = rRecur.NextInstance(nextDate, False)

            If nextDate <> DateTime.MinValue Then
                Console.WriteLine("(NextInstance) Yearly recurrence on: {0:d}", nextDate)
                nextDate = nextDate.AddMinutes(1)
            End If

        Loop While nextDate <> DateTime.MinValue

        ' Pause to review output
        Console.WriteLine("Press ENTER to continue")
        Console.ReadLine()
    End Sub

End Module
