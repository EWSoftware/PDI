//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : ParameterType.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 04/22/2019
// Note    : Copyright 2004-2019, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the enumerated type that defines the known parameter types used by the Personal Data
// Interchange (PDI) parser classes.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/14/2004  EFW  Created the code
//===============================================================================================================

using System;

namespace EWSoftware.PDI.Parser
{
    /// <summary>
    /// This enumerated type defines known parameter types.  It is used to map parameter name strings to the
    /// matching parameter property.
    /// </summary>
    [Serializable]
    public enum ParameterType
    {
        /// <summary>An ENCODING parameter</summary>
        Encoding,
        /// <summary>A CHARSET parameter</summary>
        CharacterSet,
        /// <summary>A LANGUAGE parameter</summary>
        Language,
        /// <summary>A VALUE parameter</summary>
        ValueLocation,
        /// <summary>A CONTEXT parameter</summary>
        Context,
        /// <summary>A ROLE parameter</summary>
        Role,
        /// <summary>An RSVP parameter</summary>
        Rsvp,
        /// <summary>An EXPECT parameter</summary>
        Expect,
        /// <summary>A CUTYPE parameter</summary>
        CalendarUserType,
        /// <summary>A DELEGATED-FROM parameter</summary>
        DelegatedFrom,
        /// <summary>A DELEGATED-TO parameter</summary>
        DelegatedTo,
        /// <summary>A MEMBER parameter</summary>
        Member,
        /// <summary>A STATUS or PARTSTAT parameter</summary>
        Status,
        /// <summary>A custom (X-???) parameter</summary>
        Custom,
        /// <summary>A PID (property ID) parameter</summary>
        PropertyId
    }
}
