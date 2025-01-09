//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : TimeZoneListDlg.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/05/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This is used to edit view and edit time zones and apply them to the calendar
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 01/07/2004  EFW  Created the code
// 04/09/2007  EFW  Updated for use with .NET 2.0
//===============================================================================================================

using System;
using System.Globalization;
using System.Windows.Forms;

using EWSoftware.PDI;
using EWSoftware.PDI.Objects;

namespace CalendarBrowser
{
	/// <summary>
	/// This form is used for editing time zone information.
	/// </summary>
	public partial class TimeZoneListDlg : Form
	{
        #region Private data members
        //=====================================================================

        private readonly VTimeZoneCollection timeZones;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// Get or set the currently loaded calendar
        /// </summary>
        public VCalendar? CurrentCalendar { get; set; }

        /// <summary>
        /// Set or get the modified state
        /// </summary>
        public bool Modified { get; set; }

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public TimeZoneListDlg()
		{
			InitializeComponent();

            dgvCalendar.AutoGenerateColumns = false;

            tbcLastModified.DefaultCellStyle.Format =
                CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " +
                CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern;

            timeZones = [];
            LoadGridWithItems();
		}
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// Load the grid with the specified calendar items
        /// </summary>
        private void LoadGridWithItems()
        {
            int gridIdx = dgvCalendar.CurrentCellAddress.Y;

            VCalendar.TimeZones.Sort(true);
            dgvCalendar.DataSource = null;

            // Get just the time zones used?
            if(chkLimitToCalendar.Checked)
            {
                // Get just the time zones used
                StringCollection timeZonesUsed = [];

                if(this.CurrentCalendar != null)
                {
                    this.CurrentCalendar.TimeZonesUsed(timeZonesUsed);

                    // Remove entries that don't exist
                    for(int idx = 0; idx < timeZonesUsed.Count; idx++)
                    {
                        if(VCalendar.TimeZones[timeZonesUsed[idx]] == null)
                        {
                            timeZonesUsed.RemoveAt(idx);
                            idx--;
                        }
                    }
                }

                // Add each instance to a temporary collection and bind it to the grid
                timeZones.Clear();

                foreach(string timeZoneId in timeZonesUsed)
                    timeZones.Add(VCalendar.TimeZones[timeZoneId]!);

                dgvCalendar.DataSource = timeZones;
            }
            else
                dgvCalendar.DataSource = VCalendar.TimeZones;

            // Enable or disable the buttons based on the vCard count
            btnEdit.Enabled = btnDelete.Enabled = (dgvCalendar.RowCount != 0);

            // Stay on the last item selected
            if(gridIdx > -1 && gridIdx < dgvCalendar.RowCount)
                dgvCalendar.CurrentCell = dgvCalendar[0, gridIdx];
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Close the dialog box
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Toggle the display of all time zones or just those in the currently loaded calendar
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void chkLimitToCalendar_CheckedChanged(object sender, EventArgs e)
        {
            LoadGridWithItems();
        }

        /// <summary>
        /// Add a new time zone component
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using var dlg = new VTimeZoneDlg();
            
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                VTimeZone tz = new();
                dlg.GetValues(tz);

                VCalendar.TimeZones.Add(tz);

                this.Modified = true;
                this.LoadGridWithItems();
            }
        }

        /// <summary>
        /// Edit a time zone component
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(dgvCalendar.CurrentCellAddress.Y == -1)
            {
                MessageBox.Show("Please select an item to edit", "No Item", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            using var dlg = new VTimeZoneDlg();
            
            string timeZoneId = (string)dgvCalendar[0, dgvCalendar.CurrentCellAddress.Y].Value;
            dlg.SetValues(VCalendar.TimeZones[timeZoneId]!);

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                dlg.GetValues(VCalendar.TimeZones[timeZoneId]!);

                this.Modified = true;
                this.LoadGridWithItems();
            }
        }

        /// <summary>
        /// Delete a time zone component
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dgvCalendar.CurrentCellAddress.Y == -1)
            {
                MessageBox.Show("Please select an item to delete", "No Item", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            StringCollection timeZonesUsed = [];

            string timeZoneId = (string)dgvCalendar[0, dgvCalendar.CurrentCellAddress.Y].Value;

            this.CurrentCalendar?.TimeZonesUsed(timeZonesUsed);

            if(timeZonesUsed.Contains(timeZoneId))
            {
                MessageBox.Show("The time zone is in use and cannot be deleted", "Time Zone In Use",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if(MessageBox.Show("Are you sure you want to delete the selected item?", "Delete Item",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                VCalendar.TimeZones.Remove(VCalendar.TimeZones[timeZoneId]!);

                this.Modified = true;
                this.LoadGridWithItems();
            }
        }

        /// <summary>
        /// Apply the selected time zone to all items in the calendar.  This will convert all date/time values in
        /// the calendar from their current time zone to the selected time zone.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            if(dgvCalendar.CurrentCellAddress.Y == -1)
            {
                MessageBox.Show("Please select an item to use", "No Item", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            if(MessageBox.Show("Are you sure you want to apply the selected time zone to all items in the " +
              "calendar?", "Apply Time Zone", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
              MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                string timeZoneId = (string)dgvCalendar[0, dgvCalendar.CurrentCellAddress.Y].Value;

                this.CurrentCalendar?.ApplyTimeZone(VCalendar.TimeZones[timeZoneId]);

                this.Modified = true;
            }
        }
        #endregion
    }
}
