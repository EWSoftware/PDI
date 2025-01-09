namespace EWSoftware.PDI.Windows.Forms
{
    partial class HolidayPropertiesDlg
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HolidayPropertiesDlg));
            btnOK = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            txtDescription = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            rbFloating = new System.Windows.Forms.RadioButton();
            rbFixed = new System.Windows.Forms.RadioButton();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            cboOccurrence = new System.Windows.Forms.ComboBox();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            cboDayOfWeek = new System.Windows.Forms.ComboBox();
            cboMonth = new System.Windows.Forms.ComboBox();
            epErrors = new System.Windows.Forms.ErrorProvider(components);
            chkAdjustDate = new System.Windows.Forms.CheckBox();
            udcOffset = new System.Windows.Forms.NumericUpDown();
            udcDayOfMonth = new System.Windows.Forms.NumericUpDown();
            udcMinimumYear = new System.Windows.Forms.NumericUpDown();
            label7 = new System.Windows.Forms.Label();
            udcMaximumYear = new System.Windows.Forms.NumericUpDown();
            label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)epErrors).BeginInit();
            ((System.ComponentModel.ISupportInitialize)udcOffset).BeginInit();
            ((System.ComponentModel.ISupportInitialize)udcDayOfMonth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)udcMinimumYear).BeginInit();
            ((System.ComponentModel.ISupportInitialize)udcMaximumYear).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(btnOK, "btnOK");
            btnOK.Name = "btnOK";
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(btnCancel, "btnCancel");
            btnCancel.Name = "btnCancel";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // txtDescription
            // 
            resources.ApplyResources(txtDescription, "txtDescription");
            txtDescription.Name = "txtDescription";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // rbFloating
            // 
            resources.ApplyResources(rbFloating, "rbFloating");
            rbFloating.Name = "rbFloating";
            rbFloating.CheckedChanged += this.Type_CheckedChanged;
            // 
            // rbFixed
            // 
            resources.ApplyResources(rbFixed, "rbFixed");
            rbFixed.Name = "rbFixed";
            rbFixed.CheckedChanged += this.Type_CheckedChanged;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // cboOccurrence
            // 
            cboOccurrence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(cboOccurrence, "cboOccurrence");
            cboOccurrence.Name = "cboOccurrence";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            // 
            // cboDayOfWeek
            // 
            cboDayOfWeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(cboDayOfWeek, "cboDayOfWeek");
            cboDayOfWeek.Name = "cboDayOfWeek";
            // 
            // cboMonth
            // 
            cboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(cboMonth, "cboMonth");
            cboMonth.Name = "cboMonth";
            // 
            // epErrors
            // 
            epErrors.ContainerControl = this;
            // 
            // chkAdjustDate
            // 
            resources.ApplyResources(chkAdjustDate, "chkAdjustDate");
            chkAdjustDate.Name = "chkAdjustDate";
            // 
            // udcOffset
            // 
            resources.ApplyResources(udcOffset, "udcOffset");
            udcOffset.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            udcOffset.Minimum = new decimal(new int[] { 999, 0, 0, System.Int32.MinValue });
            udcOffset.Name = "udcOffset";
            udcOffset.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // udcDayOfMonth
            // 
            resources.ApplyResources(udcDayOfMonth, "udcDayOfMonth");
            udcDayOfMonth.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            udcDayOfMonth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            udcDayOfMonth.Name = "udcDayOfMonth";
            udcDayOfMonth.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // udcMinimumYear
            // 
            resources.ApplyResources(udcMinimumYear, "udcMinimumYear");
            udcMinimumYear.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            udcMinimumYear.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            udcMinimumYear.Name = "udcMinimumYear";
            udcMinimumYear.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
            // 
            // udcMaximumYear
            // 
            resources.ApplyResources(udcMaximumYear, "udcMaximumYear");
            udcMaximumYear.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            udcMaximumYear.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            udcMaximumYear.Name = "udcMaximumYear";
            udcMaximumYear.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label8
            // 
            resources.ApplyResources(label8, "label8");
            label8.Name = "label8";
            // 
            // HolidayPropertiesDlg
            // 
            this.AcceptButton = btnOK;
            resources.ApplyResources(this, "$this");
            this.CancelButton = btnCancel;
            this.Controls.Add(udcMaximumYear);
            this.Controls.Add(label8);
            this.Controls.Add(udcMinimumYear);
            this.Controls.Add(label7);
            this.Controls.Add(udcDayOfMonth);
            this.Controls.Add(udcOffset);
            this.Controls.Add(chkAdjustDate);
            this.Controls.Add(cboMonth);
            this.Controls.Add(label6);
            this.Controls.Add(cboDayOfWeek);
            this.Controls.Add(label5);
            this.Controls.Add(cboOccurrence);
            this.Controls.Add(txtDescription);
            this.Controls.Add(label4);
            this.Controls.Add(label3);
            this.Controls.Add(rbFixed);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Controls.Add(btnCancel);
            this.Controls.Add(btnOK);
            this.Controls.Add(rbFloating);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HolidayPropertiesDlg";
            this.ShowInTaskbar = false;
            this.Closing += this.HolidayPropertiesDlg_Closing;
            ((System.ComponentModel.ISupportInitialize)epErrors).EndInit();
            ((System.ComponentModel.ISupportInitialize)udcOffset).EndInit();
            ((System.ComponentModel.ISupportInitialize)udcDayOfMonth).EndInit();
            ((System.ComponentModel.ISupportInitialize)udcMinimumYear).EndInit();
            ((System.ComponentModel.ISupportInitialize)udcMaximumYear).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.RadioButton rbFloating;
        private System.Windows.Forms.RadioButton rbFixed;
        private System.Windows.Forms.ComboBox cboOccurrence;
        private System.Windows.Forms.ComboBox cboDayOfWeek;
        private System.Windows.Forms.ComboBox cboMonth;
        private System.Windows.Forms.ErrorProvider epErrors;
        private System.Windows.Forms.CheckBox chkAdjustDate;
        private System.Windows.Forms.NumericUpDown udcOffset;
        private System.Windows.Forms.NumericUpDown udcDayOfMonth;
        private System.Windows.Forms.NumericUpDown udcMaximumYear;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown udcMinimumYear;
        private System.Windows.Forms.Label label7;
    }
}
