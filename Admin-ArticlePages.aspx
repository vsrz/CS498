<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Admin-ArticlePages.aspx.cs" Inherits="Admin_ArticlePages" Title="<%$ Resources:litPageTitle %>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        <asp:Literal ID="Literal1" Text="<%$ Resources:lblPageTitle %>" runat="server"></asp:Literal></h1>
    <p>
        <asp:Literal ID="Literal2" Text="<%$ Resources:lblDesc %>" runat="server"></asp:Literal>
    </p>
    <fieldset>
        <asp:Repeater runat="server" ID="rpArticlePages" OnItemDataBound="rpArticlePages_OnItemDataBound">
            <HeaderTemplate>
                <table>
                    <tr style="font-weight: bold;">
                        <td style="width: 200px;"><asp:Literal ID="Literal1" Text="<%$ Resources:lblTitleResource %>" runat="server"></asp:Literal></td>
                        <td style="width: 200px;"><asp:Literal ID="Literal3" Text="<%$ Resources:lblCategory %>" runat="server"></asp:Literal></td>
                        <td><asp:Literal ID="Literal4" Text="<%$ Resources:lblUrl %>" runat="server"></asp:Literal></td>
                    </tr>
                
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><asp:Literal runat="server" ID="litPageTitle"></asp:Literal></td>
                    <td><asp:Literal runat="server" ID="litCategory"></asp:Literal></td>
                    <td><asp:Literal runat="server" ID="litPageUrl"></asp:Literal></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </fieldset>
</asp:Content>
