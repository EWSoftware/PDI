//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : TimeZoneIdChangedEventArgs.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/04/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This is a custom EventArgs class for the TimeZoneIdChanged event
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//   Date      Who  Comments
// ==============================================================================================================
// 09/20/2004  EFW  Created the code
// 03/30/2007  EFW  Updated for use with .NET 2.0
//===============================================================================================================

using System;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This is a custom <see cref="EventArgs"/> class for the <c>TimeZoneIdChanged</c> event
    /// </summary>
    public class TimeZoneIdChangedEventArgs : EventArgs
    {
	    /// <summary>
        /// This property returns the old time zone ID
        /// </summary>
    	public string? OldId { get; private set; }

	    /// <summary>
        /// This property returns the new ID
        /// </summary>
    	public string? NewId { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="oldTimeZoneId">The old time zone ID</param>
        /// <param name="newTimeZoneId">The new time zone ID</param>
        public TimeZoneIdChangedEventArgs(string? oldTimeZoneId, string? newTimeZoneId)
        {
            this.OldId = oldTimeZoneId;
            this.NewId = newTimeZoneId;
        }
    }
}
