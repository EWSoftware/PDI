//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : AlarmAction.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/18/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This enumerated type defines the various alarm action types for the ActionProperty
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
    /// This enumerated type defines the various alarm action types for the <see cref="ActionProperty"/> class
    /// </summary>
    [Serializable]
    public enum AlarmAction
    {
        /// <summary>An audio alarm</summary>
        Audio,
        /// <summary>A display alarm</summary>
        Display,
        /// <summary>An e-mail alarm</summary>
        EMail,
        /// <summary>A procedure alarm</summary>
        Procedure,
        /// <summary>Indicates some other type of alarm</summary>
        Other
    }
}
