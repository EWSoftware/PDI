//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : VNoteCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2007-2025, Eric Woodruff, All rights reserved
//
// This file contains a collection class for VNote objects
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 08/19/2007  EFW  Created the code
//===============================================================================================================

// Ignore Spelling: vn sw

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;

using EWSoftware.PDI.Binding;
using EWSoftware.PDI.Properties;

namespace EWSoftware.PDI.Objects
{
    /// <summary>
    /// A type-safe collection of <see cref="VNote"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator and is serializable</remarks>
    [Serializable]
    public class VNoteCollection : ExtendedBindingList<VNote>
    {
        #region Constructor
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public VNoteCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="VNote"/> objects
        /// </summary>
        /// <param name="notes">The <see cref="IList{T}"/> of notes to add</param>
        public VNoteCollection(IList<VNote> notes) : base(notes)
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
        public VNote? this[string uniqueId]
        {
            get
            {
                for(int idx = 0; idx < base.Count; idx++)
                {
                    if(base[idx].UniqueId.Value == uniqueId)
                        return base[idx];
                }

                return null;
            }
            set
            {
                for(int idx = 0; idx < base.Count; idx++)
                {
                    if(base[idx].UniqueId.Value == uniqueId)
                    {
                        base[idx] = value!;
                        return;
                    }
                }

                throw new ArgumentException(LR.GetString("ExUIDNotFound"));
            }
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This can be used to convert an entire collection of vNotes to their string form
        /// </summary>
        /// <returns>A string containing the entire vNote collection</returns>
        public override string ToString()
        {
            using var sw = new StringWriter(new StringBuilder(4096), CultureInfo.InvariantCulture);

            this.WriteToStream(sw);
            return sw.ToString();
        }

        /// <summary>
        /// This can be used to write an entire collection of vNotes to a PDI data stream
        /// </summary>
        /// <param name="tw">A <see cref="System.IO.TextWriter"/> derived class to which the vNotes are written</param>
        /// <example>
        /// <code language="cs">
        /// // Create a vNote collection and some vNotes
        /// VNoteCollection vNotes = new VNoteCollection();
        /// VNote vn = new VNote();
        /// vn.Body.Value = "A new note";
        /// vNotes.Add(vn);
        ///
        /// vn = new VNote();
        /// vn.Body.Value = "A second note
        /// vNotes.Add(vn);
        ///
        /// // Open the file and write the vNotes to it
        /// StreamWriter sw = new StreamWriter(@"C:\Notes.vnt");
        /// vNotes.WriteToStream(sw);
        /// sw.Close();
        /// </code>
        /// <code language="vbnet">
        /// ' Create a vNote collection and some vNotes
        /// Dim vNotes As New VNoteCollection()
        /// Dim vn As New VNote()
        /// vn.Body.Value = "A new note"
        /// vNotes.Add(vn)
        ///
        /// vn = New VNote()
        /// vn.Body.Value = "A second note"
        /// vNotes.Add(vn)
        ///
        /// ' Open the file and write the vNotes to it
        /// Dim sw As New StreamWriter("C:\Notes.vnt")
        /// vNotes.WriteToStream(sw)
        /// sw.Close()
        /// </code>
        /// </example>
        public void WriteToStream(TextWriter tw)
        {
            StringBuilder? sb = null;

            if(tw is not StringWriter)
                sb = new StringBuilder(256);

            foreach(VNote n in this)
                n.WriteToStream(tw, sb);
        }

        /// <summary>
        /// This can be used to ensure that all vNotes in the collection have a unique ID assigned to them
        /// </summary>
        /// <param name="forceNew">If true, a new unique ID is assigned regardless of whether one already exists.
        /// If false and the vNote already has a unique ID, it keeps the old one.</param>
        public void AssignUniqueIds(bool forceNew)
        {
            foreach(VNote n in this)
                n.UniqueId.AssignNewId(forceNew);

            base.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        /// <summary>
        /// This can be used to propagate a version to all members of the collection
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
