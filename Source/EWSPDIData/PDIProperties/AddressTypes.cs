//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : AddressTypes.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2019
// Note    : Copyright 2004-2019, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This enumerated type defines the various address types for the AddressProperty and LabelProperty classes
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
    /// This enumerated type defines the various address types for the <see cref="AddressProperty"/> and
    /// <see cref="LabelProperty"/> classes.
    /// </summary>
    [Flags, Serializable]
    public enum AddressTypes
    {
        /// <summary>Indicates no type specified</summary>
        None =          0x0000,
        /// <summary>Indicates a domestic (DOM) address (vCard 2.1 and 3.0 only)</summary>
        Domestic =      0x0001,
        /// <summary>Indicates an international (INTL) address (vCard 2.1 and 3.0 only)</summary>
        International = 0x0002,
        /// <summary>Indicates a postal delivery (POSTAL) address (vCard 2.1 and 3.0 only)</summary>
        Postal =        0x0004,
        /// <summary>Indicates a parcel delivery (PARCEL) address (vCard 2.1 and 3.0 only)</summary>
        Parcel =        0x0008,
        /// <summary>Indicates a home delivery (HOME) address</summary>
        Home =          0x0010,
        /// <summary>Indicates a work delivery (WORK) address</summary>
        Work =          0x0020,
        /// <summary>Indicates a preferred delivery (PREF) address</summary>
        Preferred =     0x0040
    }
}
