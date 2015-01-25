namespace EWSoftware.PDI.Windows.Forms
{
    partial class AdvancedPattern
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
		/// Required method for Designer support - do not modify the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedPattern));
            this.label2 = new System.Windows.Forms.Label();
            this.udcInterval = new System.Windows.Forms.NumericUpDown();
            this.lblInterval = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtByWeekNo = new System.Windows.Forms.TextBox();
            this.txtByYearDay = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtByMonthDay = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtByHour = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtByMinute = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBySecond = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBySetPos = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lbByMonth = new System.Windows.Forms.CheckedListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lbByDay = new System.Windows.Forms.ListBox();
            this.label11 = new System.Windows.Forms.Label();
            this.udcDayInstance = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cboDOW = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.udcInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcDayInstance)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // udcInterval
            // 
            resources.ApplyResources(this.udcInterval, "udcInterval");
            this.udcInterval.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.udcInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udcInterval.Name = "udcInterval";
            this.udcInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblInterval
            // 
            resources.ApplyResources(this.lblInterval, "lblInterval");
            this.lblInterval.Name = "lblInterval";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtByWeekNo
            // 
            resources.ApplyResources(this.txtByWeekNo, "txtByWeekNo");
            this.txtByWeekNo.Name = "txtByWeekNo";
            this.txtByWeekNo.TextChanged += new System.EventHandler(this.SetDayInstanceEnabledState);
            // 
            // txtByYearDay
            // 
            resources.ApplyResources(this.txtByYearDay, "txtByYearDay");
            this.txtByYearDay.Name = "txtByYearDay";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txtByMonthDay
            // 
            resources.ApplyResources(this.txtByMonthDay, "txtByMonthDay");
            this.txtByMonthDay.Name = "txtByMonthDay";
            this.txtByMonthDay.TextChanged += new System.EventHandler(this.SetDayInstanceEnabledState);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // txtByHour
            // 
            resources.ApplyResources(this.txtByHour, "txtByHour");
            this.txtByHour.Name = "txtByHour";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtByMinute
            // 
            resources.ApplyResources(this.txtByMinute, "txtByMinute");
            this.txtByMinute.Name = "txtByMinute";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // txtBySecond
            // 
            resources.ApplyResources(this.txtBySecond, "txtBySecond");
            this.txtBySecond.Name = "txtBySecond";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtBySetPos
            // 
            resources.ApplyResources(this.txtBySetPos, "txtBySetPos");
            this.txtBySetPos.Name = "txtBySetPos";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // lbByMonth
            // 
            this.lbByMonth.CheckOnClick = true;
            resources.ApplyResources(this.lbByMonth, "lbByMonth");
            this.lbByMonth.Name = "lbByMonth";
            this.lbByMonth.Leave += new System.EventHandler(this.SetDayInstanceEnabledState);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // lbByDay
            // 
            resources.ApplyResources(this.lbByDay, "lbByDay");
            this.lbByDay.Name = "lbByDay";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // udcDayInstance
            // 
            resources.ApplyResources(this.udcDayInstance, "udcDayInstance");
            this.udcDayInstance.Maximum = new decimal(new int[] {
            53,
            0,
            0,
            0});
            this.udcDayInstance.Minimum = new decimal(new int[] {
            53,
            0,
            0,
            -2147483648});
            this.udcDayInstance.Name = "udcDayInstance";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // cboDOW
            // 
            this.cboDOW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboDOW, "cboDOW");
            this.cboDOW.Name = "cboDOW";
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            resources.ApplyResources(this.btnRemove, "btnRemove");
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.Name = "btnClear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // AdvancedPattern
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cboDOW);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.udcDayInstance);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lbByDay);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lbByMonth);
            this.Controls.Add(this.txtBySetPos);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtBySecond);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtByMinute);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtByHour);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtByMonthDay);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtByYearDay);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtByWeekNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.udcInterval);
            this.Controls.Add(this.lblInterval);
            this.Name = "AdvancedPattern";
            ((System.ComponentModel.ISupportInitialize)(this.udcInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcDayInstance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblInterval;
        private System.Windows.Forms.NumericUpDown udcInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtByWeekNo;
        private System.Windows.Forms.TextBox txtByYearDay;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtByMonthDay;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtByHour;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtByMinute;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtBySecond;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckedListBox lbByMonth;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListBox lbByDay;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown udcDayInstance;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cboDOW;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtBySetPos;
    }
}
