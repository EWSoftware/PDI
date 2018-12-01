//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : PropertyComparer.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2007-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a generic comparer class that compares two items based on values retrieved from a property
// descriptor.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 04/07/2007  EFW  Created the code
//===============================================================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace EWSoftware.PDI.Binding
{
    /// <summary>
    /// This comparer is used to compare two items based on values retrieved from a property descriptor
    /// </summary>
    /// <typeparam name="T">The type of the objects to compare</typeparam>
    /// <remarks>This takes into account child property references as well (those where the name contains an
    /// underscore).</remarks>
    public class PropertyComparer<T> : IComparer<T>
    {
        #region Private data members
        //=====================================================================

        private PropertyDescriptor property;
        private readonly ListSortDirection direction;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="descriptor">The property descriptor to use</param>
        /// <param name="sortDirection">The sort direction</param>
        public PropertyComparer(PropertyDescriptor descriptor, ListSortDirection sortDirection)
        {
            property = descriptor;
            direction = sortDirection;
        }
        #endregion

        #region IComparer<T> implementation
        //=====================================================================

        /// <summary>
        /// This compares the two items using the values obtained from the property descriptors and returns a
        /// value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare</param>
        /// <param name="y">The second object to compare</param>
        /// <returns>Returns less than zero when object X is less than object Y, zero if they are equal, greater
        /// than zero when object X is greater than object Y.</returns>
        public int Compare(T x, T y)
        {
            object value1, value2;
            int result;

            // Get the value from the property in each object
            value1 = PropertyComparer<T>.GetPropertyValue(x, property.Name);
            value2 = PropertyComparer<T>.GetPropertyValue(y, property.Name);

            // Handle null values first
            if(value1 == null && value2 == null)
                return 0;

            if(value1 == null && value2 != null)
                return (direction == ListSortDirection.Ascending) ?  -1 : 1;

            if(value1 != null && value2 == null)
                return (direction == ListSortDirection.Ascending) ? 1 : -1;

            // Try comparing using IComparable.  If that won't work, see if they are equal.  If not, compare them
            // as strings.
            if(value1 is IComparable comp)
                result = comp.CompareTo(value2);
            else
                if(value1.Equals(value2))
                    result = 0;
                else
                    result = String.Compare(value1.ToString(), value2.ToString(), StringComparison.Ordinal);

            // Invert the result for descending order
            if(direction == ListSortDirection.Descending)
                result *= -1;

            return result;
        }
        #endregion

        #region Helper method
        //=====================================================================

        /// <summary>
        /// This is used to get the property value from the object
        /// </summary>
        /// <param name="x">The object from which to retrieve the property value</param>
        /// <param name="propertyName">The name of the property from which to get the value</param>
        /// <returns>The value of the property</returns>
        /// <remarks>This takes into account child property references as well (those where the name contains an
        /// underscore).</remarks>
        public static object GetPropertyValue(T x, string propertyName)
        {
            Type t;
            object value = x;

            string[] propNames = propertyName.Split('_');

            foreach(string name in propNames)
            {
                t = value.GetType();
                value = t.GetProperty(name).GetValue(value, null);
            }

            return value;
        }
        #endregion
    }
}
