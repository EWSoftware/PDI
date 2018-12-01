namespace vCardBrowser
{
    partial class AboutDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDlg));
            this.lnkHelp = new System.Windows.Forms.LinkLabel();
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSysInfo = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lvComponents = new System.Windows.Forms.ListView();
            this.ColumnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Label1 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lnkHelp
            // 
            this.lnkHelp.Location = new System.Drawing.Point(187, 334);
            this.lnkHelp.Name = "lnkHelp";
            this.lnkHelp.Size = new System.Drawing.Size(216, 23);
            this.lnkHelp.TabIndex = 6;
            this.lnkHelp.TabStop = true;
            this.lnkHelp.Text = "Eric@EWoodruff.us";
            this.lnkHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHelp_LinkClicked);
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "Name";
            this.ColumnHeader1.Width = 275;
            // 
            // btnSysInfo
            // 
            this.btnSysInfo.Location = new System.Drawing.Point(355, 398);
            this.btnSysInfo.Name = "btnSysInfo";
            this.btnSysInfo.Size = new System.Drawing.Size(150, 32);
            this.btnSysInfo.TabIndex = 9;
            this.btnSysInfo.Text = "System Info...";
            this.btnSysInfo.Click += new System.EventHandler(this.btnSysInfo_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(199, 398);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(150, 32);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            // 
            // lvComponents
            // 
            this.lvComponents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1,
            this.ColumnHeader2});
            this.lvComponents.FullRowSelect = true;
            this.lvComponents.GridLines = true;
            this.lvComponents.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvComponents.Location = new System.Drawing.Point(12, 153);
            this.lvComponents.MultiSelect = false;
            this.lvComponents.Name = "lvComponents";
            this.lvComponents.Size = new System.Drawing.Size(493, 170);
            this.lvComponents.TabIndex = 4;
            this.lvComponents.UseCompatibleStateImageBehavior = false;
            this.lvComponents.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader2
            // 
            this.ColumnHeader2.Text = "Version";
            this.ColumnHeader2.Width = 150;
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(8, 127);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(164, 23);
            this.Label1.TabIndex = 3;
            this.Label1.Text = "Product Components";
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(80, 8);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(425, 23);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "<Application Name>";
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(80, 64);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(425, 67);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "<Description>";
            // 
            // lblCopyright
            // 
            this.lblCopyright.Location = new System.Drawing.Point(12, 364);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(493, 23);
            this.lblCopyright.TabIndex = 7;
            this.lblCopyright.Text = "<Copyright>";
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(16, 16);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(50, 50);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox1.TabIndex = 12;
            this.PictureBox1.TabStop = false;
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(80, 36);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(425, 23);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "<Version>";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 334);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "For help send e-mail to";
            // 
            // AboutDlg
            // 
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(517, 442);
            this.Controls.Add(this.lnkHelp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSysInfo);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lvComponents);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.lblVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About vCard Browser";
            this.Load += new System.EventHandler(this.AboutDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader ColumnHeader1;
        private System.Windows.Forms.ColumnHeader ColumnHeader2;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.PictureBox PictureBox1;
        private System.Windows.Forms.Button btnSysInfo;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ListView lvComponents;
        private System.Windows.Forms.LinkLabel lnkHelp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblVersion;
    }
}
