<%@ Page Language="C#" MasterPageFile="~/ControlPanel.master" AutoEventWireup="true"
    CodeFile="Retreat-AddEditRooms.aspx.cs" Inherits="Retreat_AddEditRooms" Title="<%$ Resources:litPageTitle %>" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:litAddEditRoomsForRetreat %>"></asp:Literal>
    </h1>
    <p>
        <a href="Retreat-AdminOptions.aspx?retreatid=<%= Request.QueryString["retreatid"] %>">
            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:litReturnToRetreatTools %>"></asp:Literal>
        </a>
    </p>
    <p>
        <b>
            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:litInstructionsTitle %>"></asp:Literal>
        </b>
        <br />
        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:litAddEditRoomsInstructions %>"></asp:Literal>
    </p>
    <div style="float: left; width: 500px;">
        <fieldset>
            <legend><asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:litTasks %>"></asp:Literal></legend>
            <asp:LinkButton runat="server" ID="lbtnPopupCreateHamelet" Text="<%$ Resources:btnCreateHamlet %>" Visible="false" Enabled="false"></asp:LinkButton>
            <br />
            <asp:LinkButton runat="server" ID="lbtnPopupCreateBuilding" Text="<%$ Resources:btnCreateBuilding %>" Visible="false" Enabled="false"></asp:LinkButton>
            <br />
            <asp:LinkButton runat="server" ID="lbtnPopupCreateRetreat" Text="<%$ Resources:btnCreateARoom %>"></asp:LinkButton>
        </fieldset>
        <asp:Panel runat="server" ID="pnlCreateHamlet" CssClass="popup-panel" Visible="false">
            <h1>
                Create Hamlet</h1>
            <p>
                English Name:
                <asp:TextBox runat="server" ID="txtHamletEnglishName"></asp:TextBox>
            </p>
            <p>
                Local Name:
                <asp:TextBox runat="server" ID="txtHamletLocalName"></asp:TextBox>
            </p>
            <p>
                Vietnamese Name:
                <asp:TextBox runat="server" ID="txtHamletVietName"></asp:TextBox>
            </p>
            <asp:Button runat="server" ID="btnCreateHamlet" Text="<%$ Resources:btnCreateHamlet %>" OnClick="btnCreateHamlet_Clicked" />
            <asp:Button runat="server" ID="btnCreateHamletCancel" Text="<%$ Resources:btnCreateHamletCancel %>" />
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender runat="server" ID="mdCreateHamlet" TargetControlID="lbtnPopupCreateHamelet"
            PopupControlID="pnlCreateHamlet" CancelControlID="btnCreateHamletCancel">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel runat="server" ID="pnlCreateBuilding" CssClass="popup-panel" Visible="false">
            <h1>
                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:litCreateHamlet %>"></asp:Literal>
            </h1>
            <p>
                Hamlet:
                <asp:DropDownList runat="server" ID="dlHamlets">
                    <asp:ListItem>Deer Park Hamlet 1</asp:ListItem>
                </asp:DropDownList>
            </p>
            <p>
                English Name:
                <asp:TextBox runat="server" ID="txtBuildingEnglishName"></asp:TextBox>
            </p>
            <p>
                Local Name:
                <asp:TextBox runat="server" ID="txtBuildingLocalName"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="reqBuildingLocalName" ControlToValidate="txtBuildingLocalName"
                    ValidationGroup="valBuilding"></asp:RequiredFieldValidator>
            </p>
            <p>
                Vietnamese Name:
                <asp:TextBox runat="server" ID="txtBuildingVietnameseName"></asp:TextBox>
            </p>
            <asp:Button runat="server" ID="btnCreateBuilding" Text="Create Building" OnClick="btnCreateBuilding_Clicked"
                ValidationGroup="valBuilding" />
            <asp:Button runat="server" ID="btnCreateBuildingCancel" Text="Cancel" />
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender runat="server" ID="ModalPopupExtender1" TargetControlID="lbtnPopupCreateBuilding"
            PopupControlID="pnlCreateBuilding" CancelControlID="btnCreateBuildingCancel">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel runat="server" ID="pnlCreateRoom" CssClass="popup-panel">
            <h1>
                Create Room For Retreat</h1>
            Select which site this set of rooms is for.
            <br />
            Building:
            <asp:DropDownList runat="server" ID="dlBuildings">
                <asp:ListItem>Deer Park Building 1</asp:ListItem>
            </asp:DropDownList>
            <asp:UpdatePanel runat="server" ID="upanRoomType">
                <ContentTemplate>
                    <p>
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:litRoomType %>"></asp:Literal><br />
                        <asp:DropDownList runat="server" ID="dlRoomType" AutoPostBack="true" OnSelectedIndexChanged="dlRoomTypes_Changed"
                            Style="width: 350px;">
                        </asp:DropDownList>
                        <br />
                        <asp:HyperLink runat="server" ID="hlCreateNewRoomType" NavigateUrl="~/AddEdit.aspx?typename=jkp_RoomType">Create New Room Type</asp:HyperLink>
                    </p>
                    <p>
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:litRoomCapacity %>"></asp:Literal>
                        <asp:TextBox runat="server" ID="txtCreateRoomCapacity" Text="<%$ Resources:txtDefaultRoomCapacity %>" Width="70px" MaxLength="5">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="reqCreateRoomCap" ControlToValidate="txtCreateRoomCapacity"
                            Text="Required" ValidationGroup="valRoom"></asp:RequiredFieldValidator>
                        <asp:RangeValidator runat="server" ID="rangeCapacity" ControlToValidate="txtCreateRoomCapacity"
                            Type="Integer" MinimumValue="1" MaximumValue="99999" ValidationGroup="valRoom"
                            Text="Must be a number between 1 and 99999"></asp:RangeValidator>
                    </p>
                    <fieldset>
                        <legend><asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:litRoomTypeDetails %>"></asp:Literal></legend>
                        <table>
                            <tr>
                                <td>
                                    <asp:CheckBox runat="server" ID="chkCreateRoomTypeAirConditioning" Enabled="false" />
                                </td>
                                <td style="width: 150px;">
                                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:litAirConditioning %>"></asp:Literal>
                                </td>
                                
                                <td>    
                                    <asp:CheckBox runat="server" ID="chkCreateRoomTypeBathroom" Enabled="false" />
                                </td>
                                <td>
                                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:litBathroom %>"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                            </tr>
                            <tr>
                                
                                <td>
                                    <asp:CheckBox runat="server" ID="chkCreateRoomTypeLowerBunkBed" Enabled="false" />
                                </td>
                                <td>
                                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:litLowerBunkBed %>"></asp:Literal>
                                </td>
                                
                                <td>
                                    <asp:CheckBox runat="server" ID="chkCreateRoomTypeShower" Enabled="false" />
                                </td>
                                <td>
                                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:litPrivateShower %>"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                            </tr>
                            <tr>
                                
                                <td>
                                    <asp:CheckBox runat="server" ID="chkCreateRoomTypeSingleBed" Enabled="false" />
                                </td>
                                <td>
                                    <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:litSingleBed %>"></asp:Literal>
                                </td>
                                
                                <td>
                                    <asp:CheckBox runat="server" ID="chkCreateRoomTypeUpperBunkBed" Enabled="false" />
                                </td>
                                <td>
                                    <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:litUpperBunkBed %>"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                            </tr>
                        </table>
                    </fieldset>
                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:litFloorNumber %>"></asp:Literal>
                    <br />
                    <asp:TextBox runat="server" ID="txtCreateRoomLevelNumber">1</asp:TextBox>
                    <asp:RangeValidator runat="server" ID="RangeValidator1" ControlToValidate="txtCreateRoomLevelNumber"
                        Type="Integer" MinimumValue="1" MaximumValue="99999" ValidationGroup="valRoom"
                        Text="Must be a number between 1 and 99999"></asp:RangeValidator>
                    <br />
                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:litRoomName %>"></asp:Literal>
                    <br />
                    <asp:TextBox runat="server" ID="txtCreateRoomsName"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="reqRoomName" ControlToValidate="txtCreateRoomsName"
                        ValidationGroup="valRoom" Text="<%$ Resources:rfvRoomNameValidatorErrorMessage %>"></asp:RequiredFieldValidator>
                    <br />
                    <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:litConstructedDate %>"></asp:Literal><br />
                    <asp:TextBox runat="server" ID="txtCreateRoomConstructionDate"></asp:TextBox>
                    <br />
                    <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:litDestructionDate %>"></asp:Literal><br />
                    <asp:TextBox runat="server" ID="txtCreateRoomDestructionDate"></asp:TextBox>
                    <p>
                        <br />
                        <asp:Button runat="server" ID="btnCreateRoom" Text="<%$ Resources:btnCreateRoom %>" OnClick="btnCreateRoom_Clicked"
                            ValidationGroup="valRoom" />
                        <asp:Button runat="server" ID="btnCreateRoomCancel" OnClick="btnCreateRoomCancel_Clicked"
                            Text="<%$ Resources:btnCreateRoomCancel %>" UseSubmitBehavior="false" />
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender runat="server" ID="modalRoom" TargetControlID="lbtnPopupCreateRetreat"
            PopupControlID="pnlCreateRoom" CancelControlID="btnCreateRoomCancel">
        </ajaxToolkit:ModalPopupExtender>
    </div>
    <div style="">
        <fieldset>
            <legend>Retreat Details</legend>
            <asp:UpdatePanel runat="server" ID="updateDetails">
                <ContentTemplate>
                    <p>
                        <b>Retreat Name:</b>
                        <asp:Literal runat="server" ID="litRetreatName"></asp:Literal>
                    </p>
                    <p>
                        <b>Start Date:</b>
                        <asp:Literal runat="server" ID="litStartDate"></asp:Literal>
                    </p>
                    <p>
                        <b>Site:</b>
                        <asp:Literal runat="server" ID="litSite"></asp:Literal>
                    </p>
                    <p>
                        <b>Total Rooms:</b>
                        <asp:Literal runat="server" ID="litTotalRooms"></asp:Literal>
                    </p>
                    <p>
                        <b>Total Retreat Capacity:</b>
                        <asp:Literal runat="server" ID="litRetreatCapacity"></asp:Literal>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
    </div>
    <hr style="display: block; visibility: hidden; clear: both;" />
    <style type="text/css">
        #tblForRooms td
        {
            vertical-align: top;
            padding-bottom: 30px;
            border-bottom-width: 2px;
            border-bottom-style: solid;
            border-bottom-color: Beige;
        }
        .heading td
        {
            padding-bottom: 10px;
            border-bottom-style: none;
        }
    </style>
    <fieldset>
        <legend>Rooms Created For This Retreat</legend>
        <asp:UpdatePanel runat="server" ID="upanRooms">
            <ContentTemplate>
                <asp:Repeater runat="server" ID="rpRooms" OnItemDataBound="rpRooms_OnItemDataBound">
                    <HeaderTemplate>
                        <table id="tblForRooms">
                            <tr style="font-weight: bold;" class="heading">
                                <td style="width: 100px;">
                                    <asp:Literal ID="Literal20" runat="server" text="<%$ Resources:litBuilding %>"></asp:Literal>
                                </td>
                                <td style="width: 100px;">
                                    <asp:Literal ID="Literal21" runat="server" text="<%$ Resources:litRoom %>"></asp:Literal>
                                </td>
                                <td style="width: 100px;">
                                    <asp:Literal ID="Literal22" runat="server" text="<%$ Resources:litCapacity %>"></asp:Literal>
                                </td>
                                <td style="width: 100px; padding-right: 10px;">
                                    <asp:Literal ID="Literal23" runat="server" text="<%$ Resources:litRetreatUsage %>"></asp:Literal>
                                </td>
                                <td style="width: 200px;">
                                    <asp:Literal ID="Literal24" runat="server" text="<%$ Resources:litRoomType %>"></asp:Literal>
                                </td>
                                <td style="width: 100px;">
                                </td>
                                <td style="width: 100px;">
                                </td>
                                <td>
                                    <asp:Literal ID="Literal25" runat="server" text="<%$ Resources:litCreationDate %>"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal26" runat="server" text="<%$ Resources:litDestructionDate %>"></asp:Literal>
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal runat="server" ID="litExistingRoomBuildingName"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="litExistingRoomName"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="litCapacity"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="litRoomUseage"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="litRoomType"></asp:Literal>
                            </td>
                            <td>
                                <asp:LinkButton runat="server" ID="lbtnEditRoom" Visible="false">Edit</asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton runat="server" ID="lbtnRoomDelete" OnClick="lbtnRoomDelete_Clicked">Delete</asp:LinkButton>
                            </td>
                            <td style="padding-right: 10px;">
                                <asp:Literal runat="server" ID="litRoomConstructionDate"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="litRoomDestructionDate"></asp:Literal>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
</asp:Content>
