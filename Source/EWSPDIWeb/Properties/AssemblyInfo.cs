//===============================================================================================================
// System  : EWSoftware.PDI ASP.NET Web Server Controls
// File    : AssemblyInfo.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 10/14/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// ASP.NET web server controls that are used in conjunction with the EWSoftware.PDI classes
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 02/10/2005  EFW  Created the code
//===============================================================================================================

using System.Reflection;
using System.Security;
using System.Web.UI;

// Allow partially trusted callers when running with less than full trust
[assembly: AllowPartiallyTrustedCallers()]

// This prevents the website compiler from complaining about the security inheritance level of the designer
[assembly: SecurityRules(SecurityRuleSet.Level1)]

// General assembly information
[assembly: AssemblyTitle("EWSoftware.PDI ASP.NET Web Server Controls")]
[assembly: AssemblyDescription("ASP.NET web server controls that are used in conjunction with the " +
    "EWSoftware.PDI classes")]

// Define the embedded resources
[assembly: WebResource(EWSoftware.PDI.Web.Controls.RecurrencePattern.RecurrencePatternScripts +
    "DayInstance.js", "text/javascript")]

[assembly: WebResource(EWSoftware.PDI.Web.Controls.RecurrencePattern.RecurrencePatternScripts +
    "RecurrencePattern.js", "text/javascript")]

[assembly: WebResource(EWSoftware.PDI.Web.Controls.RecurrencePattern.RecurrencePatternHtml +
    "RecurrenceStyle.css", "text/css")]

// See AssemblyInfoShared.cs for the shared attributes common to all projects in the solution
