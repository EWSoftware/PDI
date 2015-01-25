//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : FreeBusyDetails.aspx.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/31/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This page is used to demonstrate the VFreeBusy class.  Currently, it allows editing of some basic information.
// Information in the data grids could also be edited.  Time constraints limit what I have implemented so far but
// I may expand on this at a later date.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 01/25/2005  EFW  Created the code
//===============================================================================================================

using System;
using System.Globalization;
using System.Web.UI;

using EWSoftware.PDI;
using EWSoftware.PDI.Objects;
using EWSoftware.PDI.Properties;

namespace PDIWebDemoCS
{
	/// <summary>
	/// This class demonstrates the VFreeBusy class
	/// </summary>
	public partial class FreeBusyDetails : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            VCalendar cal;
            VFreeBusy fb;
            int idx;

            if(!Page.IsPostBack)
            {
                cal = (VCalendar)Session["VCalendar"];

                if(cal == null)
                    Response.Redirect("CalendarBrowser.aspx");

                if(!Int32.TryParse(Request.QueryString["Index"], out idx))
                {
                    // If not valid just go back to the browser form
                    Response.Redirect("CalendarBrowser.aspx");
                    return;
                }

                // Force it to be valid
                if(idx < 0 || idx >= cal.FreeBusys.Count)
                    idx = 0;

                this.ViewState["FreeBusyIndex"] = idx;

                // Load the data into the controls
                fb = cal.FreeBusys[idx];

                // We use the start date's time zone ID for the time zone label.  It should represent the time
                // zone used throughout the component.
                lblTimeZone.Text = fb.StartDateTime.TimeZoneId;

                // General properties
                lblUniqueId.Text = fb.UniqueId.Value;
                txtOrganizer.Text = fb.Organizer.Value;
                txtContact.Text = fb.Contact.Value;

                if(fb.StartDateTime.TimeZoneDateTime != DateTime.MinValue)
                    txtStartDate.Text = fb.StartDateTime.TimeZoneDateTime.ToString("G");

                if(fb.EndDateTime.TimeZoneDateTime != DateTime.MinValue)
                    txtEndDate.Text = fb.EndDateTime.TimeZoneDateTime.ToString("G");

                if(fb.Duration.DurationValue != Duration.Zero)
                    txtDuration.Text = fb.Duration.DurationValue.ToString(Duration.MaxUnit.Weeks);

                txtUrl.Text = fb.Url.Value;
                txtComments.Text = fb.Comment.Value;

                // Bind the data grids to display collection info
                dgAttendees.DataSource = fb.Attendees;
                dgFreeBusy.DataSource = fb.FreeBusy;
                dgReqStats.DataSource = fb.RequestStatuses;

                Page.DataBind();
            }
		}

        /// <summary>
        /// Save changes and return to the calendar browser
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DateTime startDate = DateTime.MinValue, endDate = DateTime.MinValue;
            Duration dur = Duration.Zero;

            if(!Page.IsValid)
                return;

            lblMsg.Text = null;

            // Perform some edits
            if(txtStartDate.Text.Trim().Length != 0 && !DateTime.TryParse(txtStartDate.Text,
              CultureInfo.CurrentCulture, DateTimeStyles.None, out startDate))
            {
                lblMsg.Text = "Invalid start date format<br>";
            }

            if(txtEndDate.Text.Trim().Length != 0 && !DateTime.TryParse(txtEndDate.Text,
              CultureInfo.CurrentCulture, DateTimeStyles.None, out endDate))
            {
                lblMsg.Text += "Invalid end date format<br>";
            }

            if(txtDuration.Text.Trim().Length != 0 && !Duration.TryParse(txtDuration.Text, out dur))
            {
                lblMsg.Text += "Invalid duration format<br>";
            }

            if(startDate != DateTime.MinValue && endDate != DateTime.MinValue && startDate > endDate)
            {
                lblMsg.Text += "Start date must be less than or equal to end date<br>";
            }

            if(!String.IsNullOrWhiteSpace(lblMsg.Text))
                return;

            VCalendar cal = (VCalendar)Session["VCalendar"];

            // Not very friendly, but it's just a demo
            if(cal == null)
            {
                Response.Redirect("CalendarBrowser.aspx");
                return;
            }

            VFreeBusy fb = cal.FreeBusys[(int)this.ViewState["FreeBusyIndex"]];

            // The unique ID is not changed
            fb.Organizer.Value = txtOrganizer.Text;
            fb.Contact.Value = txtContact.Text;

            // We'll use the TimeZoneDateTime property on all date/time values so that they are set literally
            // rather than being converted to the time zone as would happen with the DateTimeValue property.
            fb.StartDateTime.TimeZoneDateTime = startDate;
            fb.StartDateTime.ValueLocation = ValLocValue.DateTime;

            fb.EndDateTime.TimeZoneDateTime = endDate;
            fb.StartDateTime.ValueLocation = ValLocValue.DateTime;

            fb.Duration.DurationValue = dur;
            fb.Url.Value = txtUrl.Text;
            fb.Comment.Value = txtComments.Text;

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
	}
}
