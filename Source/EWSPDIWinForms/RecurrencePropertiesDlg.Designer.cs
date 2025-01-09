namespace EWSoftware.PDI.Windows.Forms
{
    partial class RecurrencePropertiesDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecurrencePropertiesDlg));
            rpRecurrence = new RecurrencePattern();
            btnCancel = new System.Windows.Forms.Button();
            btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rpRecurrence
            // 
            resources.ApplyResources(rpRecurrence, "rpRecurrence");
            rpRecurrence.Name = "rpRecurrence";
            rpRecurrence.ShowEndTime = false;
            // 
            // btnCancel
            // 
            resources.ApplyResources(btnCancel, "btnCancel");
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Name = "btnCancel";
            // 
            // btnOK
            // 
            resources.ApplyResources(btnOK, "btnOK");
            btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            btnOK.Name = "btnOK";
            // 
            // RecurrencePropertiesDlg
            // 
            this.AcceptButton = btnOK;
            resources.ApplyResources(this, "$this");
            this.CancelButton = btnCancel;
            this.Controls.Add(btnCancel);
            this.Controls.Add(btnOK);
            this.Controls.Add(rpRecurrence);
            this.MinimizeBox = false;
            this.Name = "RecurrencePropertiesDlg";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.ResumeLayout(false);
        }

        #endregion

        private EWSoftware.PDI.Windows.Forms.RecurrencePattern rpRecurrence;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}