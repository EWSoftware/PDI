'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : ObservanceRuleControl.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 11/25/2018
' Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
' Compiler: Microsoft VB.NET
'
' This is used to edit a VTimeZone object's observance rule collection
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 12/09/2004  EFW  Created the code
' 05/21/2007  EFW  Converted for use with .NET 2.0
'================================================================================================================

Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Globalization
Imports System.Windows.Forms

Imports EWSoftware.PDI
Imports EWSoftware.PDI.Objects

''' <summary>
''' This is used to edit the Observance Rule property collection
''' </summary>
Public Partial Class ObservanceRuleControl
    Inherits EWSoftware.PDI.Windows.Forms.BrowseControl

    Private currentRule As ObservanceRule

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        Dim rules As New List(Of ListItem) From {
            New ListItem(ObservanceRuleType.Standard, "Standard"),
            New ListItem(ObservanceRuleType.Daylight, "Daylight")
        }

        cboRuleType.ValueMember = "Value"
        cboRuleType.DisplayMember = "Display"
        cboRuleType.DataSource = rules

        ' Set the short date/long time pattern based on the current culture
        dtpStartDate.CustomFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern & " " &
            CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern

        AddHandler Me.BindingSource.PositionChanged, New EventHandler(AddressOf BindingSource_PositionChanged)

        ' Use a default collection as the data source
        Me.BindingSource.DataSource = New ObservanceRuleCollection()
    End Sub

    ''' <summary>
    ''' Enable or disable the controls based on whether or not there are items in the collection
    ''' </summary>
    ''' <param name="enable">True to enable the controls, false to disable them</param>
    Public Overrides Sub EnableControls(enable As Boolean)
        tabTimeZone.Enabled = enable

        If enable = True Then
            cboRuleType.Focus()
        End If
    End Sub

    ''' <summary>
    ''' This is overridden to bind the controls to the data source
    ''' </summary>
    ''' <remarks>In this case, we can bind to a few of the simple properties but the rest will be handled
    ''' manually when the position changes.
    ''' </remarks>
    Public Overrides Sub BindToControls()
        cboRuleType.DataBindings.Add("SelectedValue", Me.BindingSource, "RuleType")
        txtComment.DataBindings.Add("Text", Me.BindingSource, "Comment_Value")

        ' We'll use the Format event to set a valid default date
        Dim b As New Binding("Value", Me.BindingSource, "StartDateTime_TimeZoneDateTime")
        AddHandler b.Format, New ConvertEventHandler(AddressOf StartDate_Format)

        dtpStartDate.DataBindings.Add(b)
    End Sub

    ''' <summary>
    ''' This is used to set the start date to a valid value if it's outside the acceptable range
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub StartDate_Format(sender As Object, e As ConvertEventArgs)
        Dim startDate As Date = DirectCast(e.Value, Date)

        If startDate < dtpStartDate.MinDate OrElse StartDate > dtpStartDate.MaxDate Then
            e.Value = DateTime.Today
        End If
    End Sub

    ''' <summary>
    ''' A time zone name is required
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub txtTZName_Validating(sender As Object, e As CancelEventArgs) _
      Handles txtTZName.Validating
        Me.ErrorProvider.Clear()

        If Not Me.DesignMode AndAlso DirectCast(sender, Control).Enabled AndAlso txtTZName.Text.Trim().Length = 0 Then
            tabTimeZone.SelectedTab = pgGeneral
            Me.ErrorProvider.SetError(txtTZName, "A time zone name is required")
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' Minutes must be positive if an hours value is entered
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub Minutes_Validating(sender As Object, e As CancelEventArgs) _
      Handles udcFromMinutes.Validating, udcToMinutes.Validating
        Dim udcHours As NumericUpDown
        Dim udcMins As NumericUpDown = DirectCast(sender, NumericUpDown)

        Me.ErrorProvider.Clear()

        If udcMins Is udcFromMinutes
            udcHours = udcFromHours
        Else
            udcHours = udcToHours
        End If

        If Not Me.DesignMode AndAlso udcMins.Enabled AndAlso udcHours.Value <> 0 AndAlso udcMins.Value < 0 Then
            tabTimeZone.SelectedTab = pgGeneral
            Me.ErrorProvider.SetError(udcMins, "Minutes should be positive if an hour value is specified")
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' Update the prior row with the values from the unbound controls and load the values for the new row when
    ''' the position changes.
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub BindingSource_PositionChanged(sender As Object, e As EventArgs)
        Dim newItem As ObservanceRule = DirectCast(Me.BindingSource.Current, ObservanceRule)
        Dim hours, minutes As Integer

        ' If all deleted, ignore it
        If newItem Is Nothing Then
            currentRule = Nothing
            Return
        End If

        ' Save changes to the unbound controls to the prior row
        Me.StoreChanges()

        ' Load the new values into the unbound controls
        currentRule = newItem

        ' We'll only edit the first time zone name
        If currentRule.TimeZoneNames.Count = 0 Then
            currentRule.TimeZoneNames.Add("GMT")
        End If

        txtTZName.Text = currentRule.TimeZoneNames(0).Value

        hours = currentRule.OffsetFrom.TimeSpanValue.Hours
        minutes = currentRule.OffsetFrom.TimeSpanValue.Minutes

        ' If hours are specified, keep minutes positive
        If hours <> 0 AndAlso minutes < 0 Then
            minutes *= -1
        End If

        If hours < -23
            udcFromHours.Value = -23
        Else
            If hours > 23 Then
                udcFromHours.Value = 23
            Else
                udcFromHours.Value = hours
            End If
        End If

        If minutes < -59 Then
            udcFromMinutes.Value = -59
        Else
            If minutes > 59 Then
                udcFromMinutes.Value = 59
            Else
                udcFromMinutes.Value = minutes
            End If
        End If

        hours = currentRule.OffsetTo.TimeSpanValue.Hours
        minutes = currentRule.OffsetTo.TimeSpanValue.Minutes

        ' If hours are specified, keep minutes positive
        If hours <> 0 AndAlso minutes < 0 Then
            minutes *= -1
        End If

        If hours < -23
            udcToHours.Value = -23
        Else
            If hours > 23 Then
                udcToHours.Value = 23
            Else
                udcToHours.Value = hours
            End If
        End If

        If minutes < -59 Then
            udcToMinutes.Value = -59
        Else
            If minutes > 59 Then
                udcToMinutes.Value = 59
            Else
                udcToMinutes.Value = minutes
            End If
        End If

        rcRulesDates.SetValues(currentRule.RecurrenceRules, currentRule.RecurDates)
    End Sub

    ''' <summary>
    ''' Store changes to the unbound controls when the control loses the focus too
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub ObservanceRuleControl_Leave(sender As Object, e As EventArgs) _
      Handles MyBase.Leave
        Me.StoreChanges()
    End Sub

    ''' <summary>
    ''' Store changes to the unbound controls
    ''' </summary>
    Private Sub StoreChanges()
        Dim hours, minutes As Integer

        If currentRule Is Nothing Then
            Return
        End If

        ' We'll only edit the first time zone name
        currentRule.TimeZoneNames(0).Value = txtTZName.Text

        hours = CType(udcFromHours.Value, Integer)
        minutes = CType(udcFromMinutes.Value, Integer)

        If hours < 0 OrElse minutes < 0 Then
            If hours < 0 Then
                hours *= -1
            End If

            If minutes < 0 Then
                minutes *= -1
            End if

            currentRule.OffsetFrom.TimeSpanValue = New TimeSpan(hours, minutes, 0).Negate()
        Else
            currentRule.OffsetFrom.TimeSpanValue = New TimeSpan(hours, minutes, 0)
        End If

        hours = CType(udcToHours.Value, Integer)
        minutes = CType(udcToMinutes.Value, Integer)

        If hours < 0 OrElse minutes < 0 Then
            If hours < 0 Then
                hours *= -1
            End If

            If minutes < 0 Then
                minutes *= -1
            End If

            currentRule.OffsetTo.TimeSpanValue = New TimeSpan(hours, minutes, 0).Negate()
        Else
            currentRule.OffsetTo.TimeSpanValue = New TimeSpan(hours, minutes, 0)
        End If

        rcRulesDates.GetValues(currentRule.RecurrenceRules, currentRule.RecurDates)
    End Sub
End Class
