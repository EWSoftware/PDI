//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : MainForm.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/05/2025
// Note    : Copyright 2003-2025, Eric Woodruff, All rights reserved
//
// This application is used to demonstrate various features of the EWSoftware PDI classes
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

namespace PDIWinFormsTest
{
	/// <summary>
	/// This implements the main menu form and the application entry point
	/// </summary>
	public partial class MainForm : System.Windows.Forms.Form
	{
        #region Main program entry point
        //=====================================================================

        /// <summary>
        /// The main entry point for the application
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public MainForm()
		{
			InitializeComponent();
		}
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Exit the application
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Test the holiday classes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnHolidays_Click(object sender, EventArgs e)
        {
            using var dlg = new HolidayTestForm();
            
            dlg.ShowDialog();
        }

        /// <summary>
        /// Test the recurrence class
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnRRULE_Click(object sender, EventArgs e)
        {
            using var dlg = new RRuleTestForm();
            
            dlg.ShowDialog();
        }

        /// <summary>
        /// Test iCalendar recurrence
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnTestCalRecur_Click(object sender, EventArgs e)
        {
            using var dlg = new EventRecurTestForm();
            
            dlg.ShowDialog();
        }

        /// <summary>
        /// Test the VTIMEZONE classes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnTestVTimeZone_Click(object sender, EventArgs e)
        {
            using var dlg = new VTimeZoneTestForm();
            
            dlg.ShowDialog();
        }

        /// <summary>
        /// View application copyright and version information
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnAbout_Click(object sender, EventArgs e)
        {
            using var dlg = new AboutDlg();
            
            dlg.ShowDialog();
        }
        #endregion
    }
}
