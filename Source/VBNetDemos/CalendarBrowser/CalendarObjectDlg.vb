'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : CalendarObjectDlg.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/05/2015
' Note    : Copyright 2004-2015, Eric Woodruff, All rights reserved
' Compiler: Microsoft VB.NET
'
' This is used to edit a calendar object's properties (VEvent, VToDo, or a VJournal component)
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 12/17/2004  EFW  Created the code
' 05/25/2007  EFW  Converted for use with .NET 2.0
'================================================================================================================

Imports System.Globalization

Imports EWSoftware.PDI
Imports EWSoftware.PDI.Objects
Imports EWSoftware.PDI.Properties

''' <summary>
''' This is used to edit a calendar object's properties (VEvent, VToDo, or a VJournal component)
''' </summary>
Public Partial Class CalendarObjectDlg
    Inherits System.Windows.Forms.Form

    Private timeZoneIdx As Integer      ' The currently selected time zone

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        ' Set the short date/long time pattern based on the current culture
        dtpStartDate.CustomFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern & " " &
            CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern
        dtpEndDate.CustomFormat = dtpStartDate.CustomFormat
        dtpCompleted.CustomFormat = dtpStartDate.CustomFormat

        ' Load the time zone combo box.  The first entry will be for no time zone
        cboTimeZone.Items.Add("No time zone")

        For Each tz As VTimeZone In VCalendar.TimeZones
            cboTimeZone.Items.Add(tz.TimeZoneId.Value)
        Next

        cboTimeZone.SelectedIndex = 0
    End Sub

    ''' <summary>
    ''' If set to "No time zone", the Apply button has no effect
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event parameters</param>
    Private Sub cboTimeZone_SelectedIndexChanged(sender As Object, _
      e As System.EventArgs) Handles cboTimeZone.SelectedIndexChanged
        btnApplyTZ.Enabled = (cboTimeZone.SelectedIndex <> 0)
    End Sub

    ''' <summary>
    ''' This is used to apply the time zone selection to all date/time values in the dialog box
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event parameters</param>
    Private Sub btnApplyTZ_Click(sender As Object, e As System.EventArgs) _
      Handles btnApplyTZ.Click
        Dim dti As DateTimeInstance
        Dim sourceTZ, destTZ As String

        If cboTimeZone.SelectedIndex = timeZoneIdx Then
            MessageBox.Show("The time zone has not changed", "No Change", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If MessageBox.Show(String.Format("Do you want to convert all times from the '{0}' time zone to the " &
          "'{1}' time zone?", cboTimeZone.Items(timeZoneIdx), cboTimeZone.Items(cboTimeZone.SelectedIndex)),
          "Change Time Zone", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Return
        End if

        ' Get the time zone IDs
        If timeZoneIdx = 0 Then
            sourceTZ = Nothing
        Else
            sourceTZ = CType(cboTimeZone.Items(timeZoneIdx), String)
        End If

        destTZ = CType(cboTimeZone.Items(cboTimeZone.SelectedIndex), String)

        ' Convert the times.  Note that the completed date is always in universal time so it isn't touched
        If sourceTZ Is Nothing Then
            If dtpStartDate.Checked = True Then
                dti = VCalendar.LocalTimeToTimeZoneTime(dtpStartDate.Value, destTZ)
                dtpStartDate.Value = dti.StartDateTime
            End If

            If dtpEndDate.Checked = True Then
                dti = VCalendar.LocalTimeToTimeZoneTime(dtpEndDate.Value, destTZ)
                dtpEndDate.Value = dti.StartDateTime
            End If
        Else
            If dtpStartDate.Checked = True Then
                dti = VCalendar.TimeZoneToTimeZone(dtpStartDate.Value, sourceTZ, destTZ)
                dtpStartDate.Value = dti.StartDateTime
            End If

            If dtpEndDate.Checked = True Then
                dti = VCalendar.TimeZoneToTimeZone(dtpEndDate.Value, sourceTZ, destTZ)
                dtpEndDate.Value = dti.StartDateTime
            End If
        End If

        ucRecurrences.ApplyTimeZone(sourceTZ, destTZ)
        ucExceptions.ApplyTimeZone(sourceTZ, destTZ)
        ucAlarms.ApplyTimeZone(sourceTZ, destTZ)

        timeZoneIdx = cboTimeZone.SelectedIndex
    End Sub

    ''' <summary>
    ''' View the geocoding coordinates using Google Maps if possible
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event parameters</param>
    Private Sub btnFind_Click(sender As Object, e As System.EventArgs) _
      Handles btnFind.Click
        Dim url As String = String.Format("https://www.google.com/maps/place/{0},{1}", txtLatitude.Text,
            txtLongitude.Text)

        Try
            System.Diagnostics.Process.Start(url)

        Catch ex As Exception
            MessageBox.Show("Unable to start web browser", "Launch Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)

            ' Log the exception to the debugger for the developer
            System.Diagnostics.Debug.Write(ex.ToString())
        End Try
    End Sub

    ''' <summary>
    ''' Validate the information on exit if OK was clicked
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event parameters</param>
    Private Sub CalendarObjectDlg_Closing(sender As Object, _
      e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim d As Duration
        Dim latitude, longitude As Double

        ' Ignore on cancel
        If Me.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
            Return
        End if

        epErrors.Clear()

        tabInfo.SelectedTab = pgGeneral

        If txtDuration.Visible = True AndAlso txtDuration.Text.Length <> 0 Then
            If Not Duration.TryParse(txtDuration.Text, d) Then
                e.Cancel = True
                epErrors.SetError(txtDuration, "Invalid duration value")
                txtDuration.Focus()
            Else
                If d <> Duration.Zero AndAlso dtpEndDate.Visible = True AndAlso dtpEndDate.Checked Then
                    e.Cancel = True
                    epErrors.SetError(dtpEndDate, "A duration or an end date can be specified, but not both")
                    dtpEndDate.Focus()
                End If
            End If
        End If

        If dtpEndDate.Visible = True AndAlso dtpStartDate.Checked AndAlso dtpEndDate.Checked AndAlso
          dtpStartDate.Value > dtpEndDate.Value Then
            epErrors.SetError(dtpStartDate, "Start date must be less than or equal to end date")
            e.Cancel = True
            dtpStartDate.Focus()
        End If

        If txtLatitude.Enabled Then
            If txtLatitude.Text.Length <> 0 Then
                If Not Double.TryParse(txtLatitude.Text, latitude) OrElse latitude < -90.0 OrElse latitude > 90.0 Then
                    e.Cancel = True
                    epErrors.SetError(txtLatitude, "Latitude must be a valid numeric value between -90 and 90")
                    tabInfo.SelectedTab = pgMisc
                    txtLatitude.Focus()
                End If
            End If

            If txtLongitude.Text.Length <> 0 Then
                If Not Double.TryParse(txtLongitude.Text, longitude) OrElse longitude < -180.0 OrElse longitude > 180.0 Then
                    e.Cancel = True
                    epErrors.SetError(txtLongitude, "Longitude must be a valid numeric value between -180 and 180")
                    tabInfo.SelectedTab = pgMisc
                    txtLongitude.Focus()
                End If
            End If
        End If

        If ucRequestStatus.Enabled AndAlso  Not ucRequestStatus.ValidateItems() Then
            tabInfo.SelectedTab = pgMisc
            ucRequestStatus.Focus()
            e.Cancel = True
        End If

        If ucAlarms.Enabled AndAlso Not ucAlarms.ValidateItems() Then
            tabInfo.SelectedTab = pgAlarms
            ucAlarms.Focus()
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' Initialize the dialog controls using the specified object
    ''' </summary>
    ''' <param name="oCal">The calendar object from which to get the settings</param>
    Public Sub SetValues(oCal As CalendarObject)
        Dim timeZoneId As String = Nothing
        Dim isICalendar As Boolean = (oCal.Version = SpecificationVersions.iCalendar20)

        ' Disable controls that aren't relevant to the vCalendar spec
        txtDuration.Enabled = isICalendar
        txtOrganizer.Enabled = isICalendar
        ucRequestStatus.Enabled = isICalendar
        txtLatitude.Enabled = isICalendar
        txtLongitude.Enabled = isICalendar
        btnFind.Enabled = isICalendar
        cboTimeZone.Enabled = isICalendar
        btnApplyTZ.Enabled = isICalendar

        If TypeOf oCal Is VEvent Then
            Dim e As VEvent = DirectCast(oCal, VEvent)
            Me.Text = "Event Properties"

            lblCompleted.Visible = False
            dtpCompleted.Visible = False
            lblPercent.Visible = False
            udcPercent.Visible = False
            lblEndDate.Text = "End"

            ' Header
            txtUniqueId.Text = e.UniqueId.Value
            chkTransparent.Checked = e.Transparency.IsTransparent
            txtCreated.Text = e.DateCreated.TimeZoneDateTime.ToString("G")
            txtLastModified.Text = e.LastModified.TimeZoneDateTime.ToString("G")
            txtClass.Text = e.Classification.Value
            udcSequence.Value = e.Sequence.SequenceNumber
            udcPriority.Value = e.Priority.PriorityValue

            timeZoneId = e.StartDateTime.TimeZoneId

            ' General
            If e.StartDateTime.TimeZoneDateTime = DateTime.MinValue Then
                dtpStartDate.Checked = False
            Else
                dtpStartDate.Value = e.StartDateTime.TimeZoneDateTime
                dtpStartDate.Checked = True
            End If

            If e.EndDateTime.TimeZoneDateTime = DateTime.MinValue Then
                dtpEndDate.Checked = False
            Else
                dtpEndDate.Value = e.EndDateTime.TimeZoneDateTime
                dtpEndDate.Checked = True
            End If

            If Not e.Duration.DurationValue.Equals(Duration.Zero) Then
                txtDuration.Text = e.Duration.DurationValue.ToString(Duration.MaxUnit.Weeks)
            End If

            txtSummary.Text = e.Summary.Value
            txtLocation.Text = e.Location.Value
            txtDescription.Text = e.Description.Value

            ' Load status values and set status
            cboStatus.Items.Add("None")
            cboStatus.Items.Add("Tentative")
            cboStatus.Items.Add("Confirmed")
            cboStatus.Items.Add("Cancelled")

            If cboStatus.Items.Contains(e.Status.StatusValue.ToString()) = False Then
                cboStatus.Items.Add(e.Status.StatusValue.ToString())
            End If

            cboStatus.SelectedIndex = cboStatus.Items.IndexOf(e.Status.StatusValue.ToString())

            ' We'll edit categories and resources as comma separated strings
            txtCategories.Text = e.Categories.CategoriesString
            txtResources.Text = e.Resources.ResourcesString
            txtOrganizer.Text = e.Organizer.Value

            ' We could bind directly to the existing collections but that would modify them.  To preserve the
            ' original items, we'll pass a copy of the collections instead.

            ' Attendees
            ucAttendees.BindingSource.DataSource = New AttendeePropertyCollection().CloneRange(e.Attendees)

            ' Recurrences and exceptions
            ucRecurrences.SetValues(e.RecurrenceRules, e.RecurDates)
            ucExceptions.SetValues(e.ExceptionRules, e.ExceptionDates)

            ' Set attachments
            ucAttachments.SetValues(e.Attachments)

            ' Set alarms
            ucAlarms.BindingSource.DataSource = New VAlarmCollection().CloneRange(e.Alarms)

            ' Miscellaneous
            txtLatitude.Text = e.GeographicPosition.Latitude.ToString()
            txtLongitude.Text = e.GeographicPosition.Longitude.ToString()

            ucRequestStatus.BindingSource.DataSource = New RequestStatusPropertyCollection().CloneRange(e.RequestStatuses)

            txtUrl.Text = e.Url.Value
            txtComments.Text = e.Comment.Value

        ElseIf TypeOf oCal Is VToDo Then
            Dim td As VToDo = DirectCast(oCal, VToDo)
            Me.Text = "To-Do Properties"

            chkTransparent.Visible = False
            lblLocation.Visible = False
            txtLocation.Visible = False

            lblCompleted.Visible = True
            dtpCompleted.Visible = True
            lblPercent.Visible = True
            udcPercent.Visible = True
            lblEndDate.Text = "Due"

            ' Header
            txtUniqueId.Text = td.UniqueId.Value
            txtCreated.Text = td.DateCreated.TimeZoneDateTime.ToString("G")
            txtLastModified.Text = td.LastModified.TimeZoneDateTime.ToString("G")
            txtClass.Text = td.Classification.Value
            udcSequence.Value = td.Sequence.SequenceNumber
            udcPriority.Value = td.Priority.PriorityValue

            timeZoneId = td.StartDateTime.TimeZoneId

            ' General
            If td.StartDateTime.TimeZoneDateTime = DateTime.MinValue Then
                dtpStartDate.Checked = False
            Else
                dtpStartDate.Value = td.StartDateTime.TimeZoneDateTime
                dtpStartDate.Checked = True
            End If

            ' We'll reuse End Date for Due Date
            If td.DueDateTime.TimeZoneDateTime = DateTime.MinValue Then
                dtpEndDate.Checked = False
            Else
                dtpEndDate.Value = td.DueDateTime.TimeZoneDateTime
                dtpEndDate.Checked = True
            End If

            If td.CompletedDateTime.TimeZoneDateTime = DateTime.MinValue Then
                dtpCompleted.Checked = False
            Else
                dtpCompleted.Value = td.CompletedDateTime.TimeZoneDateTime
                dtpCompleted.Checked = True
            End If

            If Not td.Duration.DurationValue.Equals(Duration.Zero) Then
                txtDuration.Text = td.Duration.DurationValue.ToString(Duration.MaxUnit.Weeks)
            End If

            udcPercent.Value = td.PercentComplete.Percentage
            txtSummary.Text = td.Summary.Value
            txtDescription.Text = td.Description.Value

            ' Load status values and set status
            cboStatus.Items.Add("None")
            cboStatus.Items.Add("NeedsAction")
            cboStatus.Items.Add("Completed")
            cboStatus.Items.Add("InProcess")
            cboStatus.Items.Add("Cancelled")

            If cboStatus.Items.Contains(td.Status.StatusValue.ToString()) = False Then
                cboStatus.Items.Add(td.Status.StatusValue.ToString())
            End If

            cboStatus.SelectedIndex = cboStatus.Items.IndexOf(td.Status.StatusValue.ToString())

            ' We'll edit categories and resources as comma separated strings
            txtCategories.Text = td.Categories.CategoriesString
            txtResources.Text = td.Resources.ResourcesString
            txtOrganizer.Text = td.Organizer.Value

            ' We could bind directly to the existing collections but that would modify them.  To preserve the
            ' original items, we'll pass a copy of the collections instead.

            ' Attendees
            ucAttendees.BindingSource.DataSource = New AttendeePropertyCollection().CloneRange(td.Attendees)

            ' Recurrences and exceptions
            ucRecurrences.SetValues(td.RecurrenceRules, td.RecurDates)
            ucExceptions.SetValues(td.ExceptionRules, td.ExceptionDates)

            ' Set attachments
            ucAttachments.SetValues(td.Attachments)

            ' Set alarms
            ucAlarms.BindingSource.DataSource = New VAlarmCollection().CloneRange(td.Alarms)

            ' Miscellaneous
            txtLatitude.Text = td.GeographicPosition.Latitude.ToString()
            txtLongitude.Text = td.GeographicPosition.Longitude.ToString()

            ucRequestStatus.BindingSource.DataSource = New RequestStatusPropertyCollection().CloneRange(td.RequestStatuses)

            txtUrl.Text = td.Url.Value
            txtComments.Text = td.Comment.Value

        ElseIf TypeOf oCal Is VJournal Then
            Dim j As VJournal = DirectCast(oCal, VJournal)
            Me.Text = "Journal Properties"

            chkTransparent.Visible = False
            lblPriority.Visible = False
            udcPriority.Visible = False
            lblEndDate.Visible = False
            dtpEndDate.Visible = False
            lblDuration.Visible = False
            txtDuration.Visible = False
            lblLocation.Visible = False
            txtLocation.Visible = False
            lblResources.Visible = False
            txtResources.Visible = False
            lblLatitude.Visible = False
            txtLatitude.Visible = False
            lblLongitude.Visible = False
            txtLongitude.Visible = False
            btnFind.Visible = False
            lblCompleted.Visible = False
            dtpCompleted.Visible = False
            lblPercent.Visible = False
            udcPercent.Visible = False

            tabInfo.TabPages.Remove(pgAlarms)
            tabInfo.SelectedTab = pgGeneral

            ' Header
            txtUniqueId.Text = j.UniqueId.Value
            txtCreated.Text = j.DateCreated.TimeZoneDateTime.ToString("G")
            txtLastModified.Text = j.LastModified.TimeZoneDateTime.ToString("G")
            txtClass.Text = j.Classification.Value
            udcSequence.Value = j.Sequence.SequenceNumber

            timeZoneId = j.StartDateTime.TimeZoneId

            ' General
            If j.StartDateTime.TimeZoneDateTime = DateTime.MinValue Then
                dtpStartDate.Checked = False
            Else
                dtpStartDate.Value = j.StartDateTime.TimeZoneDateTime
                dtpStartDate.Checked = True
            End If

            txtSummary.Text = j.Summary.Value
            txtDescription.Text = j.Description.Value

            ' Load status values and set status
            cboStatus.Items.Add("None")
            cboStatus.Items.Add("Draft")
            cboStatus.Items.Add("Final")
            cboStatus.Items.Add("Cancelled")

            If cboStatus.Items.Contains(j.Status.StatusValue.ToString()) = False Then
                cboStatus.Items.Add(j.Status.StatusValue.ToString())
            End If

            cboStatus.SelectedIndex = cboStatus.Items.IndexOf(j.Status.StatusValue.ToString())

            ' We'll edit categories as a comma separated string
            txtCategories.Text = j.Categories.CategoriesString
            txtOrganizer.Text = j.Organizer.Value

            ' We could bind directly to the existing collections but that would modify them.  To preserve the
            ' original items, we'll pass a copy of the collections instead.

            ' Attendees
            ucAttendees.BindingSource.DataSource = New AttendeePropertyCollection().CloneRange(j.Attendees)

            ' Recurrences and exceptions
            ucRecurrences.SetValues(j.RecurrenceRules, j.RecurDates)
            ucExceptions.SetValues(j.ExceptionRules, j.ExceptionDates)

            ' Set attachments
            ucAttachments.SetValues(j.Attachments)

            ' Miscellaneous
            ucRequestStatus.BindingSource.DataSource = New RequestStatusPropertyCollection().CloneRange(j.RequestStatuses)

            txtUrl.Text = j.Url.Value
            txtComments.Text = j.Comment.Value
        End If

        ' We use the start date's time zone ID for the combo box.  It should represent the time zone used
        ' throughout the component.
        If timeZoneId Is Nothing Then
            cboTimeZone.SelectedIndex = 0
            timeZoneIdx = 0
        Else
            timeZoneIdx = cboTimeZone.Items.IndexOf(timeZoneId)

            If timeZoneIdx <> -1 Then
                cboTimeZone.SelectedIndex = timeZoneIdx
            Else
                cboTimeZone.SelectedIndex = 0
                timeZoneIdx = 0
            End If
        End If
    End Sub

    ''' <summary>
    ''' Update the calendar object with the dialog control values
    ''' </summary>
    ''' <param name="oCal">The calendar object in which the settings are updated</param>
    Public Sub GetValues(oCal As CalendarObject)
        ' We'll use the TimeZoneDateTime property on all date/time values so that they are set literally rather
        ' than being converted to the time zone as would happen with the DateTimeValue property.
        If TypeOf oCal Is VEvent Then
            Dim e As VEvent = DirectCast(oCal, VEvent)

            ' Header.  Unique ID and Created Date are not changed
            e.Transparency.IsTransparent = chkTransparent.Checked
            e.LastModified.TimeZoneDateTime = DateTime.Now
            e.Classification.Value = txtClass.Text
            e.Sequence.SequenceNumber = CType(udcSequence.Value, Integer)
            e.Priority.PriorityValue = CType(udcPriority.Value, Integer)

            ' General
            If dtpStartDate.Checked = False Then
                e.StartDateTime.TimeZoneDateTime = DateTime.MinValue
            Else
                e.StartDateTime.TimeZoneDateTime = dtpStartDate.Value
                e.StartDateTime.ValueLocation = ValLocValue.DateTime
            End if

            If dtpEndDate.Checked = False Then
                e.EndDateTime.TimeZoneDateTime = DateTime.MinValue
            Else
                e.EndDateTime.TimeZoneDateTime = dtpEndDate.Value
                e.EndDateTime.ValueLocation = ValLocValue.DateTime
            End if

            e.Duration.DurationValue = New Duration(txtDuration.Text)
            e.Summary.Value = txtSummary.Text
            e.Location.Value = txtLocation.Text
            e.Description.Value = txtDescription.Text

            ' Get status value
            e.Status.StatusValue = DirectCast([Enum].Parse(GetType(StatusValue),
                cboStatus.Items(cboStatus.SelectedIndex).ToString(), True), StatusValue)

            ' We'll edit categories and resources as comma separated strings
            e.Categories.CategoriesString = txtCategories.Text
            e.Resources.ResourcesString = txtResources.Text
            e.Organizer.Value = txtOrganizer.Text

            ' For the collections, we'll clear the existing items and copy the modified items from the browse
            ' control binding sources.

            ' Attendees
            e.Attendees.Clear()
            e.Attendees.CloneRange(DirectCast(ucAttendees.BindingSource.DataSource, AttendeePropertyCollection))

            ' Recurrences and exceptions.
            ucRecurrences.GetValues(e.RecurrenceRules, e.RecurDates)
            ucExceptions.GetValues(e.ExceptionRules, e.ExceptionDates)

            ' Get attachments
            ucAttachments.GetValues(e.Attachments)

            ' Get alarms
            e.Alarms.Clear()
            e.Alarms.CloneRange(DirectCast(ucAlarms.BindingSource.DataSource, VAlarmCollection))

            ' Miscellaneous
            If txtLatitude.Text.Length <> 0 Or txtLongitude.Text.Length <> 0 Then
                e.GeographicPosition.Latitude = Convert.ToDouble(txtLatitude.Text, CultureInfo.CurrentCulture)
                e.GeographicPosition.Longitude = Convert.ToDouble(txtLongitude.Text, CultureInfo.CurrentCulture)
            Else
                e.GeographicPosition.Latitude = 0.0
                e.GeographicPosition.Longitude = 0.0
            End If

            e.RequestStatuses.Clear()
            e.RequestStatuses.CloneRange(DirectCast(ucRequestStatus.BindingSource.DataSource, RequestStatusPropertyCollection))

            e.Url.Value = txtUrl.Text
            e.Comment.Value = txtComments.Text

        ElseIf TypeOf oCal Is VToDo Then
            Dim td As VToDo = DirectCast(oCal, VToDo)

            ' Header.  Unique ID and Created Date are not changed
            td.LastModified.TimeZoneDateTime = DateTime.Now
            td.Classification.Value = txtClass.Text
            td.Sequence.SequenceNumber = CType(udcSequence.Value, Integer)
            td.Priority.PriorityValue = CType(udcPriority.Value, Integer)

            ' General
            If dtpStartDate.Checked = False Then
                td.StartDateTime.TimeZoneDateTime = DateTime.MinValue
            Else
                td.StartDateTime.TimeZoneDateTime = dtpStartDate.Value
                td.StartDateTime.ValueLocation = ValLocValue.DateTime
            End if

            If dtpEndDate.Checked = False Then
                td.DueDateTime.TimeZoneDateTime = DateTime.MinValue
            Else
                td.DueDateTime.TimeZoneDateTime = dtpEndDate.Value
                td.DueDateTime.ValueLocation = ValLocValue.DateTime
            End if

            If dtpCompleted.Checked = False Then
                td.CompletedDateTime.TimeZoneDateTime = DateTime.MinValue
            Else
                td.CompletedDateTime.TimeZoneDateTime = dtpCompleted.Value
                td.CompletedDateTime.ValueLocation = ValLocValue.DateTime
            End If

            td.PercentComplete.Percentage = CType(udcPercent.Value, Integer)
            td.Duration.DurationValue = New Duration(txtDuration.Text)
            td.Summary.Value = txtSummary.Text
            td.Description.Value = txtDescription.Text

            ' Get status value
            td.Status.StatusValue = DirectCast([Enum].Parse(GetType(StatusValue),
                cboStatus.Items(cboStatus.SelectedIndex).ToString(), True), StatusValue)

            ' We'll edit categories and resources as comma separated strings
            td.Categories.CategoriesString = txtCategories.Text
            td.Resources.ResourcesString = txtResources.Text

            td.Organizer.Value = txtOrganizer.Text

            ' For the collections, we'll clear the existing items and copy the modified items from the browse
            ' control binding sources.

            ' Attendees
            td.Attendees.Clear()
            td.Attendees.CloneRange(DirectCast(ucAttendees.BindingSource.DataSource, AttendeePropertyCollection))

            ' Recurrences and exceptions.
            ucRecurrences.GetValues(td.RecurrenceRules, td.RecurDates)
            ucExceptions.GetValues(td.ExceptionRules, td.ExceptionDates)

            ' Get attachments
            ucAttachments.GetValues(td.Attachments)

            ' Get alarms
            td.Alarms.Clear()
            td.Alarms.CloneRange(DirectCast(ucAlarms.BindingSource.DataSource, VAlarmCollection))

            ' Miscellaneous
            If txtLatitude.Text.Length <> 0 Or txtLongitude.Text.Length <> 0 Then
                td.GeographicPosition.Latitude = Convert.ToDouble(txtLatitude.Text, CultureInfo.CurrentCulture)
                td.GeographicPosition.Longitude = Convert.ToDouble(txtLongitude.Text, CultureInfo.CurrentCulture)
            Else
                td.GeographicPosition.Latitude = 0.0
                td.GeographicPosition.Longitude = 0.0
            End If

            td.RequestStatuses.Clear()
            td.RequestStatuses.CloneRange(DirectCast(ucRequestStatus.BindingSource.DataSource, RequestStatusPropertyCollection))

            td.Url.Value = txtUrl.Text
            td.Comment.Value = txtComments.Text

        ElseIf TypeOf oCal Is VJournal Then
            Dim j As VJournal = DirectCast(oCal, VJournal)

            ' Header.  Unique ID and Created Date are not changed
            j.LastModified.TimeZoneDateTime = DateTime.Now
            j.Classification.Value = txtClass.Text
            j.Sequence.SequenceNumber = CType(udcSequence.Value, Integer)

            ' General
            If dtpStartDate.Checked = False Then
                j.StartDateTime.TimeZoneDateTime = DateTime.MinValue
            Else
                j.StartDateTime.TimeZoneDateTime = dtpStartDate.Value
                j.StartDateTime.ValueLocation = ValLocValue.DateTime
            End If

            j.Summary.Value = txtSummary.Text
            j.Description.Value = txtDescription.Text

            ' Get status value
            j.Status.StatusValue = DirectCast([Enum].Parse(GetType(StatusValue),
                cboStatus.Items(cboStatus.SelectedIndex).ToString(), True), StatusValue)

            ' We'll edit categories as a comma separated string
            j.Categories.CategoriesString = txtCategories.Text

            j.Organizer.Value = txtOrganizer.Text

            ' For the collections, we'll clear the existing items and copy the modified items from the browse
            ' control binding sources.

            ' Attendees
            j.Attendees.Clear()
            j.Attendees.CloneRange(DirectCast(ucAttendees.BindingSource.DataSource, AttendeePropertyCollection))

            ' Recurrences and exceptions.
            ucRecurrences.GetValues(j.RecurrenceRules, j.RecurDates)
            ucExceptions.GetValues(j.ExceptionRules, j.ExceptionDates)

            ' Get attachments
            ucAttachments.GetValues(j.Attachments)

            ' Miscellaneous
            j.RequestStatuses.Clear()
            j.RequestStatuses.CloneRange(DirectCast(ucRequestStatus.BindingSource.DataSource, RequestStatusPropertyCollection))

            j.Url.Value = txtUrl.Text
            j.Comment.Value = txtComments.Text
        End If

        ' Set the time zone in the object after getting all the data.  The "Set" method will not modify the
        ' date/times like the "Apply" method does.
        If cboTimeZone.Enabled = True And cboTimeZone.SelectedIndex <> 0 Then
            oCal.SetTimeZone(VCalendar.TimeZones(cboTimeZone.SelectedIndex - 1))
        Else
            oCal.SetTimeZone(Nothing)
        End If
    End Sub

End Class
