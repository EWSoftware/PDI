'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : Global.asax.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 11/21/2021
' Note    : Copyright 2004-2021, Eric Woodruff, All rights reserved
'
' At application start up, a common set of time zones is loaded into the VCalendar.TimeZones collection and a
' common set of holidays is loaded into the Recurrence.Holidays collection.
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 01/25/2005  EFW  Created the code
'================================================================================================================

Imports EWSoftware.PDI
Imports EWSoftware.PDI.Objects
Imports EWSoftware.PDI.Parser

Namespace PDIWebDemoVB

    Public Class [Global]
        Inherits System.Web.HttpApplication

        ''' <summary>
        ''' Initialize the time zone and recurrence information at start up
        ''' </summary>
        ''' <param name="sender">The sender of the event</param>
        ''' <param name="e">The event arguments</param>
        Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
            ' Load the time zones if not already done.  The collection is static so it only needs to be loaded
            ' once.
            If VCalendar.TimeZones.Count = 0 Then
                ' Since it is static, we will use the Lock property to synchronize access to it in the web app as
                ' multiple sessions may try to access it at the same time.  The parser will acquire a write lock
                ' if it needs to merge a time zone component but since we are loading many time zones at once,
                ' we'll lock it now.
                VCalendar.TimeZones.AcquireWriterLock(250)

                Try
                    ' If still zero, load the file
                    If VCalendar.TimeZones.Count = 0 Then
                        VCalendarParser.ParseFromFile(Server.MapPath("TimeZoneDB.ics"))

                        VCalendar.TimeZones.Sort(true)
                    End If

                Finally
                    VCalendar.TimeZones.ReleaseWriterLock()
                End Try
            End If

            ' Load a default set of holidays into the recurrence holiday collection.  It too is static, but since
            ' it probably won't change after being set, it uses a simple SyncRoot property to lock the collection.
            SyncLock CType(Recurrence.Holidays, ICollection).SyncRoot
                If Recurrence.Holidays.Count = 0 Then
                    Recurrence.Holidays.AddStandardHolidays(New FixedHoliday(6, 19, True, "Juneteenth") With  { .MinimumYear = 2021 })
                End If
            End SyncLock
        End Sub

        ''' <summary>
        ''' Handle application errors
        ''' </summary>
        ''' <param name="sender">The sender of the event</param>
        ''' <param name="e">The event arguments</param>
        Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
            ' Nothing special yet.
        End Sub

    End Class

End Namespace
