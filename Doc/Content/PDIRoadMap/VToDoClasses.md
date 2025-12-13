---
uid: 3b06a002-6294-40b9-b43c-93af3e3ae046
alt-uid: VToDoClasses
title: VTODO Classes
keywords: "property mappings, VTODO"
---
The `VToDo` class represents a To-Do component in a calendar.  This component is used by both vCalendar and
iCalendar.

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

`BEGIN:VTODO`

</td>
      <td> </td>
      <td>

[](@T:EWSoftware.PDI.Objects.VToDo) and [](@T:EWSoftware.PDI.Objects.VToDoCollection)

</td>
      <td> </td>
      <td> </td>
    </tr>
    <tr>
      <td>

`AALARM`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.Alarms)

</td>
      <td>

[](@T:EWSoftware.PDI.Objects.VAlarm) and [](@T:EWSoftware.PDI.Objects.VAlarmCollection)

</td>
      <td> </td>
      <td>

vCalendar 1.0 only. See the [](@2728ddf1-da12-446d-9142-b4bc57540d24) topic for more information.

</td>
    </tr>
    <tr>
      <td>

`ATTACH`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.Attachments)

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

</td>
    </tr>
    <tr>
      <td>

`ATTENDEE`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.Attendees)

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

`CATEGORIES`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.Categories)

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

[](@P:EWSoftware.PDI.Objects.VToDo.Classification)

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

`COMMENT`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.Comment)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.CommentProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseAltRepProperty.AlternateRepresentation) and
[](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td>iCalendar 2.0 only.</td>
    </tr>
    <tr>
      <td>

`COMPLETED`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.CompletedDateTime)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.CompletedDateProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.DateTimeValue)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`CONTACT`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.Contacts)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.ContactProperty) and [](@T:EWSoftware.PDI.Properties.ContactPropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseAltRepProperty.AlternateRepresentation) and [](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td>iCalendar 2.0 only.</td>
    </tr>
    <tr>
      <td>

`CREATED` or `DCREATED`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.DateCreated)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.DateCreatedProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.DateTimeValue)

</td>
      <td>

`DCREATED` is used by vCalendar 1.0.  `CREATED` is used by iCalendar 2.0.

</td>
    </tr>
    <tr>
      <td>

`DALARM`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.Alarms)

</td>
      <td>

[](@T:EWSoftware.PDI.Objects.VAlarm) and [](@T:EWSoftware.PDI.Objects.VAlarmCollection)

</td>
      <td> </td>
      <td>

vCalendar 1.0 only. See the [](@2728ddf1-da12-446d-9142-b4bc57540d24) topic for more information.

</td>
    </tr>
    <tr>
      <td>

`DESCRIPTION`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.Description)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.DescriptionProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseAltRepProperty.AlternateRepresentation) and
[](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`DTSTAMP`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.TimeStamp)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.TimeStampProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.DateTimeValue)

</td>
      <td>iCalendar 2.0 only.</td>
    </tr>
    <tr>
      <td>

`DTSTART`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.StartDateTime)

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

`DUE`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.DueDateTime)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.DueDateProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.DateTimeValue),
[](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.IsFloating),
[](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.TimeZoneDateTime),
and [](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.TimeZoneId)

</td>
      <td>

`DUE` and `DURATION` are mutually exclusive.

</td>
    </tr>
    <tr>
      <td>

`DURATION`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.Duration)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.DurationProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.DurationProperty.DurationValue)

</td>
      <td>

iCalendar 2.0 only.  `DUE` and `DURATION` are mutually exclusive.

</td>
    </tr>
    <tr>
      <td>

`EXDATE`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.RecurringObject.ExceptionDates)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.ExDateProperty) and [](@T:EWSoftware.PDI.Properties.ExDatePropertyCollection)

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

`EXRULE`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.RecurringObject.ExceptionRules)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.ExRuleProperty) and [](@T:EWSoftware.PDI.Properties.RRulePropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.RRuleProperty.Recurrence)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`GEO`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.GeographicPosition)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.GeographicPositionProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.GeographicPositionProperty.Latitude) and
[](@P:EWSoftware.PDI.Properties.GeographicPositionProperty.Longitude)

</td>
      <td>

iCalendar 2.0 only.  For vCalendar 1.0, the `GEO` property is on the `VCALENDAR` component.

</td>
    </tr>
    <tr>
      <td>

`LAST-MODIFIED`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.LastModified)

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

`MALARM`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.Alarms)

</td>
      <td>

[](@T:EWSoftware.PDI.Objects.VAlarm) and [](@T:EWSoftware.PDI.Objects.VAlarmCollection)

</td>
      <td> </td>
      <td>

vCalendar 1.0 only. See the [](@2728ddf1-da12-446d-9142-b4bc57540d24) topic for more information.

</td>
    </tr>
    <tr>
      <td>

`ORGANIZER`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.Organizer)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.OrganizerProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.OrganizerProperty.CommonName),
[](@P:EWSoftware.PDI.Properties.OrganizerProperty.DirectoryEntry),
[](@P:EWSoftware.PDI.Properties.OrganizerProperty.SentBy),
and [](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td>iCalendar 2.0 only.</td>
    </tr>
    <tr>
      <td>

`PALARM`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.Alarms)

</td>
      <td>

[](@T:EWSoftware.PDI.Objects.VAlarm) and [](@T:EWSoftware.PDI.Objects.VAlarmCollection)

</td>
      <td> </td>
      <td>

vCalendar 1.0 only. See the [](@2728ddf1-da12-446d-9142-b4bc57540d24) topic for more information.

</td>
    </tr>
    <tr>
      <td>

`PERCENT-COMPLETED`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.PercentComplete)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.PercentCompleteProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.PercentCompleteProperty.Percentage)

</td>
      <td>iCalendar 2.0 only.</td>
    </tr>
    <tr>
      <td>

`PRIORITY`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.Priority)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.PriorityProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.PriorityProperty.PriorityValue)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`RDATE`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.RecurringObject.RecurDates)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.RDateProperty) and [](@T:EWSoftware.PDI.Properties.RDatePropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.RDateProperty.DateTimeValue),
[](@P:EWSoftware.PDI.Properties.RDateProperty.IsFloating),
[](@P:EWSoftware.PDI.Properties.RDateProperty.PeriodValue),
[](@P:EWSoftware.PDI.Properties.RDateProperty.TimeZoneDateTime),
and [](@P:EWSoftware.PDI.Properties.RDateProperty.TimeZoneId)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`RECURRENCE-ID`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.RecurrenceId)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.RecurrenceIdProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.RecurrenceIdProperty.Range) and [](@P:EWSoftware.PDI.Properties.BaseProperty.Value)

</td>
      <td>iCalendar 2.0 only.</td>
    </tr>
    <tr>
      <td>

`RELATED-TO`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.RelatedTo)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.RelatedToProperty) and [](@T:EWSoftware.PDI.Properties.RelatedToPropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.RelatedToProperty.OtherRelationship) and
[](@P:EWSoftware.PDI.Properties.RelatedToProperty.RelationshipType)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`REQUEST-STATUS`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.RequestStatuses)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.RequestStatusProperty) and
[](@T:EWSoftware.PDI.Properties.RequestStatusPropertyCollection)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.RequestStatusProperty.ExtendedData),
[](@P:EWSoftware.PDI.Properties.RequestStatusProperty.StatusCode),
and [](@P:EWSoftware.PDI.Properties.RequestStatusProperty.StatusMessage)

</td>
      <td>iCalendar 2.0 only.</td>
    </tr>
    <tr>
      <td>

`RESOURCES`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.Resources)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.ResourcesProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.ResourcesProperty.Resources) and
[](@P:EWSoftware.PDI.Properties.ResourcesProperty.ResourcesString)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`RNUM`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.RecurrenceCount)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.RecurrenceCountProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.RecurrenceCountProperty.Count)

</td>
      <td>vCalendar 1.0 only.</td>
    </tr>
    <tr>
      <td>

`RRULE`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.RecurringObject.RecurrenceRules)

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

`SEQUENCE`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.Sequence)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.SequenceProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.SequenceProperty.SequenceNumber)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`STATUS`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.Status)

</td>
      <td>

[](@T:EWSoftware.PDI.Properties.StatusProperty)

</td>
      <td>

[](@P:EWSoftware.PDI.Properties.BaseAltRepProperty.AlternateRepresentation) and
[](@P:EWSoftware.PDI.Properties.StatusProperty.StatusValue)

</td>
      <td> </td>
    </tr>
    <tr>
      <td>

`SUMMARY`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.Summary)

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

`UID`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.UniqueId)

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

[](@P:EWSoftware.PDI.Objects.VToDo.Url)

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

[](@P:EWSoftware.PDI.Objects.VToDo.CustomProperties)

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

`BEGIN:VALARM` to `END:VALARM`

</td>
      <td>

[](@P:EWSoftware.PDI.Objects.VToDo.Alarms)

</td>
      <td>

[](@T:EWSoftware.PDI.Objects.VAlarm) and [](@T:EWSoftware.PDI.Objects.VAlarmCollection)

</td>
      <td> </td>
      <td>

iCalendar 2.0 only. See the [](@2728ddf1-da12-446d-9142-b4bc57540d24) topic for more information.

</td>
    </tr>
    <tr>
      <td>

`END:VTODO`

</td>
      <td> </td>
      <td>

[](@T:EWSoftware.PDI.Objects.VToDo) and [](@T:EWSoftware.PDI.Objects.VToDoCollection)

</td>
      <td> </td>
      <td> </td>
    </tr>
  </tbody>
</table>

## See Also
**Other Resources**  
[](@7ea5cd94-fa8a-4070-ba20-6d8c3b40d7c3)  
