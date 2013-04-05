<%@ Page Language="C#" MasterPageFile="~/ControlPanel.master" AutoEventWireup="true"
    CodeFile="ControlPanel.aspx.cs" Inherits="ControlPanel" Title="Control Panel" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <h1>
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:lblTitle %>"></asp:Literal></h1>
        <br />
    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:lblHelpText1 %>"></asp:Literal>
    <br />
    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:lblHelpText2 %>"></asp:Literal>
     
    <br />
    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:lblHelpText3 %>"></asp:Literal>
    
</asp:Content>
