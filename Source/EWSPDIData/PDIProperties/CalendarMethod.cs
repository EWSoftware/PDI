//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : CalendarMethod.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/18/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This enumerated type defines the various calendar methods for the MethodProperty class
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

using System;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This enumerated type defines the various calendar methods for the <see cref="MethodProperty"/> class
    /// </summary>
    [Serializable]
    public enum CalendarMethod
    {
        /// <summary>
        /// No calendar method defined.  No scheduling transaction should be assumed.
        /// </summary>
        None,
        /// <summary>
        /// Used to publish a calendar entry to one or more Calendar Users.  There is no interactivity between
        /// the publisher and any other calendar user. An example might include a baseball team publishing its
        /// schedule to the public.
        /// </summary>
        Publish,
        /// <summary>
        /// Used to schedule a calendar entry with other Calendar Users.  Requests are interactive in that they
        /// require the receiver to respond using the Reply methods. Meeting Requests, Busy Time requests and the
        /// assignment of VTODOs to other Calendar Users are all examples.  Requests are also used by the
        /// organizer to update the status of a calendar entry.
        /// </summary>
        Request,
        /// <summary>
        /// A Reply is used in response to a Request to convey attendee status to the organizer.  Replies are
        /// commonly used to respond to meeting and task requests.
        /// </summary>
        Reply,
        /// <summary>
        /// Add one or more instances to an existing VEVENT, VTODO, or VJOURNAL.
        /// </summary>
        Add,
        /// <summary>
        /// Cancel one or more instances of an existing VEVENT, VTODO, or VJOURNAL.
        /// </summary>
        Cancel,
        /// <summary>
        /// The Refresh method is used by an attendee to request the latest version of a calendar entry.
        /// </summary>
        Refresh,
        /// <summary>
        /// The Counter method is used by an attendee to negotiate a change in the calendar entry.  Examples
        /// include the request to change a proposed Event time or change the due date for a VTODO.
        /// </summary>
        Counter,
        /// <summary>
        /// Used by the organizer to decline the proposed counter-proposal.
        /// </summary>
        DeclineCounter,
        /// <summary>
        /// Indicates some other type of non-standard method
        /// </summary>
        Other
    }
}
