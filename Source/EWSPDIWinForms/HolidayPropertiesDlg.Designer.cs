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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HolidayPropertiesDlg));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbFloating = new System.Windows.Forms.RadioButton();
            this.rbFixed = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboOccurrence = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboDayOfWeek = new System.Windows.Forms.ComboBox();
            this.cboMonth = new System.Windows.Forms.ComboBox();
            this.epErrors = new System.Windows.Forms.ErrorProvider(this.components);
            this.chkAdjustDate = new System.Windows.Forms.CheckBox();
            this.udcOffset = new System.Windows.Forms.NumericUpDown();
            this.udcDayOfMonth = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.epErrors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcDayOfMonth)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtDescription
            // 
            resources.ApplyResources(this.txtDescription, "txtDescription");
            this.txtDescription.Name = "txtDescription";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // rbFloating
            // 
            resources.ApplyResources(this.rbFloating, "rbFloating");
            this.rbFloating.Name = "rbFloating";
            this.rbFloating.CheckedChanged += new System.EventHandler(this.Type_CheckedChanged);
            // 
            // rbFixed
            // 
            resources.ApplyResources(this.rbFixed, "rbFixed");
            this.rbFixed.Name = "rbFixed";
            this.rbFixed.CheckedChanged += new System.EventHandler(this.Type_CheckedChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // cboOccurrence
            // 
            this.cboOccurrence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboOccurrence, "cboOccurrence");
            this.cboOccurrence.Name = "cboOccurrence";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // cboDayOfWeek
            // 
            this.cboDayOfWeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboDayOfWeek, "cboDayOfWeek");
            this.cboDayOfWeek.Name = "cboDayOfWeek";
            // 
            // cboMonth
            // 
            this.cboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboMonth, "cboMonth");
            this.cboMonth.Name = "cboMonth";
            // 
            // epErrors
            // 
            this.epErrors.ContainerControl = this;
            // 
            // chkAdjustDate
            // 
            resources.ApplyResources(this.chkAdjustDate, "chkAdjustDate");
            this.chkAdjustDate.Name = "chkAdjustDate";
            // 
            // udcOffset
            // 
            resources.ApplyResources(this.udcOffset, "udcOffset");
            this.udcOffset.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.udcOffset.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.udcOffset.Name = "udcOffset";
            // 
            // udcDayOfMonth
            // 
            resources.ApplyResources(this.udcDayOfMonth, "udcDayOfMonth");
            this.udcDayOfMonth.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.udcDayOfMonth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udcDayOfMonth.Name = "udcDayOfMonth";
            this.udcDayOfMonth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // HolidayPropertiesDlg
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.udcDayOfMonth);
            this.Controls.Add(this.udcOffset);
            this.Controls.Add(this.chkAdjustDate);
            this.Controls.Add(this.cboMonth);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboDayOfWeek);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboOccurrence);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rbFixed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.rbFloating);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HolidayPropertiesDlg";
            this.ShowInTaskbar = false;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.HolidayPropertiesDlg_Closing);
            this.Load += new System.EventHandler(this.HolidayPropertiesDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.epErrors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcDayOfMonth)).EndInit();
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
    }
}
