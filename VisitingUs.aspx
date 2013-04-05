<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="VisitingUs.aspx.cs" Inherits="VisitingUs" Title="Visiting Us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="sidebar-details" style="float: left; width: 250px; background-color: White;
        border-color: #D3C6AC; border-style: solid; margin-right: 20px;">
        <h2>
            Sites</h2>
        <asp:Repeater runat="server" ID="rpCountriesOfSites" OnItemDataBound="rpCountriesOfSites_OnItemDataBound">
            <ItemTemplate>
                <asp:Literal runat="server" ID="litCountryName"></asp:Literal>
                <br />
                <div id="sidebar-country-sites" style="padding-left: 20px;">
                    <asp:Repeater runat="server" ID="rpSites" OnItemDataBound="rpSites_OnItemDataBound">
                        <ItemTemplate>
                            <p>
                                <asp:HyperLink runat="server" ID="hlSiteListName"></asp:HyperLink>
                            </p>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div style="float: left; width: 600px;">
    <h1>Visit Us At One Of Our Sites</h1>
        <p>
            To ensure that we have space available for you, and in order to properly prepare
            for your visit, it is important that you <strong>pre-register at least one week prior
                to your arrival date for any retreat you may wish to attend</strong>. For general
            retreats, please plan to arrive at the monastery by 5pm on Friday. To ensure that
            you do not come when the monastery will not be receiving guests, please check our
            <asp:HyperLink runat="server" NavigateUrl="~/Events.aspx">retreat schedule</asp:HyperLink>.</p><br />
        <p>
            You may register in the following manner:</p>
        <p>
            a) <asp:HyperLink runat="server" NavigateUrl="~/Events.aspx">
                Register for General Retreat Online</asp:HyperLink></p>
        <p>
            <strong>OR</strong></p>
        <p>
            <br />
            b) Call our business office at (760) 291-1003, “1” for English and “3” for Registration,
            between the hours of 8:30 a.m. to 2:00 p.m., Pacific Time, Tuesday through Saturday,
            and our office staff will take your registration/reservation over the phone.</p>
        <p>
            For general retreats, the event is listed on the events page of the site. Simply select the dates that you would like to stay with us.</p>
        <asp:HyperLink runat="server" ID="hlEvents" NavigateUrl="~/Events.aspx">View Our Events</asp:HyperLink>
    </div>
</asp:Content>
