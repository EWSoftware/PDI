//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : TimeZoneNameProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/19/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Time Zone Name property classes used by the Personal Data Interchange (PDI) iCalendar
// classes.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//   Date      Who  Comments
// ==============================================================================================================
// 09/20/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.Text.RegularExpressions;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Time Zone Name (TZNAME) property of a VTIMEZONE's DAYLIGHT or
    /// STANDARD component.  This specifies the customary designation for a time zone description.
    /// </summary>
    /// <remarks>This property has no special requirements or handling.  The <see cref="BaseProperty.Value"/>
    /// property contains the name.  This property is only valid for use with the iCalendar 2.0 specification.
    /// </remarks>
    public class TimeZoneNameProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private static Regex reSplit = new Regex(@"\s+");

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports iCalendar 2.0 only</value>
        public override SpecificationVersions VersionsSupported
        {
            get { return SpecificationVersions.iCalendar20; }
        }

        /// <summary>
        /// This read-only property defines the tag (TZNAME)
        /// </summary>
        public override string Tag
        {
            get { return "TZNAME"; }
        }

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation
        {
            get { return ValLocValue.Text; }
        }

        /// <summary>
        /// This read-only property is used to get the time zone name in its abbreviated form
        /// </summary>
        /// <remarks>This is useful when <see cref="BaseProperty.Value"/> is set to a full description. The
        /// property simply concatenates the first letter of every word in the description.  For example, Pacific
        /// Standard Time is returned as PST.  If there is only one word, it is assumed to be in abbreviated form
        /// already and that is returned instead.
        /// </remarks>
        public string AbbreviatedTimeZoneName
        {
            get
            {
                string[] parts = reSplit.Split(this.Value);
                int count = 0;

                if(parts.Length == 1)
                    return parts[0];

                char[] letters = new char[parts.Length];

                for(int idx = 0; idx < parts.Length; idx++)
                    if(Char.IsLetter(parts[idx][0]))
                    {
                        letters[idx] = parts[idx][0];
                        count++;
                    }

                return new String(letters, 0, count);
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        public TimeZoneNameProperty()
        {
            this.Version = SpecificationVersions.iCalendar20;
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
            TimeZoneNameProperty o = new TimeZoneNameProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
