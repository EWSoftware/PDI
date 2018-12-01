'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : AlarmControl.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/05/2015
' Note    : Copyright 2004-2015, Eric Woodruff, All rights reserved
' Compiler: Microsoft VB.NET
'
' This is used to edit a calendar object's alarm collection properties
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 01/03/2005  EFW  Created the code
' 05/22/2007  EFW  Converted for use with .NET 2.0
'================================================================================================================

Imports System.ComponentModel
Imports System.Globalization
Imports System.Windows.Forms

Imports EWSoftware.PDI
Imports EWSoftware.PDI.Objects
Imports EWSoftware.PDI.Properties

Public Partial Class AlarmControl
    Inherits EWSoftware.PDI.Windows.Forms.BrowseControl

    Private currentAlarm As VAlarm

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        ' Set the short date/long time pattern based on the current culture
        dtpTrigger.CustomFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern & " " &
            CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern

        AddHandler Me.BindingSource.PositionChanged, New EventHandler( _
            AddressOf BindingSource_PositionChanged)

        ' Use a default collection as the data source
        Me.BindingSource.DataSource = New VAlarmCollection()
    End Sub

    ''' <summary>
    ''' Enable or disable the controls based on whether or not there are items in the collection
    ''' </summary>
    ''' <param name="enable">True to enable the controls, false to disable them</param>
    public Overrides Sub EnableControls(enable As Boolean)
        tabInfo.Enabled = enable

        If enable = True Then
            cboAction.Focus()
        End If
    End Sub

    ''' <summary>
    ''' This is overridden to bind the controls to the data source
    ''' </summary>
    ''' <remarks>In this case, we can bind to a few of the simple properties but the rest will be handled
    ''' manually when the position changes.
    ''' </remarks>
    Public Overrides Sub BindToControls()
        txtOtherAction.DataBindings.Add("Text", Me.BindingSource, "Action_OtherAction")

        udcRepeat.DataBindings.Add("Value", Me.BindingSource, "Repeat_RepeatCount")
        txtSummary.DataBindings.Add("Text", Me.BindingSource, "Summary_Value")
        txtDescription.DataBindings.Add("Text", Me.BindingSource, "Description_Value")

        ' The binding events translate between the index value and the action type
        Dim b As New Binding("SelectedIndex", Me.BindingSource, "Action_Action")
        AddHandler b.Format, New ConvertEventHandler(AddressOf AlarmAction_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf AlarmAction_Parse)
        cboAction.DataBindings.Add(b)
    End Sub

    ''' <summary>
    ''' This is used to translate between the alarm action and the index
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub AlarmAction_Format(sender As Object, e As ConvertEventArgs)
        e.Value = CType(e.Value, Integer)
    End Sub

    ''' <summary>
    ''' This is used to translate between the alarm action and the index
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub AlarmAction_Parse(sender As Object, e As ConvertEventArgs)
        e.Value = CType(e.Value, AlarmAction)
    End Sub

    ''' <summary>
    ''' Validate the items in the collection
    ''' </summary>
    ''' <returns>Returns true if all are valid or false if any item is not valid.  The position is set to the
    ''' first invalid item.</returns>
    ''' <remarks>The navigator doesn't allow for validation of all fields so we have to do some checking before
    ''' saving the results.</remarks>
    Public Function ValidateItems() As Boolean
        Dim result As Boolean = True

        tabInfo.SelectedTab = pgGeneral

        Me.ErrorProvider.Clear()

        Dim alarms As VAlarmCollection = DirectCast(Me.BindingSource.DataSource, VAlarmCollection)

        For Each a As VAlarm In alarms
            If a.Trigger.DurationValue = Duration.Zero AndAlso a.Trigger.TimeZoneDateTime = DateTime.MinValue Then
                Me.BindingSource.Position = alarms.IndexOf(a)
                Me.ErrorProvider.SetError(dtpTrigger, "A trigger duration or date/time must be specified")
                result = False
            End If

            If (a.Action.Action = AlarmAction.EMail OrElse a.Action.Action = AlarmAction.Display) AndAlso
              String.IsNullOrWhiteSpace(a.Description.Value) Then
                Me.ErrorProvider.SetError(txtDescription, "A description is required")
                result = False
            End If

            If a.Action.Action = AlarmAction.EMail Then
                If String.IsNullOrWhiteSpace(a.Summary.Value) Then
                    Me.ErrorProvider.SetError(txtSummary, "A summary is required for an e-mail alarm")
                    result = False
                Else
                    If result AndAlso a.Attendees.Count = 0 Then
                        tabInfo.SelectedTab = pgAttendees
                        Me.ErrorProvider.SetError(ucAttendees, "At least one attendee is required for " &
                            "an e-mail alarm")
                        result = False
                    End If
                End If
            End If

            If Not result Then
                Exit For
            End If
        Next

        Return result
    End Function

    ''' <summary>
    ''' Update the prior row with the values from the unbound controls and load the values for the new row when
    ''' the position changes.
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub BindingSource_PositionChanged(sender As Object, e As EventArgs)
        Dim newItem As VAlarm = DirectCast(Me.BindingSource.Current, VAlarm)

        ' If all deleted, ignore it
        If newItem Is Nothing Then
            currentAlarm = Nothing
            Return
        End If

        ' Save changes to the unbound controls to the prior row
        Me.StoreChanges()

        ' Load the new values into the unbound controls
        currentAlarm = newItem

        If currentAlarm.Trigger.ValueLocation = ValLocValue.Duration Then
            If currentAlarm.Trigger.DurationValue <> Duration.Zero Then
                txtTrigger.Text = currentAlarm.Trigger.DurationValue.ToString(Duration.MaxUnit.Weeks)
            Else
                txtTrigger.Text = String.Empty
            End If

            chkFromEnd.Checked = currentAlarm.Trigger.RelatedToEnd
            dtpTrigger.Checked = false
        Else
            txtTrigger.Text = String.Empty
            chkFromEnd.Checked = False

            If currentAlarm.Trigger.TimeZoneDateTime = DateTime.MinValue Then
                dtpTrigger.Checked = False
            Else
                dtpTrigger.Value = currentAlarm.Trigger.TimeZoneDateTime
                dtpTrigger.Checked = True
            End If
        End If

        If currentAlarm.Duration.DurationValue <> Duration.Zero Then
            txtDuration.Text = currentAlarm.Duration.DurationValue.ToString(Duration.MaxUnit.Weeks)
        Else
            txtDuration.Text = String.Empty
        End If

        ' Set attendees and attachments
        ucAttendees.BindingSource.DataSource = New AttendeePropertyCollection().CloneRange(currentAlarm.Attendees)
        ucAttachments.SetValues(currentAlarm.Attachments)
    End Sub

    ''' <summary>
    ''' Store changes to the unbound controls
    ''' </summary>
    Private Sub StoreChanges()
        If currentAlarm Is Nothing Then
            Return
        End If

        If Not dtpTrigger.Checked Then
            currentAlarm.Trigger.TimeZoneDateTime = DateTime.MinValue
            currentAlarm.Trigger.DurationValue = New Duration(txtTrigger.Text)
            currentAlarm.Trigger.RelatedToEnd = chkFromEnd.Checked
        Else
            currentAlarm.Trigger.DurationValue = Duration.Zero
            currentAlarm.Trigger.TimeZoneDateTime = dtpTrigger.Value
            currentAlarm.Trigger.ValueLocation = ValLocValue.DateTime
            currentAlarm.Trigger.RelatedToEnd = False
        End If

        currentAlarm.Duration.DurationValue = New Duration(txtDuration.Text)

        ' Get attendees and attachments
        currentAlarm.Attendees.Clear()
        currentAlarm.Attendees.CloneRange(DirectCast(ucAttendees.BindingSource.DataSource, AttendeePropertyCollection))
        ucAttachments.GetValues(currentAlarm.Attachments)
    End Sub

    ''' <summary>
    ''' This is called by the containing form to apply a new time zone to the date/time values in the control
    ''' </summary>
    ''' <param name="oldTZ">The old time zone's ID</param>
    ''' <param name="newTZ">The new time zone's ID</param>
    Public Sub ApplyTimeZone(oldTZ As String, newTZ As String)
        Dim dti As DateTimeInstance

        If dtpTrigger.Checked Then
            If oldTZ Is Nothing Then
                dti = VCalendar.LocalTimeToTimeZoneTime(dtpTrigger.Value, newTZ)
            Else
                dti = VCalendar.TimeZoneToTimeZone(dtpTrigger.Value, oldTZ, newTZ)
            End If

            dtpTrigger.Value = dti.StartDateTime
            Me.StoreChanges()
        End If
    End Sub

    ''' <summary>
    ''' Store changes to the unbound controls when the control loses the focus too
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub AlarmControl_Leave(sender As Object, e As EventArgs) _
      Handles MyBase.Leave
        Me.StoreChanges()
    End Sub

    ''' <summary>
    ''' Enable the Other Action text box if Other is selected as the action type and enable or disable the other
    ''' controls as needed.
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event parameters.</param>
    Private Sub cboAction_SelectedIndexChanged(sender As Object, e As EventArgs) _
      Handles cboAction.SelectedIndexChanged
        Select Case CType(cboAction.SelectedIndex, AlarmAction)
            Case AlarmAction.Audio
                txtSummary.Enabled = False
                txtDescription.Enabled = False
                txtOtherAction.Enabled = False
                pgAttendees.Enabled = False
                pgAttachments.Enabled = True
                txtOtherAction.Text = Nothing

            Case AlarmAction.Display
                txtSummary.Enabled = False
                txtOtherAction.Enabled = False
                pgAttendees.Enabled = False
                pgAttachments.Enabled = False
                txtDescription.Enabled = True
                txtOtherAction.Text = Nothing

            case AlarmAction.EMail:
                txtOtherAction.Enabled = False
                txtSummary.Enabled = True
                txtDescription.Enabled = True
                pgAttendees.Enabled = True
                pgAttachments.Enabled = True
                txtOtherAction.Text = Nothing

            case AlarmAction.Procedure
                txtSummary.Enabled = False
                txtOtherAction.Enabled = False
                pgAttendees.Enabled = False
                txtDescription.Enabled = True
                pgAttachments.Enabled = True
                txtOtherAction.Text = Nothing

            Case Else       ' Other.  Enable everything.
                txtSummary.Enabled = True
                txtDescription.Enabled = True
                txtOtherAction.Enabled = True
                pgAttendees.Enabled = True
                pgAttachments.Enabled = True

        End Select
    End SUb

    ''' <summary>
    ''' Ensure the duration value is valid
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub Duration_Validating(sender As Object, e As CancelEventArgs) _
      Handles txtDuration.Validating, txtTrigger.Validating
        Dim t As TextBox = DirectCast(sender, TextBox)
        Dim d As Duration

        t.Text = t.Text.Trim()

        If t.Text.Length <> 0 AndAlso Not Duration.TryParse(t.Text, d) Then
            Me.ErrorProvider.SetError(t, "Invalid duration value")
            e.Cancel = True
        Else
            Me.ErrorProvider.SetError(t, String.Empty)
        End If
    End Sub

End Class
