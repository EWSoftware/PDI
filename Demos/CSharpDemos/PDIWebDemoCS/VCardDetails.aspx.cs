//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : VCardDetails.aspx.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/31/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This page is used to demonstrate the vCard classes.  Currently, it allows editing of some basic information.
// Information in the data grids could also be edited.  Time constraints limit what I have implemented so far
// but I may expand on this at a later date.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 01/23/2005  EFW  Created the code
//===============================================================================================================

using System;
using System.Globalization;
using System.Web.UI;

using EWSoftware.PDI;
using EWSoftware.PDI.Objects;
using EWSoftware.PDI.Properties;

namespace PDIWebDemoCS
{
	/// <summary>
	/// This page is used to demonstrate the vCard classes
	/// </summary>
	public partial class VCardDetails : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            VCardCollection vc;
            VCard vCard;
            int idx;

            if(!Page.IsPostBack)
            {
                vc = (VCardCollection)Session["VCards"];

                if(vc == null)
                {
                    Response.Redirect("VCardBrowser.aspx");
                    return;
                }

                if(!Int32.TryParse(Request.QueryString["Index"], out idx))
                {
                    // If not valid just go back to the browser form
                    Response.Redirect("VCardBrowser.aspx");
                    return;
                }

                // Force it to be valid
                if(idx < 0 || idx >= vc.Count)
                    idx = 0;

                this.ViewState["VCardIndex"] = idx;

                // Load the data into the controls
                vCard = vc[idx];

                // General properties
                lblUniqueId.Text = vCard.UniqueId.Value;
                lblLastRevised.Text = vCard.LastRevision.DateTimeValue.ToString("G");
                txtClass.Text = vCard.Classification.Value;

                // Enable or disable fields based on the version
                cboVersion.SelectedIndex = (vCard.Version == SpecificationVersions.vCard21) ? 0 : 1;

                // Name
                txtLastName.Text = vCard.Name.FamilyName;
                txtFirstName.Text = vCard.Name.GivenName;
                txtMiddleName.Text = vCard.Name.AdditionalNames;
                txtTitle.Text = vCard.Name.NamePrefix;
                txtSuffix.Text = vCard.Name.NameSuffix;
                txtSortString.Text = vCard.SortString.Value;
                txtFormattedName.Text = vCard.FormattedName.Value;

                // We'll edit nicknames as a comma separated string
                txtNickname.Text = vCard.Nickname.NicknamesString;

                // Work
                txtOrganization.Text = vCard.Organization.Name;
                txtJobTitle.Text = vCard.Title.Value;
                txtRole.Text = vCard.Role.Value;

                // We'll edit units and categories as comma separated strings
                txtUnits.Text = vCard.Organization.UnitsString;
                txtCategories.Text = vCard.Categories.CategoriesString;

                // Other
                if(vCard.BirthDate.DateTimeValue != DateTime.MinValue)
                    txtBirthDate.Text = vCard.BirthDate.DateTimeValue.ToString("d");

                txtTimeZone.Text = vCard.TimeZone.Value;
                txtLatitude.Text = vCard.GeographicPosition.Latitude.ToString();
                txtLongitude.Text = vCard.GeographicPosition.Longitude.ToString();
                txtWebPage.Text = vCard.Url.Value;

                // We'll only edit the first note.  Chances are there won't be more than one unless grouping is
                // used.
                if(vCard.Notes.Count != 0)
                    txtComments.Text = vCard.Notes[0].Value;

                // Photo and logo
                if(vCard.Photo.Value != null)
                {
                    if(vCard.Photo.ValueLocation == ValLocValue.Binary)
                    {
                        imgPhoto.ImageUrl = String.Format("ShowImage.aspx?Index={0}&Photo=1",
                            this.ViewState["VCardIndex"]);
                        lblPhoto.Text = "(Inline)";
                    }
                    else
                    {
                        imgPhoto.ImageUrl = vCard.Photo.Value;
                        lblPhoto.Text = vCard.Photo.Value;
                    }
                }
                else
                {
                    imgPhoto.Visible = false;
                    lblPhoto.Text = "(Not specified)";
                }

                if(vCard.Logo.Value != null)
                {
                    if(vCard.Logo.ValueLocation == ValLocValue.Binary)
                    {
                        imgLogo.ImageUrl = String.Format("ShowImage.aspx?Index={0}&Logo=1",
                            this.ViewState["VCardIndex"]);
                        lblLogo.Text = "(Inline)";
                    }
                    else
                    {
                        imgLogo.ImageUrl = vCard.Logo.Value;
                        lblLogo.Text = vCard.Logo.Value;
                    }
                }
                else
                {
                    imgLogo.Visible = false;
                    lblLogo.Text = "(Not specified)";
                }

                // Bind the data grids to display collection info
                dgAddresses.DataSource = vCard.Addresses;
                dgLabels.DataSource = vCard.Labels;
                dgPhones.DataSource = vCard.Telephones;
                dgEMail.DataSource = vCard.EMailAddresses;

                Page.DataBind();

                // Enable/disable controls as needed based on the version
                cboVersion_SelectedIndexChanged(this, e);
            }
		}

        /// <summary>
        /// Enable/disable controls based on the version selected
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        protected void cboVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCategories.Enabled = txtClass.Enabled = txtNickname.Enabled = txtSortString.Enabled =
                (cboVersion.SelectedIndex == 1);
        }

        /// <summary>
        /// Save changes and return to the vCard list
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(!Page.IsValid)
                return;

            VCardCollection vc = (VCardCollection)Session["VCards"];

            // Not very friendly, but it's just a demo
            if(vc == null)
            {
                Response.Redirect("VCardBrowser.aspx");
                return;
            }

            VCard vCard = vc[(int)this.ViewState["VCardIndex"]];

            // General properties.  Unique ID is not changed.  Last Revision is set to the current date and time.
            vCard.Classification.Value = txtClass.Text.ToUpperInvariant();
            vCard.LastRevision.DateTimeValue = DateTime.Now;

            // Set the version based on the one selected
            vCard.Version = (cboVersion.SelectedIndex == 0) ? SpecificationVersions.vCard21 :
                SpecificationVersions.vCard30;

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

            // Work
            vCard.Organization.Name = txtOrganization.Text;
            vCard.Title.Value = txtJobTitle.Text;
            vCard.Role.Value = txtRole.Text;

            // We'll parse units and categories as comma separated strings
            vCard.Organization.UnitsString = txtUnits.Text;
            vCard.Categories.CategoriesString = txtCategories.Text;

            // Other
            if(txtBirthDate.Text.Length == 0)
                vCard.BirthDate.DateTimeValue = DateTime.MinValue;
            else
            {
                // The web app is limited to date values only for now
                vCard.BirthDate.DateTimeValue = Convert.ToDateTime(txtBirthDate.Text, CultureInfo.CurrentCulture);
                vCard.BirthDate.ValueLocation = ValLocValue.Date;
            }

            // See if the new value is just an offset.  If so, set the value type to UTC Offset.
            try
            {
                vCard.TimeZone.TimeSpanValue = DateUtils.FromISO8601TimeZone(txtTimeZone.Text);
                vCard.TimeZone.ValueLocation = ValLocValue.UtcOffset;
            }
            catch
            {
                vCard.TimeZone.ValueLocation = ValLocValue.Text;
                vCard.TimeZone.Value = txtTimeZone.Text;
            }

            if(txtLatitude.Text.Length != 0 || txtLongitude.Text.Length != 0)
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

            Response.Redirect("VCardBrowser.aspx");
        }

        /// <summary>
        /// Exit without saving
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("VCardBrowser.aspx");
        }
	}
}
