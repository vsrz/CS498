<%@ Page Language="C#" MasterPageFile="~/ControlPanel.master" AutoEventWireup="true"
    CodeFile="Retreat-AssignRooms.aspx.cs" Inherits="Retreat_AssignRooms" Title="Assign Rooms" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        <asp:Literal runat="server" ID="litPagetitle" Text="<%$ Resources:PageTitle %>"></asp:Literal>
    </h1>
    <p>
        <a href="Retreat-AdminOptions.aspx?retreatid=<%= Request.QueryString["retreatid"] %>">
            <asp:Literal runat="server" ID="litRetreatTools" Text="<%$ Resources:ReturnToRetreatTools %>"></asp:Literal>
        </a>
    </p>
    <asp:UpdatePanel runat="server" ID="upanDetails">
        <ContentTemplate>
            <fieldset>
                <legend>
                    <asp:Literal runat="server" ID="litRetreatDetails" Text="<%$ Resources:RetreatDetails %>"></asp:Literal>
                </legend>
                <table>
                    <tr>
                        <td valign="top" style="width: 400px;">
                            <p>
                                <b>
                                    <asp:Literal runat="server" ID="litRetreatName" Text="<%$ Resources:RetreatName %>"></asp:Literal>
                                    <br />
                                    <b>
                                        <asp:Literal runat="server" ID="Lit1" Text="<%$ Resources:TotalRooms %>"></asp:Literal>
                                    </b>
                                    <asp:Literal runat="server" ID="litRooms"></asp:Literal>
                                    <br />
                                    <b>
                                        <asp:Literal runat="server" ID="Lit2" Text="<%$ Resources:TotalCapacity %>"></asp:Literal>
                                    </b>
                                    <asp:Literal runat="server" ID="litTotalCapacity"></asp:Literal>
                                    <br />
                                    <b>
                                        <asp:Literal runat="server" ID="Literal1" Text="<%$ Resources:NumRoomsFilled %>"></asp:Literal>
                                    </b>
                                    <asp:Literal runat="server" ID="litRoomsFilled"></asp:Literal>
                                    <br />
                            </p>
                        </td>
                        <td valign="top">
                            <p>
                                <b>
                                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:SpotsFilled %>"></asp:Literal>
                                </b>
                                <asp:Literal runat="server" ID="litSpotsFilledSoFar"></asp:Literal>
                                <br />
                                <b>
                                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:RemainingSpots %>"></asp:Literal>
                                </b>
                                <asp:Literal runat="server" ID="litRemainingSpace"></asp:Literal>
                                <br />
                                <b>
                                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:UnassignedPeople %>"></asp:Literal>
                                </b>
                                <asp:Literal runat="server" ID="litRemainingToBeAssigned"></asp:Literal>
                            </p>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="upanToBeAssignedPeople">
        <ContentTemplate>
            <fieldset>
                <legend>
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:UnassignedPeople %>"></asp:Literal></legend>
                <asp:Repeater runat="server" ID="rpUnAssignedPeople" OnItemDataBound="rpRooms_OnItemDataBound">
                    <HeaderTemplate>
                        <table>
                            <tr style="font-weight: bold;">
                                <td style="width: 125px;">
                                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Name %>"></asp:Literal>
                                </td>
                                <td style="width: 50px;">
                                    Gender
                                </td>
                                <td style="width: 35px;">
                                    Age
                                </td>
                                <td style="width: 150px;">
                                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:RequestedRoomType %>"></asp:Literal>
                                </td>
                                <td style="width: 300px;">
                                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:SelectRoom %>"></asp:Literal>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr runat="server" id="trAssignPerson">
                            <td>
                                <asp:LinkButton runat="server" ID="lbtnPersonName"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblPersonGender"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblPersonAge"></asp:Label>
                            </td>
                            <td>
                                <asp:LinkButton runat="server" ID="lbtnRequestedRoomType"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="dlRoomsToSelect" Width="300px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:UpdatePanel runat="server" ID="upanViewDetails">
                                    <ContentTemplate>
                                        <asp:LinkButton runat="server" ID="lbtnViewDetailsOfRoom" OnClick="lbtnViewDetailsOfRoom_Clicked"
                                            Text="View Selected Room Details"></asp:LinkButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel runat="server" ID="upanTrackUpdates">
                                    <ContentTemplate>
                                        <asp:Button runat="server" ID="btnSaveRoomSelection" Text="<%$ Resources:btnSaveRoomSelection %>" OnClick="btnSaveRoomSelection_Clicked" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdateProgress runat="server" ID="upProgress" AssociatedUpdatePanelID="upanTrackUpdates">
                                    <ProgressTemplate>
                                        <asp:Image ID="Image12" runat="server" ImageUrl="dpimages/waiting.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="upanViewDetails">
                                    <ProgressTemplate>
                                        <asp:Image ID="Image13" runat="server" ImageUrl="dpimages/waiting.gif" />
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
            <fieldset>
                <legend>
                    <asp:Literal ID="StaticLiteral8" runat="server" Text="<%$ Resources:PeopleAssignedToRooms %>"></asp:Literal>
                </legend>
                <asp:Repeater runat="server" ID="rpAssignedToRooms" OnItemDataBound="rpAssignedRooms_OnItemDataBound">
                    <HeaderTemplate>
                        <table>
                            <tr style="font-weight: bold;">
                                <td style="width: 150px;">
                                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:PersonsName %>"></asp:Literal>
                                </td>
                                <td style="width: 280px;">
                                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:RoomAssignedTo %>"></asp:Literal>
                                </td>
                                <td style="width: 280px;">
                                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:RoomDetails %>"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:UnassignRoom %>"></asp:Literal>
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr runat="server" id="trAssignRoom">
                            <td>
                                <asp:LinkButton runat="server" ID="lbtnPersonName"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="litAssignedRoomName"></asp:Literal>
                            </td>
                            <td>
                                <asp:UpdatePanel runat="server" ID="upanViewRoomDetails">
                                    <ContentTemplate>
                                        <asp:LinkButton runat="server" ID="lbtnUnAssignViewDetailsOfRoom" OnClick="lbtnViewDetailsOfRoom_Clicked"
                                            Text="View Selected Room Details"></asp:LinkButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel runat="server" ID="upanUnassignRoom">
                                    <ContentTemplate>
                                        <asp:Button runat="server" ID="btnUnassignRoom" Text="<%$ Resources:btnUnassignRoom %>" OnClick="btnUnassignRoom_Clicked" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdateProgress runat="server" ID="upProgress" AssociatedUpdatePanelID="upanViewRoomDetails">
                                    <ProgressTemplate>
                                        <asp:Image ID="Image14" runat="server" ImageUrl="dpimages/waiting.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <asp:UpdateProgress runat="server" ID="UpdateProgress2" AssociatedUpdatePanelID="upanUnassignRoom">
                                    <ProgressTemplate>
                                        <asp:Image ID="Image15" runat="server" ImageUrl="dpimages/waiting.gif" />
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
            <asp:Button runat="server" ID="btnCSVRoomAssignments" OnClick="GetCSVRoomAssignments"
                Text="<%$ Resources:btnCSVRoomAssignments %>" />&nbsp&nbsp
            <asp:HyperLink runat="server" ID="csvFileLink" Visible="false" Text="Download"></asp:HyperLink>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Label runat="server" ID="error"></asp:Label>
    <asp:UpdatePanel runat="server" ID="upanRoomDetails">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlRoomDetails" CssClass="popup-panel" Style="display: none;">
                <h1>
                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:RoomDetails %>"></asp:Literal>
                </h1>
                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:Hamlet %>"></asp:Literal>
                <asp:Literal runat="server" ID="litHamletName"></asp:Literal>
                <br />
                <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:Building %>"></asp:Literal>
                <asp:Literal runat="server" ID="litBuildingName"></asp:Literal>
                <br />
                <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:Name %>"></asp:Literal>
                <asp:Literal runat="server" ID="litRoomName"></asp:Literal>
                <br />
                <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:RoomType %>"></asp:Literal>
                <asp:Literal runat="server" ID="litRoomType"></asp:Literal>
                <br />
                <table>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:AirConditioning %>"></asp:Literal>
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkCreateRoomTypeAirConditioning" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:Bathroom %>"></asp:Literal>
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkCreateRoomTypeBathroom" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:LowerBunkBed %>"></asp:Literal>
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkCreateRoomTypeLowerBunkBed" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:Shower %>"></asp:Literal>
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkCreateRoomTypeShower" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:SingleBed %>"></asp:Literal>
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkCreateRoomTypeSingleBed" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:UpperBunkBed %>"></asp:Literal>
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkCreateRoomTypeUpperBunkBed" Enabled="false" />
                        </td>
                    </tr>
                </table>
                <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:FloorNumber %>"></asp:Literal>
                <asp:Literal runat="server" ID="litFloor"></asp:Literal>
                <br />
                <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:TotalCapacity %>"></asp:Literal>
                <asp:Literal runat="server" ID="litRoomTotalCapacity"></asp:Literal>
                <br />
                <asp:Literal ID="Literal25" runat="server" Text="<%$ Resources:RemainingSpace %>"></asp:Literal>
                <asp:Literal runat="server" ID="litRoomRemainingSpace"></asp:Literal>
                <br />
                <b>
                    <asp:Literal ID="Literal26" runat="server" Text="<%$ Resources:PeopleCurrentlyInRoom %>"></asp:Literal>
                </b>
                <asp:Repeater runat="server" ID="rpPeopleCurrentlyInRoom" OnItemDataBound="rpPeopleCurrentlyInRoom_OnItemDataBound">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <td style="width: 30px;">
                                </td>
                                <td style="width: 200px;">
                                </td>
                                <td>
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal runat="server" ID="litCountOfPeople"></asp:Literal>.
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="litPersonInRoomName"></asp:Literal>
                            </td>
                            <td>
                                <asp:Button runat="server" ID="lbtnRemovePersonFromRoom" Visible="false" Text="<%$ Resources:lbtnRemovePersonFromRoom %>">
                                </asp:Button>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:Button runat="server" ID="btnCloseRoomDetails" Text="<%$ Resources:btnCloseRoomDetails %>" OnClick="btnCloseRoomDetails_Clicked" />
            </asp:Panel>
            <asp:LinkButton runat="server" Style="visibility: hidden; display: none;" ID="lbtnInvisibleForPopup"
                Text="none"></asp:LinkButton>
            <ajaxToolkit:ModalPopupExtender runat="server" ID="modalRoom" TargetControlID="lbtnInvisibleForPopup"
                PopupControlID="pnlRoomDetails" CancelControlID="btnCloseRoomDetails">
            </ajaxToolkit:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
