<%@ Page Language="C#" MasterPageFile="~/Media.master" AutoEventWireup="true" CodeFile="Media-ViewAudio.aspx.cs"
    Inherits="Media_ViewAudio" Title="Media - Audio View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Audio &gt;&gt;
        <asp:Literal runat="server" ID="litAudioItem"></asp:Literal></h1>
        <p>
            <asp:HyperLink runat="server" ID="hlDownload">
                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:litDownload %>"></asp:Literal>
            </asp:HyperLink>
        </p>
    <asp:LinqDataSource ID="linqAudio" runat="server" ContextTypeName="MonkData" TableName="jkp_Audios"
        Where="Aud_ID == Guid( @Aud_ID)">
        <WhereParameters>
            <asp:QueryStringParameter Name="Aud_ID" QueryStringField="audioid" Type="Object" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" DataSourceID="linqAudio"
        BorderColor="White" Width="600px" FieldHeaderStyle-Font-Bold="true" FieldHeaderStyle-Width="150px"
        FieldHeaderStyle-Height="20px" FieldHeaderStyle-VerticalAlign="Top" 
    AutoGenerateRows="False" DataKeyNames="Aud_ID">
<FieldHeaderStyle VerticalAlign="Top" Font-Bold="True" Height="20px" Width="150px"></FieldHeaderStyle>
        <Fields>
            
            <asp:BoundField DataField="Aud_Size" HeaderText="<%$ Resources:lblSize %>" 
                SortExpression="Aud_Size" />
            <asp:BoundField DataField="Aud_Duration" HeaderText="<%$ Resources:lblDuration %>" 
                SortExpression="Aud_Duration" />
            <asp:BoundField DataField="Aud_DateAdded" HeaderText="<%$ Resources:lblDateAdded %>" 
                SortExpression="Aud_DateAdded" />
            <asp:BoundField DataField="Aud_DateModified" HeaderText="<%$ Resources:lblDateModified %>" 
                SortExpression="Aud_DateModified" />
            <asp:BoundField DataField="Aud_Title" HeaderText="<%$ Resources:lblTitle %>" 
                SortExpression="Aud_Title" />
            <asp:BoundField DataField="Aud_Genre" HeaderText="<%$ Resources:lblGenre %>" 
                SortExpression="Aud_Genre" />
            <asp:BoundField DataField="Aud_Language" HeaderText="<%$ Resources:lblLanguage %>" 
                SortExpression="Aud_Language" />
            <asp:BoundField DataField="Aud_Subject" HeaderText="<%$ Resources:lblSubject %>" 
                SortExpression="Aud_Subject" />
            <asp:BoundField DataField="Aud_Contents" HeaderText="<%$ Resources:lblContents %>" 
                HtmlEncode="False" SortExpression="Aud_Contents">
                <ItemStyle VerticalAlign="Top" />
            </asp:BoundField>
            <asp:BoundField DataField="Aud_Description" HeaderText="<%$ Resources:lblDescription %>" 
                HtmlEncode="False" SortExpression="Aud_Description" />
            <asp:BoundField DataField="Aud_Summary" HeaderText="<%$ Resources:lblSummary %>" HtmlEncode="False" 
                SortExpression="Aud_Summary" />
            <asp:BoundField DataField="Aud_Year" HeaderText="<%$ Resources:lblYear %>" 
                SortExpression="Aud_Year" />
            <asp:BoundField DataField="Aud_Disc_Number" HeaderText="<%$ Resources:lblDiscNumber %>" 
                SortExpression="Aud_Disc_Number" />
            <asp:BoundField DataField="Aud_Track_Number" HeaderText="<%$ Resources:lblTrackNumber %>" 
                SortExpression="Aud_Track_Number" />
            <asp:BoundField DataField="Aud_Comment" HeaderText="<%$ Resources:lblComment %>" HtmlEncode="False" 
                SortExpression="Aud_Comment" />
            
        </Fields>
    </asp:DetailsView>
</asp:Content>
