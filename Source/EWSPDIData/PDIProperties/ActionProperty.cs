//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : ActionProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains the Action property used by the Personal Data Interchange (PDI) vCalendar and iCalendar
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

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Action (ACTION) property of a VALARM component.  This is used to
    /// indicate the type of alarm.
    /// </summary>
    /// <remarks>This class parses the <see cref="BaseProperty.Value"/> property to allow setting and getting the
    /// value as an enumerated <see cref="AlarmAction"/> value.</remarks>
    public class ActionProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private AlarmAction alarmAction;
        private string? otherAction;

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
        /// This read-only property defines the tag (ACTION)
        /// </summary>
        public override string Tag => "ACTION";

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Text;

        /// <summary>
        /// This property is used to set or get the alarm action value
        /// </summary>
        /// <value>Setting this parameter to <c>Other</c> sets the <see cref="OtherAction"/> to <c>X-UNKNOWN</c>
        /// if not already set to something else.  It is set to null if set to any other alarm action.</value>
        public AlarmAction Action
        {
            get => alarmAction;
            set
            {
                alarmAction = value;

                if(alarmAction != AlarmAction.Other)
                    otherAction = null;
                else
                {
                    if(String.IsNullOrWhiteSpace(otherAction))
                        otherAction = "X-UNKNOWN";
                }
            }
        }

        /// <summary>
        /// This property is used to set or get the alarm action string when the alarm action is set to
        /// <c>Other</c>.
        /// </summary>
        /// <value>Setting this parameter automatically sets the <see cref="AlarmAction"/> property to
        /// <c>Other</c>.</value>
        public string? OtherAction
        {
            get => otherAction;
            set
            {
                alarmAction = AlarmAction.Other;

                if(!String.IsNullOrWhiteSpace(value))
                    otherAction = value;
                else
                    otherAction = "X-UNKNOWN";
            }
        }

        /// <summary>
        /// This property is overridden to handle converting the text value to an <see cref="AlarmAction"/> value
        /// </summary>
        public override string? Value
        {
            get
            {
                if(alarmAction == AlarmAction.Other)
                    return otherAction;

                return alarmAction.ToString().ToUpperInvariant();
            }
            set
            {
                string action;

                if(value != null)
                {
                    action = value.Trim().ToUpperInvariant();
                    otherAction = null;

                    switch(action)
                    {
                        case "AUDIO":
                            alarmAction = AlarmAction.Audio;
                            break;

                        case "DISPLAY":
                            alarmAction = AlarmAction.Display;
                            break;

                        case "EMAIL":
                            alarmAction = AlarmAction.EMail;
                            break;

                        case "PROCEDURE":
                            alarmAction = AlarmAction.Procedure;
                            break;

                        default:
                            this.OtherAction = action;
                            break;
                    }
                }
                else
                    this.OtherAction = null;
            }
        }

        /// <summary>
        /// This property is overridden to handle converting the text value to an <see cref="AlarmAction"/> value
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
        public ActionProperty()
        {
            this.OtherAction = null;
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
            ActionProperty o = new();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
