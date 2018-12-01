<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CalendarBrowserForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If sf IsNot Nothing Then
                    sf.Dispose()
                End If

                If components IsNot Nothing Then
                    components.Dispose()
                End If
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CalendarBrowserForm))
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnChgVersion = New System.Windows.Forms.Button()
        Me.cboComponents = New System.Windows.Forms.ComboBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.btnChgTimeZone = New System.Windows.Forms.Button()
        Me.lblFilename = New System.Windows.Forms.Label()
        Me.dgvCalendar = New System.Windows.Forms.DataGridView()
        Me.mnuMain = New System.Windows.Forms.MenuStrip()
        Me.miFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.miOpen = New System.Windows.Forms.ToolStripMenuItem()
        Me.miSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.miFileEncoding = New System.Windows.Forms.ToolStripMenuItem()
        Me.miFileUnicode = New System.Windows.Forms.ToolStripMenuItem()
        Me.miFileWestEuro = New System.Windows.Forms.ToolStripMenuItem()
        Me.miFileASCII = New System.Windows.Forms.ToolStripMenuItem()
        Me.miPropEncoding = New System.Windows.Forms.ToolStripMenuItem()
        Me.miPropUnicode = New System.Windows.Forms.ToolStripMenuItem()
        Me.miPropWestEuro = New System.Windows.Forms.ToolStripMenuItem()
        Me.miPropASCII = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuItem4 = New System.Windows.Forms.ToolStripSeparator()
        Me.miClear = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuItem6 = New System.Windows.Forms.ToolStripSeparator()
        Me.miAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.miExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tbcStartDateTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tbcSummary = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tbcOrganizer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tbcComment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvCalendar,System.ComponentModel.ISupportInitialize).BeginInit
        Me.mnuMain.SuspendLayout
        Me.SuspendLayout
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnAdd.Location = New System.Drawing.Point(12, 516)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(88, 32)
        Me.btnAdd.TabIndex = 3
        Me.btnAdd.Text = "&Add"
        '
        'btnEdit
        '
        Me.btnEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnEdit.Location = New System.Drawing.Point(106, 516)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(88, 32)
        Me.btnEdit.TabIndex = 4
        Me.btnEdit.Text = "&Edit"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(200, 516)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(88, 32)
        Me.btnDelete.TabIndex = 5
        Me.btnDelete.Text = "&Delete"
        '
        'btnChgVersion
        '
        Me.btnChgVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnChgVersion.Location = New System.Drawing.Point(448, 516)
        Me.btnChgVersion.Name = "btnChgVersion"
        Me.btnChgVersion.Size = New System.Drawing.Size(88, 32)
        Me.btnChgVersion.TabIndex = 6
        Me.btnChgVersion.Text = "&Version"
        '
        'cboComponents
        '
        Me.cboComponents.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cboComponents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboComponents.Location = New System.Drawing.Point(754, 519)
        Me.cboComponents.Name = "cboComponents"
        Me.cboComponents.Size = New System.Drawing.Size(176, 28)
        Me.cboComponents.TabIndex = 9
        '
        'label1
        '
        Me.label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.label1.Location = New System.Drawing.Point(679, 523)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(69, 23)
        Me.label1.TabIndex = 8
        Me.label1.Text = "V&iew"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnChgTimeZone
        '
        Me.btnChgTimeZone.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnChgTimeZone.Location = New System.Drawing.Point(542, 516)
        Me.btnChgTimeZone.Name = "btnChgTimeZone"
        Me.btnChgTimeZone.Size = New System.Drawing.Size(120, 32)
        Me.btnChgTimeZone.TabIndex = 7
        Me.btnChgTimeZone.Text = "&Time Zones"
        '
        'lblFilename
        '
        Me.lblFilename.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblFilename.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblFilename.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilename.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblFilename.Location = New System.Drawing.Point(12, 28)
        Me.lblFilename.Name = "lblFilename"
        Me.lblFilename.Size = New System.Drawing.Size(918, 23)
        Me.lblFilename.TabIndex = 1
        Me.lblFilename.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dgvCalendar
        '
        Me.dgvCalendar.AllowUserToAddRows = false
        Me.dgvCalendar.AllowUserToDeleteRows = false
        Me.dgvCalendar.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.dgvCalendar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvCalendar.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvCalendar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCalendar.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.tbcStartDateTime, Me.tbcSummary, Me.tbcOrganizer, Me.tbcComment})
        Me.dgvCalendar.Location = New System.Drawing.Point(12, 54)
        Me.dgvCalendar.MultiSelect = false
        Me.dgvCalendar.Name = "dgvCalendar"
        Me.dgvCalendar.ReadOnly = true
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvCalendar.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvCalendar.RowHeadersWidth = 25
        Me.dgvCalendar.RowTemplate.Height = 24
        Me.dgvCalendar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCalendar.Size = New System.Drawing.Size(918, 456)
        Me.dgvCalendar.StandardTab = true
        Me.dgvCalendar.TabIndex = 2
        '
        'mnuMain
        '
        Me.mnuMain.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!)
        Me.mnuMain.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miFile})
        Me.mnuMain.Location = New System.Drawing.Point(0, 0)
        Me.mnuMain.Name = "mnuMain"
        Me.mnuMain.Size = New System.Drawing.Size(942, 28)
        Me.mnuMain.TabIndex = 0
        '
        'miFile
        '
        Me.miFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miOpen, Me.miSave, Me.menuItem2, Me.miFileEncoding, Me.miPropEncoding, Me.menuItem4, Me.miClear, Me.menuItem6, Me.miAbout, Me.miExit})
        Me.miFile.Name = "miFile"
        Me.miFile.Size = New System.Drawing.Size(46, 24)
        Me.miFile.Text = "&File"
        '
        'miOpen
        '
        Me.miOpen.Name = "miOpen"
        Me.miOpen.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O),System.Windows.Forms.Keys)
        Me.miOpen.Size = New System.Drawing.Size(263, 30)
        Me.miOpen.Text = "&Open"
        '
        'miSave
        '
        Me.miSave.Name = "miSave"
        Me.miSave.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S),System.Windows.Forms.Keys)
        Me.miSave.Size = New System.Drawing.Size(263, 30)
        Me.miSave.Text = "&Save"
        '
        'menuItem2
        '
        Me.menuItem2.Name = "menuItem2"
        Me.menuItem2.Size = New System.Drawing.Size(260, 6)
        '
        'miFileEncoding
        '
        Me.miFileEncoding.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miFileUnicode, Me.miFileWestEuro, Me.miFileASCII})
        Me.miFileEncoding.Name = "miFileEncoding"
        Me.miFileEncoding.Size = New System.Drawing.Size(263, 30)
        Me.miFileEncoding.Text = "File Encoding"
        '
        'miFileUnicode
        '
        Me.miFileUnicode.Checked = true
        Me.miFileUnicode.CheckState = System.Windows.Forms.CheckState.Checked
        Me.miFileUnicode.Name = "miFileUnicode"
        Me.miFileUnicode.Size = New System.Drawing.Size(302, 30)
        Me.miFileUnicode.Text = "Unicode (UTF-8)"
        '
        'miFileWestEuro
        '
        Me.miFileWestEuro.Name = "miFileWestEuro"
        Me.miFileWestEuro.Size = New System.Drawing.Size(302, 30)
        Me.miFileWestEuro.Text = "Western European (Windows)"
        '
        'miFileASCII
        '
        Me.miFileASCII.Name = "miFileASCII"
        Me.miFileASCII.Size = New System.Drawing.Size(302, 30)
        Me.miFileASCII.Text = "ASCII"
        '
        'miPropEncoding
        '
        Me.miPropEncoding.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miPropUnicode, Me.miPropWestEuro, Me.miPropASCII})
        Me.miPropEncoding.Name = "miPropEncoding"
        Me.miPropEncoding.Size = New System.Drawing.Size(263, 30)
        Me.miPropEncoding.Text = "Property Encoding"
        '
        'miPropUnicode
        '
        Me.miPropUnicode.Name = "miPropUnicode"
        Me.miPropUnicode.Size = New System.Drawing.Size(302, 30)
        Me.miPropUnicode.Text = "Unicode (UTF-8)"
        '
        'miPropWestEuro
        '
        Me.miPropWestEuro.Name = "miPropWestEuro"
        Me.miPropWestEuro.Size = New System.Drawing.Size(302, 30)
        Me.miPropWestEuro.Text = "Western European (Windows)"
        '
        'miPropASCII
        '
        Me.miPropASCII.Checked = true
        Me.miPropASCII.CheckState = System.Windows.Forms.CheckState.Checked
        Me.miPropASCII.Name = "miPropASCII"
        Me.miPropASCII.Size = New System.Drawing.Size(302, 30)
        Me.miPropASCII.Text = "ASCII"
        '
        'menuItem4
        '
        Me.menuItem4.Name = "menuItem4"
        Me.menuItem4.Size = New System.Drawing.Size(260, 6)
        '
        'miClear
        '
        Me.miClear.Name = "miClear"
        Me.miClear.Size = New System.Drawing.Size(263, 30)
        Me.miClear.Text = "&Clear"
        '
        'menuItem6
        '
        Me.menuItem6.Name = "menuItem6"
        Me.menuItem6.Size = New System.Drawing.Size(260, 6)
        '
        'miAbout
        '
        Me.miAbout.Name = "miAbout"
        Me.miAbout.Size = New System.Drawing.Size(263, 30)
        Me.miAbout.Text = "&About Calendar Browser"
        '
        'miExit
        '
        Me.miExit.Name = "miExit"
        Me.miExit.Size = New System.Drawing.Size(263, 30)
        Me.miExit.Text = "E&xit"
        '
        'tbcStartDateTime
        '
        Me.tbcStartDateTime.DataPropertyName = "StartDateTime"
        Me.tbcStartDateTime.HeaderText = "Start Date/Time"
        Me.tbcStartDateTime.Name = "tbcStartDateTime"
        Me.tbcStartDateTime.ReadOnly = true
        Me.tbcStartDateTime.Width = 210
        '
        'tbcSummary
        '
        Me.tbcSummary.DataPropertyName = "Summary_Value"
        Me.tbcSummary.HeaderText = "Summary"
        Me.tbcSummary.Name = "tbcSummary"
        Me.tbcSummary.ReadOnly = true
        Me.tbcSummary.Width = 500
        '
        'tbcOrganizer
        '
        Me.tbcOrganizer.DataPropertyName = "Organizer_Value"
        Me.tbcOrganizer.HeaderText = "Organizer"
        Me.tbcOrganizer.Name = "tbcOrganizer"
        Me.tbcOrganizer.ReadOnly = true
        Me.tbcOrganizer.Width = 250
        '
        'tbcComment
        '
        Me.tbcComment.DataPropertyName = "Comment_Value"
        Me.tbcComment.HeaderText = "Comment"
        Me.tbcComment.Name = "tbcComment"
        Me.tbcComment.ReadOnly = true
        Me.tbcComment.Width = 500
        '
        'CalendarBrowserForm
        '
        Me.ClientSize = New System.Drawing.Size(942, 560)
        Me.Controls.Add(Me.lblFilename)
        Me.Controls.Add(Me.dgvCalendar)
        Me.Controls.Add(Me.mnuMain)
        Me.Controls.Add(Me.btnChgTimeZone)
        Me.Controls.Add(Me.cboComponents)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.btnChgVersion)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnAdd)
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MainMenuStrip = Me.mnuMain
        Me.MinimumSize = New System.Drawing.Size(800, 250)
        Me.Name = "CalendarBrowserForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "vCalendar/iCalendar Browser"
        CType(Me.dgvCalendar,System.ComponentModel.ISupportInitialize).EndInit
        Me.mnuMain.ResumeLayout(false)
        Me.mnuMain.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents cboComponents As System.Windows.Forms.ComboBox
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents btnChgVersion As System.Windows.Forms.Button
    Private WithEvents lblFilename As System.Windows.Forms.Label
    Private WithEvents dgvCalendar As System.Windows.Forms.DataGridView
    Private WithEvents mnuMain As System.Windows.Forms.MenuStrip
    Private WithEvents miFile As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents miOpen As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents miSave As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents menuItem2 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents miFileEncoding As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents miFileUnicode As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents miFileWestEuro As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents miFileASCII As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents miPropEncoding As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents miPropUnicode As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents miPropWestEuro As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents miPropASCII As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents menuItem4 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents miClear As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents menuItem6 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents miAbout As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents miExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnChgTimeZone As System.Windows.Forms.Button
    Friend WithEvents tbcStartDateTime As DataGridViewTextBoxColumn
    Friend WithEvents tbcSummary As DataGridViewTextBoxColumn
    Friend WithEvents tbcOrganizer As DataGridViewTextBoxColumn
    Friend WithEvents tbcComment As DataGridViewTextBoxColumn
End Class
