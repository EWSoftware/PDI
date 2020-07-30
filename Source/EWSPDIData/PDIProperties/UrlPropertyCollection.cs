//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : UrlPropertyCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 07/24/2020
// Note    : Copyright 2020, Eric Woodruff, All rights reserved
//
// This file contains a collection class for UrlProperty objects.  It is used by the Personal Data Interchange
// (PDI) iCalendar class.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 07/24/2020  EFW  Created the code
//===============================================================================================================

using System.Collections.Generic;
using System.ComponentModel;

using EWSoftware.PDI.Binding;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// A type-safe collection of <see cref="UrlProperty"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator</remarks>
    public class UrlPropertyCollection : ExtendedBindingList<UrlProperty>
    {
        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public UrlPropertyCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="UrlProperty"/> objects
        /// </summary>
        /// <param name="urls">The <see cref="IList{T}"/> of URLs to add</param>
        public UrlPropertyCollection(IList<UrlProperty> urls) : base(urls)
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Add a <see cref="UrlProperty"/> to the collection and assign it the specified URL
        /// </summary>
        /// <param name="url">The URL to assign to the new property</param>
        /// <returns>Returns the new property that was created and added to the collection</returns>
        public UrlProperty Add(string url)
        {
            var urlProp = new UrlProperty { Value = url };

            this.Add(urlProp);

            return urlProp;
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
