//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : VFreeBusy.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/06/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the definition for the VFreeBusy object used by vCalendar and iCalendar objects
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
    /// This class represents a VFREEBUSY component that can appear in a calendar
    /// </summary>
    [TypeDescriptionProvider(typeof(VFreeBusyTypeDescriptionProvider))]
    public class VFreeBusy : CalendarObject
    {
        #region Private data members
        //=====================================================================

        // Single free/busy properties
        private UrlProperty       url;
        private UniqueIdProperty  uid;
        private StartDateProperty startDate;
        private EndDateProperty   endDate;
        private TimeStampProperty timeStamp;
        private CommentProperty   comment;
        private OrganizerProperty organizer;
        private DurationProperty  duration;
        private ContactProperty   contact;

        // Free/busy property collections.  There can be one or more of each of these properties so they are
        // stored in a collection.
        private AttendeePropertyCollection  attendees;
        private RequestStatusPropertyCollection reqstats;
        private FreeBusyPropertyCollection  freebusy;

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
        /// This is used to get the start date/time (DTSTART) property
        /// </summary>
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
        /// This is used to get the end date/time (DTEND) property
        /// </summary>
        public EndDateProperty EndDateTime
        {
            get
            {
                if(endDate == null)
                    endDate = new EndDateProperty();

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
                if(timeStamp == null)
                {
                    timeStamp = new TimeStampProperty();
                    timeStamp.DateTimeValue = DateTime.Now;
                }

                return timeStamp;
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
        /// This is used to get the duration (DURATION) property
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
        /// This is used to get the Contact (CONTACT) property
        /// </summary>
        public ContactProperty Contact
        {
            get
            {
                if(contact == null)
                    contact = new ContactProperty();

                return contact;
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
        /// This is used to get the Request Status (REQUEST-STATUS) properties.  There may be more than one.
        /// </summary>
        /// <value>If the returned collection is empty, there are no request status properties for the object</value>
        public RequestStatusPropertyCollection RequestStatuses
        {
            get
            {
                if(reqstats == null)
                    reqstats = new RequestStatusPropertyCollection();

                return reqstats;
            }
        }

        /// <summary>
        /// This is used to get the Free/Busy (FREEBUSY) properties.  There may be more than one.
        /// </summary>
        /// <value>If the returned collection is empty, there are no free/busy properties for the object</value>
        public FreeBusyPropertyCollection FreeBusy
        {
            get
            {
                if(freebusy == null)
                    freebusy = new FreeBusyPropertyCollection();

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

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public VFreeBusy()
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
            VFreeBusy o = new VFreeBusy();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            VFreeBusy o = (VFreeBusy)p;

            this.ClearProperties();

            url = (UrlProperty)o.Url.Clone();
            uid = (UniqueIdProperty)o.UniqueId.Clone();
            startDate = (StartDateProperty)o.StartDateTime.Clone();
            endDate = (EndDateProperty)o.EndDateTime.Clone();
            timeStamp = (TimeStampProperty)o.TimeStamp.Clone();
            comment = (CommentProperty)o.Comment.Clone();
            organizer = (OrganizerProperty)o.Organizer.Clone();
            duration = (DurationProperty)o.Duration.Clone();
            contact = (ContactProperty)o.Contact.Clone();

            this.Attendees.CloneRange(o.Attendees);
            this.RequestStatuses.CloneRange(o.RequestStatuses);
            this.FreeBusy.CloneRange(o.FreeBusy);
            this.CustomProperties.CloneRange(o.CustomProperties);
        }

        /// <summary>
        /// The method can be called to clear all current property values from the free/busy object.  The version
        /// is left unchanged.
        /// </summary>
        public override void ClearProperties()
        {
            url = null;
            uid = null;
            startDate = null;
            endDate = null;
            timeStamp = null;
            comment = null;
            organizer = null;
            duration = null;
            contact = null;

            attendees = null;
            reqstats = null;
            freebusy = null;
            customProps = null;
        }

        /// <summary>
        /// This is used to propagate the version to all properties in the object that need it
        /// </summary>
        public override void PropagateVersion()
        {
            if(url != null)
                url.Version = this.Version;

            if(uid != null)
                uid.Version = this.Version;

            if(startDate != null)
                startDate.Version = this.Version;

            if(endDate != null)
                endDate.Version = this.Version;

            if(timeStamp != null)
                timeStamp.Version = this.Version;

            if(comment!= null)
                comment.Version = this.Version;

            if(organizer != null)
                organizer.Version = this.Version;

            if(duration != null)
                duration.Version = this.Version;

            if(contact != null)
                contact.Version = this.Version;

            if(attendees != null)
                attendees.PropagateVersion(this.Version);

            if(reqstats != null)
                reqstats.PropagateVersion(this.Version);

            if(freebusy != null)
                freebusy.PropagateVersion(this.Version);

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
            CalendarObject.AddTimeZoneIfUsed(startDate, timeZoneIds);
            CalendarObject.AddTimeZoneIfUsed(endDate, timeZoneIds);
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
            CalendarObject.UpdatePropertyTimeZoneId(endDate, oldId, newId);
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
            CalendarObject.ApplyPropertyTimeZone(endDate, vTimeZone);
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
            CalendarObject.SetPropertyTimeZone(endDate, vTimeZone);
        }

        /// <summary>
        /// This can be used to write a free/busy object to a PDI data stream
        /// </summary>
        /// <param name="tw">A <see cref="System.IO.TextWriter"/> derived class to which the free/busy object is
        /// written.</param>
        /// <param name="sb">A <see cref="System.Text.StringBuilder"/> used by the properties as a temporary
        /// buffer.  This can be null if the TextWriter is a <see cref="System.IO.StringWriter"/>.</param>
        /// <remarks>This is called by <see cref="CalendarObject.ToString"/> as well as owning objects when they
        /// convert themselves to a string or write themselves to a PDI data stream.</remarks>
        public override void WriteToStream(TextWriter tw, StringBuilder sb)
        {
            // DTSTAMP is always updated and written to reflect when the object was saved to the stream
            this.TimeStamp.DateTimeValue = DateTime.Now;

            PropagateVersion();

            tw.Write("BEGIN:VFREEBUSY\r\n");

            // This is a required property for iCalendar 2.0.
            BaseProperty.WriteToStream(this.UniqueId, sb, tw);

            BaseProperty.WriteToStream(organizer, sb, tw);

            if(attendees != null && attendees.Count != 0)
                foreach(AttendeeProperty a in attendees)
                    BaseProperty.WriteToStream(a, sb, tw);

            BaseProperty.WriteToStream(contact, sb, tw);
            BaseProperty.WriteToStream(startDate, sb, tw);
            BaseProperty.WriteToStream(endDate, sb, tw);
            BaseProperty.WriteToStream(duration, sb, tw);
            BaseProperty.WriteToStream(comment, sb, tw);
            BaseProperty.WriteToStream(url, sb, tw);
            BaseProperty.WriteToStream(timeStamp, sb, tw);

            if(reqstats != null && reqstats.Count != 0)
                foreach(RequestStatusProperty r in reqstats)
                    BaseProperty.WriteToStream(r, sb, tw);

            if(freebusy != null && freebusy.Count != 0)
            {
                // If there are any, they are sorted in ascending order first
                freebusy.Sort(true);

                foreach(FreeBusyProperty fb in freebusy)
                    BaseProperty.WriteToStream(fb, sb, tw);
            }

            if(customProps != null && customProps.Count != 0)
                foreach(CustomProperty c in customProps)
                    BaseProperty.WriteToStream(c, sb, tw);

            tw.Write("END:VFREEBUSY\r\n");
        }

        /// <summary>
        /// This is overridden to allow proper comparison of free/busy objects
        /// </summary>
        /// <param name="obj">The object to which this instance is compared</param>
        /// <returns>Returns true if the object equals this instance, false if it does not</returns>
        public override bool Equals(object obj)
        {
            VFreeBusy fb = obj as VFreeBusy;

            if(fb == null)
                return false;

            // The ToString() method returns a text representation of the object based on all of its settings so
            // it's a reliable way to tell if two instances are the same.
            return (this == fb || this.ToString() == fb.ToString());
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
