---
uid: f625ad22-7dbf-47b5-a26a-49759ca674c4
alt-uid: InteractOutlook
title: Interaction with Microsoft Outlook
keywords: "usage notes, interactions with Microsoft Outlook", "usage notes, Microsoft Outlook"
---
The following behaviors where observed while testing the vCard and calendar files with Microsoft Outlook 2003
and later.

<dl>
  <dt>Importing and Exporting vCard Files</dt>
  <dd>

Outlook will import vCard 2.1 or 3.0 format files.  However, it only exports vCard 2.1 format files.
    
If a file contains multiple vCards, only the first one is imported. All subsequent ones are discarded.
    
If a time is specified on the Birth Date (`BDAY`) property, the time part is discarded.  In a vCard 2.1 file,
the `BDAY` property is not imported correctly if a time is present unless the value uses the extended
date/time format that includes separators.  Date-only values are imported correctly with or without
separators.
    
Outlook does not appear to import or export the `GEO`, `PHOTO`, `LOGO`, `TZ`, or `UID` properties.  It also
discards custom properties.
    
Outlook uses a parameter to classify `URL` properties which does not conform to either specification.
Without the identifier, the URL ends up in the "All Fields" tab as a "Personal Home Page" entry on the
"Frequently-used Fields" and "All Contact Fields" selections.  This can be worked around by adding `WORK` to
the `CustomParameters` property of the vCard's `URL` property.
    
If an address is entered on more than two lines, Outlook will not be able to export it correctly in the `ADR`
tag.  Instead of breaking the field up into its component parts, it will use Quoted-Printable encoding and
join some parts into the street field as literal text with carriage returns and line feeds.  The fix is to
put the address on two lines in Outlook.  For example:

<table>
  <thead>
    <tr>
      <th>Instead of:</th>
      <th>Enter it as:</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>
        <code language="none" title=" ">
        16719 Lakeside Drive
        Medical Lake
        WA 99022
        </code>
       </td>
      <td>
        <code language="none" title=" ">
        16719 Lakeside Drive
        Medical Lake WA 99022
        </code>
      </td>
    </tr>
  </tbody>
</table>

  </dd>
  <dt>Importing and Exporting Calendar Files</dt>
  <dd>

Outlook cannot save recurring events in vCalendar format.   Non-recurring events can be saved in vCalendar format.
    
Outlook will import events with recurrence information from vCalendar files but the recurrence information is
discarded and they become non-recurring events.  Recurrence information in iCalendar files is retained.
    
Outlook only supports duration values on `VALARM` trigger properties.  It does not support date/time trigger
values.
    
Outlook will not import `ATTENDEE` entries from events.
    
It may be necessary to add a `METHOD` property to iCalendar 2.0 files so that they import correctly (i.e.
`METHOD: PUBLISH`).  This can be accomplished by setting the
[](@P:EWSoftware.PDI.Objects.VCalendar.Method){show-container="true"} property to `CalendarMethod.Publish`.

  </dd>
</dl>

## See Also
**Other Resources**  
[](@695cc5b6-0937-42cd-9e7a-2f8aab4f09b0)  
