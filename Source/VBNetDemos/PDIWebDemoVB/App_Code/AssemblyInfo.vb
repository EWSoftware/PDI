'===============================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : AssemblyInfo.cs
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 11/22/2018
' Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
' Compiler: Visual Basic .NET
'
' PDI Web Demo assembly attributes
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ==============================================================================================================
' 01/16/2005  EFW  Created the code
'===============================================================================================================

Imports System.Reflection
Imports System.Resources
Imports System.Runtime.InteropServices

' General assembly information
<Assembly: AssemblyProduct("EWSoftware Personal Data Interchange Library")>
<Assembly: AssemblyTitle("EWSoftware PDI Web Demo Application")>
<Assembly: AssemblyDescription("This is used to demonstrate the use of the PDI library classes in a web application")>
<Assembly: AssemblyCompany("Eric Woodruff")>
<Assembly: AssemblyCopyright("Copyright \xA9 2003-2018, Eric Woodruff, All Rights Reserved")>
<Assembly: AssemblyCulture("")>
#If DEBUG
<Assembly: AssemblyConfiguration("Debug")>
#Else
<Assembly: AssemblyConfiguration("Release")>
#End If

' The assembly is CLS compliant
<Assembly: CLSCompliant(True)>

' Not visible to COM
<Assembly: ComVisible(False)>

' Resources contained within the assembly are English
<Assembly: NeutralResourcesLanguageAttribute("en")>

' Version numbers.  All version numbers for an assembly consists of the following four values:
'
'      Year of release
'      Month of release
'      Day of release
'      Revision (typically zero unless multiple releases are made on the same day)
'
<Assembly: AssemblyVersion("2018.11.22.0")>
<Assembly: AssemblyFileVersion("18.11.22.0")>
