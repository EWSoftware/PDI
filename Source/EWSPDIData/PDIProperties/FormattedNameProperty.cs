//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : FormattedNameProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/21/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Formatted Name property class used by the Personal Data Interchange (PDI) vCard class
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
    /// This class is used to represent the Formatted Name (FN) property of a vCard
    /// </summary>
    /// <remarks>This property has no special requirements or handling.  It is based on the semantics of the
    /// X.520 Common Name attribute.  The <see cref="Value"/> property contains the formatted name.</remarks>
    public class FormattedNameProperty : BaseProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCard 2.1 and vCard 3.0</value>
        public override SpecificationVersions VersionsSupported
        {
            get { return SpecificationVersions.vCard21 | SpecificationVersions.vCard30; }
        }

        /// <summary>
        /// This read-only property defines the tag (FN)
        /// </summary>
        public override string Tag
        {
            get { return "FN"; }
        }

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation
        {
            get { return ValLocValue.Text; }
        }

        /// <summary>
        /// This is a required property as defined by the 3.0 specification
        /// </summary>
        /// <value>This is overridden to ensure that it always returns a non-null value for both the 2.1 and 3.0
        /// specifications.</value>
        /// <exception cref="ArgumentNullException">This is thrown if an attempt is made to set this property to
        /// null.</exception>
        public override string Value
        {
            get
            {
                string nameValue = base.Value;

                if(String.IsNullOrWhiteSpace(nameValue))
                    return "Unknown";

                return nameValue;
            }
            set
            {
                base.Value = value ?? throw new ArgumentNullException(nameof(value), LR.GetString("ExFNCannotBeNull"));
            }
        }

        /// <summary>
        /// This is a required property as defined by the 3.0 specification
        /// </summary>
        /// <value>This is overridden to ensure that it always returns a non-null value for both the 2.1 and 3.0
        /// specifications.</value>
        /// <exception cref="ArgumentNullException">This is thrown if an attempt is made to set this property to
        /// null.</exception>
        public override string EncodedValue
        {
            get
            {
                string nameValue = base.EncodedValue;

                if(String.IsNullOrWhiteSpace(nameValue))
                    return "Unknown";

                return nameValue;
            }
            set
            {
                base.EncodedValue = value ?? throw new ArgumentNullException(nameof(value), LR.GetString("ExFNCannotBeNull"));
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public FormattedNameProperty()
        {
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
            FormattedNameProperty o = new FormattedNameProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
