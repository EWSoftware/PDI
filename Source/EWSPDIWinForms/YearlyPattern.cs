//===============================================================================================================
// System  : EWSoftware.PDI Windows Forms Controls
// File    : YearlyPattern.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/02/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains one of several user controls that are combined to allow the editing of various recurrence
// parameters.  This one is used to specify the settings for a yearly recurrence pattern.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 01/20/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace EWSoftware.PDI.Windows.Forms
{
	/// <summary>
    /// This user control is used to specify the settings for a yearly recurrence pattern
	/// </summary>
    [ToolboxItem(false)]
	internal sealed partial class YearlyPattern : System.Windows.Forms.UserControl
	{
        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public YearlyPattern()
		{
			InitializeComponent();

            cboMonth.DisplayMember = "Display";
            cboMonth.ValueMember = "Value";
            cboMonth.DataSource = RecurOptsDataSource.MonthsOfYear;

            cboOccurrence.DisplayMember = "Display";
            cboOccurrence.ValueMember = "Value";
            cboOccurrence.DataSource = RecurOptsDataSource.DayOccurrences;

            cboDOW.DisplayMember = "Display";
            cboDOW.ValueMember = "Value";
            cboDOW.DataSource = RecurOptsDataSource.DaysOfWeek;

            cboDOWMonth.DisplayMember = "Display";
            cboDOWMonth.ValueMember = "Value";
            cboDOWMonth.DataSource = RecurOptsDataSource.MonthsOfYear;
		}
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// This is called to get the values from the controls and set them in the specified recurrence object
        /// </summary>
        /// <param name="recurrence">The recurrence object to which the settings are applied</param>
        /// <remarks>It is assumed that the recurrence object has been reset to a default state</remarks>
        public void GetValues(Recurrence recurrence)
        {
            int instance;
            DaysOfWeek rd;

            if(recurrence.Frequency != RecurFrequency.Yearly)
                return;

            if(rbDayXEveryYYears.Checked)
            {
                recurrence.Interval = (int)udcYears.Value;
                recurrence.ByMonth.Add((int)cboMonth.SelectedValue!);
                recurrence.ByMonthDay.Add((int)udcDay.Value);
            }
            else
            {
                recurrence.Interval = (int)udcDOWYears.Value;
                recurrence.ByMonth.Add((int)cboDOWMonth.SelectedValue!);

                // If it's a single day, use ByDay.  If it's a combination, use ByDay with BySetPos.
                rd = (DaysOfWeek)cboDOW.SelectedValue!;
                instance = ((DayOccurrence)cboOccurrence.SelectedValue! == DayOccurrence.Last) ? -1 :
                    (int)cboOccurrence.SelectedValue;

                switch(rd)
                {
                    case DaysOfWeek.EveryDay:
                        recurrence.BySetPos.Add(instance);
                        recurrence.ByDay.AddRange([ DayOfWeek.Sunday, DayOfWeek.Monday,
                            DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday,
                            DayOfWeek.Saturday ]);
                        break;

                    case DaysOfWeek.Weekdays:
                        recurrence.BySetPos.Add(instance);
                        recurrence.ByDay.AddRange([ DayOfWeek.Monday, DayOfWeek.Tuesday,
                            DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday ]);
                        break;

                    case DaysOfWeek.Weekends:
                        recurrence.BySetPos.Add(instance);
                        recurrence.ByDay.AddRange([DayOfWeek.Sunday, DayOfWeek.Saturday]);
                        break;

                    default:
                        recurrence.ByDay.Add(new DayInstance(instance, DateUtils.ToDayOfWeek(rd)));
                        break;
                }
            }
        }

        /// <summary>
        /// This is called to set the values for the controls based on the current recurrence settings
        /// </summary>
        /// <param name="recurrence">The recurrence object from which to get the settings</param>
        public void SetValues(Recurrence recurrence)
        {
            DaysOfWeek rd = DaysOfWeek.None;

            rbDayXEveryYYears.Checked = true;

            // Use default values if not a yearly frequency
            if(recurrence.Frequency != RecurFrequency.Yearly)
            {
                udcDay.Value = udcYears.Value = udcDOWYears.Value = 1;
                cboMonth.SelectedIndex = cboOccurrence.SelectedIndex = cboDOW.SelectedIndex =
                    cboDOWMonth.SelectedIndex = 0;
            }
            else
            {
                if(recurrence.ByDay.Count == 0)
                {
                    if(recurrence.ByMonth.Count != 0)
                        cboMonth.SelectedValue = recurrence.ByMonth[0];
                    else
                        cboMonth.SelectedValue = recurrence.StartDateTime.Month;

                    if(recurrence.ByMonthDay.Count != 0)
                        udcDay.Value = recurrence.ByMonthDay[0];
                    else
                        udcDay.Value = recurrence.StartDateTime.Day;

                    udcYears.Value = (recurrence.Interval < 1000) ? recurrence.Interval : 999;

                    cboOccurrence.SelectedIndex = cboDOW.SelectedIndex = cboDOWMonth.SelectedIndex = 0;
                    udcDOWYears.Value = 1;
                }
                else
                {
                    rbDayOfWeek.Checked = true;

                    udcDay.Value = udcYears.Value = 1;
                    udcDOWYears.Value = (recurrence.Interval < 1000) ? recurrence.Interval : 999;
                    cboMonth.SelectedIndex = 0;

                    if(recurrence.ByMonth.Count != 0)
                        cboDOWMonth.SelectedValue = recurrence.ByMonth[0];
                    else
                        cboDOWMonth.SelectedValue = recurrence.StartDateTime.Month;

                    // If it's a single day, use ByDay.  If it's a combination, use ByDay with BySetPos.
                    if(recurrence.ByDay.Count == 1)
                    {
                        cboOccurrence.SelectedValue = (recurrence.ByDay[0].Instance < 1 ||
                          recurrence.ByDay[0].Instance > 4) ? DayOccurrence.Last :
                            (DayOccurrence)recurrence.ByDay[0].Instance;

                        cboDOW.SelectedValue = DateUtils.ToDaysOfWeek(recurrence.ByDay[0].DayOfWeek);
                    }
                    else
                    {
                        if(recurrence.BySetPos.Count == 0)
                            cboOccurrence.SelectedIndex = 0;
                        else
                            cboOccurrence.SelectedValue = (recurrence.BySetPos[0] < 1 ||
                              recurrence.BySetPos[0] > 4) ? DayOccurrence.Last :
                                (DayOccurrence)recurrence.BySetPos[0];

                        // Figure out days
                        foreach(DayInstance di in recurrence.ByDay)
                        {
                            switch(di.DayOfWeek)
                            {
                                case DayOfWeek.Sunday:
                                    rd |= DaysOfWeek.Sunday;
                                    break;

                                case DayOfWeek.Monday:
                                    rd |= DaysOfWeek.Monday;
                                    break;

                                case DayOfWeek.Tuesday:
                                    rd |= DaysOfWeek.Tuesday;
                                    break;

                                case DayOfWeek.Wednesday:
                                    rd |= DaysOfWeek.Wednesday;
                                    break;

                                case DayOfWeek.Thursday:
                                    rd |= DaysOfWeek.Thursday;
                                    break;

                                case DayOfWeek.Friday:
                                    rd |= DaysOfWeek.Friday;
                                    break;

                                case DayOfWeek.Saturday:
                                    rd |= DaysOfWeek.Saturday;
                                    break;
                            }
                        }

                        // If not EveryDay, Weekdays, or Weekends, force it to a single day of the week
                        if(rd == DaysOfWeek.None || (rd != DaysOfWeek.EveryDay && rd != DaysOfWeek.Weekdays &&
                          rd != DaysOfWeek.Weekends))
                        {
                            rd = DateUtils.ToDaysOfWeek(DateUtils.ToDayOfWeek(rd));
                        }

                        cboDOW.SelectedValue = rd;
                    }
                }
            }
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Enable or disable the controls based on the selection
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void Yearly_CheckedChanged(object sender, System.EventArgs e)
        {
            cboMonth.Enabled = udcDay.Enabled = udcYears.Enabled = sender == rbDayXEveryYYears;
            cboOccurrence.Enabled = cboDOW.Enabled = cboDOWMonth.Enabled = udcDOWYears.Enabled = sender == rbDayOfWeek;
        }

        /// <summary>
        /// Set the maximum day of the month when the month changes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void cboMonth_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            udcDay.Maximum = DateTime.DaysInMonth(2003, cboMonth.SelectedIndex + 1);
        }
        #endregion
    }
}
