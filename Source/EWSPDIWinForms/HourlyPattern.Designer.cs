namespace EWSoftware.PDI.Windows.Forms
{
    partial class HourlyPattern
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HourlyPattern));
            this.label1 = new System.Windows.Forms.Label();
            this.udcHours = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.udcHours)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // udcHours
            // 
            resources.ApplyResources(this.udcHours, "udcHours");
            this.udcHours.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.udcHours.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udcHours.Name = "udcHours";
            this.udcHours.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // HourlyPattern
            // 
            this.Controls.Add(this.udcHours);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "HourlyPattern";
            resources.ApplyResources(this, "$this");
            ((System.ComponentModel.ISupportInitialize)(this.udcHours)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown udcHours;
        private System.Windows.Forms.Label label2;
    }
}
