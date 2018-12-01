namespace vCardBrowser
{
    partial class PhoneControl
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
            this.chkPager = new System.Windows.Forms.CheckBox();
            this.chkPreferred = new System.Windows.Forms.CheckBox();
            this.chkWork = new System.Windows.Forms.CheckBox();
            this.chkHome = new System.Windows.Forms.CheckBox();
            this.chkCell = new System.Windows.Forms.CheckBox();
            this.chkMessage = new System.Windows.Forms.CheckBox();
            this.chkFax = new System.Windows.Forms.CheckBox();
            this.chkVoice = new System.Windows.Forms.CheckBox();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.pnlControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.chkHome);
            this.pnlControls.Controls.Add(this.chkPager);
            this.pnlControls.Controls.Add(this.chkPreferred);
            this.pnlControls.Controls.Add(this.chkWork);
            this.pnlControls.Controls.Add(this.chkCell);
            this.pnlControls.Controls.Add(this.chkMessage);
            this.pnlControls.Controls.Add(this.chkFax);
            this.pnlControls.Controls.Add(this.chkVoice);
            this.pnlControls.Controls.Add(this.txtPhoneNumber);
            this.pnlControls.Controls.Add(this.label12);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlControls.Location = new System.Drawing.Point(0, 0);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(673, 100);
            this.pnlControls.TabIndex = 0;
            // 
            // chkPager
            // 
            this.chkPager.Location = new System.Drawing.Point(381, 70);
            this.chkPager.Name = "chkPager";
            this.chkPager.Size = new System.Drawing.Size(137, 24);
            this.chkPager.TabIndex = 8;
            this.chkPager.Text = "Pager";
            // 
            // chkPreferred
            // 
            this.chkPreferred.Location = new System.Drawing.Point(524, 70);
            this.chkPreferred.Name = "chkPreferred";
            this.chkPreferred.Size = new System.Drawing.Size(137, 24);
            this.chkPreferred.TabIndex = 9;
            this.chkPreferred.Text = "Preferred";
            // 
            // chkWork
            // 
            this.chkWork.Location = new System.Drawing.Point(95, 40);
            this.chkWork.Name = "chkWork";
            this.chkWork.Size = new System.Drawing.Size(137, 24);
            this.chkWork.TabIndex = 2;
            this.chkWork.Text = "Work";
            // 
            // chkHome
            // 
            this.chkHome.Location = new System.Drawing.Point(238, 40);
            this.chkHome.Name = "chkHome";
            this.chkHome.Size = new System.Drawing.Size(137, 24);
            this.chkHome.TabIndex = 3;
            this.chkHome.Text = "Home";
            // 
            // chkCell
            // 
            this.chkCell.Location = new System.Drawing.Point(238, 70);
            this.chkCell.Name = "chkCell";
            this.chkCell.Size = new System.Drawing.Size(137, 24);
            this.chkCell.TabIndex = 7;
            this.chkCell.Text = "Cell";
            // 
            // chkMessage
            // 
            this.chkMessage.Location = new System.Drawing.Point(95, 70);
            this.chkMessage.Name = "chkMessage";
            this.chkMessage.Size = new System.Drawing.Size(137, 24);
            this.chkMessage.TabIndex = 6;
            this.chkMessage.Text = "Message";
            // 
            // chkFax
            // 
            this.chkFax.Location = new System.Drawing.Point(524, 40);
            this.chkFax.Name = "chkFax";
            this.chkFax.Size = new System.Drawing.Size(137, 24);
            this.chkFax.TabIndex = 5;
            this.chkFax.Text = "Fax";
            // 
            // chkVoice
            // 
            this.chkVoice.Location = new System.Drawing.Point(381, 40);
            this.chkVoice.Name = "chkVoice";
            this.chkVoice.Size = new System.Drawing.Size(137, 24);
            this.chkVoice.TabIndex = 4;
            this.chkVoice.Text = "Voice";
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(95, 8);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(566, 26);
            this.txtPhoneNumber.TabIndex = 1;
            this.txtPhoneNumber.Validating += new System.ComponentModel.CancelEventHandler(this.txtPhoneNumber_Validating);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(5, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 23);
            this.label12.TabIndex = 0;
            this.label12.Text = "&Phone #";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PhoneControl
            // 
            this.Controls.Add(this.pnlControls);
            this.Name = "PhoneControl";
            this.Size = new System.Drawing.Size(673, 128);
            this.Controls.SetChildIndex(this.pnlControls, 0);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.CheckBox chkPager;
        private System.Windows.Forms.CheckBox chkPreferred;
        private System.Windows.Forms.CheckBox chkWork;
        private System.Windows.Forms.CheckBox chkHome;
        private System.Windows.Forms.CheckBox chkCell;
        private System.Windows.Forms.CheckBox chkMessage;
        private System.Windows.Forms.CheckBox chkFax;
        private System.Windows.Forms.CheckBox chkVoice;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.Label label12;
    }
}
