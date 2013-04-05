<%@ Page Language="C#" MasterPageFile="~/ControlPanel.master" AutoEventWireup="true"
    CodeFile="Admin-AllTables.aspx.cs" Inherits="Admin_AllTables" Title="<%$ Resources:litPageTitle %>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        <asp:Literal ID="Literal2" Text="<%$ Resources:lblHeading %>" runat="server"></asp:Literal></h1>
    <p>
        <asp:Literal ID="Literal1" Text="<%$ Resources:lblDesc %>" runat="server"></asp:Literal>
    </p>
    <fieldset>
        <legend><asp:Literal ID="Literal3" Text="<%$ Resources:lblDbaseAccess %>" runat="server"></asp:Literal></legend>
        <table>
            <tr>
                <td valign="top">
                    <h2>
                        <asp:Literal ID="Literal4" Text="<%$ Resources:lblAddItems %>" runat="server"></asp:Literal></h2>
                    <asp:PlaceHolder runat="server" ID="plcAddItemsList"></asp:PlaceHolder>
                </td>
                <td valign="top">
                    <h2>
                        <asp:Literal ID="Literal5" Text="<%$ Resources:lblViewItems %>" runat="server"></asp:Literal></h2>
                    <asp:PlaceHolder runat="server" ID="plcViewItems"></asp:PlaceHolder>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
