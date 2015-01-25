//===============================================================================================================
// System  : EWSoftware.PDI Windows Forms Controls
// File    : HolidayManager.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 10/17/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a user controls that can be used to manage a set of holidays in a HolidayCollection
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 01/20/2004  EFW  Created the code
// 04/08/2007  EFW  Updated for use with .NET 2.0
// 10/17/2014  EFW  Updated for use with .NET 4.0
//===============================================================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace EWSoftware.PDI.Windows.Forms
{
	/// <summary>
	/// This user control can be used to manage a set of holidays in a <see cref="HolidayCollection"/>
	/// </summary>
	public partial class HolidayManager : System.Windows.Forms.UserControl
    {
        #region Private data members
        //=====================================================================

        // The holiday collection to manage
        private HolidayCollection holidays;

        // The load/save control visible state.  Using the Visible property on the controls isn't reliable
        // because if the parent isn't visible, the controls return false even if they are visible on the parent
        // when it is visible.
        private bool loadSaveVisible;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This property shows or hides the <strong>Load</strong>/<strong>Save</strong> buttons
        /// </summary>
        /// <remarks>If hidden, the holiday collection cannot be loaded from or saved to a file from the control</remarks>
		[Category("Behavior"), DefaultValue(true), Bindable(true),
         Description("Show or hide the Load/Save buttons")]
        public bool ShowLoadSaveControls
        {
            get { return loadSaveVisible; }
            set
            {

                loadSaveVisible = btnLoad.Visible = btnSave.Visible = value;
            }
        }

        /// <summary>
        /// This property is used to get or set the default holidays used to populate the list box when the
        /// <strong>Default</strong> button is clicked.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable<Holiday> Defaults { get; set; }

        /// <summary>
        /// This property is used to get or set the holidays to manage
        /// </summary>
        /// <value>When set, the passed collection will not be modified.  The holidays in it will be cloned and
        /// used for the control.  Get the holidays to retrieve the modified collection if wanted.  If set to
        /// null, the collection to edit is cleared.</value>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable<Holiday> Holidays
        {
            get { return holidays; }
            set
            {
                holidays.Clear();

                if(value != null)
                    foreach(Holiday h in value)
                        holidays.Add((Holiday)h.Clone());

                this.LoadHolidayList();
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public HolidayManager()
		{
			InitializeComponent();

            holidays = new HolidayCollection();

            this.Defaults = new HolidayCollection().AddStandardHolidays();

            loadSaveVisible = true;
		}
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// This is used to reload the holiday list box and adjust the button states as needed
        /// </summary>
        private void LoadHolidayList()
        {
            int lastIdx = lbHolidays.SelectedIndex;

            lbHolidays.DataSource = null;
            lbHolidays.DisplayMember = "Description";
            lbHolidays.DataSource = holidays;

            if(holidays.Count > 0)
            {
                if(lastIdx > -1)
                    if(lastIdx < lbHolidays.Items.Count)
                        lbHolidays.SelectedIndex = lastIdx;
                    else
                        lbHolidays.SelectedIndex = lbHolidays.Items.Count - 1;

                btnEdit.Enabled = true;
                btnRemove.Enabled = true;
                btnClear.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
                btnRemove.Enabled = false;
                btnClear.Enabled = false;
            }
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Add a new holiday to the list box
        /// </summary>
        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            using(HolidayPropertiesDlg dlg = new HolidayPropertiesDlg())
            {
                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    holidays.Add(dlg.HolidayInfo);
                    this.LoadHolidayList();
                }
            }
        }

        /// <summary>
        /// Edit a holiday entry in the list box
        /// </summary>
        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            if(lbHolidays.SelectedIndex == -1)
            {
                MessageBox.Show(LR.GetString("EditHMSelectHoliday"));
                return;
            }

            using(HolidayPropertiesDlg dlg = new HolidayPropertiesDlg())
            {
                // The dialog takes care loading data from the object
                dlg.HolidayInfo = holidays[lbHolidays.SelectedIndex];

                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    // Replace the holiday with the edited information.  Because the type may change, the
                    // add/edit dialog returns a new instance.
                    holidays[lbHolidays.SelectedIndex] = dlg.HolidayInfo;
                    this.LoadHolidayList();
                }
            }
        }

        /// <summary>
        /// Delete a holiday from the list box
        /// </summary>
        private void btnRemove_Click(object sender, System.EventArgs e)
        {
            if(lbHolidays.SelectedIndex == -1)
                MessageBox.Show(LR.GetString("EditHMSelectHoliday"));
            else
            {
                holidays.RemoveAt(lbHolidays.SelectedIndex);
                this.LoadHolidayList();
            }
        }

        /// <summary>
        /// Clear all holidays from the list box
        /// </summary>
        private void btnClear_Click(object sender, System.EventArgs e)
        {
            holidays.Clear();
            this.LoadHolidayList();
        }

        /// <summary>
        /// Clear all holidays and add the default set as defined by the <see cref="Defaults"/> property
        /// </summary>
        private void btnDefault_Click(object sender, System.EventArgs e)
        {
            holidays.Clear();

            if(this.Defaults != null)
                foreach(Holiday h in this.Defaults)
                    holidays.Add((Holiday)h.Clone());

            this.LoadHolidayList();
        }

        /// <summary>
        /// Load the holiday info from a file in the format selected by the user
        /// </summary>
        private void btnLoad_Click(object sender, System.EventArgs e)
        {
            using(OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = LR.GetString("HMLoadTitle");
                dlg.DefaultExt = "xml";
                dlg.Filter = LR.GetString("HMFileDlgFilter");
                dlg.InitialDirectory = Environment.CurrentDirectory;

                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    // Deserialize the holidays from a file of the selected format
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;

                        using(FileStream fs = new FileStream(dlg.FileName, FileMode.Open))
                        {
                            XmlSerializer xs = new XmlSerializer(typeof(HolidayCollection));
                            holidays = (HolidayCollection)xs.Deserialize(fs);
                        }

                        this.LoadHolidayList();
                    }
                    catch(Exception ex)
                    {
                        System.Diagnostics.Debug.Write(ex.ToString());

                        string errorMsg = LR.GetString("HMLoadError", ex.Message);

                        if(ex.InnerException != null)
                        {
                            errorMsg += ex.InnerException.Message + "\n";

                            if(ex.InnerException.InnerException != null)
                                errorMsg += ex.InnerException.InnerException.Message;
                        }

                        System.Diagnostics.Debug.WriteLine(errorMsg);

                        MessageBox.Show(errorMsg, LR.GetString("HMErrorLoading"), MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }

        /// <summary>
        /// Save the holiday info to a file in the format selected by the user
        /// </summary>
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            using(SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = LR.GetString("HMSaveTitle");
                dlg.DefaultExt = "xml";
                dlg.Filter = LR.GetString("HMFileDlgFilter");
                dlg.InitialDirectory = Environment.CurrentDirectory;

                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    // Serialize the holidays to a file of the selected format
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;

                        using(FileStream fs = new FileStream(dlg.FileName, FileMode.Create))
                        {
                            XmlSerializer xs = new XmlSerializer(typeof(HolidayCollection));
                            xs.Serialize(fs, holidays);
                        }
                    }
                    catch(Exception ex)
                    {
                        System.Diagnostics.Debug.Write(ex.ToString());

                        string errorMsg = LR.GetString("HMSaveError", ex.Message);

                        if(ex.InnerException != null)
                        {
                            errorMsg += ex.InnerException.Message + "\n";

                            if(ex.InnerException.InnerException != null)
                                errorMsg += ex.InnerException.InnerException.Message;
                        }

                        System.Diagnostics.Debug.WriteLine(errorMsg);

                        MessageBox.Show(errorMsg, LR.GetString("HMErrorSaving"), MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }
        #endregion
    }
}
