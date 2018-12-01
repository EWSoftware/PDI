//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : VToDo.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the definition for the VToDo object used by vCalendar and iCalendar objects
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
    /// This class represents a VTODO component that can appear in a calendar
    /// </summary>
    [TypeDescriptionProvider(typeof(VToDoTypeDescriptionProvider))]
    public class VToDo : RecurringObject
    {
        #region Private data members
        //=====================================================================

        // Single to-do properties
        private ClassificationProperty     classification;
        private CategoriesProperty         categories;
        private ResourcesProperty          resources;
        private UrlProperty                url;
        private UniqueIdProperty           uid;
        private GeographicPositionProperty geo;         // iCalendar only
        private LastModifiedProperty       lastMod;
        private DateCreatedProperty        dateCreated;
        private StartDateProperty          startDate;
        private DueDateProperty            due;
        private CompletedDateProperty      completed;
        private TimeStampProperty          timeStamp;   // iCalendar only
        private SummaryProperty            summary;
        private DescriptionProperty        desc;
        private PriorityProperty           priority;
        private SequenceProperty           sequence;
        private RecurrenceCountProperty    rNum;        // vCalendar only
        private CommentProperty            comment;     // iCalendar only
        private OrganizerProperty          organizer;   // iCalendar only
        private RecurrenceIdProperty       recurId;     // iCalendar only
        private StatusProperty             status;
        private PercentCompleteProperty    pct;         // iCalendar only
        private DurationProperty           duration;    // iCalendar only

        // To-do property collections.  There can be one or more of each of these properties so they are stored
        // in a collection.
        private ContactPropertyCollection   contacts;   // iCalendar only
        private AttendeePropertyCollection  attendees;
        private RelatedToPropertyCollection relatedTo;
        private AttachPropertyCollection    attachments;
        private RequestStatusPropertyCollection reqStats;   // iCalendar only
        private VAlarmCollection            alarms;

        // This is a catch-all that holds all unknown or extension properties
        private CustomPropertyCollection customProps;

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
                if(url == null)
                    url = new UrlProperty();

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
                if(lastMod == null)
                    lastMod = new LastModifiedProperty();

                return lastMod;
            }
        }

        /// <summary>
        /// This is used to get the geographic position (GEO) property
        /// </summary>
        /// <value>This property is only applicable to iCalendar 2.0 objects.  For vCalendar 1.0 objects, the
        /// position is stored in the owning calendar object.</value>
        public GeographicPositionProperty GeographicPosition
        {
            get
            {
                if(geo == null)
                    geo = new GeographicPositionProperty();

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
                if(classification == null)
                    classification = new ClassificationProperty();

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
                if(categories == null)
                    categories = new CategoriesProperty();

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
                if(resources == null)
                    resources = new ResourcesProperty();

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
                if(dateCreated == null)
                    dateCreated = new DateCreatedProperty();

                return dateCreated;
            }
        }

        /// <summary>
        /// This is used to get the due date/time (DUE) property
        /// </summary>
        /// <remarks><c>DueDateTime</c> and <see cref="Duration"/> are mutually exclusive.  If a due date is
        /// specified, the duration value is ignored.</remarks>
        /// <seealso cref="InstanceDuration"/>
        public DueDateProperty DueDateTime
        {
            get
            {
                if(due == null)
                    due = new DueDateProperty();

                return due;
            }
        }

        /// <summary>
        /// This is used to get the start date/time (DTSTART) property
        /// </summary>
        public override StartDateProperty StartDateTime
        {
            get
            {
                if(startDate == null)
                    startDate = new StartDateProperty();

                return startDate;
            }
        }


        /// <summary>
        /// This is used to get the completed date/time (COMPLETED) property
        /// </summary>
        public CompletedDateProperty CompletedDateTime
        {
            get
            {
                if(completed == null)
                    completed = new CompletedDateProperty();

                return completed;
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
                if(timeStamp == null)
                    timeStamp = new TimeStampProperty { DateTimeValue = DateTime.Now };

                return timeStamp;
            }
        }

        /// <summary>
        /// This is used to get the summary (SUMMARY) property
        /// </summary>
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
        /// This is used to get the priority (PRIORITY) property
        /// </summary>
        public PriorityProperty Priority
        {
            get
            {
                if(priority == null)
                    priority = new PriorityProperty();

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
                if(sequence == null)
                    sequence = new SequenceProperty();

                return sequence;
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
                if(rNum == null)
                    rNum = new RecurrenceCountProperty();

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
                if(comment == null)
                    comment = new CommentProperty();

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
                if(organizer == null)
                    organizer = new OrganizerProperty();

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
                if(recurId == null)
                    recurId = new RecurrenceIdProperty();

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
                if(status == null)
                    status = new StatusProperty();

                return status;
            }
        }

        /// <summary>
        /// This is used to get the duration (DURATION) property
        /// </summary>
        /// <remarks><see cref="DueDateTime"/> and <c>Duration</c> are mutually exclusive.  If a due date is
        /// specified, the duration value is ignored.</remarks>
        /// <seealso cref="InstanceDuration"/>
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
        /// This is used to get the percent complete (PERCENT-COMPLETE) property
        /// </summary>
        public PercentCompleteProperty PercentComplete
        {
            get
            {
                if(pct == null)
                    pct = new PercentCompleteProperty();

                return pct;
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
                if(contacts == null)
                    contacts = new ContactPropertyCollection();

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
                if(attendees == null)
                    attendees = new AttendeePropertyCollection();

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
                if(relatedTo == null)
                    relatedTo = new RelatedToPropertyCollection();

                return relatedTo;
            }
        }

        /// <summary>
        /// This is used to get the Attachments (ATTACH) properties.  There may be more than one
        /// </summary>
        /// <value>If the returned collection is empty, there are no attachment properties for the object</value>
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
        /// This is used to get the Request Status (REQUEST-STATUS) properties There may be more than one.
        /// </summary>
        /// <value>If the returned collection is empty, there are no request status properties for the object</value>
        public RequestStatusPropertyCollection RequestStatuses
        {
            get
            {
                if(reqStats == null)
                    reqStats = new RequestStatusPropertyCollection();

                return reqStats;
            }
        }

        /// <summary>
        /// This is used to get the alarm properties.  There may be more than
        /// one.
        /// </summary>
        /// <value>If the returned collection is empty, there are no alarms for the object.  For vCalendar 1.0,
        /// this collection represents the AALARM, DALARM, MALARM, and PALARM properties.  For iCalendar 2.0,
        /// this collection represents the VALARM component which covers the same functions as the vCalendar 1.0
        /// properties.</value>
        public VAlarmCollection Alarms
        {
            get
            {
                if(alarms == null)
                    alarms = new VAlarmCollection();

                return alarms;
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

        /// <summary>
        /// This must be implemented to return the duration for the recurring instances
        /// </summary>
        /// <remarks>The <see cref="Duration"/> is used if one has been specified and no <see cref="DueDateTime"/>
        /// has been specified.  If a duration is not specified or there is a due date and there is no time part
        /// in the start date, the duration is set to the whole day.  If a time part is present in the start
        /// date, a zero length duration is returned.</remarks>
        /// <seealso cref="DueDateTime"/>
        /// <seealso cref="Duration"/>
        public override Duration InstanceDuration
        {
            get
            {
                if((due == null || due.TimeZoneDateTime == DateTime.MinValue) && duration != null &&
                  duration.DurationValue != PDI.Duration.Zero)
                    return duration.DurationValue;

                if(startDate != null && startDate.ValueLocation == ValLocValue.Date)
                    return new Duration(TimeSpan.TicksPerDay);

                return PDI.Duration.Zero;
            }
        }

        /// <summary>
        /// This must be implemented to return the time zone ID for the start date
        /// </summary>
        /// <remarks>It returns the <see cref="BaseDateTimeProperty.TimeZoneId"/> of the
        /// <see cref="StartDateTime"/> property.</remarks>
        public override string TimeZoneId => this.StartDateTime.TimeZoneId;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Default constructor.  Unless the version is changed, the object will conform to the iCalendar 2.0
        /// specification.
        /// </summary>
        public VToDo()
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
            VToDo o = new VToDo();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            VToDo o = (VToDo)p;

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
            due = (DueDateProperty)o.DueDateTime.Clone();
            completed = (CompletedDateProperty)o.CompletedDateTime.Clone();
            timeStamp = (TimeStampProperty)o.TimeStamp.Clone();
            summary = (SummaryProperty)o.Summary.Clone();
            desc = (DescriptionProperty)o.Description.Clone();
            priority = (PriorityProperty)o.Priority.Clone();
            sequence = (SequenceProperty)o.Sequence.Clone();
            rNum = (RecurrenceCountProperty)o.RecurrenceCount.Clone();
            comment = (CommentProperty)o.Comment.Clone();
            organizer = (OrganizerProperty)o.Organizer.Clone();
            recurId = (RecurrenceIdProperty)o.RecurrenceId.Clone();
            status = (StatusProperty)o.Status.Clone();
            pct = (PercentCompleteProperty)o.PercentComplete.Clone();
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
        /// The method can be called to clear all current property values from the To-Do object.  The version is
        /// left unchanged.
        /// </summary>
        public override void ClearProperties()
        {
            classification = null;
            categories = null;
            resources = null;
            url = null;
            uid = null;
            geo = null;
            lastMod = null;
            dateCreated = null;
            startDate = null;
            due = null;
            completed = null;
            timeStamp = null;
            summary = null;
            desc = null;
            priority = null;
            sequence = null;
            rNum = null;
            comment = null;
            organizer = null;
            recurId = null;
            status = null;
            pct = null;
            duration = null;

            contacts = null;
            attendees = null;
            relatedTo = null;
            attachments = null;
            reqStats = null;
            alarms = null;

            customProps = null;

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

            if(due != null)
                due.Version = this.Version;

            if(completed != null)
                completed.Version = this.Version;

            if(this.Version == SpecificationVersions.vCalendar10)
            {
                if(rNum != null)
                    rNum.Version = this.Version;
            }
            else
            {
                if(timeStamp != null)
                    timeStamp.Version = this.Version;

                if(comment != null)
                    comment.Version = this.Version;

                if(organizer!= null)
                    organizer.Version = this.Version;

                if(recurId != null)
                    recurId.Version = this.Version;

                if(pct != null)
                    pct.Version = this.Version;

                if(duration != null)
                    duration.Version = this.Version;

                if(contacts != null)
                    contacts.PropagateVersion(this.Version);

                if(reqStats != null)
                    reqStats.PropagateVersion(this.Version);
            }

            if(summary != null)
                summary.Version = this.Version;

            if(desc != null)
                desc.Version = this.Version;

            if(priority != null)
                priority.Version = this.Version;

            if(sequence != null)
                sequence.Version = this.Version;

            if(status != null)
                status.Version = this.Version;

            if(attendees != null)
                attendees.PropagateVersion(this.Version);

            if(relatedTo != null)
                relatedTo.PropagateVersion(this.Version);

            if(attachments != null)
                attachments.PropagateVersion(this.Version);

            if(alarms != null)
                alarms.PropagateVersion(this.Version);

            if(customProps != null)
                customProps.PropagateVersion(this.Version);

            base.PropagateVersion();
        }

        /// <summary>
        /// This is used to get a list of time zones used by all owned objects
        /// </summary>
        /// <param name="timeZoneIds">A <see cref="StringCollection"/> that will be used to store the list of
        /// unique time zone IDs used by the calendar objects.</param>
        public override void TimeZonesUsed(StringCollection timeZoneIds)
        {
            CalendarObject.AddTimeZoneIfUsed(startDate, timeZoneIds);
            CalendarObject.AddTimeZoneIfUsed(due, timeZoneIds);
            CalendarObject.AddTimeZoneIfUsed(recurId, timeZoneIds);

            if(alarms != null)
                alarms.TimeZonesUsed(timeZoneIds);

            base.TimeZonesUsed(timeZoneIds);
        }

        /// <summary>
        /// This is used to replace an old time zone ID with a new time zone ID in all properties of a calendar
        /// object.
        /// </summary>
        /// <param name="oldId">The old ID being replaced</param>
        /// <param name="newId">The new ID to use</param>
        public override void UpdateTimeZoneId(string oldId, string newId)
        {
            CalendarObject.UpdatePropertyTimeZoneId(startDate, oldId, newId);
            CalendarObject.UpdatePropertyTimeZoneId(due, oldId, newId);
            CalendarObject.UpdatePropertyTimeZoneId(recurId, oldId, newId);

            if(alarms != null)
                alarms.UpdateTimeZoneId(oldId, newId);

            base.UpdateTimeZoneId(oldId, newId);
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
            CalendarObject.ApplyPropertyTimeZone(startDate, vTimeZone);
            CalendarObject.ApplyPropertyTimeZone(due, vTimeZone);
            CalendarObject.ApplyPropertyTimeZone(recurId, vTimeZone);

            if(alarms != null)
                alarms.ApplyTimeZone(vTimeZone);

            base.ApplyTimeZone(vTimeZone);
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
            CalendarObject.SetPropertyTimeZone(startDate, vTimeZone);
            CalendarObject.SetPropertyTimeZone(due, vTimeZone);
            CalendarObject.SetPropertyTimeZone(recurId, vTimeZone);

            if(alarms != null)
                alarms.SetTimeZone(vTimeZone);

            base.SetTimeZone(vTimeZone);
        }

        /// <summary>
        /// This can be used to write a To-Do object to a PDI data stream
        /// </summary>
        /// <param name="tw">A <see cref="System.IO.TextWriter"/> derived class to which the To-Do object is
        /// written.</param>
        /// <param name="sb">A <see cref="System.Text.StringBuilder"/> used by the properties as a temporary
        /// buffer.  This can be null if the TextWriter is a <see cref="System.IO.StringWriter"/>.</param>
        /// <remarks>This is called by <see cref="CalendarObject.ToString"/> as well as owning objects when they
        /// convert themselves to a string or write themselves to a PDI data stream.</remarks>
        public override void WriteToStream(TextWriter tw, StringBuilder sb)
        {
            // DTSTAMP is always updated and written to reflect when the object was saved to the stream
            // (iCalendar 2.0 only).
            this.TimeStamp.DateTimeValue = DateTime.Now;

            PropagateVersion();

            tw.Write("BEGIN:VTODO\r\n");

            // This is a required property for iCalendar 2.0 but is optional for vCalendar 1.0
            if(this.Version != SpecificationVersions.vCalendar10 || uid != null)
                BaseProperty.WriteToStream(this.UniqueId, sb, tw);

            BaseProperty.WriteToStream(sequence, sb, tw);
            BaseProperty.WriteToStream(startDate, sb, tw);
            BaseProperty.WriteToStream(due, sb, tw);

            if(this.Version == SpecificationVersions.iCalendar20)
            {
                // Duration and due date are mutually exclusive.  If there is a due date, duration is ignored.
                if((due == null || due.TimeZoneDateTime == DateTime.MinValue) && duration != null &&
                  duration.DurationValue != PDI.Duration.Zero)
                    BaseProperty.WriteToStream(duration, sb, tw);

                BaseProperty.WriteToStream(pct, sb, tw);
            }
            else
                BaseProperty.WriteToStream(rNum, sb, tw);

            BaseProperty.WriteToStream(completed, sb, tw);
            BaseProperty.WriteToStream(summary, sb, tw);

            if(this.Version == SpecificationVersions.iCalendar20)
            {
                BaseProperty.WriteToStream(organizer, sb, tw);
                BaseProperty.WriteToStream(comment, sb, tw);
                BaseProperty.WriteToStream(timeStamp, sb, tw);
                BaseProperty.WriteToStream(geo, sb, tw);
                BaseProperty.WriteToStream(recurId, sb, tw);

                if(contacts != null && contacts.Count != 0)
                    foreach(ContactProperty c in contacts)
                        BaseProperty.WriteToStream(c, sb, tw);

                if(reqStats != null && reqStats.Count != 0)
                    foreach(RequestStatusProperty r in reqStats)
                        BaseProperty.WriteToStream(r, sb, tw);
            }

            BaseProperty.WriteToStream(classification, sb, tw);
            BaseProperty.WriteToStream(categories, sb, tw);
            BaseProperty.WriteToStream(resources, sb, tw);
            BaseProperty.WriteToStream(desc, sb, tw);
            BaseProperty.WriteToStream(priority, sb, tw);
            BaseProperty.WriteToStream(status, sb, tw);
            BaseProperty.WriteToStream(url, sb, tw);
            BaseProperty.WriteToStream(dateCreated, sb, tw);
            BaseProperty.WriteToStream(lastMod, sb, tw);

            if(attendees != null && attendees.Count != 0)
                foreach(AttendeeProperty a in attendees)
                    BaseProperty.WriteToStream(a, sb, tw);

            if(relatedTo != null && relatedTo.Count != 0)
                foreach(RelatedToProperty r in relatedTo)
                    BaseProperty.WriteToStream(r, sb, tw);

            base.WriteToStream(tw, sb);

            if(alarms != null && alarms.Count != 0)
                foreach(VAlarm a in alarms)
                    a.WriteToStream(tw, sb);

            if(attachments != null && attachments.Count != 0)
                foreach(AttachProperty a in attachments)
                    BaseProperty.WriteToStream(a, sb, tw);

            if(customProps != null && customProps.Count != 0)
                foreach(CustomProperty c in customProps)
                    BaseProperty.WriteToStream(c, sb, tw);

            tw.Write("END:VTODO\r\n");
        }

        /// <summary>
        /// This is overridden to allow proper comparison of To-Do objects
        /// </summary>
        /// <param name="obj">The object to which this instance is compared</param>
        /// <returns>Returns true if the object equals this instance, false if it does not</returns>
        public override bool Equals(object obj)
        {
            if(!(obj is VToDo td))
                return false;

            // The ToString() method returns a text representation of the object based on all of its settings so
            // it's a reliable way to tell if two instances are the same.
            return (this == td || this.ToString() == td.ToString());
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
