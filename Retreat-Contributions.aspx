<%@ Page Language="C#" MasterPageFile="~/ControlPanel.master" AutoEventWireup="true"
    CodeFile="Retreat-Contributions.aspx.cs" Inherits="Retreat_Contributions" Title="<%$ Resources:PageTitle %>" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:PageHeader %>"></asp:Literal>
    </h1>
    <h3>
        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:RetreatName %>"></asp:Literal>
        <asp:Label runat="server" ID="lblRetreatName"></asp:Label>
    </h3>
    <br />
    <p>
        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:RetreatInstructions %>"></asp:Literal>
    </p>
    <fieldset>
        <legend>
            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Contributions %>"></asp:Literal>
        </legend>
        <asp:UpdatePanel runat="server" ID="upanMarkAsPaidRepeater">
            <ContentTemplate>
                <asp:LinkButton runat="server" ID="lbtnEditContributionInvisible" Style="visibility: hidden;
                    display: none;"></asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender runat="server" ID="modalEditContribution" PopupControlID="pnlEditContribution"
                    TargetControlID="lbtnEditContributionInvisible">
                </ajaxToolkit:ModalPopupExtender>
                <asp:LinkButton runat="server" ID="lbtnMarkAsPaidInvisible" Style="visibility: hidden;
                    display: none;"></asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender runat="server" ID="modalMarkAsPaid" PopupControlID="pnlMarkAsPaid"
                    TargetControlID="lbtnMarkAsPaidInvisible">
                </ajaxToolkit:ModalPopupExtender>
                <asp:LinkButton runat="server" ID="lbtnRefundPaymentInvisible" Style="visibility: hidden;
                    display: none;"></asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender runat="server" ID="modalRefundPayment" PopupControlID="pnlRefundedAmount"
                    TargetControlID="lbtnRefundPaymentInvisible">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Repeater runat="server" ID="rpContributions" OnItemDataBound="rpContributions_OnItemDataBound">
                    <HeaderTemplate>
                        <table style="font-size: 11px;">
                            <tr style="font-weight: bold;">
                                <td>
                                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:PaidBy %>"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:Date %>"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:PeoplePaidFor %>"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:TotalPrice %>"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:AmountPaid %>"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:AmountRemaining %>"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:RefundedAmount %>"></asp:Literal>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr valign="top">
                            <td style="width: 150px;">
                                <asp:Label runat="server" ID="lblPaidByUser"></asp:Label>
                            </td>
                            <td style="width: 70px;">
                                <asp:Literal runat="server" ID="litDate"></asp:Literal>
                            </td>
                            <td style="width: 200px;">
                                <asp:Literal runat="server" ID="litPeoplePaidFor"></asp:Literal>
                            </td>
                            <td style="width: 75px;">
                                $<asp:Literal runat="server" ID="litAmountDue"></asp:Literal>
                            </td>
                            <td style="width: 75px;">
                                $<asp:Literal runat="server" ID="litAmountPaid"></asp:Literal>
                            </td>
                            <td style="width: 75px;">
                                $<asp:Label runat="server" ID="litAmountRemainingToBePaid"></asp:Label>
                            </td>
                            <td style="width: 75px;">
                                $<asp:Literal runat="server" ID="litRefundedAmount"></asp:Literal>
                            </td>
                            <td style="width: 75px;">
                                <asp:LinkButton runat="server" ID="lbtnEditContribution" OnClick="lbtnEditContribution_Clicked">
                                    <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:EditContribution %>"></asp:Literal>
                                </asp:LinkButton>
                            </td>
                            <td style="width: 75px;">
                                <asp:LinkButton runat="server" ID="lbtnMarkAsPaid" OnClick="lbtnMarkAsPaid_Clicked">
                                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:MarkAsPaid %>"></asp:Literal>
                                </asp:LinkButton>
                            </td>
                            <td style="width: 100px;">
                                <asp:LinkButton runat="server" ID="lbtnRefund" OnClick="lbtnRefund_Clicked">
                                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:RefundPayment %>"></asp:Literal>
                                </asp:LinkButton>
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
    <asp:UpdatePanel runat="server" ID="upanEditContribution">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlEditContribution" CssClass="popup-panel">
                <h2>
                    <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:EditContributionHeader %>"></asp:Literal>
                </h2>
                <asp:Literal runat="server" ID="lit1" Text="<%$ Resources:ContributionAmount %>"></asp:Literal>
                <asp:Literal runat="server" ID="contributionAmount"></asp:Literal><br />                
                <asp:Literal runat="server" ID="lit2" Text="<%$ Resources:EditContributionAmount %>"></asp:Literal>
                <asp:TextBox runat="server" ID="txtEditContribAmount" Text="0.00"></asp:TextBox><br /><br />
                <asp:Button runat="server" ID="btnEditContribSave" OnClick="btnEditContribSave_Clicked"
                    Text="<%$ Resources:btnEditContribSave %>" />
                <asp:Button runat="server" ID="btnEditContribuCancel" OnClick="btnEditContribCancel_Clicked"
                    Text="<%$ Resources:btnEditContribuCancel %>" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="upanMarkAsPaid">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlMarkAsPaid" CssClass="popup-panel">
                <h2>
                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:MarkPersonAsPaid %>"></asp:Literal>
                </h2>
                <br />
                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:TotalPrice %>"></asp:Literal>
                $<asp:Literal runat="server" ID="litMPAmountDue">0.00</asp:Literal>
                <br />
                <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:AmountPaidSoFar %>"></asp:Literal>
                $<asp:Literal runat="server" ID="litMPAmountPaidSoFar"></asp:Literal>
                <br />
                <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:AmountPaid %>"></asp:Literal>
                <asp:TextBox runat="server" ID="txtAmountPaid" Text="0.00"></asp:TextBox>
                <br />
                <asp:Button runat="server" ID="btnMarkAsPaidSave" OnClick="btnMarkAsPaidSave_Clicked"
                    Text="<%$ Resources:btnMarkAsPaidSave %>" />
                <asp:Button runat="server" ID="btnMarkAsPaidCancel" OnClick="btnMarkAsPaidCancel_Clicked"
                    Text="<%$ Resources:btnMarkAsPaidCancel %>" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="upanRefundedAmount">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlRefundedAmount" CssClass="popup-panel">
                <h2>
                    <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:RefundedTransactions %>"></asp:Literal>
                </h2>
                <br />
                <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:ContributionPaymentType %>"></asp:Literal>
                <asp:Literal runat="server" ID="litContributionType"></asp:Literal><br />
                <asp:Label runat="server" ID="lblRefundErrors"></asp:Label><br />
                <br />
                <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:AmountAvailableToBeRefunded %>"></asp:Literal>
                &nbsp;$<asp:Literal runat="server" ID="litAmountAvailableToRefund"></asp:Literal><br />
                <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:RefundPayment %>"></asp:Literal>
                <asp:TextBox runat="server" ID="txtRefundAmount" Text="0.00"></asp:TextBox><br />
                <br />
                <asp:Button runat="server" ID="btnRefundSave" OnClick="btnRefundSave_Clicked" Text="<%$ Resources:btnRefundSave %>" />
                <asp:Button runat="server" ID="btnRefundCancel" OnClick="btnRefundCancel_Clicked"
                    Text="<%$ Resources:btnRefundCancel %>" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
