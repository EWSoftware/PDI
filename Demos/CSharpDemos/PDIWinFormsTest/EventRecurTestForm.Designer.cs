namespace PDIWinFormsTest
{
    partial class EventRecurTestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventRecurTestForm));
            this.label3 = new System.Windows.Forms.Label();
            this.txtCalendar = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lbDates = new System.Windows.Forms.ListBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboTimeZone = new System.Windows.Forms.ComboBox();
            this.chkInLocalTime = new System.Windows.Forms.CheckBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(372, 24);
            this.label3.TabIndex = 7;
            this.label3.Text = "&Enter a VEVENT, VTODO, or VJOURNAL entry below.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCalendar
            // 
            this.txtCalendar.AcceptsReturn = true;
            this.txtCalendar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCalendar.Location = new System.Drawing.Point(12, 115);
            this.txtCalendar.Multiline = true;
            this.txtCalendar.Name = "txtCalendar";
            this.txtCalendar.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCalendar.Size = new System.Drawing.Size(371, 414);
            this.txtCalendar.TabIndex = 8;
            this.txtCalendar.WordWrap = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(757, 491);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 32);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "Close";
            // 
            // lbDates
            // 
            this.lbDates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDates.ItemHeight = 16;
            this.lbDates.Location = new System.Drawing.Point(389, 128);
            this.lbDates.Name = "lbDates";
            this.lbDates.Size = new System.Drawing.Size(456, 356);
            this.lbDates.TabIndex = 10;
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(663, 491);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(88, 32);
            this.btnTest.TabIndex = 11;
            this.btnTest.Text = "&Test";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(184, 8);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(190, 22);
            this.dtpStartDate.TabIndex = 1;
            this.dtpStartDate.Value = new System.DateTime(2004, 9, 6, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Find instances between";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(418, 8);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(190, 22);
            this.dtpEndDate.TabIndex = 3;
            this.dtpEndDate.Value = new System.DateTime(2004, 9, 6, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(380, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "and";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(262, 23);
            this.label6.TabIndex = 5;
            this.label6.Text = "&Apply this time zone to the component";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTimeZone
            // 
            this.cboTimeZone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTimeZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTimeZone.Location = new System.Drawing.Point(280, 40);
            this.cboTimeZone.MaxDropDownItems = 16;
            this.cboTimeZone.Name = "cboTimeZone";
            this.cboTimeZone.Size = new System.Drawing.Size(447, 24);
            this.cboTimeZone.TabIndex = 6;
            // 
            // chkInLocalTime
            // 
            this.chkInLocalTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkInLocalTime.Location = new System.Drawing.Point(622, 8);
            this.chkInLocalTime.Name = "chkInLocalTime";
            this.chkInLocalTime.Size = new System.Drawing.Size(210, 24);
            this.chkInLocalTime.TabIndex = 4;
            this.chkInLocalTime.Text = "Find instances in local time";
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCount.Location = new System.Drawing.Point(389, 67);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(454, 56);
            this.lblCount.TabIndex = 13;
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EventRecurTestForm
            // 
            this.AcceptButton = this.btnTest;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(857, 535);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.chkInLocalTime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboTimeZone);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.lbDates);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtCalendar);
            this.Controls.Add(this.label3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(865, 575);
            this.Name = "EventRecurTestForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Calendar Event Recurrence";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.EventRecurTestForm_Closing);
            this.Load += new System.EventHandler(this.EventRecurTestForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ListBox lbDates;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCalendar;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkInLocalTime;
        private System.Windows.Forms.ComboBox cboTimeZone;
        internal System.Windows.Forms.Label lblCount;
    }
}
