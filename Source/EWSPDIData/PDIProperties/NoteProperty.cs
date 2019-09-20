//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : NoteProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2019
// Note    : Copyright 2004-2019, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Note property and its collection class.  It is used with the Personal Data Interchange
// (PDI) vCard class.
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
    /// This class is used to represent the Note (NOTE) property of a vCard.  This specifies supplemental
    /// information or a comment that is associated with the vCard.
    /// </summary>
    /// <remarks>With the use of property grouping, the association can be limited to a group of properties.  It
    /// has no special requirements or handling.  The property is based on the X.520 Description attribute.  The
    /// <see cref="BaseProperty.Value"/> property contains the note.</remarks>
    public class NoteProperty : BaseProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports all vCard specifications</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll;

        /// <summary>
        /// This read-only property defines the tag (NOTE)
        /// </summary>
        public override string Tag => "NOTE";

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Text;

        /// <summary>
        /// This is overridden to enforce the correct encoding type when the version changes
        /// </summary>
        /// <remarks>vCard 2.1 defaults to Quoted-Printable.  vCard 3.0 and later uses 8-bit encoding.</remarks>
        public override SpecificationVersions Version
        {
            get => base.Version;
            set
            {
                base.Version = value;

                if(value == SpecificationVersions.vCard21)
                    this.EncodingMethod = EncodingType.QuotedPrintable;
                else
                    if(this.EncodingMethod == EncodingType.QuotedPrintable)
                        this.EncodingMethod = EncodingType.EightBit;
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public NoteProperty()
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
            NoteProperty o = new NoteProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
