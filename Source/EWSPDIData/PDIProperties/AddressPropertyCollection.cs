//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : AddressPropertyCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/22/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a collection class for AddressProperty objects.  It is used with the Personal Data
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
// 03/28/2007  EFW  Converted to use a generic base class
//===============================================================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;

using EWSoftware.PDI.Binding;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// A type-safe collection of <see cref="AddressProperty"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator</remarks>
    public class AddressPropertyCollection : ExtendedBindingList<AddressProperty>
    {
        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public AddressPropertyCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="AddressProperty"/> objects
        /// </summary>
        /// <param name="addresses">The <see cref="IList{T}"/> of addresses to add</param>
        public AddressPropertyCollection(IList<AddressProperty> addresses) : base(addresses)
        {
        }
        #endregion

        #region Methods
        //=====================================================================

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
        /// This can be used to set one address as the preferred address
        /// </summary>
        /// <param name="address">The address to make preferred</param>
        /// <remarks>The Preferred flag is turned off in all addresses except for the one specified</remarks>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the collection does not contain the
        /// specified address object.</exception>
        /// <overloads>There are two overloads for this method</overloads>
        public void SetPreferred(AddressProperty address)
        {
            int idx = base.IndexOf(address);

            if(idx == -1)
                throw new ArgumentOutOfRangeException(nameof(address), address, LR.GetString("ExAddrNotInCollection"));

            this.SetPreferred(idx);
        }

        /// <summary>
        /// This can be used to set one address as the preferred address
        /// </summary>
        /// <param name="idx">The index of the address to make preferred</param>
        /// <remarks>The Preferred flag is turned off in all addresses except for the one at the specified index</remarks>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the index is out of bounds</exception>
        public void SetPreferred(int idx)
        {
            if(idx < 0 || idx > base.Count)
                throw new ArgumentOutOfRangeException(nameof(idx), idx, LR.GetString("ExAddrInvalidIndex"));

            for(int addrIdx = 0; addrIdx < base.Count; addrIdx++)
                if(addrIdx == idx)
                    this[addrIdx].AddressTypes |= AddressTypes.Preferred;
                else
                    this[addrIdx].AddressTypes &= ~AddressTypes.Preferred;

            base.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        /// <summary>
        /// This method can be used to find the first address that has one of the specified address types
        /// </summary>
        /// <param name="addrType">The address type to match</param>
        /// <returns>The first address with an address type matching one of those specified or null if not found</returns>
        /// <remarks>Multiple address types can be specified.  Including <c>Preferred</c> will limit the match to
        /// the one with the <c>Preferred</c> flag set.  If a preferred address with one of the given types
        /// cannot be found, it will return the first address matching one of the given types without the
        /// <c>Preferred</c> flag set.  If no address can be found, it returns null.</remarks>
        public AddressProperty FindFirstByType(AddressTypes addrType)
        {
            AddressTypes addrNoPref = addrType & ~AddressTypes.Preferred;
            bool usePreferred = (addrNoPref != addrType);

            foreach(AddressProperty addr in this)
                if((addr.AddressTypes & addrNoPref) != 0 && (!usePreferred ||
                  (addr.AddressTypes & AddressTypes.Preferred) != 0))
                    return addr;

            // Try again without the preferred flag?
            if(usePreferred)
                return this.FindFirstByType(addrNoPref);

            return null;
        }
        #endregion
    }
}
