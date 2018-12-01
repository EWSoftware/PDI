<%@ Page MasterPageFile="~/MasterPage.master" Language="c#" Inherits="PDIWebDemoCS.CalendarBrowser" CodeFile="CalendarBrowser.aspx.cs" %>

<asp:Content ID="pageContent" runat="server" ContentPlaceHolderID="cpContent">

	<p>This page is used to demonstrate the parsing and display of vCalendar and iCalendar data files.  A small
file is provided by default.  You may upload another file of your choosing.  Upload size is restricted to a
maximum of 1MB in this demo.</p>

	<table class="table-noborder" style="width: 100%;">
		<tr>
			<td class="control-label">File Encoding</td>
			<td>
				<asp:DropDownList ID="cboFileEncoding" runat="server" CssClass="form-control">
					<asp:ListItem Selected="True">Unicode (UTF-8)</asp:ListItem>
					<asp:ListItem>Western European (Windows)</asp:ListItem>
					<asp:ListItem>ASCII</asp:ListItem>
				</asp:DropDownList></td>
			<td class="control-label">Property Encoding</td>
			<td>
				<asp:DropDownList ID="cboPropEncoding" runat="server" CssClass="form-control">
					<asp:ListItem>Unicode (UTF-8)</asp:ListItem>
					<asp:ListItem>Western European (Windows)</asp:ListItem>
					<asp:ListItem Selected="True">ASCII</asp:ListItem>
				</asp:DropDownList></td>
		</tr>
		<tr>
			<td class="control-label" style="vertical-align: top;">Select a file to upload</td>
			<td colspan="3">
				<input type="file" id="hifUpload" runat="server" style="width: 35em;" class="form-control"
					accept="text/*" name="hifUpload" />
				<asp:Button ID="btnUpload" runat="server" CssClass="btn btn-default"
					Text="Upload" ToolTip="Upload the file" OnClick="btnUpload_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
				<asp:Label ID="lblMsg" runat="server" CssClass="text-info" /></td>
		</tr>
	</table>

	<div class="panel panel-info">
		<div class="panel-heading">
			<h1 class="panel-title">General</h1>
		</div>
		<div class="panel-body">
			<table class="table-noborder">
				<tr>
					<td class="control-label">Version</td>
					<td ><asp:Label ID="lblVersion" runat="server" /></td>
					<td>
						<asp:Button ID="btnDownload" runat="server" Text="Download" CssClass="btn btn-default"
							ToolTip="Download this calendar" OnClick="btnDownload_Click" /></td>
				</tr>
			</table>
		</div>
	</div>

	<div class="panel panel-info">
		<div class="panel-heading">
			<h1 class="panel-title">Events</h1>
		</div>
		<asp:DataGrid ID="dgEvents" runat="server" AutoGenerateColumns="False" UseAccessibleHeader="true"
			CssClass="table table-striped table-bordered table-condensed" Width="100%" ShowFooter="true"
			OnDeleteCommand="dgEvents_DeleteCommand" OnItemCommand="dgEvents_ItemCommand">
			<Columns>
				<asp:TemplateColumn HeaderText="Start Date/Time">
					<ItemTemplate>
						<%# DataBinder.Eval(Container.DataItem, "StartDateTime.DateTimeValue", "{0:G}") %>
					</ItemTemplate>
					<FooterTemplate>
						<br />
						<asp:LinkButton CommandName="Add" Text="Add Event" runat="server" ToolTip="Add a new event"
							ID="btnAddEvent" />
					</FooterTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Summary">
					<ItemTemplate>
						<%# EncodeValue(DataBinder.Eval(Container.DataItem, "Summary.Value")) %>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
					<ItemTemplate>
						<asp:LinkButton CommandName="Edit" Text="Details" runat="server" ToolTip="View details"
							ID="btnEditEvent" />
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
					<ItemTemplate>
						<asp:LinkButton CommandName="Delete" Text="Delete" runat="server" CausesValidation="False"
							ToolTip="Delete event" ID="btnDeleteEvent" />
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</div>

	<div class="panel panel-info">
		<div class="panel-heading">
			<h1 class="panel-title">To-Dos</h1>
		</div>
		<asp:DataGrid ID="dgToDos" runat="server" AutoGenerateColumns="False" UseAccessibleHeader="true"
			CssClass="table table-striped table-bordered table-condensed" Width="100%" ShowFooter="true"
			OnDeleteCommand="dgToDos_DeleteCommand" OnItemCommand="dgToDos_ItemCommand">
			<Columns>
				<asp:TemplateColumn HeaderText="Start Date/Time">
					<ItemTemplate>
						<%# DataBinder.Eval(Container.DataItem, "StartDateTime.DateTimeValue", "{0:G}") %>
					</ItemTemplate>
					<FooterTemplate>
						<br />
						<asp:LinkButton CommandName="Add" Text="Add To-Do" runat="server" ToolTip="Add a new To-Do"
							ID="btnAddToDo" />
					</FooterTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Summary">
					<ItemTemplate>
						<%# EncodeValue(DataBinder.Eval(Container.DataItem, "Summary.Value")) %>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
					<ItemTemplate>
						<asp:LinkButton CommandName="Edit" Text="Details" runat="server" ToolTip="View details"
							ID="btnEditToDo" />
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
					<ItemTemplate>
						<asp:LinkButton CommandName="Delete" Text="Delete" runat="server" CausesValidation="False"
							ToolTip="Delete To-Do" ID="btnDeleteToDo" />
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</div>

	<div class="panel panel-info">
		<div class="panel-heading">
			<h1 class="panel-title">Journals</h1>
		</div>
		<asp:DataGrid ID="dgJournals" runat="server" AutoGenerateColumns="False" UseAccessibleHeader="true"
			CssClass="table table-striped table-bordered table-condensed" Width="100%" ShowFooter="true"
			OnDeleteCommand="dgJournals_DeleteCommand" OnItemCommand="dgJournals_ItemCommand">
			<Columns>
				<asp:TemplateColumn HeaderText="Start Date/Time">
					<ItemTemplate>
						<%# DataBinder.Eval(Container.DataItem, "StartDateTime.DateTimeValue", "{0:G}") %>
					</ItemTemplate>
					<FooterTemplate>
						<br />
						<asp:LinkButton CommandName="Add" Text="Add Journal" runat="server" ToolTip="Add a new journal"
							ID="btnAddJournal" />
					</FooterTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Summary">
					<ItemTemplate>
						<%# EncodeValue(DataBinder.Eval(Container.DataItem, "Summary.Value")) %>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
					<ItemTemplate>
						<asp:LinkButton CommandName="Edit" Text="Details" runat="server" ToolTip="View details"
							ID="btnEditJournal" />
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
					<ItemTemplate>
						<asp:LinkButton CommandName="Delete" Text="Delete" runat="server" CausesValidation="False"
							ToolTip="Delete journal" ID="btnDeleteJournal" />
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</div>

	<div class="panel panel-info">
		<div class="panel-heading">
			<h1 class="panel-title">Free/Busy Time</h1>
		</div>
		<asp:DataGrid ID="dgFreeBusys" runat="server" AutoGenerateColumns="False" UseAccessibleHeader="true"
			CssClass="table table-striped table-bordered table-condensed" Width="100%" ShowFooter="true"
			OnDeleteCommand="dgFreeBusys_DeleteCommand" OnItemCommand="dgFreeBusys_ItemCommand">
			<Columns>
				<asp:TemplateColumn HeaderText="Start Date/Time">
					<ItemTemplate>
						<%# DataBinder.Eval(Container.DataItem, "StartDateTime.DateTimeValue", "{0:G}") %>
					</ItemTemplate>
					<FooterTemplate>
						<br />
						<asp:LinkButton CommandName="Add" Text="Add Free/Busy" runat="server" ToolTip="Add a new Free/Busy"
							ID="btnAddFreeBusy" />
					</FooterTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Organizer">
					<ItemTemplate>
						<%# EncodeValue(DataBinder.Eval(Container.DataItem, "Organizer.Value")) %>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Comments">
					<ItemTemplate>
						<%# EncodeValue(DataBinder.Eval(Container.DataItem, "Comment.Value")) %>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
					<ItemTemplate>
						<asp:LinkButton CommandName="Edit" Text="Details" runat="server" ToolTip="View details"
							ID="btnEditFreeBusy" />
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
					<ItemTemplate>
						<asp:LinkButton CommandName="Delete" Text="Delete" runat="server" CausesValidation="False"
							ToolTip="Delete free/busy" ID="btnDeleteFreeBusy" />
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</div>

</asp:Content>
