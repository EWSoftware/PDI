'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : RRuleTestForm.aspx.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 12/31/2014
' Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
' Compiler: Microsoft VB.NET
'
' This page is used to demonstrate the recurrence engine
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 01/25/2005  EFW  Created the code
' 03/21/2005  EFW  Added the RecurrencePattern web server control
' 02/10/2007  EFW  Update for .NET 2.0 and new RecurrencePattern control
'================================================================================================================

Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Globalization
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Imports EWSoftware.PDI

Namespace PDIWebDemoVB

    Partial Class RRuleTestForm
        Inherits System.Web.UI.Page

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Me.Page.Title = "Recurrence Demo"

            ' On first load, set some defaults and bind the holiday list box to the defined recurrence holidays
            If Not Page.IsPostBack Then
                txtStartDate.Text = New DateTime(DateTime.Today.Year, 1, 1).ToString("G")
                txtRRULE.Text = "FREQ=DAILY;INTERVAL=5;COUNT=50"
                lbResults.DataTextFormatString = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern & " " &
                    CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern

                Dim r As New Recurrence(txtRRULE.Text)
                rpRecurrence.SetRecurrence(r)
                lblDescription.Text = r.ToDescription()

                lbHolidays.DataSource = Recurrence.Holidays
                lbHolidays.DataBind()
            End If
        End Sub

        ' Generate instances based on the settings in the pattern control
        Private Sub btnTestPattern_Click(ByVal sender As Object, _
          ByVal e As System.EventArgs) Handles btnTestPattern.Click
            Dim r As New Recurrence()
            rpRecurrence.GetRecurrence(r)

            ' Make the textbox match the pattern control and use it's event handler
            txtRRULE.Text = r.ToString()
            btnTest_Click(sender, e)
        End Sub

        ' Generate instances for the specified recurrence pattern
        Private Sub btnTest_Click(ByVal sender As Object, _
          ByVal e As System.EventArgs) Handles btnTest.Click
            Dim dc As DateTimeCollection
            Dim dt As Date
            Dim start As Integer
            Dim elapsed As Double

            Try
                lblCount.Text = String.Empty
                lblDescription.Text = String.Empty

                If Not DateTime.TryParse(txtStartDate.Text, CultureInfo.CurrentCulture, DateTimeStyles.None, dt) Then
                    lblCount.Text = "Invalid date/time or RRULE format"
                    Return
                End If

                ' Define the recurrence rule by parsing the text
                Dim r As New Recurrence(txtRRULE.Text)
                r.StartDateTime = dt

                ' Synch the pattern control to the RRULE if not called by the pattern's test button
                If Not sender.Equals(btnTestPattern) Then
                    rpRecurrence.SetRecurrence(r)
                End If

                ' Use some limitations to prevent overloading the server or timing out the page if possible
                If r.Frequency > RecurFrequency.Hourly And
                  (r.MaximumOccurrences = 0 Or r.MaximumOccurrences > 5000) Then
                    r.MaximumOccurrences = 5000
                    txtRRULE.Text = r.ToString()
                End If

                If r.MaximumOccurrences <> 0 Then
                    If r.MaximumOccurrences > 5000 Then
                        r.MaximumOccurrences = 5000
                        txtRRULE.Text = r.ToString()
                    End If
                Else
                    If r.Frequency = RecurFrequency.Hourly Then
                        If r.RecurUntil > r.StartDateTime.AddYears(5) Then
                            r.RecurUntil = r.StartDateTime.AddYears(5)
                            txtRRULE.Text = r.ToString()
                        End If
                    Else
                        If r.RecurUntil > r.StartDateTime.AddYears(50) Then
                            r.RecurUntil = r.StartDateTime.AddYears(50)
                            txtRRULE.Text = r.ToString()
                        End If
                    End If
                End If

                ' Time the calculation
                start = System.Environment.TickCount
                dc = r.InstancesBetween(r.StartDateTime, DateTime.MaxValue)
                elapsed = (System.Environment.TickCount - start) / 1000.0

                ' Bind the results to the list box
                lbResults.DataSource = dc
                lbResults.DataBind()

                lblCount.Text = String.Format("Found {0:N0} instances in {1:N2} " &
                    "seconds ({2:N2} instances/second)", dc.Count, elapsed, dc.Count / elapsed)

                ' Show a description of the pattern
                lblDescription.Text = r.ToDescription()

            Catch ex As Exception
                lblCount.Text = ex.Message
            End Try
        End Sub

    End Class

End Namespace
