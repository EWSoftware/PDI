'================================================================================================================
' File    : AboutDlg.vb
' Author  : Eric Woodruff
' Updated : 11/25/2018
' Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
' Compiler: Microsoft VB.NET
'
' This form is used to display application version information
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 12/03/2004  EFW  Created the code
'================================================================================================================

' Ignore Spelling: mailto

Imports System.Reflection

Public Partial Class AboutDlg
    Inherits System.Windows.Forms.Form

    Public Sub New()
        MyBase.New()

        InitializeComponent()
    End Sub

    Private Sub AboutDlg_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Get assembly information not available from the application object
        Dim asm As [Assembly] = [Assembly].GetEntryAssembly()
        Dim title As AssemblyTitleAttribute = DirectCast(AssemblyTitleAttribute.GetCustomAttribute(asm,
            GetType(AssemblyTitleAttribute)), AssemblyTitleAttribute)
		Dim copyright As AssemblyCopyrightAttribute= DirectCast(AssemblyCopyrightAttribute.GetCustomAttribute(asm,
			GetType(AssemblyCopyrightAttribute)), AssemblyCopyrightAttribute)
		Dim desc As AssemblyDescriptionAttribute= DirectCast(AssemblyDescriptionAttribute.GetCustomAttribute(asm,
			GetType(AssemblyDescriptionAttribute)), AssemblyDescriptionAttribute)

		' Set the labels
        lblName.Text = title.Title
		lblDescription.Text = desc.Description
		lblVersion.Text = "Version: " & Application.ProductVersion
		lblCopyright.Text = copyright.Copyright

		' Display components used by this assembly sorted by name
		For Each an As AssemblyName In asm.GetReferencedAssemblies()
			Dim lvi As  ListViewItem= lvComponents.Items.Add(an.Name)
			lvi.SubItems.Add(an.Version.ToString())
		Next

        lvComponents.Sorting = SortOrder.Ascending
        lvComponents.Sort()

        ' Set e-mail link
		lnkHelp.Links(0).LinkData = "mailto:" & lnkHelp.Text & "?Subject=EWSoftware CalendarBrowser Demo"
    End Sub

    Private Sub btnSysInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSysInfo.Click
        Try
            System.Diagnostics.Process.Start("MSInfo32.exe")
        Catch ex As Exception
            MessageBox.Show("Unable to launch system information viewer", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error)

            ' Log the exception to the debugger for the developer
            System.Diagnostics.Debug.Write(ex.ToString())
        End Try
    End Sub

    Private Sub lnkHelp_LinkClicked(ByVal sender As System.Object,
      ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
        Try
            ' Launch the e-mail URL, this will fail if user does not have an association for e-mail URLs
            System.Diagnostics.Process.Start(DirectCast(e.Link.LinkData, String))
        Catch ex As Exception
            MessageBox.Show("Unable to launch e-mail editor", "E-Mail Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error)

            ' Log the exception to the debugger for the developer
            System.Diagnostics.Debug.Write(ex.ToString())
        End Try
    End Sub
End Class
