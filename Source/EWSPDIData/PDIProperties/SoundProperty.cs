//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : SoundProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2019
// Note    : Copyright 2004-2019, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Sound property that support binary encoded sounds.  It is used with the Personal Data
// Interchange (PDI) vCard class.
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
    /// This class is used to represent the Sound (SOUND) property of a vCard
    /// </summary>
    /// <remarks>The <see cref="BaseProperty.Value"/> property contains the sound in string form.  There is
    /// limited support for this property.  It will decode the sound type parameter and make it accessible
    /// through the <see cref="SoundType"/> property.  The value is also accessible as a byte array via the
    /// <see cref="GetSoundBytes"/> method and can be set via the <see cref="SetSoundBytes"/> method.</remarks>
    public class SoundProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        // This private array is used to translate parameter names and values to sound types
        private static NameToValue<int>[] ntv = {
            new NameToValue<int>("TYPE",  0, false),
            new NameToValue<int>("WAVE",  1, true),
            new NameToValue<int>("PCM",   2, true),
            new NameToValue<int>("AIF",   3, true),
            new NameToValue<int>("BASIC", 4, true)
        };
        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports all vCard specifications</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll;

        /// <summary>
        /// This read-only property defines the tag (SOUND)
        /// </summary>
        public override string Tag => "SOUND";

        /// <summary>
        /// This read-only property defines the default value type as BINARY
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Binary;

        /// <summary>
        /// This is used to set or get the sound type
        /// </summary>
        /// <value>The value is a string defining the type of sound that the property value represents such as
        /// basic, WAV, etc.</value>
        public string SoundType { get; set; }

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Default constructor.  Unless the version is changed, the object will conform to the vCard 3.0
        /// specification.
        /// </summary>
        public SoundProperty()
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
            SoundProperty o = new SoundProperty();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            this.SoundType = ((SoundProperty)p).SoundType;
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
            if(this.SoundType != null && this.SoundType.Length != 0)
            {
                sb.Append(';');

                // The format is different for the 3.0 spec and later
                if(this.Version != SpecificationVersions.vCard21)
                {
                    sb.Append(ParameterNames.Type);
                    sb.Append('=');
                }

                sb.Append(this.SoundType);
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
                    if(ntv[idx].IsMatch(parameters[paramIdx]))
                        break;

                if(idx == ntv.Length)
                {
                    // If it was a parameter name, skip the value too
                    if(parameters[paramIdx].EndsWith("=", StringComparison.Ordinal))
                        paramIdx++;

                    continue;   // Not a sound parameter
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
                    this.SoundType = parameters[paramIdx];

                    // As above, remove the value
                    parameters.RemoveAt(paramIdx);
                }

                paramIdx--;
            }

            // Let the base class handle all other parameters
            base.DeserializeParameters(parameters);
        }

        /// <summary>
        /// This is used to get the bytes that make up the sound
        /// </summary>
        /// <returns>A byte array containing the sound bytes.  The byte array is only valid for use with a sound
        /// if the <see cref="BaseProperty.ValueLocation"/> is set to BINARY or INLINE.  If set to something
        /// else, the value is probably a URL or a pointer to some other location where the sound can be found.</returns>
        public byte[] GetSoundBytes()
        {
            Encoding enc = Encoding.GetEncoding("iso-8859-1");
            return enc.GetBytes(base.Value);
        }

        /// <summary>
        /// This is used to set the bytes that make up the sound
        /// </summary>
        /// <param name="sound">The byte array to use</param>
        /// <remarks>Setting the bytes will force the <see cref="BaseProperty.ValueLocation"/> property to BINARY</remarks>
        public void SetSoundBytes(byte[] sound)
        {
            this.ValueLocation = ValLocValue.Binary;

            Encoding enc = Encoding.GetEncoding("iso-8859-1");
            base.Value = enc.GetString(sound);
        }
        #endregion
    }
}
