//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : MimeSourceProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/21/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains Mime Source property class used by the Personal Data Interchange (PDI) vCard class
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
using System.Globalization;
using System.Text;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the MIME source type (SOURCE) property of a vCard.  This provides
    /// information on how to find the source for the vCard.
    /// </summary>
    /// <remarks>This is for use in specifying a MIME source type and is only valid for the 3.0 specification.
    /// The <see cref="BaseProperty.Value"/> property contains the source value.  It will decode the context
    /// parameter and make it accessible through the <see cref="Context"/> property.</remarks>
    public class MimeSourceProperty : BaseProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCard 3.0 only</value>
        public override SpecificationVersions VersionsSupported
        {
            get { return SpecificationVersions.vCard30; }
        }

        /// <summary>
        /// This read-only property defines the tag (SOURCE)
        /// </summary>
        public override string Tag
        {
            get { return "SOURCE"; }
        }

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation
        {
            get { return ValLocValue.Text; }
        }

        /// <summary>
        /// This property is used to set or get a string containing the context of the property value such as a
        /// protocol for a URI.
        /// </summary>
        /// <value>The value is a string defining the context of the property value such as LDAP, HTTP, etc.</value>
        public string Context { get; set; }

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public MimeSourceProperty()
        {
            this.Version = SpecificationVersions.vCard30;
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
            MimeSourceProperty o = new MimeSourceProperty();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            this.Context = ((MimeSourceProperty)p).Context;
            base.Clone(p);
        }

        /// <summary>
        /// This is overridden to provide custom handling of the CONTEXT parameter
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        public override void SerializeParameters(StringBuilder sb)
        {
            base.SerializeParameters(sb);

            // Serialize the key type if necessary
            if(!String.IsNullOrWhiteSpace(this.Context))
            {
                sb.Append(';');
                sb.Append(ParameterNames.Context);
                sb.Append('=');
                sb.Append(this.Context);
            }
        }

        /// <summary>
        /// This is overridden to provide custom handling of the CONTEXT parameter
        /// </summary>
        /// <param name="parameters">The parameters for the property</param>
        public override void DeserializeParameters(StringCollection parameters)
        {
            if(parameters == null || parameters.Count == 0)
                return;

            for(int paramIdx = 0; paramIdx < parameters.Count; paramIdx++)
                if(String.Compare(parameters[paramIdx], "CONTEXT=", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    // Remove the parameter name
                    parameters.RemoveAt(paramIdx);

                    if(paramIdx < parameters.Count)
                    {
                        this.Context = parameters[paramIdx];

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
