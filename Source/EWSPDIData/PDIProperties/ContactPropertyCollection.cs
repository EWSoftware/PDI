//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : ContactPropertyCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a collection class for ContactProperty objects.  It is used with the iCalendar Personal
// Data Interchange (PDI) classes.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/14/2004  EFW  Created the code
// 03/28/2007  EFW  Converted to use a generic base class
//===============================================================================================================

using System.Collections.Generic;
using System.ComponentModel;

using EWSoftware.PDI.Binding;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// A type-safe collection of <see cref="ContactProperty"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator</remarks>
    public class ContactPropertyCollection : ExtendedBindingList<ContactProperty>
    {
        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public ContactPropertyCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="ContactProperty"/> objects
        /// </summary>
        /// <param name="contacts">The <see cref="IList{T}"/> of contacts to add</param>
        public ContactPropertyCollection(IList<ContactProperty> contacts) : base(contacts)
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Add a <see cref="ContactProperty"/> to the collection and assign it the specified contact value
        /// </summary>
        /// <param name="contact">The contact value to assign to the new property</param>
        /// <returns>Returns the new property that was created and added to the collection</returns>
        public ContactProperty Add(string contact)
        {
            ContactProperty c = new ContactProperty { Value = contact };

            base.Add(c);

            return c;
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
