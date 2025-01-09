//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : MimeNameProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains Mime Name property class used by the Personal Data Interchange (PDI) vCard class
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

using System.Text;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the MIME name type (NAME) property of a vCard.  This specifies the
    /// displayable presentation text associated with the source for the vCard, as specified in the
    /// <see cref="MimeSourceProperty"/>.
    /// </summary>
    /// <remarks>This is for use in specifying a MIME name type and is only valid for the 3.0 specification.  Do
    /// not confuse this with the <see cref="FormattedNameProperty"/> or the <see cref="NameProperty"/> which are
    /// specific to vCards.</remarks>
    public class MimeNameProperty : BaseProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCard 3.0 only</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCard30;

        /// <summary>
        /// This read-only property defines the tag (NAME)
        /// </summary>
        public override string Tag => "NAME";

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
        public MimeNameProperty()
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
            MimeNameProperty o = new();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// The specifications do not allow parameters for this property.  Any parameters are ignored.
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        public override void SerializeParameters(StringBuilder sb)
        {
        }

        /// <summary>
        /// The specifications do not allow parameters for this property.  Any parameters are ignored.
        /// </summary>
        /// <param name="parameters">The parameters for the property</param>
        public override void DeserializeParameters(StringCollection parameters)
        {
        }
        #endregion
    }
}
