namespace CalendarBrowser
{
    partial class CalendarObjectDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtUniqueId = new System.Windows.Forms.TextBox();
            this.tabInfo = new System.Windows.Forms.TabControl();
            this.pgGeneral = new System.Windows.Forms.TabPage();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtResources = new System.Windows.Forms.TextBox();
            this.lblResources = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCategories = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblDuration = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCompleted = new System.Windows.Forms.Label();
            this.dtpCompleted = new System.Windows.Forms.DateTimePicker();
            this.lblPercent = new System.Windows.Forms.Label();
            this.udcPercent = new System.Windows.Forms.NumericUpDown();
            this.pgAttendees = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtOrganizer = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ucAttendees = new CalendarBrowser.AttendeeControl();
            this.pgRecurrence = new System.Windows.Forms.TabPage();
            this.ucRecurrences = new CalendarBrowser.RecurrenceControl();
            this.pgExceptions = new System.Windows.Forms.TabPage();
            this.ucExceptions = new CalendarBrowser.RecurrenceControl();
            this.pgAttachments = new System.Windows.Forms.TabPage();
            this.ucAttachments = new CalendarBrowser.AttachmentsControl();
            this.pgAlarms = new System.Windows.Forms.TabPage();
            this.ucAlarms = new CalendarBrowser.AlarmControl();
            this.pgMisc = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucRequestStatus = new CalendarBrowser.RequestStatusControl();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtLongitude = new System.Windows.Forms.TextBox();
            this.lblLongitude = new System.Windows.Forms.Label();
            this.txtLatitude = new System.Windows.Forms.TextBox();
            this.lblLatitude = new System.Windows.Forms.Label();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtClass = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtLastModified = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCreated = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.udcSequence = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.lblPriority = new System.Windows.Forms.Label();
            this.udcPriority = new System.Windows.Forms.NumericUpDown();
            this.chkTransparent = new System.Windows.Forms.CheckBox();
            this.epErrors = new System.Windows.Forms.ErrorProvider(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.cboTimeZone = new System.Windows.Forms.ComboBox();
            this.btnApplyTZ = new System.Windows.Forms.Button();
            this.tabInfo.SuspendLayout();
            this.pgGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udcPercent)).BeginInit();
            this.pgAttendees.SuspendLayout();
            this.pgRecurrence.SuspendLayout();
            this.pgExceptions.SuspendLayout();
            this.pgAttachments.SuspendLayout();
            this.pgAlarms.SuspendLayout();
            this.pgMisc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udcSequence)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcPriority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.epErrors)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(542, 494);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 32);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(12, 494);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 32);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "OK";
            // 
            // txtUniqueId
            // 
            this.txtUniqueId.Location = new System.Drawing.Point(112, 12);
            this.txtUniqueId.Name = "txtUniqueId";
            this.txtUniqueId.ReadOnly = true;
            this.txtUniqueId.Size = new System.Drawing.Size(376, 22);
            this.txtUniqueId.TabIndex = 1;
            this.txtUniqueId.TabStop = false;
            // 
            // tabInfo
            // 
            this.tabInfo.Controls.Add(this.pgGeneral);
            this.tabInfo.Controls.Add(this.pgAttendees);
            this.tabInfo.Controls.Add(this.pgRecurrence);
            this.tabInfo.Controls.Add(this.pgExceptions);
            this.tabInfo.Controls.Add(this.pgAttachments);
            this.tabInfo.Controls.Add(this.pgAlarms);
            this.tabInfo.Controls.Add(this.pgMisc);
            this.tabInfo.Location = new System.Drawing.Point(12, 136);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.SelectedIndex = 0;
            this.tabInfo.Size = new System.Drawing.Size(618, 352);
            this.tabInfo.TabIndex = 16;
            // 
            // pgGeneral
            // 
            this.pgGeneral.Controls.Add(this.txtLocation);
            this.pgGeneral.Controls.Add(this.lblLocation);
            this.pgGeneral.Controls.Add(this.cboStatus);
            this.pgGeneral.Controls.Add(this.label18);
            this.pgGeneral.Controls.Add(this.txtResources);
            this.pgGeneral.Controls.Add(this.lblResources);
            this.pgGeneral.Controls.Add(this.txtDescription);
            this.pgGeneral.Controls.Add(this.label11);
            this.pgGeneral.Controls.Add(this.txtSummary);
            this.pgGeneral.Controls.Add(this.label8);
            this.pgGeneral.Controls.Add(this.txtCategories);
            this.pgGeneral.Controls.Add(this.label21);
            this.pgGeneral.Controls.Add(this.lblEndDate);
            this.pgGeneral.Controls.Add(this.txtDuration);
            this.pgGeneral.Controls.Add(this.dtpEndDate);
            this.pgGeneral.Controls.Add(this.dtpStartDate);
            this.pgGeneral.Controls.Add(this.lblDuration);
            this.pgGeneral.Controls.Add(this.label1);
            this.pgGeneral.Controls.Add(this.lblCompleted);
            this.pgGeneral.Controls.Add(this.dtpCompleted);
            this.pgGeneral.Controls.Add(this.lblPercent);
            this.pgGeneral.Controls.Add(this.udcPercent);
            this.pgGeneral.Location = new System.Drawing.Point(4, 25);
            this.pgGeneral.Name = "pgGeneral";
            this.pgGeneral.Size = new System.Drawing.Size(610, 323);
            this.pgGeneral.TabIndex = 3;
            this.pgGeneral.Text = "General";
            this.pgGeneral.UseVisualStyleBackColor = true;
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(112, 152);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(424, 22);
            this.txtLocation.TabIndex = 15;
            // 
            // lblLocation
            // 
            this.lblLocation.Location = new System.Drawing.Point(29, 152);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(77, 23);
            this.lblLocation.TabIndex = 14;
            this.lblLocation.Text = "Location";
            this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Location = new System.Drawing.Point(112, 48);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(116, 24);
            this.cboStatus.TabIndex = 5;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(51, 48);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(55, 23);
            this.label18.TabIndex = 4;
            this.label18.Text = "Status";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtResources
            // 
            this.txtResources.Location = new System.Drawing.Point(112, 288);
            this.txtResources.Name = "txtResources";
            this.txtResources.Size = new System.Drawing.Size(440, 22);
            this.txtResources.TabIndex = 21;
            // 
            // lblResources
            // 
            this.lblResources.Location = new System.Drawing.Point(19, 288);
            this.lblResources.Name = "lblResources";
            this.lblResources.Size = new System.Drawing.Size(87, 23);
            this.lblResources.TabIndex = 20;
            this.lblResources.Text = "Resources";
            this.lblResources.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDescription
            // 
            this.txtDescription.AcceptsReturn = true;
            this.txtDescription.Location = new System.Drawing.Point(112, 184);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(424, 56);
            this.txtDescription.TabIndex = 17;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(19, 184);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 23);
            this.label11.TabIndex = 16;
            this.label11.Text = "Description";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSummary
            // 
            this.txtSummary.Location = new System.Drawing.Point(112, 120);
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.Size = new System.Drawing.Size(424, 22);
            this.txtSummary.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(27, 120);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 23);
            this.label8.TabIndex = 12;
            this.label8.Text = "Summary";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCategories
            // 
            this.txtCategories.Location = new System.Drawing.Point(112, 256);
            this.txtCategories.Name = "txtCategories";
            this.txtCategories.Size = new System.Drawing.Size(440, 22);
            this.txtCategories.TabIndex = 19;
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(19, 256);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(87, 23);
            this.label21.TabIndex = 18;
            this.label21.Text = "&Categories";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEndDate
            // 
            this.lblEndDate.Location = new System.Drawing.Point(338, 16);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(40, 23);
            this.lblEndDate.TabIndex = 2;
            this.lblEndDate.Text = "End";
            this.lblEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDuration
            // 
            this.txtDuration.Location = new System.Drawing.Point(384, 48);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(80, 22);
            this.txtDuration.TabIndex = 7;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Checked = false;
            this.dtpEndDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(384, 16);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.ShowCheckBox = true;
            this.dtpEndDate.Size = new System.Drawing.Size(210, 22);
            this.dtpEndDate.TabIndex = 3;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Checked = false;
            this.dtpStartDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(112, 16);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.ShowCheckBox = true;
            this.dtpStartDate.Size = new System.Drawing.Size(210, 22);
            this.dtpStartDate.TabIndex = 1;
            // 
            // lblDuration
            // 
            this.lblDuration.Location = new System.Drawing.Point(309, 48);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(69, 23);
            this.lblDuration.TabIndex = 6;
            this.lblDuration.Text = "Duration";
            this.lblDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(61, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Start";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCompleted
            // 
            this.lblCompleted.Location = new System.Drawing.Point(291, 79);
            this.lblCompleted.Name = "lblCompleted";
            this.lblCompleted.Size = new System.Drawing.Size(87, 23);
            this.lblCompleted.TabIndex = 10;
            this.lblCompleted.Text = "Completed";
            this.lblCompleted.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpCompleted
            // 
            this.dtpCompleted.Checked = false;
            this.dtpCompleted.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            this.dtpCompleted.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCompleted.Location = new System.Drawing.Point(384, 79);
            this.dtpCompleted.Name = "dtpCompleted";
            this.dtpCompleted.ShowCheckBox = true;
            this.dtpCompleted.Size = new System.Drawing.Size(210, 22);
            this.dtpCompleted.TabIndex = 11;
            // 
            // lblPercent
            // 
            this.lblPercent.Location = new System.Drawing.Point(42, 79);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(64, 23);
            this.lblPercent.TabIndex = 8;
            this.lblPercent.Text = "% Done";
            this.lblPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // udcPercent
            // 
            this.udcPercent.Location = new System.Drawing.Point(112, 80);
            this.udcPercent.Name = "udcPercent";
            this.udcPercent.Size = new System.Drawing.Size(56, 22);
            this.udcPercent.TabIndex = 9;
            this.udcPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pgAttendees
            // 
            this.pgAttendees.Controls.Add(this.groupBox1);
            this.pgAttendees.Controls.Add(this.txtOrganizer);
            this.pgAttendees.Controls.Add(this.label5);
            this.pgAttendees.Controls.Add(this.ucAttendees);
            this.pgAttendees.Location = new System.Drawing.Point(4, 25);
            this.pgAttendees.Name = "pgAttendees";
            this.pgAttendees.Size = new System.Drawing.Size(610, 323);
            this.pgAttendees.TabIndex = 0;
            this.pgAttendees.Text = "Attendees";
            this.pgAttendees.UseVisualStyleBackColor = true;
            this.pgAttendees.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(36, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(544, 8);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // txtOrganizer
            // 
            this.txtOrganizer.Location = new System.Drawing.Point(116, 16);
            this.txtOrganizer.Name = "txtOrganizer";
            this.txtOrganizer.Size = new System.Drawing.Size(448, 22);
            this.txtOrganizer.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(23, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "&Organizer";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ucAttendees
            // 
            this.ucAttendees.Location = new System.Drawing.Point(57, 72);
            this.ucAttendees.Name = "ucAttendees";
            this.ucAttendees.Size = new System.Drawing.Size(496, 216);
            this.ucAttendees.TabIndex = 3;
            // 
            // pgRecurrence
            // 
            this.pgRecurrence.Controls.Add(this.ucRecurrences);
            this.pgRecurrence.Location = new System.Drawing.Point(4, 25);
            this.pgRecurrence.Name = "pgRecurrence";
            this.pgRecurrence.Size = new System.Drawing.Size(610, 323);
            this.pgRecurrence.TabIndex = 4;
            this.pgRecurrence.Text = "Recurrence";
            this.pgRecurrence.UseVisualStyleBackColor = true;
            // 
            // ucRecurrences
            // 
            this.ucRecurrences.Location = new System.Drawing.Point(33, 21);
            this.ucRecurrences.Name = "ucRecurrences";
            this.ucRecurrences.Size = new System.Drawing.Size(545, 280);
            this.ucRecurrences.TabIndex = 0;
            // 
            // pgExceptions
            // 
            this.pgExceptions.Controls.Add(this.ucExceptions);
            this.pgExceptions.Location = new System.Drawing.Point(4, 25);
            this.pgExceptions.Name = "pgExceptions";
            this.pgExceptions.Size = new System.Drawing.Size(610, 323);
            this.pgExceptions.TabIndex = 5;
            this.pgExceptions.Text = "Exceptions";
            this.pgExceptions.UseVisualStyleBackColor = true;
            // 
            // ucExceptions
            // 
            this.ucExceptions.EditsExceptions = true;
            this.ucExceptions.Location = new System.Drawing.Point(33, 21);
            this.ucExceptions.Name = "ucExceptions";
            this.ucExceptions.Size = new System.Drawing.Size(545, 280);
            this.ucExceptions.TabIndex = 0;
            // 
            // pgAttachments
            // 
            this.pgAttachments.Controls.Add(this.ucAttachments);
            this.pgAttachments.Location = new System.Drawing.Point(4, 25);
            this.pgAttachments.Name = "pgAttachments";
            this.pgAttachments.Size = new System.Drawing.Size(610, 323);
            this.pgAttachments.TabIndex = 1;
            this.pgAttachments.Text = "Attachments";
            this.pgAttachments.UseVisualStyleBackColor = true;
            this.pgAttachments.Visible = false;
            // 
            // ucAttachments
            // 
            this.ucAttachments.Location = new System.Drawing.Point(17, 9);
            this.ucAttachments.Name = "ucAttachments";
            this.ucAttachments.Size = new System.Drawing.Size(576, 304);
            this.ucAttachments.TabIndex = 0;
            // 
            // pgAlarms
            // 
            this.pgAlarms.Controls.Add(this.ucAlarms);
            this.pgAlarms.Location = new System.Drawing.Point(4, 25);
            this.pgAlarms.Name = "pgAlarms";
            this.pgAlarms.Size = new System.Drawing.Size(610, 323);
            this.pgAlarms.TabIndex = 7;
            this.pgAlarms.Text = "Alarms";
            this.pgAlarms.UseVisualStyleBackColor = true;
            // 
            // ucAlarms
            // 
            this.ucAlarms.Location = new System.Drawing.Point(15, 5);
            this.ucAlarms.Name = "ucAlarms";
            this.ucAlarms.Size = new System.Drawing.Size(581, 312);
            this.ucAlarms.TabIndex = 0;
            // 
            // pgMisc
            // 
            this.pgMisc.Controls.Add(this.groupBox3);
            this.pgMisc.Controls.Add(this.groupBox2);
            this.pgMisc.Controls.Add(this.ucRequestStatus);
            this.pgMisc.Controls.Add(this.btnFind);
            this.pgMisc.Controls.Add(this.txtLongitude);
            this.pgMisc.Controls.Add(this.lblLongitude);
            this.pgMisc.Controls.Add(this.txtLatitude);
            this.pgMisc.Controls.Add(this.lblLatitude);
            this.pgMisc.Controls.Add(this.txtComments);
            this.pgMisc.Controls.Add(this.label17);
            this.pgMisc.Controls.Add(this.txtUrl);
            this.pgMisc.Controls.Add(this.label3);
            this.pgMisc.Location = new System.Drawing.Point(4, 25);
            this.pgMisc.Name = "pgMisc";
            this.pgMisc.Size = new System.Drawing.Size(610, 323);
            this.pgMisc.TabIndex = 6;
            this.pgMisc.Text = "Misc";
            this.pgMisc.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(197, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(2, 160);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(8, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(600, 8);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // ucRequestStatus
            // 
            this.ucRequestStatus.Location = new System.Drawing.Point(203, 16);
            this.ucRequestStatus.Name = "ucRequestStatus";
            this.ucRequestStatus.Size = new System.Drawing.Size(402, 144);
            this.ucRequestStatus.TabIndex = 6;
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(88, 100);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(64, 28);
            this.btnFind.TabIndex = 4;
            this.btnFind.Text = "&Find";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtLongitude
            // 
            this.txtLongitude.Location = new System.Drawing.Point(88, 72);
            this.txtLongitude.Name = "txtLongitude";
            this.txtLongitude.Size = new System.Drawing.Size(88, 22);
            this.txtLongitude.TabIndex = 3;
            // 
            // lblLongitude
            // 
            this.lblLongitude.Location = new System.Drawing.Point(6, 72);
            this.lblLongitude.Name = "lblLongitude";
            this.lblLongitude.Size = new System.Drawing.Size(76, 23);
            this.lblLongitude.TabIndex = 2;
            this.lblLongitude.Text = "Longitude";
            this.lblLongitude.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLatitude
            // 
            this.txtLatitude.Location = new System.Drawing.Point(88, 40);
            this.txtLatitude.Name = "txtLatitude";
            this.txtLatitude.Size = new System.Drawing.Size(88, 22);
            this.txtLatitude.TabIndex = 1;
            // 
            // lblLatitude
            // 
            this.lblLatitude.Location = new System.Drawing.Point(18, 40);
            this.lblLatitude.Name = "lblLatitude";
            this.lblLatitude.Size = new System.Drawing.Size(64, 23);
            this.lblLatitude.TabIndex = 0;
            this.lblLatitude.Text = "&Latitude";
            this.lblLatitude.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtComments
            // 
            this.txtComments.AcceptsReturn = true;
            this.txtComments.Location = new System.Drawing.Point(140, 216);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComments.Size = new System.Drawing.Size(424, 64);
            this.txtComments.TabIndex = 11;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(54, 216);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(80, 23);
            this.label17.TabIndex = 10;
            this.label17.Text = "&Comments";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(140, 184);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(424, 22);
            this.txtUrl.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(94, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 23);
            this.label3.TabIndex = 8;
            this.label3.Text = "&URL";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(26, 11);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 23);
            this.label9.TabIndex = 0;
            this.label9.Text = "Unique ID";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtClass
            // 
            this.txtClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtClass.Location = new System.Drawing.Point(112, 98);
            this.txtClass.Name = "txtClass";
            this.txtClass.Size = new System.Drawing.Size(144, 22);
            this.txtClass.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(58, 97);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 23);
            this.label10.TabIndex = 10;
            this.label10.Text = "Class";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLastModified
            // 
            this.txtLastModified.Location = new System.Drawing.Point(400, 40);
            this.txtLastModified.Name = "txtLastModified";
            this.txtLastModified.ReadOnly = true;
            this.txtLastModified.Size = new System.Drawing.Size(168, 22);
            this.txtLastModified.TabIndex = 6;
            this.txtLastModified.TabStop = false;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(298, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 23);
            this.label6.TabIndex = 5;
            this.label6.Text = "Last Modified";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCreated
            // 
            this.txtCreated.Location = new System.Drawing.Point(112, 40);
            this.txtCreated.Name = "txtCreated";
            this.txtCreated.ReadOnly = true;
            this.txtCreated.Size = new System.Drawing.Size(168, 22);
            this.txtCreated.TabIndex = 4;
            this.txtCreated.TabStop = false;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(34, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 23);
            this.label7.TabIndex = 3;
            this.label7.Text = "Created";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // udcSequence
            // 
            this.udcSequence.Location = new System.Drawing.Point(352, 98);
            this.udcSequence.Maximum = new decimal(new int[] {
            1048576,
            0,
            0,
            0});
            this.udcSequence.Name = "udcSequence";
            this.udcSequence.Size = new System.Drawing.Size(64, 22);
            this.udcSequence.TabIndex = 13;
            this.udcSequence.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(266, 98);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 23);
            this.label15.TabIndex = 12;
            this.label15.Text = "Sequence";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPriority
            // 
            this.lblPriority.Location = new System.Drawing.Point(434, 97);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(56, 23);
            this.lblPriority.TabIndex = 14;
            this.lblPriority.Text = "Priority";
            this.lblPriority.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // udcPriority
            // 
            this.udcPriority.Location = new System.Drawing.Point(496, 98);
            this.udcPriority.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.udcPriority.Name = "udcPriority";
            this.udcPriority.Size = new System.Drawing.Size(64, 22);
            this.udcPriority.TabIndex = 15;
            this.udcPriority.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkTransparent
            // 
            this.chkTransparent.Location = new System.Drawing.Point(511, 12);
            this.chkTransparent.Name = "chkTransparent";
            this.chkTransparent.Size = new System.Drawing.Size(119, 24);
            this.chkTransparent.TabIndex = 2;
            this.chkTransparent.Text = "Transparent";
            // 
            // epErrors
            // 
            this.epErrors.ContainerControl = this;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(14, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "&Time Zone";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTimeZone
            // 
            this.cboTimeZone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTimeZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTimeZone.Location = new System.Drawing.Point(112, 68);
            this.cboTimeZone.MaxDropDownItems = 16;
            this.cboTimeZone.Name = "cboTimeZone";
            this.cboTimeZone.Size = new System.Drawing.Size(448, 24);
            this.cboTimeZone.TabIndex = 8;
            this.cboTimeZone.SelectedIndexChanged += new System.EventHandler(this.cboTimeZone_SelectedIndexChanged);
            // 
            // btnApplyTZ
            // 
            this.btnApplyTZ.Location = new System.Drawing.Point(566, 65);
            this.btnApplyTZ.Name = "btnApplyTZ";
            this.btnApplyTZ.Size = new System.Drawing.Size(64, 28);
            this.btnApplyTZ.TabIndex = 9;
            this.btnApplyTZ.Text = "Apply";
            this.btnApplyTZ.Click += new System.EventHandler(this.btnApplyTZ_Click);
            // 
            // CalendarObjectDlg
            // 
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(642, 538);
            this.Controls.Add(this.btnApplyTZ);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboTimeZone);
            this.Controls.Add(this.chkTransparent);
            this.Controls.Add(this.lblPriority);
            this.Controls.Add(this.udcPriority);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.udcSequence);
            this.Controls.Add(this.txtCreated);
            this.Controls.Add(this.txtLastModified);
            this.Controls.Add(this.txtClass);
            this.Controls.Add(this.txtUniqueId);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabInfo);
            this.Controls.Add(this.label9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CalendarObjectDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CalendarObjectDlg";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.CalendarObjectDlg_Closing);
            this.tabInfo.ResumeLayout(false);
            this.pgGeneral.ResumeLayout(false);
            this.pgGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udcPercent)).EndInit();
            this.pgAttendees.ResumeLayout(false);
            this.pgAttendees.PerformLayout();
            this.pgRecurrence.ResumeLayout(false);
            this.pgExceptions.ResumeLayout(false);
            this.pgAttachments.ResumeLayout(false);
            this.pgAlarms.ResumeLayout(false);
            this.pgMisc.ResumeLayout(false);
            this.pgMisc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udcSequence)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcPriority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.epErrors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtUniqueId;
        private System.Windows.Forms.TabControl tabInfo;
        private System.Windows.Forms.TabPage pgGeneral;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage pgAttendees;
        private CalendarBrowser.AttendeeControl ucAttendees;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TabPage pgRecurrence;
        private System.Windows.Forms.TabPage pgExceptions;
        private System.Windows.Forms.TextBox txtClass;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCategories;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtLastModified;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCreated;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage pgMisc;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSummary;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtLongitude;
        private System.Windows.Forms.TextBox txtLatitude;
        private System.Windows.Forms.TextBox txtResources;
        private System.Windows.Forms.TextBox txtOrganizer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown udcSequence;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TabPage pgAttachments;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox1;
        private CalendarBrowser.RequestStatusControl ucRequestStatus;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.CheckBox chkTransparent;
        private CalendarBrowser.RecurrenceControl ucRecurrences;
        private CalendarBrowser.RecurrenceControl ucExceptions;
        private System.Windows.Forms.NumericUpDown udcPriority;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblResources;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label lblLongitude;
        private System.Windows.Forms.Label lblLatitude;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.ErrorProvider epErrors;
        private System.Windows.Forms.TabPage pgAlarms;
        private CalendarBrowser.AlarmControl ucAlarms;
        private System.Windows.Forms.Label lblCompleted;
        private System.Windows.Forms.DateTimePicker dtpCompleted;
        private System.Windows.Forms.NumericUpDown udcPercent;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboTimeZone;
        private CalendarBrowser.AttachmentsControl ucAttachments;
        private System.Windows.Forms.Button btnApplyTZ;
    }
}
