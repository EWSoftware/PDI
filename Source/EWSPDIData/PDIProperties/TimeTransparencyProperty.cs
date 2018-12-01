//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : TimeTransparencyProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Time Transparency property class used by the Personal Data Interchange (PDI) vCalendar
// and iCalendar classes.
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

using System;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Time Transparency (TRANSP) property of a vCalendar or iCalendar
    /// object.  This defines whether an event is transparent or not to busy time searches (i.e. it consumes time
    /// on the calendar (opaque) or it does not consume time on the calendar (transparent)).
    /// </summary>
    /// <remarks>This class parses the <see cref="BaseProperty.Value"/> property so that it can be accessed as a
    /// Boolean to indicate whether or not an event is transparent or not.</remarks>
    public class TimeTransparencyProperty : BaseProperty
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
        /// This read-only property defines the tag (TRANSP)
        /// </summary>
        public override string Tag => "TRANSP";

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Text;

        /// <summary>
        /// This is used to return the transparency state as a Boolean
        /// </summary>
        /// <value>If it is transparent to busy time searches, it returns true.  If not, it returns false.</value>
        public bool IsTransparent { get; set; }

        /// <summary>
        /// This property is overridden to handle converting the text value to a Boolean value
        /// </summary>
        /// <value>Instead of throwing an exception, the property will convert unrecognized non-numeric values
        /// to false.</value>
        public override string Value
        {
            get
            {
                // If opaque (the default), it return nothing and won't be serialized
                if(!this.IsTransparent)
                    return null;

                // The format is different based on the specification
                return (this.Version == SpecificationVersions.vCalendar10) ? "1" : "TRANSPARENT";
            }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                {
                    // vCalendar 1.0 uses numeric values.  iCalendar 2.0 uses OPAQUE or TRANSPARENT.
                    this.IsTransparent = ((Char.IsDigit(value[0]) && value[0] != '0') ||
                        String.Compare(value.Trim(), "TRANSPARENT", StringComparison.OrdinalIgnoreCase) == 0);
                }
                else
                    this.IsTransparent = false;
            }
        }

        /// <summary>
        /// This property is overridden to handle converting the text value to a Boolean value
        /// </summary>
        public override string EncodedValue
        {
            get => this.Value;
            set => this.Value = value;
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Default constructor.  Unless the version is changed, the object will conform to the iCalendar 2.0
        /// specification.
        /// </summary>
        public TimeTransparencyProperty()
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
            TimeTransparencyProperty o = new TimeTransparencyProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
