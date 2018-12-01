namespace CalendarBrowser
{
    partial class RecurrenceControl
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
            this.btnClearRRules = new System.Windows.Forms.Button();
            this.btnRemoveRRule = new System.Windows.Forms.Button();
            this.btnAddRRule = new System.Windows.Forms.Button();
            this.lbRRules = new System.Windows.Forms.ListBox();
            this.btnClearRDates = new System.Windows.Forms.Button();
            this.btnRemoveRDate = new System.Windows.Forms.Button();
            this.btnAddRDate = new System.Windows.Forms.Button();
            this.lbRDates = new System.Windows.Forms.ListBox();
            this.lblRRules = new System.Windows.Forms.Label();
            this.lblRDates = new System.Windows.Forms.Label();
            this.dtpRDate = new System.Windows.Forms.DateTimePicker();
            this.btnEditRRule = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkExcludeDay = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnClearRRules
            // 
            this.btnClearRRules.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClearRRules.Location = new System.Drawing.Point(285, 282);
            this.btnClearRRules.Name = "btnClearRRules";
            this.btnClearRRules.Size = new System.Drawing.Size(88, 32);
            this.btnClearRRules.TabIndex = 5;
            this.btnClearRRules.Text = "Clear";
            this.btnClearRRules.Click += new System.EventHandler(this.btnClearRRules_Click);
            // 
            // btnRemoveRRule
            // 
            this.btnRemoveRRule.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemoveRRule.Location = new System.Drawing.Point(191, 282);
            this.btnRemoveRRule.Name = "btnRemoveRRule";
            this.btnRemoveRRule.Size = new System.Drawing.Size(88, 32);
            this.btnRemoveRRule.TabIndex = 4;
            this.btnRemoveRRule.Text = "Remove";
            this.btnRemoveRRule.Click += new System.EventHandler(this.btnRemoveRRule_Click);
            // 
            // btnAddRRule
            // 
            this.btnAddRRule.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddRRule.Location = new System.Drawing.Point(3, 282);
            this.btnAddRRule.Name = "btnAddRRule";
            this.btnAddRRule.Size = new System.Drawing.Size(88, 32);
            this.btnAddRRule.TabIndex = 2;
            this.btnAddRRule.Text = "Add";
            this.btnAddRRule.Click += new System.EventHandler(this.btnAddRRule_Click);
            // 
            // lbRRules
            // 
            this.lbRRules.HorizontalExtent = 800;
            this.lbRRules.HorizontalScrollbar = true;
            this.lbRRules.IntegralHeight = false;
            this.lbRRules.ItemHeight = 20;
            this.lbRRules.Location = new System.Drawing.Point(3, 34);
            this.lbRRules.Name = "lbRRules";
            this.lbRRules.Size = new System.Drawing.Size(370, 242);
            this.lbRRules.TabIndex = 1;
            // 
            // btnClearRDates
            // 
            this.btnClearRDates.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClearRDates.Location = new System.Drawing.Point(584, 282);
            this.btnClearRDates.Name = "btnClearRDates";
            this.btnClearRDates.Size = new System.Drawing.Size(88, 32);
            this.btnClearRDates.TabIndex = 13;
            this.btnClearRDates.Text = "Clear";
            this.btnClearRDates.Click += new System.EventHandler(this.btnClearRDates_Click);
            // 
            // btnRemoveRDate
            // 
            this.btnRemoveRDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemoveRDate.Location = new System.Drawing.Point(490, 282);
            this.btnRemoveRDate.Name = "btnRemoveRDate";
            this.btnRemoveRDate.Size = new System.Drawing.Size(88, 32);
            this.btnRemoveRDate.TabIndex = 12;
            this.btnRemoveRDate.Text = "Remove";
            this.btnRemoveRDate.Click += new System.EventHandler(this.btnRemoveRDate_Click);
            // 
            // btnAddRDate
            // 
            this.btnAddRDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddRDate.Location = new System.Drawing.Point(396, 282);
            this.btnAddRDate.Name = "btnAddRDate";
            this.btnAddRDate.Size = new System.Drawing.Size(88, 32);
            this.btnAddRDate.TabIndex = 11;
            this.btnAddRDate.Text = "Add";
            this.btnAddRDate.Click += new System.EventHandler(this.btnAddRDate_Click);
            // 
            // lbRDates
            // 
            this.lbRDates.IntegralHeight = false;
            this.lbRDates.ItemHeight = 20;
            this.lbRDates.Location = new System.Drawing.Point(396, 96);
            this.lbRDates.Name = "lbRDates";
            this.lbRDates.Size = new System.Drawing.Size(276, 180);
            this.lbRDates.TabIndex = 10;
            // 
            // lblRRules
            // 
            this.lblRRules.Location = new System.Drawing.Point(3, 8);
            this.lblRRules.Name = "lblRRules";
            this.lblRRules.Size = new System.Drawing.Size(216, 23);
            this.lblRRules.TabIndex = 0;
            this.lblRRules.Text = "Recurrence &Rules";
            this.lblRRules.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRDates
            // 
            this.lblRDates.Location = new System.Drawing.Point(397, 8);
            this.lblRDates.Name = "lblRDates";
            this.lblRDates.Size = new System.Drawing.Size(174, 23);
            this.lblRDates.TabIndex = 7;
            this.lblRDates.Text = "Recurrence &Dates";
            this.lblRDates.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpRDate
            // 
            this.dtpRDate.Checked = false;
            this.dtpRDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            this.dtpRDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRDate.Location = new System.Drawing.Point(395, 34);
            this.dtpRDate.Name = "dtpRDate";
            this.dtpRDate.Size = new System.Drawing.Size(235, 26);
            this.dtpRDate.TabIndex = 8;
            // 
            // btnEditRRule
            // 
            this.btnEditRRule.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEditRRule.Location = new System.Drawing.Point(97, 282);
            this.btnEditRRule.Name = "btnEditRRule";
            this.btnEditRRule.Size = new System.Drawing.Size(88, 32);
            this.btnEditRRule.TabIndex = 3;
            this.btnEditRRule.Text = "Edit";
            this.btnEditRRule.Click += new System.EventHandler(this.btnEditRRule_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(387, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(2, 300);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // chkExcludeDay
            // 
            this.chkExcludeDay.Location = new System.Drawing.Point(393, 66);
            this.chkExcludeDay.Name = "chkExcludeDay";
            this.chkExcludeDay.Size = new System.Drawing.Size(201, 24);
            this.chkExcludeDay.TabIndex = 9;
            this.chkExcludeDay.Text = "Exclude whole day";
            // 
            // RecurrenceControl
            // 
            this.Controls.Add(this.chkExcludeDay);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnEditRRule);
            this.Controls.Add(this.dtpRDate);
            this.Controls.Add(this.lblRDates);
            this.Controls.Add(this.lblRRules);
            this.Controls.Add(this.btnClearRDates);
            this.Controls.Add(this.btnRemoveRDate);
            this.Controls.Add(this.btnAddRDate);
            this.Controls.Add(this.lbRDates);
            this.Controls.Add(this.btnClearRRules);
            this.Controls.Add(this.btnRemoveRRule);
            this.Controls.Add(this.btnAddRRule);
            this.Controls.Add(this.lbRRules);
            this.Name = "RecurrenceControl";
            this.Size = new System.Drawing.Size(685, 320);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClearRRules;
        private System.Windows.Forms.Button btnRemoveRRule;
        private System.Windows.Forms.Button btnAddRRule;
        private System.Windows.Forms.ListBox lbRRules;
        private System.Windows.Forms.Button btnClearRDates;
        private System.Windows.Forms.Button btnRemoveRDate;
        private System.Windows.Forms.Button btnAddRDate;
        private System.Windows.Forms.ListBox lbRDates;
        private System.Windows.Forms.Label lblRRules;
        private System.Windows.Forms.Label lblRDates;
        private System.Windows.Forms.DateTimePicker dtpRDate;
        private System.Windows.Forms.Button btnEditRRule;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkExcludeDay;
    }
}
