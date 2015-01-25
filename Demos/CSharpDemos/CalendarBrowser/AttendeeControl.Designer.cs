namespace CalendarBrowser
{
    partial class AttendeeControl
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
            this.txtSentBy = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAttendee = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkRSVP = new System.Windows.Forms.CheckBox();
            this.cboRole = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCommonName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUserType = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.txtSentBy);
            this.pnlControls.Controls.Add(this.label6);
            this.pnlControls.Controls.Add(this.txtAttendee);
            this.pnlControls.Controls.Add(this.label4);
            this.pnlControls.Controls.Add(this.chkRSVP);
            this.pnlControls.Controls.Add(this.cboRole);
            this.pnlControls.Controls.Add(this.label3);
            this.pnlControls.Controls.Add(this.cboStatus);
            this.pnlControls.Controls.Add(this.label10);
            this.pnlControls.Controls.Add(this.txtCommonName);
            this.pnlControls.Controls.Add(this.label1);
            this.pnlControls.Controls.Add(this.txtUserType);
            this.pnlControls.Controls.Add(this.label5);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlControls.Location = new System.Drawing.Point(0, 0);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(496, 173);
            this.pnlControls.TabIndex = 0;
            // 
            // txtSentBy
            // 
            this.txtSentBy.Location = new System.Drawing.Point(128, 107);
            this.txtSentBy.Name = "txtSentBy";
            this.txtSentBy.Size = new System.Drawing.Size(248, 22);
            this.txtSentBy.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(58, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 23);
            this.label6.TabIndex = 8;
            this.label6.Text = "Sent By";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAttendee
            // 
            this.txtAttendee.Location = new System.Drawing.Point(128, 11);
            this.txtAttendee.Name = "txtAttendee";
            this.txtAttendee.Size = new System.Drawing.Size(248, 22);
            this.txtAttendee.TabIndex = 1;
            this.txtAttendee.Validating += new System.ComponentModel.CancelEventHandler(this.txtAttendee_Validating);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(50, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 23);
            this.label4.TabIndex = 0;
            this.label4.Text = "Attendee";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkRSVP
            // 
            this.chkRSVP.Location = new System.Drawing.Point(328, 139);
            this.chkRSVP.Name = "chkRSVP";
            this.chkRSVP.Size = new System.Drawing.Size(72, 24);
            this.chkRSVP.TabIndex = 12;
            this.chkRSVP.Text = "RSVP";
            // 
            // cboRole
            // 
            this.cboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRole.Items.AddRange(new object[] {
            "",
            "CHAIR",
            "REQ-PARTICIPANT",
            "OPT-PARTICIPANT",
            "NON-PARTICIPANT"});
            this.cboRole.Location = new System.Drawing.Point(328, 75);
            this.cboRole.MaxDropDownItems = 16;
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new System.Drawing.Size(160, 24);
            this.cboRole.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(276, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "Role";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Items.AddRange(new object[] {
            "",
            "NEEDS-ACTION",
            "ACCEPTED",
            "DECLINED",
            "TENTATIVE",
            "DELEGATED",
            "COMPLETED",
            "IN-PROCESS"});
            this.cboStatus.Location = new System.Drawing.Point(128, 139);
            this.cboStatus.MaxDropDownItems = 16;
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(176, 24);
            this.cboStatus.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(63, 139);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 23);
            this.label10.TabIndex = 10;
            this.label10.Text = "Status";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCommonName
            // 
            this.txtCommonName.Location = new System.Drawing.Point(128, 43);
            this.txtCommonName.Name = "txtCommonName";
            this.txtCommonName.Size = new System.Drawing.Size(248, 22);
            this.txtCommonName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Common Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUserType
            // 
            this.txtUserType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUserType.Location = new System.Drawing.Point(128, 75);
            this.txtUserType.Name = "txtUserType";
            this.txtUserType.Size = new System.Drawing.Size(128, 22);
            this.txtUserType.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(36, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 23);
            this.label5.TabIndex = 4;
            this.label5.Text = "User Type";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // AttendeeControl
            // 
            this.Controls.Add(this.pnlControls);
            this.Name = "AttendeeControl";
            this.Size = new System.Drawing.Size(496, 200);
            this.Controls.SetChildIndex(this.pnlControls, 0);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.TextBox txtSentBy;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAttendee;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkRSVP;
        private System.Windows.Forms.ComboBox cboRole;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCommonName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUserType;
        private System.Windows.Forms.Label label5;

    }
}
