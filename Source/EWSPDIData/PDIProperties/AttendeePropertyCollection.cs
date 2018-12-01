//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : AttendeeProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a collection class for AttendeeProperty objects
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 04/04/2004  EFW  Created the code
// 03/28/2007  EFW  Converted to use a generic base class
//===============================================================================================================

using System.Collections.Generic;
using System.ComponentModel;

using EWSoftware.PDI.Binding;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// A type-safe collection of <see cref="AttendeeProperty"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator</remarks>
    public class AttendeePropertyCollection : ExtendedBindingList<AttendeeProperty>
    {
        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public AttendeePropertyCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="AttendeeProperty"/> objects
        /// </summary>
        /// <param name="attendees">The <see cref="IList{T}"/> of attendees to add</param>
        public AttendeePropertyCollection(IList<AttendeeProperty> attendees) : base(attendees)
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Add an <see cref="AttendeeProperty"/> to the collection and assign it the specified value
        /// </summary>
        /// <param name="attendee">The value to assign to the new property</param>
        /// <returns>Returns the new property that was created and added to the collection</returns>
        /// <overloads>There are two overloads for this method</overloads>
        public AttendeeProperty Add(string attendee)
        {
            AttendeeProperty a = new AttendeeProperty { Value = attendee };

            base.Add(a);

            return a;
        }

        /// <summary>
        /// Add an <see cref="AttendeeProperty"/> to the collection and assign it the specified value and common
        /// name.
        /// </summary>
        /// <param name="attendee">The value to assign to the new property</param>
        /// <param name="commonName">The common name value to assign to the new property</param>
        /// <returns>Returns the new property that was created and added to the collection</returns>
        public AttendeeProperty Add(string attendee, string commonName)
        {
            AttendeeProperty a = new AttendeeProperty { Value = attendee, CommonName = commonName };

            base.Add(a);

            return a;
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
