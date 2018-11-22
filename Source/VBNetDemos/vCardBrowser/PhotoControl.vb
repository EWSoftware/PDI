'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : PhotoControl.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/02/2015
' Note    : Copyright 2004-2015, Eric Woodruff, All rights reserved
' Compiler: Visual Basic .NET
'
' This is used to edit a vCard's photo and logo information.  It's nothing elaborate but does let you edit the
' properties fairly well.
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 12/06/2004  EFW  Created the code
'================================================================================================================

Imports System.ComponentModel
Imports System.IO
Imports System.Net

''' <summary>
''' A user control for editing vCard photo and logo properties
''' </summary>
Public Partial Class PhotoControl
    Inherits System.Windows.Forms.UserControl

    #Region "Private data members"
    '==========================================================================

    Private bmImage As Bitmap

    #End Region

    #Region "Properties"
    '==========================================================================

    ''' <summary>
    ''' This property is used to set or get the image filename
    ''' </summary>
    ''' <value>If set to a URL, an attempt is made to download the image and only the URL will be stored in the
    ''' vCard when saved unless the "Save in vCard" checkbox is checked.</value>
    <DefaultValue(""), Description("The filename for the image")> _
    Public Property ImageFilename As String
        Get
            Return txtFilename.Text
        End Get
        Set
            Dim s As Stream = Nothing

            ' We probably should also check that the image type is supported and set the image type combo box
            ' too, but this is a demo, so I'm going to leave it for a future enhancement once I get all the other
            ' demos done.
            txtFilename.Text = Value
            chkInline.Checked = False

            Me.Cursor = Cursors.WaitCursor

            If Not (Value Is Nothing) AndAlso (Value.StartsWith("http:", StringComparison.OrdinalIgnoreCase) OrElse
              Value.StartsWith("https:", StringComparison.OrdinalIgnoreCase) OrElse
              Value.StartsWith("file:", StringComparison.OrdinalIgnoreCase)) Then
                Try
                    If Value.StartsWith("http:", StringComparison.OrdinalIgnoreCase) = True Or _
                      Value.StartsWith("https:", StringComparison.OrdinalIgnoreCase) = True Then
                        Dim wrq As HttpWebRequest = DirectCast(WebRequest.Create(New Uri(Value)), HttpWebRequest)

                        Dim wrsp As WebResponse = wrq.GetResponse()
                        s = wrsp.GetResponseStream()
                    Else
                        If Value.StartsWith("file:", StringComparison.OrdinalIgnoreCase) = True Then
                            Dim frq As FileWebRequest = DirectCast(WebRequest.Create(New Uri(Value)), FileWebRequest)
                            Dim frsp As WebResponse = frq.GetResponse()
                            s = frsp.GetResponseStream()
                        End If
                    End If

                    bmImage.Dispose()
                    bmImage = New Bitmap(s)
                Catch
                    ' Ignore it, just create a blank image
                    bmImage = New Bitmap(1, 1)
                Finally
                    If Not (s Is Nothing) Then s.Close()
                End Try
            Else
                Try
                    bmImage.Dispose()

                    If Not String.IsNullOrWhiteSpace(value) Then
                        bmImage = New Bitmap(value)
                    Else
                        bmImage = new Bitmap(1, 1)
                    End If
                Catch
                    ' Ignore it, just create a blank image
                    bmImage = New Bitmap(1, 1)
                End Try
            End If

            pnlPhoto.Invalidate()
            Me.Cursor = Cursors.Default
        End Set
    End Property

    ''' <summary>
    ''' This is used to set or get the image type
    ''' </summary>
    <DefaultValue("GIF"), Description("The image type")> _
    Public Property ImageType As String
        Get
            Return DirectCast(cboImageType.SelectedItem, String)
        End Get
        Set
            Dim idx As Integer

            If Not (Value Is Nothing)
                idx = cboImageType.Items.IndexOf(Value)
            Else
                idx = 0
            End If

            If idx <> -1 Then
                cboImageType.SelectedIndex = idx
            Else
                cboImageType.SelectedIndex = 0
            End If
        End Set
    End Property

    ''' <summary>
    ''' This is used to set or get whether the image is stored in the vCard (true) or just a reference to it
    ''' (false).
    ''' </summary>
    <DefaultValue(false), Description("True to store the image in the vCard or false if it is stored externally")> _
    Public Property IsInline As Boolean
        Get
            Return chkInline.Checked
        End Get
        Set
            chkInline.Checked = Value
        End Set
    End Property
    #End Region

    '==========================================================================

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New()
        MyBase.New()

        InitializeComponent()

        bmImage = New Bitmap(1, 1)
    End Sub

    ''' <summary>
    ''' Draw the image in the panel.  Scrolling is enabled.
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub pnlPhoto_Paint(ByVal sender As Object, _
      ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlPhoto.Paint
        'Draw appropriate portion of image
        e.Graphics.DrawImage(bmImage, pnlPhoto.AutoScrollPosition.X, pnlPhoto.AutoScrollPosition.Y,
            bmImage.Width, bmImage.Height)
        pnlPhoto.AutoScrollMinSize = bmImage.Size
    End Sub

    ''' <summary>
    ''' Try to load an image from a file
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnLoad_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles btnLoad.Click
        Dim extension As String

        Using dlg As New OpenFileDialog()
            dlg.Title = "Load Image File"
            dlg.DefaultExt = "jpg"
            dlg.Filter = "Image files|*.jpg;*.gif;*.tif;*.bmp"
            dlg.InitialDirectory = Environment.CurrentDirectory

            If dlg.ShowDialog() = DialogResult.OK Then
                Me.ImageFilename = dlg.FileName

                ' If it loaded successfully, default to storing it inline
                If bmImage.Height <> 1 AndAlso bmImage.Width <> 1 Then
                    Me.IsInline = True
                    extension = Path.GetExtension(dlg.FileName).ToUpperInvariant()

                    If extension.Length > 1 And extension(0) = "."C Then
                        extension = extension.Substring(1)
                    End If

                    If extension = "JPG" Then
                        extension = "JPEG"
                    End If

                    Me.ImageType = extension
                End If
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Set the image bytes.  This is used if the image is stored as binary encoded data in the vCard
    ''' </summary>
    ''' <param name="imageBytes">The image bytes</param>
    Public Sub SetImageBytes(imageBytes As Byte())
        Try
            Me.Cursor = Cursors.WaitCursor

            bmImage.Dispose()
            bmImage = New Bitmap(New MemoryStream(imageBytes))
        Catch
            ' Ignore it, just create a blank image
            bmImage = New Bitmap(1, 1)
        Finally
            pnlPhoto.Invalidate()
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    ''' <summary>
    ''' Get the image bytes.  This is used if the image is stored as binary encoded data in the vCard
    ''' </summary>
    ''' <returns>The bytes for the current image</returns>
    Public Function GetImageBytes() As Byte()
        Using ms As New MemoryStream()
            bmImage.Save(ms, bmImage.RawFormat)
            Return ms.ToArray()
        End Using
    End Function

End Class
