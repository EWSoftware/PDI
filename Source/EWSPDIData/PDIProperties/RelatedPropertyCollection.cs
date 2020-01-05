//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : RelatedPropertyCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/20/2019
// Note    : Copyright 2019, Eric Woodruff, All rights reserved
//
// This file contains a collection class for RelatedProperty objects.  It is used with the Personal Data
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
    /// A type-safe collection of <see cref="RelatedProperty"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator</remarks>
    public class RelatedPropertyCollection : ExtendedBindingList<RelatedProperty>
    {
        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor.</overloads>
        public RelatedPropertyCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="RelatedProperty"/> objects
        /// </summary>
        /// <param name="Relateds">The <see cref="IList{T}"/> of related-to items to add</param>
        public RelatedPropertyCollection(IList<RelatedProperty> Relateds) : base(Relateds)
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Add a <see cref="RelatedProperty"/> to the collection and assign it the specified type and value
        /// </summary>
        /// <param name="relatedTypes">The related types to assign to the new property</param>
        /// <param name="relation">The value to assign to the new property</param>
        /// <returns>Returns the new property that was created and added to the collection</returns>
        public RelatedProperty Add(RelatedTypes relatedTypes, string relation)
        {
            RelatedProperty rt = new RelatedProperty { RelatedTypes = relatedTypes, Value = relation };

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

        /// <summary>
        /// This method can be used to find the first entry that has one of the specified related types
        /// </summary>
        /// <param name="relatedTypes">The phone type to match</param>
        /// <returns>The entry with a related type matching one of those specified or null if not found</returns>
        /// <remarks>Multiple related types can be specified.  If no entry can be found, it returns null.</remarks>
        public RelatedProperty FindFirstByType(RelatedTypes relatedTypes)
        {
            foreach(var r in this)
                if((r.RelatedTypes & relatedTypes) != 0)
                    return r;

            return null;
        }
        #endregion
    }
}
