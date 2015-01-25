<%@ Page MasterPageFile="~/MasterPage.master" Language="VB" Inherits="PDIWebDemoVB.RRuleTestForm" CodeFile="RRuleTestForm.aspx.vb" %>
<%@ Register TagPrefix="EWSPDI" Namespace="EWSoftware.PDI.Web.Controls" Assembly="EWSoftware.PDI.Web.Controls" %>

<asp:Content ID="pageContent" runat="server" ContentPlaceHolderID="cpContent">

	<p>This page is used to demonstrate the recurrence engine by entering iCalendar RRULE values.  You can use the
Recurrence web server control to edit the settings in a user-friendly fashion or enter RRULE text directly with
the text box below it.  Clicking the <strong>Test</strong> button below either one will generate instances based
on the specified settings.  The other control will be updated to reflect the settings used based on which button
is clicked.</p>

	<div class="alert alert-info">
		<p><strong>NOTE:</strong> To prevent overloading the server or causing time-outs, the web demo limits
recurrences to a maximum of 5000 instances if a maximum is specified and for minutely and secondly frequencies,
5 years for the hourly frequency, and 50 years for frequencies greater than hourly.</p>
	</div>

	<div class="panel panel-info">
		<div class="panel-heading">
			<h1 class="panel-title">Recurrence Start</h1>
		</div>
		<div class="panel-body">
			<table class="table-noborder">
				<tr>
					<td class="control-label">Start Date</td>
					<td>
						<asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" /></td>
					<td>&nbsp;</td>
				</tr>
			</table>
		</div>
	</div>

	<h3>Use the Pattern Editor</h3>

	<div class="panel panel-info">
		<div class="panel-body">

			<EWSPDI:RecurrencePattern id="rpRecurrence" runat="Server" CssButton="btn btn-default btn-min-width"
				CssErrorMessage="text-danger" CssInput="form-control" CssSelect="form-control"
				CssPanel="panel panel-info" CssPanelHeading="panel-heading" CssPanelBody="panel-body"
				CssPanelTitle="panel-title" />

			<p><asp:Button ID="btnTestPattern" runat="server" CssClass="btn btn-default"
				Text="Test" ToolTip="Generate instances using pattern" /></p>
		</div>

	</div>

	<h3>Or Enter a Recurrence Pattern String</h3>

	<div class="panel panel-info">
		<div class="panel-heading">
			<h1 class="panel-title">Recurrence Pattern</h1>
		</div>
		<div class="panel-body">
			<table class="table-noborder">
				<tr>
					<td class="control-label" style="vertical-align: top;">RRULE</td>
					<td>
						<asp:TextBox ID="txtRRULE" runat="server" TextMode="MultiLine" CssClass="form-control"
							Columns="75" Rows="5" /></td>
				</tr>
				<tr>
					<td class="control-label">Description</td>
					<td><asp:Label ID="lblDescription" runat="server" />&nbsp;</td>
				</tr>
				<tr>
					<td colspan="2">
						<asp:Button ID="btnTest" runat="server" CssClass="btn btn-default"
							Text="Test" ToolTip="Generate instances using RRULE" /></td>
				</tr>
			</table>
		</div>
	</div>

	<div class="panel panel-info">
		<div class="panel-heading">
			<h1 class="panel-title">Recurrence Date Generation Results</h1>
		</div>
		<div class="panel-body">
			<table class="table-noborder">
				<tr>
					<th>Defined Recurrence Holidays</th>
					<th>Results</th>
				</tr>
				<tr>
					<td><p>To exclude instances that fall on the following holidays, add the option
<strong>X-EWSOFTWARE-OCCURONHOL=0</strong> to the rule.  For example:</p>
						
						<p>FREQ=DAILY;X-EWSOFTWARE-OCCURONHOL=0; COUNT=50</p>

					</td>
					<td style="vertical-align: top;"><asp:Label ID="lblCount" runat="server" />&nbsp;</td>
				</tr>
				<tr>
					<td style="vertical-align: top; width: 50%;">
						<asp:ListBox Rows="15" ID="lbHolidays" runat="server" CssClass="form-control" /></td>
					<td style="vertical-align: top;">
						<asp:ListBox Rows="20" ID="lbResults" runat="server" EnableViewState="False"
							CssClass="form-control"  Width="250" /></td>
				</tr>
			</table>
		</div>
	</div>

</asp:Content>
