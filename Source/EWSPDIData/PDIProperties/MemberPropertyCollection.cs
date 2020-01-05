﻿//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : MemberPropertyCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/20/2019
// Note    : Copyright 2019, Eric Woodruff, All rights reserved
//
// This file contains a collection class for MemberProperty objects.  It is used with the Personal Data
// Interchange (PDI) vCard class.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 12/20/2019  EFW  Created the code
//===============================================================================================================

using System.Collections.Generic;
using System.ComponentModel;

using EWSoftware.PDI.Binding;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// A type-safe collection of <see cref="MemberProperty"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator</remarks>
    public class MemberPropertyCollection : ExtendedBindingList<MemberProperty>
    {
        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor.</overloads>
        public MemberPropertyCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="MemberProperty"/> objects
        /// </summary>
        /// <param name="members">The <see cref="IList{T}"/> of related-to items to add</param>
        public MemberPropertyCollection(IList<MemberProperty> members) : base(members)
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
        #endregion
    }
}
