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
            this.label3.Location = new System.Drawing.Point(14, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(417, 23);
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
            this.txtCalendar.Location = new System.Drawing.Point(18, 145);
            this.txtCalendar.Multiline = true;
            this.txtCalendar.Name = "txtCalendar";
            this.txtCalendar.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCalendar.Size = new System.Drawing.Size(413, 379);
            this.txtCalendar.TabIndex = 8;
            this.txtCalendar.WordWrap = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(878, 530);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 32);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "Close";
            // 
            // lbDates
            // 
            this.lbDates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDates.HorizontalScrollbar = true;
            this.lbDates.ItemHeight = 20;
            this.lbDates.Location = new System.Drawing.Point(437, 177);
            this.lbDates.Name = "lbDates";
            this.lbDates.Size = new System.Drawing.Size(529, 344);
            this.lbDates.TabIndex = 10;
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(784, 530);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(88, 32);
            this.btnTest.TabIndex = 11;
            this.btnTest.Text = "&Test";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Checked = false;
            this.dtpStartDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(329, 12);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(235, 26);
            this.dtpStartDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(124, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Find instances between";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Checked = false;
            this.dtpEndDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(627, 12);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(235, 26);
            this.dtpEndDate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(570, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "and";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(10, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(313, 23);
            this.label6.TabIndex = 5;
            this.label6.Text = "&Apply this time zone to the component";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTimeZone
            // 
            this.cboTimeZone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTimeZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTimeZone.Location = new System.Drawing.Point(329, 74);
            this.cboTimeZone.MaxDropDownItems = 16;
            this.cboTimeZone.Name = "cboTimeZone";
            this.cboTimeZone.Size = new System.Drawing.Size(568, 28);
            this.cboTimeZone.TabIndex = 6;
            // 
            // chkInLocalTime
            // 
            this.chkInLocalTime.Location = new System.Drawing.Point(329, 44);
            this.chkInLocalTime.Name = "chkInLocalTime";
            this.chkInLocalTime.Size = new System.Drawing.Size(310, 24);
            this.chkInLocalTime.TabIndex = 4;
            this.chkInLocalTime.Text = "Find instances in local time";
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCount.Location = new System.Drawing.Point(437, 118);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(529, 56);
            this.lblCount.TabIndex = 9;
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EventRecurTestForm
            // 
            this.AcceptButton = this.btnTest;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(978, 574);
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
            this.MinimumSize = new System.Drawing.Size(1000, 575);
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
