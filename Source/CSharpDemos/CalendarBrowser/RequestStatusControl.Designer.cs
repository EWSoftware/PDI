namespace CalendarBrowser
{
    partial class RequestStatusControl
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
            this.txtStatusCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExtData = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtStatusCode
            // 
            this.txtStatusCode.Location = new System.Drawing.Point(120, 8);
            this.txtStatusCode.Name = "txtStatusCode";
            this.txtStatusCode.Size = new System.Drawing.Size(128, 22);
            this.txtStatusCode.TabIndex = 1;
            this.txtStatusCode.Validating += new System.ComponentModel.CancelEventHandler(this.txtStatusCode_Validating);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(26, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "Status Code";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(120, 40);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(264, 22);
            this.txtMessage.TabIndex = 3;
            this.txtMessage.Validating += new System.ComponentModel.CancelEventHandler(this.txtMessage_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(29, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Message";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtExtData
            // 
            this.txtExtData.Location = new System.Drawing.Point(120, 72);
            this.txtExtData.Name = "txtExtData";
            this.txtExtData.Size = new System.Drawing.Size(264, 22);
            this.txtExtData.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Extended Data";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RequestStatusControl
            // 
            this.Controls.Add(this.txtExtData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtStatusCode);
            this.Controls.Add(this.label5);
            this.Name = "RequestStatusControl";
            this.Size = new System.Drawing.Size(400, 136);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.txtStatusCode, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtMessage, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtExtData, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtStatusCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtExtData;
        private System.Windows.Forms.Label label2;
    }
}
