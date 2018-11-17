<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HolidayTestForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
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
        Me.btnClose = New System.Windows.Forms.Button()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.hmHolidays = New EWSoftware.PDI.Windows.Forms.HolidayManager()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.dgvDatesFound = New System.Windows.Forms.DataGridView()
        Me.tbcDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tbcDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dtpTestDate = New System.Windows.Forms.DateTimePicker()
        Me.udcToYear = New System.Windows.Forms.NumericUpDown()
        Me.udcFromYear = New System.Windows.Forms.NumericUpDown()
        Me.grpEaster = New System.Windows.Forms.GroupBox()
        Me.rbGregorian = New System.Windows.Forms.RadioButton()
        Me.rbOrthodox = New System.Windows.Forms.RadioButton()
        Me.rbJulian = New System.Windows.Forms.RadioButton()
        Me.btnEaster = New System.Windows.Forms.Button()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.btnTestDate = New System.Windows.Forms.Button()
        Me.label3 = New System.Windows.Forms.Label()
        Me.groupBox1.SuspendLayout
        Me.groupBox2.SuspendLayout
        CType(Me.dgvDatesFound,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.udcToYear,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.udcFromYear,System.ComponentModel.ISupportInitialize).BeginInit
        Me.grpEaster.SuspendLayout
        Me.SuspendLayout
        '
        'btnClose
        '
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(724, 381)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(88, 32)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "&Close"
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.hmHolidays)
        Me.groupBox1.Location = New System.Drawing.Point(12, 12)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(286, 401)
        Me.groupBox1.TabIndex = 0
        Me.groupBox1.TabStop = false
        Me.groupBox1.Text = "H&olidays"
        '
        'hmHolidays
        '
        Me.hmHolidays.Location = New System.Drawing.Point(8, 24)
        Me.hmHolidays.Name = "hmHolidays"
        Me.hmHolidays.Size = New System.Drawing.Size(272, 368)
        Me.hmHolidays.TabIndex = 0
        '
        'groupBox2
        '
        Me.groupBox2.Controls.Add(Me.dgvDatesFound)
        Me.groupBox2.Controls.Add(Me.dtpTestDate)
        Me.groupBox2.Controls.Add(Me.udcToYear)
        Me.groupBox2.Controls.Add(Me.udcFromYear)
        Me.groupBox2.Controls.Add(Me.grpEaster)
        Me.groupBox2.Controls.Add(Me.btnEaster)
        Me.groupBox2.Controls.Add(Me.btnFind)
        Me.groupBox2.Controls.Add(Me.label2)
        Me.groupBox2.Controls.Add(Me.label1)
        Me.groupBox2.Controls.Add(Me.btnTestDate)
        Me.groupBox2.Controls.Add(Me.label3)
        Me.groupBox2.Location = New System.Drawing.Point(304, 12)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(508, 363)
        Me.groupBox2.TabIndex = 1
        Me.groupBox2.TabStop = false
        Me.groupBox2.Text = "Test Date Detection"
        '
        'dgvDatesFound
        '
        Me.dgvDatesFound.AllowUserToAddRows = false
        Me.dgvDatesFound.AllowUserToDeleteRows = false
        Me.dgvDatesFound.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDatesFound.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvDatesFound.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDatesFound.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.tbcDate, Me.tbcDescription})
        Me.dgvDatesFound.Location = New System.Drawing.Point(6, 21)
        Me.dgvDatesFound.MultiSelect = false
        Me.dgvDatesFound.Name = "dgvDatesFound"
        Me.dgvDatesFound.ReadOnly = true
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDatesFound.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvDatesFound.RowHeadersVisible = false
        Me.dgvDatesFound.RowTemplate.Height = 24
        Me.dgvDatesFound.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDatesFound.Size = New System.Drawing.Size(337, 298)
        Me.dgvDatesFound.TabIndex = 10
        '
        'tbcDate
        '
        Me.tbcDate.DataPropertyName = "Value"
        Me.tbcDate.HeaderText = "Date"
        Me.tbcDate.Name = "tbcDate"
        Me.tbcDate.ReadOnly = true
        Me.tbcDate.Width = 90
        '
        'tbcDescription
        '
        Me.tbcDescription.DataPropertyName = "Display"
        Me.tbcDescription.HeaderText = "Description"
        Me.tbcDescription.Name = "tbcDescription"
        Me.tbcDescription.ReadOnly = true
        Me.tbcDescription.Width = 210
        '
        'dtpTestDate
        '
        Me.dtpTestDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTestDate.Location = New System.Drawing.Point(96, 331)
        Me.dtpTestDate.Name = "dtpTestDate"
        Me.dtpTestDate.Size = New System.Drawing.Size(110, 22)
        Me.dtpTestDate.TabIndex = 8
        '
        'udcToYear
        '
        Me.udcToYear.Location = New System.Drawing.Point(439, 64)
        Me.udcToYear.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.udcToYear.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.udcToYear.Name = "udcToYear"
        Me.udcToYear.Size = New System.Drawing.Size(56, 22)
        Me.udcToYear.TabIndex = 3
        Me.udcToYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.udcToYear.Value = New Decimal(New Integer() {2004, 0, 0, 0})
        '
        'udcFromYear
        '
        Me.udcFromYear.Location = New System.Drawing.Point(349, 64)
        Me.udcFromYear.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.udcFromYear.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.udcFromYear.Name = "udcFromYear"
        Me.udcFromYear.Size = New System.Drawing.Size(56, 22)
        Me.udcFromYear.TabIndex = 1
        Me.udcFromYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.udcFromYear.Value = New Decimal(New Integer() {2000, 0, 0, 0})
        '
        'grpEaster
        '
        Me.grpEaster.Controls.Add(Me.rbGregorian)
        Me.grpEaster.Controls.Add(Me.rbOrthodox)
        Me.grpEaster.Controls.Add(Me.rbJulian)
        Me.grpEaster.Location = New System.Drawing.Point(349, 92)
        Me.grpEaster.Name = "grpEaster"
        Me.grpEaster.Size = New System.Drawing.Size(144, 120)
        Me.grpEaster.TabIndex = 4
        Me.grpEaster.TabStop = false
        Me.grpEaster.Text = "Easter Method"
        '
        'rbGregorian
        '
        Me.rbGregorian.Checked = true
        Me.rbGregorian.Location = New System.Drawing.Point(24, 88)
        Me.rbGregorian.Name = "rbGregorian"
        Me.rbGregorian.Size = New System.Drawing.Size(104, 24)
        Me.rbGregorian.TabIndex = 2
        Me.rbGregorian.TabStop = true
        Me.rbGregorian.Text = "Gregorian"
        '
        'rbOrthodox
        '
        Me.rbOrthodox.Location = New System.Drawing.Point(24, 56)
        Me.rbOrthodox.Name = "rbOrthodox"
        Me.rbOrthodox.Size = New System.Drawing.Size(104, 24)
        Me.rbOrthodox.TabIndex = 1
        Me.rbOrthodox.Text = "Orthodox"
        '
        'rbJulian
        '
        Me.rbJulian.Location = New System.Drawing.Point(24, 24)
        Me.rbJulian.Name = "rbJulian"
        Me.rbJulian.Size = New System.Drawing.Size(104, 24)
        Me.rbJulian.TabIndex = 0
        Me.rbJulian.Text = "Julian"
        '
        'btnEaster
        '
        Me.btnEaster.Location = New System.Drawing.Point(352, 280)
        Me.btnEaster.Name = "btnEaster"
        Me.btnEaster.Size = New System.Drawing.Size(136, 32)
        Me.btnEaster.TabIndex = 6
        Me.btnEaster.Text = "Find Ea&ster"
        '
        'btnFind
        '
        Me.btnFind.Location = New System.Drawing.Point(352, 232)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(136, 32)
        Me.btnFind.TabIndex = 5
        Me.btnFind.Text = "&Find Holidays"
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(439, 40)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(62, 23)
        Me.label2.TabIndex = 2
        Me.label2.Text = "To Year"
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(349, 40)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(84, 23)
        Me.label1.TabIndex = 0
        Me.label1.Text = "Fro&m Year"
        '
        'btnTestDate
        '
        Me.btnTestDate.Location = New System.Drawing.Point(212, 325)
        Me.btnTestDate.Name = "btnTestDate"
        Me.btnTestDate.Size = New System.Drawing.Size(88, 32)
        Me.btnTestDate.TabIndex = 9
        Me.btnTestDate.Text = "&Holiday?"
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(6, 330)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(84, 23)
        Me.label3.TabIndex = 7
        Me.label3.Text = "&Test Date"
        Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'HolidayTestForm
        '
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(824, 425)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.groupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "HolidayTestForm"
        Me.ShowInTaskbar = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Test Holiday Classes"
        Me.groupBox1.ResumeLayout(false)
        Me.groupBox2.ResumeLayout(false)
        CType(Me.dgvDatesFound,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.udcToYear,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.udcFromYear,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpEaster.ResumeLayout(false)
        Me.ResumeLayout(false)

End Sub


    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents groupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents btnTestDate As System.Windows.Forms.Button
    Friend WithEvents btnEaster As System.Windows.Forms.Button
    Friend WithEvents grpEaster As System.Windows.Forms.GroupBox
    Friend WithEvents rbJulian As System.Windows.Forms.RadioButton
    Friend WithEvents rbOrthodox As System.Windows.Forms.RadioButton
    Friend WithEvents rbGregorian As System.Windows.Forms.RadioButton
    Friend WithEvents udcFromYear As System.Windows.Forms.NumericUpDown
    Friend WithEvents udcToYear As System.Windows.Forms.NumericUpDown
    Friend WithEvents dtpTestDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents hmHolidays As EWSoftware.PDI.Windows.Forms.HolidayManager
    Private WithEvents dgvDatesFound As System.Windows.Forms.DataGridView
    Private WithEvents tbcDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Private WithEvents tbcDescription As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
