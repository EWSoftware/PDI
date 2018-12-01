<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VTimeZoneTestForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VTimeZoneTestForm))
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.cboSourceTimeZone = New System.Windows.Forms.ComboBox()
        Me.dtpSourceDate = New System.Windows.Forms.DateTimePicker()
        Me.label1 = New System.Windows.Forms.Label()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblLocalBackToSource = New System.Windows.Forms.Label()
        Me.label5 = New System.Windows.Forms.Label()
        Me.lblLocalTime = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.groupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblDestBackToSource = New System.Windows.Forms.Label()
        Me.label8 = New System.Windows.Forms.Label()
        Me.lblDestTime = New System.Windows.Forms.Label()
        Me.label10 = New System.Windows.Forms.Label()
        Me.label6 = New System.Windows.Forms.Label()
        Me.cboDestTimeZone = New System.Windows.Forms.ComboBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.txtTimeZoneInfo = New System.Windows.Forms.TextBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.btnSaveTZs = New System.Windows.Forms.Button()
        Me.groupBox1.SuspendLayout
        Me.groupBox2.SuspendLayout
        Me.groupBox3.SuspendLayout
        Me.SuspendLayout
        '
        'groupBox1
        '
        Me.groupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.groupBox1.Controls.Add(Me.cboSourceTimeZone)
        Me.groupBox1.Controls.Add(Me.dtpSourceDate)
        Me.groupBox1.Controls.Add(Me.label1)
        Me.groupBox1.Location = New System.Drawing.Point(12, 12)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(904, 66)
        Me.groupBox1.TabIndex = 0
        Me.groupBox1.TabStop = false
        Me.groupBox1.Text = "Source Date"
        '
        'cboSourceTimeZone
        '
        Me.cboSourceTimeZone.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cboSourceTimeZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSourceTimeZone.Location = New System.Drawing.Point(366, 22)
        Me.cboSourceTimeZone.MaxDropDownItems = 16
        Me.cboSourceTimeZone.Name = "cboSourceTimeZone"
        Me.cboSourceTimeZone.Size = New System.Drawing.Size(503, 28)
        Me.cboSourceTimeZone.TabIndex = 2
        '
        'dtpSourceDate
        '
        Me.dtpSourceDate.Checked = false
        Me.dtpSourceDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
        Me.dtpSourceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSourceDate.Location = New System.Drawing.Point(125, 22)
        Me.dtpSourceDate.Name = "dtpSourceDate"
        Me.dtpSourceDate.Size = New System.Drawing.Size(235, 26)
        Me.dtpSourceDate.TabIndex = 1
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(8, 24)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(111, 23)
        Me.label1.TabIndex = 0
        Me.label1.Text = "&Date/Time"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'groupBox2
        '
        Me.groupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.groupBox2.Controls.Add(Me.lblLocalBackToSource)
        Me.groupBox2.Controls.Add(Me.label5)
        Me.groupBox2.Controls.Add(Me.lblLocalTime)
        Me.groupBox2.Controls.Add(Me.label2)
        Me.groupBox2.Location = New System.Drawing.Point(12, 84)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(904, 91)
        Me.groupBox2.TabIndex = 1
        Me.groupBox2.TabStop = false
        Me.groupBox2.Text = "To Local Time"
        '
        'lblLocalBackToSource
        '
        Me.lblLocalBackToSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLocalBackToSource.Location = New System.Drawing.Point(213, 56)
        Me.lblLocalBackToSource.Name = "lblLocalBackToSource"
        Me.lblLocalBackToSource.Size = New System.Drawing.Size(580, 23)
        Me.lblLocalBackToSource.TabIndex = 3
        Me.lblLocalBackToSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label5
        '
        Me.label5.Location = New System.Drawing.Point(6, 56)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(201, 23)
        Me.label5.TabIndex = 2
        Me.label5.Text = "Back to Source"
        Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLocalTime
        '
        Me.lblLocalTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLocalTime.Location = New System.Drawing.Point(213, 24)
        Me.lblLocalTime.Name = "lblLocalTime"
        Me.lblLocalTime.Size = New System.Drawing.Size(580, 23)
        Me.lblLocalTime.TabIndex = 1
        Me.lblLocalTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(6, 24)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(201, 23)
        Me.label2.TabIndex = 0
        Me.label2.Text = "Local Time"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'groupBox3
        '
        Me.groupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.groupBox3.Controls.Add(Me.lblDestBackToSource)
        Me.groupBox3.Controls.Add(Me.label8)
        Me.groupBox3.Controls.Add(Me.lblDestTime)
        Me.groupBox3.Controls.Add(Me.label10)
        Me.groupBox3.Controls.Add(Me.label6)
        Me.groupBox3.Controls.Add(Me.cboDestTimeZone)
        Me.groupBox3.Location = New System.Drawing.Point(12, 181)
        Me.groupBox3.Name = "groupBox3"
        Me.groupBox3.Size = New System.Drawing.Size(904, 137)
        Me.groupBox3.TabIndex = 2
        Me.groupBox3.TabStop = false
        Me.groupBox3.Text = "To Other Time Zone"
        '
        'lblDestBackToSource
        '
        Me.lblDestBackToSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDestBackToSource.Location = New System.Drawing.Point(213, 100)
        Me.lblDestBackToSource.Name = "lblDestBackToSource"
        Me.lblDestBackToSource.Size = New System.Drawing.Size(580, 23)
        Me.lblDestBackToSource.TabIndex = 5
        Me.lblDestBackToSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label8
        '
        Me.label8.Location = New System.Drawing.Point(8, 100)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(199, 23)
        Me.label8.TabIndex = 4
        Me.label8.Text = "Back to Source"
        Me.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDestTime
        '
        Me.lblDestTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDestTime.Location = New System.Drawing.Point(213, 67)
        Me.lblDestTime.Name = "lblDestTime"
        Me.lblDestTime.Size = New System.Drawing.Size(580, 23)
        Me.lblDestTime.TabIndex = 3
        Me.lblDestTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label10
        '
        Me.label10.Location = New System.Drawing.Point(8, 67)
        Me.label10.Name = "label10"
        Me.label10.Size = New System.Drawing.Size(199, 23)
        Me.label10.TabIndex = 2
        Me.label10.Text = "Dest. Time"
        Me.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label6
        '
        Me.label6.Location = New System.Drawing.Point(8, 32)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(199, 23)
        Me.label6.TabIndex = 0
        Me.label6.Text = "Destination &Time Zone"
        Me.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboDestTimeZone
        '
        Me.cboDestTimeZone.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cboDestTimeZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDestTimeZone.Location = New System.Drawing.Point(213, 30)
        Me.cboDestTimeZone.MaxDropDownItems = 16
        Me.cboDestTimeZone.Name = "cboDestTimeZone"
        Me.cboDestTimeZone.Size = New System.Drawing.Size(503, 28)
        Me.cboDestTimeZone.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(816, 463)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 32)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "&Close"
        '
        'txtTimeZoneInfo
        '
        Me.txtTimeZoneInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtTimeZoneInfo.Location = New System.Drawing.Point(12, 347)
        Me.txtTimeZoneInfo.Multiline = true
        Me.txtTimeZoneInfo.Name = "txtTimeZoneInfo"
        Me.txtTimeZoneInfo.ReadOnly = true
        Me.txtTimeZoneInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTimeZoneInfo.Size = New System.Drawing.Size(904, 108)
        Me.txtTimeZoneInfo.TabIndex = 4
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(8, 321)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(160, 23)
        Me.label3.TabIndex = 3
        Me.label3.Text = "Time Zone Settings"
        '
        'btnSaveTZs
        '
        Me.btnSaveTZs.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnSaveTZs.Location = New System.Drawing.Point(12, 463)
        Me.btnSaveTZs.Name = "btnSaveTZs"
        Me.btnSaveTZs.Size = New System.Drawing.Size(100, 32)
        Me.btnSaveTZs.TabIndex = 5
        Me.btnSaveTZs.Text = "&Save TZs"
        '
        'VTimeZoneTestForm
        '
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(928, 507)
        Me.Controls.Add(Me.btnSaveTZs)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.txtTimeZoneInfo)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.groupBox3)
        Me.Controls.Add(Me.groupBox2)
        Me.Controls.Add(Me.groupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MinimizeBox = false
        Me.MinimumSize = New System.Drawing.Size(777, 552)
        Me.Name = "VTimeZoneTestForm"
        Me.ShowInTaskbar = false
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Test Time Zone Features"
        Me.groupBox1.ResumeLayout(false)
        Me.groupBox2.ResumeLayout(false)
        Me.groupBox3.ResumeLayout(false)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub


    Friend WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents groupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents label5 As System.Windows.Forms.Label
    Friend WithEvents groupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents label8 As System.Windows.Forms.Label
    Friend WithEvents label10 As System.Windows.Forms.Label
    Friend WithEvents dtpSourceDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboSourceTimeZone As System.Windows.Forms.ComboBox
    Friend WithEvents cboDestTimeZone As System.Windows.Forms.ComboBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblLocalTime As System.Windows.Forms.Label
    Friend WithEvents lblLocalBackToSource As System.Windows.Forms.Label
    Friend WithEvents lblDestBackToSource As System.Windows.Forms.Label
    Friend WithEvents lblDestTime As System.Windows.Forms.Label
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents txtTimeZoneInfo As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveTZs As System.Windows.Forms.Button

End Class
