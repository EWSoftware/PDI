//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : UniqueIdProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/04/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains the Unique ID property class used by the Personal Data Interchange (PDI) classes such as
// vCalendar, iCalendar, and vCard.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/14/2004  EFW  Created the code
// 08/19/2007  EFW  Added support for vNote objects
//===============================================================================================================

using System;
using System.Text;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Unique Identifier property of an object (UID or X-IRMC-LUID)
    /// </summary>
    /// <remarks>This property has no special requirements or handling.  If used, it should be a string of some
    /// sort that uniquely identifies the object.  The <see cref="BaseProperty.Value"/> property contains the
    /// unique ID string.  A unique ID will be created automatically if the unique ID is requested and one does
    /// not already exist.</remarks>
    public class UniqueIdProperty : BaseProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports all specifications including IrMC 1.1</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.AllButIrMC11 |
            SpecificationVersions.IrMC11;

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Text;

        /// <summary>
        /// This read-only property defines the tag.  It returns UID for all specifications except IrMC 1.1 which
        /// uses X-IRMC-LUID.
        /// </summary>
        public override string Tag
        {
            get
            {
                if(this.Version != SpecificationVersions.IrMC11)
                    return "UID";

                return "X-IRMC-LUID";
            }
        }

        /// <summary>
        /// This is overridden to create a unique ID if one does not already exist
        /// </summary>
        public override string? Value
        {
            get
            {
                if(String.IsNullOrWhiteSpace(base.Value))
                    base.Value = Guid.NewGuid().ToString().ToUpperInvariant();

                return base.Value;
            }
            set => base.Value = value;
        }

        /// <summary>
        /// This is overridden to create a unique ID if one does not already exist
        /// </summary>
        public override string? EncodedValue
        {
            get => this.Value;
            set => base.Value = value;
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        public UniqueIdProperty()
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is overridden to allow cloning of a PDI object
        /// </summary>
        /// <returns>A clone of the object</returns>
        /// <remarks>The ID will be cloned which may result in a duplicate ID if the object is stored in the same
        /// calendar.  You must manually assign a new unique ID after cloning if necessary.</remarks>
        public override object Clone()
        {
            UniqueIdProperty o = new();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// The 3.0 specification does not allow parameters.  This will also be enforced for the 2.1
        /// specification.  Any parameters are ignored.
        /// </summary>
        /// <param name="sb">The <see cref="StringBuilder"/> to which the parameters are appended</param>
        public override void SerializeParameters(StringBuilder sb)
        {
        }

        /// <summary>
        /// The 3.0 specification does not allow parameters.  This will also be enforced for the 2.1
        /// specification.  Any parameters are ignored.
        /// </summary>
        /// <param name="parameters">The parameters for the property</param>
        public override void DeserializeParameters(StringCollection parameters)
        {
        }

        /// <summary>
        /// This can be called to automatically create and assign a unique ID to the property
        /// </summary>
        /// <param name="forceNew">If true, a new unique ID is assigned regardless of whether one already exists.
        /// If false and the object already has a unique ID, it keeps the old one.</param>
        /// <returns>It returns the new or existing unique ID</returns>
        public string AssignNewId(bool forceNew)
        {
            if(!String.IsNullOrWhiteSpace(base.Value) && forceNew)
                base.Value = null;

            return this.Value!;
        }
        #endregion
    }
}
