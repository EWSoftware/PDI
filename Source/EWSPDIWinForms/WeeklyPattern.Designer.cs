namespace EWSoftware.PDI.Windows.Forms
{
    partial class WeeklyPattern
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WeeklyPattern));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkSunday = new System.Windows.Forms.CheckBox();
            this.chkMonday = new System.Windows.Forms.CheckBox();
            this.chkTuesday = new System.Windows.Forms.CheckBox();
            this.chkWednesday = new System.Windows.Forms.CheckBox();
            this.chkThursday = new System.Windows.Forms.CheckBox();
            this.chkFriday = new System.Windows.Forms.CheckBox();
            this.chkSaturday = new System.Windows.Forms.CheckBox();
            this.udcWeeks = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.udcWeeks)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // chkSunday
            // 
            resources.ApplyResources(this.chkSunday, "chkSunday");
            this.chkSunday.Name = "chkSunday";
            // 
            // chkMonday
            // 
            resources.ApplyResources(this.chkMonday, "chkMonday");
            this.chkMonday.Name = "chkMonday";
            // 
            // chkTuesday
            // 
            resources.ApplyResources(this.chkTuesday, "chkTuesday");
            this.chkTuesday.Name = "chkTuesday";
            // 
            // chkWednesday
            // 
            resources.ApplyResources(this.chkWednesday, "chkWednesday");
            this.chkWednesday.Name = "chkWednesday";
            // 
            // chkThursday
            // 
            resources.ApplyResources(this.chkThursday, "chkThursday");
            this.chkThursday.Name = "chkThursday";
            // 
            // chkFriday
            // 
            resources.ApplyResources(this.chkFriday, "chkFriday");
            this.chkFriday.Name = "chkFriday";
            // 
            // chkSaturday
            // 
            resources.ApplyResources(this.chkSaturday, "chkSaturday");
            this.chkSaturday.Name = "chkSaturday";
            // 
            // udcWeeks
            // 
            resources.ApplyResources(this.udcWeeks, "udcWeeks");
            this.udcWeeks.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.udcWeeks.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udcWeeks.Name = "udcWeeks";
            this.udcWeeks.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // WeeklyPattern
            // 
            this.Controls.Add(this.udcWeeks);
            this.Controls.Add(this.chkSaturday);
            this.Controls.Add(this.chkFriday);
            this.Controls.Add(this.chkThursday);
            this.Controls.Add(this.chkWednesday);
            this.Controls.Add(this.chkTuesday);
            this.Controls.Add(this.chkMonday);
            this.Controls.Add(this.chkSunday);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "WeeklyPattern";
            resources.ApplyResources(this, "$this");
            ((System.ComponentModel.ISupportInitialize)(this.udcWeeks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkSunday;
        private System.Windows.Forms.CheckBox chkMonday;
        private System.Windows.Forms.CheckBox chkTuesday;
        private System.Windows.Forms.CheckBox chkWednesday;
        private System.Windows.Forms.CheckBox chkThursday;
        private System.Windows.Forms.CheckBox chkFriday;
        private System.Windows.Forms.CheckBox chkSaturday;
        private System.Windows.Forms.NumericUpDown udcWeeks;
    }
}
