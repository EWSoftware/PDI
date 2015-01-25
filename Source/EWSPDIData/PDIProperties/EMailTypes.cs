//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : EMailTypes.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/21/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This enumerated type defines the various e-mail types for the EMailProperty
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

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This enumerated type defines the various e-mail types for the <see cref="EMailProperty"/>
    /// </summary>
    [Flags, Serializable]
    public enum EMailTypes
    {
        /// <summary>Indicates no type specified</summary>
        None =       0x0000,
        /// <summary>Indicates preferred e-mail (PREF) address</summary>
        Preferred =  0x0001,
        /// <summary>Indicates America On-Line (AOL)</summary>
        AOL =        0x0002,
        /// <summary>Indicates AppleLink (AppleLink)</summary>
        AppleLink =  0x0004,
        /// <summary>Indicates AT&amp;T Mail (ATTMail)</summary>
        ATTMail =    0x0008,
        /// <summary>Indicates CompuServe Information Service (CIS)</summary>
        CompuServe = 0x0010,
        /// <summary>Indicates eWorld (eWorld)</summary>
        eWorld =     0x0020,
        /// <summary>Indicates Internet SMTP (INTERNET, the default) </summary>
        Internet =   0x0040,
        /// <summary>Indicates IBM Mail (IBMMail)</summary>
        IBMMail =    0x0080,
        /// <summary>Indicates MCI Mail (MCIMail)</summary>
        MCIMail =    0x0100,
        /// <summary>Indicates PowerShare (POWERSHARE)</summary>
        PowerShare = 0x0200,
        /// <summary>Indicates Prodigy information service (PRODIGY)</summary>
        Prodigy =    0x0400,
        /// <summary>Indicates Telex number (TLX)</summary>
        Telex =      0x0800,
        /// <summary>Indicates X.400 service (X400)</summary>
        X400 =       0x1000
    }
}
