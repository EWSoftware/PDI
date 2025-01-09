//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : FreeBusyPropertyCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains a collection class for FreeBusyProperty objects.  It is used by the Personal Data
// Interchange (PDI) VFreeBusy class.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 09/10/2004  EFW  Created the code
// 03/28/2007  EFW  Converted to use a generic base class
//===============================================================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;

using EWSoftware.PDI.Binding;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// A type-safe collection of <see cref="FreeBusyProperty"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator</remarks>
    public class FreeBusyPropertyCollection : ExtendedBindingList<FreeBusyProperty>
    {
        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public FreeBusyPropertyCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="FreeBusyProperty"/> objects
        /// </summary>
        /// <param name="freeBusys">The <see cref="IList{T}"/> of free/busy items to add</param>
        public FreeBusyPropertyCollection(IList<FreeBusyProperty> freeBusys) : base(freeBusys)
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Add a <see cref="FreeBusyProperty"/> to the collection and assign it the specified type and period
        /// </summary>
        /// <param name="type">The type to assign to the new property</param>
        /// <param name="p">The period value to assign to the new property</param>
        /// <returns>Returns the new property that was created and added to the collection</returns>
        public FreeBusyProperty Add(FreeBusyType type, Period p)
        {
            FreeBusyProperty fb = new() { FreeBusyType = type, PeriodValue = p };

            this.Add(fb);

            return fb;
        }

        /// <summary>
        /// This is used to sort the collection in ascending or descending order
        /// </summary>
        /// <param name="ascending">Pass true for ascending order, false for descending order</param>
        public void Sort(bool ascending)
        {
            ((List<FreeBusyProperty>)base.Items).Sort((x, y) =>
            {
                int result;

                if(ascending)
                {
                    result = DateTime.Compare(x.PeriodValue.StartDateTime, y.PeriodValue.StartDateTime);

                    if(result == 0)
                        result = DateTime.Compare(x.PeriodValue.EndDateTime, y.PeriodValue.EndDateTime);
                }
                else
                {
                    // Flip result for descending order
                    result = DateTime.Compare(y.PeriodValue.StartDateTime, x.PeriodValue.StartDateTime);

                    if(result == 0)
                        result = DateTime.Compare(y.PeriodValue.EndDateTime, x.PeriodValue.EndDateTime);
                }

                return result;
            });

            base.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
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
