//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : FreeBusyProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/21/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class for the FREEBUSY property used by the Personal Data Interchange (PDI) VFreeBusy
// class.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 09/10/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;

using EWSoftware.PDI.Binding;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent a Free/Busy (FREEBUSY) property of a free/busy calendar object
    /// </summary>
    /// <remarks>This property class parses the <see cref="BaseProperty.Value"/> property to allow access to its
    /// content as an actual <see cref="PDI.Period"/> object.  It also allows you to set the
    /// <see cref="FreeBusyType"/> for the property.</remarks>
    [TypeDescriptionProvider(typeof(FreeBusyPropertyTypeDescriptionProvider))]
    public class FreeBusyProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private Period period;
        private FreeBusyType freeBusyType;
        private string otherType;
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
        /// This read-only property defines the tag (FREEBUSY)
        /// </summary>
        public override string Tag
        {
            get { return "FREEBUSY"; }
        }

        /// <summary>
        /// This read-only property defines the default value type as PERIOD
        /// </summary>
        public override string DefaultValueLocation
        {
            get { return ValLocValue.Period; }
        }

        /// <summary>
        /// This property is used to set or get the free/busy type (FBTYPE) parameter
        /// </summary>
        /// <value>Setting this parameter to <c>Other</c> sets the <see cref="OtherType"/> to <c>X-UNKNOWN</c>
        /// if not already set to something else.  It is set to null if set to any other free/busy type.</value>
        public FreeBusyType FreeBusyType
        {
            get { return freeBusyType; }
            set
            {
                freeBusyType = value;

                if(freeBusyType != FreeBusyType.Other)
                    otherType = null;
                else
                    if(String.IsNullOrWhiteSpace(otherType))
                        otherType = "X-UNKNOWN";
            }
        }

        /// <summary>
        /// This property is used to set or get the free/busy type string when the type is set to <c>Other</c>
        /// </summary>
        /// <value>Setting this parameter automatically sets the <see cref="FreeBusyType"/> property to
        /// <c>Other</c>.</value>
        public string OtherType
        {
            get { return otherType; }
            set
            {
                freeBusyType = FreeBusyType.Other;

                if(!String.IsNullOrWhiteSpace(value))
                    otherType = value;
                else
                    otherType = "X-UNKNOWN";
            }
        }

        /// <summary>
        /// This is used to get or set the value as a <see cref="PDI.Period"/> object
        /// </summary>
        public Period PeriodValue
        {
            get
            {
                if(period == null)
                    period = new Period();

                return period;
            }
            set
            {
                period = value;
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the period to/from its string form
        /// </summary>
        public override string Value
        {
            get
            {
                // If duration is null or less than 1, nothing will be saved
                if(period == null || period.Duration.Ticks < 0)
                    return null;

                // Periods are easy, they format themselves
                return period.ToString();
            }
            set
            {
                if(value != null && value.Length > 0)
                    period = new Period(value);
                else
                    period = null;
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the period to/from its string form
        /// </summary>
        public override string EncodedValue
        {
            get { return this.Value; }
            set { this.Value = value; }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public FreeBusyProperty()
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
            FreeBusyProperty o = new FreeBusyProperty();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            FreeBusyProperty fbp = (FreeBusyProperty)p;

            freeBusyType = fbp.FreeBusyType;

            if(freeBusyType == FreeBusyType.Other)
                otherType = fbp.OtherType;
            else
                otherType = null;

            base.Clone(p);
        }

        /// <summary>
        /// This is overridden to provide custom handling of the FBTYPE parameter
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        public override void SerializeParameters(StringBuilder sb)
        {
            base.SerializeParameters(sb);

            // Serialize the free/busy type if necessary
            if(freeBusyType != FreeBusyType.None)
            {
                sb.Append(';');
                sb.Append(ParameterNames.FreeBusyType);
                sb.Append('=');

                switch(freeBusyType)
                {
                    case FreeBusyType.Free:
                        sb.Append("FREE");
                        break;

                    case FreeBusyType.Busy:
                        sb.Append("BUSY");
                        break;

                    case FreeBusyType.BusyUnavailable:
                        sb.Append("BUSY-UNAVAILABLE");
                        break;

                    case FreeBusyType.BusyTentative:
                        sb.Append("BUSY-TENTATIVE");
                        break;

                    default:
                        if(!String.IsNullOrWhiteSpace(otherType))
                            sb.Append(otherType);
                        else
                            sb.Append("X-UNKNOWN");
                        break;
                }
            }
        }

        /// <summary>
        /// This is overridden to provide custom handling of the FBTYPE parameter
        /// </summary>
        /// <param name="parameters">The parameters for the property</param>
        public override void DeserializeParameters(StringCollection parameters)
        {
            string type;

            if(parameters == null || parameters.Count == 0)
                return;

            for(int paramIdx = 0; paramIdx < parameters.Count; paramIdx++)
                if(String.Compare(parameters[paramIdx], "FBTYPE=", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    // Remove the parameter name
                    parameters.RemoveAt(paramIdx);

                    if(paramIdx < parameters.Count)
                    {
                        type = parameters[paramIdx].Trim().ToUpperInvariant();

                        switch(type)
                        {
                            case "FREE":
                                this.FreeBusyType = FreeBusyType.Free;
                                break;

                            case "BUSY":
                                this.FreeBusyType = FreeBusyType.Busy;
                                break;

                            case "BUSY-UNAVAILABLE":
                                this.FreeBusyType = FreeBusyType.BusyUnavailable;
                                break;

                            case "BUSY-TENTATIVE":
                                this.FreeBusyType = FreeBusyType.BusyTentative;
                                break;

                            default:
                                this.OtherType = parameters[paramIdx];
                                break;
                        }

                        // As above, remove the value
                        parameters.RemoveAt(paramIdx);
                    }
                    break;
                }

            // Let the base class handle all other parameters
            base.DeserializeParameters(parameters);
        }
        #endregion
    }
}
