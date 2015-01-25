//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : RecurrenceIdProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/21/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Recurrence ID property class used by the Personal Data Interchange (PDI) iCalendar
// class.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/30/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.Globalization;
using System.Text;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Recurrence ID (RECURRENCE-ID) property of an iCalendar object
    /// </summary>
    /// <remarks><para>This property class parses the <see cref="BaseProperty.Value"/> property to allow access
    /// to its content as an actual <see cref="System.DateTime"/> object.  The property value is a character
    /// string conforming to the basic format of ISO 8601.  This property is only valid for iCalendar 2.0
    /// objects.  It is ignored for vCalendar 1.0 objects.</para>
    /// 
    /// <para>This property is used in conjunction with the <c>UID</c> and <c>SEQUENCE</c> properties to identify
    /// a specific instance of a recurring <c>VEVENT</c>, <c>VTODO</c> or <c>VJOURNAL</c> calendar component.
    /// The property value is the effective value of the <c>DTSTART</c> property of the recurrence
    /// instance.</para></remarks>
    public class RecurrenceIdProperty : BaseDateTimeProperty
    {
        #region Private data members
        //=====================================================================

        private string range;
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
        /// This read-only property defines the tag (RECURRENCE-ID)
        /// </summary>
        public override string Tag
        {
            get { return "RECURRENCE-ID"; }
        }

        /// <summary>
        /// This read-only property defines the default value type as DATE-TIME
        /// </summary>
        public override string DefaultValueLocation
        {
            get { return ValLocValue.DateTime; }
        }

        /// <summary>
        /// This property is used to get or set the range (RANGE) parameter
        /// </summary>
        public string Range
        {
            get { return range; }
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                    range = value;
                else
                    range = null;
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public RecurrenceIdProperty()
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
            RecurrenceIdProperty o = new RecurrenceIdProperty();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            range = ((RecurrenceIdProperty)p).Range;
            base.Clone(p);
        }

        /// <summary>
        /// This is overridden to provide custom handling of the RANGE parameter
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        public override void SerializeParameters(StringBuilder sb)
        {
            base.SerializeParameters(sb);

            // Serialize the range if necessary
            if(range != null)
            {
                sb.Append(';');
                sb.Append(ParameterNames.Range);
                sb.Append('=');
                sb.Append(range);
            }
        }

        /// <summary>
        /// This is overridden to provide custom handling of the RANGE parameter
        /// </summary>
        /// <param name="parameters">The parameters for the property</param>
        public override void DeserializeParameters(StringCollection parameters)
        {
            if(parameters == null || parameters.Count == 0)
                return;

            for(int paramIdx = 0; paramIdx < parameters.Count; paramIdx++)
                if(String.Compare(parameters[paramIdx], "RANGE=", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    // Remove the parameter name
                    parameters.RemoveAt(paramIdx);

                    if(paramIdx < parameters.Count)
                    {
                        this.Range = parameters[paramIdx];

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
