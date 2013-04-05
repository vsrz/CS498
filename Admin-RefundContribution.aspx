<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Admin-RefundContribution.aspx.cs" 
        Inherits="Admin_RefundContribution" Title="<%$ Resources:litPageTitle %>" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div runat="server" id="leftBodyDiv" class="leftBodyDiv" style="width:30%;float:left;">
<fieldset><h3><asp:Literal ID="Literal1" Text="<%$ Resources:litHeading %>" runat="server"></asp:Literal></h3><br />
    <span runat="server" id="userNameSpan"></span></div></fieldset>
<div runat="server" id="bodyDiv" class="bodyDiv">
<fieldset runat="server" id="bodyFieldset"></fieldset>
    </div>
</asp:Content>

