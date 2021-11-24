'================================================================================================================
' System  : EWSoftware PDI Demonstration Applications
' File    : HolidayTestForm.aspx.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 11/23/2021
' Note    : Copyright 2004-2021, Eric Woodruff, All rights reserved
'
' This page is used to demonstrate the Holiday and date utility classes
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

Imports System.Globalization
Imports System.Xml.Serialization

Imports EWSoftware.PDI

Namespace PDIWebDemoVB

    Partial Class HolidayTestForm
        Inherits Page

        Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Me.Page.Title = "Holiday Detection Demo"

            ' Create a holiday collection with some default holidays, store it in the session, and bind it to the
            ' data grid.
            If Not Page.IsPostBack Then
                Dim hc As HolidayCollection = DirectCast(Session("Holidays"), HolidayCollection)

                ' If we haven't been here before, create a new collection
                If hc Is Nothing Then
                    hc = New HolidayCollection()
                    hc.AddStandardHolidays(New FixedHoliday(6, 19, True, "Juneteenth") With  { .MinimumYear = 2021 })
                    Session("Holidays") = hc
                End If

                dgHolidays.DataSource = hc
                dgHolidays.DataBind()

                txtFromYear.Text = (DateTime.Now.Year - 1).ToString()
                txtToYear.Text = (DateTime.Now.Year + 6).ToString()
                txtTestDate.Text = DateTime.Today.ToString("d")
            End If
        End Sub

        ' This handles various commands for the data grid
        Private Sub dgHolidays_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgHolidays.ItemCommand
            Dim hc As HolidayCollection

            Select Case e.CommandName
                Case "Add"
                    ' Save changes to the edited item if there is one
                    If dgHolidays.EditItemIndex <> -1 Then
                        dgHolidays_UpdateCommand(source, New DataGridCommandEventArgs(
                            dgHolidays.Items(dgHolidays.EditItemIndex), e.CommandSource, e))
                    End If

                    ' Ignore the request if the page is not valid
                    If Page.IsValid = False Then
                        Return
                    End If

                    ' Add a new holiday and go into edit mode on it
                    hc = DirectCast(Session("Holidays"), HolidayCollection)
                    hc.AddFixed(DateTime.Today.Month, DateTime.Today.Day, True, String.Empty)

                    dgHolidays.EditItemIndex = hc.Count - 1
                    dgHolidays.DataSource = hc
                    dgHolidays.DataBind()

                Case "Clear"
                    ' Clear all holidays
                    hc = DirectCast(Session("Holidays"), HolidayCollection)
                    hc.Clear()

                    dgHolidays.EditItemIndex = -1
                    dgHolidays.DataSource = hc
                    dgHolidays.DataBind()

                Case "Default"
                    ' Revert to the default set
                    hc = DirectCast(Session("Holidays"), HolidayCollection)
                    hc.Clear()
                    hc.AddStandardHolidays(New FixedHoliday(6, 19, True, "Juneteenth") With  { .MinimumYear = 2021 })

                    dgHolidays.EditItemIndex = -1
                    dgHolidays.DataSource = hc
                    dgHolidays.DataBind()

                Case "Download"
                    ' Save changes to the edited item if there is one
                    If dgHolidays.EditItemIndex <> -1 Then
                        dgHolidays_UpdateCommand(source, New DataGridCommandEventArgs(
                            dgHolidays.Items(dgHolidays.EditItemIndex), e.CommandSource, e))
                    End If

                    ' Ignore the request if the page is not valid
                    If Page.IsValid = False Then
                        Return
                    End If

                    hc = DirectCast(Session("Holidays"), HolidayCollection)

                    ' Send the file to the user as XML
                    Me.Response.ClearContent()
                    Me.Response.ContentType = "text/xml"
                    Me.Response.AppendHeader("Content-Disposition", "inline;filename=Holidays.xml")

                    Dim xs As New XmlSerializer(GetType(HolidayCollection))
                    xs.Serialize(Response.OutputStream, hc)

                    Response.End()

            End Select
        End Sub

        ' Edit a holiday in the collection
        Private Sub dgHolidays_EditCommand(source As Object, e As DataGridCommandEventArgs) Handles dgHolidays.EditCommand
            ' Ignore the request if the page is not valid
            If Page.IsValid = False Then
                Return
            End If

            ' Save changes to the prior edited item
            If dgHolidays.EditItemIndex <> -1 Then
                dgHolidays_UpdateCommand(source, New DataGridCommandEventArgs(
                    dgHolidays.Items(dgHolidays.EditItemIndex), e.CommandSource, e))

                If Page.IsValid = False Then
                    Return
                End If
            End If

            dgHolidays.EditItemIndex = e.Item.ItemIndex
            dgHolidays.DataSource = Session("Holidays")
            dgHolidays.DataBind()
        End Sub

        ' Delete a holiday from the collection
        Private Sub dgHolidays_DeleteCommand(source As Object, e As DataGridCommandEventArgs) Handles dgHolidays.DeleteCommand
            ' Save changes to the edited item if it isn't the one being deleted
            If dgHolidays.EditItemIndex <> -1 And dgHolidays.EditItemIndex <> e.Item.ItemIndex Then
                Page.Validate()
                dgHolidays_UpdateCommand(source, New DataGridCommandEventArgs(
                    dgHolidays.Items(dgHolidays.EditItemIndex), e.CommandSource, e))

                If Page.IsValid = False Then
                    Return
                End If
            End If

            Dim hc As HolidayCollection = DirectCast(Session("Holidays"), HolidayCollection)

            hc.RemoveAt(e.Item.ItemIndex)
            dgHolidays.EditItemIndex = -1
            dgHolidays.DataSource = hc
            dgHolidays.DataBind()
        End Sub

        ' Cancel changes to a holiday in the collection
        Private Sub dgHolidays_CancelCommand(source As Object, e As DataGridCommandEventArgs) Handles dgHolidays.CancelCommand
            Dim hc As HolidayCollection = DirectCast(Session("Holidays"),  HolidayCollection)

            ' If it was a new item, remove it
            If String.IsNullOrWhiteSpace(hc(e.Item.ItemIndex).Description) Then
                hc.RemoveAt(e.Item.ItemIndex)
            End If

            dgHolidays.EditItemIndex = -1
            dgHolidays.DataSource = hc
            dgHolidays.DataBind()
        End Sub

        ' Update a holiday item in the collection
        Private Sub dgHolidays_UpdateCommand(source As Object, e As DataGridCommandEventArgs) Handles dgHolidays.UpdateCommand
            Dim hc As HolidayCollection = DirectCast(Session("Holidays"),  HolidayCollection)

            If Page.IsValid = False Then
                Return
            End If

            Dim cboMonth As DropDownList = DirectCast(e.Item.FindControl("cboMonth"), DropDownList)
            Dim rbFloating As RadioButton = DirectCast(e.Item.FindControl("rbFloating"), RadioButton)

            ' Create holiday object and replace it in the collection
            If rbFloating.Checked = True Then
                Dim fl As New FloatingHoliday With {
                    .Month = cboMonth.SelectedIndex + 1,
                    .Description = DirectCast(e.Item.FindControl("txtDescription"), TextBox).Text,
                    .MinimumYear = Convert.ToInt32(DirectCast(e.Item.FindControl("txtMinimumYear"), TextBox).Text),
                    .MaximumYear = Convert.ToInt32(DirectCast(e.Item.FindControl("txtMaximumYear"), TextBox).Text),
                    .Occurrence = CType(DirectCast(e.Item.FindControl("cboOccurrence"),
                    DropDownList).SelectedIndex + 1, DayOccurrence),
                    .Weekday = CType(DirectCast(e.Item.FindControl("cboDayOfWeek"), DropDownList).SelectedIndex,
                    DayOfWeek),
                    .Offset = Convert.ToInt32(DirectCast(e.Item.FindControl("txtOffset"), TextBox).Text)
                }

                hc(e.Item.ItemIndex) = fl
            Else
                ' See if the day of the month is valid for the month.  We won't accept Feb 29th either.
                Dim day As Integer = Convert.ToInt32(DirectCast(
                    e.Item.FindControl("txtDayOfMonth"), TextBox).Text)

                If day > DateTime.DaysInMonth(2007, cboMonth.SelectedIndex + 1) Then
                    DirectCast(e.Item.FindControl("rvDOM"), RangeValidator).IsValid = False
                    Return
                End If

                Dim fx As New FixedHoliday With {
                    .Month = cboMonth.SelectedIndex + 1,
                    .Description = DirectCast(e.Item.FindControl("txtDescription"), TextBox).Text,
                    .MinimumYear = Convert.ToInt32(DirectCast(e.Item.FindControl("txtMinimumYear"), TextBox).Text),
                    .MaximumYear = Convert.ToInt32(DirectCast(e.Item.FindControl("txtMaximumYear"), TextBox).Text),
                    .AdjustFixedDate = DirectCast(e.Item.FindControl("chkAdjustDate"), CheckBox).Checked,
                    .Day = Convert.ToInt32(DirectCast(e.Item.FindControl("txtDayOfMonth"), TextBox).Text)
                }

                hc(e.Item.ItemIndex) = fx
            End If

            dgHolidays.EditItemIndex = -1
            dgHolidays.DataSource = hc
            dgHolidays.DataBind()
        End Sub

        ' Bind data to the edit item template.  Since the holiday collection  uses the abstract class, we'll bind
        ' data in here rather than in the HTML since we have to determine the type first.
        Private Sub dgHolidays_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgHolidays.ItemDataBound
            If e.Item.ItemType = ListItemType.EditItem Then
                ' The RecurOptsDataSource class contains static methods that return lists of common values we can
                ' uses as the data sources for these drop down lists.
                Dim cboMonth As DropDownList = DirectCast(e.Item.FindControl("cboMonth"), DropDownList)
                cboMonth.DataSource = RecurOptsDataSource.MonthsOfYear
                cboMonth.DataTextField = "Display"
                cboMonth.DataValueField = "Value"
                cboMonth.DataBind()

                Dim cboOccurrence As DropDownList = DirectCast(e.Item.FindControl("cboOccurrence"), DropDownList)
                cboOccurrence.DataSource = RecurOptsDataSource.DayOccurrences
                cboOccurrence.DataTextField = "Display"
                cboOccurrence.DataValueField = "Value"
                cboOccurrence.DataBind()

                Dim cboDayOfWeek As DropDownList = DirectCast(e.Item.FindControl("cboDayOfWeek"), DropDownList)
                cboDayOfWeek.DataSource = RecurOptsDataSource.DayOfWeek
                cboDayOfWeek.DataTextField = "Display"
                cboDayOfWeek.DataValueField = "Value"
                cboDayOfWeek.DataBind()

                Dim rbFloating As RadioButton = DirectCast(e.Item.FindControl("rbFloating"), RadioButton)
                Dim rbFixed As RadioButton = DirectCast(e.Item.FindControl("rbFixed"), RadioButton)

                Dim hc As HolidayCollection = DirectCast(Session("Holidays"), HolidayCollection)

                If TypeOf hc(e.Item.ItemIndex) Is FloatingHoliday Then
                    Dim fl As FloatingHoliday = DirectCast(hc(e.Item.ItemIndex), FloatingHoliday)

                    cboOccurrence.SelectedIndex = CType(fl.Occurrence, Integer) - 1
                    cboDayOfWeek.SelectedIndex = CType(fl.Weekday, Integer)

                    Dim offset As Integer = fl.Offset

                    If offset < -999 Then
                        offset = -999
                    Else
                        If offset > 999 Then
                            offset = 999
                        End If
                    End If

                    DirectCast(e.Item.FindControl("txtOffset"), TextBox).Text = offset.ToString()

                    rbFloating.Checked = True
                    rbFixed.Checked = False
                    DirectCast(e.Item.FindControl("txtDayOfMonth"), TextBox).Enabled = False
                    DirectCast(e.Item.FindControl("rfvDOM"), RequiredFieldValidator).Enabled = False
                    DirectCast(e.Item.FindControl("rvDOM"), RangeValidator).Enabled = False
                    DirectCast(e.Item.FindControl("chkAdjustDate"), CheckBox).Enabled = False
                    DirectCast(e.Item.FindControl("txtDayOfMonth"), TextBox).CssClass = "Disabled"
                Else
                    Dim fx As FixedHoliday = DirectCast(hc(e.Item.ItemIndex), FixedHoliday)

                    DirectCast(e.Item.FindControl("txtOffset"), TextBox).Text = "0"
                    DirectCast(e.Item.FindControl("txtDayOfMonth"), TextBox).Text = fx.Day.ToString()
                    DirectCast(e.Item.FindControl("chkAdjustDate"), CheckBox).Checked = fx.AdjustFixedDate

                    rbFloating.Checked = False
                    rbFixed.Checked = True
                    cboOccurrence.Enabled = cboDayOfWeek.Enabled = False
                    DirectCast(e.Item.FindControl("txtOffset"), TextBox).Enabled = False
                    DirectCast(e.Item.FindControl("rfvOffset"), RequiredFieldValidator).Enabled = False
                    DirectCast(e.Item.FindControl("rvOffset"), RangeValidator).Enabled = False
                    cboOccurrence.CssClass = "Disabled"
                    cboDayOfWeek.CssClass = "Disabled"
                    DirectCast(e.Item.FindControl("txtOffset"), TextBox).CssClass = "Disabled"
                End If

                cboMonth.SelectedIndex = hc(e.Item.ItemIndex).Month - 1
                DirectCast(e.Item.FindControl("txtDescription"), TextBox).Text = hc(e.Item.ItemIndex).Description

                Dim minYear As Integer = hc(e.Item.ItemIndex).MinimumYear,
                    maxYear As Integer = hc(e.Item.ItemIndex).MaximumYear

                If minYear < 1 Then
                    minYear = 1
                Else
                    If minYear > 9999 Then
                        minYear = 9999
                    End If
                End If

                If maxYear < 1 Then
                    maxYear = 1
                Else
                    If maxYear > 9999 Then
                        maxYear = 9999
                    End If
                End If

                DirectCast(e.Item.FindControl("txtMinimumYear"), TextBox).Text = minYear.ToString()
                DirectCast(e.Item.FindControl("txtMaximumYear"), TextBox).Text = maxYear.ToString()

            End If
        End Sub

        ' Enable or disable the controls when the radio buttons are clicked
        Public Sub Type_CheckChanged(sender As Object, e As EventArgs)
            Dim dgi As DataGridItem = dgHolidays.Items(dgHolidays.EditItemIndex)

            Dim rbFloating As RadioButton = DirectCast(dgi.FindControl("rbFloating"), RadioButton)
            Dim rbFixed As RadioButton = DirectCast(dgi.FindControl("rbFixed"), RadioButton)
            Dim enabled As Boolean = Not (CType(sender, RadioButton) Is rbFixed)
            Dim floatClass, fixedClass As String

            rbFloating.Checked = enabled
            rbFixed.Checked = Not enabled

            If enabled = True Then
                floatClass = String.Empty
                fixedClass = "Disabled"
            Else
                floatClass = "Disabled"
                fixedClass = String.Empty
            End If

            DirectCast(dgi.FindControl("cboOccurrence"), DropDownList).Enabled = enabled
            DirectCast(dgi.FindControl("cboDayOfWeek"), DropDownList).Enabled = enabled
            DirectCast(dgi.FindControl("txtOffset"), TextBox).Enabled = enabled
            DirectCast(dgi.FindControl("rfvOffset"), RequiredFieldValidator).Enabled = enabled
            DirectCast(dgi.FindControl("rvOffset"), RangeValidator).Enabled = enabled

            DirectCast(dgi.FindControl("txtDayOfMonth"), TextBox).Enabled = Not enabled
            DirectCast(dgi.FindControl("rfvDOM"), RequiredFieldValidator).Enabled = Not enabled
            DirectCast(dgi.FindControl("rvDOM"), RangeValidator).Enabled = Not enabled
            DirectCast(dgi.FindControl("chkAdjustDate"), CheckBox).Enabled = Not enabled

            DirectCast(dgi.FindControl("cboOccurrence"), DropDownList).CssClass = floatClass
            DirectCast(dgi.FindControl("cboDayOfWeek"), DropDownList).CssClass = floatClass
            DirectCast(dgi.FindControl("txtOffset"), TextBox).CssClass = floatClass

            DirectCast(dgi.FindControl("txtDayOfMonth"), TextBox).CssClass = fixedClass
        End Sub

        ' Find holidays defined by the holiday collection in the given range
        ' of years.
        Private Sub btnFindHolidays_Click(sender As Object, e As EventArgs) Handles btnFindHolidays.Click

            Dim hc As HolidayCollection = DirectCast(Session("Holidays"), HolidayCollection)
            Dim dt As DateTime
            Dim yearFrom, yearTo, tempYear As Integer

            If Page.IsValid = False Then
                Return
            End If

            yearFrom = Convert.ToInt32(txtFromYear.Text)
            yearTo = Convert.ToInt32(txtToYear.Text)

            If yearFrom > yearTo Then
                tempYear = yearFrom
                yearFrom = yearTo
                yearTo = tempYear
            End If

            ' Limit the range to 50 years in the web app to prevent using too many resources on the server
            If yearTo > yearFrom + 49 Then
                yearTo = yearFrom + 49
            End If

            txtFromYear.Text = yearFrom.ToString()
            txtToYear.Text = yearTo.ToString()

            lbDates.Items.Clear()
            Dim dcDates As List(Of DateTime) = hc.HolidaysBetween(yearFrom, yearTo).ToList()

            If dcDates.Count > 0 Then
                For Each dt In dcDates
                    lbDates.Items.Add(String.Format("{0:d} - {1}", dt, hc.HolidayDescription(dt)))
                Next
            End If
        End Sub

        ' Find all instances of Easter using the selected method in the given range of years
        Private Sub btnFindEaster_Click(sender As Object, e As EventArgs) Handles btnFindEaster.Click
            Dim em As EasterMethod
            Dim yearFrom, yearTo, tempYear As Integer
            Dim desc As String

            If Page.IsValid = False Then
                Return
            End If

            em = CType(rblEasterMethod.SelectedIndex, EasterMethod)
            yearFrom = Convert.ToInt32(txtFromYear.Text)
            yearTo = Convert.ToInt32(txtToYear.Text)

            ' Adjust years as necessary based on the method
            If em <> EasterMethod.Julian Then
                If yearFrom < 1583 Then yearFrom = 1583
                If yearFrom > 4099 Then yearFrom = 4099
                If yearTo < 1583 Then yearTo = 1583
                If yearTo > 4099 Then yearTo = 4099
            Else
                If yearFrom < 326 Then yearFrom = 326
                If yearTo < 326 Then yearTo = 326
            End If

            If yearFrom > yearTo Then
                tempYear = yearFrom
                yearFrom = yearTo
                yearTo = tempYear
            End If

            ' Limit the range to 50 years in the web app to prevent using too many resources on the server
            If yearTo > yearFrom + 49 Then yearTo = yearFrom + 49

            txtFromYear.Text = yearFrom.ToString()
            txtToYear.Text = yearTo.ToString()

            desc = String.Format("Easter ({0})", em.ToString())
            lbDates.Items.Clear()

            Do While yearFrom <= yearTo
                lbDates.Items.Add(String.Format("{0:d} - {1}", DateUtils.EasterSunday(yearFrom, em), desc))
                yearFrom += 1
            Loop
        End Sub

        ' Test to see if the entered date is a holiday based on the set defined in the collection
        Private Sub btnIsHoliday_Click(sender As Object, e As EventArgs) Handles btnIsHoliday.Click
            If Page.IsValid Then
                Dim hc As HolidayCollection = DirectCast(Session("Holidays"), HolidayCollection)

                If hc.IsHoliday(Convert.ToDateTime(txtTestDate.Text, CultureInfo.CurrentCulture)) = True Then
                    lblIsHoliday.Text = "The test date is a defined holiday"
                Else
                    lblIsHoliday.Text = "The test date is not a defined holiday"
                End If
            End If
        End Sub

    End Class

End Namespace
