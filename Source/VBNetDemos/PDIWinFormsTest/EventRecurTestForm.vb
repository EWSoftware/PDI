'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : EventRecurTestForm.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/02/2015
' Note    : Copyright 2004-2015, Eric Woodruff, All rights reserved
' Compiler: Visual Basic .NET
'
' This is a simple demonstration used to test the event recurrence generation features of the calendar classes
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

Imports System.Globalization

Imports EWSoftware.PDI
Imports EWSoftware.PDI.Parser
Imports EWSoftware.PDI.Objects

''' <summary>
''' This form is used to test recurrence generation within an iCalendar component
''' </summary>
Public Partial Class EventRecurTestForm
    Inherits System.Windows.Forms.Form

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New()
        MyBase.New()

        InitializeComponent()

        lblCount.Text = String.Format(lblCount.Text, DateTime.Today.AddMonths(1))

        ' Set the short date/long time pattern based on the current culture
        dtpStartDate.CustomFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern & " " &
            CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern
        dtpEndDate.CustomFormat = dtpStartDate.CustomFormat
    End Sub

    ''' <summary>
    ''' Test the recurrence within the specified iCalendar component
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnTest_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles btnTest.Click
        Dim ro As RecurringObject = Nothing
        Dim instances As DateTimeInstanceCollection
        Dim calendar As String
        Dim start As Integer
        Dim elapsed As Double

        Try
            lblCount.Text = String.Empty

            ' Wrap it in VCALENDAR tags and parse it
            calendar = String.Format("BEGIN:VCALENDAR{0}VERSION:2.0{0}{1}{0}END:VCALENDAR", Environment.NewLine,
                txtCalendar.Text)

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

            ' Apply the time zone to the calendar.  If "None" is selected, the time zone will be cleared on all
            ' items.
            If cboTimeZone.SelectedIndex < 1 Then
                cal.ApplyTimeZone(Nothing)
            Else
                cal.ApplyTimeZone(VCalendar.TimeZones(cboTimeZone.SelectedIndex - 1))
            End If

            txtCalendar.Text = ro.ToString()

            lbDates.Items.Clear()
            Me.Cursor = Cursors.WaitCursor

            start = System.Environment.TickCount
            instances = ro.InstancesBetween(dtpStartDate.Value, dtpEndDate.Value, chkInLocalTime.Checked)
            elapsed = (System.Environment.TickCount - start) / 1000.0

            cal.Dispose()

            ' The date instance contains the start and end date/times, the duration, and time zone information.
            ' The duration is based on the duration of the calendar component.  The time zone information is
            ' based on the "In Local Time" parameter of the InstancesBetween() method and whether or not the
            ' component has a Time Zone ID specified.
            For Each dti As DateTimeInstance In instances
                lbDates.Items.Add(String.Format("{0:d} {0:hh:mm:ss tt} {1} to {2:d} {2:hh:mm:ss tt} {3} ({4})",
                    dti.StartDateTime, dti.AbbreviatedStartTimeZoneName, dti.EndDateTime,
                    dti.AbbreviatedEndTimeZoneName, dti.Duration.ToDescription()))

                If lbDates.Items.Count > 5000 Then
                    lblCount.Text &= "A large number of instances were returned.  Only the first 5000 have " &
                        "been loaded into the list box." & Environment.NewLine
                    Exit For
                End If
            Next

            ' If nothing was found remind the user that they may need to adjust the start and end date range to
            ' find stuff within the item.
            If instances.Count = 0 Then
                MessageBox.Show("Nothing found.  If this was unexpected, check the limiting date range in the " &
                    "two date/time text boxes at the top of the form and the calendar item date/time properties " &
                    "to make sure that they do overlap")
            End If

            lblCount.Text &= String.Format("Found {0:N0} instances in {1:N2} seconds ({2:N2} instances/second)",
                instances.Count, elapsed, instances.Count / elapsed)

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' This loads the time zone information from the registry when the form is loaded and sets the form defaults
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub EventRecurTestForm_Load(ByVal sender As Object, _
      ByVal e As System.EventArgs) Handles MyBase.Load
        Dim vtz As VTimeZone

        TimeZoneRegInfo.LoadTimeZoneInfo()

        ' Load the time zone combo box.  The first entry will be for no time zone.
        cboTimeZone.Items.Add("No time zone")

        For Each vtz In VCalendar.TimeZones
            cboTimeZone.Items.Add(vtz.TimeZoneId.Value)
        Next

        cboTimeZone.SelectedIndex = 0

        Dim dtDate As DateTime = New DateTime(DateTime.Today.Year, 1, 1)
        cboTimeZone.SelectedIndex = 0
        dtpStartDate.Value = dtDate
        dtpEndDate.Value = dtDate.AddMonths(3)

        txtCalendar.Text = String.Format(
            "BEGIN:VEVENT{0}" &
            "DTSTART:{1}{0}" &
            "DTEND:{2}{0}" &
            "RRULE:FREQ=DAILY;COUNT=10;INTERVAL=5{0}" &
            "END:VEVENT{0}", Environment.NewLine,
            dtDate.AddHours(9).ToUniversalTime().ToString(ISO8601Format.BasicDateTimeUniversal),
            dtDate.AddHours(10).ToUniversalTime().ToString(ISO8601Format.BasicDateTimeUniversal))
    End Sub

    ''' <summary>
    ''' Clear out the time zone collection so as not to affect any of the other test forms
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub EventRecurTestForm_Closing(ByVal sender As Object, _
      ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        ' Clear out the time zone collection so as not to affect any of the other test forms
        VCalendar.TimeZones.Clear()
    End Sub
End Class
