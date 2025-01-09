//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : PublicKeyProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains the Public Key property that support binary encoded public key values.  It is used with
// the Personal Data Interchange (PDI) vCard class.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/26/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.Text;

using EWSoftware.PDI.Parser;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Public Key (KEY) property of a vCard
    /// </summary>
    /// <remarks>The <see cref="BaseProperty.Value"/> property contains the key value in string form.  There is
    /// limited support for this property.  It will decode the key type parameter and make it accessible through
    /// the <see cref="KeyType"/> property.  The value is also accessible as a byte array via the
    /// <see cref="GetKeyBytes"/> method and can be set via the <see cref="SetKeyBytes"/> method.</remarks>
    public class PublicKeyProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        // This private array is used to translate parameter names and values to public key types
        private static readonly NameToValue<int>[] ntv =
        [
            new("TYPE", 0, false),
            new("X509", 1, true),
            new("PGP",  2, true)
        ];
        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports all vCard specifications</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll;

        /// <summary>
        /// This read-only property defines the default value type as BINARY
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Binary;

        /// <summary>
        /// This read-only property defines the tag (KEY)
        /// </summary>
        public override string Tag => "KEY";

        /// <summary>
        /// This is used to set or get the public key type
        /// </summary>
        /// <value>The value is a string defining the type of key that the property value represents such as
        /// X509, PGP, etc.</value>
        public string? KeyType { get; set; }

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor.  Unless the version is changed, the object will conform to the vCard 3.0 specification.
        /// </summary>
        public PublicKeyProperty()
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
            PublicKeyProperty o = new();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            this.KeyType = ((PublicKeyProperty)p).KeyType;
            base.Clone(p);
        }

        /// <summary>
        /// This is overridden to provide custom handling of the TYPE parameter
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        public override void SerializeParameters(StringBuilder sb)
        {
            base.SerializeParameters(sb);

            // Serialize the key type if necessary
            if(!String.IsNullOrWhiteSpace(this.KeyType))
            {
                sb.Append(';');

                // The format is different for the 3.0 spec and later
                if(this.Version != SpecificationVersions.vCard21)
                {
                    sb.Append(ParameterNames.Type);
                    sb.Append('=');
                }

                sb.Append(this.KeyType);
            }
        }

        /// <summary>
        /// This is overridden to provide custom handling of the TYPE parameter
        /// </summary>
        /// <param name="parameters">The parameters for the property</param>
        public override void DeserializeParameters(StringCollection parameters)
        {
            int idx;

            if(parameters == null || parameters.Count == 0)
                return;

            for(int paramIdx = 0; paramIdx < parameters.Count; paramIdx++)
            {
                for(idx = 0; idx < ntv.Length; idx++)
                {
                    if(ntv[idx].IsMatch(parameters[paramIdx]))
                        break;
                }

                if(idx == ntv.Length)
                {
                    // If it was a parameter name, skip the value too
                    if(parameters[paramIdx].EndsWith("=", StringComparison.Ordinal))
                        paramIdx++;

                    continue;   // Not a public key parameter
                }

                // Parameters may appear as a pair (name followed by value) or by value alone
                if(!ntv[idx].IsParameterValue)
                {
                    // Remove the TYPE parameter name so that the base class won't put it in the custom
                    // parameters.  We'll skip this one and decode the parameter value.
                    parameters.RemoveAt(paramIdx);
                }
                else
                {
                    this.KeyType = parameters[paramIdx];

                    // As above, remove the value
                    parameters.RemoveAt(paramIdx);
                }

                paramIdx--;
            }

            // Let the base class handle all other parameters
            base.DeserializeParameters(parameters);
        }

        /// <summary>
        /// This is used to get the bytes that make up the public key
        /// </summary>
        /// <returns>A byte array containing the public key.  The byte array is only valid for use with a key if
        /// the <see cref="BaseProperty.ValueLocation"/> is set to BINARY or INLINE.  If set to something else,
        /// the value is probably a URL or a pointer to some other location where the key can be found.</returns>
        public byte[] GetKeyBytes()
        {
            Encoding enc = Encoding.GetEncoding("iso-8859-1");
            return enc.GetBytes(base.Value);
        }

        /// <summary>
        /// This is used to set the bytes that make up the public key
        /// </summary>
        /// <param name="key">The byte array to use</param>
        /// <remarks>Setting the bytes will force the <see cref="BaseProperty.ValueLocation"/> property to
        /// BINARY.</remarks>
        public void SetKeyBytes(byte[] key)
        {
            this.ValueLocation = ValLocValue.Binary;

            Encoding enc = Encoding.GetEncoding("iso-8859-1");
            base.Value = enc.GetString(key);
        }
        #endregion
    }
}
