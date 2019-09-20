//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : GenderProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 04/22/2019
// Note    : Copyright 2019, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Gender property class used by the Personal Data Interchange (PDI) vCard class
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
using System.Linq;
using System.Text;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the gender (GENDER) property of a vCard object
    /// </summary>
    /// <remarks>This property class parses the <see cref="BaseProperty.Value"/> property to allow access to its
    /// individual sex and gender identity parts.  This property is only valid for use with the vCard 4.0
    /// specification.</remarks>
    public class GenderProperty : BaseProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Only supported by the vCard 4.0 specification</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCard40;

        /// <summary>
        /// This read-only property defines the tag (GENDER)
        /// </summary>
        public override string Tag => "GENDER";

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Text;

        /// <summary>
        /// This is used to get or set the sex
        /// </summary>
        public char? Sex { get; set; }

        /// <summary>
        /// This is used to get or set the gender identity
        /// </summary>
        public string GenderIdentity { get; set; }

        /// <summary>
        /// This property is overridden to handle parsing the component parts to/from their string form
        /// </summary>
        public override string Value
        {
            get
            {
                if(this.Sex == null && String.IsNullOrWhiteSpace(this.GenderIdentity))
                    return null;

                if(String.IsNullOrWhiteSpace(this.GenderIdentity))
                    return this.Sex.ToString();

                return String.Join(";", this.Sex?.ToString(), this.GenderIdentity);
            }
            set
            {
                this.Sex = null;
                this.GenderIdentity = null;

                if(!String.IsNullOrWhiteSpace(value))
                {
                    string[] parts = value.Split(';');

                    if(parts[0].Length != 0)
                    {
                        this.Sex = parts[0][0];

                        if(!(new[] { 'M', 'F', 'O', 'N', 'U' }).Contains(this.Sex.Value))
                            this.Sex = null;
                    }

                    if(parts.Length > 1)
                        this.GenderIdentity = parts[1];
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
            var o = new GenderProperty();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// The specifications do not allow parameters for this property.  Any parameters are ignored.
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        public override void SerializeParameters(StringBuilder sb)
        {
        }

        /// <summary>
        /// The specifications do not allow parameters for this property.  Any parameters are ignored.
        /// </summary>
        /// <param name="parameters">The parameters for the property</param>
        public override void DeserializeParameters(StringCollection parameters)
        {
        }
        #endregion
    }
}
