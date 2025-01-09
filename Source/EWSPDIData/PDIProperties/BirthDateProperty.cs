//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : BirthDateProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains the Birth Date property class used by the Personal Data Interchange (PDI) vCard classes
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/30/2004  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Birth Date (BDAY) property of a vCard
    /// </summary>
    /// <remarks>This property class parses the <see cref="BaseProperty.Value"/> property to allow access to its
    /// content as an actual <see cref="System.DateTime"/> object.  The property value is a string conforming to
    /// the ISO 8601 calendar date, complete representation, in either basic or extended format.</remarks>
    public class BirthDateProperty : BaseDateTimeProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports all vCard specifications</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll;

        /// <summary>
        /// This read-only property defines the tag (BDAY)
        /// </summary>
        public override string Tag => "BDAY";

        /// <summary>
        /// This read-only property defines the default value type as DATE
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Date;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor.  Unless the version is changed, the object will conform to the vCard 3.0 specification.
        /// </summary>
        public BirthDateProperty()
        {
            this.Version = SpecificationVersions.vCard30;
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
            BirthDateProperty o = new();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
