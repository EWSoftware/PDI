'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : RRuleTestForm.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 11/25/2018
' Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
' Compiler: Visual Basic .NET
'
' This is a simple demonstration used to test the recurrence engine which encapsulates the iCalendar 2.0 RRULE
' feature set.  It is separate from the other PDI calendar classes so that you can use the recurrence engine
' without the extra overhead of the calendar classes if you do not need it.
'
' Although this demo uses the parsing abilities of the class to set the recurrence options, you could just as
' easily set them in code as demonstrated in the PDIDatesTest demo program.
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
Imports EWSoftware.PDI.Windows.Forms

''' <summary>
''' This form is used to test the recurrence class and the recurrence editor dialog box
''' </summary>
Public Partial Class RRuleTestForm
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
    End Sub

    ''' <summary>
    ''' Test the recurrence class with the entered pattern
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnTest_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles btnTest.Click
        Dim dc As DateTimeCollection
        Dim start, count As Integer
        Dim elapsed As Double

        Try
            lblCount.Text = String.Empty

            ' Define the recurrence rule by parsing the text
            Dim r As New Recurrence(txtRRULE.Text) With { .StartDateTime = dtpStartDate.Value }

            ' Get the currently defined set of holidays if necessary
            If r.CanOccurOnHoliday = False Then
                Recurrence.Holidays.Clear()
                Recurrence.Holidays.AddRange(hmHolidays.Holidays)
            End If

            ' For hourly, minutely, and secondly, warn the user if there is no end date or max occurrences
            If r.Frequency >= RecurFrequency.Hourly And r.MaximumOccurrences = 0 And
              r.RecurUntil > r.StartDateTime.AddDays(1000) Then
                If MessageBox.Show("This recurrence may run for a very long time.  Continue?", "Confirm",
                  MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                    Return
                End If
            End If

            lbDates.DataSource = Nothing
            Me.Cursor = Cursors.WaitCursor

            start = System.Environment.TickCount
            dc = r.InstancesBetween(r.StartDateTime, DateTime.MaxValue)
            elapsed = (System.Environment.TickCount - start) / 1000.0
            count = dc.Count

            If count > 5000 Then
                dc.RemoveRange(5000, count - 5000)
                lblCount.Text &= "A large number of dates were returned.  Only the first 5000 have been " &
                    "loaded into the list box." & Environment.NewLine
            End If

            ' DateTimeCollection is bindable so we can assign it as the list box's data source
            lbDates.DataSource = dc
            lblCount.Text &= String.Format("Found {0:N0} instances in {1:N2} seconds ({2:N2} instances/second)",
                count, elapsed, count / elapsed)

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    ''' <summary>
    ''' This loads the standard holiday set on form load
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub RRuleTestForm_Load(ByVal sender As Object, _
      ByVal e As System.EventArgs) Handles MyBase.Load
        dtpStartDate.Value = New DateTime(DateTime.Today.Year, 1, 1)

        ' Use standard set of holidays by default.  The holiday manager control will make a copy of the
        ' collection and will take care of adding, editing, and deleting entries from it.  If an existing
        ' collection is passed, it won't be modified.
        Recurrence.Holidays.Clear()
        Recurrence.Holidays.AddStandardHolidays(New FixedHoliday(6, 19, True, "Juneteenth") With { .MinimumYear = 2021 })
        hmHolidays.Defaults = Recurrence.Holidays
        hmHolidays.Holidays = Recurrence.Holidays

    End Sub

    ''' <summary>
    ''' Clear the recurrence holiday collection so that it doesn't inadvertently affect the other test forms
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub RRuleTestForm_Closed(ByVal sender As Object, _
      ByVal e As System.EventArgs) Handles MyBase.Closed
        ' Clear the recurrence holiday collection so that it doesn't inadvertently affect the other test forms
        Recurrence.Holidays.Clear()
    End Sub

    ''' <summary>
    ''' Edit the recurrence pattern using the recurrence editor dialog box
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnDesign_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles btnDesign.Click
        Using dlg As New RecurrencePropertiesDlg()
            Try
                Dim r As New Recurrence(txtRRULE.Text) With { .StartDateTime = dtpStartDate.Value }

                dlg.SetRecurrence(r)

                If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    dlg.GetRecurrence(r)
                    txtRRULE.Text = r.ToString()
                End If

            Catch ex As Exception
                MessageBox.Show(ex.ToString())
            End Try
        End Using
    End Sub

    ''' <summary>
    ''' Describe the recurrence rule by using the ToDescription method
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnDescribe_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles btnDescribe.Click
        Try
            Dim r As New Recurrence(txtRRULE.Text)
            MessageBox.Show(r.ToDescription(), "Recurrence Description")
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

End Class
