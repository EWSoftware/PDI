//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : VTimeZoneTestForm.aspx.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/30/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This page is used to demonstrate some of the time zone features of the PDI classes
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 01/16/2005  EFW  Created the code
//===============================================================================================================

using System;
using System.Globalization;
using System.Web.UI;

using EWSoftware.PDI;
using EWSoftware.PDI.Objects;

namespace PDIWebDemoCS
{
	/// <summary>
	/// This demonstrates some of the time zone features of the library
	/// </summary>
	public partial class VTimeZoneTestForm : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            this.Page.Title = "VTimeZone Demo";

			// On first load, bind the drop down lists to the time zone IDs
            if(!Page.IsPostBack)
            {
                // The time zone information is loaded in the Application_Start event in Global.asax.  We'll
                // acquire a reader lock on the time zone collection as it's possible other sessions could be
                // parsing calendars with time zone data that could change the collection.
                VCalendar.TimeZones.Lock.AcquireReaderLock(250);

                try
                {
                    foreach(VTimeZone tz in VCalendar.TimeZones)
                    {
                        cboSourceTimeZone.Items.Add(tz.TimeZoneId.Value);
                        cboDestTimeZone.Items.Add(tz.TimeZoneId.Value);
                    }
                }
                finally
                {
                    VCalendar.TimeZones.Lock.ReleaseReaderLock();
                }

                txtSourceDate.Text = new DateTime(DateTime.Today.Year, 1, 1, 10, 0, 0).ToString("G");
                btnApplySrc_Click(this, EventArgs.Empty);
            }
		}

        /// <summary>
        /// Convert the selected date/time to local time and back and to the selected time zone and back to test
        /// the time zone conversion code.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        protected void btnApplySrc_Click(object sender, EventArgs e)
        {
            DateTime dt;

            VTimeZone vtzSource = VCalendar.TimeZones[cboSourceTimeZone.SelectedItem.Text];
            VTimeZone vtzDest = VCalendar.TimeZones[cboDestTimeZone.SelectedItem.Text];

            // Show information for the selected time zones
            lblTimeZoneInfo.Text = String.Format("<strong>From Time Zone:</strong>\r\n\r\n" +
                "{0}\r\n<strong>To Time Zone:</strong>\r\n\r\n{1}", vtzSource.ToString(), vtzDest.ToString());

            // Do the conversions.  Each is round tripped to make sure it gets the expected results.  Note that
            // there will be some anomalies when round tripping times that are near the standard time/DST shift
            // as these times are somewhat ambiguous and their meaning can vary depending on which side of the
            // shift you are on and the direction of the conversion.
            if(!DateTime.TryParse(txtSourceDate.Text, CultureInfo.CurrentCulture, DateTimeStyles.None, out dt))
            {
                lblLocalTime.Text = "Invalid date/time format";
                lblLocalBackToSource.Text = lblDestTime.Text = lblDestBackToSource.Text = String.Empty;
                return;
            }

            DateTimeInstance dti = VCalendar.TimeZoneTimeToLocalTime(dt, vtzSource.TimeZoneId.Value);

            lblLocalTime.Text = String.Format("{0} {1}", dti.StartDateTime, dti.StartTimeZoneName);

            dti = VCalendar.LocalTimeToTimeZoneTime(dti.StartDateTime, vtzSource.TimeZoneId.Value);
            lblLocalBackToSource.Text = String.Format("{0} {1}", dti.StartDateTime, dti.StartTimeZoneName);

            dti = VCalendar.TimeZoneToTimeZone(dt, vtzSource.TimeZoneId.Value, vtzDest.TimeZoneId.Value);
            lblDestTime.Text = String.Format("{0} {1}", dti.StartDateTime, dti.StartTimeZoneName);

            dti = VCalendar.TimeZoneToTimeZone(dti.StartDateTime, vtzDest.TimeZoneId.Value, vtzSource.TimeZoneId.Value);
            lblDestBackToSource.Text = String.Format("{0} {1}", dti.StartDateTime, dti.StartTimeZoneName);
        }

        /// <summary>
        /// Download the time zone "database"
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        protected void btnGetTZs_Click(object sender, EventArgs e)
        {
            // We could just use a link to download the file but this shows another way.
            this.Response.ClearContent();
            this.Response.ContentType = "text/calendar";
            this.Response.AppendHeader("Content-Disposition", "inline;filename=TimeZoneDB.ics");

            // Response.WriteFile would also work, but we'll do it the long way.  Since we are writing out the
            // time zone collection by itself, we'll need to provide the calender wrapper.
            this.Response.Write("BEGIN:VCALENDAR\r\n");
            this.Response.Write("VERSION:2.0\r\n");
            this.Response.Write("PRODID:-//EWSoftware//PDI Class Library//EN\r\n");

            // Time zones can be written directly to the stream
            foreach(VTimeZone tz in VCalendar.TimeZones)
                tz.WriteToStream(Response.Output);

            this.Response.Write("END:VCALENDAR\r\n");
            Response.End();
        }
	}
}
