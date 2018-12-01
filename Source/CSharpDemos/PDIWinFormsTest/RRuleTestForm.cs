//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : RRuleTestForm.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/23/2018
// Note    : Copyright 2003-2018, Eric Woodruff, All rights reserved
// Compiler: Visual C#
//
// This is a simple demonstration used to test the recurrence engine which encapsulates the iCalendar 2.0 RRULE
// feature set.  It is separate from the other PDI calendar classes so that you can use the recurrence engine
// without the extra overhead of the calendar classes if you do not need it.
//
// Although this demo uses the parsing abilities of the class to set the recurrence options, you could just as
// easily set them in code as demonstrated in the PDIDatesTest demo program.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 10/15/2004  EFW  Created the code
// 04/07/2007  EFW  Updated to use the new .NET 2.0 features
//===============================================================================================================

using System;
using System.Globalization;
using System.Windows.Forms;

using EWSoftware.PDI;
using EWSoftware.PDI.Windows.Forms;

namespace PDIWinFormsTest
{
	/// <summary>
	/// This form is used to test the recurrence class and the recurrence editor dialog box
	/// </summary>
	public partial class RRuleTestForm : System.Windows.Forms.Form
	{
        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public RRuleTestForm()
		{
			InitializeComponent();

            lblCount.Text = String.Format(lblCount.Text, DateTime.Today.AddMonths(1));

            // Set the short date/long time pattern based on the current culture
            lbDates.FormatString = dtpStartDate.CustomFormat =
                CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " +
                CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;
		}
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Test the recurrence class with the entered pattern
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnTest_Click(object sender, EventArgs e)
        {
            DateTimeCollection dc;
            int start, count;
            double elapsed;

            try
            {
                lblCount.Text = String.Empty;

                // Define the recurrence rule by parsing the text
                Recurrence r = new Recurrence(txtRRULE.Text) { StartDateTime = dtpStartDate.Value };

                // Get the currently defined set of holidays if necessary
                if(!r.CanOccurOnHoliday)
                {
                    Recurrence.Holidays.Clear();
                    Recurrence.Holidays.AddRange(hmHolidays.Holidays);
                }

                // For hourly, minutely, and secondly, warn the user if there is no end date or max occurrences
                if(r.Frequency >= RecurFrequency.Hourly && r.MaximumOccurrences == 0 &&
                  r.RecurUntil > r.StartDateTime.AddDays(100))
                    if(MessageBox.Show("This recurrence may run for a very long time.  Continue?", "Confirm",
                      MessageBoxButtons.YesNo) == DialogResult.No)
                        return;

                lbDates.DataSource = null;
                this.Cursor = Cursors.WaitCursor;

                start = System.Environment.TickCount;
                dc = r.InstancesBetween(r.StartDateTime, DateTime.MaxValue);
                elapsed = (System.Environment.TickCount - start) / 1000.0;
                count = dc.Count;

                if(count > 5000)
                {
                    dc.RemoveRange(5000, count - 5000);
                    lblCount.Text += "A large number of dates were returned.  Only the first 5000 have been " +
                        "loaded into the list box.\r\n";
                }

                // DateTimeCollection is bindable so we can assign it as the list box's data source
                lbDates.DataSource = dc;

                lblCount.Text += String.Format("Found {0:N0} instances in {1:N2} seconds ({2:N2} instances/second)",
                    count, elapsed, count / elapsed);
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

        /// <summary>
        /// This loads the standard holiday set on form load
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void RRuleTestForm_Load(object sender, EventArgs e)
        {
            dtpStartDate.Value = new DateTime(DateTime.Today.Year, 1, 1);

            // Use standard set of holidays by default.  The holiday manager control will make a copy of the
            // collection and will take care of adding, editing, and deleting entries from it.  If an existing
            // collection is passed, it won't be modified.
            Recurrence.Holidays.Clear();
            Recurrence.Holidays.AddStandardHolidays();
            hmHolidays.Holidays = Recurrence.Holidays;
        }

        /// <summary>
        /// Clear the recurrence holiday collection so that it doesn't inadvertently affect the other test forms
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void RRuleTestForm_Closed(object sender, EventArgs e)
        {
            Recurrence.Holidays.Clear();
        }

        /// <summary>
        /// Edit the recurrence pattern using the recurrence editor dialog box
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnDesign_Click(object sender, EventArgs e)
        {
            using(RecurrencePropertiesDlg dlg = new RecurrencePropertiesDlg())
            {
                try
                {
                    Recurrence r = new Recurrence(txtRRULE.Text) { StartDateTime = dtpStartDate.Value };

                    dlg.SetRecurrence(r);

                    if(dlg.ShowDialog() == DialogResult.OK)
                    {
                        dlg.GetRecurrence(r);
                        txtRRULE.Text = r.ToString();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Describe the recurrence rule by using the ToDescription method
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnDescribe_Click(object sender, EventArgs e)
        {
            try
            {
                Recurrence r = new Recurrence(txtRRULE.Text);
                MessageBox.Show(r.ToDescription(), "Recurrence Description");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion
    }
}
