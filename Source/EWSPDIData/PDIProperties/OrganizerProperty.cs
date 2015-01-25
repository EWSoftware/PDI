//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : OrganizerProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/21/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Organizer property.  This property only applies to iCalendar 2.0
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 04/04/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.Globalization;
using System.Text;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the organizer (ORGANIZER) property of an iCalendar component.  This
    /// property defines the organizer within a calendar component.
    /// </summary>
    /// <remarks>The <see cref="BaseProperty.Value"/> property contains the organizer information.  There are
    /// some additional parameters that may appear on the object.  These are parsed and can be accessed via the
    /// class properties.  This property class is only used by iCalendar 2.0 objects.  It is ignored for
    /// vCalendar 1.0 objects.  There can be only one organizer so it does not have a collection class.</remarks>
    public class OrganizerProperty : BaseProperty
    {
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
        /// This read-only property defines the tag (ORGANIZER)
        /// </summary>
        public override string Tag
        {
            get { return "ORGANIZER"; }
        }

        /// <summary>
        /// This read-only property defines the default value type as CAL-ADDRESS (calendar address)
        /// </summary>
        public override string DefaultValueLocation
        {
            get { return ValLocValue.CalAddress; }
        }

        /// <summary>
        /// This property is used to set or get the common name (CN) parameter associated with the calendar user
        /// specified by the property value.
        /// </summary>
        /// <value>This parameter is only applicable to iCalendar 2.0 objects</value>
        public string CommonName { get; set; }

        /// <summary>
        /// This property is used to set or get the directory entry (DIR) parameter associated with the calendar
        /// user specified by the property value.
        /// </summary>
        /// <value>This parameter is only applicable to iCalendar 2.0 objects</value>
        public string DirectoryEntry { get; set; }

        /// <summary>
        /// This property is used to set or get the sent-by (SENT-BY) parameter that specifies the calendar user
        /// that is acting on behalf of the calendar user specified by the property value.
        /// </summary>
        /// <value>This parameter is only applicable to iCalendar 2.0 objects</value>
        public string SentBy { get; set; }

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public OrganizerProperty()
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
            OrganizerProperty o = new OrganizerProperty();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            OrganizerProperty op = (OrganizerProperty)p;

            this.CommonName = op.CommonName;
            this.DirectoryEntry = op.DirectoryEntry;
            this.SentBy = op.SentBy;

            base.Clone(p);
        }

        /// <summary>
        /// This is overridden to provide custom handling of the extra parameters
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        public override void SerializeParameters(StringBuilder sb)
        {
            base.SerializeParameters(sb);

            // Serialize the extra parameters if necessary
            if(this.Version == SpecificationVersions.iCalendar20)
            {
                // Common name is enclosed in quotes if it contains a comma, semi-colon, or colon
                if(!String.IsNullOrWhiteSpace(this.CommonName))
                {
                    sb.Append(';');
                    sb.Append(ParameterNames.CommonName);
                    sb.Append('=');

                    if(this.CommonName.IndexOfAny(new char[] { ',', ';', ':' }) != -1)
                    {
                        sb.Append('\"');
                        sb.Append(this.CommonName);
                        sb.Append('\"');
                    }
                    else
                        sb.Append(this.CommonName);
                }

                // Directory entry is always enclosed in quotes
                if(!String.IsNullOrWhiteSpace(this.DirectoryEntry))
                {
                    sb.Append(';');
                    sb.Append(ParameterNames.DirectoryEntry);
                    sb.Append("=\"");
                    sb.Append(this.DirectoryEntry);
                    sb.Append('\"');
                }

                // Sent By entry is always enclosed in quotes
                if(!String.IsNullOrWhiteSpace(this.SentBy))
                {
                    sb.Append(';');
                    sb.Append(ParameterNames.SentBy);
                    sb.Append("=\"");
                    sb.Append(this.SentBy);
                    sb.Append('\"');
                }
            }
        }

        /// <summary>
        /// This is overridden to provide custom handling of the extra parameters
        /// </summary>
        /// <param name="parameters">The parameters for the property</param>
        public override void DeserializeParameters(StringCollection parameters)
        {
            if(parameters == null || parameters.Count == 0)
                return;

            for(int paramIdx = 0; paramIdx < parameters.Count; paramIdx++)
            {
                if(String.Compare(parameters[paramIdx], "CN=", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    // Remove the parameter name
                    parameters.RemoveAt(paramIdx);

                    if(paramIdx < parameters.Count)
                    {
                        this.CommonName = parameters[paramIdx];

                        // As above, remove the value
                        parameters.RemoveAt(paramIdx);
                    }
                }
                else
                    if(String.Compare(parameters[paramIdx], "DIR=", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        parameters.RemoveAt(paramIdx);

                        if(paramIdx < parameters.Count)
                        {
                            this.DirectoryEntry = parameters[paramIdx];
                            parameters.RemoveAt(paramIdx);
                        }
                    }
                    else
                        if(String.Compare(parameters[paramIdx], "SENT-BY=", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            parameters.RemoveAt(paramIdx);

                            if(paramIdx < parameters.Count)
                            {
                                this.SentBy = parameters[paramIdx];
                                parameters.RemoveAt(paramIdx);
                            }
                        }
            }

            // Let the base class handle all other parameters
            base.DeserializeParameters(parameters);
        }
        #endregion
    }
}
