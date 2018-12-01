//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : PDIParser.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class used to parse Personal Data Interchange (PDI) data streams containing vCalendar,
// iCalendar, and vCard objects.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/21/2004  EFW  Created the code
// 03/17/2007  EFW  Updated for use with .NET 2.0
//===============================================================================================================

// Ignore Spelling: vcp sr http https

using System;
using System.IO;
using System.Net;
using System.Text;

using EWSoftware.PDI.Properties;

namespace EWSoftware.PDI.Parser
{
    /// <summary>
    /// This abstract base class implements the common functionality for all PDI data stream parsers
    /// </summary>
    /// <remarks>The parser is very forgiving and should be able to parse most PDI data streams even when they
    /// contain syntax errors.</remarks>
    public abstract class PDIParser
    {
        #region Parser state enumeration
        //=====================================================================

        /// <summary>
        /// This enumerated type defines the various parser states
        /// </summary>
        [Serializable]
        protected enum ParserState
        {
            /// <summary>Parsing a property name</summary>
            PropertyName,
            /// <summary>Looking for a parameter name</summary>
            BeforeParameterName,
            /// <summary>Parsing a parameter name</summary>
            ParameterName,
            /// <summary>Looking for a parameter value</summary>
            BeforeParameterValue,
            /// <summary>Parsing a parameter value</summary>
            ParameterValue,
            /// <summary>Parsing a property value</summary>
            PropertyValue
        }
        #endregion

        #region Private data members
        //=====================================================================

        private int  lineNbr;           // The line number being parsed

        private bool isStartOfLine,     // Start of line indicator
                     isUnfolding,       // Unfolding indicator
                     isEscapeSeq,       // Next character is escaped if true
                     isQuotedString,    // Within quoted string parameter value
                     isQPValue;         // Value uses quote-printable encoding

        private ParserState state;      // Current parser state

        private StringBuilder sb;       // The parsing buffer

        private string propertyName;    // The property name

        // The string collection of parameters.  It's re-used to save resources
        private StringCollection propParams;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This property is used to set or get the default text encoding method used when parsing a PDI data
        /// stream.
        /// </summary>
        /// <value>The default text encoding method is UTF-8.  You can set this property before parsing a PDI
        /// data stream to alter how text within the stream is interpreted.  This parameter has no effect if
        /// using a parsing method that is passed a stream object.  In those cases, it is up to you to open the
        /// stream with the appropriate text encoding method.</value>
        public static Encoding DefaultEncoding { get; set; } = new UTF8Encoding(false, false);

        /// <summary>
        /// This returns the current line number being processed from the data stream
        /// </summary>
        public int LineNumber => lineNbr;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Default constructor.  It is protected as this class must be derived from in order to implement the
        /// abstract methods.
        /// </summary>
        /// <seealso cref="PropertyParser"/>
        protected PDIParser()
        {
            sb = new StringBuilder(100);
            propParams = new StringCollection();
        }
        #endregion

        #region Private class members
        //=====================================================================

        /// <summary>
        /// This is called when parsing input in the Property Name state
        /// </summary>
        /// <param name="ch">The incoming character</param>
        private void ProcessPropertyNameState(char ch)
        {
            switch(ch)
            {
                case ':':
                    // Store the property name
                    propertyName = sb.ToString();
                    sb.Length = 0;
                    this.ProcessParameters();

                    // Now parse out the property value
                    state = ParserState.PropertyValue;
                    break;

                case ';':
                    // Store the property name
                    propertyName = sb.ToString();
                    sb.Length = 0;

                    // Now look for parameter names or values
                    state = ParserState.BeforeParameterName;
                    break;

                case '\n':
                    // Blank line or unexpected end.  Discard this line.
                    this.ResetState(false);
                    break;

                default:
                    // Keep on adding to the property name
                    if(!Char.IsWhiteSpace(ch))
                        sb.Append(ch);
                    break;
            }
        }

        /// <summary>
        /// This is called when parsing input in the Parameter Name state
        /// </summary>
        /// <param name="ch">The incoming character</param>
        private void ProcessParameterNameState(char ch)
        {
            switch(ch)
            {
                case '=':
                    // End the parameter name with the '=' and add a null separator after the parameter name
                    sb.Append('=');
                    sb.Append('\x0');

                    // Now look for a parameter value
                    state = ParserState.BeforeParameterValue;
                    break;

                case ';':
                    // Add a null separator after the parameter name or value
                    sb.Append('\x0');

                    // Now look for another parameter name or value
                    state = ParserState.BeforeParameterName;
                    break;

                case ':':
                    // No more parameters so process them
                    this.ProcessParameters();

                    // Now parse out the property value
                    state = ParserState.PropertyValue;
                    break;

                default:
                    // Keep on adding to the parameter name or value
                    if(!Char.IsWhiteSpace(ch))
                        sb.Append(ch);
                    break;
            }
        }

        /// <summary>
        /// This looks at the incoming character and, based on the current parser state, takes the appropriate
        /// action.
        /// </summary>
        /// <param name="ch">The incoming character</param>
        private void ProcessCharacter(char ch)
        {
            switch(state)
            {
                case ParserState.PropertyName:
                    this.ProcessPropertyNameState(ch);
                    break;

                case ParserState.BeforeParameterName:
                    if(ch == ':')
                    {
                        // Technically, if we see this now it's an error but we'll ignore it and carry on like
                        // there were no parameters.  If it's really bad, it'll choke on the property name
                        // evaluation.
                        this.ProcessPropertyNameState(ch);
                    }
                    else
                        if(!Char.IsWhiteSpace(ch))
                        {
                            // Start accumulating the property name characters
                            state = ParserState.ParameterName;
                            sb.Append(ch);
                        }
                    break;

                case ParserState.ParameterName:
                    this.ProcessParameterNameState(ch);
                    break;

                case ParserState.BeforeParameterValue:
                    if(ch == ';' || ch == ':')
                    {
                        // Technically, if we see these now it's an error but we'll ignore it and carry on like
                        // there was no value.  If it's really bad, it'll choke on the property name evaluation.
                        this.ProcessParameterNameState(ch);
                    }
                    else
                        if(!Char.IsWhiteSpace(ch))
                        {
                            // Start accumulating parameter value characters
                            state = ParserState.ParameterValue;

                            // Is parameter value quoted?  If so, note it and ignore the quote.
                            if(ch == '\"')
                                isQuotedString = true;
                            else
                                sb.Append(ch);
                        }
                    break;

                case ParserState.ParameterValue:
                    if(isEscapeSeq)
                    {
                        // Escaped char.  Just add it to the parameter value.
                        isEscapeSeq = false;
                        sb.Append(ch);
                    }
                    else
                        if(!isQuotedString && (ch == ';' || ch == ':'))
                        {
                            // Ignore trailing whitespace after a parameter value
                            while(Char.IsWhiteSpace(sb[sb.Length - 1]))
                                sb.Remove(sb.Length - 1, 1);

                            this.ProcessParameterNameState(ch);
                        }
                        else
                        {
                            // Escapes and quotes are removed.  However, escapes are ignored if inside a quoted
                            // string.
                            if(ch == '\\' && !isQuotedString)
                                isEscapeSeq = true;  // Escape next char
                            else
                                if(ch == '\"')      // Quoted string
                                    isQuotedString = !isQuotedString;
                                else
                                    sb.Append(ch);
                        }
                    break;

                case ParserState.PropertyValue:
                    // End of value?
                    if(ch == '\n')
                    {
                        // Let the derived class process the property.  The property classes take care of any
                        // base 64 or quoted-printable decoding of the value.
                        this.PropertyParser(propertyName, propParams, sb.ToString());

                        // Reset the state ready for the next property
                        this.ResetState(false);
                    }
                    else
                        sb.Append(ch);
                    break;
            }
        }

        /// <summary>
        /// This method is called when the parser has determined that it has found a property name and all of its
        /// related parameters (if any).
        /// </summary>
        private void ProcessParameters()
        {
            string[] parameters = null;

            // Null characters terminate each parameter name and value in the string builder so we can split on
            // them.
            propParams.Clear();

            if(sb.Length > 0)
            {
                parameters = sb.ToString().Split('\x0');

                // Look for a QUOTED-PRINTABLE parameter value.  If present, the upcoming value is encoded that
                // way and may contain soft line breaks that require us to unfold the lines.
                for(int nIdx = 0; nIdx < parameters.Length; nIdx++)
                {
                    if(String.Compare(parameters[nIdx], EncodingValue.QuotedPrintable, StringComparison.OrdinalIgnoreCase) == 0)
                        isQPValue = true;

                    propParams.Add(parameters[nIdx]);
                }
            }

            // Now parse out the property value
            sb.Length = 0;
            state = ParserState.PropertyValue;
        }
        #endregion

        #region Public methods
        //=====================================================================

        /// <summary>
        /// This resets the parser to a default state
        /// </summary>
        /// <param name="fullReset">If true, a full reset is done (i.e. this is the start of a brand new session.
        /// If false only the line state is reset (it's done parsing a property name or value).</param>
        protected virtual void ResetState(bool fullReset)
        {
            if(fullReset)
            {
                isStartOfLine = true;
                isUnfolding = false;
                lineNbr = 0;
            }

            state = ParserState.PropertyName;
            sb.Length = 0;
            isEscapeSeq = isQuotedString = isQPValue = false;
        }

        /// <summary>
        /// This is called to flush any pending property or value data when the end of a data stream has been
        /// reached.
        /// </summary>
        protected void Flush()
        {
            this.ProcessCharacter('\n');
            this.ResetState(true);
        }

        /// <summary>
        /// This method is used to parse one or more PDI objects from a string
        /// </summary>
        /// <param name="pdiText">The PDI information to parse</param>
        /// <remarks>The derived class will build a list of objects based on the data stream.  See the derived
        /// classes for more information.  The method will parse all data from the start of the string to the end
        /// of the string.</remarks>
        /// <seealso cref="VCardParser"/>
        /// <seealso cref="VCalendarParser"/>
        /// <exception cref="PDIParserException">This is thrown if there is an error parsing the data stream.
        /// Inner exceptions may contain additional information about the cause of the error.</exception>
        /// <example>
        /// <code language="cs">
        /// VCardParser vcp = new VCardParser();
        /// vcp.ParseString(vCardsText);
        ///
        /// VCardCollection vCards = vcp.VCards;
        /// </code>
        /// <code language="vbnet">
        /// Dim vcp As New VCardParser
        /// vcp.ParseString(vCardsText)
        ///
        /// Dim vCards As VCardCollection = vcp.VCards
        /// </code>
        /// </example>
        public void ParseString(string pdiText)
        {
            // Ignore it if there is nothing to parse
            if(pdiText == null || pdiText.Length == 0)
                return;

            // We could parse it here for speed, but the code would be identical to the TextReader version below
            // so we'll save some code maintenance and wrap it in a StringReader.
            using(var sr = new StringReader(pdiText))
            {
                this.ParseReader(sr);
            }
        }

        /// <summary>
        /// This method is used to parse one or more PDI objects from a <see cref="TextReader"/> derived object
        /// such as a <see cref="StreamReader"/> or a <see cref="StringReader"/>.
        /// </summary>
        /// <param name="tr">The text reader object containing the PDI data stream.  It is up to you to open the
        /// stream with the appropriate text encoding method if necessary.</param>
        /// <remarks>The derived class will build a list of objects based on the data stream.  See the derived
        /// classes for more information.  The stream will be read from its current position to the end of the
        /// stream.</remarks>
        /// <seealso cref="VCardParser"/>
        /// <seealso cref="VCalendarParser"/>
        /// <exception cref="ArgumentNullException">This is thrown if the specified text reader object is null.</exception>
        /// <exception cref="PDIParserException">This is thrown if there is an error parsing the data stream.
        /// Inner exceptions may contain additional information about the cause of the error.</exception>
        /// <example>
        /// <code language="cs">
        /// VCardParser vcp = new VCardParser();
        /// StreamReader sr = new StreamReader(@"C:\AddressBook.vcf");
        /// vcp.ParseReader(sr);
        ///
        /// VCardCollection vCards = vcp.VCards;
        /// </code>
        /// <code language="vbnet">
        /// Dim vcp As New VCardParser
        /// Dim sr As New StreamReader("C:\AddressBook.vcf")
        /// vcp.ParseReader(sr);
        ///
        /// Dim vCards As VCardCollection = vcp.VCards
        /// </code>
        /// </example>
        public void ParseReader(TextReader tr)
        {
            char ch;
            int  nch;
            bool seenLF = false;

            if(tr == null)
                throw new ArgumentNullException(nameof(tr), LR.GetString("ExParseNullReader"));

            try
            {
                this.ResetState(true);

                while((nch = tr.Read()) != -1)
                {
                    ch = (char)nch;

                    // Ignore carriage returns and empty lines
                    if(ch == '\r' || (isStartOfLine && ch == '\n'))
                    {
                        if(ch == '\n')
                            lineNbr++;

                        // Some files have a line fold but no data on the last line (a space immediately followed
                        // by CR/LF).  In that case, we need to process the line feed to store the value.
                        if(ch == '\r' || !isUnfolding || (isQPValue && isStartOfLine && ch == '\n'))
                        {

                            // Some files contained quoted printable data with a soft line break followed by a
                            // blank line.  In those cases, treat it like the end of the line.
                            if(isQPValue && isStartOfLine && ch == '\n')
                            {
                                if(!seenLF)
                                {
                                    seenLF = true;
                                    continue;
                                }
                            }
                            else
                                continue;
                        }

                    }

                    seenLF = false;

                    if(isStartOfLine)
                    {
                        // Start unfolding lines?
                        if(!isUnfolding && Char.IsWhiteSpace(ch))
                        {
                            isUnfolding = true;
                            continue;
                        }

                        isStartOfLine = false;

                        // If unfolding lines, carry on.  This line will be appended to the last one.  If not,
                        // process a line feed to terminate the last line and flush any pending information.
                        if(!isUnfolding)
                        {
                            // We should be in the property value except if just starting
                            if(lineNbr > 0 && state != ParserState.PropertyValue)
                                throw new PDIParserException(lineNbr,
                                    LR.GetString("ExParseSepNotFound"));

                            this.ProcessCharacter('\n');
                        }

                        // Process the first character of the new line
                        this.ProcessCharacter(ch);
                    }
                    else
                    {
                        // Check for Quoted Printable soft line breaks
                        if(isQPValue && ch == '=' && (tr.Peek() == '\r' || tr.Peek() == '\n'))
                        {
                            isStartOfLine = true;    // Unfold soft line break
                            isUnfolding = true;
                        }
                        else
                            if(ch == '\n')
                            {
                                isStartOfLine = true;    // End of line
                                isUnfolding = false;
                                lineNbr++;
                            }
                            else    // Continuing current line
                                this.ProcessCharacter(ch);
                    }
                }

                this.Flush();
            }
            catch(PDIParserException )
            {
                // Just pass these on unchanged
                throw;
            }
            catch(Exception e)
            {
                // Wrap all other exceptions in a PDIParser exception
                throw new PDIParserException(lineNbr, LR.GetString("ExParseUnexpectedError"), e);
            }
        }

        /// <summary>
        /// This method is used to parse one or more PDI objects from the specified file or URL
        /// </summary>
        /// <param name="filename">The name of the file from which to read the PDI objects.  This can be a
        /// standard filename or a URL that starts with "file://", "http://", or "https://".</param>
        /// <remarks>The derived class will build a list of objects based on the data stream.  See the derived
        /// classes for more information.  The named file will be opened, its entire content parsed, and then
        /// it will be closed.</remarks>
        /// <seealso cref="VCardParser"/>
        /// <seealso cref="VCalendarParser"/>
        /// <exception cref="ArgumentNullException">This is thrown if the specified filename string is null or
        /// empty.</exception>
        /// <exception cref="PDIParserException">This is thrown if there is an error parsing the data stream.
        /// Inner exceptions may contain additional information about the cause of the error.</exception>
        /// <example>
        /// <code language="cs">
        /// VCardParser vcp = new VCardParser();
        /// vcp.ParseFile(@"C:\AddressBook.vcf");
        /// //Or vcp.ParseFile("http://www.mydomain.com/vCards/AddressBook.vcf");
        ///
        /// VCardCollection vCards = vcp.VCards;
        /// </code>
        /// <code language="vbnet">
        /// Dim vcp As New VCardParser
        /// vcp.ParseFile("C:\AddressBook.vcf");
        /// 'Or vcp.ParseFile("http://www.mydomain.com/vCards/AddressBook.vcf");
        ///
        /// Dim vCards As VCardCollection = vcp.VCards
        /// </code>
        /// </example>
        public void ParseFile(string filename)
        {
            StreamReader sr = null;

            if(filename == null || filename.Length == 0)
                throw new ArgumentNullException(nameof(filename), LR.GetString("ExParseNoFilename"));

            // Exceptions will bubble up to the caller
            try
            {
                if(filename.StartsWith("http:", StringComparison.OrdinalIgnoreCase) ||
                  filename.StartsWith("https:", StringComparison.OrdinalIgnoreCase))
                {
                    HttpWebRequest wrq = (HttpWebRequest)WebRequest.Create(new Uri(filename));
                    WebResponse wrsp = wrq.GetResponse();
                    sr = new StreamReader(wrsp.GetResponseStream(), DefaultEncoding);
                }
                else
                    if(filename.StartsWith("file:", StringComparison.OrdinalIgnoreCase))
                    {
                        FileWebRequest frq = (FileWebRequest)WebRequest.Create(new Uri(filename));
                        WebResponse frsp = frq.GetResponse();
                        sr = new StreamReader(frsp.GetResponseStream(), DefaultEncoding);
                    }
                    else
                        sr = new StreamReader(filename, DefaultEncoding);

                this.ParseReader(sr);
            }
            catch(PDIParserException )
            {
                // Just pass these on unchanged
                throw;
            }
            catch(Exception e)
            {
                // Wrap all other exceptions in a PDIParser exception
                throw new PDIParserException(lineNbr, LR.GetString("ExParseUnexpectedError"), e);
            }
            finally
            {
                if(sr != null)
                    sr.Close();
            }
        }
        #endregion

        #region Abstract methods that need to be implemented
        //=====================================================================

        /// <summary>
        /// Override this in derived classes to handle properties as they are parsed from the data stream
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="parameters">A string collection containing the parameters and their values.  If empty,
        /// there are no parameters.</param>
        /// <param name="propertyValue">The value of the property.</param>
        /// <remarks><para>Depending on the object for which data is being parsed, there may be a mixture of
        /// name/value pairs or values alone in the parameters string collection.  It is up to the derived class
        /// to process the parameter list based on the specification to which it conforms.  For entries that are
        /// parameter names, the entry will end with an equals sign (=) and the entry immediately following it in
        /// the collection is its associated parameter value.  The property name, parameter names, and their
        /// values may be in upper, lower, or mixed case.</para>
        /// 
        /// <para>The value may be an encoded string.  The properties are responsible for any decoding that may
        /// need to occur (i.e. base 64 encoded image data).</para></remarks>
        protected abstract void PropertyParser(string propertyName, StringCollection parameters,
            string propertyValue);

        #endregion
    }
}
