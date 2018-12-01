'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : VFreeBusyDlg.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/05/2015
' Note    : Copyright 2004-2015, Eric Woodruff, All rights reserved
' Compiler: Microsoft VB.NET
'
' This is used to edit a VFreeBusy object's properties
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 12/14/2004  EFW  Created the code
' 05/25/2007  EFW  Converted for use with .NET 2.0
'================================================================================================================

Imports System.Globalization

Imports EWSoftware.PDI
Imports EWSoftware.PDI.Objects
Imports EWSoftware.PDI.Properties

''' <summary>
''' This is used to edit a VFreeBusy object's properties
''' </summary>
Public Partial Class VFreeBusyDlg
    Inherits System.Windows.Forms.Form

    Private timeZoneIdx As Integer      ' The currently selected time zone

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        ' Set the short date/long time pattern based on the current culture
        dtpStartDate.CustomFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern & " " &
            CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern
        dtpEndDate.CustomFormat = dtpStartDate.CustomFormat

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
            MessageBox.Show("The time zone has not changed", "No Change", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)
            Return
        End If

        If MessageBox.Show(String.Format("Do you want to convert all times from the '{0}' time zone to the " &
          "'{1}' time zone?", cboTimeZone.Items(timeZoneIdx), cboTimeZone.Items(cboTimeZone.SelectedIndex)),
          "Change Time Zone", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Return
        End If

        ' Get the time zone IDs
        If timeZoneIdx = 0 Then
            sourceTZ = Nothing
        Else
            sourceTZ = CType(cboTimeZone.Items(timeZoneIdx), String)
        End if

        destTZ = CType(cboTimeZone.Items(cboTimeZone.SelectedIndex), String)

        ' Convert the times
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

        ucFreeBusy.ApplyTimeZone(sourceTZ, destTZ)

        timeZoneIdx = cboTimeZone.SelectedIndex
    End Sub

    ' Validate the information on exit if OK was clicked.
    Private Sub VFreeBusyDlg_Closing(sender As Object, _
      e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim d As Duration

        ' Ignore on cancel
        If Me.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
            Return
        End If

        epErrors.Clear()

        If txtDuration.Text.Length <> 0 AndAlso Not Duration.TryParse(txtDuration.Text, d) Then
            epErrors.SetError(txtDuration, "Invalid duration value")
            e.Cancel = True
            tabInfo.SelectedTab = pgGeneral
            txtDuration.Focus()
        End If

        If dtpStartDate.Checked AndAlso dtpEndDate.Checked AndAlso dtpStartDate.Value > dtpEndDate.Value Then
            epErrors.SetError(dtpStartDate, "Start date must be less than or equal to end date")
            e.Cancel = True
            tabInfo.SelectedTab = pgGeneral
            dtpStartDate.Focus()
        End If

        If Not ucRequestStatus.ValidateItems() Then
            tabInfo.SelectedTab = pgReqStats
            ucRequestStatus.Focus()
            e.Cancel = True
        Else
            If Not ucFreeBusy.ValidateItems() Then
                tabInfo.SelectedTab = pgFreeBusy
                ucFreeBusy.Focus()
                e.Cancel = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Initialize the dialog controls using the specified VFreeBusy object
    ''' </summary>
    ''' <param name="fb">The free/busy object from which to get the settings</param>
    Public Sub SetValues(fb As VFreeBusy)
        Dim timeZoneId As String = fb.StartDateTime.TimeZoneId

        txtUniqueId.Text = fb.UniqueId.Value
        txtOrganizer.Text = fb.Organizer.Value
        txtContact.Text = fb.Contact.Value

        If fb.StartDateTime.TimeZoneDateTime = DateTime.MinValue Then
            dtpStartDate.Checked = False
        Else
            dtpStartDate.Value = fb.StartDateTime.TimeZoneDateTime
            dtpStartDate.Checked = True
        End If

        If fb.EndDateTime.TimeZoneDateTime = DateTime.MinValue Then
            dtpEndDate.Checked = False
        Else
            dtpEndDate.Value = fb.EndDateTime.TimeZoneDateTime
            dtpEndDate.Checked = True
        End If

        If fb.Duration.DurationValue <> Duration.Zero Then
            txtDuration.Text = fb.Duration.DurationValue.ToString(Duration.MaxUnit.Weeks)
        End If

        txtUrl.Text = fb.Url.Value
        txtComments.Text = fb.Comment.Value

        ' We could bind directly to the existing collections but that would modify them.  To preserve the
        ' original items, we'll pass a copy of the collections instead.
        ucAttendees.BindingSource.DataSource = New AttendeePropertyCollection().CloneRange(fb.Attendees)
        ucFreeBusy.BindingSource.DataSource = New FreeBusyPropertyCollection().CloneRange(fb.FreeBusy)
        ucRequestStatus.BindingSource.DataSource = New RequestStatusPropertyCollection().CloneRange(fb.RequestStatuses)

        ' We use the start date's time zone ID for the combo box.  It should represent the time zone used
        ' throughout the component.
        If timeZoneId Is Nothing
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
    ''' Update the free/busy object with the dialog control values
    ''' </summary>
    ''' <param name="fb">The free/busy object in which the settings are updated</param>
    Public Sub GetValues(fb As VFreeBusy)
        ' The unique ID is not changed
        fb.Organizer.Value = txtOrganizer.Text
        fb.Contact.Value = txtContact.Text

        ' We'll use the TimeZoneDateTime property on all date/time values so that they are set literally rather
        ' than being converted to the time zone as would happen with the DateTimeValue property.
        If dtpStartDate.Checked = False Then
            fb.StartDateTime.TimeZoneDateTime = DateTime.MinValue
        Else
            fb.StartDateTime.TimeZoneDateTime = dtpStartDate.Value
            fb.StartDateTime.ValueLocation = ValLocValue.DateTime
        End If

        If dtpEndDate.Checked = False Then
            fb.EndDateTime.TimeZoneDateTime = DateTime.MinValue
        Else
            fb.EndDateTime.TimeZoneDateTime = dtpEndDate.Value
            fb.StartDateTime.ValueLocation = ValLocValue.DateTime
        End If

        fb.Duration.DurationValue = New Duration(txtDuration.Text)
        fb.Url.Value = txtUrl.Text
        fb.Comment.Value = txtComments.Text

        ' For the collections, we'll clear the existing items and copy the modified items from the browse control
        ' binding sources.
        fb.Attendees.Clear()
        fb.Attendees.CloneRange(DirectCast(ucAttendees.BindingSource.DataSource, AttendeePropertyCollection))

        fb.FreeBusy.Clear()
        fb.FreeBusy.CloneRange(DirectCast(ucFreeBusy.BindingSource.DataSource, FreeBusyPropertyCollection))

        fb.RequestStatuses.Clear()
        fb.RequestStatuses.CloneRange(DirectCast(ucRequestStatus.BindingSource.DataSource, RequestStatusPropertyCollection))

        ' Set the time zone in the object after getting all the data.  The "Set" method will not modify the
        ' date/times like the "Apply" method does.
        If cboTimeZone.Enabled = True And cboTimeZone.SelectedIndex <> 0 Then
            fb.SetTimeZone(VCalendar.TimeZones(cboTimeZone.SelectedIndex - 1))
        Else
            fb.SetTimeZone(Nothing)
        End If
    End Sub

End Class
