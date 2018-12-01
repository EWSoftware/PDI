//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : UrlProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the URL property class used by the Personal Data Interchange (PDI) classes such as
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
//===============================================================================================================

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Uniform Resource Locator (URL) property of a vCard, vCalendar, or
    /// iCalendar object.  This URL may be used to obtain real-time or up-to-date information about the object.
    /// </summary>
    /// <remarks><para>This property has no special requirements or handling.  The <see cref="BaseProperty.Value"/>
    /// property contains the URL.  It is not validated for correctness but should conform to the IETF RFC 1738,
    /// Uniform Resource Locators.</para>
    /// 
    /// <para>The specification does not allow for parameters on this property.  However, Microsoft Outlook uses
    /// a parameter in a non-standard fashion to classify the URL (i.e. WORK).  As such this property will allow
    /// parameters to facilitate this type of usage.</para></remarks>
    public class UrlProperty : BaseProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports all specifications except IrMC 1.1</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.Any;

        /// <summary>
        /// This read-only property defines the tag (URL)
        /// </summary>
        public override string Tag => "URL";

        /// <summary>
        /// This read-only property defines the default value type as URI
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Uri;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        public UrlProperty()
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
            UrlProperty o = new UrlProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
