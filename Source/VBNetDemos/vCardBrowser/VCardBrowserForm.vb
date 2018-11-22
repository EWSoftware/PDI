'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : VCardBrowserForm.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/02/2015
' Note    : Copyright 2004-2015, Eric Woodruff, All rights reserved
' Compiler: Visual Basic .NET
'
' This is a simple demonstration application that shows how to load, save, and manage a set of vCards including
' how to edit the various vCard properties.
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 12/03/2004  EFW  Created the code
' 05/16/2007  EFW  Updated for use with .NET 2.0
'================================================================================================================

Imports System.ComponentModel
Imports System.Globalization
Imports System.IO
Imports System.Text

Imports EWSoftware.PDI.Objects
Imports EWSoftware.PDI.Parser
Imports EWSoftware.PDI.Properties

''' <summary>
''' This application demonstrates loading, saving, and managing a set of vCards including how to edit the various
''' vCard properties.
''' </summary>
Public Partial Class VCardBrowserForm
    Inherits System.Windows.Forms.Form

    '===============================================================

    Private vCards As VCardCollection   ' The vCard collection being browsed
    Private wasModified As Boolean
    Private sf As StringFormat

    '===============================================================

    ''' <summary>
    ''' The main entry point for the application
    ''' </summary>
    Shared Sub Main(Args As String())
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(false)
        Application.Run(new VCardBrowserForm())
    End Sub

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New()
        MyBase.New()

        InitializeComponent()
        tbcVersion.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        ' The string format to use when drawing the status text
        sf = new StringFormat(StringFormatFlags.NoWrap)
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center
        sf.Trimming = StringTrimming.EllipsisCharacter

        dgvCards.AutoGenerateColumns = False
        tbcLastRevision.DefaultCellStyle.Format = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern &
            " " & CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern

        cboVersion.SelectedIndex = 0
        vCards = New VCardCollection()
        LoadGridWithVCards()
    End Sub

    ''' <summary>
    ''' Load the grid with the specified vCard collection
    ''' </summary>
    Private Sub LoadGridWithVCards()
        Dim gridIdx As Integer = dgvCards.CurrentCellAddress.Y

        ' Enable or disable the buttons based on the vCard count
        Dim enabled As Boolean = (vCards.Count <> 0)
        miClear.Enabled = enabled
        btnEdit.Enabled = enabled
        btnDelete.Enabled = enabled
        cboVersion.Enabled = enabled
        btnApplyVersion.Enabled = enabled

        ' This is an example of sorting a vCard collection.  Due to the variety of properties in a vCard, sorting
        ' is left up to the developer utilizing a comparison delegate.  This example sorts the collection by the
        ' name property taking into account the SortStringProperty if set.  Also note that the collection can be
        ' sorted in the grid by clicking on the column headers.
        vCards.Sort(AddressOf VCardSorter)

        ' Connect the ListChanged event so that we are notified when the list is modified
        AddHandler vCards.ListChanged, New ListChangedEventHandler(AddressOf vCards_ListChanged)
        wasModified = False

        ' VCardCollection is bindable so we can assign it directly as the data source.  In order to show child
        ' properties in the grid, set the column's DataPropertyName to the name of the child property separated
        ' from the parent property by an underscore (i.e. Name_SortableName, LastRevision_DateTimeValue).
        dgvCards.DataSource = vCards

        ' Stay on the last item selected
        If gridIdx > -1 And gridIdx < vCards.Count Then
            dgvCards.CurrentCell = dgvCards(0, gridIdx)
        End If
    End Sub

    ''' <summary>
    ''' Sort the VCards
    ''' </summary>
    ''' <param name="x">The first vCard to compare</param>
    ''' <param name="y">The second vCard to compare</param>
    ''' <returns>Zero if equal, a negative number if X is less than Y, or a
    ''' positive value if X is greater than Y.</returns>
    Private Function VCardSorter(ByVal x As VCard, ByVal y As VCard) As Integer
        Dim sortName1, sortName2 As String

        ' Get the names to compare.  Precedence is given to the SortStringProperty as that is the purpose of its
        ' existence.
        sortName1 = x.SortString.Value

        If String.IsNullOrWhiteSpace(sortName1) Then
            sortName1 = x.Name.SortableName
        End If

        sortName2 = y.SortString.Value

        If String.IsNullOrWhiteSpace(sortName2) Then
            sortName2 = y.Name.SortableName
        End If

        ' For descending order, change this to compare name 2 to name 1 instead
        Return String.Compare(sortName1, sortName2, StringComparison.CurrentCulture)
    End Function

    ''' <summary>
    ''' This sets the modified flag when the vCard collection is edited and adjusts the button enabled states
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub vCards_ListChanged(ByVal sender As Object, _
      ByVal e As ListChangedEventArgs)
        Dim enabled As Boolean = (vCards.Count <> 0)
        miClear.Enabled = enabled
        btnEdit.Enabled = enabled
        btnDelete.Enabled = enabled
        cboVersion.Enabled = enabled
        btnApplyVersion.Enabled = enabled

        wasModified = True
    End Sub

    ''' <summary>
    ''' Prompt to save if the collection has been modified
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub VCardBrowserForm_Closing(ByVal sender As Object, _
      ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If wasModified = True AndAlso MessageBox.Show("Do you want to discard your changes to the current vCards?",
          "Discard Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) =
          System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' Open a vCard file
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub miOpen_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles miOpen.Click
        If wasModified = True AndAlso MessageBox.Show("Do you want to discard your changes to the current vCards?",
          "Discard Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) =
          System.Windows.Forms.DialogResult.No Then
            Return
        End If

        Using dlg As New OpenFileDialog()
            dlg.Title = "Load vCard File"
            dlg.DefaultExt = "vcf"
            dlg.Filter = "VCF files (*.vcf)|*.vcf|All files (*.*)|*.*"
            dlg.InitialDirectory = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory,
                "..\..\..\..\PDIFiles"))

            If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Try
                    Me.Cursor = Cursors.WaitCursor

                    ' Parse the vCard information from the file and load the data grid with some basic
                    ' information about the vCards in it.
                    vCards = VCardParser.ParseFromFile(dlg.FileName)
                    LoadGridWithVCards()

                    lblFilename.Text = dlg.FileName

                Catch ex As Exception
                    Dim errorMsg As String = String.Format("Unable to load vCards:{0}{1}", Environment.NewLine,
                        ex.Message)

                    If Not (ex.InnerException Is Nothing)
                        errorMsg &= ex.InnerException.Message & Environment.NewLine

                        If Not (ex.InnerException.InnerException Is Nothing)
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
    ''' Save a vCard file
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub miSave_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles miSave.Click
        Using dlg As New SaveFileDialog()
            dlg.Title = "Save vCard File"
            dlg.DefaultExt = "vcf"
            dlg.Filter = "VCF files (*.vcf)|*.vcf|All files (*.*)|*.*"
            dlg.InitialDirectory = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory,
                "..\..\..\..\PDIFiles"))
            dlg.FileName = lblFilename.Text

            If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Try
                    Me.Cursor = Cursors.WaitCursor

                    ' Open the file and write the vCards to it.  We'll use the same encoding method used by the
                    ' parser.
                    Using sw As New StreamWriter(dlg.FileName, False, PDIParser.DefaultEncoding)
                        vCards.WriteToStream(sw)
                    End Using

                    lblFilename.Text = dlg.FileName
                    wasModified = False

                Catch ex As Exception
                    Dim errorMsg As String = String.Format("Unable to save vCards:{0}{1}", Environment.NewLine,
                        ex.Message)

                    If Not (ex.InnerException Is Nothing) Then
                        errorMsg &= ex.InnerException.Message + Environment.NewLine

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
    ''' Clear all loaded vCard information
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub miClear_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles miClear.Click
        If MessageBox.Show("Are you sure you want to remove all vCards?", "Clear vCards", MessageBoxButtons.YesNo,
          MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = System.Windows.Forms.DialogResult.Yes Then
            vCards.Clear()
        End If
    End Sub

    ''' <summary>
    ''' Show the About dialog box
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub miAbout_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles miAbout.Click
        Using dlg As new AboutDlg()
            dlg.ShowDialog()
        End Using
    End Sub

    ''' <summary>
    ''' Close the application
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub miExit_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles miExit.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' Add a vCard to the collection
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnAdd_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles btnAdd.Click
        Using dlg As New VCardPropertiesDlg()
            If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Dim newVCard As New VCard()
                dlg.GetValues(newVCard)

                ' Create a unique ID for the new vCard
                newVCard.UniqueId.AssignNewId(True)

                vCards.Add(newVCard)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Edit a vCard in the collection
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnEdit_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles btnEdit.Click
        If dgvCards.CurrentCellAddress.Y = -1 Then
            MessageBox.Show("Please select a vCard to edit", "No vCard", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        Using dlg As New VCardPropertiesDlg()
            Me.Cursor = Cursors.WaitCursor
            dlg.SetValues(vCards(dgvCards.CurrentCellAddress.Y))
            Me.Cursor = Cursors.Default

            If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                dlg.GetValues(vCards(dgvCards.CurrentCellAddress.Y))
                wasModified = True
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Delete a vCard from the collection
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnDelete_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles btnDelete.Click
        If dgvCards.CurrentCellAddress.Y = -1 Then
            MessageBox.Show("Please select a vCard to delete", "No vCard", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If MessageBox.Show("Are you sure you want to delete the selected vCard?", "Delete vCard",
          MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) =
          System.Windows.Forms.DialogResult.No Then
            Return
        End If

        vCards.RemoveAt(dgvCards.CurrentCellAddress.Y)
    End Sub

    ''' <summary>
    ''' Apply the selected version to all vCards in the collection.
    ''' </summary>
    ''' <remarks>Downgrading to Version 2.1 will cause the loss of any Version 3.0 properties if the file is
    ''' saved and reloaded.</remarks>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnApplyVersion_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles btnApplyVersion.Click
        If MessageBox.Show("Are you sure you want to apply the selected version to all vCards in the file?",
          "Apply vCard Version", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) =
          System.Windows.Forms.DialogResult.No Then
            Return
        End If

        If cboVersion.SelectedIndex = 0 Then
            vCards.PropagateVersion(SpecificationVersions.vCard21)
        Else
            vCards.PropagateVersion(SpecificationVersions.vCard30)
        End If
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
    ''' This is used to custom draw the version number cell
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub dgvCards_CellPainting(sender As Object, _
      e As DataGridViewCellPaintingEventArgs) Handles dgvCards.CellPainting
        Dim foreColor As Color
        Dim version As String

        If e.RowIndex > -1 AndAlso e.ColumnIndex = 0 Then
            If DirectCast(e.Value, SpecificationVersions) = SpecificationVersions.vCard21 Then
                version = "2.1"
            Else
                version = "3.0"
            End If

            e.Paint(e.CellBounds, (e.PaintParts And Not DataGridViewPaintParts.ContentForeground))

            If (e.State And DataGridViewElementStates.Selected) <> DataGridViewElementStates.None Then
                foreColor = e.CellStyle.SelectionForeColor
            Else
                foreColor = e.CellStyle.ForeColor
            End If

            Using b As SolidBrush = New SolidBrush(foreColor)
                e.Graphics.DrawString(version, e.CellStyle.Font, b, e.CellBounds, Me.sf)
            End Using

            e.Handled = True
        End If
    End Sub

    ''' <summary>
    ''' Invoke edit on cell double click
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub dgvCards_CellDoubleClick( sender As Object,  e As DataGridViewCellEventArgs) Handles dgvCards.CellDoubleClick
        btnEdit_Click(sender, e)
    End Sub
End Class
