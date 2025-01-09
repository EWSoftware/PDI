//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : VCardCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains a collection class for VCard objects
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/14/2004  EFW  Created the code
// 03/21/2007  EFW  Converted to use a generic base class
//===============================================================================================================

// Ignore Spelling: vc sw

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
    /// A type-safe collection of <see cref="VCard"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator and is serializable</remarks>
    [Serializable]
    public class VCardCollection : ExtendedBindingList<VCard>
    {
        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public VCardCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="VCard"/> objects
        /// </summary>
        /// <param name="cards">The <see cref="IList{T}"/> of vCards to add</param>
        public VCardCollection(IList<VCard> cards) : base(cards)
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
        public VCard? this[string uniqueId]
        {
            get
            {
                for(int idx = 0; idx < this.Count; idx++)
                {
                    if(base[idx].UniqueId.Value == uniqueId)
                        return base[idx];
                }

                return null;
            }
            set
            {
                for(int idx = 0; idx < this.Count; idx++)
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
        /// This can be used to convert an entire collection of vCards to their string form
        /// </summary>
        /// <returns>A string containing the entire vCard collection</returns>
        public override string ToString()
        {
            using var sw = new StringWriter(new StringBuilder(4096), CultureInfo.InvariantCulture);

            this.WriteToStream(sw);
            return sw.ToString();
        }

        /// <summary>
        /// This can be used to write an entire collection of vCards to a PDI data stream
        /// </summary>
        /// <param name="tw">A <see cref="System.IO.TextWriter"/> derived class to which the vCards are written</param>
        /// <example>
        /// <code language="cs">
        /// // Create a vCard collection and some vCards
        /// VCardCollection vCards = new VCardCollection();
        /// VCard vc = new VCard();
        /// vc.FormattedName.Value = "Smith, John";
        /// vc.Name.FamilyName = "Smith";
        /// vc.Name.GivenName = "John";
        /// vCards.Add(vc);
        ///
        /// vc = new VCard();
        /// vc.FormattedName.Value = "Doe, Jane";
        /// vc.Name.FamilyName = "Doe";
        /// vc.Name.GivenName = "Jane";
        /// vCards.Add(vc);
        ///
        /// // Open the file and write the vCards to it
        /// StreamWriter sw = new StreamWriter(@"C:\AddressBook.vcf");
        /// vCards.WriteToStream(sw);
        /// sw.Close();
        /// </code>
        /// <code language="vbnet">
        /// ' Create a vCard collection and some vCards
        /// Dim vCards As New VCardCollection()
        /// Dim vc As New VCard()
        /// vc.FormattedName.Value = "Smith, John"
        /// vc.Name.FamilyName = "Smith"
        /// vc.Name.GivenName = "John"
        /// vCards.Add(vc)
        ///
        /// vc = New VCard()
        /// vc.FormattedName.Value = "Doe, Jane"
        /// vc.Name.FamilyName = "Doe"
        /// vc.Name.GivenName = "Jane"
        /// vCards.Add(vc)
        ///
        /// ' Open the file and write the vCards to it
        /// Dim sw As New StreamWriter("C:\AddressBook.vcf")
        /// vCards.WriteToStream(sw)
        /// sw.Close()
        /// </code>
        /// </example>
        public void WriteToStream(TextWriter tw)
        {
            StringBuilder? sb = null;

            if(tw is not StringWriter)
                sb = new StringBuilder(256);

            foreach(VCard c in this)
                c.WriteToStream(tw, sb);
        }

        /// <summary>
        /// This can be used to ensure that all vCards in the collection have a unique ID assigned to them
        /// </summary>
        /// <param name="forceNew">If true, a new unique ID is assigned regardless of whether one already exists.
        /// If false and the vCard already has a unique ID, it keeps the old one.</param>
        public void AssignUniqueIds(bool forceNew)
        {
            foreach(VCard c in this)
                c.UniqueId.AssignNewId(forceNew);

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
