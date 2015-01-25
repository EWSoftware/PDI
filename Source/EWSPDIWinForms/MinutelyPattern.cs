//===============================================================================================================
// System  : EWSoftware.PDI Windows Forms Controls
// File    : MinutelyPattern.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 10/21/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains one of several user controls that are combined to allow the editing of various recurrence
// parameters.  This one is used to specify the settings for an minutely recurrence pattern.
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

using System.ComponentModel;

namespace EWSoftware.PDI.Windows.Forms
{
	/// <summary>
    /// This user control is used to specify the settings for a minutely recurrence pattern
	/// </summary>
    [ToolboxItem(false)]
	internal sealed partial class MinutelyPattern : System.Windows.Forms.UserControl
	{
        #region Constructor
        //=====================================================================

		/// <summary>
		/// Constructor
		/// </summary>
        public MinutelyPattern()
		{
			InitializeComponent();
		}
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// This is called to get the values from the controls and set them in the specified recurrence object
        /// </summary>
        /// <param name="rRecur">The recurrence object to which the settings are applied</param>
        /// <remarks>It is assumed that the recurrence object has been reset to a default state</remarks>
        public void GetValues(Recurrence rRecur)
        {
            if(rRecur.Frequency == RecurFrequency.Minutely)
                rRecur.Interval = (int)udcMinutes.Value;
        }

        /// <summary>
        /// This is called to set the values for the controls based on the current recurrence settings
        /// </summary>
        /// <param name="rRecur">The recurrence object from which to get the settings</param>
        public void SetValues(Recurrence rRecur)
        {
            if(rRecur.Frequency == RecurFrequency.Minutely)
                udcMinutes.Value = (rRecur.Interval < 1000) ? rRecur.Interval : 999;
            else
                udcMinutes.Value = 1;
        }
        #endregion
    }
}
