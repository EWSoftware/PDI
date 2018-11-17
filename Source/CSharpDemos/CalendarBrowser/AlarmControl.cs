//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : AlarmControl.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/29/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Visual C#
//
// This is used to edit a calendar object's alarm collection properties
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 01/03/2005  EFW  Created the code
// 04/18/2007  EFW  Updated for use with .NET 2.0
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

using EWSoftware.PDI;
using EWSoftware.PDI.Objects;
using EWSoftware.PDI.Properties;

namespace CalendarBrowser
{
    /// <summary>
    /// This is used to edit the Alarm property collection
    /// </summary>
    public partial class AlarmControl : EWSoftware.PDI.Windows.Forms.BrowseControl
    {
        #region Private data members
        //=====================================================================

        private VAlarm currentAlarm;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public AlarmControl()
        {
            InitializeComponent();

            // Set the short date/long time pattern based on the current culture
            dtpTrigger.CustomFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " +
                  CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;

            this.BindingSource.PositionChanged += BindingSource_PositionChanged;

            // Use a default collection as the data source
            this.BindingSource.DataSource = new VAlarmCollection();
        }
        #endregion

        #region Method overrides
        //=====================================================================

        /// <summary>
        /// Enable or disable the controls based on whether or not there are items in the collection
        /// </summary>
        /// <param name="enable">True to enable the controls, false to disable them.</param>
        public override void EnableControls(bool enable)
        {
            tabInfo.Enabled = enable;

            if(enable)
                cboAction.Focus();
        }

        /// <summary>
        /// This is overridden to bind the controls to the data source
        /// </summary>
        /// <remarks>In this case, we can bind to a few of the simple properties but the rest will be handled
        /// manually when the position changes.
        /// </remarks>
        public override void BindToControls()
        {
            txtOtherAction.DataBindings.Add("Text", this.BindingSource, "Action_OtherAction");

            udcRepeat.DataBindings.Add("Value", this.BindingSource, "Repeat_RepeatCount");
            txtSummary.DataBindings.Add("Text", this.BindingSource, "Summary_Value");
            txtDescription.DataBindings.Add("Text", this.BindingSource, "Description_Value");

            // The binding events translate between the index value and the action type
            Binding b = new Binding("SelectedIndex", this.BindingSource, "Action_Action");
            b.Format += AlarmAction_Format;
            b.Parse += AlarmAction_Parse;
            cboAction.DataBindings.Add(b);
        }
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// Validate the items in the collection
        /// </summary>
        /// <returns>Returns true if all are valid or false if any item is not valid.  The position is set to the
        /// first invalid item.</returns>
        /// <remarks>The navigator doesn't allow for validation of all fields so we have to do some checking
        /// before saving the results.</remarks>
        public bool ValidateItems()
        {
            bool result = true;

            tabInfo.SelectedTab = pgGeneral;

            this.ErrorProvider.Clear();

            VAlarmCollection alarms = (VAlarmCollection)this.BindingSource.DataSource;

            foreach(VAlarm a in alarms)
            {
                if(a.Trigger.DurationValue == Duration.Zero && a.Trigger.TimeZoneDateTime == DateTime.MinValue)
                {
                    this.BindingSource.Position = alarms.IndexOf(a);
                    this.ErrorProvider.SetError(dtpTrigger, "A trigger duration or date/time must be specified");
                    result = false;
                }

                if((a.Action.Action == AlarmAction.EMail || a.Action.Action == AlarmAction.Display) &&
                  String.IsNullOrWhiteSpace(a.Description.Value))
                {
                    this.ErrorProvider.SetError(txtDescription, "A description is required");
                    result = false;
                }

                if(a.Action.Action == AlarmAction.EMail)
                    if(String.IsNullOrEmpty(a.Summary.Value))
                    {
                        this.ErrorProvider.SetError(txtSummary, "A summary is required for an e-mail alarm");
                        result = false;
                    }
                    else
                        if(result && a.Attendees.Count == 0)
                        {
                            tabInfo.SelectedTab = pgAttendees;
                            this.ErrorProvider.SetError(ucAttendees, "At least one attendee is required for " +
                                "an e-mail alarm");
                            result = false;
                        }

                if(!result)
                    break;
            }

            return result;
        }

        /// <summary>
        /// Store changes to the unbound controls
        /// </summary>
        private void StoreChanges()
        {
            if(currentAlarm == null)
                return;

            if(!dtpTrigger.Checked)
            {
                currentAlarm.Trigger.TimeZoneDateTime = DateTime.MinValue;
                currentAlarm.Trigger.DurationValue = new Duration(txtTrigger.Text);
                currentAlarm.Trigger.RelatedToEnd = chkFromEnd.Checked;
            }
            else
            {
                currentAlarm.Trigger.DurationValue = Duration.Zero;
                currentAlarm.Trigger.TimeZoneDateTime = dtpTrigger.Value;
                currentAlarm.Trigger.ValueLocation = ValLocValue.DateTime;
                currentAlarm.Trigger.RelatedToEnd = false;
            }

            currentAlarm.Duration.DurationValue = new Duration(txtDuration.Text);

            // Get attendees and attachments
            currentAlarm.Attendees.Clear();
            currentAlarm.Attendees.CloneRange((AttendeePropertyCollection)ucAttendees.BindingSource.DataSource);
            ucAttachments.GetValues(currentAlarm.Attachments);
        }

        /// <summary>
        /// This is called by the containing form to apply a new time zone to the date/time values in the control
        /// </summary>
        /// <param name="oldTZ">The old time zone's ID</param>
        /// <param name="newTZ">The new time zone's ID</param>
        public void ApplyTimeZone(string oldTZ, string newTZ)
        {
            DateTimeInstance dti;

            if(dtpTrigger.Checked)
            {
                if(oldTZ == null)
                    dti = VCalendar.LocalTimeToTimeZoneTime(dtpTrigger.Value, newTZ);
                else
                    dti = VCalendar.TimeZoneToTimeZone(dtpTrigger.Value, oldTZ, newTZ);

                dtpTrigger.Value = dti.StartDateTime;
                this.StoreChanges();
            }
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// This is used to translate between the alarm action and the index
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void AlarmAction_Format(object sender, ConvertEventArgs e)
        {
            e.Value = (int)e.Value;
        }

        /// <summary>
        /// This is used to translate between the alarm action and the index
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void AlarmAction_Parse(object sender, ConvertEventArgs e)
        {
            e.Value = (AlarmAction)e.Value;
        }

        /// <summary>
        /// Update the prior row with the values from the unbound controls and load the values for the new row
        /// when the position changes.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void BindingSource_PositionChanged(object sender, EventArgs e)
        {
            VAlarm newItem = (VAlarm)this.BindingSource.Current;

            // If all deleted, ignore it
            if(newItem == null)
            {
                currentAlarm = null;
                return;
            }

            // Save changes to the unbound controls to the prior row
            this.StoreChanges();

            // Load the new values into the unbound controls
            currentAlarm = newItem;

            if(currentAlarm.Trigger.ValueLocation == ValLocValue.Duration)
            {
                if(currentAlarm.Trigger.DurationValue != Duration.Zero)
                    txtTrigger.Text = currentAlarm.Trigger.DurationValue.ToString(Duration.MaxUnit.Weeks);
                else
                    txtTrigger.Text = String.Empty;

                chkFromEnd.Checked = currentAlarm.Trigger.RelatedToEnd;
                dtpTrigger.Checked = false;
            }
            else
            {
                txtTrigger.Text = String.Empty;
                chkFromEnd.Checked = false;

                if(currentAlarm.Trigger.TimeZoneDateTime == DateTime.MinValue)
                    dtpTrigger.Checked = false;
                else
                {
                    dtpTrigger.Value = currentAlarm.Trigger.TimeZoneDateTime;
                    dtpTrigger.Checked = true;
                }
            }

            if(currentAlarm.Duration.DurationValue != Duration.Zero)
                txtDuration.Text = currentAlarm.Duration.DurationValue.ToString(Duration.MaxUnit.Weeks);
            else
                txtDuration.Text = String.Empty;

            // Set attendees and attachments
            ucAttendees.BindingSource.DataSource = new AttendeePropertyCollection().CloneRange(currentAlarm.Attendees);
            ucAttachments.SetValues(currentAlarm.Attachments);
        }

        /// <summary>
        /// Store changes to the unbound controls when the control loses the focus too
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void AlarmControl_Leave(object sender, EventArgs e)
        {
            this.StoreChanges();
        }

        /// <summary>
        /// Enable the Other Action text box if Other is selected as the action type and enable or disable the
        /// other controls as needed.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters.</param>
        private void cboAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch((AlarmAction)cboAction.SelectedIndex)
            {
                case AlarmAction.Audio:
                    txtSummary.Enabled = txtDescription.Enabled = txtOtherAction.Enabled = pgAttendees.Enabled = false;
                    pgAttachments.Enabled = true;
                    txtOtherAction.Text = null;
                    break;

                case AlarmAction.Display:
                    txtSummary.Enabled = txtOtherAction.Enabled = pgAttendees.Enabled = pgAttachments.Enabled = false;
                    txtDescription.Enabled = true;
                    txtOtherAction.Text = null;
                    break;

                case AlarmAction.EMail:
                    txtOtherAction.Enabled = false;
                    txtSummary.Enabled = txtDescription.Enabled = pgAttendees.Enabled = pgAttachments.Enabled = true;
                    txtOtherAction.Text = null;
                    break;

                case AlarmAction.Procedure:
                    txtSummary.Enabled = txtOtherAction.Enabled = pgAttendees.Enabled = false;
                    txtDescription.Enabled = pgAttachments.Enabled = true;
                    txtOtherAction.Text = null;
                    break;

                default:    // Other.  Enable everything.
                    txtSummary.Enabled = txtDescription.Enabled = txtOtherAction.Enabled = pgAttendees.Enabled =
                        pgAttachments.Enabled = true;
                    break;
            }
        }

        /// <summary>
        /// Ensure the duration value is valid
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void Duration_Validating(object sender, CancelEventArgs e)
        {
            TextBox t = (TextBox)sender;
            Duration d;

            t.Text = t.Text.Trim();

            if(t.Text.Length != 0 && !Duration.TryParse(t.Text, out d))
            {
                this.ErrorProvider.SetError(t, "Invalid duration value");
                e.Cancel = true;
            }
            else
                this.ErrorProvider.SetError(t, String.Empty);
        }
        #endregion
    }
}
