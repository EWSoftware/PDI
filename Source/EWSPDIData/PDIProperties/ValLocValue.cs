//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : ValLocValue.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/19/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This class holds a set of constants that define the standard value types/locations
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
    /// This class holds a set of constants that define the standard value types/locations
    /// </summary>
    public static class ValLocValue
    {
        /// <summary>Value location is inline</summary>
        public const string Inline = "INLINE";

        /// <summary>Same as inline, value is text</summary>
        public const string Text = "TEXT";

        /// <summary>Same as inline, value is an integer</summary>
        public const string Integer = "INTEGER";

        /// <summary>Same as inline, value is a floating point number</summary>
        public const string Float = "FLOAT";

        /// <summary>Same as inline, value is a date</summary>
        public const string Date = "DATE";

        /// <summary>Same as inline, value is a date/time</summary>
        public const string DateTime = "DATE-TIME";

        /// <summary>Same as inline, value is a duration</summary>
        public const string Duration = "DURATION";

        /// <summary>Same as inline, value is a period</summary>
        public const string Period = "PERIOD";

        /// <summary>Same as inline, value is binary</summary>
        public const string Binary = "BINARY";

        /// <summary>Same as inline, value is a URI</summary>
        public const string Uri = "URI";

        /// <summary>Same as inline, value is a calendar address</summary>
        public const string CalAddress = "CAL-ADDRESS";

        /// <summary>Same as inline, value is a UTC offset</summary>
        public const string UtcOffset = "UTC-OFFSET";

        /// <summary>Same as inline, value is a recurrence definition</summary>
        public const string Recur = "RECUR";

        /// <summary>Value location is a URL reference</summary>
        public const string Url = "URL";

        /// <summary>Value location is a content-id</summary>
        public const string ContentId = "CONTENT-ID";

        /// <summary>An abbreviation of Content-ID</summary>
        public const string Cid = "CID";
    }
}
