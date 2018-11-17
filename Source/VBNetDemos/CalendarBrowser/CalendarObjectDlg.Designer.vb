<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CalendarObjectDlg
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
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.txtUniqueId = New System.Windows.Forms.TextBox()
        Me.tabInfo = New System.Windows.Forms.TabControl()
        Me.pgGeneral = New System.Windows.Forms.TabPage()
        Me.txtLocation = New System.Windows.Forms.TextBox()
        Me.lblLocation = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.label18 = New System.Windows.Forms.Label()
        Me.txtResources = New System.Windows.Forms.TextBox()
        Me.lblResources = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.label11 = New System.Windows.Forms.Label()
        Me.txtSummary = New System.Windows.Forms.TextBox()
        Me.label8 = New System.Windows.Forms.Label()
        Me.txtCategories = New System.Windows.Forms.TextBox()
        Me.label21 = New System.Windows.Forms.Label()
        Me.lblEndDate = New System.Windows.Forms.Label()
        Me.txtDuration = New System.Windows.Forms.TextBox()
        Me.dtpEndDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.lblDuration = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.lblCompleted = New System.Windows.Forms.Label()
        Me.dtpCompleted = New System.Windows.Forms.DateTimePicker()
        Me.lblPercent = New System.Windows.Forms.Label()
        Me.udcPercent = New System.Windows.Forms.NumericUpDown()
        Me.pgAttendees = New System.Windows.Forms.TabPage()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtOrganizer = New System.Windows.Forms.TextBox()
        Me.label5 = New System.Windows.Forms.Label()
        Me.ucAttendees = New CalendarBrowser.AttendeeControl()
        Me.pgRecurrence = New System.Windows.Forms.TabPage()
        Me.ucRecurrences = New CalendarBrowser.RecurrenceControl()
        Me.pgExceptions = New System.Windows.Forms.TabPage()
        Me.ucExceptions = New CalendarBrowser.RecurrenceControl()
        Me.pgAttachments = New System.Windows.Forms.TabPage()
        Me.ucAttachments = New CalendarBrowser.AttachmentsControl()
        Me.pgAlarms = New System.Windows.Forms.TabPage()
        Me.ucAlarms = New CalendarBrowser.AlarmControl()
        Me.pgMisc = New System.Windows.Forms.TabPage()
        Me.groupBox3 = New System.Windows.Forms.GroupBox()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.ucRequestStatus = New CalendarBrowser.RequestStatusControl()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.txtLongitude = New System.Windows.Forms.TextBox()
        Me.lblLongitude = New System.Windows.Forms.Label()
        Me.txtLatitude = New System.Windows.Forms.TextBox()
        Me.lblLatitude = New System.Windows.Forms.Label()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.label17 = New System.Windows.Forms.Label()
        Me.txtUrl = New System.Windows.Forms.TextBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.label9 = New System.Windows.Forms.Label()
        Me.txtClass = New System.Windows.Forms.TextBox()
        Me.label10 = New System.Windows.Forms.Label()
        Me.txtLastModified = New System.Windows.Forms.TextBox()
        Me.label6 = New System.Windows.Forms.Label()
        Me.txtCreated = New System.Windows.Forms.TextBox()
        Me.label7 = New System.Windows.Forms.Label()
        Me.udcSequence = New System.Windows.Forms.NumericUpDown()
        Me.label15 = New System.Windows.Forms.Label()
        Me.lblPriority = New System.Windows.Forms.Label()
        Me.udcPriority = New System.Windows.Forms.NumericUpDown()
        Me.chkTransparent = New System.Windows.Forms.CheckBox()
        Me.epErrors = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.label2 = New System.Windows.Forms.Label()
        Me.cboTimeZone = New System.Windows.Forms.ComboBox()
        Me.btnApplyTZ = New System.Windows.Forms.Button()
        Me.tabInfo.SuspendLayout
        Me.pgGeneral.SuspendLayout
        CType(Me.udcPercent,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pgAttendees.SuspendLayout
        Me.pgRecurrence.SuspendLayout
        Me.pgExceptions.SuspendLayout
        Me.pgAttachments.SuspendLayout
        Me.pgAlarms.SuspendLayout
        Me.pgMisc.SuspendLayout
        CType(Me.udcSequence,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.udcPriority,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.epErrors,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'btnCancel
        '
        Me.btnCancel.CausesValidation = false
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(542, 494)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 32)
        Me.btnCancel.TabIndex = 18
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(12, 494)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 32)
        Me.btnOK.TabIndex = 17
        Me.btnOK.Text = "OK"
        '
        'txtUniqueId
        '
        Me.txtUniqueId.Location = New System.Drawing.Point(112, 12)
        Me.txtUniqueId.Name = "txtUniqueId"
        Me.txtUniqueId.ReadOnly = true
        Me.txtUniqueId.Size = New System.Drawing.Size(376, 22)
        Me.txtUniqueId.TabIndex = 1
        Me.txtUniqueId.TabStop = false
        '
        'tabInfo
        '
        Me.tabInfo.Controls.Add(Me.pgGeneral)
        Me.tabInfo.Controls.Add(Me.pgAttendees)
        Me.tabInfo.Controls.Add(Me.pgRecurrence)
        Me.tabInfo.Controls.Add(Me.pgExceptions)
        Me.tabInfo.Controls.Add(Me.pgAttachments)
        Me.tabInfo.Controls.Add(Me.pgAlarms)
        Me.tabInfo.Controls.Add(Me.pgMisc)
        Me.tabInfo.Location = New System.Drawing.Point(12, 136)
        Me.tabInfo.Name = "tabInfo"
        Me.tabInfo.SelectedIndex = 0
        Me.tabInfo.Size = New System.Drawing.Size(618, 352)
        Me.tabInfo.TabIndex = 16
        '
        'pgGeneral
        '
        Me.pgGeneral.Controls.Add(Me.txtLocation)
        Me.pgGeneral.Controls.Add(Me.lblLocation)
        Me.pgGeneral.Controls.Add(Me.cboStatus)
        Me.pgGeneral.Controls.Add(Me.label18)
        Me.pgGeneral.Controls.Add(Me.txtResources)
        Me.pgGeneral.Controls.Add(Me.lblResources)
        Me.pgGeneral.Controls.Add(Me.txtDescription)
        Me.pgGeneral.Controls.Add(Me.label11)
        Me.pgGeneral.Controls.Add(Me.txtSummary)
        Me.pgGeneral.Controls.Add(Me.label8)
        Me.pgGeneral.Controls.Add(Me.txtCategories)
        Me.pgGeneral.Controls.Add(Me.label21)
        Me.pgGeneral.Controls.Add(Me.lblEndDate)
        Me.pgGeneral.Controls.Add(Me.txtDuration)
        Me.pgGeneral.Controls.Add(Me.dtpEndDate)
        Me.pgGeneral.Controls.Add(Me.dtpStartDate)
        Me.pgGeneral.Controls.Add(Me.lblDuration)
        Me.pgGeneral.Controls.Add(Me.label1)
        Me.pgGeneral.Controls.Add(Me.lblCompleted)
        Me.pgGeneral.Controls.Add(Me.dtpCompleted)
        Me.pgGeneral.Controls.Add(Me.lblPercent)
        Me.pgGeneral.Controls.Add(Me.udcPercent)
        Me.pgGeneral.Location = New System.Drawing.Point(4, 25)
        Me.pgGeneral.Name = "pgGeneral"
        Me.pgGeneral.Size = New System.Drawing.Size(610, 323)
        Me.pgGeneral.TabIndex = 3
        Me.pgGeneral.Text = "General"
        Me.pgGeneral.UseVisualStyleBackColor = true
        '
        'txtLocation
        '
        Me.txtLocation.Location = New System.Drawing.Point(112, 152)
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(424, 22)
        Me.txtLocation.TabIndex = 15
        '
        'lblLocation
        '
        Me.lblLocation.Location = New System.Drawing.Point(29, 152)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(77, 23)
        Me.lblLocation.TabIndex = 14
        Me.lblLocation.Text = "Location"
        Me.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Location = New System.Drawing.Point(112, 48)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(116, 24)
        Me.cboStatus.TabIndex = 5
        '
        'label18
        '
        Me.label18.Location = New System.Drawing.Point(51, 48)
        Me.label18.Name = "label18"
        Me.label18.Size = New System.Drawing.Size(55, 23)
        Me.label18.TabIndex = 4
        Me.label18.Text = "Status"
        Me.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtResources
        '
        Me.txtResources.Location = New System.Drawing.Point(112, 288)
        Me.txtResources.Name = "txtResources"
        Me.txtResources.Size = New System.Drawing.Size(440, 22)
        Me.txtResources.TabIndex = 21
        '
        'lblResources
        '
        Me.lblResources.Location = New System.Drawing.Point(19, 288)
        Me.lblResources.Name = "lblResources"
        Me.lblResources.Size = New System.Drawing.Size(87, 23)
        Me.lblResources.TabIndex = 20
        Me.lblResources.Text = "Resources"
        Me.lblResources.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDescription
        '
        Me.txtDescription.AcceptsReturn = true
        Me.txtDescription.Location = New System.Drawing.Point(112, 184)
        Me.txtDescription.Multiline = true
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDescription.Size = New System.Drawing.Size(424, 56)
        Me.txtDescription.TabIndex = 17
        '
        'label11
        '
        Me.label11.Location = New System.Drawing.Point(19, 184)
        Me.label11.Name = "label11"
        Me.label11.Size = New System.Drawing.Size(87, 23)
        Me.label11.TabIndex = 16
        Me.label11.Text = "Description"
        Me.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSummary
        '
        Me.txtSummary.Location = New System.Drawing.Point(112, 120)
        Me.txtSummary.Name = "txtSummary"
        Me.txtSummary.Size = New System.Drawing.Size(424, 22)
        Me.txtSummary.TabIndex = 13
        '
        'label8
        '
        Me.label8.Location = New System.Drawing.Point(27, 120)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(79, 23)
        Me.label8.TabIndex = 12
        Me.label8.Text = "Summary"
        Me.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCategories
        '
        Me.txtCategories.Location = New System.Drawing.Point(112, 256)
        Me.txtCategories.Name = "txtCategories"
        Me.txtCategories.Size = New System.Drawing.Size(440, 22)
        Me.txtCategories.TabIndex = 19
        '
        'label21
        '
        Me.label21.Location = New System.Drawing.Point(19, 256)
        Me.label21.Name = "label21"
        Me.label21.Size = New System.Drawing.Size(87, 23)
        Me.label21.TabIndex = 18
        Me.label21.Text = "&Categories"
        Me.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblEndDate
        '
        Me.lblEndDate.Location = New System.Drawing.Point(338, 16)
        Me.lblEndDate.Name = "lblEndDate"
        Me.lblEndDate.Size = New System.Drawing.Size(40, 23)
        Me.lblEndDate.TabIndex = 2
        Me.lblEndDate.Text = "End"
        Me.lblEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDuration
        '
        Me.txtDuration.Location = New System.Drawing.Point(384, 48)
        Me.txtDuration.Name = "txtDuration"
        Me.txtDuration.Size = New System.Drawing.Size(80, 22)
        Me.txtDuration.TabIndex = 7
        '
        'dtpEndDate
        '
        Me.dtpEndDate.Checked = false
        Me.dtpEndDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndDate.Location = New System.Drawing.Point(384, 16)
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.ShowCheckBox = true
        Me.dtpEndDate.Size = New System.Drawing.Size(210, 22)
        Me.dtpEndDate.TabIndex = 3
        '
        'dtpStartDate
        '
        Me.dtpStartDate.Checked = false
        Me.dtpStartDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartDate.Location = New System.Drawing.Point(112, 16)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.ShowCheckBox = true
        Me.dtpStartDate.Size = New System.Drawing.Size(210, 22)
        Me.dtpStartDate.TabIndex = 1
        '
        'lblDuration
        '
        Me.lblDuration.Location = New System.Drawing.Point(309, 48)
        Me.lblDuration.Name = "lblDuration"
        Me.lblDuration.Size = New System.Drawing.Size(69, 23)
        Me.lblDuration.TabIndex = 6
        Me.lblDuration.Text = "Duration"
        Me.lblDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(61, 16)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(45, 23)
        Me.label1.TabIndex = 0
        Me.label1.Text = "&Start"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCompleted
        '
        Me.lblCompleted.Location = New System.Drawing.Point(291, 79)
        Me.lblCompleted.Name = "lblCompleted"
        Me.lblCompleted.Size = New System.Drawing.Size(87, 23)
        Me.lblCompleted.TabIndex = 10
        Me.lblCompleted.Text = "Completed"
        Me.lblCompleted.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpCompleted
        '
        Me.dtpCompleted.Checked = false
        Me.dtpCompleted.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
        Me.dtpCompleted.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpCompleted.Location = New System.Drawing.Point(384, 79)
        Me.dtpCompleted.Name = "dtpCompleted"
        Me.dtpCompleted.ShowCheckBox = true
        Me.dtpCompleted.Size = New System.Drawing.Size(210, 22)
        Me.dtpCompleted.TabIndex = 11
        '
        'lblPercent
        '
        Me.lblPercent.Location = New System.Drawing.Point(42, 79)
        Me.lblPercent.Name = "lblPercent"
        Me.lblPercent.Size = New System.Drawing.Size(64, 23)
        Me.lblPercent.TabIndex = 8
        Me.lblPercent.Text = "% Done"
        Me.lblPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'udcPercent
        '
        Me.udcPercent.Location = New System.Drawing.Point(112, 80)
        Me.udcPercent.Name = "udcPercent"
        Me.udcPercent.Size = New System.Drawing.Size(56, 22)
        Me.udcPercent.TabIndex = 9
        Me.udcPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'pgAttendees
        '
        Me.pgAttendees.Controls.Add(Me.groupBox1)
        Me.pgAttendees.Controls.Add(Me.txtOrganizer)
        Me.pgAttendees.Controls.Add(Me.label5)
        Me.pgAttendees.Controls.Add(Me.ucAttendees)
        Me.pgAttendees.Location = New System.Drawing.Point(4, 25)
        Me.pgAttendees.Name = "pgAttendees"
        Me.pgAttendees.Size = New System.Drawing.Size(610, 323)
        Me.pgAttendees.TabIndex = 0
        Me.pgAttendees.Text = "Attendees"
        Me.pgAttendees.UseVisualStyleBackColor = true
        Me.pgAttendees.Visible = false
        '
        'groupBox1
        '
        Me.groupBox1.Location = New System.Drawing.Point(36, 48)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(544, 8)
        Me.groupBox1.TabIndex = 2
        Me.groupBox1.TabStop = false
        '
        'txtOrganizer
        '
        Me.txtOrganizer.Location = New System.Drawing.Point(116, 16)
        Me.txtOrganizer.Name = "txtOrganizer"
        Me.txtOrganizer.Size = New System.Drawing.Size(448, 22)
        Me.txtOrganizer.TabIndex = 1
        '
        'label5
        '
        Me.label5.Location = New System.Drawing.Point(23, 16)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(87, 23)
        Me.label5.TabIndex = 0
        Me.label5.Text = "&Organizer"
        Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ucAttendees
        '
        Me.ucAttendees.Location = New System.Drawing.Point(57, 72)
        Me.ucAttendees.Name = "ucAttendees"
        Me.ucAttendees.Size = New System.Drawing.Size(496, 216)
        Me.ucAttendees.TabIndex = 3
        '
        'pgRecurrence
        '
        Me.pgRecurrence.Controls.Add(Me.ucRecurrences)
        Me.pgRecurrence.Location = New System.Drawing.Point(4, 25)
        Me.pgRecurrence.Name = "pgRecurrence"
        Me.pgRecurrence.Size = New System.Drawing.Size(610, 323)
        Me.pgRecurrence.TabIndex = 4
        Me.pgRecurrence.Text = "Recurrence"
        Me.pgRecurrence.UseVisualStyleBackColor = true
        '
        'ucRecurrences
        '
        Me.ucRecurrences.Location = New System.Drawing.Point(33, 21)
        Me.ucRecurrences.Name = "ucRecurrences"
        Me.ucRecurrences.Size = New System.Drawing.Size(545, 280)
        Me.ucRecurrences.TabIndex = 0
        '
        'pgExceptions
        '
        Me.pgExceptions.Controls.Add(Me.ucExceptions)
        Me.pgExceptions.Location = New System.Drawing.Point(4, 25)
        Me.pgExceptions.Name = "pgExceptions"
        Me.pgExceptions.Size = New System.Drawing.Size(610, 323)
        Me.pgExceptions.TabIndex = 5
        Me.pgExceptions.Text = "Exceptions"
        Me.pgExceptions.UseVisualStyleBackColor = true
        '
        'ucExceptions
        '
        Me.ucExceptions.EditsExceptions = true
        Me.ucExceptions.Location = New System.Drawing.Point(33, 21)
        Me.ucExceptions.Name = "ucExceptions"
        Me.ucExceptions.Size = New System.Drawing.Size(545, 280)
        Me.ucExceptions.TabIndex = 0
        '
        'pgAttachments
        '
        Me.pgAttachments.Controls.Add(Me.ucAttachments)
        Me.pgAttachments.Location = New System.Drawing.Point(4, 25)
        Me.pgAttachments.Name = "pgAttachments"
        Me.pgAttachments.Size = New System.Drawing.Size(610, 323)
        Me.pgAttachments.TabIndex = 1
        Me.pgAttachments.Text = "Attachments"
        Me.pgAttachments.UseVisualStyleBackColor = true
        Me.pgAttachments.Visible = false
        '
        'ucAttachments
        '
        Me.ucAttachments.Location = New System.Drawing.Point(17, 9)
        Me.ucAttachments.Name = "ucAttachments"
        Me.ucAttachments.Size = New System.Drawing.Size(576, 304)
        Me.ucAttachments.TabIndex = 0
        '
        'pgAlarms
        '
        Me.pgAlarms.Controls.Add(Me.ucAlarms)
        Me.pgAlarms.Location = New System.Drawing.Point(4, 25)
        Me.pgAlarms.Name = "pgAlarms"
        Me.pgAlarms.Size = New System.Drawing.Size(610, 323)
        Me.pgAlarms.TabIndex = 7
        Me.pgAlarms.Text = "Alarms"
        Me.pgAlarms.UseVisualStyleBackColor = true
        '
        'ucAlarms
        '
        Me.ucAlarms.Location = New System.Drawing.Point(15, 5)
        Me.ucAlarms.Name = "ucAlarms"
        Me.ucAlarms.Size = New System.Drawing.Size(581, 312)
        Me.ucAlarms.TabIndex = 0
        '
        'pgMisc
        '
        Me.pgMisc.Controls.Add(Me.groupBox3)
        Me.pgMisc.Controls.Add(Me.groupBox2)
        Me.pgMisc.Controls.Add(Me.ucRequestStatus)
        Me.pgMisc.Controls.Add(Me.btnFind)
        Me.pgMisc.Controls.Add(Me.txtLongitude)
        Me.pgMisc.Controls.Add(Me.lblLongitude)
        Me.pgMisc.Controls.Add(Me.txtLatitude)
        Me.pgMisc.Controls.Add(Me.lblLatitude)
        Me.pgMisc.Controls.Add(Me.txtComments)
        Me.pgMisc.Controls.Add(Me.label17)
        Me.pgMisc.Controls.Add(Me.txtUrl)
        Me.pgMisc.Controls.Add(Me.label3)
        Me.pgMisc.Location = New System.Drawing.Point(4, 25)
        Me.pgMisc.Name = "pgMisc"
        Me.pgMisc.Size = New System.Drawing.Size(610, 323)
        Me.pgMisc.TabIndex = 6
        Me.pgMisc.Text = "Misc"
        Me.pgMisc.UseVisualStyleBackColor = true
        '
        'groupBox3
        '
        Me.groupBox3.Location = New System.Drawing.Point(197, 0)
        Me.groupBox3.Name = "groupBox3"
        Me.groupBox3.Size = New System.Drawing.Size(2, 160)
        Me.groupBox3.TabIndex = 5
        Me.groupBox3.TabStop = false
        '
        'groupBox2
        '
        Me.groupBox2.Location = New System.Drawing.Point(8, 162)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(600, 8)
        Me.groupBox2.TabIndex = 7
        Me.groupBox2.TabStop = false
        '
        'ucRequestStatus
        '
        Me.ucRequestStatus.Location = New System.Drawing.Point(203, 16)
        Me.ucRequestStatus.Name = "ucRequestStatus"
        Me.ucRequestStatus.Size = New System.Drawing.Size(402, 144)
        Me.ucRequestStatus.TabIndex = 6
        '
        'btnFind
        '
        Me.btnFind.Location = New System.Drawing.Point(88, 100)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(64, 28)
        Me.btnFind.TabIndex = 4
        Me.btnFind.Text = "&Find"
        '
        'txtLongitude
        '
        Me.txtLongitude.Location = New System.Drawing.Point(88, 72)
        Me.txtLongitude.Name = "txtLongitude"
        Me.txtLongitude.Size = New System.Drawing.Size(88, 22)
        Me.txtLongitude.TabIndex = 3
        '
        'lblLongitude
        '
        Me.lblLongitude.Location = New System.Drawing.Point(6, 72)
        Me.lblLongitude.Name = "lblLongitude"
        Me.lblLongitude.Size = New System.Drawing.Size(76, 23)
        Me.lblLongitude.TabIndex = 2
        Me.lblLongitude.Text = "Longitude"
        Me.lblLongitude.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtLatitude
        '
        Me.txtLatitude.Location = New System.Drawing.Point(88, 40)
        Me.txtLatitude.Name = "txtLatitude"
        Me.txtLatitude.Size = New System.Drawing.Size(88, 22)
        Me.txtLatitude.TabIndex = 1
        '
        'lblLatitude
        '
        Me.lblLatitude.Location = New System.Drawing.Point(18, 40)
        Me.lblLatitude.Name = "lblLatitude"
        Me.lblLatitude.Size = New System.Drawing.Size(64, 23)
        Me.lblLatitude.TabIndex = 0
        Me.lblLatitude.Text = "&Latitude"
        Me.lblLatitude.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtComments
        '
        Me.txtComments.AcceptsReturn = true
        Me.txtComments.Location = New System.Drawing.Point(140, 216)
        Me.txtComments.Multiline = true
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComments.Size = New System.Drawing.Size(424, 64)
        Me.txtComments.TabIndex = 11
        '
        'label17
        '
        Me.label17.Location = New System.Drawing.Point(54, 216)
        Me.label17.Name = "label17"
        Me.label17.Size = New System.Drawing.Size(80, 23)
        Me.label17.TabIndex = 10
        Me.label17.Text = "&Comments"
        Me.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtUrl
        '
        Me.txtUrl.Location = New System.Drawing.Point(140, 184)
        Me.txtUrl.Name = "txtUrl"
        Me.txtUrl.Size = New System.Drawing.Size(424, 22)
        Me.txtUrl.TabIndex = 9
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(94, 184)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(40, 23)
        Me.label3.TabIndex = 8
        Me.label3.Text = "&URL"
        Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label9
        '
        Me.label9.Location = New System.Drawing.Point(26, 11)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(80, 23)
        Me.label9.TabIndex = 0
        Me.label9.Text = "Unique ID"
        Me.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtClass
        '
        Me.txtClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtClass.Location = New System.Drawing.Point(112, 98)
        Me.txtClass.Name = "txtClass"
        Me.txtClass.Size = New System.Drawing.Size(144, 22)
        Me.txtClass.TabIndex = 11
        '
        'label10
        '
        Me.label10.Location = New System.Drawing.Point(58, 97)
        Me.label10.Name = "label10"
        Me.label10.Size = New System.Drawing.Size(48, 23)
        Me.label10.TabIndex = 10
        Me.label10.Text = "Class"
        Me.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtLastModified
        '
        Me.txtLastModified.Location = New System.Drawing.Point(400, 40)
        Me.txtLastModified.Name = "txtLastModified"
        Me.txtLastModified.ReadOnly = true
        Me.txtLastModified.Size = New System.Drawing.Size(168, 22)
        Me.txtLastModified.TabIndex = 6
        Me.txtLastModified.TabStop = false
        '
        'label6
        '
        Me.label6.Location = New System.Drawing.Point(298, 40)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(96, 23)
        Me.label6.TabIndex = 5
        Me.label6.Text = "Last Modified"
        Me.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCreated
        '
        Me.txtCreated.Location = New System.Drawing.Point(112, 40)
        Me.txtCreated.Name = "txtCreated"
        Me.txtCreated.ReadOnly = true
        Me.txtCreated.Size = New System.Drawing.Size(168, 22)
        Me.txtCreated.TabIndex = 4
        Me.txtCreated.TabStop = false
        '
        'label7
        '
        Me.label7.Location = New System.Drawing.Point(34, 40)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(72, 23)
        Me.label7.TabIndex = 3
        Me.label7.Text = "Created"
        Me.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'udcSequence
        '
        Me.udcSequence.Location = New System.Drawing.Point(352, 98)
        Me.udcSequence.Maximum = New Decimal(New Integer() {1048576, 0, 0, 0})
        Me.udcSequence.Name = "udcSequence"
        Me.udcSequence.Size = New System.Drawing.Size(64, 22)
        Me.udcSequence.TabIndex = 13
        Me.udcSequence.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'label15
        '
        Me.label15.Location = New System.Drawing.Point(266, 98)
        Me.label15.Name = "label15"
        Me.label15.Size = New System.Drawing.Size(80, 23)
        Me.label15.TabIndex = 12
        Me.label15.Text = "Sequence"
        Me.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPriority
        '
        Me.lblPriority.Location = New System.Drawing.Point(434, 97)
        Me.lblPriority.Name = "lblPriority"
        Me.lblPriority.Size = New System.Drawing.Size(56, 23)
        Me.lblPriority.TabIndex = 14
        Me.lblPriority.Text = "Priority"
        Me.lblPriority.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'udcPriority
        '
        Me.udcPriority.Location = New System.Drawing.Point(496, 98)
        Me.udcPriority.Maximum = New Decimal(New Integer() {9, 0, 0, 0})
        Me.udcPriority.Name = "udcPriority"
        Me.udcPriority.Size = New System.Drawing.Size(64, 22)
        Me.udcPriority.TabIndex = 15
        Me.udcPriority.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkTransparent
        '
        Me.chkTransparent.Location = New System.Drawing.Point(511, 12)
        Me.chkTransparent.Name = "chkTransparent"
        Me.chkTransparent.Size = New System.Drawing.Size(119, 24)
        Me.chkTransparent.TabIndex = 2
        Me.chkTransparent.Text = "Transparent"
        '
        'epErrors
        '
        Me.epErrors.ContainerControl = Me
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(14, 68)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(92, 23)
        Me.label2.TabIndex = 7
        Me.label2.Text = "&Time Zone"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboTimeZone
        '
        Me.cboTimeZone.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cboTimeZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTimeZone.Location = New System.Drawing.Point(112, 68)
        Me.cboTimeZone.MaxDropDownItems = 16
        Me.cboTimeZone.Name = "cboTimeZone"
        Me.cboTimeZone.Size = New System.Drawing.Size(448, 24)
        Me.cboTimeZone.TabIndex = 8
        '
        'btnApplyTZ
        '
        Me.btnApplyTZ.Location = New System.Drawing.Point(566, 65)
        Me.btnApplyTZ.Name = "btnApplyTZ"
        Me.btnApplyTZ.Size = New System.Drawing.Size(64, 28)
        Me.btnApplyTZ.TabIndex = 9
        Me.btnApplyTZ.Text = "Apply"
        '
        'CalendarObjectDlg
        '
        Me.AcceptButton = Me.btnOK
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(642, 538)
        Me.Controls.Add(Me.btnApplyTZ)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.cboTimeZone)
        Me.Controls.Add(Me.chkTransparent)
        Me.Controls.Add(Me.lblPriority)
        Me.Controls.Add(Me.udcPriority)
        Me.Controls.Add(Me.label15)
        Me.Controls.Add(Me.udcSequence)
        Me.Controls.Add(Me.txtCreated)
        Me.Controls.Add(Me.txtLastModified)
        Me.Controls.Add(Me.txtClass)
        Me.Controls.Add(Me.txtUniqueId)
        Me.Controls.Add(Me.label7)
        Me.Controls.Add(Me.label6)
        Me.Controls.Add(Me.label10)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.tabInfo)
        Me.Controls.Add(Me.label9)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "CalendarObjectDlg"
        Me.ShowInTaskbar = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "CalendarObjectDlg"
        Me.tabInfo.ResumeLayout(false)
        Me.pgGeneral.ResumeLayout(false)
        Me.pgGeneral.PerformLayout
        CType(Me.udcPercent,System.ComponentModel.ISupportInitialize).EndInit
        Me.pgAttendees.ResumeLayout(false)
        Me.pgAttendees.PerformLayout
        Me.pgRecurrence.ResumeLayout(false)
        Me.pgExceptions.ResumeLayout(false)
        Me.pgAttachments.ResumeLayout(false)
        Me.pgAlarms.ResumeLayout(false)
        Me.pgMisc.ResumeLayout(false)
        Me.pgMisc.PerformLayout
        CType(Me.udcSequence,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.udcPriority,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.epErrors,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents txtUniqueId As System.Windows.Forms.TextBox
    Friend WithEvents tabInfo As System.Windows.Forms.TabControl
    Friend WithEvents pgGeneral As System.Windows.Forms.TabPage
    Friend WithEvents txtDuration As System.Windows.Forms.TextBox
    Friend WithEvents dtpEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents pgAttendees As System.Windows.Forms.TabPage
    Friend WithEvents ucAttendees As CalendarBrowser.AttendeeControl
    Friend WithEvents label9 As System.Windows.Forms.Label
    Friend WithEvents pgRecurrence As System.Windows.Forms.TabPage
    Friend WithEvents pgExceptions As System.Windows.Forms.TabPage
    Friend WithEvents txtClass As System.Windows.Forms.TextBox
    Friend WithEvents label10 As System.Windows.Forms.Label
    Friend WithEvents txtCategories As System.Windows.Forms.TextBox
    Friend WithEvents label21 As System.Windows.Forms.Label
    Friend WithEvents txtLastModified As System.Windows.Forms.TextBox
    Friend WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents txtCreated As System.Windows.Forms.TextBox
    Friend WithEvents label7 As System.Windows.Forms.Label
    Friend WithEvents pgMisc As System.Windows.Forms.TabPage
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents label17 As System.Windows.Forms.Label
    Friend WithEvents txtUrl As System.Windows.Forms.TextBox
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents txtSummary As System.Windows.Forms.TextBox
    Friend WithEvents label8 As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents label11 As System.Windows.Forms.Label
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents txtLongitude As System.Windows.Forms.TextBox
    Friend WithEvents txtLatitude As System.Windows.Forms.TextBox
    Friend WithEvents txtResources As System.Windows.Forms.TextBox
    Friend WithEvents txtOrganizer As System.Windows.Forms.TextBox
    Friend WithEvents label5 As System.Windows.Forms.Label
    Friend WithEvents udcSequence As System.Windows.Forms.NumericUpDown
    Friend WithEvents label15 As System.Windows.Forms.Label
    Friend WithEvents pgAttachments As System.Windows.Forms.TabPage
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents label18 As System.Windows.Forms.Label
    Friend WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ucRequestStatus As CalendarBrowser.RequestStatusControl
    Friend WithEvents groupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents groupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtLocation As System.Windows.Forms.TextBox
    Friend WithEvents chkTransparent As System.Windows.Forms.CheckBox
    Friend WithEvents ucRecurrences As CalendarBrowser.RecurrenceControl
    Friend WithEvents ucExceptions As CalendarBrowser.RecurrenceControl
    Friend WithEvents udcPriority As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblLocation As System.Windows.Forms.Label
    Friend WithEvents lblResources As System.Windows.Forms.Label
    Friend WithEvents lblEndDate As System.Windows.Forms.Label
    Friend WithEvents lblDuration As System.Windows.Forms.Label
    Friend WithEvents lblLongitude As System.Windows.Forms.Label
    Friend WithEvents lblLatitude As System.Windows.Forms.Label
    Friend WithEvents lblPriority As System.Windows.Forms.Label
    Friend WithEvents epErrors As System.Windows.Forms.ErrorProvider
    Friend WithEvents pgAlarms As System.Windows.Forms.TabPage
    Friend WithEvents ucAlarms As CalendarBrowser.AlarmControl
    Friend WithEvents lblCompleted As System.Windows.Forms.Label
    Friend WithEvents dtpCompleted As System.Windows.Forms.DateTimePicker
    Friend WithEvents udcPercent As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblPercent As System.Windows.Forms.Label
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents cboTimeZone As System.Windows.Forms.ComboBox
    Friend WithEvents ucAttachments As CalendarBrowser.AttachmentsControl
    Friend WithEvents btnApplyTZ As System.Windows.Forms.Button

End Class
