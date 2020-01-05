'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : VCardDetails.aspx.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/02/2020
' Note    : Copyright 2004-2020, Eric Woodruff, All rights reserved
'
' This page is used to demonstrate the vCard classes.  Currently, it allows editing of some basic information.
' Information in the data grids could also be edited.  Time constraints limit what I have implemented so far but
' I may expand on this at a later date.
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 01/27/2005  EFW  Created the code
'================================================================================================================

Imports System.Globalization

Imports EWSoftware.PDI
Imports EWSoftware.PDI.Objects
Imports EWSoftware.PDI.Properties

Namespace PDIWebDemoVB

    Partial Class VCardDetails
        Inherits System.Web.UI.Page

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim vc As VCardCollection, vCard As VCard, idx As Integer

            If Not Page.IsPostBack Then
                vc = DirectCast(Session("VCards"), VCardCollection)

                If vc Is Nothing Then
                    Response.Redirect("VCardBrowser.aspx")
                    Return
                End If

                If Not Int32.TryParse(Request.QueryString("Index"), idx) Then
                    ' If not valid just go back to the browser form
                    Response.Redirect("VCardBrowser.aspx")
                    Return
                End If

                ' Force it to be valid
                If idx < 0 Or idx >= vc.Count Then idx = 0

                Me.ViewState("VCardIndex") = idx

                ' Load the data into the controls
                vCard = vc(idx)

                ' General properties
                lblUniqueId.Text = vCard.UniqueId.Value
                lblLastRevised.Text = vCard.LastRevision.DateTimeValue.ToString("G")
                txtClass.Text = vCard.Classification.Value

                ' Enable or disable fields based on the version
                If vCard.Version = SpecificationVersions.vCard21 Then
                    cboVersion.SelectedIndex = 0
                Else
                    If vCard.Version = SpecificationVersions.vCard30 Then
                        cboVersion.SelectedIndex = 1
                    Else
                        cboVersion.SelectedIndex = 2
                    End If
                End If

                ' Name
                txtLastName.Text = vCard.Name.FamilyName
                txtFirstName.Text = vCard.Name.GivenName
                txtMiddleName.Text = vCard.Name.AdditionalNames
                txtTitle.Text = vCard.Name.NamePrefix
                txtSuffix.Text = vCard.Name.NameSuffix
                txtSortString.Text = vCard.SortString.Value
                txtFormattedName.Text = vCard.FormattedName.Value

                ' We'll edit nicknames as a comma separated string
                txtNickname.Text = vCard.Nickname.NicknamesString

                ' Work
                txtOrganization.Text = vCard.Organization.Name
                txtJobTitle.Text = vCard.Title.Value
                txtRole.Text = vCard.Role.Value

                ' We'll edit units and categories as comma separated strings
                txtUnits.Text = vCard.Organization.UnitsString
                txtCategories.Text = vCard.Categories.CategoriesString

                ' Other
                If vCard.BirthDate.DateTimeValue <> DateTime.MinValue Then
                    txtBirthDate.Text = vCard.BirthDate.DateTimeValue.ToString("d")
                End If

                txtTimeZone.Text = vCard.TimeZone.Value
                txtLatitude.Text = vCard.GeographicPosition.Latitude.ToString()
                txtLongitude.Text = vCard.GeographicPosition.Longitude.ToString()
                txtWebPage.Text = vCard.Url.Value

                ' We'll only edit the first note.  Chances are there won't be more than one unless grouping is
                ' used.
                If vCard.Notes.Count <> 0 Then
                    txtComments.Text = vCard.Notes(0).Value
                End If

                ' Photo and logo
                If Not (vCard.Photo.Value Is Nothing) Then
                    If vCard.Photo.ValueLocation = ValLocValue.Binary Then
                        imgPhoto.ImageUrl = String.Format("ShowImage.aspx?Index={0}&Photo=1",
                            Me.ViewState("VCardIndex"))
                        lblPhoto.Text = "(Inline)"
                    Else
                        imgPhoto.ImageUrl = vCard.Photo.Value
                        lblPhoto.Text = vCard.Photo.Value
                    End If
                Else
                    imgPhoto.Visible = false
                    lblPhoto.Text = "(Not specified)"
                End If

                If Not (vCard.Logo.Value Is Nothing) Then
                    If vCard.Logo.ValueLocation = ValLocValue.Binary Then
                        imgLogo.ImageUrl = String.Format("ShowImage.aspx?Index={0}&Logo=1",
                            Me.ViewState("VCardIndex"))
                        lblLogo.Text = "(Inline)"
                    Else
                        imgLogo.ImageUrl = vCard.Logo.Value
                        lblLogo.Text = vCard.Logo.Value
                    End If
                Else
                    imgLogo.Visible = false
                    lblLogo.Text = "(Not specified)"
                End If

                ' Bind the data grids to display collection info
                dgAddresses.DataSource = vCard.Addresses
                dgLabels.DataSource = vCard.Labels
                dgPhones.DataSource = vCard.Telephones
                dgEMail.DataSource = vCard.EMailAddresses

                Page.DataBind()

                ' Enable/disable controls as needed based on the version
                cboVersion_SelectedIndexChanged(Me, e)
            End If
        End Sub

        ' Enable/disable controls based on the version selected
        Private Sub cboVersion_SelectedIndexChanged(ByVal sender As Object, _
          ByVal e As System.EventArgs) Handles cboVersion.SelectedIndexChanged
            Dim enabled As Boolean = (cboVersion.SelectedIndex <> 0)

            txtCategories.Enabled = enabled
            txtClass.Enabled = enabled
            txtNickname.Enabled = enabled
            txtSortString.Enabled = enabled
        End Sub

        ' Save changes and return to the vCard list
        Private Sub btnSave_Click(ByVal sender As Object, _
          ByVal e As System.EventArgs) Handles btnSave.Click
            If Not Page.IsValid THen
                Return
            End If

            Dim vc As VCardCollection = DirectCast(Session("VCards"), VCardCollection)

            ' Not very friendly, but it's just a demo
            If vc Is Nothing Then
                Response.Redirect("VCardBrowser.aspx")
                Return
            End If

            Dim vCard As VCard = vc(DirectCast(Me.ViewState("VCardIndex"), Integer))

            ' General properties.  Unique ID is not changed.  Last Revision is set to the current date and time.
            vCard.Classification.Value = txtClass.Text.ToUpperInvariant()
            vCard.LastRevision.DateTimeValue = DateTime.Now

            ' Set the version based on the one selected
            If cboVersion.SelectedIndex = 0 Then
                vCard.Version = SpecificationVersions.vCard21
            Else
                vCard.Version = SpecificationVersions.vCard30
            End If

            ' Name properties
            vCard.Name.FamilyName = txtLastName.Text
            vCard.Name.GivenName = txtFirstName.Text
            vCard.Name.AdditionalNames = txtMiddleName.Text
            vCard.Name.NamePrefix = txtTitle.Text
            vCard.Name.NameSuffix = txtSuffix.Text
            vCard.SortString.Value = txtSortString.Text
            vCard.FormattedName.Value = txtFormattedName.Text

            ' We'll parse nicknames as a comma separated string
            vCard.Nickname.NicknamesString = txtNickname.Text

            ' Work
            vCard.Organization.Name = txtOrganization.Text
            vCard.Title.Value = txtJobTitle.Text
            vCard.Role.Value = txtRole.Text

            ' We'll parse units and categories as comma separated strings
            vCard.Organization.UnitsString = txtUnits.Text
            vCard.Categories.CategoriesString = txtCategories.Text

            ' Other
            If txtBirthDate.Text.Length = 0 Then
                vCard.BirthDate.DateTimeValue = DateTime.MinValue
            Else
                ' The web app is limited to date values only for now
                vCard.BirthDate.DateTimeValue = Convert.ToDateTime(txtBirthDate.Text, CultureInfo.CurrentCulture)
                vCard.BirthDate.ValueLocation = ValLocValue.Date
            End If

            ' See if the new value is just an offset.  If so,
            ' set the value type to UTC Offset.
            Try
                vCard.TimeZone.TimeSpanValue = DateUtils.FromISO8601TimeZone(txtTimeZone.Text)
                vCard.TimeZone.ValueLocation = ValLocValue.UtcOffset
            Catch
                vCard.TimeZone.ValueLocation = ValLocValue.Text
                vCard.TimeZone.Value = txtTimeZone.Text
            End Try

            If txtLatitude.Text.Length <> 0 Or txtLongitude.Text.Length <> 0 Then
                vCard.GeographicPosition.Latitude = Convert.ToDouble(txtLatitude.Text, CultureInfo.CurrentCulture)
                vCard.GeographicPosition.Longitude = Convert.ToDouble(txtLongitude.Text, CultureInfo.CurrentCulture)
            Else
                vCard.GeographicPosition.Latitude = 0.0
                vCard.GeographicPosition.Longitude = 0.0
            End If

            vCard.Url.Value = txtWebPage.Text

            If txtComments.Text.Length <> 0 Then
                If vCard.Notes.Count <> 0 Then
                    vCard.Notes(0).Value = txtComments.Text
                Else
                    vCard.Notes.Add(txtComments.Text)
                End If
            Else
                If vCard.Notes.Count <> 0 Then
                    vCard.Notes.RemoveAt(0)
                End If
            End If

            Response.Redirect("VCardBrowser.aspx")
        End Sub

        ' Exit without saving
        Private Sub btnExit_Click(ByVal sender As Object, _
          ByVal e As System.EventArgs) Handles btnExit.Click
            Response.Redirect("VCardBrowser.aspx")
        End Sub
    End Class

End Namespace
