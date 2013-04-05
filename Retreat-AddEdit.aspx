<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Retreat-AddEdit.aspx.cs" Inherits="Retreat_AddEdit" Title="Retreat Add / Edit"
    ValidateRequest="false" %>

<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:MultiView runat="server" ID="mvAddEdit" ActiveViewIndex="0">
        <asp:View runat="server" ID="vAddEdit">
            <h1>
                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:litPageTitle %>"></asp:Literal></h1>
            <p>
                <a href="Retreat-AdminOptions.aspx?retreatid=<%= Request.QueryString["retreatid"] %>">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:litReturnToRetreatTools %>"></asp:Literal>
                </a>
            </p>
            <p>
                <asp:HyperLink runat="server" ID="hlEditRooms" Visible="false">
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:litAddEditRoomsForRetreat %>"></asp:Literal>
                </asp:HyperLink>
            </p>
            <p>
                <asp:HyperLink runat="server" ID="hlAssignedRoomsToPeople" Visible="false">
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:litAssignRoomsToPeople %>"></asp:Literal>
                </asp:HyperLink>
            </p>
            <br />
            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:litSite %>"></asp:Literal>
            <asp:DropDownList runat="server" ID="dlSite" Style="width: 300px;">
               
            </asp:DropDownList>
            <asp:HyperLink runat="server" ID="hlCreateSite" NavigateUrl="~/Site-AddEdit.aspx">
                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:litCreateSite %>"></asp:Literal>
            </asp:HyperLink>
            <br />
            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:litName %>"></asp:Literal>
            <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                Text="*"></asp:RequiredFieldValidator>
            <br />
            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:litStartDate %>"></asp:Literal>
            <asp:TextBox runat="server" ID="txtStartDate"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtStartDate"
                Text="Must be formatted like 11/30/2003 10:12:24 am or 2/29/2003 08:14:56 pm or 5/22/2003"
                ValidationExpression="^(((((0[13578])|([13578])|(1[02]))[\-\/\s]?((0[1-9])|([1-9])|([1-2][0-9])|(3[01])))|((([469])|(11))[\-\/\s]?((0[1-9])|([1-9])|([1-2][0-9])|(30)))|((02|2)[\-\/\s]?((0[1-9])|([1-9])|([1-2][0-9]))))[\-\/\s]?\d{4})(\s(((0[1-9])|([1-9])|(1[0-2]))\:([0-5][0-9])((\s)|(\:([0-5][0-9])\s))([AM|PM|am|pm]{2,2})))?$"></asp:RegularExpressionValidator>
            <br />
            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:litEndDate %>"></asp:Literal>
            <asp:TextBox runat="server" ID="txtEndDate"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtEndDate"
                Text="Must be formatted like 11/30/2003 10:12:24 am or 2/29/2003 08:14:56 pm or 5/22/2003"
                ValidationExpression="^(((((0[13578])|([13578])|(1[02]))[\-\/\s]?((0[1-9])|([1-9])|([1-2][0-9])|(3[01])))|((([469])|(11))[\-\/\s]?((0[1-9])|([1-9])|([1-2][0-9])|(30)))|((02|2)[\-\/\s]?((0[1-9])|([1-9])|([1-2][0-9]))))[\-\/\s]?\d{4})(\s(((0[1-9])|([1-9])|(1[0-2]))\:([0-5][0-9])((\s)|(\:([0-5][0-9])\s))([AM|PM|am|pm]{2,2})))?$"></asp:RegularExpressionValidator>
            <br />
            <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:litArrivalTime %>"></asp:Literal>
            <asp:TextBox runat="server" ID="txtArrivalTime"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtArrivalTime"
                Text="Must be in the format of '1:01 AM' or 23:52:01" ValidationExpression="^((([0]?[1-9]|1[0-2])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?( )?(AM|am|aM|Am|PM|pm|pM|Pm))|(([0]?[0-9]|1[0-9]|2[0-3])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?))$"></asp:RegularExpressionValidator>
            <br />
            <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:litDepartureTime %>"></asp:Literal>
            <asp:TextBox runat="server" ID="txtDepartureTime"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDepartureTime"
                Text="Must be in the format of '1:01 AM' or 23:52:01" ValidationExpression="^((([0]?[1-9]|1[0-2])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?( )?(AM|am|aM|Am|PM|pm|pM|Pm))|(([0]?[0-9]|1[0-9]|2[0-3])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?))$"></asp:RegularExpressionValidator>
            <br />
            <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:litLanguage%>"></asp:Literal>
            <asp:DropDownList runat="server" ID="dlLanguage"></asp:DropDownList> <%--<asp:TextBox runat="server" ID="txtLanguage" MaxLength="30"></asp:TextBox>--%>
            <br />
            <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:litDescription%>"></asp:Literal>
            <br />
            <FTB:FreeTextBox runat="server" ID="txtDescription" Width="800px">
            </FTB:FreeTextBox>
            <br />
            <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:litActivitiesInstructions %>"></asp:Literal>
            <br />
            <asp:Button runat="server" ID="btnSave" Text="<%$ Resources:btnSave %>" OnClick="btnSave_Clicked" />
        </asp:View>
        <asp:View ID="vSuccess" runat="server">
            <h1>
                <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:litRetreatSaved %>"></asp:Literal>
            </h1>
            <br />
            <p>
                <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:litRetreatSavedSuccess %>"></asp:Literal>
            </p>
            <p>
                <a href="Retreat-AdminOptions.aspx?retreatid=<%= Request.QueryString["retreatid"] %>">
                    <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:litReturnToRetreatTools %>"></asp:Literal>    
                </a>
            </p>
        </asp:View>
    </asp:MultiView>
</asp:Content>
