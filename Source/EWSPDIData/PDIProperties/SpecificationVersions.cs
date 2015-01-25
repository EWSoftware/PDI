//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : SpecificationVersions.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/14/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This enumerated type defines the various specification versions that can be supported by the objects
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/14/2004  EFW  Created the code
// 08/19/2007  EFW  Added support for the vNote object
//===============================================================================================================

using System;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This enumerated type defines the various specification versions that can be supported by the objects
    /// </summary>
    /// <seealso cref="EWSoftware.PDI.Objects.VCard"/>
    /// <seealso cref="EWSoftware.PDI.Objects.VCalendar"/>
    /// <seealso cref="EWSoftware.PDI.Objects.VNote"/>
    [Flags, Serializable]
    public enum SpecificationVersions
    {
        /// <summary>The object has no version-specific behavior</summary>
        None =        0x0000,
        /// <summary>A vCard 2.1 object</summary>
        vCard21 =     0x0001,
        /// <summary>A vCard 3.0 object</summary>
        vCard30 =     0x0002,
        /// <summary>A vCalendar 1.0 object</summary>
        vCalendar10 = 0x0004,
        /// <summary>An iCalendar 2.0 object</summary>
        iCalendar20 = 0x0008,
        /// <summary>The object is used by all specifications except the IrMC 1.1 specification</summary>
        Any =         0x000F,
        /// <summary>An Infrared Mobile Computing (IrMC) 1.1 object</summary>
        IrMC11 =      0x0010
    }
}
