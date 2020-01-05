//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : AddressControl.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/02/2020
// Note    : Copyright 2004-2020, Eric Woodruff, All rights reserved
//
// This is used to edit a vCard's address information.  It's nothing elaborate but does let you edit the
// collection fairly well.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 10/31/2004  EFW  Created the code
// 04/08/2007  EFW  Updated for use with .NET 2.0
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Windows.Forms;

using EWSoftware.PDI.Properties;

namespace vCardBrowser
{
    /// <summary>
    /// A user control for editing vCard mailing addresses
    /// </summary>
    public partial class AddressControl : EWSoftware.PDI.Windows.Forms.BrowseControl
    {
        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public AddressControl()
        {
            InitializeComponent();

            // Use a default collection as the data source
            this.BindingSource.DataSource = new AddressPropertyCollection();
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
            chkInternational.Visible = chkDomestic.Visible = chkPostal.Visible = chkParcel.Visible =
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
                txtStreetAddress.Focus();
        }

        /// <summary>
        /// This is overridden to bind the controls to the data source
        /// </summary>
        public override void BindToControls()
        {
            txtStreetAddress.DataBindings.Add("Text", this.BindingSource, "StreetAddress");
            txtExtendedAddress.DataBindings.Add("Text", this.BindingSource, "ExtendedAddress");
            txtPOBox.DataBindings.Add("Text", this.BindingSource, "POBox");
            txtLocality.DataBindings.Add("Text", this.BindingSource, "Locality");
            txtRegion.DataBindings.Add("Text", this.BindingSource, "Region");
            txtPostalCode.DataBindings.Add("Text", this.BindingSource, "PostalCode");
            txtCountry.DataBindings.Add("Text", this.BindingSource, "Country");

            // For the checkboxes, the Format and Parse events are needed to get and set the checked state
            Binding b = new Binding("Checked", this.BindingSource, "AddressTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkDomestic.Tag = AddressTypes.Domestic;
            chkDomestic.DataBindings.Add(b);

            b = new Binding("Checked", this.BindingSource, "AddressTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkInternational.Tag = AddressTypes.International;
            chkInternational.DataBindings.Add(b);

            b = new Binding("Checked", this.BindingSource, "AddressTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkPostal.Tag = AddressTypes.Postal;
            chkPostal.DataBindings.Add(b);

            b = new Binding("Checked", this.BindingSource, "AddressTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkParcel.Tag = AddressTypes.Parcel;
            chkParcel.DataBindings.Add(b);

            b = new Binding("Checked", this.BindingSource, "AddressTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkHome.Tag = AddressTypes.Home;
            chkHome.DataBindings.Add(b);

            b = new Binding("Checked", this.BindingSource, "AddressTypes");
            b.Format += CheckBox_Format;
            b.Parse += CheckBox_Parse;
            chkWork.Tag = AddressTypes.Work;
            chkWork.DataBindings.Add(b);

            b = new Binding("Checked", this.BindingSource, "AddressTypes");
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
        private void CheckBox_Format(object sender, ConvertEventArgs e)
        {
            CheckBox cb = (CheckBox)((Binding)sender).Control;
            AddressTypes checkType = (AddressTypes)cb.Tag;
            AddressTypes addrTypes = (AddressTypes)e.Value;

            e.Value = (addrTypes & checkType) == checkType;
        }

        /// <summary>
        /// This is used to store the value of the address type checkbox
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void CheckBox_Parse(object sender, ConvertEventArgs e)
        {
            AddressProperty a = (AddressProperty)this.BindingSource.Current;
            CheckBox cb = (CheckBox)((Binding)sender).Control;
            AddressTypes checkType = (AddressTypes)cb.Tag;

            if(cb.Checked)
                a.AddressTypes |= checkType;
            else
                a.AddressTypes &= ~checkType;

            // Only one address can be the preferred address
            if(checkType == AddressTypes.Preferred)
                ((AddressPropertyCollection)this.BindingSource.DataSource).SetPreferred(a);
        }

        /// <summary>
        /// A street address is required
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtStreetAddress_Validating(object sender, CancelEventArgs e)
        {
            this.ErrorProvider.Clear();

            if(!this.DesignMode && ((Control)sender).Enabled && txtStreetAddress.Text.Trim().Length == 0)
            {
                this.ErrorProvider.SetError(txtStreetAddress, "A street address is required");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// View a map of the address if possible using Google Maps
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void btnMap_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder("https://www.google.com/maps/place/", 512);

            if(txtStreetAddress.Text.Length != 0)
            {
                sb.Append(HttpUtility.UrlEncode(txtStreetAddress.Text));
                sb.Append('+');
            }

            if(txtLocality.Text.Length != 0)
            {
                sb.Append(HttpUtility.UrlEncode(txtLocality.Text));
                sb.Append('+');
            }

            if(txtRegion.Text.Length != 0)
            {
                sb.Append(HttpUtility.UrlEncode(txtRegion.Text));
                sb.Append('+');
            }

            if(txtPostalCode.Text.Length != 0)
                sb.Append(HttpUtility.UrlEncode(txtPostalCode.Text));

            if(sb[sb.Length - 1] == '+')
                sb.Remove(sb.Length - 1, 1);

            try
            {
                System.Diagnostics.Process.Start(sb.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unable to start web browser", "Launch Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                // Log the exception to the debugger for the developer
                System.Diagnostics.Debug.Write(ex.ToString());
            }
        }
        #endregion
    }
}
