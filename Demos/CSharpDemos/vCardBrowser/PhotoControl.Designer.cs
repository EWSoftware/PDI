namespace vCardBrowser
{
    partial class PhotoControl
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
            if(disposing)
            {
                if(bmImage != null)
                    bmImage.Dispose();

                if(components != null)
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
            this.pnlPhoto = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.chkInline = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboImageType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // pnlPhoto
            // 
            this.pnlPhoto.AutoScroll = true;
            this.pnlPhoto.BackColor = System.Drawing.SystemColors.Window;
            this.pnlPhoto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlPhoto.Location = new System.Drawing.Point(8, 4);
            this.pnlPhoto.Name = "pnlPhoto";
            this.pnlPhoto.Size = new System.Drawing.Size(200, 200);
            this.pnlPhoto.TabIndex = 6;
            this.pnlPhoto.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPhoto_Paint);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(214, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFilename
            // 
            this.txtFilename.Location = new System.Drawing.Point(272, 8);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(280, 22);
            this.txtFilename.TabIndex = 1;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(552, 7);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(28, 24);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "...";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // chkInline
            // 
            this.chkInline.Location = new System.Drawing.Point(272, 36);
            this.chkInline.Name = "chkInline";
            this.chkInline.Size = new System.Drawing.Size(192, 24);
            this.chkInline.TabIndex = 3;
            this.chkInline.Text = "Store image in vCard";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(217, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Type";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboImageType
            // 
            this.cboImageType.Items.AddRange(new object[] {
            "GIF",
            "CGM",
            "WMF",
            "BMP",
            "MET",
            "PMB",
            "DIB",
            "PICT",
            "TIFF",
            "PS",
            "PDF",
            "JPEG",
            "MPEG",
            "MPEG2",
            "AVI",
            "QTIME",
            "PNG"});
            this.cboImageType.Location = new System.Drawing.Point(272, 66);
            this.cboImageType.MaxDropDownItems = 16;
            this.cboImageType.Name = "cboImageType";
            this.cboImageType.Size = new System.Drawing.Size(96, 24);
            this.cboImageType.TabIndex = 5;
            // 
            // PhotoControl
            // 
            this.Controls.Add(this.cboImageType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkInline);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlPhoto);
            this.Name = "PhotoControl";
            this.Size = new System.Drawing.Size(588, 208);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlPhoto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.CheckBox chkInline;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboImageType;
        private System.Windows.Forms.Button btnLoad;

    }
}
