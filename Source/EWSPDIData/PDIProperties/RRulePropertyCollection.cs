//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : RRulePropertyCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/19/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a collection class for RRuleProperty and ExRuleProperty objects.  It is used by the
// Personal Data Interchange (PDI) vCalendar and iCalendar classes.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 08/31/2004  EFW  Created the code
// 03/30/2007  EFW  Converted to use a generic base class
//===============================================================================================================

using System.Collections.Generic;
using System.ComponentModel;

using EWSoftware.PDI.Binding;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// A type-safe collection of <see cref="RRuleProperty"/> objects
    /// </summary>
    /// <remarks>This is also used to contain a collection of <see cref="ExRuleProperty"/> objects which are
    /// derived from <see cref="RRuleProperty"/>.</remarks>
    public class RRulePropertyCollection : ExtendedBindingList<RRuleProperty>
    {
        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public RRulePropertyCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="RRuleProperty"/> objects
        /// </summary>
        /// <param name="rules">The <see cref="IList{T}"/> of rules to add</param>
        public RRulePropertyCollection(IList<RRuleProperty> rules) : base(rules)
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
