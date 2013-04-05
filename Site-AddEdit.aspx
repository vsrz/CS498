<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Site-AddEdit.aspx.cs" 
    Inherits="Sites_AddEdit" Title="<%$ Resources:PageTitle %>" ValidateRequest="false" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:MultiView runat="server" ID="mvAddEdit" ActiveViewIndex="0">
        <asp:View runat="server" ID="vAddEdit">
            <h1>
                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:PageHeading %>"></asp:Literal>    
            </h1>
            <br />
            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:EnglishName %>"></asp:Literal>
            <asp:TextBox runat="server" ID="txtEnglishName"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEnglishName"
                Text="*"></asp:RequiredFieldValidator>
            <br />
            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Description %>"></asp:Literal>
            <br />
            <FTB:FreeTextBox runat="server" ID="txtDescription" Width="800px">
            </FTB:FreeTextBox>
            <br />
            <asp:Button runat="server" ID="btnSave" Text="<%$ Resources:btnSave %>" OnClick="btnSave_Clicked" />
        </asp:View>
        <asp:View ID="vSuccess" runat="server">
            <h1>
                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:SiteSaved %>"></asp:Literal>
            </h1>
            <br />
            <p>
                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:RetreatSaved %>"></asp:Literal>
            </p>
        </asp:View>
    </asp:MultiView>
</asp:Content>

