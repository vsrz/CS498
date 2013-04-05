<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Forgot-Password.aspx.cs" Inherits="Forgot_Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Forgot Your Password</h1>
    <asp:MultiView runat="server" ID="mvForgot" ActiveViewIndex="0">
        <asp:View runat="server" ID="vYourInformation">
            <center>
                Please enter the email address you signed up with:
                <br />
                <asp:TextBox runat="server" ID="txtEmailAddress"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ValidationGroup="forgot" ID="reqPassword"
                    ControlToValidate="txtEmailAddress">*</asp:RequiredFieldValidator>
                <br />
                <asp:Button runat="server" ID="btnEmailPassword" ValidationGroup="forgot" Text="Submit" OnClick="btnEmailPassword_Clicked" />
            </center>
        </asp:View>
        <asp:View runat="server" ID="vEmailSent">
            <center>
                An email has been sent to your email address containing your password to the site.
            </center>
        </asp:View>
        <asp:View runat="server" ID="vNoEmailAddressFound">
            <center>
                An email address was not found for this user in our system.
            </center>
        </asp:View>
    </asp:MultiView>
</asp:Content>
