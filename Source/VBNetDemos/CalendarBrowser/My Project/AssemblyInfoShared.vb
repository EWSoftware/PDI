'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : AssemblyInfo.cs
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/09/2025
' Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
'
' PDI library demos common assembly attributes
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 11/16/2004  EFW  Created the code
'================================================================================================================

Imports System.Reflection
Imports System.Resources
Imports System.Runtime.InteropServices

' NOTE: See AssemblyInfo.cs for project-specific assembly attributes

' General assembly information
<Assembly: AssemblyProduct("EWSoftware Personal Data Interchange Library")>
<Assembly: AssemblyCompany("Eric Woodruff")>
<Assembly: AssemblyCopyright("Copyright \xA9 2003-2021, Eric Woodruff, All Rights Reserved")>
<Assembly: AssemblyCulture("")>
#If DEBUG
<Assembly: AssemblyConfiguration("Debug")>
#Else
<Assembly: AssemblyConfiguration("Release")>
#End If

' The assembly is CLS compliant
<Assembly: CLSCompliant(true)>

' Not visible to COM
<Assembly: ComVisible(false)>

' Resources contained within the assembly are English
<Assembly: NeutralResourcesLanguageAttribute("en")>

' Version numbers.  All version numbers for an assembly consists of the following four values:
'
'      Year of release
'      Month of release
'      Day of release
'      Revision (typically zero unless multiple releases are made on the same day)
'
<Assembly: AssemblyVersion("2025.1.9.0")>
<Assembly: AssemblyFileVersion("25.1.9.0")>
