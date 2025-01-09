//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : ChildPropertyTypeDescriptionProvider.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/02/2025
// Note    : Copyright 2007-2025, Eric Woodruff, All rights reserved
//
// This file contains a custom type description provider that associates the ChildPropertyTypeDescriptor with a
// specific type of object.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 04/07/2007  EFW  Created the code
//===============================================================================================================

using System;
using System.ComponentModel;

namespace EWSoftware.PDI.Binding
{
    /// <summary>
    /// This is a custom type description provider that associates the <see cref="ChildPropertyTypeDescriptor" />
    /// with a specific type of object.
    /// </summary>
    public class ChildPropertyTypeDescriptionProvider : TypeDescriptionProvider
    {
        #region Private data members
        //=====================================================================

        private ICustomTypeDescriptor customTD = null!;

        #endregion

        #region Constructors
        //=====================================================================

        /// <summary>
        /// Construct a description provider with the given parent
        /// </summary>
        /// <param name="parent">The parent type description provider</param>
        /// <overloads>There are two overloads for the constructor</overloads>
        protected ChildPropertyTypeDescriptionProvider(TypeDescriptionProvider parent) : base(parent)
        {
        }

        /// <summary>
        /// Construct a description provider for the specified type
        /// </summary>
        /// <param name="objectType">The type to associate with the type description provider</param>
        protected ChildPropertyTypeDescriptionProvider(Type objectType) :
          base(TypeDescriptor.GetProvider(objectType))
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This gets a custom type descriptor for the given type and object
        /// </summary>
        /// <param name="objectType">The type of object for which to retrieve the type descriptor.</param>
        /// <param name="instance">An instance of the type.  This may be null if not instance was passed to the
        /// type descriptor.</param>
        /// <returns>An <see cref="ICustomTypeDescriptor"/> that can provide metadata for the type</returns>
        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            customTD ??= new ChildPropertyTypeDescriptor(base.GetTypeDescriptor(objectType, instance));

            return customTD;
        }

        /// <summary>
        /// Add a child property type description provider dynamically at runtime
        /// </summary>
        /// <param name="type">The type for which to provide child property descriptors</param>
        [System.Security.SecuritySafeCritical]
        public static void Add(Type type)
        {
            TypeDescriptionProvider parent = TypeDescriptor.GetProvider(type);
            TypeDescriptor.AddProvider(new ChildPropertyTypeDescriptionProvider(parent), type);
        }
        #endregion
    }
}
