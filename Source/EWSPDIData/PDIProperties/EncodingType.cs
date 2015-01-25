//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : EncodingType.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/21/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This enumerated type defines the various value encoding schemes recognized by the properties
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
    /// This enumerated type defines the various value encoding schemes recognized by the properties
    /// </summary>
    [Serializable]
    public enum EncodingType
    {
        /// <summary>7-bit encoding (the default)</summary>
        SevenBit,
        /// <summary>8-bit encoding</summary>
        EightBit,
        /// <summary>Quoted-printable encoding</summary>
        QuotedPrintable,
        /// <summary>Base 64 encoding</summary>
        Base64,
        /// <summary>Same as Base 64 encoding</summary>
        BEncoding,
        /// <summary>Unknown or custom type</summary>
        Custom
    }
}
