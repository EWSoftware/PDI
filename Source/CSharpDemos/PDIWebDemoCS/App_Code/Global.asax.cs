//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : Global.asax.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/21/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// At application start up, a common set of time zones is loaded into the VCalendar.TimeZones collection and a
// common set of holidays is loaded into the Recurrence.Holidays collection.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 01/16/2005  EFW  Created the code
//===============================================================================================================

using System;
using System.Collections;

using EWSoftware.PDI;
using EWSoftware.PDI.Objects;
using EWSoftware.PDI.Parser;

namespace PDIWebDemoCS
{
	/// <summary>
	/// Global application event handlers
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
        /// <summary>
        /// Initialize the time zone and recurrence information at start up
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
		protected void Application_Start(object sender, EventArgs e)
		{
            // Load the time zones if not already done.  The collection is static so it only needs to be loaded
            // once.
            if(VCalendar.TimeZones.Count == 0)
            {
                // Since it is static, we will use the Lock property to synchronize access to it in the web app
                // as multiple sessions may try to access it at the same time.  The parser will acquire a write
                // lock if it needs to merge a time zone component but since we are loading many time zones at
                // once, we'll lock it now.
                VCalendar.TimeZones.AcquireWriterLock(250);

                try
                {
                    // If still zero, load the file
                    if(VCalendar.TimeZones.Count == 0)
                    {
                        VCalendarParser.ParseFromFile(Server.MapPath("TimeZoneDB.ics"));

                        VCalendar.TimeZones.Sort(true);
                    }
                }
                finally
                {
                    VCalendar.TimeZones.ReleaseWriterLock();
                }
            }

            // Load a default set of holidays into the recurrence holiday collection.  It too is static, but
            // since it probably won't change after being set, it uses a simple SyncRoot property to lock the
            // collection.
            if(Recurrence.Holidays.Count == 0)
            {
                lock(((ICollection)Recurrence.Holidays).SyncRoot)
                {
                    if(Recurrence.Holidays.Count == 0)
                        Recurrence.Holidays.AddStandardHolidays();
                }
            }
		}

        /// <summary>
        /// Handle application errors
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
		protected void Application_Error(object sender, EventArgs e)
		{
            // Nothing special yet.
		}
	}
}
