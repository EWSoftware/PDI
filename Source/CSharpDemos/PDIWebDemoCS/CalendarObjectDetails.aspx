<%@ Page ValidateRequest="False" Language="c#" Inherits="PDIWebDemoCS.CalendarObjectDetails" CodeFile="CalendarObjectDetails.aspx.cs" %>
<%@ Register TagPrefix="EWSPDI" Namespace="EWSoftware.PDI.Web.Controls" Assembly="EWSoftware.PDI.Web.Controls" %>

<!DOCTYPE html>

<html lang="en">
<head id="Head1" runat="server">
	<meta http-equiv="X-UA-Compatible" content="IE=edge" />
	<meta http-equiv="Content-Language" content="en-us" />
	<meta http-equiv="Content-Script-Type" content="text/javascript" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />
	<meta name="author" content="Eric Woodruff" />

	<title>Untitled Page</title>

	<!-- Bootstrap core CSS -->
	<link rel="stylesheet" type="text/css" href="Content/bootstrap.min.css" />
	<!-- Bootstrap theme -->
	<link rel="stylesheet" type="text/css" href="Content/bootstrap-theme.min.css" />

	<!-- Custom styles for this template -->
	<link rel="stylesheet" type="text/css" href="Content/Styles.css" />

</head>

<body>
	<div class="container">
		<form id="CalendarItemDetails" method="post" runat="server">
			<h1><asp:Label ID="lblItemType" runat="server" /> Details</h1>

			<p><asp:Label ID="lblMsg" runat="server" CssClass="text-danger" /></p>

			<asp:ValidationSummary ID="vsSummary" DisplayMode="BulletList" CssClass="alert alert-danger" ForeColor=""
				HeaderText="Please correct the following problems:" runat="server" />

			<div class="panel panel-info">
				<div class="panel-heading">
					<h1 class="panel-title">General</h1>
				</div>
				<div class="panel-body">
					<table class="table-noborder">
						<tr>
							<td class="control-label">Unique ID</td>
							<td><asp:Label ID="lblUniqueId" runat="server" /></td>
							<td>&nbsp;</td>
							<td>
								<asp:CheckBox ID="chkTransparent" runat="server" Text="Transparent" /></td>
						</tr>
						<tr>
							<td class="control-label">Time Zone</td>
							<td><asp:Label ID="lblTimeZone" runat="server" />&nbsp;</td>
							<td colspan="2">&nbsp;</td>
						</tr>
						<tr>
							<td class="control-label">Sequence</td>
							<td>
								<asp:TextBox ID="txtSequence" runat="server" Columns="5" MaxLength="5" CssClass="form-control" />
								<asp:RangeValidator ID="rvSequence" runat="server" Type="Integer" Display="None"
									MinimumValue="0" MaximumValue="32767" ControlToValidate="txtSequence"
									ErrorMessage="Sequence number should be between 0 and 32767" /></td>
							<td class="control-label">Priority</td>
							<td>
								<asp:TextBox ID="txtPriority" runat="server" Columns="1" MaxLength="1" CssClass="form-control" />
								<asp:RangeValidator ID="rvPriority" runat="server" Type="Integer" Display="None"
									MinimumValue="0" MaximumValue="9" ControlToValidate="txtPriority"
									ErrorMessage="Priority number should be between 0 and 9" /></td>
						</tr>
						<tr>
							<td class="control-label">Start Date</td>
							<td>
								<asp:TextBox ID="txtStartDate" runat="server" Columns="20" CssClass="form-control" /></td>
							<td class="control-label"><asp:Label ID="lblEndDate" runat="server" /></td>
							<td>
								<asp:TextBox ID="txtEndDate" runat="server" Columns="20" CssClass="form-control" /></td>
						</tr>
						<tr>
							<td class="control-label">Status</td>
							<td>
								<asp:DropDownList ID="cboStatus" runat="server" CssClass="form-control" /></td>
							<td class="control-label">Duration</td>
							<td>
								<asp:TextBox ID="txtDuration" runat="server" Columns="20" CssClass="form-control" /></td>
						</tr>
						<tr>
							<td class="control-label">% Done</td>
							<td>
								<asp:TextBox ID="txtPercent" runat="server" Columns="3" MaxLength="3" CssClass="form-control" />
								<asp:RangeValidator ID="rvPercent" runat="server" Type="Integer" Display="None"
									MinimumValue="0" MaximumValue="100" ControlToValidate="txtPercent"
									ErrorMessage="Percent complete should be between 0 and 100" /></td>
							<td class="control-label">Completed</td>
							<td>
								<asp:TextBox ID="txtCompleted" runat="server" Columns="20" CssClass="form-control" /></td>
						</tr>
						<tr>
							<td class="control-label">Summary</td>
							<td colspan="3">
								<asp:TextBox ID="txtSummary" runat="server" Columns="60" CssClass="form-control" /></td>
						</tr>
						<tr>
							<td class="control-label">Location</td>
							<td colspan="3">
								<asp:TextBox ID="txtLocation" runat="server" Columns="60" CssClass="form-control" /></td>
						</tr>
						<tr>
							<td class="control-label" style="vertical-align: top;">Description</td>
							<td colspan="3">
								<asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine"
									Columns="80" Rows="5" /></td>
						</tr>
						<tr>
							<td class="control-label">Organizer</td>
							<td colspan="3">
								<asp:TextBox ID="txtOrganizer" runat="server" Columns="60" CssClass="form-control" /></td>
						</tr>
						<tr>
							<td class="control-label">URL</td>
							<td colspan="3">
								<asp:TextBox ID="txtUrl" runat="server" Columns="80" CssClass="form-control" />&nbsp;
								<a href="#" onclick="javascript: OpenWebPage(); return false;">View Page</a></td>
						</tr>
						<tr>
							<td class="control-label" style="vertical-align: top;">Comments</td>
							<td colspan="3">
								<asp:TextBox ID="txtComments" runat="server" CssClass="form-control" TextMode="MultiLine"
									Columns="80" Rows="5" /></td>
						</tr>
					</table>
				</div>
			</div>

			<table class="table-noborder" style="width: 100%;">
				<tr>
					<td>
						<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default"
							ToolTip="Save changes to free/busy item" OnClick="btnSave_Click" /></td>
					<td style="text-align: right;">
						<asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="btn btn-default"
							CausesValidation="False" ToolTip="Exit without saving changes" OnClick="btnExit_Click" /></td>
				</tr>
			</table>

			<br />
			<div class="alert alert-info">
				<p>Although not fully implemented, support could be added for editing all of the information shown below
as well as other properties not shown.</p>
			</div>

			<table class="table-noborder" style="width: 100%;">
				<tr>
					<td style="vertical-align: top;">
						<asp:DataGrid ID="dgRecurrences" runat="server" AutoGenerateColumns="False" UseAccessibleHeader="true"
							CssClass="table table-striped table-bordered table-condensed" Width="100%" ShowFooter="true"
							OnCancelCommand="dgRecurrences_CancelCommand" OnDeleteCommand="dgRecurrences_DeleteCommand"
							OnEditCommand="dgRecurrences_EditCommand" OnItemCommand="dgRecurrences_ItemCommand"
							OnItemDataBound="dgRecurrences_ItemDataBound" OnUpdateCommand="dgRecurrences_UpdateCommand">
							<Columns>
								<asp:TemplateColumn HeaderText="Recurrence Rule">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Value") %>
									</ItemTemplate>
									<EditItemTemplate>
										<EWSPDI:RecurrencePattern ID="rpRecurrence" runat="Server"
											CssButton="btn btn-default btn-min-width" CssErrorMessage="text-danger"
											CssInput="form-control" CssSelect="form-control" CssPanel="panel panel-info"
											CssPanelHeading="panel-heading" CssPanelBody="panel-body" CssPanelTitle="panel-title" />
									</EditItemTemplate>
									<FooterTemplate>
										<br />
										<asp:LinkButton CommandName="Add" Text="Add Recurrence" runat="server"
											ToolTip="Add a new recurrence" ID="btnAdd" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<asp:LinkButton CommandName="Edit" Text="Edit" runat="server"
											ToolTip="Edit this recurrence" ID="btnEdit" />
									</ItemTemplate>
									<EditItemTemplate>
										<asp:LinkButton CommandName="Update" Text="Update" runat="server"
											ToolTip="Save changes to this recurrence" ID="btnUpdate" />
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<asp:LinkButton CommandName="Delete" Text="Delete" runat="server"
											CausesValidation="False" ToolTip="Delete recurrence" ID="btnDelete" />
									</ItemTemplate>
									<EditItemTemplate>
										<asp:LinkButton CommandName="Cancel" Text="Cancel" runat="server"
											CausesValidation="False" ToolTip="Cancel changes" ID="btnCancel" />
									</EditItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid></td>
					<td style="vertical-align: top;">
						<asp:DataGrid ID="dgRecurDates" runat="server" AutoGenerateColumns="False" UseAccessibleHeader="true"
							CssClass="table table-striped table-bordered table-condensed" Width="100%">
							<Columns>
								<asp:TemplateColumn HeaderText="Recurrence Date">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "TimeZoneDateTime") %>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid></td>
				</tr>
			</table>

			<table class="table-noborder" style="width: 100%;">
				<tr>
					<td style="vertical-align: top;">
						<asp:DataGrid ID="dgExceptions" runat="server" AutoGenerateColumns="False" UseAccessibleHeader="true"
							CssClass="table table-striped table-bordered table-condensed" Width="100%" ShowFooter="true"
							OnCancelCommand="dgExceptions_CancelCommand" OnDeleteCommand="dgExceptions_DeleteCommand"
							OnEditCommand="dgExceptions_EditCommand" OnItemCommand="dgExceptions_ItemCommand"
							OnItemDataBound="dgExceptions_ItemDataBound" OnUpdateCommand="dgExceptions_UpdateCommand">
							<Columns>
								<asp:TemplateColumn HeaderText="Exception Rule">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Value") %>
									</ItemTemplate>
									<EditItemTemplate>
										<EWSPDI:RecurrencePattern ID="rpException" runat="Server"
											CssButton="btn btn-default btn-min-width" CssErrorMessage="text-danger"
											CssInput="form-control" CssSelect="form-control" CssPanel="panel panel-info"
											CssPanelHeading="panel-heading" CssPanelBody="panel-body" CssPanelTitle="panel-title" />
									</EditItemTemplate>
									<FooterTemplate>
										<br />
										<asp:LinkButton CommandName="Add" Text="Add Exception" runat="server"
											ToolTip="Add a new exception" ID="btnAdd" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<asp:LinkButton CommandName="Edit" Text="Edit" runat="server"
											ToolTip="Edit this exception" ID="btnEdit" />
									</ItemTemplate>
									<EditItemTemplate>
										<asp:LinkButton CommandName="Update" Text="Update" runat="server"
											ToolTip="Save changes to this exception" ID="btnUpdate" />&nbsp;&nbsp;
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<asp:LinkButton CommandName="Delete" Text="Delete" runat="server"
											CausesValidation="False" ToolTip="Delete exception" ID="btnDelete" />
									</ItemTemplate>
									<EditItemTemplate>
										<asp:LinkButton CommandName="Cancel" Text="Cancel" runat="server"
											CausesValidation="False" ToolTip="Cancel changes" ID="btnCancel" />
									</EditItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid></td>
					<td style="vertical-align: top;">
						<asp:DataGrid ID="dgExDates" runat="server" AutoGenerateColumns="False" UseAccessibleHeader="true"
							CssClass="table table-striped table-bordered table-condensed" Width="100%">
							<HeaderStyle CssClass="ColHeader" />
							<Columns>
								<asp:TemplateColumn HeaderText="Exception Date">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "TimeZoneDateTime") %>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid></td>
				</tr>
			</table>

			<asp:DataGrid ID="dgAttendees" runat="server" AutoGenerateColumns="False" UseAccessibleHeader="true"
				CssClass="table table-striped table-bordered table-condensed" Width="100%">
				<Columns>
					<asp:TemplateColumn HeaderText="Attendee">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "Value") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Common Name">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "CommonName") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Role">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "Role") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Status">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "ParticipationStatus") %>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>

			<asp:DataGrid ID="dgReqStats" runat="server" AutoGenerateColumns="False" UseAccessibleHeader="true"
				CssClass="table table-striped table-bordered table-condensed" Width="100%">
				<Columns>
					<asp:TemplateColumn HeaderText="Status Code">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "Status Code") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Message">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "Message") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Extended Data">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "ExtendedData") %>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
			<br />
		</form>
	</div>

	<script type="text/javascript">
<!--
	// Open the URL specified in the web page text box
	function OpenWebPage() {
		var pageUrl = document.getElementById("txtUrl").value;

		if (pageUrl.length != 0)
			window.open(pageUrl, "_blank");
		else
			alert("Enter a URL for the web page");
	}
	//-->
	</script>

	<!-- Bootstrap core JavaScript
    ================================================== -->
	<!-- Placed at the end of the document so the pages load faster -->
	<script type="text/javascript" src='<% =ResolveUrl("~/Scripts/jquery-3.3.1.min.js") %>'></script>
	<script type="text/javascript" src='<% =ResolveUrl("~/Scripts/bootstrap.min.js") %>'></script>
</body>
</html>
