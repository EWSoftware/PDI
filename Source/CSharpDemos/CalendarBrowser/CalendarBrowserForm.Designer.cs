namespace CalendarBrowser
{
    partial class CalendarBrowserForm
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

                if(vCal != null)
                    vCal.Dispose();

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalendarBrowserForm));
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
            this.btnChgVersion = new System.Windows.Forms.Button();
            this.cboComponents = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChgTimeZone = new System.Windows.Forms.Button();
            this.dgvCalendar = new System.Windows.Forms.DataGridView();
            this.tbcStartDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbcSummary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbcOrganizer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbcComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblFilename = new System.Windows.Forms.Label();
            this.mnuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalendar)).BeginInit();
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
            this.miOpen.Size = new System.Drawing.Size(229, 22);
            this.miOpen.Text = "&Open";
            this.miOpen.Click += new System.EventHandler(this.miOpen_Click);
            // 
            // miSave
            // 
            this.miSave.Name = "miSave";
            this.miSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.miSave.Size = new System.Drawing.Size(229, 22);
            this.miSave.Text = "&Save";
            this.miSave.Click += new System.EventHandler(this.miSave_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Name = "menuItem2";
            this.menuItem2.Size = new System.Drawing.Size(226, 6);
            // 
            // miFileEncoding
            // 
            this.miFileEncoding.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFileUnicode,
            this.miFileWestEuro,
            this.miFileASCII});
            this.miFileEncoding.Name = "miFileEncoding";
            this.miFileEncoding.Size = new System.Drawing.Size(229, 22);
            this.miFileEncoding.Text = "File Encoding";
            // 
            // miFileUnicode
            // 
            this.miFileUnicode.Checked = true;
            this.miFileUnicode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miFileUnicode.Name = "miFileUnicode";
            this.miFileUnicode.Size = new System.Drawing.Size(265, 26);
            this.miFileUnicode.Text = "Unicode (UTF-8)";
            this.miFileUnicode.Click += new System.EventHandler(this.ChangeFileEncoding_Click);
            // 
            // miFileWestEuro
            // 
            this.miFileWestEuro.Name = "miFileWestEuro";
            this.miFileWestEuro.Size = new System.Drawing.Size(265, 26);
            this.miFileWestEuro.Text = "Western European (Windows)";
            this.miFileWestEuro.Click += new System.EventHandler(this.ChangeFileEncoding_Click);
            // 
            // miFileASCII
            // 
            this.miFileASCII.Name = "miFileASCII";
            this.miFileASCII.Size = new System.Drawing.Size(265, 26);
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
            this.miPropEncoding.Size = new System.Drawing.Size(229, 22);
            this.miPropEncoding.Text = "Property Encoding";
            // 
            // miPropUnicode
            // 
            this.miPropUnicode.Name = "miPropUnicode";
            this.miPropUnicode.Size = new System.Drawing.Size(265, 26);
            this.miPropUnicode.Text = "Unicode (UTF-8)";
            this.miPropUnicode.Click += new System.EventHandler(this.ChangePropEncoding_Click);
            // 
            // miPropWestEuro
            // 
            this.miPropWestEuro.Name = "miPropWestEuro";
            this.miPropWestEuro.Size = new System.Drawing.Size(265, 26);
            this.miPropWestEuro.Text = "Western European (Windows)";
            this.miPropWestEuro.Click += new System.EventHandler(this.ChangePropEncoding_Click);
            // 
            // miPropASCII
            // 
            this.miPropASCII.Checked = true;
            this.miPropASCII.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miPropASCII.Name = "miPropASCII";
            this.miPropASCII.Size = new System.Drawing.Size(265, 26);
            this.miPropASCII.Text = "ASCII";
            this.miPropASCII.Click += new System.EventHandler(this.ChangePropEncoding_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Name = "menuItem4";
            this.menuItem4.Size = new System.Drawing.Size(226, 6);
            // 
            // miClear
            // 
            this.miClear.Name = "miClear";
            this.miClear.Size = new System.Drawing.Size(229, 22);
            this.miClear.Text = "&Clear";
            this.miClear.Click += new System.EventHandler(this.miClear_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Name = "menuItem6";
            this.menuItem6.Size = new System.Drawing.Size(226, 6);
            // 
            // miAbout
            // 
            this.miAbout.Name = "miAbout";
            this.miAbout.Size = new System.Drawing.Size(229, 22);
            this.miAbout.Text = "&About Calendar Browser";
            this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(229, 22);
            this.miExit.Text = "E&xit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // btnChgVersion
            // 
            this.btnChgVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChgVersion.Location = new System.Drawing.Point(480, 516);
            this.btnChgVersion.Name = "btnChgVersion";
            this.btnChgVersion.Size = new System.Drawing.Size(88, 32);
            this.btnChgVersion.TabIndex = 6;
            this.btnChgVersion.Text = "&Version";
            this.btnChgVersion.Click += new System.EventHandler(this.btnChgVersion_Click);
            // 
            // cboComponents
            // 
            this.cboComponents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cboComponents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboComponents.Location = new System.Drawing.Point(786, 521);
            this.cboComponents.Name = "cboComponents";
            this.cboComponents.Size = new System.Drawing.Size(144, 24);
            this.cboComponents.TabIndex = 9;
            this.cboComponents.SelectedIndexChanged += new System.EventHandler(this.cboComponents_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(734, 521);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "V&iew";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnChgTimeZone
            // 
            this.btnChgTimeZone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChgTimeZone.Location = new System.Drawing.Point(574, 516);
            this.btnChgTimeZone.Name = "btnChgTimeZone";
            this.btnChgTimeZone.Size = new System.Drawing.Size(120, 32);
            this.btnChgTimeZone.TabIndex = 7;
            this.btnChgTimeZone.Text = "&Time Zones";
            this.btnChgTimeZone.Click += new System.EventHandler(this.btnChgTimeZone_Click);
            // 
            // dgvCalendar
            // 
            this.dgvCalendar.AllowUserToAddRows = false;
            this.dgvCalendar.AllowUserToDeleteRows = false;
            this.dgvCalendar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCalendar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCalendar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCalendar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCalendar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tbcStartDateTime,
            this.tbcSummary,
            this.tbcOrganizer,
            this.tbcComment});
            this.dgvCalendar.Location = new System.Drawing.Point(12, 51);
            this.dgvCalendar.MultiSelect = false;
            this.dgvCalendar.Name = "dgvCalendar";
            this.dgvCalendar.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCalendar.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCalendar.RowHeadersWidth = 25;
            this.dgvCalendar.RowTemplate.Height = 24;
            this.dgvCalendar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCalendar.Size = new System.Drawing.Size(918, 459);
            this.dgvCalendar.StandardTab = true;
            this.dgvCalendar.TabIndex = 2;
            this.dgvCalendar.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCalendar_CellDoubleClick);
            this.dgvCalendar.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvCalendar_CellPainting);
            // 
            // tbcStartDateTime
            // 
            this.tbcStartDateTime.DataPropertyName = "StartDateTime";
            this.tbcStartDateTime.HeaderText = "Start Date/Time";
            this.tbcStartDateTime.Name = "tbcStartDateTime";
            this.tbcStartDateTime.ReadOnly = true;
            this.tbcStartDateTime.Width = 200;
            // 
            // tbcSummary
            // 
            this.tbcSummary.DataPropertyName = "Summary_Value";
            this.tbcSummary.HeaderText = "Summary";
            this.tbcSummary.Name = "tbcSummary";
            this.tbcSummary.ReadOnly = true;
            this.tbcSummary.Width = 500;
            // 
            // tbcOrganizer
            // 
            this.tbcOrganizer.DataPropertyName = "Organizer_Value";
            this.tbcOrganizer.HeaderText = "Organizer";
            this.tbcOrganizer.Name = "tbcOrganizer";
            this.tbcOrganizer.ReadOnly = true;
            this.tbcOrganizer.Width = 250;
            // 
            // tbcComment
            // 
            this.tbcComment.DataPropertyName = "Comment_Value";
            this.tbcComment.HeaderText = "Comment";
            this.tbcComment.Name = "tbcComment";
            this.tbcComment.ReadOnly = true;
            this.tbcComment.Width = 500;
            // 
            // lblFilename
            // 
            this.lblFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFilename.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilename.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFilename.Location = new System.Drawing.Point(12, 25);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(918, 23);
            this.lblFilename.TabIndex = 1;
            this.lblFilename.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CalendarBrowserForm
            // 
            this.ClientSize = new System.Drawing.Size(942, 560);
            this.Controls.Add(this.lblFilename);
            this.Controls.Add(this.dgvCalendar);
            this.Controls.Add(this.btnChgTimeZone);
            this.Controls.Add(this.cboComponents);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnChgVersion);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.mnuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMain;
            this.MinimumSize = new System.Drawing.Size(800, 250);
            this.Name = "CalendarBrowserForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "vCalendar/iCalendar Browser";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.CalendarBrowserForm_Closing);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalendar)).EndInit();
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
        private System.Windows.Forms.ComboBox cboComponents;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChgVersion;
        private System.Windows.Forms.Button btnChgTimeZone;
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
        private System.Windows.Forms.DataGridView dgvCalendar;
        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbcStartDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbcSummary;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbcOrganizer;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbcComment;
    }
}
