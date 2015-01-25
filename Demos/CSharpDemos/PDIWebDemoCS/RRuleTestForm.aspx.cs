//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : RRuleTestForm.aspx.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/31/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This page is used to demonstrate the recurrence engine
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 01/17/2005  EFW  Created the code
// 03/21/2005  EFW  Added the RecurrencePattern web server control
// 02/10/2007  EFW  Update for .NET 2.0 and new RecurrencePattern control
//===============================================================================================================

using System;
using System.Globalization;
using System.Web.UI;

using EWSoftware.PDI;

namespace PDIWebDemoCS
{
	/// <summary>
	/// This page is used to demonstrate the recurrence engine
	/// </summary>
	public partial class RRuleTestForm : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            this.Page.Title = "Recurrence Demo";

            // On first load, set some defaults and bind the holiday list box to the defined recurrence holidays
            if(!Page.IsPostBack)
            {
                txtStartDate.Text = new DateTime(DateTime.Today.Year, 1, 1).ToString("G");
                txtRRULE.Text = "FREQ=DAILY;INTERVAL=5;COUNT=50";
                lbResults.DataTextFormatString = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern +
                    " " + CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;

                Recurrence r = new Recurrence(txtRRULE.Text);
                rpRecurrence.SetRecurrence(r);
                lblDescription.Text = r.ToDescription();

                lbHolidays.DataSource = Recurrence.Holidays;
                lbHolidays.DataBind();
            }
		}

        /// <summary>
        /// Generate instances based on the settings in the pattern control
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        protected void btnTestPattern_Click(object sender, EventArgs e)
        {
            Recurrence r = new Recurrence();
            rpRecurrence.GetRecurrence(r);

            // Make the textbox match the pattern control and use it's event handler
            txtRRULE.Text = r.ToString();
            btnTest_Click(sender, e);
        }

        /// <summary>
        /// Generate instances for the specified recurrence pattern
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        protected void btnTest_Click(object sender, EventArgs e)
        {
            DateTimeCollection dc;
            DateTime dt;
            int start;
            double elapsed;

            try
            {
                lblCount.Text = lblDescription.Text = String.Empty;

                if(!DateTime.TryParse(txtStartDate.Text, CultureInfo.CurrentCulture, DateTimeStyles.None, out dt))
                {
                    lblCount.Text = "Invalid date/time or RRULE format";
                    return;
                }

                // Define the recurrence rule by parsing the text
                Recurrence r = new Recurrence(txtRRULE.Text);
                r.StartDateTime = dt;

                // Synch the pattern control to the RRULE if not called by the pattern's test button
                if(sender != btnTestPattern)
                    rpRecurrence.SetRecurrence(r);

                // Use some limitations to prevent overloading the server or timing out the page if possible
                if(r.Frequency > RecurFrequency.Hourly && (r.MaximumOccurrences == 0 || r.MaximumOccurrences > 5000))
                {
                    r.MaximumOccurrences = 5000;
                    txtRRULE.Text = r.ToString();
                }

                if(r.MaximumOccurrences != 0)
                {
                    if(r.MaximumOccurrences > 5000)
                    {
                        r.MaximumOccurrences = 5000;
                        txtRRULE.Text = r.ToString();
                    }
                }
                else
                    if(r.Frequency == RecurFrequency.Hourly)
                    {
                        if(r.RecurUntil > r.StartDateTime.AddYears(5))
                        {
                            r.RecurUntil = r.StartDateTime.AddYears(5);
                            txtRRULE.Text = r.ToString();
                        }
                    }
                    else
                        if(r.RecurUntil > r.StartDateTime.AddYears(50))
                        {
                            r.RecurUntil = r.StartDateTime.AddYears(50);
                            txtRRULE.Text = r.ToString();
                        }

                // Time the calculation
                start = System.Environment.TickCount;
                dc = r.InstancesBetween(r.StartDateTime, DateTime.MaxValue);
                elapsed = (System.Environment.TickCount - start) / 1000.0;

                // Bind the results to the list box
                lbResults.DataSource = dc;
                lbResults.DataBind();

                lblCount.Text = String.Format("Found {0:N0} instances in {1:N2} " +
                    "seconds ({2:N2} instances/second)", dc.Count, elapsed, dc.Count / elapsed);

                // Show a description of the pattern
                lblDescription.Text = r.ToDescription();
            }
            catch(Exception ex)
            {
                lblCount.Text = ex.Message;
            }
        }
	}
}
