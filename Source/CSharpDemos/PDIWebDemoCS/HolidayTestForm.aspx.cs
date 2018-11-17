//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : HolidayTestForm.aspx.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/30/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This page is used to demonstrate the Holiday and date utility classes
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 01/20/2005  EFW  Created the code
//===============================================================================================================

using System;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

using EWSoftware.PDI;

namespace PDIWebDemoCS
{
	/// <summary>
	/// This page is used to demonstrate the Holiday and date utility classes
	/// </summary>
	public partial class HolidayTestForm : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            this.Page.Title = "Holiday Detection Demo";

            // Create a holiday collection with some default holidays, store it in the session, and bind it to
            // the data grid.
            if(!Page.IsPostBack)
            {
                HolidayCollection hc = (HolidayCollection)Session["Holidays"];

                // If we haven't been here before, create a new collection
                if(hc == null)
                {
                    hc = new HolidayCollection();
                    hc.AddStandardHolidays();
                    Session["Holidays"] = hc;
                }

                dgHolidays.DataSource = hc;
                dgHolidays.DataBind();

                txtFromYear.Text = (DateTime.Now.Year - 1).ToString();
                txtToYear.Text = (DateTime.Now.Year + 6).ToString();
                txtTestDate.Text = DateTime.Today.ToString("d");
            }
		}

        /// <summary>
        /// This handles various commands for the data grid
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgHolidays_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            HolidayCollection hc;

            switch(e.CommandName)
            {
                case "Add":
                    // Save changes to the edited item if there is one
                    if(dgHolidays.EditItemIndex != -1)
                        dgHolidays_UpdateCommand(source, new DataGridCommandEventArgs(
                            dgHolidays.Items[dgHolidays.EditItemIndex], e.CommandSource, e));

                    // Ignore the request if the page is not valid
                    if(!Page.IsValid)
                        return;

                    // Add a new holiday and go into edit mode on it
                    hc = (HolidayCollection)Session["Holidays"];
                    hc.AddFixed(DateTime.Today.Month, DateTime.Today.Day, true, String.Empty);

                    dgHolidays.EditItemIndex = hc.Count - 1;
                    dgHolidays.DataSource = hc;
                    dgHolidays.DataBind();
                    break;

                case "Clear":
                    // Clear all holidays
                    hc = (HolidayCollection)Session["Holidays"];
                    hc.Clear();

                    dgHolidays.EditItemIndex = -1;
                    dgHolidays.DataSource = hc;
                    dgHolidays.DataBind();
                    break;

                case "Default":
                    // Revert to the default set
                    hc = (HolidayCollection)Session["Holidays"];
                    hc.Clear();
                    hc.AddStandardHolidays();

                    dgHolidays.EditItemIndex = -1;
                    dgHolidays.DataSource = hc;
                    dgHolidays.DataBind();
                    break;

                case "Download":
                    // Save changes to the edited item if there is one
                    if(dgHolidays.EditItemIndex != -1)
                        dgHolidays_UpdateCommand(source, new DataGridCommandEventArgs(
                            dgHolidays.Items[dgHolidays.EditItemIndex], e.CommandSource, e));

                    // Ignore the request if the page is not valid
                    if(!Page.IsValid)
                        return;

                    hc = (HolidayCollection)Session["Holidays"];

                    // Send the file to the user as XML
                    this.Response.ClearContent();
                    this.Response.ContentType = "text/xml";
                    this.Response.AppendHeader("Content-Disposition", "inline;filename=Holidays.xml");

                    XmlSerializer xs = new XmlSerializer(typeof(HolidayCollection));
                    xs.Serialize(Response.OutputStream, hc);

                    Response.End();
                    break;
            }
        }

        /// <summary>
        /// Edit a holiday in the collection
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgHolidays_EditCommand(object source, DataGridCommandEventArgs e)
        {
            // Ignore the request if the page is not valid
            if(!Page.IsValid)
                return;

            // Save changes to the prior edited item
            if(dgHolidays.EditItemIndex != -1)
            {
                dgHolidays_UpdateCommand(source, new DataGridCommandEventArgs(
                    dgHolidays.Items[dgHolidays.EditItemIndex], e.CommandSource, e));

                if(!Page.IsValid)
                    return;
            }

            dgHolidays.EditItemIndex = e.Item.ItemIndex;
            dgHolidays.DataSource = Session["Holidays"];
            dgHolidays.DataBind();
        }

        /// <summary>
        /// Delete a holiday from the collection
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgHolidays_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            // Save changes to the edited item if it isn't the one being deleted
            if(dgHolidays.EditItemIndex != -1 && dgHolidays.EditItemIndex != e.Item.ItemIndex)
            {
                Page.Validate();
                dgHolidays_UpdateCommand(source, new DataGridCommandEventArgs(
                    dgHolidays.Items[dgHolidays.EditItemIndex], e.CommandSource, e));

                if(!Page.IsValid)
                    return;
            }

            HolidayCollection hc = (HolidayCollection)Session["Holidays"];

            hc.RemoveAt(e.Item.ItemIndex);
            dgHolidays.EditItemIndex = -1;
            dgHolidays.DataSource = hc;
            dgHolidays.DataBind();
        }

        /// <summary>
        /// Cancel changes to a holiday in the collection
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgHolidays_CancelCommand(object source, DataGridCommandEventArgs e)
        {
            HolidayCollection hc = (HolidayCollection)Session["Holidays"];

            // If it was a new item, remove it
            if(String.IsNullOrWhiteSpace(hc[e.Item.ItemIndex].Description))
                hc.RemoveAt(e.Item.ItemIndex);

            dgHolidays.EditItemIndex = -1;
            dgHolidays.DataSource = hc;
            dgHolidays.DataBind();
        }

        /// <summary>
        /// Update a holiday item in the collection
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgHolidays_UpdateCommand(object source, DataGridCommandEventArgs e)
        {
            HolidayCollection hc = (HolidayCollection)Session["Holidays"];

            if(!Page.IsValid)
                return;

            DropDownList cboMonth = (DropDownList)e.Item.FindControl("cboMonth");
            RadioButton rbFloating = (RadioButton)e.Item.FindControl("rbFloating");

            // Create holiday object and replace it in the collection
            if(rbFloating.Checked)
            {
                FloatingHoliday fl = new FloatingHoliday();

                fl.Month = cboMonth.SelectedIndex + 1;
                fl.Description = ((TextBox)e.Item.FindControl("txtDescription")).Text;
                fl.Occurrence = (DayOccurrence)((DropDownList)e.Item.FindControl("cboOccurrence")).SelectedIndex + 1;
                fl.Weekday = (System.DayOfWeek)((DropDownList)e.Item.FindControl("cboDayOfWeek")).SelectedIndex;
                fl.Offset = Convert.ToInt32(((TextBox)e.Item.FindControl("txtOffset")).Text);

                hc[e.Item.ItemIndex] = fl;
            }
            else
            {
                // See if the day of the month is valid for the month.  We won't accept Feb 29th either.
                int day = Convert.ToInt32(((TextBox)e.Item.FindControl("txtDayOfMonth")).Text);

                if(day > DateTime.DaysInMonth(2007, cboMonth.SelectedIndex + 1))
                {
                    ((RangeValidator)e.Item.FindControl("rvDOM")).IsValid = false;
                    return;
                }

                FixedHoliday fx = new FixedHoliday();

                fx.Month = cboMonth.SelectedIndex + 1;
                fx.Description = ((TextBox)e.Item.FindControl("txtDescription")).Text;
                fx.AdjustFixedDate = ((CheckBox)e.Item.FindControl("chkAdjustDate")).Checked;
                fx.Day = Convert.ToInt32(((TextBox)e.Item.FindControl("txtDayOfMonth")).Text);

                hc[e.Item.ItemIndex] = fx;
            }

            dgHolidays.EditItemIndex = -1;
            dgHolidays.DataSource = hc;
            dgHolidays.DataBind();
        }

        /// <summary>
        /// Bind data to the edit item template.  Since the holiday collection uses the abstract class, we'll
        /// bind data in here rather than in the HTML since we have to determine the type first.
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void dgHolidays_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.EditItem)
            {
                // The RecurOptsDataSource class contains static methods that return lists of common values we
                // can uses as the data sources for these drop down lists.
                DropDownList cboMonth = (DropDownList)e.Item.FindControl("cboMonth");
                cboMonth.DataSource = RecurOptsDataSource.MonthsOfYear;
                cboMonth.DataTextField = "Display";
                cboMonth.DataValueField = "Value";
                cboMonth.DataBind();

                DropDownList cboOccurrence = (DropDownList)e.Item.FindControl("cboOccurrence");
                cboOccurrence.DataSource = RecurOptsDataSource.DayOccurrences;
                cboOccurrence.DataTextField = "Display";
                cboOccurrence.DataValueField= "Value";
                cboOccurrence.DataBind();

                DropDownList cboDayOfWeek = (DropDownList)e.Item.FindControl("cboDayOfWeek");
                cboDayOfWeek.DataSource = RecurOptsDataSource.DayOfWeek;
                cboDayOfWeek.DataTextField = "Display";
                cboDayOfWeek.DataValueField = "Value";
                cboDayOfWeek.DataBind();

                RadioButton rbFloating = (RadioButton)e.Item.FindControl("rbFloating");
                RadioButton rbFixed = (RadioButton)e.Item.FindControl("rbFixed");

                HolidayCollection hc = (HolidayCollection)Session["Holidays"];

                if(hc[e.Item.ItemIndex] is FloatingHoliday)
                {
                    FloatingHoliday fl = (FloatingHoliday)hc[e.Item.ItemIndex];

                    cboOccurrence.SelectedIndex = (int)fl.Occurrence - 1;
                    cboDayOfWeek.SelectedIndex = (int)fl.Weekday;
                    ((TextBox)e.Item.FindControl("txtOffset")).Text =
                        ((fl.Offset < -999) ? -999 : (fl.Offset > 999) ? 999 : fl.Offset).ToString();

                    rbFloating.Checked = true;
                    rbFixed.Checked = false;
                    ((TextBox)e.Item.FindControl("txtDayOfMonth")).Enabled =
                        ((RequiredFieldValidator)e.Item.FindControl("rfvDOM")).Enabled =
                        ((RangeValidator)e.Item.FindControl("rvDOM")).Enabled =
                        ((CheckBox)e.Item.FindControl("chkAdjustDate")).Enabled = false;
                    ((TextBox)e.Item.FindControl("txtDayOfMonth")).CssClass = "Disabled";
                }
                else
                {
                    FixedHoliday fx = (FixedHoliday)hc[e.Item.ItemIndex];

                    ((TextBox)e.Item.FindControl("txtOffset")).Text = "0";
                    ((TextBox)e.Item.FindControl("txtDayOfMonth")).Text = fx.Day.ToString();
                    ((CheckBox)e.Item.FindControl("chkAdjustDate")).Checked = fx.AdjustFixedDate;

                    rbFloating.Checked = false;
                    rbFixed.Checked = true;
                    cboOccurrence.Enabled = cboDayOfWeek.Enabled =
                        ((TextBox)e.Item.FindControl("txtOffset")).Enabled =
                        ((RequiredFieldValidator)e.Item.FindControl("rfvOffset")).Enabled =
                        ((RangeValidator)e.Item.FindControl("rvOffset")).Enabled = false;
                    cboOccurrence.CssClass = cboDayOfWeek.CssClass =
                        ((TextBox)e.Item.FindControl("txtOffset")).CssClass = "Disabled";
                }

                cboMonth.SelectedIndex = hc[e.Item.ItemIndex].Month - 1;
                ((TextBox)e.Item.FindControl("txtDescription")).Text = hc[e.Item.ItemIndex].Description;
            }
        }

        /// <summary>
        /// Enable or disable the controls when the radio buttons are clicked
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void Type_CheckChanged(object sender, EventArgs e)
        {
            DataGridItem dgi = dgHolidays.Items[dgHolidays.EditItemIndex];

            RadioButton rbFloating = (RadioButton)dgi.FindControl("rbFloating");
            RadioButton rbFixed = (RadioButton)dgi.FindControl("rbFixed");

            bool enabled = !((sender as RadioButton) == rbFixed);

            rbFloating.Checked = enabled;
            rbFixed.Checked = !enabled;

            string floatClass = (enabled) ? String.Empty : "Disabled",
                   fixedClass = (enabled) ? "Disabled" : String.Empty;

            ((DropDownList)dgi.FindControl("cboOccurrence")).Enabled =
                ((DropDownList)dgi.FindControl("cboDayOfWeek")).Enabled =
                ((TextBox)dgi.FindControl("txtOffset")).Enabled =
                ((RequiredFieldValidator)dgi.FindControl("rfvOffset")).Enabled =
                ((RangeValidator)dgi.FindControl("rvOffset")).Enabled = enabled;
            ((TextBox)dgi.FindControl("txtDayOfMonth")).Enabled =
                ((RequiredFieldValidator)dgi.FindControl("rfvDOM")).Enabled =
                ((RangeValidator)dgi.FindControl("rvDOM")).Enabled =
                ((CheckBox)dgi.FindControl("chkAdjustDate")).Enabled = !enabled;

            ((DropDownList)dgi.FindControl("cboOccurrence")).CssClass =
                ((DropDownList)dgi.FindControl("cboDayOfWeek")).CssClass =
                ((TextBox)dgi.FindControl("txtOffset")).CssClass = floatClass;
            ((TextBox)dgi.FindControl("txtDayOfMonth")).CssClass = fixedClass;
        }

        /// <summary>
        /// Find holidays defined by the holiday collection in the given range of years
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void btnFindHolidays_Click(object sender, EventArgs e)
        {
            HolidayCollection hc = (HolidayCollection)Session["Holidays"];
            int yearFrom, yearTo, tempYear;

            if(!Page.IsValid)
                return;

            yearFrom = Convert.ToInt32(txtFromYear.Text);
            yearTo = Convert.ToInt32(txtToYear.Text);

            if(yearFrom > yearTo)
            {
                tempYear = yearFrom;
                yearFrom = yearTo;
                yearTo = tempYear;
            }

            // Limit the range to 50 years in the web app to prevent using too many resources on the server
            if(yearTo > yearFrom + 49)
                yearTo = yearFrom + 49;

            txtFromYear.Text = yearFrom.ToString();
            txtToYear.Text = yearTo.ToString();

            lbDates.Items.Clear();
            var dcDates = hc.HolidaysBetween(yearFrom, yearTo).ToList();

            if(dcDates.Count > 0)
                foreach(DateTime dt in dcDates)
                    lbDates.Items.Add(String.Format("{0:d} - {1}", dt, hc.HolidayDescription(dt)));
        }

        /// <summary>
        /// Find all instances of Easter using the selected method in the given range of years
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void btnFindEaster_Click(object sender, EventArgs e)
        {
            EasterMethod em;
            int yearFrom, yearTo, tempYear;
            string desc;

            if(!Page.IsValid)
                return;

            em = (EasterMethod)rblEasterMethod.SelectedIndex;
            yearFrom = Convert.ToInt32(txtFromYear.Text);
            yearTo = Convert.ToInt32(txtToYear.Text);

            // Adjust years as necessary based on the method
            if(em != EasterMethod.Julian)
            {
                if(yearFrom < 1583)
                    yearFrom = 1583;

                if(yearFrom > 4099)
                    yearFrom = 4099;

                if(yearTo < 1583)
                    yearTo = 1583;

                if(yearTo > 4099)
                    yearTo = 4099;
            }
            else
            {
                if(yearFrom < 326)
                    yearFrom = 326;

                if(yearTo < 326)
                    yearTo = 326;
            }

            if(yearFrom > yearTo)
            {
                tempYear = yearFrom;
                yearFrom = yearTo;
                yearTo = tempYear;
            }

            // Limit the range to 50 years in the web app to prevent using too many resources on the server
            if(yearTo > yearFrom + 49)
                yearTo = yearFrom + 49;

            txtFromYear.Text = yearFrom.ToString();
            txtToYear.Text = yearTo.ToString();

            desc = String.Format("Easter ({0})", em.ToString());
            lbDates.Items.Clear();

            while(yearFrom <= yearTo)
                lbDates.Items.Add(String.Format("{0:d} - {1}", DateUtils.EasterSunday(yearFrom++, em), desc));
        }

        /// <summary>
        /// Test to see if the entered date is a holiday based on the set defined in the collection
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="e">The event arguments</param>
        protected void btnIsHoliday_Click(object sender, EventArgs e)
        {
            if(Page.IsValid)
            {
                HolidayCollection hc = (HolidayCollection)Session["Holidays"];

                if(hc.IsHoliday(Convert.ToDateTime(txtTestDate.Text, CultureInfo.CurrentCulture)))
                    lblIsHoliday.Text = "The test date is a defined holiday";
                else
                    lblIsHoliday.Text = "The test date is not a defined holiday";
            }
        }
	}
}
