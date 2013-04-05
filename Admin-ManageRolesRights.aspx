<%@ Page Language="C#" MasterPageFile="~/ControlPanel.master" AutoEventWireup="true"
    CodeFile="Admin-ManageRolesRights.aspx.cs" Inherits="Admin_ManageRolesRights"
    Title="<%$ Resources:litPageTitle %>" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        <asp:Literal ID="Literal5" Text="<%$ Resources:litManageRoles %>" runat="server"></asp:Literal></h1>
    <div style="float: left; width: 300px;">
        <fieldset>
            <legend>Roles</legend>
            <asp:LinkButton runat="server" ID="lbtnAddRole">
                <asp:Literal ID="Literal6" Text="<%$ Resources:litCreateNewRole %>" runat="server"></asp:Literal></asp:LinkButton>
            <br />
            <b><asp:Literal ID="Literal7" Text="<%$ Resources:litExistingRoles %>" runat="server"></asp:Literal></b><br />
            <asp:Repeater runat="server" ID="rpRoles" OnItemDataBound="rpRoles_OnItemDataBound">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:LinkButton runat="server" ID="lbtnRole" OnClick="lbtnEditRoleAssignments_clicked"></asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton runat="server" ID="lbtnDeleteRole" OnClick="lbtnDeleteRole_Clicked">Delete</asp:LinkButton>
                            <ajaxToolkit:ConfirmButtonExtender runat="server" ID="confirmDelete" TargetControlID="lbtnDeleteRole" ConfirmText="Are you sure you want to delete this role and all rights associated with it?"></ajaxToolkit:ConfirmButtonExtender>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </fieldset>
    </div>
    <div style="width: 500px; display: inline; float: right;">
        <fieldset visible="false" runat="server" id="fieldRoleDetails">
            <legend><asp:Literal ID="Literal2" Text="<%$ Resources:litRoleCurrentlyEditing %>" runat="server"></asp:Literal></legend>
            <asp:Literal ID="Literal3" Text="<%$ Resources:litRoleName %>" runat="server"></asp:Literal>
            <asp:Literal runat="server" ID="litRoleName"></asp:Literal>
            <asp:HiddenField runat="server" ID="hidRoleId" />
        </fieldset>
        <fieldset runat="server" id="fieldPagePermissions" visible="false">
            <legend><asp:Literal ID="Literal1" Text="<%$ Resources:litPermissionsToPages %>" runat="server"></asp:Literal></legend>
            <asp:LinkButton runat="server" ID="lbtnAddPagePermission">
            <asp:Literal ID="Literal4" Text="<%$ Resources:litAddPagePermission %>" runat="server"></asp:Literal>
            </asp:LinkButton>
            <br />
            <asp:Repeater runat="server" ID="rpPagePermissions" OnItemDataBound="rpPageRoles_OnItemDataBound">
                <HeaderTemplate>
                    <table>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Literal runat="server" ID="litPageTitle"></asp:Literal>
                        </td>
                        <td>
                            <asp:LinkButton runat="server" ID="lbtnDeletePageRole" OnClick="lbtnDeletePageRole_Clicked">Delete</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </fieldset>
        <fieldset runat="server" id="fieldObjectPermissions" visible="false">
            <legend>Permissions On Table</legend>
            <asp:LinkButton runat="server" ID="lbtnAddTablePermission">Add Page Access Permission</asp:LinkButton>
            <br />
            <asp:Repeater runat="server" ID="rpTablePermissions" OnItemDataBound="rpTablePermissions_OnItemDataBound">
                <HeaderTemplate>
                    <table>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Literal runat="server" ID="litTableName"></asp:Literal>
                        </td>
                        <td>
                            <asp:LinkButton runat="server" ID="lbtnDeleteTableRole" OnClick="lbtnDeleteTableRole_Clicked">Delete</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </fieldset>
    </div>
    <asp:Panel runat="server" ID="pnlAddRole" CssClass="popup-panel">
        <h1>
            Add New Role</h1>
        Role Name:
        <asp:TextBox runat="server" ID="txtRoleName"></asp:TextBox>
        <br />
        Description:
        <asp:TextBox runat="server" ID="txtDescription"></asp:TextBox>
        <br />
        <asp:Button runat="server" ID="btnSaveNewRole" OnClick="btnSaveNewRole_Clicked" Text="Save Role" />
        <asp:Button runat="server" ID="btnCancelNewRole" Text="Cancel" />
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlCreatePageRole" CssClass="popup-panel" Visible="false">
        <h1>
            Add Page Access Role</h1>
        <p>
            This gives the role access to use this page.
        </p>
        Select Page
        <br />
        <asp:DropDownList runat="server" ID="dlPages">
        </asp:DropDownList>
        <br />
        <asp:Button runat="server" ID="btnSavePageAccessRole" Text="Save Role" OnClick="btnSavePageAccessRole_Clicked" />
        <asp:Button runat="server" ID="btnCancelPageAccessRole" Text="Cancel" />
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlTablePermission" CssClass="popup-panel" Visible="false">
        <h1>
            Add Table Access Permission</h1>
        <p>
            This gives the role access to modify and view anything on this table.
        </p>
        Select Table
        <br />
        <asp:DropDownList runat="server" ID="dlTables">
        </asp:DropDownList>
        <br />
        <asp:Button runat="server" ID="btnSaveTablePermission" Text="Save Role" OnClick="btnSaveTablePermission_Clicked" />
        <asp:Button runat="server" ID="btnCancelTablePermission" Text="Cancel" />
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender runat="server" ID="popupAddRole" TargetControlID="lbtnAddRole"
        CancelControlID="btnCancelNewRole" PopupControlID="pnlAddRole">
    </ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender runat="server" ID="popupAddPageAccessRole" TargetControlID="lbtnAddPagePermission"
        CancelControlID="btnCancelPageAccessRole" PopupControlID="pnlCreatePageRole">
    </ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender runat="server" ID="ModalPopupExtender1" TargetControlID="lbtnAddTablePermission"
        CancelControlID="btnCancelTablePermission" PopupControlID="pnlTablePermission">
    </ajaxToolkit:ModalPopupExtender>
</asp:Content>
