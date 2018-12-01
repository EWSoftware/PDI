'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : TimeZoneListDlg.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 11/25/2018
' Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
' Compiler: Microsoft VB.NET
'
' This is used to edit view and edit time zones and apply them to the calendar
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 01/07/2004  EFW  Created the code
' 05/21/2007  EFW  Converted for use with .NET 2.0
'================================================================================================================

Imports System.Globalization

Imports EWSoftware.PDI
Imports EWSoftware.PDI.Objects

''' <summary>
''' This form is used for editing time zone information
''' </summary>
Public Partial Class TimeZoneListDlg
    Inherits System.Windows.Forms.Form

    Private timeZones As VTimeZoneCollection

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        dgvCalendar.AutoGenerateColumns = False
        tbcLastModified.DefaultCellStyle.Format = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern &
            " " & CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern

        Me.CurrentCalendar = Nothing
        timeZones = New VTimeZoneCollection()
        LoadGridWithItems()
    End Sub

    ''' <summary>
    ''' Get or set the currently loaded calendar
    ''' </summary>
    Public Property CurrentCalendar As VCalendar

    ''' <summary>
    ''' Set or get the modified state
    ''' </summary>
    Public Property Modified As Boolean

    ''' <summary>
    ''' Load the grid with the specified calendar items
    ''' </summary>
    Private Sub LoadGridWithItems()
        Dim gridIdx As Integer = dgvCalendar.CurrentCellAddress.Y
        Dim idx As Integer

        VCalendar.TimeZones.Sort(True)
        dgvCalendar.DataSource = Nothing

        ' Get just the time zones used?
        If chkLimitToCalendar.Checked = True Then
            ' Get just the time zones used
            Dim timeZonesUsed As New StringCollection()

            If Me.CurrentCalendar IsNot Nothing Then
                Me.CurrentCalendar.TimeZonesUsed(timeZonesUsed)

                ' Remove entries that don't exist
                idx = 0
                Do While idx <= timeZonesUsed.Count - 1
                    If VCalendar.TimeZones(timeZonesUsed(idx)) Is Nothing Then
                        timeZonesUsed.RemoveAt(idx)
                    Else
                        idx += 1
                    End If
                Loop
            End If

            ' Add each instance to a temporary collection and bind it to the grid
            timeZones.Clear()

            For Each timeZoneId As String In timeZonesUsed
                timeZones.Add(VCalendar.TimeZones(timeZoneId))
            Next

            dgvCalendar.DataSource = timeZones
        Else
            dgvCalendar.DataSource = VCalendar.TimeZones
        End If

        ' Enable or disable the buttons based on the vCard count
        btnEdit.Enabled = (dgvCalendar.RowCount <> 0)
        btnDelete.Enabled = (dgvCalendar.RowCount <> 0)

        ' Stay on the last item selected
        If gridIdx > -1 AndAlso gridIdx < dgvCalendar.RowCount Then
            dgvCalendar.CurrentCell = dgvCalendar(0, gridIdx)
        End If
    End Sub

    ''' <summary>
    ''' Close the dialog box
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnClose_Click(sender As Object, e As System.EventArgs) _
      Handles btnClose.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' Toggle the display of all time zones or just those in the currently loaded calendar
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub chkLimitToCalendar_CheckedChanged(sender As Object, _
      e As System.EventArgs) Handles chkLimitToCalendar.CheckedChanged
        LoadGridWithItems()
    End Sub

    ''' <summary>
    ''' Add a new time zone component
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnAdd_Click(sender As Object, e As System.EventArgs) _
      Handles btnAdd.Click
        Using dlg As New VTimeZoneDlg()
            If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Dim tz As New VTimeZone()
                dlg.GetValues(tz)

                VCalendar.TimeZones.Add(tz)
                Me.Modified = True
                LoadGridWithItems()
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Edit a time zone component
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnEdit_Click(sender As Object, e As System.EventArgs) _
      Handles btnEdit.Click
        If dgvCalendar.CurrentCellAddress.Y = -1 Then
            MessageBox.Show("Please select an item to edit", "No Item", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)
            Return
        End If

        Using dlg As New VTimeZoneDlg()
            Dim timeZoneId As String = CType(dgvCalendar(0, dgvCalendar.CurrentCellAddress.Y).Value, String)
            dlg.SetValues(VCalendar.TimeZones(timeZoneId))

            If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                dlg.GetValues(VCalendar.TimeZones(timeZoneId))
                Me.Modified = True
                LoadGridWithItems()
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Delete a time zone component
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnDelete_Click(sender As Object, e As System.EventArgs) _
      Handles btnDelete.Click
        If dgvCalendar.CurrentCellAddress.Y = -1 Then
            MessageBox.Show("Please select an item to delete", "No Item", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)
            Return
        End If

        Dim timeZonesUsed As New StringCollection()
        Dim timeZoneId As String = CType(dgvCalendar(0, dgvCalendar.CurrentCellAddress.Y).Value, String)

        If Not (Me.CurrentCalendar Is Nothing) Then
            Me.CurrentCalendar.TimeZonesUsed(timeZonesUsed)
        End If

        If timeZonesUsed.Contains(timeZoneId) Then
            MessageBox.Show("The time zone is in use and cannot be deleted", "Time Zone In Use",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If MessageBox.Show("Are you sure you want to delete the selected item?", "Delete Item",
          MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) =
          System.Windows.Forms.DialogResult.Yes Then
            VCalendar.TimeZones.Remove(VCalendar.TimeZones(timeZoneId))
            Me.Modified = True
            LoadGridWithItems()
        End If
    End Sub

    ''' <summary>
    ''' Apply the selected time zone to all items in the calendar.  This will convert all date/time values in the
    ''' calendar from their current time zone to the selected time zone.
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnApply_Click(sender As Object, e As System.EventArgs) _
      Handles btnApply.Click
        If dgvCalendar.CurrentCellAddress.Y = -1 Then
            MessageBox.Show("Please select an item to use", "No Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If MessageBox.Show("Are you sure you want to apply the selected time zone to all items in the calendar?",
          "Apply Time Zone", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) =
          System.Windows.Forms.DialogResult.Yes Then
            Dim timeZoneId As String = CType(dgvCalendar(0, dgvCalendar.CurrentCellAddress.Y).Value, String)
            Me.CurrentCalendar.ApplyTimeZone(VCalendar.TimeZones(timeZoneId))
            Me.Modified = True
        End If
    End Sub
End Class
