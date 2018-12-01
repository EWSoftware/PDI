//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : LabelPropertyCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/22/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
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

using System;
using System.Collections.Generic;
using System.ComponentModel;

using EWSoftware.PDI.Binding;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// A type-safe collection of <see cref="LabelProperty"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator</remarks>
    public class LabelPropertyCollection : ExtendedBindingList<LabelProperty>
    {
        #region Constructor
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public LabelPropertyCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="LabelProperty"/> objects
        /// </summary>
        /// <param name="labels">The <see cref="IList{T}"/> of labels to add</param>
        public LabelPropertyCollection(IList<LabelProperty> labels) : base(labels)
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Add a <see cref="LabelProperty"/> to the collection and assign it the specified value
        /// </summary>
        /// <param name="label">The label value to assign to the new property</param>
        /// <returns>Returns the new property that was created and added to the collection</returns>
        /// <overloads>There are two overloads for this method</overloads>
        public LabelProperty Add(string label)
        {
            LabelProperty l = new LabelProperty { Value = label };

            base.Add(l);

            return l;
        }

        /// <summary>
        /// Add a <see cref="LabelProperty"/> to the collection and assign it the specified value and type(s)
        /// </summary>
        /// <param name="addressTypes">The address types to assign to the new property</param>
        /// <param name="label">The label value to assign to the new property</param>
        /// <returns>Returns the new property that was created and added to the collection</returns>
        public LabelProperty Add(AddressTypes addressTypes, string label)
        {
            LabelProperty l = new LabelProperty { AddressTypes = addressTypes, Value = label };

            base.Add(l);

            return l;
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

        /// <summary>
        /// This can be used to set one label as the preferred label
        /// </summary>
        /// <param name="label">The label to make preferred</param>
        /// <remarks>The Preferred flag is turned off in all labels except for the one specified</remarks>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the collection does not contain the
        /// specified label object.</exception>
        /// <overloads>There are two overloads for this method</overloads>
        public void SetPreferred(LabelProperty label)
        {
            int idx = base.IndexOf(label);

            if(idx == -1)
                throw new ArgumentOutOfRangeException(nameof(label), label, LR.GetString("ExLabelNotInCollection"));

            this.SetPreferred(idx);
        }

        /// <summary>
        /// This can be used to set one label as the preferred label
        /// </summary>
        /// <param name="idx">The index of the label to make preferred</param>
        /// <remarks>The Preferred flag is turned off in all labels except for the one at the specified index</remarks>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the index is out of bounds</exception>
        public void SetPreferred(int idx)
        {
            if(idx < 0 || idx > base.Count)
                throw new ArgumentOutOfRangeException(nameof(idx), idx, LR.GetString("ExLabelInvalidIndex"));

            for(int lblIdx = 0; lblIdx < base.Count; lblIdx++)
                if(lblIdx == idx)
                    this[lblIdx].AddressTypes |= AddressTypes.Preferred;
                else
                    this[lblIdx].AddressTypes &= ~AddressTypes.Preferred;

            base.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        /// <summary>
        /// This method can be used to find the first label that has one of the specified address types
        /// </summary>
        /// <param name="addressType">The address type to match</param>
        /// <returns>The first label with an address type matching one of those specified or null if not found</returns>
        /// <remarks>Multiple address types can be specified.  Including <c>Preferred</c> will limit the match to
        /// the one with the <c>Preferred</c> flag set.  If a preferred label with one of the given types cannot
        /// be found, it will return the first label matching one of the given types without the <c>Preferred</c>
        /// flag set.  If no label can be found, it returns null.</remarks>
        public LabelProperty FindFirstByType(AddressTypes addressType)
        {
            AddressTypes addrNoPref = addressType & ~AddressTypes.Preferred;
            bool usePreferred = (addrNoPref != addressType);

            foreach(LabelProperty label in this)
                if((label.AddressTypes & addrNoPref) != 0 && (!usePreferred ||
                  (label.AddressTypes & AddressTypes.Preferred) != 0))
                    return label;

            // Try again without the preferred flag?
            if(usePreferred)
                return this.FindFirstByType(addrNoPref);

            return null;
        }
        #endregion
    }
}
