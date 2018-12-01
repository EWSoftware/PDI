namespace CalendarBrowser
{
    partial class FreeBusyControl
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
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cboBusyType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtOtherType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Checked = false;
            this.dtpStartDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(95, 53);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.ShowCheckBox = true;
            this.dtpStartDate.Size = new System.Drawing.Size(255, 26);
            this.dtpStartDate.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(19, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Start";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Checked = false;
            this.dtpEndDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(95, 90);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.ShowCheckBox = true;
            this.dtpEndDate.Size = new System.Drawing.Size(255, 26);
            this.dtpEndDate.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(23, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 23);
            this.label2.TabIndex = 6;
            this.label2.Text = "End";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboBusyType
            // 
            this.cboBusyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBusyType.Items.AddRange(new object[] {
            "None",
            "Free",
            "Busy",
            "Unavailable",
            "Tentative",
            "Other"});
            this.cboBusyType.Location = new System.Drawing.Point(95, 14);
            this.cboBusyType.Name = "cboBusyType";
            this.cboBusyType.Size = new System.Drawing.Size(128, 28);
            this.cboBusyType.TabIndex = 1;
            this.cboBusyType.SelectedIndexChanged += new System.EventHandler(this.cboBusyType_SelectedIndexChanged);
            this.cboBusyType.Validating += new System.ComponentModel.CancelEventHandler(this.cboBusyType_Validating);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(15, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 23);
            this.label10.TabIndex = 0;
            this.label10.Text = "Type";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOtherType
            // 
            this.txtOtherType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOtherType.Location = new System.Drawing.Point(342, 14);
            this.txtOtherType.Name = "txtOtherType";
            this.txtOtherType.Size = new System.Drawing.Size(136, 26);
            this.txtOtherType.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(229, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 23);
            this.label4.TabIndex = 2;
            this.label4.Text = "Other Type";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FreeBusyControl
            // 
            this.Controls.Add(this.txtOtherType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboBusyType);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.label1);
            this.Name = "FreeBusyControl";
            this.Size = new System.Drawing.Size(503, 161);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.dtpStartDate, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.dtpEndDate, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.cboBusyType, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtOtherType, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboBusyType;
        private System.Windows.Forms.TextBox txtOtherType;
    }
}
