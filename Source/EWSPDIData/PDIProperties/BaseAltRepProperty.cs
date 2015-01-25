//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : BaseAltRepProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/18/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains an abstract base class used for the various iCalenar property classes that support the
// Alternate Representation (ALTREP) parameter.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/31/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.Globalization;
using System.Text;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used as a base class for the various iCalenar property classes that support the Alternate
    /// Representation (ALTREP) parameter.
    /// </summary>
    /// <remarks>This property will extract the ALTREP parameter value if present and allow access to it via the
    /// <see cref="AlternateRepresentation"/> property of this class.</remarks>
    public abstract class BaseAltRepProperty : BaseProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This property is used to set or get the alternative representation (ALTREP) parameter
        /// </summary>
        /// <value>This parameter is only applicable to iCalendar 2.0 objects.  It specifies a URI that points to
        /// an alternate representation for a textual property value.</value>
        public string AlternateRepresentation { get; set; }

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        protected BaseAltRepProperty()
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            this.AlternateRepresentation = ((BaseAltRepProperty)p).AlternateRepresentation;
            base.Clone(p);
        }

        /// <summary>
        /// This is overridden to provide custom handling of the ALTREP parameter
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        public override void SerializeParameters(StringBuilder sb)
        {
            base.SerializeParameters(sb);

            // Serialize the alternate representation if necessary.
            // It is always enclosed in quotes.
            if(this.Version == SpecificationVersions.iCalendar20 && this.AlternateRepresentation != null &&
              this.AlternateRepresentation.Length > 0)
            {
                sb.Append(';');
                sb.Append(ParameterNames.AlternateRepresentation);
                sb.Append("=\"");
                sb.Append(this.AlternateRepresentation);
                sb.Append('\"');
            }
        }

        /// <summary>
        /// This is overridden to provide custom handling of the ALTREP parameter
        /// </summary>
        /// <param name="parameters">The parameters for the property</param>
        public override void DeserializeParameters(StringCollection parameters)
        {
            if(parameters == null || parameters.Count == 0)
                return;

            for(int paramIdx = 0; paramIdx < parameters.Count; paramIdx++)
                if(String.Compare(parameters[paramIdx], "ALTREP=", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    // Remove the parameter name
                    parameters.RemoveAt(paramIdx);

                    if(paramIdx < parameters.Count)
                    {
                        this.AlternateRepresentation = parameters[paramIdx];

                        // As above, remove the value
                        parameters.RemoveAt(paramIdx);
                    }
                    break;
                }

            // Let the base class handle all other parameters
            base.DeserializeParameters(parameters);
        }
        #endregion
    }
}
