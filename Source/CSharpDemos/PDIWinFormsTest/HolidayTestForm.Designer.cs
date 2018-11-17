namespace PDIWinFormsTest
{
    partial class HolidayTestForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.hmHolidays = new EWSoftware.PDI.Windows.Forms.HolidayManager();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvDatesFound = new System.Windows.Forms.DataGridView();
            this.tbcDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbcDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpTestDate = new System.Windows.Forms.DateTimePicker();
            this.udcToYear = new System.Windows.Forms.NumericUpDown();
            this.udcFromYear = new System.Windows.Forms.NumericUpDown();
            this.grpEaster = new System.Windows.Forms.GroupBox();
            this.rbGregorian = new System.Windows.Forms.RadioButton();
            this.rbOrthodox = new System.Windows.Forms.RadioButton();
            this.rbJulian = new System.Windows.Forms.RadioButton();
            this.btnEaster = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTestDate = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatesFound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcToYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcFromYear)).BeginInit();
            this.grpEaster.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(724, 381);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 32);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.hmHolidays);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(286, 401);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "H&olidays";
            // 
            // hmHolidays
            // 
            this.hmHolidays.Location = new System.Drawing.Point(8, 24);
            this.hmHolidays.Name = "hmHolidays";
            this.hmHolidays.Size = new System.Drawing.Size(272, 368);
            this.hmHolidays.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvDatesFound);
            this.groupBox2.Controls.Add(this.dtpTestDate);
            this.groupBox2.Controls.Add(this.udcToYear);
            this.groupBox2.Controls.Add(this.udcFromYear);
            this.groupBox2.Controls.Add(this.grpEaster);
            this.groupBox2.Controls.Add(this.btnEaster);
            this.groupBox2.Controls.Add(this.btnFind);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnTestDate);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(304, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(508, 363);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Test Date Detection";
            // 
            // dgvDatesFound
            // 
            this.dgvDatesFound.AllowUserToAddRows = false;
            this.dgvDatesFound.AllowUserToDeleteRows = false;
            this.dgvDatesFound.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDatesFound.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDatesFound.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatesFound.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tbcDate,
            this.tbcDescription});
            this.dgvDatesFound.Location = new System.Drawing.Point(6, 21);
            this.dgvDatesFound.MultiSelect = false;
            this.dgvDatesFound.Name = "dgvDatesFound";
            this.dgvDatesFound.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDatesFound.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDatesFound.RowHeadersVisible = false;
            this.dgvDatesFound.RowTemplate.Height = 24;
            this.dgvDatesFound.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatesFound.Size = new System.Drawing.Size(337, 298);
            this.dgvDatesFound.TabIndex = 10;
            // 
            // tbcDate
            // 
            this.tbcDate.DataPropertyName = "Value";
            this.tbcDate.HeaderText = "Date";
            this.tbcDate.Name = "tbcDate";
            this.tbcDate.ReadOnly = true;
            this.tbcDate.Width = 90;
            // 
            // tbcDescription
            // 
            this.tbcDescription.DataPropertyName = "Display";
            this.tbcDescription.HeaderText = "Description";
            this.tbcDescription.Name = "tbcDescription";
            this.tbcDescription.ReadOnly = true;
            this.tbcDescription.Width = 210;
            // 
            // dtpTestDate
            // 
            this.dtpTestDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTestDate.Location = new System.Drawing.Point(96, 331);
            this.dtpTestDate.Name = "dtpTestDate";
            this.dtpTestDate.Size = new System.Drawing.Size(110, 22);
            this.dtpTestDate.TabIndex = 8;
            // 
            // udcToYear
            // 
            this.udcToYear.Location = new System.Drawing.Point(439, 64);
            this.udcToYear.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.udcToYear.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udcToYear.Name = "udcToYear";
            this.udcToYear.Size = new System.Drawing.Size(56, 22);
            this.udcToYear.TabIndex = 3;
            this.udcToYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udcToYear.Value = new decimal(new int[] {
            2004,
            0,
            0,
            0});
            // 
            // udcFromYear
            // 
            this.udcFromYear.Location = new System.Drawing.Point(349, 64);
            this.udcFromYear.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.udcFromYear.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udcFromYear.Name = "udcFromYear";
            this.udcFromYear.Size = new System.Drawing.Size(56, 22);
            this.udcFromYear.TabIndex = 1;
            this.udcFromYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udcFromYear.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // grpEaster
            // 
            this.grpEaster.Controls.Add(this.rbGregorian);
            this.grpEaster.Controls.Add(this.rbOrthodox);
            this.grpEaster.Controls.Add(this.rbJulian);
            this.grpEaster.Location = new System.Drawing.Point(349, 92);
            this.grpEaster.Name = "grpEaster";
            this.grpEaster.Size = new System.Drawing.Size(144, 120);
            this.grpEaster.TabIndex = 4;
            this.grpEaster.TabStop = false;
            this.grpEaster.Text = "Easter Method";
            // 
            // rbGregorian
            // 
            this.rbGregorian.Checked = true;
            this.rbGregorian.Location = new System.Drawing.Point(24, 88);
            this.rbGregorian.Name = "rbGregorian";
            this.rbGregorian.Size = new System.Drawing.Size(104, 24);
            this.rbGregorian.TabIndex = 2;
            this.rbGregorian.TabStop = true;
            this.rbGregorian.Text = "Gregorian";
            // 
            // rbOrthodox
            // 
            this.rbOrthodox.Location = new System.Drawing.Point(24, 56);
            this.rbOrthodox.Name = "rbOrthodox";
            this.rbOrthodox.Size = new System.Drawing.Size(104, 24);
            this.rbOrthodox.TabIndex = 1;
            this.rbOrthodox.Text = "Orthodox";
            // 
            // rbJulian
            // 
            this.rbJulian.Location = new System.Drawing.Point(24, 24);
            this.rbJulian.Name = "rbJulian";
            this.rbJulian.Size = new System.Drawing.Size(104, 24);
            this.rbJulian.TabIndex = 0;
            this.rbJulian.Text = "Julian";
            // 
            // btnEaster
            // 
            this.btnEaster.Location = new System.Drawing.Point(352, 280);
            this.btnEaster.Name = "btnEaster";
            this.btnEaster.Size = new System.Drawing.Size(136, 32);
            this.btnEaster.TabIndex = 6;
            this.btnEaster.Text = "Find Ea&ster";
            this.btnEaster.Click += new System.EventHandler(this.btnEaster_Click);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(352, 232);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(136, 32);
            this.btnFind.TabIndex = 5;
            this.btnFind.Text = "&Find Holidays";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(439, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "To Year";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(349, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fro&m Year";
            // 
            // btnTestDate
            // 
            this.btnTestDate.Location = new System.Drawing.Point(212, 325);
            this.btnTestDate.Name = "btnTestDate";
            this.btnTestDate.Size = new System.Drawing.Size(88, 32);
            this.btnTestDate.TabIndex = 9;
            this.btnTestDate.Text = "&Holiday?";
            this.btnTestDate.Click += new System.EventHandler(this.btnTestDate_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 330);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 23);
            this.label3.TabIndex = 7;
            this.label3.Text = "&Test Date";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // HolidayTestForm
            // 
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(824, 425);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HolidayTestForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Holiday Classes";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatesFound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcToYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcFromYear)).EndInit();
            this.grpEaster.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnTestDate;
        private System.Windows.Forms.Button btnEaster;
        private System.Windows.Forms.GroupBox grpEaster;
        private System.Windows.Forms.RadioButton rbJulian;
        private System.Windows.Forms.RadioButton rbOrthodox;
        private System.Windows.Forms.RadioButton rbGregorian;
        private System.Windows.Forms.NumericUpDown udcFromYear;
        private System.Windows.Forms.NumericUpDown udcToYear;
        private System.Windows.Forms.DateTimePicker dtpTestDate;
        private EWSoftware.PDI.Windows.Forms.HolidayManager hmHolidays;
        private System.Windows.Forms.DataGridView dgvDatesFound;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbcDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbcDescription;
    }
}
