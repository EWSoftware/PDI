'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : CalendarObjectDetails.aspx.cs
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/01/2015
' Note    : Copyright 2004-2015, Eric Woodruff, All rights reserved
' Compiler: Microsoft Visual C#
'
' This page is used to demonstrate the calendar classes.  Currently, it allows editing of some basic information.
' Information in the data grids could also be edited.  Time constraints limit what I have implemented so far but
' I may expand on this at a later date.
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 01/29/2005  EFW  Created the code
'================================================================================================================

Imports System
Imports System.ComponentModel
Imports System.Globalization
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Imports EWSoftware.PDI
Imports EWSoftware.PDI.Objects
Imports EWSoftware.PDI.Properties
Imports EWSoftware.PDI.Web.Controls

Namespace PDIWebDemoVB

    Partial Class CalendarObjectDetails
        Inherits System.Web.UI.Page

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim cal As VCalendar, idx As Integer, itemType As String

            If Not Page.IsPostBack Then
                cal = DirectCast(Session("VCalendar"), VCalendar)
                itemType = Request.QueryString("Type")

                If cal Is Nothing Or itemType Is Nothing Then
                    Response.Redirect("CalendarBrowser.aspx")
                    Return
                End If

                If Not Int32.TryParse(Request.QueryString("Index"), idx) Then
                    ' If not valid just go back to the browser form
                    Response.Redirect("CalendarBrowser.aspx")
                    Return
                End If

                itemType = itemType.Trim().ToUpperInvariant()
                Me.ViewState("ObjectType") = itemType

                ' Disable controls that aren't relevant to the vCalendar specification
                If cal.Version = SpecificationVersions.vCalendar10 Then
                    txtDuration.Enabled = False
                    txtOrganizer.Enabled = False
                    dgReqStats.Visible = False
                End If

                Select Case itemType
                    Case "EVENT"
                        ' Force it to be valid
                        If idx < 0 Or idx >= cal.Events.Count Then
                            idx = 0
                        End If

                        ' Load the data into the controls
                        lblItemType.Text = "Event"
                        LoadEventInfo(cal.Events(idx))

                    Case "TODO"
                        ' Force it to be valid
                        If idx < 0 Or idx >= cal.ToDos.Count Then
                            idx = 0
                        End If

                        ' Load the data into the controls
                        lblItemType.Text = "To-Do"
                        LoadToDoInfo(cal.ToDos(idx))

                    Case "JOURNAL"
                        ' Force it to be valid
                        If idx < 0 Or idx >= cal.Journals.Count Then
                            idx = 0
                        End If

                        ' Load the data into the controls
                        lblItemType.Text = "Journal"
                        LoadJournalInfo(cal.Journals(idx))

                End Select

                Me.ViewState("ObjectIndex") = idx
                Page.DataBind()
            End If
        End Sub

        ' Get the recurring object being viewed from the session state
        Private Function GetCurrentObject() As RecurringObject
            Dim cal As VCalendar = DirectCast(Session("VCalendar"), VCalendar)
            Dim idx As Integer = DirectCast(Me.ViewState("ObjectIndex"), Integer)
            Dim itemType As String = DirectCast(Me.ViewState("ObjectType"), String)
            Dim ro As RecurringObject

            Select Case itemType
                Case "EVENT"
                    ro = cal.Events(idx)

                Case "TODO"
                    ro = cal.ToDos(idx)

                Case Else
                    ro = cal.Journals(idx)
            End Select

            Return ro
        End Function

        ' Save changes and return to the calendar list
        Private Sub btnSave_Click(ByVal sender As Object, _
          ByVal e As System.EventArgs) Handles btnSave.Click
            Dim isValid As Boolean = True

            If Not Page.IsValid Then
                Return
            End If

            Dim cal As VCalendar = DirectCast(Session("VCalendar"), VCalendar)

            ' Not very friendly, but it's just a demo
            If cal Is Nothing Then
                Response.Redirect("CalendarBrowser.aspx")
                Return
            End If

            Dim idx As Integer = DirectCast(Me.ViewState("ObjectIndex"), Integer)
            Dim itemType As String = DirectCast(Me.ViewState("ObjectType"), String)

            Select Case itemType
                Case "EVENT"
                    isValid = StoreEventInfo(cal.Events(idx))

                Case "TODO"
                    isValid = StoreToDoInfo(cal.ToDos(idx))

                Case "JOURNAL"
                    isValid = StoreJournalInfo(cal.Journals(idx))
            End Select

            If isValid = True Then
                Response.Redirect("CalendarBrowser.aspx")
            End If
        End Sub

        ' Exit without saving
        Private Sub btnExit_Click(ByVal sender As Object, _
          ByVal e As System.EventArgs) Handles btnExit.Click
            Response.Redirect("CalendarBrowser.aspx")
        End Sub

        ' Load event information into the controls
        Private Sub LoadEventInfo(ev As VEvent)
            txtCompleted.Enabled = False
            txtPercent.Enabled = False
            lblEndDate.Text = "End"

            lblUniqueId.Text = ev.UniqueId.Value
            lblTimeZone.Text = ev.StartDateTime.TimeZoneId
            chkTransparent.Checked = ev.Transparency.IsTransparent
            txtSequence.Text = ev.Sequence.SequenceNumber.ToString()
            txtPriority.Text = ev.Priority.PriorityValue.ToString()

            ' General
            If ev.StartDateTime.TimeZoneDateTime <> DateTime.MinValue Then
                txtStartDate.Text = ev.StartDateTime.TimeZoneDateTime.ToString("G")
            End If

            If ev.EndDateTime.TimeZoneDateTime <> DateTime.MinValue Then
                txtEndDate.Text = ev.EndDateTime.TimeZoneDateTime.ToString("G")
            End If

            If ev.Duration.DurationValue <> Duration.Zero Then
                txtDuration.Text = ev.Duration.DurationValue.ToString(Duration.MaxUnit.Weeks)
            End If

            txtSummary.Text = ev.Summary.Value
            txtLocation.Text = ev.Location.Value
            txtDescription.Text = ev.Description.Value
            txtOrganizer.Text = ev.Organizer.Value
            txtUrl.Text = ev.Url.Value
            txtComments.Text = ev.Comment.Value

            ' Load status values and set status
            cboStatus.Items.Add("None")
            cboStatus.Items.Add("Tentative")
            cboStatus.Items.Add("Confirmed")
            cboStatus.Items.Add("Cancelled")

            If cboStatus.Items.FindByValue(ev.Status.StatusValue.ToString()) Is Nothing Then
                cboStatus.Items.Add(ev.Status.StatusValue.ToString())
            End If

            cboStatus.SelectedValue = ev.Status.StatusValue.ToString()

            dgAttendees.DataSource = ev.Attendees
            dgRecurrences.DataSource = ev.RecurrenceRules
            dgRecurDates.DataSource = ev.RecurDates
            dgExceptions.DataSource = ev.ExceptionRules
            dgExDates.DataSource = ev.ExceptionDates
            dgReqStats.DataSource = ev.RequestStatuses
        End Sub

        ' Load To-Do information into the controls
        Private Sub LoadToDoInfo(td As VToDo)
            chkTransparent.Enabled = False
            txtLocation.Enabled = False

            lblEndDate.Text = "Due"

            lblUniqueId.Text = td.UniqueId.Value
            lblTimeZone.Text = td.StartDateTime.TimeZoneId
            txtSequence.Text = td.Sequence.SequenceNumber.ToString()
            txtPriority.Text = td.Priority.PriorityValue.ToString()

            ' General
            If td.StartDateTime.TimeZoneDateTime <> DateTime.MinValue Then
                txtStartDate.Text = td.StartDateTime.TimeZoneDateTime.ToString("G")
            End If

            ' We'll reuse End Date for Due Date
            If td.DueDateTime.TimeZoneDateTime <> DateTime.MinValue Then
                txtEndDate.Text = td.DueDateTime.TimeZoneDateTime.ToString("G")
            End If

            If td.CompletedDateTime.TimeZoneDateTime <> DateTime.MinValue Then
                txtCompleted.Text = td.CompletedDateTime.TimeZoneDateTime.ToString("G")
            End If

            If td.Duration.DurationValue <> Duration.Zero Then
                txtDuration.Text = td.Duration.DurationValue.ToString(Duration.MaxUnit.Weeks)
            End If

            txtPercent.Text = td.PercentComplete.Percentage.ToString()
            txtSummary.Text = td.Summary.Value
            txtDescription.Text = td.Description.Value
            txtOrganizer.Text = td.Organizer.Value
            txtUrl.Text = td.Url.Value
            txtComments.Text = td.Comment.Value

            ' Load status values and set status
            cboStatus.Items.Add("None")
            cboStatus.Items.Add("NeedsAction")
            cboStatus.Items.Add("Completed")
            cboStatus.Items.Add("InProcess")
            cboStatus.Items.Add("Cancelled")

            If cboStatus.Items.FindByValue(td.Status.StatusValue.ToString()) Is Nothing Then
                cboStatus.Items.Add(td.Status.StatusValue.ToString())
            End If

            cboStatus.SelectedValue = td.Status.StatusValue.ToString()

            dgAttendees.DataSource = td.Attendees
            dgRecurrences.DataSource = td.RecurrenceRules
            dgRecurDates.DataSource = td.RecurDates
            dgExceptions.DataSource = td.ExceptionRules
            dgExDates.DataSource = td.ExceptionDates
            dgReqStats.DataSource = td.RequestStatuses
        End Sub

        ' Load journal information into the controls
        Private Sub LoadJournalInfo(jr As VJournal)
            chkTransparent.Enabled = False
            txtPriority.Enabled = False
            txtEndDate.Enabled = False
            txtDuration.Enabled = False
            txtLocation.Enabled = False
            txtCompleted.Enabled = False
            txtPercent.Enabled = False

            lblUniqueId.Text = jr.UniqueId.Value
            lblTimeZone.Text = jr.StartDateTime.TimeZoneId
            txtSequence.Text = jr.Sequence.SequenceNumber.ToString()
            txtSummary.Text = jr.Summary.Value
            txtDescription.Text = jr.Description.Value
            txtOrganizer.Text = jr.Organizer.Value
            txtUrl.Text = jr.Url.Value
            txtComments.Text = jr.Comment.Value

            If jr.StartDateTime.TimeZoneDateTime <> DateTime.MinValue Then
                txtStartDate.Text = jr.StartDateTime.TimeZoneDateTime.ToString("G")
            End If

            ' Load status values and set status
            cboStatus.Items.Add("None")
            cboStatus.Items.Add("Draft")
            cboStatus.Items.Add("Final")
            cboStatus.Items.Add("Cancelled")

            If cboStatus.Items.FindByValue(jr.Status.StatusValue.ToString()) Is Nothing Then
                cboStatus.Items.Add(jr.Status.StatusValue.ToString())
            End If

            cboStatus.SelectedValue = jr.Status.StatusValue.ToString()

            dgAttendees.DataSource = jr.Attendees
            dgRecurrences.DataSource = jr.RecurrenceRules
            dgRecurDates.DataSource = jr.RecurDates
            dgExceptions.DataSource = jr.ExceptionRules
            dgExDates.DataSource = jr.ExceptionDates
        End Sub

        ' Store event information from the controls
        Private Function StoreEventInfo(ev As VEvent) As Boolean
            Dim startDate As DateTime = DateTime.MinValue
            Dim endDate As DateTime = DateTime.MinValue
            Dim dur As Duration = Duration.Zero

            lblMsg.Text = Nothing

            ' Perform some edits
            If txtStartDate.Text.Trim().Length <> 0 AndAlso Not DateTime.TryParse(txtStartDate.Text,
              CultureInfo.CurrentCulture, DateTimeStyles.None, startDate) Then
                lblMsg.Text = "Invalid start date format<br>"
            End If

            If txtEndDate.Text.Trim().Length <> 0 AndAlso Not DateTime.TryParse(txtEndDate.Text,
              CultureInfo.CurrentCulture, DateTimeStyles.None, endDate) Then
                lblMsg.Text = "Invalid end date format<br>"
            End If

            If txtDuration.Text.Trim().Length <> 0 AndAlso Not Duration.TryParse(txtDuration.Text, dur) Then
                lblMsg.Text += "Invalid duration format<br>"
            End If

            If dur <> Duration.Zero AndAlso txtEndDate.Enabled = True AndAlso endDate <> DateTime.MinValue Then
                lblMsg.Text += "A duration or an end date can be specified, but not both<br>"
            End If

            If txtEndDate.Enabled = True And startDate <> DateTime.MinValue And endDate <> DateTime.MinValue And
              startDate > endDate Then
                lblMsg.Text += "Start date must be less than or equal to end date<br>"
            End If

            If Not String.IsNullOrWhiteSpace(lblMsg.Text) Then
                Return False
            End if

            ' Unique ID is not changed
            ev.Transparency.IsTransparent = chkTransparent.Checked
            ev.LastModified.TimeZoneDateTime = DateTime.Now

            If txtSequence.Text.Trim().Length = 0 Then
                ev.Sequence.SequenceNumber = 0
            Else
                ev.Sequence.SequenceNumber = Convert.ToInt32(txtSequence.Text)
            End If

            If txtPriority.Text.Trim().Length = 0 Then
                ev.Priority.PriorityValue = 0
            Else
                ev.Priority.PriorityValue = Convert.ToInt32(txtPriority.Text)
            End If

            ev.StartDateTime.TimeZoneDateTime = startDate
            ev.StartDateTime.ValueLocation = ValLocValue.DateTime
            ev.EndDateTime.TimeZoneDateTime = endDate
            ev.EndDateTime.ValueLocation = ValLocValue.DateTime
            ev.Duration.DurationValue = dur

            ev.Summary.Value = txtSummary.Text
            ev.Location.Value = txtLocation.Text
            ev.Description.Value = txtDescription.Text
            ev.Organizer.Value = txtOrganizer.Text
            ev.Url.Value = txtUrl.Text
            ev.Comment.Value = txtComments.Text

            ' Get status value
            ev.Status.StatusValue = CType([Enum].Parse(GetType(StatusValue),
                cboStatus.Items(cboStatus.SelectedIndex).ToString(), True), StatusValue)

            Return True
        End Function

        ' Store To-Do information from the controls
        Private Function StoreToDoInfo(td As VToDo) As Boolean
            Dim startDate As DateTime = DateTime.MinValue
            Dim endDate As DateTime = DateTime.MinValue
            Dim completedDate As DateTime = DateTime.MinValue
            Dim dur As Duration = Duration.Zero

            lblMsg.Text = Nothing

            ' Perform some edits
            If txtStartDate.Text.Trim().Length <> 0 AndAlso Not DateTime.TryParse(txtStartDate.Text,
              CultureInfo.CurrentCulture, DateTimeStyles.None, startDate) Then
                lblMsg.Text = "Invalid start date format<br>"
            End If

            If txtEndDate.Text.Trim().Length <> 0 AndAlso Not DateTime.TryParse(txtEndDate.Text,
              CultureInfo.CurrentCulture, DateTimeStyles.None, endDate) Then
                lblMsg.Text = "Invalid end date format<br>"
            End If

            If txtCompleted.Text.Trim().Length <> 0 AndAlso Not DateTime.TryParse(txtCompleted.Text,
              CultureInfo.CurrentCulture, DateTimeStyles.None, completedDate) Then
                lblMsg.Text = "Invalid completed date format<br>"
            End If

            If txtDuration.Text.Trim().Length <> 0 AndAlso Not Duration.TryParse(txtDuration.Text, dur) Then
                lblMsg.Text += "Invalid duration format<br>"
            End If

            If isValid = False Then
                Return False
            End If

            ' Unique ID is not changed
            td.LastModified.TimeZoneDateTime = DateTime.Now

            If txtSequence.Text.Trim().Length = 0 Then
                td.Sequence.SequenceNumber = 0
            Else
                td.Sequence.SequenceNumber = Convert.ToInt32(txtSequence.Text)
            End If

            If txtPriority.Text.Trim().Length = 0 Then
                td.Priority.PriorityValue = 0
            Else
                td.Priority.PriorityValue = Convert.ToInt32(txtPriority.Text)
            End If

            td.StartDateTime.TimeZoneDateTime = startDate
            td.StartDateTime.ValueLocation = ValLocValue.DateTime
            td.DueDateTime.TimeZoneDateTime = endDate
            td.DueDateTime.ValueLocation = ValLocValue.DateTime
            td.CompletedDateTime.TimeZoneDateTime = completedDate
            td.CompletedDateTime.ValueLocation = ValLocValue.DateTime

            If txtPercent.Text.Trim().Length = 0 Then
                td.PercentComplete.Percentage = 0
            Else
                td.PercentComplete.Percentage = Convert.ToInt32(txtPercent.Text)
            End If

            td.Duration.DurationValue = dur
            td.Summary.Value = txtSummary.Text
            td.Description.Value = txtDescription.Text
            td.Organizer.Value = txtOrganizer.Text
            td.Url.Value = txtUrl.Text
            td.Comment.Value = txtComments.Text

            ' Get status value
            td.Status.StatusValue = CType([Enum].Parse(GetType(StatusValue),
                cboStatus.Items(cboStatus.SelectedIndex).ToString(), True), StatusValue)

            Return True
        End Function

        ' Store journal information from the controls
        Private Function StoreJournalInfo(jr As VJournal) As Boolean
            Dim startDate As DateTime = DateTime.MinValue

            lblMsg.Text = Nothing

            ' Perform some edits
            If txtStartDate.Text.Trim().Length <> 0 AndAlso Not DateTime.TryParse(txtStartDate.Text,
              CultureInfo.CurrentCulture, DateTimeStyles.None, startDate) Then
                lblMsg.Text = "Invalid start date format<br>"
                Return False
            End If

            ' Unique ID is not changed
            jr.LastModified.TimeZoneDateTime = DateTime.Now

            If txtSequence.Text.Trim().Length = 0 Then
                jr.Sequence.SequenceNumber = 0
            Else
                jr.Sequence.SequenceNumber = Convert.ToInt32(txtSequence.Text)
            End If

            jr.Summary.Value = txtSummary.Text
            jr.Description.Value = txtDescription.Text
            jr.Organizer.Value = txtOrganizer.Text
            jr.Url.Value = txtUrl.Text
            jr.Comment.Value = txtComments.Text
            jr.StartDateTime.TimeZoneDateTime = startDate
            jr.StartDateTime.ValueLocation = ValLocValue.DateTime

            ' Get status value
            jr.Status.StatusValue = CType([Enum].Parse(GetType(StatusValue),
                cboStatus.Items(cboStatus.SelectedIndex).ToString(), True), StatusValue)

            Return True
        End Function

        ' This handles the Add command for the recurrence data grid
        Private Sub dgRecurrences_ItemCommand(source As Object, _
          e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgRecurrences.ItemCommand
            If e.CommandName = "Add" Then
                ' Save changes to the edited item if there is one
                If dgRecurrences.EditItemIndex <> -1 Then
                    dgRecurrences_UpdateCommand(source, New DataGridCommandEventArgs(
                        dgRecurrences.Items(dgRecurrences.EditItemIndex), e.CommandSource, e))
                End If

                ' Ignore the request if the page is not valid
                If Page.IsValid = False Then
                    Return
                End If

                ' Add a new recurrence and go into edit mode on it
                Dim ro As RecurringObject = GetCurrentObject()

                Dim rrule As New RRuleProperty()
                rrule.Recurrence.RecurDaily(1)
                ro.RecurrenceRules.Add(rrule)

                dgRecurrences.EditItemIndex = ro.RecurrenceRules.Count - 1
                dgRecurrences.DataSource = ro.RecurrenceRules
                dgRecurrences.DataBind()
            End If
        End Sub

        ' Edit a recurrence in the recurrence collection
        Private Sub dgRecurrences_EditCommand(source As Object, _
          e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgRecurrences.EditCommand
            ' Ignore the request if the page is not valid
            If Page.IsValid = False Then
                Return
            End If

            ' Save changes to the prior edited item
            If dgRecurrences.EditItemIndex <> -1 Then
                dgRecurrences_UpdateCommand(source, New DataGridCommandEventArgs(
                    dgRecurrences.Items(dgRecurrences.EditItemIndex), e.CommandSource, e))

                If Page.IsValid = False Then
                    Return
                End If
            End If

            Dim ro As RecurringObject = GetCurrentObject()

            dgRecurrences.EditItemIndex = e.Item.ItemIndex
            dgRecurrences.DataSource = ro.RecurrenceRules
            dgRecurrences.DataBind()
        End Sub

        ' Delete a recurrence from the collection
        Private Sub dgRecurrences_DeleteCommand(source As Object, _
          e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgRecurrences.DeleteCommand
            ' Save changes to the edited item if it isn't the one being deleted
            If dgRecurrences.EditItemIndex <> -1 And dgRecurrences.EditItemIndex <> e.Item.ItemIndex Then
                Page.Validate()
                dgRecurrences_UpdateCommand(source, New DataGridCommandEventArgs(
                    dgRecurrences.Items(dgRecurrences.EditItemIndex), e.CommandSource, e))

                If Page.IsValid = False Then
                    Return
                End If
            End If

            Dim ro As RecurringObject = GetCurrentObject()

            ro.RecurrenceRules.RemoveAt(e.Item.ItemIndex)
            dgRecurrences.EditItemIndex = -1
            dgRecurrences.DataSource = ro.RecurrenceRules
            dgRecurrences.DataBind()
        End Sub

        ' Cancel changes to a recurrence in the collection
        Private Sub dgRecurrences_CancelCommand(source As Object, _
          e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgRecurrences.CancelCommand
            Dim ro As RecurringObject = GetCurrentObject()

            dgRecurrences.EditItemIndex = -1
            dgRecurrences.DataSource = ro.RecurrenceRules
            dgRecurrences.DataBind()
        End Sub

        ' Update a recurrence item in the collection
        Private Sub dgRecurrences_UpdateCommand(source As Object, _
          e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgRecurrences.UpdateCommand
            If Page.IsValid = False Then
                Return
            End If

            Dim ro As RecurringObject = GetCurrentObject()

            Dim rpRecurrence As RecurrencePattern = DirectCast(e.Item.FindControl("rpRecurrence"), RecurrencePattern)

            Dim r As Recurrence = ro.RecurrenceRules(e.Item.ItemIndex).Recurrence
            r.Reset()
            rpRecurrence.GetRecurrence(r)

            dgRecurrences.EditItemIndex = -1
            dgRecurrences.DataSource = ro.RecurrenceRules
            dgRecurrences.DataBind()
        End Sub

        ' Bind data to the edit item template.  We have to retrieve the calendar object from session state and
        ' retrieve the recurrence item from it to set the values in the Recurrence Pattern web server control.
        Private Sub dgRecurrences_ItemDataBound(sender As Object, _
          e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgRecurrences.ItemDataBound
            If e.Item.ItemType = ListItemType.EditItem Then
                Dim ro As RecurringObject = GetCurrentObject()

                Dim rpRecurrence As RecurrencePattern = DirectCast(e.Item.FindControl("rpRecurrence"), RecurrencePattern)

                rpRecurrence.SetRecurrence(ro.RecurrenceRules(e.Item.ItemIndex).Recurrence)
                rpRecurrence.Focus()
            End If
        End Sub

        ' This handles the Add command for the exception data grid
        Private Sub dgExceptions_ItemCommand(source As Object, _
          e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgExceptions.ItemCommand
            If e.CommandName = "Add" Then
                ' Save changes to the edited item if there is one
                If dgExceptions.EditItemIndex <> -1 Then
                    dgExceptions_UpdateCommand(source, New DataGridCommandEventArgs(
                        dgExceptions.Items(dgExceptions.EditItemIndex), e.CommandSource, e))
                End If

                ' Ignore the request if the page is not valid
                If Page.IsValid = False Then
                    Return
                End If

                ' Add a new exception and go into edit mode on it
                Dim ro As RecurringObject = GetCurrentObject()

                Dim exrule As New ExRuleProperty()
                exrule.Recurrence.RecurDaily(1)
                ro.ExceptionRules.Add(exrule)

                dgExceptions.EditItemIndex = ro.ExceptionRules.Count - 1
                dgExceptions.DataSource = ro.ExceptionRules
                dgExceptions.DataBind()
            End If
        End Sub

        ' Edit an exception in the exception collection
        Private Sub dgExceptions_EditCommand(source As Object, _
          e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgExceptions.EditCommand
            ' Ignore the request if the page is not valid
            If Page.IsValid = False Then
                Return
            End If

            ' Save changes to the prior edited item
            If dgExceptions.EditItemIndex <> -1 Then
                dgExceptions_UpdateCommand(source, New DataGridCommandEventArgs(
                    dgExceptions.Items(dgExceptions.EditItemIndex), e.CommandSource, e))

                If Page.IsValid = False Then
                    Return
                End If
            End If

            Dim ro As RecurringObject = GetCurrentObject()

            dgExceptions.EditItemIndex = e.Item.ItemIndex
            dgExceptions.DataSource = ro.ExceptionRules
            dgExceptions.DataBind()
        End Sub

        ' Delete an exception from the collection
        Private Sub dgExceptions_DeleteCommand(source As Object, _
          e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgExceptions.DeleteCommand
            ' Save changes to the edited item if it isn't the one being deleted
            If dgExceptions.EditItemIndex <> -1 And dgExceptions.EditItemIndex <> e.Item.ItemIndex Then
                Page.Validate()
                dgExceptions_UpdateCommand(source, New DataGridCommandEventArgs(
                    dgExceptions.Items(dgExceptions.EditItemIndex), e.CommandSource, e))

                If Page.IsValid = False Then
                    Return
                End If
            End If

            Dim ro As RecurringObject = GetCurrentObject()

            ro.ExceptionRules.RemoveAt(e.Item.ItemIndex)
            dgExceptions.EditItemIndex = -1
            dgExceptions.DataSource = ro.ExceptionRules
            dgExceptions.DataBind()
        End Sub

        ' Cancel changes to an exception in the collection
        Private Sub dgExceptions_CancelCommand(source As Object, _
          e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgExceptions.CancelCommand
            Dim ro As RecurringObject = GetCurrentObject()

            dgExceptions.EditItemIndex = -1
            dgExceptions.DataSource = ro.ExceptionRules
            dgExceptions.DataBind()
        End Sub

        ' Update an exception item in the collection
        Private Sub dgExceptions_UpdateCommand(source As Object, _
          e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgExceptions.UpdateCommand
            If Page.IsValid = False Then
                Return
            End If

            Dim ro As RecurringObject = GetCurrentObject()

            Dim rpException As RecurrencePattern = DirectCast(e.Item.FindControl("rpException"), RecurrencePattern)

            Dim r As Recurrence = ro.ExceptionRules(e.Item.ItemIndex).Recurrence
            r.Reset()
            rpException.GetRecurrence(r)

            dgExceptions.EditItemIndex = -1
            dgExceptions.DataSource = ro.ExceptionRules
            dgExceptions.DataBind()
        End Sub

        ' Bind data to the edit item template.  We have to retrieve the calendar object from session state and
        ' retrieve the exception item from it to set the values in the Recurrence Pattern web server control.
        Private Sub dgExceptions_ItemDataBound(sender As Object, _
          e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgExceptions.ItemDataBound
            If e.Item.ItemType = ListItemType.EditItem Then
                Dim ro As RecurringObject = GetCurrentObject()

                Dim rpException As RecurrencePattern = DirectCast(e.Item.FindControl("rpException"), RecurrencePattern)

                rpException.SetRecurrence(ro.ExceptionRules(e.Item.ItemIndex).Recurrence)
                rpException.Focus()
            End If
        End Sub

    End Class

End Namespace
