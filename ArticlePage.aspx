<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ArticlePage.aspx.cs" Inherits="ArticlePage" Title="<%$ Resources:litPageTitle %>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="sidebar-details" style="float: left; margin-right: 15px;">
        <asp:Literal runat="server" ID="litCategoryName"></asp:Literal>
        <asp:Literal runat="server" ID="Literal1" Text="<%$ Resources:litPageTitle %>"></asp:Literal>
        <br />
        <asp:Repeater runat="server" ID="rpArticles" OnItemDataBound="rpArticles_OnItemDataBound">
            <ItemTemplate>
                <asp:HyperLink runat="server" ID="hlArticle"></asp:HyperLink>
                <br />
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div style="float: left; width: 600px;">
        <h1>
            <asp:Literal runat="server" ID="litPageTitle"></asp:Literal></h1>
        <span style="font-size: 11px; color:Gray; font-style: italic;" runat="server" visible="false" id="spanAuthorName">By <asp:Literal runat="server" ID="litAuthor"></asp:Literal></span>
        <br />
        <asp:Literal runat="server" ID="litContent"></asp:Literal>
    </div>
</asp:Content>
