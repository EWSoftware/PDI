//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : VNote.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2013-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the definition for the vNote object
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 08/19/2007  EFW  Created the code
//===============================================================================================================

// Ignore Spelling: vn sw

using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

using EWSoftware.PDI.Binding;
using EWSoftware.PDI.Parser;
using EWSoftware.PDI.Properties;

namespace EWSoftware.PDI.Objects
{
    /// <summary>
    /// This class is used to represent a vNote object.  This serves as an electronic note that can be
    /// transmitted between devices.
    /// </summary>
    [Serializable, TypeDescriptionProvider(typeof(VNoteTypeDescriptionProvider))]
    public class VNote : PDIObject, ISerializable
    {
        #region Private data members
        //=====================================================================

        // Single vNote properties
        private UniqueIdProperty uid;
        private SummaryProperty summary;
        private BodyProperty body;
        private CategoriesProperty categories;
        private ClassificationProperty classification;
        private DateCreatedProperty dateCreated;
        private LastModifiedProperty lastModified;

        // This is a catch-all that holds all unknown or extension properties
        private CustomPropertyCollection customProps;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports IrMC 1.1 only</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.IrMC11;

        /// <summary>
        /// This is overridden to propagate the version to all properties in the object that need it when the
        /// version is set on the vNote.
        /// </summary>
        public override SpecificationVersions Version
        {
            get => base.Version;
            set
            {
                base.Version = value;
                PropagateVersion();
            }
        }

        /// <summary>
        /// This is used to get the Unique ID (X-IRMC_LUID) property
        /// </summary>
        /// <remarks>If a unique ID has not been assigned, one will be created</remarks>
        public UniqueIdProperty UniqueId
        {
            get
            {
                if(uid == null)
                {
                    uid = new UniqueIdProperty();
                    uid.AssignNewId(true);
                }

                return uid;
            }
        }

        /// <summary>
        /// This is used to get the Summary (SUMMARY) property
        /// </summary>
        public SummaryProperty Summary
        {
            get
            {
                if(summary == null)
                    summary = new SummaryProperty();

                return summary;
            }
        }

        /// <summary>
        /// This is used to get the Body (BODY) property
        /// </summary>
        public BodyProperty Body
        {
            get
            {
                if(body == null)
                    body = new BodyProperty();

                return body;
            }
        }

        /// <summary>
        /// This is used to get the Categories (CATEGORIES) property
        /// </summary>
        public CategoriesProperty Categories
        {
            get
            {
                if(categories == null)
                    categories = new CategoriesProperty();

                return categories;
            }
        }

        /// <summary>
        /// This is used to get the Classification (CLASS) property
        /// </summary>
        public ClassificationProperty Classification
        {
            get
            {
                if(classification == null)
                    classification = new ClassificationProperty();

                return classification;
            }
        }

        /// <summary>
        /// This is used to get the Date Created  (DCREATED) property
        /// </summary>
        public DateCreatedProperty DateCreated
        {
            get
            {
                if(dateCreated == null)
                    dateCreated = new DateCreatedProperty();

                return dateCreated;
            }
        }

        /// <summary>
        /// This is used to get the Last Modified (LAST-MODIFIED) property
        /// </summary>
        public LastModifiedProperty LastModified
        {
            get
            {
                if(lastModified == null)
                    lastModified = new LastModifiedProperty();

                return lastModified;
            }
        }

        /// <summary>
        /// This is a catch-all that holds all unknown or extension properties
        /// </summary>
        /// <value>If the returned collection is empty, there are no custom properties for the vNote</value>
        public CustomPropertyCollection CustomProperties
        {
            get
            {
                if(customProps == null)
                    customProps = new CustomPropertyCollection();

                return customProps;
            }
        }
        #endregion

        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public VNote()
        {
            this.Version = SpecificationVersions.IrMC11;
        }

        /// <summary>
        /// Deserialization constructor for use with <see cref="System.Runtime.Serialization.ISerializable"/>
        /// </summary>
        /// <param name="info">The serialization info object</param>
        /// <param name="context">The streaming context object</param>
        protected VNote(SerializationInfo info, StreamingContext context) : this()
        {
            if(info != null)
            {
                string vNote = (string)info.GetValue("VNOTE", typeof(string));

                // Parse the vNote information from the string
                VNoteParser.ParseFromString(vNote, this);
            }
        }
        #endregion

        #region ISerializable implementation
        //=====================================================================

        /// <summary>
        /// This implements the <see cref="System.Runtime.Serialization.ISerializable"/> interface and adds the
        /// appropriate members to the serialization info based on the vNote settings.
        /// </summary>
        /// <param name="info">The serialization info object</param>
        /// <param name="context">The streaming context</param>
        [SecurityCritical]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if(info != null)
                info.AddValue("VNOTE", this.ToString());
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
            VNote o = new VNote();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            VNote o = (VNote)p;

            this.ClearProperties();

            dateCreated = (DateCreatedProperty)o.DateCreated.Clone();
            lastModified = (LastModifiedProperty)o.LastModified.Clone();
            summary = (SummaryProperty)o.Summary.Clone();
            body = (BodyProperty)o.Body.Clone();
            classification = (ClassificationProperty)o.Classification.Clone();
            categories = (CategoriesProperty)o.Categories.Clone();
            uid = (UniqueIdProperty)o.UniqueId.Clone();

            this.CustomProperties.CloneRange(o.CustomProperties);
        }

        /// <summary>
        /// The method can be called to clear all current property values from the vNote.  The version is left
        /// unchanged.
        /// </summary>
        public void ClearProperties()
        {
            uid = null;
            summary = null;
            body = null;
            classification = null;
            categories = null;
            dateCreated = null;
            lastModified = null;

            customProps = null;
        }

        /// <summary>
        /// This is used to propagate the version to all properties in the object that need it
        /// </summary>
        public void PropagateVersion()
        {
            if(uid != null)
                uid.Version = this.Version;

            if(summary != null)
                summary.Version = this.Version;

            if(body != null)
                body.Version = this.Version;

            if(classification != null)
                classification.Version = this.Version;

            if(categories != null)
                categories.Version = this.Version;

            if(dateCreated != null)
                dateCreated.Version = this.Version;

            if(lastModified != null)
                lastModified.Version = this.Version;

            if(customProps != null)
                customProps.PropagateVersion(this.Version);
        }

        /// <summary>
        /// This is overridden to allow proper comparison of vNote objects
        /// </summary>
        /// <param name="obj">The object to which this instance is compared</param>
        /// <returns>Returns true if the object equals this instance, false if it does not</returns>
        public override bool Equals(object obj)
        {
            if(!(obj is VNote vn))
                return false;

            // The ToString() method returns a text representation of the vNote based on all of its settings so
            // it's a reliable way to tell if two instances are the same.
            return (this == vn || this.ToString() == vn.ToString());
        }

        /// <summary>
        /// Get a hash code for the vNote object
        /// </summary>
        /// <returns>Returns the hash code for the vNote object</returns>
        /// <remarks>Since the ToString() method returns a text representation based on all of the settings, this
        /// returns the hash code for the string returned by it.</remarks>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// Convert the vNote instance to its string form
        /// </summary>
        /// <returns>Returns a text description of the vNote suitable for saving to a PDI data stream</returns>
        public override string ToString()
        {
            using(var sw = new StringWriter(new StringBuilder(1024), CultureInfo.InvariantCulture))
            {
                this.WriteToStream(sw, null);
                return sw.ToString();
            }
        }

        /// <summary>
        /// This can be used to write a vNote to a PDI data stream
        /// </summary>
        /// <param name="tw">A <see cref="System.IO.TextWriter"/> derived class to which the vNote is written.</param>
        /// <remarks>This is called by <see cref="ToString"/> as well as owning objects when they convert
        /// themselves to a string or write themselves to a PDI data stream.</remarks>
        /// <overloads>There are two overloads for this method</overloads>
        /// <example>
        /// <code language="cs">
        /// // Create a vNote
        /// VNote vn = new VNote();
        /// vn.Summary.Value = "A brief note";
        /// vn.Body.Value = "This is just a brief note to say hello";
        ///
        /// // Open the file and write the vNote to it
        /// StreamWriter sw = new StreamWriter(@"C:\BriefNote.vnt");
        /// vn.WriteToStream(sw);
        /// sw.Close();
        /// </code>
        /// <code language="vbnet">
        /// ' Create a vNote
        /// Dim vn As New VNote()
        /// vn.Summary.Value = "A brief note"
        /// vn.Body.Value = "This is just a brief note to say hello"
        ///
        /// ' Open the file and write the vNote to it
        /// Dim sw As New StreamWriter("C:\BriefNote.vnt")
        /// vn.WriteToStream(sw)
        /// sw.Close()
        /// </code>
        /// </example>
        public void WriteToStream(TextWriter tw)
        {
            if(tw is StringWriter)
                WriteToStream(tw, null);
            else
                WriteToStream(tw, new StringBuilder(256));
        }

        /// <summary>
        /// This can be used to write a vNote to a PDI data stream
        /// </summary>
        /// <param name="tw">A <see cref="System.IO.TextWriter"/> derived class to which the vNote is written.</param>
        /// <param name="sb">A <see cref="System.Text.StringBuilder"/> used by the properties as a temporary
        /// buffer.  This can be null if the TextWriter is a <see cref="System.IO.StringWriter"/>.</param>
        /// <remarks>This is called by <see cref="ToString"/> as well as owning objects when they convert
        /// themselves to a string or write themselves to a PDI data stream.</remarks>
        public void WriteToStream(TextWriter tw, StringBuilder sb)
        {
            PropagateVersion();

            tw.Write("BEGIN:VNOTE\r\n");
            tw.Write("VERSION:1.1\r\n");

            BaseProperty.WriteToStream(uid, sb, tw);
            BaseProperty.WriteToStream(summary, sb, tw);

            // The BODY property is required, all others are optional.  However, it can be blank so we must force
            // it if it is blank.
            if(body == null || String.IsNullOrWhiteSpace(body.Value))
                tw.Write("BODY:\r\n");
            else
                BaseProperty.WriteToStream(body, sb, tw);

            BaseProperty.WriteToStream(categories, sb, tw);
            BaseProperty.WriteToStream(classification, sb, tw);
            BaseProperty.WriteToStream(dateCreated, sb, tw);
            BaseProperty.WriteToStream(lastModified, sb, tw);

            if(customProps != null && customProps.Count != 0)
                foreach(CustomProperty c in customProps)
                    BaseProperty.WriteToStream(c, sb, tw);

            tw.Write("END:VNOTE\r\n");
        }
        #endregion
    }
}
