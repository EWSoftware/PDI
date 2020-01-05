//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : PhoneTypes.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/16/2019
// Note    : Copyright 2004-2019, Eric Woodruff, All rights reserved
//
// This enumerated type defines the various phone types for the TelephoneProperty
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
    /// This enumerated type defines the various phone types for the <see cref="TelephoneProperty"/>
    /// </summary>
    [Flags, Serializable]
    public enum PhoneTypes
    {
        /// <summary>Indicates no type specified</summary>
        None =      0x0000,
        /// <summary>Indicates preferred (PREF) number (vCard 2.1 and 3.0 only)</summary>
        Preferred = 0x0001,
        /// <summary>Indicates a work (WORK) number</summary>
        Work =      0x0002,
        /// <summary>Indicates a home (HOME) number</summary>
        Home =      0x0004,
        /// <summary>Indicates a voice (VOICE) number (the default)</summary>
        Voice =     0x0008,
        /// <summary>Indicates a facsimile (FAX) number</summary>
        Fax =       0x0010,
        /// <summary>Indicates a messaging service (MSG) on the number</summary>
        Message =   0x0020,
        /// <summary>Indicates a cellular (CELL) number</summary>
        Cell =      0x0040,
        /// <summary>Indicates a pager (PAGER) number</summary>
        Pager =     0x0080,
        /// <summary>Indicates a bulletin board service (BBS) number</summary>
        BBS =       0x0100,
        /// <summary>Indicates a modem (MODEM) number</summary>
        Modem =     0x0200,
        /// <summary>Indicates a car-phone (CAR) number</summary>
        Car =       0x0400,
        /// <summary>Indicates an ISDN (ISDN) number</summary>
        ISDN =      0x0800,
        /// <summary>Indicates a video-phone (VIDEO) number</summary>
        Video =     0x1000,
        /// <summary>Indicates a personal communication services (PCS) telephone number (3.0 specification only)</summary>
        PCS =       0x2000,
        /// <summary>Indicates a text message (SMS) number (4.0 specification only)</summary>
        Text =      0x4000,
        /// <summary>Indicates a number for a telecommunication device for people with hearing or speech
        /// difficulties (4.0 specification only)</summary>
        TextPhone = 0x8000,
    }
}
