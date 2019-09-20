//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : ClientPidMapPropertyCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 04/22/2019
// Note    : Copyright 2019, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a collection class for ClientPidMapProperty objects.  It is used with the Personal Data
// Interchange (PDI) vCard class.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 04/22/2019  EFW  Created the code
//===============================================================================================================

using System.Collections.Generic;
using System.ComponentModel;

using EWSoftware.PDI.Binding;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// A type-safe collection of <see cref="ClientPidMapProperty"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator</remarks>
    public class ClientPidMapPropertyCollection : ExtendedBindingList<ClientPidMapProperty>
    {
        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public ClientPidMapPropertyCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="ClientPidMapProperty"/> objects
        /// </summary>
        /// <param name="pidMaps">The <see cref="IList{T}"/> of property ID maps to add</param>
        public ClientPidMapPropertyCollection(IList<ClientPidMapProperty> pidMaps) : base(pidMaps)
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Add a <see cref="ClientPidMapProperty"/> to the collection and assign it the specified values
        /// </summary>
        /// <param name="id">The id value to assign to the new property</param>
        /// <param name="uri">The URI for the property ID map</param>
        /// <returns>Returns the new property that was created and added to the collection</returns>
        public ClientPidMapProperty Add(int id, string uri)
        {
            var pidMap = new ClientPidMapProperty { Id = id, Uri = uri };

            base.Add(pidMap);

            return pidMap;
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
