<%@ Page Language="C#" MasterPageFile="~/ControlPanel.master" AutoEventWireup="true"
    CodeFile="Retreat-AdminOptions.aspx.cs" Inherits="Retreat_AdminOptions" Title="<%$ Resources:PageTitle %>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        <asp:Literal runat="server" ID="litRetreatName" Text="<%$ Resources:PageTitle %>"></asp:Literal></h1>
    <p>
        <asp:HyperLink runat="server" ID="hlActAddEditRooms">
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:ManageRooms %>"></asp:Literal>
        </asp:HyperLink>
    </p>
    <p>
        <asp:HyperLink runat="server" ID="hlActAssignPeople">
            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:AssignRooms %>"></asp:Literal>
        </asp:HyperLink>
    </p>
    <p>
        <asp:HyperLink runat="server" ID="hlActEditRetreatDetals">
            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:EditDetails %>"></asp:Literal>
        </asp:HyperLink>
    </p>
    <p>
        <asp:HyperLink runat="server" ID="hlActViewRetreatants">            
            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:ViewRetreatants %>"></asp:Literal>
        </asp:HyperLink>
    </p>
    <p>
        <asp:HyperLink runat="server" ID="hlActAssignments">            
            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:ManageRetreatAssignments %>"></asp:Literal>
        </asp:HyperLink>
    </p>
    <p>
        <asp:HyperLink runat="server" ID="hlActActivites">            
            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:ManageRetreatActivities %>"></asp:Literal>
        </asp:HyperLink>
    </p>
    <p>
        <asp:HyperLink runat="server" ID="hlActBuilder">            
            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:QuickImport %>"></asp:Literal>
        </asp:HyperLink>
    </p>
    <p>
        <asp:HyperLink runat="server" ID="hlActDiscussionGroups">            
            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:AddEditDiscussionGroups %>"></asp:Literal>
        </asp:HyperLink>
    </p>
    <p>
        <asp:HyperLink runat="server" ID="hlContributions">            
            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:ContributionsAdministration %>"></asp:Literal>
        </asp:HyperLink>
    </p>
    
   
</asp:Content>
