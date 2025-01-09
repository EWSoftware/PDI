//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : CustomProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains the custom property class.  It is used with the Personal Data Interchange (PDI) classes
// such as vCalendar, iCalendar, and vCard to contain non-standard property names and values.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/14/2004  EFW  Created the code
//===============================================================================================================

using System;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent a custom property (X-???) of a vCard, vCalendar, or iCalendar component.
    /// This represents an extension that is not part of any of the specifications.
    /// </summary>
    /// <remarks>It has no special requirements or handling.  The <see cref="BaseProperty.Value"/> property
    /// contains the value.</remarks>
    public class CustomProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private string customName = null!;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports all specifications including IrMC 1.1</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.AllButIrMC11 |
            SpecificationVersions.IrMC11;

        /// <summary>
        /// This read-only property defines the tag which is the value of the <see cref="CustomName"/> property
        /// </summary>
        public override string Tag => this.CustomName;

        /// <summary>
        /// This read-only property defines the default value location as INLINE
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Inline;

        /// <summary>
        /// This contains the name of the custom property
        /// </summary>
        /// <exception cref="ArgumentNullException">This is thrown if an attempt is made to set this property to
        /// null or an empty string.</exception>
        public string CustomName
        {
            get
            {
                if(String.IsNullOrWhiteSpace(customName))
                    return "X-EWSOFTWARE-UNKNOWN";

                return customName;
            }
            set
            {
                if(String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(value), LR.GetString("ExCustomNoName"));

                customName = value;
            }
        }

        /// <summary>
        /// This is overridden to prevent encoding and decoding of the value
        /// </summary>
        /// <value>Since we don't know what it is, we may not be able to tell if it's encoded or not.  It will
        /// get passed through unchanged.</value>
        public override string? EncodedValue
        {
            get => base.Value;
            set => base.Value = value;
        }
        #endregion

        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public CustomProperty()
        {
        }

        /// <summary>
        /// This constructor takes the name of the custom property
        /// </summary>
        /// <param name="propertyName">The property name</param>
        public CustomProperty(string propertyName)
        {
            this.CustomName = propertyName;
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is overridden to allow cloning of a PDI object
        /// </summary>
        /// <returns>A clone of the object</returns>
        public override object Clone()
        {
            CustomProperty o = new(customName);
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
