namespace CalendarBrowser
{
    partial class ObservanceRuleControl
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
            this.tabTimeZone = new System.Windows.Forms.TabControl();
            this.pgGeneral = new System.Windows.Forms.TabPage();
            this.cboRuleType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.udcToMinutes = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.udcToHours = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTZName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.udcFromMinutes = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.udcFromHours = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.pgRules = new System.Windows.Forms.TabPage();
            this.rcRulesDates = new CalendarBrowser.RecurrenceControl();
            this.tabTimeZone.SuspendLayout();
            this.pgGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udcToMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcToHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcFromMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcFromHours)).BeginInit();
            this.pgRules.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabTimeZone
            // 
            this.tabTimeZone.Controls.Add(this.pgGeneral);
            this.tabTimeZone.Controls.Add(this.pgRules);
            this.tabTimeZone.Location = new System.Drawing.Point(3, 3);
            this.tabTimeZone.Name = "tabTimeZone";
            this.tabTimeZone.SelectedIndex = 0;
            this.tabTimeZone.Size = new System.Drawing.Size(694, 355);
            this.tabTimeZone.TabIndex = 0;
            // 
            // pgGeneral
            // 
            this.pgGeneral.Controls.Add(this.cboRuleType);
            this.pgGeneral.Controls.Add(this.label10);
            this.pgGeneral.Controls.Add(this.label7);
            this.pgGeneral.Controls.Add(this.udcToMinutes);
            this.pgGeneral.Controls.Add(this.label8);
            this.pgGeneral.Controls.Add(this.udcToHours);
            this.pgGeneral.Controls.Add(this.label9);
            this.pgGeneral.Controls.Add(this.label6);
            this.pgGeneral.Controls.Add(this.txtComment);
            this.pgGeneral.Controls.Add(this.label5);
            this.pgGeneral.Controls.Add(this.txtTZName);
            this.pgGeneral.Controls.Add(this.label4);
            this.pgGeneral.Controls.Add(this.udcFromMinutes);
            this.pgGeneral.Controls.Add(this.label3);
            this.pgGeneral.Controls.Add(this.udcFromHours);
            this.pgGeneral.Controls.Add(this.label2);
            this.pgGeneral.Controls.Add(this.dtpStartDate);
            this.pgGeneral.Controls.Add(this.label1);
            this.pgGeneral.Location = new System.Drawing.Point(4, 29);
            this.pgGeneral.Name = "pgGeneral";
            this.pgGeneral.Size = new System.Drawing.Size(686, 322);
            this.pgGeneral.TabIndex = 0;
            this.pgGeneral.Text = "General";
            this.pgGeneral.UseVisualStyleBackColor = true;
            // 
            // cboRuleType
            // 
            this.cboRuleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRuleType.Location = new System.Drawing.Point(174, 24);
            this.cboRuleType.Name = "cboRuleType";
            this.cboRuleType.Size = new System.Drawing.Size(112, 28);
            this.cboRuleType.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(100, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 23);
            this.label10.TabIndex = 0;
            this.label10.Text = "Type";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(384, 137);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 23);
            this.label7.TabIndex = 15;
            this.label7.Text = "Minutes";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // udcToMinutes
            // 
            this.udcToMinutes.Location = new System.Drawing.Point(318, 136);
            this.udcToMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.udcToMinutes.Minimum = new decimal(new int[] {
            59,
            0,
            0,
            -2147483648});
            this.udcToMinutes.Name = "udcToMinutes";
            this.udcToMinutes.Size = new System.Drawing.Size(60, 26);
            this.udcToMinutes.TabIndex = 14;
            this.udcToMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udcToMinutes.Validating += new System.ComponentModel.CancelEventHandler(this.Minutes_Validating);
            // 
            // label8
            // 
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(240, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 23);
            this.label8.TabIndex = 13;
            this.label8.Text = "Hours";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // udcToHours
            // 
            this.udcToHours.Location = new System.Drawing.Point(174, 136);
            this.udcToHours.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.udcToHours.Minimum = new decimal(new int[] {
            23,
            0,
            0,
            -2147483648});
            this.udcToHours.Name = "udcToHours";
            this.udcToHours.Size = new System.Drawing.Size(60, 26);
            this.udcToHours.TabIndex = 12;
            this.udcToHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(39, 137);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(129, 23);
            this.label9.TabIndex = 11;
            this.label9.Text = "Offset To";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(384, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 23);
            this.label6.TabIndex = 10;
            this.label6.Text = "Minutes";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtComment
            // 
            this.txtComment.AcceptsReturn = true;
            this.txtComment.Location = new System.Drawing.Point(174, 176);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComment.Size = new System.Drawing.Size(455, 118);
            this.txtComment.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(43, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 23);
            this.label5.TabIndex = 16;
            this.label5.Text = "Comment";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTZName
            // 
            this.txtTZName.Location = new System.Drawing.Point(174, 64);
            this.txtTZName.Name = "txtTZName";
            this.txtTZName.Size = new System.Drawing.Size(264, 26);
            this.txtTZName.TabIndex = 5;
            this.txtTZName.Validating += new System.ComponentModel.CancelEventHandler(this.txtTZName_Validating);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 23);
            this.label4.TabIndex = 4;
            this.label4.Text = "Time Zone Name";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // udcFromMinutes
            // 
            this.udcFromMinutes.Location = new System.Drawing.Point(318, 104);
            this.udcFromMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.udcFromMinutes.Minimum = new decimal(new int[] {
            59,
            0,
            0,
            -2147483648});
            this.udcFromMinutes.Name = "udcFromMinutes";
            this.udcFromMinutes.Size = new System.Drawing.Size(60, 26);
            this.udcFromMinutes.TabIndex = 9;
            this.udcFromMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udcFromMinutes.Validating += new System.ComponentModel.CancelEventHandler(this.Minutes_Validating);
            // 
            // label3
            // 
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(240, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 23);
            this.label3.TabIndex = 8;
            this.label3.Text = "Hours";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // udcFromHours
            // 
            this.udcFromHours.Location = new System.Drawing.Point(174, 104);
            this.udcFromHours.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.udcFromHours.Minimum = new decimal(new int[] {
            23,
            0,
            0,
            -2147483648});
            this.udcFromHours.Name = "udcFromHours";
            this.udcFromHours.Size = new System.Drawing.Size(60, 26);
            this.udcFromHours.TabIndex = 7;
            this.udcFromHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(35, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 23);
            this.label2.TabIndex = 6;
            this.label2.Text = "Offset From";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Checked = false;
            this.dtpStartDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(394, 24);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(235, 26);
            this.dtpStartDate.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(325, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "&Start";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pgRules
            // 
            this.pgRules.Controls.Add(this.rcRulesDates);
            this.pgRules.Location = new System.Drawing.Point(4, 29);
            this.pgRules.Name = "pgRules";
            this.pgRules.Size = new System.Drawing.Size(686, 322);
            this.pgRules.TabIndex = 1;
            this.pgRules.Text = "Rules";
            this.pgRules.UseVisualStyleBackColor = true;
            // 
            // rcRulesDates
            // 
            this.rcRulesDates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rcRulesDates.Location = new System.Drawing.Point(0, 0);
            this.rcRulesDates.Name = "rcRulesDates";
            this.rcRulesDates.Size = new System.Drawing.Size(686, 322);
            this.rcRulesDates.TabIndex = 0;
            // 
            // ObservanceRuleControl
            // 
            this.Controls.Add(this.tabTimeZone);
            this.Name = "ObservanceRuleControl";
            this.Size = new System.Drawing.Size(701, 390);
            this.Leave += new System.EventHandler(this.ObservanceRuleControl_Leave);
            this.Controls.SetChildIndex(this.tabTimeZone, 0);
            this.tabTimeZone.ResumeLayout(false);
            this.pgGeneral.ResumeLayout(false);
            this.pgGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udcToMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcToHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcFromMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcFromHours)).EndInit();
            this.pgRules.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown udcToMinutes;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown udcToHours;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTZName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown udcFromMinutes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown udcFromHours;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabTimeZone;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboRuleType;
        private CalendarBrowser.RecurrenceControl rcRulesDates;
        private System.Windows.Forms.TabPage pgGeneral;
        private System.Windows.Forms.TabPage pgRules;
    }
}
