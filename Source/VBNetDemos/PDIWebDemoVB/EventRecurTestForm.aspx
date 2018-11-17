<%@ Page MasterPageFile="~/MasterPage.master" Language="VB" Inherits="PDIWebDemoVB.EventRecurTestForm" CodeFile="EventRecurTestForm.aspx.vb" %>

<asp:Content ID="pageContent" runat="server" ContentPlaceHolderID="cpContent">

	<p>This page is used to demonstrate the recurrence engine when used with iCalendar components including support
for time zones.</p>

	<div class="alert alert-info">
		<p><strong>NOTE:</strong> To prevent overloading the server or causing time-outs, the web demo limits
recurrences to a maximum of 5000 instances if a maximum is specified and for minutely and secondly frequencies,
5 years for the hourly frequency, and 50 years for frequencies greater than hourly.</p>
	</div>

	<div class="panel panel-info">
		<div class="panel-heading">
			<h1 class="panel-title">Options</h1>
		</div>
		<div class="panel-body">
			<table class="table-noborder">
				<tr>
					<td class="control-label">Find instances between</td>
					<td>
						<asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" />&nbsp;&nbsp; and &nbsp;&nbsp;
						<asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" /></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>
						<asp:CheckBox ID="chkInLocalTime" runat="server" Text="Find instances in local (server) time" /></td>
					<tr>
						<td class="control-label">Apply this time zone to the component</td>
						<td>
							<asp:DropDownList ID="cboTimeZone" runat="server" CssClass="form-control" /></td>
					</tr>
					<tr>
						<td colspan="2" class="control-label-left">Enter a VEVENT, VTODO, or VJOURNAL entry below</td>
					</tr>
					<tr>
						<td colspan="2">
							<asp:TextBox ID="txtCalendar" runat="server" TextMode="MultiLine"
								Columns="100" Rows="15" Wrap="False" CssClass="form-control" /><br />
							<br />
							<asp:Button ID="btnTest" runat="server" Text="Test" CssClass="btn btn-default"
								ToolTip="Generate instances" /></td>
					</tr>
			</table>
		</div>
	</div>

	<div class="panel panel-info">
		<div class="panel-heading">
			<h1 class="panel-title">Results</h1>
		</div>
		<div class="panel-body">
			<table class="table-noborder">
				<tr>
					<td><asp:Label ID="lblCount" runat="server">--</asp:Label>&nbsp;</td>
				</tr>
				<tr>
					<td>
						<asp:DataList ID="dlDates" runat="server" EnableViewState="False">
							<ItemTemplate>
								<%# DataBinder.Eval(Container.DataItem, "StartDateTime", "{0:G}") %>
								<%# DataBinder.Eval(Container.DataItem, "AbbreviatedStartTimeZoneName") %>&nbsp;to&nbsp;
								<%# DataBinder.Eval(Container.DataItem, "EndDateTime", "{0:G}") %>
								<%# DataBinder.Eval(Container.DataItem, "AbbreviatedEndTimeZoneName") %>&nbsp;(
								<%# DirectCast(DataBinder.Eval(Container.DataItem, "Duration"), EWSoftware.PDI.Duration).ToDescription() %>)
							</ItemTemplate>
						</asp:DataList>&nbsp;</td>
				</tr>
			</table>
		</div>
	</div>

</asp:Content>
