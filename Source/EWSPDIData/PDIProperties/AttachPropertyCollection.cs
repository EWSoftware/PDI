//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : BinaryProperties.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/18/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a collection class for AttachProperty objects
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/26/2004  EFW  Created the code
// 03/28/2007  EFW  Converted to use a generic base class
//===============================================================================================================

using System.Collections.Generic;
using System.ComponentModel;

using EWSoftware.PDI.Binding;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// A type-safe collection of <see cref="AttachProperty"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator</remarks>
    public class AttachPropertyCollection : ExtendedBindingList<AttachProperty>
    {
        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public AttachPropertyCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="AttachProperty"/> objects
        /// </summary>
        /// <param name="attachments">The <see cref="IList{T}"/> of attachments to add</param>
        public AttachPropertyCollection(IList<AttachProperty> attachments) : base(attachments)
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
