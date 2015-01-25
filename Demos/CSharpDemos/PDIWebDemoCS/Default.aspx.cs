//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : DefaultPage.aspx.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/29/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// PDI Web Demo main page
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

namespace PDIWebDemoCS
{
	/// <summary>
	/// This is the main page for the web demo.
	/// </summary>
	public partial class DefaultPage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            this.Page.Title = "EWSoftware PDI Library Web Demo";
		}
	}
}
