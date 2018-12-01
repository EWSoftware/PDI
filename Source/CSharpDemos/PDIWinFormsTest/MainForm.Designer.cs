namespace PDIWinFormsTest
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnHolidays = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRRULE = new System.Windows.Forms.Button();
            this.btnTestCalRecur = new System.Windows.Forms.Button();
            this.btnTestVTimeZone = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnHolidays
            // 
            this.btnHolidays.Location = new System.Drawing.Point(184, 18);
            this.btnHolidays.Name = "btnHolidays";
            this.btnHolidays.Size = new System.Drawing.Size(160, 40);
            this.btnHolidays.TabIndex = 0;
            this.btnHolidays.Text = "Test &Holidays";
            this.toolTip1.SetToolTip(this.btnHolidays, "Test holiday generation classes");
            this.btnHolidays.Click += new System.EventHandler(this.btnHolidays_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(184, 293);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(160, 40);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "E&xit";
            this.toolTip1.SetToolTip(this.btnExit, "Exit the application");
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRRULE
            // 
            this.btnRRULE.Location = new System.Drawing.Point(184, 73);
            this.btnRRULE.Name = "btnRRULE";
            this.btnRRULE.Size = new System.Drawing.Size(160, 40);
            this.btnRRULE.TabIndex = 1;
            this.btnRRULE.Text = "Test RR&ULE";
            this.toolTip1.SetToolTip(this.btnRRULE, "Test the recurrence class");
            this.btnRRULE.Click += new System.EventHandler(this.btnRRULE_Click);
            // 
            // btnTestCalRecur
            // 
            this.btnTestCalRecur.Location = new System.Drawing.Point(184, 128);
            this.btnTestCalRecur.Name = "btnTestCalRecur";
            this.btnTestCalRecur.Size = new System.Drawing.Size(160, 40);
            this.btnTestCalRecur.TabIndex = 2;
            this.btnTestCalRecur.Text = "Test C&al Recur";
            this.toolTip1.SetToolTip(this.btnTestCalRecur, "Test iCalendar recurrence");
            this.btnTestCalRecur.Click += new System.EventHandler(this.btnTestCalRecur_Click);
            // 
            // btnTestVTimeZone
            // 
            this.btnTestVTimeZone.Location = new System.Drawing.Point(184, 183);
            this.btnTestVTimeZone.Name = "btnTestVTimeZone";
            this.btnTestVTimeZone.Size = new System.Drawing.Size(160, 40);
            this.btnTestVTimeZone.TabIndex = 3;
            this.btnTestVTimeZone.Text = "Test &VTimeZone";
            this.toolTip1.SetToolTip(this.btnTestVTimeZone, "Test the VTIMEZONE classes");
            this.btnTestVTimeZone.Click += new System.EventHandler(this.btnTestVTimeZone_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(184, 238);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(160, 40);
            this.btnAbout.TabIndex = 4;
            this.btnAbout.Text = "A&bout";
            this.toolTip1.SetToolTip(this.btnAbout, "View application copyright and version information");
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(528, 353);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnTestVTimeZone);
            this.Controls.Add(this.btnTestCalRecur);
            this.Controls.Add(this.btnRRULE);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnHolidays);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EWSoftware.PDI Test";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnHolidays;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRRULE;
        private System.Windows.Forms.Button btnTestCalRecur;
        private System.Windows.Forms.Button btnTestVTimeZone;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
