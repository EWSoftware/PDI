//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : MemberProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2019-2025, Eric Woodruff, All rights reserved
//
// This file contains the member property class used by the Personal Data Interchange (PDI) vCard class
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 12/20/2019  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the member (MEMBER) property of a vCard.  This specifies group membership
    /// for vCards where the <see cref="KindProperty" /> is set to <see cref="CardKind.Group" />.
    /// </summary>
    public class MemberProperty : BaseProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCard 4.0 only</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCard40;

        /// <summary>
        /// This read-only property defines the tag (MEMBER)
        /// </summary>
        public override string Tag => "MEMBER";

        /// <summary>
        /// This read-only property defines the default value type as URI
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Uri;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public MemberProperty()
        {
            this.Version = SpecificationVersions.vCard40;
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
            MemberProperty o = new();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
