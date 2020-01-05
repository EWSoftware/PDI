//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : ClientPidMapProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/20/2019
// Note    : Copyright 2019, Eric Woodruff, All rights reserved
//
// This file contains the Client PID Map property class used by the Personal Data Interchange (PDI) vCard class
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 04/22/2019  EFW  Created the code
//===============================================================================================================

using System;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the client PID map (CLIENTPIDMAP) property of a vCard object
    /// </summary>
    /// <remarks>This property class parses the <see cref="BaseProperty.Value"/> property to allow access to its
    /// individual property ID and URI parts.  This property is only valid for use with the vCard 4.0
    /// specification.</remarks>
    public class ClientPidMapProperty : BaseProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Only supported by the vCard 4.0 specification</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCard40;

        /// <summary>
        /// This read-only property defines the tag (CLIENTPIDMAP)
        /// </summary>
        public override string Tag => "CLIENTPIDMAP";

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Text;
        
        /// <summary>
        /// This is used to get or set the property ID number
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// This is used to get or set the URI
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// This property is overridden to handle parsing the component parts to/from their string form
        /// </summary>
        public override string Value
        {
            get
            {
                if(this.Id == 0 || String.IsNullOrWhiteSpace(this.Uri))
                    return null;

                return String.Join(";", this.Id.ToString(), this.Uri);
            }
            set
            {
                this.Id = 0;
                this.Uri = null;

                if(!String.IsNullOrWhiteSpace(value))
                {
                    string[] parts = value.Split(';');

                    if(parts[0].Length != 0 && Int32.TryParse(parts[0], out int id) && id != 0)
                        this.Id = id;

                    if(parts.Length > 1)
                        this.Uri = parts[1];
                }
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the component parts to/from their string form
        /// </summary>
        public override string EncodedValue
        {
            get => this.Value;
            set => this.Value = value;
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
            var o = new ClientPidMapProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
