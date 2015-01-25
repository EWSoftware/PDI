namespace EWSoftware.PDI.Windows.Forms
{
    partial class YearlyPattern
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
		/// Required method for Designer support - do not modify the contents of this method with the code editor
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YearlyPattern));
            this.label4 = new System.Windows.Forms.Label();
            this.cboDOW = new System.Windows.Forms.ComboBox();
            this.cboOccurrence = new System.Windows.Forms.ComboBox();
            this.rbDayOfWeek = new System.Windows.Forms.RadioButton();
            this.rbDayXEveryYYears = new System.Windows.Forms.RadioButton();
            this.cboDOWMonth = new System.Windows.Forms.ComboBox();
            this.cboMonth = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.udcDay = new System.Windows.Forms.NumericUpDown();
            this.udcYears = new System.Windows.Forms.NumericUpDown();
            this.udcDOWYears = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.udcDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcYears)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcDOWYears)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // cboDOW
            // 
            this.cboDOW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboDOW, "cboDOW");
            this.cboDOW.Name = "cboDOW";
            // 
            // cboOccurrence
            // 
            this.cboOccurrence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboOccurrence, "cboOccurrence");
            this.cboOccurrence.Name = "cboOccurrence";
            // 
            // rbDayOfWeek
            // 
            resources.ApplyResources(this.rbDayOfWeek, "rbDayOfWeek");
            this.rbDayOfWeek.Name = "rbDayOfWeek";
            this.rbDayOfWeek.CheckedChanged += new System.EventHandler(this.Yearly_CheckedChanged);
            // 
            // rbDayXEveryYYears
            // 
            resources.ApplyResources(this.rbDayXEveryYYears, "rbDayXEveryYYears");
            this.rbDayXEveryYYears.Name = "rbDayXEveryYYears";
            this.rbDayXEveryYYears.CheckedChanged += new System.EventHandler(this.Yearly_CheckedChanged);
            // 
            // cboDOWMonth
            // 
            this.cboDOWMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboDOWMonth, "cboDOWMonth");
            this.cboDOWMonth.Name = "cboDOWMonth";
            // 
            // cboMonth
            // 
            this.cboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboMonth, "cboMonth");
            this.cboMonth.Name = "cboMonth";
            this.cboMonth.SelectedIndexChanged += new System.EventHandler(this.cboMonth_SelectedIndexChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
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
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // udcDay
            // 
            resources.ApplyResources(this.udcDay, "udcDay");
            this.udcDay.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.udcDay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udcDay.Name = "udcDay";
            this.udcDay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // udcYears
            // 
            resources.ApplyResources(this.udcYears, "udcYears");
            this.udcYears.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.udcYears.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udcYears.Name = "udcYears";
            this.udcYears.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // udcDOWYears
            // 
            resources.ApplyResources(this.udcDOWYears, "udcDOWYears");
            this.udcDOWYears.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.udcDOWYears.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udcDOWYears.Name = "udcDOWYears";
            this.udcDOWYears.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // YearlyPattern
            // 
            this.Controls.Add(this.udcDOWYears);
            this.Controls.Add(this.udcYears);
            this.Controls.Add(this.udcDay);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboMonth);
            this.Controls.Add(this.cboDOWMonth);
            this.Controls.Add(this.cboDOW);
            this.Controls.Add(this.cboOccurrence);
            this.Controls.Add(this.rbDayOfWeek);
            this.Controls.Add(this.rbDayXEveryYYears);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Name = "YearlyPattern";
            resources.ApplyResources(this, "$this");
            ((System.ComponentModel.ISupportInitialize)(this.udcDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcYears)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcDOWYears)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboDOW;
        private System.Windows.Forms.ComboBox cboOccurrence;
        private System.Windows.Forms.RadioButton rbDayOfWeek;
        private System.Windows.Forms.ComboBox cboDOWMonth;
        private System.Windows.Forms.ComboBox cboMonth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown udcDay;
        private System.Windows.Forms.NumericUpDown udcYears;
        private System.Windows.Forms.NumericUpDown udcDOWYears;
        private System.Windows.Forms.RadioButton rbDayXEveryYYears;
    }
}
