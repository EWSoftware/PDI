//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : RelatedType.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/20/2019
// Note    : Copyright 2019, Eric Woodruff, All rights reserved
//
// This enumerated type defines the various related types for the RelatedProperty
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 12/20/2019  EFW  Created the code
//===============================================================================================================

using System;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This enumerated type defines the various related types for the <see cref="RelatedProperty"/>
    /// </summary>
    [Flags, Serializable]
    public enum RelatedTypes
    {
        /// <summary>Indicates no type specified</summary>
        None            = 0x00000000,
        /// <summary>The related person is an acquaintance</summary>
        Acquaintance    = 0x00000001,
        /// <summary>The related person is an agent that can act on the person's behalf</summary>
        Agent           = 0x00000002,
        /// <summary>The related person is a child</summary>
        Child           = 0x00000004,
        /// <summary>The related person is a co-resident</summary>
        CoResident      = 0x00000008,
        /// <summary>The related person is a co-worker</summary>
        CoWorker        = 0x00000010,
        /// <summary>The related person is a colleague</summary>
        Colleague       = 0x00000020,
        /// <summary>The related person is a contact</summary>
        Contact         = 0x00000040,
        /// <summary>The related person is a crush</summary>
        Crush           = 0x00000080,
        /// <summary>The related person is a date</summary>
        Date            = 0x00000100,
        /// <summary>The related person is a emergency contact</summary>
        Emergency       = 0x00000200,
        /// <summary>The related person is a friend</summary>
        Friend          = 0x00000400,
        /// <summary>The related person is kin</summary>
        Kin             = 0x00000800,
        /// <summary>The related person is the same person</summary>
        Me              = 0x00001000,
        /// <summary>The related person is someone that was met</summary>
        Met             = 0x00002000,
        /// <summary>The related person is a muse</summary>
        Muse            = 0x00004000,
        /// <summary>The related person is a neighbor</summary>
        Neighbor        = 0x00008000,
        /// <summary>The related person is a parent</summary>
        Parent          = 0x00010000,
        /// <summary>The related person is a sibling</summary>
        Sibling         = 0x00020000,
        /// <summary>The related person is a spouse</summary>
        Spouse          = 0x00040000,
        /// <summary>The related person is a sweetheart</summary>
        Sweetheart      = 0x00080000
    }
}
