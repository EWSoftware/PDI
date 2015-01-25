//===============================================================================================================
// System  : EWSoftware.PDI Windows Forms Controls
// File    : DailyPattern.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 10/17/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains one of several user controls that are combined to allow the editing of various recurrence
// parameters.  This one is used to specify the settings for a daily recurrence pattern.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//   Date      Who  Comments
// ==============================================================================================================
// 01/20/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace EWSoftware.PDI.Windows.Forms
{
	/// <summary>
    /// This user control is used to specify the settings for a daily recurrence pattern
	/// </summary>
    [ToolboxItem(false)]
	internal sealed partial class DailyPattern : System.Windows.Forms.UserControl
	{
        #region Constructor
        //=====================================================================

		/// <summary>
		/// Constructor
		/// </summary>
        public DailyPattern()
		{
			InitializeComponent();
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
            if(recurrence.Frequency == RecurFrequency.Daily)
                if(rbEveryXDays.Checked)
                    recurrence.Interval = (int)udcDays.Value;
                else
                {
                    recurrence.Interval = 1;
                    recurrence.ByDay.AddRange(new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
                        DayOfWeek.Thursday, DayOfWeek.Friday });
                }
        }

        /// <summary>
        /// This is called to set the values for the controls based on the current recurrence settings
        /// </summary>
        /// <param name="recurrence">The recurrence object from which to get the settings</param>
        public void SetValues(Recurrence recurrence)
        {
            int idx;

            rbEveryXDays.Checked = true;

            if(recurrence.Frequency != RecurFrequency.Daily)
                udcDays.Value = 1;
            else
            {
                udcDays.Value = (recurrence.Interval < 1000) ? recurrence.Interval : 999;

                // "Daily, every weekday" is a special case that is handled as a simple pattern in this control
                if(recurrence.ByDay.Count == 5 && recurrence.Interval == 1)
                {
                    for(idx = 0; idx < 5; idx++)
                        if(recurrence.ByDay[idx].Instance != 0 || recurrence.ByDay[idx].DayOfWeek == DayOfWeek.Saturday ||
                          recurrence.ByDay[idx].DayOfWeek == DayOfWeek.Sunday)
                            break;

                    rbEveryWeekday.Checked = (idx == 5);
                }
            }
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Enable or disable the days text box based on the selection
        /// </summary>
        private void Daily_CheckedChanged(object sender, System.EventArgs e)
        {
            udcDays.Enabled = ((sender as RadioButton) == rbEveryXDays);
        }
        #endregion
    }
}
