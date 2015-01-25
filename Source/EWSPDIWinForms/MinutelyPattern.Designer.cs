namespace EWSoftware.PDI.Windows.Forms
{
    partial class MinutelyPattern
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MinutelyPattern));
            this.label1 = new System.Windows.Forms.Label();
            this.udcMinutes = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.udcMinutes)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // udcMinutes
            // 
            resources.ApplyResources(this.udcMinutes, "udcMinutes");
            this.udcMinutes.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.udcMinutes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udcMinutes.Name = "udcMinutes";
            this.udcMinutes.Value = new decimal(new int[] {
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
            // MinutelyPattern
            // 
            this.Controls.Add(this.udcMinutes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "MinutelyPattern";
            resources.ApplyResources(this, "$this");
            ((System.ComponentModel.ISupportInitialize)(this.udcMinutes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown udcMinutes;
        private System.Windows.Forms.Label label2;
    }
}
