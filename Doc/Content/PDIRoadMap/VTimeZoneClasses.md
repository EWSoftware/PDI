---
uid: b4c78e51-f041-4490-a637-10638ca9d748
alt-uid: VTimeZoneClasses
title: VTIMEZONE Classes
keywords: "property mappings, VTIMEZONE"
---
The `VTimeZone` class represents a time zone information component in a calendar.  This component is used by
iCalendar 2.0 only.

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

`BEGIN:VTIMEZONE`

</td>
      <td> </td>
      <td>

[](@T:EWSoftware.PDI.Objects.VTimeZone) and [](@T:EWSoftware.PDI.Objects.VTimeZoneCollection)

</td>
      <td> </td>
      <td> </td>
    </tr>
    <tr>
      <td>

`LAST-MODIFIED`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VTimeZone.LastModified)

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

`TZID`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VTimeZone.TimeZoneId)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.TimeZoneIdProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.TimeZoneIdProperty.Value)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`TZURL`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VTimeZone.TimeZoneUrl)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.TimeZoneUrlProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`X-???`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VTimeZone.CustomProperties)

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

`BEGIN:STANDARD` to `END:STANDARD` and `BEGIN:DAYLIGHT` to `END:DAYLIGHT`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VTimeZone.ObservanceRules)

</td>
      <td>

[](@T:EWSoftware.PDI.Objects.ObservanceRule) and [](@T:EWSoftware.PDI.Objects.ObservanceRuleCollection)

</td>
      <td> </td>
      <td>

The [](@P:EWSoftware.PDI.Objects.ObservanceRule.RuleType) property on each observance rule determines the actual
type.  See the [](@22098358-f1b3-43a0-bda7-ed43313cc99d) topic for more information.

</td>
    </tr>
    <tr>
      <td>

`END:VTIMEZONE`

</td>
      <td> </td>
      <td>

[](@T:EWSoftware.PDI.Objects.VTimeZone) and [](@T:EWSoftware.PDI.Objects.VTimeZoneCollection)

</td>
      <td> </td>
      <td> </td>
    </tr>
  </tbody>
</table>

## See Also
**Other Resources**  
[](@7ea5cd94-fa8a-4070-ba20-6d8c3b40d7c3)  
