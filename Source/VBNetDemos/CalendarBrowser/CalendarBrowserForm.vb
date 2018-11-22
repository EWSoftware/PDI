'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : CalendarBrowserForm.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/05/2015
' Note    : Copyright 2004-2015, Eric Woodruff, All rights reserved
' Compiler: Microsoft VB.NET
'
' This is a simple demonstration application that shows how to load, save, and manage vCalendar and iCalendar
' files including how to edit the properties on the various components.
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 11/16/2004  EFW  Created the code
' 05/21/2007  EFW  Converted for use with .NET 2.0
'================================================================================================================

Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports Microsoft.VisualBasic

Imports EWSoftware.PDI
Imports EWSoftware.PDI.Objects
Imports EWSoftware.PDI.Parser
Imports EWSoftware.PDI.Properties

''' <summary>
''' This application demonstrates loading, saving, and managing a vCalendar or iCalendar file including how to
''' edit the properties on the various components.
''' </summary>
Public Partial Class CalendarBrowserForm
    Inherits System.Windows.Forms.Form

    Private vCal As VCalendar       ' The calendar being browsed
    Private wasModified As Boolean
    Private sf As StringFormat

    ''' <summary>
    ''' The main entry point for the application
    ''' </summary>
    Shared Sub Main(Args As String())
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(false)
        Application.Run(new CalendarBrowserForm())
    End Sub

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New()
        MyBase.New()

        InitializeComponent()

        ' The string format to use when drawing the status text
        sf = New StringFormat(StringFormatFlags.NoWrap)
        sf.Alignment = StringAlignment.Near
        sf.LineAlignment = StringAlignment.Center
        sf.Trimming = StringTrimming.EllipsisCharacter

        dgvCalendar.AutoGenerateColumns = False

        ' Load the time zone collection with info from the registry
        TimeZoneRegInfo.LoadTimeZoneInfo()

        vCal = New VCalendar()

        LoadComponentList()
        LoadGridWithItems(True)
    End Sub

    ''' <summary>
    ''' Load the View combo box with a valid list of components based on the calendar's version
    ''' </summary>
    Private Sub LoadComponentList()
        cboComponents.Items.Clear()
        cboComponents.Items.Add("Events")
        cboComponents.Items.Add("To-Do Items")

        If vCal.Version = SpecificationVersions.iCalendar20 Then
            cboComponents.Items.Add("Journals")
            cboComponents.Items.Add("Free/Busy Items")
            btnChgTimeZone.Enabled = True
        Else
            btnChgTimeZone.Enabled = False
        End If

        cboComponents.SelectedIndex = 0
    End Sub

    ''' <summary>
    ''' Load the grid with the specified calendar items
    ''' </summary>
    ''' <param name="connectEvents">True to connect list change event, false to not connect it</param>
    Private Sub LoadGridWithItems(connectEvents As Boolean)
        Dim modFlag As Boolean = wasModified
        DIm gridIdx As Integer = dgvCalendar.CurrentCellAddress.Y

        ' The ListChanged event is connected so that we are notified when the lists are modified
        If connectEvents Then
            AddHandler vCal.Events.ListChanged, New ListChangedEventHandler(AddressOf Calendar_ListChanged)
            AddHandler vCal.ToDos.ListChanged, New ListChangedEventHandler(AddressOf Calendar_ListChanged)
            AddHandler vCal.Journals.ListChanged, New ListChangedEventHandler(AddressOf Calendar_ListChanged)
            AddHandler vCal.FreeBusys.ListChanged, New ListChangedEventHandler(AddressOf Calendar_ListChanged)
        End If

        ' All items are sorted in ascending order before adding them to the grid.
        Select Case cboComponents.SelectedIndex
            Case 0
                vCal.Events.Sort(AddressOf CalendarSorter)
                dgvCalendar.DataSource = vCal.Events

            Case 1
                vCal.ToDos.Sort(AddressOf CalendarSorter)
                dgvCalendar.DataSource = vCal.ToDos

            Case 2
                vCal.Journals.Sort(AddressOf CalendarSorter)
                dgvCalendar.DataSource = vCal.Journals

            Case 3
                vCal.FreeBusys.Sort(AddressOf CalendarSorter)
                dgvCalendar.DataSource = vCal.FreeBusys

        End Select

        ' Enable or disable the buttons based on the vCard count
        miClear.Enabled = (dgvCalendar.RowCount <> 0)
        btnEdit.Enabled = (dgvCalendar.RowCount <> 0)
        btnDelete.Enabled = (dgvCalendar.RowCount <> 0)

        ' Stay on the last item selected
        If gridIdx > -1 AndAlso gridIdx < dgvCalendar.RowCount Then
            dgvCalendar.CurrentCell = dgvCalendar(0, gridIdx)
        End If

        If connectEvents Then
            wasModified = False     ' New collection
        Else
            If Not modFlag Then
                wasModified = False     ' Sorting changes it so restore it
            End If
        End If
    End Sub

    ''' <summary>
    ''' This sets the modified flag when the collection is edited and adjusts the button enabled states
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub Calendar_ListChanged(sender As Object, e As ListChangedEventArgs)
        Dim count As Integer

        Select Case cboComponents.SelectedIndex
            Case 0
                count = vCal.Events.Count

            Case 1
                count = vCal.ToDos.Count

            Case 2
                count = vCal.Journals.Count

            Case Else
                count = vCal.FreeBusys.Count

        End Select

        miClear.Enabled = (count <> 0)
        btnEdit.Enabled = (count <> 0)
        btnDelete.Enabled = (count <> 0)
        wasModified = True
    End Sub

    ''' <summary>
    ''' Prompt to save if the collection has been modified
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub CalendarBrowserForm_Closing(sender As Object, _
      e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If wasModified = True AndAlso MessageBox.Show("Do you want to discard your changes to the current calendar?",
          "Discard Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) =
          System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End if
    End Sub

    ''' <summary>
    ''' Open a vCalendar or iCalendar file
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub miOpen_Click(sender As Object, e As System.EventArgs) _
      Handles miOpen.Click
        If wasModified = True AndAlso MessageBox.Show("Do you want to discard your changes to the current calendar?",
            "Discard Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) =
            System.Windows.Forms.DialogResult.No Then
            Return
        End If

        Using dlg As New OpenFileDialog()
            dlg.Title = "Load Calendar File"
            dlg.Filter = "ICS files (*.ics)|*.ics|VCS files (*.vcs)|*.vcs|All files (*.*)|*.*"

            If vCal.Version = SpecificationVersions.vCalendar10 Then
                dlg.DefaultExt = "vcs"
                dlg.FilterIndex = 2
            Else
                dlg.DefaultExt = "ics"
                dlg.FilterIndex = 1
            End If

            dlg.InitialDirectory = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..\..\..\..\PDIFiles"))

            If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Try
                    Me.Cursor = Cursors.WaitCursor

                    ' Parse the calendar information from the file and load the data grid with some basic
                    ' information about the items in it.
                    vCal.Dispose()
                    vCal = VCalendarParser.ParseFromFile(dlg.FileName)

                    LoadComponentList()

                    ' Find the first collection with items
                    If vCal.Events.Count <> 0 Then
                        cboComponents.SelectedIndex = 0
                    Else
                        If vCal.ToDos.Count <> 0 Then
                            cboComponents.SelectedIndex = 1
                        Else
                            If vCal.Journals.Count <> 0 Then
                                cboComponents.SelectedIndex = 2
                            Else
                                If vCal.FreeBusys.Count <> 0 Then
                                    cboComponents.SelectedIndex = 3
                                Else
                                    cboComponents.SelectedIndex = 0
                                End If
                            End If
                        End If
                    End If

                    LoadGridWithItems(True)
                    lblFilename.Text = dlg.FileName

                Catch ex As Exception
                    Dim errorMsg As String = String.Format("Unable to load calendar:{0}{1}", Environment.NewLine,
                        ex.Message)

                    If Not (ex.InnerException Is Nothing) Then
                        errorMsg &= ex.InnerException.Message & Environment.NewLine

                        If Not (ex.InnerException.InnerException Is Nothing) Then
                            errorMsg &= ex.InnerException.InnerException.Message
                        End If
                    End If

                    System.Diagnostics.Debug.Write(ex)
                    MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    Me.Cursor = Cursors.Default
                End Try
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Save a calendar file
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub miSave_Click(sender As Object, e As System.EventArgs) _
      Handles miSave.Click
        Using dlg As New SaveFileDialog()
            dlg.Title = "Save Calendar File"
            dlg.Filter = "ICS files (*.ics)|*.ics|VCS files (*.vcs)|*.vcs|All files (*.*)|*.*"

            If vCal.Version = SpecificationVersions.vCalendar10 Then
                dlg.DefaultExt = "vcs"
                dlg.FilterIndex = 2
            Else
                dlg.DefaultExt = "ics"
                dlg.FilterIndex = 1
            End If

            dlg.InitialDirectory = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..\..\..\..\PDIFiles"))
            dlg.FileName = lblFilename.Text

            If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Try
                    Me.Cursor = Cursors.WaitCursor

                    ' Open the file and write the calendar to it.  We'll use the same encoding method used by the
                    ' parser.
                    Using sw As New StreamWriter(dlg.FileName, False, PDIParser.DefaultEncoding)
                        vCal.WriteToStream(sw)
                    End Using

                    wasModified = False
                    lblFilename.Text = dlg.FileName

                Catch ex As Exception
                    Dim errorMsg As String = String.Format("Unable to save calendar:{0}{1}", Environment.NewLine,
                        ex.Message)

                    If Not (ex.InnerException Is Nothing) Then
                        errorMsg &= ex.InnerException.Message & Environment.NewLine

                        If Not (ex.InnerException.InnerException Is Nothing) Then
                            errorMsg += ex.InnerException.InnerException.Message
                        End If
                    End If

                    System.Diagnostics.Debug.Write(ex)
                    MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    Me.Cursor = Cursors.Default
                End Try
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Clear all loaded calendar information
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub miClear_Click(sender As Object, e As System.EventArgs) _
      Handles miClear.Click
        If MessageBox.Show("Are you sure you want to remove all calendar information?", "Clear calendar",
          MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) =
          System.Windows.Forms.DialogResult.Yes Then
            vCal.ClearProperties()
            LoadGridWithItems(True)
            wasModified = True
        End If
    End Sub

    ''' <summary>
    ''' Show the About dialog box
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub miAbout_Click(sender As Object, e As System.EventArgs) _
      Handles miAbout.Click
        Using dlg As New AboutDlg()
            dlg.ShowDialog()
        End Using
    End Sub

    ''' <summary>
    ''' Close the application
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub miExit_Click(sender As Object, e As System.EventArgs) _
      Handles miExit.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' Add a calendar item
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnAdd_Click(sender As Object, e As System.EventArgs) _
      Handles btnAdd.Click

        Select Case cboComponents.SelectedIndex
            Case 0
                Using calDlg As New CalendarObjectDlg()
                    Dim evt As New VEvent()
                    evt.UniqueId.AssignNewId(True)
                    evt.DateCreated.TimeZoneDateTime = DateTime.Now
                    evt.LastModified.TimeZoneDateTime = evt.DateCreated.TimeZoneDateTime
                    calDlg.SetValues(evt)

                    If calDlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        calDlg.GetValues(evt)

                        ' Create a unique ID for the new item
                        evt.UniqueId.AssignNewId(True)

                        vCal.Events.Add(evt)
                    End If
                End Using

            Case 1
                Using calDlg As New CalendarObjectDlg()
                    Dim td As New VToDo()
                    td.DateCreated.TimeZoneDateTime = DateTime.Now
                    td.LastModified.TimeZoneDateTime = td.DateCreated.TimeZoneDateTime
                    calDlg.SetValues(td)

                    If calDlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        calDlg.GetValues(td)

                        ' Create a unique ID for the new item
                        td.UniqueId.AssignNewId(True)

                        vCal.ToDos.Add(td)
                    End if
                End Using

            Case 2
                Using calDlg As New CalendarObjectDlg()
                    Dim j As New VJournal()
                    j.DateCreated.TimeZoneDateTime = DateTime.Now
                    j.LastModified.TimeZoneDateTime = j.DateCreated.TimeZoneDateTime
                    calDlg.SetValues(j)

                    If calDlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        calDlg.GetValues(j)

                        ' Create a unique ID for the new item
                        j.UniqueId.AssignNewId(True)

                        vCal.Journals.Add(j)
                    End If
                End Using

            Case 3
                Using fbDlg As New VFreeBusyDlg()
                    If fbDlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        Dim fb As New VFreeBusy()
                        fbDlg.GetValues(fb)

                        ' Create a unique ID for the new item
                        fb.UniqueId.AssignNewId(True)

                        vCal.FreeBusys.Add(fb)
                    End If
                End Using

        End Select
    End Sub

    ''' <summary>
    ''' Edit a calendar item
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnEdit_Click(sender As Object, e As System.EventArgs) _
      Handles btnEdit.Click
        If dgvCalendar.CurrentCellAddress.Y = -1 Then
            MessageBox.Show("Please select an item to edit", "No Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        Select Case cboComponents.SelectedIndex
            Case 0
                Using calDlg As New CalendarObjectDlg()
                    calDlg.SetValues(vCal.Events(dgvCalendar.CurrentCellAddress.Y))

                    If calDlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        calDlg.GetValues(vCal.Events(dgvCalendar.CurrentCellAddress.Y))

                        wasModified = True
                    End If
                End Using

            Case 1
                Using calDlg As New CalendarObjectDlg()
                    calDlg.SetValues(vCal.ToDos(dgvCalendar.CurrentCellAddress.Y))

                    If calDlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        calDlg.GetValues(vCal.ToDos(dgvCalendar.CurrentCellAddress.Y))

                        wasModified = True
                    End If
                End Using

            Case 2
                Using calDlg As New CalendarObjectDlg()
                    calDlg.SetValues(vCal.Journals(dgvCalendar.CurrentCellAddress.Y))

                    If calDlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        calDlg.GetValues(vCal.Journals(dgvCalendar.CurrentCellAddress.Y))

                        wasModified = True
                    End If
                End Using

            Case 3
                Using fbDlg As New VFreeBusyDlg()
                    fbDlg.SetValues(vCal.FreeBusys(dgvCalendar.CurrentCellAddress.Y))

                    If fbDlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        fbDlg.GetValues(vCal.FreeBusys(dgvCalendar.CurrentCellAddress.Y))

                        wasModified = True
                    End If
                End Using

            Case Else
                Using tzDlg As New VTimeZoneDlg()
                    tzDlg.SetValues(VCalendar.TimeZones(dgvCalendar.CurrentCellAddress.Y))

                    If tzDlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        tzDlg.GetValues(VCalendar.TimeZones(dgvCalendar.CurrentCellAddress.Y))

                        wasModified = True
                    End If
                End Using

        End Select
    End Sub

    ''' <summary>
    ''' Delete a calendar item
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnDelete_Click(sender As Object, e As System.EventArgs) _
      Handles btnDelete.Click
        If dgvCalendar.CurrentCellAddress.Y = -1 Then
            MessageBox.Show("Please select an item to delete", "No Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If MessageBox.Show("Are you sure you want to delete the selected item?", "Delete Item",
          MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) =
          System.Windows.Forms.DialogResult.No Then
            Return
        End If

        Select Case cboComponents.SelectedIndex
            Case 0
                vCal.Events.RemoveAt(dgvCalendar.CurrentCellAddress.Y)

            Case 1
                vCal.ToDos.RemoveAt(dgvCalendar.CurrentCellAddress.Y)

            Case 2
                vCal.Journals.RemoveAt(dgvCalendar.CurrentCellAddress.Y)

            Case 3
                vCal.FreeBusys.RemoveAt(dgvCalendar.CurrentCellAddress.Y)

            Case Else
                VCalendar.TimeZones.RemoveAt(dgvCalendar.CurrentCellAddress.Y)

        End Select
    End Sub

    ''' <summary>
    ''' Change the specification version of the calendar
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    ''' <remarks>Downgrading to Version 1.0 will cause the loss of any Version 2.0 properties if the file is
    ''' saved and reloaded.</remarks>
    Private Sub btnChgVersion_Click(sender As Object, e As System.EventArgs) _
      Handles btnChgVersion.Click
        If vCal.Version = SpecificationVersions.iCalendar20 Then
            If MessageBox.Show("Are you sure you want to downgrade to vCalendar 1.0?  iCalendar 2.0 properties " &
              "will be lost when the file is saved", "Change Calendar Version", MessageBoxButtons.YesNo,
              MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = System.Windows.Forms.DialogResult.Yes Then
                vCal.Version = SpecificationVersions.vCalendar10
            End If
        Else
            If MessageBox.Show("Are you sure you want to upgrade to iCalendar 2.0?  Some vCalendar 1.0 " &
              "properties will be lost when the file is saved.", "Change Calendar Version", MessageBoxButtons.YesNo,
              MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = System.Windows.Forms.DialogResult.Yes Then
                vCal.Version = SpecificationVersions.iCalendar20
            End if
        End If

        LoadComponentList()
        LoadGridWithItems(False)
        wasModified = True
    End Sub

    ''' <summary>
    ''' Load the grid with the selected items
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub cboComponents_SelectedIndexChanged(sender As Object, _
      e As System.EventArgs) Handles cboComponents.SelectedIndexChanged
        Select cboComponents.SelectedIndex
            Case 0
                tbcSummary.Visible = True
                tbcSummary.HeaderText = "Event Summary"
                tbcOrganizer.Visible = tbcComment.Visible = False

            Case 1
                tbcSummary.Visible = True
                tbcSummary.HeaderText = "To-Do Summary"
                tbcOrganizer.Visible = tbcComment.Visible = False

            Case 2
                tbcSummary.Visible = True
                tbcSummary.HeaderText = "Journal Summary"
                tbcOrganizer.Visible = tbcComment.Visible = False

            Case 3
                tbcSummary.Visible = False
                tbcOrganizer.Visible = tbcComment.Visible = True

        End Select

        LoadGridWithItems(False)
    End Sub

    ''' <summary>
    ''' Edit time zone information
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnChgTimeZone_Click(sender As Object, e As System.EventArgs) _
      Handles btnChgTimeZone.Click
        Using dlg As New TimeZoneListDlg()
            dlg.CurrentCalendar = vCal
            dlg.Modified = wasModified
            dlg.ShowDialog()

            If wasModified <> dlg.Modified
                wasModified = dlg.Modified
                LoadGridWithItems(False)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' This will change the encoding used to read and write PDI files
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub ChangeFileEncoding_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles miFileUnicode.Click, _
      miFileWestEuro.Click, miFileASCII.Click
        If sender Is miFileUnicode Then
            miFileUnicode.Checked = True
            miFileWestEuro.Checked = False
            miFileASCII.Checked = False

            ' UTF-8 encoding
            PDIParser.DefaultEncoding = New UTF8Encoding(False, False)
        Else
            If sender Is miFileWestEuro Then
                miFileUnicode.Checked = False
                miFileWestEuro.Checked = True
                miFileASCII.Checked = False

                ' Western European encoding
                PDIParser.DefaultEncoding = Encoding.GetEncoding("iso-8859-1")
            Else
                miFileUnicode.Checked = False
                miFileWestEuro.Checked = False
                miFileASCII.Checked = True

                ' ASCII encoding
                PDIParser.DefaultEncoding = New ASCIIEncoding()
            End If
        End If
    End Sub

    ''' <summary>
    ''' This will change the encoding used to read and write PDI properties
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub ChangePropertyEncoding_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles miPropUnicode.Click, _
      miPropWestEuro.Click, miPropASCII.Click
        If sender Is miPropUnicode Then
            miPropUnicode.Checked = True
            miPropWestEuro.Checked = False
            miPropASCII.Checked = False

            ' UTF-8 encoding
            BaseProperty.DefaultEncoding = New UTF8Encoding(False, False)
        Else
            If sender Is miPropWestEuro Then
                miPropUnicode.Checked = False
                miPropWestEuro.Checked = True
                miPropASCII.Checked = False

                ' Western European encoding
                BaseProperty.DefaultEncoding = Encoding.GetEncoding("iso-8859-1")
            Else
                miPropUnicode.Checked = False
                miPropWestEuro.Checked = False
                miPropASCII.Checked = True

                ' ASCII encoding
                BaseProperty.DefaultEncoding = New ASCIIEncoding()
            End If
        End If
    End Sub

    ''' <summary>
    ''' This is used to custom draw the start date/time and summary columns based on the information available
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub dgvCalendar_CellPainting(sender As Object, _
      e As DataGridViewCellPaintingEventArgs) Handles dgvCalendar.CellPainting
        Dim cm As CurrencyManager
        Dim startProp As StartDateProperty
        Dim summaryProp As SummaryProperty
        Dim descProp As DescriptionProperty
        Dim dti As DateTimeInstance

        Dim foreColor As Color
        Dim item As Object
        Dim columnText As String = Nothing

        If e.RowIndex > -1 AndAlso (e.ColumnIndex = 0 OrElse _
          e.ColumnIndex = 1) Then
            cm = DirectCast(dgvCalendar.BindingContext(dgvCalendar.DataSource), CurrencyManager)
            item = cm.List(e.RowIndex)

            If e.ColumnIndex = 0 Then
                If TypeOf item is VEvent Then
                    startProp = DirectCast(item, VEvent).StartDateTime
                Else
                    If TypeOf item is VToDo Then
                        startProp = DirectCast(item, VToDo).StartDateTime
                    Else
                        If TypeOf item is VJournal Then
                            startProp = DirectCast(item, VJournal).StartDateTime
                        Else
                            startProp = DirectCast(item, VFreeBusy).StartDateTime
                        End If
                    End If
                End If

                dti = VCalendar.TimeZoneTimeInfo(startProp.TimeZoneDateTime, startProp.TimeZoneId)

                ' Format as date or date/time and include time zone if available
                If startProp.ValueLocation = ValLocValue.Date Then
                    columnText = String.Format("{0:d} {1}", dti.StartDateTime, dti.AbbreviatedStartTimeZoneName)
                Else
                    columnText = String.Format("{0} {1}", dti.StartDateTime, dti.AbbreviatedStartTimeZoneName)
                End If
            Else
                If e.ColumnIndex = 1 Then
                    If TypeOf item is VEvent Then
                        summaryProp = DirectCast(item, VEvent).Summary
                        descProp = DirectCast(item, VEvent).Description
                    Else
                        If TypeOf item is VToDo Then
                            summaryProp = DirectCast(item, VToDo).Summary
                            descProp = DirectCast(item, VToDo).Description
                        Else
                            If TypeOf item is VJournal Then
                                summaryProp = DirectCast(item, VJournal).Summary
                                descProp = DirectCast(item, VJournal).Description
                            Else
                                summaryProp = Nothing
                                descProp = Nothing
                            End If
                        End If
                    End If

                    ' If summary is empty, use description instead
                    If summaryProp IsNot Nothing AndAlso summaryProp.Value IsNot Nothing Then
                        columnText = summaryProp.Value
                    Else
                        If descProp IsNot Nothing Then
                            columnText = descProp.Value
                        End If
                    End If
                End If
            End If

            If columnText IsNot Nothing Then
                ' If multi-line, limit it to the first line
                If columnText.IndexOf(ChrW(10)) <> -1 Then
                    columnText = columnText.Substring(0, columnText.IndexOf(ChrW(10)))
                End If

                e.Paint(e.CellBounds, e.PaintParts And Not DataGridViewPaintParts.ContentForeground)

                ' Based the foreground color on the selected state
                If (e.State And DataGridViewElementStates.Selected) <> 0 Then
                    foreColor = e.CellStyle.SelectionForeColor
                Else
                    foreColor = e.CellStyle.ForeColor
                End If

                Using b As New SolidBrush(foreColor)
                    e.Graphics.DrawString(columnText, e.CellStyle.Font, b, e.CellBounds, sf)
                End Using

                e.Handled = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Invoke edit on cell double click
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub dgvCalendar_CellDoubleClick( sender As Object,  e As DataGridViewCellEventArgs) Handles dgvCalendar.CellDoubleClick
        btnEdit_Click(sender, e)
    End Sub

    ''' <summary>
    ''' This is an example of how you can sort calendar object collections
    ''' </summary>
    ''' <param name="x">The first calendar object</param>
    ''' <param name="y">The second calendar object</param>
    ''' <remarks>Due to the variety of properties in a calendar object, sorting is left up to the developer
    ''' utilizing a comparison delegate.  This example sorts the collection by the start date/time property and,
    ''' if they are equal, the summary description.  This is used to handle all of the collection types.</remarks>
    Private Function CalendarSorter(x As CalendarObject, y As CalendarObject) As Integer
        Dim d1, d2 As DateTime
        Dim summary1, summary2 As String

        If TypeOf x Is VEvent Then
            Dim e1 As VEvent = DirectCast(x, VEvent), e2 As VEvent = DirectCast(y, VEvent)

            d1 = e1.StartDateTime.TimeZoneDateTime
            d2 = e2.StartDateTime.TimeZoneDateTime
            summary1 = e1.Summary.Value
            summary2 = e2.Summary.Value

        ElseIf TypeOf x Is VToDo
            Dim t1 As VToDo = DirectCast(x, VToDo), t2 As VToDo = DirectCast(y, VToDo)

            d1 = t1.StartDateTime.TimeZoneDateTime
            d2 = t2.StartDateTime.TimeZoneDateTime
            summary1 = t1.Summary.Value
            summary2 = t2.Summary.Value

        ElseIf TypeOf x Is VJournal
            Dim j1 As VJournal = DirectCast(x, VJournal), j2 As VJournal = DirectCast(y, VJournal)

            d1 = j1.StartDateTime.TimeZoneDateTime
            d2 = j2.StartDateTime.TimeZoneDateTime
            summary1 = j1.Summary.Value
            summary2 = j2.Summary.Value

        Else
            Dim f1 As VFreeBusy = DirectCast(x, VFreeBusy), f2 As VFreeBusy = DirectCast(y, VFreeBusy)

            d1 = f1.StartDateTime.TimeZoneDateTime
            d2 = f2.StartDateTime.TimeZoneDateTime
            summary1 = f1.Organizer.Value
            summary2 = f2.Organizer.Value
        End If

        If d1.CompareTo(d2) = 0 Then
            If summary1 Is Nothing Then summary1 = String.Empty

            If summary2 Is Nothing Then summary2 = String.Empty

            ' For descending order, change this to compare summary 2 to summary 1 instead
            Return String.Compare(summary1, summary2, StringComparison.CurrentCulture)
        End If

        ' For descending order, change this to compare date 2 to date 1 instead
        Return d1.CompareTo(d2)
    End Function

End Class
