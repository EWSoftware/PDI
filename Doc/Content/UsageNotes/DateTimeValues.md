---
uid: 99f67197-4d8b-45f7-bcea-3b81413a8eab
alt-uid: DateTimeValues
title: Working with Date/Time Values
keywords: "usage notes, working with date/time values"
---
When working with calendar properties that have a date/time component, the date/time value may be
represented in one of four ways:

- **Universal Time**, also referred to as UTC, Zulu time, absolute time, or
Greenwich Mean Time, is the standard time common to every place in the world.  UTC nominally reflects the mean
solar time along the Earth's prime meridian that runs through Greenwich, England.
- **Time Zone Time** is used to indicate a date/time value that is
associated with a specific time zone (i.e. Pacific Standard Time, Eastern Daylight Time, etc).  A time zone time
value is calculated by taking a UTC value and adding a positive or negative offset in hours and minutes.  If
Daylight Saving Time information is available, the offset can be adjusted accordingly to ensure that the time is
accurate based on whether or not Daylight Saving Time is in effect.  Negating the offset value and adding it to
the date/time will convert a time zone time to Universal Time.
- **Local Time** is used to represent date/time values expressed in the time
zone of the machine on which the application is currently running. UTC and time zone time values are converted to
local time by adding a positive or negative offset in hours and minutes to arrive at the local time value.
Daylight Saving Time information is also factored in if available.  Negating the offset value and adding it to
the date/time will convert a local time value to the time zone time or Universal Time.
- **Floating Time** is identical to local time and mainly applies to how a
date/time value is stored in an iCalendar file.  This term is used to indicate a date/time value stored without a
UTC indicator or time zone reference.  It is used to represent the same hour, minute, and second value regardless
of which time zone is currently being observed.  When such values are encountered, they are treated as if they
are in the machine's current time zone.

It is important to note the differences between these date/time representations.  This is
especially true when working with web applications.  In a desktop application running on a user's PC, local time
and the current user's time zone are one and the same.  However in a web based application, the web server may be
physically located in a different time zone from the client that is using the web application.  As such, you must
pay careful attention to any conversions that occur.  If you convert a user's appointment date/time to UTC, you
must convert the value based on the user's time zone rather than local time on the server. If not, you will end
up with an incorrect UTC date/time value.  For example, if a user is located in the Eastern time zone and the web
server is located in the Pacific time zone, the converted time would be off by three hours (the difference
between the Eastern and Pacific time zones).

Throughout the documentation for the PDI library, you will see several references to values being
calculated or expressed in local time.  This always refers to local time on the machine on which the application
is running which, as noted above, may not be the same time zone in which the user is located.  Where necessary,
you may need to convert such values between local time and a specific time zone or UTC.  The
[](@T:EWSoftware.PDI.Objects.VCalendar) object has
several static methods to help with these conversions:

- [](@M:EWSoftware.PDI.Objects.VCalendar.TimeZoneTimeToUtc(System.DateTime,System.String)){prefer-overload="true"} - 
  Convert a date/time value from the specified time zone time to Universal Time (UTC).
- [](@M:EWSoftware.PDI.Objects.VCalendar.UtcToTimeZoneTime(System.DateTime,System.String)){prefer-overload="true"} - 
  Convert a date/time value from Universal Time (UTC) to the specified time zone time.
- [](@M:EWSoftware.PDI.Objects.VCalendar.TimeZoneTimeToLocalTime(System.DateTime,System.String)){prefer-overload="true"} - 
  Convert a date/time value from the specified time zone time to local time.
- [](@M:EWSoftware.PDI.Objects.VCalendar.LocalTimeToTimeZoneTime(System.DateTime,System.String)){prefer-overload="true"} - 
  Convert a date/time value from local time to the specified time zone time.
- [](@M:EWSoftware.PDI.Objects.VCalendar.TimeZoneToTimeZone(System.DateTime,System.String,System.String)){prefer-overload="true"} - 
  Convert a date/time value from one time zone time to the another time zone time.

Date/time properties on calendar objects also have properties to return the value with an appropriate conversion
applied to it:

- [](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.TimeZoneDateTime) - Get or set the value as a date/time
  value expressed in the currently defined time zone ID.
- [](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.UtcDateTime) - Get or set the value as a date/time value
  expressed in Universal Time.
- [](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.DateTimeValue) - Get or set the value as a date/time value
  expressed in local time.
- [](@P:EWSoftware.PDI.Properties.BaseDateTimeProperty.IsFloating) - Get or set whether or not the date/time is a
  floating value.

## See Also
**Other Resources**  
[](@695cc5b6-0937-42cd-9e7a-2f8aab4f09b0)  
