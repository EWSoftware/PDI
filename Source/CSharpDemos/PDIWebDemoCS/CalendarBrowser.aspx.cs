//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : CalendarBrowser.aspx.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/22/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This page is used to demonstrate the vCalendar/iCalendar classes
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
	/// This page is used to demonstrate the vCalendar/iCalendar classes
	/// </summary>
	public partial class CalendarBrowser : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            this.Page.Title = "vCalendar/iCalendar Browser";

            lblMsg.Text = String.Empty;

            VCalendar vc = (VCalendar)Session["VCalendar"];

            // Load a default calendar on first use and store it in the session if not already there and bind it
            // to the data grids.
            if(!Page.IsPostBack || vc == null)
            {
                if(vc == null)
                {
                    if(Page.IsPostBack)
                        lblMsg.Text = "Session appears to have timed out.  Default calendar loaded.";

                    vc = VCalendarParser.ParseFromFile(Server.MapPath("RFC2445.ics"));

                    vc.Events.Sort(CalendarSorter);
                    vc.ToDos.Sort(CalendarSorter);
                    vc.Journals.Sort(CalendarSorter);
                    vc.FreeBusys.Sort(CalendarSorter);

                    Session["VCalendar"] = vc;
                }

                dgEvents.DataSource = vc.Events;
                dgToDos.DataSource = vc.ToDos;
                dgJournals.DataSource = vc.Journals;
                dgFreeBusys.DataSource = vc.FreeBusys;

                dgEvents.DataBind();
                dgToDos.DataBind();
                dgJournals.DataBind();
                dgFreeBusys.DataBind();

                if(vc.Version == SpecificationVersions.vCalendar10)
                {
                    lblVersion.Text = "vCalendar 1.0";
                    dgJournals.Visible = dgFreeBusys.Visible = false;
                }
                else
                {
                    lblVersion.Text = "iCalendar 2.0";
                    dgJournals.Visible = dgFreeBusys.Visible = true;
                }
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
        /// Load a calendar file uploaded by the user.  It can be a vCalendar or an iCalendar file
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            VCalendar vc = (VCalendar)Session["VCalendar"];

            if(hifUpload.Value == null || hifUpload.Value.Length == 0)
            {
                lblMsg.Text = "Specify a filename to upload";
                return;
            }

            // Get the file data from the uploaded stream
            try
            {
                // Dispose of the old calendar
                vc.Dispose();

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

                // This is only applicable for vCalendar 1.0
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
                    vc = VCalendarParser.ParseFromStream(sr);

                    vc.Events.Sort(CalendarSorter);
                    vc.ToDos.Sort(CalendarSorter);
                    vc.Journals.Sort(CalendarSorter);
                    vc.FreeBusys.Sort(CalendarSorter);

                    Session["VCalendar"] = vc;

                    dgEvents.DataSource = vc.Events;
                    dgToDos.DataSource = vc.ToDos;
                    dgJournals.DataSource = vc.Journals;
                    dgFreeBusys.DataSource = vc.FreeBusys;

                    dgEvents.DataBind();
                    dgToDos.DataBind();
                    dgJournals.DataBind();
                    dgFreeBusys.DataBind();

                    if(vc.Version == SpecificationVersions.vCalendar10)
                    {
                        lblVersion.Text = "vCalendar 1.0";
                        dgJournals.Visible = dgFreeBusys.Visible = false;
                    }
                    else
                    {
                        lblVersion.Text = "iCalendar 2.0";
                        dgJournals.Visible = dgFreeBusys.Visible = true;
                    }
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
        /// Download the calendar file
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            VCalendar vc = (VCalendar)Session["VCalendar"];

            // Send the file to the user
            this.Response.ClearContent();

            if(vc.Version == SpecificationVersions.vCalendar10)
            {
                this.Response.ContentType = "text/x-vcalendar";
                this.Response.AppendHeader("Content-Disposition", "inline;filename=Calendar.vcs");
            }
            else
            {
                this.Response.ContentType = "text/calendar";
                this.Response.AppendHeader("Content-Disposition", "inline;filename=Calendar.ics");
            }

            // This is only applicable for vCalendar 1.0
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

            // The collection can be written directly to the stream.  Note that more likely than not, it will be
            // written as UTF-8 encoded data.
            vc.WriteToStream(Response.Output);
            Response.End();
        }

        /// <summary>
        /// This handles various commands for the Events data grid
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgEvents_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            VCalendar vc = (VCalendar)Session["VCalendar"];

            switch(e.CommandName)
            {
                case "Add":
                    vc.Events.Add(new VEvent());

                    Response.Redirect(String.Format("CalendarObjectDetails.aspx?Index={0}&Type=Event",
                        vc.Events.Count - 1));
                    break;

                case "Edit":
                    if(e.Item.ItemIndex < vc.Events.Count)
                        Response.Redirect(String.Format("CalendarObjectDetails.aspx?Index={0}&Type=Event",
                            e.Item.ItemIndex));
                    break;
            }
        }

        /// <summary>
        /// Delete an event from the collection
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgEvents_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            VCalendar vc = (VCalendar)Session["VCalendar"];

            if(e.Item.ItemIndex < vc.Events.Count)
                vc.Events.RemoveAt(e.Item.ItemIndex);

            dgEvents.DataSource = vc.Events;
            dgEvents.DataBind();
        }

        /// <summary>
        /// This handles various commands for the To-Do data grid
        /// </summary>
        /// <param name="source">The source of the ToDo</param>
        /// <param name="e">The event arguments</param>
        protected void dgToDos_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            VCalendar vc = (VCalendar)Session["VCalendar"];

            switch(e.CommandName)
            {
                case "Add":
                    vc.ToDos.Add(new VToDo());

                    Response.Redirect(String.Format("CalendarObjectDetails.aspx?Index={0}&Type=ToDo",
                        vc.ToDos.Count - 1));
                    break;

                case "Edit":
                    if(e.Item.ItemIndex < vc.ToDos.Count)
                        Response.Redirect(String.Format("CalendarObjectDetails.aspx?Index={0}&Type=ToDo",
                            e.Item.ItemIndex));
                    break;
            }
        }

        /// <summary>
        /// Delete an ToDo from the collection
        /// </summary>
        /// <param name="source">The source of the ToDo</param>
        /// <param name="e">The ToDo arguments</param>
        protected void dgToDos_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            VCalendar vc = (VCalendar)Session["VCalendar"];

            if(e.Item.ItemIndex < vc.ToDos.Count)
                vc.ToDos.RemoveAt(e.Item.ItemIndex);

            dgToDos.DataSource = vc.ToDos;
            dgToDos.DataBind();
        }

        /// <summary>
        /// This handles various commands for the Journals data grid
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgJournals_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            VCalendar vc = (VCalendar)Session["VCalendar"];

            switch(e.CommandName)
            {
                case "Add":
                    vc.Journals.Add(new VJournal());

                    Response.Redirect(String.Format("CalendarObjectDetails.aspx?Index={0}&Type=Journal",
                        vc.Journals.Count - 1));
                    break;

                case "Edit":
                    if(e.Item.ItemIndex < vc.Journals.Count)
                        Response.Redirect(String.Format("CalendarObjectDetails.aspx?Index={0}&Type=Journal",
                            e.Item.ItemIndex));
                    break;
            }
        }

        /// <summary>
        /// Delete an event from the collection
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgJournals_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            VCalendar vc = (VCalendar)Session["VCalendar"];

            if(e.Item.ItemIndex < vc.Journals.Count)
                vc.Journals.RemoveAt(e.Item.ItemIndex);

            dgJournals.DataSource = vc.Journals;
            dgJournals.DataBind();
        }

        /// <summary>
        /// This handles various commands for the Free Busy data grid
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgFreeBusys_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            VCalendar vc = (VCalendar)Session["VCalendar"];

            switch(e.CommandName)
            {
                case "Add":
                    vc.FreeBusys.Add(new VFreeBusy());

                    Response.Redirect(String.Format("FreeBusyDetails.aspx?Index={0}",
                        vc.FreeBusys.Count - 1));
                    break;

                case "Edit":
                    if(e.Item.ItemIndex < vc.FreeBusys.Count)
                        Response.Redirect(String.Format("FreeBusyDetails.aspx?Index={0}",
                            e.Item.ItemIndex));
                    break;
            }
        }

        /// <summary>
        /// Delete an event from the collection
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgFreeBusys_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            VCalendar vc = (VCalendar)Session["VCalendar"];

            if(e.Item.ItemIndex < vc.FreeBusys.Count)
                vc.FreeBusys.RemoveAt(e.Item.ItemIndex);

            dgFreeBusys.DataSource = vc.FreeBusys;
            dgFreeBusys.DataBind();
        }

        /// <summary>
        /// This is an example of how you can sort calendar object collections
        /// </summary>
        /// <param name="x">The first calendar object</param>
        /// <param name="y">The second calendar object</param>
        /// <remarks>Due to the variety of properties in a calendar object, sorting is left up to the developer
        /// utilizing a comparison delegate.  This example sorts the collection by the start date/time property
        /// and, if they are equal, the summary description.  This is used to handle all of the collection
        /// types.</remarks>
        private static int CalendarSorter(CalendarObject x, CalendarObject y)
        {
            DateTime d1, d2;
            string summary1, summary2;

            if(x is VEvent)
            {
                VEvent e1 = (VEvent)x, e2 = (VEvent)y;

                d1 = e1.StartDateTime.TimeZoneDateTime;
                d2 = e2.StartDateTime.TimeZoneDateTime;
                summary1 = e1.Summary.Value;
                summary2 = e2.Summary.Value;
            }
            else
                if(x is VToDo)
                {
                    VToDo t1 = (VToDo)x, t2 = (VToDo)y;

                    d1 = t1.StartDateTime.TimeZoneDateTime;
                    d2 = t2.StartDateTime.TimeZoneDateTime;
                    summary1 = t1.Summary.Value;
                    summary2 = t2.Summary.Value;
                }
                else
                    if(x is VJournal)
                    {
                        VJournal j1 = (VJournal)x, j2 = (VJournal)y;

                        d1 = j1.StartDateTime.TimeZoneDateTime;
                        d2 = j2.StartDateTime.TimeZoneDateTime;
                        summary1 = j1.Summary.Value;
                        summary2 = j2.Summary.Value;
                    }
                    else
                    {
                        VFreeBusy f1 = (VFreeBusy)x, f2 = (VFreeBusy)y;

                        d1 = f1.StartDateTime.TimeZoneDateTime;
                        d2 = f2.StartDateTime.TimeZoneDateTime;
                        summary1 = f1.Organizer.Value;
                        summary2 = f2.Organizer.Value;
                    }

            if(d1.CompareTo(d2) == 0)
            {
                if(summary1 == null)
                    summary1 = String.Empty;

                if(summary2 == null)
                    summary2 = String.Empty;

                // For descending order, change this to compare summary 2 to summary 1 instead
                return String.Compare(summary1, summary2, StringComparison.CurrentCulture);
            }

            // For descending order, change this to compare date 2 to date 1 instead
            return d1.CompareTo(d2);
        }
    }
}
