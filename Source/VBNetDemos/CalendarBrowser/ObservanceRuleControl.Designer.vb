<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ObservanceRuleControl
    Inherits EWSoftware.PDI.Windows.Forms.BrowseControl

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
        Me.tabTimeZone = New System.Windows.Forms.TabControl()
        Me.pgGeneral = New System.Windows.Forms.TabPage()
        Me.cboRuleType = New System.Windows.Forms.ComboBox()
        Me.label10 = New System.Windows.Forms.Label()
        Me.label7 = New System.Windows.Forms.Label()
        Me.udcToMinutes = New System.Windows.Forms.NumericUpDown()
        Me.label8 = New System.Windows.Forms.Label()
        Me.udcToHours = New System.Windows.Forms.NumericUpDown()
        Me.label9 = New System.Windows.Forms.Label()
        Me.label6 = New System.Windows.Forms.Label()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.label5 = New System.Windows.Forms.Label()
        Me.txtTZName = New System.Windows.Forms.TextBox()
        Me.label4 = New System.Windows.Forms.Label()
        Me.udcFromMinutes = New System.Windows.Forms.NumericUpDown()
        Me.label3 = New System.Windows.Forms.Label()
        Me.udcFromHours = New System.Windows.Forms.NumericUpDown()
        Me.label2 = New System.Windows.Forms.Label()
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.label1 = New System.Windows.Forms.Label()
        Me.pgRules = New System.Windows.Forms.TabPage()
        Me.rcRulesDates = New CalendarBrowser.RecurrenceControl()
        Me.tabTimeZone.SuspendLayout
        Me.pgGeneral.SuspendLayout
        CType(Me.udcToMinutes,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.udcToHours,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.udcFromMinutes,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.udcFromHours,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pgRules.SuspendLayout
        Me.SuspendLayout
        '
        'tabTimeZone
        '
        Me.tabTimeZone.Controls.Add(Me.pgGeneral)
        Me.tabTimeZone.Controls.Add(Me.pgRules)
        Me.tabTimeZone.Location = New System.Drawing.Point(3, 3)
        Me.tabTimeZone.Name = "tabTimeZone"
        Me.tabTimeZone.SelectedIndex = 0
        Me.tabTimeZone.Size = New System.Drawing.Size(694, 355)
        Me.tabTimeZone.TabIndex = 0
        '
        'pgGeneral
        '
        Me.pgGeneral.Controls.Add(Me.cboRuleType)
        Me.pgGeneral.Controls.Add(Me.label10)
        Me.pgGeneral.Controls.Add(Me.label7)
        Me.pgGeneral.Controls.Add(Me.udcToMinutes)
        Me.pgGeneral.Controls.Add(Me.label8)
        Me.pgGeneral.Controls.Add(Me.udcToHours)
        Me.pgGeneral.Controls.Add(Me.label9)
        Me.pgGeneral.Controls.Add(Me.label6)
        Me.pgGeneral.Controls.Add(Me.txtComment)
        Me.pgGeneral.Controls.Add(Me.label5)
        Me.pgGeneral.Controls.Add(Me.txtTZName)
        Me.pgGeneral.Controls.Add(Me.label4)
        Me.pgGeneral.Controls.Add(Me.udcFromMinutes)
        Me.pgGeneral.Controls.Add(Me.label3)
        Me.pgGeneral.Controls.Add(Me.udcFromHours)
        Me.pgGeneral.Controls.Add(Me.label2)
        Me.pgGeneral.Controls.Add(Me.dtpStartDate)
        Me.pgGeneral.Controls.Add(Me.label1)
        Me.pgGeneral.Location = New System.Drawing.Point(4, 29)
        Me.pgGeneral.Name = "pgGeneral"
        Me.pgGeneral.Size = New System.Drawing.Size(686, 322)
        Me.pgGeneral.TabIndex = 0
        Me.pgGeneral.Text = "General"
        Me.pgGeneral.UseVisualStyleBackColor = true
        '
        'cboRuleType
        '
        Me.cboRuleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRuleType.Items.AddRange(New Object() {"Standard", "Daylight"})
        Me.cboRuleType.Location = New System.Drawing.Point(174, 24)
        Me.cboRuleType.Name = "cboRuleType"
        Me.cboRuleType.Size = New System.Drawing.Size(112, 28)
        Me.cboRuleType.TabIndex = 1
        '
        'label10
        '
        Me.label10.Location = New System.Drawing.Point(100, 26)
        Me.label10.Name = "label10"
        Me.label10.Size = New System.Drawing.Size(68, 23)
        Me.label10.TabIndex = 0
        Me.label10.Text = "Type"
        Me.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label7
        '
        Me.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.label7.Location = New System.Drawing.Point(384, 103)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(90, 23)
        Me.label7.TabIndex = 15
        Me.label7.Text = "Minutes"
        Me.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'udcToMinutes
        '
        Me.udcToMinutes.Location = New System.Drawing.Point(318, 136)
        Me.udcToMinutes.Maximum = New Decimal(New Integer() {59, 0, 0, 0})
        Me.udcToMinutes.Minimum = New Decimal(New Integer() {59, 0, 0, -2147483648})
        Me.udcToMinutes.Name = "udcToMinutes"
        Me.udcToMinutes.Size = New System.Drawing.Size(60, 26)
        Me.udcToMinutes.TabIndex = 14
        Me.udcToMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'label8
        '
        Me.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.label8.Location = New System.Drawing.Point(240, 137)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(72, 23)
        Me.label8.TabIndex = 13
        Me.label8.Text = "Hours"
        Me.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'udcToHours
        '
        Me.udcToHours.Location = New System.Drawing.Point(174, 136)
        Me.udcToHours.Maximum = New Decimal(New Integer() {23, 0, 0, 0})
        Me.udcToHours.Minimum = New Decimal(New Integer() {23, 0, 0, -2147483648})
        Me.udcToHours.Name = "udcToHours"
        Me.udcToHours.Size = New System.Drawing.Size(60, 26)
        Me.udcToHours.TabIndex = 12
        Me.udcToHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'label9
        '
        Me.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.label9.Location = New System.Drawing.Point(39, 137)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(129, 23)
        Me.label9.TabIndex = 11
        Me.label9.Text = "Offset To"
        Me.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label6
        '
        Me.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.label6.Location = New System.Drawing.Point(384, 137)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(90, 23)
        Me.label6.TabIndex = 10
        Me.label6.Text = "Minutes"
        Me.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtComment
        '
        Me.txtComment.AcceptsReturn = true
        Me.txtComment.Location = New System.Drawing.Point(174, 176)
        Me.txtComment.Multiline = true
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComment.Size = New System.Drawing.Size(455, 118)
        Me.txtComment.TabIndex = 17
        '
        'label5
        '
        Me.label5.Location = New System.Drawing.Point(43, 178)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(125, 23)
        Me.label5.TabIndex = 16
        Me.label5.Text = "Comment"
        Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTZName
        '
        Me.txtTZName.Location = New System.Drawing.Point(174, 64)
        Me.txtTZName.Name = "txtTZName"
        Me.txtTZName.Size = New System.Drawing.Size(264, 26)
        Me.txtTZName.TabIndex = 5
        '
        'label4
        '
        Me.label4.Location = New System.Drawing.Point(16, 66)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(152, 23)
        Me.label4.TabIndex = 4
        Me.label4.Text = "Time Zone Name"
        Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'udcFromMinutes
        '
        Me.udcFromMinutes.Location = New System.Drawing.Point(318, 104)
        Me.udcFromMinutes.Maximum = New Decimal(New Integer() {59, 0, 0, 0})
        Me.udcFromMinutes.Minimum = New Decimal(New Integer() {59, 0, 0, -2147483648})
        Me.udcFromMinutes.Name = "udcFromMinutes"
        Me.udcFromMinutes.Size = New System.Drawing.Size(60, 26)
        Me.udcFromMinutes.TabIndex = 9
        Me.udcFromMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'label3
        '
        Me.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.label3.Location = New System.Drawing.Point(240, 105)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(72, 23)
        Me.label3.TabIndex = 8
        Me.label3.Text = "Hours"
        Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'udcFromHours
        '
        Me.udcFromHours.Location = New System.Drawing.Point(174, 104)
        Me.udcFromHours.Maximum = New Decimal(New Integer() {23, 0, 0, 0})
        Me.udcFromHours.Minimum = New Decimal(New Integer() {23, 0, 0, -2147483648})
        Me.udcFromHours.Name = "udcFromHours"
        Me.udcFromHours.Size = New System.Drawing.Size(60, 26)
        Me.udcFromHours.TabIndex = 7
        Me.udcFromHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'label2
        '
        Me.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.label2.Location = New System.Drawing.Point(35, 105)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(133, 23)
        Me.label2.TabIndex = 6
        Me.label2.Text = "Offset From"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpStartDate
        '
        Me.dtpStartDate.Checked = false
        Me.dtpStartDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartDate.Location = New System.Drawing.Point(394, 24)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.Size = New System.Drawing.Size(235, 26)
        Me.dtpStartDate.TabIndex = 3
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(325, 26)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(63, 23)
        Me.label1.TabIndex = 2
        Me.label1.Text = "&Start"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pgRules
        '
        Me.pgRules.Controls.Add(Me.rcRulesDates)
        Me.pgRules.Location = New System.Drawing.Point(4, 29)
        Me.pgRules.Name = "pgRules"
        Me.pgRules.Size = New System.Drawing.Size(686, 322)
        Me.pgRules.TabIndex = 1
        Me.pgRules.Text = "Rules"
        Me.pgRules.UseVisualStyleBackColor = true
        '
        'rcRulesDates
        '
        Me.rcRulesDates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rcRulesDates.Location = New System.Drawing.Point(0, 0)
        Me.rcRulesDates.Name = "rcRulesDates"
        Me.rcRulesDates.Size = New System.Drawing.Size(686, 322)
        Me.rcRulesDates.TabIndex = 0
        '
        'ObservanceRuleControl
        '
        Me.Controls.Add(Me.tabTimeZone)
        Me.Name = "ObservanceRuleControl"
        Me.Size = New System.Drawing.Size(701, 390)
        Me.Controls.SetChildIndex(Me.tabTimeZone, 0)
        Me.tabTimeZone.ResumeLayout(false)
        Me.pgGeneral.ResumeLayout(false)
        Me.pgGeneral.PerformLayout
        CType(Me.udcToMinutes,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.udcToHours,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.udcFromMinutes,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.udcFromHours,System.ComponentModel.ISupportInitialize).EndInit
        Me.pgRules.ResumeLayout(false)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents label7 As System.Windows.Forms.Label
    Friend WithEvents udcToMinutes As System.Windows.Forms.NumericUpDown
    Friend WithEvents label8 As System.Windows.Forms.Label
    Friend WithEvents udcToHours As System.Windows.Forms.NumericUpDown
    Friend WithEvents label9 As System.Windows.Forms.Label
    Friend WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents label5 As System.Windows.Forms.Label
    Friend WithEvents txtTZName As System.Windows.Forms.TextBox
    Friend WithEvents label4 As System.Windows.Forms.Label
    Friend WithEvents udcFromMinutes As System.Windows.Forms.NumericUpDown
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents udcFromHours As System.Windows.Forms.NumericUpDown
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents tabTimeZone As System.Windows.Forms.TabControl
    Friend WithEvents pgGeneral As System.Windows.Forms.TabPage
    Friend WithEvents pgRules As System.Windows.Forms.TabPage
    Friend WithEvents label10 As System.Windows.Forms.Label
    Friend WithEvents cboRuleType As System.Windows.Forms.ComboBox
    Friend WithEvents rcRulesDates As CalendarBrowser.RecurrenceControl

End Class
