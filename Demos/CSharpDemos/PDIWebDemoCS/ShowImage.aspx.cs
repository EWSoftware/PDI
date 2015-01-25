//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : ShowImage.aspx.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/31/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This is used to display inline vCard photo and logo image data
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 01/24/2005  EFW  Created the code
//===============================================================================================================

using System;

using EWSoftware.PDI.Objects;

namespace PDIWebDemoCS
{
	/// <summary>
	/// This is used to display inline vCard photo and logo image data
	/// </summary>
	public partial class ShowImage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            VCardCollection vc;
            VCard vCard;
            int idx;

            if(!Int32.TryParse(Request.QueryString["Index"], out idx))
            {
                // If not valid just quit
                Response.End();
                return;
            }

            vc = (VCardCollection)Session["VCards"];

            if(vc == null || idx < 0 || idx >= vc.Count)
            {
                Response.End();
                return;
            }

            vCard = vc[idx];
            
            Response.ClearContent();

            if(Request.QueryString["Photo"] != null && vCard.Photo.Value != null)
            {
                Response.ContentType = "image/" + vCard.Photo.ImageType.ToLowerInvariant();
                byte[] byImage = vCard.Photo.GetImageBytes();
                Response.OutputStream.Write(byImage, 0, byImage.Length);
            }
            else
                if(vCard.Logo.Value != null)
                {
                    Response.ContentType = "image/" + vCard.Logo.ImageType.ToLowerInvariant();
                    byte[] byImage = vCard.Logo.GetImageBytes();
                    Response.OutputStream.Write(byImage, 0, byImage.Length);
                }

            Response.End();
		}
	}
}
