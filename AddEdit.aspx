<%@ Page Language="C#" MasterPageFile="~/ControlPanel.master" AutoEventWireup="true"
    CodeFile="AddEdit.aspx.cs" Inherits="AddEdit" Title="<%$ Resources:PageTitle %>" ValidateRequest="false" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:MultiView runat="server" ID="mvAddEdit" ActiveViewIndex="0">
        <asp:View runat="server" ID="vAddEdit">
            <h1>
                <asp:Literal runat="server" ID="litTitle"></asp:Literal>
                <asp:Literal runat="server" ID="Literal6" Text="<%$ Resources:PageHeading %>"></asp:Literal>
            </h1>
            <asp:PlaceHolder runat="server" ID="plcForm"></asp:PlaceHolder>
            <br />                               
            <asp:Button runat="server" ID="btnSave" Text="<%$ Resources:btnSave %>" ValidationGroup="AddEdit" OnClick="btnSave_Clicked" />
        </asp:View>
        <asp:View ID="vSuccess" runat="server">
            <h1>
                <asp:Literal runat="server" ID="litSavedTitle"></asp:Literal>
                <asp:Literal runat="server" ID="Literal1" Text="<%$ Resources:Saved %>"></asp:Literal>
            </h1>
            <br />
            <p>                
                <asp:Literal runat="server" ID="litSuccessItemType"></asp:Literal> 
                <asp:Literal runat="server" ID="Literal2" Text="<%$ Resources:SuccessMessage %>"></asp:Literal>
                </p>
                <p>
                    <asp:Literal runat="server" ID="Literal3" Text="<%$ Resources:ViewItem %>"></asp:Literal>
                    <asp:HyperLink runat="server" ID="hlViewItem">
                        <asp:Literal runat="server" ID="Literal4" Text="<%$ Resources:ClickHere %>"></asp:Literal>
                    </asp:HyperLink>.
                </p>
                <br />
                <br />
                <p>
                    <asp:HyperLink runat="server" ID="hlReturnToList">
                        <asp:Literal runat="server" ID="Literal5" Text="<%$ Resources:Return %>"></asp:Literal>
                    </asp:HyperLink>
                </p>
        </asp:View>
    </asp:MultiView>
</asp:Content>
