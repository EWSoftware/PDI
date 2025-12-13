---
uid: 6fc4dbbe-a274-4221-af50-680db3717693
alt-uid: VCalendarClasses
title: VCALENDAR Classes
keywords: "property mappings, VCALENDAR"
---

The `VCalendar` class represents a vCalendar or an iCalendar.  The `Version` property determines the actual
calendar type.  The `VCalendar` class contains all of the other calendar objects listed below.

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

`BEGIN:VCALENDAR`

</td>
      <td> </td>
      <td>

[](@T:EWSoftware.PDI.Objects.VCalendar)

</td>
      <td> </td>
      <td>

This class can represent a vCalendar or an iCalendar.  The `Version` property determines the actual calendar type.

</td>
    </tr>
    <tr>
      <td>

`GEO`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCalendar.VCalendarGeographicPosition)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.GeographicPositionProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.GeographicPositionProperty.Latitude) and
[](@P:EWSoftware.PDI.Properties.GeographicPositionProperty.Longitude)

</td>
      <td>

vCalendar 1.0 only.  For iCalendar 2.0, the `GEO` property is on the `VEVENT` and `VTODO` components.

</td>
    </tr>
    <tr>
      <td>

`CALSCALE`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCalendar.CalendarScale)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.CalendarScaleProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td>iCalendar 2.0 only.</td>
    </tr>
    <tr>
      <td>

`DAYLIGHT`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCalendar.VCalendarDaylightRule)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.DaylightProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.DaylightProperty.DaylightDesignation),
[](@P:EWSoftware.PDI.Properties.DaylightProperty.EndDateTime),
[](@P:EWSoftware.PDI.Properties.DaylightProperty.Offset),
[](@P:EWSoftware.PDI.Properties.DaylightProperty.StandardDesignation),
[](@P:EWSoftware.PDI.Properties.DaylightProperty.StartDateTime),
and [](@P:EWSoftware.PDI.Properties.DaylightProperty.UsesDaylightSavingTime)

</td>
      <td>

vCalendar 1.0 only.  iCalendar 2.0 uses `VTIMEZONE` components instead.

</td>
    </tr>
    <tr>
      <td>

`METHOD`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCalendar.Method)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.MethodProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td>iCalendar 2.0 only.</td>
    </tr>
    <tr>
      <td>

`PRODID`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCalendar.ProductId)

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
      <td> </td>
    </tr>
    <tr>
      <td>

`TZ`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCalendar.VCalendarTimeZone)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.TimeZoneProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.TimeZoneProperty.TimeSpanValue)

</td>
      <td>

vCalendar 1.0 only.  iCalendar 2.0 uses `VTIMEZONE` components instead.

</td>
    </tr>
    <tr>
      <td>

`VERSION`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.CalendarObject.Version)

</td>
      <td>

[](@T:EWSoftware.PDI.Objects.VCalendar)

</td>
      <td> </td>
      <td>

Version will be `vCalendar10` or `iCalendar20`.

</td>
    </tr>
    <tr>
      <td>

`X-???`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCalendar.CustomProperties)

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

`BEGIN:VEVENT` to `END:VEVENT`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCalendar.Events)

</td>
      <td>

[](@T:EWSoftware.PDI.Objects.VEvent) and [](@T:EWSoftware.PDI.Objects.VEventCollection)

</td>
      <td> </td>
      <td>

See the [](@91d0576f-e904-4e75-9f95-38a9dd368871) topic for more information.

</td>
    </tr>
    <tr>
      <td>

`BEGIN:VFREEBUSY` to `END:VFREEBUSY`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCalendar.FreeBusys)

</td>
      <td>

[](@T:EWSoftware.PDI.Objects.VFreeBusy) and [](@T:EWSoftware.PDI.Objects.VFreeBusyCollection)

</td>
      <td> </td>
      <td>

iCalendar 2.0 only. See the [](@fe961450-54b4-474a-962f-9e91083eb9ec) topic for more information.

</td>
    </tr>
    <tr>
      <td>

`BEGIN:VJOURNAL` to `END:VJOURNAL`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCalendar.Journals)

</td>
      <td>

[](@T:EWSoftware.PDI.Objects.VJournal) and [](@T:EWSoftware.PDI.Objects.VJournalCollection)

</td>
      <td> </td>
      <td>

iCalendar 2.0 only. See the [](@bcfa7d27-b157-4387-a4a6-74f9aa12416f) topic for more information.

</td>
    </tr>
    <tr>
      <td>

`BEGIN:VTIMEZONE` to `END:VTIMEZONE`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCalendar.TimeZones)

</td>
      <td>

[](@T:EWSoftware.PDI.Objects.VTimeZone) and [](@T:EWSoftware.PDI.Objects.VTimeZoneCollection)

</td>
      <td> </td>
      <td>

iCalendar 2.0 only. See the [](@b4c78e51-f041-4490-a637-10638ca9d748) topic for more information.

</td>
    </tr>
    <tr>
      <td>

`BEGIN:VTODO` to `END:VTODO`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VCalendar.ToDos)

</td>
      <td>

[](@T:EWSoftware.PDI.Objects.VToDo) and [](@T:EWSoftware.PDI.Objects.VToDoCollection)

</td>
      <td> </td>
      <td>

See the [](@3b06a002-6294-40b9-b43c-93af3e3ae046) topic for more information.

</td>
    </tr>
    <tr>
      <td>

`END:CALENDAR`

</td>
      <td> </td>
      <td>

[](@T:EWSoftware.PDI.Objects.VCalendar)

</td>
      <td> </td>
      <td> </td>
    </tr>
  </tbody>
</table>

## See Also
**Other Resources**  
[](@7ea5cd94-fa8a-4070-ba20-6d8c3b40d7c3)  
