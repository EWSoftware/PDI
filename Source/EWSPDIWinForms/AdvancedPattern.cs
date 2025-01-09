//===============================================================================================================
// System  : EWSoftware.PDI Windows Forms Controls
// File    : AdvancedPattern.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/02/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains one of several user controls that are combined to allow the editing of various recurrence
// parameters.  This one is used to specify the settings for an advanced pattern that uses a more complex
// combination of the various rule parts that cannot easily be represented by the simple frequency patterns
// created using the other frequency type user controls.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 11/07/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Globalization;

namespace EWSoftware.PDI.Windows.Forms
{
	/// <summary>
	/// This user control is used to specify the settings for an advanced pattern that uses a more complex
    /// combination of the various rule parts that cannot easily be represented by the simple frequency patterns
    /// created using the other frequency type user controls.
	/// </summary>
    [ToolboxItem(false)]
	internal sealed partial class AdvancedPattern : System.Windows.Forms.UserControl
	{
        #region Private data members
        //=====================================================================

        // Current recurrence frequency
        private RecurFrequency rf;

        // Day instances for the ByDay option with and without instances
        private readonly DayInstanceCollection dicWithInst, dicDayOnly;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
		/// Constructor
		/// </summary>
        public AdvancedPattern()
		{
            dicWithInst = [];
            dicDayOnly = [];

			InitializeComponent();

            lbByMonth.DisplayMember = "Display";
            lbByMonth.ValueMember = "Value";
            lbByMonth.DataSource = RecurOptsDataSource.MonthsOfYear;

            cboDOW.DisplayMember = "Display";
            cboDOW.ValueMember = "Value";
            cboDOW.DataSource = RecurOptsDataSource.DayOfWeek;
		}
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// This is used to load the By Day list box with values from the ByDay collection
        /// </summary>
        private void LoadByDayValues()
        {
            int selIdx = lbByDay.SelectedIndex;

            lbByDay.Items.Clear();

            // The weekly frequency only uses the day, not the instance
            if(udcDayInstance.Enabled)
            {
                foreach(DayInstance di in dicWithInst)
                    lbByDay.Items.Add(di.ToDescription());
            }
            else
            {
                string[] dayNames = CultureInfo.CurrentCulture.DateTimeFormat.DayNames;

                foreach(DayInstance di in dicDayOnly)
                    lbByDay.Items.Add(dayNames[(int)di.DayOfWeek]);
            }

            if(lbByDay.Items.Count > 0)
            {
                btnRemove.Enabled = btnClear.Enabled = true;
                lbByDay.SelectedIndex = (selIdx < lbByDay.Items.Count) ? selIdx : lbByDay.Items.Count - 1;
            }
            else
                btnRemove.Enabled = btnClear.Enabled = false;
        }

        /// <summary>
        /// This is used by the recurrence pattern control to update the current frequency when the radio buttons
        /// selection changes.
        /// </summary>
        /// <param name="frequency">The new frequency</param>
        public void SetFrequency(RecurFrequency frequency)
        {
            rf = frequency;

            // Set the interval label and enable or disable the ByDay instance control based on the frequency
            switch(rf)
            {
                case RecurFrequency.Yearly:
                    // Day instance is disabled if ByMonth and ByMonthDay are use together or if ByWeekNo is used
                    if(lbByMonth.CheckedItems.Count > 0)
                        udcDayInstance.Enabled = (txtByMonthDay.Text.Length == 0);
                    else
                        udcDayInstance.Enabled = (txtByWeekNo.Text.Length == 0);

                    lblInterval.Text = LR.GetString("APYearly");
                    break;

                case RecurFrequency.Monthly:
                    // Day instance is disabled if ByMonthDay is specified
                    udcDayInstance.Enabled = (txtByMonthDay.Text.Length == 0);
                    lblInterval.Text = LR.GetString("APMonthly");
                    break;

                case RecurFrequency.Weekly:
                    udcDayInstance.Enabled = false;
                    lblInterval.Text = LR.GetString("APWeekly");
                    break;

                case RecurFrequency.Daily:
                    udcDayInstance.Enabled = false;
                    lblInterval.Text = LR.GetString("APDaily");
                    break;

                case RecurFrequency.Hourly:
                    udcDayInstance.Enabled = false;
                    lblInterval.Text = LR.GetString("APHourly");
                    break;

                case RecurFrequency.Minutely:
                    udcDayInstance.Enabled = false;
                    lblInterval.Text = LR.GetString("APMinutely");
                    break;

                case RecurFrequency.Secondly:
                    udcDayInstance.Enabled = false;
                    lblInterval.Text = LR.GetString("APSecondly");
                    break;
            }

            LoadByDayValues();

            // By Week # is only used by the Yearly frequency
            txtByWeekNo.Enabled = (rf == RecurFrequency.Yearly);
        }

        /// <summary>
        /// This is called to get the values from the controls and set them in the specified recurrence object
        /// </summary>
        /// <param name="recurrence">The recurrence object to which the settings are applied</param>
        /// <remarks>It is assumed that the recurrence object has been reset to a default state</remarks>
        public void GetValues(Recurrence recurrence)
        {
            recurrence.Frequency = rf;
            recurrence.Interval = (int)udcInterval.Value;

            for(int month = 1; month < 13; month++)
                if(lbByMonth.GetItemChecked(month - 1))
                    recurrence.ByMonth.Add(month);

            if(udcDayInstance.Enabled)
                recurrence.ByDay.AddRange(dicWithInst);
            else
                recurrence.ByDay.AddRange(dicDayOnly);

            if(recurrence.Frequency == RecurFrequency.Yearly)
                recurrence.ByWeekNo.ParseValues(txtByWeekNo.Text);

            recurrence.ByYearDay.ParseValues(txtByYearDay.Text);
            recurrence.ByMonthDay.ParseValues(txtByMonthDay.Text);
            recurrence.ByHour.ParseValues(txtByHour.Text);
            recurrence.ByMinute.ParseValues(txtByMinute.Text);
            recurrence.BySecond.ParseValues(txtBySecond.Text);
            recurrence.BySetPos.ParseValues(txtBySetPos.Text);
        }

        /// <summary>
        /// This is called to set the values for the controls based on the current recurrence settings
        /// </summary>
        /// <param name="recurrence">The recurrence object from which to get the settings</param>
        public void SetValues(Recurrence recurrence)
        {
            this.SetFrequency(recurrence.Frequency);
            udcInterval.Value = (recurrence.Interval < 1000) ? recurrence.Interval : 999;

            for(int month = 1; month < 13; month++)
                lbByMonth.SetItemChecked(month - 1, recurrence.ByMonth.Contains(month));

            dicWithInst.Clear();
            dicDayOnly.Clear();

            foreach(DayInstance di in recurrence.ByDay)
            {
                dicWithInst.Add(di);
                dicDayOnly.Add(di.DayOfWeek);
            }

            if(recurrence.Frequency == RecurFrequency.Yearly)
                txtByWeekNo.Text = recurrence.ByWeekNo.ToString();

            txtByYearDay.Text = recurrence.ByYearDay.ToString();
            txtByMonthDay.Text = recurrence.ByMonthDay.ToString();
            txtByHour.Text = recurrence.ByHour.ToString();
            txtByMinute.Text = recurrence.ByMinute.ToString();
            txtBySecond.Text = recurrence.BySecond.ToString();
            txtBySetPos.Text = recurrence.BySetPos.ToString();

            LoadByDayValues();
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Add a ByDay entry
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(udcDayInstance.Enabled)
                dicWithInst.Add((int)udcDayInstance.Value, (DayOfWeek)cboDOW.SelectedValue!);

            dicDayOnly.Add((DayOfWeek)cboDOW.SelectedValue!);

            LoadByDayValues();
        }

        /// <summary>
        /// Remove a ByDay entry
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if(lbByDay.SelectedIndex == -1)
                lbByDay.SelectedIndex = 0;
            else
            {
                if(udcDayInstance.Enabled)
                    dicWithInst.RemoveAt(lbByDay.SelectedIndex);
                else
                    dicDayOnly.RemoveAt(lbByDay.SelectedIndex);
            }

            LoadByDayValues();
        }

        /// <summary>
        /// Clear all ByDay entries
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            dicWithInst.Clear();
            dicDayOnly.Clear();
            lbByDay.Items.Clear();
        }

        /// <summary>
        /// Update the state of the ByDay instance control when the ByMonth, ByMonthDay, or ByWeekNo rules change
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void SetDayInstanceEnabledState(object sender, EventArgs e)
        {
            bool newState = udcDayInstance.Enabled;

            if(rf == RecurFrequency.Yearly)
            {
                if(lbByMonth.CheckedItems.Count > 0)
                    newState = (txtByMonthDay.Text.Length == 0);
                else
                    newState = (txtByWeekNo.Text.Length == 0);
            }
            else
            {
                if(rf == RecurFrequency.Monthly)
                    newState = (txtByMonthDay.Text.Length == 0);
            }

            if(udcDayInstance.Enabled != newState)
            {
                udcDayInstance.Enabled = newState;
                LoadByDayValues();
            }
        }
        #endregion
    }
}
