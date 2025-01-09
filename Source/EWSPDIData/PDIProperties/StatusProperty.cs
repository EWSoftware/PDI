//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : StatusProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains the Status property class used by the Personal Data Interchange (PDI) vCalendar and
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

using EWSoftware.PDI.Parser;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Status (STATUS) property of an iCalendar object.  This defines the
    /// overall status or confirmation for the calendar component.
    /// </summary>
    /// <remarks>This class decodes the <see cref="BaseProperty.Value"/> property to allow access to it as a
    /// <see cref="StatusValue"/> value.</remarks>
    public class StatusProperty : BaseAltRepProperty
    {
        #region Private data members
        //=====================================================================

        // This private array is used to translate status value names to enumerated status values
        private static readonly NameToValue<StatusValue>[] ntv =
        [
            new("ACCEPTED", StatusValue.Accepted, true),
            new("NEEDS-ACTION", StatusValue.NeedsAction, true),
            new("NEEDS ACTION", StatusValue.NeedsAction, true),
            new("SENT", StatusValue.Sent, true),
            new("TENTATIVE", StatusValue.Tentative, true),
            new("CONFIRMED", StatusValue.Confirmed, true),
            new("DECLINED", StatusValue.Declined, true),
            new("COMPLETED", StatusValue.Completed, true),
            new("DELEGATED", StatusValue.Delegated, true),
            new("CANCELLED", StatusValue.Cancelled, true),
            new("IN-PROCESS", StatusValue.InProcess, true),
            new("DRAFT", StatusValue.Draft, true),
            new("FINAL", StatusValue.Final, true)
        ];
        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCalendar 1.0 and iCalendar 2.0</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCalendar10 |
            SpecificationVersions.iCalendar20;

        /// <summary>
        /// This read-only property defines the tag (STATUS)
        /// </summary>
        public override string Tag => "STATUS";

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Text;

        /// <summary>
        /// This property is used to set or get the status value
        /// </summary>
        public StatusValue StatusValue { get; set; }

        /// <summary>
        /// This property is overridden to handle converting the text value to an enumerated status value
        /// </summary>
        public override string? Value
        {
            get
            {
                // If it's the default, return nothing
                if(this.StatusValue == StatusValue.None)
                    return null;

                for(int idx = 0; idx < ntv.Length; idx++)
                {
                    if(this.StatusValue == ntv[idx].EnumValue)
                        return ntv[idx].Name;
                }

                return null;
            }
            set
            {
                this.StatusValue = StatusValue.None;

                if(value != null && value.Length != 0)
                {
                    for(int idx = 0; idx < ntv.Length; idx++)
                    {
                        if(ntv[idx].IsMatch(value))
                        {
                            this.StatusValue = ntv[idx].EnumValue;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This property is overridden to handle converting the text value to an enumerated status value
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
        /// Default constructor.  Unless the version is changed, the object will conform to the iCalendar 2.0
        /// specification.
        /// </summary>
        public StatusProperty()
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
            StatusProperty o = new();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
