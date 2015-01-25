namespace PDIWinFormsTest
{
    partial class VTimeZoneTestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VTimeZoneTestForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboSourceTimeZone = new System.Windows.Forms.ComboBox();
            this.dtpSourceDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblLocalBackToSource = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblLocalTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblDestBackToSource = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblDestTime = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboDestTimeZone = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtTimeZoneInfo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSaveTZs = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cboSourceTimeZone);
            this.groupBox1.Controls.Add(this.dtpSourceDate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(735, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source Date";
            // 
            // cboSourceTimeZone
            // 
            this.cboSourceTimeZone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSourceTimeZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSourceTimeZone.Location = new System.Drawing.Point(292, 24);
            this.cboSourceTimeZone.MaxDropDownItems = 16;
            this.cboSourceTimeZone.Name = "cboSourceTimeZone";
            this.cboSourceTimeZone.Size = new System.Drawing.Size(430, 24);
            this.cboSourceTimeZone.TabIndex = 2;
            this.cboSourceTimeZone.SelectedIndexChanged += new System.EventHandler(this.UpdateTimes);
            // 
            // dtpSourceDate
            // 
            this.dtpSourceDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            this.dtpSourceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSourceDate.Location = new System.Drawing.Point(96, 24);
            this.dtpSourceDate.Name = "dtpSourceDate";
            this.dtpSourceDate.Size = new System.Drawing.Size(190, 22);
            this.dtpSourceDate.TabIndex = 1;
            this.dtpSourceDate.Value = new System.DateTime(2004, 9, 6, 0, 0, 0, 0);
            this.dtpSourceDate.ValueChanged += new System.EventHandler(this.UpdateTimes);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Date/Time";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblLocalBackToSource);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.lblLocalTime);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(735, 80);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "To Local Time";
            // 
            // lblLocalBackToSource
            // 
            this.lblLocalBackToSource.Location = new System.Drawing.Point(136, 48);
            this.lblLocalBackToSource.Name = "lblLocalBackToSource";
            this.lblLocalBackToSource.Size = new System.Drawing.Size(580, 23);
            this.lblLocalBackToSource.TabIndex = 3;
            this.lblLocalBackToSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 23);
            this.label5.TabIndex = 2;
            this.label5.Text = "Back to Source";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLocalTime
            // 
            this.lblLocalTime.Location = new System.Drawing.Point(136, 24);
            this.lblLocalTime.Name = "lblLocalTime";
            this.lblLocalTime.Size = new System.Drawing.Size(580, 23);
            this.lblLocalTime.TabIndex = 1;
            this.lblLocalTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(24, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Local Time";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.lblDestBackToSource);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.lblDestTime);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cboDestTimeZone);
            this.groupBox3.Location = new System.Drawing.Point(12, 160);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(735, 112);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "To Other Time Zone";
            // 
            // lblDestBackToSource
            // 
            this.lblDestBackToSource.Location = new System.Drawing.Point(132, 80);
            this.lblDestBackToSource.Name = "lblDestBackToSource";
            this.lblDestBackToSource.Size = new System.Drawing.Size(580, 23);
            this.lblDestBackToSource.TabIndex = 5;
            this.lblDestBackToSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(3, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 23);
            this.label8.TabIndex = 4;
            this.label8.Text = "Back to Source";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDestTime
            // 
            this.lblDestTime.Location = new System.Drawing.Point(132, 56);
            this.lblDestTime.Name = "lblDestTime";
            this.lblDestTime.Size = new System.Drawing.Size(580, 23);
            this.lblDestTime.TabIndex = 3;
            this.lblDestTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(24, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 23);
            this.label10.TabIndex = 2;
            this.label10.Text = "Dest. Time";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(152, 23);
            this.label6.TabIndex = 0;
            this.label6.Text = "Destination &Time Zone";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDestTimeZone
            // 
            this.cboDestTimeZone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDestTimeZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDestTimeZone.Location = new System.Drawing.Point(168, 24);
            this.cboDestTimeZone.MaxDropDownItems = 16;
            this.cboDestTimeZone.Name = "cboDestTimeZone";
            this.cboDestTimeZone.Size = new System.Drawing.Size(430, 24);
            this.cboDestTimeZone.TabIndex = 1;
            this.cboDestTimeZone.SelectedIndexChanged += new System.EventHandler(this.UpdateTimes);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(659, 463);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 32);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            // 
            // txtTimeZoneInfo
            // 
            this.txtTimeZoneInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimeZoneInfo.Location = new System.Drawing.Point(12, 312);
            this.txtTimeZoneInfo.Multiline = true;
            this.txtTimeZoneInfo.Name = "txtTimeZoneInfo";
            this.txtTimeZoneInfo.ReadOnly = true;
            this.txtTimeZoneInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTimeZoneInfo.Size = new System.Drawing.Size(735, 143);
            this.txtTimeZoneInfo.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 286);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "Time Zone Settings";
            // 
            // btnSaveTZs
            // 
            this.btnSaveTZs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveTZs.Location = new System.Drawing.Point(12, 463);
            this.btnSaveTZs.Name = "btnSaveTZs";
            this.btnSaveTZs.Size = new System.Drawing.Size(88, 32);
            this.btnSaveTZs.TabIndex = 5;
            this.btnSaveTZs.Text = "&Save TZs";
            this.btnSaveTZs.Click += new System.EventHandler(this.btnSaveTZs_Click);
            // 
            // VTimeZoneTestForm
            // 
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(759, 507);
            this.Controls.Add(this.btnSaveTZs);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTimeZoneInfo);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(777, 552);
            this.Name = "VTimeZoneTestForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Time Zone Features";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.VTimeZoneTestForm_Closing);
            this.Load += new System.EventHandler(this.VTimeZoneTestForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpSourceDate;
        private System.Windows.Forms.ComboBox cboSourceTimeZone;
        private System.Windows.Forms.ComboBox cboDestTimeZone;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblLocalTime;
        private System.Windows.Forms.Label lblLocalBackToSource;
        private System.Windows.Forms.Label lblDestBackToSource;
        private System.Windows.Forms.Label lblDestTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTimeZoneInfo;
        private System.Windows.Forms.Button btnSaveTZs;
    }
}
