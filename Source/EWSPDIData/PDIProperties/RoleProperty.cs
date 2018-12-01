//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : RoleProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Role property class used by the Personal Data Interchange (PDI) vCard class
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
    /// This class is used to represent the Role (ROLE) property of a vCard.  This specifies information
    /// concerning the role, occupation, or business category of the vCard object within an organization.
    /// </summary>
    /// <remarks>This property has no special requirements or handling.  It is based on the X.520 Business
    /// Category explanatory attribute.  The <see cref="BaseProperty.Value"/> property contains the role.</remarks>
    public class RoleProperty : BaseProperty
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
        /// This read-only property defines the tag (ROLE)
        /// </summary>
        public override string Tag => "ROLE";

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
        public RoleProperty()
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
            RoleProperty o = new RoleProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
