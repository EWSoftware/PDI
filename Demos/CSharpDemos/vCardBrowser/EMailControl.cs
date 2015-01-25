//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : EMailControl.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/27/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Visual C#
//
// This is used to edit a vCard's e-mail address information.  It's nothing elaborate but does let you edit the
// collection fairly well.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 11/01/2004  EFW  Created the code
// 04/08/2007  EFW  Updated for use with .NET 2.0
//===============================================================================================================

using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using EWSoftware.PDI.Properties;

namespace vCardBrowser
{
	/// <summary>
	/// A user control for editing vCard e-mail addresses
	/// </summary>
	public partial class EMailControl : EWSoftware.PDI.Windows.Forms.BrowseControl
    {
        #region Private data members
        //=====================================================================

        private static Regex reEMailAddress = new Regex(@"^([a-z0-9_\-])([a-z0-9_\-\.]*)@" +
            @"(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-z0-9\-]+)\.)+))([a-z]" +
            @"{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$",
            RegexOptions.IgnoreCase);

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public EMailControl()
		{
			InitializeComponent();

            // Use a default collection as the data source
            this.BindingSource.DataSource = new EMailPropertyCollection();
        }
        #endregion

        #region Method overrides
        //=====================================================================

        /// <summary>
        /// Enable or disable the controls based on whether or not there are items in the collection
        /// </summary>
        /// <param name="enable">True to enable the controls, false to disable them.</param>
        public override void EnableControls(bool enable)
        {
            // The easiest way to do this is to add a panel control with its Dock property set to Fill.  Place
            // the controls in it and then enable or disable the panel.
            pnlControls.Enabled = enable;

            if(enable)
                txtEMailAddress.Focus();
        }

        /// <summary>
        /// This is overridden to bind the controls to the data source
        /// </summary>
        public override void BindToControls()
        {
            txtEMailAddress.DataBindings.Add("Text", this.BindingSource, "Value");

            // For the checkboxes, the Format and Parse events are needed to get and set the checked state.  We
            // aren't going to check for all of the older less common ones.
            Binding b = new Binding("Checked", this.BindingSource, "EMailTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkAOL.Tag = EMailTypes.AOL;
            chkAOL.DataBindings.Add(b);

            b = new Binding("Checked", this.BindingSource, "EMailTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkInternet.Tag = EMailTypes.Internet;
            chkInternet.DataBindings.Add(b);

            b = new Binding("Checked", this.BindingSource, "EMailTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkX400.Tag = EMailTypes.X400;
            chkX400.DataBindings.Add(b);

            b = new Binding("Checked", this.BindingSource, "EMailTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkPreferred.Tag = EMailTypes.Preferred;
            chkPreferred.DataBindings.Add(b);
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// This is used to set the checked state of an e-mail type checkbox
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void CheckBox_Format(object sender, ConvertEventArgs e)
        {
            CheckBox cb = (CheckBox)((Binding)sender).Control;
            EMailTypes checkType = (EMailTypes)cb.Tag;
            EMailTypes emailTypes = (EMailTypes)e.Value;

            e.Value = (emailTypes & checkType) == checkType;
        }

        /// <summary>
        /// This is used to store the value of the e-mail type checkbox
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void CheckBox_Parse(object sender, ConvertEventArgs e)
        {
            EMailProperty email = (EMailProperty)this.BindingSource.Current;
            CheckBox cb = (CheckBox)((Binding)sender).Control;
            EMailTypes checkType = (EMailTypes)cb.Tag;

            if(cb.Checked)
                email.EMailTypes |= checkType;
            else
                email.EMailTypes &= ~checkType;

            // Only one address can be the preferred address
            if(checkType == EMailTypes.Preferred)
                ((EMailPropertyCollection)this.BindingSource.DataSource).SetPreferred(email);
        }

        /// <summary>
        /// A street address is required
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtEMailAddress_Validating(object sender, CancelEventArgs e)
        {
            this.ErrorProvider.Clear();

            if(!this.DesignMode && ((Control)sender).Enabled && (txtEMailAddress.Text.Trim().Length == 0 ||
              !reEMailAddress.IsMatch(txtEMailAddress.Text)))
            {
                this.ErrorProvider.SetError(txtEMailAddress, "A valid e-mail address is required");
                e.Cancel = true;
            }
        }
        #endregion
    }
}
