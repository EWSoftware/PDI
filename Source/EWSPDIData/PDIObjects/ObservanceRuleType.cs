//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : ObservanceRuleType.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/06/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the enumerated type that defines the type of time zone observance rules used by the
// ObservanceRule class.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 09/20/2004  EFW  Created the code
//===============================================================================================================

using System;

namespace EWSoftware.PDI.Objects
{
    /// <summary>
    /// This enumerated type defines the type of time zone observance rules for the <see cref="ObservanceRule"/>
    /// class.
    /// </summary>
    [Serializable]
    public enum ObservanceRuleType
    {
        /// <summary>A standard time rule</summary>
        Standard,
        /// <summary>A daylight saving time rule</summary>
        Daylight
    }
}
