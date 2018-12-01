//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : SortStringProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Sort String property class used by the Personal Data Interchange (PDI) vCard class
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
    /// This class is used to represent the Sort String (SORT-STRING) property of a vCard.  This specifies the
    /// family name or given name text to be used for national-language-specific sorting of the formatted name
    /// (<c>FN</c>) and name (<c>N</c>) properties.
    /// </summary>
    /// <remarks>This property has no special requirements or handling.  The <see cref="BaseProperty.Value"/>
    /// property contains the role.  This property is only valid for use with the vCard 3.0 specification.
    /// </remarks>
    public class SortStringProperty : BaseProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCard 3.0 only</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCard30;

        /// <summary>
        /// This read-only property defines the tag (SORT-STRING)
        /// </summary>
        public override string Tag => "SORT-STRING";

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Text;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        public SortStringProperty()
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
            SortStringProperty o = new SortStringProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
