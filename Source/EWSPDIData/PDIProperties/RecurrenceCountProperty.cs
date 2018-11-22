//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : RecurrenceCountProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/21/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Recurrence Count property class used by the Personal Data Interchange (PDI) vCalendar
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
    /// This class is used to represent the Recurrence Count (RNUM) property of a vCalendar object.  This defines
    /// the number of times the calendar entry will reoccur.
    /// </summary>
    /// <remarks>This property is only valid for use with the vCalendar 1.0 specification.  The class parses the
    /// <see cref="BaseProperty.Value"/> property to allow setting and getting the value as an integer.</remarks>
    public class RecurrenceCountProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private static Regex reNumber = new Regex(@"^\d*$");

        private int count;
        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCalendar 1.0 only</value>
        public override SpecificationVersions VersionsSupported
        {
            get { return SpecificationVersions.vCalendar10; }
        }

        /// <summary>
        /// This read-only property defines the tag (RNUM)
        /// </summary>
        public override string Tag
        {
            get { return "RNUM"; }
        }

        /// <summary>
        /// This read-only property defines the default value type as INTEGER
        /// </summary>
        public override string DefaultValueLocation
        {
            get { return ValLocValue.Integer; }
        }

        /// <summary>
        /// This allows the recurrence count to be set or retrieved as an integer for a vCalendar object
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">An exception is thrown if the value is less than zero</exception>
        public int Count
        {
            get { return count; }
            set
            {
                if(value < 0)
                    throw new ArgumentOutOfRangeException("value", value, LR.GetString("ExRNumBadValue"));

                count = value;
            }
        }

        /// <summary>
        /// This property is overridden to handle converting the text value to a numeric value
        /// </summary>
        /// <value>Instead of throwing an exception, the property will convert non-numeric values to the default
        /// count (0).</value>
        public override string Value
        {
            get
            {
                // Not used if set to zero
                if(count == 0)
                    return null;

                return count.ToString(CultureInfo.InvariantCulture);
            }
            set
            {
                count = 0;

                if(!String.IsNullOrWhiteSpace(value) && reNumber.IsMatch(value))
                {
                    count = Convert.ToInt32(value, CultureInfo.InvariantCulture);

                    if(count < 0)
                        count = 0;
                }
            }
        }

        /// <summary>
        /// This property is overridden to handle converting the text value to a numeric value
        /// </summary>
        public override string EncodedValue
        {
            get { return this.Value; }
            set { this.Value = value; }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public RecurrenceCountProperty()
        {
            this.Version = SpecificationVersions.vCalendar10;
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
            RecurrenceCountProperty o = new RecurrenceCountProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
