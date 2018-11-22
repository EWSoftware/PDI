//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : PhotoControl.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/27/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Visual C#
//
// This is used to edit a vCard's photo and logo information.  It's nothing elaborate but does let you edit the
// properties fairly well.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 10/31/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace vCardBrowser
{
	/// <summary>
	/// A user control for editing vCard photo and logo properties
	/// </summary>
	public partial class PhotoControl : System.Windows.Forms.UserControl
    {
        #region Private data members
        //=====================================================================

        private Bitmap bmImage;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This property is used to set or get the image filename
        /// </summary>
        /// <value>If set to a URL, an attempt is made to download the image and only the URL will be stored in
        /// the vCard when saved unless the "Save in vCard" checkbox is checked.</value>
        [DefaultValue(""), Description("The filename for the image")]
        public string ImageFilename
        {
            get { return txtFilename.Text; }
            set
            {
                Stream s = null;

                // We probably should also check that the image type is supported and set the image type combo
                // box too, but this is a demo, so I'm going to leave it for a future enhancement once I get all
                // the other demos done.
                txtFilename.Text = value;
                chkInline.Checked = false;

                this.Cursor = Cursors.WaitCursor;

                if(value != null && (value.StartsWith("http:", StringComparison.OrdinalIgnoreCase) ||
                  value.StartsWith("https:", StringComparison.OrdinalIgnoreCase) ||
                  value.StartsWith("file:", StringComparison.OrdinalIgnoreCase)))
                {
                    try
                    {
                        if(value.StartsWith("http:", StringComparison.OrdinalIgnoreCase) ||
                          value.StartsWith("https:", StringComparison.OrdinalIgnoreCase))
                        {
                            HttpWebRequest wrq = (HttpWebRequest)WebRequest.Create(new Uri(value));

                            WebResponse wrsp = wrq.GetResponse();
                            s = wrsp.GetResponseStream();
                        }
                        else
                            if(value.StartsWith("file:", StringComparison.OrdinalIgnoreCase))
                            {
                                FileWebRequest frq = (FileWebRequest)WebRequest.Create(new Uri(value));

                                WebResponse frsp = frq.GetResponse();
                                s = frsp.GetResponseStream();
                            }

                        bmImage.Dispose();
                        bmImage = new Bitmap(s);
                    }
                    catch
                    {
                        // Ignore it, just create a blank image
                        bmImage = new Bitmap(1, 1);
                    }
                    finally
                    {
                        if(s != null)
                            s.Close();
                    }
                }
                else
                {
                    try
                    {
                        bmImage.Dispose();

                        if(!String.IsNullOrWhiteSpace(value))
                            bmImage = new Bitmap(value);
                        else
                            bmImage = new Bitmap(1, 1);
                    }
                    catch
                    {
                        // Ignore it, just create a blank image
                        bmImage = new Bitmap(1, 1);
                    }
                }

                pnlPhoto.Invalidate();
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// This is used to set or get the image type
        /// </summary>
        [DefaultValue("GIF"), Description("The image type")]
        public string ImageType
        {
            get { return (string)cboImageType.SelectedItem; }
            set
            {
                int idx = (value != null) ? cboImageType.Items.IndexOf(value) : 0;

                if(idx != -1)
                    cboImageType.SelectedIndex = idx;
                else
                    cboImageType.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// This is used to set or get whether the image is stored in the vCard (true) or just a reference to
        /// it (false).
        /// </summary>
        [DefaultValue(false), Description("True to store the image in the vCard or false if it is stored externally")]
        public bool IsInline
        {
            get { return chkInline.Checked; }
            set { chkInline.Checked = value; }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public PhotoControl()
		{
            InitializeComponent();

            bmImage = new Bitmap(1, 1);
		}
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// Set the image bytes.  This is used if the image is stored
        /// as binary encoded data in the vCard.
        /// </summary>
        /// <param name="imageBytes">The image bytes</param>
        public void SetImageBytes(byte[] imageBytes)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                bmImage.Dispose();
                bmImage = new Bitmap(new MemoryStream(imageBytes));
            }
            catch
            {
                // Ignore it, just create a blank image
                bmImage = new Bitmap(1, 1);
            }
            finally
            {
                pnlPhoto.Invalidate();
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Get the image bytes.  This is used if the image is stored
        /// as binary encoded data in the vCard.
        /// </summary>
        /// <returns>The bytes for the current image</returns>
        public byte[] GetImageBytes()
        {
            using(var ms = new MemoryStream())
            {
                bmImage.Save(ms, bmImage.RawFormat);
                return ms.ToArray();
            }
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Draw the image in the panel.  Scrolling is enabled.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void pnlPhoto_Paint(object sender, PaintEventArgs e)
        {
            // Draw the appropriate portion of the image
            e.Graphics.DrawImage(bmImage, pnlPhoto.AutoScrollPosition.X, pnlPhoto.AutoScrollPosition.Y,
                bmImage.Width, bmImage.Height);
            pnlPhoto.AutoScrollMinSize = bmImage.Size;
        }

        /// <summary>
        /// Try to load an image from a file
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnLoad_Click(object sender, EventArgs e)
        {
            string extension;

            using(OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Load Image File";
                dlg.DefaultExt = "jpg";
                dlg.Filter = "Image files|*.jpg;*.gif;*.tif;*.bmp";
                dlg.InitialDirectory = Environment.CurrentDirectory;

                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    this.ImageFilename = dlg.FileName;

                    // If it loaded successfully, default to storing it inline
                    if(bmImage.Height != 1 && bmImage.Width != 1)
                    {
                        this.IsInline = true;
                        extension = Path.GetExtension(dlg.FileName).ToUpperInvariant();

                        if(extension.Length > 1 && extension[0] == '.')
                            extension = extension.Substring(1);

                        if(extension == "JPG")
                            extension = "JPEG";

                        this.ImageType = extension;
                    }
                }
            }
        }
        #endregion
    }
}
