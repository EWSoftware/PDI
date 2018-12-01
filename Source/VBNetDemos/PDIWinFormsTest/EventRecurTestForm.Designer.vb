<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EventRecurTestForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EventRecurTestForm))
        Me.label3 = New System.Windows.Forms.Label()
        Me.txtCalendar = New System.Windows.Forms.TextBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lbDates = New System.Windows.Forms.ListBox()
        Me.btnTest = New System.Windows.Forms.Button()
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.label1 = New System.Windows.Forms.Label()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.dtpEndDate = New System.Windows.Forms.DateTimePicker()
        Me.label2 = New System.Windows.Forms.Label()
        Me.chkInLocalTime = New System.Windows.Forms.CheckBox()
        Me.label6 = New System.Windows.Forms.Label()
        Me.cboTimeZone = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(14, 118)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(417, 23)
        Me.label3.TabIndex = 7
        Me.label3.Text = "&Enter a VEVENT, VTODO, or VJOURNAL entry below."
        Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCalendar
        '
        Me.txtCalendar.AcceptsReturn = true
        Me.txtCalendar.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtCalendar.Location = New System.Drawing.Point(18, 145)
        Me.txtCalendar.Multiline = true
        Me.txtCalendar.Name = "txtCalendar"
        Me.txtCalendar.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtCalendar.Size = New System.Drawing.Size(413, 379)
        Me.txtCalendar.TabIndex = 8
        Me.txtCalendar.WordWrap = false
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(878, 530)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(88, 32)
        Me.btnClose.TabIndex = 12
        Me.btnClose.Text = "&Close"
        '
        'lbDates
        '
        Me.lbDates.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lbDates.ItemHeight = 20
        Me.lbDates.Location = New System.Drawing.Point(437, 177)
        Me.lbDates.Name = "lbDates"
        Me.lbDates.Size = New System.Drawing.Size(529, 344)
        Me.lbDates.TabIndex = 10
        '
        'btnTest
        '
        Me.btnTest.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnTest.Location = New System.Drawing.Point(784, 530)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(88, 32)
        Me.btnTest.TabIndex = 11
        Me.btnTest.Text = "&Test"
        '
        'dtpStartDate
        '
        Me.dtpStartDate.Checked = false
        Me.dtpStartDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartDate.Location = New System.Drawing.Point(329, 12)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.Size = New System.Drawing.Size(235, 26)
        Me.dtpStartDate.TabIndex = 1
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(124, 12)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(199, 23)
        Me.label1.TabIndex = 0
        Me.label1.Text = "&Find instances between"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCount
        '
        Me.lblCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCount.Location = New System.Drawing.Point(437, 118)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(529, 56)
        Me.lblCount.TabIndex = 9
        Me.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpEndDate
        '
        Me.dtpEndDate.Checked = false
        Me.dtpEndDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndDate.Location = New System.Drawing.Point(627, 12)
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.Size = New System.Drawing.Size(235, 26)
        Me.dtpEndDate.TabIndex = 3
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(570, 12)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(51, 23)
        Me.label2.TabIndex = 2
        Me.label2.Text = "and"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkInLocalTime
        '
        Me.chkInLocalTime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.chkInLocalTime.Location = New System.Drawing.Point(329, 44)
        Me.chkInLocalTime.Name = "chkInLocalTime"
        Me.chkInLocalTime.Size = New System.Drawing.Size(310, 24)
        Me.chkInLocalTime.TabIndex = 4
        Me.chkInLocalTime.Text = "Find instances in local time"
        '
        'label6
        '
        Me.label6.Location = New System.Drawing.Point(10, 76)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(313, 23)
        Me.label6.TabIndex = 5
        Me.label6.Text = "&Apply this time zone to the component"
        Me.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboTimeZone
        '
        Me.cboTimeZone.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cboTimeZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTimeZone.Location = New System.Drawing.Point(329, 74)
        Me.cboTimeZone.MaxDropDownItems = 16
        Me.cboTimeZone.Name = "cboTimeZone"
        Me.cboTimeZone.Size = New System.Drawing.Size(568, 28)
        Me.cboTimeZone.TabIndex = 6
        '
        'EventRecurTestForm
        '
        Me.AcceptButton = Me.btnTest
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(978, 574)
        Me.Controls.Add(Me.chkInLocalTime)
        Me.Controls.Add(Me.label6)
        Me.Controls.Add(Me.cboTimeZone)
        Me.Controls.Add(Me.dtpEndDate)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.dtpStartDate)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.btnTest)
        Me.Controls.Add(Me.lbDates)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.txtCalendar)
        Me.Controls.Add(Me.label3)
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MinimizeBox = false
        Me.MinimumSize = New System.Drawing.Size(1000, 575)
        Me.Name = "EventRecurTestForm"
        Me.ShowInTaskbar = false
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Test Calendar Event Recurrence"
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lbDates As System.Windows.Forms.ListBox
    Friend WithEvents btnTest As System.Windows.Forms.Button
    Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents txtCalendar As System.Windows.Forms.TextBox
    Friend WithEvents dtpEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents chkInLocalTime As System.Windows.Forms.CheckBox
    Friend WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents cboTimeZone As System.Windows.Forms.ComboBox

End Class
