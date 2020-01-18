//===============================================================================================================
// System  : EWSoftware PDI Demonstration Applications
// File    : AssemblyInfo.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/18/2020
// Note    : Copyright 2004-2020, Eric Woodruff, All rights reserved
//
// PDI library demos common assembly attributes
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 11/16/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

// NOTE: See AssemblyInfo.cs for project-specific assembly attributes

// General assembly information
[assembly: AssemblyProduct("EWSoftware Personal Data Interchange Library")]
[assembly: AssemblyCompany("Eric Woodruff")]
[assembly: AssemblyCopyright("Copyright \xA9 2003-2020, Eric Woodruff, All Rights Reserved")]
[assembly: AssemblyCulture("")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

// The assembly is CLS compliant
[assembly: CLSCompliant(true)]

// Not visible to COM
[assembly: ComVisible(false)]

// Resources contained within the assembly are English
[assembly: NeutralResourcesLanguage("en")]

// Version numbers.  All version numbers for an assembly consists of the following four values:
//
//      Year of release
//      Month of release
//      Day of release
//      Revision (typically zero unless multiple releases are made on the same day)
//
[assembly: AssemblyVersion("2020.1.18.0")]
[assembly: AssemblyFileVersion("20.1.18.0")]
