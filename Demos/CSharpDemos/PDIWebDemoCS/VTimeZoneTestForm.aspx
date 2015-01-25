<%@ Page MasterPageFile="~/MasterPage.master" Language="c#" Inherits="PDIWebDemoCS.VTimeZoneTestForm" CodeFile="VTimeZoneTestForm.aspx.cs" %>

<asp:Content ID="pageContent" runat="server" ContentPlaceHolderID="cpContent">

	<p>This page is used to demonstrate some of the time zone features of the PDI classes.</p>

	<div class="alert alert-info">
		<p><strong>NOTE</strong>: Standard/Daylight time shift calculations utilize the recurrence engine.  The time
zone information is based on information provided by the operating system.  It will be accurate for current year
and future dates but may not provide accurate dates for prior years as it does not supply rules for historical
dates.  Rules can be added manually if needed for prior years.</p>
	</div>

	<div class="panel panel-info">
		<div class="panel-heading">
			<h1 class="panel-title">Source Date/Time</h1>
		</div>
		<div class="panel-body">
			<table class="table-noborder">
				<tr>
					<td class="control-label">Source Date</td>
					<td colspan="2"><asp:TextBox ID="txtSourceDate" runat="server" CssClass="form-control" /></td>
				</tr>
				<tr>
					<td class="control-label">Source Time Zone</td>
					<td>
						<asp:DropDownList ID="cboSourceTimeZone" runat="server" CssClass="form-control" /></td>
					<td>
						<asp:Button ID="btnApplySrc" runat="server" CssClass="btn btn-default"
							Text="Apply" ToolTip="Apply time zone to date/times" OnClick="btnApplySrc_Click" /></td>
				</tr>
			</table>
		</div>
	</div>

	<div class="panel panel-info">
		<div class="panel-heading">
			<h1 class="panel-title">To Local (Server) Time</h1>
		</div>
		<div class="panel-body">
			<table class="table-noborder">
				<tr>
					<td class="control-label">Local (Server) Time</td>
					<td><asp:Label ID="lblLocalTime" runat="server" />&nbsp;</td>
				</tr>
				<tr>
					<td class="control-label">Back to Source</td>
					<td><asp:Label ID="lblLocalBackToSource" runat="server" />&nbsp;</td>
				</tr>
			</table>
		</div>
	</div>

	<div class="panel panel-info">
		<div class="panel-heading">
			<h1 class="panel-title">To Other Time Zone</h1>
		</div>
		<div class="panel-body">
			<table class="table-noborder">
				<tr>
					<td class="control-label">Destination Time Zone</td>
					<td>
						<asp:DropDownList ID="cboDestTimeZone" runat="server" CssClass="form-control" /></td>
					<td>
						<asp:Button ID="btnApplyDest" runat="server" CssClass="btn btn-default"
							Text="Apply" ToolTip="Apply time zone to date/times" OnClick="btnApplySrc_Click" /></td>
				</tr>
				<tr>
					<td class="control-label">Dest. Time</td>
					<td colspan="2"><asp:Label ID="lblDestTime" runat="server" />&nbsp;</td>
				</tr>
				<tr>
					<td class="control-label">Back to Source</td>
					<td colspan="2"><asp:Label ID="lblDestBackToSource" runat="server" />&nbsp;</td>
				</tr>
			</table>
		</div>
	</div>

	<div class="panel panel-info">
		<div class="panel-heading">
			<h1 class="panel-title">Time Zone Settings</h1>
		</div>
		<div class="panel-body">
			<pre><asp:Label ID="lblTimeZoneInfo" Runat="server" /></pre>
			<asp:Button ID="btnGetTZs" runat="server" CssClass="btn btn-default" Text="Download All"
				ToolTip="Download the time zone information in iCalendar format" OnClick="btnGetTZs_Click" />
		</div>
	</div>

</asp:Content>
