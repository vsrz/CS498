<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Address-AddEdit.aspx.cs" Inherits="Address_AddEdit" Title="<%$ Resources:litPageTitle %>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:MultiView runat="server" ID="mvAddEdit" ActiveViewIndex="0">
        <asp:View runat="server" ID="vAddEdit">
            <h1>
                <asp:Literal ID="Literal1" text="<%$ Resources:lblAddEdit %>" runat="server"></asp:Literal>
            </h1>
            <div>
                <div style="width: 10%; float: left;">
                    <asp:Literal ID="Literal2" text="<%$ Resources:lblStreet %>" runat="server"></asp:Literal>
                    <br />
                    <asp:Literal ID="Literal3" text="<%$ Resources:lblCity %>" runat="server"></asp:Literal>
                    <br />
                    <asp:Literal ID="Literal4" text="<%$ Resources:lblState %>" runat="server"></asp:Literal>
                    <br />
                    <asp:Literal ID="Literal5" text="<%$ Resources:lblPostalCode %>" runat="server"></asp:Literal>
                    <br />
                    <asp:Literal ID="Literal6" text="<%$ Resources:lblCountry %>" runat="server"></asp:Literal>
                    <br /><br />
                    <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Clicked" />
                </div>
                <div style="width: 89%; float:right;">
                    <asp:TextBox runat="server" ID="txtAddress"></asp:TextBox>
                    <br />
                    <asp:TextBox runat="server" ID="txtCity"></asp:TextBox>
                    <br />
                    <asp:TextBox runat="server" ID="txtState"></asp:TextBox>
                    <br />
                    <asp:TextBox runat="server" ID="txtZip"></asp:TextBox>
                    <br />
                    <asp:TextBox runat="server" ID="txtCountry"></asp:TextBox>
                    <br />
                </div>
            </div>
        </asp:View>
        <asp:View ID="vSuccess" runat="server">
            <h1>
                <asp:Literal ID="Literal7" text="<%$ Resources:lblSiteSaved %>" runat="server"></asp:Literal></h1>
            <br />
            <p>
                <asp:Literal ID="Literal8" text="<%$ Resources:lblRetreatSaved %>" runat="server"></asp:Literal></p>
        </asp:View>
    </asp:MultiView>
</asp:Content>
