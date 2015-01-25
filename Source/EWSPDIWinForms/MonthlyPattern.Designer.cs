namespace EWSoftware.PDI.Windows.Forms
{
    partial class MonthlyPattern
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonthlyPattern));
            this.label1 = new System.Windows.Forms.Label();
            this.rbDayOfWeek = new System.Windows.Forms.RadioButton();
            this.rbDayXEveryYMonths = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.cboOccurrence = new System.Windows.Forms.ComboBox();
            this.cboDOW = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.udcDay = new System.Windows.Forms.NumericUpDown();
            this.udcMonths = new System.Windows.Forms.NumericUpDown();
            this.udcDOWMonths = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.udcDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcMonths)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcDOWMonths)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // rbDayOfWeek
            // 
            resources.ApplyResources(this.rbDayOfWeek, "rbDayOfWeek");
            this.rbDayOfWeek.Name = "rbDayOfWeek";
            this.rbDayOfWeek.CheckedChanged += new System.EventHandler(this.Monthly_CheckedChanged);
            // 
            // rbDayXEveryYMonths
            // 
            resources.ApplyResources(this.rbDayXEveryYMonths, "rbDayXEveryYMonths");
            this.rbDayXEveryYMonths.Name = "rbDayXEveryYMonths";
            this.rbDayXEveryYMonths.CheckedChanged += new System.EventHandler(this.Monthly_CheckedChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cboOccurrence
            // 
            this.cboOccurrence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboOccurrence, "cboOccurrence");
            this.cboOccurrence.Name = "cboOccurrence";
            // 
            // cboDOW
            // 
            this.cboDOW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboDOW, "cboDOW");
            this.cboDOW.Name = "cboDOW";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
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
            // udcMonths
            // 
            resources.ApplyResources(this.udcMonths, "udcMonths");
            this.udcMonths.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.udcMonths.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udcMonths.Name = "udcMonths";
            this.udcMonths.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // udcDOWMonths
            // 
            resources.ApplyResources(this.udcDOWMonths, "udcDOWMonths");
            this.udcDOWMonths.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.udcDOWMonths.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udcDOWMonths.Name = "udcDOWMonths";
            this.udcDOWMonths.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // MonthlyPattern
            // 
            this.Controls.Add(this.udcDOWMonths);
            this.Controls.Add(this.udcMonths);
            this.Controls.Add(this.udcDay);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboDOW);
            this.Controls.Add(this.cboOccurrence);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbDayXEveryYMonths);
            this.Controls.Add(this.rbDayOfWeek);
            this.Name = "MonthlyPattern";
            resources.ApplyResources(this, "$this");
            ((System.ComponentModel.ISupportInitialize)(this.udcDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcMonths)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcDOWMonths)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbDayOfWeek;
        private System.Windows.Forms.RadioButton rbDayXEveryYMonths;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboOccurrence;
        private System.Windows.Forms.ComboBox cboDOW;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown udcDay;
        private System.Windows.Forms.NumericUpDown udcMonths;
        private System.Windows.Forms.NumericUpDown udcDOWMonths;
    }
}
