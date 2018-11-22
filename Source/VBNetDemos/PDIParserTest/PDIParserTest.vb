'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : PDIParserTest.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 11/20/2018
' Note    : Copyright 2003-2018, Eric Woodruff, All rights reserved
' Compiler: Visual Basic .NET
'
' This is just a quick test of the PDI vCard and calendar parser classes in a console mode application
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 12/01/2004  EFW  Created the code
' 08/19/2007  EFW  Added support for vNote object
'================================================================================================================

' Ignore Spelling: iso

Imports System.IO
Imports System.Text

Imports EWSoftware.PDI.Parser
Imports EWSoftware.PDI.Properties

Module PDIParserTest

    ' This is used to test the EWSoftware.PDI.Data namespace Personal Data Interchange classes.  It scans the
    ' specified folder for vCard and vCalendar/iCalendar files and loads them to test the parser.  It then saves
    ' them to another specified folder so that the contents can be compared to verify that the PDI output methods
    ' are working as expected.  Note that the content order and formatting may change but the information overall
    ' should still be the same.
    Sub Main(args As String())
        Dim outputFolder, outputFile, file As String

        If args.Length < 2 Then
            Console.WriteLine("Using files from .\PDIFiles for the test" + Environment.NewLine)
            args = New String(1) { "..\..\..\..\..\..\PDIFiles", "..\..\..\..\..\..\PDIFiles_Copy" }
        End If

        Try
            ' Set the default encoding to iso-8859-1 (Western European) as several of the files are encoded that
            ' way.
            PDIParser.DefaultEncoding = Encoding.GetEncoding("iso-8859-1")
            BaseProperty.DefaultEncoding = Encoding.GetEncoding("iso-8859-1")

            outputFolder = args(1)

            If outputFolder.EndsWith("\", StringComparison.Ordinal) = False Then
                outputFolder &= "\"
            End If

            If Not Directory.Exists(outputFolder)
                Directory.CreateDirectory(outputFolder)
            End If

            Console.WriteLine("Parsing vCard files...")

            ' Since we'll be parsing multiple files, we'll create an instance of the parser and re-use it rather
            ' than using the static parsing methods.
            Dim vcardp As New VCardParser()

            ' Get a list of vCard files to parse
            For Each file In Directory.EnumerateFiles(args(0), "*.vcf")
                Console.WriteLine("{0}{1}", Environment.NewLine, file)
                vcardp.ParseFile(file)

                Console.WriteLine(vcardp.VCards.ToString())

                ' Write it to a stream
                outputFile = outputFolder + Path.GetFileName(file)

                ' NOTE: We must use the same encoding method when writing the file back out
                Using sw As new StreamWriter(outputFile, False, PDIParser.DefaultEncoding)
                    vcardp.VCards.WriteToStream(sw)
                End Using

                ' Clear the collection ready for the next run
                vcardp.VCards.Clear()
            Next

            Console.WriteLine("{0}Parsing vCalendar files...", Environment.NewLine)

            ' Since we'll be parsing multiple files, we'll create an instance of the parser and re-use it rather
            ' than using the static parser methods.
            Dim vcalp As New VCalendarParser()

            ' Get a list of vCalendar files to parse
            For Each file In Directory.EnumerateFiles(args(0), "*.vcs")
                Console.WriteLine("{0}{1}", Environment.NewLine, file)
                vcalp.ParseFile(file)

                Console.WriteLine(vcalp.VCalendar.ToString())

                ' Write it to a stream
                outputFile = outputFolder + Path.GetFileName(file)

                ' NOTE: We must use the same encoding method when writing the file back out
                Using sw As New StreamWriter(outputFile, False, PDIParser.DefaultEncoding)
                    vcalp.VCalendar.WriteToStream(sw)
                End Using

                ' Clear the calendar ready for the next run
                vcalp.VCalendar.ClearProperties()
            Next

            Console.WriteLine("{0}Parsing iCalendar files...", Environment.NewLine)

            ' Get a list of iCalendar files to parse.  It uses the same parser as the vCalendar files
            For Each file In Directory.EnumerateFiles(args(0), "*.ics")
                Console.WriteLine("{0}{1}", Environment.NewLine, file)
                vcalp.ParseFile(file)

                Console.WriteLine(vcalp.VCalendar.ToString())

                ' Write it to a stream
                outputFile = outputFolder + Path.GetFileName(file)

                ' NOTE: We must use the same encoding method when writing the file back out
                Using sw As New StreamWriter(outputFile, False, PDIParser.DefaultEncoding)
                    vcalp.VCalendar.WriteToStream(sw)
                End Using

                ' Clear the calendar ready for the next run
                vcalp.VCalendar.ClearProperties()
            Next

            Console.WriteLine("{0}Parsing vNote files...", Environment.NewLine)

            ' Since we'll be parsing multiple files, we'll create an instance of the parser and re-use it rather
            ' than using the static parser methods.
            Dim vnp As new VNoteParser()

            ' Get a list of vNote files to parse.
            For Each file In Directory.EnumerateFiles(args(0), "*.vnt")
                Console.WriteLine("{0}{1}", Environment.NewLine, file)
                vnp.ParseFile(file)

                Console.WriteLine(vnp.VNotes.ToString())

                ' Write it to a stream
                outputFile = outputFolder + Path.GetFileName(file)

                ' NOTE: We must use the same encoding method when
                ' writing the file back out.
                Using sw As New StreamWriter(outputFile, False, PDIParser.DefaultEncoding)
                    vnp.VNotes.WriteToStream(sw)
                End Using

                ' Clear the collection ready for the next run
                vnp.VNotes.Clear()
            Next

        Catch pe As PDIParserException
            Console.WriteLine("{0}{0}Error at line #{1}{0}{0}{2}", Environment.NewLine, pe.LineNumber, pe.ToString())

        Catch ex As Exception
            Console.WriteLine("Unexpected failure:{0}{1}", Environment.NewLine, ex.ToString())

        End Try
    End Sub

End Module
