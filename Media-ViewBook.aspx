<%@ Page Language="C#" MasterPageFile="~/Media.master" AutoEventWireup="true" CodeFile="Media-ViewBook.aspx.cs"
    Inherits="Media_ViewBook" Title="<%$ Resources:litPageTitle %>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:litBook %>"></asp:Literal>&nbsp;&gt;&gt;
        <asp:Literal runat="server" ID="litTitle"></asp:Literal>
    </h1>
    
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="MonkData"
        TableName="jkp_Books" Where="Bk_ID == Guid(@Bk_ID)">
        <WhereParameters>
            <asp:QueryStringParameter Name="Bk_ID" QueryStringField="bookid" Type="Object" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" AutoGenerateRows="False"
        DataKeyNames="Bk_ID" DataSourceID="LinqDataSource1" BorderColor="White" Width="600px"
        FieldHeaderStyle-Font-Bold="true" FieldHeaderStyle-Width="150px" FieldHeaderStyle-Height="20px"
        FieldHeaderStyle-VerticalAlign="Top">
        <Fields>
            <asp:BoundField DataField="Bk_Title" HeaderText="<%$ Resources:lblTitle %>" SortExpression="Bk_Title" />
            <asp:BoundField DataField="Bk_Size" HeaderText="<%$ Resources:lblSize %>" SortExpression="Bk_Size" />
            <asp:BoundField DataField="Bk_DateAdded" DataFormatString="{0:D}" HeaderText="<%$ Resources:lblDateAdded %>"
                SortExpression="Bk_DateAdded" />
            <asp:BoundField DataField="Bk_DateModified" HeaderText="<%$ Resources:lblDateModified %>" SortExpression="Bk_DateModified" />
            <asp:BoundField DataField="Bk_Genre" HeaderText="<%$ Resources:lblGenre %>" SortExpression="Bk_Genre" />
            <asp:BoundField DataField="Bk_Language" HeaderText="<%$ Resources:lblLanguage %>" SortExpression="Bk_Language" />
            <asp:BoundField DataField="Bk_Edition" HeaderText="<%$ Resources:lblEdition %>" SortExpression="Bk_Edition" />
            <asp:BoundField DataField="Bk_Publisher" HeaderText="<%$ Resources:lblPublisher %>" SortExpression="Bk_Publisher" />
            <asp:BoundField DataField="Bk_Subject" HeaderText="<%$ Resources:lblSubject %>" HtmlEncode="False"
                SortExpression="Bk_Subject" />
            <asp:BoundField DataField="Bk_Contents" HeaderText="<%$ Resources:lblContents %>" HtmlEncode="False"
                SortExpression="Bk_Contents" />
            <asp:BoundField DataField="Bk_Description" HeaderText="<%$ Resources:lblDescription %>" HtmlEncode="False"
                SortExpression="Bk_Description" />
            <asp:BoundField DataField="Bk_Summary" HeaderText="<%$ Resources:lblSummary %>" SortExpression="Bk_Summary" HtmlEncode="false" />
            <asp:BoundField DataField="Bk_Year" HeaderText="<%$ Resources:lblYear %>" SortExpression="Bk_Year" />
            
            <asp:BoundField DataField="Bk_Comment" HeaderText="<%$ Resources:lblComment %>" HtmlEncode="False"
                SortExpression="Bk_Comment" />
        </Fields>
    </asp:DetailsView>
</asp:Content>
