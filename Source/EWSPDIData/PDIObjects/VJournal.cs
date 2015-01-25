//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : VJournal.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/06/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the definition for the VJournal object used by iCalendar objects
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
    /// This class represents a VJOURNAL component that can appear in a calendar
    /// </summary>
    [TypeDescriptionProvider(typeof(VJournalTypeDescriptionProvider))]
    public class VJournal : RecurringObject
    {
        #region Private data members
        //=====================================================================

        // Single journal properties
        private ClassificationProperty classification;
        private CategoriesProperty     categories;
        private UrlProperty            url;
        private UniqueIdProperty       uid;
        private LastModifiedProperty   lastMod;
        private DateCreatedProperty    dateCreated;
        private StartDateProperty      startDate;
        private TimeStampProperty      timeStamp;
        private SummaryProperty        summary;
        private DescriptionProperty    desc;
        private SequenceProperty       sequence;
        private CommentProperty        comment;
        private OrganizerProperty      organizer;
        private RecurrenceIdProperty   recurId;
        private StatusProperty         status;

        // Journal property collections.  There can be one or more of each of these properties so they are stored
        // in a collection.
        private ContactPropertyCollection   contacts;
        private AttendeePropertyCollection  attendees;
        private RelatedToPropertyCollection relatedTo;
        private AttachPropertyCollection    attachments;
        private RequestStatusPropertyCollection reqStats;

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
        /// the data stream.  A new ID will be created automatically if necessary.</remarks>
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
        /// This is used to get the time stamp (DTSTAMP) property
        /// </summary>
        /// <remarks>This will be updated whenever the object is written to a PDI data stream</remarks>
        public TimeStampProperty TimeStamp
        {
            get
            {
                if(timeStamp == null)
                {
                    timeStamp = new TimeStampProperty();
                    timeStamp.DateTimeValue = DateTime.Now;
                }

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
        /// This is used to get the organizer (ORGANIZER) property
        /// </summary>
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
        /// This is used to get the Contact (CONTACT) properties.  There may be more than one.
        /// </summary>
        /// <value>If the returned collection is empty, there are no contact properties for the object</value>
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
        /// This is used to get the Attachments (ATTACH) properties.  There may be more than one.
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
        /// This is used to get the Request Status (REQUEST-STATUS) properties.  There may be more than one.
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

        /// <summary>
        /// This must be implemented to return the duration for the recurring instances
        /// </summary>
        /// <remarks>If there is no time part in the start date, the duration is set to the whole day.  If a time
        /// part is present in the start date, a zero length duration is returned.</remarks>
        public override Duration InstanceDuration
        {
            get
            {
                if(startDate != null && startDate.ValueLocation == ValLocValue.Date)
                    return new Duration(TimeSpan.TicksPerDay);

                return Duration.Zero;
            }
        }

        /// <summary>
        /// This must be implemented to return the time zone ID for the start date
        /// </summary>
        /// <remarks>It returns the <see cref="BaseDateTimeProperty.TimeZoneId"/> of the <see cref="StartDateTime"/>
        /// property.</remarks>
        public override string TimeZoneId
        {
            get { return this.StartDateTime.TimeZoneId; }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public VJournal()
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
            VJournal o = new VJournal();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            VJournal o = (VJournal)p;

            this.ClearProperties();

            classification = (ClassificationProperty)o.Classification.Clone();
            categories = (CategoriesProperty)o.Categories.Clone();
            url = (UrlProperty)o.Url.Clone();
            uid = (UniqueIdProperty)o.UniqueId.Clone();
            lastMod = (LastModifiedProperty)o.LastModified.Clone();
            dateCreated = (DateCreatedProperty)o.DateCreated.Clone();
            startDate = (StartDateProperty)o.StartDateTime.Clone();
            timeStamp = (TimeStampProperty)o.TimeStamp.Clone();
            summary = (SummaryProperty)o.Summary.Clone();
            desc = (DescriptionProperty)o.Description.Clone();
            sequence = (SequenceProperty)o.Sequence.Clone();
            comment = (CommentProperty)o.Comment.Clone();
            organizer = (OrganizerProperty)o.Organizer.Clone();
            recurId = (RecurrenceIdProperty)o.RecurrenceId.Clone();
            status = (StatusProperty)o.Status.Clone();

            this.Contacts.CloneRange(o.Contacts);
            this.Attendees.CloneRange(o.Attendees);
            this.RelatedTo.CloneRange(o.RelatedTo);
            this.Attachments.CloneRange(o.Attachments);
            this.RequestStatuses.CloneRange(o.RequestStatuses);
            this.CustomProperties.CloneRange(o.CustomProperties);

            base.Clone(p);
        }

        /// <summary>
        /// The method can be called to clear all current property values from the journal.  The version is left
        /// unchanged.
        /// </summary>
        public override void ClearProperties()
        {
            classification = null;
            categories = null;
            url = null;
            uid = null;
            lastMod = null;
            dateCreated = null;
            startDate = null;
            timeStamp = null;
            summary = null;
            desc = null;
            sequence = null;
            comment = null;
            organizer = null;
            recurId = null;
            status = null;

            contacts = null;
            attendees = null;
            relatedTo = null;
            attachments = null;
            reqStats = null;
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

            if(url != null)
                url.Version = this.Version;

            if(uid != null)
                uid.Version = this.Version;

            if(lastMod != null)
                lastMod.Version = this.Version;

            if(dateCreated != null)
                dateCreated.Version = this.Version;

            if(startDate != null)
                startDate.Version = this.Version;

            if(timeStamp != null)
                timeStamp.Version = this.Version;

            if(summary != null)
                summary.Version = this.Version;

            if(desc != null)
                desc.Version = this.Version;

            if(sequence != null)
                sequence.Version = this.Version;

            if(comment != null)
                comment.Version = this.Version;

            if(organizer != null)
                organizer.Version = this.Version;

            if(recurId != null)
                recurId.Version = this.Version;

            if(status != null)
                status.Version = this.Version;

            if(contacts != null)
                contacts.PropagateVersion(this.Version);

            if(attendees != null)
                attendees.PropagateVersion(this.Version);

            if(relatedTo != null)
                relatedTo.PropagateVersion(this.Version);

            if(attachments != null)
                attachments.PropagateVersion(this.Version);

            if(reqStats != null)
                reqStats.PropagateVersion(this.Version);

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
            CalendarObject.AddTimeZoneIfUsed(recurId, timeZoneIds);

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
            CalendarObject.UpdatePropertyTimeZoneId(recurId, oldId, newId);

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
            CalendarObject.ApplyPropertyTimeZone(recurId, vTimeZone);

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
            CalendarObject.SetPropertyTimeZone(recurId, vTimeZone);

            base.SetTimeZone(vTimeZone);
        }

        /// <summary>
        /// This can be used to write a journal to a PDI data stream
        /// </summary>
        /// <param name="tw">A <see cref="System.IO.TextWriter"/> derived class to which the journal is written.</param>
        /// <param name="sb">A <see cref="System.Text.StringBuilder"/> used by the properties as a temporary
        /// buffer.  This can be null if the TextWriter is a <see cref="System.IO.StringWriter"/>.</param>
        /// <remarks>This is called by <see cref="CalendarObject.ToString"/> as well as owning objects when they
        /// convert themselves to a string or write themselves to a PDI data stream.</remarks>
        public override void WriteToStream(TextWriter tw, StringBuilder sb)
        {
            // DTSTAMP is always updated and written to reflect when the object was saved to the stream
            this.TimeStamp.DateTimeValue = DateTime.Now;

            PropagateVersion();

            tw.Write("BEGIN:VJOURNAL\r\n");

            // This is a required property for iCalendar 2.0
            BaseProperty.WriteToStream(this.UniqueId, sb, tw);

            BaseProperty.WriteToStream(timeStamp, sb, tw);
            BaseProperty.WriteToStream(sequence, sb, tw);
            BaseProperty.WriteToStream(startDate, sb, tw);
            BaseProperty.WriteToStream(summary, sb, tw);
            BaseProperty.WriteToStream(desc, sb, tw);
            BaseProperty.WriteToStream(comment, sb, tw);
            BaseProperty.WriteToStream(organizer, sb, tw);
            BaseProperty.WriteToStream(classification, sb, tw);
            BaseProperty.WriteToStream(categories, sb, tw);
            BaseProperty.WriteToStream(status, sb, tw);
            BaseProperty.WriteToStream(recurId, sb, tw);
            BaseProperty.WriteToStream(url, sb, tw);
            BaseProperty.WriteToStream(dateCreated, sb, tw);
            BaseProperty.WriteToStream(lastMod, sb, tw);

            if(contacts != null && contacts.Count != 0)
                foreach(ContactProperty c in contacts)
                    BaseProperty.WriteToStream(c, sb, tw);

            if(attendees != null && attendees.Count != 0)
                foreach(AttendeeProperty a in attendees)
                    BaseProperty.WriteToStream(a, sb, tw);

            if(relatedTo != null && relatedTo.Count != 0)
                foreach(RelatedToProperty r in relatedTo)
                    BaseProperty.WriteToStream(r, sb, tw);

            if(reqStats != null && reqStats.Count != 0)
                foreach(RequestStatusProperty r in reqStats)
                    BaseProperty.WriteToStream(r, sb, tw);

            base.WriteToStream(tw, sb);

            if(attachments != null && attachments.Count != 0)
                foreach(AttachProperty a in attachments)
                    BaseProperty.WriteToStream(a, sb, tw);

            if(customProps != null && customProps.Count != 0)
                foreach(CustomProperty c in customProps)
                    BaseProperty.WriteToStream(c, sb, tw);

            tw.Write("END:VJOURNAL\r\n");
        }

        /// <summary>
        /// This is overridden to allow proper comparison of journal objects
        /// </summary>
        /// <param name="obj">The object to which this instance is compared</param>
        /// <returns>Returns true if the object equals this instance, false if it does not</returns>
        public override bool Equals(object obj)
        {
            VJournal j = obj as VJournal;

            if(j == null)
                return false;

            // The ToString() method returns a text representation of the journal based on all of its settings so
            // it's a reliable way to tell if two instances are the same.
            return (this == j || this.ToString() == j.ToString());
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
