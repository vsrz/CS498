<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Access-Denied.aspx.cs" Inherits="Access_Denied" Title="<%$ Resources:litPageTitle %>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        <asp:Literal runat="server" ID="litAccessDeniedHeader" Text=" <%$ Resources:lblAccessDeniedHeader %>">
        </asp:Literal>
    </h1>
    <p style="width: 420px;">
        <asp:Literal runat="server" ID="litAccessDeniedBody" Text=" <%$ Resources:lblAccessDeniedBody %>">
        </asp:Literal>
    </p>
    <p>
        <b>
            <asp:Literal runat="server" ID="litAccessDeniedRolesRequired" Text=" <%$ Resources:lblAccessDeniedRolesRequired %>">
            </asp:Literal>
            <asp:Literal runat="server" ID="litRolesNeeded">
            </asp:Literal>
        </b>
    </p>
</asp:Content>
    