'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : VCardPropertiesDlg.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 07/24/2020
' Note    : Copyright 2004-2020, Eric Woodruff, All rights reserved
'
' This is used to edit a vCard's information.  It supports most of the common properties of the vCard including
' photo, logo, and sound.
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 12/07/2004  EFW  Created the code
' 05/16/2007  EFW  Updated for use with .NET 2.0
'================================================================================================================

Imports System.Collections.Generic
Imports System.Globalization

Imports EWSoftware.PDI
Imports EWSoftware.PDI.Objects
Imports EWSoftware.PDI.Properties

''' <summary>
''' This dialog box is used to edit a vCard's properties
''' </summary>
Partial Public Class VCardPropertiesDlg
    Inherits System.Windows.Forms.Form

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        cboVersion.SelectedIndex = 1

        ' Set the short date/long time pattern based on the current culture
        dtpBirthDate.CustomFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern & " " &
            CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern

        cboSex.ValueMember = "Value"
        cboSex.DisplayMember = "Display"
        cboSex.DataSource = New List(Of ListItem) From
        {
            New ListItem(Convert.ToChar(0), String.Empty),
            New ListItem("M"c, "Male"),
            New ListItem("F"c, "Female"),
            New ListItem("O"c, "Other"),
            New ListItem("N"c, "N/A"),
            New ListItem("U"c, "Unknown")
        }

    End Sub

    ''' <summary>
    ''' Enable and disable fields based on the version chosen
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event parameters</param>
    Private Sub cboVersion_SelectedIndexChanged(ByVal sender As System.Object,
      ByVal e As System.EventArgs) Handles cboVersion.SelectedIndexChanged
        txtCategories.Enabled = (cboVersion.SelectedIndex <> 0)
        txtNickname.Enabled = (cboVersion.SelectedIndex <> 0)
        txtClass.Enabled = (cboVersion.SelectedIndex = 1)
        txtSortString.Enabled = (cboVersion.SelectedIndex = 1)
        cboSex.Enabled = (cboVersion.SelectedIndex = 2)
        txtGenderIdentity.Enabled = (cboVersion.SelectedIndex = 2)

        ucAddresses.SetControlStateBasedOnVersion(If(cboVersion.SelectedIndex = 0, SpecificationVersions.vCard21,
            If(cboVersion.SelectedIndex = 1, SpecificationVersions.vCard30, SpecificationVersions.vCard40)))
        ucEMail.SetControlStateBasedOnVersion(If(cboVersion.SelectedIndex = 0, SpecificationVersions.vCard21,
            If(cboVersion.SelectedIndex = 1, SpecificationVersions.vCard30, SpecificationVersions.vCard40)))
        ucPhones.SetControlStateBasedOnVersion(If(cboVersion.SelectedIndex = 0, SpecificationVersions.vCard21,
            If(cboVersion.SelectedIndex = 1, SpecificationVersions.vCard30, SpecificationVersions.vCard40)))
    End Sub

    ''' <summary>
    ''' View the geocoding coordinates using Google Maps if possible
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event parameters</param>
    Private Sub btnFind_Click(ByVal sender As System.Object,
      ByVal e As System.EventArgs) Handles btnFind.Click
        Dim url As String = String.Format("https://www.google.com/maps/place/{0},{1}", txtLatitude.Text,
            txtLongitude.Text)

        Try
            System.Diagnostics.Process.Start(url)

        Catch ex As Exception
            MessageBox.Show("Unable to start web browser", "Launch Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)

            ' Log the exception to the debugger for the developer
            System.Diagnostics.Debug.Write(ex.ToString())
        End Try
    End Sub

    ''' <summary>
    ''' Launch a web browser to display the web page
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event parameters</param>
    Private Sub btnWebPage_Click(ByVal sender As System.Object,
      ByVal e As System.EventArgs) Handles btnWebPage.Click
        If txtWebPage.Text.Length = 0 Then
            MessageBox.Show("Enter a web page URL first", "No URL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Try
                System.Diagnostics.Process.Start(txtWebPage.Text)

            Catch ex As Exception
                MessageBox.Show("Unable to start web browser for this URL", "URL Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation)

                ' Log the exception to the debugger for the developer.
                System.Diagnostics.Debug.Write(ex.ToString())
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Validate the information on exit if OK was clicked
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event parameters</param>
    Private Sub VCardPropertiesDlg_Closing(ByVal sender As Object,
      ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim latitude, longitude As Double

        ' Ignore on cancel
        If Me.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
            Return
        End If

        epErrors.Clear()

        If txtLatitude.Text.Length <> 0 Then
            If Not Double.TryParse(txtLatitude.Text, latitude) OrElse latitude < -90.0 OrElse latitude > 90.0 Then
                e.Cancel = True
                epErrors.SetError(txtLatitude, "Latitude must be a valid decimal value between -90 and 90")
                tabInfo.SelectedTab = pgOther
                txtLatitude.Focus()
            End If
        End If

        If txtLongitude.Text.Length <> 0 Then
            If Not Double.TryParse(txtLongitude.Text, longitude) OrElse longitude < -180.0 OrElse longitude > 180.0 Then
                e.Cancel = True
                epErrors.SetError(txtLongitude, "Longitude must be a valid decimal value between -180 and 180")
                tabInfo.SelectedTab = pgOther
                txtLongitude.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Initialize the dialog controls using the specified vCard
    ''' </summary>
    ''' <param name="vCard">The vCard from which to get the settings</param>
    Public Sub SetValues(vCard As VCard)
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

        ' General properties
        txtUniqueId.Text = vCard.UniqueId.Value
        txtClass.Text = vCard.Classification.Value
        txtLastRevised.Text = vCard.LastRevision.DateTimeValue.ToString("G")

        ' Name properties
        txtLastName.Text = vCard.Name.FamilyName
        txtFirstName.Text = vCard.Name.GivenName
        txtMiddleName.Text = vCard.Name.AdditionalNames
        txtTitle.Text = vCard.Name.NamePrefix
        txtSuffix.Text = vCard.Name.NameSuffix
        txtSortString.Text = vCard.SortString.Value
        txtFormattedName.Text = vCard.FormattedName.Value

        ' We'll edit nicknames as a comma separated string
        txtNickname.Text = vCard.Nickname.NicknamesString

        cboSex.SelectedValue = If(vCard.Gender.Sex, Convert.ToChar(0))
        txtGenderIdentity.Text = vCard.Gender.GenderIdentity

        ' We could bind directly to the existing collections but that would modify them.  To preserve the
        ' original items, we'll pass a copy of the collections instead.

        ' Addresses
        ucAddresses.BindingSource.DataSource = New AddressPropertyCollection().CloneRange(vCard.Addresses)

        ' Labels
        ucLabels.BindingSource.DataSource = New LabelPropertyCollection().CloneRange(vCard.Labels)

        ' Phone/E-Mail
        ucPhones.BindingSource.DataSource = New TelephonePropertyCollection().CloneRange(vCard.Telephones)
        ucEMail.BindingSource.DataSource = New EMailPropertyCollection().CloneRange(vCard.EMailAddresses)

        ' Work
        txtOrganization.Text = vCard.Organization.Name
        txtJobTitle.Text = vCard.Title.Value
        txtRole.Text = vCard.Role.Value

        ' We'll edit units and categories as comma separated strings
        txtUnits.Text = vCard.Organization.UnitsString
        txtCategories.Text = vCard.Categories.CategoriesString

        ' Other
        If vCard.BirthDate.DateTimeValue = DateTime.MinValue Then
            dtpBirthDate.Checked = False
        Else
            dtpBirthDate.Value = vCard.BirthDate.DateTimeValue
            dtpBirthDate.Checked = True
        End If

        txtTimeZone.Text = vCard.TimeZone.Value
        txtLatitude.Text = vCard.GeographicPosition.Latitude.ToString()
        txtLongitude.Text = vCard.GeographicPosition.Longitude.ToString()

        ' We'll only edit the first one
        If vCard.Urls.Count <> 0 Then
            txtWebPage.Text = vCard.Urls(0).Value
        End If

        ' We'll only edit the first note.  Chances are there won't be more than one unless grouping is used
        If vCard.Notes.Count <> 0 Then
            txtComments.Text = vCard.Notes(0).Value
        End If

        ' Photo
        If Not (vCard.Photo.Value Is Nothing) Then
            If vCard.Photo.ValueLocation = ValLocValue.Binary Then
                ucPhoto.SetImageBytes(vCard.Photo.GetImageBytes())
                ucPhoto.IsInline = True
            Else
                ucPhoto.ImageFilename = vCard.Photo.Value
            End If
        End If

        ucPhoto.ImageType = vCard.Photo.ImageType

        ' Logo
        If Not (vCard.Logo.Value Is Nothing) Then
            If vCard.Logo.ValueLocation = ValLocValue.Binary Then
                ucLogo.SetImageBytes(vCard.Logo.GetImageBytes())
                ucLogo.IsInline = True
            Else
                ucLogo.ImageFilename = vCard.Logo.Value
            End If
        End If

        ucLogo.ImageType = vCard.Logo.ImageType
    End Sub

    ''' <summary>
    ''' Update the vCard with the dialog control values
    ''' </summary>
    ''' <param name="vCard">The vCard in which the settings are updated</param>
    Public Sub GetValues(vCard As VCard)
        ' Set the version based on the one selected
        If cboVersion.SelectedIndex = 0 Then
            vCard.Version = SpecificationVersions.vCard21
        Else
            If cboVersion.SelectedIndex = 1 Then
                vCard.Version = SpecificationVersions.vCard30
            Else
                vCard.Version = SpecificationVersions.vCard40
            End If
        End If

        ' General properties.  Unique ID is not changed.  Last Revision is set to the current date and time.
        vCard.Classification.Value = txtClass.Text
        vCard.LastRevision.DateTimeValue = DateTime.Now

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


        vCard.Gender.Sex = If(cboSex.SelectedIndex = 0, Nothing, Convert.ToChar(cboSex.SelectedValue))
        vCard.Gender.GenderIdentity = txtGenderIdentity.Text

        ' For the collections, we'll clear the existing items and copy the modified items from the browse control
        ' binding sources.

        ' Addresses
        vCard.Addresses.Clear()
        vCard.Addresses.CloneRange(DirectCast(ucAddresses.BindingSource.DataSource, AddressPropertyCollection))

        ' Labels
        vCard.Labels.Clear()
        vCard.Labels.CloneRange(DirectCast(ucLabels.BindingSource.DataSource, LabelPropertyCollection))

        ' Phone/E-Mail
        vCard.Telephones.Clear()
        vCard.Telephones.CloneRange(DirectCast(ucPhones.BindingSource.DataSource, TelephonePropertyCollection))

        vCard.EMailAddresses.Clear()
        vCard.EMailAddresses.CloneRange(DirectCast(ucEMail.BindingSource.DataSource, EMailPropertyCollection))

        ' Work
        vCard.Organization.Name = txtOrganization.Text
        vCard.Title.Value = txtJobTitle.Text
        vCard.Role.Value = txtRole.Text

        ' We'll parse units and categories as comma separated strings
        vCard.Organization.UnitsString = txtUnits.Text
        vCard.Categories.CategoriesString = txtCategories.Text

        ' Other
        If dtpBirthDate.Checked = False Then
            vCard.BirthDate.DateTimeValue = DateTime.MinValue
        Else
            vCard.BirthDate.DateTimeValue = dtpBirthDate.Value

            ' Store time too if it isn't midnight
            If dtpBirthDate.Value <> dtpBirthDate.Value.Date Then
                vCard.BirthDate.ValueLocation = ValLocValue.DateTime
            Else
                vCard.BirthDate.ValueLocation = ValLocValue.Date
            End If
        End If

        ' See if the new value is just an offset.  If so, set the value type to UTC Offset.
        Try
            If txtTimeZone.Text.Trim().Length = 0 Then
                vCard.TimeZone.ValueLocation = ValLocValue.Text
                vCard.TimeZone.Value = String.Empty
            Else
                vCard.TimeZone.TimeSpanValue = DateUtils.FromISO8601TimeZone(txtTimeZone.Text)
                vCard.TimeZone.ValueLocation = ValLocValue.UtcOffset
            End If
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

        If txtWebPage.Text.Length <> 0 Then
            If vCard.Urls.Count <> 0 Then
                vCard.Urls(0).Value = txtWebPage.Text
            Else
                vCard.Urls.Add(txtWebPage.Text)
            End If
        Else
            If vCard.Urls.Count <> 0 Then
                vCard.Urls.RemoveAt(0)
            End If
        End If

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

        ' Photo
        If ucPhoto.IsInline = True Then
            vCard.Photo.ValueLocation = ValLocValue.Binary
            vCard.Photo.SetImageBytes(ucPhoto.GetImageBytes())
            vCard.Photo.ImageType = ucPhoto.ImageType
        Else
            vCard.Photo.ValueLocation = ValLocValue.Uri
            vCard.Photo.Value = ucPhoto.ImageFilename
            vCard.Photo.ImageType = ucPhoto.ImageType
        End If

        ' Logo
        If ucLogo.IsInline = True Then
            vCard.Logo.ValueLocation = ValLocValue.Binary
            vCard.Logo.SetImageBytes(ucLogo.GetImageBytes())
            vCard.Logo.ImageType = ucLogo.ImageType
        Else
            vCard.Logo.ValueLocation = ValLocValue.Uri
            vCard.Logo.Value = ucLogo.ImageFilename
            vCard.Logo.ImageType = ucLogo.ImageType
        End If
    End Sub

End Class
