<%@ Page Language="C#" MasterPageFile="~/ControlPanel.master" AutoEventWireup="true"
    CodeFile="Media-RoleManager.aspx.cs" Inherits="Media_RoleManager" Title="<%$ Resources:litPageTitle %>" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        <asp:Literal ID="lilTitle" runat="server" Text="<%$ Resources:litPageTitle %>"></asp:Literal></h1>
    <asp:UpdatePanel runat="server" ID="upanContent">
        <ContentTemplate>
            <fieldset>
                <legend><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:litMediaDetails %>"></asp:Literal></legend>
                <b><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:litType %>"></asp:Literal></b>
                <asp:Literal runat="server" ID="litMediaType"></asp:Literal>
                <br />
                <b><asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:litTitle %>"></asp:Literal></b>
                <asp:Literal runat="server" ID="litMediaTitle"></asp:Literal>
            </fieldset>
            <fieldset>
                <legend><asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:litRolesAccessingMedia %>"></asp:Literal></legend>
                <asp:LinkButton runat="server" ID="lbtnAddNewRole">
                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:litAddAccessToRole %>"></asp:Literal>
                </asp:LinkButton>
                <br />
                <b><asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:litRolesWithAccess %>"></asp:Literal></b>
                <asp:Repeater runat="server" ID="rpAccessRoles" OnItemDataBound="rpAccessRoles_OnItemDataBound">
                    <HeaderTemplate>
                        <table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal runat="server" ID="litRoleName"></asp:Literal>
                            </td>
                            <td>
                                <asp:UpdatePanel runat="server" ID="upanRemoveRole">
                                    <ContentTemplate>
                                        <asp:LinkButton runat="server" ID="lbtnRemoveMediaRole" OnClick="lbtnRemoveMediaRole_Clicked">
                                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:litRemoveAccess %>"></asp:Literal>
                                        </asp:LinkButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upanRemoveRole">
                                    <ProgressTemplate>
                                        <img src="dpimages/waiting.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </fieldset>
            <asp:Panel runat="server" ID="pnlRoleSelect" CssClass="popup-panel" Style="display: none;">
                <h1>
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:litAddAccessToRole %>"></asp:Literal></h1>
                Roles
                <br />
                <asp:DropDownList runat="server" ID="dlRolesToAssign">
                </asp:DropDownList>
                <br />
                <asp:Button runat="server" ID="btnAddMediaRole" Text="<%$ Resources:btnAddMediaRole %>" OnClick="btnAddMediaRole_Clicked" />
                <asp:Button runat="server" ID="btnAddMediaCancel" Text="<%$ Resources:btnAddMediaCancel %>" OnClick="btnAddMediaRole_Cancel" />
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender runat="server" ID="modalAddRole" TargetControlID="lbtnAddNewRole"
                PopupControlID="pnlRoleSelect">
            </ajaxToolkit:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
