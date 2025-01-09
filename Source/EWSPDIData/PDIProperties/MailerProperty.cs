//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : MailerProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains Mailer property class used by the Personal Data Interchange (PDI) vCard class
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
    /// This class is used to represent the Mailer (MAILER) property of a vCard.  This specifies the type of
    /// electronic mail software that is in use by the individual associated with the vCard object.
    /// </summary>
    /// <remarks>This property has no special requirements or handling.  This property parameter is based on
    /// currently accepted practices within the Internet MIME community with the <c>X-Mailer</c> header field.
    /// This information may provide assistance to a correspondent regarding the type of data representation
    /// which can be used, and how they may be packaged.  The <see cref="BaseProperty.Value"/> property contains
    /// the mailer information.</remarks>
    public class MailerProperty : BaseProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCard 2.1 and vCard 3.0</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCard21 |
            SpecificationVersions.vCard30;

        /// <summary>
        /// This read-only property defines the tag (MAILER)
        /// </summary>
        public override string Tag => "MAILER";

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Text;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public MailerProperty()
        {
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
            MailerProperty o = new();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
