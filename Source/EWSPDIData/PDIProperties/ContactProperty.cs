//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : ContactProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/18/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Contact property.  It is used with the iCalendar Personal Data Interchange (PDI)
// classes.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/14/2004  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Contact (CONTACT) property of an iCalendar component.  This is used
    /// to represent contact information or alternately a reference to contact information associated with the
    /// calendar component.
    /// </summary>
    /// <remarks>It has no special requirements or handling.  The <see cref="BaseProperty.Value"/> property
    /// contains the contact info.</remarks>
    public class ContactProperty : BaseAltRepProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports iCalendar 2.0 only</value>
        public override SpecificationVersions VersionsSupported
        {
            get { return SpecificationVersions.iCalendar20; }
        }

        /// <summary>
        /// This read-only property defines the tag (CONTACT)
        /// </summary>
        public override string Tag
        {
            get { return "CONTACT"; }
        }

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation
        {
            get { return ValLocValue.Text; }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public ContactProperty()
        {
            this.Version = SpecificationVersions.iCalendar20;
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is overridden to allow cloning of a PDI object
        /// </summary>
        /// <returns>A clone of the object</returns>
        public override object Clone()
        {
            ContactProperty o = new ContactProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
