//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : FreeBusyType.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/21/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This enumerated type defines the various free/busy types for the FreeBusyProperty
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
    /// This enumerated type defines the various free/busy types for the <see cref="FreeBusyProperty"/> class
    /// </summary>
    [Serializable]
    public enum FreeBusyType
    {
        /// <summary>No type specified</summary>
        None,
        /// <summary>Represents free time</summary>
        Free,
        /// <summary>Represents busy time</summary>
        Busy,
        /// <summary>Represents busy (unavailable) time</summary>
        BusyUnavailable,
        /// <summary>Represents tentative busy time</summary>
        BusyTentative,
        /// <summary>Indicates some other type of busy/free time</summary>
        Other
    }
}
