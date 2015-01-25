<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RRuleTestForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RRuleTestForm))
        Me.label3 = New System.Windows.Forms.Label()
        Me.txtRRULE = New System.Windows.Forms.TextBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lbDates = New System.Windows.Forms.ListBox()
        Me.btnTest = New System.Windows.Forms.Button()
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.label1 = New System.Windows.Forms.Label()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.hmHolidays = New EWSoftware.PDI.Windows.Forms.HolidayManager()
        Me.btnDesign = New System.Windows.Forms.Button()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnDescribe = New System.Windows.Forms.Button()
        Me.groupBox1.SuspendLayout
        Me.SuspendLayout
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(14, 41)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(56, 23)
        Me.label3.TabIndex = 2
        Me.label3.Text = "RR&ULE"
        Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRRULE
        '
        Me.txtRRULE.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtRRULE.Location = New System.Drawing.Point(76, 41)
        Me.txtRRULE.Multiline = true
        Me.txtRRULE.Name = "txtRRULE"
        Me.txtRRULE.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRRULE.Size = New System.Drawing.Size(590, 64)
        Me.txtRRULE.TabIndex = 3
        Me.txtRRULE.Text = "FREQ=DAILY;INTERVAL=5;COUNT=50"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(674, 456)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(88, 32)
        Me.btnClose.TabIndex = 10
        Me.btnClose.Text = "&Close"
        '
        'lbDates
        '
        Me.lbDates.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lbDates.ItemHeight = 16
        Me.lbDates.Location = New System.Drawing.Point(12, 169)
        Me.lbDates.Name = "lbDates"
        Me.lbDates.Size = New System.Drawing.Size(386, 276)
        Me.lbDates.TabIndex = 7
        '
        'btnTest
        '
        Me.btnTest.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnTest.Location = New System.Drawing.Point(580, 456)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(88, 32)
        Me.btnTest.TabIndex = 9
        Me.btnTest.Text = "&Test"
        '
        'dtpStartDate
        '
        Me.dtpStartDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartDate.Location = New System.Drawing.Point(76, 12)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.Size = New System.Drawing.Size(190, 22)
        Me.dtpStartDate.TabIndex = 1
        Me.dtpStartDate.Value = New Date(2004, 9, 6, 0, 0, 0, 0)
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(21, 12)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(49, 23)
        Me.label1.TabIndex = 0
        Me.label1.Text = "&Start"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCount
        '
        Me.lblCount.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCount.Location = New System.Drawing.Point(12, 112)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(750, 48)
        Me.lblCount.TabIndex = 6
        Me.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'hmHolidays
        '
        Me.hmHolidays.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.hmHolidays.Location = New System.Drawing.Point(8, 24)
        Me.hmHolidays.Name = "hmHolidays"
        Me.hmHolidays.ShowLoadSaveControls = false
        Me.hmHolidays.Size = New System.Drawing.Size(342, 256)
        Me.hmHolidays.TabIndex = 0
        '
        'btnDesign
        '
        Me.btnDesign.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnDesign.Location = New System.Drawing.Point(674, 38)
        Me.btnDesign.Name = "btnDesign"
        Me.btnDesign.Size = New System.Drawing.Size(88, 32)
        Me.btnDesign.TabIndex = 4
        Me.btnDesign.Text = "Desig&n"
        '
        'groupBox1
        '
        Me.groupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.groupBox1.Controls.Add(Me.hmHolidays)
        Me.groupBox1.Location = New System.Drawing.Point(404, 157)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(358, 288)
        Me.groupBox1.TabIndex = 8
        Me.groupBox1.TabStop = false
        Me.groupBox1.Text = "H&olidays"
        '
        'btnDescribe
        '
        Me.btnDescribe.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnDescribe.Location = New System.Drawing.Point(674, 76)
        Me.btnDescribe.Name = "btnDescribe"
        Me.btnDescribe.Size = New System.Drawing.Size(88, 32)
        Me.btnDescribe.TabIndex = 5
        Me.btnDescribe.Text = "Descri&be"
        '
        'RRuleTestForm
        '
        Me.AcceptButton = Me.btnTest
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(774, 500)
        Me.Controls.Add(Me.btnDescribe)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.btnDesign)
        Me.Controls.Add(Me.dtpStartDate)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.txtRRULE)
        Me.Controls.Add(Me.btnTest)
        Me.Controls.Add(Me.lbDates)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.lblCount)
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MinimizeBox = false
        Me.MinimumSize = New System.Drawing.Size(768, 528)
        Me.Name = "RRuleTestForm"
        Me.ShowInTaskbar = false
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Test Recurrence Rule Parsing and Generation"
        Me.groupBox1.ResumeLayout(false)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents txtRRULE As System.Windows.Forms.TextBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lbDates As System.Windows.Forms.ListBox
    Friend WithEvents btnTest As System.Windows.Forms.Button
    Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents hmHolidays As EWSoftware.PDI.Windows.Forms.HolidayManager
    Friend WithEvents btnDesign As System.Windows.Forms.Button
    Friend WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnDescribe As System.Windows.Forms.Button

End Class
