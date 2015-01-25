'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : ShowImage.aspx.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 12/31/2014
' Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
' Compiler: Microsoft VB.NET
'
' This is used to display inline vCard photo and logo image data
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

Imports System
Imports System.ComponentModel
Imports System.Globalization
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Imports EWSoftware.PDI.Objects
Imports EWSoftware.PDI.Properties

Namespace PDIWebDemoVB

Partial Class ShowImage
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim vc As VCardCollection, vCard As VCard, idx As Integer

        If Not Int32.TryParse(Request.QueryString("Index"), idx) Then
            ' If not valid just quit
            Response.End()
            Return
        End If

        vc = DirectCast(Session("VCards"), VCardCollection)

        If vc Is Nothing Or idx < 0 Or idx >= vc.Count Then
            Response.End()
            Return
        End If

        vCard = vc(idx)

        Response.ClearContent()

        If Not (Request.QueryString("Photo") Is Nothing) And Not (vCard.Photo.Value Is Nothing) Then
            Response.ContentType = "image/" & vCard.Photo.ImageType.ToLowerInvariant()
            Dim byImage() As Byte = vCard.Photo.GetImageBytes()
            Response.OutputStream.Write(byImage, 0, byImage.Length)
        Else
            If Not (vCard.Logo.Value Is Nothing) Then
                Response.ContentType = "image/" & vCard.Logo.ImageType.ToLowerInvariant()
                Dim byImage() As Byte = vCard.Logo.GetImageBytes()
                Response.OutputStream.Write(byImage, 0, byImage.Length)
            End If
        End If

        Response.End()
    End Sub

End Class

End Namespace
