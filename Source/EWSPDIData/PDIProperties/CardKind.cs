//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : CardKind.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/20/2019
// Note    : Copyright 2019, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This enumerated type defines the various vCard kinds for the KindProperty class
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
    /// This enumerated type defines the various vCard kinds for the <see cref="KindProperty"/> class
    /// </summary>
    [Serializable]
    public enum CardKind
    {
        /// <summary>
        /// No type defined
        /// </summary>
        None,
        /// <summary>
        /// An individual
        /// </summary>
        Individual,
        /// <summary>
        /// A group
        /// </summary>
        Group,
        /// <summary>
        /// An organization
        /// </summary>
        Organization,
        /// <summary>
        /// A location
        /// </summary>
        Location,
        /// <summary>
        /// Indicates some other type of vCard
        /// </summary>
        Other
    }
}
