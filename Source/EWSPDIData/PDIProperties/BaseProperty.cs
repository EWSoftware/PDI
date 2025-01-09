//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : BaseProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/02/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains the base property class used by the Personal Data Interchange (PDI) classes such as
// vCalendar, iCalendar, and vCard.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//   Date      Who  Comments
// ==============================================================================================================
// 03/14/2004  EFW  Created the code
// 12/29/2005  EFW  Fixed up some encoding issues
//===============================================================================================================

using System;
using System.IO;
using System.Text;

using EWSoftware.PDI.Parser;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This abstract class is used as the base class for all other properties
    /// </summary>
    /// <remarks>It implements functionality common to all properties.  It also provides <c>ToString</c> methods
    /// used to convert the instance to a format suitable for writing to a PDI data stream.</remarks>
    public abstract class BaseProperty : PDIObject
    {
        #region Private data members
        //=====================================================================

        private string encMethod = null!,   // The encoding method used on the property value
            charSet = null!,        // The character set used for the property value
            language = null!;       // The language used for the property value
        private string? propValue,   // The value of the property
            location;       // The data type/location of the property value

        private EncodingType encType; // This is kept in sync with encMethod

        // This is used to map parameter name and value strings to a ParameterType enumeration
        private static readonly NameToValue<ParameterType>[] ntv =
        [
            new(ParameterNames.Encoding, ParameterType.Encoding),
            new(ParameterNames.CharacterSet, ParameterType.CharacterSet),
            new(ParameterNames.Language, ParameterType.Language),
            new(ParameterNames.ValueLocation, ParameterType.ValueLocation),
            new(ParameterNames.PropertyId, ParameterType.PropertyId),
            new(EncodingValue.SevenBit, ParameterType.Encoding, true),
            new(EncodingValue.EightBit, ParameterType.Encoding, true),
            new(EncodingValue.QuotedPrintable, ParameterType.Encoding, true),
            new(EncodingValue.Base64, ParameterType.Encoding, true),
            new(EncodingValue.BEncoding, ParameterType.Encoding, true),
            new(CharSetValue.ASCII, ParameterType.CharacterSet, true),
            new(LanguageValue.USEnglish, ParameterType.Language, true),
            new(ValLocValue.Inline, ParameterType.ValueLocation, true),
            new(ValLocValue.Text, ParameterType.ValueLocation, true),
            new(ValLocValue.Date, ParameterType.ValueLocation, true),
            new(ValLocValue.DateTime, ParameterType.ValueLocation, true),
            new(ValLocValue.Binary, ParameterType.ValueLocation, true),
            new(ValLocValue.Url, ParameterType.ValueLocation, true),
            new(ValLocValue.Uri, ParameterType.ValueLocation, true),
            new(ValLocValue.ContentId, ParameterType.ValueLocation, true),
            new(ValLocValue.Cid, ParameterType.ValueLocation, true),

            // The last entry should always be Custom to catch all unrecognized parameters.  The actual property
            // name is not relevant.
            new NameToValue<ParameterType>("X-", ParameterType.Custom)
        ];
        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used for the default text encoding when reading and writing properties with a non-ASCII
        /// CHARSET parameter.
        /// </summary>
        /// <value>The default text encoding method is ASCII.  You can set this property before parsing a PDI
        /// data stream to alter how text with non-ASCII CHARSET properties are interpreted.  In general, this
        /// property is only useful with vCard 2.1 and vCalendar 1.0 objects as they are the only ones that make
        /// use of the CHARSET parameter.</value>
        public static Encoding DefaultEncoding { get; set; } = new ASCIIEncoding();

        /// <summary>
        /// This read-only property must be specified to define the tag used for the property (i.e. FN, N, ADR,
        /// etc).
        /// </summary>
        /// <value>This should return a string that represents the property type in the PDI data stream</value>
        public abstract string Tag { get; }

        /// <summary>
        /// Derived classes should override this to define the default data type/location to use if a VALUE
        /// parameter is not specified.
        /// </summary>
        public abstract string DefaultValueLocation { get; }

        /// <summary>
        /// This is overridden to enforce the correct encoding type when the version changes
        /// </summary>
        /// <remarks>B Encoding is only used by vCard 3.0.  All other specifications use Base 64 Encoding.</remarks>
        public override SpecificationVersions Version
        {
            get => base.Version;
            set
            {
                base.Version = value;

                if(value == SpecificationVersions.vCard30)
                {
                    if(this.EncodingMethod == EncodingType.Base64)
                        this.EncodingMethod = EncodingType.BEncoding;
                }
                else
                {
                    if(this.EncodingMethod == EncodingType.BEncoding)
                        this.EncodingMethod = EncodingType.Base64;
                }
            }
        }

        /// <summary>
        /// The group to which this property belongs
        /// </summary>
        /// <remarks>vCard properties support grouping.  If grouped, this property will contain the name of the
        /// group with which it is associated.  This property is ignored for vCalendar and iCalendar
        /// properties.</remarks>
        public string? Group { get; set; }

        /// <summary>
        /// Set or get the encoding method for this property's value as a string
        /// </summary>
        /// <value>This defines how the value of the property is encoded.  If not set, it defaults to 7-bit ASCII
        /// encoding.  The value passed to the property is not case-sensitive.</value>
        /// <exception cref="ArgumentException">This is thrown if the value is not set to 7BIT, 8BIT,
        /// QUOTED-PRINTABLE, BASE64, B, or a value starting with "X-" to indicate a custom type.</exception>
        public string EncodingString
        {
            get => encMethod;
            set
            {
                if(value == null || value.Length == 0)
                    encMethod = EncodingValue.SevenBit;
                else
                    encMethod = value.Trim().ToUpperInvariant();

                switch(encMethod)
                {
                    case EncodingValue.SevenBit:
                        encType = EncodingType.SevenBit;
                        break;

                    case EncodingValue.EightBit:
                        encType = EncodingType.EightBit;
                        break;

                    case EncodingValue.QuotedPrintable:
                        encType = EncodingType.QuotedPrintable;
                        break;

                    case EncodingValue.Base64:
                        encType = EncodingType.Base64;
                        break;

                    case EncodingValue.BEncoding:
                        encType = EncodingType.BEncoding;
                        break;

                    default:
                        encType = EncodingType.Custom;

                        if(encMethod.Substring(0, 2) != "X-")
                            throw new ArgumentException(LR.GetString("ExBPBadEncodingValue"));
                        break;
                }
            }
        }

        /// <summary>
        /// Set or get the encoding method for this property's value as a value in the <see cref="EncodingType"/>
        /// enumeration.
        /// </summary>
        /// <value>This defines how the value of the property is encoded.  If not set, it defaults to 7-bit ASCII
        /// encoding.</value>
        public EncodingType EncodingMethod
        {
            get => encType;
            set
            {
                encType = value;

                switch(value)
                {
                    case EncodingType.SevenBit:
                        encMethod = EncodingValue.SevenBit;
                        break;

                    case EncodingType.EightBit:
                        encMethod = EncodingValue.EightBit;
                        break;

                    case EncodingType.QuotedPrintable:
                        encMethod = EncodingValue.QuotedPrintable;
                        break;

                    case EncodingType.Base64:
                        encMethod = EncodingValue.Base64;
                        break;

                    case EncodingType.BEncoding:
                        encMethod = EncodingValue.BEncoding;
                        break;

                    default:
                        // You should really use EncodingString for custom
                        encMethod = "X-EWSOFTWARE-UNKNOWN";
                        break;
                }
            }
        }

        /// <summary>
        /// This is used to set or get the character set used for the value
        /// </summary>
        /// <value><para>If not set, it is assumed to be ASCII.  The value should be a character set string as
        /// defined in Section 7.1 of RFC 1521 but the class does not enforce this through validation.</para>
        /// 
        /// <para>This property is only applicable to properties in objects conforming to the vCard 2.1 or
        /// vCalendar 1.0 specification.  It is ignored by all other specifications.</para></value>
        public string CharacterSet
        {
            get => charSet;
            set
            {
                if(value == null || value.Length == 0)
                    charSet = CharSetValue.ASCII;
                else
                    charSet = value;
            }
        }

        /// <summary>
        /// This is used to set or get the language used for the value
        /// </summary>
        /// <value>If not set, it is assumed to be en-US (US English).  The value should be a language string as
        /// defined in RFC 1766 but the class does not enforce this through validation.</value>
        public string Language
        {
            get => language;
            set
            {
                if(!String.IsNullOrWhiteSpace(value))
                    language = value;
                else
                    language = LanguageValue.USEnglish;
            }
        }

        /// <summary>
        /// This is used to get or set the property ID for the value
        /// </summary>
        /// <remarks>This is only valid for vCard 4.0 objects.  There may be a single value or multiple values
        /// separated by commas.  It is up to the user to decode and make use of these values.</remarks>
        public string? PropertyId { get; set; }

        /// <summary>
        /// The value (data) type or location of this property's value
        /// </summary>
        /// <remarks><para>If not set, it uses the type or location in the <see cref="DefaultValueLocation"/>
        /// property and the <see cref="Value"/> property refers to the actual value.  If set to something other
        /// than a data type, the <see cref="Value"/> property contains a pointer to the location of the actual
        /// value (i.e. a URL, content ID, etc).  The value passed to the property is not case-sensitive.</para>
        /// 
        /// <para>If necessary, the encoding method is reset to an appropriate setting based on the new
        /// location/type (i.e. Base64 for binary, 7-bit for non-binary data, etc).</para></remarks>
        public string? ValueLocation
        {
            get => location ?? this.DefaultValueLocation;
            set
            {
                if(value == null || value.Length == 0)
                    location = null;
                else
                    location = value.Trim().ToUpperInvariant();

                // Adjust the encoding method if necessary
                if(encType == EncodingType.Base64 || encType == EncodingType.BEncoding)
                {
                    if(location != ValLocValue.Binary)
                        this.EncodingMethod = EncodingType.SevenBit;
                }
                else
                    if(location == ValLocValue.Binary)
                        this.EncodingMethod = EncodingType.Base64;
            }
        }

        /// <summary>
        /// This property is used to set or get a string containing custom parameters that are not part of the
        /// specification.  These are usually prefixed with "X-" to indicate an extension.
        /// </summary>
        /// <value>The parameters are returned in a string containing each parameter and its value separated by
        /// semi-colons (i.e. "X-ABC-Custom1=Value;X-ABC-Custom2=3".  It is up to the caller to determine what to
        /// do with them.  It can be overridden in derived classes to alter its behavior.</value>
        public virtual string? CustomParameters { get; set; }

        /// <summary>
        /// This is used to set or get the value of the property in its unencoded string form
        /// </summary>
        /// <remarks><para>If the <see cref="ValueLocation"/> property is set to <c>INLINE</c> or a data type,
        /// this property contains the actual value in string form.  If it is set to some other value, this
        /// property contains a pointer to the actual value (i.e. a URL, content ID, etc).  In such cases, the
        /// value is probably not encoded.</para>
        /// 
        /// <para>This property can be used to set or get the property value in its decoded form.  For setting or
        /// getting the value in its encoded form, use the <see cref="EncodedValue"/> property instead.</para>
        /// 
        /// <para>Derived classes can override this to parse the value and provide access to it via type-specific
        /// value properties.  If this property is overridden, override <c>EncodedValue</c> as well to ensure the
        /// value is stored correctly in either form.</para></remarks>
        public virtual string? Value
        {
            get => propValue;
            set => propValue = value;
        }

        /// <summary>
        /// This is used to set or get the value of the property in its encoded string form representing how it
        /// will be read from or written to the PDI data stream (i.e. Base 64, Quoted-Printable, etc).
        /// </summary>
        /// <value>This method is always used when parsing a PDI data stream to set the value of a property.  It
        /// is also used when returning the value for output to a PDI data stream.</value>
        /// <remarks><para>If the <see cref="ValueLocation"/> property is set to <c>INLINE</c> or a data type,
        /// this property contains the actual value in encoded string form.  If it is set to some other value,
        /// this property contains a pointer to the actual value (i.e. a URL, content ID, etc).  In such cases,
        /// the value is probably not encoded.</para>
        /// 
        /// <para>Derived classes can override this property to convert the string to the actual data that they
        /// represent when set using data from a PDI data stream and to convert their data to a string when
        /// retrieved for writing to a PDI data stream (i.e. an image or sound).  If not overridden, the data is
        /// stored as a string that can be accessed via the <see cref="Value"/> property.</para>
        /// 
        /// <para>If this property is overridden, override <c>Value</c> as well to ensure the value is stored
        /// correctly in either form.</para></remarks>
        public virtual string? EncodedValue
        {
            get
            {
                if(propValue == null || propValue.Length == 0)
                    return propValue;

                return this.Encode(propValue);
            }
            set => propValue = this.Decode(value);
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor.  By default, it uses 7-bit ASCII encoding for the value, and the value type or location
        /// is determined from the <see cref="DefaultValueLocation"/> property.
        /// </summary>
        protected BaseProperty()
        {
            this.CharacterSet = null!;
            this.Language = null!;
            this.EncodingString = null!;
            this.ValueLocation = null;
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// A simple helper method that will return <c>String.Empty</c> if the specified string value is null.
        /// If not null, it returns the string value.
        /// </summary>
        /// <param name="propertyValue">The property value string to check</param>
        /// <returns>The property value if not null or <c>String.Empty</c> if the value is null</returns>
        /// <remarks>This may be useful if you would prefer not to have to always check for null values in
        /// string-type properties before using a string method that may throw an exception if it is null (i.e.
        /// <c>Length</c>).</remarks>
        /// <example>
        /// <code language="cs">
        /// string country = BaseProperty.ValueOrStringEmpty(vCard.Address.Country);
        ///
        /// // No need to check for null first
        /// if(country.Length != 0)
        ///     // ... Do something with the country value ...
        /// </code>
        /// <code language="vbnet">
        /// Dim country As String = BaseProperty.ValueOrStringEmpty(vCard.Address.Country)
        ///
        /// ' No need to check for null first
        /// If country.Length &lt;&gt; 0 Then
        ///     ' ... Do something with the country value ...
        /// End If
        /// </code>
        /// </example>
        public static string ValueOrStringEmpty(string propertyValue)
        {
            return (propertyValue ?? String.Empty);
        }

        /// <summary>
        /// This is overridden to allow copying values from the specified PDI object into the instance
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        /// <remarks>Derived classes must call this method to copy the standard PDI object properties.  It only
        /// needs to be overridden if the derived class has additional properties.</remarks>
        protected override void Clone(PDIObject p)
        {
            BaseProperty o = (BaseProperty)p;

            if(o.Version != SpecificationVersions.None)
                this.Version = o.Version;

            this.Group = o.Group;
            this.EncodingMethod = o.EncodingMethod;
            this.CharacterSet = o.CharacterSet;
            this.Language = o.Language;
            this.PropertyId = o.PropertyId;
            this.ValueLocation = o.ValueLocation;
            this.CustomParameters = o.CustomParameters;
            this.Value = o.Value;
        }

        /// <summary>
        /// This is a helper method that converts a property to its string form and writes it to the given text
        /// writer.
        /// </summary>
        /// <param name="prop">The property to use.</param>
        /// <param name="sb"><para>A <see cref="System.Text.StringBuilder"/> used by the property to convert
        /// itself to a string.  This is a shared instance used by all properties written to the text writer.  It
        /// is cleared before the specified property is converted.</para>
        /// 
        /// <para>It can be null if the TextWriter is a <see cref="System.IO.StringWriter"/>.  In that case, the
        /// property will append itself directly to its StringBuilder instead.</para></param>
        /// <param name="tw">A <see cref="System.IO.TextWriter"/> to which the string form is written.</param>
        /// <remarks>Properties use a StringBuilder to convert themselves to a string rather than writing
        /// directly to the TextWriter as they may need to manipulate the string to fold lines, etc.</remarks>
        /// <exception cref="InvalidCastException">This is thrown if the StringBuilder parameter is null and the
        /// TextWriter is not a StringWriter.</exception>
        public static void WriteToStream(BaseProperty? prop, StringBuilder? sb, TextWriter tw)
        {
            if(prop != null)
            {
                if(sb != null)
                {
                    sb.Length = 0;
                    prop.ToString(sb);

                    if(sb.Length != 0)
                        tw.Write(sb.ToString());
                }
                else
                    prop.ToString(((StringWriter)tw).GetStringBuilder());
            }
        }

        /// <summary>
        /// This is overridden to allow proper comparison of property objects
        /// </summary>
        /// <param name="obj">The object to which this instance is compared</param>
        /// <returns>Returns true if the object equals this instance, false if it does not</returns>
        public override bool Equals(object obj)
        {
            if(obj is not BaseProperty bp)
                return false;

            // The ToString() method returns a text representation of the property based on all of its settings
            // so it's a reliable way to tell if two instances are the same.
            return this == bp || this.ToString() == bp.ToString();
        }

        /// <summary>
        /// Get a hash code for the property object
        /// </summary>
        /// <remarks>Since the ToString() method returns a text representation based on all of the settings, this
        /// returns the hash code for the string returned by it.</remarks>
        /// <returns>Returns the hash code for the property object</returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// This is overridden to convert the instance to a string
        /// </summary>
        /// <returns>The property information as a string</returns>
        /// <remarks>Derived classes cannot override this method.  Instead, they should override the various
        /// <c>SerializeXXXX</c> methods and the <see cref="Value"/> and <see cref="EncodedValue"/> properties
        /// that control the formatting of the property parameters and the value that will get written to the PDI
        /// data stream.</remarks>
        /// <overloads>There are two overloads for this method</overloads>
        public sealed override string ToString()
        {
            StringBuilder sb = new(256);

            // Let the derived class format the rest of the property
            this.ToString(sb);

            return sb.ToString();
        }

        /// <summary>
        /// This is used to convert the property instance to a format suitable for writing to a PDI data stream
        /// </summary>
        /// <param name="sb">The <see cref="System.Text.StringBuilder"/> to which the formatted text is appended</param>
        /// <remarks><para>This version is called by <see cref="ToString()"/> as well as owning objects when they
        /// convert themselves to a string for writing to a PDI data stream.  If the value of the property is
        /// null or an empty string, it will not append itself as there is nothing to specify.</para>
        /// 
        /// <para>Properties use a StringBuilder to convert themselves to a string rather than writing directly
        /// to the TextWriter as they may need to manipulate the string to fold lines, etc.</para></remarks>
        public void ToString(StringBuilder sb)
        {
            int priorLength, length;

            // If the value is null or empty, don't add it
            string? encVal = this.EncodedValue;

            if(encVal == null || encVal.Length == 0)
                return;

            priorLength = sb.Length;

            // Prefix the property with the group name if there is one
            if(this.Group != null && this.Group.Length != 0)
            {
                sb.Append(this.Group);
                sb.Append('.');
            }

            // Append the property's tag name
            sb.Append(this.Tag);

            // Append any parameters
            this.SerializeParameters(sb);

            // Append the value followed by a carriage return and line feed.  If using quoted printable or binary
            // encoding, the lines are already folded if they are too long so start the value on a new line if
            // that is the case to stop it folding the first line unnecessarily.
            sb.Append(':');

            if(this.EncodingMethod == EncodingType.QuotedPrintable && encVal.Length + (sb.Length - priorLength) > 75)
                sb.Append("=\r\n");
            else
                if(encVal.Length > 75 && (this.EncodingMethod == EncodingType.Base64 ||
                  this.EncodingMethod == EncodingType.BEncoding))
                {
                    sb.Append("\r\n ");
                }

            sb.Append(encVal);
            sb.Append("\r\n");

            // Insert line folds?
            if(sb.Length - priorLength > 75)
            {
                length = 1;

                while(priorLength < sb.Length)
                {
                    if(sb[priorLength] == '\r' || sb[priorLength] == '\n')
                        length = 1;
                    else
                        length++;

                    priorLength++;

                    if(length == 76 && priorLength < sb.Length && sb[priorLength] != '\r' && sb[priorLength] != '\n')
                    {
                        // Find the first non-whitespace character that isn't '=' (possible quoted-printable) and
                        // insert the break there.
                        do
                        {
                            length--;
                            priorLength--;

                        } while(length > 0 && (Char.IsWhiteSpace(sb[priorLength]) || sb[priorLength] == '='));

                        if(length > 0)
                        {
                            length = 1;
                            sb.Insert(priorLength + 1, "\r\n ");
                            priorLength += 3;
                        }
                        else // Couldn't find a non-whitespace char, give up
                            priorLength += 75;
                    }
                }
            }
        }

        /// <summary>
        /// This method can be overridden to customize how the parameters are appended to the property when being
        /// serialized to a PDI data stream.
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        /// <remarks>The individual serialize parameter methods can also be overridden for finer control over the
        /// parameters.  If overridden, this method should still call the other parameter serialization methods
        /// unless you want to exclude a particular parameter.</remarks>
        public virtual void SerializeParameters(StringBuilder sb)
        {
            this.SerializeEncoding(sb);
            this.SerializeCharacterSet(sb);
            this.SerializeLanguage(sb);
            this.SerializeValueLocation(sb);
            this.SerializePropertyId(sb);
            this.SerializeCustomParameters(sb);
        }

        /// <summary>
        /// This is called to serialize the ENCODING parameter if necessary
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        /// <remarks>The method should append a semi-colon followed by the parameter name, an equal sign, and the
        /// parameter value.</remarks>
        public virtual void SerializeEncoding(StringBuilder sb)
        {
            if(this.EncodingMethod != EncodingType.SevenBit && this.EncodingMethod != EncodingType.EightBit &&
              (this.ValueLocation == ValLocValue.Inline || this.ValueLocation == ValLocValue.Text ||
              this.ValueLocation == ValLocValue.Binary) && this.Version != SpecificationVersions.vCard40)
            {
                sb.Append(';');
                sb.Append(ParameterNames.Encoding);
                sb.Append('=');
                sb.Append(this.EncodingString);
            }
        }

        /// <summary>
        /// This is called to serialize the CHARSET parameter if necessary
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        /// <remarks>The method should append a semi-colon followed by the parameter name, an equal sign, and the
        /// parameter value.  This parameter is only applicable to vCard 2.1 and vCalendar 1.0 property
        /// objects.</remarks>
        public virtual void SerializeCharacterSet(StringBuilder sb)
        {
            if((this.Version == SpecificationVersions.vCard21 || this.Version == SpecificationVersions.vCalendar10) &&
              String.Compare(this.CharacterSet, CharSetValue.ASCII, StringComparison.OrdinalIgnoreCase) != 0)
            {
                sb.Append(';');
                sb.Append(ParameterNames.CharacterSet);
                sb.Append('=');
                sb.Append(this.CharacterSet);
            }
        }

        /// <summary>
        /// This is called to serialize the LANGUAGE parameter if necessary
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        /// <remarks>The method should append a semi-colon followed by the parameter name, an equal sign, and the
        /// parameter value.</remarks>
        public virtual void SerializeLanguage(StringBuilder sb)
        {
            if(String.Compare(this.Language, LanguageValue.USEnglish, StringComparison.OrdinalIgnoreCase) != 0)
            {
                sb.Append(';');
                sb.Append(ParameterNames.Language);
                sb.Append('=');
                sb.Append(this.Language);
            }
        }

        /// <summary>
        /// This is called to serialize the value location parameter if necessary
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        /// <remarks>The method should append a semi-colon followed by the parameter name, an equal sign, and the
        /// parameter value.</remarks>
        public virtual void SerializeValueLocation(StringBuilder sb)
        {
            if(this.ValueLocation != this.DefaultValueLocation && this.ValueLocation != ValLocValue.Inline)
            {
                sb.Append(';');
                sb.Append(ParameterNames.ValueLocation);
                sb.Append('=');
                sb.Append(this.ValueLocation);
            }
        }

        /// <summary>
        /// This is called to serialize the PID parameter if necessary
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        /// <remarks>The method should append a semi-colon followed by the parameter name, an equal sign, and the
        /// parameter value.  This parameter is only applicable to vCard 4.0 objects.</remarks>
        public virtual void SerializePropertyId(StringBuilder sb)
        {
            if(this.Version == SpecificationVersions.vCard40 && !String.IsNullOrWhiteSpace(this.PropertyId))
            {
                sb.Append(';');
                sb.Append(ParameterNames.PropertyId);
                sb.Append('=');
                sb.Append(this.PropertyId);
            }
        }

        /// <summary>
        /// This is called to serialize any custom parameters if necessary
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        /// <remarks>The method should append a semi-colon followed by the custom parameters in the correct
        /// format (i.e. ";X-ABC-Custom1=Test;X-ABC-Custom2=3".</remarks>
        public virtual void SerializeCustomParameters(StringBuilder sb)
        {
            if(this.CustomParameters != null && this.CustomParameters.Length > 0)
            {
                sb.Append(';');
                sb.Append(this.CustomParameters);
            }
        }

        /// <summary>
        /// This is used to deserialize parameter values from a string collection
        /// </summary>
        /// <param name="parameters">The parameters for the property</param>
        /// <remarks><para>The base class will extract and set the value of the following parameters.  Unless a
        /// derived class wants to handle them itself, they can be ignored and this base class version called to
        /// process them.  Any parameters that are handled should be removed from the collection so that they do
        /// not end up in the custom parameters property if the base class does not recognize them.</para>
        /// 
        /// <para>Depending on the object for which data is being parsed, there may be a mixture of name/value
        /// pairs or values alone in the parameters string collection.  It is up to the derived class to process
        /// the parameter list based on the specification to which it conforms.  For entries that are parameter
        /// names, the entry will end with an equals sign (=) and the entry immediately following it in the
        /// collection is its associated parameter value.  Entries that do not end in an equals sign are assumed
        /// to be values alone.  The property name, parameter names, and their values may be in upper, lower, or
        /// mixed case.</para>
        /// 
        /// <para>The default parameters handled include:</para>
        /// 
        /// <list type="table">
        ///     <listheader>
        ///         <term>Parameter</term>
        ///         <description>Description</description>
        ///     </listheader>
        ///     <item>
        ///         <term>Encoding</term>
        ///         <description>The encoding type used on the property value</description>
        ///     </item>
        ///     <item>
        ///         <term>Character Set</term>
        ///         <description>The character set for the property value</description>
        ///     </item>
        ///     <item>
        ///         <term>Language</term>
        ///         <description>The language used for the value</description>
        ///     </item>
        ///     <item>
        ///         <term>Value (Location)</term>
        ///         <description>The location/data type of the property value</description>
        ///     </item>
        ///     <item>
        ///         <term>Property ID (PID)</term>
        ///         <description>The property ID for the property used to match properties across instances</description>
        ///     </item>
        ///     <item>
        ///         <term>Custom parameters</term>
        ///         <description>Any unrecognized parameters will be stored as a string of custom parameters.  If
        /// a derived class handles a parameter, it should remove it from the collection.</description>
        ///     </item>
        /// </list>
        /// </remarks>
        public virtual void DeserializeParameters(StringCollection parameters)
        {
            int idx;

            if(parameters == null || parameters.Count == 0)
                return;

            StringBuilder sb = new(80);

            for(int paramIdx = 0; paramIdx < parameters.Count; paramIdx++)
            {
                // The last entry is always Custom so scan for length minus one
                for(idx = 0; idx < ntv.Length - 1; idx++)
                    if(ntv[idx].IsMatch(parameters[paramIdx]))
                        break;

                // Parameters may appear as a pair (name followed by value) or by value alone
                switch(ntv[idx].EnumValue)
                {
                    case ParameterType.Encoding:
                        if(!ntv[idx].IsParameterValue)
                            paramIdx++;

                        if(paramIdx < parameters.Count)
                            this.EncodingString = parameters[paramIdx];
                        break;

                    case ParameterType.CharacterSet:
                        if(!ntv[idx].IsParameterValue)
                            paramIdx++;

                        if(paramIdx < parameters.Count)
                            this.CharacterSet = parameters[paramIdx];
                        break;

                    case ParameterType.Language:
                        if(!ntv[idx].IsParameterValue)
                            paramIdx++;

                        if(paramIdx < parameters.Count)
                            this.Language = parameters[paramIdx];
                        break;

                    case ParameterType.ValueLocation:
                        if(!ntv[idx].IsParameterValue)
                            paramIdx++;

                        if(paramIdx < parameters.Count)
                            this.ValueLocation = parameters[paramIdx];
                        break;

                    case ParameterType.PropertyId:
                        if(!ntv[idx].IsParameterValue)
                            paramIdx++;

                        if(paramIdx < parameters.Count)
                            this.PropertyId = parameters[paramIdx];
                        break;

                    default:
                        // Unknown
                        if(sb.Length > 0)
                            sb.Append(';');

                        // Append the parameter name/value.  If it ends with '=' it's a parameter name and the
                        // value should follow it.
                        if(paramIdx + 1 < parameters.Count && parameters[paramIdx].EndsWith("=", StringComparison.Ordinal))
                        {
                            sb.Append(parameters[paramIdx]);
                            paramIdx++;
                        }

                        // If it contains a semi-colon, colon, or comma, enclose the value in quotes
                        if(parameters[paramIdx].IndexOfAny([',', ';', ':']) != -1)
                        {
                            sb.Append('\"');
                            sb.Append(parameters[paramIdx]);
                            sb.Append('\"');
                        }
                        else
                            sb.Append(parameters[paramIdx]);
                        break;
                }
            }

            // Store any unknown stuff in the CustomParameters property
            if(sb.Length > 0)
                this.CustomParameters = sb.ToString();
        }

        /// <summary>
        /// This method is used to encode a value for output to a PDI data stream
        /// </summary>
        /// <param name="data">The data string to encode</param>
        /// <returns>The encoded data</returns>
        /// <remarks><para>By default, it can handle Base 64 and Quoted-Printable encoding.  For 7-bit and 8-bit
        /// encoding, it will escape characters as needed.  The method used depends on the current setting of the
        /// <see cref="Encoding"/> property.  It can be overridden to support other types of encoding.</para>
        /// 
        /// <para>For vCard 2.1 and vCalendar 1.0 properties, if a <see cref="CharacterSet"/> parameter has been
        /// specified, the value is converted from the one specified by the <see cref="DefaultEncoding"/>
        /// property to the one specified by the <c>CharacterSet</c> property before encoding it.</para></remarks>
        public virtual string? Encode(string? data)
        {
            string? encoded;

            // It's only done if the value is inline, text, or binary
            if(this.ValueLocation != ValLocValue.Inline && this.ValueLocation != ValLocValue.Text &&
              this.ValueLocation != ValLocValue.Binary)
            {
                return data;
            }

            if((this.Version == SpecificationVersions.vCard21 || this.Version == SpecificationVersions.vCalendar10) &&
              String.Compare(this.CharacterSet, CharSetValue.ASCII, StringComparison.OrdinalIgnoreCase) != 0)
            {
                Encoding destEnc = Encoding.GetEncoding(this.CharacterSet);
                byte[] destBytes = Encoding.Convert(DefaultEncoding, destEnc, DefaultEncoding.GetBytes(data));

                // Use the literal bytes.  If we get the string, the encoder gives us the decoded values which is
                // not what we want to write out.
                StringBuilder sb = new(destBytes.Length);

                for(int nIdx = 0; nIdx < destBytes.Length; nIdx++)
                    sb.Append((char)destBytes[nIdx]);

                data = sb.ToString();
            }

            switch(this.EncodingMethod)
            {
                case EncodingType.SevenBit:
                case EncodingType.EightBit:
                    // Escape characters for these if necessary.  vCard 2.1 and vCalendar 1.0 do not escape
                    // commas and semi-colons.
                    if(this.Version == SpecificationVersions.vCard21 || this.Version == SpecificationVersions.vCalendar10)
                        encoded = EncodingUtils.RestrictedEscape(data)!;
                    else
                        encoded = EncodingUtils.Escape(data)!;
                    break;

                case EncodingType.QuotedPrintable:
                    encoded = EncodingUtils.ToQuotedPrintable(data, 75)!;
                    break;

                case EncodingType.Base64:
                case EncodingType.BEncoding:
                    // Base 64 encoding
                    encoded = EncodingUtils.ToBase64(data, 75, this.EncodingMethod == EncodingType.Base64)!;
                    break;

                default:    // Custom, can't do anything with it
                    encoded = data;
                    break;
            }

            return encoded;
        }

        /// <summary>
        /// This method is used to decode a value read in from a PDI data stream
        /// </summary>
        /// <param name="data">The data string to decode</param>
        /// <returns>The decoded data</returns>
        /// <remarks><para>By default, it can handle Base 64 and Quoted-Printable decoding.  For 7-bit and 8-bit
        /// decoding, it will unescape characters where necessary.  The method used depends on the current
        /// setting of the <see cref="Encoding"/> property.  It can be overridden to support other types of
        /// decoding.</para>
        /// 
        /// <para>For vCard 2.1 and vCalendar 1.0 properties, if a <see cref="CharacterSet"/> parameter has been
        /// specified, the value is converted from the specified character set to the one specified by the
        /// <see cref="DefaultEncoding"/> property after decoding.</para></remarks>
        public virtual string? Decode(string? data)
        {
            string? decoded;

            // It's only done if the value is inline, text, or binary
            if(this.ValueLocation != ValLocValue.Inline && this.ValueLocation != ValLocValue.Text &&
              this.ValueLocation != ValLocValue.Binary)
            {
                return data;
            }

            switch(this.EncodingMethod)
            {
                case EncodingType.SevenBit:
                case EncodingType.EightBit:
                    // Unescape characters for these if necessary
                    decoded = EncodingUtils.Unescape(data);
                    break;

                case EncodingType.QuotedPrintable:
                    decoded = EncodingUtils.FromQuotedPrintable(data);
                    break;

                case EncodingType.Base64:
                case EncodingType.BEncoding:
                    // Base 64 decoding
                    decoded = EncodingUtils.FromBase64(data);
                    break;

                default:    // Custom, can't do anything with it
                    decoded = data;
                    break;
            }

            if((this.Version == SpecificationVersions.vCard21 || this.Version == SpecificationVersions.vCalendar10) &&
              String.Compare(this.CharacterSet, CharSetValue.ASCII, StringComparison.OrdinalIgnoreCase) != 0)
            {
                // The string is already encoded.  We just want the bytes in array form
                Encoding enc = Encoding.GetEncoding("iso-8859-1");
                byte[] srcBytes = enc.GetBytes(decoded);

                decoded = DefaultEncoding.GetString(Encoding.Convert(Encoding.GetEncoding(this.CharacterSet),
                    DefaultEncoding, srcBytes));
            }

            return decoded;
        }
        #endregion
    }
}
