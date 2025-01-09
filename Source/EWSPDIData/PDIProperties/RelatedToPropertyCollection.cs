//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : RelatedToPropertyCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains a collection class for RelatedToProperty objects.  It is used with the Personal Data
// Interchange (PDI) vCalendar and iCalendar class.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 04/04/2004  EFW  Created the code
// 03/30/2007  EFW  Converted to use a generic base class
//===============================================================================================================

using System.Collections.Generic;
using System.ComponentModel;

using EWSoftware.PDI.Binding;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// A type-safe collection of <see cref="RelatedToProperty"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator</remarks>
    public class RelatedToPropertyCollection : ExtendedBindingList<RelatedToProperty>
    {
        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor.</overloads>
        public RelatedToPropertyCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="RelatedToProperty"/> objects
        /// </summary>
        /// <param name="relatedTos">The <see cref="IList{T}"/> of related-to items to add</param>
        public RelatedToPropertyCollection(IList<RelatedToProperty> relatedTos) : base(relatedTos)
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Add a <see cref="RelatedToProperty"/> to the collection and assign it the specified type and value
        /// </summary>
        /// <param name="rType">The type to assign to the new property</param>
        /// <param name="relation">The value to assign to the new property</param>
        /// <returns>Returns the new property that was created and added to the collection</returns>
        public RelatedToProperty Add(RelationshipType rType, string relation)
        {
            RelatedToProperty rt = new() { RelationshipType = rType, Value = relation };

            base.Add(rt);

            return rt;
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
