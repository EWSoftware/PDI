---
uid: 87814df5-add9-4307-89a7-4a46a9456eed
alt-uid: VNoteClasses
title: VNOTE Classes
keywords: "property mappings, VNOTE"
---
vNote is a format used for transferring notes between hand-held devices that is part of the Infrared Mobile
Computing (IrMC) protocol.  A note object is typically a self-authored memo.  The `VNote` class represents a
single vNote.  The `VNoteCollection` can contain multiple vNotes.

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

`BEGIN:VNOTE`

</td>
      <td> </td>
      <td>

[](@T:EWSoftware.PDI.Objects.VNote) and [](@T:EWSoftware.PDI.Objects.VNoteCollection)

</td>
      <td> </td>
      <td> </td>
    </tr>
    <tr>
      <td>

`BODY`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VNote.Body)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.BodyProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`CATEGORIES`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VNote.Categories)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.CategoriesProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.CategoriesProperty.Categories) and
[](@P:EWSoftware.PDI.Properties.CategoriesProperty.CategoriesString)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`CLASS`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VNote.Classification)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.ClassificationProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`DCREATED`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VNote.DateCreated)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.DateCreatedProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.DateTimeValue)

</td>
      <td>

`DCREATED` is used by the IrMC 1.1 `VNOTE` object.

</td>
    </tr>
    <tr>
      <td>

`LAST-MODIFIED`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VNote.LastModified)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.LastModifiedProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.DateTimeValue)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`SUMMARY`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VNote.Summary)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.SummaryProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseAltRepProperty.AlternateRepresentation) and
[](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`VERSION`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VNote.Version)

</td>
      <td>

[](@T:EWSoftware.PDI.Objects.VNote)

</td>
      <td> </td>
      <td>

Version will always be `IrMC11`.

</td>
    </tr>
    <tr>
      <td>

`X-IRMC-LUID`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VNote.UniqueId)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.UniqueIdProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.UniqueIdProperty.Value)

</td>
      <td>

For `VNOTE`, the property tag is `X-IRMC-LUID` rather than `UID`.

</td>
    </tr>
    <tr>
      <td>

`X-???`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VNote.CustomProperties)

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

`END:VNOTE`

</td>
      <td> </td>
      <td>

[](@T:EWSoftware.PDI.Objects.VNote) and [](@T:EWSoftware.PDI.Objects.VNoteCollection)

</td>
      <td> </td>
      <td> </td>
    </tr>
  </tbody>
</table>

## See Also
**Other Resources**  
[](@7ea5cd94-fa8a-4070-ba20-6d8c3b40d7c3)  
