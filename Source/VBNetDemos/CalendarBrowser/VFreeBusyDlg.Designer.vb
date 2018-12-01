<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VFreeBusyDlg
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
        Me.components = New System.ComponentModel.Container()
        Me.txtUniqueId = New System.Windows.Forms.TextBox()
        Me.label9 = New System.Windows.Forms.Label()
        Me.tabInfo = New System.Windows.Forms.TabControl()
        Me.pgGeneral = New System.Windows.Forms.TabPage()
        Me.label2 = New System.Windows.Forms.Label()
        Me.txtDuration = New System.Windows.Forms.TextBox()
        Me.dtpEndDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.label4 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.label17 = New System.Windows.Forms.Label()
        Me.txtUrl = New System.Windows.Forms.TextBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.pgAttendees = New System.Windows.Forms.TabPage()
        Me.ucAttendees = New CalendarBrowser.AttendeeControl()
        Me.pgFreeBusy = New System.Windows.Forms.TabPage()
        Me.ucFreeBusy = New CalendarBrowser.FreeBusyControl()
        Me.pgReqStats = New System.Windows.Forms.TabPage()
        Me.ucRequestStatus = New CalendarBrowser.RequestStatusControl()
        Me.txtOrganizer = New System.Windows.Forms.TextBox()
        Me.label5 = New System.Windows.Forms.Label()
        Me.txtContact = New System.Windows.Forms.TextBox()
        Me.label6 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.epErrors = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.label7 = New System.Windows.Forms.Label()
        Me.cboTimeZone = New System.Windows.Forms.ComboBox()
        Me.btnApplyTZ = New System.Windows.Forms.Button()
        Me.tabInfo.SuspendLayout
        Me.pgGeneral.SuspendLayout
        Me.pgAttendees.SuspendLayout
        Me.pgFreeBusy.SuspendLayout
        Me.pgReqStats.SuspendLayout
        CType(Me.epErrors,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'txtUniqueId
        '
        Me.txtUniqueId.Location = New System.Drawing.Point(141, 12)
        Me.txtUniqueId.Name = "txtUniqueId"
        Me.txtUniqueId.ReadOnly = true
        Me.txtUniqueId.Size = New System.Drawing.Size(380, 26)
        Me.txtUniqueId.TabIndex = 1
        Me.txtUniqueId.TabStop = false
        '
        'label9
        '
        Me.label9.Location = New System.Drawing.Point(32, 14)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(103, 23)
        Me.label9.TabIndex = 0
        Me.label9.Text = "Unique ID"
        Me.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tabInfo
        '
        Me.tabInfo.Controls.Add(Me.pgGeneral)
        Me.tabInfo.Controls.Add(Me.pgAttendees)
        Me.tabInfo.Controls.Add(Me.pgFreeBusy)
        Me.tabInfo.Controls.Add(Me.pgReqStats)
        Me.tabInfo.Location = New System.Drawing.Point(12, 149)
        Me.tabInfo.Name = "tabInfo"
        Me.tabInfo.SelectedIndex = 0
        Me.tabInfo.Size = New System.Drawing.Size(742, 297)
        Me.tabInfo.TabIndex = 9
        '
        'pgGeneral
        '
        Me.pgGeneral.Controls.Add(Me.label2)
        Me.pgGeneral.Controls.Add(Me.txtDuration)
        Me.pgGeneral.Controls.Add(Me.dtpEndDate)
        Me.pgGeneral.Controls.Add(Me.dtpStartDate)
        Me.pgGeneral.Controls.Add(Me.label4)
        Me.pgGeneral.Controls.Add(Me.label1)
        Me.pgGeneral.Controls.Add(Me.txtComments)
        Me.pgGeneral.Controls.Add(Me.label17)
        Me.pgGeneral.Controls.Add(Me.txtUrl)
        Me.pgGeneral.Controls.Add(Me.label3)
        Me.pgGeneral.Location = New System.Drawing.Point(4, 29)
        Me.pgGeneral.Name = "pgGeneral"
        Me.pgGeneral.Size = New System.Drawing.Size(734, 264)
        Me.pgGeneral.TabIndex = 3
        Me.pgGeneral.Text = "General"
        Me.pgGeneral.UseVisualStyleBackColor = true
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(391, 16)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(54, 23)
        Me.label2.TabIndex = 2
        Me.label2.Text = "End"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDuration
        '
        Me.txtDuration.Location = New System.Drawing.Point(116, 56)
        Me.txtDuration.Name = "txtDuration"
        Me.txtDuration.Size = New System.Drawing.Size(116, 26)
        Me.txtDuration.TabIndex = 5
        '
        'dtpEndDate
        '
        Me.dtpEndDate.Checked = false
        Me.dtpEndDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndDate.Location = New System.Drawing.Point(451, 16)
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.ShowCheckBox = true
        Me.dtpEndDate.Size = New System.Drawing.Size(255, 26)
        Me.dtpEndDate.TabIndex = 3
        '
        'dtpStartDate
        '
        Me.dtpStartDate.Checked = false
        Me.dtpStartDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartDate.Location = New System.Drawing.Point(116, 16)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.ShowCheckBox = true
        Me.dtpStartDate.Size = New System.Drawing.Size(255, 26)
        Me.dtpStartDate.TabIndex = 1
        '
        'label4
        '
        Me.label4.Location = New System.Drawing.Point(16, 58)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(94, 23)
        Me.label4.TabIndex = 4
        Me.label4.Text = "Duration"
        Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(41, 16)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(69, 23)
        Me.label1.TabIndex = 0
        Me.label1.Text = "&Start"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtComments
        '
        Me.txtComments.AcceptsReturn = true
        Me.txtComments.Location = New System.Drawing.Point(116, 136)
        Me.txtComments.Multiline = true
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComments.Size = New System.Drawing.Size(590, 110)
        Me.txtComments.TabIndex = 9
        '
        'label17
        '
        Me.label17.Location = New System.Drawing.Point(10, 138)
        Me.label17.Name = "label17"
        Me.label17.Size = New System.Drawing.Size(100, 23)
        Me.label17.TabIndex = 8
        Me.label17.Text = "&Comments"
        Me.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtUrl
        '
        Me.txtUrl.Location = New System.Drawing.Point(116, 96)
        Me.txtUrl.Name = "txtUrl"
        Me.txtUrl.Size = New System.Drawing.Size(590, 26)
        Me.txtUrl.TabIndex = 7
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(47, 98)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(63, 23)
        Me.label3.TabIndex = 6
        Me.label3.Text = "&URL"
        Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pgAttendees
        '
        Me.pgAttendees.Controls.Add(Me.ucAttendees)
        Me.pgAttendees.Location = New System.Drawing.Point(4, 29)
        Me.pgAttendees.Name = "pgAttendees"
        Me.pgAttendees.Size = New System.Drawing.Size(734, 264)
        Me.pgAttendees.TabIndex = 0
        Me.pgAttendees.Text = "Attendees"
        Me.pgAttendees.UseVisualStyleBackColor = true
        '
        'ucAttendees
        '
        Me.ucAttendees.Location = New System.Drawing.Point(90, 25)
        Me.ucAttendees.Name = "ucAttendees"
        Me.ucAttendees.Size = New System.Drawing.Size(554, 214)
        Me.ucAttendees.TabIndex = 0
        '
        'pgFreeBusy
        '
        Me.pgFreeBusy.Controls.Add(Me.ucFreeBusy)
        Me.pgFreeBusy.Location = New System.Drawing.Point(4, 29)
        Me.pgFreeBusy.Name = "pgFreeBusy"
        Me.pgFreeBusy.Size = New System.Drawing.Size(734, 264)
        Me.pgFreeBusy.TabIndex = 1
        Me.pgFreeBusy.Text = "Free/Busy"
        Me.pgFreeBusy.UseVisualStyleBackColor = true
        '
        'ucFreeBusy
        '
        Me.ucFreeBusy.Location = New System.Drawing.Point(105, 50)
        Me.ucFreeBusy.Name = "ucFreeBusy"
        Me.ucFreeBusy.Size = New System.Drawing.Size(525, 164)
        Me.ucFreeBusy.TabIndex = 0
        '
        'pgReqStats
        '
        Me.pgReqStats.Controls.Add(Me.ucRequestStatus)
        Me.pgReqStats.Location = New System.Drawing.Point(4, 29)
        Me.pgReqStats.Name = "pgReqStats"
        Me.pgReqStats.Size = New System.Drawing.Size(734, 264)
        Me.pgReqStats.TabIndex = 2
        Me.pgReqStats.Text = "Request Status"
        Me.pgReqStats.UseVisualStyleBackColor = true
        '
        'ucRequestStatus
        '
        Me.ucRequestStatus.Location = New System.Drawing.Point(110, 56)
        Me.ucRequestStatus.Name = "ucRequestStatus"
        Me.ucRequestStatus.Size = New System.Drawing.Size(514, 152)
        Me.ucRequestStatus.TabIndex = 0
        '
        'txtOrganizer
        '
        Me.txtOrganizer.Location = New System.Drawing.Point(141, 83)
        Me.txtOrganizer.Name = "txtOrganizer"
        Me.txtOrganizer.Size = New System.Drawing.Size(572, 26)
        Me.txtOrganizer.TabIndex = 6
        '
        'label5
        '
        Me.label5.Location = New System.Drawing.Point(22, 83)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(113, 23)
        Me.label5.TabIndex = 5
        Me.label5.Text = "&Organizer"
        Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtContact
        '
        Me.txtContact.Location = New System.Drawing.Point(141, 115)
        Me.txtContact.Name = "txtContact"
        Me.txtContact.Size = New System.Drawing.Size(572, 26)
        Me.txtContact.TabIndex = 8
        '
        'label6
        '
        Me.label6.Location = New System.Drawing.Point(22, 115)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(113, 23)
        Me.label6.TabIndex = 7
        Me.label6.Text = "Contact"
        Me.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCancel
        '
        Me.btnCancel.CausesValidation = false
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(666, 452)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 32)
        Me.btnCancel.TabIndex = 11
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(12, 452)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 32)
        Me.btnOK.TabIndex = 10
        Me.btnOK.Text = "OK"
        '
        'epErrors
        '
        Me.epErrors.ContainerControl = Me
        '
        'label7
        '
        Me.label7.Location = New System.Drawing.Point(18, 49)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(117, 23)
        Me.label7.TabIndex = 2
        Me.label7.Text = "&Time Zone"
        Me.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboTimeZone
        '
        Me.cboTimeZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTimeZone.Location = New System.Drawing.Point(141, 49)
        Me.cboTimeZone.MaxDropDownItems = 16
        Me.cboTimeZone.Name = "cboTimeZone"
        Me.cboTimeZone.Size = New System.Drawing.Size(502, 28)
        Me.cboTimeZone.TabIndex = 3
        '
        'btnApplyTZ
        '
        Me.btnApplyTZ.Location = New System.Drawing.Point(649, 48)
        Me.btnApplyTZ.Name = "btnApplyTZ"
        Me.btnApplyTZ.Size = New System.Drawing.Size(64, 28)
        Me.btnApplyTZ.TabIndex = 4
        Me.btnApplyTZ.Text = "Apply"
        '
        'VFreeBusyDlg
        '
        Me.AcceptButton = Me.btnOK
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(766, 496)
        Me.Controls.Add(Me.btnApplyTZ)
        Me.Controls.Add(Me.label7)
        Me.Controls.Add(Me.cboTimeZone)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtContact)
        Me.Controls.Add(Me.txtOrganizer)
        Me.Controls.Add(Me.txtUniqueId)
        Me.Controls.Add(Me.label6)
        Me.Controls.Add(Me.label5)
        Me.Controls.Add(Me.tabInfo)
        Me.Controls.Add(Me.label9)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "VFreeBusyDlg"
        Me.ShowInTaskbar = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Free/Busy Properties"
        Me.tabInfo.ResumeLayout(false)
        Me.pgGeneral.ResumeLayout(false)
        Me.pgGeneral.PerformLayout
        Me.pgAttendees.ResumeLayout(false)
        Me.pgFreeBusy.ResumeLayout(false)
        Me.pgReqStats.ResumeLayout(false)
        CType(Me.epErrors,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents txtUniqueId As System.Windows.Forms.TextBox
    Friend WithEvents label9 As System.Windows.Forms.Label
    Friend WithEvents tabInfo As System.Windows.Forms.TabControl
    Friend WithEvents pgAttendees As System.Windows.Forms.TabPage
    Friend WithEvents pgFreeBusy As System.Windows.Forms.TabPage
    Friend WithEvents pgReqStats As System.Windows.Forms.TabPage
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents label17 As System.Windows.Forms.Label
    Friend WithEvents txtUrl As System.Windows.Forms.TextBox
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents txtOrganizer As System.Windows.Forms.TextBox
    Friend WithEvents label5 As System.Windows.Forms.Label
    Friend WithEvents txtContact As System.Windows.Forms.TextBox
    Friend WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents ucAttendees As CalendarBrowser.AttendeeControl
    Friend WithEvents ucFreeBusy As CalendarBrowser.FreeBusyControl
    Friend WithEvents ucRequestStatus As CalendarBrowser.RequestStatusControl
    Friend WithEvents pgGeneral As System.Windows.Forms.TabPage
    Friend WithEvents txtDuration As System.Windows.Forms.TextBox
    Friend WithEvents dtpEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents label4 As System.Windows.Forms.Label
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents epErrors As System.Windows.Forms.ErrorProvider
    Friend WithEvents label7 As System.Windows.Forms.Label
    Friend WithEvents cboTimeZone As System.Windows.Forms.ComboBox
    Friend WithEvents btnApplyTZ As System.Windows.Forms.Button

End Class
