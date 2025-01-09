//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : PercentCompleteProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains the Percent Complete property class used by the Personal Data Interchange (PDI) vCalendar
// and iCalendar classes.
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
    /// This class is used to represent the Percent Completed (PERCENT-COMPLETED) property of an iCalendar
    /// object.  This defines the percentage of the to-do currently completed.
    /// </summary>
    /// <remarks>This class parses the <see cref="BaseProperty.Value"/> property to allow setting and getting the
    /// value as an integer.  This property is only applicable to iCalendar objects.  It is ignored for vCalendar
    /// objects.</remarks>
    public class PercentCompleteProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private static readonly Regex reNumber = new(@"^\d*$");

        private int percentage;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports iCalendar 2.0 only</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.iCalendar20;

        /// <summary>
        /// This read-only property defines the tag (PERCENT-COMPLETE)
        /// </summary>
        public override string Tag => "PERCENT-COMPLETE";

        /// <summary>
        /// This read-only property defines the default value type as INTEGER
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Integer;

        /// <summary>
        /// This allows the percentage to be set or retrieved as an integer
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">An exception is thrown if the value is not between 0
        /// and 100.</exception>
        public int Percentage
        {
            get => percentage;
            set
            {
                if(value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException(nameof(value), value, LR.GetString("ExPercentageBadValue"));

                percentage = value;
            }
        }

        /// <summary>
        /// This property is overridden to handle converting the text value to a numeric value
        /// </summary>
        /// <value>The value should be between 0 and 100.  Instead of throwing an exception, the property will
        /// convert invalid values to zero.</value>
        public override string? Value
        {
            get
            {
                // Return null if zero
                if(percentage == 0)
                    return null;

                return percentage.ToString(CultureInfo.InvariantCulture);
            }
            set
            {
                percentage = 0;

                if(!String.IsNullOrWhiteSpace(value) && reNumber.IsMatch(value))
                {
                    percentage = Convert.ToInt32(value, CultureInfo.InvariantCulture);

                    if(percentage < 0 || percentage > 100)
                        percentage = 0;
                }
            }
        }

        /// <summary>
        /// This property is overridden to handle converting the text value to a numeric value
        /// </summary>
        public override string? EncodedValue
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
        public PercentCompleteProperty()
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
            PercentCompleteProperty o = new();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
