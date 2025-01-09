//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : AssemblyInfo.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/09/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// PDI library common assembly attributes
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 07/01/2004  EFW  Created the code
// 12/21/2004  EFW  Various updates to the Windows Forms controls
// 11/23/2004  EFW  Fixed some problems with view state on dynamic image areas
// 06/28/2006  EFW  Reworked code for use with .NET 2.0
// 07/05/2013  EFW  Updated for use with .NET 4.0 and converted the project to open source
// 11/17/2018  EFW  Updated for use with .NET Standard 2.0
//===============================================================================================================

using System;
using System.Reflection;
using System.Runtime.InteropServices;

// NOTE: See AssemblyInfo.cs for project-specific assembly attributes

// General assembly information
[assembly: AssemblyCulture("")]

// The assembly is CLS compliant
[assembly: CLSCompliant(true)]

// Not visible to COM
[assembly: ComVisible(false)]

// Version numbers.  All version numbers for an assembly consists of the following four values:
//
//      Year of release
//      Month of release
//      Day of release
//      Revision (typically zero unless multiple releases are made on the same day)
//

// Common assembly strong name version - DO NOT CHANGE UNLESS NECESSARY.
//
// This is used to set the assembly version in the strong name.  This should remain unchanged to maintain binary
// compatibility with prior releases.  It should only be changed if a breaking change is made that requires
// assemblies that reference older versions to be recompiled against the newer version.
[assembly: AssemblyVersion("2021.11.23.0")]

// Common assembly file version
//
// This is used to set the assembly file version.  This will change with each new release.  MSIs only support a
// Major value between 0 and 255 so we drop the century from the year on this one.
[assembly: AssemblyFileVersion("25.1.9.0")]

// Common product version
//
// This may contain additional text to indicate Alpha or Beta states.  The version number will always match the
// file version above but includes the century on the year.
[assembly: AssemblyInformationalVersion("2025.1.9.0")]
