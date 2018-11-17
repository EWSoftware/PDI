//===============================================================================================================
// File    : AboutDlg.cs
// Author  : Eric Woodruff
// Updated : 12/27/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This form is used to display application version information
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 11/10/2003  EFW  Created the code
//===============================================================================================================

using System;
using System.Reflection;
using System.Windows.Forms;

namespace CalendarBrowser
{
    public partial class AboutDlg : System.Windows.Forms.Form
    {
        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public AboutDlg()
        {
            InitializeComponent();
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Set the version and assembly info on load
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void AboutDlg_Load(object sender, EventArgs e)
        {
            // Get assembly information not available from the application object
            Assembly asm = Assembly.GetEntryAssembly();
            AssemblyTitleAttribute title = (AssemblyTitleAttribute)
                AssemblyTitleAttribute.GetCustomAttribute(asm, typeof(AssemblyTitleAttribute));
            AssemblyCopyrightAttribute copyright = (AssemblyCopyrightAttribute)
                AssemblyCopyrightAttribute.GetCustomAttribute(asm, typeof(AssemblyCopyrightAttribute));
            AssemblyDescriptionAttribute desc = (AssemblyDescriptionAttribute)
                AssemblyDescriptionAttribute.GetCustomAttribute(asm, typeof(AssemblyDescriptionAttribute));

            // Set the labels
            lblName.Text = title.Title;
            lblDescription.Text = desc.Description;
            lblVersion.Text = "Version: " + Application.ProductVersion;
            lblCopyright.Text = copyright.Copyright;

            // Display components used by this assembly sorted by name
            foreach(AssemblyName an in asm.GetReferencedAssemblies())
            {
                ListViewItem lvi = lvComponents.Items.Add(an.Name);
                lvi.SubItems.Add(an.Version.ToString());
            }

            lvComponents.Sorting = SortOrder.Ascending;
            lvComponents.Sort();

            // Set e-mail link
            lnkHelp.Links[0].LinkData = "mailto:" + lnkHelp.Text + "?Subject=EWSoftware CalendarBrowser Demo";
        }

        /// <summary>
        /// View system information
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnSysInfo_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("MSInfo32.exe");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unable to launch system information viewer", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                // Log the exception to the debugger for the developer
                System.Diagnostics.Debug.Write(ex.ToString());
            }
        }

        /// <summary>
        /// Attempt to send an e-mail
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void lnkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // Launch the e-mail URL, this will fail if user does not have an association for e-mail URLs
                System.Diagnostics.Process.Start((string)e.Link.LinkData);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unable to launch e-mail editor", "E-Mail Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                // Log the exception to the debugger for the developer
                System.Diagnostics.Debug.Write(ex.ToString());
            }
        }
        #endregion
    }
}
