//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : BodyProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2007-2025, Eric Woodruff, All rights reserved
//
// This file contains the Body property.  It is used with the Personal Data Interchange (PDI) vNote class.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 08/19/2007  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Body (BODY) property of a vNote.  This specifies the body text of the
    /// note.
    /// </summary>
    /// <remarks>It has no special requirements or handling. The <see cref="BaseProperty.Value"/> property
    /// contains the body text.</remarks>
    public class BodyProperty : BaseProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports IrMC 1.1 only</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.IrMC11;

        /// <summary>
        /// This read-only property defines the tag (BODY)
        /// </summary>
        public override string Tag => "BODY";

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
        public BodyProperty()
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
            BodyProperty o = new();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
