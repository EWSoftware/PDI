//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : ChildPropertyDescriptor.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/05/2014
// Note    : Copyright 2007-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a property descriptor that is used to return information for a child property
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
    /// This property descriptor is used to return information for a child property
    /// </summary>
    public class ChildPropertyDescriptor : PropertyDescriptor
    {
        #region Private data members
        //=====================================================================

        // The parent and child property descriptors
        private PropertyDescriptor parentPD, childPD;
        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to indicate whether or not the property is read-only
        /// </summary>
        /// <returns>True if the property is read-only or false if it is not</returns>
        public override bool IsReadOnly
        {
            get { return childPD.IsReadOnly; }
        }

        /// <summary>
        /// This returns the type for the component to which the property is bound
        /// </summary>
        /// <returns>Returns a <see cref="Type"/> that represents the type of component to which the property is
        /// bound.</returns>
        public override Type ComponentType
        {
            get { return parentPD.ComponentType; }
        }

        /// <summary>
        /// This returns the type for the property
        /// </summary>
        /// <returns>Returns a <see cref="Type"/> that represents the type of the property</returns>
        public override Type PropertyType
        {
            get { return childPD.PropertyType; }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parent">The parent property descriptor</param>
        /// <param name="child">The child property descriptor</param>
        /// <param name="name">The name of the property</param>
        public ChildPropertyDescriptor(PropertyDescriptor parent, PropertyDescriptor child, string name) :
          base(name, null)
        {
            parentPD = parent;
            childPD = child;
        }
        #endregion

        #region Method overrides
        //=====================================================================

        /// <summary>
        /// This is used to indicate whether or not the property can be reset
        /// </summary>
        /// <param name="component">The component to test for reset capability</param>
        /// <returns>Returns true if the component can be reset or false if it cannot</returns>
        public override bool CanResetValue(object component)
        {
            return childPD.CanResetValue(parentPD.GetValue(component));
        }

        /// <summary>
        /// This is used to indicate whether or not the property should be persisted
        /// </summary>
        /// <param name="component">The component with the property to examine for persistence</param>
        /// <returns>Returns true if the property should be persisted or false if it should not</returns>
        public override bool ShouldSerializeValue(object component)
        {
            return childPD.ShouldSerializeValue(parentPD.GetValue(component));
        }

        /// <summary>
        /// This returns the current value of the property on the component
        /// </summary>
        /// <param name="component">The component with the property for which to retrieve the value</param>
        /// <returns>The value of the property in the given component</returns>
        public override object GetValue(object component)
        {
            return childPD.GetValue(parentPD.GetValue(component));
        }

        /// <summary>
        /// This is used to set the property on the component to a new value
        /// </summary>
        /// <param name="component">The component with the property to be set</param>
        /// <param name="value">The new value for the property</param>
        public override void SetValue(object component, object value)
        {
            childPD.SetValue(parentPD.GetValue(component), value);
            base.OnValueChanged(component, EventArgs.Empty);
        }

        /// <summary>
        /// This is used to reset the property to its default value
        /// </summary>
        /// <param name="component">The component with the property to reset</param>
        public override void ResetValue(object component)
        {
            childPD.ResetValue(parentPD.GetValue(component));
        }
        #endregion
    }
}
