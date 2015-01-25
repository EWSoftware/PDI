//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : PropertyType.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/13/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the enumerated type that defines all known properties used by the Personal Data Interchange
// (PDI) parser classes.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/14/2004  EFW  Created the code
// 09/19/2007  EFW  Added support for vNote objects
//===============================================================================================================

using System;

namespace EWSoftware.PDI.Parser
{
    /// <summary>
    /// This enumerated type defines all known properties.  It is used to map property name strings to the
    /// matching property class.
    /// </summary>
    [Serializable]
    public enum PropertyType
    {
        #region Properties used by various objects
        //=====================================================================

        /// <summary>A BEGIN property</summary>
        Begin,
        /// <summary>An END property</summary>
        End,
        /// <summary>A VERSION property</summary>
        Version,
        /// <summary>A PRODID property</summary>
        ProductId,
        /// <summary>A CLASS property</summary>
        Class,
        /// <summary>A CATEGORIES property</summary>
        Categories,
        /// <summary>A RESOURCES property</summary>
        Resources,
        /// <summary>A LAST-MODIFIED property</summary>
        LastModified,
        /// <summary>A URL property</summary>
        Url,
        /// <summary>A UID property</summary>
        UniqueId,
        /// <summary>A TZ property</summary>
        TimeZone,
        /// <summary>A GEO property</summary>
        GeographicPosition,
        /// <summary>A COMMENT property</summary>
        Comment,
        /// <summary>A CONTACT property</summary>
        Contact,
        /// <summary>An ORGANIZER property</summary>
        Organizer,
        /// <summary>An ATTENDEE property</summary>
        Attendee,
        /// <summary>A RELATED-TO property</summary>
        RelatedTo,
        /// <summary>An ATTACH property</summary>
        Attachment,
        /// <summary>A RECURRENCE-ID property</summary>
        RecurrenceId,
        /// <summary>A STATUS property</summary>
        Status,
        /// <summary>A REQUEST-STATUS property</summary>
        RequestStatus,
        /// <summary>A SUMMARY property</summary>
        Summary,
        /// <summary>A DESCRIPTION property</summary>
        Description,
        /// <summary>A DURATION property</summary>
        Duration,
        /// <summary>An RRULE property</summary>
        RecurrenceRule,
        /// <summary>An RDATE property</summary>
        RecurDate,
        /// <summary>An EXRULE property</summary>
        ExceptionRule,
        /// <summary>An EXDATE property</summary>
        ExceptionDate,
        /// <summary>A custom (X-???) property</summary>
        Custom,

        #endregion

        #region vCard only
        //=====================================================================

        /// <summary>A PROFILE property</summary>
        Profile,
        /// <summary>A NAME property </summary>
        MimeName,
        /// <summary>A SOURCE property</summary>
        MimeSource,
        /// <summary>A NICKNAME property</summary>
        Nickname,
        /// <summary>A SORT-STRING property</summary>
        SortString,
        /// <summary>An FN property</summary>
        FormattedName,
        /// <summary>An N property</summary>
        Name,
        /// <summary>A TITLE property</summary>
        Title,
        /// <summary>A ROLE property</summary>
        Role,
        /// <summary>A MAILER property</summary>
        Mailer,
        /// <summary>An ORG property</summary>
        Organization,
        /// <summary>A BDAY property</summary>
        BirthDate,
        /// <summary>A REV property</summary>
        Revision,
        /// <summary>A KEY property</summary>
        PublicKey,
        /// <summary>A PHOTO property</summary>
        Photo,
        /// <summary>A LOGO property</summary>
        Logo,
        /// <summary>A SOUND property</summary>
        Sound,
        /// <summary>A NOTE property</summary>
        Note,
        /// <summary>An ADR property</summary>
        Address,
        /// <summary>A LABEL property</summary>
        Label,
        /// <summary>A TEL property</summary>
        Telephone,
        /// <summary>An EMAIL property</summary>
        EMail,
        /// <summary>An AGENT property</summary>
        Agent,

        #endregion

        #region vCalendar/iCalendar only
        //=====================================================================

        /// <summary>A CALSCALE property</summary>
        CalendarScale,
        /// <summary>A METHOD property</summary>
        Method,
        /// <summary>A DAYLIGHT property</summary>
        Daylight,

        #endregion

        #region VEvent only
        //=====================================================================

        /// <summary>A DTEND property</summary>
        EndDateTime,
        /// <summary>A LOCATION property</summary>
        Location,
        /// <summary>A TRANSP property</summary>
        Transparency,

        #endregion

        #region VToDo only
        //=====================================================================

        /// <summary>A DUE property</summary>
        DueDate,
        /// <summary>A COMPLETED property</summary>
        CompletedDate,
        /// <summary>A PERCENT-COMPLETE property</summary>
        PercentComplete,

        #endregion

        #region VEvent and VToDo only
        //=====================================================================

        /// <summary>A DCREATED/CREATED property</summary>
        DateCreated,
        /// <summary>A DTSTART property</summary>
        StartDateTime,
        /// <summary>A DTSTAMP property</summary>
        TimeStamp,
        /// <summary>A PRIORITY property</summary>
        Priority,
        /// <summary>A SEQUENCE property</summary>
        Sequence,
        /// <summary>An RNUM property</summary>
        RecurrenceCount,
        /// <summary>An AALARM property</summary>
        AudioAlarm,
        /// <summary>A DALARM property</summary>
        DisplayAlarm,
        /// <summary>An MALARM property</summary>
        EMailAlarm,
        /// <summary>A PALARM property</summary>
        ProcedureAlarm,

        #endregion

        #region VAlarm only
        //=====================================================================

        /// <summary>An ACTION property</summary>
        Action,
        /// <summary>A TRIGGER property</summary>
        Trigger,
        /// <summary>A REPEAT property</summary>
        Repeat,

        #endregion

        #region VFreeBusy only
        //=====================================================================

        /// <summary>A FREEBUSY property</summary>
        FreeBusy,

        #endregion

        #region VTimeZone only
        //=====================================================================

        /// <summary>A TZID property</summary>
        TimeZoneId,
        /// <summary>A TZURL property</summary>
        TimeZoneUrl,
        /// <summary>A TZNAME property</summary>
        TimeZoneName,
        /// <summary>A TZOFFSETFROM property</summary>
        TimeZoneOffsetFrom,
        /// <summary>A TZOFFSETTO property</summary>
        TimeZoneOffsetTo,

        #endregion

        #region Custom properties
        //=====================================================================

        /// <summary>The Exclude Start Date/Time property for recurring items</summary>
        ExcludeStartDateTime,

        #endregion

        #region IrMC 1.1 only
        //=====================================================================

        /// <summary>The BODY property for vNote items</summary>
        Body

        #endregion
    }
}
