<%@ Page Title="Retreat Information" Language="C#" MasterPageFile="~/ControlPanel.master"
    AutoEventWireup="true" CodeFile="Retreat-GuestInformation.aspx.cs" Inherits="Retreat_GuestInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        <asp:Literal runat="server" ID="litRetreatName"></asp:Literal></h1>
    <fieldset>
        <legend>Signup Information</legend><b>Contribution Amount:</b> $<asp:Literal runat="server"
            ID="litContributionAmount"></asp:Literal>
        <br />
        <b>Contribution Made By:</b>
        <asp:Literal runat="server" ID="litContributionPaidBy"></asp:Literal>
        <br />
        <asp:Panel runat="server" ID="pnlAmountRemainingToBePaid" Visible="false">
            <b>Amount Remaining To Be Paid:</b> $<asp:Literal runat="server" ID="litAmountRemainingToBePaid"></asp:Literal>
            <br />
        </asp:Panel>
        <b>Assigned Room:</b>
        <asp:Literal runat="server" ID="litAssignedRoom"></asp:Literal>
        <br />
        <b>
            <asp:Literal ID="litCostOfSignupForMyselfText" runat="server" Text="<%$ Resources:litCostOfSignupForMyself %>"></asp:Literal></b>
        $<asp:Literal ID="litCostOfSignupForMyself" runat="server"></asp:Literal>
    </fieldset>
    <fieldset>
        <legend>Information You Should Know About This Retreat</legend>
        <asp:Literal runat="server" ID="litAttendeeInformation"></asp:Literal>
    </fieldset>
</asp:Content>
