'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : EMailControl.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/02/2015
' Note    : Copyright 2004-2015, Eric Woodruff, All rights reserved
' Compiler: Visual Basic .NET
'
' This is used to edit a vCard's e-mail address information.  It's nothing elaborate but does let you edit the
' collection fairly well.
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 12/06/2004  EFW  Created the code
' 05/16/2007  EFW  Updated for use with .NET 2.0
'================================================================================================================

Imports System.ComponentModel
Imports System.Text.RegularExpressions

Imports EWSoftware.PDI.Properties
Imports EWSoftware.PDI.Windows.Forms

''' <summary>
''' A user control for editing vCard e-mail addresses
''' </summary>
Public Partial Class EMailControl
    Inherits EWSoftware.PDI.Windows.Forms.BrowseControl

    Private Shared reEMailAddress As New Regex(
        "^([a-z0-9_\-])([a-z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|" &
        "1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-z0-9\-]+)\.)+))" &
        "([a-z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$", RegexOptions.IgnoreCase)

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        ' Use a default collection as the data source
        Me.BindingSource.DataSource = New EMailPropertyCollection()
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
            txtEMailAddress.Focus()
        End If
    End Sub

    ''' <summary>
    ''' This is overridden to bind the controls to the data source
    ''' </summary>
    Public Overrides Sub BindToControls()
        txtEMailAddress.DataBindings.Add("Text", Me.BindingSource, "Value")

        ' For the checkboxes, the Format and Parse events are needed to get and set the checked state.  We
        ' aren't going to check for all of the older less common ones.
        Dim b As New Binding("Checked", Me.BindingSource, "EMailTypes")
        AddHandler b.Format, New ConvertEventHandler(AddressOf CheckBox_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf CheckBox_Parse)
        chkAOL.Tag = EMailTypes.AOL
        chkAOL.DataBindings.Add(b)

        b = new Binding("Checked", Me.BindingSource, "EMailTypes")
        AddHandler b.Format, New ConvertEventHandler(AddressOf CheckBox_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf CheckBox_Parse)
        chkInternet.Tag = EMailTypes.Internet
        chkInternet.DataBindings.Add(b)

        b = new Binding("Checked", Me.BindingSource, "EMailTypes")
        AddHandler b.Format, New ConvertEventHandler(AddressOf CheckBox_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf CheckBox_Parse)
        chkX400.Tag = EMailTypes.X400
        chkX400.DataBindings.Add(b)

        b = new Binding("Checked", Me.BindingSource, "EMailTypes")
        AddHandler b.Format, New ConvertEventHandler(AddressOf CheckBox_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf CheckBox_Parse)
        chkPreferred.Tag = EMailTypes.Preferred
        chkPreferred.DataBindings.Add(b)
    End Sub

    ''' <summary>
    ''' This is used to set the checked state of an e-mail type checkbox
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub CheckBox_Format(sender As object, e As ConvertEventArgs)
        Dim cb As CheckBox = DirectCast(DirectCast(sender, Binding).Control, CheckBox)
        Dim checkType As EMailTypes = DirectCast(cb.Tag, EMailTypes)
        Dim emailTypes As EMailTypes = DirectCast(e.Value, EMailTypes)

        If (emailTypes And checkType) = checkType Then
            e.Value = True
        Else
            e.Value = False
        End If
    End Sub

    ''' <summary>
    ''' This is used to store the value of the e-mail type checkbox
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub CheckBox_Parse(sender As object, e As ConvertEventArgs)
        Dim email As EMailProperty = DirectCast(Me.BindingSource.Current, EMailProperty)
        Dim cb As CheckBox = DirectCast(DirectCast(sender, Binding).Control, CheckBox)
        Dim checkType As EMailTypes = DirectCast(cb.Tag, EMailTypes)

        If cb.Checked Then
            email.EMailTypes = email.EMailTypes Or checkType
        Else
            email.EMailTypes = email.EMailTypes And Not checkType
        End If

        ' Only one address can be the preferred address
        If checkType = EMailTypes.Preferred Then
            DirectCast(Me.BindingSource.DataSource, EMailPropertyCollection).SetPreferred(email)
        End If
    End Sub

    ''' <summary>
    ''' A street address is required
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub txtEMailAddress_Validating(sender As object, _
      e As CancelEventArgs) Handles txtEMailAddress.Validating
        Me.ErrorProvider.Clear()

        If Not Me.DesignMode AndAlso DirectCast(sender, Control).Enabled AndAlso
          (txtEMailAddress.Text.Trim().Length = 0 OrElse Not reEMailAddress.IsMatch(txtEMailAddress.Text)) Then
            Me.ErrorProvider.SetError(txtEMailAddress, "A valid e-mail address is required")
            e.Cancel = True
        End If
    End Sub
End Class
