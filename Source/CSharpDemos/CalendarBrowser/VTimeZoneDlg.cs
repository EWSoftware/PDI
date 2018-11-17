//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : VTimeZoneDlg.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/29/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Visual C#
//
// This is used to edit a VTimeZone object's properties
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 12/09/2004  EFW  Created the code
// 04/09/2007  EFW  Updated for use with .NET 2.0
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Windows.Forms;

using EWSoftware.PDI.Objects;

namespace CalendarBrowser
{
	/// <summary>
    /// This is used to edit a VTimeZone object's properties
	/// </summary>
	public partial class VTimeZoneDlg : System.Windows.Forms.Form
	{
        #region Private data members
        //=====================================================================

        private string originalId;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public VTimeZoneDlg()
		{
			InitializeComponent();
		}
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// Initialize the dialog controls using the specified VTimeZone object
        /// </summary>
        /// <param name="tz">The time zone object from which to get the settings</param>
        public void SetValues(VTimeZone tz)
        {
            originalId = txtTimeZoneId.Text = tz.TimeZoneId.Value;
            txtTimeZoneUrl.Text = tz.TimeZoneUrl.Value;
            txtLastModified.Text = tz.LastModified.DateTimeValue.ToString("G");

            // We could bind directly to the existing collection but that would modify it.  To preserve the
            // original items, we'll pass a copy of the collection instead.
            ucRules.BindingSource.DataSource = new ObservanceRuleCollection().CloneRange(tz.ObservanceRules);
        }

        /// <summary>
        /// Update the time zone object with the dialog control values
        /// </summary>
        /// <param name="tz">The time zone object in which the settings are updated</param>
        public void GetValues(VTimeZone tz)
        {
            tz.TimeZoneId.Value = txtTimeZoneId.Text;
            tz.TimeZoneUrl.Value = txtTimeZoneUrl.Text;
            tz.LastModified.DateTimeValue = DateTime.Now;

            // For the collection, we'll clear the existing items and copy the modified items from the browse
            // control binding source.
            tz.ObservanceRules.Clear();
            tz.ObservanceRules.CloneRange((ObservanceRuleCollection)ucRules.BindingSource.DataSource);
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Validate the information on exit if OK was clicked.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void VTimeZoneDlg_Closing(object sender, CancelEventArgs e)
        {
            // Ignore on cancel
            if(this.DialogResult == DialogResult.Cancel)
                return;

            epErrors.Clear();
            ucRules.BindingSource.EndEdit();

            if(txtTimeZoneId.Text.Trim().Length == 0)
            {
                epErrors.SetError(txtTimeZoneId, "A time zone ID is required");
                e.Cancel = true;
                txtTimeZoneId.Focus();
            }
            else
            {
                // Disallow duplicate time zone IDs
                if(originalId == null || txtTimeZoneId.Text != originalId)
                    if(VCalendar.TimeZones[txtTimeZoneId.Text] != null)
                    {
                        epErrors.SetError(txtTimeZoneId, "A time zone with the specified ID already exists");
                        e.Cancel = true;
                        txtTimeZoneId.Focus();
                    }
            }
        }
        #endregion
    }
}
