//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : CalendarObjectDlg.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/05/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This is used to edit a calendar object's properties (VEvent, VToDo, or a VJournal component)
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 12/17/2004  EFW  Created the code
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
	/// This is used to edit a calendar object's properties (VEvent, VToDo, or a VJournal component)
	/// </summary>
	public partial class CalendarObjectDlg : System.Windows.Forms.Form
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
		public CalendarObjectDlg()
		{
			InitializeComponent();

            // Set the short date/long time pattern based on the current culture
            dtpStartDate.CustomFormat = dtpEndDate.CustomFormat = dtpCompleted.CustomFormat =
                CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " +
                CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;

            // Load the time zone combo box.  The first entry will be for no time zone
            cboTimeZone.Items.Add("No time zone");

            foreach(VTimeZone tz in VCalendar.TimeZones)
                cboTimeZone.Items.Add(tz.TimeZoneId.Value!);

            cboTimeZone.SelectedIndex = 0;
		}
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// Initialize the dialog controls using the specified object
        /// </summary>
        /// <param name="oCal">The calendar object from which to get the settings</param>
        public void SetValues(CalendarObject oCal)
        {
            string? timeZoneId = null;
            bool isICalendar = (oCal?.Version == SpecificationVersions.iCalendar20);

            // Disable controls that aren't relevant to the vCalendar spec
            txtDuration.Enabled = txtOrganizer.Enabled = ucRequestStatus.Enabled = txtLatitude.Enabled =
                txtLongitude.Enabled = btnFind.Enabled = cboTimeZone.Enabled = btnApplyTZ.Enabled = isICalendar;

            if(oCal is VEvent e)
            {
                this.Text = "Event Properties";

                lblCompleted.Visible = dtpCompleted.Visible = lblPercent.Visible = udcPercent.Visible = false;
                lblEndDate.Text = "End";

                // Header
                txtUniqueId.Text = e.UniqueId.Value;
                chkTransparent.Checked = e.Transparency.IsTransparent;
                txtCreated.Text = e.DateCreated.TimeZoneDateTime.ToString("G", CultureInfo.CurrentCulture);
                txtLastModified.Text = e.LastModified.TimeZoneDateTime.ToString("G", CultureInfo.CurrentCulture);
                txtClass.Text = e.Classification.Value;
                udcSequence.Value = e.Sequence.SequenceNumber;
                udcPriority.Value = e.Priority.PriorityValue;

                timeZoneId = e.StartDateTime.TimeZoneId;

                // General
                if(e.StartDateTime.TimeZoneDateTime == DateTime.MinValue)
                    dtpStartDate.Checked = false;
                else
                {
                    dtpStartDate.Value = e.StartDateTime.TimeZoneDateTime;
                    dtpStartDate.Checked = true;
                }

                if(e.EndDateTime.TimeZoneDateTime == DateTime.MinValue)
                    dtpEndDate.Checked = false;
                else
                {
                    dtpEndDate.Value = e.EndDateTime.TimeZoneDateTime;
                    dtpEndDate.Checked = true;
                }

                if(e.Duration.DurationValue != Duration.Zero)
                    txtDuration.Text = e.Duration.DurationValue.ToString(Duration.MaxUnit.Weeks);

                txtSummary.Text = e.Summary.Value;
                txtLocation.Text = e.Location.Value;
                txtDescription.Text = e.Description.Value;

                // Load status values and set status
                cboStatus.Items.AddRange(["None", "Tentative", "Confirmed", "Cancelled"]);

                if(!cboStatus.Items.Contains(e.Status.StatusValue.ToString()))
                    cboStatus.Items.Add(e.Status.StatusValue.ToString());

                cboStatus.SelectedIndex = cboStatus.Items.IndexOf(e.Status.StatusValue.ToString());

                // We'll edit categories and resources as comma separated strings
                txtCategories.Text = e.Categories.CategoriesString;
                txtResources.Text = e.Resources.ResourcesString;

                txtOrganizer.Text = e.Organizer.Value;

                // We could bind directly to the existing collections but that would modify them.  To preserve
                // the original items, we'll pass a copy of the collections instead.

                // Attendees
                ucAttendees.BindingSource.DataSource = new AttendeePropertyCollection().CloneRange(e.Attendees);

                // Recurrences and exceptions
                ucRecurrences.SetValues(e.RecurrenceRules, e.RecurDates);
                ucExceptions.SetValues(e.ExceptionRules, e.ExceptionDates);

                // Set attachments
                ucAttachments.SetValues(e.Attachments);

                // Set alarms
                ucAlarms.BindingSource.DataSource = new VAlarmCollection().CloneRange(e.Alarms);

                // Miscellaneous
                txtLatitude.Text = e.GeographicPosition.Latitude.ToString(CultureInfo.CurrentCulture);
                txtLongitude.Text = e.GeographicPosition.Longitude.ToString(CultureInfo.CurrentCulture);

                ucRequestStatus.BindingSource.DataSource = new RequestStatusPropertyCollection().CloneRange(e.RequestStatuses);

                txtUrl.Text = e.Url.Value;
                txtComments.Text = e.Comment.Value;
            }
            else if(oCal is VToDo td)
            {
                this.Text = "To-Do Properties";

                chkTransparent.Visible = lblLocation.Visible = txtLocation.Visible = false;

                lblCompleted.Visible = dtpCompleted.Visible = lblPercent.Visible = udcPercent.Visible = true;
                lblEndDate.Text = "Due";

                // Header
                txtUniqueId.Text = td.UniqueId.Value;
                txtCreated.Text = td.DateCreated.TimeZoneDateTime.ToString("G", CultureInfo.CurrentCulture);
                txtLastModified.Text = td.LastModified.TimeZoneDateTime.ToString("G", CultureInfo.CurrentCulture);
                txtClass.Text = td.Classification.Value;
                udcSequence.Value = td.Sequence.SequenceNumber;
                udcPriority.Value = td.Priority.PriorityValue;

                timeZoneId = td.StartDateTime.TimeZoneId;

                // General
                if(td.StartDateTime.TimeZoneDateTime == DateTime.MinValue)
                    dtpStartDate.Checked = false;
                else
                {
                    dtpStartDate.Value = td.StartDateTime.TimeZoneDateTime;
                    dtpStartDate.Checked = true;
                }

                // We'll reuse End Date for Due Date
                if(td.DueDateTime.TimeZoneDateTime == DateTime.MinValue)
                    dtpEndDate.Checked = false;
                else
                {
                    dtpEndDate.Value = td.DueDateTime.TimeZoneDateTime;
                    dtpEndDate.Checked = true;
                }

                if(td.CompletedDateTime.TimeZoneDateTime == DateTime.MinValue)
                    dtpCompleted.Checked = false;
                else
                {
                    dtpCompleted.Value = td.CompletedDateTime.TimeZoneDateTime;
                    dtpCompleted.Checked = true;
                }

                if(td.Duration.DurationValue != Duration.Zero)
                    txtDuration.Text = td.Duration.DurationValue.
                        ToString(Duration.MaxUnit.Weeks);

                udcPercent.Value = td.PercentComplete.Percentage;
                txtSummary.Text = td.Summary.Value;
                txtDescription.Text = td.Description.Value;

                // Load status values and set status
                cboStatus.Items.AddRange(["None", "NeedsAction", "Completed", "InProcess", "Cancelled"]);

                if(!cboStatus.Items.Contains(td.Status.StatusValue.ToString()))
                    cboStatus.Items.Add(td.Status.StatusValue.ToString());

                cboStatus.SelectedIndex = cboStatus.Items.IndexOf(td.Status.StatusValue.ToString());

                // We'll edit categories and resources as comma separated strings
                txtCategories.Text = td.Categories.CategoriesString;
                txtResources.Text = td.Resources.ResourcesString;

                txtOrganizer.Text = td.Organizer.Value;

                // We could bind directly to the existing collections but that would modify them.  To preserve
                // the original items, we'll pass a copy of the collections instead.

                // Attendees
                ucAttendees.BindingSource.DataSource = new AttendeePropertyCollection().CloneRange(td.Attendees);

                // Recurrences and exceptions
                ucRecurrences.SetValues(td.RecurrenceRules, td.RecurDates);
                ucExceptions.SetValues(td.ExceptionRules, td.ExceptionDates);

                // Set attachments
                ucAttachments.SetValues(td.Attachments);

                // Set alarms
                ucAlarms.BindingSource.DataSource = new VAlarmCollection().CloneRange(td.Alarms);

                // Miscellaneous
                txtLatitude.Text = td.GeographicPosition.Latitude.ToString(CultureInfo.CurrentCulture);
                txtLongitude.Text = td.GeographicPosition.Longitude.ToString(CultureInfo.CurrentCulture);

                ucRequestStatus.BindingSource.DataSource = new RequestStatusPropertyCollection().CloneRange(td.RequestStatuses);

                txtUrl.Text = td.Url.Value;
                txtComments.Text = td.Comment.Value;
            }
            else if(oCal is VJournal j)
            {
                this.Text = "Journal Properties";

                chkTransparent.Visible = lblPriority.Visible = udcPriority.Visible = lblEndDate.Visible =
                    dtpEndDate.Visible = lblDuration.Visible = txtDuration.Visible = lblLocation.Visible =
                    txtLocation.Visible = lblResources.Visible = txtResources.Visible = lblLatitude.Visible =
                    txtLatitude.Visible = lblLongitude.Visible = txtLongitude.Visible = btnFind.Visible =
                    lblCompleted.Visible = dtpCompleted.Visible = lblPercent.Visible = udcPercent.Visible = false;

                tabInfo.TabPages.Remove(pgAlarms);
                tabInfo.SelectedTab = pgGeneral;

                // Header
                txtUniqueId.Text = j.UniqueId.Value;
                txtCreated.Text = j.DateCreated.TimeZoneDateTime.ToString("G", CultureInfo.CurrentCulture);
                txtLastModified.Text = j.LastModified.TimeZoneDateTime.ToString("G", CultureInfo.CurrentCulture);
                txtClass.Text = j.Classification.Value;
                udcSequence.Value = j.Sequence.SequenceNumber;

                timeZoneId = j.StartDateTime.TimeZoneId;

                // General
                if(j.StartDateTime.TimeZoneDateTime == DateTime.MinValue)
                    dtpStartDate.Checked = false;
                else
                {
                    dtpStartDate.Value = j.StartDateTime.TimeZoneDateTime;
                    dtpStartDate.Checked = true;
                }

                txtSummary.Text = j.Summary.Value;
                txtDescription.Text = j.Description.Value;

                // Load status values and set status
                cboStatus.Items.AddRange(["None", "Draft", "Final", "Cancelled"]);

                if(!cboStatus.Items.Contains(j.Status.StatusValue.ToString()))
                    cboStatus.Items.Add(j.Status.StatusValue.ToString());

                cboStatus.SelectedIndex = cboStatus.Items.IndexOf(j.Status.StatusValue.ToString());

                // We'll edit categories as a comma separated string
                txtCategories.Text = j.Categories.CategoriesString;

                txtOrganizer.Text = j.Organizer.Value;

                // We could bind directly to the existing collections but that would modify them.  To preserve
                // the original items, we'll pass a copy of the collections instead.

                // Attendees
                ucAttendees.BindingSource.DataSource = new AttendeePropertyCollection().CloneRange(j.Attendees);

                // Recurrences and exceptions
                ucRecurrences.SetValues(j.RecurrenceRules, j.RecurDates);
                ucExceptions.SetValues(j.ExceptionRules, j.ExceptionDates);

                // Set attachments
                ucAttachments.SetValues(j.Attachments);

                // Miscellaneous
                ucRequestStatus.BindingSource.DataSource = new RequestStatusPropertyCollection().CloneRange(j.RequestStatuses);

                txtUrl.Text = j.Url.Value;
                txtComments.Text = j.Comment.Value;
            }

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
        /// Update the calendar object with the dialog control values
        /// </summary>
        /// <param name="oCal">The calendar object in which the settings are updated</param>
        public void GetValues(CalendarObject oCal)
        {
            // We'll use the TimeZoneDateTime property on all date/time values so that they are set literally
            // rather than being converted to the time zone as would happen with the DateTimeValue property.
            if(oCal is VEvent e)
            {
                // Header.  Unique ID and Created Date are not changed
                e.Transparency.IsTransparent = chkTransparent.Checked;
                e.LastModified.TimeZoneDateTime = DateTime.Now;
                e.Classification.Value = txtClass.Text;
                e.Sequence.SequenceNumber = (int)udcSequence.Value;
                e.Priority.PriorityValue = (int)udcPriority.Value;

                // General
                if(!dtpStartDate.Checked)
                    e.StartDateTime.TimeZoneDateTime = DateTime.MinValue;
                else
                {
                    e.StartDateTime.TimeZoneDateTime = dtpStartDate.Value;
                    e.StartDateTime.ValueLocation = ValLocValue.DateTime;
                }

                if(!dtpEndDate.Checked)
                    e.EndDateTime.TimeZoneDateTime = DateTime.MinValue;
                else
                {
                    e.EndDateTime.TimeZoneDateTime = dtpEndDate.Value;
                    e.EndDateTime.ValueLocation = ValLocValue.DateTime;
                }

                e.Duration.DurationValue = new Duration(txtDuration.Text);
                e.Summary.Value = txtSummary.Text;
                e.Location.Value = txtLocation.Text;
                e.Description.Value = txtDescription.Text;

                // Get status value
                e.Status.StatusValue = (StatusValue)Enum.Parse(typeof(StatusValue),
                    cboStatus.Items[cboStatus.SelectedIndex]!.ToString()!, true);

                // We'll edit categories and resources as comma separated strings
                e.Categories.CategoriesString = txtCategories.Text;
                e.Resources.ResourcesString = txtResources.Text;

                e.Organizer.Value = txtOrganizer.Text;

                // For the collections, we'll clear the existing items and copy the modified items from the
                // browse control binding sources.

                // Attendees
                e.Attendees.Clear();
                e.Attendees.CloneRange((AttendeePropertyCollection)ucAttendees.BindingSource.DataSource);

                // Recurrences and exceptions.
                ucRecurrences.GetValues(e.RecurrenceRules, e.RecurDates);
                ucExceptions.GetValues(e.ExceptionRules, e.ExceptionDates);

                // Get attachments
                ucAttachments.GetValues(e.Attachments);

                // Get alarms
                e.Alarms.Clear();
                e.Alarms.CloneRange((VAlarmCollection)ucAlarms.BindingSource.DataSource);

                // Miscellaneous
                if(txtLatitude.Text.Length != 0 || txtLongitude.Text.Length != 0)
                {
                    e.GeographicPosition.Latitude = Convert.ToDouble(txtLatitude.Text, CultureInfo.CurrentCulture);
                    e.GeographicPosition.Longitude = Convert.ToDouble(txtLongitude.Text, CultureInfo.CurrentCulture);
                }
                else
                    e.GeographicPosition.Latitude = e.GeographicPosition.Longitude = 0.0F;

                e.RequestStatuses.Clear();
                e.RequestStatuses.CloneRange((RequestStatusPropertyCollection)ucRequestStatus.BindingSource.DataSource);

                e.Url.Value = txtUrl.Text;
                e.Comment.Value = txtComments.Text;
            }
            else if(oCal is VToDo td)
            {
                // Header.  Unique ID and Created Date are not changed
                td.LastModified.TimeZoneDateTime = DateTime.Now;
                td.Classification.Value = txtClass.Text;
                td.Sequence.SequenceNumber = (int)udcSequence.Value;
                td.Priority.PriorityValue = (int)udcPriority.Value;

                // General
                if(!dtpStartDate.Checked)
                    td.StartDateTime.TimeZoneDateTime = DateTime.MinValue;
                else
                {
                    td.StartDateTime.TimeZoneDateTime = dtpStartDate.Value;
                    td.StartDateTime.ValueLocation = ValLocValue.DateTime;
                }

                if(!dtpEndDate.Checked)
                    td.DueDateTime.TimeZoneDateTime = DateTime.MinValue;
                else
                {
                    td.DueDateTime.TimeZoneDateTime = dtpEndDate.Value;
                    td.DueDateTime.ValueLocation = ValLocValue.DateTime;
                }

                if(!dtpCompleted.Checked)
                    td.CompletedDateTime.TimeZoneDateTime = DateTime.MinValue;
                else
                {
                    td.CompletedDateTime.TimeZoneDateTime = dtpCompleted.Value;
                    td.CompletedDateTime.ValueLocation = ValLocValue.DateTime;
                }

                td.PercentComplete.Percentage = (int)udcPercent.Value;
                td.Duration.DurationValue = new Duration(txtDuration.Text);
                td.Summary.Value = txtSummary.Text;
                td.Description.Value = txtDescription.Text;

                // Get status value
                td.Status.StatusValue = (StatusValue)Enum.Parse(typeof(StatusValue),
                    cboStatus.Items[cboStatus.SelectedIndex]!.ToString()!, true);

                // We'll edit categories and resources as comma separated strings
                td.Categories.CategoriesString = txtCategories.Text;
                td.Resources.ResourcesString = txtResources.Text;

                td.Organizer.Value = txtOrganizer.Text;

                // For the collections, we'll clear the existing items and copy the modified items from the
                // browse control binding sources.

                // Attendees
                td.Attendees.Clear();
                td.Attendees.CloneRange((AttendeePropertyCollection)ucAttendees.BindingSource.DataSource);

                // Recurrences and exceptions.
                ucRecurrences.GetValues(td.RecurrenceRules, td.RecurDates);
                ucExceptions.GetValues(td.ExceptionRules, td.ExceptionDates);

                // Get attachments
                ucAttachments.GetValues(td.Attachments);

                // Get alarms
                td.Alarms.Clear();
                td.Alarms.CloneRange((VAlarmCollection)ucAlarms.BindingSource.DataSource);

                // Miscellaneous
                if(txtLatitude.Text.Length != 0 || txtLongitude.Text.Length != 0)
                {
                    td.GeographicPosition.Latitude = Convert.ToDouble(txtLatitude.Text, CultureInfo.CurrentCulture);
                    td.GeographicPosition.Longitude = Convert.ToDouble(txtLongitude.Text, CultureInfo.CurrentCulture);
                }
                else
                    td.GeographicPosition.Latitude = td.GeographicPosition.Longitude = 0.0F;

                td.RequestStatuses.Clear();
                td.RequestStatuses.CloneRange((RequestStatusPropertyCollection)ucRequestStatus.BindingSource.DataSource);

                td.Url.Value = txtUrl.Text;
                td.Comment.Value = txtComments.Text;
            }
            else if(oCal is VJournal j)
            {
                // Header.  Unique ID and Created Date are not changed
                j.LastModified.TimeZoneDateTime = DateTime.Now;
                j.Classification.Value = txtClass.Text;
                j.Sequence.SequenceNumber = (int)udcSequence.Value;

                // General
                if(!dtpStartDate.Checked)
                    j.StartDateTime.TimeZoneDateTime = DateTime.MinValue;
                else
                {
                    j.StartDateTime.TimeZoneDateTime = dtpStartDate.Value;
                    j.StartDateTime.ValueLocation = ValLocValue.DateTime;
                }

                j.Summary.Value = txtSummary.Text;
                j.Description.Value = txtDescription.Text;

                // Get status value
                j.Status.StatusValue = (StatusValue)Enum.Parse(typeof(StatusValue),
                    cboStatus.Items[cboStatus.SelectedIndex]!.ToString()!, true);

                // We'll edit categories as a comma separated string
                j.Categories.CategoriesString = txtCategories.Text;

                j.Organizer.Value = txtOrganizer.Text;

                // For the collections, we'll clear the existing items and copy the modified items from the
                // browse control binding sources.

                // Attendees
                j.Attendees.Clear();
                j.Attendees.CloneRange((AttendeePropertyCollection)ucAttendees.BindingSource.DataSource);

                // Recurrences and exceptions.
                ucRecurrences.GetValues(j.RecurrenceRules, j.RecurDates);
                ucExceptions.GetValues(j.ExceptionRules, j.ExceptionDates);

                // Get attachments
                ucAttachments.GetValues(j.Attachments);

                // Miscellaneous
                j.RequestStatuses.Clear();
                j.RequestStatuses.CloneRange((RequestStatusPropertyCollection)ucRequestStatus.BindingSource.DataSource);

                j.Url.Value = txtUrl.Text;
                j.Comment.Value = txtComments.Text;
            }

            // Set the time zone in the object after getting all the data.  The "Set" method will not modify the
            // date/times like the "Apply" method does.
            if(cboTimeZone.Enabled && cboTimeZone.SelectedIndex != 0)
                oCal?.SetTimeZone(VCalendar.TimeZones[cboTimeZone.SelectedIndex - 1]);
            else
                oCal?.SetTimeZone(null);
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
            string? sourceTZ, destTZ;

            if(cboTimeZone.SelectedIndex == timeZoneIdx)
            {
                MessageBox.Show("The time zone has not changed", "No Change", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            if(MessageBox.Show($"Do you want to convert all times from the '{cboTimeZone.Items[timeZoneIdx]}' " +
              $"time zone to the '{cboTimeZone.Items[cboTimeZone.SelectedIndex]}' time zone?", "Change Time Zone",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            // Get the time zone IDs
            if(timeZoneIdx == 0)
                sourceTZ = null;
            else
                sourceTZ = (string?)cboTimeZone.Items[timeZoneIdx];

            destTZ = (string?)cboTimeZone.Items[cboTimeZone.SelectedIndex];

            // Convert the times.  Note that the completed date is always in universal time so it isn't touched.
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

            ucRecurrences.ApplyTimeZone(sourceTZ, destTZ);
            ucExceptions.ApplyTimeZone(sourceTZ, destTZ);
            ucAlarms.ApplyTimeZone(sourceTZ, destTZ);

            timeZoneIdx = cboTimeZone.SelectedIndex;
        }

        /// <summary>
        /// View the geocoding coordinates using Google Maps if possible
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            string url = $"https://www.google.com/maps/place/{txtLatitude.Text},{txtLongitude.Text}";

            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true,
                });
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unable to start web browser", "Launch Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                // Log the exception to the debugger for the developer
                System.Diagnostics.Debug.Write(ex.ToString());
            }
        }

        /// <summary>
        /// Validate the information on exit if OK was clicked
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void CalendarObjectDlg_Closing(object sender, CancelEventArgs e)
        {
            // Ignore on cancel
            if(this.DialogResult == DialogResult.Cancel)
                return;

            epErrors.Clear();

            tabInfo.SelectedTab = pgGeneral;

            if(txtDuration.Visible && txtDuration.Text.Length != 0)
            {
                if(!Duration.TryParse(txtDuration.Text, out Duration d))
                {
                    e.Cancel = true;
                    epErrors.SetError(txtDuration, "Invalid duration value");
                    txtDuration.Focus();
                }
                else
                {
                    if(d != Duration.Zero && dtpEndDate.Visible && dtpEndDate.Checked)
                    {
                        e.Cancel = true;
                        epErrors.SetError(dtpEndDate, "A duration or an end date can be specified, but not both");
                        dtpEndDate.Focus();
                    }
                }
            }

            if(dtpEndDate.Visible && dtpStartDate.Checked && dtpEndDate.Checked && dtpStartDate.Value > dtpEndDate.Value)
            {
                epErrors.SetError(dtpStartDate, "Start date must be less than or equal to end date");
                e.Cancel = true;
                dtpStartDate.Focus();
            }

            if(txtLatitude.Enabled)
            {
                if(txtLatitude.Text.Length != 0)
                {
                    if(!Double.TryParse(txtLatitude.Text, out double latitude) || latitude < -90.0 || latitude > 90.0)
                    {
                        e.Cancel = true;
                        epErrors.SetError(txtLatitude, "Latitude must be a valid numeric value between -90 and 90");
                        tabInfo.SelectedTab = pgMisc;
                        txtLatitude.Focus();
                    }
                }

                if(txtLongitude.Text.Length != 0)
                {
                    if(!Double.TryParse(txtLongitude.Text, out double longitude) || longitude < -180.0 || longitude > 180.0)
                    {
                        e.Cancel = true;
                        epErrors.SetError(txtLongitude, "Longitude must be a valid numeric value between -180 and 180");
                        tabInfo.SelectedTab = pgMisc;
                        txtLongitude.Focus();
                    }
                }
            }

            if(ucRequestStatus.Enabled && !ucRequestStatus.ValidateItems())
            {
                tabInfo.SelectedTab = pgMisc;
                ucRequestStatus.Focus();
                e.Cancel = true;
            }

            if(ucAlarms.Enabled && !ucAlarms.ValidateItems())
            {
                tabInfo.SelectedTab = pgAlarms;
                ucAlarms.Focus();
                e.Cancel = true;
            }
        }
        #endregion
	}
}
