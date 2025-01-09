//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : RequestStatusControl.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/05/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This is used to edit a calendar object's request status collection
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 12/14/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Windows.Forms;

using EWSoftware.PDI.Properties;

namespace CalendarBrowser
{
	/// <summary>
	/// This is used to edit the Request Status property collection
	/// </summary>
	public partial class RequestStatusControl : EWSoftware.PDI.Windows.Forms.BrowseControl
	{
        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public RequestStatusControl()
		{
			InitializeComponent();

            // Use a default collection as the data source
            this.BindingSource.DataSource = new RequestStatusPropertyCollection();
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
            txtStatusCode.Enabled = txtMessage.Enabled = txtExtData.Enabled = enable;

            if(enable)
                txtStatusCode.Focus();
        }

        /// <summary>
        /// This is overridden to bind the controls to the data source
        /// </summary>
        public override void BindToControls()
        {
            txtStatusCode.DataBindings.Add("Text", this.BindingSource, "StatusCode");
            txtMessage.DataBindings.Add("Text", this.BindingSource, "StatusMessage");
            txtExtData.DataBindings.Add("Text", this.BindingSource, "ExtendedData");
        }
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// Validate the items in the collection
        /// </summary>
        /// <returns>Returns true if all are valid or false if any item is not valid.  The position is set to the
        /// first invalid item.</returns>
        /// <remarks>The navigator doesn't allow for validation of all fields so we have to do some checking
        /// before saving the results.</remarks>
        public bool ValidateItems()
        {
            this.ErrorProvider.Clear();

            RequestStatusPropertyCollection requests = (RequestStatusPropertyCollection)this.BindingSource.DataSource;

            foreach(RequestStatusProperty status in requests)
            {
                if(String.IsNullOrWhiteSpace(status.StatusMessage))
                {
                    this.BindingSource.Position = requests.IndexOf(status);
                    txtMessage.Focus();
                    txtMessage_Validating(txtMessage, new CancelEventArgs());
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// A status code is required
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtStatusCode_Validating(object sender, CancelEventArgs e)
        {
            this.ErrorProvider.Clear();

            if(!this.DesignMode && ((Control)sender).Enabled && txtStatusCode.Text.Trim().Length == 0)
            {
                this.ErrorProvider.SetError(txtStatusCode, "A status code is required");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// A message is required
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtMessage_Validating(object sender, CancelEventArgs e)
        {
            this.ErrorProvider.Clear();

            if(!this.DesignMode && ((Control)sender).Enabled && txtMessage.Text.Length == 0)
            {
                this.ErrorProvider.SetError(txtMessage, "A status message is required");
                e.Cancel = true;
            }
        }
        #endregion
    }
}
