//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : ChildPropertyTypeDescriptor.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/02/2025
// Note    : Copyright 2007-2025, Eric Woodruff, All rights reserved
//
// This file contains a custom type descriptor that is used to add child properties to the set of visible,
// bindable properties for an object.
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
using System.Collections.Generic;
using System.ComponentModel;

namespace EWSoftware.PDI.Binding
{
    /// <summary>
    /// This custom type descriptor is used to add child properties to the set of visible, bindable properties
    /// for an object.
    /// </summary>
    /// <remarks><para>This type descriptor allows child properties to be bound to controls and edited.  The type
    /// descriptor will scan down up to three levels and goes no further to prevent endless recursion and/or
    /// stack overflows.  Primitive and <c>String</c> property types are ignored as they have no useful
    /// properties for binding purposes.</para>
    /// 
    /// <para>Properties with a <see cref="BrowsableAttribute"/> set to false are ignored along with all of their
    /// child properties.  Properties with a <see cref="HidePropertyAttribute"/> are ignored but their child
    /// properties are added to the list.  This allows you to exclude the parent property if it serves no purpose
    /// other than to contain the child properties.</para>
    /// 
    /// <para>Child properties are prefixed with the parent property name followed by an underscore.  Use this
    /// naming convention when binding to the child properties. (i.e. <c>Address_Street</c>, <c>Address_State</c>
    /// where <c>Address</c> is a class and <c>Street</c> and <c>State</c> are two of its properties).</para></remarks>
    public class ChildPropertyTypeDescriptor : CustomTypeDescriptor
    {
        #region Private data members
        //=====================================================================

        // This is used to track the nesting level when scanning for child properties
        private int nestingLevel;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parent">The parent custom type descriptor</param>
        public ChildPropertyTypeDescriptor(ICustomTypeDescriptor parent) : base(parent)
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is used to recursively add all child properties up to three levels down on reference type
        /// properties to the property collection.
        /// </summary>
        /// <param name="parentProp">The parent property descriptor</param>
        /// <param name="baseName">The base name used as a prefix for child property names</param>
        /// <param name="filter">The attribute filter, if any</param>
        /// <param name="props">The properties to search</param>
        /// <param name="newProps">The list to which new child properties are added</param>
        /// <remarks>To prevent endless recursion and stack overflows, it will only go down three levels.
        /// Properties with a <see cref="BrowsableAttribute"/> set to false are ignored.  Properties with a
        /// <see cref="HidePropertyAttribute"/> are not added to the collection but their children are added.</remarks>
        private void GetChildProperties(PropertyDescriptor? parentProp, string baseName, Attribute[] filter,
          PropertyDescriptorCollection props, List<PropertyDescriptor> newProps)
        {
            PropertyDescriptorCollection childProps, otherChildren;
            PropertyDescriptor parent;
            BrowsableAttribute browsable;
            string rootName, childName;

            // Don't go too far down or we could get stuck in an endless loop on properties that contain
            // instances of themselves.
            if(nestingLevel > 2)
                return;

            try
            {
                nestingLevel++;

                foreach(PropertyDescriptor pd in props)
                {
                    if(!pd.PropertyType.IsPrimitive && pd.PropertyType != typeof(string))
                    {
                        // If it's not browsable, ignore it.  We shouldn't have to do this but the initial
                        // collection from GetProperties() (see below) seems to ignore the filter.
                        browsable = (BrowsableAttribute)pd.Attributes[typeof(BrowsableAttribute)];

                        if(browsable != null && !browsable.Browsable)
                            continue;

                        // The browsable filter does work here though
                        childProps = pd.GetChildProperties(filter);

                        // Each child property will be prefixed with the parent property's name separated by an
                        // underscore.  We can't use a period as certain list controls such as ListBox and
                        // ComboBox use the period as a binding path separator.
                        rootName = baseName + pd.Name + "_";

                        foreach(PropertyDescriptor child in childProps)
                        {
                            childName = rootName + child.Name;

                            // If this is a top-level property, the current property descriptor is used.  If it's
                            // a child, we need to wrap it in a ChildPropertyDescriptor.
                            if(parentProp == null)
                                parent = pd;
                            else
                                parent = new ChildPropertyDescriptor(parentProp, pd, rootName);

                            // If hidden, don't add it to the visible properties but do include its children
                            if(child.Attributes[typeof(HidePropertyAttribute)] == null)
                                newProps.Add(new ChildPropertyDescriptor(parent, child, childName));

                            // Get all children of this child property
                            otherChildren = child.GetChildProperties(filter);

                            if(otherChildren.Count > 0)
                                this.GetChildProperties(parent, rootName, filter, otherChildren, newProps);
                        }
                    }
                }
            }
            finally
            {
                nestingLevel--;
            }
        }

        /// <summary>
        /// This is overridden to return a property descriptors for the object represented by this type
        /// descriptor along with extra property descriptors for its child properties.
        /// </summary>
        /// <param name="attributes">An array of attributes to use as a filter or null for no filter</param>
        /// <returns>Returns a <see cref="PropertyDescriptorCollection"/> that contains property descriptors for
        /// the object and its child properties.</returns>
        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            // This seems to ignore the filter so GetChildProperties() will get rid of all non-browsable
            // properties.
            PropertyDescriptorCollection props = base.GetProperties(attributes);
            List<PropertyDescriptor> newProps = [];

            this.GetChildProperties(null, String.Empty, attributes, props, newProps);

            if(newProps.Count != 0)
            {
                // The collection is read-only so we'll need to create a new one
                PropertyDescriptor[] tempProps = new PropertyDescriptor[props.Count + newProps.Count];

                props.CopyTo(tempProps, 0);
                newProps.CopyTo(tempProps, props.Count);

                props = new PropertyDescriptorCollection(tempProps);

                // Now we'll remove hidden top-level properties
                for(int idx = 0; idx < props.Count; idx++)
                {
                    if(props[idx].Attributes[typeof(HidePropertyAttribute)] != null)
                    {
                        props.RemoveAt(idx);
                        idx--;
                    }
                }
            }

            return props;
        }
        #endregion
    }
}
