//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : TimeStampProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/04/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains the time stamp property class used by the Personal Data Interchange (PDI) iCalendar class
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/30/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.Globalization;
using System.Text;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Time Stamp (DTSTAMP) property of an iCalendar object
    /// </summary>
    /// <remarks><para>This property class parses the <see cref="BaseProperty.Value"/> property to allow access
    /// to its content as an actual <see cref="DateTime"/> object.  The property value is a character
    /// string conforming to the basic format of ISO 8601.  The value is in universal time.</para>
    /// 
    /// <para>This property represents the date and time that the object was converted to vCalendar or iCalendar
    /// format and is different than the <c>CREATED</c> property which represents the time an object was created
    /// in the calendar store and the <c>LAST-MODIFIED</c> property which represents the time the object was last
    /// modified in the calendar store.  Objects containing this property should update it before writing
    /// themselves out to a data stream.</para></remarks>
    public class TimeStampProperty : BaseDateTimeProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports iCalendar 2.0 only</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.iCalendar20;

        /// <summary>
        /// This read-only property defines the tag (DTSTAMP)
        /// </summary>
        public override string Tag => "DTSTAMP";

        /// <summary>
        /// This read-only property defines the default value type as DATE-TIME
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.DateTime;

        /// <summary>
        /// This property does not allow a time zone and is always a UTC date/time value
        /// </summary>
        public override string? Value
        {
            get
            {
                DateTime dtDate = base.TimeZoneDateTime;

                // If equal to minimum date, nothing will be saved
                if(dtDate == DateTime.MinValue)
                    return null;

                return dtDate.ToUniversalTime().ToString(ISO8601Format.BasicDateTimeUniversal, CultureInfo.InvariantCulture);
            }
            set => base.Value = value;
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        public TimeStampProperty()
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
            TimeStampProperty o = new();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This property does not allow a time zone and is always a UTC date/time value
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        public override void SerializeParameters(StringBuilder sb)
        {
            this.ValueLocation = ValLocValue.DateTime;
            this.TimeZoneId = null;
            this.IsFloating = false;
            base.SerializeParameters(sb);
        }
        #endregion
    }
}
