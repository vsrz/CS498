<%@ Page Language="C#" MasterPageFile="~/ControlPanel.master" AutoEventWireup="true"
    CodeFile="Report-EmailAddresses.aspx.cs" Inherits="Report_EmailAddresses" Title="Administration - E-Mail Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Email Addresses For Users</h1>
    <br />
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="MonkData"
        OrderBy="Per_FirstName" Select="new ( Per_FirstName as FirstName, Per_LastName as LastName, Per_Email as Email, jkp_Address.Add_State as State, jkp_Address.Add_Country as Country, Per_PrimaryLanguage as FirstLanguage, Per_SecondaryLanguage as SecondLanguage)"
        TableName="jkp_Persons">
    </asp:LinqDataSource>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="gvEmail" runat="server" AllowSorting="True" DataSourceID="LinqDataSource1" 
                    AutoGenerateColumns="False" GridLines="Horizontal" Width="800">
            <Columns>
                <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" />
                <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
                <asp:BoundField DataField="FirstLanguage" HeaderText="Primary Language" SortExpression="FirstLanguage" />
                <asp:BoundField DataField="SecondLanguage" HeaderText="Secondary Language" SortExpression="SecondLanguage" />
            </Columns>
            </asp:GridView><br />
            <asp:Button runat="server" ID="btnEmailExportCSV" Text="Export to CSV" OnClick="EmailExportCSV" />
            <asp:HyperLink runat="server" ID="hypEmailCSVDownload" Visible="false" Text="Download"></asp:HyperLink>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
