//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : RepeatProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Repeat property used by the Personal Data Interchange (PDI) vCalendar and iCalendar
// VAlarm class.
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
using System.Text.RegularExpressions;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Repeat (REPEAT) property of a vCalendar or iCalendar alarm object.
    /// This defines the number of times the alarm will repeat.
    /// </summary>
    /// <remarks>This class parses the <see cref="BaseProperty.Value"/> property to allow setting and getting the
    /// value as an integer.</remarks>
    public class RepeatProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private static Regex reNumber = new Regex(@"^\d*$");

        private int repeatCount;

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
        /// This read-only property defines the tag (REPEAT)
        /// </summary>
        public override string Tag => "REPEAT";

        /// <summary>
        /// This read-only property defines the default value type as INTEGER
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Integer;

        /// <summary>
        /// This allows the repeat count to be set or retrieved as an integer
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">An exception is thrown if the value is less than zero</exception>
        public int RepeatCount
        {
            get => repeatCount;
            set
            {
                if(value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, LR.GetString("ExRptCountBadValue"));

                repeatCount = value;
            }
        }

        /// <summary>
        /// This property is overridden to handle converting the text value to a numeric value
        /// </summary>
        /// <value>Instead of throwing an exception, the property will convert non-numeric values to the default
        /// repeat count (0).</value>
        public override string Value
        {
            get
            {
                // Not used if set to zero
                if(repeatCount == 0)
                    return null;

                return repeatCount.ToString(CultureInfo.InvariantCulture);
            }
            set
            {
                repeatCount = 0;

                if(!String.IsNullOrWhiteSpace(value) && reNumber.IsMatch(value))
                {
                    repeatCount = Convert.ToInt32(value, CultureInfo.InvariantCulture);

                    if(repeatCount < 0)
                        repeatCount = 0;
                }
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
        public RepeatProperty()
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
            RepeatProperty o = new RepeatProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
