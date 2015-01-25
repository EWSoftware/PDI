namespace PDIWinFormsTest
{
    partial class RRuleTestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RRuleTestForm));
            this.label3 = new System.Windows.Forms.Label();
            this.txtRRULE = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lbDates = new System.Windows.Forms.ListBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.hmHolidays = new EWSoftware.PDI.Windows.Forms.HolidayManager();
            this.btnDesign = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDescribe = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(14, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "RR&ULE";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRRULE
            // 
            this.txtRRULE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRRULE.Location = new System.Drawing.Point(76, 41);
            this.txtRRULE.Multiline = true;
            this.txtRRULE.Name = "txtRRULE";
            this.txtRRULE.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRRULE.Size = new System.Drawing.Size(590, 64);
            this.txtRRULE.TabIndex = 3;
            this.txtRRULE.Text = "FREQ=DAILY;INTERVAL=5;COUNT=50";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(674, 456);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 32);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            // 
            // lbDates
            // 
            this.lbDates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbDates.FormattingEnabled = true;
            this.lbDates.ItemHeight = 16;
            this.lbDates.Location = new System.Drawing.Point(12, 169);
            this.lbDates.Name = "lbDates";
            this.lbDates.Size = new System.Drawing.Size(386, 276);
            this.lbDates.TabIndex = 7;
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(580, 456);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(88, 32);
            this.btnTest.TabIndex = 9;
            this.btnTest.Text = "&Test";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(76, 12);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(190, 22);
            this.dtpStartDate.TabIndex = 1;
            this.dtpStartDate.Value = new System.DateTime(2004, 9, 6, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(21, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Start";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCount.Location = new System.Drawing.Point(12, 112);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(750, 48);
            this.lblCount.TabIndex = 6;
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // hmHolidays
            // 
            this.hmHolidays.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hmHolidays.Location = new System.Drawing.Point(8, 24);
            this.hmHolidays.Name = "hmHolidays";
            this.hmHolidays.ShowLoadSaveControls = false;
            this.hmHolidays.Size = new System.Drawing.Size(342, 256);
            this.hmHolidays.TabIndex = 0;
            // 
            // btnDesign
            // 
            this.btnDesign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDesign.Location = new System.Drawing.Point(674, 38);
            this.btnDesign.Name = "btnDesign";
            this.btnDesign.Size = new System.Drawing.Size(88, 32);
            this.btnDesign.TabIndex = 4;
            this.btnDesign.Text = "Desig&n";
            this.btnDesign.Click += new System.EventHandler(this.btnDesign_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.hmHolidays);
            this.groupBox1.Location = new System.Drawing.Point(404, 157);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(358, 288);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "H&olidays";
            // 
            // btnDescribe
            // 
            this.btnDescribe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDescribe.Location = new System.Drawing.Point(674, 76);
            this.btnDescribe.Name = "btnDescribe";
            this.btnDescribe.Size = new System.Drawing.Size(88, 32);
            this.btnDescribe.TabIndex = 5;
            this.btnDescribe.Text = "Descri&be";
            this.btnDescribe.Click += new System.EventHandler(this.btnDescribe_Click);
            // 
            // RRuleTestForm
            // 
            this.AcceptButton = this.btnTest;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(774, 500);
            this.Controls.Add(this.btnDescribe);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDesign);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRRULE);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.lbDates);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblCount);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(768, 528);
            this.Name = "RRuleTestForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Recurrence Rule Parsing and Generation";
            this.Closed += new System.EventHandler(this.RRuleTestForm_Closed);
            this.Load += new System.EventHandler(this.RRuleTestForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRRULE;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ListBox lbDates;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCount;
        private EWSoftware.PDI.Windows.Forms.HolidayManager hmHolidays;
        private System.Windows.Forms.Button btnDesign;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDescribe;

    }
}
