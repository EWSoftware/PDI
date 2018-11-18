<%@ Page ValidateRequest="False" Language="VB" Inherits="PDIWebDemoVB.FreeBusyDetails" CodeFile="FreeBusyDetails.aspx.vb" %>

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
		<form method="post" runat="server">
			<h1>Free/Busy Details</h1>
			<p><asp:Label ID="lblMsg" runat="server" CssClass="text-danger" />&nbsp;</p>

			<div class="panel panel-info">
				<div class="panel-heading">
					<h1 class="panel-title">General</h1>
				</div>
				<div class="panel-body">
					<table class="table-noborder">
						<tr>
							<td class="control-label">Unique ID</td>
							<td><asp:Label ID="lblUniqueId" runat="server" /></td>
							<td colspan="2">&nbsp;</td>
						</tr>
						<tr>
							<td class="control-label">Time Zone</td>
							<td><asp:Label ID="lblTimeZone" runat="server" />&nbsp;</td>
							<td colspan="2">&nbsp;</td>
						</tr>
						<tr>
							<td class="control-label">Organizer</td>
							<td colspan="3">
								<asp:TextBox ID="txtOrganizer" runat="server" Columns="60" CssClass="form-control" /></td>
						</tr>
						<tr>
							<td class="control-label">Contact</td>
							<td colspan="3">
								<asp:TextBox ID="txtContact" runat="server" Columns="60" CssClass="form-control" /></td>
						</tr>
						<tr>
							<td class="control-label">Start Date</td>
							<td>
								<asp:TextBox ID="txtStartDate" runat="server" Columns="20" CssClass="form-control" /></td>
							<td class="control-label">End Date</td>
							<td>
								<asp:TextBox ID="txtEndDate" runat="server" Columns="20" CssClass="form-control" /></td>
						</tr>
						<tr>
							<td class="control-label">Duration</td>
							<td colspan="3">
								<asp:TextBox ID="txtDuration" runat="server" Columns="20" CssClass="form-control" /></td>
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
							ToolTip="Save changes to free/busy item" /></td>
					<td style="text-align: right;">
						<asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="btn btn-default"
							CausesValidation="False" ToolTip="Exit without saving changes" /></td>
				</tr>
			</table>

			<br />
			<div class="alert alert-info">
				<p>Although not currently implemented, support could be added for editing the information shown below as
well.</p>
			</div>

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

			<asp:DataGrid ID="dgFreeBusy" runat="server" AutoGenerateColumns="False" UseAccessibleHeader="true"
				CssClass="table table-striped table-bordered table-condensed" Width="100%">
				<Columns>
					<asp:TemplateColumn HeaderText="Type">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "FreeBusyType") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Other Type">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "OtherType") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Start">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "PeriodValue.StartDateTime") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Status">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "PeriodValue.EndDateTime") %>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>

			<asp:DataGrid ID="dgReqStats" runat="server" AutoGenerateColumns="False" UseAccessibleHeader="true"
				CssClass="table table-striped table-bordered table-condensed" Width="100%">
				<Columns>
					<asp:TemplateColumn HeaderText="Status Code">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "StatusCode") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Message">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "StatusMessage") %>
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
