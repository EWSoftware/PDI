//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : SummaryProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains the Summary property class used by the Personal Data Interchange (PDI) vCalendar and
// iCalendar classes.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/31/2004  EFW  Created the code
// 08/19/2007  EFW  Added support for vNote objects
//===============================================================================================================

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Summary (SUMMARY) property of a vNote, vCalendar, or iCalendar
    /// object.  This contains a summary or the subject of the event that is shorter than the full description.
    /// </summary>
    /// <remarks>It has no special requirements or handling.  The <see cref="BaseProperty.Value"/> property
    /// contains the summary.
    /// </remarks>
    public class SummaryProperty : BaseAltRepProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCalendar 1.0, iCalendar 2.0, and IrMC 1.1</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCalendar10 |
            SpecificationVersions.iCalendar20 | SpecificationVersions.IrMC11;

        /// <summary>
        /// This read-only property defines the tag (SUMMARY)
        /// </summary>
        public override string Tag => "SUMMARY";

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Text;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Default constructor.  Unless the version is changed, the object will conform to the iCalendar 2.0
        /// specification.
        /// </summary>
        public SummaryProperty()
        {
            this.Version = SpecificationVersions.iCalendar20;
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
            SummaryProperty o = new();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
