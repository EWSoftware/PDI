<%@ Page MasterPageFile="~/MasterPage.master" Language="VB" Inherits="PDIWebDemoVB.HolidayTestForm" CodeFile="HolidayTestForm.aspx.vb" %>

<asp:Content ID="pageContent" runat="server" ContentPlaceHolderID="cpContent">

	<p>This page is used to demonstrate the holiday classes and methods.</p>

	<div class="alert alert-info">
		<p><strong>Please Note:</strong> To prevent overloading the server or causing time-outs, the web demo limits
the test search range to a maximum of 50 years from the starting year.</p>
	</div>

	<asp:ValidationSummary ID="vsSummary" DisplayMode="BulletList" CssClass="alert alert-danger" ForeColor=""
		HeaderText="Please correct the following problems:" runat="server" />

	<div class="panel panel-info">
		<div class="panel-heading">
			<h1 class="panel-title">Holiday Definitions</h1>
		</div>
		<asp:DataGrid ID="dgHolidays" runat="server" AutoGenerateColumns="False"
			CssClass="table table-striped table-bordered table-condensed" ShowHeader="false" ShowFooter="true">
			<Columns>
				<asp:TemplateColumn>
					<ItemTemplate>
						<%# DataBinder.Eval(Container.DataItem, "Description") %>
					(<%# IIf(TypeOf Container.DataItem is EWSoftware.PDI.FixedHoliday, "Fixed", "Floating") %>)
					</ItemTemplate>
					<EditItemTemplate>
						<table class="table-noborder">
							<tr>
								<td class="control-label" style="width: 25%;">Description&nbsp;</td>
								<td colspan="2">
									<asp:TextBox ID="txtDescription" runat="server" Columns="30" CssClass="form-control" />
									<asp:RequiredFieldValidator ID="rfvDesc" runat="server" Display="None"
										ErrorMessage="A description is required" ControlToValidate="txtDescription" /></td>
							</tr>
							<tr>
								<td class="control-label">Month&nbsp;</td>
								<td colspan="2">
									<asp:DropDownList ID="cboMonth" runat="server" CssClass="form-control" /></td>
							</tr>
							<tr>
								<td class="control-label">Minimum Year</td>
								<td colspan="2">
									<asp:TextBox ID="txtMinimumYear" runat="server" Columns="4" MaxLength="4" CssClass="form-control" />
									<asp:RequiredFieldValidator ID="rfvMinimumYear" runat="server" Display="None"
										ErrorMessage="A minimum year is required.  Use 1 for no minimum." ControlToValidate="txtMinimumYear" />
									<asp:RangeValidator ID="rvMinimumYear" runat="server" Display="None"
										ErrorMessage="Minimum year should be between 1 and 9999" Type="Integer"
										MinimumValue="1" MaximumValue="9999" ControlToValidate="txtMinimumYear" /></td>
							</tr>
							<tr>
								<td class="control-label">Maximum Year</td>
								<td colspan="2">
									<asp:TextBox ID="txtMaximumYear" runat="server" Columns="4" MaxLength="4" CssClass="form-control" />
									<asp:RequiredFieldValidator ID="rfvMaximumYear" runat="server" Display="None"
										ErrorMessage="A maximum year is required.  Use 9999 for no maximum." ControlToValidate="txtMaximumYear" />
									<asp:RangeValidator ID="rvMaximumYear" runat="server" Display="None"
										ErrorMessage="Maximum year should be between 1 and 9999" Type="Integer"
										MinimumValue="1" MaximumValue="9999" ControlToValidate="txtMaximumYear" /></td>
							</tr>
							<tr>
								<td>
									<asp:RadioButton ID="rbFloating" runat="server"
										AutoPostBack="True" Text="Floating" OnCheckedChanged="Type_CheckChanged" /></td>
								<td class="control-label" style="width: 25%;">Occurrence</td>
								<td>
									<asp:DropDownList ID="cboOccurrence" runat="server" CssClass="form-control" /></td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td class="control-label">Day of Week</td>
								<td>
									<asp:DropDownList ID="cboDayOfWeek" runat="server" CssClass="form-control" /></td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td class="control-label">Offset</td>
								<td>
									<asp:TextBox ID="txtOffset" runat="server" Columns="5" MaxLength="5" CssClass="form-control" />
									<asp:RequiredFieldValidator ID="rfvOffset" runat="server" Display="None"
										ErrorMessage="An offset is required.  Use 0 for no offset." ControlToValidate="txtOffset" />
									<asp:RangeValidator ID="rvOffset" runat="server" Display="None"
										ErrorMessage="Offset should be between -999 and 999" Type="Integer"
										MinimumValue="-999" MaximumValue="999" ControlToValidate="txtOffset" /></td>
							</tr>
							<tr>
								<td>
									<asp:RadioButton ID="rbFixed" runat="server"
										AutoPostBack="True" Text="Fixed" OnCheckedChanged="Type_CheckChanged" /></td>
								<td class="control-label">Day of Month</td>
								<td>
									<asp:TextBox ID="txtDayOfMonth" runat="server" Columns="5" MaxLength="5" CssClass="form-control" />
									<asp:RequiredFieldValidator ID="rfvDOM" runat="server" Display="None"
										ErrorMessage="A day of the month is required" ControlToValidate="txtDayOfMonth" />
									<asp:RangeValidator ID="rvDOM" runat="server" Display="None"
										ErrorMessage="Day of month should be between 1 and the maximum number of days in the selected month"
										Type="Integer" MinimumValue="1" MaximumValue="31" ControlToValidate="txtDayOfMonth" /></td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td colspan="2">
									<asp:CheckBox ID="chkAdjustDate" runat="server"
										Text="Adjust date if it falls on a weekend" /></td>
							</tr>
						</table>
					</EditItemTemplate>
					<FooterTemplate>
						<br />
						&nbsp;<asp:LinkButton CommandName="Add" Text="Add Holiday"
							runat="server" ToolTip="Add a new holiday" ID="btnAdd" />&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:LinkButton CommandName="Default" Text="Default"
										runat="server" ToolTip="Reset to default holiday set" ID="btnDefault" />
					</FooterTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
					<ItemTemplate>
						<asp:LinkButton CommandName="Edit" Text="Edit" runat="server"
							ToolTip="Edit this holiday" ID="btnEdit" />
					</ItemTemplate>
					<EditItemTemplate>
						<asp:LinkButton CommandName="Update" Text="Update" runat="server"
							ToolTip="Save changes to this holiday" ID="btnUpdate" />&nbsp;&nbsp;
					<asp:LinkButton CommandName="Cancel" Text="Cancel" runat="server"
						CausesValidation="False" ToolTip="Cancel changes" ID="btnCancel" />
					</EditItemTemplate>
					<FooterTemplate>
						<br />
						<asp:LinkButton CommandName="Clear" Text="Clear" runat="server"
							ToolTip="Clear all holidays" ID="btnClear" CausesValidation="False" />
					</FooterTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
					FooterStyle-HorizontalAlign="Center">
					<ItemTemplate>
						<asp:LinkButton CommandName="Delete" Text="Delete" runat="server"
							CausesValidation="False" ToolTip="Delete holiday" ID="btnDelete" />
					</ItemTemplate>
					<FooterTemplate>
						<br />
						<asp:LinkButton CommandName="Download" Text="Download" runat="server"
							ToolTip="Download Holiday XML File" ID="btnDownload" />
					</FooterTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</div>

	<div class="panel panel-info">
		<div class="panel-heading">
			<h1 class="panel-title">Test Date Detection</h1>
		</div>
		<div class="panel-body">
			<table class="table-noborder">
				<tr>
					<td>
						<asp:ListBox ID="lbDates" runat="server" CssClass="form-control" Style="width: 3in" Rows="15" />
					</td>
					<td>
						<table class="table-noborder">
							<tr>
								<td class="control-label-left">From Year</td>
								<td class="control-label-left" colspan="2">To Year</td>
							</tr>
							<tr>
								<td>
									<asp:TextBox ID="txtFromYear" runat="server" Columns="4" MaxLength="4" CssClass="form-control" />
									<asp:RequiredFieldValidator ID="rfvFromYear" runat="server" Display="None"
										ControlToValidate="txtFromYear" ErrorMessage="A From Year is required" />
									<asp:RangeValidator ID="rvFromYear" runat="server" Type="Integer"
										MinimumValue="1" MaximumValue="9999" Display="None" ControlToValidate="txtFromYear"
										ErrorMessage="From Year should be between 1 and 9999" /></td>
								<td colspan="2">
									<asp:TextBox ID="txtToYear" runat="server" Columns="4" MaxLength="4" CssClass="form-control" />
									<asp:RequiredFieldValidator ID="rfvToYear" runat="server" Display="None"
										ControlToValidate="txtToYear" ErrorMessage="A To Year is required" />
									<asp:RangeValidator ID="rvToYear" runat="server" Type="Integer"
										MinimumValue="1" MaximumValue="9999" Display="None" ControlToValidate="txtToYear"
										ErrorMessage="To Year should be between 1 and 9999" /></td>
							</tr>
							<tr>
								<td colspan="3">
									<asp:Button ID="btnFindHolidays" runat="server"
										Text="Find Holidays" CssClass="btn btn-default" Style="width: 1.5in"
										ToolTip="Find holidays in the given year range" />
									<br /><br />
								</td>
							</tr>
							<tr>
								<td class="control-label">Test Date</td>
								<td>
									<asp:TextBox ID="txtTestDate" runat="server" Columns="10" MaxLength="10" CssClass="form-control" />
									<asp:RequiredFieldValidator ID="rfvTestDate" runat="server" Display="None"
										ErrorMessage="A test date is required" ControlToValidate="txtTestDate" />
									<asp:CompareValidator ID="cvTestDate" runat="server"
										ControlToValidate="txtTestDate" Operator="DataTypeCheck" Type="Date"
										Display="None" ErrorMessage="Test Date should be a valid date" /></td>
								<td>
									<asp:Button ID="btnIsHoliday" runat="server" Text="Holiday?"
										CssClass="btn btn-default" ToolTip="See if the date specified is a holiday" /></td>
							</tr>
							<tr>
								<td colspan="3"><asp:Label ID="lblIsHoliday" runat="server" CssClass="Note" />&nbsp;</td>
							</tr>
						</table>
					</td>
					<td>
						<table class="table-noborder">
							<tr>
								<td class="control-label-left">Easter Method</td>
							</tr>
							<tr>
								<td>
									<asp:RadioButtonList ID="rblEasterMethod" runat="server">
										<asp:ListItem>Julian</asp:ListItem>
										<asp:ListItem>Orthodox</asp:ListItem>
										<asp:ListItem Selected="True">Gregorian</asp:ListItem>
									</asp:RadioButtonList></td>
							</tr>
							<tr>
								<td>
									<asp:Button ID="btnFindEaster" runat="server"
										Text="Find Easter" CssClass="btn btn-default" Style="width: 1.5in"
										ToolTip="Find Easter in the given year range"/><br />
									<br />
								</td>
							</tr>

						</table>
					</td>
				</tr>
			</table>
		</div>
	</div>

</asp:Content>
