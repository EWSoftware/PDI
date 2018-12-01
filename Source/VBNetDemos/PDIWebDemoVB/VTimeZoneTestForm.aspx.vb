'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : VTimeZoneTestForm.aspx.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 11/21/2018
' Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
' Compiler: Microsoft VB.NET
'
' This page is used to demonstrate some of the time zone features of the PDI classes
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 01/25/2005  EFW  Created the code
'================================================================================================================

Imports System.Globalization

Imports EWSoftware.PDI
Imports EWSoftware.PDI.Objects

Namespace PDIWebDemoVB

    Partial Class VTimeZoneTestForm
        Inherits System.Web.UI.Page

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim tz As VTimeZone

            Me.Page.Title = "VTimeZone Demo"

            ' On first load, bind the drop down lists to the time zone IDs.
            If Not Page.IsPostBack Then
                ' The time zone information is loaded in the  Application_Start event in Global.asax.  We'll
                ' acquire a reader lock on the time zone collection as it's possible other sessions could be
                ' parsing calendars with time zone data that could change the collection.
                VCalendar.TimeZones.AcquireReaderLock(250)

                Try
                    For Each tz In VCalendar.TimeZones
                        cboSourceTimeZone.Items.Add(tz.TimeZoneId.Value)
                        cboDestTimeZone.Items.Add(tz.TimeZoneId.Value)
                    Next
                Finally
                    VCalendar.TimeZones.ReleaseReaderLock()
                End Try

                txtSourceDate.Text = New DateTime(DateTime.Today.Year, 1, 1, 10, 0, 0).ToString("G")
                btnApplySrc_Click(Me, EventArgs.Empty)
            End If
        End Sub

        ' Convert the selected date/time to local time and back and to the selected time zone and back to test
        ' the time zone conversion code.
        Private Sub btnApplySrc_Click(ByVal sender As Object, _
          ByVal e As System.EventArgs) Handles btnApplySrc.Click, btnApplyDest.Click
            Dim dt As DateTime

            Dim vtzSource As VTimeZone = VCalendar.TimeZones(cboSourceTimeZone.SelectedItem.Text)
            Dim vtzDest As VTimeZone = VCalendar.TimeZones(cboDestTimeZone.SelectedItem.Text)

            ' Show information for the selected time zones
            lblTimeZoneInfo.Text = String.Format("<strong>From Time Zone:</strong>{2}{2}{0}{2}" &
                "<strong>To Time Zone:</strong>{2}{2}{1}", vtzSource.ToString(), vtzDest.ToString(),
                Environment.NewLine)

            ' Do the conversions.  Each is round tripped to make sure it gets the expected results.  Note that
            ' there will be some anomalies when round tripping times that are near the standard time/DST shift as
            ' these times are somewhat ambiguous and their meaning can vary depending on which side of the shift
            ' you are on and the direction of the conversion.
            If Not DateTime.TryParse(txtSourceDate.Text, CultureInfo.CurrentCulture, DateTimeStyles.None, dt) Then
                lblLocalTime.Text = "Invalid date/time format"
                lblLocalBackToSource.Text = String.Empty
                lblDestTime.Text = String.Empty
                lblDestBackToSource.Text = String.Empty
                Return
            End If

            Dim dti As DateTimeInstance = VCalendar.TimeZoneTimeToLocalTime(dt, vtzSource.TimeZoneId.Value)

            lblLocalTime.Text = String.Format("{0} {1}", dti.StartDateTime, dti.StartTimeZoneName)

            dti = VCalendar.LocalTimeToTimeZoneTime(dti.StartDateTime, vtzSource.TimeZoneId.Value)
            lblLocalBackToSource.Text = String.Format("{0} {1}", dti.StartDateTime, dti.StartTimeZoneName)

            dti = VCalendar.TimeZoneToTimeZone(dt, vtzSource.TimeZoneId.Value, vtzDest.TimeZoneId.Value)
            lblDestTime.Text = String.Format("{0} {1}", dti.StartDateTime, dti.StartTimeZoneName)

            dti = VCalendar.TimeZoneToTimeZone(dti.StartDateTime, vtzDest.TimeZoneId.Value, vtzSource.TimeZoneId.Value)
            lblDestBackToSource.Text = String.Format("{0} {1}", dti.StartDateTime, dti.StartTimeZoneName)
        End Sub

        ' Download the time zone "database"
        Private Sub btnGetTZs_Click(ByVal sender As Object, _
          ByVal e As System.EventArgs) Handles btnGetTZs.Click
            Dim tz As VTimeZone

            ' We could just use a link to download the file but this shows another way
            Me.Response.ClearContent()
            Me.Response.ContentType = "text/calendar"
            Me.Response.AppendHeader("Content-Disposition", "inline;filename=TimeZoneDB.ics")

            ' Response.WriteFile would also work, but we'll do it the long way.  Since we are writing out the
            ' time zone collection by itself, we'll need to provide the calender wrapper.
            Me.Response.Write("BEGIN:VCALENDAR")
            Me.Response.Write(Environment.NewLine)
            Me.Response.Write("VERSION:2.0")
            Me.Response.Write(Environment.NewLine)
            Me.Response.Write("PRODID:-//EWSoftware//PDI Class Library//EN")
            Me.Response.Write(Environment.NewLine)

            ' Time zones can be written directly to the stream
            For Each tz In VCalendar.TimeZones
                tz.WriteToStream(Response.Output)
            Next

            Me.Response.Write("END:VCALENDAR")
            Me.Response.Write(Environment.NewLine)
            Response.End()
        End Sub

    End Class

End Namespace
