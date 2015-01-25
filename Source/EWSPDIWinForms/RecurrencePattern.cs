//===============================================================================================================
// System  : EWSoftware.PDI Windows Forms Controls
// File    : RecurrencePattern.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/31/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains one of several user controls that are combined to allow the editing of various recurrence
// parameters.  This one is used to contain all the other user controls and sets the overall pattern to use for
// the recurrence.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 01/20/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace EWSoftware.PDI.Windows.Forms
{
	/// <summary>
    /// This user control is used to contain all the other recurrence pattern user controls and sets the overall
    /// pattern to use for the recurrence.
	/// </summary>
	public partial class RecurrencePattern : System.Windows.Forms.UserControl
	{
        #region Private data members
        //=====================================================================

        // The control visibility states and "no copy" flag.  Using the Visible property on the controls isn't
        // reliable because if the parent isn't visible, the controls return false even if they are visible on
        // the parent when it is visible.
        private bool weekStartDayVisible, holidayVisible, advancedVisible, noCopy;
        private RecurFrequency maxPattern;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This property is used to get or set whether or not the Week Start Day option is displayed.  It
        /// is visible by default.
        /// </summary>
        /// <remarks>If hidden, the Week Start Day combo box will not be visible and a default week start day
        /// will be used in all recurrences edited by the control.</remarks>
        [Category("Behavior"), DefaultValue(true), Bindable(true),
         Description("Show or hide the Week Start Day combo box")]
        public bool ShowWeekStartDay
        {
            get { return weekStartDayVisible; }
            set { weekStartDayVisible = lblWeekStartDay.Visible = cboWeekStartDay.Visible = value; }
        }

        /// <summary>
        /// This property is used to get or set whether or not the Can Occur On Holiday option is displayed.  It
        /// is visible by default.
        /// </summary>
        /// <remarks>If hidden, the check box will not be visible and the option will be set to true in all
        /// recurrences edited by the control.</remarks>
        [Category("Behavior"), DefaultValue(true), Bindable(true),
         Description("Show or hide the Can Occur on Holiday checkbox")]
        public bool ShowCanOccurOnHoliday
        {
            get { return holidayVisible; }
            set
            {
                holidayVisible = chkHolidays.Visible = value;

                // Always on if not visible
                if(!value)
                    chkHolidays.Checked = true;
            }
        }

        /// <summary>
        /// This property is used to get or set whether or not the Advanced checkbox is visible and thus give
        /// access to the advanced pattern options.  It is visible by default.
        /// </summary>
        /// <remarks>If hidden, only basic patterns similar to those in Microsoft Outlook can be created.  When
        /// edited, all advanced options currently in effect will be lost and the pattern will be made to conform
        /// to the simple options available for the currently selected frequency.</remarks>
        [Category("Behavior"), DefaultValue(true), Bindable(true),
         Description("Show or hide the Advanced checkbox")]
        public bool ShowAdvanced
        {
            get { return advancedVisible; }
            set
            {
                advancedVisible = chkAdvanced.Visible = value;

                if(!value)
                {
                    noCopy = true;
                    chkAdvanced.Checked = false;
                    noCopy = false;
                }
            }
        }

        /// <summary>
        /// This property is used to get or set the maximum pattern option to display.  All pattern options will
        /// be visible by default.
        /// </summary>
        /// <remarks>If set to <c>Daily</c>, only basic patterns similar to those in Microsoft Outlook can be
        /// created (yearly, monthly, weekly, and daily).  If set to a recurrence with a pattern higher than the
        /// maximum, all options currently in effect will be lost and the pattern will be made to conform to the
        /// simple pattern for the maximum allowed pattern.</remarks>
        [Category("Behavior"), DefaultValue(RecurFrequency.Secondly),
          Bindable(true), Description("This determines the maximum editable recurrence pattern type")]
        public RecurFrequency MaximumPattern
        {
            get { return maxPattern; }
            set
            {
                if(value == RecurFrequency.Undefined)
                    maxPattern = RecurFrequency.Secondly;
                else
                    maxPattern = value;

                switch(maxPattern)
                {
                    case RecurFrequency.Yearly:
                        if(rbMonthly.Checked || rbWeekly.Checked || rbDaily.Checked || rbHourly.Checked ||
                          rbMinutely.Checked || rbSecondly.Checked)
                            rbYearly.Checked = true;

                        rbMonthly.Visible = rbWeekly.Visible = rbDaily.Visible = rbHourly.Visible =
                            rbMinutely.Visible = rbSecondly.Visible = false;
                        break;

                    case RecurFrequency.Monthly:
                        if(rbWeekly.Checked || rbDaily.Checked || rbHourly.Checked || rbMinutely.Checked ||
                          rbSecondly.Checked)
                            rbMonthly.Checked = true;

                        rbMonthly.Visible = true;
                        rbWeekly.Visible = rbDaily.Visible = rbHourly.Visible = rbMinutely.Visible =
                            rbSecondly.Visible = false;
                        break;

                    case RecurFrequency.Weekly:
                        if(rbDaily.Checked || rbHourly.Checked || rbMinutely.Checked || rbSecondly.Checked)
                            rbWeekly.Checked = true;

                        rbMonthly.Visible = rbWeekly.Visible = true;
                        rbDaily.Visible = rbHourly.Visible = rbMinutely.Visible = rbSecondly.Visible = false;
                        break;

                    case RecurFrequency.Daily:
                        if(rbHourly.Checked || rbMinutely.Checked || rbSecondly.Checked)
                            rbDaily.Checked = true;

                        rbMonthly.Visible = rbWeekly.Visible = rbDaily.Visible = true;
                        rbHourly.Visible = rbMinutely.Visible = rbSecondly.Visible = false;
                        break;

                    case RecurFrequency.Hourly:
                        if(rbMinutely.Checked || rbSecondly.Checked)
                            rbHourly.Checked = true;

                        rbMonthly.Visible = rbWeekly.Visible = rbDaily.Visible = rbHourly.Visible = true;
                        rbMinutely.Visible = rbSecondly.Visible = false;
                        break;

                    case RecurFrequency.Minutely:
                        if(rbSecondly.Checked)
                            rbMinutely.Checked = true;

                        rbMonthly.Visible = rbWeekly.Visible = rbDaily.Visible = rbHourly.Visible =
                            rbMinutely.Visible = true;
                        rbSecondly.Visible = false;
                        break;

                    default:
                        rbMonthly.Visible = rbWeekly.Visible = rbDaily.Visible = rbHourly.Visible =
                            rbMinutely.Visible = rbSecondly.Visible = true;
                        break;
                }
            }
        }

        /// <summary>
        /// This property is used to get or set whether or not the time value on the "end by date" option is
        /// visible and can be set.
        /// </summary>
        /// <remarks>If visible (the default), the end time can also be set for the "end by date" option.  If not
        /// visible, the time value on the "end by date" option will always be 12:00am.</remarks>
        [Category("Behavior"), DefaultValue(true), Bindable(true),
         Description("Show or hide the time value in the 'end by date' option")]
        public bool ShowEndTime
        {
            get { return (dtpEndDate.Width == 190); }
            set
            {
                // Set the date/time pattern based on the current culture
                if(value)
                {
                    dtpEndDate.Width = 190;
                    dtpEndDate.CustomFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " +
                        CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;
                }
                else
                {
                    dtpEndDate.Width = 110;
                    dtpEndDate.CustomFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                }
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public RecurrencePattern()
		{
			InitializeComponent();

            weekStartDayVisible = holidayVisible = advancedVisible = true;
            maxPattern = RecurFrequency.Secondly;

            cboWeekStartDay.DisplayMember = "Display";
            cboWeekStartDay.ValueMember = "Value";
            cboWeekStartDay.DataSource = RecurOptsDataSource.DayOfWeek;
            this.ShowEndTime = true;

            SetRecurrence(null);
		}
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// This is used to retrieve the recurrence information into the passed recurrence object
        /// </summary>
        /// <param name="recurrence">The recurrence in which to store the settings</param>
        /// <exception cref="ArgumentNullException">This is thrown if the passed recurrence object is null</exception>
        public void GetRecurrence(Recurrence recurrence)
        {
            Recurrence r = new Recurrence();

            if(recurrence == null)
                throw new ArgumentNullException("recurrence", LR.GetString("ExRPRecurrenceIsNull"));

            // Get the basic stuff
            r.Reset();
            r.WeekStart = (DayOfWeek)cboWeekStartDay.SelectedValue;
            r.CanOccurOnHoliday = chkHolidays.Checked;

            r.Frequency = rbYearly.Checked ? RecurFrequency.Yearly :
                rbMonthly.Checked ? RecurFrequency.Monthly :
                    rbWeekly.Checked ? RecurFrequency.Weekly :
                        rbDaily.Checked ? RecurFrequency.Daily :
                            rbHourly.Checked ? RecurFrequency.Hourly :
                                rbMinutely.Checked ? RecurFrequency.Minutely : RecurFrequency.Secondly;

            if(rbEndAfter.Checked)
                r.MaximumOccurrences = (int)udcOccurrences.Value;
            else
                if(rbEndByDate.Checked)
                    if(this.ShowEndTime)
                        r.RecurUntil = dtpEndDate.Value;
                    else
                        r.RecurUntil = dtpEndDate.Value.Date;

            // Get the frequency-specific stuff
            if(chkAdvanced.Checked)
                ucAdvanced.GetValues(r);
            else
                if(rbYearly.Checked)
                    ucYearly.GetValues(r);
                else
                    if(rbMonthly.Checked)
                        ucMonthly.GetValues(r);
                    else
                        if(rbWeekly.Checked)
                            ucWeekly.GetValues(r);
                        else
                            if(rbDaily.Checked)
                                ucDaily.GetValues(r);
                            else
                                if(rbHourly.Checked)
                                    ucHourly.GetValues(r);
                                else
                                    if(rbMinutely.Checked)
                                        ucMinutely.GetValues(r);
                                    else
                                        ucSecondly.GetValues(r);

            recurrence.Parse(r.ToString());
        }

        /// <summary>
        /// This is used to initialize the control with settings from an existing recurrence object
        /// </summary>
        /// <param name="recurrence">The recurrence from which to get the settings.  If null, it uses a default
        /// daily recurrence pattern.</param>
        public void SetRecurrence(Recurrence recurrence)
        {
            Recurrence r = new Recurrence();

            if(recurrence == null)
            {
                r.StartDateTime = DateTime.Today;
                r.RecurDaily(1);
            }
            else
                r.Parse(recurrence.ToStringWithStartDateTime());

            // If the given pattern is not available, set it to the next best pattern
            if(maxPattern < r.Frequency)
                switch(maxPattern)
                {
                    case RecurFrequency.Yearly:
                        r.RecurYearly(DateTime.Now.Month, DateTime.Now.Day, r.Interval);
                        break;

                    case RecurFrequency.Monthly:
                        r.RecurMonthly(DateTime.Now.Day, r.Interval);
                        break;

                    case RecurFrequency.Weekly:
                        r.RecurWeekly(r.Interval, DateUtils.ToDaysOfWeek(DateTime.Now.DayOfWeek));
                        break;

                    case RecurFrequency.Daily:
                        r.RecurDaily(r.Interval);
                        break;

                    case RecurFrequency.Hourly:
                        r.RecurDaily(r.Interval);
                        r.Frequency = RecurFrequency.Hourly;
                        break;

                    default:
                        r.RecurDaily(r.Interval);
                        r.Frequency = RecurFrequency.Minutely;
                        break;
                }

            switch(r.Frequency)
            {
                case RecurFrequency.Yearly:
                    rbYearly.Checked = true;
                    break;

                case RecurFrequency.Monthly:
                    rbMonthly.Checked = true;
                    break;

                case RecurFrequency.Weekly:
                    rbWeekly.Checked = true;
                    break;

                case RecurFrequency.Hourly:
                    rbHourly.Checked = true;
                    break;

                case RecurFrequency.Minutely:
                    rbMinutely.Checked = true;
                    break;

                case RecurFrequency.Secondly:
                    rbSecondly.Checked = true;
                    break;

                default:    // Daily or undefined
                    rbDaily.Checked = true;
                    break;
            }

            cboWeekStartDay.SelectedValue = r.WeekStart;
            dtpEndDate.Value = DateTime.Today;
            udcOccurrences.Value = 1;

            // If not visible, this option is always on in the recurrence
            if(holidayVisible)
                chkHolidays.Checked = r.CanOccurOnHoliday;
            else
                chkHolidays.Checked = true;

            if(r.MaximumOccurrences != 0)
            {
                rbEndAfter.Checked = true;
                udcOccurrences.Value = (r.MaximumOccurrences < 1000) ? r.MaximumOccurrences : 999;
            }
            else
                if(r.RecurUntil == DateTime.MaxValue)
                    rbNeverEnds.Checked = true;
                else
                {
                    rbEndByDate.Checked = true;
                    dtpEndDate.Value = r.RecurUntil;
                }

            // Set parameters in the sub-controls.  Set them all so that when the Advanced pane is hidden or
            // shown, it keeps them consistent when first opened.
            ucYearly.SetValues(r);
            ucMonthly.SetValues(r);
            ucWeekly.SetValues(r);
            ucDaily.SetValues(r);
            ucHourly.SetValues(r);
            ucMinutely.SetValues(r);
            ucSecondly.SetValues(r);
            ucAdvanced.SetValues(r);

            if(advancedVisible)
            {
                noCopy = true;
                chkAdvanced.Checked = r.IsAdvancedPattern;
                noCopy = false;
            }
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Update the status of the controls when the pattern is changed
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void Pattern_CheckedChanged(object sender, System.EventArgs e)
        {
            RadioButton r = sender as RadioButton;

            ucAdvanced.SetFrequency(rbYearly.Checked ? RecurFrequency.Yearly :
                rbMonthly.Checked ? RecurFrequency.Monthly :
                    rbWeekly.Checked ? RecurFrequency.Weekly :
                        rbDaily.Checked ? RecurFrequency.Daily :
                            rbHourly.Checked ? RecurFrequency.Hourly :
                                rbMinutely.Checked ? RecurFrequency.Minutely : RecurFrequency.Secondly);

            ucYearly.Visible = (r == rbYearly);
            ucMonthly.Visible = (r == rbMonthly);
            ucWeekly.Visible = (r == rbWeekly);
            ucDaily.Visible = (r == rbDaily);
            ucHourly.Visible = (r == rbHourly);
            ucMinutely.Visible = (r == rbMinutely);
            ucSecondly.Visible = (r == rbSecondly);
        }

        /// <summary>
        /// Update the status of the controls when the range is changed
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void Range_CheckedChanged(object sender, System.EventArgs e)
        {
            RadioButton r = sender as RadioButton;

            udcOccurrences.Enabled = (r == rbEndAfter);
            dtpEndDate.Enabled = (r == rbEndByDate);
        }

        /// <summary>
        /// Show or hide the advanced pattern rule options
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void chkAdvanced_CheckedChanged(object sender, System.EventArgs e)
        {
            Recurrence r = new Recurrence();

            // Don't copy settings if the current context doesn't warrant it.  Just change the state of the
            // panels.
            if(noCopy)
            {
                pnlPatterns.Visible = !chkAdvanced.Checked;
                ucAdvanced.Visible = chkAdvanced.Checked;
                return;
            }

            r.Frequency = rbYearly.Checked ? RecurFrequency.Yearly :
                rbMonthly.Checked ? RecurFrequency.Monthly :
                    rbWeekly.Checked ? RecurFrequency.Weekly :
                        rbDaily.Checked ? RecurFrequency.Daily :
                            rbHourly.Checked ? RecurFrequency.Hourly :
                                rbMinutely.Checked ? RecurFrequency.Minutely : RecurFrequency.Secondly;

            // Copy settings between the advanced and simple panels based on the prior setting
            if(!chkAdvanced.Checked)
            {
                ucAdvanced.GetValues(r);

                // Set the state in all simple patterns so that the ones other than the current frequency reset
                // to their default state.
                ucYearly.SetValues(r);
                ucMonthly.SetValues(r);
                ucWeekly.SetValues(r);
                ucDaily.SetValues(r);
                ucHourly.SetValues(r);
                ucMinutely.SetValues(r);
                ucSecondly.SetValues(r);

                pnlPatterns.Visible = true;
                ucAdvanced.Visible = false;
            }
            else
            {
                if(rbYearly.Checked)
                    ucYearly.GetValues(r);
                else
                    if(rbMonthly.Checked)
                        ucMonthly.GetValues(r);
                    else
                        if(rbWeekly.Checked)
                            ucWeekly.GetValues(r);
                        else
                            if(rbDaily.Checked)
                                ucDaily.GetValues(r);
                            else
                                if(rbHourly.Checked)
                                    ucHourly.GetValues(r);
                                else
                                    if(rbMinutely.Checked)
                                        ucMinutely.GetValues(r);
                                    else
                                        ucSecondly.GetValues(r);

                ucAdvanced.SetValues(r);

                pnlPatterns.Visible = false;
                ucAdvanced.Visible = true;
            }
        }
        #endregion
    }
}
