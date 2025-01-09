//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : VNoteParser.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2007-2025, Eric Woodruff, All rights reserved
//
// This file contains a class used to parse vNote Personal Data Interchange (PDI) data streams.  It supports
// the IrMC 1.1 specification file format.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 08/19/2007  EFW  Created the code
//===============================================================================================================

// Ignore Spelling: sr

using System;
using System.IO;

using EWSoftware.PDI.Objects;
using EWSoftware.PDI.Properties;

namespace EWSoftware.PDI.Parser
{
    /// <summary>
    /// This class implements the parser that handles vNote PDI objects
    /// </summary>
    public class VNoteParser : PDIParser
    {
        #region Private data members
        //=====================================================================

        private VNote? currentNote;                 // The current vNote being processed
        private readonly VNoteCollection vNotes;    // The collection of vNotes

        //=====================================================================

        // This private array is used to translate property names into property types
        private static readonly NameToValue<PropertyType>[] ntv =
        [
            new("BEGIN", PropertyType.Begin),
            new("END", PropertyType.End),
            new("VERSION", PropertyType.Version),
            new("X-IRMC-LUID", PropertyType.UniqueId),
            new("SUMMARY", PropertyType.Summary),
            new("BODY", PropertyType.Body),
            new("DCREATED", PropertyType.DateCreated),
            new("LAST-MODIFIED", PropertyType.LastModified),
            new("CLASS", PropertyType.Class),
            new("CATEGORIES", PropertyType.Categories),

            // The last entry should always be CustomProperty to catch all unrecognized properties.  The actual
            // property name is not relevant.
            new("X-", PropertyType.Custom)
        ];
        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to get the collection of vNotes parsed from the PDI data stream
        /// </summary>
        /// <remarks>The vNotes from prior calls to the parsing methods are not cleared automatically.  Call
        /// <c>VNotes.Clear()</c> before calling a parsing method if you do not want to retain the vNotes from
        /// prior runs.</remarks>
        public VNoteCollection VNotes => vNotes;

        #endregion

        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public VNoteParser()
        {
            vNotes = [];
        }

        /// <summary>
        /// This version of the constructor is used when parsing vNote data that is to be stored in an existing
        /// vNote instance.
        /// </summary>
        /// <remarks>The properties in the passed vNote will be cleared</remarks>
        /// <param name="vNote">The existing vNote instance</param>
        /// <exception cref="ArgumentNullException">This is thrown if the specified vNote object is null</exception>
        protected VNoteParser(VNote vNote) : this()
        {
            currentNote = vNote ?? throw new ArgumentNullException(nameof(vNote),
                LR.GetString("ExParseNullObject", "vNote"));

            currentNote.ClearProperties();
            vNotes.Add(vNote);
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This static method can be used to load property values into a new instance of a single vNote from a
        /// string.
        /// </summary>
        /// <param name="vNoteText">A set of properties for a single vNote in a string</param>
        /// <returns>A new vNote instance as created from the string</returns>
        /// <example>
        /// <code language="cs">
        /// VNote vNote = VNoteParser.ParseFromString(oneVNote);
        /// </code>
        /// <code language="vbnet">
        /// Dim vNote As VNote = VNoteParser.ParseFromString(oneVNote)
        /// </code>
        /// </example>
        public static VNote ParseFromString(string vNoteText)
        {
            VNoteParser vcp = new();
            vcp.ParseString(vNoteText);

            return vcp.VNotes[0];
        }

        /// <summary>
        /// This static method can be used to load property values into an existing instance of a single vNote
        /// from a string.
        /// </summary>
        /// <param name="vNoteText">A set of properties for a single vNote in a string</param>
        /// <param name="vNote">The vNote instance into which the properties will be loaded</param>
        /// <remarks>The properties of the specified vNote will be cleared before the new properties are loaded
        /// into it.</remarks>
        /// <example>
        /// <code language="cs">
        /// VNote vNote = new VNote();
        /// VNoteParser.ParseFromString(oneVNote, vNote);
        /// </code>
        /// <code language="vbnet">
        /// Dim vNote As New VNote
        /// VNoteParser.ParseFromString(oneVNote, vNote)
        /// </code>
        /// </example>
        public static void ParseFromString(string vNoteText, VNote vNote)
        {
            VNoteParser vcp = new(vNote);
            vcp.ParseString(vNoteText);
        }

        /// <summary>
        /// This static method can be used to load property values into a new vNote collection from a string
        /// containing one or more vNotes.
        /// </summary>
        /// <param name="vNotes">A set of properties for one or more vNotes in a string</param>
        /// <returns>A new vNote collection as created from the string</returns>
        /// <example>
        /// <code language="cs">
        /// VNoteCollection vNotes = VNoteParser.ParseSetFromString(vNotes);
        /// </code>
        /// <code language="vbnet">
        /// Dim vNotes As VNoteCollection = VNoteParser.ParseSetFromString(vNotes)
        /// </code>
        /// </example>
        public static VNoteCollection ParseSetFromString(string vNotes)
        {
            VNoteParser vcp = new();
            vcp.ParseString(vNotes);

            return vcp.VNotes;
        }

        /// <summary>
        /// This static method can be used to load property values into a new vNote collection from a file.  The
        /// filename can be a disk file or a URL.
        /// </summary>
        /// <param name="filename">A path or URL to a file containing one or more vNotes</param>
        /// <returns>A new vNote collection as created from the file</returns>
        /// <example>
        /// <code language="cs">
        /// VNoteCollection vNotes1 = VNoteParser.ParseFromFile(@"C:\Notes.vnt");
        /// VNoteCollection vNotes2 = VNoteParser.ParseFromFile(
        ///     "http://www.mydomain.com/VNotes/Notes.vnt");
        /// </code>
        /// <code language="vbnet">
        /// Dim vNotes1 As VNoteCollection = VNoteParser.ParseFromFile("C:\Notes.vnt")
        /// Dim vNotes2 As VNoteCollection = VNoteParser.ParseFromFile( _
        ///     "http://www.mydomain.com/VNotes/Notes.vnt")
        /// </code>
        /// </example>
        public static VNoteCollection ParseFromFile(string filename)
        {
            VNoteParser vcp = new();
            vcp.ParseFile(filename);

            return vcp.VNotes;
        }

        /// <summary>
        /// This static method can be used to load property values into a new vNote collection from a
        /// <see cref="TextReader"/> derived object such as a <see cref="StreamReader"/> or a
        /// <see cref="StringReader"/>.
        /// </summary>
        /// <param name="stream">An IO stream from which to read the vNotes.  It is up to you to open the stream
        /// with the appropriate text encoding method if necessary.</param>
        /// <returns>A new vNote collection as created from the IO stream</returns>
        /// <example>
        /// <code language="cs">
        /// StreamReader sr = new StreamReader(@"C:\Test.vcf");
        /// VNoteCollection vNotes1 = VNoteParser.ParseFromStream(sr);
        /// sr.Close();
        /// </code>
        /// <code language="vbnet">
        /// Dim sr As New StreamReader("C:\Test.vcf")
        /// Dim vNotes1 As VNoteCollection = VNoteParser.ParseFromStream(sr)
        /// sr.Close()
        /// </code>
        /// </example>
        public static VNoteCollection ParseFromStream(TextReader stream)
        {
            VNoteParser vcp = new();
            vcp.ParseReader(stream);

            return vcp.VNotes;
        }

        /// <summary>
        /// This is implemented to handle properties as they are parsed from the data stream
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="parameters">A string collection containing the parameters and their values.  If empty,
        /// there are no parameters.</param>
        /// <param name="propertyValue">The value of the property.</param>
        /// <remarks><para>There may be a mixture of name/value pairs or values alone in the parameters string
        /// collection.  It is up to the derived class to process the parameter list based on the specification
        /// to which it conforms.  For entries that are parameter names, the entry immediately following it in
        /// the collection is its associated parameter value.  The property name, parameter names, and their
        /// values may be in upper, lower, or mixed case.</para>
        /// 
        /// <para>The value may be an encoded string.  The properties are responsible for any decoding that may
        /// need to occur (i.e. base 64 encoded image data).</para></remarks>
        /// <exception cref="PDIParserException">This is thrown if an error is encountered while parsing the data
        /// stream.  Refer to the and inner exceptions for information on the cause of the problem.</exception>
        protected override void PropertyParser(string propertyName, StringCollection parameters, string propertyValue)
        {
            string temp;
            int idx;

            // The last entry is always CustomProperty so scan for length minus one
            for(idx = 0; idx < ntv.Length - 1; idx++)
            {
                if(ntv[idx].IsMatch(propertyName))
                    break;
            }

            // An opening BEGIN:VNOTE property must have been seen
            if(currentNote == null && ntv[idx].EnumValue != PropertyType.Begin)
            {
                throw new PDIParserException(this.LineNumber, LR.GetString("ExParseNoBeginProp", "BEGIN:VNOTE",
                    propertyName));
            }

            // Handle or create the property
            switch(ntv[idx].EnumValue)
            {
                case PropertyType.Begin:
                    // The value must be VNOTE
                    if(String.Compare(propertyValue.Trim(), "VNOTE", StringComparison.OrdinalIgnoreCase) != 0)
                    {
                        throw new PDIParserException(this.LineNumber, LR.GetString("ExParseUnrecognizedTagValue",
                            ntv[idx].Name, propertyValue));
                    }

                    // NOTE: If serializing into an existing instance, this may not be null.  If so, it is
                    // ignored.
                    if(currentNote == null)
                    {
                        currentNote = new VNote();
                        vNotes.Add(currentNote);
                    }
                    break;

                case PropertyType.End:
                    // The value must be VNOTE
                    if(String.Compare(propertyValue.Trim(), "VNOTE", StringComparison.OrdinalIgnoreCase) != 0)
                        throw new PDIParserException(this.LineNumber, LR.GetString("ExParseUnrecognizedTagValue",
                            ntv[idx].Name, propertyValue));

                    // When done, we'll propagate the version number to all objects to make it consistent
                    currentNote!.PropagateVersion();

                    // The vNote is added to the collection when created so we don't have to rely on an END:VNOTE
                    // to add it.
                    currentNote = null;
                    break;

                case PropertyType.Version:
                    // Version must be 1.1
                    temp = propertyValue.Trim();

                    if(temp != "1.1")
                    {
                        throw new PDIParserException(this.LineNumber, LR.GetString("ExParseUnrecognizedVersion",
                            "vNote", temp));
                    }

                    currentNote!.Version = SpecificationVersions.IrMC11;
                    break;

                case PropertyType.UniqueId:
                    currentNote!.UniqueId.EncodedValue = propertyValue;
                    break;

                case PropertyType.Summary:
                    currentNote!.Summary.DeserializeParameters(parameters);
                    currentNote.Summary.EncodedValue = propertyValue;
                    break;

                case PropertyType.Body:
                    currentNote!.Body.DeserializeParameters(parameters);
                    currentNote.Body.EncodedValue = propertyValue;
                    break;

                case PropertyType.Class:
                    currentNote!.Classification.EncodedValue = propertyValue;
                    break;

                case PropertyType.Categories:
                    currentNote!.Categories.DeserializeParameters(parameters);
                    currentNote.Categories.EncodedValue = propertyValue;
                    break;

                case PropertyType.DateCreated:
                    currentNote!.DateCreated.DeserializeParameters(parameters);
                    currentNote.DateCreated.EncodedValue = propertyValue;
                    break;

                case PropertyType.LastModified:
                    currentNote!.LastModified.DeserializeParameters(parameters);
                    currentNote.LastModified.EncodedValue = propertyValue;
                    break;

                default:    // Anything else is a custom property
                    CustomProperty c = new(propertyName);
                    c.DeserializeParameters(parameters);
                    c.EncodedValue = propertyValue;
                    currentNote!.CustomProperties.Add(c);
                    break;
            }
        }
        #endregion
    }
}
