'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : RequestStatusControl.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/05/2015
' Note    : Copyright 2004-2015, Eric Woodruff, All rights reserved
' Compiler: Microsoft VB.NET
'
' This is used to edit a calendar object's request status collection
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 12/14/2004  EFW  Created the code
' 05/25/2007  EFW  Converted for use with .NET 2.0
'================================================================================================================

Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Imports EWSoftware.PDI.Objects
Imports EWSoftware.PDI.Properties
Imports EWSoftware.PDI.Windows.Forms

''' <summary>
''' This is used to edit the Request Status property collection
''' </summary>
Public Partial Class RequestStatusControl
    Inherits EWSoftware.PDI.Windows.Forms.BrowseControl

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        ' Use a default collection as the data source
        Me.BindingSource.DataSource = New RequestStatusPropertyCollection()
    End Sub

    ''' <summary>
    ''' Enable or disable the controls based on whether or not there are items in the collection
    ''' </summary>
    ''' <param name="enable">True to enable the controls, false to disable them</param>
    Public Overrides Sub EnableControls(enable As Boolean)
        txtStatusCode.Enabled = enable
        txtMessage.Enabled = enable
        txtExtData.Enabled = enable

        If enable = True Then
            txtStatusCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' This is overridden to bind the controls to the data source
    ''' </summary>
    Public Overrides Sub BindToControls()
        txtStatusCode.DataBindings.Add("Text", Me.BindingSource, "StatusCode")
        txtMessage.DataBindings.Add("Text", Me.BindingSource, "StatusMessage")
        txtExtData.DataBindings.Add("Text", Me.BindingSource, "ExtendedData")
    End Sub

    ''' <summary>
    ''' Validate the items in the collection
    ''' </summary>
    ''' <returns>Returns true if all are valid or false if any item is not valid.  The position is set to the
    ''' first invalid item.</returns>
    ''' <remarks>The navigator doesn't allow for validation of all fields so we have to do some checking before
    ''' saving the results.</remarks>
    Public Function ValidateItems() As Boolean
        Me.ErrorProvider.Clear()

        Dim requests As RequestStatusPropertyCollection = DirectCast(Me.BindingSource.DataSource,
            RequestStatusPropertyCollection)

        For Each status As RequestStatusProperty In requests
            If String.IsNullOrEmpty(status.StatusMessage) Then
                Me.BindingSource.Position = requests.IndexOf(status)
                txtMessage.Focus()
                txtMessage_Validating(txtMessage, New CancelEventArgs())
                Return False
            End If
        Next

        Return true
    End Function

    ''' <summary>
    ''' A status code is required
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub txtStatusCode_Validating(sender As Object, e As CancelEventArgs) _
      Handles txtStatusCode.Validating
        Me.ErrorProvider.Clear()

        If Not Me.DesignMode AndAlso DirectCast(sender, Control).Enabled AndAlso txtStatusCode.Text.Trim().Length = 0 Then
            Me.ErrorProvider.SetError(txtStatusCode, "A status code is required")
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' A message is required
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub txtMessage_Validating(sender As Object, e As CancelEventArgs) _
      Handles txtMessage.Validating
        Me.ErrorProvider.Clear()

        If Not Me.DesignMode AndAlso DirectCast(sender, Control).Enabled AndAlso txtMessage.Text.Length = 0 Then
            Me.ErrorProvider.SetError(txtMessage, "A status message is required")
            e.Cancel = True
        End If
    End Sub

End Class
