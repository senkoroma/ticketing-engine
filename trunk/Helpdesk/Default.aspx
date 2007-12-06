<%@ Page Language="C#" MasterPageFile="~/Assets/MasterPages/Main.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

			<div class="content_panel">
				<h1>
					Custom Sort</h1>
				<p>
					Use the controls below in order to peform a custom sort / search on our ticketing
					system. Your results will be displayed in the Ticket Results Pane.</p>
				<table align="center" cellpadding="2">
					<tr>
						<td>
							Queue:
						</td>
						<td>
							<asp:DropDownList ID="DropDownList2" runat="server" />
						</td>
						<td>
							Module:
						</td>
						<td>
							<asp:DropDownList ID="DropDownList3" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							Category:
						</td>
						<td>
							<asp:DropDownList ID="DropDownList4" runat="server" />
						</td>
						<td>
							Status:
						</td>
						<td>
							<asp:DropDownList ID="DropDownList5" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							Business:
						</td>
						<td>
							<asp:DropDownList ID="DropDownList6" runat="server" />
						</td>
						<td>
							Requestor:
						</td>
						<td>
							<asp:DropDownList ID="DropDownList7" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							Assigned To:
						</td>
						<td>
							<asp:DropDownList ID="DropDownList8" runat="server" />
						</td>
						<td>
							Created By:
						</td>
						<td>
							<asp:DropDownList ID="DropDownList9" runat="server" />
						</td>
					</tr>
					</tr>
				</table>
				<p class="bottom">
				</p>
			</div>
			<div class="content_panel">
				<h1>
					Ticket Query Results</h1>
				<p>
					Sorted By:</p>
				<p style="background: yellow;">
					Assigned To You <strong>&amp;</strong> Ticket Status = Open <strong>&amp;</strong>
					Text - Regex-IsNotMatch: ( /d )
				</p>
				<br />
				<table align="center" cellpadding="10">
					<tr>
						<th class="first">
							Ticket Id #
						</th>
						<th>
							Queue - Mod
						</th>
						<th>
							Created On
						</th>
						<th>
							Status
						</th>
					</tr>
					<tr class="row-a">
						<td class="first"  rowspan="2">
							<asp:CheckBox ID="CheckBox1" Text="# 4289" runat="server" />
						</td>
						<td>
							<a href="index.html">SAM - FAQ</a>
						</td>
						<td>
							<a href="index.html">
								<%= DateTime.Now.ToShortDateString() %></a>
						</td>
						<td>
							Open
						</td>
					</tr>
					<tr class="row-base">
						<td colspan="3">
							<p>
								<strong>assigned to:</strong> nblevins, <strong>requestor:</strong> DCS - crystal
								blevins</p>
						</td>
					</tr>
					<tr class="row-a">
						<td class="first"  rowspan="2">
							<asp:CheckBox ID="CheckBox2" Text="# 4289" runat="server" />
						</td>
						<td>
							<a href="index.html">SAM - FAQ</a>
						</td>
						<td>
							<a href="index.html">
								<%= DateTime.Now.ToShortDateString() %></a>
						</td>
						<td>
							Stalled
						</td>
					</tr>
					
					<tr class="row-base">
						<td colspan="3">
							<p>
								<strong>assigned to:</strong> nblevins, <strong>requestor:</strong> DCS - crystal
								blevins</p>
						</td>
					</tr>

					<tr class="row-a">
						<td class="first"  rowspan="2">
							<asp:CheckBox ID="CheckBox3" Text="# 4289" runat="server" />
						</td>
						<td>
							<a href="index.html">TPRIC - Reports</a>
						</td>
						<td>
							<a href="index.html">
								<%= DateTime.Now.ToShortDateString() %></a>
						</td>
						<td>
							In Queue
						</td>
					</tr>
					
					<tr class="row-base">
						<td colspan="3">
							<p>
								<strong>assigned to:</strong> nblevins, <strong>requestor:</strong> DCS - crystal
								blevins</p>
						</td>
					</tr>

					<tr class="row-a">
						<td class="first"  rowspan="2">
							<asp:CheckBox ID="CheckBox4" Text="# 4289" runat="server" />
						</td>
						<td>
							<a href="index.html">SAM - FAQ</a>
						</td>
						<td>
							<a href="index.html">
								<%= DateTime.Now.ToShortDateString() %></a>
						</td>
						<td>
							Open
						</td>
					</tr>
					
					<tr class="row-base">
						<td colspan="3">
							<p>
								<strong>assigned to:</strong> nblevins, <strong>requestor:</strong> DCS - crystal
								blevins</p>
						</td>
					</tr>
					
					<tr class="row-a">
						<td class="first"  rowspan="2">
							<asp:CheckBox ID="CheckBox5" Text="# 4289" runat="server" />
						</td>
						<td>
							<a href="index.html">SAM - FAQ</a>
						</td>
						<td>
							<a href="index.html">
								<%= DateTime.Now.ToShortDateString() %></a>
						</td>
						<td>
							Open
						</td>
					</tr>
					
					<tr class="row-base">
						<td colspan="3">
							<p>
								<strong>assigned to:</strong> nblevins, <strong>requestor:</strong> DCS - crystal
								blevins</p>
						</td>
					</tr>
					
					<tr class="row-a">
						<td class="first"  rowspan="2">
							<asp:CheckBox ID="CheckBox6" Text="# 4289" runat="server" />
						</td>
						<td>
							<a href="index.html">HMIS - Error</a>
						</td>
						<td>
							<a href="index.html">
								<%= DateTime.Now.ToShortDateString() %></a>
						</td>
						<td>
							Closed
						</td>
					</tr>
					
					<tr class="row-base">
						<td colspan="3">
							<p>
								<strong>assigned to:</strong> nblevins, <strong>requestor:</strong> DCS - crystal
								blevins</p>
						</td>
					</tr>
					
					<tr class="row-a">
						<td class="first"  rowspan="2">
							<asp:CheckBox ID="CheckBox7" Text="# 4289" runat="server" />
						</td>
						<td>
							<a href="index.html">SAM - FAQ</a>
						</td>
						<td>
							<a href="index.html">
								<%= DateTime.Now.ToShortDateString() %></a>
						</td>
						<td>
							NGH
						</td>
					</tr>
					
					<tr class="row-base">
						<td colspan="3">
							<p>
								<strong>assigned to:</strong> nblevins, <strong>requestor:</strong> DCS - crystal
								blevins</p>
						</td>
					</tr>
					
					<tr class="row-a">
						<td class="first"  rowspan="2">
							<asp:CheckBox ID="CheckBox8" Text="# 4289" runat="server" />
						</td>
						<td>
							<a href="index.html">SAM - FAQ</a>
						</td>
						<td>
							<a href="index.html">
								<%= DateTime.Now.ToShortDateString() %></a>
						</td>
						<td>
							Open
						</td>
					</tr>
					
					<tr class="row-base">
						<td colspan="3">
							<p>
								<strong>assigned to:</strong> nblevins, <strong>requestor:</strong> DCS - crystal
								blevins</p>
						</td>
					</tr>
					
					<tr class="row-a">
						<td class="first"  rowspan="2">
							<asp:CheckBox ID="CheckBox9" Text="# 4289" runat="server" />
						</td>
						<td>
							<a href="index.html">Stars - Something</a>
						</td>
						<td>
							<a href="index.html">
								<%= DateTime.Now.ToShortDateString() %></a>
						</td>
						<td>
							Open
						</td>
					</tr>
					
					<tr class="row-base">
						<td colspan="3">
							<p>
								<strong>assigned to:</strong> nblevins, <strong>requestor:</strong> DCS - crystal
								blevins</p>
						</td>
					</tr>
					
					<tr class="row-a">
						<td class="first"  rowspan="2">
							<asp:CheckBox ID="CheckBox10" Text="# 4289" runat="server" />
						</td>
						<td>
							<a href="index.html">HMIS - Mod1</a>
						</td>
						<td>
							<a href="index.html">
								<%= DateTime.Now.ToShortDateString() %></a>
						</td>
						<td>
							Open
						</td>
					</tr>
					
					<tr class="row-base">
						<td colspan="3">
							<p>
								<strong>assigned to:</strong> nblevins, <strong>requestor:</strong> DCS - crystal
								blevins</p>
						</td>
					</tr>
					
				</table>
				
				<p>Batch Edits:</p>
				
				
				
				<p class="bottom">
					&nbsp;</p>
			</div>

</asp:Content>

