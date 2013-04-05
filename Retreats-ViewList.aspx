<%@ Page Language="C#" MasterPageFile="ControlPanel.master" AutoEventWireup="true"
    CodeFile="Retreats-ViewList.aspx.cs" Inherits="Retreats_ViewList" Title="Retreats - View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Retreats</h1>
        
        <div style="float: left; width: 400px;">
            <fieldset>
                <legend>Upcoming Retreats</legend>
                <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="MonkData" 
                                    TableName="jkp_Retreats" OrderBy="Ret_StartDate" 
                                    Where="Ret_StartDate &gt; @Ret_StartDate">
                    <WhereParameters>
                        <asp:SessionParameter Name="Ret_StartDate" SessionField="CurrentDateTime" 
                            Type="DateTime" />
                    </WhereParameters>
                </asp:LinqDataSource>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Ret_ID" 
                                    DataSourceID="LinqDataSource1" BorderColor="White" BorderStyle="None" HeaderStyle-BorderStyle="None" HeaderStyle-BorderColor="White"     >
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="Ret_ID" DataNavigateUrlFormatString="Retreat-PublicView.aspx?retreatid={0}"
                        HeaderText="Retreat Name" DataTextField="Ret_Name" HeaderStyle-Width="175px" />
                        
                        <asp:BoundField DataField="Ret_StartDate" HeaderText="Start Date" 
                            SortExpression="Ret_StartDate" DataFormatString="{0:D}" />
                    </Columns>
                </asp:GridView>
            </fieldset>
        </div>
        <div style="">
            <fieldset>
                <legend>People You Went To Retreats With</legend>
                <asp:Repeater runat="server" ID="rpAttendedRetreatWith" OnItemDataBound="rpAttendedRetreatWith_OnItemDataBound">
                    <HeaderTemplate>
                        <table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="width: 250px;"><asp:Literal runat="server" ID="litPersonAttendingRetreat"></asp:Literal></td>
                            <td style=""><asp:Literal runat="server" ID="litRetreatName"></asp:Literal></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </fieldset>
            <fieldset>
                <legend>Retreats You've Signed Up For</legend>
                
                <asp:Repeater runat="server" ID="rpRetreatsAttended" OnItemDataBound="rpRetreatsAttended_OnItemDataBound">
                    <HeaderTemplate>
                        <table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="width: 350px;"><asp:Hyperlink runat="server" ID="linkRetreatName"></asp:Hyperlink></td>
                            <td><asp:HyperLink runat="server" ID="hlSignupInformation" Text="Signup Information"></asp:HyperLink></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                
                
            </fieldset>
            
        </div>
        <hr style="clear: both; display: block; visibility: hidden;" />
    
    <br />
    
</asp:Content>
