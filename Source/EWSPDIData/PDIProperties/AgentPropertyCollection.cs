//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : AgentPropertyCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/18/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a collection class for AgentProperty objects.  It is used with the Personal Data
// Interchange (PDI) classes such as vCalendar, iCalendar, and vCard.
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

using System.Collections.Generic;
using System.ComponentModel;

using EWSoftware.PDI.Binding;
using EWSoftware.PDI.Objects;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// A type-safe collection of <see cref="AgentProperty"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator</remarks>
    public class AgentPropertyCollection : ExtendedBindingList<AgentProperty>
    {
        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public AgentPropertyCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="AgentProperty"/> objects
        /// </summary>
        /// <param name="agents">The <see cref="IList{T}"/> of agents to add</param>
        public AgentPropertyCollection(IList<AgentProperty> agents) : base(agents)
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Add an <see cref="AgentProperty"/> to the collection and assign it the specified vCard
        /// </summary>
        /// <param name="vCard">The vCard value to assign to the new property</param>
        /// <returns>Returns the new property that was created and added to the collection</returns>
        public AgentProperty Add(VCard vCard)
        {
            AgentProperty a = new AgentProperty();
            a.VCard = vCard;
            base.Add(a);

            return a;
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
