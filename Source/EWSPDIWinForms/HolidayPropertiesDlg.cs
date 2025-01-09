//===============================================================================================================
// System  : EWSoftware.PDI Windows Forms Controls
// File    : HolidayPropertiesDlg.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/02/2025
// Note    : Copyright 2003-2025, Eric Woodruff, All rights reserved
//
// This is used to add or edit holiday object information
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
using System.Windows.Forms;

namespace EWSoftware.PDI.Windows.Forms
{
	/// <summary>
	/// This form is used to add or edit holiday object information.
	/// </summary>
	public partial class HolidayPropertiesDlg : System.Windows.Forms.Form
	{
        #region Private data members
        //=====================================================================

        private Holiday holiday = null!;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// Get or set the holiday information
        /// </summary>
        /// <exception cref="ArgumentNullException">This is thrown if the holiday information property is set to
        /// null.</exception>
        public Holiday HolidayInfo
        {
            get => holiday;
            set
            {
                holiday = value ?? throw new ArgumentNullException(nameof(value), LR.GetString("ExHAEHolidayIsNull"));

                if(holiday is FloatingHoliday fl)
                {
                    cboOccurrence.SelectedValue = fl.Occurrence;
                    cboDayOfWeek.SelectedValue = fl.Weekday;
                    udcOffset.Value = (fl.Offset < -999) ? -999 : (fl.Offset > 999) ? 999 : fl.Offset;
                    rbFloating.Checked = true;
                    rbFixed.Checked = false;
                }
                else
                {
                    FixedHoliday fx = (FixedHoliday)holiday;

                    chkAdjustDate.Checked = fx.AdjustFixedDate;
                    udcDayOfMonth.Value = fx.Day;
                    rbFloating.Checked = false;
                    rbFixed.Checked = true;
                }

                cboMonth.SelectedValue = holiday.Month;
                txtDescription.Text = holiday.Description;

                if(holiday.MinimumYear > 0 && holiday.MinimumYear < 10000)
                    udcMinimumYear.Value = holiday.MinimumYear;

                if(holiday.MaximumYear > 0 && holiday.MaximumYear < 10000)
                    udcMaximumYear.Value = holiday.MaximumYear;
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public HolidayPropertiesDlg()
		{
			InitializeComponent();

            cboMonth.DisplayMember = "Display";
            cboMonth.ValueMember = "Value";
            cboMonth.DataSource = RecurOptsDataSource.MonthsOfYear;

            cboOccurrence.DisplayMember = "Display";
            cboOccurrence.ValueMember = "Value";
            cboOccurrence.DataSource = RecurOptsDataSource.DayOccurrences;

            cboDayOfWeek.DisplayMember = "Display";
            cboDayOfWeek.ValueMember = "Value";
            cboDayOfWeek.DataSource = RecurOptsDataSource.DayOfWeek;

            this.HolidayInfo = new FixedHoliday(1, 1, true, String.Empty);
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Perform validation and store the changes
        /// </summary>
        private void HolidayPropertiesDlg_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Ignore on cancel
            if(this.DialogResult == DialogResult.Cancel)
                return;

            txtDescription.Text = txtDescription.Text.Trim();
            epErrors.Clear();

            if(udcMinimumYear.Value > udcMaximumYear.Value)
            {
                decimal tempYear = udcMinimumYear.Value;
                udcMinimumYear.Value = udcMaximumYear.Value;
                udcMaximumYear.Value = tempYear;
            }

            // We must have a description
            if(txtDescription.Text.Length == 0)
            {
                epErrors.SetError(txtDescription, LR.GetString("EditHAEBlankDesc"));
                e.Cancel = true;
            }

            // Leap years aren't accepted so always use a non-leap year to check the date
            if(rbFixed.Checked && udcDayOfMonth.Value > DateTime.DaysInMonth(2003, (int)cboMonth.SelectedValue!))
            {
                epErrors.SetError(udcDayOfMonth, LR.GetString("EditHAEBadDayOfMonth"));
                e.Cancel = true;
            }

            // If all is good, update the holiday object with the new settings
            if(!e.Cancel)
            {
                if(!rbFixed.Checked)
                {
                    FloatingHoliday fl = new();
                    holiday = fl;

                    fl.Occurrence = (DayOccurrence)cboOccurrence.SelectedValue!;
                    fl.Weekday = (DayOfWeek)cboDayOfWeek.SelectedValue!;
                    fl.Offset = (int)udcOffset.Value;
                }
                else
                {
                    FixedHoliday fx = new();
                    holiday = fx;

                    fx.AdjustFixedDate = chkAdjustDate.Checked;
                    fx.Day = (int)udcDayOfMonth.Value;
                }

                holiday.Month = (int)cboMonth.SelectedValue!;
                holiday.Description = txtDescription.Text;
                holiday.MinimumYear = (int)udcMinimumYear.Value;
                holiday.MaximumYear = (int)udcMaximumYear.Value;
            }
        }

        /// <summary>
        /// Enable or disable the controls based on the type selected
        /// </summary>
        private void Type_CheckedChanged(object sender, System.EventArgs e)
        {
            bool enabled = ((sender as RadioButton) == rbFloating);

            cboOccurrence.Enabled = cboDayOfWeek.Enabled = udcOffset.Enabled = enabled;
            udcDayOfMonth.Enabled = chkAdjustDate.Enabled = !enabled;
        }
        #endregion
    }
}
