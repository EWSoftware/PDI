---
uid: fe961450-54b4-474a-962f-9e91083eb9ec
alt-uid: VFreeBusyClasses
title: VFREEBUSY Classes
keywords: "property mappings, VFREEBUSY"
---
The `VFreeBusy` class represents a free/busy time component in a calendar.  This component is used by iCalendar
2.0 only.

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

`BEGIN:VFREEBUSY`

</td>
      <td> </td>
      <td>


[](@T:EWSoftware.PDI.Objects.VFreeBusy) and [](@T:EWSoftware.PDI.Objects.VFreeBusyCollection)

</td>
      <td> </td>
      <td> </td>
    </tr>
    <tr>
      <td>

`ATTENDEE`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VFreeBusy.Attendees)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.AttendeeProperty) and [](@T:EWSoftware.PDI.Properties.AttendeePropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.AttendeeProperty.CalendarUserType),
[](@P:EWSoftware.PDI.Properties.OrganizerProperty.CommonName),
[](@P:EWSoftware.PDI.Properties.AttendeeProperty.DelegatedFrom),
[](@P:EWSoftware.PDI.Properties.AttendeeProperty.DelegatedTo),
[](@P:EWSoftware.PDI.Properties.OrganizerProperty.DirectoryEntry),
[](@P:EWSoftware.PDI.Properties.AttendeeProperty.Expectation),
[](@P:EWSoftware.PDI.Properties.AttendeeProperty.Membership),
[](@P:EWSoftware.PDI.Properties.AttendeeProperty.ParticipationStatus),
[](@P:EWSoftware.PDI.Properties.AttendeeProperty.Role),
[](@P:EWSoftware.PDI.Properties.AttendeeProperty.Rsvp),
[](@P:EWSoftware.PDI.Properties.OrganizerProperty.SentBy),
and [](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`COMMENT`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VFreeBusy.Comment)

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

`CONTACT`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VFreeBusy.Contact)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.ContactProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseAltRepProperty.AlternateRepresentation) and
[](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`DTEND`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VFreeBusy.EndDateTime)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.EndDateProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.DateTimeValue),
[](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.IsFloating),
[](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.TimeZoneDateTime),
and [](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.TimeZoneId)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`DTSTAMP`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VFreeBusy.TimeStamp)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.TimeStampProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.DateTimeValue)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`DTSTART`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VFreeBusy.StartDateTime)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.StartDateProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.DateTimeValue),
[](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.IsFloating),
[](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.TimeZoneDateTime),
and [](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.TimeZoneId)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`DURATION`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VFreeBusy.Duration)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.DurationProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.DurationProperty.DurationValue)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`FREEBUSY`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VFreeBusy.FreeBusy)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.FreeBusyProperty) and [](@T:EWSoftware.PDI.Properties.FreeBusyPropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.FreeBusyProperty.FreeBusyType),
[](@P:EWSoftware.PDI.Properties.FreeBusyProperty.OtherType),
and [](@P:EWSoftware.PDI.Properties.FreeBusyProperty.PeriodValue)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`ORGANIZER`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VFreeBusy.Organizer)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.OrganizerProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.OrganizerProperty.CommonName),
[](@P:EWSoftware.PDI.Properties.OrganizerProperty.DirectoryEntry),
[](@P:EWSoftware.PDI.Properties.OrganizerProperty.SentBy), and
[](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`REQUEST-STATUS`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VFreeBusy.RequestStatuses)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.RequestStatusProperty) and [](@T:EWSoftware.PDI.Properties.RequestStatusPropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.RequestStatusProperty.ExtendedData),
[](@P:EWSoftware.PDI.Properties.RequestStatusProperty.StatusCode),
and [](@P:EWSoftware.PDI.Properties.RequestStatusProperty.StatusMessage)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`UID`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VFreeBusy.UniqueId)

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

[](@P:EWSoftware.PDI.Objects.VFreeBusy.Url)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.UrlProperty)

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

[](@P:EWSoftware.PDI.Objects.VFreeBusy.CustomProperties)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.CustomProperty) and [](@T:EWSoftware.PDI.Properties.CustomPropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.CustomProperty.CustomName) and [](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td>

This property class represents custom or extended properties that are not part of the specification.

</td>
    </tr>
    <tr>
      <td>

`END:VFREEBUSY`

</td>
      <td> </td>
      <td>

[](@T:EWSoftware.PDI.Objects.VFreeBusy) and [](@T:EWSoftware.PDI.Objects.VFreeBusyCollection)

</td>
      <td> </td>
      <td> </td>
    </tr>
  </tbody>
</table>

## See Also
**Other Resources**  
[](@7ea5cd94-fa8a-4070-ba20-6d8c3b40d7c3)  
