namespace CalendarBrowser
{
    partial class VFreeBusyDlg
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
            this.txtUniqueId = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tabInfo = new System.Windows.Forms.TabControl();
            this.pgGeneral = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pgAttendees = new System.Windows.Forms.TabPage();
            this.ucAttendees = new CalendarBrowser.AttendeeControl();
            this.pgFreeBusy = new System.Windows.Forms.TabPage();
            this.ucFreeBusy = new CalendarBrowser.FreeBusyControl();
            this.pgReqStats = new System.Windows.Forms.TabPage();
            this.ucRequestStatus = new CalendarBrowser.RequestStatusControl();
            this.txtOrganizer = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.epErrors = new System.Windows.Forms.ErrorProvider(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.cboTimeZone = new System.Windows.Forms.ComboBox();
            this.btnApplyTZ = new System.Windows.Forms.Button();
            this.tabInfo.SuspendLayout();
            this.pgGeneral.SuspendLayout();
            this.pgAttendees.SuspendLayout();
            this.pgFreeBusy.SuspendLayout();
            this.pgReqStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epErrors)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUniqueId
            // 
            this.txtUniqueId.Location = new System.Drawing.Point(96, 12);
            this.txtUniqueId.Name = "txtUniqueId";
            this.txtUniqueId.ReadOnly = true;
            this.txtUniqueId.Size = new System.Drawing.Size(380, 22);
            this.txtUniqueId.TabIndex = 1;
            this.txtUniqueId.TabStop = false;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(14, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 23);
            this.label9.TabIndex = 0;
            this.label9.Text = "Unique ID";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabInfo
            // 
            this.tabInfo.Controls.Add(this.pgGeneral);
            this.tabInfo.Controls.Add(this.pgAttendees);
            this.tabInfo.Controls.Add(this.pgFreeBusy);
            this.tabInfo.Controls.Add(this.pgReqStats);
            this.tabInfo.Location = new System.Drawing.Point(12, 138);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.SelectedIndex = 0;
            this.tabInfo.Size = new System.Drawing.Size(599, 256);
            this.tabInfo.TabIndex = 9;
            // 
            // pgGeneral
            // 
            this.pgGeneral.Controls.Add(this.label2);
            this.pgGeneral.Controls.Add(this.txtDuration);
            this.pgGeneral.Controls.Add(this.dtpEndDate);
            this.pgGeneral.Controls.Add(this.dtpStartDate);
            this.pgGeneral.Controls.Add(this.label4);
            this.pgGeneral.Controls.Add(this.label1);
            this.pgGeneral.Controls.Add(this.txtComments);
            this.pgGeneral.Controls.Add(this.label17);
            this.pgGeneral.Controls.Add(this.txtUrl);
            this.pgGeneral.Controls.Add(this.label3);
            this.pgGeneral.Location = new System.Drawing.Point(4, 25);
            this.pgGeneral.Name = "pgGeneral";
            this.pgGeneral.Size = new System.Drawing.Size(591, 227);
            this.pgGeneral.TabIndex = 3;
            this.pgGeneral.Text = "General";
            this.pgGeneral.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(312, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "End";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDuration
            // 
            this.txtDuration.Location = new System.Drawing.Point(96, 56);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(108, 22);
            this.txtDuration.TabIndex = 5;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Checked = false;
            this.dtpEndDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(358, 16);
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
            this.dtpStartDate.Location = new System.Drawing.Point(96, 16);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.ShowCheckBox = true;
            this.dtpStartDate.Size = new System.Drawing.Size(210, 22);
            this.dtpStartDate.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(15, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 23);
            this.label4.TabIndex = 4;
            this.label4.Text = "Duration";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(40, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Start";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtComments
            // 
            this.txtComments.AcceptsReturn = true;
            this.txtComments.Location = new System.Drawing.Point(96, 136);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComments.Size = new System.Drawing.Size(424, 72);
            this.txtComments.TabIndex = 9;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(10, 136);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(80, 23);
            this.label17.TabIndex = 8;
            this.label17.Text = "&Comments";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(96, 96);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(424, 22);
            this.txtUrl.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(50, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "&URL";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pgAttendees
            // 
            this.pgAttendees.Controls.Add(this.ucAttendees);
            this.pgAttendees.Location = new System.Drawing.Point(4, 25);
            this.pgAttendees.Name = "pgAttendees";
            this.pgAttendees.Size = new System.Drawing.Size(591, 227);
            this.pgAttendees.TabIndex = 0;
            this.pgAttendees.Text = "Attendees";
            this.pgAttendees.UseVisualStyleBackColor = true;
            // 
            // ucAttendees
            // 
            this.ucAttendees.Location = new System.Drawing.Point(47, 5);
            this.ucAttendees.Name = "ucAttendees";
            this.ucAttendees.Size = new System.Drawing.Size(496, 216);
            this.ucAttendees.TabIndex = 0;
            // 
            // pgFreeBusy
            // 
            this.pgFreeBusy.Controls.Add(this.ucFreeBusy);
            this.pgFreeBusy.Location = new System.Drawing.Point(4, 25);
            this.pgFreeBusy.Name = "pgFreeBusy";
            this.pgFreeBusy.Size = new System.Drawing.Size(591, 227);
            this.pgFreeBusy.TabIndex = 1;
            this.pgFreeBusy.Text = "Free/Busy";
            this.pgFreeBusy.UseVisualStyleBackColor = true;
            // 
            // ucFreeBusy
            // 
            this.ucFreeBusy.Location = new System.Drawing.Point(76, 37);
            this.ucFreeBusy.Name = "ucFreeBusy";
            this.ucFreeBusy.Size = new System.Drawing.Size(438, 152);
            this.ucFreeBusy.TabIndex = 0;
            // 
            // pgReqStats
            // 
            this.pgReqStats.Controls.Add(this.ucRequestStatus);
            this.pgReqStats.Location = new System.Drawing.Point(4, 25);
            this.pgReqStats.Name = "pgReqStats";
            this.pgReqStats.Size = new System.Drawing.Size(591, 227);
            this.pgReqStats.TabIndex = 2;
            this.pgReqStats.Text = "Request Status";
            this.pgReqStats.UseVisualStyleBackColor = true;
            // 
            // ucRequestStatus
            // 
            this.ucRequestStatus.Location = new System.Drawing.Point(87, 41);
            this.ucRequestStatus.Name = "ucRequestStatus";
            this.ucRequestStatus.Size = new System.Drawing.Size(416, 144);
            this.ucRequestStatus.TabIndex = 0;
            // 
            // txtOrganizer
            // 
            this.txtOrganizer.Location = new System.Drawing.Point(96, 72);
            this.txtOrganizer.Name = "txtOrganizer";
            this.txtOrganizer.Size = new System.Drawing.Size(437, 22);
            this.txtOrganizer.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(18, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 23);
            this.label5.TabIndex = 5;
            this.label5.Text = "&Organizer";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtContact
            // 
            this.txtContact.Location = new System.Drawing.Point(96, 104);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(437, 22);
            this.txtContact.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(26, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 23);
            this.label6.TabIndex = 7;
            this.label6.Text = "Contact";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(523, 400);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 32);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(12, 400);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 32);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "OK";
            // 
            // epErrors
            // 
            this.epErrors.ContainerControl = this;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(8, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 23);
            this.label7.TabIndex = 2;
            this.label7.Text = "&Time Zone";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTimeZone
            // 
            this.cboTimeZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTimeZone.Location = new System.Drawing.Point(96, 40);
            this.cboTimeZone.MaxDropDownItems = 16;
            this.cboTimeZone.Name = "cboTimeZone";
            this.cboTimeZone.Size = new System.Drawing.Size(437, 24);
            this.cboTimeZone.TabIndex = 3;
            this.cboTimeZone.SelectedIndexChanged += new System.EventHandler(this.cboTimeZone_SelectedIndexChanged);
            // 
            // btnApplyTZ
            // 
            this.btnApplyTZ.Location = new System.Drawing.Point(539, 38);
            this.btnApplyTZ.Name = "btnApplyTZ";
            this.btnApplyTZ.Size = new System.Drawing.Size(64, 28);
            this.btnApplyTZ.TabIndex = 4;
            this.btnApplyTZ.Text = "Apply";
            this.btnApplyTZ.Click += new System.EventHandler(this.btnApplyTZ_Click);
            // 
            // VFreeBusyDlg
            // 
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(623, 444);
            this.Controls.Add(this.btnApplyTZ);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboTimeZone);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtContact);
            this.Controls.Add(this.txtOrganizer);
            this.Controls.Add(this.txtUniqueId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tabInfo);
            this.Controls.Add(this.label9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VFreeBusyDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Free/Busy Properties";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.VFreeBusyDlg_Closing);
            this.tabInfo.ResumeLayout(false);
            this.pgGeneral.ResumeLayout(false);
            this.pgGeneral.PerformLayout();
            this.pgAttendees.ResumeLayout(false);
            this.pgFreeBusy.ResumeLayout(false);
            this.pgReqStats.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.epErrors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUniqueId;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TabControl tabInfo;
        private System.Windows.Forms.TabPage pgAttendees;
        private System.Windows.Forms.TabPage pgFreeBusy;
        private System.Windows.Forms.TabPage pgReqStats;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOrganizer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private CalendarBrowser.AttendeeControl ucAttendees;
        private CalendarBrowser.FreeBusyControl ucFreeBusy;
        private CalendarBrowser.RequestStatusControl ucRequestStatus;
        private System.Windows.Forms.TabPage pgGeneral;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider epErrors;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboTimeZone;
        private System.Windows.Forms.Button btnApplyTZ;
    }
}
