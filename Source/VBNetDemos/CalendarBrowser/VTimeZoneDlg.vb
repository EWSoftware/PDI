'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : VTimeZoneDlg.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/05/2015
' Note    : Copyright 2004-2015, Eric Woodruff, All rights reserved
' Compiler: Microsoft VB.NET
'
' This is used to edit a VTimeZone object's properties
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 12/09/2004  EFW  Created the code
' 05/21/2007  EFW  Converted for use with .NET 2.0
'================================================================================================================

Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Imports EWSoftware.PDI.Objects

''' <summary>
''' This is used to edit a VTimeZone object's properties
''' </summary>
Public Partial Class VTimeZoneDlg
    Inherits System.Windows.Forms.Form

    Private originalId As String

    Public Sub New()
        MyBase.New()

        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Validate the information on exit if OK was clicked
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event parameters</param>
    Private Sub VTimeZoneDlg_Closing(sender As Object, _
      e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        ' Ignore on cancel
        If Me.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
            Return
        End If

        epErrors.Clear()
        ucRules.BindingSource.EndEdit()

        If txtTimeZoneId.Text.Length = 0 Then
            epErrors.SetError(txtTimeZoneId, "A time zone ID is required")
            e.Cancel = True
            txtTimeZoneId.Focus()
        Else
            ' Disallow duplicate time zone IDs
            If originalId Is Nothing Or txtTimeZoneId.Text <> originalId Then
                If Not (VCalendar.TimeZones(txtTimeZoneId.Text) Is Nothing) Then
                    epErrors.SetError(txtTimeZoneId, "A time zone with the specified ID already exists")
                    e.Cancel = True
                    txtTimeZoneId.Focus()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Initialize the dialog controls using the specified VTimeZone object
    ''' </summary>
    ''' <param name="tz">The time zone object from which to get the settings</param>
    Public Sub SetValues(tz As VTimeZone)
        originalId = tz.TimeZoneId.Value
        txtTimeZoneId.Text = tz.TimeZoneId.Value
        txtTimeZoneUrl.Text = tz.TimeZoneUrl.Value
        txtLastModified.Text = tz.LastModified.DateTimeValue.ToString("G")

        ' We could bind directly to the existing collection but that would modify it.  To preserve the original
        ' items, we'll pass a copy of the collection instead.
        ucRules.BindingSource.DataSource = New ObservanceRuleCollection().CloneRange(tz.ObservanceRules)
    End Sub

    ''' <summary>
    ''' Update the time zone object with the dialog control values
    ''' </summary>
    ''' <param name="tz">The time zone object in which the settings are updated</param>
    Public Sub GetValues(tz As VTimeZone)
        tz.TimeZoneId.Value = txtTimeZoneId.Text
        tz.TimeZoneUrl.Value = txtTimeZoneUrl.Text
        tz.LastModified.DateTimeValue = DateTime.Now

        ' For the collection, we'll clear the existing items and copy the modified items from the browse control
        ' binding source.
        tz.ObservanceRules.Clear()
        tz.ObservanceRules.CloneRange(DirectCast(ucRules.BindingSource.DataSource, ObservanceRuleCollection))
    End Sub

End Class
