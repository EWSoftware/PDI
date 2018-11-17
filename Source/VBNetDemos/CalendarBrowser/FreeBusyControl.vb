'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : FreeBusyControl.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/05/2015
' Note    : Copyright 2004-2015, Eric Woodruff, All rights reserved
' Compiler: Microsoft VB.NET
'
' This is used to edit a free/busy object's free/busy collection
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

Imports System
Imports System.ComponentModel
Imports System.Globalization
Imports System.Windows.Forms

Imports EWSoftware.PDI
Imports EWSoftware.PDI.Objects
Imports EWSoftware.PDI.Properties
Imports EWSoftware.PDI.Windows.Forms

Public Partial Class FreeBusyControl
    Inherits EWSoftware.PDI.Windows.Forms.BrowseControl

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        cboBusyType.SelectedIndex = 0

        ' Set the short date/long time pattern based on the current culture
        dtpStartDate.CustomFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern & " " &
            CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern
        dtpEndDate.CustomFormat = dtpStartDate.CustomFormat


        ' Use a default collection as the data source
        Me.BindingSource.DataSource = New FreeBusyPropertyCollection()
    End Sub

    ''' <summary>
    ''' Enable or disable the controls based on whether or not there are items in the collection
    ''' </summary>
    ''' <param name="enable">True to enable the controls, false to disable them</param>
    Public Overrides Sub EnableControls(enable As Boolean)
        cboBusyType.Enabled = enable
        txtOtherType.Enabled = enable
        dtpStartDate.Enabled = enable
        dtpEndDate.Enabled = enable

        If enable = True Then
            cboBusyType.Focus()
        End If
    End Sub

    ''' <summary>
    ''' This is overridden to bind the controls to the data source
    ''' </summary>
    Public Overrides Sub BindToControls()
        txtOtherType.DataBindings.Add("Text", Me.BindingSource, "OtherType")

        ' The binding events translate between the index value and the free/busy type
        Dim b As New Binding("SelectedIndex", Me.BindingSource, "FreeBusyType")
        AddHandler b.Format, New ConvertEventHandler(AddressOf FreeBusyType_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf FreeBusyType_Parse)
        cboBusyType.DataBindings.Add(b)

        ' This binding events will take care of values that haven't been set in the date/time controls
        b = New Binding("Value", Me.BindingSource, "PeriodValue_StartDateTime")
        AddHandler b.Format, New ConvertEventHandler(AddressOf DateTime_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf DateTime_Parse)
        dtpStartDate.DataBindings.Add(b)

        b = New Binding("Value", Me.BindingSource, "PeriodValue_EndDateTime")
        AddHandler b.Format, New ConvertEventHandler(AddressOf DateTime_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf DateTime_Parse)
        dtpEndDate.DataBindings.Add(b)
    End Sub

    ''' <summary>
    ''' This is used to translate between the free/busy type and the index
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub FreeBusyType_Format(sender As Object, e As ConvertEventArgs)
        e.Value = CType(e.Value, Integer)
    End SUb

    ''' <summary>
    ''' This is used to translate between the free/busy type and the index
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub FreeBusyType_Parse(sender As Object, e As ConvertEventArgs)
        e.Value = CType(e.Value, FreeBusyType)
    End Sub

    ''' <summary>
    ''' Get the date/time control based on the property value
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub DateTime_Format(sender As Object, e As ConvertEventArgs)
        Dim dtp As DateTimePicker = DirectCast(DirectCast(sender, Binding).Control, DateTimePicker)
        Dim dt As DateTime = DirectCast(e.Value, DateTime)

        If dt = DateTime.MinValue Then
            dtp.Checked = False
            e.Value = DateTime.Today
        Else
            dtp.Checked = True
        End If
    End Sub

    ''' <summary>
    ''' Set the date/time control based on the property value
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub DateTime_Parse(sender As Object, e As ConvertEventArgs)
        Dim dtp As DateTimePicker = DirectCast(DirectCast(sender, Binding).Control, DateTimePicker)

        If Not dtp.Checked Then
            e.Value = DateTime.MinValue
        End If
    End Sub

    ''' <summary>
    ''' Validate the items in the collection
    ''' </summary>
    ''' <returns>Returns true if all are valid or false if any item is not valid.  The position is set to the
    ''' first invalid item.</returns>
    ''' <remarks>The navigator doesn't allow for validation of all fields so we have to do some checking before
    ''' saving the results.</remarks>
    Public Function ValidateItems() As Boolean
        Me.ErrorProvider.Clear()

        Dim freebusys As FreeBusyPropertyCollection = DirectCast(Me.BindingSource.DataSource, FreeBusyPropertyCollection)

        For Each fb As FreeBusyProperty In freebusys
            If fb.FreeBusyType = FreeBusyType.Other AndAlso fb.OtherType.Trim().Length = 0 Then
                Me.BindingSource.Position = freebusys.IndexOf(fb)
                txtOtherType.Focus()

                Me.ErrorProvider.SetError(txtOtherType, "When type is set to 'OTHER', a description is required")
                Return False
            End If

            If fb.PeriodValue.StartDateTime = DateTime.MinValue Then
                Me.BindingSource.Position = freebusys.IndexOf(fb)
                dtpStartDate.Focus()

                Me.ErrorProvider.SetError(dtpStartDate, "A start date is required")
                Return False
            End If

            If fb.PeriodValue.EndDateTime = DateTime.MinValue Then
                Me.BindingSource.Position = freebusys.IndexOf(fb)
                dtpEndDate.Focus()

                Me.ErrorProvider.SetError(dtpEndDate, "An end date is required")
                Return False
            End If

            If fb.PeriodValue.StartDateTime > fb.PeriodValue.EndDateTime Then
                Me.BindingSource.Position = freebusys.IndexOf(fb)
                dtpStartDate.Focus()

                Me.ErrorProvider.SetError(dtpStartDate, "Start date must be less than or equal to end date")
                Return False
            End If
        Next

        Return true
    End Function

    ''' <summary>
    ''' A free/busy type other than None is required
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event parameters</param>
    Private Sub cboBusyType_Validating(sender As Object, e As CancelEventArgs) _
      Handles cboBusyType.Validating
        Me.ErrorProvider.Clear()

        If Not Me.DesignMode AndAlso DirectCast(sender, Control).Enabled AndAlso cboBusyType.SelectedIndex = 0 Then
            Me.ErrorProvider.SetError(cboBusyType, "A busy type other than None is required")
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' Enable the Other Type text box if Other is selected as the free/busy type
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event parameters.</param>
    Private Sub cboBusyType_SelectedIndexChanged(sender As Object, _
      e As System.EventArgs) Handles cboBusyType.SelectedIndexChanged
        If cboBusyType.SelectedIndex = CType(FreeBusyType.Other, Integer) Then
            txtOtherType.Enabled = True
        Else
            txtOtherType.Enabled = False
            txtOtherType.Text = Nothing
        End If
    End Sub

    ''' <summary>
    ''' This is called by the containing form to apply a new time zone to the date/time values in the control
    ''' </summary>
    ''' <param name="oldTZ">The old time zone's ID</param>
    ''' <param name="newTZ">The new time zone's ID</param>
    Public Sub ApplyTimeZone(oldTZ As String, newTZ As String)
        Dim dti As DateTimeInstance

        If oldTZ Is Nothing Then
            If dtpStartDate.Checked = True Then
                dti = VCalendar.LocalTimeToTimeZoneTime(dtpStartDate.Value, newTZ)
                dtpStartDate.Value = dti.StartDateTime
            End If

            If dtpEndDate.Checked = True Then
                dti = VCalendar.LocalTimeToTimeZoneTime(dtpEndDate.Value, newTZ)
                dtpEndDate.Value = dti.StartDateTime
            End If
        Else
            If dtpStartDate.Checked = True Then
                dti = VCalendar.TimeZoneToTimeZone(dtpStartDate.Value, oldTZ, newTZ)
                dtpStartDate.Value = dti.StartDateTime
            End If

            If dtpEndDate.Checked = True Then
                dti = VCalendar.TimeZoneToTimeZone(dtpEndDate.Value, oldTZ, newTZ)
                dtpEndDate.Value = dti.StartDateTime
            End If
        End If
    End Sub

End Class
