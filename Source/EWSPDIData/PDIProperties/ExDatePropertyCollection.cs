//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : ExDatePropertyCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a collection class for ExDateProperty objects.  It is used by the Personal Data
// Interchange (PDI) vCalendar and iCalendar classes.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/30/2004  EFW  Created the code
// 03/28/2007  EFW  Converted to use a generic base class
//===============================================================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;

using EWSoftware.PDI.Binding;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// A type-safe collection of <see cref="ExDateProperty"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator</remarks>
    public class ExDatePropertyCollection : ExtendedBindingList<ExDateProperty>
    {
        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public ExDatePropertyCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="ExDateProperty"/> objects
        /// </summary>
        /// <param name="exDates">The <see cref="IList{T}"/> of exception dates to add</param>
        public ExDatePropertyCollection(IList<ExDateProperty> exDates) : base(exDates)
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Add an <see cref="ExDateProperty"/> to the collection and assign it the specified date/time value
        /// </summary>
        /// <param name="dt">The date/time value to assign to the new property</param>
        /// <returns>Returns the new property that was created and added to the collection</returns>
        public ExDateProperty Add(DateTime dt)
        {
            ExDateProperty exd = new ExDateProperty { DateTimeValue = dt };

            base.Add(exd);

            return exd;
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
