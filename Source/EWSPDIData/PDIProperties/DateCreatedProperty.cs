//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : DateCreatedProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains the Date Created property class used by the Personal Data Interchange (PDI) vCalendar and
// iCalendar classes.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/30/2004  EFW  Created the code
// 08/19/2007  EFW  Added support for vNote objects
//===============================================================================================================

using System;
using System.Globalization;
using System.Text;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the DateCreated property of a vNote or vCalendar (DCREATED) or an
    /// iCalendar (CREATED) Event or To-Do object.
    /// </summary>
    /// <remarks>This property class parses the <see cref="BaseProperty.Value"/> property to allow access to its
    /// content as an actual <see cref="System.DateTime"/> object.  The property value is a character string
    /// conforming to the basic format of ISO 8601.  The value is in universal time.</remarks>
    public class DateCreatedProperty : BaseDateTimeProperty
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
        /// This read-only property defines the tag (DCREATED for vCalendar and VNOTE, CREATED for iCalendar)
        /// </summary>
        public override string Tag
        {
            get
            {
                if(this.Version != SpecificationVersions.iCalendar20)
                    return "DCREATED";

                return "CREATED";
            }
        }

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
        /// Constructor.  Unless the version is changed, the object will conform to the iCalendar 2.0
        /// specification.
        /// </summary>
        public DateCreatedProperty()
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
            DateCreatedProperty o = new();
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
