<%@ Page Language="C#" MasterPageFile="~/ControlPanel.master" AutoEventWireup="true"
    CodeFile="Admin-GivePersonRoles.aspx.cs" Inherits="Admin_GivePersonRoles" Title="<%$ Resources:litPageTitle %>" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        <asp:Literal ID="Literal1" Text="<%$ Resources:litHeading %>" runat="server"></asp:Literal></h1>
    <asp:UpdatePanel runat="server" ID="upanRoles">
        <ContentTemplate>
            <fieldset>
                <legend><asp:Literal ID="Literal2" Text="<%$ Resources:litPersonDetails %>" runat="server"></asp:Literal></legend>
                <asp:Literal ID="Literal8" Text="<%$ Resources:litPersonName %>" runat="server"></asp:Literal>
                <asp:Literal runat="server" ID="litPersonName"></asp:Literal>
            </fieldset>
            <fieldset>
                <legend><asp:Literal ID="Literal3" Text="<%$ Resources:litPersonName %>" runat="server"></asp:Literal></legend>
                <asp:LinkButton runat="server" ID="lbtnAddNewRole"><asp:Literal ID="Literal4" Text="<%$ Resources:litAssignPersonToRole %>" runat="server"></asp:Literal></asp:LinkButton>
                <br />
                <b><asp:Literal ID="Literal5" Text="<%$ Resources:litListRoles %>" runat="server"></asp:Literal></b>
                <br />
                <asp:Repeater runat="server" ID="rpPersonRoles" OnItemDataBound="rpPersonRoles_OnItemDataBound">
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
                                        <asp:LinkButton runat="server" ID="lbtnRemoveRole" OnClick="lbtnRemoveRole_Clicked">
                                            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:Remove %>"></asp:Literal>
                                        </asp:LinkButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upanRemoveRole">
                                    <ProgressTemplate>
                                        <asp:Image runat="server" ImageUrl="<%$ Resources:imgWaiting %>" />                                        
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
                    <asp:Literal ID="Literal6" Text="<%$ Resources:litAddRoleAccess %>" runat="server"></asp:Literal></h1>
                <asp:Literal ID="Literal7" Text="<%$ Resources:litRoles %>" runat="server"></asp:Literal>
                <br />
                <asp:DropDownList runat="server" ID="dlRolesToAssign">
                </asp:DropDownList>
                <br />
                <asp:Button runat="server" ID="btnAddRole" Text="Assign To Role" OnClick="btnAddRole_Clicked" />
                <asp:Button runat="server" ID="btnAddRoleCancel" Text="Cancel" OnClick="btnAddRole_Cancel" />
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender runat="server" ID="modalAddRole" TargetControlID="lbtnAddNewRole"
                PopupControlID="pnlRoleSelect">
            </ajaxToolkit:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
