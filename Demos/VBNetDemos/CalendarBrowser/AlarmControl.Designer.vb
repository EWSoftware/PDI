<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AlarmControl
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
        Me.tabInfo = New System.Windows.Forms.TabControl()
        Me.pgGeneral = New System.Windows.Forms.TabPage()
        Me.label3 = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.label11 = New System.Windows.Forms.Label()
        Me.txtSummary = New System.Windows.Forms.TextBox()
        Me.label8 = New System.Windows.Forms.Label()
        Me.txtDuration = New System.Windows.Forms.TextBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label15 = New System.Windows.Forms.Label()
        Me.udcRepeat = New System.Windows.Forms.NumericUpDown()
        Me.chkFromEnd = New System.Windows.Forms.CheckBox()
        Me.txtTrigger = New System.Windows.Forms.TextBox()
        Me.dtpTrigger = New System.Windows.Forms.DateTimePicker()
        Me.label1 = New System.Windows.Forms.Label()
        Me.txtOtherAction = New System.Windows.Forms.TextBox()
        Me.label4 = New System.Windows.Forms.Label()
        Me.cboAction = New System.Windows.Forms.ComboBox()
        Me.label10 = New System.Windows.Forms.Label()
        Me.pgAttendees = New System.Windows.Forms.TabPage()
        Me.ucAttendees = New CalendarBrowser.AttendeeControl()
        Me.pgAttachments = New System.Windows.Forms.TabPage()
        Me.ucAttachments = New CalendarBrowser.AttachmentsControl()
        Me.tabInfo.SuspendLayout
        Me.pgGeneral.SuspendLayout
        CType(Me.udcRepeat,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pgAttendees.SuspendLayout
        Me.pgAttachments.SuspendLayout
        Me.SuspendLayout
        '
        'tabInfo
        '
        Me.tabInfo.Controls.Add(Me.pgGeneral)
        Me.tabInfo.Controls.Add(Me.pgAttendees)
        Me.tabInfo.Controls.Add(Me.pgAttachments)
        Me.tabInfo.Location = New System.Drawing.Point(8, 8)
        Me.tabInfo.Name = "tabInfo"
        Me.tabInfo.SelectedIndex = 0
        Me.tabInfo.Size = New System.Drawing.Size(562, 272)
        Me.tabInfo.TabIndex = 0
        '
        'pgGeneral
        '
        Me.pgGeneral.Controls.Add(Me.label3)
        Me.pgGeneral.Controls.Add(Me.txtDescription)
        Me.pgGeneral.Controls.Add(Me.label11)
        Me.pgGeneral.Controls.Add(Me.txtSummary)
        Me.pgGeneral.Controls.Add(Me.label8)
        Me.pgGeneral.Controls.Add(Me.txtDuration)
        Me.pgGeneral.Controls.Add(Me.label2)
        Me.pgGeneral.Controls.Add(Me.label15)
        Me.pgGeneral.Controls.Add(Me.udcRepeat)
        Me.pgGeneral.Controls.Add(Me.chkFromEnd)
        Me.pgGeneral.Controls.Add(Me.txtTrigger)
        Me.pgGeneral.Controls.Add(Me.dtpTrigger)
        Me.pgGeneral.Controls.Add(Me.label1)
        Me.pgGeneral.Controls.Add(Me.txtOtherAction)
        Me.pgGeneral.Controls.Add(Me.label4)
        Me.pgGeneral.Controls.Add(Me.cboAction)
        Me.pgGeneral.Controls.Add(Me.label10)
        Me.pgGeneral.Location = New System.Drawing.Point(4, 25)
        Me.pgGeneral.Name = "pgGeneral"
        Me.pgGeneral.Size = New System.Drawing.Size(554, 243)
        Me.pgGeneral.TabIndex = 0
        Me.pgGeneral.Text = "General"
        Me.pgGeneral.UseVisualStyleBackColor = true
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(20, 72)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(104, 23)
        Me.label3.TabIndex = 16
        Me.label3.Text = "or Date/Time"
        Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDescription
        '
        Me.txtDescription.AcceptsReturn = true
        Me.txtDescription.Location = New System.Drawing.Point(130, 168)
        Me.txtDescription.Multiline = true
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDescription.Size = New System.Drawing.Size(384, 64)
        Me.txtDescription.TabIndex = 15
        '
        'label11
        '
        Me.label11.Location = New System.Drawing.Point(25, 168)
        Me.label11.Name = "label11"
        Me.label11.Size = New System.Drawing.Size(99, 23)
        Me.label11.TabIndex = 14
        Me.label11.Text = "Description"
        Me.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSummary
        '
        Me.txtSummary.Location = New System.Drawing.Point(130, 136)
        Me.txtSummary.Name = "txtSummary"
        Me.txtSummary.Size = New System.Drawing.Size(384, 22)
        Me.txtSummary.TabIndex = 13
        '
        'label8
        '
        Me.label8.Location = New System.Drawing.Point(38, 136)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(86, 23)
        Me.label8.TabIndex = 12
        Me.label8.Text = "Summary"
        Me.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDuration
        '
        Me.txtDuration.Location = New System.Drawing.Point(284, 103)
        Me.txtDuration.Name = "txtDuration"
        Me.txtDuration.Size = New System.Drawing.Size(108, 22)
        Me.txtDuration.TabIndex = 11
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(202, 103)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(76, 23)
        Me.label2.TabIndex = 10
        Me.label2.Text = "Duration"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label15
        '
        Me.label15.Location = New System.Drawing.Point(47, 103)
        Me.label15.Name = "label15"
        Me.label15.Size = New System.Drawing.Size(77, 23)
        Me.label15.TabIndex = 7
        Me.label15.Text = "Repeat"
        Me.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'udcRepeat
        '
        Me.udcRepeat.Location = New System.Drawing.Point(130, 104)
        Me.udcRepeat.Maximum = New Decimal(New Integer() {32767, 0, 0, 0})
        Me.udcRepeat.Name = "udcRepeat"
        Me.udcRepeat.Size = New System.Drawing.Size(64, 22)
        Me.udcRepeat.TabIndex = 8
        Me.udcRepeat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkFromEnd
        '
        Me.chkFromEnd.Location = New System.Drawing.Point(248, 40)
        Me.chkFromEnd.Name = "chkFromEnd"
        Me.chkFromEnd.Size = New System.Drawing.Size(280, 24)
        Me.chkFromEnd.TabIndex = 9
        Me.chkFromEnd.Text = "Duration Trigger is relative to end time"
        '
        'txtTrigger
        '
        Me.txtTrigger.Location = New System.Drawing.Point(130, 40)
        Me.txtTrigger.Name = "txtTrigger"
        Me.txtTrigger.Size = New System.Drawing.Size(108, 22)
        Me.txtTrigger.TabIndex = 5
        '
        'dtpTrigger
        '
        Me.dtpTrigger.Checked = false
        Me.dtpTrigger.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
        Me.dtpTrigger.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTrigger.Location = New System.Drawing.Point(130, 72)
        Me.dtpTrigger.Name = "dtpTrigger"
        Me.dtpTrigger.ShowCheckBox = true
        Me.dtpTrigger.Size = New System.Drawing.Size(210, 22)
        Me.dtpTrigger.TabIndex = 6
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(5, 40)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(119, 23)
        Me.label1.TabIndex = 4
        Me.label1.Text = "Duration Trigger"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOtherAction
        '
        Me.txtOtherAction.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtOtherAction.Location = New System.Drawing.Point(346, 8)
        Me.txtOtherAction.Name = "txtOtherAction"
        Me.txtOtherAction.Size = New System.Drawing.Size(136, 22)
        Me.txtOtherAction.TabIndex = 3
        '
        'label4
        '
        Me.label4.Location = New System.Drawing.Point(250, 8)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(90, 23)
        Me.label4.TabIndex = 2
        Me.label4.Text = "Other Action"
        Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboAction
        '
        Me.cboAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAction.Items.AddRange(New Object() {"Audio", "Display", "E-Mail", "Procedure", "Other"})
        Me.cboAction.Location = New System.Drawing.Point(130, 8)
        Me.cboAction.Name = "cboAction"
        Me.cboAction.Size = New System.Drawing.Size(112, 24)
        Me.cboAction.TabIndex = 1
        '
        'label10
        '
        Me.label10.Location = New System.Drawing.Point(63, 8)
        Me.label10.Name = "label10"
        Me.label10.Size = New System.Drawing.Size(61, 23)
        Me.label10.TabIndex = 0
        Me.label10.Text = "Action"
        Me.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pgAttendees
        '
        Me.pgAttendees.Controls.Add(Me.ucAttendees)
        Me.pgAttendees.Location = New System.Drawing.Point(4, 25)
        Me.pgAttendees.Name = "pgAttendees"
        Me.pgAttendees.Size = New System.Drawing.Size(554, 243)
        Me.pgAttendees.TabIndex = 1
        Me.pgAttendees.Text = "Attendees"
        Me.pgAttendees.UseVisualStyleBackColor = true
        '
        'ucAttendees
        '
        Me.ucAttendees.Location = New System.Drawing.Point(22, 17)
        Me.ucAttendees.Name = "ucAttendees"
        Me.ucAttendees.Size = New System.Drawing.Size(510, 208)
        Me.ucAttendees.TabIndex = 0
        '
        'pgAttachments
        '
        Me.pgAttachments.Controls.Add(Me.ucAttachments)
        Me.pgAttachments.Location = New System.Drawing.Point(4, 25)
        Me.pgAttachments.Name = "pgAttachments"
        Me.pgAttachments.Size = New System.Drawing.Size(554, 243)
        Me.pgAttachments.TabIndex = 2
        Me.pgAttachments.Text = "Attachments"
        Me.pgAttachments.UseVisualStyleBackColor = true
        '
        'ucAttachments
        '
        Me.ucAttachments.Location = New System.Drawing.Point(6, 8)
        Me.ucAttachments.Name = "ucAttachments"
        Me.ucAttachments.Size = New System.Drawing.Size(543, 226)
        Me.ucAttachments.TabIndex = 0
        '
        'AlarmControl
        '
        Me.Controls.Add(Me.tabInfo)
        Me.Name = "AlarmControl"
        Me.Size = New System.Drawing.Size(584, 312)
        Me.Controls.SetChildIndex(Me.tabInfo, 0)
        Me.tabInfo.ResumeLayout(false)
        Me.pgGeneral.ResumeLayout(false)
        Me.pgGeneral.PerformLayout
        CType(Me.udcRepeat,System.ComponentModel.ISupportInitialize).EndInit
        Me.pgAttendees.ResumeLayout(false)
        Me.pgAttachments.ResumeLayout(false)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents tabInfo As System.Windows.Forms.TabControl
    Friend WithEvents pgGeneral As System.Windows.Forms.TabPage
    Friend WithEvents pgAttendees As System.Windows.Forms.TabPage
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents label11 As System.Windows.Forms.Label
    Friend WithEvents txtSummary As System.Windows.Forms.TextBox
    Friend WithEvents label8 As System.Windows.Forms.Label
    Friend WithEvents txtDuration As System.Windows.Forms.TextBox
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents label15 As System.Windows.Forms.Label
    Friend WithEvents udcRepeat As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkFromEnd As System.Windows.Forms.CheckBox
    Friend WithEvents txtTrigger As System.Windows.Forms.TextBox
    Friend WithEvents dtpTrigger As System.Windows.Forms.DateTimePicker
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents txtOtherAction As System.Windows.Forms.TextBox
    Friend WithEvents label4 As System.Windows.Forms.Label
    Friend WithEvents cboAction As System.Windows.Forms.ComboBox
    Friend WithEvents label10 As System.Windows.Forms.Label
    Friend WithEvents pgAttachments As System.Windows.Forms.TabPage
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents ucAttendees As CalendarBrowser.AttendeeControl
    Friend WithEvents ucAttachments As CalendarBrowser.AttachmentsControl

End Class
