//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : VFreeBusyDlg.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/29/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Visual C#
//
// This is used to edit a VFreeBusy object's properties
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 12/14/2004  EFW  Created the code
// 04/19/2007  EFW  Converted for use with .NET 2.0
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
    /// This is used to edit a VFreeBusy object's properties
	/// </summary>
	public partial class VFreeBusyDlg : System.Windows.Forms.Form
	{
        #region Private data members
        //=====================================================================

        private int timeZoneIdx;    // The currently selected time zone

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public VFreeBusyDlg()
		{
			InitializeComponent();

            // Set the short date/long time pattern based on the current culture
            dtpStartDate.CustomFormat = dtpEndDate.CustomFormat =
                CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " +
                CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;

            // Load the time zone combo box.  The first entry will be
            // for no time zone.
            cboTimeZone.Items.Add("No time zone");

            foreach(VTimeZone tz in VCalendar.TimeZones)
                cboTimeZone.Items.Add(tz.TimeZoneId.Value);

            cboTimeZone.SelectedIndex = 0;
		}
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// Initialize the dialog controls using the specified VFreeBusy object
        /// </summary>
        /// <param name="fb">The free/busy object from which to get the settings</param>
        public void SetValues(VFreeBusy fb)
        {
            string timeZoneId = fb.StartDateTime.TimeZoneId;

            txtUniqueId.Text = fb.UniqueId.Value;
            txtOrganizer.Text = fb.Organizer.Value;
            txtContact.Text = fb.Contact.Value;

            if(fb.StartDateTime.TimeZoneDateTime == DateTime.MinValue)
                dtpStartDate.Checked = false;
            else
            {
                dtpStartDate.Value = fb.StartDateTime.TimeZoneDateTime;
                dtpStartDate.Checked = true;
            }

            if(fb.EndDateTime.TimeZoneDateTime == DateTime.MinValue)
                dtpEndDate.Checked = false;
            else
            {
                dtpEndDate.Value = fb.EndDateTime.TimeZoneDateTime;
                dtpEndDate.Checked = true;
            }

            if(fb.Duration.DurationValue != Duration.Zero)
                txtDuration.Text = fb.Duration.DurationValue.ToString(Duration.MaxUnit.Weeks);

            txtUrl.Text = fb.Url.Value;
            txtComments.Text = fb.Comment.Value;

            // We could bind directly to the existing collections but that would modify them.  To preserve the
            // original items, we'll pass a copy of the collections instead.
            ucAttendees.BindingSource.DataSource = new AttendeePropertyCollection().CloneRange(fb.Attendees);
            ucFreeBusy.BindingSource.DataSource = new FreeBusyPropertyCollection().CloneRange(fb.FreeBusy);
            ucRequestStatus.BindingSource.DataSource = new RequestStatusPropertyCollection().CloneRange(fb.RequestStatuses);

            // We use the start date's time zone ID for the combo box.  It should represent the time zone used
            // throughout the component.
            if(timeZoneId == null)
                cboTimeZone.SelectedIndex = timeZoneIdx = 0;
            else
            {
                timeZoneIdx = cboTimeZone.Items.IndexOf(timeZoneId);

                if(timeZoneIdx != -1)
                    cboTimeZone.SelectedIndex = timeZoneIdx;
                else
                    cboTimeZone.SelectedIndex = timeZoneIdx = 0;
            }
        }

        /// <summary>
        /// Update the free/busy object with the dialog control values
        /// </summary>
        /// <param name="fb">The free/busy object in which the settings are updated</param>
        public void GetValues(VFreeBusy fb)
        {
            // The unique ID is not changed
            fb.Organizer.Value = txtOrganizer.Text;
            fb.Contact.Value = txtContact.Text;

            // We'll use the TimeZoneDateTime property on all date/time values so that they are set literally
            // rather than being converted to the time zone as would happen with the DateTimeValue property.
            if(!dtpStartDate.Checked)
                fb.StartDateTime.TimeZoneDateTime = DateTime.MinValue;
            else
            {
                fb.StartDateTime.TimeZoneDateTime = dtpStartDate.Value;
                fb.StartDateTime.ValueLocation = ValLocValue.DateTime;
            }

            if(!dtpEndDate.Checked)
                fb.EndDateTime.TimeZoneDateTime = DateTime.MinValue;
            else
            {
                fb.EndDateTime.TimeZoneDateTime = dtpEndDate.Value;
                fb.StartDateTime.ValueLocation = ValLocValue.DateTime;
            }

            fb.Duration.DurationValue = new Duration(txtDuration.Text);
            fb.Url.Value = txtUrl.Text;
            fb.Comment.Value = txtComments.Text;

            // For the collections, we'll clear the existing items and copy the modified items from the browse
            // control binding sources.
            fb.Attendees.Clear();
            fb.Attendees.CloneRange((AttendeePropertyCollection)ucAttendees.BindingSource.DataSource);

            fb.FreeBusy.Clear();
            fb.FreeBusy.CloneRange((FreeBusyPropertyCollection)ucFreeBusy.BindingSource.DataSource);

            fb.RequestStatuses.Clear();
            fb.RequestStatuses.CloneRange((RequestStatusPropertyCollection)ucRequestStatus.BindingSource.DataSource);

            // Set the time zone in the object after getting all the data.  The "Set" method will not modify the
            // date/times like the "Apply" method does.
            if(cboTimeZone.Enabled && cboTimeZone.SelectedIndex != 0)
                fb.SetTimeZone(VCalendar.TimeZones[cboTimeZone.SelectedIndex - 1]);
            else
                fb.SetTimeZone(null);
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// If set to "No time zone", the Apply button has no effect
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void cboTimeZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnApplyTZ.Enabled = (cboTimeZone.SelectedIndex != 0);
        }

        /// <summary>
        /// This is used to apply the time zone selection to all date/time values in the dialog box
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void btnApplyTZ_Click(object sender, EventArgs e)
        {
            DateTimeInstance dti;
            string sourceTZ, destTZ;

            if(cboTimeZone.SelectedIndex == timeZoneIdx)
            {
                MessageBox.Show("The time zone has not changed", "No Change", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            if(MessageBox.Show(String.Format("Do you want to convert all times from the '{0}' time zone to " +
              "the '{1}' time zone?", cboTimeZone.Items[timeZoneIdx], cboTimeZone.Items[cboTimeZone.SelectedIndex]),
              "Change Time Zone", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            // Get the time zone IDs
            if(timeZoneIdx == 0)
                sourceTZ = null;
            else
                sourceTZ = (string)cboTimeZone.Items[timeZoneIdx];

            destTZ = (string)cboTimeZone.Items[cboTimeZone.SelectedIndex];

            // Convert the times
            if(sourceTZ == null)
            {
                if(dtpStartDate.Checked)
                {
                    dti = VCalendar.LocalTimeToTimeZoneTime(dtpStartDate.Value, destTZ);
                    dtpStartDate.Value = dti.StartDateTime;
                }

                if(dtpEndDate.Checked)
                {
                    dti = VCalendar.LocalTimeToTimeZoneTime(dtpEndDate.Value, destTZ);
                    dtpEndDate.Value = dti.StartDateTime;
                }
            }
            else
            {
                if(dtpStartDate.Checked)
                {
                    dti = VCalendar.TimeZoneToTimeZone(dtpStartDate.Value, sourceTZ, destTZ);
                    dtpStartDate.Value = dti.StartDateTime;
                }

                if(dtpEndDate.Checked)
                {
                    dti = VCalendar.TimeZoneToTimeZone(dtpEndDate.Value, sourceTZ, destTZ);
                    dtpEndDate.Value = dti.StartDateTime;
                }
            }

            ucFreeBusy.ApplyTimeZone(sourceTZ, destTZ);

            timeZoneIdx = cboTimeZone.SelectedIndex;
        }

        /// <summary>
        /// Validate the information on exit if OK was clicked
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void VFreeBusyDlg_Closing(object sender, CancelEventArgs e)
        {
            Duration d;

            // Ignore on cancel
            if(this.DialogResult == DialogResult.Cancel)
                return;

            epErrors.Clear();

            if(txtDuration.Text.Length != 0 && !Duration.TryParse(txtDuration.Text, out d))
            {
                epErrors.SetError(txtDuration, "Invalid duration value");
                e.Cancel = true;
                tabInfo.SelectedTab = pgGeneral;
                txtDuration.Focus();
            }

            if(dtpStartDate.Checked && dtpEndDate.Checked && dtpStartDate.Value > dtpEndDate.Value)
            {
                epErrors.SetError(dtpStartDate, "Start date must be less than or equal to end date");
                e.Cancel = true;
                tabInfo.SelectedTab = pgGeneral;
                dtpStartDate.Focus();
            }

            if(!ucRequestStatus.ValidateItems())
            {
                tabInfo.SelectedTab = pgReqStats;
                ucRequestStatus.Focus();
                e.Cancel = true;
            }
            else
                if(!ucFreeBusy.ValidateItems())
                {
                    tabInfo.SelectedTab = pgFreeBusy;
                    ucFreeBusy.Focus();
                    e.Cancel = true;
                }
        }
        #endregion
	}
}
