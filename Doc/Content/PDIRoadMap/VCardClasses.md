---
uid: 0207df86-ca35-4e95-a711-6d4725291e40
alt-uid: VCardClasses
title: VCARD Classes
keywords: "property mappings, VCARD"
---
The `VCard` class represents a single vCard.  The `VCardCollection` can contain multiple vCards.

<table>
  <thead>
    <tr>
      <th>PDI Tag</th>
      <th>Component Property</th>
      <th>EWSoftware PDI Class</th>
      <th>Data Value Property</th>
      <th>Notes</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>

`BEGIN:VCARD`

</td>
      <td> </td>
      <td>

[](@T:EWSoftware.PDI.Objects.VCard) and [](@T:EWSoftware.PDI.Objects.VCardCollection)

</td>
      <td> </td>
      <td> </td>
    </tr>
    <tr>
      <td>

`ADR`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.Addresses)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.AddressProperty) and [](@T:EWSoftware.PDI.Properties.AddressPropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.AddressProperty.AddressTypes),
[](@P:EWSoftware.PDI.Properties.AddressProperty.Country),
[](@P:EWSoftware.PDI.Properties.AddressProperty.ExtendedAddress),
[](@P:EWSoftware.PDI.Properties.AddressProperty.Locality),
[](@P:EWSoftware.PDI.Properties.AddressProperty.POBox),
[](@P:EWSoftware.PDI.Properties.AddressProperty.PostalCode),
[](@P:EWSoftware.PDI.Properties.AddressProperty.Region),
and [](@P:EWSoftware.PDI.Properties.AddressProperty.StreetAddress)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`AGENT`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.Agents)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.AgentProperty) and [](@T:EWSoftware.PDI.Properties.AgentPropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.AgentProperty.VCard) if the value location is inline or
[](@P:EWSoftware.PDI.Properties.AgentProperty.Value) if the value location is a URI.

</td>
      <td>If the value location is inline, this property actually represents a nested vCard.  There can
be more than one.</td>
    </tr>
    <tr>
      <td>

`ANNIVERSARY`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.Anniversary)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.AnniversaryProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.DateTimeValue)

</td>
      <td>vCard 4.0 only.</td>
    </tr>
    <tr>
      <td>

`BDAY`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.BirthDate)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.BirthDateProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.DateTimeValue)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`CATEGORIES`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.Categories)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.CategoriesProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.CategoriesProperty.Categories) and
[](@P:EWSoftware.PDI.Properties.CategoriesProperty.CategoriesString)

</td>
      <td>vCard 3.0 and 4.0 only.</td>
    </tr>
    <tr>
      <td>

`CLASS`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.Classification)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.ClassificationProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td>vCard 3.0 only.</td>
    </tr>
    <tr>
      <td>

`CLIENTPIDMAP`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.ClientPidMaps)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.ClientPidMapProperty) and
[](@T:EWSoftware.PDI.Properties.ClientPidMapPropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.ClientPidMapProperty.Id) and
[](@P:EWSoftware.PDI.Properties.ClientPidMapProperty.Uri)

</td>
      <td>vCard 4.0 only.</td>
    </tr>
    <tr>
      <td>

`EMAIL`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.EMailAddresses)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.EMailProperty) and [](@T:EWSoftware.PDI.Properties.EMailPropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.EMailProperty.EMailTypes) and [](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`FN`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.FormattedName)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.FormattedNameProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`GENDER`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.Gender)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.GenderProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.GenderProperty.Sex)	and [](@P:EWSoftware.PDI.Properties.GenderProperty.GenderIdentity)

</td>
      <td>vCard 4.0 only.</td>
    </tr>
    <tr>
      <td>

`GEO`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.GeographicPosition)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.GeographicPositionProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.GeographicPositionProperty.Latitude) and
[](@P:EWSoftware.PDI.Properties.GeographicPositionProperty.Longitude)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`KEY`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.PublicKey)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.PublicKeyProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.PublicKeyProperty.KeyType),
[](@M:EWSoftware.PDI.Properties.PublicKeyProperty.GetKeyBytes){prefer-overload="true"},
[](@M:EWSoftware.PDI.Properties.PublicKeyProperty.SetKeyBytes(System.Byte[])){prefer-overload="true"},
and [](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td>

If the value location is inline, the data value is a binary encoded string representing the key.  If not inline,
the `Value` property refers to something like a URI where the key can be found.

</td>
    </tr>
    <tr>
      <td>

`KIND`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.Kind)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.KindProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.KindProperty.CardKind) and [](@P:EWSoftware.PDI.Properties.KindProperty.OtherKind)

</td>
      <td>vCard 4.0 only.</td>
    </tr>
    <tr>
      <td>

`LABEL`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.Labels)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.LabelProperty) and [](@T:EWSoftware.PDI.Properties.LabelPropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.LabelProperty.AddressTypes) and [](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`LOGO`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.Logo)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.LogoProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.PhotoProperty.ImageType),
[](@M:EWSoftware.PDI.Properties.PhotoProperty.GetImageBytes){prefer-overload="true"},
[](@M:EWSoftware.PDI.Properties.PhotoProperty.SetImageBytes(System.Byte[])){prefer-overload="true"},
and [](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td>

If the value location is inline, the data value is a binary encoded string representing the image.  If not
inline, the `Value` property refers to something like a URI where the image can be found.

</td>
    </tr>
    <tr>
      <td>

`MAILER`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.Mailer)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.MailerProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`MEMBER`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.Members)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.MemberProperty) and [](@T:EWSoftware.PDI.Properties.MemberPropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td>vCard 4.0 only.</td>
    </tr>
    <tr>
      <td>

`N`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.Name)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.NameProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.NameProperty.AdditionalNames),
[](@P:EWSoftware.PDI.Properties.NameProperty.FamilyName),
[](@P:EWSoftware.PDI.Properties.NameProperty.FormattedName),
[](@P:EWSoftware.PDI.Properties.NameProperty.GivenName),
[](@P:EWSoftware.PDI.Properties.NameProperty.NamePrefix),
[](@P:EWSoftware.PDI.Properties.NameProperty.NameSuffix),
and [](@P:EWSoftware.PDI.Properties.NameProperty.SortableName)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`NAME`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.MimeName)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.MimeNameProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td>vCard 3.0 only.</td>
    </tr>
    <tr>
      <td>

`NICKNAME`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.Nickname)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.NicknameProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.NicknameProperty.Nicknames) and
[](@P:EWSoftware.PDI.Properties.NicknameProperty.NicknamesString)

</td>
      <td>vCard 3.0 and 4.0 only.</td>
    </tr>
    <tr>
      <td>

`NOTE`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.Notes)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.NoteProperty) and [](@T:EWSoftware.PDI.Properties.NotePropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`ORG`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.Organization)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.OrganizationProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.OrganizationProperty.Name),
[](@P:EWSoftware.PDI.Properties.OrganizationProperty.Units),
and [](@P:EWSoftware.PDI.Properties.OrganizationProperty.UnitsString)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`PHOTO`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.Photo)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.PhotoProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.PhotoProperty.ImageType),
[](@M:EWSoftware.PDI.Properties.PhotoProperty.GetImageBytes){prefer-overload="true"},
[](@M:EWSoftware.PDI.Properties.PhotoProperty.SetImageBytes(System.Byte[])){prefer-overload="true"},
and [](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td>

If the value location is inline, the data value is a binary encoded string representing the image.  If not
inline, the `Value` property refers to something like a URI where the image can be found.

</td>
    </tr>
    <tr>
      <td>

`PRODID`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.ProductId)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.ProductIdProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.ProductIdProperty.KeywordDescription),
[](@P:EWSoftware.PDI.Properties.ProductIdProperty.ProductLanguage),
[](@P:EWSoftware.PDI.Properties.ProductIdProperty.OwnerName),
and [](@P:EWSoftware.PDI.Properties.ProductIdProperty.IsRegistered)

</td>
      <td>vCard 3.0 and 4.0 only.</td>
    </tr>
    <tr>
      <td>

`PROFILE:VCARD`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.AddProfile)

</td>
      <td> </td>
      <td> </td>
      <td>vCard 3.0 and 4.0 only.</td>
    </tr>
    <tr>
      <td>

`RELATED`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.Related)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.RelatedProperty) and [](@T:EWSoftware.PDI.Properties.RelatedPropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.RelatedProperty.RelatedTypes) and
[](@P:EWSoftware.PDI.Properties.RelatedProperty.PreferredOrder)

</td>
      <td>vCard 4.0 only.</td>
    </tr>
    <tr>
      <td>

`REV`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.LastRevision)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.LastRevisionProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.DateTimeValue)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`ROLE`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.Role)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.RoleProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`SORT-STRING`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.SortString)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.SortStringProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td>vCard 3.0 only.</td>
    </tr>
    <tr>
      <td>

`SOUND`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.Sound)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.SoundProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.SoundProperty.SoundType),
[](@M:EWSoftware.PDI.Properties.SoundProperty.GetSoundBytes){prefer-overload="true"},
[](@M:EWSoftware.PDI.Properties.SoundProperty.SetSoundBytes(System.Byte[])){prefer-overload="true"},
and [](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td>

If the value location is inline, the data value is a binary encoded string representing the sound.  If not
inline, the `Value` property refers to something like a URI where the sound can be found.

</td>
    </tr>
    <tr>
      <td>

`SOURCE`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.MimeSource)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.MimeSourceProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.MimeSourceProperty.Context) and [](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td>vCard 3.0 and 4.0 only.</td>
    </tr>
    <tr>
      <td>

`TEL`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.Telephones)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.TelephoneProperty) and [](@T:EWSoftware.PDI.Properties.TelephonePropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.TelephoneProperty.PhoneTypes) and [](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`TITLE`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.Title)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.TitleProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`TZ`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.TimeZone)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.TimeZoneProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.TimeZoneProperty.TimeSpanValue) and [](@P:EWSoftware.PDI.Properties.TimeZoneProperty.Value)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`UID`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.UniqueId)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.UniqueIdProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.UniqueIdProperty.Value)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`URL`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.Urls)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.UrlProperty) and [](@T:EWSoftware.PDI.Properties.UrlPropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`VERSION`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.Version)

</td>
      <td>

[](@T:EWSoftware.PDI.Objects.VCard)

</td>
      <td> </td>
      <td>

Version will be `vCard21`, `vCard30`, or `vCard40`.

</td>
    </tr>
    <tr>
      <td>

`X-???`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCard.CustomProperties)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.CustomProperty) and [](@T:EWSoftware.PDI.Properties.CustomPropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.CustomProperty.CustomName) and [](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td>This property class represents custom or extended properties that are not part of the specification.</td>
    </tr>
    <tr>
      <td>

`END:VCARD`

</td>
      <td> </td>
      <td>

[](@T:EWSoftware.PDI.Objects.VCard) and [](@T:EWSoftware.PDI.Objects.VCardCollection)

</td>
      <td> </td>
      <td> </td>
    </tr>
  </tbody>
</table>

## See Also
**Other Resources**  
[](@7ea5cd94-fa8a-4070-ba20-6d8c3b40d7c3)  
