'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : LabelControl.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/02/2015
' Note    : Copyright 2004-2015, Eric Woodruff, All rights reserved
' Compiler: Visual C#
'
' This is used to edit a vCard's label information.  It's nothing elaborate but does let you edit the collection
' fairly well.
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

Imports EWSoftware.PDI.Properties
Imports EWSoftware.PDI.Windows.Forms

''' <summary>
''' A user control for editing vCard mailing labels
''' </summary>
Public Partial Class LabelControl
    Inherits EWSoftware.PDI.Windows.Forms.BrowseControl

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        ' Use a default collection as the data source
        Me.BindingSource.DataSource = New LabelPropertyCollection()
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
            txtLabelText.Focus()
        End If
    End Sub

    ''' <summary>
    ''' This is overridden to bind the controls to the data source
    ''' </summary>
    Public Overrides Sub BindToControls()
        txtLabelText.DataBindings.Add("Text", Me.BindingSource, "Value")

        ' For the checkboxes, the Format and Parse events are needed to get and set the checked state
        Dim b As New Binding("Checked", Me.BindingSource, "AddressTypes")
        AddHandler b.Format, New ConvertEventHandler(AddressOf CheckBox_Format)
        AddHandler b.Parse, New ConvertEventHandler(AddressOf CheckBox_Parse)
        chkDomestic.Tag = AddressTypes.Domestic
        chkDomestic.DataBindings.Add(b)

        b = new Binding("Checked", Me.BindingSource, "AddressTypes")
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
    Private Sub CheckBox_Format(sender As Object, e As ConvertEventArgs)
        Dim cb As CheckBox = DirectCast(DirectCast(sender, Binding).Control, CheckBox)
        Dim checkType As AddressTypes= DirectCast(cb.Tag, AddressTypes)
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
    Private Sub CheckBox_Parse(sender As Object, e As ConvertEventArgs)
        Dim l As LabelProperty = DirectCast(Me.BindingSource.Current, LabelProperty)
        Dim cb As CheckBox = DirectCast(DirectCast(sender, Binding).Control, CheckBox)
        Dim checkType As AddressTypes= DirectCast(cb.Tag, AddressTypes)

        If cb.Checked Then
            l.AddressTypes = l.AddressTypes Or checkType
        Else
            l.AddressTypes = l.AddressTypes And Not checkType
        End If

        ' Only one address can be the preferred address
        If checkType = AddressTypes.Preferred Then
            DirectCast(Me.BindingSource.DataSource, LabelPropertyCollection).SetPreferred(l)
        End If
    End Sub

    ''' <summary>
    ''' A label is required
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub txtLabelText_Validating(sender As Object, e As CancelEventArgs) Handles txtLabelText.Validating
        Me.ErrorProvider.Clear()

        If Not Me.DesignMode AndAlso DirectCast(sender, Control).Enabled AndAlso txtLabelText.Text.Trim().Length = 0 Then
            Me.ErrorProvider.SetError(txtLabelText, "Label text is required")
            e.Cancel = True
        End If
    End Sub
End Class
