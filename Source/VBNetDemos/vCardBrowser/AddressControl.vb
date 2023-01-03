'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : AddressControl.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/02/2020
' Note    : Copyright 2004-2020, Eric Woodruff, All rights reserved
'
' This is used to edit a vCard's address information.  It's nothing elaborate but does let you edit the
' collection fairly well.
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

Imports System.ComponentModel
Imports System.Text
Imports System.Web

Imports EWSoftware.PDI.Properties

''' <summary>
''' A user control for editing vCard mailing addresses
''' </summary>
Public Partial Class AddressControl
    Inherits EWSoftware.PDI.Windows.Forms.BrowseControl

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        ' Use a default collection as the data source
        Me.BindingSource.DataSource = New AddressPropertyCollection()
    End Sub

    ''' <summary>
    ''' Set the control states based on the vCard version
    ''' </summary>
    ''' <param name="version">The vCard version</param>
    Public Sub SetControlStateBasedOnVersion(version As SpecificationVersions)
        chkInternational.Visible = (version <> SpecificationVersions.vCard40)
        chkDomestic.Visible = (version <> SpecificationVersions.vCard40)
        chkPostal.Visible = (version <> SpecificationVersions.vCard40)
        chkParcel.Visible = (version <> SpecificationVersions.vCard40)
        chkPreferred.Visible = (version <> SpecificationVersions.vCard40)
    End Sub

    ''' <summary>
    ''' Enable or disable the controls based on whether or not there are items in the collection
    ''' </summary>
    ''' <param name="enable">True to enable the controls, false to disable them</param>
    Public Overrides Sub EnableControls(enable As Boolean)
        ' The easiest way to do this is to add a panel control with its Dock property set to Fill.  Place the
        ' controls in it and then enable or disable the panel.
        pnlControls.Enabled = enable

        If enable Then
            txtStreetAddress.Focus()
        End If
    End Sub

    ''' <summary>
    ''' This is overridden to bind the controls to the data source
    ''' </summary>
    Public Overrides Sub BindToControls()
        txtStreetAddress.DataBindings.Add("Text", Me.BindingSource, "StreetAddress")
        txtExtendedAddress.DataBindings.Add("Text", Me.BindingSource, "ExtendedAddress")
        txtPOBox.DataBindings.Add("Text", Me.BindingSource, "POBox")
        txtLocality.DataBindings.Add("Text", Me.BindingSource, "Locality")
        txtRegion.DataBindings.Add("Text", Me.BindingSource, "Region")
        txtPostalCode.DataBindings.Add("Text", Me.BindingSource, "PostalCode")
        txtCountry.DataBindings.Add("Text", Me.BindingSource, "Country")

        ' For the checkboxes, the Format and Parse events are needed to get and set the checked state
        Dim b As New Binding("Checked", Me.BindingSource, "AddressTypes")
        AddHandler b.Format, New ConvertEventHandler(AddressOf CheckBox_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf CheckBox_Parse)
        chkDomestic.Tag = AddressTypes.Domestic
        chkDomestic.DataBindings.Add(b)

        b = New Binding("Checked", Me.BindingSource, "AddressTypes")
        AddHandler b.Format, New ConvertEventHandler(AddressOf CheckBox_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf CheckBox_Parse)
        chkInternational.Tag = AddressTypes.International
        chkInternational.DataBindings.Add(b)

        b = new Binding("Checked", Me.BindingSource, "AddressTypes")
        AddHandler b.Format, New ConvertEventHandler(AddressOf CheckBox_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf CheckBox_Parse)
        chkPostal.Tag = AddressTypes.Postal
        chkPostal.DataBindings.Add(b)

        b = new Binding("Checked", Me.BindingSource, "AddressTypes")
        AddHandler b.Format, New ConvertEventHandler(AddressOf CheckBox_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf CheckBox_Parse)
        chkParcel.Tag = AddressTypes.Parcel
        chkParcel.DataBindings.Add(b)

        b = new Binding("Checked", Me.BindingSource, "AddressTypes")
        AddHandler b.Format, New ConvertEventHandler(AddressOf CheckBox_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf CheckBox_Parse)
        chkHome.Tag = AddressTypes.Home
        chkHome.DataBindings.Add(b)

        b = new Binding("Checked", Me.BindingSource, "AddressTypes")
        AddHandler b.Format, New ConvertEventHandler(AddressOf CheckBox_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf CheckBox_Parse)
        chkWork.Tag = AddressTypes.Work
        chkWork.DataBindings.Add(b)

        b = new Binding("Checked", Me.BindingSource, "AddressTypes")
        AddHandler b.Format, New ConvertEventHandler(AddressOf CheckBox_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf CheckBox_Parse)
        chkPreferred.Tag = AddressTypes.Preferred
        chkPreferred.DataBindings.Add(b)
    End Sub

    ''' <summary>
    ''' This is used to set the checked state of an address type checkbox
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub CheckBox_Format(sender As object, e As ConvertEventArgs)
        Dim cb As CheckBox = DirectCast(DirectCast(sender, Binding).Control, CheckBox)
        Dim checkType As AddressTypes = DirectCast(cb.Tag, AddressTypes)
        Dim addrTypes As AddressTypes = DirectCast(e.Value, AddressTypes)

        If (addrTypes And checkType) = checkType Then
            e.Value = True
        Else
            e.Value = False
        End If
    End Sub

    ''' <summary>
    ''' This is used to store the value of the address type checkbox
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub CheckBox_Parse(sender As object, e As ConvertEventArgs)
        Dim a As AddressProperty = DirectCast(Me.BindingSource.Current, AddressProperty)
        Dim cb As CheckBox = DirectCast(DirectCast(sender, Binding).Control, CheckBox)
        Dim checkType As AddressTypes = DirectCast(cb.Tag, AddressTypes)

        If cb.Checked Then
            a.AddressTypes = a.AddressTypes Or checkType
        Else
            a.AddressTypes = a.AddressTypes And Not checkType
        End If

        ' Only one address can be the preferred address
        If checkType = AddressTypes.Preferred Then
            DirectCast(Me.BindingSource.DataSource, AddressPropertyCollection).SetPreferred(a)
        End If
    End Sub

    ''' <summary>
    ''' A street address is required
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub txtStreetAddress_Validating(sender As Object, _
      e As CancelEventArgs) Handles txtStreetAddress.Validating
        Me.ErrorProvider.Clear()

        If Not Me.DesignMode AndAlso DirectCast(sender, Control).Enabled AndAlso txtStreetAddress.Text.Trim().Length = 0 Then
            Me.ErrorProvider.SetError(txtStreetAddress, "A street address is required")
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' View a map of the address if possible using Google Maps
    ''' </summary>
    ''' <param name="sender">The event sender</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnMap_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles btnMap.Click
        Dim sb As New StringBuilder("https://www.google.com/maps/place/", 512)

        If txtStreetAddress.Text.Length <> 0 Then
            sb.Append(HttpUtility.UrlEncode(txtStreetAddress.Text))
            sb.Append("+"c)
        End If

        If txtLocality.Text.Length <> 0 Then
            sb.Append(HttpUtility.UrlEncode(txtLocality.Text))
            sb.Append("+"c)
        End If

        If txtRegion.Text.Length <> 0 Then
            sb.Append(HttpUtility.UrlEncode(txtRegion.Text))
            sb.Append("+"c)
        End If

        If txtPostalCode.Text.Length <> 0 Then
            sb.Append(HttpUtility.UrlEncode(txtPostalCode.Text))
        End If

        If sb(sb.Length - 1) = "+"C Then
            sb.Remove(sb.Length - 1, 1)
        End If

        Try
            System.Diagnostics.Process.Start(sb.ToString())

        Catch ex As Exception
            MessageBox.Show("Unable to start web browser", "Launch Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)

            ' Log the exception to the debugger for the developer
            System.Diagnostics.Debug.Write(ex.ToString())

        End Try
    End Sub

End Class
