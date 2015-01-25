'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : TimeZoneRegInfo.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/02/2015
' Note    : Copyright 2004-2015, Eric Woodruff, All rights reserved
' Compiler: VB.NET
'
' This class is used to create a time zone information from the time zone data found in the registry on Windows
' PCs.
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 01/05/2005  EFW  Factored out the code to share between applications
'================================================================================================================

Imports System.Runtime.InteropServices

Imports Microsoft.Win32

Imports EWSoftware.PDI
Imports EWSoftware.PDI.Objects
Imports EWSoftware.PDI.Properties

Public NotInheritable Class TimeZoneRegInfo

    ' This is used to represent a system date/time in the time zone registry structure
    <StructLayout(LayoutKind.Sequential, Pack:=2)> _
    Private Structure SYSTEMTIME
        Public wYear As Int16
        Public wMonth As Int16
        Public wDayOfWeek As Int16
        Public wDay As Int16
        Public wHour As Int16
        Public wMinute As Int16
        Public wSecond As Int16
        Public wMilliseconds As Int16

        Public Function ToDateTime() As DateTime
            Return New DateTime(wYear, wMonth, wDay, wHour, wMinute, wSecond, wMilliseconds)
        End Function

        Public Function ToDateTime(nSpecificYear As Integer) As DateTime
            Dim docc As DayOccurrence

            If wDay > 4 Then
                docc = DayOccurrence.Last
            Else
                docc = CType(wDay, DayOccurrence)
            End If

            Return DateUtils.CalculateFloatingDate(nSpecificYear, wMonth, docc, CType(wDayOfWeek, DayOfWeek), 0).Add(
                New TimeSpan(0, wHour, wMinute, wSecond, wMilliseconds))
        End Function

        Public Function DayOfWeek() As DaysOfWeek
            Select Case wDayOfWeek
                Case 0
                    Return DaysOfWeek.Sunday

                Case 1
                    Return DaysOfWeek.Monday

                Case 2
                    Return DaysOfWeek.Tuesday

                Case 3
                    Return DaysOfWeek.Wednesday

                Case 4
                    Return DaysOfWeek.Thursday

                Case 5
                    Return DaysOfWeek.Friday

            End Select

            Return DaysOfWeek.Saturday
        End Function
    End Structure

    ' This represents time zone information for area.  The information is retrieved from the registry.
    <StructLayout(LayoutKind.Sequential, Pack:=2)> _
    Private Structure TIMEZONE
        Public nBias As Int32
        public nStandardBias As Int32
        public nDaylightBias As Int32
        public standardDate As SYSTEMTIME
        public daylightDate As SYSTEMTIME

        Public Shared Function FromRegistry(oTZI As Object) As TIMEZONE
            Dim byTZI As Byte() = CType(oTZI, Byte())
            Dim size As Integer = byTZI.Length
            Dim buffer As IntPtr = IntPtr.Zero
            Dim tzi As TIMEZONE

            Try
                buffer = Marshal.AllocHGlobal(size)

                Marshal.Copy(byTZI, 0, buffer, size)
                tzi = DirectCast(Marshal.PtrToStructure(buffer, GetType(TIMEZONE)), TIMEZONE)

            Finally
                If buffer <> IntPtr.Zero Then
                    Marshal.FreeHGlobal(buffer)
                End If
            End Try

            Return tzi
        End Function
    End Structure

    '==========================================================================

    Private Sub New()

    End Sub

    ' This is used to load the VCalendar.TimeZones collection with time zone information from the registry
    Public Shared Sub LoadTimeZoneInfo()
        Dim keyName, display, standardDesc, dstDesc As String
        Dim tz As TIMEZONE
        Dim docc As DayOccurrence

        VCalendar.TimeZones.Clear()

        ' To keep things simple, we'll load the time zone data from the setting available in the registry.  We
        ' could take it a step further and load it from something like a copy of the Olson time zone database but
        ' I haven't been that ambitious yet.
        If Environment.OSVersion.Platform = PlatformID.Win32NT Then
            keyName = "SOFTWARE\Microsoft\Windows NT\CurrentVersion\Time Zones"
        Else
            keyName = "SOFTWARE\Microsoft\Windows\CurrentVersion\Time Zones"
        End If

        Using rk As RegistryKey = Registry.LocalMachine.OpenSubKey(keyName)
            For Each s As String In rk.GetSubKeyNames()

                Using rsk As RegistryKey = rk.OpenSubKey(s)
                    display = DirectCast(rsk.GetValue("Display"), String)
                    standardDesc = DirectCast(rsk.GetValue("Std"), String)
                    dstDesc = DirectCast(rsk.GetValue("Dlt"), String)
                    tz = TIMEZONE.FromRegistry(rsk.GetValue("TZI"))
                End Using

                ' Create the time zone object
                Dim vtz As New VTimeZone()
                vtz.TimeZoneId.Value = display

                Dim obr As ObservanceRule = vtz.ObservanceRules.Add(ObservanceRuleType.Standard)

                obr.OffsetFrom.TimeSpanValue = TimeSpan.FromMinutes(tz.nBias + tz.nDaylightBias).Negate()
                obr.OffsetTo.TimeSpanValue = TimeSpan.FromMinutes(tz.nBias + tz.nStandardBias).Negate()
                obr.TimeZoneNames.Add(standardDesc)

                ' If the standard date month is zero, it doesn't use standard time.  Assume 01/01/1970 and set it
                ' up to return the offset.
                If tz.standardDate.wMonth = 0 Then
                    obr.StartDateTime.DateTimeValue = New DateTime(1970, 1, 1)
                Else
                    ' If year is zero, its a recurrence.  If not zero, it's a fixed date.
                    If tz.standardDate.wYear = 0 Then
                        obr.StartDateTime.DateTimeValue = tz.standardDate.ToDateTime(1970)

                        Dim rrule As RRuleProperty = New RRuleProperty()

                        If tz.standardDate.wDay > 4 Then
                            docc = DayOccurrence.Last
                        Else
                            docc = CType(tz.standardDate.wDay, DayOccurrence)
                        End If

                        rrule.Recurrence.RecurYearly(docc, tz.standardDate.DayOfWeek(), tz.standardDate.wMonth, 1)

                        obr.RecurrenceRules.Add(rrule)
                    Else
                        obr.StartDateTime.DateTimeValue = tz.standardDate.ToDateTime()
                        obr.RecurDates.Add(obr.StartDateTime.DateTimeValue)
                    End If
                End If

                ' If the daylight month is zero, it doesn't use DST.  The standard rule will handle everything.
                If tz.daylightDate.wMonth <> 0 Then
                    obr = vtz.ObservanceRules.Add(ObservanceRuleType.Daylight)

                    obr.OffsetFrom.TimeSpanValue = TimeSpan.FromMinutes(tz.nBias + tz.nStandardBias).Negate()
                    obr.OffsetTo.TimeSpanValue = TimeSpan.FromMinutes(tz.nBias + tz.nDaylightBias).Negate()
                    obr.TimeZoneNames.Add(dstDesc)

                    ' If year is zero, its a recurrence.  If not zero, it's a fixed date.
                    If tz.daylightDate.wYear = 0 Then
                        obr.StartDateTime.DateTimeValue = tz.daylightDate.ToDateTime(1970)

                        Dim rrule As RRuleProperty = New RRuleProperty()

                        If tz.daylightDate.wDay > 4 Then
                            docc = DayOccurrence.Last
                        Else
                            docc = CType(tz.daylightDate.wDay, DayOccurrence)
                        End If

                        rrule.Recurrence.RecurYearly(docc, tz.daylightDate.DayOfWeek(), tz.daylightDate.wMonth, 1)

                        obr.RecurrenceRules.Add(rrule)
                    Else
                        obr.StartDateTime.DateTimeValue = tz.daylightDate.ToDateTime()
                        obr.RecurDates.Add(obr.StartDateTime.DateTimeValue)
                    End If
                End If

                VCalendar.TimeZones.Add(vtz)
            Next

        End Using

        ' Put the time zones in sorted order
        VCalendar.TimeZones.Sort(true)
    End Sub

End Class
