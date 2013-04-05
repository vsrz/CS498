<%@ Page Language="C#" MasterPageFile="~/ControlPanel.master" AutoEventWireup="true"
    CodeFile="Retreat-DiscussionGroups.aspx.cs" Inherits="Retreat_DiscussionGroups"
    Title="<%$ Resources:PageTitle %>" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        <asp:Literal ID="Lit1" runat="server" Text="<%$ Resources:PageHeading %>"></asp:Literal>
        <asp:Literal runat="server" ID="litRetreatName"></asp:Literal></h1>
    <fieldset>
        <legend>
            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:DiscussionGroupsForPeopleAttendingRetreat %>"></asp:Literal>
        </legend>
        <asp:LinkButton runat="server" ID="lbtnCreateNewGroup" OnClick="lbtnCreateNewGroup_Clicked">
            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:CreateNewDiscussionGroup %>"></asp:Literal>
        </asp:LinkButton>
        <br />
        <asp:Repeater runat="server" ID="rpDiscussionGroups" OnItemDataBound="rpDiscussionGroups_OnItemDataBound">
            <HeaderTemplate>
                <table>
                    <tr style="font-weight: bold;">
                        <td style="width: 250px;">
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:DiscussionGroup %>"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Lit4" runat="server" Text="<%$ Resources:PeopleInGroup %>"></asp:Literal>
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td valign="top">
                        <asp:HiddenField runat="server" ID="hidDiscussionGroupId" />
                        <asp:Literal runat="server" ID="litDiscussionGroupName"></asp:Literal>
                    </td>
                    <td style="width: 400px;">
                        <asp:UpdatePanel runat="server" ID="upanAddPersonToGroup">
                            <ContentTemplate>
                                <asp:LinkButton runat="server" ID="lbtnAddPersonToGroup" OnClick="lbtnAddPersonToGroup_Clicked">
                                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:AddAPersonToADiscussionGroup %>"></asp:Literal>
                                </asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upanAddPersonToGroup">
                            <ProgressTemplate>
                                <asp:Image runat="server" ImageUrl="<%$ Resources:imgWaiting %>" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <br />
                        <asp:UpdatePanel runat="server" ID="upanRepeaterForPeopleInGroup">
                            <ContentTemplate>
                                <asp:Repeater runat="server" ID="rpPeopleInGroup" OnItemDataBound="rpPeopleInGroup_OnItemDataBound">
                                    <HeaderTemplate>
                                        <table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Literal runat="server" ID="litPersonName"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel runat="server" ID="upanRemovePersonFromGroup">
                                                    <ContentTemplate>
                                                        <asp:LinkButton runat="server" ID="lbtnRemovePersonFromGroup" OnClick="lbtnRemovePersonFromGroup_Clicked">
                                                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Delete %>"></asp:Literal>
                                                        </asp:LinkButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="upanRemovePersonFromGroup">
                                                    <ProgressTemplate>
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="<%$ Resources:imgWaiting %>" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </fieldset>
    <asp:UpdatePanel runat="server" ID="upanPopups">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlCreateNewDiscussionGroup" CssClass="popup-panel" Style="display: none;">
                <h1>
                    <asp:Literal ID="Litz4" runat="server" Text="<%$ Resources:CreateNewDiscussionGroupForThisRetreat %>"></asp:Literal>    
                </h1>
                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:DiscussionGroupName %>"></asp:Literal>
                <asp:TextBox runat="server" ID="txtCreateGroupName" MaxLength="30"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="reqCreateGroupName" ControlToValidate="txtCreateGroupName"
                    ValidationGroup="CreateGroup"></asp:RequiredFieldValidator>
                <br />
                Location:<br />
                <asp:TextBox runat="server" ID="txtCreateGroupLocation" Width="300px"></asp:TextBox>
                <br />
                <asp:Button runat="server" ID="btnCreateGroupSave" Text="<%$ Resources:btnCreateGroupSave %>" OnClick="btnCreateGroupSave_Clicked"
                    ValidationGroup="CreateGroup" />
                <asp:Button runat="server" ID="btnCreateGroupCancel" OnClick="btnCreateGroupCancel_Clicked"
                    Text="<%$ Resources:btnCreateGroupCancel %>" />
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlAssignPersonToGroup" CssClass="popup-panel" Style="display: none;">
                <h1>
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:AssignPersonToGroup %>"></asp:Literal>    
                </h1>
                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:GroupName %>"></asp:Literal>
                <asp:Literal runat="server" ID="litAssnGroupName"></asp:Literal>
                <br />
                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:People %>"></asp:Literal>
                <asp:DropDownList runat="server" ID="dlAssnPersons" Width="250px">
                </asp:DropDownList>
                <br />
                <asp:Button runat="server" ID="btnAssnSave" Text="<%$ Resources:btnAssnSave %>" OnClick="btnAssnSave_Clicked" />
                <asp:Button runat="server" ID="btnAssnCancel" Text="<%$ Resources:btnAssnCancel %>" OnClick="btnAssnCancel_Clicked" />
            </asp:Panel>
            <asp:LinkButton runat="server" ID="lbtnInvisible2" Style="visibility: hidden; display: none;"></asp:LinkButton>
            <ajaxtoolkit:modalpopupextender runat="server" id="modalCreateAssignment" targetcontrolid="lbtnInvisible2"
                popupcontrolid="pnlCreateNewDiscussionGroup" cancelcontrolid="btnCreateGroupCancel">
            </ajaxtoolkit:modalpopupextender>
            <asp:LinkButton runat="server" ID="lbtnFakeInvisible" Style="visibility: hidden;
                display: none;"></asp:LinkButton>
            <ajaxtoolkit:modalpopupextender runat="server" id="modalAssignPerson" targetcontrolid="lbtnFakeInvisible"
                popupcontrolid="pnlAssignPersonToGroup" cancelcontrolid="btnAssnCancel">
            </ajaxtoolkit:modalpopupextender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
