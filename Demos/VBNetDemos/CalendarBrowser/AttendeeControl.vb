'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : AttendeeControl.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/05/2015
' Note    : Copyright 2004-2015, Eric Woodruff, All rights reserved
' Compiler: Microsoft VB.NET
'
' This is used to edit a calendar object's attendee collection
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 12/14/2004  EFW  Created the code
' 05/22/2007  EFW  Converted for use with .NET 2.0
'================================================================================================================

Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Imports EWSoftware.PDI.Objects
Imports EWSoftware.PDI.Properties
Imports EWSoftware.PDI.Windows.Forms

''' <summary>
''' This is used to edit the Attendees property collection
''' </summary>
Public Partial Class AttendeeControl
    Inherits EWSoftware.PDI.Windows.Forms.BrowseControl

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        ' Use a default collection as the data source
        Me.BindingSource.DataSource = New AttendeePropertyCollection()
    End Sub

    ''' <summary>
    ''' Enable or disable the controls based on whether or not there are items in the collection
    ''' </summary>
    ''' <param name="enable">True to enable the controls, false to disable them.</param>
    Public Overrides Sub EnableControls(enable As Boolean)
        ' The easiest way to do this is to add a panel control with its Dock property set to Fill.  Place the
        ' controls in it and then enable or disable the panel.
        pnlControls.Enabled = enable

        If enable = True Then
            txtAttendee.Focus()
        End If
    End Sub

    ''' <summary>
    ''' This is overridden to bind the controls to the data source
    ''' </summary>
    Public Overrides Sub BindToControls()
        txtAttendee.DataBindings.Add("Text", Me.BindingSource, "Value")
        txtCommonName.DataBindings.Add("Text", Me.BindingSource, "CommonName")
        txtUserType.DataBindings.Add("Text", Me.BindingSource, "CalendarUserType")
        txtSentBy.DataBindings.Add("Text", Me.BindingSource, "SentBy")
        chkRSVP.DataBindings.Add("Checked", Me.BindingSource, "Rsvp")

        ' For the combo boxes, we'll handle unknown values too via the binding events
        Dim b As New Binding("SelectedIndex", Me.BindingSource, "Role")
        AddHandler b.Format, New ConvertEventHandler(AddressOf RoleStatus_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf RoleStatus_Parse)
        cboRole.DataBindings.Add(b)

        b = New Binding("SelectedIndex", Me.BindingSource, "ParticipationStatus")
        AddHandler b.Format, New ConvertEventHandler(AddressOf RoleStatus_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf RoleStatus_Parse)
        cboStatus.DataBindings.Add(b)
    End Sub

    ''' <summary>
    ''' This handles adding unknown role and status values to the combo boxes
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub RoleStatus_Format(sender As Object, e As ConvertEventArgs)
        Dim c As ComboBox = DirectCast(DirectCast(sender, Binding).Control, ComboBox)
        Dim idx As Integer

        If e.Value Is Nothing Then
            e.Value = 0
        Else
            idx = c.Items.IndexOf(e.Value)

            If idx = -1 Then
                idx = c.Items.Add(e.Value)
            End If

            e.Value = idx
        End If
    End Sub

    ''' <summary>
    ''' This handles converting from the selected index to a value
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub RoleStatus_Parse(sender As Object, e As ConvertEventArgs)
        Dim c As ComboBox = DirectCast(DirectCast(sender, Binding).Control, ComboBox)

        If c.SelectedIndex <> -1 Then
            e.Value = c.Items(c.SelectedIndex)
        Else
            e.Value = Nothing
        End If
    End Sub

    ''' <summary>
    ''' An attendee value is required
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub txtAttendee_Validating(sender As Object, e As CancelEventArgs) _
      Handles txtAttendee.Validating
        Me.ErrorProvider.Clear()

        If Not Me.DesignMode AndAlso DirectCast(sender, Control).Enabled AndAlso txtAttendee.Text.Trim().Length = 0 Then
            Me.ErrorProvider.SetError(txtAttendee, "An attendee value is required")
            e.Cancel = True
        End If
    End Sub

End Class
