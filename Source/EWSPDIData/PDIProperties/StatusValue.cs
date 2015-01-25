//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : StatusValue.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/19/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This enumerated type defines the various status values for the StatusProperty
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
    /// This enumerated type defines the various status values for the <see cref="StatusProperty"/>
    /// </summary>
    [Serializable]
    public enum StatusValue
    {
        /// <summary>No status defined</summary>
        None,
        /// <summary>Indicates to-do was accepted (vCalendar only)</summary>
        Accepted,
        /// <summary>Indicates event or to-do requires action</summary>
        NeedsAction,
        /// <summary>Indicates event or to-do was sent out (vCalendar only)</summary>
        Sent,
        /// <summary>Indicates event is tentatively accepted</summary>
        Tentative,
        /// <summary>Indicates event is confirmed (vCalendar only)</summary>
        Confirmed,
        /// <summary>Indicates event or to-do has been declined (vCalendar only)</summary>
        Declined,
        /// <summary>Indicates to-do has been completed</summary>
        Completed,
        /// <summary>Indicates event or to-do has been delegated (vCalendar only)</summary>
        Delegated,
        /// <summary>Indicates event, to-do, or journal was canceled (iCalendar only)</summary>
        Cancelled,
        /// <summary>Indicates to-do in process (iCalendar only)</summary>
        InProcess,
        /// <summary>Indicates journal is draft (iCalendar only)</summary>
        Draft,
        /// <summary>Indicates journal is final (iCalendar only)</summary>
        Final
    }
}
