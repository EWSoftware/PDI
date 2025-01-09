//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : BinaryProperties.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains the Attach property that support binary encoded attachments.  It is used with the Personal
// Data Interchange (PDI) vCalendar and iCalendar classes.
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

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Attachment (ATTACH) property of a vCalendar or iCalendar object
    /// </summary>
    /// <remarks>The <see cref="BaseProperty.Value"/> property contains the attachment value in string form.
    /// There is limited support for this property.  It will decode the format type parameter and make it
    /// accessible through the <see cref="FormatType"/> property.  The value is also accessible as a byte array
    /// via the <see cref="GetAttachmentBytes"/> method and can be set via the <see cref="SetAttachmentBytes"/>
    /// method.</remarks>
    public class AttachProperty : BaseProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCalendar 1.0 and iCalendar 2.0</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCalendar10 |
            SpecificationVersions.iCalendar20;

        /// <summary>
        /// This read-only property defines the tag (ATTACH)
        /// </summary>
        public override string Tag => "ATTACH";

        /// <summary>
        /// This read-only property defines the default value type as URI
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Uri;

        /// <summary>
        /// This is used to set or get the attachment format type
        /// </summary>
        /// <value>The value is a string defining the type of attachment that the property value represents such
        /// as <c>image/jpeg</c>, <c>application/binary</c>, <c>audio/basic</c>, etc.</value>
        public string? FormatType { get; set; }

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public AttachProperty()
        {
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
            AttachProperty o = new();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            this.FormatType = ((AttachProperty)p).FormatType;
            base.Clone(p);
        }

        /// <summary>
        /// This is overridden to provide custom handling of the FMTTYPE parameter
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        public override void SerializeParameters(StringBuilder sb)
        {
            base.SerializeParameters(sb);

            // Serialize the key type if necessary
            if(this.FormatType != null && this.FormatType.Length != 0)
            {
                sb.Append(';');
                sb.Append(ParameterNames.FormatType);
                sb.Append('=');
                sb.Append(this.FormatType);
            }
        }

        /// <summary>
        /// This is overridden to provide custom handling of the FMTTYPE parameter
        /// </summary>
        /// <param name="parameters">The parameters for the property</param>
        public override void DeserializeParameters(StringCollection parameters)
        {
            if(parameters == null || parameters.Count == 0)
                return;

            for(int paramIdx = 0; paramIdx < parameters.Count; paramIdx++)
            {
                if(String.Compare(parameters[paramIdx], "FMTTYPE=", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    // Remove the parameter name
                    parameters.RemoveAt(paramIdx);

                    if(paramIdx < parameters.Count)
                    {
                        this.FormatType = parameters[paramIdx];

                        // As above, remove the value
                        parameters.RemoveAt(paramIdx);
                    }
                    break;
                }
            }

            // Let the base class handle all other parameters
            base.DeserializeParameters(parameters);
        }

        /// <summary>
        /// This is used to get the bytes that make up the attachment
        /// </summary>
        /// <returns>A byte array containing the attachment bytes.  The byte array is only valid for use with an
        /// image if the <see cref="BaseProperty.ValueLocation"/> is set to BINARY.  If set to something else,
        /// the value is probably a URL or a pointer to some other location where the attachment can be found.</returns>
        public byte[] GetAttachmentBytes()
        {
            Encoding enc = Encoding.GetEncoding("iso-8859-1");
            return enc.GetBytes(base.Value);
        }

        /// <summary>
        /// This is used to set the bytes that make up the attachment
        /// </summary>
        /// <param name="attachment">The byte array to use</param>
        /// <remarks>Setting the bytes will force the <see cref="BaseProperty.ValueLocation"/> property to
        /// BINARY.</remarks>
        public void SetAttachmentBytes(byte[] attachment)
        {
            this.ValueLocation = ValLocValue.Binary;

            Encoding enc = Encoding.GetEncoding("iso-8859-1");
            base.Value = enc.GetString(attachment);
        }
        #endregion
    }
}
