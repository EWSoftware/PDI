//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : RelationshipType.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/21/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This enumerated type defines the various relationship types for the RelatedToProperty
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
    /// This enumerated type defines the various relationship types for the <see cref="RelatedToProperty"/>
    /// </summary>
    [Serializable]
    public enum RelationshipType
    {
        /// <summary>The related item is the parent of this item</summary>
        Parent,
        /// <summary>The related item is a child of this item</summary>
        Child,
        /// <summary>The related item is a sibling of this item</summary>
        Sibling,
        /// <summary>Indicates some other type of relationship</summary>
        Other
    }
}
