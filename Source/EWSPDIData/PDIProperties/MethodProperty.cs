//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : MethodProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/21/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Method property used by the Personal Data Interchange (PDI) iCalendar classes
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

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Method (METHOD) property of an iCalendar object.  This defines the
    /// iCalendar object method associated with the calendar object.
    /// </summary>
    /// <remarks>This class parses the <see cref="BaseProperty.Value"/> property to allow setting and getting the
    /// value as an enumerated <see cref="CalendarMethod"/> value.  This property is only valid for use with the
    /// iCalendar 2.0 specification.</remarks>
    public class MethodProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private CalendarMethod calendarMethod;
        private string otherMethod;
        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports iCalendar 2.0 only</value>
        public override SpecificationVersions VersionsSupported
        {
            get { return SpecificationVersions.iCalendar20; }
        }

        /// <summary>
        /// This read-only property defines the tag (METHOD)
        /// </summary>
        public override string Tag
        {
            get { return "METHOD"; }
        }

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation
        {
            get { return ValLocValue.Text; }
        }

        /// <summary>
        /// This property is used to set or get the calendar method value
        /// </summary>
        /// <value>Setting this parameter to <c>Other</c> sets the <see cref="OtherMethod"/> to <c>X-UNKNOWN</c>
        /// if not already set to something else.  It is set to null if set to any other calendar method.</value>
        public CalendarMethod CalendarMethod
        {
            get { return calendarMethod; }
            set
            {
                calendarMethod = value;

                if(calendarMethod != CalendarMethod.Other)
                    otherMethod = null;
                else
                    if(String.IsNullOrWhiteSpace(otherMethod))
                        otherMethod = "X-UNKNOWN";
            }
        }

        /// <summary>
        /// This property is used to set or get the calendar method string when the method is set to <c>Other</c>
        /// </summary>
        /// <value>Setting this parameter automatically sets the <see cref="CalendarMethod"/> property to
        /// <c>Other</c>.</value>
        public string OtherMethod
        {
            get { return otherMethod; }
            set
            {
                calendarMethod = CalendarMethod.Other;

                if(!String.IsNullOrWhiteSpace(value))
                    otherMethod = value;
                else
                    otherMethod = "X-UNKNOWN";
            }
        }

        /// <summary>
        /// This property is overridden to handle converting the text value to a <see cref="CalendarMethod"/>
        /// value.
        /// </summary>
        public override string Value
        {
            get
            {
                string method;

                switch(calendarMethod)
                {
                    case CalendarMethod.Other:
                        method = otherMethod;
                        break;

                    case CalendarMethod.DeclineCounter:
                        method = "DECLINE-COUNTER";
                        break;

                    default:
                        method = calendarMethod.ToString().ToUpperInvariant();
                        break;
                }

                return method;
            }
            set
            {
                string method;

                if(value != null)
                {
                    method = value.Trim().ToUpperInvariant();
                    otherMethod = null;

                    switch(method)
                    {
                        case "PUBLISH":
                            calendarMethod = CalendarMethod.Publish;
                            break;

                        case "REQUEST":
                            calendarMethod = CalendarMethod.Request;
                            break;

                        case "REPLY":
                            calendarMethod = CalendarMethod.Reply;
                            break;

                        case "ADD":
                            calendarMethod = CalendarMethod.Add;
                            break;

                        case "CANCEL":
                            calendarMethod = CalendarMethod.Cancel;
                            break;

                        case "REFRESH":
                            calendarMethod = CalendarMethod.Refresh;
                            break;

                        case "COUNTER":
                            calendarMethod = CalendarMethod.Counter;
                            break;

                        case "DECLINE-COUNTER":
                            calendarMethod = CalendarMethod.DeclineCounter;
                            break;

                        default:
                            this.OtherMethod = method;
                            break;
                    }
                }
                else
                    this.CalendarMethod = CalendarMethod.None;
            }
        }

        /// <summary>
        /// This property is overridden to handle converting the text value to a <see cref="CalendarMethod"/>
        /// value.
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
        public MethodProperty()
        {
            this.Version = SpecificationVersions.iCalendar20;
            this.CalendarMethod = CalendarMethod.None;
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
            MethodProperty o = new MethodProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
