//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : VCard.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/06/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the definition for the vCard object and a collection of vCard objects
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
    /// This class is used to represent a vCard object.  This serves as an electronic business card containing
    /// name, address, contact information, etc.
    /// </summary>
    /// <remarks>The <see cref="Version"/> property determines whether it is a vCard 2.1 or vCard 3.0 object</remarks>
    [Serializable, TypeDescriptionProvider(typeof(VCardTypeDescriptionProvider))]
    public class VCard : PDIObject, ISerializable
    {
        #region Private data members
        //=====================================================================

        private string groupName;       // Group name

        // Single vCard properties
        private FormattedNameProperty       fn;
        private NameProperty                name;
        private TitleProperty               title;
        private RoleProperty                role;
        private MailerProperty              mailer;
        private UrlProperty                 url;
        private OrganizationProperty        org;
        private UniqueIdProperty            uid;
        private BirthDateProperty           bday;
        private LastRevisionProperty        rev;
        private TimeZoneProperty            tz;
        private GeographicPositionProperty  geo;
        private PublicKeyProperty           key;
        private PhotoProperty               photo;
        private LogoProperty                logo;
        private SoundProperty               sound;

        // vCard property collections.  There can be one or more of each of these properties so they are stored
        // in a collection.
        private NotePropertyCollection      notes;
        private AddressPropertyCollection   addrs;
        private LabelPropertyCollection     labels;
        private TelephonePropertyCollection phones;
        private EMailPropertyCollection     email;
        private AgentPropertyCollection     agents;

        // This is a catch-all that holds all unknown or extension properties
        private CustomPropertyCollection customProps;

        // These properties are valid for the 3.0 specification only
        private bool addProfile;

        private MimeNameProperty        mimeName;
        private MimeSourceProperty      mimeSource;
        private ProductIdProperty       prodId;
        private NicknameProperty        nickname;
        private SortStringProperty      sortString;
        private ClassificationProperty  classification;
        private CategoriesProperty      categories;
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
        /// This is overridden to propagate the version to all properties in the object that need it when the
        /// version is set on the vCard.
        /// </summary>
        public override SpecificationVersions Version
        {
            get { return base.Version; }
            set
            {
                base.Version = value;
                PropagateVersion();
            }
        }

        /// <summary>
        /// The group to which this vCard belongs
        /// </summary>
        /// <remarks>vCards support grouping.  If grouped, this property will contain the name of the group with
        /// which the vCard is associated.</remarks>
        public string Group
        {
            get { return groupName; }
            set { groupName = value; }
        }

        /// <summary>
        /// This property is used to set or get a flag indicating whether or not the PROFILE:VCARD property
        /// should be output.
        /// </summary>
        /// <value>This property is valid only for the 3.0 specification.  Since it doesn't serve much purpose,
        /// it is false by default.</value>
        public bool AddProfile
        {
            get { return addProfile; }
            set { addProfile = value; }
        }

        /// <summary>
        /// This is used to get the Formatted Name (FN) property
        /// </summary>
        public FormattedNameProperty FormattedName
        {
            get
            {
                if(fn == null)
                    fn = new FormattedNameProperty();

                return fn;
            }
        }

        /// <summary>
        /// This is used to get the Name (N) property
        /// </summary>
        public NameProperty Name
        {
            get
            {
                if(name == null)
                    name = new NameProperty();

                return name;
            }
        }

        /// <summary>
        /// This is used to get the Title (TITLE) property
        /// </summary>
        public TitleProperty Title
        {
            get
            {
                if(title == null)
                    title = new TitleProperty();

                return title;
            }
        }

        /// <summary>
        /// This is used to get the Role (ROLE) property
        /// </summary>
        public RoleProperty Role
        {
            get
            {
                if(role == null)
                    role = new RoleProperty();

                return role;
            }
        }

        /// <summary>
        /// This is used to get the Mailer (MAILER) property
        /// </summary>
        public MailerProperty Mailer
        {
            get
            {
                if(mailer == null)
                    mailer = new MailerProperty();

                return mailer;
            }
        }

        /// <summary>
        /// This is used to get the Uniform Resource Locator (URL) property
        /// </summary>
        public UrlProperty Url
        {
            get
            {
                if(url == null)
                    url = new UrlProperty();

                return url;
            }
        }

        /// <summary>
        /// This is used to get the Organization (ORG) property
        /// </summary>
        public OrganizationProperty Organization
        {
            get
            {
                if(org == null)
                    org = new OrganizationProperty();

                return org;
            }
        }

        /// <summary>
        /// This is used to get the Unique ID (UID) property
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
        /// This is used to get the Birth Date (BDAY) property
        /// </summary>
        public BirthDateProperty BirthDate
        {
            get
            {
                if(bday == null)
                    bday = new BirthDateProperty();

                return bday;
            }
        }

        /// <summary>
        /// This is used to get the Last Revision (REV) property
        /// </summary>
        public LastRevisionProperty LastRevision
        {
            get
            {
                if(rev == null)
                    rev = new LastRevisionProperty();

                return rev;
            }
        }

        /// <summary>
        /// This is used to get the Time Zone (TZ) property
        /// </summary>
        public TimeZoneProperty TimeZone
        {
            get
            {
                if(tz == null)
                    tz = new TimeZoneProperty();

                return tz;
            }
        }

        /// <summary>
        /// This is used to get the geographic position (GEO) property
        /// </summary>
        public GeographicPositionProperty GeographicPosition
        {
            get
            {
                if(geo == null)
                    geo = new GeographicPositionProperty();

                return geo;
            }
        }

        /// <summary>
        /// This is used to get the Public Key (KEY) property
        /// </summary>
        public PublicKeyProperty PublicKey
        {
            get
            {
                if(key == null)
                    key = new PublicKeyProperty();

                return key;
            }
        }

        /// <summary>
        /// This is used to get the Photo (PHOTO) property
        /// </summary>
        public PhotoProperty Photo
        {
            get
            {
                if(photo == null)
                    photo = new PhotoProperty();

                return photo;
            }
        }

        /// <summary>
        /// This is used to get the Logo (LOGO) property
        /// </summary>
        public LogoProperty Logo
        {
            get
            {
                if(logo == null)
                    logo = new LogoProperty();

                return logo;
            }
        }

        /// <summary>
        /// This is used to get the Sound (SOUND) property
        /// </summary>
        public SoundProperty Sound
        {
            get
            {
                if(sound == null)
                    sound = new SoundProperty();

                return sound;
            }
        }

        /// <summary>
        /// This is used to get the Note (NOTE) properties.  There may be more than one if notes are grouped.
        /// </summary>
        /// <value>If the returned collection is empty, there are no note properties for the vCard</value>
        public NotePropertyCollection Notes
        {
            get
            {
                if(notes == null)
                    notes = new NotePropertyCollection();

                return notes;
            }
        }

        /// <summary>
        /// This is used to get the Address (ADR) properties.  There may be more than one.
        /// </summary>
        /// <value>If the returned collection is empty, there are no address properties for the vCard</value>
        public AddressPropertyCollection Addresses
        {
            get
            {
                if(addrs == null)
                    addrs = new AddressPropertyCollection();

                return addrs;
            }
        }

        /// <summary>
        /// This is used to get the Label (LABEL) properties.  There may be more than one.
        /// </summary>
        /// <value>If the returned collection is empty, there are no label properties for the vCard</value>
        public LabelPropertyCollection Labels
        {
            get
            {
                if(labels == null)
                    labels = new LabelPropertyCollection();

                return labels;
            }
        }

        /// <summary>
        /// This is used to get the Telephone (TEL) properties.  There may be more than one.
        /// </summary>
        /// <value>If the returned collection is empty, there are no telephone properties for the vCard</value>
        public TelephonePropertyCollection Telephones
        {
            get
            {
                if(phones == null)
                    phones = new TelephonePropertyCollection();

                return phones;
            }
        }

        /// <summary>
        /// This is used to get the E-Mail (EMAIL) properties.  There may be more than one.
        /// </summary>
        /// <value>If the returned collection is empty, there are no e-mail properties for the vCard</value>
        public EMailPropertyCollection EMailAddresses
        {
            get
            {
                if(email == null)
                    email = new EMailPropertyCollection();

                return email;
            }
        }

        /// <summary>
        /// This is used to get the Agent (AGENT) properties.  There may be more than one agent.
        /// </summary>
        /// <value>If the returned collection is empty, there are no agent properties for the vCard</value>
        public AgentPropertyCollection Agents
        {
            get
            {
                if(agents == null)
                    agents = new AgentPropertyCollection();

                return agents;
            }
        }

        /// <summary>
        /// This is a catch-all that holds all unknown or extension properties
        /// </summary>
        /// <value>If the returned collection is empty, there are no custom properties for the vCard
        /// </value>
        public CustomPropertyCollection CustomProperties
        {
            get
            {
                if(customProps == null)
                    customProps = new CustomPropertyCollection();

                return customProps;
            }
        }

        /// <summary>
        /// This is used to get the MIME Name (NAME) property used to specify the displayable presentation text
        /// associated with the source for the vCard, as specified in the <see cref="MimeSource"/> property.
        /// </summary>
        /// <value>This is for use in specifying a MIME name type and is only valid for the 3.0 specification  Do
        /// not confuse this with the <see cref="FormattedName"/> or the <see cref="Name"/> properties which are
        /// specific to vCards.</value>
        public MimeNameProperty MimeName
        {
            get
            {
                if(mimeName == null)
                    mimeName = new MimeNameProperty();

                return mimeName;
            }
        }

        /// <summary>
        /// This is used to get the MIME Source (SOURCE) property
        /// </summary>
        /// <value>This property is valid only for the 3.0 specification</value>
        public MimeSourceProperty MimeSource
        {
            get
            {
                if(mimeSource == null)
                    mimeSource = new MimeSourceProperty();

                return mimeSource;
            }
        }

        /// <summary>
        /// This is used to get the Product ID (PRODID) property
        /// </summary>
        /// <value>This property is valid only for the 3.0 specification</value>
        public ProductIdProperty ProductId
        {
            get
            {
                if(prodId == null)
                    prodId = new ProductIdProperty();

                return prodId;
            }
        }

        /// <summary>
        /// This is used to get the nickname (NICKNAME) property
        /// </summary>
        /// <value>This property is valid only for the 3.0 specification</value>
        public NicknameProperty Nickname
        {
            get
            {
                if(nickname == null)
                    nickname = new NicknameProperty();

                return nickname;
            }
        }

        /// <summary>
        /// This is used to get the sort string (SORT-STRING) property
        /// </summary>
        /// <value>This property is valid only for the 3.0 specification</value>
        public SortStringProperty SortString
        {
            get
            {
                if(sortString == null)
                    sortString = new SortStringProperty();

                return sortString;
            }
        }

        /// <summary>
        /// This is used to get the access classification (CLASS) property
        /// </summary>
        /// <value>This property is valid only for the 3.0 specification</value>
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
        /// This is used to get the categories (CATEGORIES) property
        /// </summary>
        /// <value>This property is valid only for the 3.0 specification.  It is used to define application
        /// category information for the vCard.</value>
        public CategoriesProperty Categories
        {
            get
            {
                if(categories == null)
                    categories = new CategoriesProperty();

                return categories;
            }
        }
        #endregion

        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor.  Unless the version is changed, the object will conform to the vCard 3.0
        /// specification.
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public VCard()
        {
            this.Version = SpecificationVersions.vCard30;
        }

        /// <summary>
        /// Deserialization constructor for use with <see cref="System.Runtime.Serialization.ISerializable"/>
        /// </summary>
        /// <param name="info">The serialization info object</param>
        /// <param name="context">The streaming context object</param>
        protected VCard(SerializationInfo info, StreamingContext context) : this()
        {
            if(info != null)
            {
                string vCard = (string)info.GetValue("VCARD", typeof(string));

                // Parse the vCard information from the string
                VCardParser.ParseFromString(vCard, this);
            }
        }
        #endregion

        #region ISerializable implementation
        //=====================================================================

        /// <summary>
        /// This implements the <see cref="System.Runtime.Serialization.ISerializable"/> interface and adds the
        /// appropriate members to the serialization info based on the vCard settings.
        /// </summary>
        /// <param name="info">The serialization info object</param>
        /// <param name="context">The streaming context</param>
        [SecurityCritical]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if(info != null)
                info.AddValue("VCARD", this.ToString());
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
            VCard o = new VCard();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            VCard o = (VCard)p;

            this.ClearProperties();

            groupName = o.Group;

            fn = (FormattedNameProperty)o.FormattedName.Clone();
            name = (NameProperty)o.Name.Clone();
            title = (TitleProperty)o.Title.Clone();
            role = (RoleProperty)o.Role.Clone();
            mailer = (MailerProperty)o.Mailer.Clone();
            url = (UrlProperty)o.Url.Clone();
            org = (OrganizationProperty)o.Organization.Clone();
            uid = (UniqueIdProperty)o.UniqueId.Clone();
            bday = (BirthDateProperty)o.BirthDate.Clone();
            rev = (LastRevisionProperty)o.LastRevision.Clone();
            tz = (TimeZoneProperty)o.TimeZone.Clone();
            geo = (GeographicPositionProperty)o.GeographicPosition.Clone();
            key = (PublicKeyProperty)o.PublicKey.Clone();
            photo = (PhotoProperty)o.Photo.Clone();
            logo = (LogoProperty)o.Logo.Clone();
            sound = (SoundProperty)o.Sound.Clone();

            this.Notes.CloneRange(o.Notes);
            this.Addresses.CloneRange(o.Addresses);
            this.Labels.CloneRange(o.Labels);
            this.Telephones.CloneRange(o.Telephones);
            this.EMailAddresses.CloneRange(o.EMailAddresses);
            this.Agents.CloneRange(o.Agents);
            this.CustomProperties.CloneRange(o.CustomProperties);

            addProfile = o.AddProfile;
            mimeName = (MimeNameProperty)o.MimeName.Clone();
            mimeSource = (MimeSourceProperty)o.MimeSource.Clone();
            prodId = (ProductIdProperty)o.ProductId.Clone();
            nickname = (NicknameProperty)o.Nickname.Clone();
            sortString = (SortStringProperty)o.SortString.Clone();
            classification = (ClassificationProperty)o.Classification.Clone();
            categories = (CategoriesProperty)o.Categories.Clone();
        }

        /// <summary>
        /// The method can be called to clear all current property values from the vCard.  The version is left
        /// unchanged.
        /// </summary>
        public void ClearProperties()
        {
            groupName = null;

            fn = null;
            name = null;
            title = null;
            role = null;
            mailer = null;
            url = null;
            org = null;
            uid = null;
            bday = null;
            rev = null;
            tz = null;
            geo = null;
            key = null;
            photo = null;
            logo = null;
            sound = null;

            notes = null;
            addrs = null;
            labels = null;
            phones = null;
            email = null;
            agents = null;
            customProps = null;

            addProfile = false;
            mimeName = null;
            mimeSource = null;
            prodId = null;
            nickname = null;
            sortString = null;
            classification = null;
            categories = null;
        }

        /// <summary>
        /// This is used to propagate the version to all properties in the object that need it
        /// </summary>
        public void PropagateVersion()
        {
            if(fn != null)
                fn.Version = this.Version;

            if(name != null)
                name.Version = this.Version;

            if(title != null)
                title.Version = this.Version;

            if(role != null)
                role.Version = this.Version;

            if(mailer != null)
                mailer.Version = this.Version;

            if(url != null)
                url.Version = this.Version;

            if(org != null)
                org.Version = this.Version;

            if(uid != null)
                uid.Version = this.Version;

            if(bday != null)
                bday.Version = this.Version;

            if(rev != null)
                rev.Version = this.Version;

            if(tz != null)
                tz.Version = this.Version;

            if(geo != null)
                geo.Version = this.Version;

            if(key != null)
                key.Version = this.Version;

            if(photo != null)
                photo.Version = this.Version;

            if(logo != null)
                logo.Version = this.Version;

            if(sound != null)
                sound.Version = this.Version;

            if(notes != null)
                notes.PropagateVersion(this.Version);

            if(addrs != null)
                addrs.PropagateVersion(this.Version);

            if(labels != null)
                labels.PropagateVersion(this.Version);

            if(phones != null)
                phones.PropagateVersion(this.Version);

            if(email != null)
                email.PropagateVersion(this.Version);

            if(agents != null)
                agents.PropagateVersion(this.Version);

            if(customProps != null)
                customProps.PropagateVersion(this.Version);

            if(this.Version != SpecificationVersions.vCard21)
            {
                if(mimeName != null)
                    mimeName.Version = this.Version;

                if(mimeSource != null)
                    mimeSource.Version = this.Version;

                if(prodId != null)
                    prodId.Version = this.Version;

                if(nickname != null)
                    nickname.Version = this.Version;

                if(sortString != null)
                    sortString.Version = this.Version;

                if(classification != null)
                    classification.Version = this.Version;

                if(categories != null)
                    categories.Version = this.Version;
            }
        }

        /// <summary>
        /// This is overridden to allow proper comparison of vCard objects
        /// </summary>
        /// <param name="obj">The object to which this instance is compared</param>
        /// <returns>Returns true if the object equals this instance, false if it does not</returns>
        public override bool Equals(object obj)
        {
            VCard vc = obj as VCard;

            if(vc == null)
                return false;

            // The ToString() method returns a text representation of the vCard based on all of its settings so
            // it's a reliable way to tell if two instances are the same.
            return (this == vc || this.ToString() == vc.ToString());
        }

        /// <summary>
        /// Get a hash code for the vCard object
        /// </summary>
        /// <returns>Returns the hash code for the vCard object</returns>
        /// <remarks>Since the ToString() method returns a text representation based on all of the settings, this
        /// returns the hash code for the string returned by it.</remarks>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// Convert the vCard instance to its string form
        /// </summary>
        /// <returns>Returns a text description of the vCard suitable for saving to a PDI data stream</returns>
        public override string ToString()
        {
            using(var sw = new StringWriter(new StringBuilder(1024), CultureInfo.InvariantCulture))
            {
                this.WriteToStream(sw, null);
                return sw.ToString();
            }
        }

        /// <summary>
        /// This can be used to write a vCard to a PDI data stream
        /// </summary>
        /// <param name="tw">A <see cref="System.IO.TextWriter"/> derived class to which the vCard is written.</param>
        /// <remarks>This is called by <see cref="ToString"/> as well as owning objects when they convert
        /// themselves to a string or write themselves to a PDI data stream.</remarks>
        /// <overloads>There are two overloads for this method.</overloads>
        /// <example>
        /// <code language="cs">
        /// // Create a vCard
        /// VCard vc = new VCard();
        /// vc.FormattedName.Value = "Smith, John";
        /// vc.Name.FamilyName = "Smith";
        /// vc.Name.GivenName = "John";
        ///
        /// // Open the file and write the vCard to it
        /// StreamWriter sw = new StreamWriter(@"C:\AddressBook.vcf");
        /// vc.WriteToStream(sw);
        /// sw.Close();
        /// </code>
        /// <code language="vbnet">
        /// ' Create a vCard
        /// Dim vc As New VCard()
        /// vc.FormattedName.Value = "Smith, John"
        /// vc.Name.FamilyName = "Smith"
        /// vc.Name.GivenName = "John"
        ///
        /// ' Open the file and write the vCard to it
        /// Dim sw As New StreamWriter("C:\AddressBook.vcf")
        /// vc.WriteToStream(sw)
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
        /// This can be used to write a vCard to a PDI data stream
        /// </summary>
        /// <param name="tw">A <see cref="System.IO.TextWriter"/> derived class to which the vCard is written.</param>
        /// <param name="sb">A <see cref="System.Text.StringBuilder"/> used by the properties as a temporary
        /// buffer.  This can be null if the TextWriter is a <see cref="System.IO.StringWriter"/>.</param>
        /// <remarks>This is called by <see cref="ToString"/> as well as owning objects when they convert
        /// themselves to a string or write themselves to a PDI data stream.</remarks>
        public void WriteToStream(TextWriter tw, StringBuilder sb)
        {
            // If Formatted Name is undefined, try to set it based on Name
            if(fn == null || fn.Value == "Unknown")
                this.FormattedName.Value = this.Name.FormattedName;

            PropagateVersion();

            if(groupName != null)
            {
                tw.Write(groupName);
                tw.Write('.');
            }

            tw.Write("BEGIN:VCARD\r\n");

            if(this.Version == SpecificationVersions.vCard21)
                tw.Write("VERSION:2.1\r\n");
            else
                tw.Write("VERSION:3.0\r\n");

            // Save 3.0 specification properties when needed.  We'll group the properties to keep similar ones
            // together.  It's not necessary, but it makes things easier to find.
            if(this.Version == SpecificationVersions.vCard30)
            {
                if(addProfile)
                    tw.Write("PROFILE:VCARD\r\n");

                BaseProperty.WriteToStream(prodId, sb, tw);
                BaseProperty.WriteToStream(mimeName, sb, tw);
                BaseProperty.WriteToStream(mimeSource, sb, tw);
            }

            // These two are required properties
            BaseProperty.WriteToStream(this.FormattedName, sb, tw);
            BaseProperty.WriteToStream(this.Name, sb, tw);

            if(this.Version == SpecificationVersions.vCard30)
            {
                BaseProperty.WriteToStream(nickname, sb, tw);
                BaseProperty.WriteToStream(sortString, sb, tw);
            }

            BaseProperty.WriteToStream(bday, sb, tw);
            BaseProperty.WriteToStream(org, sb, tw);
            BaseProperty.WriteToStream(title, sb, tw);
            BaseProperty.WriteToStream(role, sb, tw);

            if(this.Version == SpecificationVersions.vCard30)
            {
                BaseProperty.WriteToStream(classification, sb, tw);
                BaseProperty.WriteToStream(categories, sb, tw);
            }

            if(addrs != null && addrs.Count != 0)
                foreach(AddressProperty a in addrs)
                    BaseProperty.WriteToStream(a, sb, tw);

            if(labels != null && labels.Count != 0)
                foreach(LabelProperty l in labels)
                    BaseProperty.WriteToStream(l, sb, tw);

            if(phones != null && phones.Count != 0)
                foreach(TelephoneProperty t in phones)
                    BaseProperty.WriteToStream(t, sb, tw);

            if(email != null && email.Count != 0)
                foreach(EMailProperty e in email)
                    BaseProperty.WriteToStream(e, sb, tw);

            if(agents != null && agents.Count != 0)
                foreach(AgentProperty a in agents)
                    BaseProperty.WriteToStream(a, sb, tw);

            if(notes != null && notes.Count != 0)
                foreach(NoteProperty n in notes)
                    BaseProperty.WriteToStream(n, sb, tw);

            BaseProperty.WriteToStream(url, sb, tw);
            BaseProperty.WriteToStream(tz, sb, tw);
            BaseProperty.WriteToStream(geo, sb, tw);
            BaseProperty.WriteToStream(mailer, sb, tw);
            BaseProperty.WriteToStream(rev, sb, tw);
            BaseProperty.WriteToStream(uid, sb, tw);

            BaseProperty.WriteToStream(key, sb, tw);
            BaseProperty.WriteToStream(photo, sb, tw);
            BaseProperty.WriteToStream(logo, sb, tw);
            BaseProperty.WriteToStream(sound, sb, tw);

            if(customProps != null && customProps.Count != 0)
                foreach(CustomProperty c in customProps)
                    BaseProperty.WriteToStream(c, sb, tw);

            if(groupName != null)
            {
                tw.Write(groupName);
                tw.Write('.');
            }

            tw.Write("END:VCARD\r\n");
        }
        #endregion
    }
}
