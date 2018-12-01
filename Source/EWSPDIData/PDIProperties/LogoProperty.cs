//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : LogoProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Logo property that support binary encoded images.  It is used with the Personal Data
// Interchange (PDI) vCard class.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/26/2004  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Logo (LOGO) property of a vCard
    /// </summary>
    /// <remarks>Since it also represents an image, it is derived from the <see cref="PhotoProperty"/> class and
    /// provides the same features and functionality.  Only the property tag is different.</remarks>
    public class LogoProperty : PhotoProperty
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
        /// This read-only property defines the tag (LOGO)
        /// </summary>
        public override string Tag => "LOGO";

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public LogoProperty()
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
            LogoProperty o = new LogoProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
