<%@ Page Language="C#" MasterPageFile="~/Assets/MasterPages/Main.master" AutoEventWireup="true" CodeFile="AdminQueue.aspx.cs" Inherits="Administration_AdminQueue" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
	</asp:ScriptManagerProxy>


	<div class="content_panel">
	
		<h1>Please select an action.</h1>
		
		<cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" Collapsed="true" CollapseControlID="pnlEditQueueCtrl"  ExpandControlID="pnlEditQueueCtrl" TargetControlID="pnlEditQueue" runat="server">
		</cc1:CollapsiblePanelExtender>
		
		<cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" Collapsed="true" CollapseControlID="pnlNewQueueCtrl" ExpandControlID="pnlNewQueueCtrl" TargetControlID="pnlNewQueue" runat="server">
		</cc1:CollapsiblePanelExtender>
		
		
		<asp:Panel runat="server" ID="pnlEditQueueCtrl" >
			<p><img src="../App_Themes/Default/images/icons/file_edit.png" /> Edit Queue</p>
		</asp:Panel>
		<asp:Panel runat="server" ID="pnlEditQueue" >
			<br />
			<asp:Button ID="btnEditQueue" runat="server" Text="Edit" />
		</asp:Panel>
		
		<asp:Panel runat="server" ID="pnlNewQueueCtrl" >
		
			<p><img src="../App_Themes/Default/images/icons/file_add.png" /> Create New Queue</p>
		
		</asp:Panel>
		
		<asp:Panel runat="server" ID="pnlNewQueue" >
		
			<p>Please complete the following fields.</p>
			
			<div id="New Queue">
			
				<p>Name<br />
				<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></p>
				<p>Description<br />
				<asp:TextBox ID="TextBox2" TextMode="MultiLine" Width="500px" Height="50px" runat="server"></asp:TextBox></p>
					
				<asp:ImageButton ID="ImageButton1" ImageUrl="~/App_Themes/Default/images/icons/save.png" ToolTip="Save Queue" runat="server" />
				<asp:HyperLink ID="HyperLink1" runat="server">Save Queue</asp:HyperLink>
			</div>
			
				
		</asp:Panel>		
		
		
		<p class="bottom"></p>

	</div>
</asp:Content>

