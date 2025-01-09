//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : ObservanceRuleControl.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/05/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This is used to edit a VTimeZone object's observance rule collection
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 12/09/2004  EFW  Created the code
// 04/09/2007  EFW  Updated for use with .NET 2.0
//===============================================================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

using EWSoftware.PDI;
using EWSoftware.PDI.Objects;

namespace CalendarBrowser
{
	/// <summary>
	/// This is used to edit the Observance Rule property collection
	/// </summary>
	public partial class ObservanceRuleControl : EWSoftware.PDI.Windows.Forms.BrowseControl
	{
        #region Private data members
        //=====================================================================

        private ObservanceRule? currentRule;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public ObservanceRuleControl()
		{
			InitializeComponent();

            List<ListItem> rules =
            [
                new(ObservanceRuleType.Standard, "Standard"),
                new(ObservanceRuleType.Daylight, "Daylight")
            ];

            cboRuleType.ValueMember = "Value";
            cboRuleType.DisplayMember = "Display";
            cboRuleType.DataSource = rules;

            // Set the short date/long time pattern based on the current culture
            dtpStartDate.CustomFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " +
                CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;

            this.BindingSource.PositionChanged += BindingSource_PositionChanged;

            // Use a default collection as the data source
            this.BindingSource.DataSource = new ObservanceRuleCollection();
        }
        #endregion

        #region Method overrides
        //=====================================================================

        /// <summary>
        /// Enable or disable the controls based on whether or not there are items in the collection
        /// </summary>
        /// <param name="enable">True to enable the controls, false to disable them</param>
        public override void EnableControls(bool enable)
        {
            tabTimeZone.Enabled = enable;

            if(enable)
                cboRuleType.Focus();
        }

        /// <summary>
        /// This is overridden to bind the controls to the data source
        /// </summary>
        /// <remarks>In this case, we can bind to a few of the simple properties but the rest will be handled
        /// manually when the position changes.
        /// </remarks>
        public override void BindToControls()
        {
            cboRuleType.DataBindings.Add("SelectedValue", this.BindingSource, "RuleType");
            txtComment.DataBindings.Add("Text", this.BindingSource, "Comment_Value");

            // We'll use the Format event to set a valid default date
            Binding b = new("Value", this.BindingSource, "StartDateTime_TimeZoneDateTime");
            b.Format += StartDate_Format;

            dtpStartDate.DataBindings.Add(b);
        }
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// Store changes to the unbound controls
        /// </summary>
        private void StoreChanges()
        {
            int hours, minutes;

            if(currentRule == null)
                return;

            // We'll only edit the first time zone name
            currentRule.TimeZoneNames[0].Value = txtTZName.Text;

            hours = (int)udcFromHours.Value;
            minutes = (int)udcFromMinutes.Value;

            if(hours < 0 || minutes < 0)
            {
                if(hours < 0)
                    hours *= -1;

                if(minutes < 0)
                    minutes *= -1;

                currentRule.OffsetFrom.TimeSpanValue = new TimeSpan(hours, minutes, 0).Negate();
            }
            else
                currentRule.OffsetFrom.TimeSpanValue = new TimeSpan(hours, minutes, 0);

            hours = (int)udcToHours.Value;
            minutes = (int)udcToMinutes.Value;

            if(hours < 0 || minutes < 0)
            {
                if(hours < 0)
                    hours *= -1;

                if(minutes < 0)
                    minutes *= -1;

                currentRule.OffsetTo.TimeSpanValue = new TimeSpan(hours, minutes, 0).Negate();
            }
            else
                currentRule.OffsetTo.TimeSpanValue = new TimeSpan(hours, minutes, 0);

            rcRulesDates.GetValues(currentRule.RecurrenceRules, currentRule.RecurDates);
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// This is used to set the start date to a valid value if it's outside the acceptable range
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void StartDate_Format(object? sender, ConvertEventArgs e)
        {
            DateTime date = (DateTime)e.Value!;

            if(date < dtpStartDate.MinDate || date > dtpStartDate.MaxDate)
                e.Value = DateTime.Today;
        }

        /// <summary>
        /// A time zone name is required
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtTZName_Validating(object sender, CancelEventArgs e)
        {
            this.ErrorProvider.Clear();

            if(!this.DesignMode && ((Control)sender).Enabled && txtTZName.Text.Trim().Length == 0)
            {
                tabTimeZone.SelectedTab = pgGeneral;
                this.ErrorProvider.SetError(txtTZName, "A time zone name is required");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Minutes must be positive if an hours value is entered
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void Minutes_Validating(object sender, CancelEventArgs e)
        {
            NumericUpDown udcHours, udcMins = (sender as NumericUpDown)!;

            this.ErrorProvider.Clear();

            if(udcMins == udcFromMinutes)
                udcHours = udcFromHours;
            else
                udcHours = udcToHours;

            if(!this.DesignMode && udcMins.Enabled && udcHours.Value != 0 && udcMins.Value < 0)
            {
                tabTimeZone.SelectedTab = pgGeneral;
                this.ErrorProvider.SetError(udcMins, "Minutes should be positive if an hour value is specified");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Update the prior row with the values from the unbound controls and load the values for the new row
        /// when the position changes.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void BindingSource_PositionChanged(object? sender, EventArgs e)
        {
            ObservanceRule newItem = (ObservanceRule)this.BindingSource.Current;
            int hours, minutes;

            // If all deleted, ignore it
            if(newItem == null)
            {
                currentRule = null;
                return;
            }

            // Save changes to the unbound controls to the prior row
            this.StoreChanges();

            // Load the new values into the unbound controls
            currentRule = newItem;

            // We'll only edit the first time zone name
            if(currentRule.TimeZoneNames.Count == 0)
                currentRule.TimeZoneNames.Add("GMT");

            txtTZName.Text = currentRule.TimeZoneNames[0].Value;

            hours = currentRule.OffsetFrom.TimeSpanValue.Hours;
            minutes = currentRule.OffsetFrom.TimeSpanValue.Minutes;

            // If hours are specified, keep minutes positive
            if(hours != 0 && minutes < 0)
                minutes *= -1;

            udcFromHours.Value = (hours < -23) ? -23 : (hours > 23) ? 23 : hours;
            udcFromMinutes.Value = (minutes < -59) ? -59 : (minutes > 59) ? 59 : minutes;

            hours = currentRule.OffsetTo.TimeSpanValue.Hours;
            minutes = currentRule.OffsetTo.TimeSpanValue.Minutes;

            // If hours are specified, keep minutes positive
            if(hours != 0 && minutes < 0)
                minutes *= -1;

            udcToHours.Value = (hours < -23) ? -23 : (hours > 23) ? 23 : hours;
            udcToMinutes.Value = (minutes < -59) ? -59 : (minutes > 59) ? 59 : minutes;

            rcRulesDates.SetValues(currentRule.RecurrenceRules, currentRule.RecurDates);
        }

        /// <summary>
        /// Store changes to the unbound controls when the control loses the focus too
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void ObservanceRuleControl_Leave(object sender, EventArgs e)
        {
            this.StoreChanges();
        }
        #endregion
    }
}
