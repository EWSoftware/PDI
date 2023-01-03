'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : AttachmentsControl.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/02/2023
' Note    : Copyright 2004-2023, Eric Woodruff, All rights reserved
'
' This is used to edit a calendar object's attachment properties
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 01/05/2005  EFW  Created the code
' 05/22/2007  EFW  Converted for use with .NET 2.0
'================================================================================================================

Imports System.IO

Imports EWSoftware.PDI.Properties

''' <summary>
''' This is used to edit a calendar object's attachment properties
''' </summary>
Public Partial Class AttachmentsControl
    Inherits System.Windows.Forms.UserControl

    Private ReadOnly attach As AttachPropertyCollection

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        attach = New AttachPropertyCollection()
        Me.SetButtonStates()
    End Sub

    ''' <summary>
    ''' Enable or disable buttons based on the collection item count
    ''' </summary>
    Private Sub SetButtonStates()
        Dim isEnabled As Boolean = (attach.Count <> 0)

        btnRemove.Enabled = isEnabled
        btnClear.Enabled = isEnabled
        lbAttachments_SelectedIndexChanged(Me, EventArgs.Empty)
    End Sub

    ''' <summary>
    ''' Initialize the dialog controls using the specified attachments collection
    ''' </summary>
    ''' <param name="attachments">The attachments from which to get the settings</param>
    Public Sub SetValues(attachments As AttachPropertyCollection)
        Dim desc As String, a As AttachProperty

        attach.Clear()
        attach.CloneRange(attachments)

        lbAttachments.Items.Clear()

        For Each a In attach
            If a.ValueLocation = ValLocValue.Binary Then
                desc = $"Inline - {a.FormatType}"
            Else
                desc = $"External - {a.FormatType}, {a.Value}"
            End If

            lbAttachments.Items.Add(desc)
        Next

        If attach.Count > 0 Then
            lbAttachments.SelectedIndex = 0
        Else
            lbAttachments.SelectedIndex = -1
        End If

        Me.SetButtonStates()
    End Sub

    ''' <summary>
    ''' Update the attachments collection with the dialog control values
    ''' </summary>
    ''' <param name="attachments">The attachments collection to update.</param>
    Public Sub GetValues(attachments As AttachPropertyCollection)
        attachments?.Clear()
        attachments?.CloneRange(attach)
    End Sub

    ''' <summary>
    ''' Enable or disable the Detach button depending on whether or not the selected attachment is inline
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub lbAttachments_SelectedIndexChanged(sender As Object, _
      e As System.EventArgs) Handles lbAttachments.SelectedIndexChanged
        If lbAttachments.SelectedIndex <> -1 AndAlso attach(lbAttachments.SelectedIndex).ValueLocation = ValLocValue.Binary Then
            btnDetach.Enabled = True
        Else
            btnDetach.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Pick a file using the Open File dialog box
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnLoad_Click(sender As Object, e As System.EventArgs) _
      Handles btnLoad.Click
        Using dlg As New OpenFileDialog()
            dlg.Title = "Add Attachment"
            dlg.InitialDirectory = Environment.CurrentDirectory

            If dlg.ShowDialog() = DialogResult.OK Then
                txtFilename.Text = dlg.FileName
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Add an attachment
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnAdd_Click(sender As Object, e As System.EventArgs) _
      Handles btnAdd.Click
        Dim desc As String

        If txtFilename.Text.Trim().Length = 0 Then
            MessageBox.Show("A filename is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtFilename.Focus()
            Return
        End If

        If txtFormat.Text.Trim().Length = 0 And chkInline.Checked = True Then
            MessageBox.Show("A format is required for inline attachments", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)
            txtFormat.Focus()
            Return
        End If

        Try
            Me.Cursor = Cursors.WaitCursor

            Dim a As New AttachProperty()

            If txtFormat.Text.Trim().Length = 0 Then
                a.FormatType = Nothing
            Else
                a.FormatType = txtFormat.Text
            End If

            ' If not inline, store the filename.  If inline, store the data from the file
            If chkInline.Checked = False Then
                a.ValueLocation = ValLocValue.Uri
                a.Value = txtFilename.Text
                desc = $"External - {a.FormatType}, {a.Value}"
            Else
                Using fs As New FileStream(txtFilename.Text, FileMode.Open, FileAccess.Read)
                    Dim byData As Byte() = New Byte(CType(fs.Length - 1, Integer)) {}
                    fs.Read(byData, 0, byData.Length)

                    a.ValueLocation = ValLocValue.Binary
                    a.SetAttachmentBytes(byData)
                End Using

                desc = $"Inline - {a.FormatType}"
            End If

            attach.Add(a)
            lbAttachments.Items.Add(desc)

        Catch ex As Exception
            Dim errorMsg As String = $"Unable to add attachment:{Environment.NewLine}{ex.Message}"

            If Not (ex.InnerException Is Nothing) Then
                errorMsg &= ex.InnerException.Message & Environment.NewLine

                If Not (ex.InnerException.InnerException Is Nothing) Then
                    errorMsg &= ex.InnerException.InnerException.Message
                End If
            End If

            System.Diagnostics.Debug.Write(ex)
            MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try

        Me.SetButtonStates()
    End Sub

    ''' <summary>
    ''' Remove an attachment
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnRemove_Click(sender As Object, e As System.EventArgs) _
      Handles btnRemove.Click
        If lbAttachments.SelectedIndex = -1 Then
            MessageBox.Show("Select an attachment to remove")
            Return
        End If

        ' Remove the attachment from the collection and the list box
        attach.RemoveAt(lbAttachments.SelectedIndex)
        lbAttachments.Items.RemoveAt(lbAttachments.SelectedIndex)
        Me.SetButtonStates()
    End Sub

    ''' <summary>
    ''' Clear all attachments
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnClear_Click(sender As Object, e As System.EventArgs) _
      Handles btnClear.Click
        attach.Clear()
        lbAttachments.Items.Clear()
        Me.SetButtonStates()
    End Sub

    ''' <summary>
    ''' Detach an inline attachment
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnDetach_Click(sender As Object, e As System.EventArgs) _
      Handles btnDetach.Click
        Using dlg As New SaveFileDialog()
            dlg.Title = "Save Inline Attachment"
            dlg.InitialDirectory = Environment.CurrentDirectory

            If dlg.ShowDialog() = DialogResult.OK Then
                Try
                    Me.Cursor = Cursors.WaitCursor

                    ' Open the file and write the data to it
                    Using fs As New FileStream(dlg.FileName, FileMode.Create, FileAccess.Write)
                        Dim byData As Byte() = attach(lbAttachments.SelectedIndex).GetAttachmentBytes()
                        fs.Write(byData, 0, byData.Length)
                    End Using

                Catch ex As Exception
                    Dim errorMsg As String = $"Unable to save attachment:{Environment.NewLine}{ex.Message}"

                    If Not (ex.InnerException Is Nothing) Then
                        errorMsg &= ex.InnerException.Message + Environment.NewLine

                        If Not (ex.InnerException.InnerException Is Nothing) Then
                            errorMsg &= ex.InnerException.InnerException.Message
                        End If
                    End If

                    System.Diagnostics.Debug.Write(ex)
                    MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    Me.Cursor = Cursors.Default
                End Try
            End If
        End Using
    End Sub

End Class
