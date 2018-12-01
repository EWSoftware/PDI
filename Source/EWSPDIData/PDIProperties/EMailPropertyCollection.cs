//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : EMailPropertyCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/22/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a collection class for EMailProperty objects.  It is used with the Personal Data
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
    /// A type-safe collection of <see cref="EMailProperty"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator</remarks>
    public class EMailPropertyCollection : ExtendedBindingList<EMailProperty>
    {
        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public EMailPropertyCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="EMailProperty"/> objects
        /// </summary>
        /// <param name="emailAddresses">The <see cref="IList{T}"/> of e-mail addresses to add</param>
        public EMailPropertyCollection(IList<EMailProperty> emailAddresses) : base(emailAddresses)
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Add an <see cref="EMailProperty"/> to the collection and assign it the specified value
        /// </summary>
        /// <param name="email">The e-mail address value to assign to the new property</param>
        /// <returns>Returns the new property that was created and added to the collection</returns>
        public EMailProperty Add(string email)
        {
            EMailProperty e = new EMailProperty { Value = email };

            base.Add(e);

            return e;
        }

        /// <summary>
        /// Add an <see cref="EMailProperty"/> to the collection and assign it the specified value and type(s)
        /// </summary>
        /// <param name="emailTypes">The e-mail types to assign to the new property</param>
        /// <param name="email">The e-mail address value to assign to the new property</param>
        /// <returns>Returns the new property that was created and added to the collection</returns>
        public EMailProperty Add(EMailTypes emailTypes, string email)
        {
            EMailProperty e = new EMailProperty { EMailTypes = emailTypes, Value = email };

            base.Add(e);

            return e;
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
        /// This can be used to set one e-mail address as the preferred e-mail address
        /// </summary>
        /// <param name="email">The e-mail address to make preferred</param>
        /// <remarks>The Preferred flag is turned off in all e-mail addresses except for the one specified</remarks>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the collection does not contain the
        /// specified e-mail address object.</exception>
        /// <overloads>There are two overloads for this method</overloads>
        public void SetPreferred(EMailProperty email)
        {
            int idx = base.IndexOf(email);

            if(idx == -1)
                throw new ArgumentOutOfRangeException(nameof(email), email, LR.GetString("ExEAddrNotInCollection"));

            this.SetPreferred(idx);
        }

        /// <summary>
        /// This can be used to set one e-mail address as the preferred e-mail address
        /// </summary>
        /// <param name="idx">The index of the address to make preferred</param>
        /// <remarks>The Preferred flag is turned off in all e-mail addresses except for the one at the specified
        /// index.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the index is out of bounds</exception>
        public void SetPreferred(int idx)
        {
            if(idx < 0 || idx > base.Count)
                throw new ArgumentOutOfRangeException(nameof(idx), idx, LR.GetString("ExEAddrInvalidIndex"));

            for(int nAddrIdx = 0; nAddrIdx < base.Count; nAddrIdx++)
                if(nAddrIdx == idx)
                    this[nAddrIdx].EMailTypes |= EMailTypes.Preferred;
                else
                    this[nAddrIdx].EMailTypes &= ~EMailTypes.Preferred;

            base.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        /// <summary>
        /// This method can be used to find the first e-mail address that has one of the specified e-mail types
        /// </summary>
        /// <param name="emailType">The e-mail type to match</param>
        /// <returns>The first e-mail address with an e-mail type matching one of those specified or null if not
        /// found.</returns>
        /// <remarks>Multiple e-mail types can be specified.  Including <c>Preferred</c> will limit the match to
        /// the one with the <c>Preferred</c> flag set.  If a preferred e-mail address with one of the given
        /// types cannot be found, it will return the first e-mail address matching one of the given types
        /// without the <c>Preferred</c> flag set.  If no e-mail address can be found, it returns null.</remarks>
        public EMailProperty FindFirstByType(EMailTypes emailType)
        {
            EMailTypes emailNoPref = emailType & ~EMailTypes.Preferred;
            bool usePreferred = (emailNoPref != emailType);

            foreach(EMailProperty email in this)
                if((email.EMailTypes & emailNoPref) != 0 && (!usePreferred || (email.EMailTypes & EMailTypes.Preferred) != 0))
                    return email;

            // Try again without the preferred flag?
            if(usePreferred)
                return this.FindFirstByType(emailNoPref);

            return null;
        }
        #endregion
    }
}
