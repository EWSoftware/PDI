//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : VEvent.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains the definition for the VEvent object used by vCalendar and iCalendar objects
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
    /// This class represents a VEVENT component that can appear in a calendar
    /// </summary>
    [TypeDescriptionProvider(typeof(VEventTypeDescriptionProvider))]
    public class VEvent : RecurringObject
    {
        #region Private data members
        //=====================================================================

        // Single event properties
        private ClassificationProperty     classification = null!;
        private CategoriesProperty         categories = null!;
        private ResourcesProperty          resources = null!;
        private UrlProperty                url = null!;
        private UniqueIdProperty           uid = null!;
        private GeographicPositionProperty geo = null!;         // iCalendar only
        private LastModifiedProperty       lastMod = null!;
        private DateCreatedProperty        dateCreated = null!;
        private StartDateProperty          startDate = null!;
        private EndDateProperty            endDate = null!;
        private TimeStampProperty          dateStamp = null!;   // iCalendar only
        private SummaryProperty            summary = null!;
        private DescriptionProperty        desc = null!;
        private LocationProperty           location = null!;
        private PriorityProperty           priority = null!;
        private SequenceProperty           sequence = null!;
        private TimeTransparencyProperty   transp = null!;
        private RecurrenceCountProperty    rNum = null!;        // vCalendar only
        private CommentProperty            comment = null!;     // iCalendar only
        private OrganizerProperty          organizer = null!;   // iCalendar only
        private RecurrenceIdProperty       recurId = null!;     // iCalendar only
        private StatusProperty             status = null!;
        private DurationProperty           duration = null!;    // iCalendar only

        // Event property collections.  There can be one or more of each of these properties so they are stored
        // in a collection.
        private ContactPropertyCollection   contacts = null!;   // iCalendar only
        private AttendeePropertyCollection  attendees = null!;
        private RelatedToPropertyCollection relatedTo = null!;
        private AttachPropertyCollection    attachments = null!;
        private RequestStatusPropertyCollection reqStats = null!;   // iCalendar only
        private VAlarmCollection            alarms = null!;

        // This is a catch-all that holds all unknown or extension properties
        private CustomPropertyCollection customProps = null!;

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
        /// This is used to get the Uniform Resource Locator (URL) property
        /// </summary>
        public UrlProperty Url
        {
            get
            {
                url ??= new UrlProperty();

                return url;
            }
        }

        /// <summary>
        /// This is used to get the Unique ID (UID) property
        /// </summary>
        /// <remarks>For the iCalendar 2.0 specification, this is a required property and will always by saved to
        /// the data stream.  A new ID will be created automatically if necessary.  It is not required for
        /// vCalendar 1.0 objects and will not be saved unless the property has been accessed.</remarks>
        public UniqueIdProperty UniqueId
        {
            get
            {
                if(uid == null)
                {
                    uid = new UniqueIdProperty();
                    uid.AssignNewId(true);
                }

                return uid;
            }
        }

        /// <summary>
        /// This is used to get the Last Modified (LAST-MODIFIED) property
        /// </summary>
        public LastModifiedProperty LastModified
        {
            get
            {
                lastMod ??= new LastModifiedProperty();

                return lastMod;
            }
        }

        /// <summary>
        /// This is used to get the geographic position (GEO) property
        /// </summary>
        /// <value>This property is only applicable to iCalendar 2.0 objects.  For vCalendar 1.0 objects, the
        /// position is stored in the owning calendar object.
        /// </value>
        public GeographicPositionProperty GeographicPosition
        {
            get
            {
                geo ??= new GeographicPositionProperty();

                return geo;
            }
        }

        /// <summary>
        /// This is used to get the access classification (CLASS) property
        /// </summary>
        public ClassificationProperty Classification
        {
            get
            {
                classification ??= new ClassificationProperty();

                return classification;
            }
        }

        /// <summary>
        /// This is used to get the categories (CATEGORIES) property
        /// </summary>
        /// <value>It is used to define application category information for the item</value>
        public CategoriesProperty Categories
        {
            get
            {
                categories ??= new CategoriesProperty();

                return categories;
            }
        }

        /// <summary>
        /// This is used to get the resources (RESOURCES) property
        /// </summary>
        public ResourcesProperty Resources
        {
            get
            {
                resources ??= new ResourcesProperty();

                return resources;
            }
        }

        /// <summary>
        /// This is used to get the date created (DCREATED) property
        /// </summary>
        public DateCreatedProperty DateCreated
        {
            get
            {
                dateCreated ??= new DateCreatedProperty();

                return dateCreated;
            }
        }

        /// <summary>
        /// This is used to get the start date/time (DTSTART) property
        /// </summary>
        public override StartDateProperty StartDateTime
        {
            get
            {
                startDate ??= new StartDateProperty();

                return startDate;
            }
        }

        /// <summary>
        /// This is used to get the end date/time (DTEND) property
        /// </summary>
        /// <remarks><c>EndDateTime</c> and <see cref="Duration"/> are mutually exclusive.  If specified and the
        /// <see cref="Duration"/> property is not set (zero-length duration), this is used to calculate the
        /// duration of the event.  If there is a non-zero-length duration, it is used instead and the end
        /// date/time is ignored.</remarks>
        /// <seealso cref="InstanceDuration"/>
        public EndDateProperty EndDateTime
        {
            get
            {
                endDate ??= new EndDateProperty();

                return endDate;
            }
        }

        /// <summary>
        /// This is used to get the time stamp (DTSTAMP) property
        /// </summary>
        /// <remarks>This will be updated whenever the object is written to a PDI data stream</remarks>
        public TimeStampProperty TimeStamp
        {
            get
            {
                dateStamp ??= new TimeStampProperty { DateTimeValue = DateTime.Now };

                return dateStamp;
            }
        }

        /// <summary>
        /// This is used to get the summary (SUMMARY) property
        /// </summary>
        public SummaryProperty Summary
        {
            get
            {
                summary ??= new SummaryProperty();

                return summary;
            }
        }

        /// <summary>
        /// This is used to get the description (DESCRIPTION) property
        /// </summary>
        public DescriptionProperty Description
        {
            get
            {
                desc ??= new DescriptionProperty();

                return desc;
            }
        }

        /// <summary>
        /// This is used to get the location (LOCATION) property
        /// </summary>
        public LocationProperty Location
        {
            get
            {
                location ??= new LocationProperty();

                return location;
            }
        }

        /// <summary>
        /// This is used to get the priority (PRIORITY) property
        /// </summary>
        public PriorityProperty Priority
        {
            get
            {
                priority ??= new PriorityProperty();

                return priority;
            }
        }

        /// <summary>
        /// This is used to get the revision sequence number (SEQUENCE) property
        /// </summary>
        public SequenceProperty Sequence
        {
            get
            {
                sequence ??= new SequenceProperty();

                return sequence;
            }
        }

        /// <summary>
        /// This is used to get the time transparency (TRANSP) property
        /// </summary>
        public TimeTransparencyProperty Transparency
        {
            get
            {
                transp ??= new TimeTransparencyProperty();

                return transp;
            }
        }

        /// <summary>
        /// This is used to get the recurrence count (RNUM) property
        /// </summary>
        /// <remarks>This property is only valid for the vCalendar 1.0 specification.  It is ignored for
        /// iCalendar 2.0 objects.</remarks>
        public RecurrenceCountProperty RecurrenceCount
        {
            get
            {
                rNum ??= new RecurrenceCountProperty();

                return rNum;
            }
        }

        /// <summary>
        /// This is used to get the comment (COMMENT) property
        /// </summary>
        /// <remarks>This property is only valid for the iCalendar 2.0 specification.  It is ignored for
        /// vCalendar 1.0 objects.</remarks>
        public CommentProperty Comment
        {
            get
            {
                comment ??= new CommentProperty();

                return comment;
            }
        }

        /// <summary>
        /// This is used to get the organizer (ORGANIZER) property
        /// </summary>
        /// <remarks>This property is only valid for the iCalendar 2.0 specification.  It is ignored for
        /// vCalendar 1.0 objects.</remarks>
        public OrganizerProperty Organizer
        {
            get
            {
                organizer ??= new OrganizerProperty();

                return organizer;
            }
        }

        /// <summary>
        /// This is used to get the recurrence ID (RECURRENCE-ID) property
        /// </summary>
        /// <remarks>This property is only valid for the iCalendar 2.0 specification.  It is ignored for
        /// vCalendar 1.0 objects.</remarks>
        public RecurrenceIdProperty RecurrenceId
        {
            get
            {
                recurId ??= new RecurrenceIdProperty();

                return recurId;
            }
        }

        /// <summary>
        /// This is used to get the status (STATUS) property
        /// </summary>
        public StatusProperty Status
        {
            get
            {
                status ??= new StatusProperty();

                return status;
            }
        }

        /// <summary>
        /// This is used to get the duration (DURATION) property
        /// </summary>
        /// <remarks><c>Duration</c> and <see cref="EndDateTime"/> are mutually exclusive.  If an end date/time
        /// is specified and the <c>Duration</c> property is not set (zero-length duration), the end date/time is
        /// used to calculate the duration of the event.  If there is a non-zero-length duration, it is used
        /// instead and the end date/time is ignored.</remarks>
        /// <seealso cref="InstanceDuration"/>
        public DurationProperty Duration
        {
            get
            {
                duration ??= new DurationProperty();

                return duration;
            }
        }

        /// <summary>
        /// This is used to get the Contact (CONTACT) properties.  There may be more than one.
        /// </summary>
        /// <value>If the returned collection is empty, there are no contact properties for the object.  This
        /// property is only valid for iCalendar 2.0 objects.  It is ignored for vCalendar 1.0 objects.</value>
        public ContactPropertyCollection Contacts
        {
            get
            {
                contacts ??= [];

                return contacts;
            }
        }

        /// <summary>
        /// This is used to get the Attendee (ATTENDEE) properties.  There may be more than one.
        /// </summary>
        /// <value>If the returned collection is empty, there are no attendee properties for the object</value>
        public AttendeePropertyCollection Attendees
        {
            get
            {
                attendees ??= [];

                return attendees;
            }
        }

        /// <summary>
        /// This is used to get the Related To (RELATED-TO) properties.  There may be more than one.
        /// </summary>
        /// <value>If the returned collection is empty, there are no related-to properties for the object</value>
        public RelatedToPropertyCollection RelatedTo
        {
            get
            {
                relatedTo ??= [];

                return relatedTo;
            }
        }

        /// <summary>
        /// This is used to get the Attachments (ATTACH) properties.  There may be more than one.
        /// </summary>
        /// <value>If the returned collection is empty, there are no attachment properties for the object</value>
        public AttachPropertyCollection Attachments
        {
            get
            {
                attachments ??= [];

                return attachments;
            }
        }

        /// <summary>
        /// This is used to get the Request Status (REQUEST-STATUS) properties.  There may be more than one.
        /// </summary>
        /// <value>If the returned collection is empty, there are no request status properties for the object</value>
        public RequestStatusPropertyCollection RequestStatuses
        {
            get
            {
                reqStats ??= [];

                return reqStats;
            }
        }

        /// <summary>
        /// This is used to get the alarm properties.  There may be more than one.
        /// </summary>
        /// <value>If the returned collection is empty, there are no alarms for the object.  For vCalendar 1.0,
        /// this collection represents the AALARM, DALARM, MALARM, and PALARM properties.  For iCalendar 2.0,
        /// this collection represents the VALARM component which covers the same functions as the vCalendar 1.0
        /// properties.</value>
        public VAlarmCollection Alarms
        {
            get
            {
                alarms ??= [];

                return alarms;
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
                customProps ??= [];

                return customProps;
            }
        }

        /// <summary>
        /// This must be implemented to return the duration for the recurring instances
        /// </summary>
        /// <remarks>If a non-zero <see cref="Duration"/> has been specified, it will be used.  If there is no
        /// duration specified but an <see cref="EndDateTime"/> has been specified, it is used to compute the
        /// duration by subtracting the <see cref="StartDateTime"/> value from it.  If there is no end date and
        /// there is no time part in the start date, the duration is set to the whole day.  If a time part is
        /// present in the start date, a zero length duration is returned.</remarks>
        /// <seealso cref="EndDateTime"/>
        /// <seealso cref="Duration"/>
        public override Duration InstanceDuration
        {
            get
            {
                if(duration != null && duration.DurationValue != PDI.Duration.Zero)
                    return duration.DurationValue;

                if(endDate != null && endDate.TimeZoneDateTime != DateTime.MinValue)
                    return new Duration(endDate.TimeZoneDateTime - this.StartDateTime.TimeZoneDateTime);

                if(startDate != null && startDate.ValueLocation == ValLocValue.Date)
                    return new Duration(TimeSpan.TicksPerDay);

                return PDI.Duration.Zero;
            }
        }

        /// <summary>
        /// This must be implemented to return the time zone ID for the start date
        /// </summary>
        /// <remarks>It returns the <see cref="BaseDateTimeProperty.TimeZoneId"/> of the <see cref="StartDateTime"/>
        /// property.</remarks>
        public override string? TimeZoneId => this.StartDateTime.TimeZoneId;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Default constructor.  Unless the version is changed, the object will conform to the iCalendar 2.0
        /// specification.
        /// </summary>
        public VEvent()
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
            VEvent o = new();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            VEvent o = (VEvent)p;

            this.ClearProperties();

            classification = (ClassificationProperty)o.Classification.Clone();
            categories = (CategoriesProperty)o.Categories.Clone();
            resources = (ResourcesProperty)o.Resources.Clone();
            url = (UrlProperty)o.Url.Clone();
            uid = (UniqueIdProperty)o.UniqueId.Clone();
            geo = (GeographicPositionProperty)o.GeographicPosition.Clone();
            lastMod = (LastModifiedProperty)o.LastModified.Clone();
            dateCreated = (DateCreatedProperty)o.DateCreated.Clone();
            startDate = (StartDateProperty)o.StartDateTime.Clone();
            endDate = (EndDateProperty)o.EndDateTime.Clone();
            dateStamp = (TimeStampProperty)o.TimeStamp.Clone();
            summary = (SummaryProperty)o.Summary.Clone();
            desc = (DescriptionProperty)o.Description.Clone();
            location = (LocationProperty)o.Location.Clone();
            priority = (PriorityProperty)o.Priority.Clone();
            sequence = (SequenceProperty)o.Sequence.Clone();
            transp = (TimeTransparencyProperty)o.Transparency.Clone();
            rNum = (RecurrenceCountProperty)o.RecurrenceCount.Clone();
            comment = (CommentProperty)o.Comment.Clone();
            organizer = (OrganizerProperty)o.Organizer.Clone();
            recurId = (RecurrenceIdProperty)o.RecurrenceId.Clone();
            status = (StatusProperty)o.Status.Clone();
            duration = (DurationProperty)o.Duration.Clone();

            this.Contacts.CloneRange(o.Contacts);
            this.Attendees.CloneRange(o.Attendees);
            this.RelatedTo.CloneRange(o.RelatedTo);
            this.Attachments.CloneRange(o.Attachments);
            this.RequestStatuses.CloneRange(o.RequestStatuses);
            this.Alarms.CloneRange(o.Alarms);
            this.CustomProperties.CloneRange(o.CustomProperties);

            base.Clone(p);
        }

        /// <summary>
        /// The method can be called to clear all current property values from the event.  The version is left
        /// unchanged.
        /// </summary>
        public override void ClearProperties()
        {
            classification = null!;
            categories = null!;
            resources = null!;
            url = null!;
            uid = null!;
            geo = null!;
            lastMod = null!;
            dateCreated = null!;
            startDate = null!;
            endDate = null!;
            dateStamp = null!;
            summary = null!;
            desc = null!;
            location = null!;
            priority = null!;
            sequence = null!;
            transp = null!;
            rNum = null!;
            comment = null!;
            organizer = null!;
            recurId = null!;
            status = null!;
            duration = null!;

            contacts = null!;
            attendees = null!;
            relatedTo = null!;
            attachments = null!;
            reqStats = null!;
            alarms = null!;

            customProps = null!;

            base.ClearProperties();
        }

        /// <summary>
        /// This is used to propagate the version to all properties in the object that need it
        /// </summary>
        public override void PropagateVersion()
        {
            if(classification != null)
                classification.Version = this.Version;

            if(categories != null)
                categories.Version = this.Version;

            if(resources != null)
                resources.Version = this.Version;

            if(url != null)
                url.Version = this.Version;

            if(uid != null)
                uid.Version = this.Version;

            if(geo != null)
                geo.Version = this.Version;

            if(lastMod != null)
                lastMod.Version = this.Version;

            if(dateCreated != null)
                dateCreated.Version = this.Version;

            if(startDate != null)
                startDate.Version = this.Version;

            if(endDate != null)
                endDate.Version = this.Version;

            if(this.Version == SpecificationVersions.vCalendar10)
            {
                if(rNum != null)
                    rNum.Version = this.Version;
            }
            else
            {
                if(dateStamp != null)
                    dateStamp.Version = this.Version;

                if(comment != null)
                    comment.Version = this.Version;

                if(organizer!= null)
                    organizer.Version = this.Version;

                if(recurId != null)
                    recurId.Version = this.Version;

                contacts?.PropagateVersion(this.Version);
                reqStats?.PropagateVersion(this.Version);
            }

            if(summary != null)
                summary.Version = this.Version;

            if(desc != null)
                desc.Version = this.Version;

            if(location != null)
                location.Version = this.Version;

            if(priority != null)
                priority.Version = this.Version;

            if(sequence != null)
                sequence.Version = this.Version;

            if(transp != null)
                transp.Version = this.Version;

            if(status != null)
                status.Version = this.Version;

            if(duration != null)
                duration.Version = this.Version;

            attendees?.PropagateVersion(this.Version);
            relatedTo?.PropagateVersion(this.Version);
            attachments?.PropagateVersion(this.Version);
            alarms?.PropagateVersion(this.Version);
            customProps?.PropagateVersion(this.Version);

            base.PropagateVersion();
        }

        /// <summary>
        /// This is used to get a list of time zones used by all owned objects
        /// </summary>
        /// <param name="timeZoneIds">A <see cref="StringCollection"/> that will be used to store the list of
        /// unique time zone IDs used by the calendar objects.</param>
        public override void TimeZonesUsed(StringCollection timeZoneIds)
        {
            AddTimeZoneIfUsed(startDate, timeZoneIds);
            AddTimeZoneIfUsed(endDate, timeZoneIds);
            AddTimeZoneIfUsed(recurId, timeZoneIds);

            alarms?.TimeZonesUsed(timeZoneIds);

            base.TimeZonesUsed(timeZoneIds);
        }

        /// <summary>
        /// This is used to replace an old time zone ID with a new time zone ID in all properties of a calendar
        /// object.
        /// </summary>
        /// <param name="oldId">The old ID being replaced</param>
        /// <param name="newId">The new ID to use</param>
        public override void UpdateTimeZoneId(string? oldId, string? newId)
        {
            UpdatePropertyTimeZoneId(startDate, oldId, newId);
            UpdatePropertyTimeZoneId(endDate, oldId, newId);
            UpdatePropertyTimeZoneId(recurId, oldId, newId);

            alarms?.UpdateTimeZoneId(oldId, newId);

            base.UpdateTimeZoneId(oldId, newId);
        }

        /// <summary>
        /// This is used to apply the selected time zone to all date/time objects in the component and convert
        /// them to the new time zone.
        /// </summary>
        /// <param name="vTimeZone">A <see cref="VTimeZone"/> object that will be used for all date/time objects
        /// in the component.</param>
        /// <remarks>When applied, all date/time values in the object will be converted to the new time zone</remarks>
        public override void ApplyTimeZone(VTimeZone? vTimeZone)
        {
            ApplyPropertyTimeZone(startDate, vTimeZone);
            ApplyPropertyTimeZone(endDate, vTimeZone);
            ApplyPropertyTimeZone(recurId, vTimeZone);

            alarms?.ApplyTimeZone(vTimeZone);

            base.ApplyTimeZone(vTimeZone);
        }

        /// <summary>
        /// This is used to set the selected time zone in all date/time objects in the component without
        /// modifying the date/time values.
        /// </summary>
        /// <param name="vTimeZone">A <see cref="VTimeZone"/> object that will be used for all date/time objects
        /// in the component.</param>
        /// <remarks>This method does not affect the date/time values</remarks>
        public override void SetTimeZone(VTimeZone? vTimeZone)
        {
            SetPropertyTimeZone(startDate, vTimeZone);
            SetPropertyTimeZone(endDate, vTimeZone);
            SetPropertyTimeZone(recurId, vTimeZone);

            alarms?.SetTimeZone(vTimeZone);

            base.SetTimeZone(vTimeZone);
        }

        /// <summary>
        /// This can be used to write an event to a PDI data stream
        /// </summary>
        /// <param name="tw">A <see cref="System.IO.TextWriter"/> derived class to which the event is written.</param>
        /// <param name="sb">A <see cref="System.Text.StringBuilder"/> used by the properties as a temporary
        /// buffer.  This can be null if the TextWriter is a <see cref="System.IO.StringWriter"/>.</param>
        /// <remarks>This is called by <see cref="CalendarObject.ToString"/> as well as owning objects when they
        /// convert themselves to a string or write themselves to a PDI data stream.</remarks>
        public override void WriteToStream(TextWriter tw, StringBuilder? sb)
        {
            // DTSTAMP is always updated and written to reflect when the object was saved to the stream
            // (iCalendar 2.0 only).
            this.TimeStamp.DateTimeValue = DateTime.Now;

            PropagateVersion();

            tw.Write("BEGIN:VEVENT\r\n");

            // This is a required property for iCalendar 2.0 but is optional for vCalendar 1.0
            if(this.Version != SpecificationVersions.vCalendar10 || uid != null)
                BaseProperty.WriteToStream(this.UniqueId, sb, tw);

            BaseProperty.WriteToStream(startDate, sb, tw);

            if(this.Version == SpecificationVersions.iCalendar20)
            {
                // Duration and end date are mutually exclusive.  If there is a duration, end date is ignored.
                if(duration != null && duration.DurationValue != PDI.Duration.Zero)
                    BaseProperty.WriteToStream(duration, sb, tw);

                if(endDate != null && (duration == null || duration.DurationValue == PDI.Duration.Zero))
                    BaseProperty.WriteToStream(endDate, sb, tw);

                BaseProperty.WriteToStream(organizer, sb, tw);
            }
            else
            {
                // vCalendar uses DTEND and doesn't have a DURATION
                BaseProperty.WriteToStream(endDate, sb, tw);
                BaseProperty.WriteToStream(rNum, sb, tw);
            }

            BaseProperty.WriteToStream(summary, sb, tw);
            BaseProperty.WriteToStream(desc, sb, tw);
            BaseProperty.WriteToStream(classification, sb, tw);
            BaseProperty.WriteToStream(categories, sb, tw);
            BaseProperty.WriteToStream(resources, sb, tw);
            BaseProperty.WriteToStream(location, sb, tw);
            BaseProperty.WriteToStream(priority, sb, tw);
            BaseProperty.WriteToStream(sequence, sb, tw);
            BaseProperty.WriteToStream(transp, sb, tw);
            BaseProperty.WriteToStream(status, sb, tw);
            BaseProperty.WriteToStream(url, sb, tw);
            BaseProperty.WriteToStream(dateCreated, sb, tw);
            BaseProperty.WriteToStream(lastMod, sb, tw);

            if(this.Version == SpecificationVersions.iCalendar20)
            {
                BaseProperty.WriteToStream(dateStamp, sb, tw);
                BaseProperty.WriteToStream(comment, sb, tw);
                BaseProperty.WriteToStream(geo, sb, tw);
                BaseProperty.WriteToStream(recurId, sb, tw);

                if(contacts != null && contacts.Count != 0)
                {
                    foreach(ContactProperty c in contacts)
                        BaseProperty.WriteToStream(c, sb, tw);
                }

                if(reqStats != null && reqStats.Count != 0)
                {
                    foreach(RequestStatusProperty r in reqStats)
                        BaseProperty.WriteToStream(r, sb, tw);
                }
            }

            if(attendees != null && attendees.Count != 0)
            {
                foreach(AttendeeProperty a in attendees)
                    BaseProperty.WriteToStream(a, sb, tw);
            }

            if(relatedTo != null && relatedTo.Count != 0)
            {
                foreach(RelatedToProperty r in relatedTo)
                    BaseProperty.WriteToStream(r, sb, tw);
            }

            base.WriteToStream(tw, sb);

            if(alarms != null && alarms.Count != 0)
            {
                foreach(VAlarm a in alarms)
                    a.WriteToStream(tw, sb);
            }

            if(attachments != null && attachments.Count != 0)
            {
                foreach(AttachProperty a in attachments)
                    BaseProperty.WriteToStream(a, sb, tw);
            }

            if(customProps != null && customProps.Count != 0)
            {
                foreach(CustomProperty c in customProps)
                    BaseProperty.WriteToStream(c, sb, tw);
            }

            tw.Write("END:VEVENT\r\n");
        }

        /// <summary>
        /// This is overridden to allow proper comparison of event objects
        /// </summary>
        /// <param name="obj">The object to which this instance is compared</param>
        /// <returns>Returns true if the object equals this instance, false if it does not</returns>
        public override bool Equals(object obj)
        {
            if(obj is not VEvent ev)
                return false;

            // The ToString() method returns a text representation of the event based on all of its settings so
            // it's a reliable way to tell if two instances are the same.
            return this == ev || this.ToString() == ev.ToString();
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
