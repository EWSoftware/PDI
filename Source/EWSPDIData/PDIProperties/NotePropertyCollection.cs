//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : NotePropertyCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains a collection class for LabelProperty objects.  It is used with the Personal Data
// Interchange (PDI) vCard class.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/14/2004  EFW  Created the code
// 03/30/2007  EFW  Converted to use a generic base class
//===============================================================================================================

using System.Collections.Generic;
using System.ComponentModel;

using EWSoftware.PDI.Binding;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// A type-safe collection of <see cref="NoteProperty"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator</remarks>
    public class NotePropertyCollection : ExtendedBindingList<NoteProperty>
    {
        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public NotePropertyCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="NoteProperty"/> objects
        /// </summary>
        /// <param name="notes">The <see cref="IList{T}"/> of notes to add</param>
        public NotePropertyCollection(IList<NoteProperty> notes) : base(notes)
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Add a <see cref="NoteProperty"/> to the collection and assign it the specified value
        /// </summary>
        /// <param name="note">The note value to assign to the new property</param>
        /// <returns>Returns the new property that was created and added to the collection</returns>
        public NoteProperty Add(string note)
        {
            NoteProperty n = new() { Value = note };

            this.Add(n);

            return n;
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
