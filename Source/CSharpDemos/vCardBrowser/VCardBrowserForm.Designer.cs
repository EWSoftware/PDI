namespace vCardBrowser
{
    partial class VCardBrowserForm
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
                if(sf != null)
                    sf.Dispose();

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VCardBrowserForm));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.miFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.miSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.miFileEncoding = new System.Windows.Forms.ToolStripMenuItem();
            this.miFileUnicode = new System.Windows.Forms.ToolStripMenuItem();
            this.miFileWestEuro = new System.Windows.Forms.ToolStripMenuItem();
            this.miFileASCII = new System.Windows.Forms.ToolStripMenuItem();
            this.miPropEncoding = new System.Windows.Forms.ToolStripMenuItem();
            this.miPropUnicode = new System.Windows.Forms.ToolStripMenuItem();
            this.miPropWestEuro = new System.Windows.Forms.ToolStripMenuItem();
            this.miPropASCII = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.miClear = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.cboVersion = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.btnApplyVersion = new System.Windows.Forms.Button();
            this.lblFilename = new System.Windows.Forms.Label();
            this.dgvCards = new System.Windows.Forms.DataGridView();
            this.tbcVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbcName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbcTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbcOrganization = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbcLastRevision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mnuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCards)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Location = new System.Drawing.Point(12, 516);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 32);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "&Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.Location = new System.Drawing.Point(106, 516);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(88, 32);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(200, 516);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(88, 32);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // mnuMain
            // 
            this.mnuMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.mnuMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(942, 25);
            this.mnuMain.TabIndex = 0;
            // 
            // miFile
            // 
            this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpen,
            this.miSave,
            this.menuItem2,
            this.miFileEncoding,
            this.miPropEncoding,
            this.menuItem4,
            this.miClear,
            this.menuItem6,
            this.miAbout,
            this.miExit});
            this.miFile.Name = "miFile";
            this.miFile.Size = new System.Drawing.Size(42, 21);
            this.miFile.Text = "&File";
            // 
            // miOpen
            // 
            this.miOpen.Name = "miOpen";
            this.miOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.miOpen.Size = new System.Drawing.Size(215, 26);
            this.miOpen.Text = "&Open";
            this.miOpen.Click += new System.EventHandler(this.miOpen_Click);
            // 
            // miSave
            // 
            this.miSave.Name = "miSave";
            this.miSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.miSave.Size = new System.Drawing.Size(215, 26);
            this.miSave.Text = "&Save";
            this.miSave.Click += new System.EventHandler(this.miSave_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Name = "menuItem2";
            this.menuItem2.Size = new System.Drawing.Size(212, 6);
            // 
            // miFileEncoding
            // 
            this.miFileEncoding.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFileUnicode,
            this.miFileWestEuro,
            this.miFileASCII});
            this.miFileEncoding.Name = "miFileEncoding";
            this.miFileEncoding.Size = new System.Drawing.Size(215, 26);
            this.miFileEncoding.Text = "File Encoding";
            // 
            // miFileUnicode
            // 
            this.miFileUnicode.Checked = true;
            this.miFileUnicode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miFileUnicode.Name = "miFileUnicode";
            this.miFileUnicode.Size = new System.Drawing.Size(271, 26);
            this.miFileUnicode.Text = "Unicode (UTF-8)";
            this.miFileUnicode.Click += new System.EventHandler(this.ChangeFileEncoding_Click);
            // 
            // miFileWestEuro
            // 
            this.miFileWestEuro.Name = "miFileWestEuro";
            this.miFileWestEuro.Size = new System.Drawing.Size(271, 26);
            this.miFileWestEuro.Text = "Western European (Windows)";
            this.miFileWestEuro.Click += new System.EventHandler(this.ChangeFileEncoding_Click);
            // 
            // miFileASCII
            // 
            this.miFileASCII.Name = "miFileASCII";
            this.miFileASCII.Size = new System.Drawing.Size(271, 26);
            this.miFileASCII.Text = "ASCII";
            this.miFileASCII.Click += new System.EventHandler(this.ChangeFileEncoding_Click);
            // 
            // miPropEncoding
            // 
            this.miPropEncoding.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miPropUnicode,
            this.miPropWestEuro,
            this.miPropASCII});
            this.miPropEncoding.Name = "miPropEncoding";
            this.miPropEncoding.Size = new System.Drawing.Size(215, 26);
            this.miPropEncoding.Text = "Property Encoding";
            // 
            // miPropUnicode
            // 
            this.miPropUnicode.Name = "miPropUnicode";
            this.miPropUnicode.Size = new System.Drawing.Size(271, 26);
            this.miPropUnicode.Text = "Unicode (UTF-8)";
            this.miPropUnicode.Click += new System.EventHandler(this.ChangePropEncoding_Click);
            // 
            // miPropWestEuro
            // 
            this.miPropWestEuro.Name = "miPropWestEuro";
            this.miPropWestEuro.Size = new System.Drawing.Size(271, 26);
            this.miPropWestEuro.Text = "Western European (Windows)";
            this.miPropWestEuro.Click += new System.EventHandler(this.ChangePropEncoding_Click);
            // 
            // miPropASCII
            // 
            this.miPropASCII.Checked = true;
            this.miPropASCII.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miPropASCII.Name = "miPropASCII";
            this.miPropASCII.Size = new System.Drawing.Size(271, 26);
            this.miPropASCII.Text = "ASCII";
            this.miPropASCII.Click += new System.EventHandler(this.ChangePropEncoding_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Name = "menuItem4";
            this.menuItem4.Size = new System.Drawing.Size(212, 6);
            // 
            // miClear
            // 
            this.miClear.Name = "miClear";
            this.miClear.Size = new System.Drawing.Size(215, 26);
            this.miClear.Text = "&Clear";
            this.miClear.Click += new System.EventHandler(this.miClear_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Name = "menuItem6";
            this.menuItem6.Size = new System.Drawing.Size(212, 6);
            // 
            // miAbout
            // 
            this.miAbout.Name = "miAbout";
            this.miAbout.Size = new System.Drawing.Size(215, 26);
            this.miAbout.Text = "&About vCard Browser";
            this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(215, 26);
            this.miExit.Text = "E&xit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // cboVersion
            // 
            this.cboVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cboVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVersion.Items.AddRange(new object[] {
            "2.1",
            "3.0",
            "4.0"});
            this.cboVersion.Location = new System.Drawing.Point(771, 519);
            this.cboVersion.Name = "cboVersion";
            this.cboVersion.Size = new System.Drawing.Size(65, 24);
            this.cboVersion.TabIndex = 7;
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.Location = new System.Drawing.Point(662, 521);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(103, 23);
            this.label23.TabIndex = 6;
            this.label23.Text = "&Version";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnApplyVersion
            // 
            this.btnApplyVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyVersion.Location = new System.Drawing.Point(842, 516);
            this.btnApplyVersion.Name = "btnApplyVersion";
            this.btnApplyVersion.Size = new System.Drawing.Size(88, 32);
            this.btnApplyVersion.TabIndex = 8;
            this.btnApplyVersion.Text = "A&pply";
            this.btnApplyVersion.Click += new System.EventHandler(this.btnApplyVersion_Click);
            // 
            // lblFilename
            // 
            this.lblFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFilename.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilename.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFilename.Location = new System.Drawing.Point(12, 28);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(918, 23);
            this.lblFilename.TabIndex = 1;
            this.lblFilename.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvCards
            // 
            this.dgvCards.AllowUserToAddRows = false;
            this.dgvCards.AllowUserToDeleteRows = false;
            this.dgvCards.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCards.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCards.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCards.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCards.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tbcVersion,
            this.tbcName,
            this.tbcTitle,
            this.tbcOrganization,
            this.tbcLastRevision});
            this.dgvCards.Location = new System.Drawing.Point(12, 54);
            this.dgvCards.MultiSelect = false;
            this.dgvCards.Name = "dgvCards";
            this.dgvCards.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCards.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCards.RowHeadersWidth = 25;
            this.dgvCards.RowTemplate.Height = 24;
            this.dgvCards.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCards.Size = new System.Drawing.Size(918, 456);
            this.dgvCards.StandardTab = true;
            this.dgvCards.TabIndex = 2;
            this.dgvCards.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCards_CellDoubleClick);
            this.dgvCards.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvCards_CellPainting);
            // 
            // tbcVersion
            // 
            this.tbcVersion.DataPropertyName = "Version";
            this.tbcVersion.HeaderText = "Ver";
            this.tbcVersion.Name = "tbcVersion";
            this.tbcVersion.ReadOnly = true;
            this.tbcVersion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tbcVersion.Width = 45;
            // 
            // tbcName
            // 
            this.tbcName.DataPropertyName = "Name_SortableName";
            this.tbcName.HeaderText = "Name";
            this.tbcName.Name = "tbcName";
            this.tbcName.ReadOnly = true;
            this.tbcName.Width = 240;
            // 
            // tbcTitle
            // 
            this.tbcTitle.DataPropertyName = "Title_Value";
            this.tbcTitle.HeaderText = "Title";
            this.tbcTitle.Name = "tbcTitle";
            this.tbcTitle.ReadOnly = true;
            this.tbcTitle.Width = 200;
            // 
            // tbcOrganization
            // 
            this.tbcOrganization.DataPropertyName = "Organization_Name";
            this.tbcOrganization.HeaderText = "Organization";
            this.tbcOrganization.Name = "tbcOrganization";
            this.tbcOrganization.ReadOnly = true;
            this.tbcOrganization.Width = 185;
            // 
            // tbcLastRevision
            // 
            this.tbcLastRevision.DataPropertyName = "LastRevision_DateTimeValue";
            this.tbcLastRevision.HeaderText = "Last Revision";
            this.tbcLastRevision.Name = "tbcLastRevision";
            this.tbcLastRevision.ReadOnly = true;
            this.tbcLastRevision.Width = 175;
            // 
            // VCardBrowserForm
            // 
            this.ClientSize = new System.Drawing.Size(942, 560);
            this.Controls.Add(this.dgvCards);
            this.Controls.Add(this.lblFilename);
            this.Controls.Add(this.btnApplyVersion);
            this.Controls.Add(this.cboVersion);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.mnuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMain;
            this.MinimumSize = new System.Drawing.Size(640, 250);
            this.Name = "VCardBrowserForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "vCard Browser";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.VCardBrowserForm_Closing);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCards)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem miFile;
        private System.Windows.Forms.ToolStripMenuItem miOpen;
        private System.Windows.Forms.ToolStripMenuItem miSave;
        private System.Windows.Forms.ToolStripMenuItem miClear;
        private System.Windows.Forms.ToolStripMenuItem miAbout;
        private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.ComboBox cboVersion;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btnApplyVersion;
        private System.Windows.Forms.ToolStripMenuItem miFileEncoding;
        private System.Windows.Forms.ToolStripMenuItem miFileUnicode;
        private System.Windows.Forms.ToolStripMenuItem miFileWestEuro;
        private System.Windows.Forms.ToolStripMenuItem miFileASCII;
        private System.Windows.Forms.ToolStripMenuItem miPropEncoding;
        private System.Windows.Forms.ToolStripMenuItem miPropUnicode;
        private System.Windows.Forms.ToolStripMenuItem miPropWestEuro;
        private System.Windows.Forms.ToolStripMenuItem miPropASCII;
        private System.Windows.Forms.ToolStripSeparator menuItem2;
        private System.Windows.Forms.ToolStripSeparator menuItem4;
        private System.Windows.Forms.ToolStripSeparator menuItem6;
        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.DataGridView dgvCards;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbcVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbcName;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbcTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbcOrganization;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbcLastRevision;
    }
}
