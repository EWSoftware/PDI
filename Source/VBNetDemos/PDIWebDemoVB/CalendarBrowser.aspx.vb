'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : CalendarBrowser.aspx.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 11/22/2018
' Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
' Compiler: Microsoft VB.NET
'
' This page is used to demonstrate the vCalendar/iCalendar classes
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

Imports System.IO

Imports EWSoftware.PDI.Objects
Imports EWSoftware.PDI.Parser
Imports EWSoftware.PDI.Properties

Namespace PDIWebDemoVB

Partial Class CalendarBrowser
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Page.Title = "vCalendar/iCalendar Browser"

        lblMsg.Text = String.Empty

        Dim vc As VCalendar = DirectCast(Session("VCalendar"), VCalendar)

        ' Load a default calendar on first use and store it in the session if not already there and bind it to
        ' the data grids.
        If Not Page.IsPostBack Or vc Is Nothing Then
            If vc Is Nothing Then
                If Page.IsPostBack Then
                    lblMsg.Text = "Session appears to have timed out.  Default calendar loaded."
                End If

                vc = VCalendarParser.ParseFromFile(Server.MapPath("RFC2445.ics"))

                vc.Events.Sort(AddressOf CalendarSorter)
                vc.ToDos.Sort(AddressOf CalendarSorter)
                vc.Journals.Sort(AddressOf CalendarSorter)
                vc.FreeBusys.Sort(AddressOf CalendarSorter)

                Session("VCalendar") = vc
            End If

            dgEvents.DataSource = vc.Events
            dgToDos.DataSource = vc.ToDos
            dgJournals.DataSource = vc.Journals
            dgFreeBusys.DataSource = vc.FreeBusys

            dgEvents.DataBind()
            dgToDos.DataBind()
            dgJournals.DataBind()
            dgFreeBusys.DataBind()

            If vc.Version = SpecificationVersions.vCalendar10 Then
                lblVersion.Text = "vCalendar 1.0"
                dgJournals.Visible = False
                dgFreeBusys.Visible = False
            Else
                lblVersion.Text = "iCalendar 2.0"
                dgJournals.Visible = True
                dgFreeBusys.Visible = True
            End If
        End If
    End Sub

    ' HTML encode values displayed in the grid.
    Protected Shared Function EncodeValue(oValue As Object) As String
        If Not (oValue Is Nothing) Then
            Return HttpUtility.HtmlEncode(oValue.ToString())
        End If

        Return "&nbsp;"
    End Function

    ' Load a calendar file uploaded by the user.  It can be a vCalendar
    ' or an iCalendar file.
    Private Sub btnUpload_Click(ByVal sender As Object, _
      ByVal e As System.EventArgs) Handles btnUpload.Click
        Dim vc As VCalendar = DirectCast(Session("VCalendar"), VCalendar)

        If hifUpload.Value Is Nothing OrElse hifUpload.Value.Length = 0 Then
            lblMsg.Text = "Specify a filename to upload"
            Return
        End If

        ' Get the file data from the uploaded stream
        Try
            ' Dispose of the old calendar
            vc.Dispose()

            ' Set the file and property encodings to use.  Since we are opening the stream, we have to pass the
            ' encoding to the StreamReader rather than using PDIParser.DefaultEncoding.
            Dim fileEnc As Encoding

            Select Case cboFileEncoding.SelectedIndex
                Case 0
                    fileEnc = New UTF8Encoding(False, False)

                Case 1
                    fileEnc = Encoding.GetEncoding("iso-8859-1")

                Case Else
                    fileEnc = New ASCIIEncoding()

            End Select

            ' This is only applicable for vCalendar 1.0
            Select Case cboPropEncoding.SelectedIndex
                Case 0
                    BaseProperty.DefaultEncoding = New UTF8Encoding(False, False)

                Case 1
                    BaseProperty.DefaultEncoding = Encoding.GetEncoding("iso-8859-1")

                Case Else
                    BaseProperty.DefaultEncoding = New ASCIIEncoding()

            End Select

            Using sr As New StreamReader(hifUpload.PostedFile.InputStream, fileEnc)
                vc = VCalendarParser.ParseFromStream(sr)

                vc.Events.Sort(AddressOf CalendarSorter)
                vc.ToDos.Sort(AddressOf CalendarSorter)
                vc.Journals.Sort(AddressOf CalendarSorter)
                vc.FreeBusys.Sort(AddressOf CalendarSorter)

                Session("VCalendar") = vc

                dgEvents.DataSource = vc.Events
                dgToDos.DataSource = vc.ToDos
                dgJournals.DataSource = vc.Journals
                dgFreeBusys.DataSource = vc.FreeBusys

                dgEvents.DataBind()
                dgToDos.DataBind()
                dgJournals.DataBind()
                dgFreeBusys.DataBind()

                If vc.Version = SpecificationVersions.vCalendar10 Then
                    lblVersion.Text = "vCalendar 1.0"
                    dgJournals.Visible = False
                    dgFreeBusys.Visible = False
                Else
                    lblVersion.Text = "iCalendar 2.0"
                    dgJournals.Visible = True
                    dgFreeBusys.Visible = True
                End If
            End Using

            lblMsg.Text = "The file was loaded successfully"

        Catch pex As PDIParserException
            System.Diagnostics.Debug.WriteLine(pex.ToString())
            lblMsg.Text = "Unable to parse file: " + pex.Message

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine(ex.ToString())
            lblMsg.Text = "Unable to load file: " + ex.Message

        End Try
    End Sub

    ' Download the calendar file.
    Private Sub btnDownload_Click(ByVal sender As Object, _
      ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim vc As VCalendar = DirectCast(Session("VCalendar"), VCalendar)

        ' Send the file to the user
        Me.Response.ClearContent()

        If vc.Version = SpecificationVersions.vCalendar10 Then
            Me.Response.ContentType = "text/x-vcalendar"
            Me.Response.AppendHeader("Content-Disposition", "inline;filename=Calendar.vcs")
        Else
            Me.Response.ContentType = "text/calendar"
            Me.Response.AppendHeader("Content-Disposition", "inline;filename=Calendar.ics")
        End If

        ' This is only applicable for vCalendar 1.0
        Select Case cboPropEncoding.SelectedIndex
            Case 0
                BaseProperty.DefaultEncoding = New UTF8Encoding(False, False)

            Case 1
                BaseProperty.DefaultEncoding = Encoding.GetEncoding("iso-8859-1")

            Case Else
                BaseProperty.DefaultEncoding = New ASCIIEncoding()

        End Select

        ' The collection can be written directly to the stream.  Note that more likely than not, it will be
        ' written as UTF-8 encoded data.
        vc.WriteToStream(Response.Output)
        Response.End()
    End Sub

    ' This handles various commands for the Events data grid
    Private Sub dgEvents_ItemCommand(ByVal source As Object, _
      ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) _
      Handles dgEvents.ItemCommand
        Dim vc As VCalendar = DirectCast(Session("VCalendar"), VCalendar)

        Select Case e.CommandName
            Case "Add"
                vc.Events.Add(New VEvent())

                Response.Redirect(String.Format("CalendarObjectDetails.aspx?Index={0}&Type=Event",
                    vc.Events.Count - 1))

            Case "Edit"
                If e.Item.ItemIndex < vc.Events.Count Then
                    Response.Redirect(String.Format("CalendarObjectDetails.aspx?Index={0}&Type=Event",
                        e.Item.ItemIndex))
                End If

        End Select
    End Sub

    ' Delete an event from the collection
    Private Sub dgEvents_DeleteCommand(ByVal source As Object, _
      ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) _
      Handles dgEvents.DeleteCommand
        Dim vc As VCalendar = DirectCast(Session("VCalendar"), VCalendar)

        If e.Item.ItemIndex < vc.Events.Count Then
            vc.Events.RemoveAt(e.Item.ItemIndex)
        End If

        dgEvents.DataSource = vc.Events
        dgEvents.DataBind()
    End Sub

    ' This handles various commands for the To-Do data grid
    Private Sub dgToDos_ItemCommand(ByVal source As Object, _
      ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) _
      Handles dgToDos.ItemCommand
        Dim vc As VCalendar = DirectCast(Session("VCalendar"), VCalendar)

        Select Case e.CommandName
            Case "Add"
                vc.ToDos.Add(New VToDo())

                Response.Redirect(String.Format("CalendarObjectDetails.aspx?Index={0}&Type=ToDo",
                    vc.ToDos.Count - 1))

            Case "Edit"
                If e.Item.ItemIndex < vc.ToDos.Count Then
                    Response.Redirect(String.Format("CalendarObjectDetails.aspx?Index={0}&Type=ToDo",
                        e.Item.ItemIndex))
                End If

        End Select
    End Sub

    ' Delete an ToDo from the collection
    Private Sub dgToDos_DeleteCommand(ByVal source As Object, _
      ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) _
      Handles dgToDos.DeleteCommand
        Dim vc As VCalendar = DirectCast(Session("VCalendar"), VCalendar)

        If e.Item.ItemIndex < vc.ToDos.Count Then
            vc.ToDos.RemoveAt(e.Item.ItemIndex)
        End If

        dgToDos.DataSource = vc.ToDos
        dgToDos.DataBind()
    End Sub

    ' This handles various commands for the Journals data grid
    Private Sub dgJournals_ItemCommand(ByVal source As Object, _
      ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) _
      Handles dgJournals.ItemCommand
        Dim vc As VCalendar = DirectCast(Session("VCalendar"), VCalendar)

        Select Case e.CommandName
            Case "Add"
                vc.Journals.Add(New VJournal())

                Response.Redirect(String.Format("CalendarObjectDetails.aspx?Index={0}&Type=Journal",
                    vc.Journals.Count - 1))

            Case "Edit"
                If e.Item.ItemIndex < vc.Journals.Count Then
                    Response.Redirect(String.Format("CalendarObjectDetails.aspx?Index={0}&Type=Journal",
                        e.Item.ItemIndex))
                End If

        End Select
    End Sub

    ' Delete an event from the collection
    Private Sub dgJournals_DeleteCommand(ByVal source As Object, _
      ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) _
      Handles dgJournals.DeleteCommand
        Dim vc As VCalendar = DirectCast(Session("VCalendar"), VCalendar)

        If e.Item.ItemIndex < vc.Journals.Count Then
            vc.Journals.RemoveAt(e.Item.ItemIndex)
        End If

        dgJournals.DataSource = vc.Journals
        dgJournals.DataBind()
    End Sub

    ' This handles various commands for the Free Busy data grid
    Private Sub dgFreeBusys_ItemCommand(ByVal source As Object, _
      ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) _
      Handles dgFreeBusys.ItemCommand
        Dim vc As VCalendar = DirectCast(Session("VCalendar"), VCalendar)

        Select Case e.CommandName
            Case "Add"
                vc.FreeBusys.Add(New VFreeBusy())

                Response.Redirect(String.Format("FreeBusyDetails.aspx?Index={0}",
                    vc.FreeBusys.Count - 1))

            Case "Edit"
                If e.Item.ItemIndex < vc.FreeBusys.Count Then
                    Response.Redirect(String.Format("FreeBusyDetails.aspx?Index={0}",
                        e.Item.ItemIndex))
                End If

        End Select
    End Sub

    ' Delete an event from the collection
    Private Sub dgFreeBusys_DeleteCommand(ByVal source As Object, _
      ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) _
      Handles dgFreeBusys.DeleteCommand
        Dim vc As VCalendar = DirectCast(Session("VCalendar"), VCalendar)

        If e.Item.ItemIndex < vc.FreeBusys.Count Then
            vc.FreeBusys.RemoveAt(e.Item.ItemIndex)
        End If

        dgFreeBusys.DataSource = vc.FreeBusys
        dgFreeBusys.DataBind()
    End Sub

    ''' <summary>
    ''' This is an example of how you can sort calendar object collections
    ''' </summary>
    ''' <param name="x">The first calendar object</param>
    ''' <param name="y">The second calendar object</param>
    ''' <remarks>Due to the variety of properties in a calendar object, sorting is left up to the developer
    ''' utilizing a comparison delegate.  This example sorts the collection by the start date/time property and,
    ''' if they are equal, the summary description.  This is used to handle all of the collection types.</remarks>
    Private Shared Function CalendarSorter(x As CalendarObject, y As CalendarObject) As Integer
        Dim d1, d2 As DateTime
        Dim summary1, summary2 As String

        If TypeOf x Is VEvent Then
            Dim e1 As VEvent = DirectCast(x, VEvent), e2 As VEvent = DirectCast(y, VEvent)

            d1 = e1.StartDateTime.TimeZoneDateTime
            d2 = e2.StartDateTime.TimeZoneDateTime
            summary1 = e1.Summary.Value
            summary2 = e2.Summary.Value

        ElseIf TypeOf x Is VToDo
            Dim t1 As VToDo = DirectCast(x, VToDo), t2 As VToDo = DirectCast(y, VToDo)

            d1 = t1.StartDateTime.TimeZoneDateTime
            d2 = t2.StartDateTime.TimeZoneDateTime
            summary1 = t1.Summary.Value
            summary2 = t2.Summary.Value

        ElseIf TypeOf x Is VJournal
            Dim j1 As VJournal = DirectCast(x, VJournal), j2 As VJournal = DirectCast(y, VJournal)

            d1 = j1.StartDateTime.TimeZoneDateTime
            d2 = j2.StartDateTime.TimeZoneDateTime
            summary1 = j1.Summary.Value
            summary2 = j2.Summary.Value

        Else
            Dim f1 As VFreeBusy = DirectCast(x, VFreeBusy), f2 As VFreeBusy = DirectCast(y, VFreeBusy)

            d1 = f1.StartDateTime.TimeZoneDateTime
            d2 = f2.StartDateTime.TimeZoneDateTime
            summary1 = f1.Organizer.Value
            summary2 = f2.Organizer.Value
        End If

        If d1.CompareTo(d2) = 0 Then
            If summary1 Is Nothing Then summary1 = String.Empty

            If summary2 Is Nothing Then summary2 = String.Empty

            ' For descending order, change this to compare summary 2 to summary 1 instead
            Return String.Compare(summary1, summary2, StringComparison.CurrentCulture)
        End If

        ' For descending order, change this to compare date 2 to date 1 instead
        Return d1.CompareTo(d2)
    End Function
End Class

End Namespace
