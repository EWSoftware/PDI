//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : PriorityProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Priority property class used by the Personal Data Interchange (PDI) vCalendar and
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
//===============================================================================================================

using System;
using System.Globalization;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Priority (PRIORITY) property of a vCalendar or iCalendar object.
    /// This defines the relative priority for a calendar component.
    /// </summary>
    /// <remarks>This class parses the <see cref="BaseProperty.Value"/> property to allow setting and getting the
    /// value as an integer.</remarks>
    public class PriorityProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private int priorityValue;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCalendar 1.0 and iCalendar 2.0</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCalendar10 |
            SpecificationVersions.iCalendar20;

        /// <summary>
        /// This read-only property defines the tag (PRIORITY)
        /// </summary>
        public override string Tag => "PRIORITY";

        /// <summary>
        /// This read-only property defines the default value type as INTEGER
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Integer;

        /// <summary>
        /// This allows the priority to be set or retrieved as an integer
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">An exception is thrown if the value is not between 0
        /// and 9.</exception>
        public int PriorityValue
        {
            get => priorityValue;
            set
            {
                if(value < 0 || value > 9)
                    throw new ArgumentOutOfRangeException(nameof(value), value, LR.GetString("ExPriorityBadValue"));

                priorityValue = value;
            }
        }

        /// <summary>
        /// This property is overridden to handle converting the text value to a numeric value
        /// </summary>
        /// <value>The value should be between 0 and 9.  Instead of throwing an exception, the property will
        /// convert invalid values to the undefined priority (0).</value>
        public override string Value
        {
            get
            {
                // Return null if undefined
                if(priorityValue == 0)
                    return null;

                return priorityValue.ToString(CultureInfo.InvariantCulture);
            }
            set
            {
                if(value != null && value.Length > 0 && value[0] >= '0' && value[0] <= '9')
                    priorityValue = (int)value[0] - 48;
                else
                    priorityValue = 0;
            }
        }

        /// <summary>
        /// This property is overridden to handle converting the text value to a numeric value
        /// </summary>
        public override string EncodedValue
        {
            get => this.Value;
            set => this.Value = value;
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public PriorityProperty()
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
            PriorityProperty o = new PriorityProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
