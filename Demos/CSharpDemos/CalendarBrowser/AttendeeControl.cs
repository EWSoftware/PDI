//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : AttendeeControl.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/29/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Visual C#
//
// This is used to edit a calendar object's attendee collection
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 12/14/2004  EFW  Created the code
// 04/18/2007  EFW  Converted for use with .NET 2.0
//===============================================================================================================

using System.ComponentModel;
using System.Windows.Forms;

using EWSoftware.PDI.Properties;

namespace CalendarBrowser
{
    /// <summary>
    /// This is used to edit the Attendees property collection
    /// </summary>
    public partial class AttendeeControl : EWSoftware.PDI.Windows.Forms.BrowseControl
    {
        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public AttendeeControl()
        {
            InitializeComponent();

            // Use a default collection as the data source
            this.BindingSource.DataSource = new AttendeePropertyCollection();
        }
        #endregion

        #region Method overrides
        //=====================================================================

        /// <summary>
        /// Enable or disable the controls based on whether or not there are items in the collection
        /// </summary>
        /// <param name="enable">True to enable the controls, false to disable them</param>
        public override void EnableControls(bool enable)
        {
            // The easiest way to do this is to add a panel control with its Dock property set to Fill.  Place
            // the controls in it and then enable or disable the panel.
            pnlControls.Enabled = enable;

            if(enable)
                txtAttendee.Focus();
        }

        /// <summary>
        /// This is overridden to bind the controls to the data source
        /// </summary>
        public override void BindToControls()
        {
            txtAttendee.DataBindings.Add("Text", this.BindingSource, "Value");
            txtCommonName.DataBindings.Add("Text", this.BindingSource, "CommonName");
            txtUserType.DataBindings.Add("Text", this.BindingSource, "CalendarUserType");
            txtSentBy.DataBindings.Add("Text", this.BindingSource, "SentBy");
            chkRSVP.DataBindings.Add("Checked", this.BindingSource, "Rsvp");

            // For the combo boxes, we'll handle unknown values too via the binding events
            Binding b = new Binding("SelectedIndex", this.BindingSource, "Role");
            b.Format += RoleStatus_Format;
            b.Parse += RoleStatus_Parse;
            cboRole.DataBindings.Add(b);

            b = new Binding("SelectedIndex", this.BindingSource, "ParticipationStatus");
            b.Format += RoleStatus_Format;
            b.Parse += RoleStatus_Parse;
            cboStatus.DataBindings.Add(b);
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// This handles adding unknown role and status values to the combo boxes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void RoleStatus_Format(object sender, ConvertEventArgs e)
        {
            ComboBox c = (ComboBox)(((Binding)sender).Control);
            int idx;

            if(e.Value == null)
                e.Value = 0;
            else
            {
                idx = c.Items.IndexOf(e.Value);

                if(idx == -1)
                    idx = c.Items.Add(e.Value);

                e.Value = idx;
            }
        }

        /// <summary>
        /// This handles converting from the selected index to a value
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void RoleStatus_Parse(object sender, ConvertEventArgs e)
        {
            ComboBox c = (ComboBox)(((Binding)sender).Control);

            if(c.SelectedIndex != -1)
                e.Value = c.Items[c.SelectedIndex];
            else
                e.Value = null;
        }

        /// <summary>
        /// An attendee value is required
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtAttendee_Validating(object sender, CancelEventArgs e)
        {
            this.ErrorProvider.Clear();

            if(!this.DesignMode && ((Control)sender).Enabled && txtAttendee.Text.Trim().Length == 0)
            {
                this.ErrorProvider.SetError(txtAttendee, "An attendee value is required");
                e.Cancel = true;
            }
        }
        #endregion
    }
}
