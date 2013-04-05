<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login" Title="Login" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <center>
            <h1>
                <%--<asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:litLogin %>"></asp:Literal>--%>

            </h1>
            <asp:Login ID="Login1" runat="server" CreateUserUrl="~/Signup.aspx" DestinationPageUrl="~/ControlPanel.aspx" OnLoggedIn="Login_Success" 
                DisplayRememberMe="true" RememberMeSet="true" PasswordRecoveryUrl="~/Forgot-Password.aspx" PasswordRecoveryText="Forgot your password?">
            </asp:Login>
        </center>
    </asp:Content>
