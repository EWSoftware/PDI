//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : LabelControl.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/05/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
// Compiler: Visual C#
//
// This is used to edit a vCard's label information.  It's nothing elaborate but does let you edit the collection
// fairly well.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 11/05/2004  EFW  Created the code
// 04/08/2007  EFW  Updated for use with .NET 2.0
//===============================================================================================================

using System.ComponentModel;
using System.Windows.Forms;

using EWSoftware.PDI.Properties;

namespace vCardBrowser
{
	/// <summary>
	/// A user control for editing vCard mailing labels
	/// </summary>
	public partial class LabelControl : EWSoftware.PDI.Windows.Forms.BrowseControl
	{
        #region Constructor
        //=====================================================================

		public LabelControl()
		{
			InitializeComponent();

            // Use a default collection as the data source
            this.BindingSource.DataSource = new LabelPropertyCollection();
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
                txtLabelText.Focus();
        }

        /// <summary>
        /// This is overridden to bind the controls to the data source
        /// </summary>
        public override void BindToControls()
        {
            txtLabelText.DataBindings.Add("Text", this.BindingSource, "Value");

            // For the checkboxes, the Format and Parse events are needed to get and set the checked state
            Binding b = new("Checked", this.BindingSource, "AddressTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkDomestic.Tag = AddressTypes.Domestic;
            chkDomestic.DataBindings.Add(b);

            b = new("Checked", this.BindingSource, "AddressTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkInternational.Tag = AddressTypes.International;
            chkInternational.DataBindings.Add(b);

            b = new("Checked", this.BindingSource, "AddressTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkPostal.Tag = AddressTypes.Postal;
            chkPostal.DataBindings.Add(b);

            b = new("Checked", this.BindingSource, "AddressTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkParcel.Tag = AddressTypes.Parcel;
            chkParcel.DataBindings.Add(b);

            b = new("Checked", this.BindingSource, "AddressTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkHome.Tag = AddressTypes.Home;
            chkHome.DataBindings.Add(b);

            b = new("Checked", this.BindingSource, "AddressTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkWork.Tag = AddressTypes.Work;
            chkWork.DataBindings.Add(b);

            b = new("Checked", this.BindingSource, "AddressTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkPreferred.Tag = AddressTypes.Preferred;
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
        private void CheckBox_Format(object? sender, ConvertEventArgs e)
        {
            CheckBox cb = (CheckBox)((Binding)sender!).Control;
            AddressTypes checkType = (AddressTypes)cb.Tag!;
            AddressTypes addrTypes = (AddressTypes)e.Value!;

            e.Value = (addrTypes & checkType) == checkType;
        }

        /// <summary>
        /// This is used to store the value of the address type checkbox
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void CheckBox_Parse(object? sender, ConvertEventArgs e)
        {
            LabelProperty l = (LabelProperty)this.BindingSource.Current;
            CheckBox cb = (CheckBox)((Binding)sender!).Control;
            AddressTypes checkType = (AddressTypes)cb.Tag!;

            if(cb.Checked)
                l.AddressTypes |= checkType;
            else
                l.AddressTypes &= ~checkType;

            // Only one address can be the preferred address
            if(checkType == AddressTypes.Preferred)
                ((LabelPropertyCollection)this.BindingSource.DataSource).SetPreferred(l);
        }

        /// <summary>
        /// A label is required
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtLabelText_Validating(object sender, CancelEventArgs e)
        {
            this.ErrorProvider.Clear();

            if(!this.DesignMode && ((Control)sender).Enabled && txtLabelText.Text.Trim().Length == 0)
            {
                this.ErrorProvider.SetError(txtLabelText, "Label text is required");
                e.Cancel = true;
            }
        }
        #endregion
    }
}
