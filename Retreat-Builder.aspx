<%@ Page Language="C#" MasterPageFile="~/ControlPanel.master" AutoEventWireup="true"
    CodeFile="Retreat-Builder.aspx.cs" Inherits="Retreat_Builder" Title="<%$ Resources:PageTitle %>" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:MultiView runat="server" ID="mvCopy" ActiveViewIndex="0">
        <asp:View runat="server">
            <h1>
                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:BuildRetreatItems %>"></asp:Literal>
                <asp:Literal runat="server" ID="litRetreatName"></asp:Literal></h1>
            <p>
                <a href="Retreat-AdminOptions.aspx?retreatid=<%= Request.QueryString["retreatid"] %>">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:ReturnToRetreatTools %>"></asp:Literal>
                </a>
            </p>
            <fieldset>
                <legend>
                    <asp:Literal ID="Lit1" runat="server" Text="<%$ Resources:Instructions %>"></asp:Literal>
                </legend>
                
                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:InstructionsText %>"></asp:Literal>
            </fieldset>
            <fieldset>
                <legend><asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:RetreatDetails %>"></asp:Literal></legend>
                <table>
                    <tr>
                        <td valign="top" style="width: 400px;">
                            <b>
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:Site %>"></asp:Literal>
                            </b>
                                <asp:Literal runat="server" ID="litSite"></asp:Literal>
                            <br />
                            <b>
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:Hamlets %>"></asp:Literal>
                            </b>
                                <asp:Literal runat="server" ID="litHamlets"></asp:Literal>
                            <br />
                            <b>
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:Buildings %>"></asp:Literal>
                            </b>
                            <asp:Literal runat="server" ID="litBuildings"></asp:Literal>
                        </td>
                        <td valign="top">
                            <b>
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:Rooms %>"></asp:Literal>
                            </b> 
                            <asp:Literal runat="server" ID="litRooms"></asp:Literal>
                            <br />
                            <b>
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:Assignments %>"></asp:Literal>
                            </b>
                            <asp:Literal runat="server" ID="litAssingments"></asp:Literal>
                            <br />
                            <b>
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:Activities %>"></asp:Literal>
                            </b>
                            <asp:Literal runat="server" ID="litAcitivities"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend>
                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:GenerateRooms %>"></asp:Literal>
                </legend>
                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:SelectSiteToSeeRetreatsFrom %>"></asp:Literal>
                <asp:DropDownList runat="server" ID="dlSites">
                </asp:DropDownList>
                <asp:Button runat="server" ID="btnGetRetreats" Text="<%$ Resources:btnGetRetreats %>" OnClick="btnGetRetreats_Clicked" />
                <asp:Repeater runat="server" ID="rpRetreats" OnItemDataBound="rpRetreats_OnItemDataBound">
                    <HeaderTemplate>
                        <table>
                            <tr style="font-weight: bold;">
                                <td style="width: 200px;">
                                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:RetreatName %>"></asp:Literal>
                                </td>
                                
                                
                                <td style="width: 100px;">
                                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:AssignmentsTag %>"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:ActivitiesTag %>"></asp:Literal>
                                </td>
                                <td style="width: 150px;">
                                </td>
                                <td style="width: 150px;">
                                </td>
                                <td style="width: 150px;">
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal runat="server" ID="litRetName"></asp:Literal>
                            </td>
                            
                            <td>
                                <asp:Literal runat="server" ID="litAssignmentTypes"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="litActivityTypes"></asp:Literal>
                            </td>
                            <td>
                                <asp:LinkButton runat="server" ID="lbtnCopyRooms" OnClick="lbtnCopyRooms_Clicked" Visible="false">Copy Rooms</asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton runat="server" ID="lbtnCopyAssignments" OnClick="lbtnCopyAssignments_Clicked">Copy Assignments</asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton runat="server" ID="lbtnCopyActivities" OnClick="lbtnCopyActivities_Clicked">Copy Activities</asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </fieldset>
        </asp:View>
        <asp:View runat="server">
            <h1>
                <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:CopySuccessful %>"></asp:Literal>
            </h1>
            <p>
                <a href="Retreat-AdminOptions.aspx?retreatid=<%= Request.QueryString["retreatid"] %>">
                    <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:ReturnToRetreatTools %>"></asp:Literal>    
                </a>
            </p>
            <br />
            <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:CopySuccessNote %>"></asp:Literal>
        </asp:View>
    </asp:MultiView>
</asp:Content>
