//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : VAlarm.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/06/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the definition for the VAlarm object used by vCalendar and iCalendar objects
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/28/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.ComponentModel;
using System.IO;
using System.Text;

using EWSoftware.PDI.Binding;
using EWSoftware.PDI.Properties;

namespace EWSoftware.PDI.Objects
{
    /// <summary>
    /// This class represents a VALARM component that can appear in a calendar
    /// </summary>
    /// <remarks>This object is written as a nested object for iCalendar 2.0.  For vCalendar 1.0, it is written
    /// as a single property based on the type of alarm.  The calendar object treats them as a collection of
    /// objects regardless of the specification version used.</remarks>
    [TypeDescriptionProvider(typeof(VAlarmTypeDescriptionProvider))]
    public class VAlarm : CalendarObject
    {
        #region Private data members
        //=====================================================================

        // Single alarm properties
        private ActionProperty      action;
        private TriggerProperty     trigger;
        private RepeatProperty      repeat;
        private DurationProperty    duration;
        private SummaryProperty     summary;
        private DescriptionProperty desc;

        // Alarm property collections.  There can be one or more of each of these properties so they are stored
        // in a collection.
        private AttendeePropertyCollection attendees;
        private AttachPropertyCollection   attachments;

        // This is a catch-all that holds all unknown or extension properties
        private CustomPropertyCollection customProps;
        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCalendar 1.0 and iCalendar 2.0</value>
        public override SpecificationVersions VersionsSupported
        {
            get { return SpecificationVersions.vCalendar10 | SpecificationVersions.iCalendar20; }
        }

        /// <summary>
        /// This is used to get the Action (ACTION) property.  This is a required property and will always be
        /// set.
        /// </summary>
        public ActionProperty Action
        {
            get
            {
                if(action == null)
                    action = new ActionProperty();

                return action;
            }
        }

        /// <summary>
        /// This is used to get the Trigger (TRIGGER) property.  This is a required property and will always be
        /// set.
        /// </summary>
        public TriggerProperty Trigger
        {
            get
            {
                if(trigger == null)
                    trigger = new TriggerProperty();

                return trigger;
            }
        }

        /// <summary>
        /// This is used to get the Repeat (REPEAT) property
        /// </summary>
        public RepeatProperty Repeat
        {
            get
            {
                if(repeat == null)
                    repeat = new RepeatProperty();

                return repeat;
            }
        }

        /// <summary>
        /// This is used to get the Duration (DURATION) property
        /// </summary>
        public DurationProperty Duration
        {
            get
            {
                if(duration == null)
                    duration = new DurationProperty();

                return duration;
            }
        }

        /// <summary>
        /// This is used to get the summary (SUMMARY) property
        /// </summary>
        /// <value>This property is only applicable for e-mail alarms.  It is ignored for other alarm types.</value>
        public SummaryProperty Summary
        {
            get
            {
                if(summary == null)
                    summary = new SummaryProperty();

                return summary;
            }
        }

        /// <summary>
        /// This is used to get the description (DESCRIPTION) property
        /// </summary>
        /// <value>This property is only applicable for display, e-mail, and procedure alarms.  It is ignored for
        /// audio alarms.</value>
        public DescriptionProperty Description
        {
            get
            {
                if(desc == null)
                    desc = new DescriptionProperty();

                return desc;
            }
        }

        /// <summary>
        /// This is used to get the Attendee (ATTENDEE) properties.  There may be more than one.
        /// </summary>
        /// <value>If the returned collection is empty, there are no attendee properties for the object.  This
        /// property is only applicable for e-mail alarms.  It is ignored for other alarm types.</value>
        public AttendeePropertyCollection Attendees
        {
            get
            {
                if(attendees == null)
                    attendees = new AttendeePropertyCollection();

                return attendees;
            }
        }

        /// <summary>
        /// This is used to get the Attachment (ATTACH) properties.  There may be more than one.
        /// </summary>
        /// <value>If the returned collection is empty, there are no attachment properties for the object.  This
        /// property is only applicable for audio, e-mail, and procedure alarms.  It is ignored for display
        /// alarms.  E-mail alarms can have multiple attachments.  Audio and procedure alarms can only have one
        /// attachment.  Any additional attachments after the first will be ignored for those alarm types.
        /// </value>
        public AttachPropertyCollection Attachments
        {
            get
            {
                if(attachments == null)
                    attachments = new AttachPropertyCollection();

                return attachments;
            }
        }

        /// <summary>
        /// This is a catch-all that holds all unknown or extension properties
        /// </summary>
        /// <value>If the returned collection is empty, there are no custom properties for the calendar</value>
        public CustomPropertyCollection CustomProperties
        {
            get
            {
                if(customProps == null)
                    customProps = new CustomPropertyCollection();

                return customProps;
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor.  Unless the version is changed, the object will conform to the iCalendar 2.0
        /// specification.
        /// </summary>
        public VAlarm()
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
            VAlarm o = new VAlarm();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            VAlarm o = (VAlarm)p;

            this.ClearProperties();

            action = (ActionProperty)o.Action.Clone();
            trigger = (TriggerProperty)o.Trigger.Clone();
            repeat = (RepeatProperty)o.Repeat.Clone();
            duration = (DurationProperty)o.Duration.Clone();
            summary = (SummaryProperty)o.Summary.Clone();
            desc = (DescriptionProperty)o.Description.Clone();

            this.Attendees.CloneRange(o.Attendees);
            this.Attachments.CloneRange(o.Attachments);
            this.CustomProperties.CloneRange(o.CustomProperties);
        }

        /// <summary>
        /// The method can be called to clear all current property values from the alarm.  The version is left
        /// unchanged.
        /// </summary>
        public override void ClearProperties()
        {
            action = null;
            trigger = null;
            repeat = null;
            duration = null;
            summary = null;
            desc = null;

            attendees = null;
            attachments = null;
            customProps = null;
        }

        /// <summary>
        /// This is used to propagate the version to all properties in the object that need it
        /// </summary>
        public override void PropagateVersion()
        {
            if(action != null)
                action.Version = this.Version;

            if(trigger != null)
                trigger.Version = this.Version;

            if(repeat != null)
                repeat.Version = this.Version;

            if(duration != null)
                duration.Version = this.Version;

            if(summary != null)
                summary.Version = this.Version;

            if(desc != null)
                desc.Version = this.Version;

            if(attendees != null)
                attendees.PropagateVersion(this.Version);

            if(attachments != null)
                attachments.PropagateVersion(this.Version);

            if(customProps != null)
                customProps.PropagateVersion(this.Version);
        }

        /// <summary>
        /// This is used to get a list of time zones used by all owned objects
        /// </summary>
        /// <param name="timeZoneIds">A <see cref="StringCollection"/> that will be used to store the list of
        /// unique time zone IDs used by the calendar objects.</param>
        public override void TimeZonesUsed(StringCollection timeZoneIds)
        {
            CalendarObject.AddTimeZoneIfUsed(trigger, timeZoneIds);
        }

        /// <summary>
        /// This is used to replace an old time zone ID with a new time zone ID in all properties of a calendar
        /// object.
        /// </summary>
        /// <param name="oldId">The old ID being replaced</param>
        /// <param name="newId">The new ID to use</param>
        public override void UpdateTimeZoneId(string oldId, string newId)
        {
            CalendarObject.UpdatePropertyTimeZoneId(trigger, oldId, newId);
        }

        /// <summary>
        /// This is used to apply the selected time zone to all date/time objects in the component and convert
        /// them to the new time zone.
        /// </summary>
        /// <param name="vTimeZone">A <see cref="VTimeZone"/> object that will be used for all date/time objects
        /// in the component.</param>
        /// <remarks>When applied, all date/time values in the object will be converted to the new time zone</remarks>
        public override void ApplyTimeZone(VTimeZone vTimeZone)
        {
            CalendarObject.ApplyPropertyTimeZone(trigger, vTimeZone);
        }

        /// <summary>
        /// This is used to set the selected time zone in all date/time objects in the component without
        /// modifying the date/time values.
        /// </summary>
        /// <param name="vTimeZone">A <see cref="VTimeZone"/> object that will be used for all date/time objects
        /// in the component.</param>
        /// <remarks>This method does not affect the date/time values</remarks>
        public override void SetTimeZone(VTimeZone vTimeZone)
        {
            CalendarObject.SetPropertyTimeZone(trigger, vTimeZone);
        }

        /// <summary>
        /// This can be used to write an alarm to a PDI data stream
        /// </summary>
        /// <param name="tw">A <see cref="System.IO.TextWriter"/> derived class to which the alarm is written.</param>
        /// <param name="sb">A <see cref="System.Text.StringBuilder"/> used by the properties as a temporary
        /// buffer.  This can be null if the TextWriter is a <see cref="System.IO.StringWriter"/>.</param>
        /// <remarks>This is called by <see cref="CalendarObject.ToString"/> as well as owning objects when they
        /// convert themselves to a string or write themselves to a PDI data stream.</remarks>
        public override void WriteToStream(TextWriter tw, StringBuilder sb)
        {
            StringBuilder sbVCal = null;

            PropagateVersion();

            // If vCalendar 1.0, write in alternate format
            if(this.Version == SpecificationVersions.vCalendar10)
            {
                // If using a StringWriter, append directly to it
                if(sb == null)
                    sbVCal = ((StringWriter)tw).GetStringBuilder();
                else
                {
                    sbVCal = sb;
                    sbVCal.Length = 0;
                }

                int priorLen = sbVCal.Length;

                switch(this.Action.Action)
                {
                    case AlarmAction.Audio:
                        sbVCal.Append("AALARM");

                        if(this.Attachments.Count != 0)
                            this.Attachments[0].SerializeParameters(sbVCal);

                        sbVCal.Append(':');
                        sbVCal.Append(this.Trigger.EncodedValue);
                        sbVCal.Append(';');

                        if(duration != null)
                            sbVCal.Append(duration.EncodedValue);

                        sbVCal.Append(';');

                        if(repeat != null)
                            sbVCal.Append(repeat.EncodedValue);

                        sbVCal.Append(';');

                        if(this.Attachments.Count != 0)
                            sbVCal.Append(this.Attachments[0].EncodedValue);
                        break;

                    case AlarmAction.Display:
                        sbVCal.Append("DALARM");
                        this.Description.SerializeParameters(sbVCal);

                        sbVCal.Append(':');
                        sbVCal.Append(this.Trigger.EncodedValue);
                        sbVCal.Append(';');

                        if(duration != null)
                            sbVCal.Append(duration.EncodedValue);

                        sbVCal.Append(';');

                        if(repeat != null)
                            sbVCal.Append(repeat.EncodedValue);

                        sbVCal.Append(';');
                        sbVCal.Append(this.Description.EncodedValue);
                        break;

                    case AlarmAction.EMail:
                        sbVCal.Append("MALARM");
                        this.Description.SerializeParameters(sbVCal);

                        sbVCal.Append(':');
                        sbVCal.Append(this.Trigger.EncodedValue);

                        sbVCal.Append(';');

                        if(duration != null)
                            sbVCal.Append(duration.EncodedValue);

                        sbVCal.Append(';');

                        if(repeat != null)
                            sbVCal.Append(repeat.EncodedValue);

                        sbVCal.Append(';');

                        if(this.Attendees.Count != 0)
                            sbVCal.Append(this.Attendees[0].EncodedValue);

                        sbVCal.Append(';');
                        sbVCal.Append(this.Description.EncodedValue);
                        break;

                    case AlarmAction.Procedure:
                        sbVCal.Append("PALARM");

                        if(this.Attachments.Count != 0)
                            this.Attachments[0].SerializeParameters(sbVCal);

                        sbVCal.Append(':');
                        sbVCal.Append(this.Trigger.EncodedValue);
                        sbVCal.Append(';');

                        if(duration != null)
                            sbVCal.Append(duration.EncodedValue);

                        sbVCal.Append(';');

                        if(repeat != null)
                            sbVCal.Append(repeat.EncodedValue);

                        sbVCal.Append(';');

                        if(this.Attachments.Count != 0)
                            sbVCal.Append(this.Attachments[0].EncodedValue);
                        break;

                    default:
                        break;      // Anything else is unknown
                }

                sbVCal.Append("\r\n");

                // Insert line folds?
                if(sbVCal.Length - priorLen > 75)
                {
                    int len = 1;
                    while(priorLen < sbVCal.Length)
                    {
                        if(sbVCal[priorLen] == '\r' || sbVCal[priorLen] == '\n')
                            len = 1;
                        else
                            len++;

                        priorLen++;

                        if(len == 76 && priorLen < sbVCal.Length && sbVCal[priorLen] != '\r' &&
                          sbVCal[priorLen] != '\n')
                        {
                            // Find the first non-whitespace character that isn't '=' (possible quoted-printable)
                            // and insert the break there.
                            do
                            {
                                len--;
                                priorLen--;

                            } while(len > 0 && (Char.IsWhiteSpace(sbVCal[priorLen]) || sbVCal[priorLen] == '='));

                            if(len > 0)
                            {
                                len = 1;
                                sbVCal.Insert(priorLen + 1, "\r\n ");
                                priorLen += 3;
                            }
                            else // Couldn't find a non-whitespace char, give up
                                priorLen += 75;
                        }
                    }
                }

                if(sb != null)
                    tw.Write(sb.ToString());

                return;
            }

            tw.Write("BEGIN:VALARM\r\n");

            // Action and Trigger are required properties
            BaseProperty.WriteToStream(this.Action, sb, tw);
            BaseProperty.WriteToStream(this.Trigger, sb, tw);

            // These are optional but if one is specified, the other must be as well
            if((repeat != null && repeat.RepeatCount != 0) || (duration != null &&
              duration.DurationValue != PDI.Duration.Zero))
            {
                // Force a repeat if not specified
                if(this.Repeat.RepeatCount < 1)
                    this.Repeat.RepeatCount = 1;

                // Specify a minimum duration if one hasn't been specified
                if(this.Duration.DurationValue == PDI.Duration.Zero)
                    this.Duration.DurationValue = new Duration("PT15M");

                BaseProperty.WriteToStream(this.Repeat, sb, tw);
                BaseProperty.WriteToStream(this.Duration, sb, tw);
            }

            // The remaining properties are saved based on the action type
            switch(this.Action.Action)
            {
                case AlarmAction.Audio:
                    // Only one attachment is allowed if specified
                    if(attachments != null && attachments.Count != 0)
                        BaseProperty.WriteToStream(attachments[0], sb, tw);
                    break;

                case AlarmAction.Display:
                    BaseProperty.WriteToStream(desc, sb, tw);
                    break;

                case AlarmAction.Procedure:
                    BaseProperty.WriteToStream(desc, sb, tw);

                    // Only one attachment is allowed if specified
                    if(attachments != null && attachments.Count != 0)
                        BaseProperty.WriteToStream(attachments[0], sb, tw);
                    break;

                default:        // Email or Other
                    BaseProperty.WriteToStream(summary, sb, tw);
                    BaseProperty.WriteToStream(desc, sb, tw);

                    if(attendees != null && attendees.Count != 0)
                        foreach(AttendeeProperty a in attendees)
                            BaseProperty.WriteToStream(a, sb, tw);

                    if(attachments != null && attachments.Count != 0)
                        foreach(AttachProperty a in attachments)
                            BaseProperty.WriteToStream(a, sb, tw);
                    break;
            }

            // Add any custom properties
            if(customProps != null && customProps.Count != 0)
                foreach(CustomProperty c in customProps)
                    BaseProperty.WriteToStream(c, sb, tw);

            tw.Write("END:VALARM\r\n");
        }

        /// <summary>
        /// This is overridden to allow proper comparison of alarm objects
        /// </summary>
        /// <param name="obj">The object to which this instance is compared</param>
        /// <returns>Returns true if the object equals this instance, false if it does not</returns>
        public override bool Equals(object obj)
        {
            VAlarm a = obj as VAlarm;

            if(a == null)
                return false;

            // The ToString() method returns a text representation of the alarm based on all of its settings so
            // it's a reliable way to tell if two instances are the same.
            return (this == a || this.ToString() == a.ToString());
        }

        /// <summary>
        /// Get a hash code for the calendar object
        /// </summary>
        /// <returns>Returns the hash code for the calendar object</returns>
        /// <remarks>Since the ToString() method returns a text representation based on all of the settings, this
        /// returns the hash code for the string returned by it.</remarks>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        #endregion
    }
}
