//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : RDatePropertyCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains a collection class for RDateProperty objects.  It is used with the Personal Data
// Interchange (PDI) vCalendar and iCalendar classes.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 09/10/2004  EFW  Created the code
// 03/30/2007  EFW  Converted to use a generic base class
//===============================================================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;

using EWSoftware.PDI.Binding;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// A type-safe collection of <see cref="RDateProperty"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator</remarks>
    public class RDatePropertyCollection : ExtendedBindingList<RDateProperty>
    {
        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public RDatePropertyCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="RDateProperty"/> objects
        /// </summary>
        /// <param name="recurDates">The <see cref="IList{T}"/> of recur dates to add</param>
        public RDatePropertyCollection(IList<RDateProperty> recurDates) : base(recurDates)
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Add an <see cref="RDateProperty"/> to the collection and assign it the specified date/time value
        /// </summary>
        /// <param name="dt">The date/time value to assign to the new property</param>
        /// <returns>Returns the new property that was created and added to the collection</returns>
        /// <overloads>There are two overloads for this method</overloads>
        public RDateProperty Add(DateTime dt)
        {
            RDateProperty rdt = new() { DateTimeValue = dt };

            this.Add(rdt);

            return rdt;
        }

        /// <summary>
        /// Add an <see cref="RDateProperty"/> to the collection and assign it the specified period value
        /// </summary>
        /// <param name="p">The period value to assign to the new property</param>
        /// <returns>Returns the new property that was created and added to the collection</returns>
        public RDateProperty Add(Period p)
        {
            RDateProperty rdt = new() { PeriodValue = p };

            this.Add(rdt);

            return rdt;
        }

        /// <summary>
        /// This is used to propagate a common version to all objects in the collection
        /// </summary>
        /// <param name="version">The version to use</param>
        public void PropagateVersion(SpecificationVersions version)
        {
            foreach(PDIObject o in this)
                o.Version = version;

            base.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
        #endregion
    }
}
