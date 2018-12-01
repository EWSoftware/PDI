namespace CalendarBrowser
{
    partial class AlarmControl
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
            this.tabInfo = new System.Windows.Forms.TabControl();
            this.pgGeneral = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.udcRepeat = new System.Windows.Forms.NumericUpDown();
            this.chkFromEnd = new System.Windows.Forms.CheckBox();
            this.txtTrigger = new System.Windows.Forms.TextBox();
            this.dtpTrigger = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOtherAction = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboAction = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pgAttendees = new System.Windows.Forms.TabPage();
            this.ucAttendees = new CalendarBrowser.AttendeeControl();
            this.pgAttachments = new System.Windows.Forms.TabPage();
            this.ucAttachments = new CalendarBrowser.AttachmentsControl();
            this.tabInfo.SuspendLayout();
            this.pgGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udcRepeat)).BeginInit();
            this.pgAttendees.SuspendLayout();
            this.pgAttachments.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabInfo
            // 
            this.tabInfo.Controls.Add(this.pgGeneral);
            this.tabInfo.Controls.Add(this.pgAttendees);
            this.tabInfo.Controls.Add(this.pgAttachments);
            this.tabInfo.Location = new System.Drawing.Point(3, 3);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.SelectedIndex = 0;
            this.tabInfo.Size = new System.Drawing.Size(660, 308);
            this.tabInfo.TabIndex = 0;
            // 
            // pgGeneral
            // 
            this.pgGeneral.Controls.Add(this.label3);
            this.pgGeneral.Controls.Add(this.txtDescription);
            this.pgGeneral.Controls.Add(this.label11);
            this.pgGeneral.Controls.Add(this.txtSummary);
            this.pgGeneral.Controls.Add(this.label8);
            this.pgGeneral.Controls.Add(this.txtDuration);
            this.pgGeneral.Controls.Add(this.label2);
            this.pgGeneral.Controls.Add(this.label15);
            this.pgGeneral.Controls.Add(this.udcRepeat);
            this.pgGeneral.Controls.Add(this.chkFromEnd);
            this.pgGeneral.Controls.Add(this.txtTrigger);
            this.pgGeneral.Controls.Add(this.dtpTrigger);
            this.pgGeneral.Controls.Add(this.label1);
            this.pgGeneral.Controls.Add(this.txtOtherAction);
            this.pgGeneral.Controls.Add(this.label4);
            this.pgGeneral.Controls.Add(this.cboAction);
            this.pgGeneral.Controls.Add(this.label10);
            this.pgGeneral.Location = new System.Drawing.Point(4, 29);
            this.pgGeneral.Name = "pgGeneral";
            this.pgGeneral.Size = new System.Drawing.Size(652, 275);
            this.pgGeneral.TabIndex = 0;
            this.pgGeneral.Text = "General";
            this.pgGeneral.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(20, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 23);
            this.label3.TabIndex = 7;
            this.label3.Text = "or Date/Time";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDescription
            // 
            this.txtDescription.AcceptsReturn = true;
            this.txtDescription.Location = new System.Drawing.Point(153, 195);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(457, 64);
            this.txtDescription.TabIndex = 16;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(25, 197);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(122, 23);
            this.label11.TabIndex = 15;
            this.label11.Text = "Description";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSummary
            // 
            this.txtSummary.Location = new System.Drawing.Point(153, 161);
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.Size = new System.Drawing.Size(457, 26);
            this.txtSummary.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(29, 163);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(118, 23);
            this.label8.TabIndex = 13;
            this.label8.Text = "Summary";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDuration
            // 
            this.txtDuration.Location = new System.Drawing.Point(345, 126);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(192, 26);
            this.txtDuration.TabIndex = 12;
            this.txtDuration.Validating += new System.ComponentModel.CancelEventHandler(this.Duration_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(263, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 23);
            this.label2.TabIndex = 11;
            this.label2.Text = "Duration";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(33, 128);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(114, 23);
            this.label15.TabIndex = 9;
            this.label15.Text = "Repeat";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // udcRepeat
            // 
            this.udcRepeat.Location = new System.Drawing.Point(153, 127);
            this.udcRepeat.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.udcRepeat.Name = "udcRepeat";
            this.udcRepeat.Size = new System.Drawing.Size(85, 26);
            this.udcRepeat.TabIndex = 10;
            this.udcRepeat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkFromEnd
            // 
            this.chkFromEnd.Location = new System.Drawing.Point(289, 61);
            this.chkFromEnd.Name = "chkFromEnd";
            this.chkFromEnd.Size = new System.Drawing.Size(350, 24);
            this.chkFromEnd.TabIndex = 6;
            this.chkFromEnd.Text = "Duration Trigger is relative to end time";
            // 
            // txtTrigger
            // 
            this.txtTrigger.Location = new System.Drawing.Point(153, 59);
            this.txtTrigger.Name = "txtTrigger";
            this.txtTrigger.Size = new System.Drawing.Size(108, 26);
            this.txtTrigger.TabIndex = 5;
            this.txtTrigger.Validating += new System.ComponentModel.CancelEventHandler(this.Duration_Validating);
            // 
            // dtpTrigger
            // 
            this.dtpTrigger.Checked = false;
            this.dtpTrigger.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            this.dtpTrigger.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTrigger.Location = new System.Drawing.Point(153, 93);
            this.dtpTrigger.Name = "dtpTrigger";
            this.dtpTrigger.ShowCheckBox = true;
            this.dtpTrigger.Size = new System.Drawing.Size(255, 26);
            this.dtpTrigger.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Duration Trigger";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOtherAction
            // 
            this.txtOtherAction.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOtherAction.Location = new System.Drawing.Point(411, 23);
            this.txtOtherAction.Name = "txtOtherAction";
            this.txtOtherAction.Size = new System.Drawing.Size(192, 26);
            this.txtOtherAction.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(285, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 23);
            this.label4.TabIndex = 2;
            this.label4.Text = "Other Action";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboAction
            // 
            this.cboAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAction.Items.AddRange(new object[] {
            "Audio",
            "Display",
            "E-Mail",
            "Procedure",
            "Other"});
            this.cboAction.Location = new System.Drawing.Point(153, 23);
            this.cboAction.Name = "cboAction";
            this.cboAction.Size = new System.Drawing.Size(112, 28);
            this.cboAction.TabIndex = 1;
            this.cboAction.SelectedIndexChanged += new System.EventHandler(this.cboAction_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(33, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(114, 23);
            this.label10.TabIndex = 0;
            this.label10.Text = "Action";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pgAttendees
            // 
            this.pgAttendees.Controls.Add(this.ucAttendees);
            this.pgAttendees.Location = new System.Drawing.Point(4, 29);
            this.pgAttendees.Name = "pgAttendees";
            this.pgAttendees.Size = new System.Drawing.Size(647, 270);
            this.pgAttendees.TabIndex = 1;
            this.pgAttendees.Text = "Attendees";
            this.pgAttendees.UseVisualStyleBackColor = true;
            // 
            // ucAttendees
            // 
            this.ucAttendees.Location = new System.Drawing.Point(26, 18);
            this.ucAttendees.Name = "ucAttendees";
            this.ucAttendees.Size = new System.Drawing.Size(594, 234);
            this.ucAttendees.TabIndex = 0;
            // 
            // pgAttachments
            // 
            this.pgAttachments.Controls.Add(this.ucAttachments);
            this.pgAttachments.Location = new System.Drawing.Point(4, 29);
            this.pgAttachments.Name = "pgAttachments";
            this.pgAttachments.Size = new System.Drawing.Size(647, 270);
            this.pgAttachments.TabIndex = 2;
            this.pgAttachments.Text = "Attachments";
            this.pgAttachments.UseVisualStyleBackColor = true;
            // 
            // ucAttachments
            // 
            this.ucAttachments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAttachments.Location = new System.Drawing.Point(0, 0);
            this.ucAttachments.Name = "ucAttachments";
            this.ucAttachments.Size = new System.Drawing.Size(647, 270);
            this.ucAttachments.TabIndex = 0;
            // 
            // AlarmControl
            // 
            this.Controls.Add(this.tabInfo);
            this.Name = "AlarmControl";
            this.Size = new System.Drawing.Size(666, 342);
            this.Leave += new System.EventHandler(this.AlarmControl_Leave);
            this.Controls.SetChildIndex(this.tabInfo, 0);
            this.tabInfo.ResumeLayout(false);
            this.pgGeneral.ResumeLayout(false);
            this.pgGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udcRepeat)).EndInit();
            this.pgAttendees.ResumeLayout(false);
            this.pgAttachments.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabInfo;
        private System.Windows.Forms.TabPage pgGeneral;
        private System.Windows.Forms.TabPage pgAttendees;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSummary;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown udcRepeat;
        private System.Windows.Forms.CheckBox chkFromEnd;
        private System.Windows.Forms.TextBox txtTrigger;
        private System.Windows.Forms.DateTimePicker dtpTrigger;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOtherAction;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboAction;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabPage pgAttachments;
        private System.Windows.Forms.Label label3;
        private CalendarBrowser.AttendeeControl ucAttendees;
        private CalendarBrowser.AttachmentsControl ucAttachments;
    }
}
