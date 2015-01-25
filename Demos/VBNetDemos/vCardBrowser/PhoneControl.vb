'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : PhoneControl.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/02/2015
' Note    : Copyright 2004-2015, Eric Woodruff, All rights reserved
' Compiler: Visual C#
'
' This is used to edit a vCard's phone information.  It's nothing elaborate but does let you edit the collection
' fairly well.
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

Imports EWSoftware.PDI.Properties
Imports EWSoftware.PDI.Windows.Forms

''' <summary>
''' A user control for editing vCard phone numbers
''' </summary>
Public Partial Class PhoneControl
    Inherits EWSoftware.PDI.Windows.Forms.BrowseControl

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        ' Use a default collection as the data source
        Me.BindingSource.DataSource = New TelephonePropertyCollection()
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
            txtPhoneNumber.Focus()
        End If
    End Sub

    ''' <summary>
    ''' This is overridden to bind the controls to the data source
    ''' </summary>
    Public Overrides Sub BindToControls()
        txtPhoneNumber.DataBindings.Add("Text", Me.BindingSource, "Value")

        ' For the checkboxes, the Format and Parse events are needed to get and set the checked state.  We aren't
        ' going to check for a few of the less common ones.
        Dim b As Binding = New Binding("Checked", Me.BindingSource, "PhoneTypes")
        AddHandler b.Format, New ConvertEventHandler(AddressOf CheckBox_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf CheckBox_Parse)
        chkWork.Tag = PhoneTypes.Work
        chkWork.DataBindings.Add(b)

        b = new Binding("Checked", Me.BindingSource, "PhoneTypes")
        AddHandler b.Format, New ConvertEventHandler(AddressOf CheckBox_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf CheckBox_Parse)
        chkHome.Tag = PhoneTypes.Home
        chkHome.DataBindings.Add(b)

        b = new Binding("Checked", Me.BindingSource, "PhoneTypes")
        AddHandler b.Format, New ConvertEventHandler(AddressOf CheckBox_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf CheckBox_Parse)
        chkVoice.Tag = PhoneTypes.Voice
        chkVoice.DataBindings.Add(b)

        b = new Binding("Checked", Me.BindingSource, "PhoneTypes")
        AddHandler b.Format, New ConvertEventHandler(AddressOf CheckBox_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf CheckBox_Parse)
        chkFax.Tag = PhoneTypes.Fax
        chkFax.DataBindings.Add(b)

        b = new Binding("Checked", Me.BindingSource, "PhoneTypes")
        AddHandler b.Format, New ConvertEventHandler(AddressOf CheckBox_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf CheckBox_Parse)
        chkMessage.Tag = PhoneTypes.Message
        chkMessage.DataBindings.Add(b)

        b = new Binding("Checked", Me.BindingSource, "PhoneTypes")
        AddHandler b.Format, New ConvertEventHandler(AddressOf CheckBox_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf CheckBox_Parse)
        chkCell.Tag = PhoneTypes.Cell
        chkCell.DataBindings.Add(b)

        b = new Binding("Checked", Me.BindingSource, "PhoneTypes")
        AddHandler b.Format, New ConvertEventHandler(AddressOf CheckBox_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf CheckBox_Parse)
        chkPager.Tag = PhoneTypes.Pager
        chkPager.DataBindings.Add(b)

        b = new Binding("Checked", Me.BindingSource, "PhoneTypes")
        AddHandler b.Format, New ConvertEventHandler(AddressOf CheckBox_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf CheckBox_Parse)
        chkPreferred.Tag = PhoneTypes.Preferred
        chkPreferred.DataBindings.Add(b)
    End Sub

    ''' <summary>
    ''' This is used to set the checked state of an address type checkbox
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub CheckBox_Format(sender As Object, e As ConvertEventArgs)
        Dim cb As CheckBox = DirectCast(DirectCast(sender, Binding).Control, CheckBox)
        Dim checkType As PhoneTypes = DirectCast(cb.Tag, PhoneTypes)
        Dim addrTypes As PhoneTypes = DirectCast(e.Value, PhoneTypes)

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
    Private Sub CheckBox_Parse(sender As Object, e As ConvertEventArgs)
        Dim t As TelephoneProperty = DirectCast(Me.BindingSource.Current, TelephoneProperty)
        Dim cb As CheckBox= DirectCast(DirectCast(sender, Binding).Control, CheckBox)
        Dim checkType As PhoneTypes = DirectCast(cb.Tag, PhoneTypes)

        If cb.Checked Then
            t.PhoneTypes = t.PhoneTypes Or checkType
        Else
            t.PhoneTypes = t.PhoneTypes And Not checkType
        End If

        ' Only one address can be the preferred address
        If checkType = PhoneTypes.Preferred Then
            DirectCast(Me.BindingSource.DataSource, TelephonePropertyCollection).SetPreferred(t)
        End If
    End Sub

    ''' <summary>
    ''' A street address is required
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub txtPhoneNumber_Validating(sender As Object, _
      e As CancelEventArgs) Handles txtPhoneNumber.Validating
        Me.ErrorProvider.Clear()

        If Not Me.DesignMode AndAlso DirectCast(sender, Control).Enabled AndAlso txtPhoneNumber.Text.Trim().Length = 0 Then
            Me.ErrorProvider.SetError(txtPhoneNumber, "A phone number is required")
            e.Cancel = True
        End If
    End Sub
End Class
