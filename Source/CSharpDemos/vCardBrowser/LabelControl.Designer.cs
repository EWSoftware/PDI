namespace vCardBrowser
{
    partial class LabelControl
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
            this.chkWork = new System.Windows.Forms.CheckBox();
            this.chkHome = new System.Windows.Forms.CheckBox();
            this.chkParcel = new System.Windows.Forms.CheckBox();
            this.chkPostal = new System.Windows.Forms.CheckBox();
            this.chkInternational = new System.Windows.Forms.CheckBox();
            this.chkDomestic = new System.Windows.Forms.CheckBox();
            this.txtLabelText = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.pnlControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.chkPreferred);
            this.pnlControls.Controls.Add(this.chkWork);
            this.pnlControls.Controls.Add(this.chkHome);
            this.pnlControls.Controls.Add(this.chkParcel);
            this.pnlControls.Controls.Add(this.chkPostal);
            this.pnlControls.Controls.Add(this.chkInternational);
            this.pnlControls.Controls.Add(this.chkDomestic);
            this.pnlControls.Controls.Add(this.txtLabelText);
            this.pnlControls.Controls.Add(this.label12);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlControls.Location = new System.Drawing.Point(0, 0);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(464, 173);
            this.pnlControls.TabIndex = 0;
            // 
            // chkPreferred
            // 
            this.chkPreferred.Location = new System.Drawing.Point(286, 138);
            this.chkPreferred.Name = "chkPreferred";
            this.chkPreferred.Size = new System.Drawing.Size(96, 24);
            this.chkPreferred.TabIndex = 8;
            this.chkPreferred.Text = "Preferred";
            // 
            // chkWork
            // 
            this.chkWork.Location = new System.Drawing.Point(166, 138);
            this.chkWork.Name = "chkWork";
            this.chkWork.Size = new System.Drawing.Size(72, 24);
            this.chkWork.TabIndex = 7;
            this.chkWork.Text = "Work";
            // 
            // chkHome
            // 
            this.chkHome.Location = new System.Drawing.Point(70, 138);
            this.chkHome.Name = "chkHome";
            this.chkHome.Size = new System.Drawing.Size(78, 24);
            this.chkHome.TabIndex = 6;
            this.chkHome.Text = "Home";
            // 
            // chkParcel
            // 
            this.chkParcel.Location = new System.Drawing.Point(372, 108);
            this.chkParcel.Name = "chkParcel";
            this.chkParcel.Size = new System.Drawing.Size(78, 24);
            this.chkParcel.TabIndex = 5;
            this.chkParcel.Text = "Parcel";
            // 
            // chkPostal
            // 
            this.chkPostal.Location = new System.Drawing.Point(286, 108);
            this.chkPostal.Name = "chkPostal";
            this.chkPostal.Size = new System.Drawing.Size(80, 24);
            this.chkPostal.TabIndex = 4;
            this.chkPostal.Text = "Postal";
            // 
            // chkInternational
            // 
            this.chkInternational.Location = new System.Drawing.Point(166, 108);
            this.chkInternational.Name = "chkInternational";
            this.chkInternational.Size = new System.Drawing.Size(114, 24);
            this.chkInternational.TabIndex = 3;
            this.chkInternational.Text = "International";
            // 
            // chkDomestic
            // 
            this.chkDomestic.Location = new System.Drawing.Point(70, 108);
            this.chkDomestic.Name = "chkDomestic";
            this.chkDomestic.Size = new System.Drawing.Size(90, 24);
            this.chkDomestic.TabIndex = 2;
            this.chkDomestic.Text = "Domestic";
            // 
            // txtLabelText
            // 
            this.txtLabelText.AcceptsReturn = true;
            this.txtLabelText.Location = new System.Drawing.Point(70, 12);
            this.txtLabelText.Multiline = true;
            this.txtLabelText.Name = "txtLabelText";
            this.txtLabelText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLabelText.Size = new System.Drawing.Size(368, 88);
            this.txtLabelText.TabIndex = 1;
            this.txtLabelText.Validating += new System.ComponentModel.CancelEventHandler(this.txtLabelText_Validating);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(14, 12);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 23);
            this.label12.TabIndex = 0;
            this.label12.Text = "&Label";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelControl
            // 
            this.Controls.Add(this.pnlControls);
            this.Name = "LabelControl";
            this.Size = new System.Drawing.Size(464, 200);
            this.Controls.SetChildIndex(this.pnlControls, 0);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.CheckBox chkPreferred;
        private System.Windows.Forms.CheckBox chkWork;
        private System.Windows.Forms.CheckBox chkHome;
        private System.Windows.Forms.CheckBox chkParcel;
        private System.Windows.Forms.CheckBox chkPostal;
        private System.Windows.Forms.CheckBox chkInternational;
        private System.Windows.Forms.CheckBox chkDomestic;
        private System.Windows.Forms.TextBox txtLabelText;
        private System.Windows.Forms.Label label12;


    }
}
