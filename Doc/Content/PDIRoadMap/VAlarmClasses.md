---
uid: 2728ddf1-da12-446d-9142-b4bc57540d24
alt-uid: VAlarmClasses
title: VALARM Classes
keywords: "property mappings, VALARM"
---

The `VAlarm` class represents an alarm component in a `VEVENT` or a `VTODO` component.  The `VALARM` component is
used by iCalendar 2.0.  In a vCalendar, the alarms are represented by four separate properties (`AALARM`,
`DALARM`, `MALARM`, and `PALARM`).  The `VAlarm` class is used to represent both the iCalendar sub-component and
the vCalendar properties.  It will write itself out in an appropriate format based on the current setting of its
`Version` property.  The `ActionProperty` is used to determine the actual alarm type.

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

`BEGIN:VALARM`

</td>
      <td> </td>
      <td>

[](@T:EWSoftware.PDI.Objects.VAlarm) and [](@T:EWSoftware.PDI.Objects.VAlarmCollection)

</td>
      <td> </td>
      <td>vCalendar represents this component as a property when written to a data stream.</td>
    </tr>
    <tr>
      <td>

`ACTION`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VAlarm.Action)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.ActionProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.ActionProperty.Action) and [](@P:EWSoftware.PDI.Properties.ActionProperty.OtherAction)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`ATTACH`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VAlarm.Attachments)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.AttachProperty) and [](@T:EWSoftware.PDI.Properties.AttachPropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.AttachProperty.FormatType),
[](@M:EWSoftware.PDI.Properties.AttachProperty.GetAttachmentBytes){prefer-overload="true"},
[](@M:EWSoftware.PDI.Properties.AttachProperty.SetAttachmentBytes(System.Byte[])){prefer-overload="true"},
and [](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td>

If the value location is inline, the data value is a binary encoded string representing the attachment.  If not
inline, the `Value` property refers to something like a URI where the attachment can be found.

Only valid for audio, e-mail and procedure alarms.  E-mail alarms can have multiple attachments but audio and
procedure alarms can only have one.

</td>
    </tr>
    <tr>
      <td>

`ATTENDEE`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VAlarm.Attendees)

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
      <td>This property is only valid for e-mail alarms.</td>
    </tr>
    <tr>
      <td>

`DESCRIPTION`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VAlarm.Description)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.DescriptionProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseAltRepProperty.AlternateRepresentation)
and [](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td>This property is only valid for display, e-mail, and procedure alarms.</td>
    </tr>
    <tr>
      <td>

`DURATION`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VAlarm.Duration)

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

`REPEAT`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VAlarm.Repeat)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.RepeatProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.RepeatProperty.RepeatCount)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`SUMMARY`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VAlarm.Summary)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.SummaryProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseAltRepProperty.AlternateRepresentation) and
[](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td>This property is only valid for e-mail alarms.</td>
    </tr>
    <tr>
      <td>

`TRIGGER`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VAlarm.Trigger)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.TriggerProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.TriggerProperty.DateTimeValue),
[](@P:EWSoftware.PDI.Properties.TriggerProperty.DurationValue),
[](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.IsFloating),
[](@P:EWSoftware.PDI.Properties.TriggerProperty.RelatedToEnd),
[](@P:EWSoftware.PDI.Properties.TriggerProperty.TimeZoneDateTime),
and [](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.TimeZoneId)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`X-???`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VAlarm.CustomProperties)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.CustomProperty)
and [](@T:EWSoftware.PDI.Properties.CustomPropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.CustomProperty.CustomName) and [](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td>This property class represents custom or extended properties that are not part of the specification.</td>
    </tr>
    <tr>
      <td>

`END:VALARM`

</td>
      <td> </td>
      <td>

[](@T:EWSoftware.PDI.Objects.VAlarm) and [](@T:EWSoftware.PDI.Objects.VAlarmCollection)

</td>
      <td> </td>
      <td>vCalendar represents this component as a property when written to a data stream.</td>
    </tr>
  </tbody>
</table>

## See Also
**Other Resources**  
[](@7ea5cd94-fa8a-4070-ba20-6d8c3b40d7c3)  
