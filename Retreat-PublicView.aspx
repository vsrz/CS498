<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Retreat-PublicView.aspx.cs" 
    Inherits="Retreat_PublicView" Title="<%$ Resources:PageTitle %>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1><asp:Literal runat="server" ID="litRetreatName"></asp:Literal></h1>


<div class="sidebar-details" style="">
    <h2>
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:PageHeader %>"></asp:Literal>
    </h2>
    
    
    <p>
    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Language %>"></asp:Literal>
    <asp:Literal runat="server" ID="litLanguage"></asp:Literal>
    </p><p>
    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:RemainingSpots %>"></asp:Literal>
    <asp:Literal runat="server" ID="litRemainingSpots"></asp:Literal>
    </p><p>    
    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:StartDate%>"></asp:Literal>
    <asp:Literal runat='server' ID="litStartDate"></asp:Literal>
    </p>
    <p>
    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:EndDate %>"></asp:Literal>
    <asp:Literal runat="server" ID="litEndDate"></asp:Literal>    
    </p>
    <h2>
        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:Location %>"></asp:Literal>
    </h2>
    <br /><p>
    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:Site %>"></asp:Literal>
    <asp:Literal runat="server" ID="litSite"></asp:Literal>
    </p><p>
    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:Address %>"></asp:Literal>
    <asp:Literal runat="server" ID="litAddress"></asp:Literal>
    </p><p>
    
    <asp:Button runat="server" ID="hlRegisterForReteat" Text="<%$ Resources:btnRegister %>" OnClick="RegisterButtonClicked" />
    </p>
</div>
<div>
    <asp:Literal runat="server" ID="litDescription"></asp:Literal>
</div>

</asp:Content>

