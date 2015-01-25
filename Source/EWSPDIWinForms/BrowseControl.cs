//===============================================================================================================
// System  : EWSoftware.PDI Windows Forms Controls
// File    : BrowseControl.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 10/14/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Visual C#
//
// This is used to move forward and backward through a collection of objects.  It can also add objects to and
// delete objects from the collection.  Derived classes should override the virtual methods to add the required
// functionality.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 12/08/2004  EFW  Created the code
// 04/08/2007  EFW  Updated for use with .NET 2.0
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace EWSoftware.PDI.Windows.Forms
{
	/// <summary>
    /// This is used to move forward and backward through a collection of objects.  It can also add, edit, and
    /// delete objects from the collection.
	/// </summary>
    /// <remarks>Derived classes should override the virtual methods to add the required functionality</remarks>
	public partial class BrowseControl : System.Windows.Forms.UserControl
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This property is used to get a reference to the binding source
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BindingSource BindingSource
        {
            get { return bindingSource; }
        }

        /// <summary>
        /// This property is used to get a reference to the error provider control
        /// </summary>
        [Category("Misc"), Bindable(false), Description("Error provider settings")]
        public ErrorProvider ErrorProvider
        {
            get { return epErrors; }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public BrowseControl()
		{
			InitializeComponent();
		}
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// This should be overridden in derived classes to bind the controls to the data source
        /// </summary>
        /// <remarks>The default implementation does nothing</remarks>
        public virtual void BindToControls()
        {
        }

        /// <summary>
        /// This should be overridden in derived classes to enable or disable the controls based on whether or
        /// not there are items in the collection.
        /// </summary>
        /// <param name="enable">True to enable the controls, false to disable the controls</param>
        /// <remarks>The default implementation does nothing.  For simplicity, it is easiest to place the
        /// controls in a panel and then enable or disable the panel control.</remarks>
        public virtual void EnableControls(bool enable)
        {
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// This is overridden to call the bind method for the derived class
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void BrowseControl_Load(object sender, EventArgs e)
        {
            if(!this.DesignMode)
                this.BindToControls();
        }

        /// <summary>
        /// Enable or disable the panel containing the controls based on whether or not there are items in the
        /// collection.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void bindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            if(e.ListChangedType != ListChangedType.ItemChanged)
                this.EnableControls(bindingSource.Count != 0);
        }
        #endregion
    }
}
