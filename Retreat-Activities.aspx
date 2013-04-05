<%@ Page Language="C#" MasterPageFile="~/ControlPanel.master" AutoEventWireup="true" CodeFile="Retreat-Activities.aspx.cs" 
    Inherits="Retreat_Activities" Title="<%$ Resources:litPageTitle %>" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:litActivitiesForRetreat %>"></asp:Literal></h1>
        <p>
            <a href="Retreat-AdminOptions.aspx?retreatid=<%= Request.QueryString["retreatid"] %>">
                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:litReturnToRetreatTools %>"></asp:Literal>
            </a>
        </p>
    <p>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:LinkButton runat="server" ID="lbtnCreateActivity" OnClick="lbtnCreateActivity_Clicked">
                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:litCreateActivity %>"></asp:Literal>
                </asp:LinkButton>
            </ContentTemplate>
        </asp:UpdatePanel>
    </p>
    <fieldset>
        <legend><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:litActivitiesForRetreatants %>"></asp:Literal></legend>
        <asp:Repeater runat="server" ID="rpActivitiesForPeople" OnItemDataBound="rpActivitiesForPeople_OnItemDataBound">
            <HeaderTemplate>
                <table>
                    <tr style="font-weight: bold;">
                        <td style="width: 250px;">
                            <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:litName %>"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:litTasks %>"></asp:Literal>
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td valign="top">
                        <asp:HiddenField runat="server" ID="hidPersonId" />
                        <asp:Literal runat="server" ID="litRetreatantName"></asp:Literal>
                    </td>
                    <td style="width: 400px;">
                        <asp:UpdatePanel runat="server" ID="upanAddActivityToPerson">
                            <ContentTemplate>
                                <asp:LinkButton runat="server" ID="lbtnAddActivity" OnClick="lbtnAddActivity_Clicked">
                                <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:litAddActivity %>"></asp:Literal>
                                </asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upanAddActivityToPerson">
                            <ProgressTemplate>
                                <asp:Image ID="Image1" runat="server" ImageUrl="<%$ Resources:imgWaiting %>" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <br />
                        <asp:UpdatePanel runat="server" ID="upanRepeaterForActivities">
                            <ContentTemplate>
                                <asp:Repeater runat="server" ID="rpActivities" OnItemDataBound="rpActivities_OnItemDataBOund">
                                    <HeaderTemplate>
                                        <table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Literal runat="server" ID="litActivityName"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel runat="server" ID="upanActivityDelete">
                                                    <ContentTemplate>
                                                        <asp:LinkButton runat="server" ID="lbtnActivityDelete" OnClick="lbtnActivityDelete_Clicked">
                                                            <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:litDelete %>"></asp:Literal>
                                                        </asp:LinkButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="upanActivityDelete">
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
            <asp:Panel runat="server" ID="pnlCreateNewActivity" CssClass="popup-panel" Style="display: none;">
                <h1>
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:litCreateActivity %>"></asp:Literal>
                </h1>
                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:litActivityName %>"></asp:Literal>
                <asp:TextBox runat="server" ID="txtCreateAssnName" MaxLength="30"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="reqCreateAssnName" ControlToValidate="txtCreateAssnName"
                    ValidationGroup="CreateActivity"></asp:RequiredFieldValidator>
                <br />
                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:litDescription %>"></asp:Literal>
                <br />
                <asp:TextBox runat="server" ID="txtDescription" Width="300px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                <br />
                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:litStartTime %>"></asp:Literal>
                <asp:TextBox runat="server" ID="txtCreateActStartTime"></asp:TextBox>
                <br />
                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:litEndTime %>"></asp:Literal>
                <asp:TextBox runat="server" ID="txtCreateActEndTime"></asp:TextBox>
                <asp:Button runat="server" ID="btnCreateAssnSave" Text="<%$ Resources:btnSave %>" OnClick="btnCreateAssnSave_Clicked"
                    ValidationGroup="CreateActivity" />
                <asp:Button runat="server" ID="btnCreateAssnCancel" OnClick="btnCreateAssnCancel_Clicked"
                    Text="<%$ Resources:btnCancel %>" />
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlAssignPerson" CssClass="popup-panel" Style="display: none;">
                <h1>
                    <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:litGiveActivityToPerson %>"></asp:Literal>
                </h1>
                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:litAssignActivityToPerson %>"></asp:Literal>
                <asp:Literal runat="server" ID="litAssnName"></asp:Literal>
                <br />
                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:litActivities %>"></asp:Literal>
                <asp:DropDownList runat="server" ID="dlAssnActivities" Width="250px">
                </asp:DropDownList>
                <br />
                <asp:Button runat="server" ID="btnAssnSave" Text="<%$ Resources:btnSave %>" OnClick="btnAssnSave_Clicked" />
                <asp:Button runat="server" ID="btnAssnCancel" Text="<%$ Resources:btnCancel %>" OnClick="btnAssnCancel_Clicked" />
            </asp:Panel>
            <asp:LinkButton runat="server" ID="lbtnInvisible2" Style="visibility: hidden; display: none;"></asp:LinkButton>
            <ajaxToolkit:ModalPopupExtender runat="server" ID="modalCreateActivity" TargetControlID="lbtnInvisible2"
                PopupControlID="pnlCreateNewActivity" CancelControlID="btnCreateAssnCancel">
            </ajaxToolkit:ModalPopupExtender>
            <asp:LinkButton runat="server" ID="lbtnFakeInvisible" Style="visibility: hidden;
                display: none;"></asp:LinkButton>
            <ajaxToolkit:ModalPopupExtender runat="server" ID="modalAssignPerson" TargetControlID="lbtnFakeInvisible"
                PopupControlID="pnlAssignPerson" CancelControlID="btnAssnCancel">
            </ajaxToolkit:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

