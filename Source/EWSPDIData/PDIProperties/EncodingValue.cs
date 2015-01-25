//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : EncodingValue.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/21/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This class holds a set of constants that define the standard encoding values
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

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class holds a set of constants that define the standard encoding values
    /// </summary>
    public static class EncodingValue
    {
        /// <summary>7-bit encoding</summary>
        public const string SevenBit = "7BIT";

        /// <summary>8-bit encoding</summary>
        public const string EightBit = "8BIT";

        /// <summary>Quoted-printable encoding</summary>
        public const string QuotedPrintable = "QUOTED-PRINTABLE";

        /// <summary>Base 64 encoding</summary>
        public const string Base64 = "BASE64";

        /// <summary>B encoding</summary>
        public const string BEncoding = "B";
    }
}
