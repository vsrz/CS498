<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="Events.aspx.cs" Inherits="Events" Title="<%$ Resources:litPageTitle %>" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <h1><asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:litPageTitle %>"></asp:Literal></h1>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="MonkData" OrderBy="Ret_StartDate" 
        Select="new (Ret_Name, Ret_StartDate, Ret_EndDate, Ret_ArrivalTime, Ret_DepartureTime, Ret_Description, Ret_Site_ID, Ret_ID)" 
        TableName="jkp_Retreats" Where="Ret_StartDate &gt;= @Ret_StartDate">
        <WhereParameters>
            <asp:SessionParameter Name="Ret_StartDate" SessionField="CurrentDateTime" 
                Type="DateTime"  />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="srcEnglishSites" ContextTypeName="MonkData" OrderBy="Site_EnglishName" 
        runat="server" Select="Site_EnglishName" TableName="jkp_Sites">
    </asp:LinqDataSource>
    <div id="eventsContainer">
        <div id="eventsTopPane">
            <fieldset>
                <legend>
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:lblEventsSearchbox %>"></asp:Literal>
                </legend>
                <div style="float: left; width: 49%; vertical-align:middle;">
                    <asp:Literal runat="server" Text="<%$ Resources:lblSite %>"></asp:Literal>
                    <br />
                    <asp:DropDownList runat="server" OnSelectedIndexChanged="ddlEventsSites_OnSelectedIndexChanged"
                        id="ddlEventsSites" AutoPostBack="true" DataSourceID="srcEnglishSites" style="" Width="180px">
                    </asp:DropDownList>
                </div>
                <div style="float: right; width: 49%; vertical-align: middle;">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:lblEventDate %>"></asp:Literal>
                    <br />
                    <asp:TextBox ID="txtEventDate" runat="server" Width="148px" 
                        OnTextChanged="txtEventDate_OnTextChanged" AutoPostBack="true">
                    </asp:TextBox>
                    <%-- <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEventDate" Format="MM/dd/yyyy" /> --%>
                    <br />
                    <asp:CompareValidator id="cpvDateValidator" runat="server" Type="Date" Operator="DataTypeCheck" 
                       ControlToValidate="txtEventDate" ErrorMessage="Invalid Date">
                    </asp:CompareValidator>
                    <span style="color:Red;"><asp:Literal runat="server" ID="litEventDateError"></asp:Literal></span>
                </div>
            </fieldset>
        </div>
        <br />
        <div id="eventsBottomPane" style="text-align: left;">
            <center>
                <h2>
                    <asp:Literal ID="litNoResultsReturned" runat="server" text="<%$ Resources:lblNoResultsReturned %>" Visible="false">
                    </asp:Literal></h2>
            </center>
            <asp:UpdatePanel runat="server" ID="updBottom">
                <ContentTemplate>
                    <asp:Repeater ID="rpEventsListing" runat="server" OnItemDataBound="EventsListing_OnItemDataBound">
                        <HeaderTemplate>
                            <h4>
                                <table>
                                    <tr>
                                        <td style="width: 250px; margin-left: 2px;">
                                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:litEventName %>"></asp:Literal>
                                        </td>
                                        <td style="width: 100px; margin-left: 2px;">
                                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:litStartDate %>"></asp:Literal>
                                        </td>
                                        <td style="width: 100px; margin-left: 2px;">
                                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:litEndDate %>"></asp:Literal>
                                        </td>
                                        <td style="width: 380px; margin-left: 2px;">
                                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:litDescription %>"></asp:Literal>
                                        </td>
                                    </tr>
                            </h4>
                        </HeaderTemplate>
                        <ItemTemplate>
                                <tr style="font-weight: normal;">
                                    <td style="width: 250px; margin-left: 2px; vertical-align: top;">
                                        <asp:HyperLink ID="lblEventName" runat="server" NavigateUrl="Retreat-PublicView.aspx?retreatid=">
                                        </asp:HyperLink>
                                    </td>
                                    <td style="width: 100px; margin-left: 2px; vertical-align: top;">
                                        <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 100px; margin-left: 2px; vertical-align: top;">
                                        <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 380px; margin-left: 2px;">
                                        <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                    </td>
                                </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

