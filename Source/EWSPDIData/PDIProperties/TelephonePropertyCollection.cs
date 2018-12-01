//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : TelephonePropertyCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/22/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a collection class for TelephoneProperty objects.  It is used with the Personal Data
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
    /// A type-safe collection of <see cref="TelephoneProperty"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator</remarks>
    public class TelephonePropertyCollection : ExtendedBindingList<TelephoneProperty>
    {
        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public TelephonePropertyCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="TelephoneProperty"/> objects
        /// </summary>
        /// <param name="phones">The <see cref="IList{T}"/> of phone numbers to add</param>
        public TelephonePropertyCollection(IList<TelephoneProperty> phones) : base(phones)
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Add a <see cref="TelephoneProperty"/> to the collection and assign it the specified value
        /// </summary>
        /// <param name="phone">The phone number value to assign to the new property</param>
        /// <returns>Returns the new property that was created and added to the collection</returns>
        public TelephoneProperty Add(string phone)
        {
            TelephoneProperty t = new TelephoneProperty { Value = phone };

            base.Add(t);

            return t;
        }

        /// <summary>
        /// Add a <see cref="TelephoneProperty"/> to the collection and assign it the specified value and type(s)
        /// </summary>
        /// <param name="phoneTypes">The telephone types to assign to the new property</param>
        /// <param name="phone">The phone number value to assign to the new property</param>
        /// <returns>Returns the new property that was created and added to the collection</returns>
        public TelephoneProperty Add(PhoneTypes phoneTypes, string phone)
        {
            TelephoneProperty t = new TelephoneProperty { PhoneTypes = phoneTypes, Value = phone };

            base.Add(t);

            return t;
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
        /// This can be used to set one phone number as the preferred phone number
        /// </summary>
        /// <param name="phone">The phone property to make preferred</param>
        /// <remarks>The Preferred flag is turned off in all phone numbers except for the one specified</remarks>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the collection does not contain the
        /// specified phone number object.</exception>
        /// <overloads>There are two overloads for this method</overloads>
        public void SetPreferred(TelephoneProperty phone)
        {
            int idx = base.IndexOf(phone);

            if(idx == -1)
                throw new ArgumentOutOfRangeException(nameof(phone), phone, LR.GetString("ExPhoneNotInCollection"));

            this.SetPreferred(idx);
        }

        /// <summary>
        /// This can be used to set one phone number as the preferred phone number
        /// </summary>
        /// <param name="idx">The index of the phone property to make preferred</param>
        /// <remarks>The Preferred flag is turned off in all phone numbers except for the one at the specified
        /// index.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the index is out of bounds</exception>
        public void SetPreferred(int idx)
        {
            if(idx < 0 || idx > base.Count)
                throw new ArgumentOutOfRangeException(nameof(idx), idx, LR.GetString("ExPhoneInvalidIndex"));

            for(int phoneIdx = 0; phoneIdx < base.Count; phoneIdx++)
                if(phoneIdx == idx)
                    this[phoneIdx].PhoneTypes |= PhoneTypes.Preferred;
                else
                    this[phoneIdx].PhoneTypes &= ~PhoneTypes.Preferred;

            base.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        /// <summary>
        /// This method can be used to find the first phone number that has one of the specified phone types
        /// </summary>
        /// <param name="phoneType">The phone type to match</param>
        /// <returns>The first phone number with a phone type matching one of those specified or null if not found</returns>
        /// <remarks>Multiple phone types can be specified.  Including <c>Preferred</c> will limit the match to
        /// the one with the <c>Preferred</c> flag set.  If a preferred phone number with one of the given types
        /// cannot be found, it will return the first phone number matching one of the given types without the
        /// <c>Preferred</c> flag set.  If no phone number can be found, it returns null.</remarks>
        public TelephoneProperty FindFirstByType(PhoneTypes phoneType)
        {
            PhoneTypes phoneNoPref = phoneType & ~PhoneTypes.Preferred;
            bool usePreferred = (phoneNoPref != phoneType);

            foreach(TelephoneProperty phone in this)
                if((phone.PhoneTypes & phoneNoPref) != 0 && (!usePreferred ||
                  (phone.PhoneTypes & PhoneTypes.Preferred) != 0))
                    return phone;

            // Try again without the preferred flag?
            if(usePreferred)
                return this.FindFirstByType(phoneNoPref);

            return null;
        }
        #endregion
    }
}
