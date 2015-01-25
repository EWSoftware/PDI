//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : ObservanceRule.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/05/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the definition for the Observance Rule object used by iCalendar STANDARD and DAYLIGHT
// objects.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 09/20/2004  EFW  Created the code
//===============================================================================================================

using System.ComponentModel;
using System.IO;
using System.Text;

using EWSoftware.PDI.Binding;
using EWSoftware.PDI.Properties;

namespace EWSoftware.PDI.Objects
{
    /// <summary>
    /// This class represents an observance rule component (STANDARD or DAYLIGHT) that can appear in a VTIMEZONE
    /// calendar component.
    /// </summary>
    [TypeDescriptionProvider(typeof(ObservanceRuleTypeDescriptionProvider))]
    public class ObservanceRule : CalendarObject
    {
        #region Private data members
        //=====================================================================

        private ObservanceRuleType ruleType;

        // Single properties
        private StartDateProperty      startDate;
        private TimeZoneOffsetProperty offsetFrom;
        private TimeZoneOffsetProperty offsetTo;
        private CommentProperty        comment;

        // Observance property collections.  There can be one or more of each of these properties so they are
        // stored in a collection.
        private RRulePropertyCollection        rRules;
        private RDatePropertyCollection        rDates;
        private TimeZoneNamePropertyCollection tzNames;

        // This is a catch-all that holds all unknown or extension properties
        private CustomPropertyCollection customProps;

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
        /// This is used to set or get the rule type
        /// </summary>
        public ObservanceRuleType RuleType
        {
            get { return ruleType; }
            set { ruleType = value; }
        }

        /// <summary>
        /// This is used to get the start date/time (DTSTART) property
        /// </summary>
        /// <value>The value is always expressed in local time for the time zone</value>
        public StartDateProperty StartDateTime
        {
            get
            {
                if(startDate == null)
                    startDate = new StartDateProperty();

                return startDate;
            }
        }

        /// <summary>
        /// This is used to get the time zone Offset From (TZOFFSETFROM) property
        /// </summary>
        /// <value>This value is used to convert the start date of the observance rule to universal time</value>
        public TimeZoneOffsetProperty OffsetFrom
        {
            get
            {
                if(offsetFrom == null)
                    offsetFrom = new TimeZoneOffsetProperty(true);

                return offsetFrom;
            }
        }

        /// <summary>
        /// This is used to get the time zone Offset To (TZOFFSETTO) property
        /// </summary>
        /// <value>This value is used to convert a universal time value to local time in the associated time zone
        /// for the observance rule.
        /// </value>
        public TimeZoneOffsetProperty OffsetTo
        {
            get
            {
                if(offsetTo == null)
                    offsetTo = new TimeZoneOffsetProperty(false);

                return offsetTo;
            }
        }


        /// <summary>
        /// This is used to get the comment (COMMENT) property
        /// </summary>
        public CommentProperty Comment
        {
            get
            {
                if(comment == null)
                    comment = new CommentProperty();

                return comment;
            }
        }

        /// <summary>
        /// This is used to get the Recurrence Rule (RRULE) properties.  There may be more than one
        /// </summary>
        /// <value>If the returned collection is empty, there are no recurrence rule properties for the object</value>
        public RRulePropertyCollection RecurrenceRules
        {
            get
            {
                if(rRules == null)
                    rRules = new RRulePropertyCollection();

                return rRules;
            }
        }

        /// <summary>
        /// This is used to get the Recur Date (RDATE) properties.  There may be more than one.
        /// </summary>
        /// <value>If the returned collection is empty, there are no recur date properties for the object.  These
        /// dates will always be expressed in local time for the time zone.</value>
        public RDatePropertyCollection RecurDates
        {
            get
            {
                if(rDates == null)
                    rDates = new RDatePropertyCollection();

                return rDates;
            }
        }

        /// <summary>
        /// This is used to get the Time Zone Name (TZNAME) properties.  There may be more than one.
        /// </summary>
        /// <value>If the returned collection is empty, there are no time zone name properties for the object</value>
        public TimeZoneNamePropertyCollection TimeZoneNames
        {
            get
            {
                if(tzNames == null)
                    tzNames = new TimeZoneNamePropertyCollection();

                return tzNames;
            }
        }

        /// <summary>
        /// This is a catch-all that holds all unknown or extension properties
        /// </summary>
        /// <value>If the returned collection is empty, there are no custom properties for the calendar
        /// </value>
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

        #region Constructors
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public ObservanceRule() : this(ObservanceRuleType.Standard)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">The type of rule that this represents</param>
        public ObservanceRule(ObservanceRuleType type)
        {
            this.Version = SpecificationVersions.iCalendar20;
            ruleType = type;
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is overridden to allow cloning of a PDI object
        /// </summary>
        /// <returns>A clone of the object.</returns>
        public override object Clone()
        {
            ObservanceRule o = new ObservanceRule();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            ObservanceRule o = (ObservanceRule)p;

            this.ClearProperties();

            ruleType = o.RuleType;
            startDate = (StartDateProperty)o.StartDateTime.Clone();
            offsetFrom = (TimeZoneOffsetProperty)o.OffsetFrom.Clone();
            offsetTo = (TimeZoneOffsetProperty)o.OffsetTo.Clone();
            comment = (CommentProperty)o.Comment.Clone();

            this.RecurrenceRules.CloneRange(o.RecurrenceRules);
            this.RecurDates.CloneRange(o.RecurDates);
            this.TimeZoneNames.CloneRange(o.TimeZoneNames);
            this.CustomProperties.CloneRange(o.CustomProperties);
        }

        /// <summary>
        /// The method can be called to clear all current property values from the observance.  The version and
        /// rule type are left unchanged.
        /// </summary>
        public override void ClearProperties()
        {
            startDate = null;
            offsetFrom = null;
            offsetTo = null;
            comment = null;

            rRules = null;
            rDates = null;
            tzNames = null;

            customProps = null;
        }

        /// <summary>
        /// This is used to propagate the version to all properties in the object that need it
        /// </summary>
        public override void PropagateVersion()
        {
            if(startDate != null)
                startDate.Version = this.Version;

            if(offsetFrom != null)
                offsetFrom.Version = this.Version;

            if(offsetTo != null)
                offsetTo.Version = this.Version;

            if(comment != null)
                comment.Version = this.Version;

            if(rRules != null)
                rRules.PropagateVersion(this.Version);

            if(rDates != null)
                rDates.PropagateVersion(this.Version);

            if(tzNames != null)
                tzNames.PropagateVersion(this.Version);

            if(customProps != null)
                customProps.PropagateVersion(this.Version);
        }

        /// <summary>
        /// The observance rule does not allow time zone IDs on its date/time objects
        /// </summary>
        /// <param name="timeZoneIds">A <see cref="StringCollection"/> that will be used to store the list of
        /// unique time zone IDs used by the calendar objects.</param>
        public override void TimeZonesUsed(StringCollection timeZoneIds)
        {
        }

        /// <summary>
        /// The observance rule does not allow time zone IDs on its date/time objects
        /// </summary>
        /// <param name="oldId">The old ID being replaced</param>
        /// <param name="newId">The new ID to use</param>
        public override void UpdateTimeZoneId(string oldId, string newId)
        {
        }

        /// <summary>
        /// The observance rule does not allow time zone IDs on its date/time objects
        /// </summary>
        /// <param name="vTimeZone">A <see cref="VTimeZone"/> object that will be used for all date/time objects
        /// in the component.</param>
        public override void ApplyTimeZone(VTimeZone vTimeZone)
        {
        }

        /// <summary>
        /// The observance rule does not allow time zone IDs on its date/time objects
        /// </summary>
        /// <param name="vTimeZone">A <see cref="VTimeZone"/> object that will be used for all date/time objects
        /// in the component.</param>
        public override void SetTimeZone(VTimeZone vTimeZone)
        {
        }

        /// <summary>
        /// This can be used to write an observance rule to a PDI data stream
        /// </summary>
        /// <param name="tw">A <see cref="System.IO.TextWriter"/> derived class to which the observance rule is
        /// written.</param>
        /// <param name="sb">A <see cref="System.Text.StringBuilder"/> used by the properties as a temporary
        /// buffer.  This can be null if the TextWriter is a <see cref="System.IO.StringWriter"/>.</param>
        /// <remarks>This is called by <see cref="CalendarObject.ToString"/> as well as owning objects when they
        /// convert themselves to a string or write themselves to a PDI data stream.</remarks>
        public override void WriteToStream(TextWriter tw, StringBuilder sb)
        {
            PropagateVersion();

            if(ruleType == ObservanceRuleType.Standard)
                tw.Write("BEGIN:STANDARD\r\n");
            else
                tw.Write("BEGIN:DAYLIGHT\r\n");

            if(startDate != null)
            {
                startDate.TimeZoneId = null;  // Never use a time zone ID
                startDate.IsFloating = true;  // Always floating
                BaseProperty.WriteToStream(startDate, sb, tw);
            }

            BaseProperty.WriteToStream(offsetFrom, sb, tw);
            BaseProperty.WriteToStream(offsetTo, sb, tw);
            BaseProperty.WriteToStream(comment, sb, tw);

            if(rRules != null && rRules.Count != 0)
                foreach(RRuleProperty r in rRules)
                    BaseProperty.WriteToStream(r, sb, tw);

            if(rDates != null && rDates.Count != 0)
                foreach(RDateProperty rdt in rDates)
                {
                    rdt.TimeZoneId = null;  // Never use a time zone ID
                    rdt.IsFloating = true;  // Always floating

                    // Periods aren't supported so force them to a date/time
                    if(rdt.ValueLocation == ValLocValue.Period)
                        rdt.ValueLocation = ValLocValue.DateTime;

                    BaseProperty.WriteToStream(rdt, sb, tw);
                }

            if(tzNames != null && tzNames.Count != 0)
                foreach(TimeZoneNameProperty tzn in tzNames)
                    BaseProperty.WriteToStream(tzn, sb, tw);

            if(customProps != null && customProps.Count != 0)
                foreach(CustomProperty c in customProps)
                    BaseProperty.WriteToStream(c, sb, tw);

            if(ruleType == ObservanceRuleType.Standard)
                tw.Write("END:STANDARD\r\n");
            else
                tw.Write("END:DAYLIGHT\r\n");
        }

        /// <summary>
        /// This is overridden to allow proper comparison of observance objects
        /// </summary>
        /// <param name="obj">The object to which this instance is compared</param>
        /// <returns>Returns true if the object equals this instance, false if it does not</returns>
        public override bool Equals(object obj)
        {
            ObservanceRule obr = obj as ObservanceRule;

            if(obr == null)
                return false;

            // The ToString() method returns a text representation of the observance based on all of its settings
            // so it's a reliable way to tell if two instances are the same.
            return (this == obr || this.ToString() == obr.ToString());
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
