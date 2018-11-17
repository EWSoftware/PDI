namespace CalendarBrowser
{
    partial class VTimeZoneDlg
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
            this.components = new System.ComponentModel.Container();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTimeZoneId = new System.Windows.Forms.TextBox();
            this.txtTimeZoneUrl = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLastModified = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grpObRules = new System.Windows.Forms.GroupBox();
            this.ucRules = new CalendarBrowser.ObservanceRuleControl();
            this.epErrors = new System.Windows.Forms.ErrorProvider(this.components);
            this.grpObRules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epErrors)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(12, 493);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 32);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            // 
            // btnCancel
            // 
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(502, 493);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 32);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(17, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Time Zone ID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTimeZoneId
            // 
            this.txtTimeZoneId.Location = new System.Drawing.Point(129, 12);
            this.txtTimeZoneId.Name = "txtTimeZoneId";
            this.txtTimeZoneId.Size = new System.Drawing.Size(448, 22);
            this.txtTimeZoneId.TabIndex = 1;
            // 
            // txtTimeZoneUrl
            // 
            this.txtTimeZoneUrl.Location = new System.Drawing.Point(129, 40);
            this.txtTimeZoneUrl.Name = "txtTimeZoneUrl";
            this.txtTimeZoneUrl.Size = new System.Drawing.Size(448, 22);
            this.txtTimeZoneUrl.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Time Zone &URL";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLastModified
            // 
            this.txtLastModified.Location = new System.Drawing.Point(129, 72);
            this.txtLastModified.Name = "txtLastModified";
            this.txtLastModified.ReadOnly = true;
            this.txtLastModified.Size = new System.Drawing.Size(168, 22);
            this.txtLastModified.TabIndex = 5;
            this.txtLastModified.TabStop = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(19, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Last Modified";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpObRules
            // 
            this.grpObRules.Controls.Add(this.ucRules);
            this.grpObRules.Location = new System.Drawing.Point(12, 104);
            this.grpObRules.Name = "grpObRules";
            this.grpObRules.Size = new System.Drawing.Size(578, 383);
            this.grpObRules.TabIndex = 6;
            this.grpObRules.TabStop = false;
            this.grpObRules.Text = "Observance Rules";
            // 
            // ucRules
            // 
            this.ucRules.Location = new System.Drawing.Point(5, 28);
            this.ucRules.Name = "ucRules";
            this.ucRules.Size = new System.Drawing.Size(568, 344);
            this.ucRules.TabIndex = 0;
            // 
            // epErrors
            // 
            this.epErrors.ContainerControl = this;
            // 
            // VTimeZoneDlg
            // 
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(602, 537);
            this.Controls.Add(this.grpObRules);
            this.Controls.Add(this.txtLastModified);
            this.Controls.Add(this.txtTimeZoneUrl);
            this.Controls.Add(this.txtTimeZoneId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VTimeZoneDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Time Zone Properties";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.VTimeZoneDlg_Closing);
            this.grpObRules.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.epErrors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTimeZoneId;
        private System.Windows.Forms.TextBox txtTimeZoneUrl;
        private System.Windows.Forms.TextBox txtLastModified;
        private System.Windows.Forms.GroupBox grpObRules;
        private CalendarBrowser.ObservanceRuleControl ucRules;
        private System.Windows.Forms.ErrorProvider epErrors;
    }
}
