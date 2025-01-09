//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : CalendarBrowserForm.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/05/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This is a simple demonstration application that shows how to load, save, and manage vCalendar and iCalendar
// files including how to edit the properties on the various components.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 11/16/2004  EFW  Created the code
// 04/09/2007  EFW  Updated for use with .NET 2.0
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using EWSoftware.PDI;
using EWSoftware.PDI.Objects;
using EWSoftware.PDI.Parser;
using EWSoftware.PDI.Properties;

namespace CalendarBrowser
{
    /// <summary>
    /// This application demonstrates loading, saving, and managing a vCalendar or iCalendar file including how
    /// to edit the properties on the various components.
    /// </summary>
    public partial class CalendarBrowserForm : Form
    {
        #region Private data members
        //=====================================================================

        private VCalendar vCal;     // The calendar being browsed
        private bool wasModified;
        private readonly StringFormat sf;

        #endregion

        #region Main program entry point
        //=====================================================================

        /// <summary>
        /// The main entry point for the application
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CalendarBrowserForm());
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public CalendarBrowserForm()
        {
            InitializeComponent();

            // The string format to use when drawing the status text
            sf = new StringFormat(StringFormatFlags.NoWrap)
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter
            };

            dgvCalendar.AutoGenerateColumns = false;

            // Load the time zone collection with info from the registry
            TimeZoneRegInfo.LoadTimeZoneInfo();

            vCal = new VCalendar();

            LoadComponentList();
            LoadGridWithItems(true);
        }
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// Load the View combo box with a valid list of components based on the calendar's version
        /// </summary>
        private void LoadComponentList()
        {
            cboComponents.Items.Clear();
            cboComponents.Items.Add("Events");
            cboComponents.Items.Add("To-Do Items");

            if(vCal.Version == SpecificationVersions.iCalendar20)
            {
                cboComponents.Items.Add("Journals");
                cboComponents.Items.Add("Free/Busy Items");
                btnChgTimeZone.Enabled = true;
            }
            else
                btnChgTimeZone.Enabled = false;

            cboComponents.SelectedIndex = 0;
        }

        /// <summary>
        /// Load the grid with the specified calendar items
        /// </summary>
        /// <param name="connectEvents">True to connect list change event, false to not connect it</param>
        private void LoadGridWithItems(bool connectEvents)
        {
            bool modFlag = wasModified;
            int gridIdx = dgvCalendar.CurrentCellAddress.Y;

            // The ListChanged event is connected so that we are notified when the lists are modified
            if(connectEvents)
            {
                vCal.Events.ListChanged += Calendar_ListChanged;
                vCal.ToDos.ListChanged += Calendar_ListChanged;
                vCal.Journals.ListChanged += Calendar_ListChanged;
                vCal.FreeBusys.ListChanged += Calendar_ListChanged;
            }

            // All items are sorted in ascending order before adding them to the grid
            switch(cboComponents.SelectedIndex)
            {
                case 0:
                    vCal.Events.Sort(CalendarSorter);
                    dgvCalendar.DataSource = vCal.Events;
                    break;

                case 1:
                    vCal.ToDos.Sort(CalendarSorter);
                    dgvCalendar.DataSource = vCal.ToDos;
                    break;

                case 2:
                    vCal.Journals.Sort(CalendarSorter);
                    dgvCalendar.DataSource = vCal.Journals;
                    break;

                case 3:
                    vCal.FreeBusys.Sort(CalendarSorter);
                    dgvCalendar.DataSource = vCal.FreeBusys;
                    break;
            }

            // Enable or disable the buttons based on the vCard count
            miClear.Enabled = btnEdit.Enabled = btnDelete.Enabled = (dgvCalendar.RowCount != 0);

            // Stay on the last item selected
            if(gridIdx > -1 && gridIdx < dgvCalendar.RowCount)
                dgvCalendar.CurrentCell = dgvCalendar[0, gridIdx];

            if(connectEvents)
                wasModified = false;    // New collection
            else
                if(!modFlag)
                    wasModified = false;    // Sorting changes it so restore it
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// This sets the modified flag when the collection is edited and adjusts the button enabled states
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void Calendar_ListChanged(object? sender, ListChangedEventArgs e)
        {
            int count;

            switch(cboComponents.SelectedIndex)
            {
                case 0:
                    count = vCal.Events.Count;
                    break;

                case 1:
                    count = vCal.ToDos.Count;
                    break;

                case 2:
                    count = vCal.Journals.Count;
                    break;

                default:
                    count = vCal.FreeBusys.Count;
                    break;
            }

            miClear.Enabled = btnEdit.Enabled = btnDelete.Enabled = (count != 0);
            wasModified = true;
        }

        /// <summary>
        /// Prompt to save if the collection has been modified
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void CalendarBrowserForm_Closing(object sender, CancelEventArgs e)
        {
            if(wasModified && MessageBox.Show("Do you want to discard your changes to the current calendar?",
              "Discard Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
              MessageBoxDefaultButton.Button2) == DialogResult.No)
                e.Cancel = true;
        }

        /// <summary>
        /// Open a vCalendar or iCalendar file
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miOpen_Click(object sender, EventArgs e)
        {
            if(wasModified && MessageBox.Show("Do you want to discard your changes to the current calendar?",
              "Discard Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
              MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            using var dlg = new OpenFileDialog();
            
            dlg.Title = "Load Calendar File";
            dlg.Filter = "ICS files (*.ics)|*.ics|VCS files (*.vcs)|*.vcs|All files (*.*)|*.*";

            if(vCal.Version == SpecificationVersions.vCalendar10)
            {
                dlg.DefaultExt = "vcs";
                dlg.FilterIndex = 2;
            }
            else
            {
                dlg.DefaultExt = "ics";
                dlg.FilterIndex = 1;
            }

            dlg.InitialDirectory = Path.GetFullPath(Path.Combine(
                Environment.CurrentDirectory, @"..\..\..\..\..\PDIFiles"));

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    // Parse the calendar information from the file and load the data grid with some basic
                    // information about the items in it.
                    vCal.Dispose();
                    vCal = VCalendarParser.ParseFromFile(dlg.FileName) ??
                        throw new InvalidOperationException("Invalid calendar file");

                    LoadComponentList();

                    // Find the first collection with items
                    if(vCal.Events.Count != 0)
                        cboComponents.SelectedIndex = 0;
                    else
                    {
                        if(vCal.ToDos.Count != 0)
                            cboComponents.SelectedIndex = 1;
                        else
                        {
                            if(vCal.Journals.Count != 0)
                                cboComponents.SelectedIndex = 2;
                            else
                            {
                                if(vCal.FreeBusys.Count != 0)
                                    cboComponents.SelectedIndex = 3;
                                else
                                    cboComponents.SelectedIndex = 0;
                            }
                        }
                    }

                    LoadGridWithItems(true);
                    lblFilename.Text = dlg.FileName;
                }
                catch(Exception ex)
                {
                    string error = $"Unable to load calendar:\n{ex.Message}";

                    if(ex.InnerException != null)
                    {
                        error += ex.InnerException.Message + "\n";

                        if(ex.InnerException.InnerException != null)
                            error += ex.InnerException.InnerException.Message;
                    }

                    System.Diagnostics.Debug.Write(ex);

                    MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Save a calendar file
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miSave_Click(object sender, EventArgs e)
        {
            using var dlg = new SaveFileDialog();
            
            dlg.Title = "Save Calendar File";
            dlg.Filter = "ICS files (*.ics)|*.ics|VCS files (*.vcs)|*.vcs|All files (*.*)|*.*";

            if(vCal.Version == SpecificationVersions.vCalendar10)
            {
                dlg.DefaultExt = "vcs";
                dlg.FilterIndex = 2;
            }
            else
            {
                dlg.DefaultExt = "ics";
                dlg.FilterIndex = 1;
            }

            dlg.InitialDirectory = Path.GetFullPath(Path.Combine(
                Environment.CurrentDirectory, @"..\..\..\..\..\PDIFiles"));
            dlg.FileName = lblFilename.Text;

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    // Open the file and write the calendar to it. We'll use the same encoding method used by
                    // the parser.
                    using(var sw = new StreamWriter(dlg.FileName, false, PDIParser.DefaultEncoding))
                    {
                        vCal.WriteToStream(sw);
                    }

                    wasModified = false;
                    lblFilename.Text = dlg.FileName;
                }
                catch(Exception ex)
                {
                    string error = $"Unable to save calendar:\n{ex.Message}";

                    if(ex.InnerException != null)
                    {
                        error += ex.InnerException.Message + "\n";

                        if(ex.InnerException.InnerException != null)
                            error += ex.InnerException.InnerException.Message;
                    }

                    System.Diagnostics.Debug.Write(ex);

                    MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Clear all loaded calendar information
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miClear_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to remove all calendar information?",
              "Clear calendar", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
              MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                vCal.ClearProperties();
                this.LoadGridWithItems(true);
                wasModified = true;
            }
        }

        /// <summary>
        /// Show the About dialog box
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miAbout_Click(object sender, EventArgs e)
        {
            using var dlg = new AboutDlg();
            
            dlg.ShowDialog();
        }

        /// <summary>
        /// Close the application
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Add a calendar item
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            switch(cboComponents.SelectedIndex)
            {
                case 0:
                    using(var dlg = new CalendarObjectDlg())
                    {
                        var evt = new VEvent();
                        evt.UniqueId.AssignNewId(true);
                        evt.DateCreated.TimeZoneDateTime = DateTime.Now;
                        evt.LastModified.TimeZoneDateTime = evt.DateCreated.TimeZoneDateTime;
                        dlg.SetValues(evt);

                        if(dlg.ShowDialog() == DialogResult.OK)
                        {
                            dlg.GetValues(evt);

                            // Create a unique ID for the new item
                            evt.UniqueId.AssignNewId(true);

                            vCal.Events.Add(evt);
                        }
                    }
                    break;

                case 1:
                    using(var dlg = new CalendarObjectDlg())
                    {
                        var td = new VToDo();
                        td.DateCreated.TimeZoneDateTime = DateTime.Now;
                        td.LastModified.TimeZoneDateTime = td.DateCreated.TimeZoneDateTime;
                        dlg.SetValues(td);

                        if(dlg.ShowDialog() == DialogResult.OK)
                        {
                            dlg.GetValues(td);

                            // Create a unique ID for the new item
                            td.UniqueId.AssignNewId(true);

                            vCal.ToDos.Add(td);
                        }
                    }
                    break;

                case 2:
                    using(var dlg = new CalendarObjectDlg())
                    {
                        var j = new VJournal();
                        j.DateCreated.TimeZoneDateTime = DateTime.Now;
                        j.LastModified.TimeZoneDateTime = j.DateCreated.TimeZoneDateTime;
                        dlg.SetValues(j);

                        if(dlg.ShowDialog() == DialogResult.OK)
                        {
                            dlg.GetValues(j);

                            // Create a unique ID for the new item
                            j.UniqueId.AssignNewId(true);

                            vCal.Journals.Add(j);
                        }
                    }
                    break;

                case 3:
                    using(var dlg = new VFreeBusyDlg())
                    {
                        if(dlg.ShowDialog() == DialogResult.OK)
                        {
                            var fb = new VFreeBusy();
                            dlg.GetValues(fb);

                            // Create a unique ID for the new item
                            fb.UniqueId.AssignNewId(true);

                            vCal.FreeBusys.Add(fb);
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Edit a calendar item
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(dgvCalendar.CurrentCellAddress.Y == -1)
            {
                MessageBox.Show("Please select an item to edit", "No Item", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            switch(cboComponents.SelectedIndex)
            {
                case 0:
                    using(var dlg = new CalendarObjectDlg())
                    {
                        dlg.SetValues(vCal.Events[dgvCalendar.CurrentCellAddress.Y]);

                        if(dlg.ShowDialog() == DialogResult.OK)
                        {
                            dlg.GetValues(vCal.Events[dgvCalendar.CurrentCellAddress.Y]);

                            wasModified = true;
                        }
                    }
                    break;

                case 1:
                    using(var dlg = new CalendarObjectDlg())
                    {
                        dlg.SetValues(vCal.ToDos[dgvCalendar.CurrentCellAddress.Y]);

                        if(dlg.ShowDialog() == DialogResult.OK)
                        {
                            dlg.GetValues(vCal.ToDos[dgvCalendar.CurrentCellAddress.Y]);

                            wasModified = true;
                        }
                    }
                    break;

                case 2:
                    using(var dlg = new CalendarObjectDlg())
                    {
                        dlg.SetValues(vCal.Journals[dgvCalendar.CurrentCellAddress.Y]);

                        if(dlg.ShowDialog() == DialogResult.OK)
                        {
                            dlg.GetValues(vCal.Journals[dgvCalendar.CurrentCellAddress.Y]);

                            wasModified = true;
                        }
                    }
                    break;

                case 3:
                    using(var dlg = new VFreeBusyDlg())
                    {
                        dlg.SetValues(vCal.FreeBusys[dgvCalendar.CurrentCellAddress.Y]);

                        if(dlg.ShowDialog() == DialogResult.OK)
                        {
                            dlg.GetValues(vCal.FreeBusys[dgvCalendar.CurrentCellAddress.Y]);

                            wasModified = true;
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Delete a calendar item
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dgvCalendar.CurrentCellAddress.Y == -1)
            {
                MessageBox.Show("Please select an item to delete", "No Item", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            if(MessageBox.Show("Are you sure you want to delete the selected item?", "Delete Item",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            switch(cboComponents.SelectedIndex)
            {
                case 0:
                    vCal.Events.RemoveAt(dgvCalendar.CurrentCellAddress.Y);
                    break;

                case 1:
                    vCal.ToDos.RemoveAt(dgvCalendar.CurrentCellAddress.Y);
                    break;

                case 2:
                    vCal.Journals.RemoveAt(dgvCalendar.CurrentCellAddress.Y);
                    break;

                case 3:
                    vCal.FreeBusys.RemoveAt(dgvCalendar.CurrentCellAddress.Y);
                    break;
            }
        }

        /// <summary>
        /// Change the specification version of the calendar
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        /// <remarks>Downgrading to Version 1.0 will cause the loss of any Version 2.0 properties if the file is
        /// saved and reloaded.</remarks>
        private void btnChgVersion_Click(object sender, EventArgs e)
        {
            if(vCal.Version == SpecificationVersions.iCalendar20)
            {
                if(MessageBox.Show("Are you sure you want to downgrade to vCalendar 1.0?  iCalendar 2.0 " +
                  "properties will be lost when the file is saved", "Change Calendar Version",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    vCal.Version = SpecificationVersions.vCalendar10;
            }
            else
                if(MessageBox.Show("Are you sure you want to upgrade to iCalendar 2.0?  Some vCalendar 1.0 " +
                  "properties will be lost when the file is saved.", "Change Calendar Version",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    vCal.Version = SpecificationVersions.iCalendar20;

            LoadComponentList();
            LoadGridWithItems(false);
            wasModified = true;
        }

        /// <summary>
        /// Load the grid with the selected items
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void cboComponents_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cboComponents.SelectedIndex)
            {
                case 0:
                    tbcSummary.Visible = true;
                    tbcSummary.HeaderText = "Event Summary";
                    tbcOrganizer.Visible = tbcComment.Visible = false;
                    break;

                case 1:
                    tbcSummary.Visible = true;
                    tbcSummary.HeaderText = "To-Do Summary";
                    tbcOrganizer.Visible = tbcComment.Visible = false;
                    break;

                case 2:
                    tbcSummary.Visible = true;
                    tbcSummary.HeaderText = "Journal Summary";
                    tbcOrganizer.Visible = tbcComment.Visible = false;
                    break;

                case 3:
                    tbcSummary.Visible = false;
                    tbcOrganizer.Visible = tbcComment.Visible = true;
                    break;
            }

            LoadGridWithItems(false);
        }

        /// <summary>
        /// Edit time zone information
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnChgTimeZone_Click(object sender, EventArgs e)
        {
            using var dlg = new TimeZoneListDlg();
            
            dlg.CurrentCalendar = vCal;
            dlg.Modified = wasModified;
            dlg.ShowDialog();

            if(wasModified != dlg.Modified)
            {
                wasModified = true;
                LoadGridWithItems(false);
            }
        }

        /// <summary>
        /// This will change the encoding used to read and write PDI files
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void ChangeFileEncoding_Click(object sender, EventArgs e)
        {
            if(sender == miFileUnicode)
            {
                miFileUnicode.Checked = true;
                miFileWestEuro.Checked = false;
                miFileASCII.Checked = false;

                // UTF-8 encoding
                PDIParser.DefaultEncoding = new UTF8Encoding(false, false);
            }
            else
                if(sender == miFileWestEuro)
                {
                    miFileUnicode.Checked = false;
                    miFileWestEuro.Checked = true;
                    miFileASCII.Checked = false;

                    // Western European encoding
                    PDIParser.DefaultEncoding = Encoding.GetEncoding("iso-8859-1");
                }
                else
                {
                    miFileUnicode.Checked = false;
                    miFileWestEuro.Checked = false;
                    miFileASCII.Checked = true;

                    // ASCII encoding
                    PDIParser.DefaultEncoding = new ASCIIEncoding();
                }
        }

        /// <summary>
        /// This will change the encoding used to read and write PDI properties
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void ChangePropEncoding_Click(object sender, EventArgs e)
        {
            if(sender == miPropUnicode)
            {
                miPropUnicode.Checked = true;
                miPropWestEuro.Checked = false;
                miPropASCII.Checked = false;

                // UTF-8 encoding
                BaseProperty.DefaultEncoding = new UTF8Encoding(false, false);
            }
            else
                if(sender == miPropWestEuro)
                {
                    miPropUnicode.Checked = false;
                    miPropWestEuro.Checked = true;
                    miPropASCII.Checked = false;

                    // Western European encoding
                    BaseProperty.DefaultEncoding = Encoding.GetEncoding("iso-8859-1");
                }
                else
                {
                    miPropUnicode.Checked = false;
                    miPropWestEuro.Checked = false;
                    miPropASCII.Checked = true;

                    // ASCII encoding
                    BaseProperty.DefaultEncoding = new ASCIIEncoding();
                }
        }

        /// <summary>
        /// This is an example of how you can sort calendar object collections
        /// </summary>
        /// <remarks>Due to the variety of properties in a calendar object, sorting is left up to the developer
        /// utilizing a comparison delegate.  This example sorts the collection by the start date/time property
        /// and, if they are equal, the summary description.  This is used to handle all of the collection types.
        /// </summary>
        private static int CalendarSorter(CalendarObject x, CalendarObject y)
        {
            DateTime d1, d2;
            string? summary1, summary2;

            if(x is VEvent e1)
            {
                VEvent e2 = (VEvent)y;

                d1 = e1.StartDateTime.TimeZoneDateTime;
                d2 = e2.StartDateTime.TimeZoneDateTime;
                summary1 = e1.Summary.Value;
                summary2 = e2.Summary.Value;
            }
            else
            {
                if(x is VToDo t1)
                {
                    VToDo t2 = (VToDo)y;

                    d1 = t1.StartDateTime.TimeZoneDateTime;
                    d2 = t2.StartDateTime.TimeZoneDateTime;
                    summary1 = t1.Summary.Value;
                    summary2 = t2.Summary.Value;
                }
                else
                {
                    if(x is VJournal j1)
                    {
                        VJournal j2 = (VJournal)y;

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
                }
            }

            if(d1.CompareTo(d2) == 0)
            {
                summary1 ??= String.Empty;
                summary2 ??= String.Empty;

                // For descending order, change this to compare summary 2 to summary 1 instead
                return String.Compare(summary1, summary2, StringComparison.Ordinal);
            }

            // For descending order, change this to compare date 2 to date 1 instead
            return d1.CompareTo(d2);
        }

        /// <summary>
        /// This is used to custom draw the start date/time and summary columns based on the information
        /// available.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void dgvCalendar_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            CurrencyManager cm;
            StartDateProperty startProp;
            SummaryProperty? summaryProp;
            DescriptionProperty? descProp;
            DateTimeInstance dti;

            Color foreColor;
            object item;
            string? columnText = null;

            if(e.RowIndex > -1 && (e.ColumnIndex == 0 || e.ColumnIndex == 1))
            {
                cm = (CurrencyManager)dgvCalendar.BindingContext![dgvCalendar.DataSource];
                item = cm.List[e.RowIndex]!;

                if(e.ColumnIndex == 0)
                {
                    if(item is VEvent ev)
                        startProp = ev.StartDateTime;
                    else
                    {
                        if(item is VToDo td)
                            startProp = td.StartDateTime;
                        else
                        {
                            if(item is VJournal jr)
                                startProp = jr.StartDateTime;
                            else
                                startProp = ((VFreeBusy)item).StartDateTime;
                        }
                    }

                    dti = VCalendar.TimeZoneTimeInfo(startProp.TimeZoneDateTime, startProp.TimeZoneId);

                    // Format as date or date/time and include time zone if available
                    if(startProp.ValueLocation == ValLocValue.Date)
                        columnText = $"{dti.StartDateTime:d} {dti.AbbreviatedStartTimeZoneName}";
                    else
                        columnText = $"{dti.StartDateTime} {dti.AbbreviatedStartTimeZoneName}";
                }
                else
                {
                    if(e.ColumnIndex == 1)
                    {
                        if(item is VEvent ev)
                        {
                            summaryProp = ev.Summary;
                            descProp = ev.Description;
                        }
                        else
                        {
                            if(item is VToDo td)
                            {
                                summaryProp = td.Summary;
                                descProp = td.Description;
                            }
                            else
                            {
                                if(item is VJournal jr)
                                {
                                    summaryProp = jr.Summary;
                                    descProp = jr.Description;
                                }
                                else
                                {
                                    summaryProp = null;
                                    descProp = null;
                                }
                            }
                        }

                        // If summary is empty, use description instead
                        if(summaryProp != null && summaryProp.Value != null)
                            columnText = summaryProp.Value;
                        else
                        {
                            if(descProp != null)
                                columnText = descProp.Value;
                        }
                    }
                }

                if(columnText != null)
                {
                    // If multi-line, limit it to the first line
                    if(columnText.IndexOf('\n') != -1)
                        columnText = columnText.Substring(0, columnText.IndexOf('\n'));

                    e.Paint(e.CellBounds, e.PaintParts & ~DataGridViewPaintParts.ContentForeground);

                    // Based the foreground color on the selected state
                    if((e.State & DataGridViewElementStates.Selected) != 0)
                        foreColor = e.CellStyle!.SelectionForeColor;
                    else
                        foreColor = e.CellStyle!.ForeColor;

                    using var b = new SolidBrush(foreColor);
                    
                    e.Graphics!.DrawString(columnText, e.CellStyle.Font, b, e.CellBounds, sf);

                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Invoke edit on cell double click
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void dgvCalendar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > -1)
                btnEdit_Click(sender, e);
        }
        #endregion
    }
}
