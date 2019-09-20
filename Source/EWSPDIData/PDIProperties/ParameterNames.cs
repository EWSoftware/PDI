//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : ParameterNames.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 04/22/2019
// Note    : Copyright 2004-2019, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This class holds a set of constants that define the standard parameter names for the various specifications
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/14/2004  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class holds a set of constants that define the standard parameter names for the various
    /// specifications.
    /// </summary>
    public static class ParameterNames
    {
        /// <summary>Encoding parameter</summary>
        public const string Encoding = "ENCODING";

        /// <summary>Character set parameter</summary>
        public const string CharacterSet = "CHARSET";

        /// <summary>Language parameter</summary>
        public const string Language = "LANGUAGE";

        /// <summary>Value type/location parameter (inline, date, etc)</summary>
        public const string ValueLocation = "VALUE";

        /// <summary>Type parameter (address type, phone type, etc)</summary>
        public const string Type = "TYPE";

        /// <summary>Context parameter</summary>
        public const string Context = "CONTEXT";

        /// <summary>Time Zone ID</summary>
        public const string TimeZoneId = "TZID";

        /// <summary>Alternate representation</summary>
        public const string AlternateRepresentation = "ALTREP";

        /// <summary>Attendee/organizer common name</summary>
        public const string CommonName = "CN";

        /// <summary>Attendee/organizer directory entry</summary>
        public const string DirectoryEntry = "DIR";

        /// <summary>Attendee/organizer sent by</summary>
        public const string SentBy = "SENT-BY";

        /// <summary>Attendee Role</summary>
        public const string Role = "ROLE";

        /// <summary>Attendee RSVP</summary>
        public const string Rsvp = "RSVP";

        /// <summary>Attendee expectation</summary>
        public const string Expect = "EXPECT";

        /// <summary>Attendee user type</summary>
        public const string CalendarUserType = "CUTYPE";

        /// <summary>Attendee delegated from</summary>
        public const string DelegatedFrom = "DELEGATED-FROM";

        /// <summary>Attendee delegated to</summary>
        public const string DelegatedTo = "DELEGATED-TO";

        /// <summary>Attendee membership</summary>
        public const string Member = "MEMBER";

        /// <summary>Attendee participation status (vCalendar)</summary>
        public const string Status = "STATUS";

        /// <summary>Attendee participation status (iCalendar)</summary>
        public const string PartStatus = "PARTSTAT";

        /// <summary>Related To relationship type (iCalendar)</summary>
        public const string RelationshipType = "RELTYPE";

        /// <summary>Attachment format type (iCalendar)</summary>
        public const string FormatType = "FMTTYPE";

        /// <summary>Recurrence ID range (iCalendar)</summary>
        public const string Range = "RANGE";

        /// <summary>Trigger Related (iCalendar)</summary>
        public const string Related = "RELATED";

        /// <summary>Free/busy type (iCalendar)</summary>
        public const string FreeBusyType = "FBTYPE";

        /// <summary>Sort as value (vCard 4.0)</summary>
        public const string SortAs = "SORT-AS";

        /// <summary>Property ID (vCard 4.0)</summary>
        public const string PropertyId = "PID";

        /// <summary>Calendar Scale (vCard 4.0)</summary>
        public const string CalendarScale = "CALSCALE";
    }
}
