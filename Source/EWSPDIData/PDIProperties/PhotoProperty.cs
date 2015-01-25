//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : PhotoProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/21/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Photo property that support binary encoded images.  It is used with the Personal Data
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
    /// This class is used to represent the Photo (PHOTO) property of a vCard
    /// </summary>
    /// <remarks>The <see cref="BaseProperty.Value"/> property contains the photo in string form.  There is
    /// limited support for this property.  It will decode the image type parameter and make it accessible
    /// through the <see cref="ImageType"/> property.  The value is also accessible as a byte array via the
    /// <see cref="GetImageBytes"/> method and can be set via the <see cref="SetImageBytes"/> method.</remarks>
    public class PhotoProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        // This private array is used to translate parameter names and values to image types
        private static NameToValue<int>[] ntv = {
            new NameToValue<int>("TYPE", 0, false),
            new NameToValue<int>("GIF",  1, true),
            new NameToValue<int>("CGM",  2, true),
            new NameToValue<int>("WMF",  3, true),
            new NameToValue<int>("BMP",  4, true),
            new NameToValue<int>("MET",  5, true),
            new NameToValue<int>("PMB",  6, true),
            new NameToValue<int>("DIB",  7, true),
            new NameToValue<int>("PICT", 8, true),
            new NameToValue<int>("TIFF", 9, true),
            new NameToValue<int>("PS",   10, true),
            new NameToValue<int>("PDF",  11, true),
            new NameToValue<int>("JPEG", 12, true),
            new NameToValue<int>("MPEG", 13, true),
            new NameToValue<int>("MPEG2", 14, true),
            new NameToValue<int>("AVI",  15, true),
            new NameToValue<int>("QTIME", 16, true),
            new NameToValue<int>("PNG", 17, true)
        };
        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCard 2.1 and vCard 3.0</value>
        public override SpecificationVersions VersionsSupported
        {
            get { return SpecificationVersions.vCard21 | SpecificationVersions.vCard30; }
        }

        /// <summary>
        /// This read-only property defines the tag (PHOTO)
        /// </summary>
        public override string Tag
        {
            get { return "PHOTO"; }
        }

        /// <summary>
        /// This read-only property defines the default value type as BINARY
        /// </summary>
        public override string DefaultValueLocation
        {
            get { return ValLocValue.Binary; }
        }

        /// <summary>
        /// This is used to set or get the image type
        /// </summary>
        /// <value>The value is a string defining the type of image that the property value represents such as
        /// GIF, JPEG, TIFF, AVI, etc.
        /// </value>
        public string ImageType { get; set; }

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor.  Unless the version is changed, the object will conform to the vCard 3.0 specification.
        /// </summary>
        public PhotoProperty()
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
            PhotoProperty o = new PhotoProperty();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            this.ImageType = ((PhotoProperty)p).ImageType;
            base.Clone(p);
        }

        /// <summary>
        /// This is overridden to provide custom handling of the TYPE parameter
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        public override void SerializeParameters(StringBuilder sb)
        {
            base.SerializeParameters(sb);

            // Serialize the image type if necessary
            if(!String.IsNullOrWhiteSpace(this.ImageType))
            {
                sb.Append(';');

                // The format is different for the 3.0 spec
                if(this.Version == SpecificationVersions.vCard30)
                {
                    sb.Append(ParameterNames.Type);
                    sb.Append('=');
                }

                sb.Append(this.ImageType);
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

                    continue;   // Not a photo parameter
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
                    this.ImageType = parameters[paramIdx];

                    // As above, remove the value
                    parameters.RemoveAt(paramIdx);
                }

                paramIdx--;
            }

            // Let the base class handle all other parameters
            base.DeserializeParameters(parameters);
        }

        /// <summary>
        /// This is used to get the bytes that make up the image
        /// </summary>
        /// <returns>A byte array containing the image bytes.  The byte array is only valid for use with an image
        /// if the <see cref="BaseProperty.ValueLocation"/> is set to BINARY or INLINE.  If set to something
        /// else, the value is probably a URL or a pointer to some other location where the image can be found.</returns>
        public byte[] GetImageBytes()
        {
            Encoding enc = Encoding.GetEncoding("iso-8859-1");
            return enc.GetBytes(base.Value);
        }

        /// <summary>
        /// This is used to set the bytes that make up the image
        /// </summary>
        /// <param name="image">The byte array to use</param>
        /// <remarks>Setting the bytes will force the <see cref="BaseProperty.ValueLocation"/> property to
        /// BINARY.</remarks>
        public void SetImageBytes(byte[] image)
        {
            this.ValueLocation = ValLocValue.Binary;

            Encoding enc = Encoding.GetEncoding("iso-8859-1");
            base.Value = enc.GetString(image);
        }
        #endregion
    }
}
