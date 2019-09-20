//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : VCardBrowser.aspx.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2019
// Note    : Copyright 2004-2019, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This page is used to demonstrate the vCard classes
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 01/23/2005  EFW  Created the code
//===============================================================================================================

using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EWSoftware.PDI.Objects;
using EWSoftware.PDI.Parser;
using EWSoftware.PDI.Properties;

namespace PDIWebDemoCS
{
	/// <summary>
	/// This page is used to demonstrate the vCard classes
	/// </summary>
	public partial class VCardBrowser : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            this.Page.Title = "vCard Browser";

            lblMsg.Text = String.Empty;

            VCardCollection vc = (VCardCollection)Session["VCards"];

            // Load a default set of vCards on first use and store them in the session if not already there and
            // bind them to the data grid.
            if(!Page.IsPostBack || vc == null)
            {
                if(vc == null)
                {
                    if(Page.IsPostBack)
                        lblMsg.Text = "Session appears to have timed out.  Default vCards loaded.";

                    vc = VCardParser.ParseFromFile(Server.MapPath("RFC2426.vcf"));
                    vc.Sort(VCardSorter);

                    Session["VCards"] = vc;
                }

                dgVCards.DataSource = vc;
                dgVCards.DataBind();
            }
		}

        /// <summary>
        /// HTML encode values displayed in the grid
        /// </summary>
        /// <param name="oValue">The value to encode</param>
        /// <returns>The value as an HTML-encoded string</returns>
        protected static string EncodeValue(object oValue)
        {
            if(oValue != null)
                return HttpUtility.HtmlEncode(oValue.ToString());

            return "&nbsp;";
        }

        /// <summary>
        /// Load a vCard file uploaded by the user
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            VCardCollection vc;

            if(hifUpload.Value == null || hifUpload.Value.Length == 0)
            {
                lblMsg.Text = "Specify a filename to upload";
                return;
            }

            // Get the file data from the uploaded stream
            try
            {
                // Set the file and property encodings to use.  Since we are opening the stream, we have to pass
                // the encoding to the StreamReader rather than using PDIParser.DefaultEncoding.
                Encoding fileEnc;

                switch(cboFileEncoding.SelectedIndex)
                {
                    case 0:
                        fileEnc = new UTF8Encoding(false, false);
                        break;

                    case 1:
                        fileEnc = Encoding.GetEncoding("iso-8859-1");
                        break;

                    default:
                        fileEnc = new ASCIIEncoding();
                        break;
                }

                // This is only applicable for vCard 2.1
                switch(cboPropEncoding.SelectedIndex)
                {
                    case 0:
                        BaseProperty.DefaultEncoding = new UTF8Encoding(false, false);
                        break;

                    case 1:
                        BaseProperty.DefaultEncoding = Encoding.GetEncoding("iso-8859-1");
                        break;

                    default:
                        BaseProperty.DefaultEncoding = new ASCIIEncoding();
                        break;
                }

                using(var sr = new StreamReader(hifUpload.PostedFile.InputStream, fileEnc))
                {
                    vc = VCardParser.ParseFromStream(sr);
                    vc.Sort(VCardSorter);
                    Session["VCards"] = vc;

                    dgVCards.DataSource = vc;
                    dgVCards.DataBind();
                }

                lblMsg.Text = "The file was loaded successfully";
            }
            catch(PDIParserException pex)
            {
                System.Diagnostics.Debug.WriteLine(pex.ToString());
                lblMsg.Text = "Unable to parse file: " + pex.Message;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                lblMsg.Text = "Unable to load file: " + ex.Message;
            }
        }

        /// <summary>
        /// This handles various commands for the data grid
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgVCards_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            VCardCollection vc = (VCardCollection)Session["VCards"];

            switch(e.CommandName)
            {
                case "Add":
                    vc.Add(new VCard());

                    Response.Redirect(String.Format("VCardDetails.aspx?Index={0}", vc.Count - 1));
                    break;

                case "Edit":
                    if(e.Item.ItemIndex < vc.Count)
                        Response.Redirect(String.Format("VCardDetails.aspx?Index={0}", e.Item.ItemIndex));
                    break;

                case "Download":
                    if(vc.Count == 0)
                        lblMsg.Text = "No vCards to download";
                    else
                    {
                        // This is only applicable for vCard 2.1
                        switch(cboPropEncoding.SelectedIndex)
                        {
                            case 0:
                                BaseProperty.DefaultEncoding = new UTF8Encoding(false, false);
                                break;

                            case 1:
                                BaseProperty.DefaultEncoding = Encoding.GetEncoding("iso-8859-1");
                                break;

                            default:
                                BaseProperty.DefaultEncoding = new ASCIIEncoding();
                                break;
                        }

                        // Send the file to the user
                        this.Response.ClearContent();
                        this.Response.ContentType = "text/vcard";
                        this.Response.AppendHeader("Content-Disposition", "inline;filename=VCards.vcf");

                        // The collection can be written directly to the stream.  Note that more likely than not,
                        // it will be written as UTF-8 encoded data.
                        vc.WriteToStream(Response.Output);
                        Response.End();
                    }
                    break;
            }
        }

        /// <summary>
        /// Delete a vCard from the collection
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgVCards_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            VCardCollection vc = (VCardCollection)Session["VCards"];

            if(e.Item.ItemIndex < vc.Count)
                vc.RemoveAt(e.Item.ItemIndex);

            dgVCards.DataSource = vc;
            dgVCards.DataBind();
        }

        /// <summary>
        /// This is an example of sorting a vCard collection
        /// </summary>
        /// <param name="x">The first vCard</param>
        /// <param name="y">The second vCard</param>
        /// <returns>0 if equal, -1 if x is less than y, or 1 if x is greater than y</returns>
        /// <remarks>Due to the variety of properties in a vCard, sorting is left up to the developer utilizing a
        /// comparison delegate.  This example sorts the collection by the name property taking into account the
        /// SortStringProperty if set.</remarks>
        private static int VCardSorter(VCard x, VCard y)
        {
            string sortName1, sortName2;

            // Get the names to compare.  Precedence is given to the SortStringProperty as that is the purpose
            // of its existence.
            sortName1 = x.SortString.Value;

            if(String.IsNullOrWhiteSpace(sortName1))
                sortName1 = x.Name.SortableName;

            sortName2 = y.SortString.Value;

            if(String.IsNullOrWhiteSpace(sortName2))
                sortName2 = y.Name.SortableName;

            // For descending order, change this to compare name 2 to name 1 instead.
            return String.Compare(sortName1, sortName2, StringComparison.CurrentCulture);
        }
    }
}
