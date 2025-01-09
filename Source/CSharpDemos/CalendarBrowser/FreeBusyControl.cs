//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : FreeBusyControl.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/05/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This is used to edit a free/busy object's free/busy collection
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 12/14/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

using EWSoftware.PDI;
using EWSoftware.PDI.Objects;
using EWSoftware.PDI.Properties;

namespace CalendarBrowser
{
	/// <summary>
	/// This is used to edit the Free/Busy property collection
	/// </summary>
	public partial class FreeBusyControl : EWSoftware.PDI.Windows.Forms.BrowseControl
	{
        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public FreeBusyControl()
		{
			InitializeComponent();

            cboBusyType.SelectedIndex = 0;

            // Set the short date/long time pattern based on the current culture
            dtpStartDate.CustomFormat = dtpEndDate.CustomFormat =
                CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " +
                CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;

            // Use a default collection as the data source
            this.BindingSource.DataSource = new FreeBusyPropertyCollection();
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
            cboBusyType.Enabled = txtOtherType.Enabled = dtpStartDate.Enabled = dtpEndDate.Enabled = enable;

            if(enable)
                cboBusyType.Focus();
        }

        /// <summary>
        /// This is overridden to bind the controls to the data source
        /// </summary>
        public override void BindToControls()
        {
            txtOtherType.DataBindings.Add("Text", this.BindingSource, "OtherType");

            // The binding events translate between the index value and the free/busy type
            Binding b = new("SelectedIndex", this.BindingSource, "FreeBusyType");
            b.Format += FreeBusyType_Format;
            b.Parse += FreeBusyType_Parse;
            cboBusyType.DataBindings.Add(b);

            // This binding events will take care of values that haven't been set in the date/time controls
            b = new("Value", this.BindingSource, "PeriodValue_StartDateTime");
            b.Format += DateTime_Format;
            b.Parse += DateTime_Parse;
            dtpStartDate.DataBindings.Add(b);

            b = new("Value", this.BindingSource, "PeriodValue_EndDateTime");
            b.Format += DateTime_Format;
            b.Parse += DateTime_Parse;
            dtpEndDate.DataBindings.Add(b);
        }
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// Validate the items in the collection
        /// </summary>
        /// <returns>Returns true if all are valid or false if any item is not valid.  The position is set to the
        /// first invalid item.</returns>
        /// <remarks>The navigator doesn't allow for validation of all fields so we have to do some checking
        /// before saving the results.</remarks>
        public bool ValidateItems()
        {
            this.ErrorProvider.Clear();

            FreeBusyPropertyCollection freebusys = (FreeBusyPropertyCollection)this.BindingSource.DataSource;

            foreach(FreeBusyProperty fb in freebusys)
            {
                if(fb.FreeBusyType == FreeBusyType.Other && fb.OtherType!.Trim().Length == 0)
                {
                    this.BindingSource.Position = freebusys.IndexOf(fb);
                    txtOtherType.Focus();
                    this.ErrorProvider.SetError(txtOtherType, "When type is set to 'OTHER', a description " +
                        "is required");
                    return false;
                }

                if(fb.PeriodValue.StartDateTime == DateTime.MinValue)
                {
                    this.BindingSource.Position = freebusys.IndexOf(fb);
                    dtpStartDate.Focus();
                    this.ErrorProvider.SetError(dtpStartDate, "A start date is required");
                    return false;
                }

                if(fb.PeriodValue.EndDateTime == DateTime.MinValue)
                {
                    this.BindingSource.Position = freebusys.IndexOf(fb);
                    dtpEndDate.Focus();
                    this.ErrorProvider.SetError(dtpEndDate, "An end date is required");
                    return false;
                }

                if(fb.PeriodValue.StartDateTime > fb.PeriodValue.EndDateTime)
                {
                    this.BindingSource.Position = freebusys.IndexOf(fb);
                    dtpStartDate.Focus();
                    this.ErrorProvider.SetError(dtpStartDate, "Start date must be less than or equal to end date");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// This is called by the containing form to apply a new time zone to the date/time values in the control
        /// </summary>
        /// <param name="oldTZ">The old time zone's ID</param>
        /// <param name="newTZ">The new time zone's ID</param>
        public void ApplyTimeZone(string? oldTZ, string? newTZ)
        {
            DateTimeInstance dti;

            if(oldTZ == null)
            {
                if(dtpStartDate.Checked)
                {
                    dti = VCalendar.LocalTimeToTimeZoneTime(dtpStartDate.Value, newTZ);
                    dtpStartDate.Value = dti.StartDateTime;
                }

                if(dtpEndDate.Checked)
                {
                    dti = VCalendar.LocalTimeToTimeZoneTime(dtpEndDate.Value, newTZ);
                    dtpEndDate.Value = dti.StartDateTime;
                }
            }
            else
            {
                if(dtpStartDate.Checked)
                {
                    dti = VCalendar.TimeZoneToTimeZone(dtpStartDate.Value, oldTZ, newTZ);
                    dtpStartDate.Value = dti.StartDateTime;
                }

                if(dtpEndDate.Checked)
                {
                    dti = VCalendar.TimeZoneToTimeZone(dtpEndDate.Value, oldTZ, newTZ);
                    dtpEndDate.Value = dti.StartDateTime;
                }
            }
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// This is used to translate between the free/busy type and the index
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void FreeBusyType_Format(object? sender, ConvertEventArgs e)
        {
            e.Value = (int)e.Value!;
        }

        /// <summary>
        /// This is used to translate between the free/busy type and the index
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void FreeBusyType_Parse(object? sender, ConvertEventArgs e)
        {
            e.Value = (FreeBusyType)e.Value!;
        }

        /// <summary>
        /// Get the date/time control based on the property value
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void DateTime_Format(object? sender, ConvertEventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)(((Binding)sender!).Control);
            DateTime date = (DateTime)e.Value!;

            if(date == DateTime.MinValue)
            {
                dtp.Checked = false;
                e.Value = DateTime.Today;
            }
            else
                dtp.Checked = true;
        }

        /// <summary>
        /// Set the date/time control based on the property value
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        void DateTime_Parse(object? sender, ConvertEventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)(((Binding)sender!).Control);

            if(!dtp.Checked)
                e.Value = DateTime.MinValue;
        }

        /// <summary>
        /// A free/busy type other than None is required
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters.</param>
        private void cboBusyType_Validating(object sender, CancelEventArgs e)
        {
            this.ErrorProvider.Clear();

            if(!this.DesignMode && ((Control)sender).Enabled && cboBusyType.SelectedIndex == 0)
            {
                this.ErrorProvider.SetError(cboBusyType, "A busy type other than None is required");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Enable the Other Type text box if Other is selected as the free/busy type
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters.</param>
        private void cboBusyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboBusyType.SelectedIndex == (int)FreeBusyType.Other)
                txtOtherType.Enabled = true;
            else
            {
                txtOtherType.Enabled = false;
                txtOtherType.Text = null;
            }
        }
        #endregion
    }
}
