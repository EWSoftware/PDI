//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : RecurrenceControl.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/02/2023
// Note    : Copyright 2004-2023, Eric Woodruff, All rights reserved
//
// This is used to edit a recurring calendar object's recurrence rule and recurrence date properties
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 12/09/2004  EFW  Created the code
// 04/09/2007  EFW  Updated for use with .NET 2.0
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

using EWSoftware.PDI;
using EWSoftware.PDI.Objects;
using EWSoftware.PDI.Properties;
using EWSoftware.PDI.Windows.Forms;

namespace CalendarBrowser
{
	/// <summary>
	/// This is used to edit the recurrence rules and dates property collections of a recurring object
	/// </summary>
	public partial class RecurrenceControl : System.Windows.Forms.UserControl
	{
        #region Private data members
        //=====================================================================

        private readonly RRulePropertyCollection rRules;
        private readonly RDatePropertyCollection rDates;
        private bool editsExceptions;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This property is used to get or set whether or not the control is used for exceptions
        /// </summary>
        /// <remarks>If true, the labels are changed to reflect the usage.</remarks>
        [Category("Appearance"), DefaultValue(false), Bindable(true),
         Description("False for recurrence items, true for exception items")]
        public bool EditsExceptions
        {
            get => editsExceptions;
            set
            {
                editsExceptions = value;

                if(!value)
                {
                    lblRRules.Text = "Recurrence &Rules";
                    lblRDates.Text = "Recurrence &Dates";
                }
                else
                {
                    lblRRules.Text = "Exception &Rules";
                    lblRDates.Text = "Exception &Dates";
                }
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public RecurrenceControl()
		{
			InitializeComponent();

            // Set the short date/long time pattern based on the current culture
            dtpRDate.CustomFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " +
                CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;
            dtpRDate.Value = DateTime.Today;

            rRules = new RRulePropertyCollection();
            rDates = new RDatePropertyCollection();
            this.SetButtonStates();
		}
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// Enable or disable buttons based on the collection item count
        /// </summary>
        private void SetButtonStates()
        {
            bool enabled = (rRules.Count != 0);

            btnEditRRule.Enabled = enabled;
            btnRemoveRRule.Enabled = enabled;
            btnClearRRules.Enabled = enabled;

            enabled = (rDates.Count != 0);
            btnRemoveRDate.Enabled = enabled;
            btnClearRDates.Enabled = enabled;
        }

        /// <summary>
        /// Initialize the dialog controls using the specified recurrence rule and recurrence date collections
        /// </summary>
        /// <param name="rr">The recurrence rules from which to get the settings</param>
        /// <param name="rd">The recurrence dates from which to get the settings</param>
        public void SetValues(RRulePropertyCollection rr, RDatePropertyCollection rd)
        {
            rRules.Clear();
            rDates.Clear();
            rRules.CloneRange(rr);
            rDates.CloneRange(rd);

            lbRRules.Items.Clear();

            foreach(RRuleProperty r in rRules)
                lbRRules.Items.Add(r.Recurrence.ToString());

            // We won't handle period RDate values
            lbRDates.Items.Clear();

            foreach(RDateProperty rdt in rDates)
            {
                if(rdt.ValueLocation == ValLocValue.Date)
                    lbRDates.Items.Add(rdt.TimeZoneDateTime.ToString("d", CultureInfo.CurrentCulture));
                else
                    lbRDates.Items.Add(rdt.TimeZoneDateTime.ToString("G", CultureInfo.CurrentCulture));
            }

            this.SetButtonStates();
        }

        /// <summary>
        /// Initialize the dialog controls using the specified exception rule and exception date collections
        /// </summary>
        /// <param name="er">The exception rules from which to get the settings</param>
        /// <param name="ed">The exception dates from which to get the settings</param>
        public void SetValues(RRulePropertyCollection er, ExDatePropertyCollection ed)
        {
            if(ed == null)
                throw new ArgumentNullException(nameof(ed));

            rRules.Clear();
            rDates.Clear();
            rRules.CloneRange(er);

            // Convert the exception dates to RDate format internally
            foreach(ExDateProperty edate in ed)
            {
                RDateProperty rd = new RDateProperty
                {
                    ValueLocation = edate.ValueLocation,
                    TimeZoneId = edate.TimeZoneId,
                    TimeZoneDateTime = edate.TimeZoneDateTime
                };

                rDates.Add(rd);
            }

            lbRRules.Items.Clear();

            foreach(RRuleProperty r in rRules)
                lbRRules.Items.Add(r.Recurrence.ToString());

            lbRDates.Items.Clear();

            foreach(RDateProperty rdt in rDates)
                if(rdt.ValueLocation == ValLocValue.Date)
                    lbRDates.Items.Add(rdt.TimeZoneDateTime.ToString("d", CultureInfo.CurrentCulture));
                else
                    lbRDates.Items.Add(rdt.TimeZoneDateTime.ToString("G", CultureInfo.CurrentCulture));

            this.SetButtonStates();
        }

        /// <summary>
        /// Update the recurrence rule and recurrence date collections with the dialog control values
        /// </summary>
        /// <param name="rr">The recurrence rules collection to update</param>
        /// <param name="rd">The recurrence dates collection to update</param>
        /// <remarks>Note that the time zone ID will need to be set in the returned RDATE property collection to
        /// make sure that it is consistent with the containing component.</remarks>
        public void GetValues(RRulePropertyCollection rr, RDatePropertyCollection rd)
        {
            rr?.Clear();
            rd?.Clear();
            rr?.CloneRange(rRules);
            rd?.CloneRange(rDates);
        }

        /// <summary>
        /// Update the exception rule and exception date collections with the dialog control values
        /// </summary>
        /// <param name="er">The exception rules collection to update</param>
        /// <param name="ed">The exception dates collection to update</param>
        /// <remarks>Note that the time zone ID will need to be set in the returned EXDATE property collection to
        /// make sure that it is consistent with the containing component.</remarks>
        public void GetValues(RRulePropertyCollection er, ExDatePropertyCollection ed)
        {
            er?.Clear();
            ed?.Clear();
            er?.CloneRange(rRules);

            // Convert the RDates to ExDate format
            foreach(RDateProperty rdate in rDates)
            {
                ExDateProperty edate = new ExDateProperty
                {
                    ValueLocation = rdate.ValueLocation,
                    TimeZoneId = rdate.TimeZoneId,
                    TimeZoneDateTime = rdate.TimeZoneDateTime
                };

                ed?.Add(edate);
            }
        }

        /// <summary>
        /// This is called by the containing form to apply a new time zone to the RDATE/EXDATE property date/time
        /// values in the control.
        /// </summary>
        /// <param name="oldTZ">The old time zone's ID</param>
        /// <param name="newTZ">The new time zone's ID</param>
        public void ApplyTimeZone(string oldTZ, string newTZ)
        {
            DateTimeInstance dti;
            int idx = 0;

            // This only applies to RDATE/EXDATE properties with a time
            foreach(RDateProperty rdt in rDates)
            {
                if(rdt.ValueLocation == ValLocValue.DateTime)
                {
                    if(oldTZ == null)
                        dti = VCalendar.LocalTimeToTimeZoneTime(rdt.TimeZoneDateTime, newTZ);
                    else
                        dti = VCalendar.TimeZoneToTimeZone(rdt.TimeZoneDateTime, oldTZ, newTZ);

                    rdt.TimeZoneDateTime = dti.StartDateTime;
                    lbRDates.Items[idx] = dti.StartDateTime.ToString("G", CultureInfo.CurrentCulture);
                }

                idx++;
            }
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Add a recurrence rule
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void btnAddRRule_Click(object sender, EventArgs e)
        {
            using(RecurrencePropertiesDlg dlg = new RecurrencePropertiesDlg())
            {
                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    // Add the recurrence rule to the collection and the list box using the appropriate type
                    if(!editsExceptions)
                    {
                        RRuleProperty rr = new RRuleProperty();
                        dlg.GetRecurrence(rr.Recurrence);
                        rRules.Add(rr);
                        lbRRules.Items.Add(rr.Recurrence.ToString());
                    }
                    else
                    {
                        ExRuleProperty er = new ExRuleProperty();
                        dlg.GetRecurrence(er.Recurrence);
                        rRules.Add(er);
                        lbRRules.Items.Add(er.Recurrence.ToString());
                    }
                }
            }

            this.SetButtonStates();
        }

        /// <summary>
        /// Edit a recurrence rule
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void btnEditRRule_Click(object sender, EventArgs e)
        {
            if(lbRRules.SelectedIndex == -1)
            {
                MessageBox.Show("Select a recurrence rule to edit");
                return;
            }

            using(RecurrencePropertiesDlg dlg = new RecurrencePropertiesDlg())
            {
                try
                {
                    Recurrence r = rRules[lbRRules.SelectedIndex].Recurrence;
                    dlg.SetRecurrence(r);

                    if(dlg.ShowDialog() == DialogResult.OK)
                    {
                        // Update the recurrence rule in the collection and the list box
                        dlg.GetRecurrence(r);
                        lbRRules.Items[lbRRules.SelectedIndex] = r.ToString();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Remove a recurrence rule
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void btnRemoveRRule_Click(object sender, EventArgs e)
        {
            if(lbRRules.SelectedIndex == -1)
            {
                MessageBox.Show("Select a recurrence rule to remove");
                return;
            }

            // Remove the recurrence rule from the collection and the list box
            rRules.RemoveAt(lbRRules.SelectedIndex);
            lbRRules.Items.RemoveAt(lbRRules.SelectedIndex);

            this.SetButtonStates();
        }

        /// <summary>
        /// Clear all recurrence rules
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void btnClearRRules_Click(object sender, EventArgs e)
        {
            rRules.Clear();
            lbRRules.Items.Clear();
            this.SetButtonStates();
        }

        /// <summary>
        /// Add a recurrence date
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void btnAddRDate_Click(object sender, EventArgs e)
        {
            // Add a recurrence date to the collection and the list box.  If "Exclude whole day" is checked, the
            // time part is omitted.
            if(chkExcludeDay.Checked)
            {
                RDateProperty rdate = new RDateProperty
                {
                    ValueLocation = ValLocValue.Date,
                    TimeZoneDateTime = dtpRDate.Value.Date
                };

                rDates.Add(rdate);
                lbRDates.Items.Add(dtpRDate.Value.Date.ToString("d", CultureInfo.CurrentCulture));
            }
            else
            {
                RDateProperty rdate = new RDateProperty { TimeZoneDateTime = dtpRDate.Value };

                rDates.Add(rdate);
                lbRDates.Items.Add(dtpRDate.Value.ToString("G", CultureInfo.CurrentCulture));
            }

            this.SetButtonStates();
        }

        /// <summary>
        /// Remove a recurrence date
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void btnRemoveRDate_Click(object sender, EventArgs e)
        {
            if(lbRDates.SelectedIndex == -1)
            {
                MessageBox.Show("Select a recurrence date to remove");
                return;
            }

            // Remove a recurrence date from the collection and the list box
            rDates.RemoveAt(lbRDates.SelectedIndex);
            lbRDates.Items.RemoveAt(lbRDates.SelectedIndex);
            this.SetButtonStates();
        }

        /// <summary>
        /// Clear all recurrence dates
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void btnClearRDates_Click(object sender, EventArgs e)
        {
            // Remove all recurrence dates from the collection and the list box
            rDates.Clear();
            lbRDates.Items.Clear();
            this.SetButtonStates();
        }
        #endregion
    }
}
