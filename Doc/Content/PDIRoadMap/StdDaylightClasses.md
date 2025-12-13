---
uid: 22098358-f1b3-43a0-bda7-ed43313cc99d
alt-uid: StdDaylightClasses
title: STANDARD/DAYLIGHT Classes
keywords: "property mappings, DAYLIGHT", "property mappings, STANDARD"
---

The `ObservanceRule` class represents a standard time or daylight saving time observance rule sub-component of a
`VTIMEZONE` component.  This is used by iCalendar 2.0 only.  The [](@P:EWSoftware.PDI.Objects.ObservanceRule.RuleType)
property is used to determine the actual type.

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

`BEGIN:STANDARD` or `BEGIN:DAYLIGHT`

</td>
      <td> </td>
      <td>

[](@T:EWSoftware.PDI.Objects.ObservanceRule) and [](@T:EWSoftware.PDI.Objects.ObservanceRuleCollection)

</td>
      <td> </td>
      <td>

The `RuleType` property determines the actual type.

</td>
    </tr>
    <tr>
      <td>

`COMMENT`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.ObservanceRule.Comment)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.CommentProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseAltRepProperty.AlternateRepresentation) and
[](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`DTSTART`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.ObservanceRule.StartDateTime)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.StartDateProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.DateTimeValue)

</td>
      <td>

In this component, the `DTSTART` value is always stored in a floating format.

</td>
    </tr>
    <tr>
      <td>

`RDATE`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.ObservanceRule.RecurDates)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.RDateProperty) and [](@T:EWSoftware.PDI.Properties.RDatePropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.RDateProperty.DateTimeValue)

</td>
      <td>

In this component, `RDATE` values are always stored as date/time values in a floating format.

</td>
    </tr>
    <tr>
      <td>

`RRULE`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.ObservanceRule.RecurrenceRules)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.RRuleProperty) and [](@T:EWSoftware.PDI.Properties.RRulePropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.RRuleProperty.Recurrence)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`TZNAME`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.ObservanceRule.TimeZoneNames)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.TimeZoneNameProperty) and [](@T:EWSoftware.PDI.Properties.TimeZoneNamePropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseProperty.Value) and [](@P:EWSoftware.PDI.Properties.TimeZoneNameProperty.AbbreviatedTimeZoneName)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`TZOFFSETFROM`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.ObservanceRule.OffsetFrom)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.TimeZoneOffsetProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.TimeZoneOffsetProperty.TimeSpanValue)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`TZOFFSETTO`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.ObservanceRule.OffsetTo)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.TimeZoneOffsetProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.TimeZoneOffsetProperty.TimeSpanValue)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`X-???`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.ObservanceRule.CustomProperties)

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

`END:STANDARD` or `END:DAYLIGHT`

</td>
      <td> </td>
      <td>

[](@T:EWSoftware.PDI.Objects.ObservanceRule) and [](@T:EWSoftware.PDI.Objects.ObservanceRuleCollection)

</td>
      <td> </td>
      <td> </td>
    </tr>
  </tbody>
</table>

## See Also
**Other Resources**  
[](@7ea5cd94-fa8a-4070-ba20-6d8c3b40d7c3)  
