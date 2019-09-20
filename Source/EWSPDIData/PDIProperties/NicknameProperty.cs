//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : NicknameProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2019
// Note    : Copyright 2004-2019, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Nickname property class used by the Personal Data Interchange (PDI) vCard class
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
using System.Text;
using System.Text.RegularExpressions;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Nickname (NICKNAME) property of a vCard
    /// </summary>
    /// <remarks>This property class parses the <see cref="Value"/> property and allows access to the component
    /// parts.  It is used to specify the text corresponding to the nickname(s) of the object the vCard
    /// represents.  This property is only valid for use with the vCard 3.0 and 4.0 specification.</remarks>
    public class NicknameProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private static Regex reSplit = new Regex(@"(?:^[,;])|(?<=(?:[^\\]))[,;]");

        private StringCollection nicknames;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports the vCard 3.0 and 4.0 specifications</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCard30 |
            SpecificationVersions.vCard40;

        /// <summary>
        /// This read-only property defines the tag (NICKNAME)
        /// </summary>
        public override string Tag => "NICKNAME";

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Text;

        /// <summary>
        /// This property is used to get the nicknames string collection
        /// </summary>
        /// <value>Nicknames can be added to or removed from the returned collection reference</value>
        public StringCollection Nicknames => nicknames;

        /// <summary>
        /// This property is used to get or set the nicknames as a string value
        /// </summary>
        /// <value>The string can contain one or more nicknames separated by commas or semi-colons.  The string
        /// will be split and loaded into the nicknames string collection.</value>
        public string NicknamesString
        {
            get => String.Join(", ", nicknames);
            set
            {
                string tempName;
                string[] entries;

                nicknames.Clear();

                if(value != null)
                {
                    entries = value.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach(string s in entries)
                    {
                        tempName = s.Trim();

                        if(tempName.Length > 0)
                            nicknames.Add(tempName);
                    }
                }
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the nicknames and concatenating them when requested
        /// </summary>
        /// <value>The nicknames are escaped as needed</value>
        public override string Value
        {
            get
            {
                // If empty, nothing will be saved
                if(this.Nicknames.Count == 0)
                    return null;

                StringBuilder sb = new StringBuilder(256);

                foreach(string s in this.Nicknames)
                {
                    sb.Append(';');
                    sb.Append(EncodingUtils.Escape(s));
                }

                sb.Remove(0, 1);

                return sb.ToString();
            }
            set
            {
                string tempName;
                string[] entries;

                this.Nicknames.Clear();

                if(value != null && value.Length > 0)
                {
                    // Split on all semi-colons and commas except escaped ones
                    entries = reSplit.Split(value);

                    foreach(string s in entries)
                    {
                        tempName = EncodingUtils.Unescape(s.Trim());

                        if(tempName.Length > 0)
                            nicknames.Add(tempName);
                    }
                }
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the nicknames and concatenating them when requested
        /// </summary>
        /// <value>The nicknames are escaped as needed</value>
        public override string EncodedValue
        {
            get => this.Value;
            set => this.Value = value;
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public NicknameProperty()
        {
            this.Version = SpecificationVersions.vCard30;
            nicknames = new StringCollection();
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is overridden to allow cloning of a PDI object
        /// </summary>
        /// <returns>A clone of the object</returns>
        public override object Clone()
        {
            NicknameProperty o = new NicknameProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
