'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : HolidayTestForm.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 11/22/2021
' Note    : Copyright 2004-2021, Eric Woodruff, All rights reserved
'
' This is used to test the various Holiday classes and the DateUtils class
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 08/19/2003  EFW  Created the code
'================================================================================================================

Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq

Imports EWSoftware.PDI

''' <summary>
''' This form is used to test the various Holiday classes and the DateUtils class
''' </summary>
Public Partial Class HolidayTestForm
    Inherits System.Windows.Forms.Form

    ''' <summary>
	''' Constructor
	''' </summary>
    Public Sub New()
        MyBase.New()

        InitializeComponent()
        dgvDatesFound.AutoGenerateColumns = False
        tbcDate.DefaultCellStyle.Format = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern

        Dim hols As Holiday() = { New FixedHoliday(6, 19, True, "Juneteenth") With { .MinimumYear = 2021 } }

        hmHolidays.Defaults = hmHolidays.Defaults.Concat(hols).OrderBy(Function(h) h.Month).ToList()

        ' Use the standard set of holidays by default.  The holiday manager control will make a copy of the
        ' collection and will take care of adding, editing, and deleting entries from it.  If an existing
        ' collection is passed, it won't be modified.
        hmHolidays.Holidays = hmHolidays.Defaults

        udcFromYear.Value = DateTime.Now.Year - 1
        udcToYear.Value = udcFromYear.Value + 6
        dtpTestDate.Value = DateTime.Today.Date
    End Sub

    ''' <summary>
    ''' Test the entered date to see if it is a holiday as defined by the entries in the holiday manager's
    ''' holiday collection.
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnTestDate_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles btnTestDate.Click
        Dim hcHols As HolidayCollection = New HolidayCollection(hmHolidays.Holidays)

        If hcHols.IsHoliday(dtpTestDate.Value) = True Then
            MessageBox.Show("The test date is a holiday", "Found!", MessageBoxButtons.OK,
                MessageBoxIcon.Information)
        Else
            MessageBox.Show("The test date is not a holiday", "Not Found!", MessageBoxButtons.OK,
                MessageBoxIcon.Information)
        End If
    End Sub

    ''' <summary>
    ''' Find all holidays in the given range of years using the entries defined in the holiday manager's holiday
    ''' collection.  The found dates and descriptions are loaded into the grid view.
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnFind_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles btnFind.Click
        Dim holidays As HolidayCollection = New HolidayCollection(hmHolidays.Holidays)
        Dim fromYear, toYear, year As Integer

        fromYear = CType(udcFromYear.Value, Integer)
        toYear = CType(udcToYear.Value, Integer)

        If fromYear > toYear Then
            year = fromYear
            fromYear = toYear
            toYear = year
            udcFromYear.Value = fromYear
            udcToYear.Value = toYear
        End If

        Me.Cursor = Cursors.WaitCursor

        dgvDatesFound.DataSource = holidays.HolidaysBetween(fromYear, toYear).OrderBy(Function(d) d).Select(
            Function(d) New ListItem(d, holidays.HolidayDescription(d))).ToList()

        Me.Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Load the data grid with all Easter Sunday dates for the specified range of years
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnEaster_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles btnEaster.Click
        Dim fromYear, toYear, year As Integer
        Dim desc As String

        Dim em As EasterMethod = EasterMethod.Gregorian

        fromYear = CType(udcFromYear.Value, Integer)
        toYear = CType(udcToYear.Value, Integer)

        If rbJulian.Checked = True Then
            em = EasterMethod.Julian
        Else
            If rbOrthodox.Checked = True Then
                em = EasterMethod.Orthodox
            End If
        End If

        ' Adjust years as necessary based on the method
        If em <> EasterMethod.Julian Then
            If fromYear < 1583 Then fromYear = 1583
            If toYear < 1583 Then toYear = 1583

            If fromYear > 4099 Then fromYear = 4099
            If toYear > 4099 Then toYear = 4099
        Else
            If fromYear < 326 Then fromYear = 326
            If toYear < 326 Then toYear = 326
        End If

        If fromYear > toYear Then
            year = fromYear
            fromYear = toYear
            toYear = year
        End If

        udcFromYear.Value = fromYear
        udcToYear.Value = toYear

        Me.Cursor = Cursors.WaitCursor

        ' Create the grid view's data source
        Dim items As New List(Of ListItem)()
        desc = String.Format("Easter ({0})", em.ToString())

        Do While fromYear <= toYear
            items.Add(New ListItem(DateUtils.EasterSunday(fromYear, em), desc))
            fromYear += 1
        Loop

        dgvDatesFound.DataSource = items

        Me.Cursor = Cursors.Default
    End Sub
End Class
