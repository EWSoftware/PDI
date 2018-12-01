'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : MasterPage.master.cs
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 12/29/2014
' Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
' Compiler: Microsoft VB.NET
'
' PDI Web Demo master page
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 01/16/2005  EFW  Created the code
'================================================================================================================

Namespace PDIWebDemoVB

    Public Partial Class MasterPage
        Inherits System.Web.UI.MasterPage

        ''' <summary>
        ''' This loads the values into the controls on load
        ''' </summary>
        ''' <param name="sender">The sender of the event</param>
        ''' <param name="e">The event arguments</param>
        Protected Sub Page_Load(sender As Object, e As EventArgs)
            If Not Page.IsPostBack
                lblVersion.Text = ConfigurationManager.AppSettings("Version")

                Dim dsMenu As DataSet = new DataSet()
                dsMenu.ReadXmlSchema(Server.MapPath(ConfigurationManager.AppSettings("AppMenuXSD")))
                dsMenu.ReadXml(Server.MapPath(ConfigurationManager.AppSettings("AppMenuXML")))

                rptMenu.DataSource = dsMenu
                rptMenu.DataBind()
            End If
        End Sub

        ''' <summary>
        ''' Set the page title
        ''' </summary>
        ''' <param name="e">The event arguments</param>
        Protected Overrides Sub OnPreRender(e As EventArgs)
            lblPageTitle.Text = Me.Page.Title
            MyBase.OnPreRender(e)
        End Sub

    End Class

End Namespace
