//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : AssemblyInfo.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 10/14/2014
// Note    : Copyright 2003-2013, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// PDI utility library assembly attributes
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 08/19/2003  EFW  Created the code
//===============================================================================================================

using System.Reflection;
using System.Security;

// Allow partially trusted callers when running with less than full trust
[assembly: AllowPartiallyTrustedCallers()]

// General assembly information
[assembly: AssemblyTitle("Personal Data Interchange and Miscellaneous Date Utility Classes")]
[assembly: AssemblyDescription("A set of useful Personal Data Interchange (PDI) and date utility classes")]

// See AssemblyInfoShared.cs for the shared attributes common to all projects in the solution
