'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : MainForm.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/02/2015
' Note    : Copyright 2004-2015, Eric Woodruff, All rights reserved
' Compiler: Visual Basic .NET
'
' This application is used to demonstrate various features of the EWSoftware PDI classes
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 12/01/2004  EFW  Created the code
'================================================================================================================

''' <summary>
''' This implements the main menu form and the application entry point.
''' </summary>
Public Partial Class MainForm
    Inherits System.Windows.Forms.Form

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New()
        MyBase.New()

        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Exit the application
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnExit_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' Test the holiday classes
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnHolidays_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles btnHolidays.Click
        Using dlg As New HolidayTestForm()
            dlg.ShowDialog()
        End Using
    End Sub

    ''' <summary>
    ''' Test the recurrence class
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnRRULE_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles btnRRULE.Click
        Using dlg As New RRuleTestForm()
            dlg.ShowDialog()
        End Using
    End Sub

    ''' <summary>
    ''' Test iCalendar recurrence
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnTestCalRecur_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles btnTestCalRecur.Click
        Using dlg As New EventRecurTestForm()
            dlg.ShowDialog()
        End Using
    End Sub

    ''' <summary>
    ''' Test the VTIMEZONE classes
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnTestVTimeZone_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles btnTestVTimeZone.Click
        Using dlg As New VTimeZoneTestForm()
            dlg.ShowDialog()
        End Using
    End Sub

    ''' <summary>
    ''' View application copyright and version information
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnAbout_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles btnAbout.Click
        Using dlg As New AboutDlg()
            dlg.ShowDialog()
        End Using
    End Sub

End Class
