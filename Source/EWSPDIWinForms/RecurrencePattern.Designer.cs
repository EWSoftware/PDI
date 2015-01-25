namespace EWSoftware.PDI.Windows.Forms
{
    partial class RecurrencePattern
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecurrencePattern));
            this.grpPattern = new System.Windows.Forms.GroupBox();
            this.chkAdvanced = new System.Windows.Forms.CheckBox();
            this.rbSecondly = new System.Windows.Forms.RadioButton();
            this.rbMinutely = new System.Windows.Forms.RadioButton();
            this.rbHourly = new System.Windows.Forms.RadioButton();
            this.gbSeparator = new System.Windows.Forms.GroupBox();
            this.pnlPatterns = new System.Windows.Forms.Panel();
            this.ucSecondly = new EWSoftware.PDI.Windows.Forms.SecondlyPattern();
            this.ucHourly = new EWSoftware.PDI.Windows.Forms.HourlyPattern();
            this.ucMinutely = new EWSoftware.PDI.Windows.Forms.MinutelyPattern();
            this.ucDaily = new EWSoftware.PDI.Windows.Forms.DailyPattern();
            this.ucWeekly = new EWSoftware.PDI.Windows.Forms.WeeklyPattern();
            this.ucMonthly = new EWSoftware.PDI.Windows.Forms.MonthlyPattern();
            this.ucYearly = new EWSoftware.PDI.Windows.Forms.YearlyPattern();
            this.rbYearly = new System.Windows.Forms.RadioButton();
            this.rbMonthly = new System.Windows.Forms.RadioButton();
            this.rbWeekly = new System.Windows.Forms.RadioButton();
            this.rbDaily = new System.Windows.Forms.RadioButton();
            this.ucAdvanced = new EWSoftware.PDI.Windows.Forms.AdvancedPattern();
            this.grpRange = new System.Windows.Forms.GroupBox();
            this.cboWeekStartDay = new System.Windows.Forms.ComboBox();
            this.lblWeekStartDay = new System.Windows.Forms.Label();
            this.udcOccurrences = new System.Windows.Forms.NumericUpDown();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.rbEndByDate = new System.Windows.Forms.RadioButton();
            this.rbNeverEnds = new System.Windows.Forms.RadioButton();
            this.chkHolidays = new System.Windows.Forms.CheckBox();
            this.rbEndAfter = new System.Windows.Forms.RadioButton();
            this.grpPattern.SuspendLayout();
            this.pnlPatterns.SuspendLayout();
            this.grpRange.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udcOccurrences)).BeginInit();
            this.SuspendLayout();
            // 
            // grpPattern
            // 
            resources.ApplyResources(this.grpPattern, "grpPattern");
            this.grpPattern.Controls.Add(this.chkAdvanced);
            this.grpPattern.Controls.Add(this.rbSecondly);
            this.grpPattern.Controls.Add(this.rbMinutely);
            this.grpPattern.Controls.Add(this.rbHourly);
            this.grpPattern.Controls.Add(this.gbSeparator);
            this.grpPattern.Controls.Add(this.pnlPatterns);
            this.grpPattern.Controls.Add(this.rbYearly);
            this.grpPattern.Controls.Add(this.rbMonthly);
            this.grpPattern.Controls.Add(this.rbWeekly);
            this.grpPattern.Controls.Add(this.rbDaily);
            this.grpPattern.Controls.Add(this.ucAdvanced);
            this.grpPattern.Name = "grpPattern";
            this.grpPattern.TabStop = false;
            // 
            // chkAdvanced
            // 
            resources.ApplyResources(this.chkAdvanced, "chkAdvanced");
            this.chkAdvanced.Name = "chkAdvanced";
            this.chkAdvanced.CheckedChanged += new System.EventHandler(this.chkAdvanced_CheckedChanged);
            // 
            // rbSecondly
            // 
            resources.ApplyResources(this.rbSecondly, "rbSecondly");
            this.rbSecondly.Name = "rbSecondly";
            this.rbSecondly.CheckedChanged += new System.EventHandler(this.Pattern_CheckedChanged);
            // 
            // rbMinutely
            // 
            resources.ApplyResources(this.rbMinutely, "rbMinutely");
            this.rbMinutely.Name = "rbMinutely";
            this.rbMinutely.CheckedChanged += new System.EventHandler(this.Pattern_CheckedChanged);
            // 
            // rbHourly
            // 
            resources.ApplyResources(this.rbHourly, "rbHourly");
            this.rbHourly.Name = "rbHourly";
            this.rbHourly.CheckedChanged += new System.EventHandler(this.Pattern_CheckedChanged);
            // 
            // gbSeparator
            // 
            resources.ApplyResources(this.gbSeparator, "gbSeparator");
            this.gbSeparator.Name = "gbSeparator";
            this.gbSeparator.TabStop = false;
            // 
            // pnlPatterns
            // 
            this.pnlPatterns.Controls.Add(this.ucSecondly);
            this.pnlPatterns.Controls.Add(this.ucHourly);
            this.pnlPatterns.Controls.Add(this.ucMinutely);
            this.pnlPatterns.Controls.Add(this.ucDaily);
            this.pnlPatterns.Controls.Add(this.ucWeekly);
            this.pnlPatterns.Controls.Add(this.ucMonthly);
            this.pnlPatterns.Controls.Add(this.ucYearly);
            resources.ApplyResources(this.pnlPatterns, "pnlPatterns");
            this.pnlPatterns.Name = "pnlPatterns";
            // 
            // ucSecondly
            // 
            resources.ApplyResources(this.ucSecondly, "ucSecondly");
            this.ucSecondly.Name = "ucSecondly";
            // 
            // ucHourly
            // 
            resources.ApplyResources(this.ucHourly, "ucHourly");
            this.ucHourly.Name = "ucHourly";
            // 
            // ucMinutely
            // 
            resources.ApplyResources(this.ucMinutely, "ucMinutely");
            this.ucMinutely.Name = "ucMinutely";
            // 
            // ucDaily
            // 
            resources.ApplyResources(this.ucDaily, "ucDaily");
            this.ucDaily.Name = "ucDaily";
            // 
            // ucWeekly
            // 
            resources.ApplyResources(this.ucWeekly, "ucWeekly");
            this.ucWeekly.Name = "ucWeekly";
            // 
            // ucMonthly
            // 
            resources.ApplyResources(this.ucMonthly, "ucMonthly");
            this.ucMonthly.Name = "ucMonthly";
            // 
            // ucYearly
            // 
            resources.ApplyResources(this.ucYearly, "ucYearly");
            this.ucYearly.Name = "ucYearly";
            // 
            // rbYearly
            // 
            resources.ApplyResources(this.rbYearly, "rbYearly");
            this.rbYearly.Name = "rbYearly";
            this.rbYearly.CheckedChanged += new System.EventHandler(this.Pattern_CheckedChanged);
            // 
            // rbMonthly
            // 
            resources.ApplyResources(this.rbMonthly, "rbMonthly");
            this.rbMonthly.Name = "rbMonthly";
            this.rbMonthly.CheckedChanged += new System.EventHandler(this.Pattern_CheckedChanged);
            // 
            // rbWeekly
            // 
            resources.ApplyResources(this.rbWeekly, "rbWeekly");
            this.rbWeekly.Name = "rbWeekly";
            this.rbWeekly.CheckedChanged += new System.EventHandler(this.Pattern_CheckedChanged);
            // 
            // rbDaily
            // 
            resources.ApplyResources(this.rbDaily, "rbDaily");
            this.rbDaily.Name = "rbDaily";
            this.rbDaily.CheckedChanged += new System.EventHandler(this.Pattern_CheckedChanged);
            // 
            // ucAdvanced
            // 
            resources.ApplyResources(this.ucAdvanced, "ucAdvanced");
            this.ucAdvanced.Name = "ucAdvanced";
            // 
            // grpRange
            // 
            resources.ApplyResources(this.grpRange, "grpRange");
            this.grpRange.Controls.Add(this.cboWeekStartDay);
            this.grpRange.Controls.Add(this.lblWeekStartDay);
            this.grpRange.Controls.Add(this.udcOccurrences);
            this.grpRange.Controls.Add(this.dtpEndDate);
            this.grpRange.Controls.Add(this.label2);
            this.grpRange.Controls.Add(this.rbEndByDate);
            this.grpRange.Controls.Add(this.rbNeverEnds);
            this.grpRange.Controls.Add(this.chkHolidays);
            this.grpRange.Controls.Add(this.rbEndAfter);
            this.grpRange.Name = "grpRange";
            this.grpRange.TabStop = false;
            // 
            // cboWeekStartDay
            // 
            this.cboWeekStartDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboWeekStartDay, "cboWeekStartDay");
            this.cboWeekStartDay.Name = "cboWeekStartDay";
            // 
            // lblWeekStartDay
            // 
            resources.ApplyResources(this.lblWeekStartDay, "lblWeekStartDay");
            this.lblWeekStartDay.Name = "lblWeekStartDay";
            // 
            // udcOccurrences
            // 
            resources.ApplyResources(this.udcOccurrences, "udcOccurrences");
            this.udcOccurrences.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.udcOccurrences.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udcOccurrences.Name = "udcOccurrences";
            this.udcOccurrences.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // dtpEndDate
            // 
            resources.ApplyResources(this.dtpEndDate, "dtpEndDate");
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Name = "dtpEndDate";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // rbEndByDate
            // 
            resources.ApplyResources(this.rbEndByDate, "rbEndByDate");
            this.rbEndByDate.Name = "rbEndByDate";
            this.rbEndByDate.CheckedChanged += new System.EventHandler(this.Range_CheckedChanged);
            // 
            // rbNeverEnds
            // 
            resources.ApplyResources(this.rbNeverEnds, "rbNeverEnds");
            this.rbNeverEnds.Name = "rbNeverEnds";
            this.rbNeverEnds.CheckedChanged += new System.EventHandler(this.Range_CheckedChanged);
            // 
            // chkHolidays
            // 
            resources.ApplyResources(this.chkHolidays, "chkHolidays");
            this.chkHolidays.Name = "chkHolidays";
            // 
            // rbEndAfter
            // 
            resources.ApplyResources(this.rbEndAfter, "rbEndAfter");
            this.rbEndAfter.Name = "rbEndAfter";
            this.rbEndAfter.CheckedChanged += new System.EventHandler(this.Range_CheckedChanged);
            // 
            // RecurrencePattern
            // 
            this.Controls.Add(this.grpRange);
            this.Controls.Add(this.grpPattern);
            this.Name = "RecurrencePattern";
            resources.ApplyResources(this, "$this");
            this.grpPattern.ResumeLayout(false);
            this.pnlPatterns.ResumeLayout(false);
            this.grpRange.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.udcOccurrences)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.GroupBox grpPattern;
        private System.Windows.Forms.GroupBox gbSeparator;
        private System.Windows.Forms.Panel pnlPatterns;
        private System.Windows.Forms.RadioButton rbYearly;
        private System.Windows.Forms.RadioButton rbMonthly;
        private System.Windows.Forms.RadioButton rbWeekly;
        private System.Windows.Forms.RadioButton rbDaily;
        private System.Windows.Forms.GroupBox grpRange;
        private System.Windows.Forms.RadioButton rbNeverEnds;
        private System.Windows.Forms.RadioButton rbEndAfter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private EWSoftware.PDI.Windows.Forms.WeeklyPattern ucWeekly;
        private EWSoftware.PDI.Windows.Forms.DailyPattern ucDaily;
        private System.Windows.Forms.RadioButton rbEndByDate;
        private System.Windows.Forms.CheckBox chkHolidays;
        private EWSoftware.PDI.Windows.Forms.MonthlyPattern ucMonthly;
        private EWSoftware.PDI.Windows.Forms.YearlyPattern ucYearly;
        private System.Windows.Forms.NumericUpDown udcOccurrences;
        private System.Windows.Forms.Label lblWeekStartDay;
        private System.Windows.Forms.ComboBox cboWeekStartDay;
        private System.Windows.Forms.RadioButton rbHourly;
        private System.Windows.Forms.RadioButton rbMinutely;
        private System.Windows.Forms.RadioButton rbSecondly;
        private EWSoftware.PDI.Windows.Forms.MinutelyPattern ucMinutely;
        private EWSoftware.PDI.Windows.Forms.HourlyPattern ucHourly;
        private EWSoftware.PDI.Windows.Forms.SecondlyPattern ucSecondly;
        private System.Windows.Forms.CheckBox chkAdvanced;
        private EWSoftware.PDI.Windows.Forms.AdvancedPattern ucAdvanced;
    }
}
