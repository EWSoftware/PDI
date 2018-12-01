//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : ExtendedBindingList.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2007-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a generic, bindable base class used to hold a collection of items and supports various
// useful features such as sorting and searching.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/21/2007  EFW  Created the code
//===============================================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace EWSoftware.PDI.Binding
{
    /// <summary>
    /// This is a generic, bindable base class used to hold a collection of items and supports various useful
    /// features such as sorting and searching.
    /// </summary>
    /// <typeparam name="T">The object type contained in the collection</typeparam>
    public class ExtendedBindingList<T> : BindingList<T> where T : ICloneable
    {
        #region Private data members
        //=====================================================================

        // The sorted flag
        private bool isSorted;

        // The property descriptor and direction for the current sort
        private PropertyDescriptor sortProperty;
        private ListSortDirection sortDirection;

        #endregion

        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public ExtendedBindingList()
        {
        }

        /// <summary>
        /// Construct the collection using the specified list of items
        /// </summary>
        /// <param name="items">An <see cref="IList{T}" /> of items to be contained in the binding list</param>
        public ExtendedBindingList(IList<T> items) : base(items)
        {
        }
        #endregion

        #region Standard list methods
        //=====================================================================

        /// <summary>
        /// This returns an object that can be used to synchronize access to the collection
        /// </summary>
        public object SyncRoot => ((ICollection)this).SyncRoot;

        /// <summary>
        /// Add a range of items from an array
        /// </summary>
        /// <param name="items">The array of items</param>
        /// <overloads>There are two overloads for this method</overloads>
        public void AddRange(T[] items)
        {
            if(items != null)
                foreach(T item in items)
                    base.Add(item);
        }

        /// <summary>
        /// Clone and add a range of items from an enumerable list of items
        /// </summary>
        /// <param name="items">The enumerable list of items from which to copy the items</param>
        /// <remarks>Items in the source list will be cloned and added to this collection</remarks>
        /// <returns>A reference to the collection</returns>
        public ExtendedBindingList<T> CloneRange(IEnumerable<T> items)
        {
            if(items != null)
                foreach(T item in items)
                    base.Add((T)item.Clone());

            return this;
        }
        #endregion

        #region Useful List<T>-like method implementations
        //=====================================================================

        /// <summary>
        /// Determine whether or not the collection contains at least one item that matches the conditions in the
        /// specified predicate.
        /// </summary>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions of the match</param>
        /// <returns>Returns true if at least one item matches the conditions or false if none of them do</returns>
        public bool Exists(Predicate<T> match)
        {
            return (((List<T>)base.Items).FindIndex(0, base.Count, match) != -1);
        }

        /// <summary>
        /// Search the collection for the first item that matches the conditions in the specified predicate
        /// </summary>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions of the match</param>
        /// <returns>The first item found that matches the conditions or the default value for type T if nothing
        /// is found.</returns>
        public T Find(Predicate<T> match)
        {
            return ((List<T>)base.Items).Find(match);
        }

        /// <summary>
        /// Retrieve all of the items that match the conditions defined by the specified predicate
        /// </summary>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions of the match</param>
        /// <returns>Returns an <see cref="ExtendedBindingList{T}"/> containing all of the items that match the
        /// conditions defined by the specified predicate.  If none are found, the collection will be empty.</returns>
        public ExtendedBindingList<T> FindAll(Predicate<T> match)
        {
            List<T> foundItems = ((List<T>)base.Items).FindAll(match);

            ExtendedBindingList<T> collection = new ExtendedBindingList<T>();

            foreach(T item in foundItems)
                collection.Add(item);

            return collection;
        }

        /// <summary>
        /// Search for the first item that matches the conditions defined by the specified predicate and return
        /// its zero-based index.
        /// </summary>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions of the match</param>
        /// <returns>Returns the zero-based index of the first occurrence of an item that matches the conditions
        /// defined by the predicate or -1 if nothing is found.</returns>
        /// <overloads>There are three overloads for this method.</overloads>
        public int FindIndex(Predicate<T> match)
        {
            return ((List<T>)base.Items).FindIndex(0, base.Count, match);
        }

        /// <summary>
        /// Search for the first item that matches the conditions defined by the specified predicate between the
        /// starting index and the end of the collection and return its zero-based index.
        /// </summary>
        /// <param name="startIndex">The zero-based starting index for the search</param>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions of the match</param>
        /// <returns>Returns the zero-based index of the first occurrence of an item that matches the conditions
        /// defined by the predicate or -1 if nothing is found.</returns>
        public int FindIndex(int startIndex, Predicate<T> match)
        {
            return ((List<T>)base.Items).FindIndex(startIndex, base.Count, match);
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
        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            return ((List<T>)base.Items).FindIndex(startIndex, count, match);
        }

        /// <summary>
        /// Search the collection for the last item that matches the conditions in the specified predicate
        /// </summary>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions of the match</param>
        /// <returns>The last item found that matches the conditions or the default value for type T if nothing
        /// is found.</returns>
        public T FindLast(Predicate<T> match)
        {
            return ((List<T>)base.Items).FindLast(match);
        }

        /// <summary>
        /// Search for the last item that matches the conditions defined by the specified predicate and return
        /// its zero-based index.
        /// </summary>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions of the match</param>
        /// <returns>Returns the zero-based index of the last occurrence of an item that matches the conditions
        /// defined by the predicate or -1 if nothing is found.</returns>
        /// <overloads>There are three overloads for this method</overloads>
        public int FindLastIndex(Predicate<T> match)
        {
            return ((List<T>)base.Items).FindLastIndex(0, base.Count, match);
        }

        /// <summary>
        /// Search for the last item that matches the conditions defined by the specified predicate between the
        /// starting index and searching backwards to the start of the collection and return its zero-based
        /// index.
        /// </summary>
        /// <param name="startIndex">The zero-based starting index for the backwards search</param>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions of the match</param>
        /// <returns>Returns the zero-based index of the last occurrence of an item that matches the conditions
        /// defined by the predicate or -1 if nothing is found.</returns>
        public int FindLastIndex(int startIndex, Predicate<T> match)
        {
            return ((List<T>)base.Items).FindLastIndex(startIndex, base.Count, match);
        }

        /// <summary>
        /// Search for the last item that matches the conditions defined by the specified predicate between the
        /// starting index and searching backwards for the given number of items and return its zero-based
        /// index.
        /// </summary>
        /// <param name="startIndex">The zero-based starting index for the backwards search</param>
        /// <param name="count">The number of items to search</param>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions of the match</param>
        /// <returns>Returns the zero-based index of the first occurrence of an item that matches the conditions
        /// defined by the predicate or -1 if nothing is found.</returns>
        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            return ((List<T>)base.Items).FindLastIndex(startIndex, count, match);
        }

        /// <summary>
        /// Performs the specified action on each item in the collection
        /// </summary>
        /// <param name="action">The <see cref="Action{T}"/> delegate to perform on each item in the collection</param>
        public void ForEach(Action<T> action)
        {
            ((List<T>)base.Items).ForEach(action);
        }

        /// <summary>
        /// Remove all items that match the conditions defined by the specified predicate
        /// </summary>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions of the match</param>
        /// <returns>Returns the number of items removed from the collection</returns>
        public int RemoveAll(Predicate<T> match)
        {
            int count = ((List<T>)base.Items).RemoveAll(match);

            if(count != 0)
                base.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));

            return count;
        }

        /// <summary>
        /// Remove the specified range of items from the collection
        /// </summary>
        /// <param name="index">The zero-based starting index of the range to remove</param>
        /// <param name="count">The number of items to remove</param>
        public void RemoveRange(int index, int count)
        {
            ((List<T>)base.Items).RemoveRange(index, count);

            base.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        /// <summary>
        /// Sort the items in the collection using the default comparer
        /// </summary>
        /// <overloads>There are three overloads for this method</overloads>
        public void Sort()
        {
            ((List<T>)base.Items).Sort(0, base.Count, null);

            base.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        /// <summary>
        /// Sort the items in the collection using the specified comparer
        /// </summary>
        /// <param name="comparer">The <see cref="IComparer{T}"/> implementation to use when comparing items or
        /// null to use the default comparer.</param>
        public void Sort(IComparer<T> comparer)
        {
            ((List<T>)base.Items).Sort(comparer);

            base.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        /// <summary>
        /// Sort the items in the collection using the specified <see cref="Comparison{T}"/> delegate
        /// </summary>
        /// <param name="comparison">The <see cref="Comparison{T}"/> delegate to use for the comparisons</param>
        public void Sort(Comparison<T> comparison)
        {
            ((List<T>)base.Items).Sort(comparison);

            base.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        /// <summary>
        /// Determine if every item in the collection matches the conditions defined by the specified predicate
        /// </summary>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that defines the conditions to check in
        /// each item.</param>
        /// <returns>Returns true if every item in the collection matches the conditions defined in the specified
        /// predicate or if the collection is empty.  It returns false if at least one item does not match the
        /// conditions.</returns>
        public bool TrueForAll(Predicate<T> match)
        {
            return ((List<T>)base.Items).TrueForAll(match);
        }
        #endregion

        #region IBindingList<T> sorting support
        //=====================================================================

        /// <summary>
        /// This is used to indicate whether or not sorting is supported
        /// </summary>
        /// <value>This always returns true.</value>
        protected override bool SupportsSortingCore => true;

        /// <summary>
        /// This returns a flag indicating whether or not sorting is currently applied
        /// </summary>
        /// <value>Returns true if sorting is applied or false if not</value>
        protected override bool IsSortedCore => isSorted;

        /// <summary>
        /// This returns the current sort direction if sorting has been applied
        /// </summary>
        protected override ListSortDirection SortDirectionCore => sortDirection;

        /// <summary>
        /// This returns the current property descriptor if sorting has been applied
        /// </summary>
        protected override PropertyDescriptor SortPropertyCore => sortProperty;

        /// <summary>
        /// This is overridden to apply a sort based on the selected property descriptor and sort direction
        /// </summary>
        /// <param name="prop">The property descriptor to use for the sort values</param>
        /// <param name="direction">The direction of the sort</param>
        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            PropertyComparer<T> comparer = new PropertyComparer<T>(prop, direction);
            ((List<T>)base.Items).Sort(comparer);

            sortProperty = prop;
            sortDirection = direction;
            isSorted = true;

            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        /// <summary>
        /// This is used to remove the sort
        /// </summary>
        /// <remarks>For PDI object collections, all this does is set the sorted flag to false.  The object
        /// remains sorted in the last order selected.</remarks>
        protected override void RemoveSortCore()
        {
            isSorted = false;
        }
        #endregion

        #region IBindingList<T> searching support
        //=====================================================================

        /// <summary>
        /// This is used to indicate whether or not searching is supported
        /// </summary>
        /// <value>This always returns true</value>
        protected override bool SupportsSearchingCore => true;

        /// <summary>
        /// This searches for the index of the item that has the specified property descriptor with the specified
        /// value.
        /// </summary>
        /// <param name="prop">The property descriptor used for the search</param>
        /// <param name="key">The value of the property to match</param>
        /// <returns>Returns the zero-based index of the item that matches the property descriptor and contains
        /// the specified value.</returns>
        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            return ((List<T>)base.Items).FindIndex(0, base.Count, (x) =>
                {
                    int result;

                    object value = PropertyComparer<T>.GetPropertyValue(x, prop.Name);

                    // Handle null values first
                    if(key == null && value == null)
                        return true;

                    if((key == null && value != null) || (key != null && value == null))
                        return false;

                    // Try comparing using IComparable.  If that won't work, see if they are equal.  If not,
                    // compare them as strings.
                    if(key is IComparable)
                        result = ((IComparable)key).CompareTo(value);
                    else
                        if(key.Equals(value))
                            result = 0;
                        else
                            result = String.Compare(key.ToString(), value.ToString(), StringComparison.Ordinal);

                    return (result == 0);
                });
        }
        #endregion
    }
}
