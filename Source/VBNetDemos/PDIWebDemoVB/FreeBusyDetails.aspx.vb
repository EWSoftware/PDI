'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : FreeBusyDetails.aspx.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 12/31/2014
' Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
' Compiler: Microsoft VB.NET
'
' This page is used to demonstrate the VFreeBusy class.  Currently, it allows editing of some basic information.
' Information in the data grids could also be edited.  Time constraints limit what I have implemented so far but
' I may expand on this at a later date.
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 01/27/2005  EFW  Created the code
'================================================================================================================

Imports System.Globalization

Imports EWSoftware.PDI
Imports EWSoftware.PDI.Objects
Imports EWSoftware.PDI.Properties

Namespace PDIWebDemoVB

    Partial Class FreeBusyDetails
        Inherits System.Web.UI.Page

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim cal As VCalendar, fb As VFreeBusy, idx As Integer

            If Not Page.IsPostBack Then
                cal = DirectCast(Session("VCalendar"), VCalendar)

                If cal Is Nothing Then
                    Response.Redirect("CalendarBrowser.aspx")
                End If

                If Not Int32.TryParse(Request.QueryString("Index"), idx) Then
                    ' If not valid just go back to the browser form
                    Response.Redirect("CalendarBrowser.aspx")
                    Return
                End If

                ' Force it to be valid
                If idx < 0 Or idx >= cal.FreeBusys.Count Then idx = 0

                Me.ViewState("FreeBusyIndex") = idx

                ' Load the data into the controls
                fb = cal.FreeBusys(idx)

                ' We use the start date's time zone ID for the time zone label.  It should represent the time
                ' zone used throughout the component.
                lblTimeZone.Text = fb.StartDateTime.TimeZoneId

                ' General properties
                lblUniqueId.Text = fb.UniqueId.Value
                txtOrganizer.Text = fb.Organizer.Value
                txtContact.Text = fb.Contact.Value

                If fb.StartDateTime.TimeZoneDateTime <> DateTime.MinValue Then
                    txtStartDate.Text = fb.StartDateTime.TimeZoneDateTime.ToString("G")
                End If

                If fb.EndDateTime.TimeZoneDateTime <> DateTime.MinValue Then
                    txtEndDate.Text = fb.EndDateTime.TimeZoneDateTime.ToString("G")
                End If

                If fb.Duration.DurationValue <> Duration.Zero Then
                    txtDuration.Text = fb.Duration.DurationValue.ToString(Duration.MaxUnit.Weeks)
                End If

                txtUrl.Text = fb.Url.Value
                txtComments.Text = fb.Comment.Value

                ' Bind the data grids to display collection info
                dgAttendees.DataSource = fb.Attendees
                dgFreeBusy.DataSource = fb.FreeBusy
                dgReqStats.DataSource = fb.RequestStatuses

                Page.DataBind()
            End If
        End Sub

        ' Save changes and return to the calendar browser
        Private Sub btnSave_Click(ByVal sender As Object, _
          ByVal e As System.EventArgs) Handles btnSave.Click
            Dim startDate As DateTime = DateTime.MinValue
            Dim endDate As DateTime = DateTime.MinValue
            Dim dur As Duration = Duration.Zero

            If Not Page.IsValid Then
                Return
            End if

            lblMsg.Text = Nothing

            ' Perform some edits
            If txtStartDate.Text.Trim().Length <> 0 AndAlso Not DateTime.TryParse(txtStartDate.Text,
              CultureInfo.CurrentCulture, DateTimeStyles.None, startDate) Then
                lblMsg.Text = "Invalid start date format<br>"
            End If

            If txtEndDate.Text.Trim().Length <> 0 AndAlso Not DateTime.TryParse(txtEndDate.Text,
              CultureInfo.CurrentCulture, DateTimeStyles.None, endDate) Then
                lblMsg.Text &= "Invalid end date format<br>"
            End If

            If txtDuration.Text.Trim().Length <> 0 AndAlso Not Duration.TryParse(txtDuration.Text, dur) Then
                lblMsg.Text &= "Invalid duration format<br>"
            End If

            If startDate <> DateTime.MinValue And endDate <> DateTime.MinValue And startDate > endDate Then
                lblMsg.Text &= "Start date must be less than or equal to end date<br>"
            End If

            If Not String.IsNullOrWhiteSpace(lblMsg.Text) Then
                Return
            End If

            Dim cal As VCalendar = DirectCast(Session("VCalendar"), VCalendar)

            ' Not very friendly, but it's just a demo
            If cal Is Nothing Then
                Response.Redirect("CalendarBrowser.aspx")
                Return
            End If

            Dim fb As VFreeBusy = cal.FreeBusys(DirectCast(Me.ViewState("FreeBusyIndex"), Integer))

            ' The unique ID is not changed
            fb.Organizer.Value = txtOrganizer.Text
            fb.Contact.Value = txtContact.Text

            ' We'll use the TimeZoneDateTime property on all date/time values so that they are set literally
            ' rather than being converted to the time zone as would happen with the DateTimeValue property.
            fb.StartDateTime.TimeZoneDateTime = startDate
            fb.StartDateTime.ValueLocation = ValLocValue.DateTime

            fb.EndDateTime.TimeZoneDateTime = endDate
            fb.StartDateTime.ValueLocation = ValLocValue.DateTime

            fb.Duration.DurationValue = dur
            fb.Url.Value = txtUrl.Text
            fb.Comment.Value = txtComments.Text

            Response.Redirect("CalendarBrowser.aspx")
        End Sub

        ' Exit without saving
        Private Sub btnExit_Click(ByVal sender As Object, _
          ByVal e As System.EventArgs) Handles btnExit.Click
            Response.Redirect("CalendarBrowser.aspx")
        End Sub
    End Class

End Namespace
