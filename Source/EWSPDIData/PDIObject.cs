//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : PDIObject.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the base class for all of the Personal Data Interchange (PDI) classes
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/29/2004  EFW  Created the code
// 08/19/2007  EFW  Added support for vNote object
//===============================================================================================================

using System;

using EWSoftware.PDI.Properties;

namespace EWSoftware.PDI
{
    /// <summary>
    /// This is the common abstract base class for all PDI objects
    /// </summary>
    public abstract class PDIObject : ICloneable
    {
        #region Private data members
        //=====================================================================

        private SpecificationVersions version;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// All objects derived from this class must implement this to indicate the specification versions with
        /// which they can be used.
        /// </summary>
        public abstract SpecificationVersions VersionsSupported { get; }

        /// <summary>
        /// This is used to establish the specification version to which the PDI object will conform when
        /// converted to its string form.
        /// </summary>
        /// <value>Some PDI objects are handled differently under the different versions of the specifications.
        /// This property allows them to react accordingly.  If the object is owned by another object, the owner
        /// should set the version as needed before doing anything that is version dependent such as converting
        /// it to a string.  This should also be overridden where necessary to propagate the version to all owned
        /// objects.  Derived classes with only one version or with version-dependent behavior should always set
        /// the default version when constructed.</value>
        /// <exception cref="ArgumentException">This exception is thrown if an attempt is made to set the version
        /// to <c>None</c>, a combination of version values, or if the specified version is not supported by the
        /// object.</exception>
        public virtual SpecificationVersions Version
        {
            get => version;
            set
            {
                if(value == SpecificationVersions.None)
                    throw new ArgumentException(LR.GetString("ExPDIOVersionSetToNone"));

                if(value != SpecificationVersions.vCard21 && value != SpecificationVersions.vCard30 &&
                   value != SpecificationVersions.vCalendar10 && value != SpecificationVersions.iCalendar20 &&
                   value != SpecificationVersions.IrMC11)
                    throw new ArgumentException(LR.GetString("ExPDIOVersionCombo"));

                if((this.VersionsSupported & value) == 0)
                    throw new ArgumentException(LR.GetString("ExPDIOVersionNotSupported"));

                version = value;
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Default constructor.  The version is set to <c>None</c>.
        /// </summary>
        /// <remarks>Derived top-level classes and those classes that conform to only one specification should
        /// always set a default version other than <c>None</c>.</remarks>
        protected PDIObject()
        {
            version = SpecificationVersions.None;
        }
        #endregion

        #region ICloneable implementation
        //=====================================================================

        /// <summary>
        /// This must be overridden to allow cloning of a PDI object
        /// </summary>
        /// <returns>A clone of the object</returns>
        public abstract object Clone();

        /// <summary>
        /// This must be overridden to allow copying values from the specified PDI object into the instance
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected abstract void Clone(PDIObject p);

        #endregion
    }
}
