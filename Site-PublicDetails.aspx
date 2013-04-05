<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Site-PublicDetails.aspx.cs" Inherits="Site_PublicDetails" Title="Sites - Public Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1><asp:Literal runat="server" ID="litTitleSiteName"></asp:Literal></h1>

<div class="sidebar-details" style="">
    <h2>Site Details</h2>
    <p>
        Name: <asp:Literal runat="server" ID="litSiteName"></asp:Literal>
    </p>
    <asp:Panel runat="server" ID="pnlSiteName" Visible="false">
        <p>
            Website: <asp:HyperLink runat="server" ID="hlWebsite">Visit Website</asp:HyperLink>
        </p>
    </asp:Panel>
    <p>
        Address: <asp:Literal runat="server" ID="litAddress"></asp:Literal>
    </p>
    <p>
    <asp:HyperLink runat="server" ID="hlEvents" NavigateUrl="~/Events.aspx">View Our Events</asp:HyperLink>
    
    </p>
</div>
<div>
    <asp:Literal runat="server" ID="litDescription"></asp:Literal>
</div>

</asp:Content>

