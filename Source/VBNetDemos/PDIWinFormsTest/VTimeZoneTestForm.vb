'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : VTimeZoneTestForm.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/02/2015
' Note    : Copyright 2004-2015, Eric Woodruff, All rights reserved
' Compiler: Visual Basic .NET
'
' This is a simple demonstration used to test the EWSoftware PDI time zone classes and methods.  This
' demonstration depends on the time zone information present in the Windows registry.
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 12/02/2004  EFW  Created the code
'================================================================================================================

Imports System.ComponentModel
Imports System.Globalization
Imports System.IO

Imports EWSoftware.PDI
Imports EWSoftware.PDI.Objects

''' <summary>
''' This form is used to test the time zone classes and methods
''' </summary>
Public Partial Class VTimeZoneTestForm
    Inherits System.Windows.Forms.Form

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New()
        MyBase.New()

        InitializeComponent()

        ' Set the short date/long time pattern based on the current culture
        dtpSourceDate.CustomFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern & " " &
            CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern
    End Sub

    ''' <summary>
    ''' Convert the selected date/time to local time and back and to the selected time zone and back to test the
    ''' time zone conversion code.
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub UpdateTimes(sender As Object, e As System.EventArgs) _
      Handles cboSourceTimeZone.SelectedIndexChanged, _
      cboDestTimeZone.SelectedIndexChanged, dtpSourceDate.ValueChanged
        ' Wait until both have a value selected
        If cboSourceTimeZone.SelectedIndex = -1 Or cboDestTimeZone.SelectedIndex = -1 Then
            Return
        End If

        Dim vtzSource As VTimeZone = VCalendar.TimeZones(cboSourceTimeZone.SelectedIndex)
        Dim vtzDest As VTimeZone = VCalendar.TimeZones(cboDestTimeZone.SelectedIndex)

        ' Show information for the selected time zones
        txtTimeZoneInfo.Clear()
        txtTimeZoneInfo.AppendText("From Time Zone:" & Environment.NewLine)
        txtTimeZoneInfo.AppendText(vtzSource.ToString())
        txtTimeZoneInfo.AppendText(Environment.NewLine)
        txtTimeZoneInfo.AppendText("To Time Zone:" & Environment.NewLine)
        txtTimeZoneInfo.AppendText(vtzDest.ToString())

        ' Do the conversions.  Each is round tripped to make sure it gets the expected results.  Note that there
        ' will be some anomalies when round tripping times that are near the standard time/DST shift as these
        ' times are somewhat ambiguous and their meaning can vary depending on which side of the shift you are on
        ' and the direction of the conversion.
        Dim dt As DateTime = dtpSourceDate.Value

        Dim dti As DateTimeInstance = VCalendar.TimeZoneTimeToLocalTime(dt, vtzSource.TimeZoneId.Value)

        lblLocalTime.Text = String.Format("{0} {1}", dti.StartDateTime, dti.StartTimeZoneName)

        dti = VCalendar.LocalTimeToTimeZoneTime(dti.StartDateTime, vtzSource.TimeZoneId.Value)
        lblLocalBackToSource.Text = String.Format("{0} {1}", dti.StartDateTime, dti.StartTimeZoneName)

        dti = VCalendar.TimeZoneToTimeZone(dt, vtzSource.TimeZoneId.Value, vtzDest.TimeZoneId.Value)
        lblDestTime.Text = String.Format("{0} {1}", dti.StartDateTime, dti.StartTimeZoneName)

        dti = VCalendar.TimeZoneToTimeZone(dti.StartDateTime, vtzDest.TimeZoneId.Value, vtzSource.TimeZoneId.Value)
        lblDestBackToSource.Text = String.Format("{0} {1}", dti.StartDateTime, dti.StartTimeZoneName)
    End Sub

    ''' <summary>
    ''' This loads the time zone information from the registry when the form opens
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub VTimeZoneTestForm_Load(ByVal sender As Object, _
      ByVal e As System.EventArgs) Handles MyBase.Load
        TimeZoneRegInfo.LoadTimeZoneInfo()

        ' VTimeZoneCollection is bindable.  Since they will share the same data source, give each combo box its
        ' own binding context.
        cboSourceTimeZone.BindingContext = new BindingContext()
        cboDestTimeZone.BindingContext = new BindingContext()

        ' To access a child property, separate the child property name from the parent property name with an
        ' underscore.
        cboSourceTimeZone.DisplayMember = "TimeZoneId_Value"
        cboDestTimeZone.DisplayMember = "TimeZoneId_Value"
        cboSourceTimeZone.ValueMember = "TimeZoneId_Value"
        cboDestTimeZone.DisplayMember = "TimeZoneId_Value"

        cboSourceTimeZone.DataSource = VCalendar.TimeZones
        cboDestTimeZone.DataSource = VCalendar.TimeZones

        dtpSourceDate.Value = new DateTime(DateTime.Today.Year, 1, 1, 10, 0, 0)
        UpdateTimes(sender, e)
    End Sub

    ''' <summary>
    ''' Clear out the time zone collection so as not to affect any of the other test forms
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub VTimeZoneTestForm_Closing(ByVal sender As Object, _
      ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        ' Clear out the time zone collection so as not to affect any of the other test forms
        VCalendar.TimeZones.Clear()
    End Sub

    ''' <summary>
    ''' Save time zone information to a calendar file for use as a "time zone database"
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnSaveTZs_Click(sender As Object, e As System.EventArgs) _
      Handles btnSaveTZs.Click
        Dim tz As VTimeZone

        Using dlg As New SaveFileDialog()
            dlg.Title = "Save Time Zone Database"
            dlg.Filter = "ICS files (*.ics)|*.ics|All files (*.*)|*.*"
            dlg.DefaultExt = "ics"
            dlg.FilterIndex = 1
            dlg.InitialDirectory = Environment.CurrentDirectory
            dlg.FileName = "TimeZoneDB.ics"

            If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Try
                    Me.Cursor = Cursors.WaitCursor

                    ' Open the file and write the time zone data to it
                    Using sw As New StreamWriter(dlg.FileName)
                        ' Since we are writing out the time zone collection by itself, we'll need to provide the
                        ' calender wrapper.
                        sw.WriteLine("BEGIN:VCALENDAR")
                        sw.WriteLine("VERSION:2.0")
                        sw.WriteLine("PRODID:-//EWSoftware//PDI Class Library//EN")

                        For Each tz In VCalendar.TimeZones
                            tz.WriteToStream(sw)
                        Next

                        sw.WriteLine("END:VCALENDAR")
                    End Using

                Catch ex As Exception
                    Dim errorMsg As String = String.Format("Unable to save time zone info:{0}{1}",
                        Environment.NewLine, ex.Message)

                    If Not (ex.InnerException Is Nothing) Then
                        errorMsg &= ex.InnerException.Message & Environment.NewLine

                        If Not (ex.InnerException.InnerException Is Nothing) Then
                            errorMsg &= ex.InnerException.InnerException.Message
                        End If
                    End If

                    System.Diagnostics.Debug.Write(ex)
                    MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    Me.Cursor = Cursors.Default
                End Try
            End If
        End Using
    End Sub

End Class
