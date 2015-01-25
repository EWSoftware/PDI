//===============================================================================================================
// System  : EWSoftware.PDI ASP.NET Web Server Controls
// File    : RecurrencePattern.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/30/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains one of several user controls that are combined to allow the editing of various recurrence
// parameters.  This one is used to contain all the other user controls and sets the overall pattern to use for
// the recurrence.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//   Date      Who  Comments
// ==============================================================================================================
// 02/13/2005  EFW  Created the code
// 02/19/2007  EFW  Rewrote the control to use a DHTML implementation that gets rid of all unnecessary post
//                  backs.  Also moving to .NET 2.0.
// 12/30/2014  EFW  Simplified the HTML and styling to allow for the use of alternate CSS frameworks such as
//                  Bootstrap.
//===============================================================================================================

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using EWSoftware.PDI.Web.Design;

namespace EWSoftware.PDI.Web.Controls
{
	/// <summary>
    /// This user control is used to contain all the other recurrence pattern user controls and sets the overall
    /// pattern to use for the recurrence.
	/// </summary>
    /// <remarks>The control renders a default style sheet for itself.  The default styles can be overridden by
    /// specifying one or more CSS class names in each of the <c>Css*</c> properties.  Any property not set will
    /// cause the default style to be use for the associated elements.</remarks>
	[DefaultProperty("ShowCanOccurOnHoliday"), ToolboxData("<{0}:RecurrencePattern runat=server />"),
      Designer(typeof(RecurrencePatternDesigner)), ParseChildren(true)]
	public class RecurrencePattern : System.Web.UI.Control, INamingContainer, IPostBackDataHandler
	{
        #region Private data members
        //=====================================================================

        // The paths to the HTML and script resources
        internal const string RecurrencePatternHtml = "EWSoftware.PDI.Web.Controls.HTML.";
        internal const string RecurrencePatternScripts = "EWSoftware.PDI.Web.Controls.Scripts.";

        // The current recurrence pattern
        private Recurrence rRecur;

        // Set focus flag
        private bool setFocusToPattern;

        // The validator
        private CustomValidator validator;
        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This property is used to get or set the CSS class name(s) to use for the containing panel
        /// </summary>
        /// <remarks>If not set, a default style is used and included inline within the page</remarks>
		[Category("Appearance"), DefaultValue(null), Bindable(true),
         Description("The CSS class name(s) to use for the containing panel")]
        public string CssPanel
        {
            get { return (string)this.ViewState["CssPanel"]; }
            set { this.ViewState["CssPanel"] = value; }
        }

        /// <summary>
        /// This property is used to get or set the CSS class name(s) to use for the panel heading
        /// </summary>
        /// <remarks>If not set, a default style is used and included inline within the page</remarks>
        [Category("Appearance"), DefaultValue(null), Bindable(true),
         Description("The CSS class name(s) to use for the panel heading")]
        public string CssPanelHeading
        {
            get { return (string)this.ViewState["CssPanelHeading"]; }
            set { this.ViewState["CssPanelHeading"] = value; }
        }

        /// <summary>
        /// This property is used to get or set the CSS class name(s) to use for the panel title
        /// </summary>
        /// <remarks>If not set, a default style is used and included inline within the page</remarks>
        [Category("Appearance"), DefaultValue(null), Bindable(true),
         Description("The CSS class name(s) to use for the panel title")]
        public string CssPanelTitle
        {
            get { return (string)this.ViewState["CssPanelTitle"]; }
            set { this.ViewState["CssPanelTitle"] = value; }
        }

        /// <summary>
        /// This property is used to get or set the CSS class name(s) to use for the panel body
        /// </summary>
        /// <remarks>If not set, a default style is used and included inline within the page</remarks>
        [Category("Appearance"), DefaultValue(null), Bindable(true),
         Description("The CSS class name(s) to use for the panel body")]
        public string CssPanelBody
        {
            get { return (string)this.ViewState["CssPanelBody"]; }
            set { this.ViewState["CssPanelBody"] = value; }
        }

        /// <summary>
        /// This property is used to get or set the CSS class name(s) to use for the input (textbox) controls
        /// </summary>
        /// <remarks>If not set, a default style is used and included inline within the page</remarks>
        [Category("Appearance"), DefaultValue(null), Bindable(true),
         Description("The CSS class name(s) to use for the input (textbox) controls")]
        public string CssInput
        {
            get { return (string)this.ViewState["CssInput"]; }
            set { this.ViewState["CssInput"] = value; }
        }

        /// <summary>
        /// This property is used to get or set the CSS class name(s) to use for the select (combo box) controls
        /// </summary>
        /// <remarks>If not set, a default style is used and included inline within the page</remarks>
        [Category("Appearance"), DefaultValue(null), Bindable(true),
         Description("The CSS class name(s) to use for the select (combo box) controls")]
        public string CssSelect
        {
            get { return (string)this.ViewState["CssSelect"]; }
            set { this.ViewState["CssSelect"] = value; }
        }

        /// <summary>
        /// This property is used to get or set the CSS class name(s) to use for error messages
        /// </summary>
        /// <remarks>If not set, a default style is used and included inline within the page</remarks>
        [Category("Appearance"), DefaultValue(null), Bindable(true),
         Description("The CSS class name(s) to use for error messages")]
        public string CssErrorMessage
        {
            get { return (string)this.ViewState["CssErrorMessage"]; }
            set { this.ViewState["CssErrorMessage"] = value; }
        }

        /// <summary>
        /// This property is used to get or set the CSS class name(s) to use for the button controls
        /// </summary>
        /// <remarks>If not set, a default style is used and included inline within the page</remarks>
        [Category("Appearance"), DefaultValue(null), Bindable(true),
         Description("The CSS class name(s) to use for the button controls")]
        public string CssButton
        {
            get { return (string)this.ViewState["CssButton"]; }
            set { this.ViewState["CssButton"] = value; }
        }

        /// <summary>
        /// This property is used to get or set whether or not the Week Start Day option is displayed.  It
        /// is visible by default.
        /// </summary>
        /// <remarks>If hidden, the week start day combo box will not be visible and a default week start day
        /// will be used in all recurrences edited by the control.</remarks>
        [Category("Behavior"), DefaultValue(true), Bindable(true),
         Description("Show or hide the Week Start Day combo box")]
        public bool ShowWeekStartDay
        {
            get
            {
                object oShowWeekStartDay = this.ViewState["ShowWSD"];
                return (oShowWeekStartDay == null) ? true : (bool)oShowWeekStartDay;
            }
            set { this.ViewState["ShowWSD"] = value; }
        }

        /// <summary>
        /// This property is used to get or set whether or not the Can Occur On Holiday option is displayed.  It
        /// is visible by default.
        /// </summary>
        /// <remarks>If hidden, the check box will not be visible and the option will be set to true in all
        /// recurrences edited by the control.</remarks>
		[Category("Behavior"), DefaultValue(true), Bindable(true),
         Description("Show or hide the Can Occur on Holiday checkbox")]
        public bool ShowCanOccurOnHoliday
        {
            get
            {
                object oShowHoliday = this.ViewState["ShowHol"];
                return (oShowHoliday == null) ? true : (bool)oShowHoliday;
            }
            set { this.ViewState["ShowHol"] = value; }
        }

        /// <summary>
        /// This property is used to get or set whether or not the Advanced checkbox is visible and thus give
        /// access to the advanced pattern options.  It is visible by default.
        /// </summary>
        /// <remarks>If hidden, only basic patterns similar to those in Microsoft Outlook can be created.  When
        /// edited, all advanced options currently in effect will be lost and the pattern will be made to conform
        /// to the simple options available for the currently selected frequency.</remarks>
		[Category("Behavior"), DefaultValue(true), Bindable(true),
         Description("Show or hide the Advanced checkbox")]
        public bool ShowAdvanced
        {
            get
            {
                object oShowAdvanced = this.ViewState["ShowAdv"];
                return (oShowAdvanced == null) ? true : (bool)oShowAdvanced;
            }
            set { this.ViewState["ShowAdv"] = value; }
        }

        /// <summary>
        /// This property is used to get or set the maximum pattern option to display.  All pattern options will
        /// be visible by default.
        /// </summary>
        /// <remarks>If set to <c>Daily</c>, only basic patterns similar to those in Microsoft Outlook can be
        /// created (yearly, monthly, weekly, and daily).  If set to a recurrence with a pattern higher than the
        /// maximum, all options currently in effect will be lost and the pattern will be made to conform to the
        /// simple pattern for the maximum allowed pattern.</remarks>
		[Category("Behavior"), DefaultValue(RecurFrequency.Secondly),
          Bindable(true), Description("This determines the maximum editable recurrence pattern type")]
        public RecurFrequency MaximumPattern
        {
            get
            {
                object oMaxPattern = this.ViewState["MaxPattern"];
                return (oMaxPattern == null) ? RecurFrequency.Secondly : (RecurFrequency)oMaxPattern;
            }
            set
            {
                if(value == RecurFrequency.Undefined)
                    value = RecurFrequency.Secondly;

                this.ViewState["MaxPattern"] = value;
            }
        }

        /// <summary>
        /// This is overridden to ensure that the child controls are always created when needed
        /// </summary>
        [Browsable(false)]
        public override ControlCollection Controls
        {
            get
            {
                EnsureChildControls();
                return base.Controls;
            }
        }
        #endregion

        #region Private class methods
        //=====================================================================

        /// <summary>
        /// This is used to determine whether or not the standard ASP.NET client-side validators can be used
        /// </summary>
        /// <returns></returns>
        private bool DetermineRenderUplevel
        {
            get
            {
                if(this.DesignMode)
                    return true;

                Page page = this.Page;

                if(page != null && page.Request != null &&
                  page.Request.Browser.W3CDomVersion.Major >= 1)
                    return (page.Request.Browser.EcmaScriptVersion.CompareTo(new Version(1, 2)) >= 0);

                return false;
            }
        }

        /// <summary>
        /// Generate the script to set the daily pattern options
        /// </summary>
        /// <param name="sb">The string builder to which the script is appended</param>
        private void GenerateDailyPatternScript(StringBuilder sb)
        {
            string option = "false";
            int idx = 0;

            // Every weekday?
            if(rRecur.ByDay.Count == 5 && rRecur.Interval == 1)
            {
                foreach(DayInstance di in rRecur.ByDay)
                {
                    if(di.Instance != 0 || di.DayOfWeek == DayOfWeek.Saturday || di.DayOfWeek == DayOfWeek.Sunday)
                        break;

                    idx++;
                }

                if(idx == 5)
                    option = "true";
            }

            sb.AppendFormat("RP_{0}.SetDailyInterval({1}, {2});\r\n", this.ID, rRecur.Interval, option);
        }

        /// <summary>
        /// Generate the script to set the weekly pattern options
        /// </summary>
        /// <param name="sb">The string builder to which the script is appended</param>
        private void GenerateWeeklyPatternScript(StringBuilder sb)
        {
            sb.AppendFormat("var RP_{0}_dayList = new Array(", this.ID);

            // Add unique day of week instances
            for(int idx = 0; idx < rRecur.ByDay.Count; idx++)
            {
                if(idx != 0)
                    sb.Append(',');

                sb.AppendFormat("new EWSRP_DayInstance(0, {0})", (int)rRecur.ByDay[idx].DayOfWeek);
            }

            sb.AppendFormat(");\r\nRP_{0}.SetWeeklyInterval({1}, RP_{0}_dayList);\r\n", this.ID, rRecur.Interval);
        }

        /// <summary>
        /// Generate the script to set the monthly pattern options
        /// </summary>
        /// <param name="sb">The string builder to which the script is appended</param>
        private void GenerateMonthlyPatternScript(StringBuilder sb)
        {
            int idx, dayOfMonth, occurrence;
            idx = dayOfMonth = occurrence = 0;

            sb.AppendFormat("var RP_{0}_dayList = new Array(", this.ID);

            foreach(DayInstance di in rRecur.ByDay)
            {
                if(idx != 0)
                    sb.Append(',');
                else
                    idx++;

                sb.AppendFormat("new EWSRP_DayInstance({0}, {1})", di.Instance, (int)di.DayOfWeek);
            }

            if(rRecur.ByDay.Count == 0)
            {
                if(rRecur.ByMonthDay.Count != 0)
                    dayOfMonth = rRecur.ByMonthDay[0];
                else
                    dayOfMonth = rRecur.StartDateTime.Day;
            }
            else
            {
                // If it's a single day, use ByDay.  If it's a combination, use ByDay with BySetPos.
                if(rRecur.ByDay.Count == 1)
                {
                    occurrence = (rRecur.ByDay[0].Instance < 1 || rRecur.ByDay[0].Instance > 4) ? 5 :
                        rRecur.ByDay[0].Instance;
                }
                else
                    if(rRecur.BySetPos.Count != 0)
                        occurrence = (rRecur.BySetPos[0] < 1 || rRecur.BySetPos[0] > 4) ? 5 : rRecur.BySetPos[0];
            }

            sb.AppendFormat(CultureInfo.InvariantCulture,
                ");\r\nRP_{0}.SetMonthlyInterval({1}, {2}, RP_{0}_dayList, {3});\r\n", this.ID, dayOfMonth,
                rRecur.Interval, occurrence);
        }

        /// <summary>
        /// Generate the script to set the yearly pattern options
        /// </summary>
        /// <param name="sb">The string builder to which the script is appended</param>
        private void GenerateYearlyPatternScript(StringBuilder sb)
        {
            int idx, month, dayOfMonth, occurrence;
            idx = dayOfMonth = occurrence = 0;

            sb.AppendFormat("var RP_{0}_dayList = new Array(", this.ID);

            foreach(DayInstance di in rRecur.ByDay)
            {
                if(idx != 0)
                    sb.Append(',');
                else
                    idx++;

                sb.AppendFormat("new EWSRP_DayInstance({0}, {1})", di.Instance, (int)di.DayOfWeek);
            }

            if(rRecur.ByMonth.Count != 0)
                month = rRecur.ByMonth[0];
            else
                month = rRecur.StartDateTime.Month;

            if(rRecur.ByDay.Count == 0)
            {
                if(rRecur.ByMonthDay.Count != 0)
                    dayOfMonth = rRecur.ByMonthDay[0];
                else
                    dayOfMonth = rRecur.StartDateTime.Day;
            }
            else
            {
                // If it's a single day, use ByDay.  If it's a combination, use ByDay with BySetPos.
                if(rRecur.ByDay.Count == 1)
                {
                    occurrence = (rRecur.ByDay[0].Instance < 1 || rRecur.ByDay[0].Instance > 4) ? 5 :
                        rRecur.ByDay[0].Instance;
                }
                else
                    if(rRecur.BySetPos.Count != 0)
                        occurrence = (rRecur.BySetPos[0] < 1 || rRecur.BySetPos[0] > 4) ? 5 : rRecur.BySetPos[0];
            }

            sb.AppendFormat(CultureInfo.InvariantCulture,
                ");\r\nRP_{0}.SetYearlyInterval({1}, {2}, {3}, RP_{0}_dayList, {4});\r\n", this.ID, month,
                dayOfMonth, rRecur.Interval, occurrence);
        }

        /// <summary>
        /// Generate the script to set the advanced pattern options
        /// </summary>
        /// <param name="sb">The string builder to which the script is appended</param>
        private void GenerateAdvancedPatternScript(StringBuilder sb)
        {
            int idx = 0;

            sb.AppendFormat("var RP_{0}_dayList = new Array(", this.ID);

            foreach(DayInstance di in rRecur.ByDay)
            {
                if(idx != 0)
                    sb.Append(',');
                else
                    idx++;

                sb.AppendFormat("new EWSRP_DayInstance({0}, {1})", di.Instance, (int)di.DayOfWeek);
            }

            sb.AppendFormat(");\r\nvar RP_{0}_months = [", this.ID);
            idx = 0;

            foreach(int month in rRecur.ByMonth)
            {
                if(idx != 0)
                    sb.Append(',');
                else
                    idx++;

                sb.Append(month);
            }

            sb.AppendFormat(CultureInfo.InvariantCulture, "];\r\nRP_{0}.SetAdvancedInterval({1}, {2}, " +
                "RP_{0}_dayList, RP_{0}_months, '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}');\r\n", this.ID,
                (int)rRecur.Frequency, rRecur.Interval, rRecur.ByWeekNo.ToString(), rRecur.ByYearDay.ToString(),
                rRecur.ByMonthDay.ToString(), rRecur.ByHour.ToString(), rRecur.ByMinute.ToString(),
                rRecur.BySecond.ToString(), rRecur.BySetPos.ToString());
        }

        /// <summary>
        /// This is used to parse the advanced options on postback
        /// </summary>
        /// <param name="options">The options from the client</param>
        private void ParseAdvancedOptions(string[] options)
        {
            int idx, instance;
            string dayPart;
            string[] dayInstances, days = { "SU", "MO", "TU", "WE", "TH", "FR", "SA" };

            rRecur.Frequency = (RecurFrequency)Convert.ToInt32(options[1], CultureInfo.InvariantCulture);
            rRecur.Interval = Convert.ToInt32(options[2], CultureInfo.InvariantCulture);

            for(idx = 0; idx < options[3].Length; idx++)
                if(options[3][idx] == '1')
                    rRecur.ByMonth.Add(idx + 1);

            if(options[4].Length != 0)
            {
                dayInstances = options[4].Split(',');

                foreach(string day in dayInstances)
                {
                    if(day.Length < 3)
                        instance = 0;
                    else
                        instance = Convert.ToInt32(day.Substring(0, day.Length - 2), CultureInfo.InvariantCulture);

                    dayPart = day.Substring(day.Length - 2);

                    for(idx = 0; idx < 7; idx++)
                        if(days[idx] == dayPart)
                            break;

                    rRecur.ByDay.Add(new DayInstance(instance, (DayOfWeek)idx));
                }
            }

            if(rRecur.Frequency == RecurFrequency.Yearly)
                rRecur.ByWeekNo.ParseValues(options[5]);

            rRecur.ByYearDay.ParseValues(options[6]);
            rRecur.ByMonthDay.ParseValues(options[7]);
            rRecur.ByHour.ParseValues(options[8]);
            rRecur.ByMinute.ParseValues(options[9]);
            rRecur.BySecond.ParseValues(options[10]);
            rRecur.BySetPos.ParseValues(options[11]);
        }

        /// <summary>
        /// This is called by the <see cref="EWSoftware.PDI.Web.Design.RecurrencePatternDesigner"/> control
        /// designer class when rendering at design time.
        /// </summary>
        /// <returns>The HTML to render at design time</returns>
        internal string RenderAtDesignTime()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            StringBuilder sb = new StringBuilder(4096);

            using(var sr = new StreamReader(asm.GetManifestResourceStream(RecurrencePatternHtml +
              "RecurrencePattern.html")))
            {
                sb.Append(sr.ReadToEnd());
            }

            using(var sr = new StreamReader(asm.GetManifestResourceStream(RecurrencePatternHtml +
              "DailyPattern.html")))
            {
                sb.Replace("<@Daily@>", sr.ReadToEnd().Replace("none;", "block;"));
            }

            // For design-time, we'll only show the daily pattern
            sb.Replace("<@Secondly@>", String.Empty);
            sb.Replace("<@Minutely@>", String.Empty);
            sb.Replace("<@Hourly@>", String.Empty);
            sb.Replace("<@Weekly@>", String.Empty);
            sb.Replace("<@Monthly@>", String.Empty);
            sb.Replace("<@Yearly@>", String.Empty);
            sb.Replace("<@Advanced@>", String.Empty);

            // Set the ID for all contained HTML controls and method calls
            sb.Replace("@_", this.ID + "_");
            sb.Replace("@.", "RP_" + this.ID + ".");

            // Replace styles as needed
            if(!String.IsNullOrWhiteSpace(this.CssErrorMessage))
                sb.Replace("EWS_RP_ErrorMessage", this.CssErrorMessage);

            if(!String.IsNullOrWhiteSpace(this.CssInput))
                sb.Replace("EWS_RP_Input", this.CssInput);

            if(!String.IsNullOrWhiteSpace(this.CssPanelBody))
                sb.Replace("EWS_RP_PanelBody", this.CssPanelBody);

            if(!String.IsNullOrWhiteSpace(this.CssPanelHeading))
                sb.Replace("EWS_RP_PanelHeading", this.CssPanelHeading);

            if(!String.IsNullOrWhiteSpace(this.CssPanelTitle))
                sb.Replace("EWS_RP_PanelTitle", this.CssPanelTitle);

            // Do this after the others or it matches them too
            if(!String.IsNullOrWhiteSpace(this.CssPanel))
                sb.Replace("EWS_RP_Panel", this.CssPanel);

            // Add the default style sheet if needed
            if(String.IsNullOrWhiteSpace(this.CssButton) || String.IsNullOrWhiteSpace(this.CssErrorMessage) ||
              String.IsNullOrWhiteSpace(this.CssInput) || String.IsNullOrWhiteSpace(this.CssPanel) ||
              String.IsNullOrWhiteSpace(this.CssPanelBody) || String.IsNullOrWhiteSpace(this.CssPanelHeading) ||
              String.IsNullOrWhiteSpace(this.CssPanelTitle) || String.IsNullOrWhiteSpace(this.CssSelect))
            {
                sb.AppendFormat(CultureInfo.InvariantCulture, "<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\">",
                    this.Page.ClientScript.GetWebResourceUrl(typeof(RecurrencePattern),
                        RecurrencePattern.RecurrencePatternHtml + "RecurrenceStyle.css"));
            }

            return sb.ToString();
        }
        #endregion

        #region IPostBackDataHandler implementation
        //=====================================================================

        /// <summary>
        /// This is used to load the recurrence data for the control on post back
        /// </summary>
        /// <param name="postDataKey">The key identifier for the control</param>
        /// <param name="postCollection">The collection of values</param>
        /// <returns>True if the server control's state changes as a result of the postback; otherwise, false</returns>
        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            string[] options;
            string pattern = null;
            DaysOfWeek daysOfWeek = DaysOfWeek.None;
            int val;

            if(postCollection != null)
                pattern = postCollection[this.ID + "_Value"];

            if(!String.IsNullOrEmpty(pattern))
            {
                // Parse and load the recurrence
                rRecur = new Recurrence();
                options = pattern.Split('\xFF');

                // Parse the pattern options
                if(options[0] == "1")
                    this.ParseAdvancedOptions(options);
                else
                {
                    switch(options[1])
                    {
                        case "1":   // Yearly
                            if(options[2] == "0")
                            {
                                rRecur.RecurYearly(
                                    Convert.ToInt32(options[3], CultureInfo.InvariantCulture) + 1,
                                    Convert.ToInt32(options[4], CultureInfo.InvariantCulture),
                                    Convert.ToInt32(options[5], CultureInfo.InvariantCulture));
                            }
                            else
                            {
                                val = Convert.ToInt32(options[4], CultureInfo.InvariantCulture);

                                if(val < 7)
                                    daysOfWeek = DateUtils.ToDaysOfWeek((DayOfWeek)val);
                                else
                                    if(val == 7)
                                        daysOfWeek = DaysOfWeek.Weekdays;
                                    else
                                        if(val == 8)
                                            daysOfWeek = DaysOfWeek.Weekends;
                                        else
                                            daysOfWeek = DaysOfWeek.EveryDay;

                                val = Convert.ToInt32(options[3], CultureInfo.InvariantCulture) + 1;

                                rRecur.RecurYearly((DayOccurrence)val, daysOfWeek,
                                    Convert.ToInt32(options[5], CultureInfo.InvariantCulture) + 1,
                                    Convert.ToInt32(options[6], CultureInfo.InvariantCulture));
                            }
                            break;

                        case "2":   // Monthly
                            if(options[2] == "0")
                            {
                                rRecur.RecurMonthly(
                                    Convert.ToInt32(options[3], CultureInfo.InvariantCulture),
                                    Convert.ToInt32(options[4], CultureInfo.InvariantCulture));
                            }
                            else
                            {
                                val = Convert.ToInt32(options[4], CultureInfo.InvariantCulture);

                                if(val < 7)
                                    daysOfWeek = DateUtils.ToDaysOfWeek((DayOfWeek)val);
                                else
                                    if(val == 7)
                                        daysOfWeek = DaysOfWeek.Weekdays;
                                    else
                                        if(val == 8)
                                            daysOfWeek = DaysOfWeek.Weekends;
                                        else
                                            daysOfWeek = DaysOfWeek.EveryDay;

                                val = Convert.ToInt32(options[3], CultureInfo.InvariantCulture) + 1;

                                rRecur.RecurMonthly((DayOccurrence)val, daysOfWeek, Convert.ToInt32(options[5],
                                    CultureInfo.InvariantCulture));
                            }
                            break;

                        case "3":   // Weekly
                            if(options[3][0] == '1')
                                daysOfWeek |= DaysOfWeek.Sunday;

                            if(options[3][1] == '1')
                                daysOfWeek |= DaysOfWeek.Monday;

                            if(options[3][2] == '1')
                                daysOfWeek |= DaysOfWeek.Tuesday;

                            if(options[3][3] == '1')
                                daysOfWeek |= DaysOfWeek.Wednesday;

                            if(options[3][4] == '1')
                                daysOfWeek |= DaysOfWeek.Thursday;

                            if(options[3][5] == '1')
                                daysOfWeek |= DaysOfWeek.Friday;

                            if(options[3][6] == '1')
                                daysOfWeek |= DaysOfWeek.Saturday;

                            rRecur.RecurWeekly(Convert.ToInt32(options[2], CultureInfo.InvariantCulture), daysOfWeek);
                            break;

                        case "4":   // Daily
                            if(options[2] == "1")
                            {
                                // Every week day
                                rRecur.Frequency = RecurFrequency.Daily;
                                rRecur.Interval = 1;
                                rRecur.ByDay.AddRange(new [] { DayOfWeek.Monday, DayOfWeek.Tuesday,
                                    DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday });
                            }
                            else
                                rRecur.RecurDaily(Convert.ToInt32(options[3], CultureInfo.InvariantCulture));
                            break;

                        default:    // Hourly, minutely, secondly
                            rRecur.Frequency = (RecurFrequency)Convert.ToInt32(options[1], CultureInfo.InvariantCulture);
                            rRecur.Interval = Convert.ToInt32(options[2], CultureInfo.InvariantCulture);
                            break;
                    }
                }

                // Parse the range options
                rRecur.WeekStart = (DayOfWeek)Convert.ToInt32(options[options.Length - 4], CultureInfo.InvariantCulture);
                rRecur.CanOccurOnHoliday = (options[options.Length - 3] == "1");

                switch(options[options.Length - 2])
                {
                    case "1":   // End after N occurrences
                        rRecur.MaximumOccurrences = Convert.ToInt32(options[options.Length - 1],
                            CultureInfo.InvariantCulture);
                        break;

                    case "2":   // End after a specific date
                        rRecur.RecurUntil = Convert.ToDateTime(options[options.Length - 1],
                            CultureInfo.InvariantCulture);
                        break;

                    default:    // Never ends
                        break;
                }
            }

            return false;
        }

        /// <summary>
        /// This is not currently used by the control
        /// </summary>
        public void RaisePostDataChangedEvent()
        {
        }
        #endregion

        #region Methods
        //=====================================================================

	    /// <summary>
        /// This method is overridden to generate the controls
	    /// </summary>
        protected override void CreateChildControls()
        {
            if(this.DesignMode)
                return;

            Assembly asm = Assembly.GetExecutingAssembly();
            StringBuilder sb = new StringBuilder(4096);

            if(rRecur == null)
            {
                rRecur = new Recurrence();
                rRecur.StartDateTime = DateTime.Today;
                rRecur.RecurDaily(1);
            }

            // For this control, JavaScript support is required
            if(this.Page.Request.Browser.EcmaScriptVersion.Major < 1)
            {
                this.Controls.Add(new LiteralControl(String.Format(CultureInfo.InvariantCulture,
                    "<div class='{0}'>The <strong>EWSoftware.PDI.Web.Controls.RecurrencePattern</strong> " +
                    "control requires that JavaScript be enabled in order to work correctly.</div>",
                    String.IsNullOrWhiteSpace(this.CssPanel) ? "EWS_RP_Panel" : this.CssPanel)));
                return;
            }

            using(var sr = new StreamReader(asm.GetManifestResourceStream(RecurrencePatternHtml +
              "RecurrencePattern.html")))
            {
                sb.Append(sr.ReadToEnd().Trim());
            }

            using(var sr = new StreamReader(asm.GetManifestResourceStream(RecurrencePatternHtml +
              "SecondlyPattern.html")))
            {
                sb.Replace("<@Secondly@>", sr.ReadToEnd().Trim());
            }

            using(var sr = new StreamReader(asm.GetManifestResourceStream(RecurrencePatternHtml +
              "MinutelyPattern.html")))
            {
                sb.Replace("<@Minutely@>", sr.ReadToEnd().Trim());
            }

            using(var sr = new StreamReader(asm.GetManifestResourceStream(RecurrencePatternHtml +
              "HourlyPattern.html")))
            {
                sb.Replace("<@Hourly@>", sr.ReadToEnd().Trim());
            }

            using(var sr = new StreamReader(asm.GetManifestResourceStream(RecurrencePatternHtml +
              "DailyPattern.html")))
            {
                sb.Replace("<@Daily@>", sr.ReadToEnd().Trim());
            }

            using(var sr = new StreamReader(asm.GetManifestResourceStream(RecurrencePatternHtml +
              "WeeklyPattern.html")))
            {
                sb.Replace("<@Weekly@>", sr.ReadToEnd().Trim());
            }

            using(var sr = new StreamReader(asm.GetManifestResourceStream(RecurrencePatternHtml +
              "MonthlyPattern.html")))
            {
                sb.Replace("<@Monthly@>", sr.ReadToEnd().Trim());
            }

            using(var sr = new StreamReader(asm.GetManifestResourceStream(RecurrencePatternHtml +
              "YearlyPattern.html")))
            {
                sb.Replace("<@Yearly@>", sr.ReadToEnd().Trim());
            }

            using(var sr = new StreamReader(asm.GetManifestResourceStream(RecurrencePatternHtml +
              "AdvancedPattern.html")))
            {
                sb.Replace("<@Advanced@>", sr.ReadToEnd().Trim());
            }

            // Set the ID for all contained HTML controls and method calls
            sb.Replace("@_", this.ID + "_");
            sb.Replace("@.", "RP_" + this.ID + ".");

            // Replace styles as needed
            if(!String.IsNullOrWhiteSpace(this.CssButton))
                sb.Replace("EWS_RP_Button", this.CssButton);

            if(!String.IsNullOrWhiteSpace(this.CssErrorMessage))
                sb.Replace("EWS_RP_ErrorMessage", this.CssErrorMessage);

            if(!String.IsNullOrWhiteSpace(this.CssInput))
                sb.Replace("EWS_RP_Input", this.CssInput);

            if(!String.IsNullOrWhiteSpace(this.CssPanelBody))
                sb.Replace("EWS_RP_PanelBody", this.CssPanelBody);

            if(!String.IsNullOrWhiteSpace(this.CssPanelHeading))
                sb.Replace("EWS_RP_PanelHeading", this.CssPanelHeading);

            if(!String.IsNullOrWhiteSpace(this.CssPanelTitle))
                sb.Replace("EWS_RP_PanelTitle", this.CssPanelTitle);

            // Do this after the others or it matches them too
            if(!String.IsNullOrWhiteSpace(this.CssPanel))
                sb.Replace("EWS_RP_Panel", this.CssPanel);

            if(!String.IsNullOrWhiteSpace(this.CssSelect))
                sb.Replace("EWS_RP_Select", this.CssSelect);

            // Add the default style sheet if needed.  Try to put it in the header. If not, just add it as a
            // child of this control.
            if(String.IsNullOrWhiteSpace(this.CssButton) || String.IsNullOrWhiteSpace(this.CssErrorMessage) ||
              String.IsNullOrWhiteSpace(this.CssInput) || String.IsNullOrWhiteSpace(this.CssPanel) ||
              String.IsNullOrWhiteSpace(this.CssPanelBody) || String.IsNullOrWhiteSpace(this.CssPanelHeading) ||
              String.IsNullOrWhiteSpace(this.CssPanelTitle) || String.IsNullOrWhiteSpace(this.CssSelect))
            {
                LiteralControl cssLink = new LiteralControl(String.Format(CultureInfo.InvariantCulture,
                    "<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\">",
                    this.Page.ClientScript.GetWebResourceUrl(typeof(RecurrencePattern),
                        RecurrencePattern.RecurrencePatternHtml + "RecurrenceStyle.css")));

                cssLink.ID = "EWS_RP_Stylesheet";

                if(this.Page != null && this.Page.Header != null && this.Page.Header.FindControl(cssLink.ID) == null)
                    this.Page.Header.Controls.Add(cssLink);
                else
                    this.Controls.Add(cssLink);
            }

            this.Controls.Add(new LiteralControl(sb.ToString()));

            // Add the validator if supported.  If not, use an OnSubmit function with a custom
            // Page_ClientValidate function for __doPostBack (added by OnPreRender).
            if(this.DetermineRenderUplevel)
            {
                validator = new CustomValidator();
                validator.ID = this.ID + "_Validator";
                validator.ClientValidationFunction = this.ID + "_Validate";
                validator.Display = ValidatorDisplay.None;
                validator.ErrorMessage = "The recurrence pattern is not valid";
                this.Controls.Add(validator);
            }
            else
                this.Page.ClientScript.RegisterOnSubmitStatement(typeof(RecurrencePattern), this.ID + "_Submit",
                    "return RP_" + this.ID + ".ValidateRecurrence();");

            this.Page.RegisterRequiresPostBack(this);
        }

        /// <summary>
        /// This is overridden to render the style sheet for the control if necessary.  It also renders some
        /// supporting JavaScript code that initializes the control when the page loads.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnPreRender(EventArgs e)
        {
            StringBuilder sb = new StringBuilder(1024);
            string patternId;
            int range;

            // There's nothing to do if script isn't supported
            if(!this.DesignMode && this.Page.Request.Browser.EcmaScriptVersion.Major < 1)
                return;

            // Add start-up code to initialize the pattern
            sb.AppendFormat(CultureInfo.InvariantCulture, "<script type='text/javascript'>\r\n<!--\r\n" +
                "var RP_{0} = new EWSRP_RecurrencePattern(\"{0}\");\r\n" +
                "RP_{0}.ShowWeekStartDay({1});\r\n" +
                "RP_{0}.ShowCanOccurOnHoliday({2});\r\n" +
                "RP_{0}.ShowAdvanced({3});\r\n" +
                "RP_{0}.ShowPatternOptions({4});\r\n", this.ID,
                this.ShowWeekStartDay.ToString().ToLowerInvariant(),
                this.ShowCanOccurOnHoliday.ToString().ToLowerInvariant(),
                this.ShowAdvanced.ToString().ToLowerInvariant(), (int)this.MaximumPattern);

            // Add script to set the current pattern options
            if(this.ShowAdvanced && rRecur.IsAdvancedPattern)
                this.GenerateAdvancedPatternScript(sb);
            else
                switch(rRecur.Frequency)
                {
                    case RecurFrequency.Yearly:
                        this.GenerateYearlyPatternScript(sb);
                        break;

                    case RecurFrequency.Monthly:
                        this.GenerateMonthlyPatternScript(sb);
                        break;

                    case RecurFrequency.Weekly:
                        this.GenerateWeeklyPatternScript(sb);
                        break;

                    case RecurFrequency.Daily:
                        this.GenerateDailyPatternScript(sb);
                        break;

                    default:    // Hourly, minutely, secondly
                        sb.AppendFormat("RP_{0}.SetInterval({1}, {2});\r\n", this.ID, (int)rRecur.Frequency,
                            rRecur.Interval);
                        break;
                }

            // Set the range of recurrence
            if(rRecur.MaximumOccurrences != 0)
                range = 1;
            else
                if(rRecur.RecurUntil == DateTime.MaxValue)
                    range = 0;
                else
                    range = 2;

            sb.AppendFormat(CultureInfo.InvariantCulture,
                "RP_{0}.SetRangeOfRecurrence({1}, {2}, {3}, {4}, \"{5}\");\r\n", this.ID,
                (int)rRecur.WeekStart, (rRecur.CanOccurOnHoliday) ? "true" : "false", range,
                (range == 1) ? rRecur.MaximumOccurrences : 0,
                (range == 2) ? rRecur.RecurUntil.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern,
                    CultureInfo.CurrentCulture) : String.Empty);

            // Set the focus to the selected pattern radio button?
            if(setFocusToPattern)
            {
                switch(rRecur.Frequency)
                {
                    case RecurFrequency.Yearly:
                        patternId = "YEAR";
                        break;

                    case RecurFrequency.Monthly:
                        patternId = "MON";
                        break;

                    case RecurFrequency.Weekly:
                        patternId = "WEEK";
                        break;

                    case RecurFrequency.Daily:
                        patternId = "DAY";
                        break;

                    case RecurFrequency.Hourly:
                        patternId = "HOUR";
                        break;

                    case RecurFrequency.Minutely:
                        patternId = "MIN";
                        break;

                    default:
                        patternId = "SEC";
                        break;
                }

                sb.AppendFormat("document.getElementById('{0}_RP_RB_{1}').focus();", this.ID, patternId);
            }

            // Append the validation script
            sb.AppendFormat("function {0}_Validate(source, args)\r\n" +
                "{{\r\nargs.IsValid = RP_{0}.ValidateRecurrence();\r\n}}\r\n",
                this.ID);

            if(!this.DetermineRenderUplevel)
                sb.Append("function Page_ClientValidate()\r\n{\r\nreturn RP_" + this.ID +
                    ".ValidateRecurrence();\r\n}\r\n");

            sb.Append("//-->\r\n</script>");

            // Register the client-side script
            if(!this.Page.ClientScript.IsClientScriptBlockRegistered("EWS_RP_DayInstanceScript"))
                this.Page.ClientScript.RegisterClientScriptInclude(typeof(RecurrencePattern),
                    "EWS_RP_DayInstanceScript", this.Page.ClientScript.GetWebResourceUrl(typeof(RecurrencePattern),
                    RecurrencePattern.RecurrencePatternScripts + "DayInstance.js"));

            if(!this.Page.ClientScript.IsClientScriptBlockRegistered("EWS_RP_RecurrenceScript"))
                this.Page.ClientScript.RegisterClientScriptInclude(typeof(RecurrencePattern),
                    "EWS_RP_RecurrenceScript", this.Page.ClientScript.GetWebResourceUrl(typeof(RecurrencePattern),
                    RecurrencePattern.RecurrencePatternScripts + "RecurrencePattern.js"));

            // Register the hidden field used to pass back the edited recurrence information
            this.Page.ClientScript.RegisterHiddenField(this.ID + "_Value", String.Empty);

            // Register the startup script
            this.Page.ClientScript.RegisterStartupScript(typeof(RecurrencePattern), this.ID + "_Init",
                sb.ToString());

            base.OnPreRender(e);
        }

        /// <summary>
        /// Call this to set the focus to the recurrence pattern when the page loads
        /// </summary>
        public new void Focus()
        {
            setFocusToPattern = true;
        }

        /// <summary>
        /// This is used to retrieve the recurrence information into the passed recurrence object
        /// </summary>
        /// <param name="recurrence">The recurrence in which to store the settings.</param>
        /// <exception cref="ArgumentNullException">This is thrown if the passed recurrence object is null</exception>
        public void GetRecurrence(Recurrence recurrence)
        {
            if(recurrence == null)
                throw new ArgumentNullException("recurrence", LR.GetString("ExRPRecurrenceIsNull"));

            recurrence.Parse(rRecur.ToString());
        }

        /// <summary>
        /// This is used to initialize the control with settings from an existing recurrence object
        /// </summary>
        /// <param name="recurrence">The recurrence from which to get the settings.  If null, it uses a default daily
        /// recurrence pattern.</param>
        public void SetRecurrence(Recurrence recurrence)
        {
            rRecur = new Recurrence();

            if(recurrence == null)
            {
                rRecur.StartDateTime = DateTime.Today;
                rRecur.RecurDaily(1);
            }
            else
            {
                rRecur.Parse(recurrence.ToStringWithStartDateTime());

                // Reset to daily if undefined
                if(rRecur.Frequency == RecurFrequency.Undefined)
                    rRecur.RecurDaily(rRecur.Interval);
            }

            // If the given pattern is not available, set it to the next best pattern
            if(this.MaximumPattern < rRecur.Frequency)
                switch(this.MaximumPattern)
                {
                    case RecurFrequency.Yearly:
                        rRecur.RecurYearly(DateTime.Now.Month, DateTime.Now.Day, rRecur.Interval);
                        break;

                    case RecurFrequency.Monthly:
                        rRecur.RecurMonthly(DateTime.Now.Day, rRecur.Interval);
                        break;

                    case RecurFrequency.Weekly:
                        rRecur.RecurWeekly(rRecur.Interval, DateUtils.ToDaysOfWeek(DateTime.Now.DayOfWeek));
                        break;

                    case RecurFrequency.Daily:
                        rRecur.RecurDaily(rRecur.Interval);
                        break;

                    case RecurFrequency.Hourly:
                        rRecur.RecurDaily(rRecur.Interval);
                        rRecur.Frequency = RecurFrequency.Hourly;
                        break;

                    default:
                        rRecur.RecurDaily(rRecur.Interval);
                        rRecur.Frequency = RecurFrequency.Minutely;
                        break;
                }
        }
        #endregion
    }
}
