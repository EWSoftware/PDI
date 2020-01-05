//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : PhoneControl.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/01/2020
// Note    : Copyright 2004-2020, Eric Woodruff, All rights reserved
//
// This is used to edit a vCard's phone information.  It's nothing elaborate but does let you edit the collection
// fairly well.
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

using EWSoftware.PDI.Properties;

namespace vCardBrowser
{
	/// <summary>
	/// A user control for editing vCard phone numbers
	/// </summary>
	public partial class PhoneControl : EWSoftware.PDI.Windows.Forms.BrowseControl
    {
        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public PhoneControl()
		{
			InitializeComponent();

            // Use a default collection as the data source
            this.BindingSource.DataSource = new TelephonePropertyCollection();
        }
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// Set the control states based on the vCard version
        /// </summary>
        /// <param name="version">The vCard version</param>
        public void SetControlStateBasedOnVersion(SpecificationVersions version)
        {
            chkPreferred.Visible = (version != SpecificationVersions.vCard40);
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
                txtPhoneNumber.Focus();
        }

        /// <summary>
        /// This is overridden to bind the controls to the data source
        /// </summary>
        public override void BindToControls()
        {
            txtPhoneNumber.DataBindings.Add("Text", this.BindingSource, "Value");

            // For the checkboxes, the Format and Parse events are needed to get and set the checked state.  We
            // aren't going to check for a few of the less common ones.
            Binding b = new Binding("Checked", this.BindingSource, "PhoneTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkWork.Tag = PhoneTypes.Work;
            chkWork.DataBindings.Add(b);

            b = new Binding("Checked", this.BindingSource, "PhoneTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkHome.Tag = PhoneTypes.Home;
            chkHome.DataBindings.Add(b);

            b = new Binding("Checked", this.BindingSource, "PhoneTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkVoice.Tag = PhoneTypes.Voice;
            chkVoice.DataBindings.Add(b);

            b = new Binding("Checked", this.BindingSource, "PhoneTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkFax.Tag = PhoneTypes.Fax;
            chkFax.DataBindings.Add(b);

            b = new Binding("Checked", this.BindingSource, "PhoneTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkMessage.Tag = PhoneTypes.Message;
            chkMessage.DataBindings.Add(b);

            b = new Binding("Checked", this.BindingSource, "PhoneTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkCell.Tag = PhoneTypes.Cell;
            chkCell.DataBindings.Add(b);

            b = new Binding("Checked", this.BindingSource, "PhoneTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkPager.Tag = PhoneTypes.Pager;
            chkPager.DataBindings.Add(b);

            b = new Binding("Checked", this.BindingSource, "PhoneTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkPreferred.Tag = PhoneTypes.Preferred;
            chkPreferred.DataBindings.Add(b);
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// This is used to set the checked state of an address type checkbox
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void CheckBox_Format(object sender, ConvertEventArgs e)
        {
            CheckBox cb = (CheckBox)((Binding)sender).Control;
            PhoneTypes checkType = (PhoneTypes)cb.Tag;
            PhoneTypes addrTypes = (PhoneTypes)e.Value;

            e.Value = (addrTypes & checkType) == checkType;
        }

        /// <summary>
        /// This is used to store the value of the address type checkbox
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void CheckBox_Parse(object sender, ConvertEventArgs e)
        {
            TelephoneProperty t = (TelephoneProperty)this.BindingSource.Current;
            CheckBox cb = (CheckBox)((Binding)sender).Control;
            PhoneTypes checkType = (PhoneTypes)cb.Tag;

            if(cb.Checked)
                t.PhoneTypes |= checkType;
            else
                t.PhoneTypes &= ~checkType;

            // Only one address can be the preferred address
            if(checkType == PhoneTypes.Preferred)
                ((TelephonePropertyCollection)this.BindingSource.DataSource).SetPreferred(t);
        }

        /// <summary>
        /// A street address is required
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtPhoneNumber_Validating(object sender, CancelEventArgs e)
        {
            this.ErrorProvider.Clear();

            if(!this.DesignMode && ((Control)sender).Enabled && txtPhoneNumber.Text.Trim().Length == 0)
            {
                this.ErrorProvider.SetError(txtPhoneNumber, "A phone number is required");
                e.Cancel = true;
            }
        }
        #endregion
    }
}
