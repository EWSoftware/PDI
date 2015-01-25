//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : PDIParserTest.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/24/2014
// Note    : Copyright 2003-2014, Eric Woodruff, All rights reserved
// Compiler: Visual C#
//
// This is just a quick test of the PDI vCard and calendar parser classes in a console mode application
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/21/2004  EFW  Created the code
// 08/19/2007  EFW  Added support for vNote files
//===============================================================================================================

using System;
using System.IO;
using System.Text;

using EWSoftware.PDI.Parser;
using EWSoftware.PDI.Properties;

namespace PDIParserTest
{
	/// <summary>
    /// A simple test of the EWSoftware.PDI Personal Data Interchange classes
	/// </summary>
	class PDIParserTest
	{
		/// <summary>
        /// This is used to test the EWSoftware.PDI.Data namespace Personal Data Interchange classes.  It scans
        /// the specified folder for vCard and vCalendar/iCalendar files and loads them to test the parser.  It
        /// then saves them to another specified folder so that the contents can be compared to verify that the
        /// PDI output methods are working as expected.  Note that the content order and formatting may change
        /// but the information overall should still be the same.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
            string outputFolder, outputFile;

            if(args.GetUpperBound(0) < 1)
            {
                Console.WriteLine("Specify a folder name containing PDI files and a different one for the " +
                    "output files.");
                return;
            }

            try
            {
                // Set the default encoding to iso-8859-1 (Western European) as several of the files are encoded
                // that way.
                PDIParser.DefaultEncoding = BaseProperty.DefaultEncoding = Encoding.GetEncoding("iso-8859-1");

                outputFolder = args[1];

                if(!outputFolder.EndsWith(@"\", StringComparison.Ordinal))
                    outputFolder += @"\";

                if(!Directory.Exists(outputFolder))
                    Directory.CreateDirectory(outputFolder);

                Console.WriteLine("Parsing vCard files...");

                // Since we'll be parsing multiple files, we'll create an instance of the parser and re-use it
                // rather than using the static parsing methods.
                VCardParser vcardp = new VCardParser();

                foreach(string file in Directory.EnumerateFiles(args[0], "*.vcf"))
                {
                    Console.WriteLine("\n{0}", file);

                    vcardp.ParseFile(file);

                    Console.WriteLine(vcardp.VCards.ToString());

                    // Write it to a stream
                    outputFile = outputFolder + Path.GetFileName(file);

                    // NOTE: We must use the same encoding method when writing the file back out
                    using(var sw = new StreamWriter(outputFile, false, PDIParser.DefaultEncoding))
                    {
                        vcardp.VCards.WriteToStream(sw);
                    }

                    // Clear the collection ready for the next run
                    vcardp.VCards.Clear();
                }

                Console.WriteLine("\nParsing vCalendar files...");

                // Since we'll be parsing multiple files, we'll create an instance of the parser and re-use it
                // rather than using the static parser methods.
                VCalendarParser vcalp = new VCalendarParser();

                foreach(string file in Directory.EnumerateFiles(args[0], "*.vcs"))
                {
                    Console.WriteLine("\n{0}", file);

                    vcalp.ParseFile(file);

                    Console.WriteLine(vcalp.VCalendar.ToString());

                    // Write it to a stream
                    outputFile = outputFolder + Path.GetFileName(file);

                    // NOTE: We must use the same encoding method when writing the file back out
                    using(var sw = new StreamWriter(outputFile, false, PDIParser.DefaultEncoding))
                    {
                        vcalp.VCalendar.WriteToStream(sw);
                    }

                    // Clear the calendar ready for the next run
                    vcalp.VCalendar.ClearProperties();
                }

                Console.WriteLine("\nParsing iCalendar files...");

                // Get a list of iCalendar files to parse.  It uses the same parser as the vCalendar files
                foreach(string file in Directory.EnumerateFiles(args[0], "*.ics"))
                {
                    Console.WriteLine("\n{0}", file);

                    vcalp.ParseFile(file);

                    Console.WriteLine(vcalp.VCalendar.ToString());

                    // Write it to a stream
                    outputFile = outputFolder + Path.GetFileName(file);

                    // NOTE: We must use the same encoding method when writing the file back out
                    using(var sw = new StreamWriter(outputFile, false, PDIParser.DefaultEncoding))
                    {
                        vcalp.VCalendar.WriteToStream(sw);
                    }

                    // Clear the calendar ready for the next run
                    vcalp.VCalendar.ClearProperties();
                }

                Console.WriteLine("\nParsing vNote files...");

                // Since we'll be parsing multiple files, we'll create an instance of the parser and re-use it
                // rather than using the static parser methods.
                VNoteParser vnp = new VNoteParser();

                foreach(string file in Directory.EnumerateFiles(args[0], "*.vnt"))
                {
                    Console.WriteLine("\n{0}", file);

                    vnp.ParseFile(file);

                    Console.WriteLine(vnp.VNotes.ToString());

                    // Write it to a stream
                    outputFile = outputFolder + Path.GetFileName(file);

                    // NOTE: We must use the same encoding method when writing the file back out
                    using(var sw = new StreamWriter(outputFile, false, PDIParser.DefaultEncoding))
                    {
                        vnp.VNotes.WriteToStream(sw);
                    }

                    // Clear the collection ready for the next run
                    vnp.VNotes.Clear();
                }
            }
            catch(PDIParserException pe)
            {
                Console.WriteLine("\n\nError at line #{0}\n\n{1}", pe.LineNumber, pe.ToString());
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unexpected failure:\n{0}", ex.ToString());
            }
        }
    }
}
