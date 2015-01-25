//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : DateTimeInstanceCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 10/31/2014
// Note    : Copyright 2003-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a type-safe collection class that is used to contain DateTimeInstance objects
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 08/18/2003  EFW  Created the code
// 03/17/2005  EFW  Converted to use a generic base class
//===============================================================================================================

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EWSoftware.PDI
{
    /// <summary>
    /// A type-safe collection of <see cref="DateTimeInstance"/> objects.  The other classes in this namespace
    /// use this collection when returning a list of dates.  The collection can be used as a data source for data
    /// binding.
    /// </summary>
    [Serializable]
    public class DateTimeInstanceCollection : Collection<DateTimeInstance>
    {
        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public DateTimeInstanceCollection()
        {
        }

        /// <summary>
        /// Construct a collection from an enumerable list of <see cref="DateTimeInstance"/> objects
        /// </summary>
        /// <param name="dates">The enumerable list of dates</param>
        public DateTimeInstanceCollection(IEnumerable<DateTimeInstance> dates)
        {
            if(dates != null)
                this.AddRange(dates);
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Add a range of <see cref="DateTimeInstance"/> objects from an enumerable list
        /// </summary>
        /// <param name="dates">The enumerable list of dates</param>
        public void AddRange(IEnumerable<DateTimeInstance> dates)
        {
            if(dates != null)
                foreach(DateTimeInstance d in dates)
                    base.Add(d);
        }

        /// <summary>
        /// Remove a range of items from the collection
        /// </summary>
        /// <param name="index">The zero-based index at which to start removing  items</param>
        /// <param name="count">The number of items to remove</param>
        public void RemoveRange(int index, int count)
        {
            ((List<DateTimeInstance>)base.Items).RemoveRange(index, count);
        }

        /// <summary>
        /// This converts all date/time instances in the collection to local time
        /// </summary>
        /// <remarks>This converts the date time instances from their current time zone identified by their
        /// <see cref="DateTimeInstance.TimeZoneId"/> property to local time.  The time zone ID property will be
        /// set to null in each one after conversion to indicate that the instances are in local time.</remarks>
        public void ToLocalTime()
        {
            foreach(DateTimeInstance dti in this)
                dti.ToLocalTime();
        }

        /// <summary>
        /// This converts all date/time instances to the specified time zone
        /// </summary>
        /// <param name="tzid">The time zone to which to convert the instances</param>
        /// <remarks>This converts the date time instances from their current time zone identified by their
        /// <see cref="DateTimeInstance.TimeZoneId"/> property to the specified time zone.  The time zone ID
        /// property will be set to the specified time zone in each one after conversion.</remarks>
        public void ToTimeZone(string tzid)
        {
            foreach(DateTimeInstance dti in this)
                dti.ToTimeZone(tzid);
        }
        #endregion
    }
}
