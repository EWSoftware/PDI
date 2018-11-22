//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : VTimeZoneCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/21/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a collection class for VTimeZone objects
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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

using EWSoftware.PDI.Binding;
using EWSoftware.PDI.Properties;

namespace EWSoftware.PDI.Objects
{
    /// <summary>
    /// A type-safe collection of <see cref="VTimeZone"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator.  It also uses a <see cref="ReaderWriterLockSlim"/> to
    /// synchronize access to the underlying collection as it is used as a static collection in the
    /// <see cref="VCalendar"/> object.</remarks>
    [Serializable, DebuggerDisplay("Count = {Count}")]
    public class VTimeZoneCollection : IList<VTimeZone>, ICollection<VTimeZone>, IEnumerable<VTimeZone>, IList,
      ICollection, IEnumerable
    {
        #region Private data members
        //=====================================================================

        [NonSerialized]
        private ReaderWriterLockSlim rwl;

        private List<VTimeZone> items;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to get or set whether merging of time zone information is disabled
        /// </summary>
        /// <value>If a time zone object with the same time zone ID does not already exist in the collection, it
        /// is added as-is regardless of this property's setting.  If this property is set to true (the default)
        /// and a time zone object does exist with the same time zone ID, the rules from the new object are
        /// merged with the existing one.  If set to false, no merge will occur and the existing object's
        /// settings will remain unchanged.  This can be set to false to speed up loading of calendar files and
        /// to preserve settings from a common set of time zone objects that you have loaded yourself.</value>
        public bool MergingEnabled { get; set; }

        #endregion

        #region Events
        //=====================================================================

        /// <summary>
        /// This event is raised when the value of a <see cref="VTimeZone.TimeZoneId"/> property of an item in
        /// the collection is changed.
        /// </summary>
        public event EventHandler<TimeZoneIdChangedEventArgs> TimeZoneIdChanged;

        /// <summary>
        /// This raises the <see cref="TimeZoneIdChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnTimeZoneIdChanged(TimeZoneIdChangedEventArgs e)
        {
            TimeZoneIdChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This is called when the TimeZoneId property's name changes on an item in the collection
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void tzid_TimeZoneIdChanged(object sender, TimeZoneIdChangedEventArgs e)
        {
            OnTimeZoneIdChanged(e);
        }
        #endregion

        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <remarks>Merging of time zone information is enabled by default</remarks>
        /// <overloads>There are two overloads for the constructor</overloads>
        public VTimeZoneCollection()
        {
            this.MergingEnabled = true;
            items = new List<VTimeZone>();
        }

        /// <summary>
        /// Construct a collection from an enumerable list of <see cref="VTimeZone"/> objects
        /// </summary>
        /// <param name="timeZones">The enumerable list of VTimeZone objects</param>
        /// <remarks>Merging of time zone information is enabled by default</remarks>
        public VTimeZoneCollection(IEnumerable<VTimeZone> timeZones) : this()
        {
            if(timeZones != null)
                this.AddRange(timeZones);
        }
        #endregion

        #region IList<T> implementation
        //=====================================================================

        /// <summary>
        /// Get the index of the <see cref="VTimeZone"/> object
        /// </summary>
        /// <param name="item">The <c>VTimeZone</c> object to find.</param>
        /// <returns>The zero-based index of the entry if it exists within the collection or -1 if it is not
        /// found.</returns>
        /// <overloads>There are two overloads for this method</overloads>
        public int IndexOf(VTimeZone item)
        {
            this.AcquireReaderLock(250);

            try
            {
                return items.IndexOf(item);
            }
            finally
            {
                this.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Get the index of a <see cref="VTimeZone"/> object by its time zone ID
        /// </summary>
        /// <param name="tzid">The time zone ID to find</param>
        /// <returns>The zero-based index of the entry if it exists within the collection or -1 if it is not
        /// found.  This version is much faster than the other version as it only has to compare ID values.</returns>
        public int IndexOf(string tzid)
        {
            this.AcquireReaderLock(250);

            try
            {
                for(int idx = 0; idx < items.Count; idx++)
                    if(items[idx].TimeZoneId.Value == tzid)
                        return idx;

                return -1;
            }
            finally
            {
                this.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Insert a <see cref="VTimeZone"/> into the collection at the specified index
        /// </summary>
        /// <param name="index">The position at which to insert the item</param>
        /// <param name="item">The <c>VTimeZone</c> object to insert</param>
        /// <remarks>If a time zone item with the same ID already exists in the collection, it will be merged
        /// with it and moved to the new position.  See the <see cref="Merge"/> method for more information.
        /// </remarks>
        public void Insert(int index, VTimeZone item)
        {
            VTimeZone existing = this[item.TimeZoneId.Value];

            if(existing == null)
            {
                this.AcquireWriterLock(250);

                try
                {
                    item.TimeZoneId.TimeZoneIdChanged += tzid_TimeZoneIdChanged;
                    items.Insert(index, item);
                }
                finally
                {
                    this.ReleaseWriterLock();
                }
            }
            else
            {
                int curIdx = this.Merge(item);

                if(index != curIdx)
                {
                    this.AcquireWriterLock(250);

                    try
                    {
                        items.RemoveAt(curIdx);

                        if(index > items.Count)
                            items.Add(existing);
                        else
                            items.Insert(index, existing);
                    }
                    finally
                    {
                        this.ReleaseWriterLock();
                    }
                }
            }
        }

        /// <summary>
        /// Remove the item at the specified index from the collection
        /// </summary>
        /// <param name="index">The index of the item to remove</param>
        public void RemoveAt(int index)
        {
            this.Remove(items[index]);
        }

        /// <summary>
        /// Collection indexer
        /// </summary>
        /// <param name="index">The item to get or set</param>
        /// <returns>The entry at the requested position</returns>
        /// <overloads>There are two overloads for the indexer</overloads>
        public VTimeZone this[int index]
        {
            get
            {
                this.AcquireReaderLock(250);

                try
                {
                    return items[index];
                }
                finally
                {
                    this.ReleaseReaderLock();
                }
            }
            set
            {
                this.AcquireWriterLock(250);

                try
                {
                    items[index].TimeZoneId.TimeZoneIdChanged -= tzid_TimeZoneIdChanged;
                    value.TimeZoneId.TimeZoneIdChanged += tzid_TimeZoneIdChanged;

                    items[index] = value;
                }
                finally
                {
                    this.ReleaseWriterLock();
                }
            }
        }

        /// <summary>
        /// Collection indexer
        /// </summary>
        /// <param name="timeZoneId">The time zone ID of the item to get or set.  When retrieving an item, null
        /// is returned if it does not exist in the collection.</param>
        /// <exception cref="ArgumentException">This is thrown if an attempt is made to set an item using a time
        /// zone ID that does not exist in the collection.</exception>
        public VTimeZone this[string timeZoneId]
        {
            get
            {
                this.AcquireReaderLock(250);

                try
                {
                    for(int idx = 0; idx < items.Count; idx++)
                        if(items[idx].TimeZoneId.Value == timeZoneId)
                            return items[idx];

                    return null;
                }
                finally
                {
                    this.ReleaseReaderLock();
                }
            }
            set
            {
                this.AcquireWriterLock(250);

                try
                {
                    for(int idx = 0; idx < items.Count; idx++)
                        if(items[idx].TimeZoneId.Value == timeZoneId)
                        {
                            this[idx] = value;
                            return;
                        }

                    throw new ArgumentException(LR.GetString("ExVTZIDNotFound"));
                }
                finally
                {
                    this.ReleaseWriterLock();
                }
            }
        }
        #endregion

        #region ICollection<T> implementation
        //=====================================================================

        /// <summary>
        /// Add a <see cref="VTimeZone"/> to the collection
        /// </summary>
        /// <param name="item">The <c>VTimeZone</c> object to add</param>
        /// <remarks>This calls the <see cref="Merge"/> method to merge common time zone information.  See its
        /// documentation for more details.</remarks>
        public void Add(VTimeZone item)
        {
            this.Merge(item);
        }

        /// <summary>
        /// Remove all items from the collection
        /// </summary>
        public void Clear()
        {
            this.AcquireWriterLock(250);

            try
            {
                foreach(VTimeZone vtz in this)
                    vtz.TimeZoneId.TimeZoneIdChanged -= tzid_TimeZoneIdChanged;

                items.Clear();
            }
            finally
            {
                this.ReleaseWriterLock();
            }
        }

        /// <summary>
        /// Check to see if the <see cref="VTimeZone"/> object is in the collection
        /// </summary>
        /// <param name="item">The <c>VTimeZone</c> object to find</param>
        /// <returns>True if found, false if not</returns>
        public bool Contains(VTimeZone item)
        {
            this.AcquireReaderLock(250);

            try
            {
                return items.Contains(item);
            }
            finally
            {
                this.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Copy the <see cref="VTimeZone"/> objects to an array
        /// </summary>
        /// <param name="array">The array into which the objects are copied</param>
        /// <param name="arrayIndex">The index at which to start copying</param>
        public void CopyTo(VTimeZone[] array, int arrayIndex)
        {
            this.AcquireReaderLock(250);

            try
            {
                items.CopyTo(array, arrayIndex);
            }
            finally
            {
                this.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Return the number of items in the collection
        /// </summary>
        public int Count
        {
            get
            {
                this.AcquireReaderLock(250);

                try
                {
                    return items.Count;
                }
                finally
                {
                    this.ReleaseReaderLock();
                }
            }
        }

        /// <summary>
        /// This collection is never read-only
        /// </summary>
        /// <value>This always returns false</value>
        public bool IsReadOnly => false;

        /// <summary>
        /// Remove a <see cref="VTimeZone"/> from the collection
        /// </summary>
        /// <param name="item">The <c>VTimeZone</c> object to remove</param>
        /// <returns>True if the item was removed or false if it was not in the collection</returns>
        public bool Remove(VTimeZone item)
        {
            this.AcquireWriterLock(250);

            try
            {
                if(items.Contains(item))
                {
                    item.TimeZoneId.TimeZoneIdChanged -= tzid_TimeZoneIdChanged;
                    items.Remove(item);

                    return true;
                }
            }
            finally
            {
                this.ReleaseWriterLock();
            }

            return false;
        }
        #endregion

        #region IEnumerable<T> and IEnumerable implementations
        //=====================================================================

        /// <summary>
        /// Get a type-safe <see cref="VTimeZoneCollection"/> enumerator
        /// </summary>
        /// <returns>A type-safe <c>VTimeZoneCollection</c> enumerator</returns>
        public IEnumerator<VTimeZone> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        /// <summary>
        /// Explicit IEnumerable.GetEnumerator implementation
        /// </summary>
        /// <returns>An enumerator for the collection</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion

        #region IList implementation
        //=====================================================================

        /// <summary>
        /// Explicit IList.Add implementation
        /// </summary>
        /// <param name="value">The value to add</param>
        /// <returns>The index of the added item</returns>
        int IList.Add(object value)
        {
            this.Add((VTimeZone)value);
            return this.Count - 1;
        }

        /// <summary>
        /// Explicit IList.Clear implementation
        /// </summary>
        void IList.Clear()
        {
            this.Clear();
        }

        /// <summary>
        /// Explicit IList.Contains implementation
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <returns>True if in the collection, false if not</returns>
        bool IList.Contains(object value)
        {
            return this.Contains((VTimeZone)value);
        }

        /// <summary>
        /// Explicit IList.IndexOf
        /// </summary>
        /// <param name="value">The value for which to search</param>
        /// <returns>The index of the item or -1 if not found</returns>
        int IList.IndexOf(object value)
        {
            return this.IndexOf((VTimeZone)value);
        }

        /// <summary>
        /// Explicit IList.Insert
        /// </summary>
        /// <param name="index">The index at which to insert the item</param>
        /// <param name="value">The item to insert</param>
        void IList.Insert(int index, object value)
        {
            this.Insert(index, (VTimeZone)value);
        }

        /// <summary>
        /// The collection is not of a fixed size
        /// </summary>
        /// <returns>This always returns false</returns>
        public bool IsFixedSize => false;

        /// <summary>
        /// The collection is not read-only
        /// </summary>
        /// <returns>This always returns false</returns>
        bool IList.IsReadOnly => false;

        /// <summary>
        /// Explicit IList.Remove implementation 
        /// </summary>
        /// <param name="value">The value to remove</param>
        void IList.Remove(object value)
        {
            this.Remove((VTimeZone)value);
        }

        /// <summary>
        /// Explicit ILst.RemoveAt implementation
        /// </summary>
        /// <param name="index">The index of the item to remove</param>
        void IList.RemoveAt(int index)
        {
            this.RemoveAt(index);
        }

        /// <summary>
        /// Explicit IList indexer
        /// </summary>
        /// <param name="index">The index of the item to set or get</param>
        /// <returns>The item at the specified index</returns>
        object IList.this[int index]
        {
            get => this[index];
            set => this[index] = (VTimeZone)value;
        }
        #endregion

        #region ICollection implementation
        //=====================================================================

        /// <summary>
        /// Explicit ICollection.CopyTo implementation
        /// </summary>
        /// <param name="array">The array into which the objects are copied</param>
        /// <param name="index">The index at which to start copying</param>
        void ICollection.CopyTo(Array array, int index)
        {
            VTimeZone[] zones = array as VTimeZone[];
            this.CopyTo(zones, index);
        }

        /// <summary>
        /// Explicit ICollection.Count implementation
        /// </summary>
        int ICollection.Count => this.Count;

        /// <summary>
        /// ICollection.IsSynchronized implementation
        /// </summary>
        /// <returns>This always returns true</returns>
        public bool IsSynchronized => true;

        /// <summary>
        /// ICollection.SyncRoot implementation
        /// </summary>
        /// <remarks>This provides a less efficient way of synchronizing access to the collection</remarks>
        public object SyncRoot => ((ICollection)items).SyncRoot;

        #endregion

        #region Common helper methods
        //=====================================================================

        /// <summary>
        /// This can be used to acquire a reader lock on the collection to perform bulk read operations
        /// </summary>
        /// <param name="timeout">The timeout in milliseconds after which the lock attempt will fail</param>
        /// <param name="upgradeable">True if the reader lock should be upgradeable to a writer lock,
        /// false if not.  Typically, this should be false unless you may need to acquire a writer lock during
        /// the period which the read lock is held.</param>
        /// <remarks><para>Calls to this method should be paired with a call to <see cref="ReleaseReaderLock"/>
        /// to release the lock when done.</para>
        /// 
        /// <para>This is useful for access to the <see cref="VCalendar.TimeZones"/> collection in web
        /// applications when enumerating the collection or bulk loading time zone components.  It allows
        /// multiple concurrent readers of the collection but only one writer at a time to the collection.</para>
        /// </remarks>
        /// <exception cref="TimeoutException">This is thrown if the timeout expires before acquiring the lock</exception>
        public void AcquireReaderLock(int timeout, bool upgradeable = false)
        {
            if(rwl == null)
            {
                lock(this.SyncRoot)
                {
                    if(rwl == null)
                        rwl = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
                }
            }

            bool success = false;

            if(upgradeable || rwl.IsUpgradeableReadLockHeld)
                success = rwl.TryEnterUpgradeableReadLock(timeout);
            else
                success = rwl.TryEnterReadLock(timeout);

            if(!success)
                throw new TimeoutException("Unable to obtain a reader lock on the time zone collection in the alloted time");
        }

        /// <summary>
        /// This is used to free a reader lock previously acquired by calling <see cref="AcquireReaderLock"/>
        /// </summary>
        public void ReleaseReaderLock()
        {
            if(rwl != null)
                if(rwl.IsUpgradeableReadLockHeld)
                    rwl.ExitUpgradeableReadLock();
                else
                    if(rwl.IsReadLockHeld)
                        rwl.ExitReadLock();
        }

        /// <summary>
        /// This can be used to acquire a writer lock on the collection to perform bulk updates
        /// </summary>
        /// <param name="timeout">The timeout in milliseconds after which the lock attempt will fail</param>
        /// <remarks><para>Calls to this method should be paired with a call to <see cref="ReleaseWriterLock"/>
        /// to release the lock when done.</para>
        /// 
        /// <para>This is useful for access to the <see cref="VCalendar.TimeZones"/> collection in web
        /// applications when enumerating the collection or bulk loading time zone components.  It allows
        /// multiple concurrent readers of the collection but only one writer at a time to the collection.</para>
        /// </remarks>
        /// <exception cref="TimeoutException">This is thrown if the timeout expires before acquiring the lock</exception>
        public void AcquireWriterLock(int timeout)
        {
            if(rwl == null)
            {
                lock(this.SyncRoot)
                {
                    if(rwl == null)
                        rwl = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
                }
            }

            if(!rwl.TryEnterWriteLock(timeout))
                throw new TimeoutException("Unable to obtain a reader lock on the time zone collection in the alloted time");
        }

        /// <summary>
        /// This is used to free a writer lock previously acquired by calling <see cref="AcquireWriterLock"/>
        /// </summary>
        public void ReleaseWriterLock()
        {
            if(rwl != null && rwl.IsWriteLockHeld)
                rwl.ExitWriteLock();
        }

        /// <summary>
        /// Determine whether or not the collection contains at least one item that matches the conditions in the
        /// specified predicate.
        /// </summary>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions of the match</param>
        /// <returns>Returns true if at least one item matches the conditions or false if none of them do</returns>
        public bool Exists(Predicate<VTimeZone> match)
        {
            this.AcquireReaderLock(250);

            try
            {
                return (items.FindIndex(0, items.Count, match) != -1);
            }
            finally
            {
                this.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Search the collection for the first item that matches the conditions in the specified predicate
        /// </summary>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions of the match</param>
        /// <returns>The first item found that matches the conditions or the default value for type T if nothing
        /// is found.</returns>
        public VTimeZone Find(Predicate<VTimeZone> match)
        {
            this.AcquireReaderLock(250);

            try
            {
                return items.Find(match);
            }
            finally
            {
                this.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Retrieve all of the items that match the conditions defined by the specified predicate
        /// </summary>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions of the match</param>
        /// <returns>Returns a <see cref="ExtendedBindingList{T}"/> containing all of the items that match the
        /// conditions defined by the specified predicate.  If none are found, the collection will be empty.</returns>
        public ExtendedBindingList<VTimeZone> FindAll(Predicate<VTimeZone> match)
        {
            this.AcquireReaderLock(250);

            try
            {
                List<VTimeZone> foundItems = items.FindAll(match);

                ExtendedBindingList<VTimeZone> collection = new ExtendedBindingList<VTimeZone>();

                foreach(VTimeZone item in foundItems)
                    collection.Add(item);

                return collection;
            }
            finally
            {
                this.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Search for the first item that matches the conditions defined by the specified predicate and return
        /// its zero-based index.
        /// </summary>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions of the match</param>
        /// <returns>Returns the zero-based index of the first occurrence of an item that matches the conditions
        /// defined by the predicate or -1 if nothing is found.</returns>
        public int FindIndex(Predicate<VTimeZone> match)
        {
            this.AcquireReaderLock(250);

            try
            {
                return items.FindIndex(0, items.Count, match);
            }
            finally
            {
                this.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Search for the first item that matches the conditions defined by the specified predicate between the
        /// starting index and the end of the collection and return its zero-based index.
        /// </summary>
        /// <param name="startIndex">The zero-based starting index for the search</param>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions of the match</param>
        /// <returns>Returns the zero-based index of the first occurrence of an item that matches the conditions
        /// defined by the predicate or -1 if nothing is found.</returns>
        public int FindIndex(int startIndex, Predicate<VTimeZone> match)
        {
            this.AcquireReaderLock(250);

            try
            {
                return items.FindIndex(startIndex, items.Count, match);
            }
            finally
            {
                this.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Search for the first item that matches the conditions defined by the specified predicate between the
        /// starting index and the given number of items and return its zero-based index.
        /// </summary>
        /// <param name="startIndex">The zero-based starting index for the search</param>
        /// <param name="count">The number of items to search</param>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions of the match</param>
        /// <returns>Returns the zero-based index of the first occurrence of an item that matches the conditions
        /// defined by the predicate or -1 if nothing is found.</returns>
        public int FindIndex(int startIndex, int count, Predicate<VTimeZone> match)
        {
            this.AcquireReaderLock(250);

            try
            {
                return items.FindIndex(startIndex, count, match);
            }
            finally
            {
                this.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Search the collection for the last item that matches the conditions in the specified predicate
        /// </summary>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions of the match</param>
        /// <returns>The last item found that matches the conditions or the default value for type T if nothing
        /// is found.</returns>
        public VTimeZone FindLast(Predicate<VTimeZone> match)
        {
            this.AcquireReaderLock(250);

            try
            {
                return items.FindLast(match);
            }
            finally
            {
                this.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Search for the last item that matches the conditions defined by the specified predicate and return
        /// its zero-based index.
        /// </summary>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions of the match</param>
        /// <returns>Returns the zero-based index of the last occurrence of an item that matches the conditions
        /// defined by the predicate or -1 if nothing is found.</returns>
        public int FindLastIndex(Predicate<VTimeZone> match)
        {
            this.AcquireReaderLock(250);

            try
            {
                return items.FindLastIndex(0, items.Count, match);
            }
            finally
            {
                this.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Search for the last item that matches the conditions defined by the specified predicate between the
        /// starting index and searching backwards to the start of the collection and return its zero-based index.
        /// </summary>
        /// <param name="startIndex">The zero-based starting index for the backwards search</param>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions of the match</param>
        /// <returns>Returns the zero-based index of the last occurrence of an item that matches the conditions
        /// defined by the predicate or -1 if nothing is found.</returns>
        public int FindLastIndex(int startIndex, Predicate<VTimeZone> match)
        {
            this.AcquireReaderLock(250);

            try
            {
                return items.FindLastIndex(startIndex, items.Count, match);
            }
            finally
            {
                this.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Search for the last item that matches the conditions defined by the specified predicate between the
        /// starting index and searching backwards for the given number of items and return its zero-based index.
        /// </summary>
        /// <param name="startIndex">The zero-based starting index for the backwards search</param>
        /// <param name="count">The number of items to search</param>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions of the match</param>
        /// <returns>Returns the zero-based index of the first occurrence of an item that matches the conditions
        /// defined by the predicate or -1 if nothing is found.</returns>
        public int FindLastIndex(int startIndex, int count, Predicate<VTimeZone> match)
        {
            this.AcquireReaderLock(250);

            try
            {
                return items.FindLastIndex(startIndex, count, match);
            }
            finally
            {
                this.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Performs the specified action on each item in the collection
        /// </summary>
        /// <param name="action">The <see cref="Action{T}"/> delegate to perform on each item in the collection</param>
        public void ForEach(Action<VTimeZone> action)
        {
            this.AcquireWriterLock(250);

            try
            {
                items.ForEach(action);
            }
            finally
            {
                this.ReleaseWriterLock();
            }
        }

        /// <summary>
        /// Remove all items that match the conditions defined by the specified predicate
        /// </summary>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions of the match</param>
        /// <returns>Returns the number of items removed from the collection</returns>
        public int RemoveAll(Predicate<VTimeZone> match)
        {
            this.AcquireWriterLock(250);

            try
            {
                return items.RemoveAll(match);
            }
            finally
            {
                this.ReleaseWriterLock();
            }
        }

        /// <summary>
        /// Remove the specified range of items from the collection
        /// </summary>
        /// <param name="index">The zero-based starting index of the range to remove</param>
        /// <param name="count">The number of items to remove</param>
        public void RemoveRange(int index, int count)
        {
            this.AcquireWriterLock(250);

            try
            {
                items.RemoveRange(index, count);
            }
            finally
            {
                this.ReleaseWriterLock();
            }
        }

        /// <summary>
        /// This is used to sort the collection in ascending or descending order by time zone ID
        /// </summary>
        /// <param name="ascending">Pass true for ascending order, false for descending order</param>
        public void Sort(bool ascending)
        {
            this.AcquireWriterLock(250);

            try
            {
                items.Sort((x, y) =>
                {
                    if(ascending)
                        return String.Compare(x.TimeZoneId.Value, y.TimeZoneId.Value, StringComparison.Ordinal);

                    // Flip the result for descending order
                    return String.Compare(y.TimeZoneId.Value, x.TimeZoneId.Value, StringComparison.Ordinal);
                });
            }
            finally
            {
                this.ReleaseWriterLock();
            }
        }

        /// <summary>
        /// Sort the items in the collection using the specified comparer
        /// </summary>
        /// <param name="comparer">The <see cref="IComparer{T}"/> implementation to use when comparing items or
        /// null to use the default comparer.</param>
        public void Sort(IComparer<VTimeZone> comparer)
        {
            this.AcquireWriterLock(250);

            try
            {
                items.Sort(comparer);
            }
            finally
            {
                this.ReleaseWriterLock();
            }
        }

        /// <summary>
        /// Sort the items in the collection using the specified <see cref="Comparison{T}"/> delegate
        /// </summary>
        /// <param name="comparison">The <see cref="Comparison{T}"/> delegate to use for the comparisons</param>
        public void Sort(Comparison<VTimeZone> comparison)
        {
            this.AcquireWriterLock(250);

            try
            {
                items.Sort(comparison);
            }
            finally
            {
                this.ReleaseWriterLock();
            }
        }

        /// <summary>
        /// Determine if every item in the collection matches the conditions defined by the specified predicate
        /// </summary>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions to check in
        /// each item.</param>
        /// <returns>Returns true if every item in the collection matches the conditions defined in the specified
        /// predicate or if the collection is empty.  It returns false if at least one item does not match the
        /// conditions.</returns>
        public bool TrueForAll(Predicate<VTimeZone> match)
        {
            this.AcquireReaderLock(250);

            try
            {
                return items.TrueForAll(match);
            }
            finally
            {
                this.ReleaseReaderLock();
            }
        }
        #endregion

        #region Collection-specific members
        //=====================================================================

        /// <summary>
        /// Add a range of <see cref="VTimeZone"/> objects from an enumerable list
        /// </summary>
        /// <param name="timeZones">The enumerable list of <c>VTimeZone</c> objects</param>
        /// <remarks>This calls the <see cref="Merge"/> method to merge common time zone information.  See its
        /// documentation for more details.</remarks>
        public void AddRange(IEnumerable<VTimeZone> timeZones)
        {
            if(timeZones != null)
                foreach(VTimeZone tz in timeZones)
                    this.Merge(tz);
        }

        /// <summary>
        /// This is used to propagate a common version to all objects in the collection
        /// </summary>
        /// <param name="version">The version to use</param>
        public void PropagateVersion(SpecificationVersions version)
        {
            this.AcquireReaderLock(250);

            try
            {
                foreach(PDIObject o in this)
                    o.Version = version;
            }
            finally
            {
                this.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// This is used to merge a time zone instance into the collection
        /// </summary>
        /// <param name="vTimeZone">The time zone information to merge with the collection</param>
        /// <returns>The index of the merged time zone item</returns>
        /// <remarks>If a time zone object with the same time zone ID does not already exist in the collection,
        /// it is added as-is.  If one does exist with the same time zone ID, the rules from the new object are
        /// merged with the existing one if the <see cref="MergingEnabled"/> property is set to true.  If it is
        /// set to false, the new object is ignored and the existing settings remain unchanged.  The index of the
        /// existing entry will still be returned.</remarks>
        public int Merge(VTimeZone vTimeZone)
        {
            ObservanceRuleCollection rules;
            int idx, tzIdx;

            tzIdx = this.IndexOf(vTimeZone.TimeZoneId.Value);

            if(tzIdx == -1)
            {
                this.AcquireWriterLock(250);

                try
                {
                    vTimeZone.TimeZoneId.TimeZoneIdChanged += tzid_TimeZoneIdChanged;
                    items.Add(vTimeZone);
                    tzIdx = items.Count - 1;
                }
                finally
                {
                    this.ReleaseWriterLock();
                }
            }
            else    // Merge the rules if allowed
                if(this.MergingEnabled)
                {
                    this.AcquireWriterLock(250);

                    try
                    {
                        rules = items[tzIdx].ObservanceRules;

                        foreach(ObservanceRule or in vTimeZone.ObservanceRules)
                        {
                            for(idx = 0; idx < rules.Count; idx++)
                                if(rules[idx].Equals(or))
                                    break;

                            if(idx == rules.Count)
                                rules.Add(or);
                        }
                    }
                    finally
                    {
                        this.ReleaseWriterLock();
                    }
                }

            return tzIdx;
        }
        #endregion
    }
}
