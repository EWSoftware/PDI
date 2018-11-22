//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : VCalendar.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/20/2013
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the definition for the vCalendar/iCalendar object.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/28/2004  EFW  Created the code
// 04/15/2006  EFW  Added UTC/time zone conversion methods
// 03/05/2007  EFW  Updated for use with .NET 2.0
// 08/12/2007  EFW  Fixed issues in time zone conversion methods when going between standard and daylight time
//===============================================================================================================

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

using EWSoftware.PDI.Parser;
using EWSoftware.PDI.Properties;

namespace EWSoftware.PDI.Objects
{
    /// <summary>
    /// This class is used to represent a vCalendar or iCalendar object
    /// </summary>
    /// <remarks>The <see cref="Version"/> property determines whether it is a vCalendar or an iCalendar</remarks>
    [Serializable]
    public class VCalendar : CalendarObject, ISerializable, IDisposable
    {
        #region Private data members
        //=====================================================================

        // Single calendar properties
        private ProductIdProperty prodId;

        private CalendarScaleProperty calScale;      // iCalendar only
        private MethodProperty        method;        // iCalendar only

        private GeographicPositionProperty geo;      // vCalendar only
        private TimeZoneProperty           tz;       // vCalendar only
        private DaylightProperty           daylight; // vCalendar only

        // Calendar property collections.  There can be one or more of each of these properties so they are
        // stored in a collection.
        private VEventCollection    events;
        private VToDoCollection     todos;
        private VJournalCollection  journals;        // iCalendar only
        private VFreeBusyCollection freebusy;        // iCalendar only

        // This is a catch-all that holds all unknown or extension properties
        private CustomPropertyCollection customProps;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCalendar 1.0 and iCalendar 2.0</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCalendar10 | SpecificationVersions.iCalendar20;

        /// <summary>
        /// This is used to get the Product ID (PRODID) property
        /// </summary>
        /// <value>For vCalendar 1.0 objects, this is an optional property.  For iCalendar 2.0 objects, this is a
        /// required property.  If not explicitly set, the property will contain default values.</value>
        public ProductIdProperty ProductId
        {
            get
            {
                if(prodId == null)
                    prodId = new ProductIdProperty();

                return prodId;
            }
        }

        /// <summary>
        /// This is used to get the Calendar Scale (CALSCALE) property
        /// </summary>
        /// <value>This property is only applicable to iCalendar 2.0 objects and is ignored for vCalendar
        /// objects.</value>
        public CalendarScaleProperty CalendarScale
        {
            get
            {
                if(calScale == null)
                    calScale = new CalendarScaleProperty();

                return calScale;
            }
        }

        /// <summary>
        /// This is used to get the method (METHOD) property
        /// </summary>
        /// <value>This property is only applicable to iCalendar 2.0 objects and is ignored for vCalendar
        /// objects.</value>
        public MethodProperty Method
        {
            get
            {
                if(method == null)
                    method = new MethodProperty();

                return method;
            }
        }

        /// <summary>
        /// This is used to get the vCalendar geographic position (GEO) property
        /// </summary>
        /// <value>This property is only applicable to vCalendar 1.0 objects.  For iCalendar 2.0 objects, the
        /// position is stored in the contained items such as events and to-do entries and this is ignored.</value>
        public GeographicPositionProperty VCalendarGeographicPosition
        {
            get
            {
                if(geo == null)
                    geo = new GeographicPositionProperty();

                return geo;
            }
        }

        /// <summary>
        /// This is used to get the vCalendar Time Zone (TZ) property
        /// </summary>
        /// <value>This property is only applicable to vCalendar 1.0 objects and is ignored for iCalendar
        /// objects.  For iCalendar objects, see the <see cref="TimeZones"/> property instead.</value>
        public TimeZoneProperty VCalendarTimeZone
        {
            get
            {
                if(tz == null)
                    tz = new TimeZoneProperty();

                return tz;
            }
        }

        /// <summary>
        /// This is used to get the vCalendar Daylight Saving Time rule (DAYLIGHT) property
        /// </summary>
        /// <value>This property is only applicable to vCalendar 1.0 objects and is ignored for iCalendar
        /// objects.  For iCalendar objects, see the <see cref="TimeZones"/> property instead.</value>
        public DaylightProperty VCalendarDaylightRule
        {
            get
            {
                if(daylight == null)
                    daylight = new DaylightProperty();

                return daylight;
            }
        }

        /// <summary>
        /// This is used to hold a set of time zone (VTIMEZONE) objects associated with calendars
        /// </summary>
        /// <value><para>If the returned collection is empty, there are no time zone items.  This property is
        /// only applicable to iCalendar 2.0 objects.</para>
        /// 
        /// <para>Note that all calendars share a common set of time zone information.  Any time zone components
        /// parsed are added to this collection if they do not exist or, if they do, are merged into existing
        /// entries.  This serves as a common source of time zone info.  It also saves us from having to
        /// introduce some method of letting the sub-objects like events know who their containing calendar is in
        /// order to get a reference to a time zone collection.</para>
        /// </value>
        public static VTimeZoneCollection TimeZones { get; } = new VTimeZoneCollection();

        /// <summary>
        /// This is used to hold a set of event (VEVENT) objects associated with the calendar
        /// </summary>
        /// <value>If the returned collection is empty, there are no events for the calendar</value>
        public VEventCollection Events
        {
            get
            {
                if(events == null)
                    events = new VEventCollection();

                return events;
            }
        }

        /// <summary>
        /// This is used to hold a set of to-do (VTODO) objects associated with the calendar
        /// </summary>
        /// <value>If the returned collection is empty, there are no to-do items for the calendar</value>
        public VToDoCollection ToDos
        {
            get
            {
                if(todos == null)
                    todos = new VToDoCollection();

                return todos;
            }
        }

        /// <summary>
        /// This is used to hold a set of journal (VJOURNAL) objects associated with the calendar
        /// </summary>
        /// <value>If the returned collection is empty, there are no journal items for the calendar.  This
        /// property is only applicable to iCalendar 2.0 objects.</value>
        public VJournalCollection Journals
        {
            get
            {
                if(journals == null)
                    journals = new VJournalCollection();

                return journals;
            }
        }

        /// <summary>
        /// This is used to hold a set of free/busy (VFREEBUSY) objects associated with the calendar
        /// </summary>
        /// <value>If the returned collection is empty, there are no free/busy items for the calendar.  This
        /// property is only applicable to iCalendar 2.0 objects.</value>
        public VFreeBusyCollection FreeBusys
        {
            get
            {
                if(freebusy == null)
                    freebusy = new VFreeBusyCollection();

                return freebusy;
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

        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor.  Unless the version is changed, the object will conform to the iCalendar 2.0
        /// specification.
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public VCalendar()
        {
            this.Version = SpecificationVersions.iCalendar20;

            // Subscribe to the TimeZoneIdChanged event on the Time Zones collection so that the calendar can
            // update all owned objects with changes to time zone IDs.
            TimeZones.AcquireWriterLock(250);

            try
            {
                TimeZones.TimeZoneIdChanged += tzid_TimeZoneIdChanged;
            }
            finally
            {
                TimeZones.ReleaseWriterLock();
            }
        }

        /// <summary>
        /// Deserialization constructor for use with <see cref="System.Runtime.Serialization.ISerializable"/>
        /// </summary>
        /// <param name="info">The serialization info object</param>
        /// <param name="context">The streaming context object</param>
        protected VCalendar(SerializationInfo info, StreamingContext context) : this()
        {
            if(info != null)
            {
                string calendar = (string)info.GetValue("VCALENDAR", typeof(string));

                // Parse the calendar information from the string
                VCalendarParser.ParseFromString(calendar, this);
            }
        }
        #endregion

        #region IDisposable implementation
        //=====================================================================

        /// <summary>
        /// This handles garbage collection to ensure proper disposal of the calendar if not done explicitly with
        /// <see cref="Dispose()"/>.
        /// </summary>
        ~VCalendar()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// This implements the Dispose() interface to properly dispose of the calendar object
        /// </summary>
        /// <overloads>There are two overloads for this method</overloads>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// This can be overridden by derived classes to add their own disposal code if necessary
        /// </summary>
        /// <remarks>This is implemented to ensure that the calendar disconnects from the time zone collection's
        /// <c>TimeZoneIdChanged</c> event handler when it is no longer in use.</remarks>
        /// <param name="disposing">Pass true to dispose of the managed and unmanaged resources or false to just
        /// dispose of the unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            // There are no unmanaged resources in this class.  Just disconnect the event handler if not done
            // already.
            TimeZones.AcquireWriterLock(250);

            try
            {
                TimeZones.TimeZoneIdChanged -= tzid_TimeZoneIdChanged;
            }
            finally
            {
                TimeZones.ReleaseWriterLock();
            }
        }
        #endregion

        #region ISerializable implementation
        //=====================================================================

        /// <summary>
        /// This implements the <see cref="System.Runtime.Serialization.ISerializable"/> interface and adds the
        /// appropriate members to the serialization info based on the calendar settings.
        /// </summary>
        /// <param name="info">The serialization info object</param>
        /// <param name="context">The streaming context</param>
        [SecurityCritical]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if(info != null)
                info.AddValue("VCALENDAR", this.ToString());
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
            VCalendar o = new VCalendar();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            VCalendar o = (VCalendar)p;

            this.ClearProperties();

            prodId = (ProductIdProperty)o.ProductId.Clone();
            calScale = (CalendarScaleProperty)o.CalendarScale.Clone();
            method = (MethodProperty)o.Method.Clone();
            geo = (GeographicPositionProperty)o.VCalendarGeographicPosition.Clone();
            tz = (TimeZoneProperty)o.VCalendarTimeZone.Clone();
            daylight = (DaylightProperty)o.VCalendarDaylightRule.Clone();

            this.Events.CloneRange(o.Events);
            this.ToDos.CloneRange(o.ToDos);
            this.Journals.CloneRange(o.Journals);
            this.FreeBusys.CloneRange(o.FreeBusys);
            this.CustomProperties.CloneRange(o.CustomProperties);
        }

        /// <summary>
        /// The method can be called to clear all current property values in the calendar.  The version is left
        /// unchanged.
        /// </summary>
        /// <remarks>Because time zone information is shared amongst all calendars, the <see cref="TimeZones"/>
        /// collection will not be cleared.  If you want it cleared, you must do it manually.</remarks>
        public override void ClearProperties()
        {
            prodId = null;
            calScale = null;
            method = null;
            geo = null;
            tz = null;
            daylight = null;

            events = null;
            todos = null;
            journals = null;
            freebusy = null;

            customProps = null;
        }

        /// <summary>
        /// This is used to propagate the version to all properties in the object that need it
        /// </summary>
        public override void PropagateVersion()
        {
            if(prodId != null)
                prodId.Version = this.Version;

            if(events != null)
                events.PropagateVersion(this.Version);

            if(todos != null)
                todos.PropagateVersion(this.Version);

            if(customProps != null)
                customProps.PropagateVersion(this.Version);

            if(geo != null)
                geo.Version = this.Version;

            if(this.Version == SpecificationVersions.vCalendar10)
            {
                if(tz != null)
                    tz.Version = this.Version;

                if(daylight != null)
                    daylight.Version = this.Version;
            }
            else
            {
                if(calScale!= null)
                    calScale.Version = this.Version;

                if(method != null)
                    method.Version = this.Version;

                if(journals != null)
                    journals.PropagateVersion(this.Version);

                if(freebusy != null)
                    freebusy.PropagateVersion(this.Version);

                TimeZones.PropagateVersion(this.Version);
            }
        }

        /// <summary>
        /// This is used to get a list of time zones used by all owned objects
        /// </summary>
        /// <param name="timeZoneIds">A <see cref="StringCollection"/> that will be used to store the list of
        /// unique time zone IDs used by the calendar objects.</param>
        public override void TimeZonesUsed(StringCollection timeZoneIds)
        {
            if(events != null)
                events.TimeZonesUsed(timeZoneIds);

            if(todos != null)
                todos.TimeZonesUsed(timeZoneIds);

            if(journals != null)
                journals.TimeZonesUsed(timeZoneIds);

            if(freebusy != null)
                freebusy.TimeZonesUsed(timeZoneIds);
        }

        /// <summary>
        /// This is used to replace an old time zone ID with a new time zone ID in all date/time objects in the
        /// calendar.
        /// </summary>
        /// <param name="oldId">The old ID being replaced</param>
        /// <param name="newId">The new ID to use</param>
        public override void UpdateTimeZoneId(string oldId, string newId)
        {
            if(events != null)
                events.UpdateTimeZoneId(oldId, newId);

            if(todos != null)
                todos.UpdateTimeZoneId(oldId, newId);

            if(journals != null)
                journals.UpdateTimeZoneId(oldId, newId);

            if(freebusy != null)
                freebusy.UpdateTimeZoneId(oldId, newId);
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
            if(events != null)
                events.ApplyTimeZone(vTimeZone);

            if(todos != null)
                todos.ApplyTimeZone(vTimeZone);

            if(journals != null)
                journals.ApplyTimeZone(vTimeZone);

            if(freebusy != null)
                freebusy.ApplyTimeZone(vTimeZone);
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
            if(events != null)
                events.SetTimeZone(vTimeZone);

            if(todos != null)
                todos.SetTimeZone(vTimeZone);

            if(journals != null)
                journals.SetTimeZone(vTimeZone);

            if(freebusy != null)
                freebusy.SetTimeZone(vTimeZone);
        }

        /// <summary>
        /// This can be used to write a calendar to a PDI data stream
        /// </summary>
        /// <param name="tw">A <see cref="System.IO.TextWriter"/> derived class to which the calendar is
        /// written.</param>
        /// <param name="sb">A <see cref="System.Text.StringBuilder"/> used by the properties as a temporary
        /// buffer.  This can be null if the TextWriter is a <see cref="System.IO.StringWriter"/>.</param>
        /// <remarks>This is called by <see cref="CalendarObject.ToString"/> as well as owning objects when they
        /// convert themselves to a string or write themselves to a PDI data stream.</remarks>
        public override void WriteToStream(TextWriter tw, StringBuilder sb)
        {
            PropagateVersion();

            tw.Write("BEGIN:VCALENDAR\r\n");

            if(this.Version == SpecificationVersions.vCalendar10)
                tw.Write("VERSION:1.0\r\n");
            else
                tw.Write("VERSION:2.0\r\n");

            // The product ID is required for iCalendar 2.0 so we'll enforce it for both specs
            BaseProperty.WriteToStream(this.ProductId, sb, tw);

            // Save version-specific properties
            if(this.Version == SpecificationVersions.vCalendar10)
            {
                BaseProperty.WriteToStream(daylight, sb, tw);
                BaseProperty.WriteToStream(geo, sb, tw);
                BaseProperty.WriteToStream(tz, sb, tw);
            }
            else
            {
                BaseProperty.WriteToStream(calScale, sb, tw);
                BaseProperty.WriteToStream(method, sb, tw);

                // Time zones are usually listed first.  Build a list to see which time zones are needed and
                // stream only those.
                StringCollection tzIds = new StringCollection();
                this.TimeZonesUsed(tzIds);

                if(tzIds.Count != 0)
                {
                    // Lock it while we write the collection out
                    TimeZones.AcquireReaderLock(250);

                    try
                    {
                        foreach(VTimeZone tz in TimeZones)
                            if(tzIds.Contains(tz.TimeZoneId.Value))
                                tz.WriteToStream(tw, sb);
                    }
                    finally
                    {
                        TimeZones.ReleaseReaderLock();
                    }
                }

                if(journals != null && journals.Count != 0)
                    foreach(VJournal j in journals)
                        j.WriteToStream(tw, sb);

                if(freebusy != null && freebusy.Count != 0)
                    foreach(VFreeBusy f in freebusy)
                        f.WriteToStream(tw, sb);
            }

            if(events != null && events.Count != 0)
                foreach(VEvent v in events)
                    v.WriteToStream(tw, sb);

            if(todos != null && todos.Count != 0)
                foreach(VToDo t in todos)
                    t.WriteToStream(tw, sb);

            if(customProps != null && customProps.Count != 0)
                foreach(CustomProperty c in customProps)
                    BaseProperty.WriteToStream(c, sb, tw);

            tw.Write("END:VCALENDAR\r\n");
        }

        /// <summary>
        /// This is overridden to allow proper comparison of calendar objects
        /// </summary>
        /// <param name="obj">The object to which this instance is compared</param>
        /// <returns>Returns true if the object equals this instance, false if it does not</returns>
        public override bool Equals(object obj)
        {
            if(!(obj is VCalendar c))
                return false;

            // The ToString() method returns a text representation of the calendar based on all of its settings
            // so it's a reliable way to tell if two instances are the same.
            return (this == c || this.ToString() == c.ToString());
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

        /// <summary>
        /// This is called when the <c>TimeZoneId</c> property's name changes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void tzid_TimeZoneIdChanged(object sender, TimeZoneIdChangedEventArgs e)
        {
            UpdateTimeZoneId(e.OldId, e.NewId);
        }

        /// <summary>
        /// This is used by the time zone conversion methods to find the correct observance rules to calculate
        /// the standard time and daylight saving time information.
        /// </summary>
        /// <param name="convertDate">The date being converted.</param>
        /// <param name="timeZoneId">The time zone for the conversion.</param>
        /// <param name="useLocalTime">True to return information in local time or false to return the
        /// information in the specified time zone's time.</param>
        /// <param name="standardRule">This is used to returned the standard rule.  It is null if one is not
        /// found.</param>
        /// <param name="daylightRule">This is used to returned the daylight rule.  It is null if one is not
        /// found.</param>
        /// <param name="standardDate">This is used to return the start of standard time for the returned
        /// standard rule.</param>
        /// <param name="dstDate">This is used to return the start of daylight saving time for the returned
        /// daylight rule.</param>
        private static void FindRules(DateTime convertDate, string timeZoneId, bool useLocalTime,
          out ObservanceRule standardRule, out ObservanceRule daylightRule, out DateTime standardDate,
          out DateTime dstDate)
        {
            DateTime startDate;
            DateTimeCollection dates;

            standardRule = daylightRule = null;
            standardDate = dstDate = DateTime.MinValue;

            VTimeZone vtz = TimeZones[timeZoneId];

            if(vtz == null)
                return;

            // In order to calculate the correct time, we need the UTC offset for standard time locally
            TimeZoneInfo tzCurrent = TimeZoneInfo.Local;
            var rule = tzCurrent.GetAdjustmentRules().FirstOrDefault(a => convertDate.Date >= a.DateStart &&
                convertDate.Date <= a.DateEnd);
            TimeSpan tsUTC = TimeSpan.Zero;

            if(rule != null)
                tsUTC = tzCurrent.GetUtcOffset(rule.DateEnd);

            // Get the observance rules to use in the conversion
            foreach(ObservanceRule or in vtz.ObservanceRules)
            {
                startDate = or.StartDateTime.TimeZoneDateTime;

                if(startDate.Year > convertDate.Year)
                    continue;

                // Find the first standard rule that generates a date for the date/time's year
                if(standardRule == null && or.RuleType == ObservanceRuleType.Standard)
                {
                    // Check for an RDATE that matches.
                    foreach(RDateProperty rdt in or.RecurDates)
                        if(rdt.TimeZoneDateTime.Year == convertDate.Year)
                        {
                            standardRule = or;

                            if(useLocalTime)
                                standardDate = rdt.TimeZoneDateTime.Add(or.OffsetFrom.TimeSpanValue.Negate()).ToLocalTime();
                            else
                                standardDate = rdt.TimeZoneDateTime;

                            break;
                        }

                    // If there wasn't one, check the RRULEs
                    if(standardRule == null)
                        foreach(RRuleProperty rrule in or.RecurrenceRules)
                        {
                            // Set the start date and resolve the recurrence
                            rrule.Recurrence.StartDateTime = startDate;
                            dates = rrule.Recurrence.InstancesBetween(new DateTime(convertDate.Year, 1, 1),
                                new DateTime(convertDate.Year, 12, 31, 23, 59, 59));

                            // There should only be one...
                            if(dates.Count != 0)
                            {
                                standardRule = or;

                                if(useLocalTime)
                                    standardDate = dates[0].Add(or.OffsetTo.TimeSpanValue.Negate()).Add(tsUTC);
                                else
                                    standardDate = dates[0];

                                break;
                            }
                        }

                    // If it doesn't have any RDATEs or RRULEs and the year is on or after the start date year,
                    // use the start date.
                    if(or.RecurDates.Count == 0 && or.RecurrenceRules.Count == 0 && startDate.Year <= convertDate.Year)
                    {
                        standardRule = or;

                        if(useLocalTime)
                            standardDate = startDate.Add(or.OffsetTo.TimeSpanValue.Negate()).Add(tsUTC);
                        else
                            standardDate = startDate;
                    }
                }

                // Find the first daylight rule that generates a date for the date/times' year
                if(daylightRule == null && or.RuleType == ObservanceRuleType.Daylight)
                {
                    // Check for an RDATE that matches.
                    foreach(RDateProperty rdt in or.RecurDates)
                        if(rdt.TimeZoneDateTime.Year == convertDate.Year)
                        {
                            daylightRule = or;

                            if(useLocalTime)
                                dstDate = rdt.TimeZoneDateTime.Add(or.OffsetFrom.TimeSpanValue.Negate()).ToLocalTime();
                            else
                                dstDate = rdt.TimeZoneDateTime;

                            break;
                        }

                    // If there wasn't one, check the RRULEs
                    if(daylightRule == null)
                        foreach(RRuleProperty rrule in or.RecurrenceRules)
                        {
                            // Set the start date and resolve the recurrence
                            rrule.Recurrence.StartDateTime = startDate;
                            dates = rrule.Recurrence.InstancesBetween(new DateTime(convertDate.Year, 1, 1),
                                new DateTime(convertDate.Year, 12, 31, 23, 59, 59));

                            // There should only be one...
                            if(dates.Count != 0)
                            {
                                daylightRule = or;

                                if(useLocalTime)
                                    dstDate = dates[0].Add(or.OffsetFrom.TimeSpanValue.Negate()).Add(tsUTC);
                                else
                                    dstDate = dates[0];

                                break;
                            }
                        }

                    // If it doesn't have any RDATEs or RRULEs and the year is on or after the start date year,
                    // use the start date.
                    if(or.RecurDates.Count == 0 && or.RecurrenceRules.Count == 0 && startDate.Year <= convertDate.Year)
                    {
                        daylightRule = or;

                        if(useLocalTime)
                            dstDate = startDate.Add(or.OffsetFrom.TimeSpanValue.Negate()).Add(tsUTC);
                        else
                            dstDate = startDate;
                    }
                }

                // Stop when we've got a match for each
                if(standardRule != null && daylightRule != null)
                    break;
            }
        }

        /// <summary>
        /// This method can be called to convert a date/time value from the specified time zone time to Universal
        /// Time (UTC).
        /// </summary>
        /// <param name="convertDate">The time zone date/time to convert to Universal Time.</param>
        /// <param name="timeZoneId">The time zone ID of a time zone definition in the <see cref="TimeZones"/>
        /// collection.</param>
        /// <returns>The date/time in Universal Time (UTC) if the specified time zone is found in the collection
        /// or the unmodified date/time if it cannot be found.</returns>
        public static DateTime TimeZoneTimeToUtc(DateTime convertDate, string timeZoneId)
        {

            // We won't adjust values in year 1 or year 9999 as we could underflow or overflow the date/time
            // object.
            if(convertDate.Year == 1 || convertDate.Year == 9999)
                return convertDate;

            // Get the observance rules to use in the conversion
            FindRules(convertDate, timeZoneId, false, out ObservanceRule standardRule, out ObservanceRule daylightRule,
                out DateTime standardDate, out DateTime dstDate);

            // If neither observance rule was found, use it as-is
            if(standardRule == null && daylightRule == null)
                return convertDate;

            // Standard rule only?
            if(standardRule != null && daylightRule == null)
            {
                // If on or after the standard time, use TZOFFSETTO.  Otherwise, assume it's DST and use
                // TZOFFSETFROM.
                if(convertDate >= standardDate)
                    return convertDate.Add(standardRule.OffsetTo.TimeSpanValue.Negate());

                return convertDate.Add(standardRule.OffsetFrom.TimeSpanValue.Negate());
            }

            // Daylight rule only?
            if(daylightRule != null && standardRule == null)
            {
                // If on or after the daylight time, use TZOFFSETTO.  Otherwise, assume it's standard time and
                // use TZOFFSETFROM.
                if(convertDate >= dstDate)
                    return convertDate.Add(daylightRule.OffsetTo.TimeSpanValue.Negate());

                return convertDate.Add(daylightRule.OffsetFrom.TimeSpanValue.Negate());
            }

            // Got both, see if it's between them.  In the southern hemisphere, the dates are reversed so take
            // that into account too.
            if(standardDate > dstDate)
            {
                // Northern hemisphere
                if(convertDate >= dstDate && convertDate < standardDate)
                    return convertDate.Add(daylightRule.OffsetTo.TimeSpanValue.Negate());

                return convertDate.Add(standardRule.OffsetTo.TimeSpanValue.Negate());
            }

            // Southern hemisphere
            if(convertDate >= standardDate && convertDate < dstDate)
                return convertDate.Add(standardRule.OffsetTo.TimeSpanValue.Negate());

            return convertDate.Add(daylightRule.OffsetTo.TimeSpanValue.Negate());
        }

        /// <summary>
        /// This method can be called to convert a date/time value from Universal Time (UTC) to the specified
        /// time zone time.
        /// </summary>
        /// <param name="convertDate">The universal (UTC) date/time value to convert to the time zone time.</param>
        /// <param name="timeZoneId">The time zone ID of a time zone definition in the <see cref="TimeZones"/>
        /// collection.</param>
        /// <returns>The date/time information in the time zone's time if the specified time zone is found in the
        /// collection or the unmodified date/time information if it cannot be found.</returns>
        /// <remarks>For this method, the end date/time information in the returned object is the same as the
        /// start date/time information and it has a zero length duration.</remarks>
        public static DateTimeInstance UtcToTimeZoneTime(DateTime convertDate, string timeZoneId)
        {
            TimeZoneNameProperty tzn;

            DateTimeInstance dti = new DateTimeInstance(convertDate);

            // We won't adjust values in year 1 or year 9999 as we could underflow or overflow the date/time
            // object.
            if(convertDate.Year == 1 || convertDate.Year == 9999)
                return dti;

            dti.TimeZoneId = timeZoneId;

            // Get the observance rules to use in the conversion
            FindRules(convertDate.ToLocalTime(), timeZoneId, false, out ObservanceRule standardRule,
                out ObservanceRule daylightRule, out DateTime standardDate, out DateTime dstDate);

            // If neither observance rule was found, use it as-is
            if(standardRule == null && daylightRule == null)
                return dti;

            // Keep the day part of the returned values but use the time value from the rule and shift it to UTC
            if(standardRule != null)
                standardDate = standardDate.Date.Add(standardRule.StartDateTime.DateTimeValue.TimeOfDay.Add(
                    standardRule.OffsetFrom.TimeSpanValue.Negate()));

            if(daylightRule != null)
                dstDate = dstDate.Date.Add(daylightRule.StartDateTime.DateTimeValue.TimeOfDay.Add(
                    daylightRule.OffsetFrom.TimeSpanValue.Negate()));

            // Standard rule only?
            if(standardRule != null && daylightRule == null)
            {
                // If on or after the standard time, use TZOFFSETTO.  Otherwise, assume it's DST and use
                // TZOFFSETFROM.
                if(convertDate >= standardDate)
                {
                    tzn = standardRule.TimeZoneNames[CultureInfo.CurrentCulture.Name];
                    dti.StartTimeZoneName = dti.EndTimeZoneName = (tzn == null) ? String.Empty : tzn.Value;
                    dti.StartDateTime = dti.EndDateTime = convertDate.Add(standardRule.OffsetTo.TimeSpanValue);
                }
                else
                {
                    // We don't have a time zone name for this case
                    dti.StartIsDaylightSavingTime = dti.EndIsDaylightSavingTime = true;
                    dti.StartDateTime = dti.EndDateTime = convertDate.Add(standardRule.OffsetFrom.TimeSpanValue);
                }

                return dti;
            }

            // Daylight rule only?
            if(daylightRule != null && standardRule == null)
            {
                // If on or after the daylight time, use TZOFFSETTO.  Otherwise, assume it's standard time and
                // use TZOFFSETFROM.
                if(convertDate >= dstDate)
                {
                    dti.StartIsDaylightSavingTime = dti.EndIsDaylightSavingTime = true;
                    tzn = daylightRule.TimeZoneNames[CultureInfo.CurrentCulture.Name];
                    dti.StartTimeZoneName = dti.EndTimeZoneName = (tzn == null) ? String.Empty : tzn.Value;
                    dti.StartDateTime = dti.EndDateTime = convertDate.Add(daylightRule.OffsetTo.TimeSpanValue);
                }
                else
                {
                    // We don't have a time zone name for this case
                    dti.StartDateTime = dti.EndDateTime = convertDate.Add(daylightRule.OffsetFrom.TimeSpanValue);
                }

                return dti;
            }

            // Got both, see if it's between them.  In the southern hemisphere, the dates are reversed so take
            // that into account.
            if(standardDate > dstDate)
            {
                // Northern hemisphere
                if(convertDate >= dstDate && convertDate < standardDate)
                {
                    dti.StartIsDaylightSavingTime = dti.EndIsDaylightSavingTime = true;
                    tzn = daylightRule.TimeZoneNames[CultureInfo.CurrentCulture.Name];
                    dti.StartTimeZoneName = dti.EndTimeZoneName = (tzn == null) ? String.Empty : tzn.Value;
                    dti.StartDateTime = dti.EndDateTime = convertDate.Add(daylightRule.OffsetTo.TimeSpanValue);
                }
                else
                {
                    tzn = standardRule.TimeZoneNames[CultureInfo.CurrentCulture.Name];
                    dti.StartTimeZoneName =  dti.EndTimeZoneName = (tzn == null) ? String.Empty : tzn.Value;
                    dti.StartDateTime = dti.EndDateTime = convertDate.Add(standardRule.OffsetTo.TimeSpanValue);
                }
            }
            else    // Southern hemisphere
                if(convertDate >= standardDate && convertDate < dstDate)
                {
                    tzn = standardRule.TimeZoneNames[CultureInfo.CurrentCulture.Name];
                    dti.StartTimeZoneName = dti.EndTimeZoneName = (tzn == null) ? String.Empty : tzn.Value;
                    dti.StartDateTime = dti.EndDateTime = convertDate.Add(standardRule.OffsetTo.TimeSpanValue);
                }
                else
                {
                    dti.StartIsDaylightSavingTime = dti.EndIsDaylightSavingTime = true;
                    tzn = daylightRule.TimeZoneNames[CultureInfo.CurrentCulture.Name];
                    dti.StartTimeZoneName = dti.EndTimeZoneName = (tzn == null) ? String.Empty : tzn.Value;
                    dti.StartDateTime = dti.EndDateTime = convertDate.Add(daylightRule.OffsetTo.TimeSpanValue);
                }

            return dti;
        }

        /// <summary>
        /// This method can be called to convert a date/time value from the specified time zone time to local
        /// time.
        /// </summary>
        /// <param name="convertDate">The time zone date/time to convert to local time.</param>
        /// <param name="timeZoneId">The time zone ID of a time zone definition in the <see cref="TimeZones"/>
        /// collection.</param>
        /// <returns>The date/time information in local time if the specified time zone is found in the
        /// collection or the unmodified date/time information if it cannot be found.</returns>
        /// <remarks>For this method, the end date/time information in the returned object is the same as the
        /// start date/time information and it has a zero length duration.</remarks>
        public static DateTimeInstance TimeZoneTimeToLocalTime(DateTime convertDate, string timeZoneId)
        {
            DateTimeInstance dti = new DateTimeInstance(convertDate);

            // We won't adjust values in year 1 or year 9999 as we could underflow or overflow the date/time
            // object.
            if(convertDate.Year == 1 || convertDate.Year == 9999)
                return dti;

            // Get the observance rules to use in the conversion
            FindRules(convertDate, timeZoneId, false, out ObservanceRule standardRule,
                out ObservanceRule daylightRule, out DateTime standardDate, out DateTime daylightDate);

            // If neither observance rule was found, use it as-is
            if(standardRule == null && daylightRule == null)
                return dti;

            // Standard rule only?
            if(standardRule != null && daylightRule == null)
            {
                // If on or after the standard time, use TZOFFSETTO.  Otherwise, assume it's DST and use
                // TZOFFSETFROM.
                if(convertDate >= standardDate)
                {
                    dti.StartDateTime = dti.EndDateTime = convertDate.Add(
                        standardRule.OffsetTo.TimeSpanValue.Negate()).ToLocalTime();
                }
                else
                    dti.StartDateTime = dti.EndDateTime = convertDate.Add(
                        standardRule.OffsetFrom.TimeSpanValue.Negate()).ToLocalTime();

                // Base the time zone name on the local time's DST setting
                if(TimeZoneInfo.Local.IsDaylightSavingTime(dti.StartDateTime))
                {
                    dti.StartIsDaylightSavingTime = dti.EndIsDaylightSavingTime = true;
                    dti.StartTimeZoneName = dti.EndTimeZoneName = TimeZoneInfo.Local.DaylightName;
                }
                else
                    dti.StartTimeZoneName = dti.EndTimeZoneName = TimeZoneInfo.Local.StandardName;

                return dti;
            }

            // Daylight rule only?
            if(daylightRule != null && standardRule == null)
            {
                // If on or after the daylight time, use TZOFFSETTO.  Otherwise, assume it's standard time and
                // use TZOFFSETFROM.
                if(convertDate >= daylightDate)
                {
                    // If in the missing hour, bump it forward an hour
                    if(convertDate.Date == daylightDate.Date && convertDate.Hour == daylightDate.Hour)
                        convertDate = convertDate.AddHours(1);

                    dti.StartDateTime = dti.EndDateTime = convertDate.Add(
                        daylightRule.OffsetTo.TimeSpanValue.Negate()).ToLocalTime();
                }
                else
                    dti.StartDateTime = dti.EndDateTime = convertDate.Add(
                        daylightRule.OffsetFrom.TimeSpanValue.Negate()).ToLocalTime();

                // Base the time zone name on the local time's DST setting
                if(TimeZoneInfo.Local.IsDaylightSavingTime(dti.StartDateTime))
                {
                    dti.StartIsDaylightSavingTime = dti.EndIsDaylightSavingTime = true;
                    dti.StartTimeZoneName = dti.EndTimeZoneName = TimeZoneInfo.Local.DaylightName;
                }
                else
                    dti.StartTimeZoneName = dti.EndTimeZoneName = TimeZoneInfo.Local.StandardName;

                return dti;
            }

            // Got both, see if it's between them.  In the southern hemisphere, the dates are reversed so take
            // that into account too.
            if(standardDate > daylightDate)
            {
                // Northern hemisphere
                if(convertDate >= daylightDate && convertDate < standardDate)
                {
                    // If in the missing hour, bump it forward an hour
                    if(convertDate.Date == daylightDate.Date && convertDate.Hour == daylightDate.Hour)
                        convertDate = convertDate.AddHours(1);

                    dti.StartDateTime = dti.EndDateTime = convertDate.Add(
                        daylightRule.OffsetTo.TimeSpanValue.Negate()).ToLocalTime();
                }
                else
                    dti.StartDateTime = dti.EndDateTime = convertDate.Add(
                        standardRule.OffsetTo.TimeSpanValue.Negate()).ToLocalTime();
            }
            else    // Southern hemisphere
                if(convertDate >= standardDate && convertDate < daylightDate)
                {
                    dti.StartDateTime = dti.EndDateTime = convertDate.Add(
                        standardRule.OffsetTo.TimeSpanValue.Negate()).ToLocalTime();
                }
                else
                {
                    // If in the missing hour, bump it forward an hour
                    if(convertDate.Date == daylightDate.Date && convertDate.Hour == daylightDate.Hour)
                        convertDate = convertDate.AddHours(1);

                    dti.StartDateTime = dti.EndDateTime = convertDate.Add(
                        daylightRule.OffsetTo.TimeSpanValue.Negate()).ToLocalTime();
                }

            // Base the time zone name on the local time's DST setting
            if(TimeZoneInfo.Local.IsDaylightSavingTime(dti.StartDateTime))
            {
                dti.StartIsDaylightSavingTime = dti.EndIsDaylightSavingTime = true;
                dti.StartTimeZoneName = dti.EndTimeZoneName = TimeZoneInfo.Local.DaylightName;
            }
            else
                dti.StartTimeZoneName = dti.EndTimeZoneName = TimeZoneInfo.Local.StandardName;

            return dti;
        }

        /// <summary>
        /// This method can be called to convert a date/time value from local time to the specified time zone
        /// time.
        /// </summary>
        /// <param name="convertDate">The local date/time to convert to the time zone time.</param>
        /// <param name="timeZoneId">The time zone ID of a time zone definition in the <see cref="TimeZones"/>
        /// collection.</param>
        /// <returns>The date/time information in the time zone's time if the specified time zone is found in the
        /// collection or the unmodified date/time information if it cannot be found.</returns>
        /// <remarks>For this method, the end date/time information in the returned object is the same as the
        /// start date/time information and it has a zero length duration.</remarks>
        public static DateTimeInstance LocalTimeToTimeZoneTime(DateTime convertDate, string timeZoneId)
        {
            TimeZoneNameProperty tzn;

            DateTimeInstance dti = new DateTimeInstance(convertDate);

            // We won't adjust values in year 1 or year 9999 as we could underflow or overflow the date/time
            // object
            if(convertDate.Year == 1 || convertDate.Year == 9999)
                return dti;

            dti.TimeZoneId = timeZoneId;

            // Get the observance rules to use in the conversion
            FindRules(convertDate, timeZoneId, true, out ObservanceRule standardRule, out ObservanceRule daylightRule,
                out DateTime standardDate, out DateTime daylightDate);

            // If neither observance rule was found, use it as-is
            if(standardRule == null && daylightRule == null)
                return dti;

            // Standard rule only?
            if(standardRule != null && daylightRule == null)
            {
                // If on or after the standard time, use TZOFFSETTO.  Otherwise, assume it's DST and use
                // TZOFFSETFROM.
                if(convertDate >= standardDate)
                {
                    tzn = standardRule.TimeZoneNames[CultureInfo.CurrentCulture.Name];
                    dti.StartTimeZoneName = dti.EndTimeZoneName = (tzn == null) ? String.Empty : tzn.Value;
                    dti.StartDateTime = dti.EndDateTime = convertDate.ToUniversalTime().Add(standardRule.OffsetTo.TimeSpanValue);
                }
                else
                {
                    // We don't have a time zone name for this case
                    dti.StartIsDaylightSavingTime = dti.EndIsDaylightSavingTime = true;
                    dti.StartDateTime = dti.EndDateTime = convertDate.ToUniversalTime().Add(standardRule.OffsetFrom.TimeSpanValue);
                }

                return dti;
            }

            // Daylight rule only?
            if(daylightRule != null && standardRule == null)
            {
                // If on or after the daylight time, use TZOFFSETTO.  Otherwise, assume it's standard time and
                // use TZOFFSETFROM.
                if(convertDate >= daylightDate)
                {
                    // No need to adjust if in the missing hour as the conversion to UTC will fix it up
                    dti.StartIsDaylightSavingTime = dti.EndIsDaylightSavingTime = true;
                    tzn = daylightRule.TimeZoneNames[CultureInfo.CurrentCulture.Name];
                    dti.StartTimeZoneName = dti.EndTimeZoneName = (tzn == null) ? String.Empty : tzn.Value;
                    dti.StartDateTime = dti.EndDateTime = convertDate.ToUniversalTime().Add(daylightRule.OffsetTo.TimeSpanValue);
                }
                else
                {
                    // We don't have a time zone name for this case
                    dti.StartDateTime = dti.EndDateTime = convertDate.ToUniversalTime().Add(daylightRule.OffsetFrom.TimeSpanValue);
                }

                return dti;
            }

            // Got both, see if it's between them.  In the southern hemisphere, the dates are reversed so take
            // that into account.
            if(standardDate > daylightDate)
            {
                // Northern hemisphere
                if(convertDate >= daylightDate && convertDate < standardDate)
                {
                    // No need to adjust if in the missing hour as the conversion to UTC will fix it up
                    dti.StartIsDaylightSavingTime = dti.EndIsDaylightSavingTime = true;
                    tzn = daylightRule.TimeZoneNames[CultureInfo.CurrentCulture.Name];
                    dti.StartTimeZoneName = dti.EndTimeZoneName = (tzn == null) ? String.Empty : tzn.Value;
                    dti.StartDateTime = dti.EndDateTime = convertDate.ToUniversalTime().Add(daylightRule.OffsetTo.TimeSpanValue);
                }
                else
                {
                    tzn = standardRule.TimeZoneNames[CultureInfo.CurrentCulture.Name];
                    dti.StartTimeZoneName =  dti.EndTimeZoneName = (tzn == null) ? String.Empty : tzn.Value;
                    dti.StartDateTime = dti.EndDateTime = convertDate.ToUniversalTime().Add(standardRule.OffsetTo.TimeSpanValue);
                }
            }
            else    // Southern hemisphere
                if(convertDate >= standardDate && convertDate < daylightDate)
                {
                    tzn = standardRule.TimeZoneNames[CultureInfo.CurrentCulture.Name];
                    dti.StartTimeZoneName = dti.EndTimeZoneName = (tzn == null) ? String.Empty : tzn.Value;
                    dti.StartDateTime = dti.EndDateTime = convertDate.ToUniversalTime().Add(standardRule.OffsetTo.TimeSpanValue);
                }
                else
                {
                    // No need to adjust if in the missing hour as the conversion to UTC will fix it up
                    dti.StartIsDaylightSavingTime = dti.EndIsDaylightSavingTime = true;
                    tzn = daylightRule.TimeZoneNames[CultureInfo.CurrentCulture.Name];
                    dti.StartTimeZoneName = dti.EndTimeZoneName = (tzn == null) ? String.Empty : tzn.Value;
                    dti.StartDateTime = dti.EndDateTime = convertDate.ToUniversalTime().Add(daylightRule.OffsetTo.TimeSpanValue);
                }

            return dti;
        }

        /// <summary>
        /// This method can be called to convert a date/time value from one time zone time to the another time
        /// zone time.
        /// </summary>
        /// <param name="convertDate">The date/time in the source time zone to convert to the destination time
        /// zone</param>
        /// <param name="sourceId">The time zone ID of a time zone definition in the <see cref="TimeZones"/>
        /// collection that represents the date/time's current time zone.</param>
        /// <param name="destId">The time zone ID of a time zone definition in the <see cref="TimeZones"/>
        /// collection that represents the destination time zone.</param>
        /// <returns>The time zone date/time information in the destination time zone's time if the specified
        /// time zones are found in the collection or the unmodified date/time information if one or both cannot
        /// be found.</returns>
        /// <remarks>For this method, the end date/time information in the returned object is the same as the
        /// start date/time information and it has a zero length duration.</remarks>
        public static DateTimeInstance TimeZoneToTimeZone(DateTime convertDate, string sourceId, string destId)
        {
            DateTimeInstance dti;

            // If one or both is not found, return it as-is
            if(TimeZones[sourceId] == null || TimeZones[destId] == null)
                dti = new DateTimeInstance(convertDate);
            else
            {
                // Convert it from the source time zone to UTC and then from UTC to the destination time zone
                dti = VCalendar.UtcToTimeZoneTime(VCalendar.TimeZoneTimeToUtc(convertDate, sourceId), destId);
            }

            return dti;
        }

        /// <summary>
        /// This method can be called to get daylight saving time and time zone name information for a specified
        /// date/time.
        /// </summary>
        /// <param name="infoDate">The time zone date/time for which to get information.</param>
        /// <param name="timeZoneId">The time zone ID of a time zone definition in the <see cref="TimeZones"/>
        /// collection.</param>
        /// <returns>The date/time information if the specified time zone is found in the collection or the
        /// unmodified date/time information if it cannot be found.</returns>
        /// <remarks>For this method, the end date/time information in the returned object is the same as the
        /// start date/time information and it has a zero length duration.</remarks>
        public static DateTimeInstance TimeZoneTimeInfo(DateTime infoDate, string timeZoneId)
        {
            TimeZoneNameProperty tzn;

            DateTimeInstance dti = new DateTimeInstance(infoDate);

            // We won't adjust values in year 1 or year 9999 as we could underflow or overflow the date/time
            // object.
            if(infoDate.Year == 1 || infoDate.Year == 9999)
                return dti;

            dti.TimeZoneId = timeZoneId;

            // Get the observance rules to use in the conversion
            FindRules(infoDate, timeZoneId, false, out ObservanceRule standardRule, out ObservanceRule daylightRule,
                out DateTime standardDate, out DateTime dstDate);

            // If neither observance rule was found, use it as-is
            if(standardRule == null && daylightRule == null)
                return dti;

            // Standard rule only?
            if(standardRule != null && daylightRule == null)
            {
                if(infoDate >= standardDate)
                {
                    tzn = standardRule.TimeZoneNames[CultureInfo.CurrentCulture.Name];
                    dti.StartTimeZoneName = dti.EndTimeZoneName = (tzn == null) ? String.Empty : tzn.Value;
                }
                else    // We don't have a time zone name for this case
                    dti.StartIsDaylightSavingTime = dti.EndIsDaylightSavingTime = true;

                return dti;
            }

            // Daylight rule only?
            if(daylightRule != null && standardRule == null)
            {
                // We don't have a time zone name if not DST
                if(infoDate >= dstDate)
                {
                    // If in the missing hour, bump it forward an hour
                    if(infoDate.Date == dstDate.Date && infoDate.Hour == dstDate.Hour)
                        dti.StartDateTime = dti.EndDateTime = infoDate.AddHours(1);

                    dti.StartIsDaylightSavingTime = dti.EndIsDaylightSavingTime = true;
                    tzn = daylightRule.TimeZoneNames[CultureInfo.CurrentCulture.Name];
                    dti.StartTimeZoneName = dti.EndTimeZoneName = (tzn == null) ? String.Empty : tzn.Value;
                }

                return dti;
            }

            // Got both, see if it's between them.  In the southern hemisphere, the dates are reversed so take
            // that into account.
            if(standardDate > dstDate)
            {
                // Northern hemisphere
                if(infoDate >= dstDate && infoDate < standardDate)
                {
                    // If in the missing hour, bump it forward an hour
                    if(infoDate.Date == dstDate.Date && infoDate.Hour == dstDate.Hour)
                        dti.StartDateTime = dti.EndDateTime = infoDate.AddHours(1);

                    dti.StartIsDaylightSavingTime = dti.EndIsDaylightSavingTime = true;
                    tzn = daylightRule.TimeZoneNames[CultureInfo.CurrentCulture.Name];
                    dti.StartTimeZoneName = dti.EndTimeZoneName = (tzn == null) ? String.Empty : tzn.Value;
                }
                else
                {
                    tzn = standardRule.TimeZoneNames[CultureInfo.CurrentCulture.Name];
                    dti.StartTimeZoneName =  dti.EndTimeZoneName = (tzn == null) ? String.Empty : tzn.Value;
                }
            }
            else    // Southern hemisphere
                if(infoDate >= standardDate && infoDate < dstDate)
                {
                    tzn = standardRule.TimeZoneNames[CultureInfo.CurrentCulture.Name];
                    dti.StartTimeZoneName = dti.EndTimeZoneName = (tzn == null) ? String.Empty : tzn.Value;
                }
                else
                {
                    // If in the missing hour, bump it forward an hour
                    if(infoDate.Date == dstDate.Date && infoDate.Hour == dstDate.Hour)
                        dti.StartDateTime = dti.EndDateTime = infoDate.AddHours(1);

                    dti.StartIsDaylightSavingTime = dti.EndIsDaylightSavingTime = true;
                    tzn = daylightRule.TimeZoneNames[CultureInfo.CurrentCulture.Name];
                    dti.StartTimeZoneName = dti.EndTimeZoneName = (tzn == null) ? String.Empty : tzn.Value;
                }

            return dti;
        }
        #endregion
    }
}
