//===============================================================================================================
// System  : EWSoftware.PDI Windows Forms Controls
// File    : WeeklyPattern.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 10/21/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains one of several user controls that are combined to allow the editing of various recurrence
// parameters.  This one is used to specify the settings for a weekly recurrence pattern.
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
    /// This user control is used to specify the settings for a weekly recurrence pattern
	/// </summary>
    [ToolboxItem(false)]
	internal sealed partial class WeeklyPattern : System.Windows.Forms.UserControl
	{
        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public WeeklyPattern()
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
            if(recurrence.Frequency == RecurFrequency.Weekly)
            {
                recurrence.Interval = (int)udcWeeks.Value;

                if(chkSunday.Checked)
                    recurrence.ByDay.Add(DayOfWeek.Sunday);

                if(chkMonday.Checked)
                    recurrence.ByDay.Add(DayOfWeek.Monday);

                if(chkTuesday.Checked)
                    recurrence.ByDay.Add(DayOfWeek.Tuesday);

                if(chkWednesday.Checked)
                    recurrence.ByDay.Add(DayOfWeek.Wednesday);

                if(chkThursday.Checked)
                    recurrence.ByDay.Add(DayOfWeek.Thursday);

                if(chkFriday.Checked)
                    recurrence.ByDay.Add(DayOfWeek.Friday);

                if(chkSaturday.Checked)
                    recurrence.ByDay.Add(DayOfWeek.Saturday);
            }
        }

        /// <summary>
        /// This is called to set the values for the controls based on the current recurrence settings
        /// </summary>
        /// <param name="recurrence">The recurrence object from which to get the settings</param>
        public void SetValues(Recurrence recurrence)
        {
            CheckBox ckb;

            chkSunday.Checked = chkMonday.Checked = chkTuesday.Checked = chkWednesday.Checked =
                chkThursday.Checked = chkFriday.Checked = chkSaturday.Checked = false;

            switch(recurrence.StartDateTime.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    ckb = chkSunday;
                    break;

                case DayOfWeek.Monday:
                    ckb = chkMonday;
                    break;

                case DayOfWeek.Tuesday:
                    ckb = chkTuesday;
                    break;

                case DayOfWeek.Wednesday:
                    ckb = chkWednesday;
                    break;

                case DayOfWeek.Thursday:
                    ckb = chkThursday;
                    break;

                case DayOfWeek.Friday:
                    ckb = chkFriday;
                    break;

                default:
                    ckb = chkSaturday;
                    break;
            }

            // Use default values if not a weekly frequency
            if(recurrence.Frequency != RecurFrequency.Weekly)
            {
                udcWeeks.Value = 1;
                ckb.Checked = true;
            }
            else
            {
                // Any instances on the days are ignored for this frequency
                udcWeeks.Value = (recurrence.Interval < 1000) ? recurrence.Interval : 999;

                if(recurrence.ByDay.Count == 0)
                    ckb.Checked = true;
                else
                    foreach(DayInstance di in recurrence.ByDay)
                        switch(di.DayOfWeek)
                        {
                            case DayOfWeek.Sunday:
                                chkSunday.Checked = true;
                                break;

                            case DayOfWeek.Monday:
                                chkMonday.Checked = true;
                                break;

                            case DayOfWeek.Tuesday:
                                chkTuesday.Checked = true;
                                break;

                            case DayOfWeek.Wednesday:
                                chkWednesday.Checked = true;
                                break;

                            case DayOfWeek.Thursday:
                                chkThursday.Checked = true;
                                break;

                            case DayOfWeek.Friday:
                                chkFriday.Checked = true;
                                break;

                            case DayOfWeek.Saturday:
                                chkSaturday.Checked = true;
                                break;
                        }
            }
        }
        #endregion
    }
}
