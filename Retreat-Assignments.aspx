<%@ Page Language="C#" MasterPageFile="~/ControlPanel.master" AutoEventWireup="true"
    CodeFile="Retreat-Assignments.aspx.cs" Inherits="Retreat_Assignments" Title="Retreat Assignments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:PageTitle %>"></asp:Literal>
    </h1>
        <p>
            <a href="Retreat-AdminOptions.aspx?retreatid=<%= Request.QueryString["retreatid"] %>">
                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:ReturnToRetreatTools %>"></asp:Literal>
            </a>
        </p>
    <p>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:LinkButton runat="server" ID="lbtnCreateAssignment" OnClick="lbtnCreateAssignment_Clicked">
                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:CreateAssignment %>"></asp:Literal></asp:LinkButton>
            </ContentTemplate>
        </asp:UpdatePanel>
    </p>
    <fieldset>
        <legend>
                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:AssignmentsForPeopleAttendingRetreat %>"></asp:Literal>
        </legend>
        <asp:Repeater runat="server" ID="rpAssignmentsForPeople" OnItemDataBound="rpAssignmentsForPeople_OnItemDataBound">
            <HeaderTemplate>
                <table>
                    <tr style="font-weight: bold;">
                        <td style="width: 250px;">
                            <asp:Literal ID="litRetName" runat="server" Text="<%$ Resources:RetreatantName %>"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:Tasks %>"></asp:Literal>
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
                        <asp:UpdatePanel runat="server" ID="upanAddAssignmentToPerson">
                            <ContentTemplate>
                                <asp:LinkButton runat="server" ID="lbtnAddAssignment" OnClick="lbtnAddAssignment_Clicked">
                                    <asp:Literal ID="litAddAssignToPerson" runat="server" Text="<%$ Resources:AddAssignToPerson %>"></asp:Literal>
                                </asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="upanAddAssignmentToPerson">
                            <ProgressTemplate>
                                <asp:image runat="server" ImageUrl="<%$ Resources:imgWaiting %>" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <br />
                        <asp:UpdatePanel runat="server" ID="upanRepeaterForAssignments">
                            <ContentTemplate>
                                <asp:Repeater runat="server" ID="rpAssignments" OnItemDataBound="rpAssignments_OnItemDataBOund">
                                    <HeaderTemplate>
                                        <table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Literal runat="server" ID="litAssignmentName"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel runat="server" ID="upanAssignmentDelete">
                                                    <ContentTemplate>
                                                        <asp:LinkButton runat="server" ID="lbtnAssignmentDelete" OnClick="lbtnAssignmentDelete_Clicked">
                                                            <asp:Literal ID="litDelete" runat="server" Text="<%$ Resources:Delete %>"></asp:Literal>
                                                        </asp:LinkButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="upanAssignmentDelete">
                                                    <ProgressTemplate>
                                                        <asp:image runat="server" ImageUrl="<%$ Resources:imgWaiting %>" />
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
            <asp:Panel runat="server" ID="pnlCreateNewAssignment" CssClass="popup-panel" Style="display: none;">
                <h1>
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:CreateAssignmentThisRetreat %>"></asp:Literal>    
                </h1>
                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:AssignmentName %>"></asp:Literal>
                <asp:TextBox runat="server" ID="txtCreateAssnName" MaxLength="30"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="reqCreateAssnName" ControlToValidate="txtCreateAssnName"
                    ValidationGroup="CreateAssignment"></asp:RequiredFieldValidator>
                <br />
                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:Description %>"></asp:Literal>
                <br />
                <asp:TextBox runat="server" ID="txtDescription" Width="300px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                <br />
                <asp:Button runat="server" ID="btnCreateAssnSave" Text="<%$ Resources:btnCreateAssnSave %>" OnClick="btnCreateAssnSave_Clicked"
                    ValidationGroup="CreateAssignment" />
                <asp:Button runat="server" ID="btnCreateAssnCancel" OnClick="btnCreateAssnCancel_Clicked"
                    Text="<%$ Resources:btnCreateAssnCancel %>" />
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlAssignPerson" CssClass="popup-panel" Style="display: none;">
                <h1>
                    <asp:Literal ID="Literalstatic9" runat="server" Text="<%$ Resources:GiveAssignmentToPerson%>"></asp:Literal>

                </h1>
                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:PersonBeingAssignedTo %>"></asp:Literal>

                <asp:Literal runat="server" ID="litAssnName"></asp:Literal>
                <br />
                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:Assignments %>"></asp:Literal>

                <asp:DropDownList runat="server" ID="dlAssnAssignments" Width="250px">
                </asp:DropDownList>
                <br />
                <asp:Button runat="server" ID="btnAssnSave" Text="<%$ Resources:btnAssnSave %>" OnClick="btnAssnSave_Clicked" />
                <asp:Button runat="server" ID="btnAssnCancel" Text="<%$ Resources:btnAssnCancel %>" OnClick="btnAssnCancel_Clicked" />
            </asp:Panel>
            <asp:LinkButton runat="server" ID="lbtnInvisible2" Style="visibility: hidden; display: none;"></asp:LinkButton>
            <ajaxToolkit:ModalPopupExtender runat="server" ID="modalCreateAssignment" TargetControlID="lbtnInvisible2"
                PopupControlID="pnlCreateNewAssignment" CancelControlID="btnCreateAssnCancel">
            </ajaxToolkit:ModalPopupExtender>
            <asp:LinkButton runat="server" ID="lbtnFakeInvisible" Style="visibility: hidden;
                display: none;"></asp:LinkButton>
            <ajaxToolkit:ModalPopupExtender runat="server" ID="modalAssignPerson" TargetControlID="lbtnFakeInvisible"
                PopupControlID="pnlAssignPerson" CancelControlID="btnAssnCancel">
            </ajaxToolkit:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
