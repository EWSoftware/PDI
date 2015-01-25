namespace EWSoftware.PDI.Windows.Forms
{
    partial class DailyPattern
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DailyPattern));
            this.rbEveryXDays = new System.Windows.Forms.RadioButton();
            this.rbEveryWeekday = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.udcDays = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.udcDays)).BeginInit();
            this.SuspendLayout();
            // 
            // rbEveryXDays
            // 
            resources.ApplyResources(this.rbEveryXDays, "rbEveryXDays");
            this.rbEveryXDays.Name = "rbEveryXDays";
            this.rbEveryXDays.CheckedChanged += new System.EventHandler(this.Daily_CheckedChanged);
            // 
            // rbEveryWeekday
            // 
            resources.ApplyResources(this.rbEveryWeekday, "rbEveryWeekday");
            this.rbEveryWeekday.Name = "rbEveryWeekday";
            this.rbEveryWeekday.CheckedChanged += new System.EventHandler(this.Daily_CheckedChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // udcDays
            // 
            resources.ApplyResources(this.udcDays, "udcDays");
            this.udcDays.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.udcDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udcDays.Name = "udcDays";
            this.udcDays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // DailyPattern
            // 
            this.Controls.Add(this.udcDays);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbEveryWeekday);
            this.Controls.Add(this.rbEveryXDays);
            this.Name = "DailyPattern";
            resources.ApplyResources(this, "$this");
            ((System.ComponentModel.ISupportInitialize)(this.udcDays)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbEveryXDays;
        private System.Windows.Forms.RadioButton rbEveryWeekday;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown udcDays;
    }
}
