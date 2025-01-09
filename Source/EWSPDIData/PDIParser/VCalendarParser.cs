//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : VCalendarParser.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains a class used to parse vCalendar and iCalendar Personal Data Interchange (PDI) data streams.
// It supports both the vCalendar 1.0 and iCalendar 2.0 specification file formats.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/21/2004  EFW  Created the code
// 03/17/2007  EFW  Updated for use with .NET 2.0
//===============================================================================================================

// Ignore Spelling: sr

using System;
using System.Collections.Generic;
using System.IO;

using EWSoftware.PDI.Objects;
using EWSoftware.PDI.Properties;

namespace EWSoftware.PDI.Parser
{
    /// <summary>
    /// This class implements the parser that handles vCalendar and iCalendar PDI objects
    /// </summary>
    public class VCalendarParser : PDIParser
    {
        #region Calendar parser enumerated type
        //=====================================================================

        /// <summary>
        /// This enumerated type defines the various calendar parser states
        /// </summary>
        [Serializable]
        protected enum VCalendarParserState
        {
            /// <summary>Parsing a calendar</summary>
            VCalendar,
            /// <summary>Parsing an event entry</summary>
            VEvent,
            /// <summary>Parsing a to-do entry</summary>
            VToDo,
            /// <summary>Parsing a journal entry</summary>
            VJournal,
            /// <summary>Parsing an alarm entry</summary>
            VAlarm,
            /// <summary>Parsing a free/busy entry</summary>
            VFreeBusy,
            /// <summary>Parsing a time zone entry</summary>
            VTimeZone,
            /// <summary>Parsing a time zone observance rule entry</summary>
            ObservanceRule,
            /// <summary>Parsing a custom entry</summary>
            Custom
        }
        #endregion

        #region Private data members
        //=====================================================================

        private VCalendar? vCal;            // The vCalendar/iCalendar being processed
        private VEvent?    vEvent;          // The event item being processed
        private VToDo?     vToDo;           // The to-do item being processed
        private VJournal?  vJournal;        // The journal item being processed
        private VAlarm?    vAlarm;          // The alarm item being processed
        private VFreeBusy? vFreeBusy;       // The free/busy item being processed
        private VTimeZone? vTimeZone;       // The time zone being processed
        private ObservanceRule? obsRule;    // The observance rule being processed

        // The current calendar parser state
        private VCalendarParserState currentState;

        // Prior states
        private readonly Stack<VCalendarParserState> priorState;
        private readonly Stack<string> beginValue;

        //=====================================================================
        // These private arrays are used to translate property names into property types

        // The calendar overall
        private static readonly NameToValue<PropertyType>[] ntvCal =
        [
            new("BEGIN", PropertyType.Begin),
            new("END", PropertyType.End),
            new("VERSION", PropertyType.Version),
            new("PRODID", PropertyType.ProductId),
            new("CALSCALE", PropertyType.CalendarScale),
            new("METHOD", PropertyType.Method),
            new("GEO", PropertyType.GeographicPosition),
            new("TZ", PropertyType.TimeZone),
            new("DAYLIGHT", PropertyType.Daylight),

            // The last entry should always be CustomProperty to catch all unrecognized properties.  The actual
            // property name is not relevant.
            new("X-", PropertyType.Custom)
        ];

        // The calendar objects
        private static readonly NameToValue<VCalendarParserState>[] ntvObjs =
        [
            new("VCALENDAR", VCalendarParserState.VCalendar),
            new("VEVENT", VCalendarParserState.VEvent),
            new("VTODO", VCalendarParserState.VToDo),
            new("VJOURNAL", VCalendarParserState.VJournal),
            new("VFREEBUSY", VCalendarParserState.VFreeBusy),
            new("VTIMEZONE", VCalendarParserState.VTimeZone),

            // The last entry should always be custom to catch all unrecognized objects.  The actual property
            // name is not relevant.
            new("X-", VCalendarParserState.Custom)
        ];

        // Event items
        private static readonly NameToValue<PropertyType>[] ntvEvent =
        [
            new("BEGIN", PropertyType.Begin),
            new("END", PropertyType.End),
            new("CLASS", PropertyType.Class),
            new("CATEGORIES", PropertyType.Categories),
            new("RESOURCES", PropertyType.Resources),
            new("URL", PropertyType.Url),
            new("UID", PropertyType.UniqueId),
            new("LAST-MODIFIED", PropertyType.LastModified),
            new("GEO", PropertyType.GeographicPosition),
            new("DCREATED", PropertyType.DateCreated),
            new("CREATED", PropertyType.DateCreated),
            new("DTSTART", PropertyType.StartDateTime),
            new("DTEND", PropertyType.EndDateTime),
            new("DTSTAMP", PropertyType.TimeStamp),
            new("SUMMARY", PropertyType.Summary),
            new("DESCRIPTION", PropertyType.Description),
            new("LOCATION", PropertyType.Location),
            new("PRIORITY", PropertyType.Priority),
            new("SEQUENCE", PropertyType.Sequence),
            new("TRANSP", PropertyType.Transparency),
            new("RNUM", PropertyType.RecurrenceCount),
            new("COMMENT", PropertyType.Comment),
            new("CONTACT", PropertyType.Contact),
            new("ORGANIZER", PropertyType.Organizer),
            new("ATTENDEE", PropertyType.Attendee),
            new("RELATED-TO", PropertyType.RelatedTo),
            new("ATTACH", PropertyType.Attachment),
            new("RECURRENCE-ID", PropertyType.RecurrenceId),
            new("STATUS", PropertyType.Status),
            new("REQUEST-STATUS", PropertyType.RequestStatus),
            new("DURATION", PropertyType.Duration),
            new("AALARM", PropertyType.AudioAlarm),
            new("DALARM", PropertyType.DisplayAlarm),
            new("MALARM", PropertyType.EMailAlarm),
            new("PALARM", PropertyType.ProcedureAlarm),
            new("RRULE", PropertyType.RecurrenceRule),
            new("RDATE", PropertyType.RecurDate),
            new("EXRULE", PropertyType.ExceptionRule),
            new("EXDATE", PropertyType.ExceptionDate),
            new("X-EWSOFTWARE-EXCLUDESTART", PropertyType.ExcludeStartDateTime),

            // The last entry should always be CustomProperty to catch all unrecognized properties.  The actual
            // property name is not relevant.
            new("X-", PropertyType.Custom)
        ];

        // To-Do items
        private static readonly NameToValue<PropertyType>[] ntvToDo =
        [
            new("BEGIN", PropertyType.Begin),
            new("END", PropertyType.End),
            new("CLASS", PropertyType.Class),
            new("CATEGORIES", PropertyType.Categories),
            new("RESOURCES", PropertyType.Resources),
            new("URL", PropertyType.Url),
            new("UID", PropertyType.UniqueId),
            new("LAST-MODIFIED", PropertyType.LastModified),
            new("GEO", PropertyType.GeographicPosition),
            new("DCREATED", PropertyType.DateCreated),
            new("CREATED", PropertyType.DateCreated),
            new("DUE", PropertyType.DueDate),
            new("DTSTART", PropertyType.StartDateTime),
            new("COMPLETED", PropertyType.CompletedDate),
            new("DTSTAMP", PropertyType.TimeStamp),
            new("SUMMARY", PropertyType.Summary),
            new("DESCRIPTION", PropertyType.Description),
            new("PRIORITY", PropertyType.Priority),
            new("SEQUENCE", PropertyType.Sequence),
            new("RNUM", PropertyType.RecurrenceCount),
            new("COMMENT", PropertyType.Comment),
            new("CONTACT", PropertyType.Contact),
            new("ORGANIZER", PropertyType.Organizer),
            new("ATTENDEE", PropertyType.Attendee),
            new("RELATED-TO", PropertyType.RelatedTo),
            new("ATTACH", PropertyType.Attachment),
            new("RECURRENCE-ID", PropertyType.RecurrenceId),
            new("STATUS", PropertyType.Status),
            new("REQUEST-STATUS", PropertyType.RequestStatus),
            new("PERCENT-COMPLETE", PropertyType.PercentComplete),
            new("DURATION", PropertyType.Duration),
            new("AALARM", PropertyType.AudioAlarm),
            new("DALARM", PropertyType.DisplayAlarm),
            new("MALARM", PropertyType.EMailAlarm),
            new("PALARM", PropertyType.ProcedureAlarm),
            new("RRULE", PropertyType.RecurrenceRule),
            new("RDATE", PropertyType.RecurDate),
            new("EXRULE", PropertyType.ExceptionRule),
            new("EXDATE", PropertyType.ExceptionDate),
            new("X-EWSOFTWARE-EXCLUDESTART", PropertyType.ExcludeStartDateTime),

            // The last entry should always be CustomProperty to catch all unrecognized properties.  The actual
            // property name is not relevant.
            new("X-", PropertyType.Custom)
        ];

        // Journal items
        private static readonly NameToValue<PropertyType>[] ntvJournal =
        [
            new("BEGIN", PropertyType.Begin),
            new("END", PropertyType.End),
            new("CLASS", PropertyType.Class),
            new("CATEGORIES", PropertyType.Categories),
            new("URL", PropertyType.Url),
            new("UID", PropertyType.UniqueId),
            new("LAST-MODIFIED", PropertyType.LastModified),
            new("CREATED", PropertyType.DateCreated),
            new("DTSTART", PropertyType.StartDateTime),
            new("DTSTAMP", PropertyType.TimeStamp),
            new("SUMMARY", PropertyType.Summary),
            new("DESCRIPTION", PropertyType.Description),
            new("SEQUENCE", PropertyType.Sequence),
            new("COMMENT", PropertyType.Comment),
            new("CONTACT", PropertyType.Contact),
            new("ORGANIZER", PropertyType.Organizer),
            new("ATTENDEE", PropertyType.Attendee),
            new("RELATED-TO", PropertyType.RelatedTo),
            new("ATTACH", PropertyType.Attachment),
            new("RECURRENCE-ID", PropertyType.RecurrenceId),
            new("STATUS", PropertyType.Status),
            new("REQUEST-STATUS", PropertyType.RequestStatus),
            new("RRULE", PropertyType.RecurrenceRule),
            new("RDATE", PropertyType.RecurDate),
            new("EXRULE", PropertyType.ExceptionRule),
            new("EXDATE", PropertyType.ExceptionDate),
            new("X-EWSOFTWARE-EXCLUDESTART", PropertyType.ExcludeStartDateTime),

            // The last entry should always be CustomProperty to catch all unrecognized properties.  The actual
            // property name is not relevant.
            new("X-", PropertyType.Custom)
        ];

        // VAlarm items
        private static readonly NameToValue<PropertyType>[] ntvAlarm =
        [
            new("BEGIN", PropertyType.Begin),
            new("END", PropertyType.End),
            new("ACTION", PropertyType.Action),
            new("TRIGGER", PropertyType.Trigger),
            new("REPEAT", PropertyType.Repeat),
            new("DURATION", PropertyType.Duration),
            new("SUMMARY", PropertyType.Summary),
            new("DESCRIPTION", PropertyType.Description),
            new("ATTACH", PropertyType.Attachment),
            new("ATTENDEE", PropertyType.Attendee),

            // The last entry should always be CustomProperty to catch all unrecognized properties.  The actual
            // property name is not relevant.
            new("X-", PropertyType.Custom)
        ];

        // Free/Busy items
        private static readonly NameToValue<PropertyType>[] ntvFreeBusy =
        [
            new("BEGIN", PropertyType.Begin),
            new("END", PropertyType.End),
            new("URL", PropertyType.Url),
            new("UID", PropertyType.UniqueId),
            new("DTSTART", PropertyType.StartDateTime),
            new("DTEND", PropertyType.EndDateTime),
            new("DURATION", PropertyType.Duration),
            new("DTSTAMP", PropertyType.TimeStamp),
            new("COMMENT", PropertyType.Comment),
            new("CONTACT", PropertyType.Contact),
            new("ORGANIZER", PropertyType.Organizer),
            new("ATTENDEE", PropertyType.Attendee),
            new("REQUEST-STATUS", PropertyType.RequestStatus),
            new("FREEBUSY", PropertyType.FreeBusy),

            // The last entry should always be CustomProperty to catch all unrecognized properties.  The actual
            // property name is not relevant.
            new("X-", PropertyType.Custom)
        ];

        // Time Zone items
        private static readonly NameToValue<PropertyType>[] ntvTimeZone =
        [
            new("BEGIN", PropertyType.Begin),
            new("END", PropertyType.End),
            new("TZID", PropertyType.TimeZoneId),
            new("TZURL", PropertyType.TimeZoneUrl),
            new("LAST-MODIFIED", PropertyType.LastModified),

            // The last entry should always be CustomProperty to catch all unrecognized properties.  The actual
            // property name is not relevant.
            new("X-", PropertyType.Custom)
        ];

        // Time zone observance rule items
        private static readonly NameToValue<PropertyType>[] ntvORule =
        [
            new("BEGIN", PropertyType.Begin),
            new("END", PropertyType.End),
            new("DTSTART", PropertyType.StartDateTime),
            new("TZOFFSETFROM", PropertyType.TimeZoneOffsetFrom),
            new("TZOFFSETTO", PropertyType.TimeZoneOffsetTo),
            new("COMMENT", PropertyType.Comment),
            new("RRULE", PropertyType.RecurrenceRule),
            new("RDATE", PropertyType.RecurDate),
            new("TZNAME", PropertyType.TimeZoneName),

            // The last entry should always be CustomProperty to catch all unrecognized properties.  The actual
            // property name is not relevant.
            new("X-", PropertyType.Custom)
        ];
        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to get the calendar parsed from the PDI data stream
        /// </summary>
        /// <value>The returned calendar may be a vCalendar 1.0 or an iCalendar 2.0 object.  Check the
        /// <c>Version</c> property to find out.</value>
        /// <remarks>The calendar from prior calls to the parsing methods is not cleared automatically.  Call
        /// <c>VCalendar.ClearProperties</c> before calling a parsing method if you do not want to retain the
        /// calendar information from prior runs.</remarks>
        public VCalendar? VCalendar => vCal;

        #endregion

        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public VCalendarParser()
        {
            priorState = new Stack<VCalendarParserState>();
            beginValue = new Stack<string>();
        }

        /// <summary>
        /// This version of the constructor is used when parsing calendar data that is to be stored in an
        /// existing vCalendar/iCalendar instance.
        /// </summary>
        /// <remarks>The properties in the passed calendar will be cleared</remarks>
        /// <param name="calendar">The existing calendar instance</param>
        /// <exception cref="ArgumentNullException">This is thrown if the specified calendar object is null</exception>
        protected VCalendarParser(VCalendar calendar) : this()
        {
            vCal = calendar ?? throw new ArgumentNullException(nameof(calendar),
                LR.GetString("ExParseNullObject", "vCalendar/iCalendar"));

            vCal.ClearProperties();
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is overridden to handle the additional state maintained by the calendar parser
        /// </summary>
        /// <param name="fullReset">If true, a full reset is done (i.e. this is the start of a brand new session.
        /// If false only the line state is reset (it's done parsing a property name or value).</param>
        protected override void ResetState(bool fullReset)
        {
            if(fullReset)
            {
                currentState = VCalendarParserState.VCalendar;
                vEvent = null;
                vToDo = null;
                vJournal = null;
                vAlarm = null;
                vFreeBusy = null;
                vTimeZone = null;
                obsRule = null;

                priorState.Clear();
                beginValue.Clear();
            }

            base.ResetState(fullReset);
        }

        /// <summary>
        /// This static method can be used to load property values into a new instance of a calendar from a
        /// string.
        /// </summary>
        /// <param name="calendarText">A set of calendar properties in a string</param>
        /// <returns>A new calendar instance as created from the string</returns>
        /// <example>
        /// <code language="cs">
        /// VCalendar vCal = VCalendarParser.ParseFromString(calendar);
        /// </code>
        /// <code language="vbnet">
        /// Dim vCal As VCalendar = VCalendarParser.ParseFromString(calendar)
        /// </code>
        /// </example>
        public static VCalendar ParseFromString(string calendarText)
        {
            VCalendarParser vcp = new();
            vcp.ParseString(calendarText);

            return vcp.VCalendar!;
        }

        /// <summary>
        /// This static method can be used to load property values into an existing instance of a calendar from a
        /// string.
        /// </summary>
        /// <param name="calendarText">A set of calendar properties in a string</param>
        /// <param name="calendar">The calendar instance into which the properties will be loaded</param>
        /// <remarks>The properties of the specified calendar will be cleared before the new properties are
        /// loaded into it.</remarks>
        /// <example>
        /// <code language="cs">
        /// VCalendar vCalendar = new VCalendar();
        /// VCalendarParser.ParseFromString(calendarString, vCalendar);
        /// </code>
        /// <code language="vbnet">
        /// Dim vCalendar As New VCalendar
        /// VCalendarParser.ParseFromString(calendarString, vCalendar)
        /// </code>
        /// </example>
        public static void ParseFromString(string calendarText, VCalendar calendar)
        {
            VCalendarParser vcp = new(calendar);
            vcp.ParseString(calendarText);
        }

        /// <summary>
        /// This static method can be used to load property values into a new instance of a calendar from a file.
        /// The filename can be a disk file or a URL.
        /// </summary>
        /// <param name="filename">A path or URL to a file containing or more calendar objects</param>
        /// <returns>A new calendar as created from the file</returns>
        /// <example>
        /// <code language="cs">
        /// VCalendar vCal1 = VCalendarParser.ParseFromFile(@"C:\MySchedule.ics");
        /// VCalendar vCal2 = VCalendarParser.ParseFromFile(
        ///     "http://www.mydomain.com/Calendars/MySchedule.ics");
        /// </code>
        /// <code language="vbnet">
        /// Dim vCal1 As VCalendar = VCalendarParser.ParseFromFile("C:\MySchedule.ics")
        /// Dim vCal2 As VCalendar = VCalendarParser.ParseFromFile(
        ///     "http://www.mydomain.com/Calendars/MySchedule.ics")
        /// </code>
        /// </example>
        public static VCalendar? ParseFromFile(string filename)
        {
            VCalendarParser vcp = new();
            vcp.ParseFile(filename);

            return vcp.VCalendar;
        }

        /// <summary>
        /// This static method can be used to load property values into a new instance of a calendar from a
        /// <see cref="TextReader"/> derived object such as a <see cref="StreamReader"/> or a <see cref="StringReader"/>.
        /// </summary>
        /// <param name="stream">An IO stream from which to read the vCards.  It is up to you to open the stream
        /// with the appropriate text encoding method if necessary.</param>
        /// <returns>A new calendar as created from the IO stream</returns>
        /// <example>
        /// <code language="cs">
        /// StreamReader sr = new StreamReader(@"C:\Test.ics");
        /// VCalendar vCal1 = VCalendarParser.ParseFromStream(sr);
        /// sr.Close();
        /// </code>
        /// <code language="vbnet">
        /// Dim sr As New StreamReader("C:\Test.ics")
        /// Dim vCal1 As VCalendar = VCalendarParser.ParseFromStream(sr)
        /// sr.Close()
        /// </code>
        /// </example>
        public static VCalendar? ParseFromStream(TextReader stream)
        {
            VCalendarParser vcp = new();
            vcp.ParseReader(stream);

            return vcp.VCalendar;
        }

        /// <summary>
        /// This is implemented to handle properties as they are parsed from the data stream
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="parameters">A string collection containing the parameters and their values.  If empty,
        /// there are no parameters.</param>
        /// <param name="propertyValue">The value of the property.</param>
        /// <remarks><para>There may be a mixture of name/value pairs or values alone in the parameters string
        /// collection.  It is up to the derived class to process the parameter list based on the specification
        /// to which it conforms.  For entries that are parameter names, the entry immediately following it in
        /// the collection is its associated parameter value.  The property name, parameter names, and their
        /// values may be in upper, lower, or mixed case.</para>
        /// 
        /// <para>The value may be an encoded string.  The properties are responsible for any decoding that may
        /// need to occur (i.e. base 64 encoded image data).</para></remarks>
        /// <exception cref="PDIParserException">This is thrown if an error is encountered while parsing the data
        /// stream.  Refer to the and inner exceptions for information on the cause of the problem.</exception>
        protected override void PropertyParser(string propertyName, StringCollection parameters, string propertyValue)
        {
            string temp;
            int idx;

            // Is it parsing a sub-object?
            if(currentState != VCalendarParserState.VCalendar)
            {
                switch(currentState)
                {
                    case VCalendarParserState.VEvent:
                        VEventParser(propertyName, parameters, propertyValue);
                        break;

                    case VCalendarParserState.VToDo:
                        VToDoParser(propertyName, parameters, propertyValue);
                        break;

                    case VCalendarParserState.VJournal:
                        VJournalParser(propertyName, parameters, propertyValue);
                        break;

                    case VCalendarParserState.VAlarm:
                        VAlarmParser(propertyName, parameters, propertyValue);
                        break;

                    case VCalendarParserState.VFreeBusy:
                        VFreeBusyParser(propertyName, parameters, propertyValue);
                        break;

                    case VCalendarParserState.VTimeZone:
                        VTimeZoneParser(propertyName, parameters, propertyValue);
                        break;

                    case VCalendarParserState.ObservanceRule:
                        ObservanceRuleParser(propertyName, parameters, propertyValue);
                        break;

                    case VCalendarParserState.Custom:
                        CustomObjectParser(propertyName, parameters, propertyValue);
                        break;
                }

                return;
            }

            // The last entry is always CustomProperty so scan for length minus one
            for(idx = 0; idx < ntvCal.Length - 1; idx++)
            {
                if(ntvCal[idx].IsMatch(propertyName))
                    break;
            }

            // An opening BEGIN:VCALENDAR property must have been seen
            if(vCal == null && ntvCal[idx].EnumValue != PropertyType.Begin)
            {
                throw new PDIParserException(this.LineNumber, LR.GetString("ExParseNoBeginProp", "BEGIN:VCALENDAR",
                    propertyName));
            }

            // Handle or create the property
            switch(ntvCal[idx].EnumValue)
            {
                case PropertyType.Begin:
                    // Start a new object
                    temp = propertyValue.Trim();

                    // The last entry is always Custom so scan for length - 1
                    for(idx = 0; idx < ntvObjs.Length - 1; idx++)
                    {
                        if(ntvObjs[idx].IsMatch(temp))
                            break;
                    }

                    priorState.Push(currentState);
                    currentState = ntvObjs[idx].EnumValue;

                    switch(currentState)
                    {
                        case VCalendarParserState.VCalendar:
                            // NOTE: If serializing into an existing instance, this may not be null.  If so, it
                            // is ignored. It may also exist if two calendars appear in the same file.  In that
                            // case, they will be merged into one calendar.
                            vCal ??= new VCalendar();
                            break;

                        case VCalendarParserState.VEvent:
                            vEvent = new VEvent();
                            vCal!.Events.Add(vEvent);
                            break;

                        case VCalendarParserState.VToDo:
                            vToDo = new VToDo();
                            vCal!.ToDos.Add(vToDo);
                            break;

                        case VCalendarParserState.VJournal:
                            vJournal = new VJournal();
                            vCal!.Journals.Add(vJournal);
                            break;

                        case VCalendarParserState.VFreeBusy:
                            vFreeBusy = new VFreeBusy();
                            vCal!.FreeBusys.Add(vFreeBusy);
                            break;

                        case VCalendarParserState.VTimeZone:
                            // NOTE: Unlike the other objects, time zone are not added to the collection until
                            // the END Tag is encountered as they are shared amongst all calendar instances.
                            vTimeZone = new VTimeZone();
                            break;

                        case VCalendarParserState.Custom:
                            CustomObjectParser(propertyName, parameters, propertyValue);
                            break;
                    }
                    break;

                case PropertyType.End:
                    // The value must be VCALENDAR
                    if(String.Compare(propertyValue.Trim(), "VCALENDAR", StringComparison.OrdinalIgnoreCase) != 0)
                        throw new PDIParserException(this.LineNumber, LR.GetString("ExParseUnrecognizedTagValue",
                            ntvCal[idx].Name, propertyValue));

                    // When done, we'll propagate the version number to all objects to make it consistent
                    vCal!.PropagateVersion();
                    break;

                case PropertyType.Version:
                    // Version must be 1.0 or 2.0
                    temp = propertyValue.Trim();

                    SpecificationVersions version;

                    if(temp == "1.0")
                        version = SpecificationVersions.vCalendar10;
                    else
                    {
                        if(temp == "2.0")
                            version = SpecificationVersions.iCalendar20;
                        else
                        {
                            throw new PDIParserException(this.LineNumber, LR.GetString("ExParseUnrecognizedVersion",
                                "vCalendar/iCalendar", temp));
                        }
                    }

                    vCal!.Version = version;
                    break;

                case PropertyType.ProductId:
                    vCal!.ProductId.EncodedValue = propertyValue;
                    break;

                case PropertyType.CalendarScale:
                    vCal!.CalendarScale.DeserializeParameters(parameters);
                    vCal.CalendarScale.EncodedValue = propertyValue;
                    break;

                case PropertyType.Method:
                    vCal!.Method.DeserializeParameters(parameters);
                    vCal.Method.EncodedValue = propertyValue;
                    break;

                case PropertyType.GeographicPosition:
                    vCal!.VCalendarGeographicPosition.EncodedValue = propertyValue;
                    break;

                case PropertyType.TimeZone:
                    vCal!.VCalendarTimeZone.DeserializeParameters(parameters);
                    vCal.VCalendarTimeZone.EncodedValue = propertyValue;
                    break;

                case PropertyType.Daylight:
                    vCal!.VCalendarDaylightRule.DeserializeParameters(parameters);
                    vCal.VCalendarDaylightRule.EncodedValue = propertyValue;
                    break;

                default:    // Anything else is a custom property
                    CustomProperty c = new(propertyName);
                    c.DeserializeParameters(parameters);
                    c.EncodedValue = propertyValue;
                    vCal!.CustomProperties.Add(c);
                    break;
            }
        }

        /// <summary>
        /// This is implemented to handle properties related to VEvent items
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        /// <param name="parameters">A string collection containing the parameters and their values.  If empty,
        /// there are no parameters.</param>
        /// <param name="propertyValue">The value of the property.</param>
        protected virtual void VEventParser(string propertyName, StringCollection parameters, string propertyValue)
        {
            StringCollection sc;
            string[] parts, parms;
            int idx;

            // The last entry is always CustomProperty so scan for length minus one
            for(idx = 0; idx < ntvEvent.Length - 1; idx++)
            {
                if(ntvEvent[idx].IsMatch(propertyName))
                    break;
            }

            // An opening BEGIN:VEVENT property must have been seen
            if(vEvent == null)
            {
                throw new PDIParserException(this.LineNumber, LR.GetString("ExParseNoBeginProp", "BEGIN:VEVENT",
                    propertyName));
            }

            // Handle or create the property
            switch(ntvEvent[idx].EnumValue)
            {
                case PropertyType.Begin:    // Handle nested objects
                    priorState.Push(currentState);

                    // Is it an alarm?
                    if(String.Compare(propertyValue.Trim(), "VALARM", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        vAlarm = new VAlarm();
                        vEvent.Alarms.Add(vAlarm);
                        currentState = VCalendarParserState.VAlarm;
                    }
                    else
                    {
                        // Unknown/custom object
                        currentState = VCalendarParserState.Custom;
                        CustomObjectParser(propertyName, parameters, propertyValue);
                    }
                    break;

                case PropertyType.End:
                    // For this, the value must be VEVENT
                    if(String.Compare(propertyValue.Trim(), "VEVENT", StringComparison.OrdinalIgnoreCase) != 0)
                    {
                        throw new PDIParserException(this.LineNumber, LR.GetString("ExParseUnrecognizedTagValue",
                            ntvEvent[idx].Name, propertyValue));
                    }

                    // The event is added to the collection when created so we don't have to rely on an END tag
                    // to add it.
                    vEvent = null;
                    currentState = priorState.Pop();
                    break;

                case PropertyType.Class:
                    vEvent.Classification.EncodedValue = propertyValue;
                    break;

                case PropertyType.Categories:
                    // If this is seen more than once, just add the new stuff to the existing property
                    CategoriesProperty cp = new();
                    cp.DeserializeParameters(parameters);
                    cp.EncodedValue = propertyValue;

                    foreach(string s in cp.Categories)
                        vEvent.Categories.Categories.Add(s);
                    break;

                case PropertyType.Resources:
                    // If this is seen more than once, just add the new stuff to the existing property
                    ResourcesProperty rp = new();
                    rp.DeserializeParameters(parameters);
                    rp.EncodedValue = propertyValue;

                    foreach(string s in rp.Resources)
                        vEvent.Resources.Resources.Add(s);
                    break;

                case PropertyType.Url:
                    vEvent.Url.DeserializeParameters(parameters);
                    vEvent.Url.EncodedValue = propertyValue;
                    break;

                case PropertyType.UniqueId:
                    vEvent.UniqueId.EncodedValue = propertyValue;
                    break;

                case PropertyType.LastModified:
                    vEvent.LastModified.DeserializeParameters(parameters);
                    vEvent.LastModified.EncodedValue = propertyValue;
                    break;

                case PropertyType.GeographicPosition:
                    vEvent.GeographicPosition.EncodedValue = propertyValue;
                    break;

                case PropertyType.DateCreated:
                    vEvent.DateCreated.DeserializeParameters(parameters);
                    vEvent.DateCreated.EncodedValue = propertyValue;
                    break;

                case PropertyType.StartDateTime:
                    vEvent.StartDateTime.DeserializeParameters(parameters);
                    vEvent.StartDateTime.EncodedValue = propertyValue;
                    break;

                case PropertyType.EndDateTime:
                    vEvent.EndDateTime.DeserializeParameters(parameters);
                    vEvent.EndDateTime.EncodedValue = propertyValue;
                    break;

                case PropertyType.TimeStamp:
                    vEvent.TimeStamp.DeserializeParameters(parameters);
                    vEvent.TimeStamp.EncodedValue = propertyValue;
                    break;

                case PropertyType.Summary:
                    vEvent.Summary.DeserializeParameters(parameters);
                    vEvent.Summary.EncodedValue = propertyValue;
                    break;

                case PropertyType.Description:
                    vEvent.Description.DeserializeParameters(parameters);
                    vEvent.Description.EncodedValue = propertyValue;
                    break;

                case PropertyType.Location:
                    vEvent.Location.DeserializeParameters(parameters);
                    vEvent.Location.EncodedValue = propertyValue;
                    break;

                case PropertyType.Priority:
                    vEvent.Priority.DeserializeParameters(parameters);
                    vEvent.Priority.EncodedValue = propertyValue;
                    break;

                case PropertyType.Sequence:
                    vEvent.Sequence.DeserializeParameters(parameters);
                    vEvent.Sequence.EncodedValue = propertyValue;
                    break;

                case PropertyType.Transparency:
                    vEvent.Transparency.DeserializeParameters(parameters);
                    vEvent.Transparency.EncodedValue = propertyValue;
                    break;

                case PropertyType.RecurrenceCount:
                    vEvent.RecurrenceCount.DeserializeParameters(parameters);
                    vEvent.RecurrenceCount.EncodedValue = propertyValue;
                    break;

                case PropertyType.Comment:
                    // If this is seen more than once, just add the new stuff to the existing property
                    if(vEvent.Comment.Value != null)
                    {
                        vEvent.Comment.EncodedValue += "\r\n";
                        vEvent.Comment.EncodedValue += propertyValue;
                    }
                    else
                    {
                        vEvent.Comment.DeserializeParameters(parameters);
                        vEvent.Comment.EncodedValue = propertyValue;
                    }
                    break;

                case PropertyType.Contact:
                    ContactProperty c = new();
                    c.DeserializeParameters(parameters);
                    c.EncodedValue = propertyValue;
                    vEvent.Contacts.Add(c);
                    break;

                case PropertyType.Organizer:
                    vEvent.Organizer.DeserializeParameters(parameters);
                    vEvent.Organizer.EncodedValue = propertyValue;
                    break;

                case PropertyType.Attendee:
                    AttendeeProperty ap = new();
                    ap.DeserializeParameters(parameters);
                    ap.EncodedValue = propertyValue;
                    vEvent.Attendees.Add(ap);
                    break;

                case PropertyType.RelatedTo:
                    RelatedToProperty rt = new();
                    rt.DeserializeParameters(parameters);
                    rt.EncodedValue = propertyValue;
                    vEvent.RelatedTo.Add(rt);
                    break;

                case PropertyType.Attachment:
                    AttachProperty att = new();
                    att.DeserializeParameters(parameters);
                    att.EncodedValue = propertyValue;
                    vEvent.Attachments.Add(att);
                    break;

                case PropertyType.RecurrenceId:
                    vEvent.RecurrenceId.DeserializeParameters(parameters);
                    vEvent.RecurrenceId.EncodedValue = propertyValue;
                    break;

                case PropertyType.Status:
                    vEvent.Status.DeserializeParameters(parameters);
                    vEvent.Status.EncodedValue = propertyValue;
                    break;

                case PropertyType.RequestStatus:
                    RequestStatusProperty rs = new();
                    rs.DeserializeParameters(parameters);
                    rs.EncodedValue = propertyValue;
                    vEvent.RequestStatuses.Add(rs);
                    break;

                case PropertyType.Duration:
                    vEvent.Duration.DeserializeParameters(parameters);
                    vEvent.Duration.EncodedValue = propertyValue;
                    break;

                case PropertyType.AudioAlarm:
                case PropertyType.DisplayAlarm:
                case PropertyType.EMailAlarm:
                case PropertyType.ProcedureAlarm:
                    // These are converted to a VAlarm object
                    vAlarm = new VAlarm();
                    ParseVCalendarAlarm(ntvEvent[idx].EnumValue, parameters, propertyValue);
                    vEvent.Alarms.Add(vAlarm);
                    vAlarm = null;
                    break;

                case PropertyType.RecurrenceRule:
                    RRuleProperty rr = new();
                    rr.DeserializeParameters(parameters);
                    rr.EncodedValue = propertyValue;
                    vEvent.RecurrenceRules.Add(rr);
                    break;

                case PropertyType.RecurDate:
                    // There may be more than one date in the value.  If so, split them into separate ones.  This
                    // makes it easier to manage.  They'll get written back out as individual properties but
                    // that's okay.
                    parts = propertyValue.Split(',', ';');

                    // It's important that we retain the same parameters for each one
                    parms = new string[parameters.Count];
                    parameters.CopyTo(parms, 0);

                    foreach(string s in parts)
                    {
                        sc = [.. parms];

                        RDateProperty rd = new();
                        rd.DeserializeParameters(sc);
                        rd.EncodedValue = s;

                        vEvent.RecurDates.Add(rd);
                    }
                    break;

                case PropertyType.ExceptionRule:
                    ExRuleProperty er = new();
                    er.DeserializeParameters(parameters);
                    er.EncodedValue = propertyValue;
                    vEvent.ExceptionRules.Add(er);
                    break;

                case PropertyType.ExceptionDate:
                    // There may be more than one date in the value.  If so, split them into separate ones.  This
                    // makes it easier to manage.  They'll get written back out as individual properties but
                    // that's okay.
                    parts = propertyValue.Split(',', ';');

                    // It's important that we retain the same parameters for each one
                    parms = new string[parameters.Count];
                    parameters.CopyTo(parms, 0);

                    foreach(string s in parts)
                    {
                        sc = [.. parms];

                        ExDateProperty ed = new();
                        ed.DeserializeParameters(sc);
                        ed.EncodedValue = s;

                        vEvent.ExceptionDates.Add(ed);
                    }
                    break;

                case PropertyType.ExcludeStartDateTime:
                    // This is a custom property not defined by the spec
                    vEvent.ExcludeStartDateTime = (propertyValue[0] == '1');
                    break;

                default:    // Anything else is a custom property
                    CustomProperty cust = new(propertyName);
                    cust.DeserializeParameters(parameters);
                    cust.EncodedValue = propertyValue;
                    vEvent.CustomProperties.Add(cust);
                    break;
            }
        }

        /// <summary>
        /// This is implemented to handle properties related to VToDo items
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="parameters">A string collection containing the parameters and their values.  If empty,
        /// there are no parameters.</param>
        /// <param name="propertyValue">The value of the property.</param>
        protected virtual void VToDoParser(string propertyName, StringCollection parameters, string propertyValue)
        {
            StringCollection sc;
            string[] parts, parms;
            int idx;

            // The last entry is always CustomProperty so scan for length minus one
            for(idx = 0; idx < ntvToDo.Length - 1; idx++)
            {
                if(ntvToDo[idx].IsMatch(propertyName))
                    break;
            }

            // An opening BEGIN:VTODO property must have been seen
            if(vToDo == null)
            {
                throw new PDIParserException(this.LineNumber, LR.GetString("ExParseNoBeginProp", "BEGIN:VTODO",
                    propertyName));
            }

            // Handle or create the property
            switch(ntvToDo[idx].EnumValue)
            {
                case PropertyType.Begin:    // Handle nested objects
                    priorState.Push(currentState);

                    // Is it an alarm?
                    if(String.Compare(propertyValue.Trim(), "VALARM", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        vAlarm = new VAlarm();
                        vToDo.Alarms.Add(vAlarm);
                        currentState = VCalendarParserState.VAlarm;
                    }
                    else
                    {
                        // Unknown/custom object
                        currentState = VCalendarParserState.Custom;
                        CustomObjectParser(propertyName, parameters, propertyValue);
                    }
                    break;

                case PropertyType.End:
                    // For this, the value must be VTODO
                    if(String.Compare(propertyValue.Trim(), "VTODO", StringComparison.OrdinalIgnoreCase) != 0)
                        throw new PDIParserException(this.LineNumber, LR.GetString("ExParseUnrecognizedTagValue",
                            ntvToDo[idx].Name, propertyValue));

                    // The To-Do is added to the collection when created so we don't have to rely on an END tag
                    // to add it.
                    vToDo = null;
                    currentState = priorState.Pop();
                    break;

                case PropertyType.Class:
                    vToDo.Classification.EncodedValue = propertyValue;
                    break;

                case PropertyType.Categories:
                    // If this is seen more than once, just add the new stuff to the existing property
                    CategoriesProperty cp = new();
                    cp.DeserializeParameters(parameters);
                    cp.EncodedValue = propertyValue;

                    foreach(string s in cp.Categories)
                        vToDo.Categories.Categories.Add(s);
                    break;

                case PropertyType.Resources:
                    // If this is seen more than once, just add the new stuff to the existing property
                    ResourcesProperty rp = new();
                    rp.DeserializeParameters(parameters);
                    rp.EncodedValue = propertyValue;

                    foreach(string s in rp.Resources)
                        vToDo.Resources.Resources.Add(s);
                    break;

                case PropertyType.Url:
                    vToDo.Url.DeserializeParameters(parameters);
                    vToDo.Url.EncodedValue = propertyValue;
                    break;

                case PropertyType.UniqueId:
                    vToDo.UniqueId.EncodedValue = propertyValue;
                    break;

                case PropertyType.LastModified:
                    vToDo.LastModified.DeserializeParameters(parameters);
                    vToDo.LastModified.EncodedValue = propertyValue;
                    break;

                case PropertyType.GeographicPosition:
                    vToDo.GeographicPosition.EncodedValue = propertyValue;
                    break;

                case PropertyType.DateCreated:
                    vToDo.DateCreated.DeserializeParameters(parameters);
                    vToDo.DateCreated.EncodedValue = propertyValue;
                    break;

                case PropertyType.DueDate:
                    vToDo.DueDateTime.DeserializeParameters(parameters);
                    vToDo.DueDateTime.EncodedValue = propertyValue;
                    break;

                case PropertyType.StartDateTime:
                    vToDo.StartDateTime.DeserializeParameters(parameters);
                    vToDo.StartDateTime.EncodedValue = propertyValue;
                    break;

                case PropertyType.CompletedDate:
                    vToDo.CompletedDateTime.DeserializeParameters(parameters);
                    vToDo.CompletedDateTime.EncodedValue = propertyValue;
                    break;

                case PropertyType.TimeStamp:
                    vToDo.TimeStamp.DeserializeParameters(parameters);
                    vToDo.TimeStamp.EncodedValue = propertyValue;
                    break;

                case PropertyType.Summary:
                    vToDo.Summary.DeserializeParameters(parameters);
                    vToDo.Summary.EncodedValue = propertyValue;
                    break;

                case PropertyType.Description:
                    vToDo.Description.DeserializeParameters(parameters);
                    vToDo.Description.EncodedValue = propertyValue;
                    break;

                case PropertyType.Priority:
                    vToDo.Priority.DeserializeParameters(parameters);
                    vToDo.Priority.EncodedValue = propertyValue;
                    break;

                case PropertyType.Sequence:
                    vToDo.Sequence.DeserializeParameters(parameters);
                    vToDo.Sequence.EncodedValue = propertyValue;
                    break;

                case PropertyType.RecurrenceCount:
                    vToDo.RecurrenceCount.DeserializeParameters(parameters);
                    vToDo.RecurrenceCount.EncodedValue = propertyValue;
                    break;

                case PropertyType.Comment:
                    // If this is seen more than once, just add the new stuff to the existing property
                    if(vToDo.Comment.Value != null)
                    {
                        vToDo.Comment.EncodedValue += "\r\n";
                        vToDo.Comment.EncodedValue += propertyValue;
                    }
                    else
                    {
                        vToDo.Comment.DeserializeParameters(parameters);
                        vToDo.Comment.EncodedValue = propertyValue;
                    }
                    break;

                case PropertyType.Contact:
                    ContactProperty cont = new();
                    cont.DeserializeParameters(parameters);
                    cont.EncodedValue = propertyValue;
                    vToDo.Contacts.Add(cont);
                    break;

                case PropertyType.Organizer:
                    vToDo.Organizer.DeserializeParameters(parameters);
                    vToDo.Organizer.EncodedValue = propertyValue;
                    break;

                case PropertyType.Attendee:
                    AttendeeProperty ap = new();
                    ap.DeserializeParameters(parameters);
                    ap.EncodedValue = propertyValue;
                    vToDo.Attendees.Add(ap);
                    break;

                case PropertyType.RelatedTo:
                    RelatedToProperty rt = new();
                    rt.DeserializeParameters(parameters);
                    rt.EncodedValue = propertyValue;
                    vToDo.RelatedTo.Add(rt);
                    break;

                case PropertyType.Attachment:
                    AttachProperty att = new();
                    att.DeserializeParameters(parameters);
                    att.EncodedValue = propertyValue;
                    vToDo.Attachments.Add(att);
                    break;

                case PropertyType.RecurrenceId:
                    vToDo.RecurrenceId.DeserializeParameters(parameters);
                    vToDo.RecurrenceId.EncodedValue = propertyValue;
                    break;

                case PropertyType.Status:
                    vToDo.Status.DeserializeParameters(parameters);
                    vToDo.Status.EncodedValue = propertyValue;
                    break;

                case PropertyType.RequestStatus:
                    RequestStatusProperty rs = new();
                    rs.DeserializeParameters(parameters);
                    rs.EncodedValue = propertyValue;
                    vToDo.RequestStatuses.Add(rs);
                    break;

                case PropertyType.PercentComplete:
                    vToDo.PercentComplete.DeserializeParameters(parameters);
                    vToDo.PercentComplete.EncodedValue = propertyValue;
                    break;

                case PropertyType.Duration:
                    vToDo.Duration.DeserializeParameters(parameters);
                    vToDo.Duration.EncodedValue = propertyValue;
                    break;

                case PropertyType.AudioAlarm:
                case PropertyType.DisplayAlarm:
                case PropertyType.EMailAlarm:
                case PropertyType.ProcedureAlarm:
                    // These are converted to a VAlarm object
                    vAlarm = new VAlarm();
                    ParseVCalendarAlarm(ntvToDo[idx].EnumValue, parameters, propertyValue);
                    vToDo.Alarms.Add(vAlarm);
                    vAlarm = null;
                    break;

                case PropertyType.RecurrenceRule:
                    RRuleProperty rr = new();
                    rr.DeserializeParameters(parameters);
                    rr.EncodedValue = propertyValue;
                    vToDo.RecurrenceRules.Add(rr);
                    break;

                case PropertyType.RecurDate:
                    // There may be more than one date in the value.  If so, split them into separate ones.  This
                    // makes it easier to manage.  They'll get written back out as individual properties but
                    // that's okay.
                    parts = propertyValue.Split(',', ';');

                    // It's important that we retain the same parameters for each one
                    parms = new string[parameters.Count];
                    parameters.CopyTo(parms, 0);

                    foreach(string s in parts)
                    {
                        sc = [.. parms];

                        RDateProperty rd = new();
                        rd.DeserializeParameters(sc);
                        rd.EncodedValue = s;

                        vToDo.RecurDates.Add(rd);
                    }
                    break;

                case PropertyType.ExceptionRule:
                    ExRuleProperty er = new();
                    er.DeserializeParameters(parameters);
                    er.EncodedValue = propertyValue;
                    vToDo.ExceptionRules.Add(er);
                    break;

                case PropertyType.ExceptionDate:
                    // There may be more than one date in the value.  If so, split them into separate ones.  This
                    // makes it easier to manage.  They'll get written back out as individual properties but
                    // that's okay.
                    parts = propertyValue.Split(',', ';');

                    // It's important that we retain the same parameters for each one
                    parms = new string[parameters.Count];
                    parameters.CopyTo(parms, 0);

                    foreach(string s in parts)
                    {
                        sc = [.. parms];

                        ExDateProperty ed = new();
                        ed.DeserializeParameters(sc);
                        ed.EncodedValue = s;

                        vToDo.ExceptionDates.Add(ed);
                    }
                    break;

                case PropertyType.ExcludeStartDateTime:
                    // This is a custom property not defined by the spec
                    vToDo.ExcludeStartDateTime = (propertyValue[0] == '1');
                    break;

                default:    // Anything else is a custom property
                    CustomProperty cust = new(propertyName);
                    cust.DeserializeParameters(parameters);
                    cust.EncodedValue = propertyValue;
                    vToDo.CustomProperties.Add(cust);
                    break;
            }
        }

        /// <summary>
        /// This is implemented to handle properties related to VJournal items
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="parameters">A string collection containing the parameters and their values.  If empty,
        /// there are no parameters.</param>
        /// <param name="propertyValue">The value of the property.</param>
        protected virtual void VJournalParser(string propertyName, StringCollection parameters, string propertyValue)
        {
            StringCollection sc;
            string[] parts, parms;
            int idx;

            // The last entry is always CustomProperty so scan for length minus one
            for(idx = 0; idx < ntvJournal.Length - 1; idx++)
            {
                if(ntvJournal[idx].IsMatch(propertyName))
                    break;
            }

            // An opening BEGIN:VJOURNAL property must have been seen
            if(vJournal == null)
            {
                throw new PDIParserException(this.LineNumber, LR.GetString("ExParseNoBeginProp", "BEGIN:VJOURNAL",
                    propertyName));
            }

            // Handle or create the property
            switch(ntvJournal[idx].EnumValue)
            {
                case PropertyType.Begin:    // Handle unknown nested objects
                    priorState.Push(currentState);
                    currentState = VCalendarParserState.Custom;
                    CustomObjectParser(propertyName, parameters, propertyValue);
                    break;

                case PropertyType.End:
                    // For this, the value must be VJOURNAL
                    if(String.Compare(propertyValue.Trim(), "VJOURNAL", StringComparison.OrdinalIgnoreCase) != 0)
                    {
                        throw new PDIParserException(this.LineNumber, LR.GetString("ExParseUnrecognizedTagValue",
                            ntvJournal[idx].Name, propertyValue));
                    }

                    // The journal is added to the collection when created so we don't have to rely on an END tag
                    // to add it.
                    vJournal = null;
                    currentState = priorState.Pop();
                    break;

                case PropertyType.Class:
                    vJournal.Classification.EncodedValue = propertyValue;
                    break;

                case PropertyType.Categories:
                    // If this is seen more than once, just add the new stuff to the existing property
                    CategoriesProperty cp = new();
                    cp.DeserializeParameters(parameters);
                    cp.EncodedValue = propertyValue;

                    foreach(string s in cp.Categories)
                        vJournal.Categories.Categories.Add(s);
                    break;

                case PropertyType.Url:
                    vJournal.Url.DeserializeParameters(parameters);
                    vJournal.Url.EncodedValue = propertyValue;
                    break;

                case PropertyType.UniqueId:
                    vJournal.UniqueId.EncodedValue = propertyValue;
                    break;

                case PropertyType.LastModified:
                    vJournal.LastModified.DeserializeParameters(parameters);
                    vJournal.LastModified.EncodedValue = propertyValue;
                    break;

                case PropertyType.DateCreated:
                    vJournal.DateCreated.DeserializeParameters(parameters);
                    vJournal.DateCreated.EncodedValue = propertyValue;
                    break;

                case PropertyType.StartDateTime:
                    vJournal.StartDateTime.DeserializeParameters(parameters);
                    vJournal.StartDateTime.EncodedValue = propertyValue;
                    break;

                case PropertyType.TimeStamp:
                    vJournal.TimeStamp.DeserializeParameters(parameters);
                    vJournal.TimeStamp.EncodedValue = propertyValue;
                    break;

                case PropertyType.Summary:
                    vJournal.Summary.DeserializeParameters(parameters);
                    vJournal.Summary.EncodedValue = propertyValue;
                    break;

                case PropertyType.Description:
                    vJournal.Description.DeserializeParameters(parameters);
                    vJournal.Description.EncodedValue = propertyValue;
                    break;

                case PropertyType.Sequence:
                    vJournal.Sequence.DeserializeParameters(parameters);
                    vJournal.Sequence.EncodedValue = propertyValue;
                    break;

                case PropertyType.Comment:
                    // If this is seen more than once, just add the new stuff to the existing property
                    if(vJournal.Comment.Value != null)
                    {
                        vJournal.Comment.EncodedValue += "\r\n";
                        vJournal.Comment.EncodedValue += propertyValue;
                    }
                    else
                    {
                        vJournal.Comment.DeserializeParameters(parameters);
                        vJournal.Comment.EncodedValue = propertyValue;
                    }
                    break;

                case PropertyType.Contact:
                    ContactProperty cont = new();
                    cont.DeserializeParameters(parameters);
                    cont.EncodedValue = propertyValue;
                    vJournal.Contacts.Add(cont);
                    break;

                case PropertyType.Organizer:
                    vJournal.Organizer.DeserializeParameters(parameters);
                    vJournal.Organizer.EncodedValue = propertyValue;
                    break;

                case PropertyType.Attendee:
                    AttendeeProperty ap = new();
                    ap.DeserializeParameters(parameters);
                    ap.EncodedValue = propertyValue;
                    vJournal.Attendees.Add(ap);
                    break;

                case PropertyType.RelatedTo:
                    RelatedToProperty rt = new();
                    rt.DeserializeParameters(parameters);
                    rt.EncodedValue = propertyValue;
                    vJournal.RelatedTo.Add(rt);
                    break;

                case PropertyType.Attachment:
                    AttachProperty att = new();
                    att.DeserializeParameters(parameters);
                    att.EncodedValue = propertyValue;
                    vJournal.Attachments.Add(att);
                    break;

                case PropertyType.RecurrenceId:
                    vJournal.RecurrenceId.DeserializeParameters(parameters);
                    vJournal.RecurrenceId.EncodedValue = propertyValue;
                    break;

                case PropertyType.Status:
                    vJournal.Status.DeserializeParameters(parameters);
                    vJournal.Status.EncodedValue = propertyValue;
                    break;

                case PropertyType.RequestStatus:
                    RequestStatusProperty rs = new();
                    rs.DeserializeParameters(parameters);
                    rs.EncodedValue = propertyValue;
                    vJournal.RequestStatuses.Add(rs);
                    break;

                case PropertyType.RecurrenceRule:
                    RRuleProperty rr = new();
                    rr.DeserializeParameters(parameters);
                    rr.EncodedValue = propertyValue;
                    vJournal.RecurrenceRules.Add(rr);
                    break;

                case PropertyType.RecurDate:
                    // There may be more than one date in the value.  If so, split them into separate ones.  This
                    // makes it easier to manage.  They'll get written back out as individual properties but
                    // that's okay.
                    parts = propertyValue.Split(',', ';');

                    // It's important that we retain the same parameters for each one
                    parms = new string[parameters.Count];
                    parameters.CopyTo(parms, 0);

                    foreach(string s in parts)
                    {
                        sc = [.. parms];

                        RDateProperty rd = new();
                        rd.DeserializeParameters(sc);
                        rd.EncodedValue = s;

                        vJournal.RecurDates.Add(rd);
                    }
                    break;

                case PropertyType.ExceptionRule:
                    ExRuleProperty er = new();
                    er.DeserializeParameters(parameters);
                    er.EncodedValue = propertyValue;
                    vJournal.ExceptionRules.Add(er);
                    break;

                case PropertyType.ExceptionDate:
                    // There may be more than one date in the value.  If so, split them into separate ones.  This
                    // makes it easier to manage.  They'll get written back out as individual properties but
                    // that's okay.
                    parts = propertyValue.Split(',', ';');

                    // It's important that we retain the same parameters for each one
                    parms = new string[parameters.Count];
                    parameters.CopyTo(parms, 0);

                    foreach(string s in parts)
                    {
                        sc = [.. parms];

                        ExDateProperty ed = new();
                        ed.DeserializeParameters(sc);
                        ed.EncodedValue = s;

                        vJournal.ExceptionDates.Add(ed);
                    }
                    break;

                case PropertyType.ExcludeStartDateTime:
                    // This is a custom property not defined by the spec
                    vJournal.ExcludeStartDateTime = (propertyValue[0] == '1');
                    break;

                default:    // Anything else is a custom property
                    CustomProperty cust = new(propertyName);
                    cust.DeserializeParameters(parameters);
                    cust.EncodedValue = propertyValue;
                    vJournal.CustomProperties.Add(cust);
                    break;
            }
        }

        /// <summary>
        /// This is implemented to handle properties related to VTimeZone items
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="parameters">A string collection containing the parameters and their values.  If empty,
        /// there are no parameters.</param>
        /// <param name="propertyValue">The value of the property.</param>
        protected virtual void VTimeZoneParser(string propertyName, StringCollection parameters, string propertyValue)
        {
            int idx;

            // The last entry is always CustomProperty so scan for length minus one
            for(idx = 0; idx < ntvTimeZone.Length - 1; idx++)
            {
                if(ntvTimeZone[idx].IsMatch(propertyName))
                    break;
            }

            // An opening BEGIN:VTIMEZONE property must have been seen
            if(vTimeZone == null)
            {
                throw new PDIParserException(this.LineNumber, LR.GetString("ExParseNoBeginProp", "BEGIN:VTIMEZONE",
                    propertyName));
            }

            // Handle or create the property
            switch(ntvTimeZone[idx].EnumValue)
            {
                case PropertyType.Begin:    // Handle nested objects
                    priorState.Push(currentState);

                    // Is it a STANDARD or DAYLIGHT property?
                    if(String.Compare(propertyValue.Trim(), "STANDARD", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        obsRule = new ObservanceRule(ObservanceRuleType.Standard);
                        vTimeZone.ObservanceRules.Add(obsRule);
                        currentState = VCalendarParserState.ObservanceRule;
                    }
                    else
                    {
                        if(String.Compare(propertyValue.Trim(), "DAYLIGHT", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            obsRule = new ObservanceRule(ObservanceRuleType.Daylight);
                            vTimeZone.ObservanceRules.Add(obsRule);
                            currentState = VCalendarParserState.ObservanceRule;
                        }
                        else
                        {
                            // Unknown/custom object
                            currentState = VCalendarParserState.Custom;
                            CustomObjectParser(propertyName, parameters, propertyValue);
                        }
                    }
                    break;

                case PropertyType.End:
                    // For this, the value must be VTIMEZONE
                    if(String.Compare(propertyValue.Trim(), "VTIMEZONE", StringComparison.OrdinalIgnoreCase) != 0)
                    {
                        throw new PDIParserException(this.LineNumber, LR.GetString("ExParseUnrecognizedTagValue",
                            ntvTimeZone[idx].Name, propertyValue));
                    }

                    // Unlike other objects, we do rely on the end tag to add the time zone object to the
                    // collection as they are shared amongst all instances.
                    VCalendar.TimeZones.Merge(vTimeZone);

                    vTimeZone = null;
                    currentState = priorState.Pop();
                    break;

                case PropertyType.TimeZoneId:
                    vTimeZone.TimeZoneId.EncodedValue = propertyValue;
                    break;

                case PropertyType.TimeZoneUrl:
                    vTimeZone.TimeZoneUrl.EncodedValue = propertyValue;
                    break;

                case PropertyType.LastModified:
                    vTimeZone.LastModified.DeserializeParameters(parameters);
                    vTimeZone.LastModified.EncodedValue = propertyValue;
                    break;

                default:    // Anything else is a custom property
                    CustomProperty cust = new(propertyName);
                    cust.DeserializeParameters(parameters);
                    cust.EncodedValue = propertyValue;
                    vTimeZone.CustomProperties.Add(cust);
                    break;
            }
        }

        /// <summary>
        /// This is implemented to handle properties related to VAlarm items
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="parameters">A string collection containing the parameters and their values.  If empty,
        /// there are no parameters.</param>
        /// <param name="propertyValue">The value of the property.</param>
        protected virtual void VAlarmParser(string propertyName, StringCollection parameters, string propertyValue)
        {
            int idx;

            // The last entry is always CustomProperty so scan for length minus one
            for(idx = 0; idx < ntvAlarm.Length - 1; idx++)
            {
                if(ntvAlarm[idx].IsMatch(propertyName))
                    break;
            }

            // An opening BEGIN:VALARM property must have been seen
            if(vAlarm == null)
            {
                throw new PDIParserException(this.LineNumber, LR.GetString("ExParseNoBeginProp", "BEGIN:VALARM",
                    propertyName));
            }

            // Handle or create the property
            switch(ntvAlarm[idx].EnumValue)
            {
                case PropertyType.Begin:    // Handle unknown nested objects
                    priorState.Push(currentState);
                    currentState = VCalendarParserState.Custom;
                    CustomObjectParser(propertyName, parameters, propertyValue);
                    break;

                case PropertyType.End:
                    // For this, the value must be VALARM
                    if(String.Compare(propertyValue.Trim(), "VALARM", StringComparison.OrdinalIgnoreCase) != 0)
                    {
                        throw new PDIParserException(this.LineNumber, LR.GetString("ExParseUnrecognizedTagValue",
                            ntvAlarm[idx].Name, propertyValue));
                    }

                    // The alarm is added to the collection when created so we don't have to rely on an END tag
                    // to add it.
                    vAlarm = null;
                    currentState = priorState.Pop();
                    break;

                case PropertyType.Action:
                    vAlarm.Action.DeserializeParameters(parameters);
                    vAlarm.Action.EncodedValue = propertyValue;
                    break;

                case PropertyType.Trigger:
                    vAlarm.Trigger.DeserializeParameters(parameters);
                    vAlarm.Trigger.EncodedValue = propertyValue;
                    break;

                case PropertyType.Repeat:
                    vAlarm.Repeat.DeserializeParameters(parameters);
                    vAlarm.Repeat.EncodedValue = propertyValue;
                    break;

                case PropertyType.Duration:
                    vAlarm.Duration.DeserializeParameters(parameters);
                    vAlarm.Duration.EncodedValue = propertyValue;
                    break;

                case PropertyType.Summary:
                    vAlarm.Summary.DeserializeParameters(parameters);
                    vAlarm.Summary.EncodedValue = propertyValue;
                    break;

                case PropertyType.Description:
                    vAlarm.Description.DeserializeParameters(parameters);
                    vAlarm.Description.EncodedValue = propertyValue;
                    break;

                case PropertyType.Attendee:
                    AttendeeProperty ap = new();
                    ap.DeserializeParameters(parameters);
                    ap.EncodedValue = propertyValue;
                    vAlarm.Attendees.Add(ap);
                    break;

                case PropertyType.Attachment:
                    AttachProperty att = new();
                    att.DeserializeParameters(parameters);
                    att.EncodedValue = propertyValue;
                    vAlarm.Attachments.Add(att);
                    break;

                default:    // Anything else is a custom property
                    CustomProperty cust = new(propertyName);
                    cust.DeserializeParameters(parameters);
                    cust.EncodedValue = propertyValue;
                    vAlarm.CustomProperties.Add(cust);
                    break;
            }
        }

        /// <summary>
        /// This is used to parse vCalendar 1.0 alarm properties into a VAlarm object
        /// </summary>
        /// <param name="propertyType">The property type</param>
        /// <param name="parameters">The string collection containing the parameter values</param>
        /// <param name="propertyValue">The value of the property</param>
        /// <remarks>The VAlarm object has the ability to write itself out as a vCalendar 1.0 alarm type property
        /// or an iCalendar 2.0 VALARM component.  As such, individual properties do not exist for the vCalendar
        /// 1.0 AALARM, DALARM, MALARM, and PALARM properties.</remarks>
        protected virtual void ParseVCalendarAlarm(PropertyType propertyType, StringCollection parameters,
          string propertyValue)
        {
            string[] parts = propertyValue.Split(';');

            if(parts.Length != 0 && parts[0].Length > 0)
                vAlarm!.Trigger.EncodedValue = parts[0];

            if(parts.Length > 1 && parts[1].Length > 0)
                vAlarm!.Duration.EncodedValue = parts[1];

            if(parts.Length > 2 && parts[2].Length > 0)
                vAlarm!.Repeat.EncodedValue = parts[2];

            switch(propertyType)
            {
                case PropertyType.AudioAlarm:
                    vAlarm!.Action.Action = AlarmAction.Audio;

                    if(parts.Length > 3 && parts[3].Length > 0)
                    {
                        AttachProperty att = new();
                        att.DeserializeParameters(parameters);
                        att.EncodedValue = parts[3];
                        vAlarm.Attachments.Add(att);
                    }
                    break;

                case PropertyType.DisplayAlarm:
                    vAlarm!.Action.Action = AlarmAction.Display;

                    if(parts.Length > 3 && parts[3].Length > 0)
                    {
                        vAlarm.Description.DeserializeParameters(parameters);
                        vAlarm.Description.EncodedValue = parts[3];
                    }
                    break;

                case PropertyType.EMailAlarm:
                    vAlarm!.Action.Action = AlarmAction.EMail;

                    if(parts.Length > 3 && parts[3].Length > 0)
                    {
                        AttendeeProperty ap = new();
                        ap.DeserializeParameters(parameters);
                        ap.EncodedValue = parts[3];
                        vAlarm.Attendees.Add(ap);
                    }

                    if(parts.Length > 4 && parts[4].Length > 0)
                        vAlarm.Description.EncodedValue = parts[4];
                    break;

                case PropertyType.ProcedureAlarm:
                    vAlarm!.Action.Action = AlarmAction.Procedure;

                    if(parts.Length > 3 && parts[3].Length > 0)
                    {
                        AttachProperty att = new();
                        att.DeserializeParameters(parameters);
                        att.EncodedValue = parts[3];
                        vAlarm.Attachments.Add(att);
                    }
                    break;
            }
        }

        /// <summary>
        /// This is implemented to handle properties related to VFreeBusy items
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="parameters">A string collection containing the parameters and their values.  If empty,
        /// there are no parameters.</param>
        /// <param name="propertyValue">The value of the property.</param>
        protected virtual void VFreeBusyParser(string propertyName, StringCollection parameters, string propertyValue)
        {
            StringCollection sc;
            string[] parts, parms;
            int idx;

            // The last entry is always CustomProperty so scan for length minus one
            for(idx = 0; idx < ntvFreeBusy.Length - 1; idx++)
            {
                if(ntvFreeBusy[idx].IsMatch(propertyName))
                    break;
            }

            // An opening BEGIN:VFREEBUSY property must have been seen
            if(vFreeBusy == null)
            {
                throw new PDIParserException(this.LineNumber, LR.GetString("ExParseNoBeginProp", "BEGIN:VFREEBUSY",
                    propertyName));
            }

            // Handle or create the property
            switch(ntvFreeBusy[idx].EnumValue)
            {
                case PropertyType.Begin:    // Handle unknown nested objects
                    priorState.Push(currentState);
                    currentState = VCalendarParserState.Custom;
                    CustomObjectParser(propertyName, parameters, propertyValue);
                    break;

                case PropertyType.End:
                    // For this, the value must be VFREEBUSY
                    if(String.Compare(propertyValue.Trim(), "VFREEBUSY", StringComparison.OrdinalIgnoreCase) != 0)
                    {
                        throw new PDIParserException(this.LineNumber, LR.GetString("ExParseUnrecognizedTagValue",
                            ntvFreeBusy[idx].Name, propertyValue));
                    }

                    // The free/busy item is added to the collection when created so we don't have to rely on an
                    // END tag to add it.
                    vFreeBusy = null;
                    currentState = priorState.Pop();
                    break;

                case PropertyType.Url:
                    vFreeBusy.Url.DeserializeParameters(parameters);
                    vFreeBusy.Url.EncodedValue = propertyValue;
                    break;

                case PropertyType.UniqueId:
                    vFreeBusy.UniqueId.EncodedValue = propertyValue;
                    break;

                case PropertyType.StartDateTime:
                    vFreeBusy.StartDateTime.DeserializeParameters(parameters);
                    vFreeBusy.StartDateTime.EncodedValue = propertyValue;
                    break;

                case PropertyType.EndDateTime:
                    vFreeBusy.EndDateTime.DeserializeParameters(parameters);
                    vFreeBusy.EndDateTime.EncodedValue = propertyValue;
                    break;

                case PropertyType.Duration:
                    vFreeBusy.Duration.DeserializeParameters(parameters);
                    vFreeBusy.Duration.EncodedValue = propertyValue;
                    break;

                case PropertyType.TimeStamp:
                    vFreeBusy.TimeStamp.DeserializeParameters(parameters);
                    vFreeBusy.TimeStamp.EncodedValue = propertyValue;
                    break;

                case PropertyType.Comment:
                    // If this is seen more than once, just add the new stuff to the existing property
                    if(vFreeBusy.Comment.Value != null)
                    {
                        vFreeBusy.Comment.EncodedValue += "\r\n";
                        vFreeBusy.Comment.EncodedValue += propertyValue;
                    }
                    else
                    {
                        vFreeBusy.Comment.DeserializeParameters(parameters);
                        vFreeBusy.Comment.EncodedValue = propertyValue;
                    }
                    break;

                case PropertyType.Contact:
                    vFreeBusy.Contact.DeserializeParameters(parameters);
                    vFreeBusy.Contact.EncodedValue = propertyValue;
                    break;

                case PropertyType.Organizer:
                    vFreeBusy.Organizer.DeserializeParameters(parameters);
                    vFreeBusy.Organizer.EncodedValue = propertyValue;
                    break;

                case PropertyType.Attendee:
                    AttendeeProperty ap = new();
                    ap.DeserializeParameters(parameters);
                    ap.EncodedValue = propertyValue;
                    vFreeBusy.Attendees.Add(ap);
                    break;

                case PropertyType.RequestStatus:
                    RequestStatusProperty rs = new();
                    rs.DeserializeParameters(parameters);
                    rs.EncodedValue = propertyValue;
                    vFreeBusy.RequestStatuses.Add(rs);
                    break;

                case PropertyType.FreeBusy:
                    // There may be more than one period in the value.  If so, split them into separate ones.
                    // This makes it easier to manage.  They'll get written back out as individual properties but
                    // that's okay.
                    parts = propertyValue.Split(',', ';');

                    // It's important that we retain the same parameters for each one
                    parms = new string[parameters.Count];
                    parameters.CopyTo(parms, 0);

                    foreach(string s in parts)
                    {
                        sc = [.. parms];

                        FreeBusyProperty fb = new();
                        fb.DeserializeParameters(sc);
                        fb.EncodedValue = s;

                        vFreeBusy.FreeBusy.Add(fb);
                    }
                    break;

                default:    // Anything else is a custom property
                    CustomProperty cust = new(propertyName);
                    cust.DeserializeParameters(parameters);
                    cust.EncodedValue = propertyValue;
                    vFreeBusy.CustomProperties.Add(cust);
                    break;
            }
        }

        /// <summary>
        /// This is implemented to handle properties related to observance rule items in VTimeZone objects
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="parameters">A string collection containing the parameters and their values.  If empty,
        /// there are no parameters.</param>
        /// <param name="propertyValue">The value of the property.</param>
        protected virtual void ObservanceRuleParser(string propertyName, StringCollection parameters, string propertyValue)
        {
            StringCollection sc;
            string[] parts, parms;
            int idx;

            // The last entry is always CustomProperty so scan for length minus one
            for(idx = 0; idx < ntvORule.Length - 1; idx++)
            {
                if(ntvORule[idx].IsMatch(propertyName))
                    break;
            }

            // An opening BEGIN:STANDARD or BEGIN:DAYLIGHT property must have been seen.
            if(obsRule == null)
            {
                throw new PDIParserException(this.LineNumber, LR.GetString("ExParseNoBeginProp",
                        "BEGIN:STANDARD/BEGIN:DAYLIGHT", propertyName));
            }

            // Handle or create the property
            switch(ntvORule[idx].EnumValue)
            {
                case PropertyType.Begin:    // Handle unknown nested objects
                    priorState.Push(currentState);
                    currentState = VCalendarParserState.Custom;
                    CustomObjectParser(propertyName, parameters, propertyValue);
                    break;

                case PropertyType.End:
                    // For this, the value must be STANDARD or DAYLIGHT depending on the rule type
                    if((obsRule.RuleType == ObservanceRuleType.Standard &&
                      String.Compare(propertyValue.Trim(), "STANDARD", StringComparison.OrdinalIgnoreCase) != 0) ||
                      (obsRule.RuleType == ObservanceRuleType.Daylight &&
                      String.Compare(propertyValue.Trim(), "DAYLIGHT", StringComparison.OrdinalIgnoreCase) != 0))
                    {
                        throw new PDIParserException(this.LineNumber, LR.GetString("ExParseUnrecognizedTagValue",
                            ntvORule[idx].Name, propertyValue));
                    }

                    // The rule is added to the collection when created so we don't have to rely on an END tag to
                    // add it.
                    obsRule = null;
                    currentState = priorState.Pop();
                    break;

                case PropertyType.StartDateTime:
                    obsRule.StartDateTime.DeserializeParameters(parameters);
                    obsRule.StartDateTime.EncodedValue = propertyValue;
                    break;

                case PropertyType.TimeZoneOffsetFrom:
                    obsRule.OffsetFrom.DeserializeParameters(parameters);
                    obsRule.OffsetFrom.EncodedValue = propertyValue;
                    break;

                case PropertyType.TimeZoneOffsetTo:
                    obsRule.OffsetTo.DeserializeParameters(parameters);
                    obsRule.OffsetTo.EncodedValue = propertyValue;
                    break;

                case PropertyType.Comment:
                    // If this is seen more than once, just add the new stuff to the existing property
                    if(obsRule.Comment.Value != null)
                    {
                        obsRule.Comment.EncodedValue += "\r\n";
                        obsRule.Comment.EncodedValue += propertyValue;
                    }
                    else
                    {
                        obsRule.Comment.DeserializeParameters(parameters);
                        obsRule.Comment.EncodedValue = propertyValue;
                    }
                    break;

                case PropertyType.RecurrenceRule:
                    RRuleProperty rr = new();
                    rr.DeserializeParameters(parameters);
                    rr.EncodedValue = propertyValue;
                    obsRule.RecurrenceRules.Add(rr);
                    break;

                case PropertyType.RecurDate:
                    // There may be more than one date in the value.  If so, split them into separate ones.  This
                    // makes it easier to manage.  They'll get written back out as individual properties but
                    // that's okay.
                    parts = propertyValue.Split(',', ';');

                    // It's important that we retain the same parameters for each one
                    parms = new string[parameters.Count];
                    parameters.CopyTo(parms, 0);

                    foreach(string s in parts)
                    {
                        sc = [.. parms];

                        RDateProperty rd = new();
                        rd.DeserializeParameters(sc);
                        rd.EncodedValue = s;

                        obsRule.RecurDates.Add(rd);
                    }
                    break;

                case PropertyType.TimeZoneName:
                    TimeZoneNameProperty tzn = new();
                    tzn.DeserializeParameters(parameters);
                    tzn.EncodedValue = propertyValue;
                    obsRule.TimeZoneNames.Add(tzn);
                    break;

                default:    // Anything else is a custom property
                    CustomProperty cust = new(propertyName);
                    cust.DeserializeParameters(parameters);
                    cust.EncodedValue = propertyValue;
                    obsRule.CustomProperties.Add(cust);
                    break;
            }
        }

        /// <summary>
        /// This is implemented to handle properties related to custom entries that are wrapped in a BEGIN/END
        /// sequence.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="parameters">A string collection containing the parameters and their values.  If empty,
        /// there are no parameters.</param>
        /// <param name="propertyValue">The value of the property.</param>
        /// <remarks>All unknown objects are stuck in the custom properties collection of the item being
        /// processed.</remarks>
        protected virtual void CustomObjectParser(string propertyName, StringCollection parameters, string propertyValue)
        {
            CustomProperty cust = new(propertyName);
            cust.DeserializeParameters(parameters);
            cust.EncodedValue = propertyValue;

            switch(priorState.Peek())
            {
                case VCalendarParserState.VCalendar:
                    vCal!.CustomProperties.Add(cust);
                    break;

                case VCalendarParserState.VEvent:
                    vEvent!.CustomProperties.Add(cust);
                    break;

                case VCalendarParserState.VToDo:
                    vToDo!.CustomProperties.Add(cust);
                    break;

                case VCalendarParserState.VJournal:
                    vJournal!.CustomProperties.Add(cust);
                    break;

                case VCalendarParserState.VAlarm:
                    vAlarm!.CustomProperties.Add(cust);
                    break;

                case VCalendarParserState.VFreeBusy:
                    vFreeBusy!.CustomProperties.Add(cust);
                    break;

                case VCalendarParserState.VTimeZone:
                    vTimeZone!.CustomProperties.Add(cust);
                    break;

                default:
                    throw new NotImplementedException(LR.GetString("ExCOPNotImplemented"));
            }

            // Is it another nested object
            if(String.Compare(propertyName, "BEGIN", StringComparison.OrdinalIgnoreCase) == 0)
                beginValue.Push(propertyValue.Trim());
            else
            {
                // Is it the end of the object?
                if(String.Compare(propertyName, "END", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    if(String.Compare(propertyValue.Trim(), beginValue.Peek(), StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        beginValue.Pop();

                        if(beginValue.Count == 0)
                            currentState = priorState.Pop();
                    }
                    else
                    {
                        throw new PDIParserException(this.LineNumber, LR.GetString("ExCOPUnexpectedEnd",
                            beginValue.Pop(), propertyValue));
                    }
                }
            }
        }
        #endregion
    }
}
