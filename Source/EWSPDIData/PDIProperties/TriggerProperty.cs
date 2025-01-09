//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : AlarmProperties.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/04/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains the Trigger property used by the Personal Data Interchange (PDI) vCalendar and iCalendar
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
using System.Text;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the trigger (TRIGGER) property of an iCalendar
    /// <see cref="Objects.VAlarm"/> object.
    /// </summary>
    /// <remarks>This property class parses the <see cref="BaseProperty.Value"/> property to allow access to its
    /// content as an actual <see cref="DateTime"/> object or <see cref="Duration"/> object.  The property
    /// value is a character string conforming to ISO 8601.</remarks>
    public class TriggerProperty : BaseDateTimeProperty
    {
        #region Private data members
        //=====================================================================

        private Duration duration;

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
        /// This read-only property defines the tag (TRIGGER)
        /// </summary>
        public override string Tag => "TRIGGER";

        /// <summary>
        /// This read-only property defines the default value type as DURATION
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Duration;

        /// <summary>
        /// This property is used to set or get the related (RELATED) parameter.  If true, the trigger is related
        /// to the end time of the event.  If false, the trigger is related to the start time of the event.
        /// </summary>
        public bool RelatedToEnd { get; set; }

        /// <summary>
        /// This property is used to set or get the duration to use for the trigger.  If set, the
        /// <see cref="DateTimeValue"/> and <see cref="TimeZoneDateTime"/> properties will be
        /// ignored.
        /// </summary>
        public Duration DurationValue
        {
            get => duration;
            set
            {
                duration = value;
                this.ValueLocation = ValLocValue.Duration;
            }
        }

        /// <summary>
        /// This is overridden to set or get the date/time to use for the trigger expressed in the
        /// <see cref="BaseDateTimeProperty.TimeZoneId"/> time rather than local time on the current system.  If
        /// set, the <see cref="DurationValue"/> will be ignored.
        /// </summary>
        public override DateTime TimeZoneDateTime
        {
            get => base.TimeZoneDateTime;
            set
            {
                base.TimeZoneDateTime = value;
                this.ValueLocation = ValLocValue.DateTime;
            }
        }

        /// <summary>
        /// This is overridden to set or get the date/time to use for the trigger expressed in local time.  If
        /// set, the <see cref="DurationValue"/> will be ignored.
        /// </summary>
        public override DateTime DateTimeValue
        {
            get => base.DateTimeValue;
            set
            {
                base.DateTimeValue = value;
                this.ValueLocation = ValLocValue.DateTime;
            }
        }

        /// <summary>
        /// This property is overridden to handle converting the text value to a duration or date/time value
        /// </summary>
        public override string? Value
        {
            get
            {
                // If it's not a date/time, it's a duration.  If it has no length, it isn't saved.
                if(this.ValueLocation != ValLocValue.DateTime)
                {
                    if(this.DurationValue == Duration.Zero)
                        return null;

                    return this.DurationValue.ToString();
                }

                return base.Value;
            }
            set
            {
                if(value == null || value.Length == 0)
                    this.DurationValue = Duration.Zero;
                else
                {
                    if(this.ValueLocation == ValLocValue.DateTime || Char.IsDigit(value[0]))
                    {
                        if(this.ValueLocation == ValLocValue.Duration)
                            this.ValueLocation = ValLocValue.DateTime;

                        base.Value = value;
                    }
                    else
                        this.DurationValue = new Duration(value);
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
        /// Default constructor
        /// </summary>
        public TriggerProperty()
        {
            this.RelatedToEnd = false;
            this.DurationValue = Duration.Zero;
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
            TriggerProperty o = new();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            this.RelatedToEnd = ((TriggerProperty)p).RelatedToEnd;
            base.Clone(p);
        }

        /// <summary>
        /// This is overridden to provide custom handling of the RELATED parameter
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        public override void SerializeParameters(StringBuilder sb)
        {
            base.SerializeParameters(sb);

            // Serialize the Related parameter if necessary
            if(this.ValueLocation != ValLocValue.DateTime && this.RelatedToEnd)
            {
                sb.Append(';');
                sb.Append(ParameterNames.Related);
                sb.Append("=END");
            }
        }

        /// <summary>
        /// This is overridden to provide custom handling of the RELATED parameter
        /// </summary>
        /// <param name="parameters">The parameters for the property</param>
        public override void DeserializeParameters(StringCollection parameters)
        {
            if(parameters == null || parameters.Count == 0)
                return;

            for(int paramIdx = 0; paramIdx < parameters.Count; paramIdx++)
            {
                if(String.Compare(parameters[paramIdx], "RELATED=", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    // Remove the parameter name
                    parameters.RemoveAt(paramIdx);

                    if(paramIdx < parameters.Count)
                    {
                        this.RelatedToEnd = (String.Compare(parameters[paramIdx].Trim(), "END",
                            StringComparison.OrdinalIgnoreCase) == 0);

                        // As above, remove the value
                        parameters.RemoveAt(paramIdx);
                    }
                    break;
                }
            }

            // Let the base class handle all other parameters
            base.DeserializeParameters(parameters);
        }
        #endregion
    }
}
