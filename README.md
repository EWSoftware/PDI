# Legacy Branch
This branch is for use with Visual Studio 2010 through Visual Studio 2017 15.4.  It uses standard MSBuild
project files and only targets .NET 4.0.  The version number on all PDI assemblies is 2015.1.19.0.
**This branch is not likely to receive any future updates unless pull requests are submitted by third parties
that still want to make use of this version.**

All new development will be done on the master branch using Visual Studio 2017 15.5 or later and will use the new
project format that supports .NET Standard.  The master branch targets .NET 3.5, 4.x, and .NET Standard 2.0 and
later.  All subsequent NuGet packages will be generated off of the master branch.

# EWSoftware.PDI Library
The EWSoftware Personal Data Interchange (PDI) Library presents you with a complete set of classes that let you
have access to all objects, properties, parameter types, and data types as defined by the vCard (RFC 2426),
vCalendar, and iCalendar (RFC 2445) specifications.  Using these classes, you can read and write data files in a
well-defined format used by many applications on various platforms to exchange personal information such as
business cards, telephone numbers, addresses, dates and times of appointments, etc.

A recurrence engine is also provided that allows you to easily and reliably calculate recurrence dates and times
for even the most complex recurrence patterns.  The classes can be used in both Windows Forms applications and
ASP.NET web applications.  Windows Forms and ASP.NET controls are included for editing recurrence patterns in a
user-friendly fashion.

The included web application demonstrates various aspects of the EWSoftware PDI classes (a C# and a VB.NET
version are included).  It covers most of the same areas as the other Windows Forms applications included in the
source code and shows several examples of data binding with the collection objects.  It also demonstrates the
recurrence pattern editor web server control.  The demo web application is available online so you can
[try it out](http://www.ewoodruff.us/PDIWebDemoCS/Default.aspx).

See the [online help content](http://EWSoftware.github.io/PDI/index.html) for usage and API information.
