<%@ Page Language="C#" MasterPageFile="~/Media.master" AutoEventWireup="true"
    CodeFile="Media-ViewVideo.aspx.cs" Inherits="Media_ViewVideo" Title="<%$ Resources:litPageTitle %>" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <h1>
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:litVideo %>"></asp:Literal>&nbsp;&gt;&gt;
        <asp:Literal runat="server" ID="litVideoTitle"></asp:Literal></h1>
    <p>
        <asp:HyperLink runat="server" ID="hlVideoDownload"></asp:HyperLink>
    </p>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="MonkData"
        TableName="jkp_Videos" Where="Vid_ID == Guid( @Vid_ID)">
        <WhereParameters>
            <asp:QueryStringParameter Name="Vid_ID" QueryStringField="videoid" Type="Object" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" AutoGenerateRows="False"
        DataKeyNames="Vid_ID" DataSourceID="LinqDataSource1"
        BorderColor="White" Width="600px" FieldHeaderStyle-Font-Bold="true" FieldHeaderStyle-Width="150px" FieldHeaderStyle-Height="20px" FieldHeaderStyle-VerticalAlign="Top" >
        <Fields>
            <asp:ImageField DataImageUrlField="Vid_PreviewSmallUrl"></asp:ImageField>
            
            <asp:BoundField DataField="Vid_Title" HeaderText="<%$ Resources:lblTitle %>" SortExpression="Vid_Title"
                 />
            <asp:BoundField DataField="Vid_Subject" HeaderText="<%$ Resources:lblSubject %>" SortExpression="Vid_Subject" ItemStyle-Width="500px" HtmlEncode="false" />
            <asp:BoundField DataField="Vid_Summary" HeaderText="<%$ Resources:lblSummary %>" SortExpression="Vid_Summary" HtmlEncode="false" />
            <asp:BoundField DataField="Vid_Size" HeaderText="<%$ Resources:lblSize %>" SortExpression="Vid_Size" />
            <asp:BoundField DataField="Vid_Duration" HeaderText="<%$ Resources:lblDuration %>" SortExpression="Vid_Duration" />
            <asp:BoundField DataField="Vid_DateAdded" HeaderText="<%$ Resources:lblDateAdded %>" SortExpression="Vid_DateAdded" />
            <asp:BoundField DataField="Vid_DateModified" HeaderText="<%$ Resources:lblDateModified %>" SortExpression="Vid_DateModified" />
            <asp:BoundField DataField="Vid_Genre" HeaderText="<%$ Resources:lblGenre %>" SortExpression="Vid_Genre" />
            <asp:BoundField DataField="Vid_Language" HeaderText="<%$ Resources:lblLanguage %>" SortExpression="Vid_Language" />
            <asp:BoundField DataField="Vid_Credits" HeaderText="<%$ Resources:lblCredits %>" SortExpression="Vid_Credits" HtmlEncode="false" />
            <asp:BoundField DataField="Vid_Contents" HeaderText="<%$ Resources:lblContents %>" SortExpression="Vid_Contents" />
            <asp:BoundField DataField="Vid_Description" HeaderText="<%$ Resources:lblDescription %>" SortExpression="Vid_Description" />
            <asp:BoundField DataField="Vid_Year" HeaderText="<%$ Resources:lblYear %>" SortExpression="Vid_Year" />
            <asp:BoundField DataField="Vid_Disc_Number" HeaderText="<%$ Resources:lblDiscNumber %>" SortExpression="Vid_Disc_Number" />
            
        </Fields>
    </asp:DetailsView>
    
</asp:Content>
