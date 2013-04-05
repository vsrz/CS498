<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PersonDetails.ascx.cs"
    Inherits="Controls_PersonDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:LinkButton ID="lbtnButtonToDisplayModal" runat="server"></asp:LinkButton>
<asp:Panel runat="server" ID="pnlPersonDetails" CssClass="popup-panel">
    <h1>
        Person:
        <asp:Literal runat="server" ID="litPersonName"></asp:Literal></h1>
    <br />
    Email:
    <asp:Literal runat="server" ID="litEmailAddress"></asp:Literal>
    <br />
    Cell Phone Number: <asp:Literal runat="server" ID="litCellPhoneNumber"></asp:Literal>
    <br />
    Home Phone Number: <asp:Literal runat="server" ID="litHomePhoneNumber"></asp:Literal>
    <br />
    Address: <asp:Literal runat="server" ID="litAddress"></asp:Literal>
    
    <br />
    <asp:Button runat="server" ID="btnClose" Text="Close" />
</asp:Panel>
<ajaxToolkit:ModalPopupExtender runat="server" ID="modal" PopupControlID="pnlPersonDetails"
    TargetControlID="lbtnButtonToDisplayModal" CancelControlID="btnClose">
</ajaxToolkit:ModalPopupExtender>
