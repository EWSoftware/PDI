//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : AnniversaryProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 05/17/2019
// Note    : Copyright 2019, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Anniversary property class used by the Personal Data Interchange (PDI) vCard classes
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 05/17/2019  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Anniversary (ANNIVERSARY) property of a vCard
    /// </summary>
    /// <remarks>This property class parses the <see cref="BaseProperty.Value"/> property to allow access to its
    /// content as an actual <see cref="System.DateTime"/> object.  The property value is a string conforming to
    /// the ISO 8601 calendar date, complete representation, in either basic or extended format.</remarks>
    public class AnniversaryProperty : BaseDateTimeProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports all vCard specifications</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCard40;

        /// <summary>
        /// This read-only property defines the tag (ANNIVERSARY)
        /// </summary>
        public override string Tag => "ANNIVERSARY";

        /// <summary>
        /// This read-only property defines the default value type as DATE
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Date;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public AnniversaryProperty()
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
            AnniversaryProperty o = new AnniversaryProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
