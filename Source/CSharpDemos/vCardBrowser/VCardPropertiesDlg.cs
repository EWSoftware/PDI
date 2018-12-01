//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : VCardPropertiesDlg.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/23/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Visual C#
//
// This is used to edit a vCard's properties.  It supports most of the common properties of the vCard including
// photo, logo, and sound.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 10/15/2004  EFW  Created the code
// 04/09/2007  EFW  Updated for use with .NET 2.0
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

using EWSoftware.PDI;
using EWSoftware.PDI.Objects;
using EWSoftware.PDI.Properties;

namespace vCardBrowser
{
	/// <summary>
	/// This dialog box is used to edit a vCard's properties
	/// </summary>
	public partial class VCardPropertiesDlg : System.Windows.Forms.Form
	{
        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public VCardPropertiesDlg()
		{
			InitializeComponent();

            cboVersion.SelectedIndex = 1;

            // Set the short date/long time pattern based on the current culture
            dtpBirthDate.CustomFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " +
                CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;
		}
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Enable and disable fields based on the version chosen.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void cboVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCategories.Enabled = txtClass.Enabled = txtNickname.Enabled =
                txtSortString.Enabled = (cboVersion.SelectedIndex == 1);
        }

        /// <summary>
        /// View the geocoding coordinates using Google Maps if possible
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            string url = String.Format("https://www.google.com/maps/place/{0},{1}", txtLatitude.Text,
                txtLongitude.Text);

            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unable to start web browser", "Launch Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                // Log the exception to the debugger for the developer
                System.Diagnostics.Debug.Write(ex.ToString());
            }
        }

        /// <summary>
        /// Launch a web browser to display the web page
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void btnWebPage_Click(object sender, EventArgs e)
        {
            if(txtWebPage.Text.Trim().Length == 0)
            {
                MessageBox.Show("Enter a web page URL first", "No URL", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    System.Diagnostics.Process.Start(txtWebPage.Text);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Unable to start web browser for this URL", "URL Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);

                    // Log the exception to the debugger for the developer
                    System.Diagnostics.Debug.Write(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Validate the information on exit if OK was clicked
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void VCardPropertiesDlg_Closing(object sender, CancelEventArgs e)
        {
            // Ignore on cancel
            if(this.DialogResult == DialogResult.Cancel)
                return;

            epErrors.Clear();

            if(txtLatitude.Text.Trim().Length != 0)
                if(!Double.TryParse(txtLatitude.Text, out double latitude) || latitude < -90.0 || latitude > 90.0)
                {
                    e.Cancel = true;
                    epErrors.SetError(txtLatitude, "Latitude must be a valid decimal value between -90 and 90");
                    tabInfo.SelectedTab = pgOther;
                    txtLatitude.Focus();
                }

            if(txtLongitude.Text.Trim().Length != 0)
                if(!Double.TryParse(txtLongitude.Text, out double longitude) || longitude < -180.0 || longitude > 180.0)
                {
                    e.Cancel = true;
                    epErrors.SetError(txtLongitude, "Longitude must be a valid decimal value between -180 and 180");
                    tabInfo.SelectedTab = pgOther;
                    txtLongitude.Focus();
                }
        }

        /// <summary>
        /// Initialize the dialog controls using the specified vCard
        /// </summary>
        /// <param name="vCard">The vCard from which to get the settings</param>
        public void SetValues(VCard vCard)
        {
            // Enable or disable fields based on the version
            cboVersion.SelectedIndex = (vCard.Version == SpecificationVersions.vCard21) ? 0 : 1;

            // General properties
            txtUniqueId.Text = vCard.UniqueId.Value;
            txtClass.Text = vCard.Classification.Value;
            txtLastRevised.Text = vCard.LastRevision.DateTimeValue.ToString("G");

            // Name properties
            txtLastName.Text = vCard.Name.FamilyName;
            txtFirstName.Text = vCard.Name.GivenName;
            txtMiddleName.Text = vCard.Name.AdditionalNames;
            txtTitle.Text = vCard.Name.NamePrefix;
            txtSuffix.Text = vCard.Name.NameSuffix;
            txtSortString.Text = vCard.SortString.Value;
            txtFormattedName.Text = vCard.FormattedName.Value;

            // We'll edit nicknames as a comma separated string
            txtNickname.Text = vCard.Nickname.NicknamesString;

            // We could bind directly to the existing collections but that would modify them.  To preserve the
            // original items, we'll pass a copy of the collections instead.

            // Addresses
            ucAddresses.BindingSource.DataSource = new AddressPropertyCollection().CloneRange(vCard.Addresses);

            // Labels
            ucLabels.BindingSource.DataSource = new LabelPropertyCollection().CloneRange(vCard.Labels);

            // Phone/E-Mail
            ucPhones.BindingSource.DataSource = new TelephonePropertyCollection().CloneRange(vCard.Telephones);
            ucEMail.BindingSource.DataSource = new EMailPropertyCollection().CloneRange(vCard.EMailAddresses);

            // Work
            txtOrganization.Text = vCard.Organization.Name;
            txtJobTitle.Text = vCard.Title.Value;
            txtRole.Text = vCard.Role.Value;

            // We'll edit units and categories as comma separated strings
            txtUnits.Text = vCard.Organization.UnitsString;
            txtCategories.Text = vCard.Categories.CategoriesString;

            // Other
            if(vCard.BirthDate.DateTimeValue == DateTime.MinValue)
                dtpBirthDate.Checked = false;
            else
            {
                dtpBirthDate.Value = vCard.BirthDate.DateTimeValue;
                dtpBirthDate.Checked = true;
            }

            txtTimeZone.Text = vCard.TimeZone.Value;
            txtLatitude.Text = vCard.GeographicPosition.Latitude.ToString();
            txtLongitude.Text = vCard.GeographicPosition.Longitude.ToString();
            txtWebPage.Text = vCard.Url.Value;

            // We'll only edit the first note.  Chances are there won't be more than one unless grouping is used.
            if(vCard.Notes.Count != 0)
                txtComments.Text = vCard.Notes[0].Value;

            // Photo
            if(vCard.Photo.Value != null)
                if(vCard.Photo.ValueLocation == ValLocValue.Binary)
                {
                    ucPhoto.SetImageBytes(vCard.Photo.GetImageBytes());
                    ucPhoto.IsInline = true;
                }
                else
                    ucPhoto.ImageFilename = vCard.Photo.Value;

            ucPhoto.ImageType = vCard.Photo.ImageType;

            // Logo
            if(vCard.Logo.Value != null)
                if(vCard.Logo.ValueLocation == ValLocValue.Binary)
                {
                    ucLogo.SetImageBytes(vCard.Logo.GetImageBytes());
                    ucLogo.IsInline = true;
                }
                else
                    ucLogo.ImageFilename = vCard.Logo.Value;

            ucLogo.ImageType = vCard.Logo.ImageType;
        }

        /// <summary>
        /// Update the vCard with the dialog control values
        /// </summary>
        /// <param name="vCard">The vCard in which the settings are updated</param>
        public void GetValues(VCard vCard)
        {
            // Set the version based on the one selected
            vCard.Version = (cboVersion.SelectedIndex == 0) ? SpecificationVersions.vCard21 : SpecificationVersions.vCard30;

            // General properties.  Unique ID is not changed.  Last Revision is set to the current date and time.
            vCard.Classification.Value = txtClass.Text;
            vCard.LastRevision.DateTimeValue = DateTime.Now;

            // Name properties
            vCard.Name.FamilyName = txtLastName.Text;
            vCard.Name.GivenName = txtFirstName.Text;
            vCard.Name.AdditionalNames = txtMiddleName.Text;
            vCard.Name.NamePrefix = txtTitle.Text;
            vCard.Name.NameSuffix = txtSuffix.Text;
            vCard.SortString.Value = txtSortString.Text;
            vCard.FormattedName.Value = txtFormattedName.Text;

            // We'll parse nicknames as a comma separated string
            vCard.Nickname.NicknamesString = txtNickname.Text;

            // For the collections, we'll clear the existing items and copy the modified items from the browse
            // control binding sources.

            // Addresses
            vCard.Addresses.Clear();
            vCard.Addresses.CloneRange((AddressPropertyCollection)ucAddresses.BindingSource.DataSource);

            // Labels
            vCard.Labels.Clear();
            vCard.Labels.CloneRange((LabelPropertyCollection)ucLabels.BindingSource.DataSource);

            // Phone/E-Mail
            vCard.Telephones.Clear();
            vCard.Telephones.CloneRange((TelephonePropertyCollection)ucPhones.BindingSource.DataSource);

            vCard.EMailAddresses.Clear();
            vCard.EMailAddresses.CloneRange((EMailPropertyCollection)ucEMail.BindingSource.DataSource);

            // Work
            vCard.Organization.Name = txtOrganization.Text;
            vCard.Title.Value = txtJobTitle.Text;
            vCard.Role.Value = txtRole.Text;

            // We'll parse units and categories as comma separated strings
            vCard.Organization.UnitsString = txtUnits.Text;
            vCard.Categories.CategoriesString = txtCategories.Text;

            // Other
            if(!dtpBirthDate.Checked)
                vCard.BirthDate.DateTimeValue = DateTime.MinValue;
            else
            {
                vCard.BirthDate.DateTimeValue = dtpBirthDate.Value;

                // Store time too if it isn't midnight
                if(dtpBirthDate.Value != dtpBirthDate.Value.Date)
                    vCard.BirthDate.ValueLocation = ValLocValue.DateTime;
                else
                    vCard.BirthDate.ValueLocation = ValLocValue.Date;
            }

            // See if the new value is just an offset.  If so, set the value type to UTC Offset.
            try
            {
                if(txtTimeZone.Text.Trim().Length == 0)
                {
                    vCard.TimeZone.ValueLocation = ValLocValue.Text;
                    vCard.TimeZone.Value = String.Empty;
                }
                else
                {
                    vCard.TimeZone.TimeSpanValue = DateUtils.FromISO8601TimeZone(
                        txtTimeZone.Text);
                    vCard.TimeZone.ValueLocation = ValLocValue.UtcOffset;
                }
            }
            catch
            {
                vCard.TimeZone.ValueLocation = ValLocValue.Text;
                vCard.TimeZone.Value = txtTimeZone.Text;
            }

            if(txtLatitude.Text.Trim().Length != 0 || txtLongitude.Text.Trim().Length != 0)
            {
                vCard.GeographicPosition.Latitude = Convert.ToDouble(txtLatitude.Text, CultureInfo.CurrentCulture);
                vCard.GeographicPosition.Longitude = Convert.ToDouble(txtLongitude.Text, CultureInfo.CurrentCulture);
            }
            else
                vCard.GeographicPosition.Latitude = vCard.GeographicPosition.Longitude = 0.0F;

            vCard.Url.Value = txtWebPage.Text;

            if(txtComments.Text.Length != 0)
            {
                if(vCard.Notes.Count != 0)
                    vCard.Notes[0].Value = txtComments.Text;
                else
                    vCard.Notes.Add(txtComments.Text);
            }
            else
                if(vCard.Notes.Count != 0)
                    vCard.Notes.RemoveAt(0);

            // Photo
            if(ucPhoto.IsInline)
            {
                vCard.Photo.ValueLocation = ValLocValue.Binary;
                vCard.Photo.SetImageBytes(ucPhoto.GetImageBytes());
                vCard.Photo.ImageType = ucPhoto.ImageType;
            }
            else
            {
                vCard.Photo.ValueLocation = ValLocValue.Uri;
                vCard.Photo.Value = ucPhoto.ImageFilename;
                vCard.Photo.ImageType = ucPhoto.ImageType;
            }

            // Logo
            if(ucLogo.IsInline)
            {
                vCard.Logo.ValueLocation = ValLocValue.Binary;
                vCard.Logo.SetImageBytes(ucLogo.GetImageBytes());
                vCard.Logo.ImageType = ucLogo.ImageType;
            }
            else
            {
                vCard.Logo.ValueLocation = ValLocValue.Uri;
                vCard.Logo.Value = ucLogo.ImageFilename;
                vCard.Logo.ImageType = ucLogo.ImageType;
            }
        }
        #endregion
    }
}
