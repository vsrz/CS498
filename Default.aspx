<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" Title="Home" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField runat="server" ID="dateTime" />
    <h1>
        <asp:Literal runat="server" Text="<%$ Resources:lblTitle %>"></asp:Literal></h1>
    <div id="left-pan" style="width: 200px; float: left;">
        <b>
            <asp:Literal runat="server" Text="<%$ Resources:lblPickLanguage %>"></asp:Literal></b>
        <br />
        <asp:DropDownList runat="server" ID="dlLanguages" Width="175px" OnSelectedIndexChanged="dlLanguages_Picked"
            AutoPostBack="true">
        </asp:DropDownList>
        <br />
        <br />
        <br />
        
        <b>
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:lblUpcomingEvents %>"></asp:Literal></b>
            <br />
            <asp:Repeater runat="server" ID="rpRetreats">
                <ItemTemplate>
                    <div style="padding-bottom: 11px;">
                        <b><a href="<%# "Retreat-PublicView.aspx?retreatid=" + Eval("Ret_ID").ToString() %>">
                            <%# this.ShortenString( Eval("Ret_Name").ToString(), 40) %></a></b>
                            <br />
                            <span style="font-size: 11px;">
                            <%# Eval("jkp_Site") != null? ((Monks.jkp_Site) Eval("jkp_Site")).GetName : "Unknown Location" %>
                            <br />
                            <%# ((DateTime)Eval("Ret_StartDate")).ToLongDateString() %>
                            </span>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        
        <br />
    </div>
    <div id="middle-pan" style="width: 600px; float: right; margin-right: 50px;">
        <asp:Image runat="server" ImageUrl="" ID="image1" Width="590px" Height="450px" />
    </div>
    <hr style="display: block; visibility: hidden; clear: both;" />
</asp:Content>
