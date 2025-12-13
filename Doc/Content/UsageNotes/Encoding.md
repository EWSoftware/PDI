---
uid: 6836dc6c-6dbe-4620-a968-3e6744a2dc9c
alt-uid: Encoding
title: File and Property Encoding
keywords: "usage notes, file encoding", "usage notes, property encoding"
---

By default, the parser classes will open a file stream for input with UTF-8 encoding.  However, this
may not be appropriate if you are using files saved with some other type of encoding format.  This only becomes
an issue if your files contain entities such as accented characters, etc.


In such cases, you can change the parser's default encoding used to open files by assigning an
appropriate `Encoding` class instance to the
[](@P:EWSoftware.PDI.Parser.PDIParser.DefaultEncoding){show-container="true"}
property.  When opening an input stream manually or for saving a file, it is up to you to open the file stream
with an appropriate encoding.  You can use the `PDIParser.DefaultEncoding` value to open
an output file stream with the same encoding used for reading the file.  The demo applications contain some
examples of this.


In addition, the vCard 2.1 and vCalendar 1.0 specification support a `CHARSET`
parameter on properties.  This allows them to specify what character set the property value uses.  In order to
read the values, they must be translated from the given character set. For example, if using the UTF-8 character
set, the characters need to be decoded for display on your system and encoded when written back out.  The
[](@P:EWSoftware.PDI.Properties.BaseProperty.DefaultEncoding){show-container="true"}
property exists to handle this translation in a fashion similar to the parser.  By default, it is set to use
ASCII encoding.  If you will be dealing with files that contain properties with alternate encodings, you may need
to set this to something more appropriate for your system (i.e. use a Western European encoding so that accented
characters are converted correctly). The demo applications contain some examples of this.



## See Also


**Other Resources**  
[](@695cc5b6-0937-42cd-9e7a-2f8aab4f09b0)  
