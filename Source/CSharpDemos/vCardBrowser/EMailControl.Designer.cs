namespace vCardBrowser
{
    partial class EMailControl
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
            this.pnlControls = new System.Windows.Forms.Panel();
            this.chkPreferred = new System.Windows.Forms.CheckBox();
            this.chkX400 = new System.Windows.Forms.CheckBox();
            this.chkInternet = new System.Windows.Forms.CheckBox();
            this.chkAOL = new System.Windows.Forms.CheckBox();
            this.txtEMailAddress = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.pnlControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.chkPreferred);
            this.pnlControls.Controls.Add(this.chkX400);
            this.pnlControls.Controls.Add(this.chkInternet);
            this.pnlControls.Controls.Add(this.chkAOL);
            this.pnlControls.Controls.Add(this.txtEMailAddress);
            this.pnlControls.Controls.Add(this.label12);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlControls.Location = new System.Drawing.Point(0, 0);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(532, 76);
            this.pnlControls.TabIndex = 0;
            // 
            // chkPreferred
            // 
            this.chkPreferred.Location = new System.Drawing.Point(386, 44);
            this.chkPreferred.Name = "chkPreferred";
            this.chkPreferred.Size = new System.Drawing.Size(120, 24);
            this.chkPreferred.TabIndex = 5;
            this.chkPreferred.Text = "Preferred";
            // 
            // chkX400
            // 
            this.chkX400.Location = new System.Drawing.Point(281, 44);
            this.chkX400.Name = "chkX400";
            this.chkX400.Size = new System.Drawing.Size(99, 24);
            this.chkX400.TabIndex = 4;
            this.chkX400.Text = "X400";
            // 
            // chkInternet
            // 
            this.chkInternet.Location = new System.Drawing.Point(173, 44);
            this.chkInternet.Name = "chkInternet";
            this.chkInternet.Size = new System.Drawing.Size(102, 24);
            this.chkInternet.TabIndex = 3;
            this.chkInternet.Text = "Internet";
            // 
            // chkAOL
            // 
            this.chkAOL.Location = new System.Drawing.Point(76, 44);
            this.chkAOL.Name = "chkAOL";
            this.chkAOL.Size = new System.Drawing.Size(91, 24);
            this.chkAOL.TabIndex = 2;
            this.chkAOL.Text = "AOL";
            // 
            // txtEMailAddress
            // 
            this.txtEMailAddress.Location = new System.Drawing.Point(76, 10);
            this.txtEMailAddress.Name = "txtEMailAddress";
            this.txtEMailAddress.Size = new System.Drawing.Size(430, 26);
            this.txtEMailAddress.TabIndex = 1;
            this.txtEMailAddress.Validating += new System.ComponentModel.CancelEventHandler(this.txtEMailAddress_Validating);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(1, 12);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 23);
            this.label12.TabIndex = 0;
            this.label12.Text = "&E-Mail";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // EMailControl
            // 
            this.Controls.Add(this.pnlControls);
            this.Name = "EMailControl";
            this.Size = new System.Drawing.Size(532, 104);
            this.Controls.SetChildIndex(this.pnlControls, 0);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.CheckBox chkPreferred;
        private System.Windows.Forms.CheckBox chkX400;
        private System.Windows.Forms.CheckBox chkInternet;
        private System.Windows.Forms.CheckBox chkAOL;
        private System.Windows.Forms.TextBox txtEMailAddress;
        private System.Windows.Forms.Label label12;

    }
}
