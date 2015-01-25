//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : AttachmentsControl.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/29/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Visual C#
//
// This is used to edit a calendar object's attachment properties
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 01/05/2005  EFW  Created the code
// 04/16/2007  EFW  Converted for use with .NET 2.0
//===============================================================================================================

using System;
using System.IO;
using System.Windows.Forms;

using EWSoftware.PDI.Properties;

namespace CalendarBrowser
{
	/// <summary>
	/// This is used to edit a calendar object's attachment properties
	/// </summary>
	public partial class AttachmentsControl : System.Windows.Forms.UserControl
	{
        #region Private data members
        //=====================================================================

        private AttachPropertyCollection attach;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public AttachmentsControl()
		{
			InitializeComponent();

            attach = new AttachPropertyCollection();
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
            btnRemove.Enabled = btnClear.Enabled = (attach.Count != 0);
            lbAttachments_SelectedIndexChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Initialize the dialog controls using the specified attachments collection
        /// </summary>
        /// <param name="attachments">The attachments from which to get the settings</param>
        public void SetValues(AttachPropertyCollection attachments)
        {
            string desc;

            attach.Clear();
            attach.CloneRange(attachments);

            lbAttachments.Items.Clear();

            foreach(AttachProperty a in attach)
            {
                if(a.ValueLocation == ValLocValue.Binary)
                    desc = String.Format("Inline - {0}", a.FormatType);
                else
                    desc = String.Format("External - {0}, {1}", a.FormatType, a.Value);

                lbAttachments.Items.Add(desc);
            }

            if(attach.Count > 0)
                lbAttachments.SelectedIndex = 0;
            else
                lbAttachments.SelectedIndex = -1;

            this.SetButtonStates();
        }

        /// <summary>
        /// Update the attachments collection with the dialog control values
        /// </summary>
        /// <param name="attachments">The attachments collection to update</param>
        public void GetValues(AttachPropertyCollection attachments)
        {
            attachments.Clear();
            attachments.CloneRange(attach);
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Enable or disable the Detach button depending on whether or not the selected attachment is inline
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void lbAttachments_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDetach.Enabled = (lbAttachments.SelectedIndex != -1 &&
                attach[lbAttachments.SelectedIndex].ValueLocation == ValLocValue.Binary);
        }

        /// <summary>
        /// Pick a file using the Open File dialog box
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnLoad_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Add Attachment";
                dlg.InitialDirectory = Environment.CurrentDirectory;

                if(dlg.ShowDialog() == DialogResult.OK)
                    txtFilename.Text = dlg.FileName;
            }
        }

        /// <summary>
        /// Add an attachment
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string desc;

            if(txtFilename.Text.Trim().Length == 0)
            {
                MessageBox.Show("A filename is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtFilename.Focus();
                return;
            }

            if(txtFormat.Text.Trim().Length == 0 && chkInline.Checked)
            {
                MessageBox.Show("A format is required for inline attachments", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                txtFormat.Focus();
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;

                AttachProperty a = new AttachProperty();

                a.FormatType = (txtFormat.Text.Trim().Length == 0) ? null : txtFormat.Text;

                // If not inline, store the filename.  If inline, store the data from the file.
                if(!chkInline.Checked)
                {
                    a.ValueLocation = ValLocValue.Uri;
                    a.Value = txtFilename.Text;
                    desc = String.Format("External - {0}, {1}", a.FormatType, a.Value);
                }
                else
                {
                    using(var fs = new FileStream(txtFilename.Text, FileMode.Open, FileAccess.Read))
                    {
                        byte[] byData = new byte[fs.Length];
                        fs.Read(byData, 0, byData.Length);

                        a.ValueLocation = ValLocValue.Binary;
                        a.SetAttachmentBytes(byData);
                    }

                    desc = String.Format("Inline - {0}", a.FormatType);
                }

                attach.Add(a);
                lbAttachments.Items.Add(desc);
            }
            catch(Exception ex)
            {
                string error = String.Format("Unable to add attachment:\n{0}", ex.Message);

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

            this.SetButtonStates();
        }

        /// <summary>
        /// Remove an attachment
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if(lbAttachments.SelectedIndex == -1)
            {
                MessageBox.Show("Select an attachment to remove");
                return;
            }

            // Remove the attachment from the collection and the list box
            attach.RemoveAt(lbAttachments.SelectedIndex);
            lbAttachments.Items.RemoveAt(lbAttachments.SelectedIndex);
            this.SetButtonStates();
        }

        /// <summary>
        /// Clear all attachments
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            attach.Clear();
            lbAttachments.Items.Clear();
            this.SetButtonStates();
        }

        /// <summary>
        /// Detach an inline attachment
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnDetach_Click(object sender, EventArgs e)
        {
            using(SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = "Save Inline Attachment";
                dlg.InitialDirectory = Environment.CurrentDirectory;

                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;

                        // Open the file and write the data to it
                        using(var fs = new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write))
                        {
                            byte[] byData = attach[lbAttachments.SelectedIndex].GetAttachmentBytes();
                            fs.Write(byData, 0, byData.Length);
                        }
                    }
                    catch(Exception ex)
                    {
                        string error = String.Format("Unable to save attachment:\n{0}", ex.Message);

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
        #endregion
    }
}
