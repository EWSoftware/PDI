//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : ExRuleProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/21/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Exception Rule property.  It is used with the Personal Data Interchange (PDI)
// vCalendar and iCalendar classes.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 08/31/2004  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Exception Rule (EXRULE) property of a vCalendar or iCalendar.  This
    /// defines a rule or repeating pattern for exceptions to recurring items.
    /// </summary>
    /// <remarks>The <see cref="BaseProperty.Value"/> property contains the recurrence information in string
    /// form.  The <see cref="Recurrence"/> property can be used to access it as a Recurrence object.</remarks>
    public class ExRuleProperty : RRuleProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This read-only property defines the tag (EXRULE)
        /// </summary>
        public override string Tag
        {
            get { return "EXRULE"; }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor.  Unless the version is changed, the object will conform to the iCalendar 2.0
        /// specification.
        /// </summary>
        public ExRuleProperty()
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
            ExRuleProperty o = new ExRuleProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
