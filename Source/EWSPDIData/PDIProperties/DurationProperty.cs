//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : DurationProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains the Duration property class used by the Personal Data Interchange (PDI) vCalendar and
// iCalendar classes.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/31/2004  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the duration (DURATION) property of an iCalendar object.  This defines a
    /// duration of time for an item.
    /// </summary>
    /// <remarks>This class parses the <see cref="BaseProperty.Value"/> property to allow setting and getting the
    /// value as a <see cref="Duration"/> object.  This property is only applicable to iCalendar objects.  It is
    /// ignored for vCalendar objects except in the <c>VALARM</c> component where it represents the duration of
    /// the alarm.</remarks>
    public class DurationProperty : BaseProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCalendar 1.0 and iCalendar 2.0</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCalendar10 |
            SpecificationVersions.iCalendar20;

        /// <summary>
        /// This read-only property defines the tag (DURATION)
        /// </summary>
        public override string Tag => "DURATION";

        /// <summary>
        /// This read-only property defines the default value type as DURATION
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Duration;

        /// <summary>
        /// This allows the duration to be set or retrieved as a <see cref="Duration"/> object
        /// </summary>
        public Duration DurationValue { get; set; }

        /// <summary>
        /// This property is overridden to handle converting the text value to a duration object
        /// </summary>
        /// <value>Due to the variable nature of the definition of months and years, the value is always returned
        /// with the maximum unit of time expressed in weeks if needed.</value>
        public override string? Value
        {
            get
            {
                // If not set, nothing is returned
                if(this.DurationValue == PDI.Duration.Zero)
                    return null;

                return this.DurationValue.ToString(Duration.MaxUnit.Weeks);
            }
            set => this.DurationValue = new Duration(value);
        }

        /// <summary>
        /// This property is overridden to handle converting the text value to a duration object
        /// </summary>
        public override string? EncodedValue
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
        public DurationProperty()
        {
            this.DurationValue = Duration.Zero;
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
            DurationProperty o = new();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
