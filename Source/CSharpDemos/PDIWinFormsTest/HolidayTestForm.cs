//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : HolidayTestForm.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/05/2025
// Note    : Copyright 2003-2025, Eric Woodruff, All rights reserved
//
// This is used to test the various Holiday classes and the DateUtils class
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 08/19/2003  EFW  Created the code
//===============================================================================================================

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

using EWSoftware.PDI;

namespace PDIWinFormsTest
{
	/// <summary>
	/// This form is used to test the holiday collection class
	/// </summary>
	public partial class HolidayTestForm : System.Windows.Forms.Form
	{
        #region Constructor
        //=====================================================================

		/// <summary>
		/// Constructor
		/// </summary>
        public HolidayTestForm()
		{
			InitializeComponent();

            dgvDatesFound.AutoGenerateColumns = false;
            tbcDate.DefaultCellStyle.Format = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;

            hmHolidays.Defaults = [.. hmHolidays.Defaults.Concat(
                [new FixedHoliday(6, 19, true, "Juneteenth") { MinimumYear = 2021 }]).OrderBy(h => h.Month)];

            // Use the standard set of holidays by default.  The holiday manager control will make a copy of the
            // collection and will take care of adding, editing, and deleting entries from it.  If an existing
            // collection is passed, it won't be modified.
            hmHolidays.Holidays = hmHolidays.Defaults;

            udcFromYear.Value = DateTime.Now.Year - 1;
            udcToYear.Value = udcFromYear.Value + 6;
            dtpTestDate.Value = DateTime.Today.Date;
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Test the entered date to see if it is a holiday as defined by the entries in the holiday manager's
        /// holiday collection.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnTestDate_Click(object sender, EventArgs e)
        {
            var holidays = new HolidayCollection(hmHolidays.Holidays);

            if(holidays.IsHoliday(dtpTestDate.Value))
            {
                MessageBox.Show("The test date is a holiday", "Found!", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("The test date is not a holiday", "Not Found!", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Find all holidays in the given range of years using the entries defined in the holiday manager's
        /// holiday collection.  The found dates and descriptions are loaded into the grid view.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            var holidays = new HolidayCollection(hmHolidays.Holidays);
            int fromYear, toYear, year;

            fromYear = (int)udcFromYear.Value;
            toYear = (int)udcToYear.Value;

            if(fromYear > toYear)
            {
                year = fromYear;
                fromYear = toYear;
                toYear = year;
                udcFromYear.Value = fromYear;
                udcToYear.Value = toYear;
            }

            this.Cursor = Cursors.WaitCursor;

            dgvDatesFound.DataSource = holidays.HolidaysBetween(fromYear, toYear).OrderBy(d => d).Select(d =>
                new ListItem(d, holidays.HolidayDescription(d))).ToList();

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Load the data grid with all Easter Sunday dates for the specified range of years
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnEaster_Click(object sender, EventArgs e)
        {
            int fromYear, toYear, year;
            string desc;

            EasterMethod em = EasterMethod.Gregorian;

            fromYear = (int)udcFromYear.Value;
            toYear = (int)udcToYear.Value;

            if(rbJulian.Checked)
                em = EasterMethod.Julian;
            else
            {
                if(rbOrthodox.Checked)
                    em = EasterMethod.Orthodox;
            }

            // Adjust years as necessary based on the method
            if(em != EasterMethod.Julian)
            {
                if(fromYear < 1583)
                    fromYear = 1583;

                if(fromYear > 4099)
                    fromYear = 4099;

                if(toYear < 1583)
                    toYear = 1583;

                if(toYear > 4099)
                    toYear = 4099;
            }
            else
            {
                if(fromYear < 326)
                    fromYear = 326;

                if(toYear < 326)
                    toYear = 326;
            }

            if(fromYear > toYear)
            {
                year = fromYear;
                fromYear = toYear;
                toYear = year;
            }

            udcFromYear.Value = fromYear;
            udcToYear.Value = toYear;

            this.Cursor = Cursors.WaitCursor;

            // Create the grid view's data source
            List<ListItem> items = [];
            desc = $"Easter ({em})";

            while(fromYear <= toYear)
                items.Add(new ListItem(DateUtils.EasterSunday(fromYear++, em), desc));

            dgvDatesFound.DataSource = items;

            this.Cursor = Cursors.Default;
        }
        #endregion
    }
}
