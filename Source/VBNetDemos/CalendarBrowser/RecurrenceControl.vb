'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : RecurrenceControl.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/02/2023
' Note    : Copyright 2004-2023, Eric Woodruff, All rights reserved
'
' This is used to edit a recurring calendar object's recurrence rule and recurrence date properties
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 12/09/2004  EFW  Created the code
'================================================================================================================

Imports System.ComponentModel
Imports System.Globalization

Imports EWSoftware.PDI
Imports EWSoftware.PDI.Objects
Imports EWSoftware.PDI.Properties
Imports EWSoftware.PDI.Windows.Forms

''' <summary>
''' This is used to edit the recurrence rules and dates property collections of a recurring object
''' </summary>
Public Partial Class RecurrenceControl
    Inherits System.Windows.Forms.UserControl

    Private ReadOnly rRules As RRulePropertyCollection
    Private ReadOnly rDates As RDatePropertyCollection
    Private ctrlEditsExceptions As Boolean

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        ' Set the short date/long time pattern based on the current culture
        dtpRDate.CustomFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern & " " &
            CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern
        dtpRDate.Value = DateTime.Today

        rRules = New RRulePropertyCollection()
        rDates = New RDatePropertyCollection()
        Me.SetButtonStates()
    End Sub

    ''' <summary>
    ''' This property is used to get or set whether or not the control is used for exceptions
    ''' </summary>
    ''' <remarks>If true, the labels are changed to reflect the usage</remarks>
	<Category("Appearance"), DefaultValue(False), Bindable(True), _
     Description("False for recurrence items, True for exception items")> _
    Public Property EditsExceptions As Boolean
        Get
            Return ctrlEditsExceptions
        End Get
        Set
            ctrlEditsExceptions = Value

            If Value = False Then
                lblRRules.Text = "Recurrence &Rules"
                lblRDates.Text = "Recurrence &Dates"
            Else
                lblRRules.Text = "Exception &Rules"
                lblRDates.Text = "Exception &Dates"
            End If
        End Set
    End Property

    ''' <summary>
    ''' Enable or disable buttons based on the collection item count
    ''' </summary>
    Private Sub SetButtonStates()
        Dim enabled As Boolean = (rRules.Count <> 0)

        btnEditRRule.Enabled = enabled
        btnRemoveRRule.Enabled = enabled
        btnClearRRules.Enabled = enabled

        enabled = (rDates.Count <> 0)
        btnRemoveRDate.Enabled = enabled
        btnClearRDates.Enabled = enabled
    End Sub

    ''' <summary>
    ''' Initialize the dialog controls using the specified recurrence rule and recurrence date collections
    ''' </summary>
    ''' <param name="rr">The recurrence rules from which to get the settings</param>
    ''' <param name="rd">The recurrence dates from which to get the settings</param>
    Public Sub SetValues(rr As RRulePropertyCollection, rd As RDatePropertyCollection)
        Dim r As RRuleProperty, rdt As RDateProperty

        rRules.Clear()
        rDates.Clear()
        rRules.CloneRange(rr)
        rDates.CloneRange(rd)

        lbRRules.Items.Clear()

        For Each r In rRules
            lbRRules.Items.Add(r.Recurrence.ToString())
        Next

        ' We won't handle period RDate values
        lbRDates.Items.Clear()

        For Each rdt In rDates
            If rdt.ValueLocation = ValLocValue.Date Then
                lbRDates.Items.Add(rdt.TimeZoneDateTime.ToString("d", CultureInfo.CurrentCulture))
            Else
                lbRDates.Items.Add(rdt.TimeZoneDateTime.ToString("G", CultureInfo.CurrentCulture))
            End If
        Next

        Me.SetButtonStates()
    End Sub

    ''' <summary>
    ''' Initialize the dialog controls using the specified exception rule and exception date collections
    ''' </summary>
    ''' <param name="er">The exception rules from which to get the settings</param>
    ''' <param name="ed">The exception dates from which to get the settings</param>
    Public Sub SetValues(er As RRulePropertyCollection, ed As ExDatePropertyCollection)
        Dim r As RRuleProperty, edate As ExDateProperty
        Dim rdt, rd As RDateProperty

        If ed Is Nothing
            Throw New ArgumentNullException(NameOf(ed))
        End If

        rRules.Clear()
        rDates.Clear()
        rRules.CloneRange(er)

        ' Convert the exception dates to RDate format internally
        For Each edate In ed
            rd = New RDateProperty With {
                .ValueLocation = edate.ValueLocation,
                .TimeZoneId = edate.TimeZoneId,
                .TimeZoneDateTime = edate.TimeZoneDateTime
            }

            rDates.Add(rd)
        Next

        lbRRules.Items.Clear()

        For Each r In rRules
            lbRRules.Items.Add(r.Recurrence.ToString())
        Next

        lbRDates.Items.Clear()

        For Each rdt In rDates
            If(rdt.ValueLocation = ValLocValue.Date)
                lbRDates.Items.Add(rdt.TimeZoneDateTime.ToString("d", CultureInfo.CurrentCulture))
            Else
                lbRDates.Items.Add(rdt.TimeZoneDateTime.ToString("G", CultureInfo.CurrentCulture))
            End If
        Next

        Me.SetButtonStates()
    End Sub

    ''' <summary>
    ''' Update the recurrence rule and recurrence date collections with the dialog control values
    ''' </summary>
    ''' <param name="rr">The recurrence rules collection to update</param>
    ''' <param name="rd">The recurrence dates collection to update</param>
    ''' <remarks>Note that the time zone ID will need to be set in the returned RDATE property collection to make
    ''' sure that it is consistent with the containing component.</remarks>
    Public Sub GetValues(rr As RRulePropertyCollection, rd As RDatePropertyCollection)
        rr?.Clear()
        rd?.Clear()
        rr?.CloneRange(rRules)
        rd?.CloneRange(rDates)
    End Sub

    ''' <summary>
    ''' Update the exception rule and exception date collections with the dialog control values
    ''' </summary>
    ''' <param name="er">The exception rules collection to update</param>
    ''' <param name="ed">The exception dates collection to update</param>
    ''' <remarks>Note that the time zone ID will need to be set in the returned EXDATE property collection to
    ''' make sure that it is consistent with the containing component.</remarks>
    Public Sub GetValues(er As RRulePropertyCollection, ed As ExDatePropertyCollection)
        Dim rdate As RDateProperty, edate As ExDateProperty

        er?.Clear()
        ed?.Clear()
        er?.CloneRange(rRules)

        ' Convert the RDates to ExDate format
        For Each rdate In rDates
            edate = New ExDateProperty With {
                .ValueLocation = rdate.ValueLocation,
                .TimeZoneId = rdate.TimeZoneId,
                .TimeZoneDateTime = rdate.TimeZoneDateTime
            }

            ed?.Add(edate)
        Next
    End Sub

    ''' <summary>
    ''' This is called by the containing form to apply a new time zone to the RDATE/EXDATE property date/time
    ''' values in the control.
    ''' </summary>
    ''' <param name="oldTZ">The old time zone's ID</param>
    ''' <param name="newTZ">The new time zone's ID</param>
    Public Sub ApplyTimeZone(oldTZ As String, newTZ As String)
        Dim dti As DateTimeInstance, rdt As RDateProperty
        Dim idx As Integer = 0

        ' This only applies to RDATE/EXDATE properties with a time
        For Each rdt In rDates
            If rdt.ValueLocation = ValLocValue.DateTime Then
                If oldTZ Is Nothing
                    dti = VCalendar.LocalTimeToTimeZoneTime(rdt.TimeZoneDateTime, newTZ)
                Else
                    dti = VCalendar.TimeZoneToTimeZone(rdt.TimeZoneDateTime, oldTZ, newTZ)
                End If

                rdt.TimeZoneDateTime = dti.StartDateTime
                lbRDates.Items(idx) = dti.StartDateTime.ToString("G", CultureInfo.CurrentCulture)
            End If

            idx += 1
        Next
    End Sub

    ''' <summary>
    ''' Add a recurrence rule
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event parameters</param>
    Private Sub btnAddRRule_Click(sender As Object, e As System.EventArgs) _
      Handles btnAddRRule.Click
        Using dlg As New RecurrencePropertiesDlg()
            If dlg.ShowDialog() = DialogResult.OK Then
                ' Add the recurrence rule to the collection and the list box using the appropriate type
                If ctrlEditsExceptions = False Then
                    Dim rr As New RRuleProperty()
                    dlg.GetRecurrence(rr.Recurrence)
                    rRules.Add(rr)
                    lbRRules.Items.Add(rr.Recurrence.ToString())
                Else
                    Dim er As New ExRuleProperty()
                    dlg.GetRecurrence(er.Recurrence)
                    rRules.Add(er)
                    lbRRules.Items.Add(er.Recurrence.ToString())
                End If
            End If

            Me.SetButtonStates()
        End Using
    End Sub

    ''' <summary>
    ''' Edit a recurrence rule
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event parameters</param>
    Private Sub btnEditRRule_Click(sender As Object, e As System.EventArgs) _
      Handles btnEditRRule.Click
        If lbRRules.SelectedIndex = -1 Then
            MessageBox.Show("Select a recurrence rule to edit")
            Return
        End If

        Using dlg As New RecurrencePropertiesDlg()
            Try
                Dim r As Recurrence = rRules(lbRRules.SelectedIndex).Recurrence
                dlg.SetRecurrence(r)

                If dlg.ShowDialog() = DialogResult.OK Then
                    ' Update the recurrence rule in the collection and the list box
                    dlg.GetRecurrence(r)
                    lbRRules.Items(lbRRules.SelectedIndex) = r.ToString()
                End If

            Catch ex As Exception
                MessageBox.Show(ex.ToString())
            End Try
        End Using
    End Sub

    ''' <summary>
    ''' Remove a recurrence rule
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event parameters</param>
    Private Sub btnRemoveRRule_Click(sender As Object, e As System.EventArgs) _
      Handles btnRemoveRRule.Click
        If lbRRules.SelectedIndex = -1 Then
            MessageBox.Show("Select a recurrence rule to remove")
            Return
        End If

        ' Remove the recurrence rule from the collection and the list box
        rRules.RemoveAt(lbRRules.SelectedIndex)
        lbRRules.Items.RemoveAt(lbRRules.SelectedIndex)

        Me.SetButtonStates()
    End Sub

    ''' <summary>
    ''' Clear all recurrence rules
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event parameters</param>
    Private Sub btnClearRRules_Click(sender As Object, e As System.EventArgs) _
      Handles btnClearRRules.Click
        rRules.Clear()
        lbRRules.Items.Clear()
        Me.SetButtonStates()
    End Sub

    ''' <summary>
    ''' Add a recurrence date
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event parameters</param>
    Private Sub btnAddRDate_Click(sender As Object, e As System.EventArgs) _
      Handles btnAddRDate.Click
        ' Add a recurrence date to the collection and the list box.  If "Exclude whole day" is checked, the time
        ' part is omitted.
        If chkExcludeDay.Checked = True Then
            Dim rdate As New RDateProperty With {
                .ValueLocation = ValLocValue.Date,
                .TimeZoneDateTime = dtpRDate.Value.Date
            }

            rDates.Add(rdate)
            lbRDates.Items.Add(dtpRDate.Value.Date.ToString("d", CultureInfo.CurrentCulture))
        Else
            Dim rdate As New RDateProperty With {
                .TimeZoneDateTime = dtpRDate.Value
            }

            rDates.Add(rdate)
            lbRDates.Items.Add(dtpRDate.Value.ToString("G", CultureInfo.CurrentCulture))
        End If

        Me.SetButtonStates()
    End Sub

    ''' <summary>
    ''' Remove a recurrence date
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event parameters</param>
    Private Sub btnRemoveRDate_Click(sender As Object, e As System.EventArgs) _
      Handles btnRemoveRDate.Click
        If lbRDates.SelectedIndex = -1 Then
            MessageBox.Show("Select a recurrence date to remove")
            Return
        End If

        ' Remove a recurrence date from the collection and the list box
        rDates.RemoveAt(lbRDates.SelectedIndex)
        lbRDates.Items.RemoveAt(lbRDates.SelectedIndex)
        Me.SetButtonStates()
    End Sub

    ''' <summary>
    ''' Clear all recurrence dates
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event parameters</param>
    Private Sub btnClearRDates_Click(sender As Object, e As System.EventArgs) _
      Handles btnClearRDates.Click
        ' Remove all recurrence dates from the collection and the list box
        rDates.Clear()
        lbRDates.Items.Clear()
        Me.SetButtonStates()
    End Sub

End Class
