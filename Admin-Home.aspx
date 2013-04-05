<%@ Page Language="C#" MasterPageFile="~/ControlPanel.master" AutoEventWireup="true"
    CodeFile="Admin-Home.aspx.cs" Inherits="Admin_Home" Title="<%$ Resources:litPageTitle %>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        <asp:Literal ID="Literal11" Text="<%$ Resources:litHeading %>" runat="server"></asp:Literal></h1>
    <style type="text/css">
        #body-lists-of-links ul
        {
            list-style-type: none;
            margin-left: 20px;
        }
    </style>
    <div id="body-lists-of-links" class="Admin-home">
        <%--Left Panel--%>
        <div id="left-panel" style="width: 300px;">
            <fieldset runat="server" id="fieldsetViewAllTables">
                <legend><asp:Literal ID="Literal10" Text="<%$ Resources:litMasterAdminHeading %>" runat="server"></asp:Literal></legend>
                <ul>
                    <li>
                        <a href="Admin-AllTables.aspx">
                            <asp:Literal ID="Literal9" Text="<%$ Resources:litAdmin-AllTables %>" runat="server"></asp:Literal>
                        </a>
                        <br />
                        <asp:Literal ID="Literal49" Text="<%$ Resources:litAdmin-Desc1 %>" runat="server"></asp:Literal>
                    </li>
                </ul>
            </fieldset>
            <fieldset runat="server" id="fieldsetRetreats">
                <legend>
                    <asp:Literal runat="server" ID="StaticLiteral_Retreats" Text="<%$ Resources:litRetreatsHeading %>"></asp:Literal>
                </legend>
                <ul>
                    <li runat="server" id="liAddRetreat">
                        <a href="AddEdit.aspx?typename=jkp_Retreat" >
                            <asp:Literal ID="Literal1" Text="<%$ Resources:litRetreat-MenuItem1 %>" runat="server"></asp:Literal>
                        </a>
                        <br />
                        <asp:Literal ID="Literal2" Text="<%$ Resources:litRetreat-Desc1 %>" runat="server"></asp:Literal>
                    </li>
                    <li runat="server" id="liViewRetreats">
                        <a href="GridView.aspx?typename=jkp_Retreat">
                            <asp:Literal ID="Literal3" Text="<%$ Resources:litRetreat-MenuItem2 %>" runat="server"></asp:Literal>
                        </a>
                        <br />
                        <asp:Literal ID="Literal4" Text="<%$ Resources:litRetreat-Desc2 %>" runat="server"></asp:Literal>
                    </li>
                </ul>
            </fieldset>
            <fieldset runat="server" id="fieldsetLocationAdmin">
                <legend>
                    <asp:Literal ID="StaticLiteral_Rooms" Text="<%$ Resources:RoomsSection %>" runat="server">
                    </asp:Literal>
                </legend>
                <ul>
                    <%--Sites--%>
                    <li runat="server" id="liSiteAdmin">
                        <a href="GridView.aspx?typename=jkp_Site">
                            <asp:Literal ID="Literal14" Text="<%$ Resources:Rooms-MenuItem1 %>" runat="server"></asp:Literal>
                        </a>
                        <br />
                        <asp:Literal ID="Literal34" Text="<%$ Resources:Rooms-Desc1 %>" runat="server"></asp:Literal>
                    </li>                    
                    <%--Hamlets--%>
                    <li runat="server" id="liHamletAdmin">
                        <a href="GridView.aspx?typename=jkp_Hamlet">
                            <asp:Literal ID="Literal35" Text="<%$ Resources:Rooms-MenuItem2 %>" runat="server"></asp:Literal>
                        </a>
                        <br />
                        <asp:Literal ID="Literal36" Text="<%$ Resources:Rooms-Desc2 %>" runat="server"></asp:Literal>
                    </li>                    
                    <%--Buildings--%>
                    <li runat="server" id="liBuildingAdmin">
                        <a href="GridView.aspx?typename=jkp_Building">
                            <asp:Literal ID="Literal37" Text="<%$ Resources:Rooms-MenuItem3 %>" runat="server"></asp:Literal>                        
                        </a>
                        <br />
                        <asp:Literal ID="Literal38" Text="<%$ Resources:Rooms-Desc3 %>" runat="server"></asp:Literal>
                    </li>
                    <%--Rooms--%>
                    <li runat="server" id="liRoomsAdmin">
                        <a href="GridView.aspx?typename=jkp_Room">
                        <asp:Literal ID="Literal39" Text="<%$ Resources:Rooms-MenuItem4 %>" runat="server"></asp:Literal>
                        </a>
                        <br />
                        <asp:Literal ID="Literal40" Text="<%$ Resources:Rooms-Desc4 %>" runat="server"></asp:Literal>
                    </li>
                    <%--Room Types--%>
                    <li runat="server" id="liRoomTypeAdmin">
                        <a href="GridView.aspx?typename=jkp_RoomType">
                            <asp:Literal ID="Literal5" Text="<%$ Resources:Rooms-MenuItem5 %>" runat="server"></asp:Literal>
                        </a>
                        <br />
                        <asp:Literal ID="Literal6" Text="<%$ Resources:Rooms-Desc5 %>" runat="server"></asp:Literal>
                    </li>
                    <%--Room Rates--%>
                    <li runat="server" id="liRateAdmin">
                        <a href="GridView.aspx?typename=jkp_Rate">
                            <asp:Literal ID="Literal7" Text="<%$ Resources:Rooms-MenuItem6 %>" runat="server"></asp:Literal>
                        </a>
                        <br />
                        <asp:Literal ID="Literal8" Text="<%$ Resources:Rooms-Desc6 %>" runat="server"></asp:Literal>
                    </li>
                </ul>
            </fieldset>
        </div>
        <%--Middle Panel--%>
        <div style="width: 300px; float: left;">
            <fieldset runat="server" id="fieldsetUserAdmin">
                <legend><asp:Literal ID="Literal12" Text="<%$ Resources:litUserAdministration %>" runat="server"></asp:Literal></legend>
                <ul>
                    <li runat="server" id="litManageRoleRights">
                        <a href="Admin-ManageRolesRights.aspx">
                            <asp:Literal ID="Literal13" Text="<%$ Resources:litUserAdministration-MenuItem1 %>" runat="server">
                            </asp:Literal>                            
                        </a>
                        <br />
                        <asp:Literal ID="staticLiteral1" Text="<%$ Resources:litUserAdministration-Desc1 %>" runat="server">
                        </asp:Literal>
                    </li>
                             
                    <li runat="server" id="litViewUsers">
                        <a href="GridView.aspx?typename=aspnet_Membership">
                            <asp:Literal ID="Literal15" Text="<%$ Resources:litUserAdministration-MenuItem3 %>" runat="server">
                            </asp:Literal>
                        </a>                        
                        <br />
                        <asp:Literal ID="Literal42" Text="<%$ Resources:litUserAdministration-Desc3 %>" runat="server">
                        </asp:Literal>
                    </li>
                    <li style="visibility: hidden; display: none;">
                        <a href="GridView.aspx?typename=jkp_Person">
                            <asp:Literal ID="Literal41" Text="<%$ Resources:litUserAdministration-MenuItem4 %>" runat="server">
                            </asp:Literal>
                        </a>                                            
                        <br />
                        <asp:Literal ID="Literal43" Text="<%$ Resources:litUserAdministration-Desc4 %>" runat="server">
                        </asp:Literal>

                    </li>
                </ul>
            </fieldset>
            <fieldset runat="server" id="fieldsetReports">
                <legend><asp:Literal ID="Literal16" Text="<%$ Resources:litReporting %>" runat="server"></asp:Literal></legend>
                <ul>
                    <li runat="server" id="lisReportEmailAddresses">
                        <a href="Report-EmailAddresses.aspx">
                            <asp:Literal ID="Literal17" Text="<%$ Resources:litReporting-MenuItem1 %>" runat="server"></asp:Literal>
                        </a>
                        <br />
                        <asp:Literal ID="Literal48" Text="<%$ Resources:litReporting-Desc1 %>" runat="server"></asp:Literal>
                    </li>
                </ul>
            </fieldset>
        </div>
        <%--Right Panel--%>
        <div style="width: 300px; float: left; margin-right: 4px;">
            <fieldset id="fieldsetArticles" runat="server">
                <legend><asp:Literal ID="Literal28" Text="<%$ Resources:litArticles %>" runat="server"></asp:Literal></legend>
                <ul>
                    <li runat="server" id="liArticlePagesListing">
                        <a href="Admin-ArticlePages.aspx">
                            <asp:Literal ID="Literal22" Text="<%$ Resources:litArticles-MenuItem1 %>" runat="server"></asp:Literal>
                        </a>
                        <br />
                        <asp:Literal ID="Literal23" Text="<%$ Resources:litArticles-Desc1 %>" runat="server"></asp:Literal>
                    </li>
                    <li runat="server" id="liArticles">
                        <a href="GridView.aspx?typename=jkp_Article">
                            <asp:Literal ID="Literal21" Text="<%$ Resources:litArticles-MenuItem2 %>" runat="server"></asp:Literal>
                        </a>
                        <br />
                        <asp:Literal ID="Literal24" Text="<%$ Resources:litArticles-Desc2 %>" runat="server"></asp:Literal>
                    </li>
                    <li runat="server" id="liArticleCategories">
                        <a href="AddEdit.aspx?typename=jkp_ArticleCategory">
                            <asp:Literal ID="Literal20" Text="<%$ Resources:litArticles-MenuItem3 %>" runat="server"></asp:Literal>
                        </a>
                        <br />
                        <asp:Literal ID="Literal25" Text="<%$ Resources:litArticles-Desc3 %>" runat="server"></asp:Literal>
                    </li>
                    <li runat="server" id="liArticleAdd">
                        <a href="AddEdit.aspx?typename=jkp_Article">
                            <asp:Literal ID="Literal19" Text="<%$ Resources:litArticles-MenuItem4 %>" runat="server"></asp:Literal>
                        </a>
                        <br />
                        <asp:Literal ID="Literal26" Text="<%$ Resources:litArticles-Desc4 %>" runat="server"></asp:Literal>
                    </li>
                    <li runat="server" id="liArticlePagesAdd">
                        <a href="AddEdit.aspx?typename=jkp_ArticlePage">
                            <asp:Literal ID="Literal18" Text="<%$ Resources:litArticles-MenuItem5 %>" runat="server"></asp:Literal>
                        </a>
                        <br />
                        <asp:Literal ID="Literal27" Text="<%$ Resources:litArticles-Desc5 %>" runat="server"></asp:Literal>
                    </li>
                </ul>
            </fieldset>
            <fieldset runat="server" id="fieldsetMedia">
                <legend><asp:Literal ID="Literal29" Text="<%$ Resources:litMedia %>" runat="server"></asp:Literal></legend>
                <ul>
                    <li runat="server" id="liBooks">
                        <a href="GridView.aspx?typename=jkp_Book">
                            <asp:Literal ID="Literal30" Text="<%$ Resources:litMedia-Books %>" runat="server"></asp:Literal>
                        </a>
                        <br />
                        <asp:Literal ID="Literal44" Text="<%$ Resources:litMediaDesc-Books %>" runat="server"></asp:Literal>
                    </li>
                    <li runat="server" id="liAudio">
                        <a href="GridView.aspx?typename=jkp_Audio">
                            <asp:Literal ID="Literal31" Text="<%$ Resources:litMedia-Audio %>" runat="server"></asp:Literal>
                        </a>
                        <br />
                        <asp:Literal ID="Literal45" Text="<%$ Resources:litMediaDesc-Audio %>" runat="server"></asp:Literal>
                        </li>
                    <li runat="server" id="liVideos">
                        <a href="GridView.aspx?typename=jkp_Video">
                            <asp:Literal ID="Literal32" Text="<%$ Resources:litMedia-Videos %>" runat="server"></asp:Literal>
                        </a>
                        <br />
                        <asp:Literal ID="Literal46" Text="<%$ Resources:litMediaDesc-Videos %>" runat="server"></asp:Literal>
                        </li>
                    <li runat="server" id="liImages">
                        <a href="GridView.aspx?typename=jkp_Image">
                            <asp:Literal ID="Literal33" Text="<%$ Resources:litMedia-Images %>" runat="server"></asp:Literal>
                        </a>
                        <br />
                        <asp:Literal ID="Literal47" Text="<%$ Resources:litMediaDesc-Images %>" runat="server"></asp:Literal>
                        </li>
                </ul>
            </fieldset>
        </div>
    </div>
    <hr style="clear: both; display: block; visibility: hidden;" />
</asp:Content>
