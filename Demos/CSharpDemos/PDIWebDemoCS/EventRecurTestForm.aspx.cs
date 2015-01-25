//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : EventRecurTestForm.aspx.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/31/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This page is used to demonstrate recurrence with the calendar components
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 01/20/2005  EFW  Created the code
//===============================================================================================================

using System;
using System.Globalization;
using System.Web.UI;

using EWSoftware.PDI;
using EWSoftware.PDI.Parser;
using EWSoftware.PDI.Objects;
using EWSoftware.PDI.Properties;

namespace PDIWebDemoCS
{
	/// <summary>
	/// This page is used to demonstrate recurrence with the calendar components
	/// </summary>
	public partial class EventRecurTestForm : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            this.Page.Title = "iCalendar Component Recurrence Demo";

            if(!Page.IsPostBack)
            {
                // The time zone information is loaded in the Application_Start event in Global.asax.  We'll
                // acquire a reader lock on the time zone collection as it's possible other sessions could be
                // parsing calendars with time zone data that could change the collection.
                VCalendar.TimeZones.Lock.AcquireReaderLock(250);

                try
                {
                    cboTimeZone.Items.Add("No time zone");

                    foreach(VTimeZone tz in VCalendar.TimeZones)
                        cboTimeZone.Items.Add(tz.TimeZoneId.Value);
                }
                finally
                {
                    VCalendar.TimeZones.Lock.ReleaseReaderLock();
                }

                // Set up some defaults for testing
                cboTimeZone.SelectedIndex = 0;

                DateTime dtDate = new DateTime(DateTime.Today.Year, 1, 1);
                txtStartDate.Text = dtDate.ToString("G");
                txtEndDate.Text = DateTime.Today.AddMonths(3).ToString("G");

                txtCalendar.Text = String.Format(
                    "BEGIN:VEVENT\r\n" +
                    "DTSTART:{0}\r\n" +
                    "DTEND:{1}\r\n" +
                    "RRULE:FREQ=DAILY;COUNT=10;INTERVAL=5\r\n" +
                    "END:VEVENT\r\n",
                    dtDate.AddHours(9).ToUniversalTime().ToString(ISO8601Format.BasicDateTimeUniversal),
                    dtDate.AddHours(10).ToUniversalTime().ToString(ISO8601Format.BasicDateTimeUniversal));
            }
		}

        /// <summary>
        /// Use some limitations to prevent overloading the server or timing out the page if possible
        /// </summary>
        /// <param name="ro">The recurring object</param>
        /// <param name="r">The recurrence to check</param>
        private void ApplyLimits(RecurringObject ro, Recurrence r)
        {
            if(r.Frequency > RecurFrequency.Hourly)
                r.MaximumOccurrences = 5000;

            if(r.MaximumOccurrences != 0)
            {
                if(r.MaximumOccurrences > 5000)
                    r.MaximumOccurrences = 5000;
            }
            else
                if(r.Frequency == RecurFrequency.Hourly)
                {
                    if(r.RecurUntil > ro.StartDateTime.DateTimeValue.AddYears(5))
                        r.RecurUntil = ro.StartDateTime.DateTimeValue.AddYears(5);
                }
                else
                    if(r.RecurUntil > ro.StartDateTime.DateTimeValue.AddYears(50))
                        r.RecurUntil = ro.StartDateTime.DateTimeValue.AddYears(50);
        }

        /// <summary>
        /// Generate instances for the specified component
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        protected void btnTest_Click(object sender, EventArgs e)
        {
            RecurringObject ro = null;
            DateTimeInstanceCollection instances;
            string calendar;
            int start;
            double elapsed;
            DateTime startDate, endDate;

            try
            {
                lblCount.Text = String.Empty;

                if(!DateTime.TryParse(txtStartDate.Text, CultureInfo.CurrentCulture, DateTimeStyles.None,
                  out startDate) || !DateTime.TryParse(txtEndDate.Text, CultureInfo.CurrentCulture,
                  DateTimeStyles.None, out endDate))
                {
                    lblCount.Text = "Invalid start or end date/time format";
                    return;
                }

                // Wrap it in VCALENDAR tags and parse it
                calendar = String.Format("BEGIN:VCALENDAR\nVERSION:2.0\n{0}\nEND:VCALENDAR", txtCalendar.Text);

                VCalendar cal = VCalendarParser.ParseFromString(calendar);

                // Get the first event, to-do, or journal item
                if(cal.Events.Count > 0)
                    ro = cal.Events[0];
                else
                    if(cal.ToDos.Count > 0)
                        ro = cal.ToDos[0];
                else
                    if(cal.Journals.Count > 0)
                        ro = cal.Journals[0];

                if(ro == null)
                {
                    lblCount.Text = "No event, to-do, or journal item found";
                    return;
                }

                // Apply the time zone to the calendar.  If "None" is selected, the time zone will be cleared on
                // all items.
                if(cboTimeZone.SelectedIndex < 1)
                    cal.ApplyTimeZone(null);
                else
                    cal.ApplyTimeZone(VCalendar.TimeZones[cboTimeZone.SelectedIndex - 1]);

                foreach(RRuleProperty rrule in ro.RecurrenceRules)
                    ApplyLimits(ro, rrule.Recurrence);

                foreach(ExRuleProperty exrule in ro.ExceptionRules)
                    ApplyLimits(ro, exrule.Recurrence);

                txtCalendar.Text = ro.ToString();

                start = System.Environment.TickCount;
                instances = ro.InstancesBetween(startDate, endDate, chkInLocalTime.Checked);
                elapsed = (System.Environment.TickCount - start) / 1000.0;

                cal.Dispose();

                // The date instance contains the start and end date/times, the duration, and time zone
                // information.  The duration is based on the duration of the calendar component.  The time zone
                // information is based on the "In Local Time" parameter of the InstancesBetween() method and
                // whether or not the component has a Time Zone ID specified.
                dlDates.DataSource = instances;
                dlDates.DataBind();

                // If nothing was found remind the user that they may need to adjust the start and end date range
                // to find stuff within the item.
                if(instances.Count == 0)
                    lblCount.Text = "Nothing found.  If this was unexpected, check the limiting date range " +
                        "in the two date/time text boxes at the top of the form and the calendar item date/time " +
                        "properties to make sure that they do overlap<br/><br/>";

                lblCount.Text += String.Format("Found {0:N0} instances in {1:N2} seconds ({2:N2} instances/second)",
                    instances.Count, elapsed, instances.Count / elapsed);
            }
            catch(Exception ex)
            {
                lblCount.Text = ex.Message;
            }
        }
	}
}
