//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : EncodeDecode.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 10/31/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a utility classes that helps with encoding and decoding values
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/21/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.Text;

namespace EWSoftware.PDI
{
    /// <summary>
    /// This class contains some static utility methods used to encode and decode data
    /// </summary>
    public static class EncodingUtils
    {
        /// <summary>
        /// This method is used to Base 64 encode the specified string using the iso-8859-1 encoding (Western
        /// European (ISO), Windows code page 1252) which works well for 8-bit binary data.
        /// </summary>
        /// <param name="encode">The string to encode.</param>
        /// <param name="foldWidth">If greater than zero, line folds are inserted at the specified interval with
        /// one leading space.</param>
        /// <param name="appendBlankLine">If true, an extra carriage return and line feed are appended to the end
        /// of the encoded data to satisfy the requirements of some specifications such as vCard 2.1.  If false,
        /// they are not appended.</param>
        /// <returns>The Base 64 encoded string</returns>
        public static string ToBase64(this string encode, int foldWidth, bool appendBlankLine)
        {
            // Don't bother if there is nothing to encode
            if(encode == null || encode.Length == 0)
                return encode;

            Encoding enc = Encoding.GetEncoding("iso-8859-1");
            byte[] ba = enc.GetBytes(encode);

            StringBuilder sb = new StringBuilder(Convert.ToBase64String(ba));

            // Insert line folds where necessary if requested.
            if(foldWidth > 0)
            {
                for(int idx = foldWidth - 1; idx < sb.Length; idx += foldWidth + 2)
                    sb.Insert(idx, "\r\n ");
            }

            if(appendBlankLine)
                sb.Append("\r\n");

            return sb.ToString();
        }

        /// <summary>
        /// This method is used to decode the specified Base 64 encoded string using the iso-8859-1 encoding
        /// (Western European (ISO), Windows code page 1252) which works well for 8-bit binary data.
        /// </summary>
        /// <param name="decode">The string to decode</param>
        /// <returns>The decoded data as a string.  This may or may not be a human-readable string.</returns>
        public static string FromBase64(this string decode)
        {
            // Don't bother if there is nothing to decode
            if(decode == null || decode.Length == 0)
                return decode;

            Encoding enc = Encoding.GetEncoding("iso-8859-1");
            return enc.GetString(Convert.FromBase64String(decode));
        }

        /// <summary>
        /// This method is used to replace carriage returns, line feeds, commas, semi-colons, and backslashes
        /// within the string with an appropriate escape sequence (\r \n \, \; \\).
        /// </summary>
        /// <param name="escapeText">The string to escape</param>
        /// <returns>The escaped string</returns>
        public static string Escape(this string escapeText)
        {
            // Don't bother if there is nothing to escape
            if(escapeText == null || escapeText.Length == 0 || escapeText.IndexOfAny(new[] {
              '\r', '\n', ',', ';', '\\'}) == -1)
            {
                return escapeText;
            }

            StringBuilder sb = new StringBuilder(escapeText, escapeText.Length + 100);

            sb.Replace("\\", "\\\\");
            sb.Replace(";", "\\;");
            sb.Replace(",", "\\,");

            // Per the spec, CRLF pairs are written as a single "\n" escape
            sb.Replace("\r\n", "\\n");
            sb.Replace("\r", "\\n");
            sb.Replace("\n", "\\n");

            return sb.ToString();
        }

        /// <summary>
        /// This method is used to replace carriage returns, line feeds, and backslashes within the string with
        /// an appropriate escape sequence (\r \n \\).  Commas and semi-colons are not escaped by this method.
        /// </summary>
        /// <param name="escapeText">The string to escape</param>
        /// <returns>The escaped string</returns>
        /// <remarks>This is mainly for vCard 2.1 properties in which commas and semi-colons should not be
        /// escaped.</remarks>
        public static string RestrictedEscape(this string escapeText)
        {
            // Don't bother if there is nothing to escape
            if(escapeText == null || escapeText.Length == 0 || escapeText.IndexOfAny(new[] { '\r', '\n', '\\'}) == -1)
                return escapeText;

            StringBuilder sb = new StringBuilder(escapeText, escapeText.Length + 100);

            sb.Replace("\\", "\\\\");

            // Per the spec, CRLF pairs are written as a single "\n" escape
            sb.Replace("\r\n", "\\n");
            sb.Replace("\r", "\\n");
            sb.Replace("\n", "\\n");

            return sb.ToString();
        }

        /// <summary>
        /// This method is used to unescape carriage returns, line feeds, commas, semi-colons, and backslashes
        /// within the string by replacing them with their literal characters.
        /// </summary>
        /// <param name="unescapeText">The string to unescape</param>
        /// <returns>The unescaped string</returns>
        /// <remarks>If any escaped single quotes, double quotes, or colons are encountered, they are also
        /// unescaped.  The specifications do not state that they have to be escaped, but some implementations
        /// do, so they are handled here too just in case.  However, they will not be escaped when written back
        /// out.</remarks>
        public static string Unescape(this string unescapeText)
        {
            // Don't bother if there is nothing to escape
            if(unescapeText == null || unescapeText.Length == 0 || unescapeText.IndexOf('\\') == -1)
                return unescapeText;

            StringBuilder sb = new StringBuilder(unescapeText, unescapeText.Length + 100);

            sb.Replace("\\r\\n", "\r\n");
            sb.Replace("\\R\\N", "\r\n");
            sb.Replace("\\r", "\r\n");
            sb.Replace("\\n", "\r\n");
            sb.Replace("\\R", "\r\n");
            sb.Replace("\\N", "\r\n");
            sb.Replace("\\,", ",");
            sb.Replace("\\;", ";");
            sb.Replace("\\\\", "\\");

            // Some implementation escape quotes and colons but they don't have to be according to the spec.
            // We'll unescape them in case they are encountered.
            sb.Replace("\\\"", "\"");
            sb.Replace("\\'", "'");
            sb.Replace("\\:", ":");

            return sb.ToString();
        }

        /// <summary>
        /// This method is used to encode the specified string as Quoted-Printable text
        /// </summary>
        /// <param name="encode">The string to encode.</param>
        /// <param name="foldWidth">If greater than zero, line folds with soft line breaks are inserted at the
        /// specified interval.</param>
        /// <returns>The Quoted-Printable encoded string</returns>
        /// <remarks>Character values 9, 32-60, and 62-126 go through as-is.  All others are encoded as "=XX"
        /// where XX is the 2 digit hex value of the character (i.e. =0D=0A for a carriage return and line feed).</remarks>
        public static string ToQuotedPrintable(this string encode, int foldWidth)
        {
            // Don't bother if there's nothing to encode
            if(encode == null || encode.Length == 0)
                return encode;

            StringBuilder sb = new StringBuilder(encode.Length + 100);

            foldWidth--;    // Account for soft line break character

            for(int idx = 0, len = 0; idx < encode.Length; idx++)
            {
                // Characters 9, 32-60, and 62-126 go through as-is as do any Unicode characters
                if(encode[idx] == '\t' || (encode[idx] > '\x1F' && encode[idx] < '=') || (encode[idx] > '=' &&
                  encode[idx] < '\x7F') || (int)encode[idx] > 255)
                {
                    if(foldWidth > 0 && len + 1 > foldWidth)
                    {
                        sb.Append("=\r\n");     // Soft line break
                        len = 0;
                    }

                    sb.Append(encode[idx]);
                    len++;
                }
                else
                {
                    // All others encode as =XX where XX is the 2 digit hex value of the character
                    if(foldWidth > 0 && len + 3 > foldWidth)
                    {
                        sb.Append("=\r\n");     // Soft line break
                        len = 0;
                    }

                    sb.AppendFormat("={0:X2}", (int)encode[idx]);
                    len += 3;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// This method is used to decode the specified Quoted-Printable encoded string
        /// </summary>
        /// <param name="decode">The string to decode</param>
        /// <returns>The decoded data as a string</returns>
        public static string FromQuotedPrintable(this string decode)
        {
            // Don't bother if there's nothing to decode
            if(decode == null || decode.Length == 0 || decode.IndexOf('=') == -1)
                return decode;

            StringBuilder sb = new StringBuilder(decode.Length);

            string hexDigits = "0123456789ABCDEF";
            int pos1, pos2;

            for(int idx = 0; idx < decode.Length; idx++)
            {
                // Is it an encoded character?
                if(decode[idx] == '=' && idx + 2 <= decode.Length)
                {
                    // Ignore a soft line break
                    if(decode[idx + 1] == '\r' && decode[idx + 2] == '\n')
                    {
                        idx += 2;
                        continue;
                    }

                    pos1 = hexDigits.IndexOf(decode[idx + 1]);
                    pos2 = hexDigits.IndexOf(decode[idx + 2]);

                    if(pos1 != -1 && pos2 != -1)
                    {
                        idx += 2;
                        sb.Append((char)(pos1 * 16 + pos2));
                    }
                }
                else
                    sb.Append(decode[idx]);
            }

            return sb.ToString();
        }
    }
}
