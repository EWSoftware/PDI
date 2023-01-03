//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : VCardBrowserForm.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/02/2020
// Note    : Copyright 2004-2020, Eric Woodruff, All rights reserved
//
// This is a simple demonstration application that shows how to load, save, and manage a set of vCards including
// how to edit the various vCard properties.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 10/15/2004  EFW  Created the code
// 04/06/2007  EFW  Updated to make use of the new .NET 2.0 features
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EWSoftware.PDI.Objects;
using EWSoftware.PDI.Parser;
using EWSoftware.PDI.Properties;

namespace vCardBrowser
{
	/// <summary>
	/// This application demonstrates loading, saving, and managing a set of vCards including how to edit the
    /// various vCard properties.
	/// </summary>
	public partial class VCardBrowserForm : System.Windows.Forms.Form
	{
        #region Private data members
        //=====================================================================

        private VCardCollection vCards;
        private bool wasModified;
        private StringFormat sf;

        #endregion

        #region Main program entry point
        //=====================================================================

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new VCardBrowserForm());
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public VCardBrowserForm()
		{
			InitializeComponent();
            tbcVersion.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // The string format to use when drawing the status text
            sf = new StringFormat(StringFormatFlags.NoWrap)
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter
            };

            dgvCards.AutoGenerateColumns = false;
            tbcLastRevision.DefaultCellStyle.Format = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " +
                  CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern;

            cboVersion.SelectedIndex = 0;
            vCards = new VCardCollection();
            LoadGridWithVCards();
		}
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// Load the grid with the specified vCard collection
        /// </summary>
        private void LoadGridWithVCards()
        {
            int gridIdx = dgvCards.CurrentCellAddress.Y;

            // Enable or disable the buttons based on the vCard count
            miClear.Enabled = btnEdit.Enabled = btnDelete.Enabled = cboVersion.Enabled =
                btnApplyVersion.Enabled = (vCards.Count != 0);

            // This is an example of sorting a vCard collection.  Due to the variety of properties in a vCard,
            // sorting is left up to the developer utilizing a comparison delegate.  This example sorts the
            // collection by the name property taking into account the SortStringProperty if set.  Also note that
            // the collection can be sorted in the grid by clicking on the column headers.
            vCards.Sort((x, y) =>
            {
                string sortName1, sortName2;

                // Get the names to compare.  Precedence is given to the SortString property or SortAs parameter
                // as that is the purpose of their existence.
                if(x.Version != SpecificationVersions.vCard40)
                    sortName1 = x.SortString.Value;
                else
                    sortName1 = x.Name.SortAs;

                if(String.IsNullOrWhiteSpace(sortName1))
                    sortName1 = x.Name.SortableName;

                if(y.Version != SpecificationVersions.vCard40)
                    sortName2 = y.SortString.Value;
                else
                    sortName2 = y.Name.SortAs;

                if(String.IsNullOrWhiteSpace(sortName2))
                    sortName2 = y.Name.SortableName;

                // For descending order, change this to compare name 2 to name 1 instead.
                return String.Compare(sortName1, sortName2, StringComparison.CurrentCulture);
            });

            // Connect the ListChanged event so that we are notified when the list is modified
            vCards.ListChanged += vCards_ListChanged;
            wasModified = false;

            // VCardCollection is bindable so we can assign it directly as the data source.  In order to show
            // child properties in the grid, set the column's DataPropertyName to the name of the child property
            // separated from the parent property by an underscore (i.e. Name_SortableName,
            // LastRevision_DateTimeValue).
            dgvCards.DataSource = vCards;

            // Stay on the last item selected
            if(gridIdx > -1 && gridIdx < vCards.Count)
                dgvCards.CurrentCell = dgvCards[0, gridIdx];
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// This sets the modified flag when the vCard collection is edited and adjusts the button enabled states
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void vCards_ListChanged(object sender, ListChangedEventArgs e)
        {
            miClear.Enabled = btnEdit.Enabled = btnDelete.Enabled = cboVersion.Enabled =
                btnApplyVersion.Enabled = (vCards.Count != 0);

            wasModified = true;
        }

        /// <summary>
        /// Prompt to save if the collection has been modified
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void VCardBrowserForm_Closing(object sender, CancelEventArgs e)
        {
            if(wasModified && MessageBox.Show("Do you want to discard your changes to the current vCards?",
              "Discard Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
              MessageBoxDefaultButton.Button2) == DialogResult.No)
                e.Cancel = true;
        }

        /// <summary>
        /// Open a vCard file
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miOpen_Click(object sender, EventArgs e)
        {
            if(wasModified && MessageBox.Show("Do you want to discard your changes to the current vCards?",
              "Discard Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
              MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            using(OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Load vCard File";
                dlg.DefaultExt = "vcf";
                dlg.Filter = "VCF files (*.vcf)|*.vcf|All files (*.*)|*.*";
                dlg.InitialDirectory = Path.GetFullPath(Path.Combine(
                    Environment.CurrentDirectory, @"..\..\..\..\..\PDIFiles"));

                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;

                        // Parse the vCard information from the file and load the data grid with some basic
                        // information about the vCards in it.
                        vCards = VCardParser.ParseFromFile(dlg.FileName);
                        this.LoadGridWithVCards();

                        lblFilename.Text = dlg.FileName;
                    }
                    catch(Exception ex)
                    {
                        string error = $"Unable to load vCards:\n{ex.Message}";

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
        }

        /// <summary>
        /// Save a vCard file
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miSave_Click(object sender, EventArgs e)
        {
            using(SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = "Save vCard File";
                dlg.DefaultExt = "vcf";
                dlg.Filter = "VCF files (*.vcf)|*.vcf|All files (*.*)|*.*";
                dlg.InitialDirectory = Path.GetFullPath(Path.Combine(
                    Environment.CurrentDirectory, @"..\..\..\..\..\PDIFiles"));
                dlg.FileName = lblFilename.Text;

                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;

                        // Enforce UTF-8 encoding if using vCard 4.0
                        if((!miFileUnicode.Checked || !miPropUnicode.Checked) && vCards.Any(
                          c => c.Version == SpecificationVersions.vCard40))
                        {
                            this.ChangeFileEncoding_Click(miFileUnicode, e);
                        }

                        // Open the file and write the vCards to it.  We'll use the same encoding method used by
                        // the parser.
                        using(var sw = new StreamWriter(dlg.FileName, false, PDIParser.DefaultEncoding))
                        {
                            vCards.WriteToStream(sw);
                        }

                        lblFilename.Text = dlg.FileName;
                        wasModified = false;
                    }
                    catch(Exception ex)
                    {
                        string error = $"Unable to save vCards:\n{ex.Message}";

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
        }

        /// <summary>
        /// Clear all loaded vCard information
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miClear_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to remove all vCards?", "Clear vCards",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                vCards.Clear();
        }

        /// <summary>
        /// Show the About dialog box
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miAbout_Click(object sender, EventArgs e)
        {
            using(AboutDlg dlg = new AboutDlg())
            {
                dlg.ShowDialog();
            }
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
        /// Add a vCard to the collection
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using(VCardPropertiesDlg dlg = new VCardPropertiesDlg())
            {
                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    VCard newVCard = new VCard();
                    dlg.GetValues(newVCard);

                    // Create a unique ID for the new vCard
                    newVCard.UniqueId.AssignNewId(true);

                    vCards.Add(newVCard);
                }
            }
        }

        /// <summary>
        /// Edit a vCard in the collection
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(dgvCards.CurrentCellAddress.Y == -1)
            {
                MessageBox.Show("Please select a vCard to edit", "No vCard", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            using(VCardPropertiesDlg dlg = new VCardPropertiesDlg())
            {
                this.Cursor = Cursors.WaitCursor;
                dlg.SetValues(vCards[dgvCards.CurrentCellAddress.Y]);
                this.Cursor = Cursors.Default;

                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    dlg.GetValues(vCards[dgvCards.CurrentCellAddress.Y]);
                    wasModified = true;
                }
            }
        }

        /// <summary>
        /// Delete a vCard from the collection
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dgvCards.CurrentCellAddress.Y == -1)
            {
                MessageBox.Show("Please select a vCard to delete", "No vCard", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            if(MessageBox.Show("Are you sure you want to delete the selected vCard?", "Delete vCard",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            vCards.RemoveAt(dgvCards.CurrentCellAddress.Y);
        }

        /// <summary>
        /// Apply the selected version to all vCards in the collection
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        /// <remarks>Downgrading to Version 2.1 will cause the loss of any Version 3.0 or later properties if the
        /// file is saved and reloaded.  Likewise when going from 4.0 to an earlier version.</remarks>
        private void btnApplyVersion_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to apply the selected version to all vCards in the file?",
              "Apply vCard Version", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
              MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            vCards.PropagateVersion((cboVersion.SelectedIndex == 0) ?
                SpecificationVersions.vCard21 : (cboVersion.SelectedIndex == 1) ? SpecificationVersions.vCard30 :
                SpecificationVersions.vCard40);
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
        /// This is used to custom draw the version number cell
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void dgvCards_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            Color foreColor;

            if(e.RowIndex > -1 && e.ColumnIndex == 0)
            {
                var specVer = (SpecificationVersions)e.Value;
                string version = (specVer == SpecificationVersions.vCard21) ? "2.1" :
                    (specVer == SpecificationVersions.vCard30) ? "3.0" : "4.0";

                e.Paint(e.CellBounds, e.PaintParts & ~DataGridViewPaintParts.ContentForeground);

                // Based the foreground color on the selected state
                if((e.State & DataGridViewElementStates.Selected) != 0)
                    foreColor = e.CellStyle.SelectionForeColor;
                else
                    foreColor = e.CellStyle.ForeColor;

                using(SolidBrush b = new SolidBrush(foreColor))
                {
                    e.Graphics.DrawString(version, e.CellStyle.Font, b, e.CellBounds, sf);
                }

                e.Handled = true;
            }
        }

        /// <summary>
        /// Invoke edit on cell double click
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void dgvCards_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > -1)
                btnEdit_Click(sender, e);
        }
        #endregion
    }
}
