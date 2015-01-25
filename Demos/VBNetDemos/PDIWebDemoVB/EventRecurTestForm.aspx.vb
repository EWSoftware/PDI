'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : EventRecurTestForm.aspx.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 12/31/2014
' Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
' Compiler: Microsoft VB.NET
'
' This page is used to demonstrate recurrence with the calendar components
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 01/25/2005  EFW  Created the code
'================================================================================================================

Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Globalization
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Imports EWSoftware.PDI
Imports EWSoftware.PDI.Parser
Imports EWSoftware.PDI.Objects
Imports EWSoftware.PDI.Properties

Namespace PDIWebDemoVB

    Partial Class EventRecurTestForm
        Inherits System.Web.UI.Page

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim tz As VTimeZone

            Me.Page.Title = "iCalendar Component Recurrence Demo"

            If Not Page.IsPostBack Then
                ' The time zone information is loaded in the Application_Start event in Global.asax.  We'll
                ' acquire a reader lock on the time zone collection as it's possible other sessions could be
                ' parsing calendars with time zone data that could change the collection.
                VCalendar.TimeZones.Lock.AcquireReaderLock(250)

                Try
                    cboTimeZone.Items.Add("No time zone")

                    For Each tz In VCalendar.TimeZones
                        cboTimeZone.Items.Add(tz.TimeZoneId.Value)
                    Next

                Finally
                    VCalendar.TimeZones.Lock.ReleaseReaderLock()
                End Try

                ' Set up some defaults for testing
                cboTimeZone.SelectedIndex = 0

                Dim dtDate As DateTime = New DateTime(DateTime.Today.Year, 1, 1)
                txtStartDate.Text = dtDate.ToString("G")
                txtEndDate.Text = dtDate.AddMonths(3).ToString("G")

                txtCalendar.Text = String.Format(
                    "BEGIN:VEVENT{2}" &
                    "DTSTART:{0}{2}" &
                    "DTEND:{1}{2}" &
                    "RRULE:FREQ=DAILY;COUNT=10;INTERVAL=5{2}" &
                    "END:VEVENT{2}",
                    dtDate.AddHours(9).ToUniversalTime().ToString(ISO8601Format.BasicDateTimeUniversal),
                    dtDate.AddHours(10).ToUniversalTime().ToString(ISO8601Format.BasicDateTimeUniversal),
                    Environment.NewLine)
            End If
        End Sub

        ' Use some limitations to prevent overloading the server or timing out the page if possible
        Private Sub ApplyLimits(ro As RecurringObject, r As Recurrence)
            If r.Frequency > RecurFrequency.Hourly Then
                r.MaximumOccurrences = 5000
            End If

            If r.MaximumOccurrences <> 0 Then
                If r.MaximumOccurrences > 5000 Then
                    r.MaximumOccurrences = 5000
                End If
            Else
                If r.Frequency = RecurFrequency.Hourly Then
                    If r.RecurUntil > ro.StartDateTime.DateTimeValue.AddYears(5) Then
                        r.RecurUntil = ro.StartDateTime.DateTimeValue.AddYears(5)
                    End If
                Else
                    If r.RecurUntil > ro.StartDateTime.DateTimeValue.AddYears(50) Then
                        r.RecurUntil = ro.StartDateTime.DateTimeValue.AddYears(50)
                    End If
                End If
            End If
        End Sub

        ' Generate instances for the specified component
        Private Sub btnTest_Click(ByVal sender As Object, _
          ByVal e As System.EventArgs) Handles btnTest.Click
            Dim ro As RecurringObject = Nothing
            Dim instances As DateTimeInstanceCollection
            Dim calendar As String, start As Integer, elapsed As Double
            Dim startDate, endDate As DateTime
            Dim rrule As RRuleProperty, exrule As ExRuleProperty

            Try
                lblCount.Text = String.Empty

                If Not DateTime.TryParse(txtStartDate.Text, CultureInfo.CurrentCulture, DateTimeStyles.None,
                  startDate) OrElse Not DateTime.TryParse(txtEndDate.Text, CultureInfo.CurrentCulture,
                  DateTimeStyles.None, endDate) Then
                    lblCount.Text = "Invalid start or end date/time format"
                    Return
                End If

                ' Wrap it in VCALENDAR tags and parse it
                calendar = String.Format("BEGIN:VCALENDAR{1}VERSION:2.0{1}{0}{1}END:VCALENDAR", txtCalendar.Text,
                    Environment.NewLine)

                Dim cal As VCalendar = VCalendarParser.ParseFromString(calendar)

                ' Get the first event, to-do, or journal item
                If cal.Events.Count > 0 Then
                    ro = cal.Events(0)
                Else
                    If cal.ToDos.Count > 0 Then
                        ro = cal.ToDos(0)
                    Else
                        If cal.Journals.Count > 0 Then
                            ro = cal.Journals(0)
                        End If
                    End If
                End If

                If ro Is Nothing Then
                    lblCount.Text = "No event, to-do, or journal item found"
                    Return
                End If

                ' Apply the time zone to the calendar.  If "None" is selected, the time zone will be cleared on
                ' all items.
                If cboTimeZone.SelectedIndex < 1 Then
                    cal.ApplyTimeZone(Nothing)
                Else
                    cal.ApplyTimeZone(VCalendar.TimeZones(cboTimeZone.SelectedIndex - 1))
                End If

                For Each rrule In ro.RecurrenceRules
                    ApplyLimits(ro, rrule.Recurrence)
                Next

                For Each exrule In ro.ExceptionRules
                    ApplyLimits(ro, exrule.Recurrence)
                Next

                txtCalendar.Text = ro.ToString()

                start = System.Environment.TickCount
                instances = ro.InstancesBetween(startDate, endDate, chkInLocalTime.Checked)
                elapsed = (System.Environment.TickCount - start) / 1000.0

                cal.Dispose()

                ' The date instance contains the start and end date/times, the duration, and time zone
                ' information.  The duration is based on the duration of the calendar component.  The time zone
                ' information is based on the "In Local Time" parameter of the InstancesBetween() method and
                ' whether or not the component has a Time Zone ID specified.
                dlDates.DataSource = instances
                dlDates.DataBind()

                ' If nothing was found remind the user that they may need to adjust the start and end date range
                ' to find stuff within the item.
                If instances.Count = 0 Then
                    lblCount.Text = "Nothing found.  If this was unexpected, check the limiting date range in " &
                        "the two date/time text boxes at the top of the form and the calendar item date/time " &
                        "properties to make sure that they do overlap<br/><br/>"
                End If

                lblCount.Text &= String.Format("Found {0:N0} instances in {1:N2} seconds ({2:N2} instances/second)",
                    instances.Count, elapsed, instances.Count / elapsed)

            Catch ex As Exception
                lblCount.Text = ex.Message
            End Try
        End Sub

    End Class

End Namespace
