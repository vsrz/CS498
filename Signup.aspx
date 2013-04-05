<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Signup.aspx.cs" Inherits="Signup" Title="New User Signup" UICulture="auto" Culture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:MultiView runat="server" ID="mvSignup" ActiveViewIndex="0">
        <asp:View runat="server" ID="vSignup">
            <h1>
                <asp:Label runat="server" ID="lblPageTitle" Text="<%$ Resources:lblPageTitle %>"></asp:Label>
            </h1>
            <a href="Login.aspx">
                <asp:Label runat="server" Text="<%$ Resources:lblHaveAnAccount %>"></asp:Label>
            </a>
            <br />
            <br />
            <asp:Label runat="server" ID="lblResTest"></asp:Label>                 
            <div style="padding-left: 30px; font-weight: bold;">
                <asp:ValidationSummary runat="server" ID="valSummaryBottom" ValidationGroup="Signup"
                    ShowSummary="true" DisplayMode="List" Style="list-style-type: circle;"  EnableClientScript="true" HeaderText="The following fields were filled out incorrectly." />
            </div>
            <div id="signupPageContainer">
                <div id="signupAccountInformation">
                    <fieldset>
                        <legend>
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:lblAccountInformationHeading %>">
                            </asp:Literal>
                        </legend>            
                        
                        <!-- Username -->         
                        <asp:UpdatePanel runat="server" ID="upanUsername" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Literal runat="server" Text="<%$ Resources:lblUserName %>"></asp:Literal>
                                <br />              
                                <asp:TextBox runat="server" ID="txtUsername" MaxLength="15" Width="180px" OnTextChanged="CheckUsername_Clicked"
                                    AutoPostBack="true"></asp:TextBox>
                                <asp:Label runat="server" ID="lblUsernameAlreadyExists" Style="color: Red;" Visible="false">Username already exists.</asp:Label>
                                <asp:RequiredFieldValidator runat="server" ID="reqUserName" ControlToValidate="txtUsername"
                                    ValidationGroup="Signup" ErrorMessage="Username required" Display="Dynamic">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ValidationGroup="Signup" ID="regValidateUsername"
                                    ControlToValidate="txtUsername" ValidationExpression="^((?=[^\.]*\.?[^\.]*)(?=[^_]*_?[^_]*)(?=[^@]*@?[^@]*)[a-zA-Z0-9]{4,15})$"
                                    ErrorMessage="Username must only be 6 to 15 characters long with alphanumeric characters.">Invalid username formatting. </asp:RegularExpressionValidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        
                        <!-- Password -->
                        <asp:Literal runat="server" Text="<%$ Resources:lblPassword %>"></asp:Literal>
                        <br />
                        <asp:TextBox runat="server" ID="txtPassword1" TextMode="Password" MaxLength="15"
                            Width="180px"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtPassword1"
                            ValidationGroup="Signup" ErrorMessage="First password required" Display="Dynamic">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ControlToValidate="txtPassword1" runat="server" ID="regPassword1"
                            ValidationExpression="^[a-zA-Z]\w{3,14}$" ErrorMessage="Password must be between 4 and 15 characters in length and only contain alpha numeric characters. Cannot start with a number.">Invalid Password</asp:RegularExpressionValidator>
                        <br />
                        
                        <!-- Password Verification -->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:lblConfirmPassword %>"></asp:Literal>                       
                        <br />
                        <asp:TextBox runat="server" ID="txtPassword2" TextMode="Password" MaxLength="15"
                            Width="180px"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtPassword2"
                            ValidationGroup="Signup" ErrorMessage="Second password required" Display="Dynamic">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator runat="server" ID="comparePassword" ControlToCompare="txtPassword1" 
                            ControlToValidate="txtPassword2" ValidationGroup="Signup" ErrorMessage="Passwords must match">Passwords don't match.</asp:CompareValidator>
                        <br />
                        
                        <!-- E-Mail Address -->
                        <asp:Literal runat="server" Text="<%$ Resources:lblEmailAddress %>"></asp:Literal>
                        <br />
                        <asp:TextBox runat="server" ID="txtEmailAddress1" MaxLength="200" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtEmailAddress1"
                            ValidationGroup="Signup" ErrorMessage="First email address required" Display="Dynamic">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="valRegEmail" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
                            ControlToValidate="txtEmailAddress1" runat="server" ErrorMessage="Invalid email address">*</asp:RegularExpressionValidator>
                        <br />
                        
                        <!-- Confirm E-Mail -->
                        <asp:Literal runat="server" Text="<%$ Resources:lblConfirmEmail %>"></asp:Literal>
                        <br />               
                        <asp:TextBox runat="server" ID="txtEmailAddress2" MaxLength="200" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtEmailAddress2"
                            ValidationGroup="Signup" ErrorMessage="Second email address required" Display="Dynamic">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
                            ControlToValidate="txtEmailAddress2" runat="server" ErrorMessage="Invalid email address">*</asp:RegularExpressionValidator>
                        <br />
                        
                        <!-- Security Question -->
                        <asp:Literal runat="server" Text="<%$ Resources:lblQuestion %>"></asp:Literal>                        
                        <br />
                        <asp:TextBox runat="server" ID="txtQuestion" MaxLength="200" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator14" ControlToValidate="txtQuestion"
                            ValidationGroup="Signup" ErrorMessage="Question required" Display="Dynamic">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <!-- Answer Security Question -->
                        <asp:Literal runat="server" Text="<%$ Resources:lblAnswer %>"></asp:Literal>
                        <br />
                        <asp:TextBox runat="server" ID="txtAnswer" MaxLength="127" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator15" ControlToValidate="txtEmailAddress2"
                            ValidationGroup="Signup" ErrorMessage="Answer to question required" Display="Dynamic">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <!-- Language Choices -->
                        <asp:Literal runat="server" Text="<%$ Resources:litFirstLang %>"></asp:Literal>&nbsp;
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:litSecondLang %>"></asp:Literal>
                        <br />
                        <asp:DropDownList runat="server" ID="dlFirstLang"></asp:DropDownList>
                        <asp:DropDownList runat="server" ID="dlSecondLang"></asp:DropDownList>
                    </fieldset>
                </div>
                <div id="signupPersonalInformation">
                    <fieldset>
                        <legend>
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:lblPersonalInformationHeading %>">
                            </asp:Literal>
                        </legend>
                        
                        <!-- First Name -->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:lblFirstName %>"></asp:Literal>                
                        <br />
                        <asp:TextBox runat="server" ID="txtFirstName" MaxLength="50" Width="250"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtFirstName"
                            ValidationGroup="Signup" ErrorMessage="First Name required" Display="Dynamic">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <!-- Last Name -->
                        <asp:Literal runat="server" Text="<%$ Resources:lblLastName %>"></asp:Literal>                
                        <br />
                        <asp:TextBox runat="server" ID="txtLastName" MaxLength="50" Width="250"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtLastName"
                            ValidationGroup="Signup" ErrorMessage="Last Name required" Display="Dynamic">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <!-- Birthdate -->
                        <asp:Literal runat="server" Text="<%$ Resources:lblBirthdate %>"></asp:Literal><br />
                        <asp:TextBox runat="server" ID="txtBirthdate" MaxLength="10" Width="250" Text="<%$ Resources:txtBirthdateFormat %>"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="txtBirthdate"
                            ValidationGroup="Signup" ErrorMessage="Birthdate required" Display="Dynamic">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <!-- Gender -->
                        <asp:Literal runat="server" Text="<%$ Resources:lblGender %>"></asp:Literal><br />
                        <asp:DropDownList runat="server" ID="ddlGender">
                            <asp:ListItem>Male</asp:ListItem>
                            <asp:ListItem>Female</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        
                        <!-- Address -->
                        <asp:Literal runat="server" Text="<%$ Resources:lblAddress %>"></asp:Literal>
                        <br />
                        <asp:TextBox runat="server" ID="txtAddress" MaxLength="255" Width="300"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtAddress"
                            ValidationGroup="Signup" ErrorMessage="Street Address Required" Display="Dynamic">*
                        </asp:RequiredFieldValidator>
                        <br />
                        
                        <!-- City -->
                        <asp:Literal runat="server" Text="<%$ Resources:lblCity %>"></asp:Literal>
                        <br />
                        <asp:TextBox runat="server" ID="txtCity" MaxLength="50" Width="250"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txtAddress"
                            ValidationGroup="Signup" ErrorMessage="City required" Display="Dynamic">*
                        </asp:RequiredFieldValidator>
                        <br />
                        
                        <!-- State/Province -->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:lblState %>"></asp:Literal>&nbsp;&nbsp;
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:lblPostalCode %>"></asp:Literal>
                        <br />
                        <asp:TextBox runat="server" ID="txtState" MaxLength="50" Width="95"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="txtState"
                            ValidationGroup="Signup" ErrorMessage="State required" Display="Dynamic">*
                        </asp:RequiredFieldValidator>
                        
                        <!-- Postal Code -->
                        <asp:TextBox runat="server" ID="txtPostalCode" MaxLength="50" Width="140"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ControlToValidate="txtPostalCode"
                            ValidationGroup="Signup" ErrorMessage="Postal Code Required" Display="Dynamic">*
                        </asp:RequiredFieldValidator>
                        <br />
                        
                        <!-- Country Drop Down -->
                        <asp:Literal runat="server" Text="<%$ Resources:lblCountry %>"></asp:Literal>
                        <br />
                        <asp:DropDownList runat="server" ID="ddlCountry">
                        </asp:DropDownList>                        
                        <br />
                    </fieldset>
                </div>
            </div>
            <br />
            <hr style="display: block; visibility: hidden; clear: both;"/>
            <asp:Button runat="server" ID="btnSignup" OnClick="btnSignup_Clicked" Text="<%$ Resources:btnSignup %>" ValidationGroup="Signup" />
        </asp:View>
        <asp:View runat="server" ID="vSignupComplete">
        <h1><asp:Literal runat="server" Text="<%$ Resources:lblSignupSuccessTitle %>"></asp:Literal></h1>
        <p>
            <asp:Literal runat="server" Text="<%$ Resources:lblSuccessWelcomePara %>"></asp:Literal>        
        </p>
        <p>
            <b><a href="Default.aspx">Click here to return to the main page.</a></b>
        </p>
        </asp:View>
    </asp:MultiView>
</asp:Content>
