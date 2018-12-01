//===============================================================================================================
// System  : EWSoftware.PDI Windows Forms Controls
// File    : RecurrencePropertiesDlg.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/23/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Visual C#
//
// This is used to add or edit recurrence object information
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 11/07/2004  EFW  Created the code
//===============================================================================================================

using System;

namespace EWSoftware.PDI.Windows.Forms
{
	/// <summary>
	/// This form is used to add or edit recurrence object information
	/// </summary>
	public partial class RecurrencePropertiesDlg : System.Windows.Forms.Form
	{
        #region Properties
        //=====================================================================

        /// <summary>
        /// This property is used to get or set whether or not the Week Start Day option is displayed.  It
        /// is visible by default.
        /// </summary>
        /// <remarks>If hidden, the Week Start Day combo box will not be visible and a default week start day
        /// will be used in all recurrences edited by the control.</remarks>
        public bool ShowWeekStartDay
        {
            get => rpRecurrence.ShowWeekStartDay;
            set => rpRecurrence.ShowWeekStartDay = value;
        }

        /// <summary>
        /// This property is used to get or set whether or not the "Can Occur On Holiday" option is displayed.
        /// It is visible by default.
        /// </summary>
        /// <remarks>If hidden, the check box will not be visible and the option will be set to true in all
        /// recurrences edited by the control.</remarks>
        public bool ShowCanOccurOnHoliday
        {
            get => rpRecurrence.ShowCanOccurOnHoliday;
            set => rpRecurrence.ShowCanOccurOnHoliday = value;
        }

        /// <summary>
        /// This property is used to get or set whether or not the "Advanced" checkbox is visible and thus give
        /// access to the advanced pattern options.  It is visible by default.
        /// </summary>
        /// <remarks>If hidden, only basic patterns similar to those in Microsoft Outlook can be created.  When
        /// edited, all advanced options currently in effect will be lost and the pattern will be made to conform
        /// to the simple options available for the currently selected frequency.</remarks>
        public bool ShowAdvanced
        {
            get => rpRecurrence.ShowAdvanced;
            set => rpRecurrence.ShowAdvanced = value;
        }

        /// <summary>
        /// This property is used to get or set the maximum pattern option to display.  All pattern options will
        /// be visible by default.
        /// </summary>
        /// <remarks>If set to <c>Daily</c>, only basic patterns similar to those in Microsoft Outlook can be
        /// created (yearly, monthly, weekly, and daily).  If set to a recurrence with a pattern higher than the
        /// maximum, all options currently in effect will be lost and the pattern will be made to conform to the
        /// simple pattern for the maximum allowed pattern.</remarks>
        public RecurFrequency MaximumPattern
        {
            get => rpRecurrence.MaximumPattern;
            set => rpRecurrence.MaximumPattern = value;
        }

        /// <summary>
        /// This property is used to get or set whether or not the time value on the "End by Date" option is
        /// visible and can be set.
        /// </summary>
        /// <remarks>If visible (the default), the end time can also be set for the "End by Date" option.  If not
        /// visible, the time value on the "End by Date" option will always be 12:00am.</remarks>
        public bool ShowEndTime
        {
            get => rpRecurrence.ShowEndTime;
            set => rpRecurrence.ShowEndTime = value;
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public RecurrencePropertiesDlg()
        {
            InitializeComponent();
        }
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// This is used to retrieve the recurrence information into the passed recurrence object
        /// </summary>
        /// <param name="recurrence">The recurrence in which to store the settings.</param>
        /// <exception cref="ArgumentNullException">This is thrown if the passed recurrence object is null</exception>
        public void GetRecurrence(Recurrence recurrence)
        {
            if(recurrence == null)
                throw new ArgumentNullException(nameof(recurrence), LR.GetString("ExRPRecurrenceIsNull"));

            rpRecurrence.GetRecurrence(recurrence);
        }

        /// <summary>
        /// This is used to initialize the dialog box with settings from an existing recurrence object
        /// </summary>
        /// <param name="recurrence">The recurrence from which to get the settings.  If null, it uses a default
        /// daily recurrence pattern.</param>
        public void SetRecurrence(Recurrence recurrence)
        {
            rpRecurrence.SetRecurrence(recurrence);
        }
        #endregion
    }
}
