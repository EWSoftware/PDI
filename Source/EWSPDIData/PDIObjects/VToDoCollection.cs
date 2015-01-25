//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : VToDoCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/13/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a collection class for VToDo objects
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/28/2004  EFW  Created the code
// 03/21/2007  EFW  Converted to use a generic base class
//===============================================================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;

using EWSoftware.PDI.Binding;
using EWSoftware.PDI.Properties;

namespace EWSoftware.PDI.Objects
{
    /// <summary>
    /// A type-safe collection of <see cref="VToDo"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator</remarks>
    public class VToDoCollection : ExtendedBindingList<VToDo>
    {
        #region Constructor
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public VToDoCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="VToDo"/> objects
        /// </summary>
        /// <param name="todos">The <see cref="IList{T}"/> of to-do items to add</param>
        public VToDoCollection(IList<VToDo> todos) : base(todos)
        {
        }
        #endregion

        #region Indexer
        //=====================================================================

        /// <summary>
        /// Collection indexer
        /// </summary>
        /// <param name="uniqueId">The unique ID of the item to get or set.  When retrieving an item, null is
        /// returned if it does not exist in the collection.</param>
        /// <exception cref="ArgumentException">This is thrown if an attempt is made to set an item using a
        /// unique ID that does not exist in the collection.</exception>
        public VToDo this[string uniqueId]
        {
            get
            {
                for(int idx = 0; idx < base.Count; idx++)
                    if(base[idx].UniqueId.Value == uniqueId)
                        return base[idx];

                return null;
            }
            set
            {
                for(int idx = 0; idx < base.Count; idx++)
                    if(base[idx].UniqueId.Value == uniqueId)
                    {
                        base[idx] = value;
                        return;
                    }

                throw new ArgumentException(LR.GetString("ExUIDNotFound"));
            }
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This can be used to ensure that all to-do items in the collection have a unique ID assigned to them
        /// </summary>
        /// <param name="forceNew">If true, a new unique ID is assigned regardless of whether one already exists.
        /// If false and the to-do item already has a unique ID, it keeps the old one.</param>
        public void AssignUniqueIds(bool forceNew)
        {
            foreach(VToDo t in this)
                t.UniqueId.AssignNewId(forceNew);

            base.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        /// <summary>
        /// This is used to propagate a common version to all objects in the collection
        /// </summary>
        /// <param name="version">The version to user</param>
        public void PropagateVersion(SpecificationVersions version)
        {
            foreach(PDIObject o in this)
                o.Version = version;

            base.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        /// <summary>
        /// This is used to get a list of time zones used by all owned objects
        /// </summary>
        /// <param name="timeZoneIds">A <see cref="StringCollection"/> that will be used to store the list of
        /// unique time zone IDs used by the calendar objects.</param>
        public void TimeZonesUsed(StringCollection timeZoneIds)
        {
            foreach(VToDo t in this)
                t.TimeZonesUsed(timeZoneIds);
        }

        /// <summary>
        /// This is used to replace an old time zone ID with a new time zone ID in all properties of a calendar
        /// object.
        /// </summary>
        /// <param name="oldId">The old ID being replaced</param>
        /// <param name="newId">The new ID to use</param>
        public void UpdateTimeZoneId(string oldId, string newId)
        {
            foreach(VToDo t in this)
                t.UpdateTimeZoneId(oldId, newId);

            base.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        /// <summary>
        /// This is used to apply the selected time zone to all date/time objects in the component and convert
        /// them to the new time zone.
        /// </summary>
        /// <param name="vTimeZone">A <see cref="VTimeZone"/> object that will be used for all date/time objects
        /// in the component.</param>
        /// <remarks>When applied, all date/time values in the object will be converted to the new time zone</remarks>
        public void ApplyTimeZone(VTimeZone vTimeZone)
        {
            foreach(VToDo t in this)
                t.ApplyTimeZone(vTimeZone);

            base.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        /// <summary>
        /// This is used to set the selected time zone in all date/time objects in the component without
        /// modifying the date/time values.
        /// </summary>
        /// <param name="vTimeZone">A <see cref="VTimeZone"/> object that will be used for all date/time objects
        /// in the component.</param>
        /// <remarks>This method does not affect the date/time values</remarks>
        public void SetTimeZone(VTimeZone vTimeZone)
        {
            foreach(VToDo t in this)
                t.SetTimeZone(vTimeZone);

            base.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
        #endregion
    }
}
