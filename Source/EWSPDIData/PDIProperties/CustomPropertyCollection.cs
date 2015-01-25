//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : CustomProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/18/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a collection class for CustomProperty objects.  It is used with the Personal Data
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

using EWSoftware.PDI.Binding;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// A type-safe collection of <see cref="CustomProperty"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator</remarks>
    public class CustomPropertyCollection : ExtendedBindingList<CustomProperty>
    {
        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public CustomPropertyCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="CustomProperty"/> objects
        /// </summary>
        /// <param name="customProps">The <see cref="IList{T}"/> of custom properties to add</param>
        public CustomPropertyCollection(IList<CustomProperty> customProps) : base(customProps)
        {
        }
        #endregion

        #region Collection indexer
        //=====================================================================

        /// <summary>
        /// Collection indexer
        /// </summary>
        /// <param name="propertyName">The name of the item to get or set.  When retrieving an item, null is
        /// returned if it does not exist in the collection.</param>
        /// <exception cref="ArgumentException">This is thrown if an attempt is made to set an item using a name
        /// that does not exist in the collection.</exception>
        /// <remarks>The property name is case-insensitive</remarks>
        public CustomProperty this[string propertyName]
        {
            get
            {
                for(int idx = 0; idx < base.Count; idx++)
                    if(String.Compare(base[idx].Tag, propertyName, StringComparison.OrdinalIgnoreCase) == 0)
                        return base[idx];

                return null;
            }
            set
            {
                for(int idx = 0; idx < base.Count; idx++)
                    if(String.Compare(base[idx].Tag, propertyName, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        base[idx] = value;
                        return;
                    }

                throw new ArgumentException(LR.GetString("ExCPropIDNotFound"));
            }
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Add a <see cref="CustomProperty"/> to the collection with the specified tag name and value
        /// </summary>
        /// <param name="tag">The tag name to use for the custom property</param>
        /// <param name="propertyValue">The value to use for the custom property</param>
        /// <returns>Returns the newly created custom property</returns>
        public CustomProperty Add(string tag, string propertyValue)
        {
            CustomProperty cprop = new CustomProperty(tag);
            cprop.Value = propertyValue;
            base.Add(cprop);

            return cprop;
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
