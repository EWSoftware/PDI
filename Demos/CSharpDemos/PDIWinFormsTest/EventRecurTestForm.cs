//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : EventRecurTestForm.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/27/2014
// Note    : Copyright 2003-2014, Eric Woodruff, All rights reserved
// Compiler: Visual C#
//
// This is a simple demonstration used to test the event recurrence generation features of the calendar classes
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 10/15/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

using EWSoftware.PDI;
using EWSoftware.PDI.Parser;
using EWSoftware.PDI.Objects;

namespace PDIWinFormsTest
{
	/// <summary>
	/// This form is used to test recurrence generation within an iCalendar component
	/// </summary>
	public partial class EventRecurTestForm : System.Windows.Forms.Form
	{
        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public EventRecurTestForm()
		{
			InitializeComponent();

            // Set the short date/long time pattern based on the current culture
            dtpStartDate.CustomFormat = dtpEndDate.CustomFormat =
                CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " +
                CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;
		}
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// This loads the time zone information from the registry when the form is loaded and sets the form
        /// defaults.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void EventRecurTestForm_Load(object sender, EventArgs e)
        {
            TimeZoneRegInfo.LoadTimeZoneInfo();

            // Load the time zone combo box.  The first entry will be for no time zone.
            cboTimeZone.Items.Add("No time zone");

            foreach(VTimeZone vtz in VCalendar.TimeZones)
                cboTimeZone.Items.Add(vtz.TimeZoneId.Value);

            cboTimeZone.SelectedIndex = 0;

            DateTime dtDate = new DateTime(DateTime.Today.Year, 1, 1);
            dtpStartDate.Value = dtDate;
            dtpEndDate.Value = dtDate.AddMonths(3);

            txtCalendar.Text = String.Format(
                "BEGIN:VEVENT\r\n" +
                "DTSTART:{0}\r\n" +
                "DTEND:{1}\r\n" +
                "RRULE:FREQ=DAILY;COUNT=10;INTERVAL=5\r\n" +
                "END:VEVENT\r\n",
                dtDate.AddHours(9).ToUniversalTime().ToString(ISO8601Format.BasicDateTimeUniversal),
                dtDate.AddHours(10).ToUniversalTime().ToString(ISO8601Format.BasicDateTimeUniversal));
        }

        /// <summary>
        /// Clear out the time zone collection so as not to affect any of the other test forms
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void EventRecurTestForm_Closing(object sender, CancelEventArgs e)
        {
            VCalendar.TimeZones.Clear();
        }

        /// <summary>
        /// Test the recurrence within the specified iCalendar component
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnTest_Click(object sender, EventArgs e)
        {
            RecurringObject ro = null;
            DateTimeInstanceCollection instances;
            string calendar;
            int start;
            double elapsed;

            try
            {
                lblCount.Text = null;

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

                txtCalendar.Text = ro.ToString();

                lbDates.Items.Clear();
                this.Cursor = Cursors.WaitCursor;

                start = System.Environment.TickCount;
                instances = ro.InstancesBetween(dtpStartDate.Value, dtpEndDate.Value, chkInLocalTime.Checked);
                elapsed = (System.Environment.TickCount - start) / 1000.0;

                cal.Dispose();

                // The date instance contains the start and end date/times, the duration, and time zone
                // information.  The duration is based on the duration of the calendar component.  The time
                // zone information is based on the "In Local Time" parameter of the InstancesBetween() method
                // and whether or not the component has a Time Zone ID specified.
                foreach(DateTimeInstance dti in instances)
                {
                    lbDates.Items.Add(String.Format("{0:G} {1} to {2:G} {3} ({4})", dti.StartDateTime,
                      dti.AbbreviatedStartTimeZoneName, dti.EndDateTime, dti.AbbreviatedEndTimeZoneName,
                      dti.Duration.ToDescription()));

                    if(lbDates.Items.Count > 5000)
                    {
                        lblCount.Text += "A large number of instances were returned.  Only the first 5000 " +
                            "have been loaded into the list box.\r\n";
                        break;
                    }
                }

                // If nothing was found remind the user that they may need to adjust the start and end date range
                // to find stuff within the item.
                if(instances.Count == 0)
                    MessageBox.Show("Nothing found.  If this was unexpected, check the limiting date range in " +
                        "the two date/time text boxes at the top of the form and the calendar item date/time " +
                        "properties to make sure that they do overlap");

                lblCount.Text += String.Format("Found {0:N0} instances in {1:N2} seconds ({2:N2} instances/second)",
                    instances.Count, elapsed, instances.Count / elapsed);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion
    }
}
