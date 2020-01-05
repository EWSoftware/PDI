<%@ Page ValidateRequest="False" Language="c#" Inherits="PDIWebDemoCS.VCardDetails" CodeFile="VCardDetails.aspx.cs" %>

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
			<h1>vCard Details</h1>
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
							<td class="control-label">Version</td>
							<td>
								<asp:DropDownList ID="cboVersion" runat="server" AutoPostBack="True" CssClass="form-control"
									OnSelectedIndexChanged="cboVersion_SelectedIndexChanged">
									<asp:ListItem>2.1</asp:ListItem>
									<asp:ListItem>3.0</asp:ListItem>
									<asp:ListItem>4.0</asp:ListItem>
								</asp:DropDownList></td>
						</tr>
						<tr>
							<td class="control-label">Last Revised</td>
							<td><asp:Label ID="lblLastRevised" runat="server" /></td>
							<td class="control-label">Class</td>
							<td>
								<asp:TextBox ID="txtClass" runat="server" Columns="10" CssClass="form-control" /></td>
						</tr>
					</table>
				</div>
			</div>

			<div class="panel panel-info">
				<div class="panel-heading">
					<h1 class="panel-title">Name</h1>
				</div>
				<div class="panel-body">
					<table class="table-noborder">
						<tr>
							<td class="control-label">Last Name</td>
							<td>
								<asp:TextBox ID="txtLastName" runat="server" Columns="30" CssClass="form-control" /></td>
							<td class="control-label">Title</td>
							<td>
								<asp:TextBox ID="txtTitle" runat="server" Columns="30" CssClass="form-control" /></td>
						</tr>
						<tr>
							<td class="control-label">First Name</td>
							<td>
								<asp:TextBox ID="txtFirstName" runat="server" Columns="30" CssClass="form-control" /></td>
							<td class="control-label">Suffix</td>
							<td>
								<asp:TextBox ID="txtSuffix" runat="server" Columns="30" CssClass="form-control" /></td>
						</tr>
						<tr>
							<td class="control-label">Middle Name</td>
							<td>
								<asp:TextBox ID="txtMiddleName" runat="server" Columns="30" CssClass="form-control" /></td>
							<td class="control-label">Sort String</td>
							<td>
								<asp:TextBox ID="txtSortString" runat="server" Columns="30" CssClass="form-control" /></td>
						</tr>
						<tr>
							<td class="control-label">Nickname</td>
							<td>
								<asp:TextBox ID="txtNickname" runat="server" Columns="30" CssClass="form-control" /></td>
							<td class="control-label">Formatted Name</td>
							<td>
								<asp:TextBox ID="txtFormattedName" runat="server" Columns="30" CssClass="form-control" /></td>
						</tr>
					</table>
				</div>
			</div>

			<div class="panel panel-info">
				<div class="panel-heading">
					<h1 class="panel-title">Work</h1>
				</div>
				<div class="panel-body">
					<table class="table-noborder">
						<tr>
							<td class="control-label">Organization</td>
							<td>
								<asp:TextBox ID="txtOrganization" runat="server" Columns="30" CssClass="form-control" /></td>
							<td class="control-label">Units</td>
							<td>
								<asp:TextBox ID="txtUnits" runat="server" Columns="30" CssClass="form-control" /></td>
						</tr>
						<tr>
							<td class="control-label">Job Title</td>
							<td>
								<asp:TextBox ID="txtJobTitle" runat="server" Columns="30" CssClass="form-control" /></td>
							<td class="control-label">Role</td>
							<td>
								<asp:TextBox ID="txtRole" runat="server" Columns="30" CssClass="form-control" /></td>
						</tr>
						<tr>
							<td class="control-label">Categories</td>
							<td colspan="3">
								<asp:TextBox ID="txtCategories" runat="server" Columns="60" CssClass="form-control" /></td>
						</tr>
					</table>
				</div>
			</div>

			<div class="panel panel-info">
				<div class="panel-heading">
					<h1 class="panel-title">Other</h1>
				</div>
				<div class="panel-body">
					<table class="table-noborder">
						<tr>
							<td class="control-label">Birth Date</td>
							<td colspan="3">
								<asp:TextBox ID="txtBirthDate" runat="server" Columns="10" CssClass="form-control" />
								<asp:CompareValidator ID="cvBirthDate" runat="server" Display="None"
									ControlToValidate="txtBirthDate" Operator="DataTypeCheck" Type="Date"
									ErrorMessage="Birth Date should be a valid date" /></td>
						</tr>
						<tr>
							<td class="control-label">Time Zone</td>
							<td colspan="3">
								<asp:TextBox ID="txtTimeZone" runat="server" Columns="60" CssClass="form-control" /></td>
						</tr>
						<tr>
							<td class="control-label">Latitude</td>
							<td>
								<asp:TextBox ID="txtLatitude" runat="server" Columns="20" CssClass="form-control" />
								<asp:RangeValidator ID="rvLatitude" runat="server" Type="Double" Display="None"
									MinimumValue="-90.0" MaximumValue="90.0" ControlToValidate="txtLatitude"
									ErrorMessage="Latitude should be between -90.0 and +90.0" /></td>
							<td class="control-label">Longitude</td>
							<td>
								<asp:TextBox ID="txtLongitude" runat="server" Columns="20" CssClass="form-control" />&nbsp;
								<a href="#" onclick="javascript: GeoLocate(); return false;">Find</a>
								<asp:RangeValidator ID="rvLongitude" runat="server" Type="Double" Display="None"
									MinimumValue="-180.0" MaximumValue="180.0" ControlToValidate="txtLongitude"
									ErrorMessage="Longitude should be between -180.0 and +180.0" /></td>
						</tr>
						<tr>
							<td class="control-label">Web Page</td>
							<td colspan="3">
								<asp:TextBox ID="txtWebPage" runat="server" Columns="80" CssClass="form-control" />&nbsp;
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
							ToolTip="Save changes to vCard" OnClick="btnSave_Click" /></td>
					<td style="text-align: right;">
						<asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="btn btn-default"
							CausesValidation="False" ToolTip="Exit without saving changes" OnClick="btnExit_Click" /></td>
				</tr>
			</table>

			<br />
			<div class="alert alert-info">
				<p>Although not currently implemented, support could be added for editing the information shown below as
well.</p>
			</div>

			<table class="table table-striped table-bordered table-condensed" style="width: 100%;">
				<tr>
					<th style="text-align: center;">Photo</th>
					<th style="text-align: center;">Logo</th>
				</tr>
				<tr>
					<td style="text-align: center">
						<asp:Image ID="imgPhoto" runat="server" /><br />
						<asp:Label ID="lblPhoto" runat="server" /></td>
					<td style="text-align: center">
						<asp:Image ID="imgLogo" runat="server" /><br />
						<asp:Label ID="lblLogo" runat="server" /></td>
				</tr>
			</table>

			<asp:DataGrid ID="dgAddresses" runat="server" AutoGenerateColumns="False" UseAccessibleHeader="true"
				CssClass="table table-striped table-bordered table-condensed" Width="100%">
				<Columns>
					<asp:TemplateColumn HeaderText="Address">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "StreetAddress") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="City">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "Locality") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Type">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "AddressTypes") %>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>

			<asp:DataGrid ID="dgLabels" runat="server" AutoGenerateColumns="False" UseAccessibleHeader="true"
				CssClass="table table-striped table-bordered table-condensed" Width="100%">
				<Columns>
					<asp:TemplateColumn HeaderText="Label">
						<ItemTemplate>
							<%# ((string)DataBinder.Eval(Container.DataItem, "Value")).Replace("\n", "<br />") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Type">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "AddressTypes") %>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>

			<asp:DataGrid ID="dgPhones" runat="server" AutoGenerateColumns="False" UseAccessibleHeader="true"
				CssClass="table table-striped table-bordered table-condensed" Width="100%">
				<Columns>
					<asp:TemplateColumn HeaderText="Phone Number">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "Value") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Type">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "PhoneTypes") %>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>

			<asp:DataGrid ID="dgEMail" runat="server" AutoGenerateColumns="False" UseAccessibleHeader="true"
				CssClass="table table-striped table-bordered table-condensed" Width="100%">
				<Columns>
					<asp:TemplateColumn HeaderText="E-Mail Address">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "Value") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Type">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "EMailTypes") %>
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
		var pageUrl = document.getElementById("txtWebPage").value;

		if (pageUrl.length != 0)
			window.open(pageUrl, "_blank");
		else
			alert("Enter a URL for the web page");
	}

	// Geo-locate the longitude and latitude values
	function GeoLocate() {
		var latitude = document.getElementById("txtLatitude").value,
				longitude = document.getElementById("txtLongitude").value;

		if (latitude.length != 0 && longitude.length != 0)
			window.open("https://www.google.com/maps/place/" + latitude + "," + longitude);
		else
			alert("Enter a latitude and longitude to locate");
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
