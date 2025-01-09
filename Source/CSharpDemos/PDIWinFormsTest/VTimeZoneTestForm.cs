//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : VTimeZoneTestForm.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/05/2025
// Note    : Copyright 2003-2025, Eric Woodruff, All rights reserved
//
// This is a simple demonstration used to test the EWSoftware PDI time zone classes and methods.  This
// demonstration depends on the time zone information present in the Windows registry.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 10/15/2004  EFW  Created the code
// 04/07/2007  EFW  Updated to use the new .NET 2.0 features
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

using EWSoftware.PDI;
using EWSoftware.PDI.Objects;

namespace PDIWinFormsTest
{
	/// <summary>
	/// This form is used to test the time zone classes and methods
	/// </summary>
	public partial class VTimeZoneTestForm : System.Windows.Forms.Form
	{
        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public VTimeZoneTestForm()
		{
			InitializeComponent();

            // Set the short date/long time pattern based on the current culture
            dtpSourceDate.CustomFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " +
                  CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;
		}
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// This loads the time zone information from the registry when the form opens
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void VTimeZoneTestForm_Load(object sender, EventArgs e)
        {
            TimeZoneRegInfo.LoadTimeZoneInfo();

            // VTimeZoneCollection is bindable.  Since they will share the same data source, give each combo box
            // its own binding context.
            cboSourceTimeZone.BindingContext = new BindingContext();
            cboDestTimeZone.BindingContext = new BindingContext();

            // To access a child property, separate the child property name from the parent property name with an
            // underscore.
            cboSourceTimeZone.DisplayMember = cboDestTimeZone.DisplayMember =
                cboSourceTimeZone.ValueMember = cboDestTimeZone.DisplayMember = "TimeZoneId_Value";
            cboSourceTimeZone.DataSource = cboDestTimeZone.DataSource = VCalendar.TimeZones;

            dtpSourceDate.Value = new DateTime(DateTime.Today.Year, 1, 1, 10, 0, 0);
            UpdateTimes(sender, e);
        }

        /// <summary>
        /// Clear out the time zone collection so as not to affect any of the other test forms
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void VTimeZoneTestForm_Closing(object sender, CancelEventArgs e)
        {
            VCalendar.TimeZones.Clear();
        }

        /// <summary>
        /// Convert the selected date/time to local time and back and to the selected time zone and back to test
        /// the time zone conversion code.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void UpdateTimes(object sender, EventArgs e)
        {
            // Wait until both have a value selected
            if(cboSourceTimeZone.SelectedIndex == -1 || cboDestTimeZone.SelectedIndex == -1)
                return;

            VTimeZone vtzSource = VCalendar.TimeZones[cboSourceTimeZone.SelectedIndex];
            VTimeZone vtzDest = VCalendar.TimeZones[cboDestTimeZone.SelectedIndex];

            // Show information for the selected time zones
            txtTimeZoneInfo.Clear();
            txtTimeZoneInfo.AppendText("From Time Zone:\r\n");
            txtTimeZoneInfo.AppendText(vtzSource.ToString());
            txtTimeZoneInfo.AppendText("\r\n");
            txtTimeZoneInfo.AppendText("To Time Zone:\r\n");
            txtTimeZoneInfo.AppendText(vtzDest.ToString());

            // Do the conversions.  Each is round tripped to make sure it gets the expected results.  Note that
            // there will be some anomalies when round tripping times that are near the standard time/DST shift
            // as these times are somewhat ambiguous and their meaning can vary depending on which side of the
            // shift you are on and the direction of the conversion.
            DateTime dt = dtpSourceDate.Value;

            DateTimeInstance dti = VCalendar.TimeZoneTimeToLocalTime(dt, vtzSource.TimeZoneId.Value);

            lblLocalTime.Text = $"{dti.StartDateTime} {dti.StartTimeZoneName}";

            dti = VCalendar.LocalTimeToTimeZoneTime(dti.StartDateTime, vtzSource.TimeZoneId.Value);
            lblLocalBackToSource.Text = $"{dti.StartDateTime} {dti.StartTimeZoneName}";

            dti = VCalendar.TimeZoneToTimeZone(dt, vtzSource.TimeZoneId.Value, vtzDest.TimeZoneId.Value);
            lblDestTime.Text = $"{dti.StartDateTime} {dti.StartTimeZoneName}";

            dti = VCalendar.TimeZoneToTimeZone(dti.StartDateTime, vtzDest.TimeZoneId.Value, vtzSource.TimeZoneId.Value);
            lblDestBackToSource.Text = $"{dti.StartDateTime} {dti.StartTimeZoneName}";
        }

        /// <summary>
        /// Save time zone information to a calendar file for use as a "time zone database"
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnSaveTZs_Click(object sender, EventArgs e)
        {
            using var dlg = new SaveFileDialog();
            
            dlg.Title = "Save Time Zone Database";
            dlg.Filter = "ICS files (*.ics)|*.ics|All files (*.*)|*.*";
            dlg.DefaultExt = "ics";
            dlg.FilterIndex = 1;
            dlg.InitialDirectory = Environment.CurrentDirectory;
            dlg.FileName = "TimeZoneDB.ics";

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    // Open the file and write the time zone data to it
                    using var sw = new StreamWriter(dlg.FileName);
                    
                    // Since we are writing out the time zone collection by itself, we'll need to provide
                    // the calender wrapper.
                    sw.WriteLine("BEGIN:VCALENDAR");
                    sw.WriteLine("VERSION:2.0");
                    sw.WriteLine("PRODID:-//EWSoftware//PDI Class Library//EN");

                    foreach(VTimeZone tz in VCalendar.TimeZones)
                        tz.WriteToStream(sw);

                    sw.WriteLine("END:VCALENDAR");
                }
                catch(Exception ex)
                {
                    string errorMsg = $"Unable to save time zone info:\n{ex.Message}";

                    if(ex.InnerException != null)
                    {
                        errorMsg += ex.InnerException.Message + "\n";

                        if(ex.InnerException.InnerException != null)
                            errorMsg += ex.InnerException.InnerException.Message;
                    }

                    System.Diagnostics.Debug.Write(ex);

                    MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }
        #endregion
    }
}
