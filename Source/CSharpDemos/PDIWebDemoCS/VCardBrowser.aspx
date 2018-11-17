<%@ Page MasterPageFile="~/MasterPage.master" Language="c#" Inherits="PDIWebDemoCS.VCardBrowser" CodeFile="VCardBrowser.aspx.cs" %>

<asp:Content ID="pageContent" runat="server" ContentPlaceHolderID="cpContent">

	<p>This page is used to demonstrate the parsing and display of vCard files.  A small file is provided by
default.  You may upload another file of your choosing.  Upload size is restricted to a maximum of 1MB in this
demo.</p>

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
				<input type="file" id="hifUpload" runat="server" size="50" class="form-control"
					accept="text/*" name="hifUpload" />
				<asp:Button ID="btnUpload" runat="server" CssClass="btn btn-default"
					Text="Upload" ToolTip="Upload the file" OnClick="btnUpload_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
				<asp:Label ID="lblMsg" runat="server" CssClass="text-info" /></td>
		</tr>
	</table>

	<asp:DataGrid ID="dgVCards" runat="server" AutoGenerateColumns="False" UseAccessibleHeader="true"
		CssClass="table table-striped table-bordered table-condensed" Width="100%" ShowFooter="true"
		OnDeleteCommand="dgVCards_DeleteCommand" OnItemCommand="dgVCards_ItemCommand">
		<Columns>
			<asp:TemplateColumn HeaderText="Ver">
				<ItemTemplate>
					<%# ((EWSoftware.PDI.Objects.VCard)Container.DataItem).Version ==
					EWSoftware.PDI.Properties.SpecificationVersions.vCard21 ? "2.1" : "3.0" %>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Name">
				<ItemTemplate>
					<%# EncodeValue(DataBinder.Eval(Container.DataItem, "Name.SortableName")) %>
				</ItemTemplate>
				<FooterTemplate>
					<br />
					<asp:LinkButton CommandName="Add" Text="Add vCard" runat="server"
						ToolTip="Add a new vCard" ID="btnAdd" />
				</FooterTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Title">
				<ItemTemplate>
					<%# EncodeValue(DataBinder.Eval(Container.DataItem, "Title.Value")) %>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Organization">
				<ItemTemplate>
					<%# EncodeValue(DataBinder.Eval(Container.DataItem, "Organization.Name")) %>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Last Rev">
				<ItemTemplate>
					<%# DataBinder.Eval(Container.DataItem, "LastRevision.DateTimeValue", "{0:d}") %>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
					<asp:LinkButton CommandName="Edit" Text="Details" runat="server"
						ToolTip="View details" ID="btnEdit" />
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
				FooterStyle-HorizontalAlign="Center">
				<ItemTemplate>
					<asp:LinkButton CommandName="Delete" Text="Delete" runat="server" CausesValidation="False"
						ToolTip="Delete vCard" ID="btnDelete" />
				</ItemTemplate>
				<FooterTemplate>
					<br />
					<asp:LinkButton CommandName="Download" Text="Download" runat="server"
						ToolTip="Download vCard File" ID="btnDownload" />
				</FooterTemplate>
			</asp:TemplateColumn>
		</Columns>
	</asp:DataGrid>

</asp:Content>
