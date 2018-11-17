//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : CalendarObjectDetails.aspx.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/01/2015
// Note    : Copyright 2004-2015, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This page is used to demonstrate the calendar classes.  Currently, it allows editing of some basic
// information.  Information in the data grids could also be edited.  Time constraints limit what I have
// implemented so far but I may expand on this at a later date.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 01/28/2005  EFW  Created the code
//===============================================================================================================

using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

using EWSoftware.PDI;
using EWSoftware.PDI.Objects;
using EWSoftware.PDI.Properties;
using EWSoftware.PDI.Web.Controls;

namespace PDIWebDemoCS
{
	/// <summary>
	/// This form is used to edit basic information in events, to-do, and journal items
	/// </summary>
	public partial class CalendarObjectDetails : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            VCalendar cal;
            int idx;
            string itemType;

            if(!Page.IsPostBack)
            {
                cal = (VCalendar)Session["VCalendar"];
                itemType = Request.QueryString["Type"];

                if(cal == null || itemType == null)
                {
                    Response.Redirect("CalendarBrowser.aspx");
                    return;
                }

                if(!Int32.TryParse(Request.QueryString["Index"], out idx))
                {
                    // If not valid just go back to the browser form
                    Response.Redirect("CalendarBrowser.aspx");
                    return;
                }

                itemType = itemType.Trim().ToUpperInvariant();
                this.ViewState["ObjectType"] = itemType;

                // Disable controls that aren't relevant to the vCalendar specification
                if(cal.Version == SpecificationVersions.vCalendar10)
                    txtDuration.Enabled = txtOrganizer.Enabled = dgReqStats.Visible = false;

                switch(itemType)
                {
                    case "EVENT":
                        // Force it to be valid
                        if(idx < 0 || idx >= cal.Events.Count)
                            idx = 0;

                        // Load the data into the controls
                        lblItemType.Text = "Event";
                        LoadEventInfo(cal.Events[idx]);
                        break;

                    case "TODO":
                        // Force it to be valid
                        if(idx < 0 || idx >= cal.ToDos.Count)
                            idx = 0;

                        // Load the data into the controls
                        lblItemType.Text = "To-Do";
                        LoadToDoInfo(cal.ToDos[idx]);
                        break;

                    case "JOURNAL":
                        // Force it to be valid
                        if(idx < 0 || idx >= cal.Journals.Count)
                            idx = 0;

                        // Load the data into the controls
                        lblItemType.Text = "Journal";
                        LoadJournalInfo(cal.Journals[idx]);
                        break;
                }

                this.ViewState["ObjectIndex"] = idx;
                Page.DataBind();
            }
		}

        /// <summary>
        /// Get the recurring object being viewed from the session state
        /// </summary>
        /// <returns>The recurring object being viewed</returns>
        private RecurringObject GetCurrentObject()
        {
            VCalendar cal = (VCalendar)Session["VCalendar"];
            int idx = (int)this.ViewState["ObjectIndex"];
            string itemType = (string)this.ViewState["ObjectType"];
            RecurringObject ro;

            switch(itemType)
            {
                case "EVENT":
                    ro = cal.Events[idx];
                    break;

                case "TODO":
                    ro = cal.ToDos[idx];
                    break;

                default:
                    ro = cal.Journals[idx];
                    break;
            }

            return ro;
        }

        /// <summary>
        /// Save changes and return to the calendar list
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool isValid = true;

            if(!Page.IsValid)
                return;

            VCalendar cal = (VCalendar)Session["VCalendar"];

            // Not very friendly, but it's just a demo
            if(cal == null)
            {
                Response.Redirect("CalendarBrowser.aspx");
                return;
            }

            int idx = (int)this.ViewState["ObjectIndex"];
            string itemType = (string)this.ViewState["ObjectType"];

            switch(itemType)
            {
                case "EVENT":
                    isValid = StoreEventInfo(cal.Events[idx]);
                    break;

                case "TODO":
                    isValid = StoreToDoInfo(cal.ToDos[idx]);
                    break;

                case "JOURNAL":
                    isValid = StoreJournalInfo(cal.Journals[idx]);
                    break;
            }

            if(isValid)
                Response.Redirect("CalendarBrowser.aspx");
        }

        /// <summary>
        /// Exit without saving
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("CalendarBrowser.aspx");
        }

        /// <summary>
        /// Load event information into the controls
        /// </summary>
        /// <param name="ev">The event to use</param>
        private void LoadEventInfo(VEvent ev)
        {
            txtCompleted.Enabled = txtPercent.Enabled = false;
            lblEndDate.Text = "End";

            lblUniqueId.Text = ev.UniqueId.Value;
            lblTimeZone.Text = ev.StartDateTime.TimeZoneId;
            chkTransparent.Checked = ev.Transparency.IsTransparent;
            txtSequence.Text = ev.Sequence.SequenceNumber.ToString();
            txtPriority.Text = ev.Priority.PriorityValue.ToString();

            // General
            if(ev.StartDateTime.TimeZoneDateTime != DateTime.MinValue)
                txtStartDate.Text = ev.StartDateTime.TimeZoneDateTime.ToString("G");

            if(ev.EndDateTime.TimeZoneDateTime != DateTime.MinValue)
                txtEndDate.Text = ev.EndDateTime.TimeZoneDateTime.ToString("G");

            if(ev.Duration.DurationValue != Duration.Zero)
                txtDuration.Text = ev.Duration.DurationValue.
                    ToString(Duration.MaxUnit.Weeks);

            txtSummary.Text = ev.Summary.Value;
            txtLocation.Text = ev.Location.Value;
            txtDescription.Text = ev.Description.Value;
            txtOrganizer.Text = ev.Organizer.Value;
            txtUrl.Text = ev.Url.Value;
            txtComments.Text = ev.Comment.Value;

            // Load status values and set status
            cboStatus.Items.Add("None");
            cboStatus.Items.Add("Tentative");
            cboStatus.Items.Add("Confirmed");
            cboStatus.Items.Add("Cancelled");

            if(cboStatus.Items.FindByValue(ev.Status.StatusValue.ToString()) == null)
                cboStatus.Items.Add(ev.Status.StatusValue.ToString());

            cboStatus.SelectedValue = ev.Status.StatusValue.ToString();

            dgAttendees.DataSource = ev.Attendees;
            dgRecurrences.DataSource = ev.RecurrenceRules;
            dgRecurDates.DataSource = ev.RecurDates;
            dgExceptions.DataSource = ev.ExceptionRules;
            dgExDates.DataSource = ev.ExceptionDates;
            dgReqStats.DataSource = ev.RequestStatuses;
        }

        /// <summary>
        /// Load To-Do information into the controls
        /// </summary>
        /// <param name="td">The To-Do to use</param>
        private void LoadToDoInfo(VToDo td)
        {
            chkTransparent.Enabled = txtLocation.Enabled = false;

            lblEndDate.Text = "Due";

            lblUniqueId.Text = td.UniqueId.Value;
            lblTimeZone.Text = td.StartDateTime.TimeZoneId;
            txtSequence.Text = td.Sequence.SequenceNumber.ToString();
            txtPriority.Text = td.Priority.PriorityValue.ToString();

            // General
            if(td.StartDateTime.TimeZoneDateTime != DateTime.MinValue)
                txtStartDate.Text = td.StartDateTime.TimeZoneDateTime.ToString("G");

            // We'll reuse End Date for Due Date
            if(td.DueDateTime.TimeZoneDateTime != DateTime.MinValue)
                txtEndDate.Text = td.DueDateTime.TimeZoneDateTime.ToString("G");

            if(td.CompletedDateTime.TimeZoneDateTime != DateTime.MinValue)
                txtCompleted.Text = td.CompletedDateTime.TimeZoneDateTime.ToString("G");

            if(td.Duration.DurationValue != Duration.Zero)
                txtDuration.Text = td.Duration.DurationValue.ToString(Duration.MaxUnit.Weeks);

            txtPercent.Text = td.PercentComplete.Percentage.ToString();
            txtSummary.Text = td.Summary.Value;
            txtDescription.Text = td.Description.Value;
            txtOrganizer.Text = td.Organizer.Value;
            txtUrl.Text = td.Url.Value;
            txtComments.Text = td.Comment.Value;

            // Load status values and set status
            cboStatus.Items.Add("None");
            cboStatus.Items.Add("NeedsAction");
            cboStatus.Items.Add("Completed");
            cboStatus.Items.Add("InProcess");
            cboStatus.Items.Add("Cancelled");

            if(cboStatus.Items.FindByValue(td.Status.StatusValue.ToString()) == null)
                cboStatus.Items.Add(td.Status.StatusValue.ToString());

            cboStatus.SelectedValue = td.Status.StatusValue.ToString();

            dgAttendees.DataSource = td.Attendees;
            dgRecurrences.DataSource = td.RecurrenceRules;
            dgRecurDates.DataSource = td.RecurDates;
            dgExceptions.DataSource = td.ExceptionRules;
            dgExDates.DataSource = td.ExceptionDates;
            dgReqStats.DataSource = td.RequestStatuses;
        }

        /// <summary>
        /// Load journal information into the controls
        /// </summary>
        /// <param name="jr">The journal to use</param>
        private void LoadJournalInfo(VJournal jr)
        {
            chkTransparent.Enabled = txtPriority.Enabled = txtEndDate.Enabled = txtDuration.Enabled =
                txtLocation.Enabled = txtCompleted.Enabled = txtPercent.Enabled = false;

            lblUniqueId.Text = jr.UniqueId.Value;
            lblTimeZone.Text = jr.StartDateTime.TimeZoneId;
            txtSequence.Text = jr.Sequence.SequenceNumber.ToString();
            txtSummary.Text = jr.Summary.Value;
            txtDescription.Text = jr.Description.Value;
            txtOrganizer.Text = jr.Organizer.Value;
            txtUrl.Text = jr.Url.Value;
            txtComments.Text = jr.Comment.Value;

            if(jr.StartDateTime.TimeZoneDateTime != DateTime.MinValue)
                txtStartDate.Text = jr.StartDateTime.TimeZoneDateTime.ToString("G");

            // Load status values and set status
            cboStatus.Items.Add("None");
            cboStatus.Items.Add("Draft");
            cboStatus.Items.Add("Final");
            cboStatus.Items.Add("Cancelled");

            if(cboStatus.Items.FindByValue(jr.Status.StatusValue.ToString()) == null)
                cboStatus.Items.Add(jr.Status.StatusValue.ToString());

            cboStatus.SelectedValue = jr.Status.StatusValue.ToString();

            dgAttendees.DataSource = jr.Attendees;
            dgRecurrences.DataSource = jr.RecurrenceRules;
            dgRecurDates.DataSource = jr.RecurDates;
            dgExceptions.DataSource = jr.ExceptionRules;
            dgExDates.DataSource = jr.ExceptionDates;
        }

        /// <summary>
        /// Store event information from the controls
        /// </summary>
        /// <param name="ev">The event to use</param>
        private bool StoreEventInfo(VEvent ev)
        {
            DateTime startDate = DateTime.MinValue, endDate = DateTime.MinValue;
            Duration dur = Duration.Zero;

            lblMsg.Text = null;

            // Perform some edits
            if(txtStartDate.Text.Trim().Length != 0 && !DateTime.TryParse(txtStartDate.Text,
              CultureInfo.CurrentCulture, DateTimeStyles.None, out startDate))
                lblMsg.Text = "Invalid start date format<br>";

            if(txtEndDate.Text.Trim().Length != 0 && !DateTime.TryParse(txtEndDate.Text,
              CultureInfo.CurrentCulture, DateTimeStyles.None, out endDate))
                lblMsg.Text = "Invalid end date format<br>";

            if(txtDuration.Text.Trim().Length != 0 && !Duration.TryParse(txtDuration.Text, out dur))
                lblMsg.Text += "Invalid duration format<br>";

            if(dur != Duration.Zero && txtEndDate.Enabled && endDate != DateTime.MinValue)
                lblMsg.Text += "A duration or an end date can be specified, but not both<br>";

            if(txtEndDate.Enabled && startDate != DateTime.MinValue && endDate != DateTime.MinValue && startDate > endDate)
                lblMsg.Text += "Start date must be less than or equal to end date<br>";

            if(!String.IsNullOrWhiteSpace(lblMsg.Text))
                return false;

            // Unique ID is not changed
            ev.Transparency.IsTransparent = chkTransparent.Checked;
            ev.LastModified.TimeZoneDateTime = DateTime.Now;

            if(txtSequence.Text.Trim().Length == 0)
                ev.Sequence.SequenceNumber = 0;
            else
                ev.Sequence.SequenceNumber = Convert.ToInt32(txtSequence.Text);

            if(txtPriority.Text.Trim().Length == 0)
                ev.Priority.PriorityValue = 0;
            else
                ev.Priority.PriorityValue = Convert.ToInt32(txtPriority.Text);

            ev.StartDateTime.TimeZoneDateTime = startDate;
            ev.StartDateTime.ValueLocation = ValLocValue.DateTime;
            ev.EndDateTime.TimeZoneDateTime = endDate;
            ev.EndDateTime.ValueLocation = ValLocValue.DateTime;
            ev.Duration.DurationValue = dur;

            ev.Summary.Value = txtSummary.Text;
            ev.Location.Value = txtLocation.Text;
            ev.Description.Value = txtDescription.Text;
            ev.Organizer.Value = txtOrganizer.Text;
            ev.Url.Value = txtUrl.Text;
            ev.Comment.Value = txtComments.Text;

            // Get status value
            ev.Status.StatusValue = (StatusValue)Enum.Parse(typeof(StatusValue),
                cboStatus.Items[cboStatus.SelectedIndex].ToString(), true);

            return true;
        }

        /// <summary>
        /// Store To-Do information from the controls
        /// </summary>
        /// <param name="td">The To-Do to use</param>
        private bool StoreToDoInfo(VToDo td)
        {
            DateTime startDate = DateTime.MinValue, endDate = DateTime.MinValue, completedDate = DateTime.MinValue;
            Duration dur = Duration.Zero;

            lblMsg.Text = null;

            // Perform some edits
            if(txtStartDate.Text.Trim().Length != 0 && !DateTime.TryParse(txtStartDate.Text,
              CultureInfo.CurrentCulture, DateTimeStyles.None, out startDate))
                lblMsg.Text = "Invalid start date format<br>";

            if(txtEndDate.Text.Trim().Length != 0 && !DateTime.TryParse(txtEndDate.Text,
              CultureInfo.CurrentCulture, DateTimeStyles.None, out endDate))
                lblMsg.Text = "Invalid end date format<br>";

            if(txtCompleted.Text.Trim().Length != 0 && !DateTime.TryParse(txtCompleted.Text,
              CultureInfo.CurrentCulture, DateTimeStyles.None, out completedDate))
                lblMsg.Text = "Invalid completed date format<br>";

            if(txtDuration.Text.Trim().Length != 0 && !Duration.TryParse(txtDuration.Text, out dur))
                lblMsg.Text += "Invalid duration format<br>";

            if(!String.IsNullOrWhiteSpace(lblMsg.Text))
                return false;

            // Unique ID is not changed
            td.LastModified.TimeZoneDateTime = DateTime.Now;

            if(txtSequence.Text.Trim().Length == 0)
                td.Sequence.SequenceNumber = 0;
            else
                td.Sequence.SequenceNumber = Convert.ToInt32(txtSequence.Text);

            if(txtPriority.Text.Trim().Length == 0)
                td.Priority.PriorityValue = 0;
            else
                td.Priority.PriorityValue = Convert.ToInt32(txtPriority.Text);

            td.StartDateTime.TimeZoneDateTime = startDate;
            td.StartDateTime.ValueLocation = ValLocValue.DateTime;
            td.DueDateTime.TimeZoneDateTime = endDate;
            td.DueDateTime.ValueLocation = ValLocValue.DateTime;
            td.CompletedDateTime.TimeZoneDateTime = completedDate;
            td.CompletedDateTime.ValueLocation = ValLocValue.DateTime;

            if(txtPercent.Text.Trim().Length == 0)
                td.PercentComplete.Percentage = 0;
            else
                td.PercentComplete.Percentage = Convert.ToInt32(txtPercent.Text);

            td.Duration.DurationValue = dur;
            td.Summary.Value = txtSummary.Text;
            td.Description.Value = txtDescription.Text;
            td.Organizer.Value = txtOrganizer.Text;
            td.Url.Value = txtUrl.Text;
            td.Comment.Value = txtComments.Text;

            // Get status value
            td.Status.StatusValue = (StatusValue)Enum.Parse(typeof(StatusValue),
                cboStatus.Items[cboStatus.SelectedIndex].ToString(), true);

            return true;
        }

        /// <summary>
        /// Store journal information from the controls
        /// </summary>
        /// <param name="jr">The journal to use</param>
        private bool StoreJournalInfo(VJournal jr)
        {
            DateTime startDate = DateTime.MinValue;

            lblMsg.Text = null;

            // Perform some edits
            if(txtStartDate.Text.Trim().Length != 0 && !DateTime.TryParse(txtStartDate.Text,
              CultureInfo.CurrentCulture, DateTimeStyles.None, out startDate))
            {
                lblMsg.Text = "Invalid start date format<br>";
                return false;
            }

            // Unique ID is not changed
            jr.LastModified.TimeZoneDateTime = DateTime.Now;

            if(txtSequence.Text.Trim().Length == 0)
                jr.Sequence.SequenceNumber = 0;
            else
                jr.Sequence.SequenceNumber = Convert.ToInt32(txtSequence.Text);

            jr.Summary.Value = txtSummary.Text;
            jr.Description.Value = txtDescription.Text;
            jr.Organizer.Value = txtOrganizer.Text;
            jr.Url.Value = txtUrl.Text;
            jr.Comment.Value = txtComments.Text;
            jr.StartDateTime.TimeZoneDateTime = startDate;
            jr.StartDateTime.ValueLocation = ValLocValue.DateTime;

            // Get status value
            jr.Status.StatusValue = (StatusValue)Enum.Parse(typeof(StatusValue),
                cboStatus.Items[cboStatus.SelectedIndex].ToString(), true);

            return true;
        }

        /// <summary>
        /// This handles the Add command for the recurrence data grid
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgRecurrences_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if(e.CommandName == "Add")
            {
                // Save changes to the edited item if there is one
                if(dgRecurrences.EditItemIndex != -1)
                    dgRecurrences_UpdateCommand(source, new DataGridCommandEventArgs(
                        dgRecurrences.Items[dgRecurrences.EditItemIndex], e.CommandSource, e));

                // Ignore the request if the page is not valid
                if(!Page.IsValid)
                    return;

                // Add a new recurrence and go into edit mode on it
                RecurringObject ro = GetCurrentObject();

                RRuleProperty rrule = new RRuleProperty();
                rrule.Recurrence.RecurDaily(1);
                ro.RecurrenceRules.Add(rrule);

                dgRecurrences.EditItemIndex = ro.RecurrenceRules.Count - 1;
                dgRecurrences.DataSource = ro.RecurrenceRules;
                dgRecurrences.DataBind();
            }
        }

        /// <summary>
        /// Edit a recurrence in the recurrence collection
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgRecurrences_EditCommand(object source, DataGridCommandEventArgs e)
        {
            // Ignore the request if the page is not valid
            if(!Page.IsValid)
                return;

            // Save changes to the prior edited item
            if(dgRecurrences.EditItemIndex != -1)
            {
                dgRecurrences_UpdateCommand(source, new DataGridCommandEventArgs(
                    dgRecurrences.Items[dgRecurrences.EditItemIndex], e.CommandSource, e));

                if(!Page.IsValid)
                    return;
            }

            RecurringObject ro = GetCurrentObject();

            dgRecurrences.EditItemIndex = e.Item.ItemIndex;
            dgRecurrences.DataSource = ro.RecurrenceRules;
            dgRecurrences.DataBind();
        }

        /// <summary>
        /// Delete a recurrence from the collection
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgRecurrences_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            // Save changes to the edited item if it isn't the one being deleted
            if(dgRecurrences.EditItemIndex != -1 && dgRecurrences.EditItemIndex != e.Item.ItemIndex)
            {
                Page.Validate();
                dgRecurrences_UpdateCommand(source, new DataGridCommandEventArgs(
                    dgRecurrences.Items[dgRecurrences.EditItemIndex], e.CommandSource, e));

                if(!Page.IsValid)
                    return;
            }

            RecurringObject ro = GetCurrentObject();

            ro.RecurrenceRules.RemoveAt(e.Item.ItemIndex);
            dgRecurrences.EditItemIndex = -1;
            dgRecurrences.DataSource = ro.RecurrenceRules;
            dgRecurrences.DataBind();
        }

        /// <summary>
        /// Cancel changes to a recurrence in the collection
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgRecurrences_CancelCommand(object source, DataGridCommandEventArgs e)
        {
            RecurringObject ro = GetCurrentObject();

            dgRecurrences.EditItemIndex = -1;
            dgRecurrences.DataSource = ro.RecurrenceRules;
            dgRecurrences.DataBind();
        }

        /// <summary>
        /// Update a recurrence item in the collection
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgRecurrences_UpdateCommand(object source, DataGridCommandEventArgs e)
        {
            if(!Page.IsValid)
                return;

            RecurringObject ro = GetCurrentObject();

            RecurrencePattern rpRecurrence = (RecurrencePattern)e.Item.FindControl("rpRecurrence");

            Recurrence r = ro.RecurrenceRules[e.Item.ItemIndex].Recurrence;
            r.Reset();
            rpRecurrence.GetRecurrence(r);

            dgRecurrences.EditItemIndex = -1;
            dgRecurrences.DataSource = ro.RecurrenceRules;
            dgRecurrences.DataBind();
        }

        /// <summary>
        /// Bind data to the edit item template.  We have to retrieve the calendar object from session state and
        /// retrieve the recurrence item from it to set the values in the Recurrence Pattern web server control.
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgRecurrences_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.EditItem)
            {
                RecurringObject ro = GetCurrentObject();

                RecurrencePattern rpRecurrence = (RecurrencePattern)e.Item.FindControl("rpRecurrence");
                rpRecurrence.SetRecurrence(ro.RecurrenceRules[e.Item.ItemIndex].Recurrence);
                rpRecurrence.Focus();
            }
        }

        /// <summary>
        /// This handles the Add command for the exception data grid
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgExceptions_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if(e.CommandName == "Add")
            {
                // Save changes to the edited item if there is one
                if(dgExceptions.EditItemIndex != -1)
                    dgExceptions_UpdateCommand(source, new DataGridCommandEventArgs(
                        dgExceptions.Items[dgExceptions.EditItemIndex], e.CommandSource, e));

                // Ignore the request if the page is not valid
                if(!Page.IsValid)
                    return;

                // Add a new exception and go into edit mode on it
                RecurringObject ro = GetCurrentObject();

                ExRuleProperty exrule = new ExRuleProperty();
                exrule.Recurrence.RecurDaily(1);
                ro.ExceptionRules.Add(exrule);

                dgExceptions.EditItemIndex = ro.ExceptionRules.Count - 1;
                dgExceptions.DataSource = ro.ExceptionRules;
                dgExceptions.DataBind();
            }
        }

        /// <summary>
        /// Edit an exception in the exception collection
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgExceptions_EditCommand(object source, DataGridCommandEventArgs e)
        {
            // Ignore the request if the page is not valid
            if(!Page.IsValid)
                return;

            // Save changes to the prior edited item
            if(dgExceptions.EditItemIndex != -1)
            {
                dgExceptions_UpdateCommand(source, new DataGridCommandEventArgs(
                    dgExceptions.Items[dgExceptions.EditItemIndex], e.CommandSource, e));

                if(!Page.IsValid)
                    return;
            }

            RecurringObject ro = GetCurrentObject();

            dgExceptions.EditItemIndex = e.Item.ItemIndex;
            dgExceptions.DataSource = ro.ExceptionRules;
            dgExceptions.DataBind();
        }

        /// <summary>
        /// Delete an exception from the collection
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgExceptions_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            // Save changes to the edited item if it isn't the one being deleted
            if(dgExceptions.EditItemIndex != -1 && dgExceptions.EditItemIndex != e.Item.ItemIndex)
            {
                Page.Validate();
                dgExceptions_UpdateCommand(source, new DataGridCommandEventArgs(
                    dgExceptions.Items[dgExceptions.EditItemIndex], e.CommandSource, e));

                if(!Page.IsValid)
                    return;
            }

            RecurringObject ro = GetCurrentObject();

            ro.ExceptionRules.RemoveAt(e.Item.ItemIndex);
            dgExceptions.EditItemIndex = -1;
            dgExceptions.DataSource = ro.ExceptionRules;
            dgExceptions.DataBind();
        }

        /// <summary>
        /// Cancel changes to an exception in the collection
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgExceptions_CancelCommand(object source, DataGridCommandEventArgs e)
        {
            RecurringObject ro = GetCurrentObject();

            dgExceptions.EditItemIndex = -1;
            dgExceptions.DataSource = ro.ExceptionRules;
            dgExceptions.DataBind();
        }

        /// <summary>
        /// Update an exception item in the collection
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgExceptions_UpdateCommand(object source, DataGridCommandEventArgs e)
        {
            if(!Page.IsValid)
                return;

            RecurringObject ro = GetCurrentObject();

            RecurrencePattern rpException = (RecurrencePattern)e.Item.FindControl("rpException");

            Recurrence r = ro.ExceptionRules[e.Item.ItemIndex].Recurrence;
            r.Reset();
            rpException.GetRecurrence(r);

            dgExceptions.EditItemIndex = -1;
            dgExceptions.DataSource = ro.ExceptionRules;
            dgExceptions.DataBind();
        }

        /// <summary>
        /// Bind data to the edit item template.  We have to retrieve the calendar object from session state and
        /// retrieve the exception item from it to set the values in the Recurrence Pattern web server control.
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgExceptions_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.EditItem)
            {
                RecurringObject ro = GetCurrentObject();

                RecurrencePattern rpException = (RecurrencePattern)e.Item.FindControl("rpException");
                rpException.SetRecurrence(ro.ExceptionRules[e.Item.ItemIndex].Recurrence);
                rpException.Focus();
            }
        }
	}
}
