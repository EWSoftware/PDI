namespace EWSoftware.PDI.Windows.Forms
{
    partial class SecondlyPattern
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SecondlyPattern));
            this.label1 = new System.Windows.Forms.Label();
            this.udcSeconds = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.udcSeconds)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // udcSeconds
            // 
            resources.ApplyResources(this.udcSeconds, "udcSeconds");
            this.udcSeconds.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.udcSeconds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udcSeconds.Name = "udcSeconds";
            this.udcSeconds.Value = new decimal(new int[] {
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
            // SecondlyPattern
            // 
            this.Controls.Add(this.udcSeconds);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "SecondlyPattern";
            resources.ApplyResources(this, "$this");
            ((System.ComponentModel.ISupportInitialize)(this.udcSeconds)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown udcSeconds;
        private System.Windows.Forms.Label label2;
    }
}
