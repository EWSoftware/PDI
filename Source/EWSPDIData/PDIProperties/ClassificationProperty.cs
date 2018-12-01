//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : ClassificationProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains Classification property class used by the Personal Data Interchange (PDI) classes such as
// vCalendar, iCalendar, and vCard.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/14/2004  EFW  Created the code
// 08/19/2007  EFW  Added support for vNote objects
//===============================================================================================================

using System.Text;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the access classification (CLASS) property of a vCard, vCalendar, or
    /// iCalendar object.  This specifies the access classification for an object such as public, private, or
    /// confidential.
    /// </summary>
    /// <remarks>This property has no special requirements or handling.  The <see cref="BaseProperty.Value"/>
    /// property contains the role.  This property is only valid for use with the vCard 3.0 specification, vNote,
    /// vCalendar, or iCalendar objects.</remarks>
    public class ClassificationProperty : BaseProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCard 3.0, vCalendar 1.0, iCalendar 2.0, and IrMC 1.1</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCard30 |
            SpecificationVersions.vCalendar10 | SpecificationVersions.iCalendar20 | SpecificationVersions.IrMC11;

        /// <summary>
        /// This read-only property defines the tag (CLASS)
        /// </summary>
        public override string Tag => "CLASS";

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
        public ClassificationProperty()
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
            ClassificationProperty o = new ClassificationProperty();
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
