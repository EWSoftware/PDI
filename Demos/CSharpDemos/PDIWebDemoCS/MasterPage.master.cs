//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : MasterPage.master.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/29/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// PDI Web Demo master page
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 01/16/2005  EFW  Created the code
//===============================================================================================================

using System;
using System.Data;
using System.Configuration;

namespace PDIWebDemoCS
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        /// <summary>
        /// This loads the values into the controls on load
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                lblVersion.Text = ConfigurationManager.AppSettings["Version"];

                DataSet dsMenu = new DataSet();
                dsMenu.ReadXmlSchema(Server.MapPath(ConfigurationManager.AppSettings["AppMenuXSD"]));
                dsMenu.ReadXml(Server.MapPath(ConfigurationManager.AppSettings["AppMenuXML"]));

                rptMenu.DataSource = dsMenu;
                rptMenu.DataBind();
            }
        }

        /// <summary>
        /// Set the page title
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnPreRender(EventArgs e)
        {
            lblPageTitle.Text = this.Page.Title;
            base.OnPreRender(e);
        }
    }
}
