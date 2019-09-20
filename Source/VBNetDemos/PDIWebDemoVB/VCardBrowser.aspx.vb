'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : VCardBrowser.aspx.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/03/2019
' Note    : Copyright 2004-2019, Eric Woodruff, All rights reserved
' Compiler: Microsoft VB.NET
'
' This page is used to demonstrate the vCard classes
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

Imports System.IO

Imports EWSoftware.PDI.Objects
Imports EWSoftware.PDI.Parser
Imports EWSoftware.PDI.Properties

Namespace PDIWebDemoVB

Partial Class VCardBrowser
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Page.Title = "vCard Browser"

        lblMsg.Text = String.Empty

        ' Load a default set of vCards on first use and store them in the session if not already there and bind
        ' them to the data grid.
        If Not Page.IsPostBack Then
            Dim vc As VCardCollection = DirectCast(Session("VCards"), VCardCollection)

            If vc Is Nothing Then
                If Page.IsPostBack Then
                    lblMsg.Text = "Session appears to have timed out.  Default vCards loaded."
                End If

                vc = VCardParser.ParseFromFile(Server.MapPath("RFC2426.vcf"))
                vc.Sort(AddressOf VCardSorter)
                Session("VCards") = vc
            End If

            dgVCards.DataSource = vc
            dgVCards.DataBind()
        End If
    End Sub

    ' HTML encode values displayed in the grid.
    Protected Shared Function EncodeValue(oValue As Object) As String
        If Not (oValue Is Nothing) Then
            Return HttpUtility.HtmlEncode(oValue.ToString())
        End If

        Return "&nbsp;"
    End Function

    ' Load a vCard file uploaded by the user
    Private Sub btnUpload_Click(ByVal sender As Object, _
      ByVal e As System.EventArgs) Handles btnUpload.Click
        Dim vc As VCardCollection

        If hifUpload.Value Is Nothing OrElse hifUpload.Value.Length = 0 Then
            lblMsg.Text = "Specify a filename to upload"
            Return
        End If

        ' Get the file data from the uploaded stream
        Try
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

            ' This is only applicable for vCard 2.1
            Select Case cboPropEncoding.SelectedIndex
                Case 0
                    BaseProperty.DefaultEncoding = New UTF8Encoding(False, False)

                Case 1
                    BaseProperty.DefaultEncoding = Encoding.GetEncoding("iso-8859-1")

                Case Else
                    BaseProperty.DefaultEncoding = New ASCIIEncoding()

            End Select

            Using sr As StreamReader = New StreamReader(hifUpload.PostedFile.InputStream, fileEnc)
                vc = VCardParser.ParseFromStream(sr)
                vc.Sort(AddressOf VCardSorter)
                Session("VCards") = vc

                dgVCards.DataSource = vc
                dgVCards.DataBind()
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

    ' This handles various commands for the data grid
    Private Sub dgVCards_ItemCommand(ByVal source As Object, _
      ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgVCards.ItemCommand
        Dim vc As VCardCollection = DirectCast(Session("VCards"), VCardCollection)

        Select Case e.CommandName
            Case "Add"
                vc.Add(New VCard())

                Response.Redirect(String.Format("VCardDetails.aspx?Index={0}", vc.Count - 1))

            Case "Edit"
                If e.Item.ItemIndex < vc.Count
                    Response.Redirect(String.Format("VCardDetails.aspx?Index={0}", e.Item.ItemIndex))
                End If

            Case "Download"
                If vc.Count = 0 Then
                    lblMsg.Text = "No vCards to download"
                Else
                    ' This is only applicable for vCard 2.1
                    Select Case cboPropEncoding.SelectedIndex
                        Case 0
                            BaseProperty.DefaultEncoding = New UTF8Encoding(False, False)

                        Case 1
                            BaseProperty.DefaultEncoding = Encoding.GetEncoding("iso-8859-1")

                        Case Else
                            BaseProperty.DefaultEncoding = New ASCIIEncoding()

                    End Select

                    ' Send the file to the user
                    Me.Response.ClearContent()
                    Me.Response.ContentType = "text/vcard"
                    Me.Response.AppendHeader("Content-Disposition", "inline;filename=VCards.vcf")

                    ' The collection can be written directly to the stream.  Note that more likely than not, it
                    ' will be written as UTF-8 encoded data.
                    vc.WriteToStream(Response.Output)
                    Response.End()
                End If
        End Select
    End Sub

    ' Delete a vCard from the collection
    Private Sub dgVCards_DeleteCommand(ByVal source As Object, _
      ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgVCards.DeleteCommand
        Dim vc As VCardCollection = DirectCast(Session("VCards"), VCardCollection)

        If e.Item.ItemIndex < vc.Count Then
            vc.RemoveAt(e.Item.ItemIndex)
        End If

        dgVCards.DataSource = vc
        dgVCards.DataBind()
    End Sub

    ''' <summary>
    ''' This is an example of sorting a vCard collection
    ''' </summary>
    ''' <param name="x">The first vCard</param>
    ''' <param name="y">The second vCard</param>
    ''' <returns>0 if equal, -1 if x is less than y, or 1 if x is greater than y</returns>
    ''' <remarks>Due to the variety of properties in a vCard, sorting is left up to the developer utilizing a
    ''' comparison delegate.  This example sorts the collection by the name property taking into account the
    ''' SortStringProperty if set.</remarks>
    Private Shared Function VCardSorter(x As VCard, y As VCard) As Integer
        Dim sortName1, sortName2 As String

        ' Get the names to compare.  Precedence is given to the SortStringProperty as that is the purpose of its
        ' existence.
        sortName1 = x.SortString.Value

        If String.IsNullOrWhiteSpace(sortName1) Then
            sortName1 = x.Name.SortableName
        End If

        sortName2 = y.SortString.Value

        If String.IsNullOrWhiteSpace(sortName2) Then
            sortName2 = y.Name.SortableName
        End If

        ' For descending order, change this to compare name 2 to name 1 instead.
        Return String.Compare(sortName1, sortName2, StringComparison.CurrentCulture)
    End Function

End Class

End Namespace
