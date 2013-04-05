<%@ Page Language="C#" MasterPageFile="~/ControlPanel.master" AutoEventWireup="true" CodeFile="Admin-UserOptions.aspx.cs" Inherits="Admin_UserOptions" Title="Administration User Options" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>
    Person Details: <asp:Literal runat="server" ID="litUserName"></asp:Literal>
</h1>
<asp:HyperLink runat="server" ID="hlEditMembership">Edit Membership</asp:HyperLink>
<br />
<asp:HyperLink runat="server" ID="hlEditUserDetails">Edit User Details</asp:HyperLink>
<br />
<asp:HyperLink runat="server" ID="hlEditRoles">Edit Role Assignments</asp:HyperLink>
<br />
<asp:HyperLink runat="server" ID="hlLoginAsUser">Login As User</asp:HyperLink>

</asp:Content>

