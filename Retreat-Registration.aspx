<%@ Page Language="C#" MasterPageFile="~/ControlPanel.master" AutoEventWireup="true"
    CodeFile="Retreat-Registration.aspx.cs" Inherits="Event_Registration" Title="<%$ Resources:PageTitle %>"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:PageTitle %>"></asp:Literal>
    </h1>
    <h2 runat="server" id="h3retreatName">
        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:RegistrationFormFor %>"></asp:Literal>
        <asp:Label runat="server" ID="lblRetreatName"></asp:Label></h2>
    <h4 visible="false">
        <asp:Label runat="server" ID="lblRetreatError"></asp:Label></h4>
    <asp:HyperLink runat="server" ID="redirectLink" Visible="false" NavigateUrl="~/Events.aspx"
        Text="Click here to go back to the events page."></asp:HyperLink>
    <asp:MultiView ID="registrationView" runat="server" ActiveViewIndex="0">
        <asp:View ID="personStep" runat="server">
            <p>
                <asp:Literal ID="StaticLiteral3" runat="server" Text="<%$ Resources:Greeting %>"></asp:Literal>&nbsp;<span
                    id="formUserName" runat="server"></span>.&nbsp;
                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:RegisteringYourselfQuestion %>"></asp:Literal>
            </p>
            <asp:RadioButtonList ID="userSingleRetreatantButton" OnSelectedIndexChanged="UserSelectSingleStatus"
                runat="server" AutoPostBack="True">
                <asp:ListItem Selected="True" Text="<%$ Resources:RegisteringAloneButton %>"></asp:ListItem>
                <asp:ListItem Text="<%$ Resources:RegisteringWithOthersButton %>"></asp:ListItem>
            </asp:RadioButtonList>
            <br />
            <br />
            <asp:Panel runat="server" ID="pnlHowManyAttending" Visible="false">
                <fieldset class="pastPersonFieldset">
                    <legend>
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:PeopleYouHaveRegisteredWithBefore%>"></asp:Literal>
                    </legend>
                    <asp:Repeater ID="rpPastRetreatants" runat="server" OnItemDataBound="FillPastRetreatants">
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <th>
                                    </th>
                                    <th>
                                        <asp:Literal ID="StaticLiteralFirstName" runat="server" Text="<%$ Resources:FirstName %>"></asp:Literal>
                                    </th>
                                    <th>
                                        <asp:Literal ID="StaticLiteralLastName" runat="server" Text="<%$ Resources:LastName %>"></asp:Literal>
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="pastPersonCheckBox" runat="server" />
                                </td>
                                <td>
                                    <asp:Literal ID="pastPersonFirstName" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="pastPersonLastName" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:Label runat="server" ID="famGroupErrorStatus"></asp:Label>
                </fieldset>
                <br />
                <fieldset class="newPersonFieldset">
                    <legend>
                        <asp:Literal ID="StaticLiteralAdditionalPeople" runat="server" Text="<%$ Resources:AdditionalPeople%>"></asp:Literal>
                    </legend>
                    <div class="dlHowManyDiv">
                        <asp:DropDownList runat="server" ID="dlHowManyPeople" AutoPostBack="true" OnSelectedIndexChanged="SetNewPersonsAttending">
                        </asp:DropDownList>
                    </div>
                    <h3>
                        <asp:Literal ID="StaticLiteralAnyOthers" runat="server" Text="<%$ Resources:AnyOthers %>"></asp:Literal>
                    </h3>
                    <asp:ValidationSummary runat="server" ID="valSummaryBottom" ValidationGroup="additionalPersons"
                        ShowSummary="true" DisplayMode="List" Style="list-style-type: circle;" EnableClientScript="true"
                        HeaderText="The following fields were filled out incorrectly." />
                    <asp:Repeater ID="rpAdditionalRetreatants" runat="server" OnItemDataBound="SetLanguageDropdowns">
                        <HeaderTemplate>
                            <h4>
                                <asp:Literal ID="StaticLiteralARI" runat="server" Text="<%$ Resources:AdditionalRetreatantInfo %>"></asp:Literal>
                            </h4>
                            <table>
                                <tr>
                                    <th>
                                        <asp:Literal ID="StaticLiteralFN" runat="server" Text="<%$ Resources:FirstName %>"></asp:Literal>
                                    </th>
                                    <th>
                                        <asp:Literal ID="SLLN1" runat="server" Text="<%$ Resources:LastName %>"></asp:Literal>
                                    </th>
                                    <th>
                                        <asp:Literal ID="SLBDate" runat="server" Text="<%$ Resources:Birthdate %>"></asp:Literal>
                                    </th>
                                    <th>
                                        <asp:Literal ID="SLGender" runat="server" Text="<%$ Resources:Gender %>"></asp:Literal>
                                    </th>
                                    <th>
                                        <asp:Literal ID="FLanguage" runat="server" Text="<%$ Resources:FirstLanguage %>"></asp:Literal>
                                    </th>
                                    <th>
                                        <asp:Literal ID="SLanguage" runat="server" Text="<%$ Resources:SecondLanguage %>"></asp:Literal>
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtAddFirstName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="reqAddFirstName" ControlToValidate="txtAddFirstName"
                                        ValidationGroup="additionalPersons" ErrorMessage="<%$ Resources:FirstNameValidation %>"
                                        Display="Dynamic">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ValidationGroup="additionalPersons"
                                        ID="regValidateFirstName" ControlToValidate="txtAddFirstName" ValidationExpression="^((?=[^\.]*\.?[^\.]*)(?=[^_]*_?[^_]*)(?=[^@]*@?[^@]*)[a-zA-Z0-9]{2,15})$"
                                        ErrorMessage="First must be between 3 and 15 characters long with alphanumeric characters.">*</asp:RegularExpressionValidator>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAddLastName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="reqAddLastName" ControlToValidate="txtAddLastName"
                                        ValidationGroup="additionalPersons" ErrorMessage="<%$ Resources:LastNameValidation %>"
                                        Display="Dynamic">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ValidationGroup="additionalPersons"
                                        ID="regValidateLastName" ControlToValidate="txtAddLastName" ValidationExpression="^((?=[^\.]*\.?[^\.]*)(?=[^_]*_?[^_]*)(?=[^@]*@?[^@]*)[a-zA-Z0-9]{2,15})$"
                                        ErrorMessage="Last name must be between 3 and 15 characters long with alphanumeric characters.">*</asp:RegularExpressionValidator>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAddBirthdate" runat="server" Text="MM/DD/YYYY"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="reqAddBirthdate" ControlToValidate="txtAddBirthdate"
                                        ValidationGroup="additionalPersons" ErrorMessage="<%$ Resources:BirthdateValidation %>"
                                        Display="Dynamic">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="regValidateBirthdate" runat="server" ControlToValidate="txtAddBirthdate"
                                        Text="Must be formatted like 11/30/2003" ValidationGroup="additionalPersons"
                                        ValidationExpression="^(((((0[13578])|([13578])|(1[02]))[\-\/\s]?((0[1-9])|([1-9])|([1-2][0-9])|(3[01])))|((([469])|(11))[\-\/\s]?((0[1-9])|([1-9])|([1-2][0-9])|(30)))|((02|2)[\-\/\s]?((0[1-9])|([1-9])|([1-2][0-9]))))[\-\/\s]?\d{4})(\s(((0[1-9])|([1-9])|(1[0-2]))\:([0-5][0-9])((\s)|(\:([0-5][0-9])\s))([AM|PM|am|pm]{2,2})))?$">*</asp:RegularExpressionValidator>
                                </td>
                                <td>
                                    <asp:DropDownList ID="dlAddGender" runat="server">
                                        <asp:ListItem Text="<%$ Resources:Male %>"></asp:ListItem>
                                        <asp:ListItem Text="<%$ Resources:Female %>"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="dlFirstLangPref">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="dlSecondLangPref">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </fieldset>
            </asp:Panel>
            <br />
            <asp:Button runat="server" ID="personStepNext" Text="<%$ Resources:personStepNext %>"
                ValidationGroup="additionalPersons" OnClick="ChangeToView1" />
        </asp:View>
        <asp:View runat="server" ID="roomStep">
            <h3>
                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:AvailableRoomTypes %>"></asp:Literal>
            </h3>
            <p>
                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:PricingForRoomsIsPerPersonPerNight %>"></asp:Literal>
            </p>
            <fieldset>
                <legend>
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:RoomTypes %>"></asp:Literal>
                </legend>
                <asp:Repeater runat="server" ID="roomTypeRepeater" OnItemDataBound="roomTypeRepeaterBound">
                    <HeaderTemplate>
                        <table class="roomTypeSelectTable">
                            <tr>
                                <th>
                                    <asp:Literal ID="Literalzz3" runat="server" Text="<%$ Resources:RoomType %>"></asp:Literal>
                                </th>
                                <th>
                                    <asp:Literal ID="StaticLiteralAC" runat="server" Text="<%$ Resources:AirConditioning %>"></asp:Literal>
                                </th>
                                <th>
                                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:Bathroom %>"></asp:Literal>
                                </th>
                                <th>
                                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:Heating %>"></asp:Literal>
                                </th>
                                <th>
                                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:BunkBed %>"></asp:Literal>
                                </th>
                                <th>
                                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:SingleBed %>"></asp:Literal>
                                </th>
                                <th>
                                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:Shower %>"></asp:Literal>
                                </th>
                                <th>
                                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:CostPerNight %>"></asp:Literal>
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblRoomType"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblAC"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblBathroom"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblHeating"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblBunkBed"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblSingleBed"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblShower"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblCostPerNight"></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </fieldset>
            <br />
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <fieldset>
                        <legend>
                            <asp:Literal ID="StaticLiteralSetRoomPreferences" runat="server" Text="<%$ Resources:SetRoomPreferences%>"></asp:Literal>
                        </legend>
                        <p>
                            <asp:Literal ID="SLitAC" runat="server" Text="<%$ Resources:RoomPreferencesInfo %>"></asp:Literal>
                            <asp:Literal runat="server" ID="retStart"></asp:Literal>
                            <asp:Literal ID="slRPI2" runat="server" Text="<%$ Resources:RoomPreferencesInfo2 %>"></asp:Literal>
                            <asp:Literal runat="server" ID="retEnd"></asp:Literal>
                            <asp:Literal ID="slRPI3" runat="server" Text="<%$ Resources:RoomPreferencesInfo3 %>"></asp:Literal>
                        </p>
                        <asp:Repeater runat="server" ID="roomPreferenceRepeater" OnItemDataBound="PersonRoomPrefRepeater">
                            <HeaderTemplate>
                                <table>
                                    <tr>
                                        <th>
                                            <asp:Literal ID="sliPerson" runat="server" Text="<%$ Resources:Person %>"></asp:Literal>
                                        </th>
                                        <th>
                                            <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:RoomTypePreference %>"></asp:Literal>
                                        </th>
                                        <th>
                                            <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:ArrivalDate %>"></asp:Literal>
                                        </th>
                                        <th>
                                            <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:DepartureDate %>"></asp:Literal>
                                        </th>
                                        <th>
                                            <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:NumberOfNights %>"></asp:Literal>
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="roomTypePrefPerson"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="dlRoomTypePref" OnSelectedIndexChanged="CalculateFamilyTotal"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="roomTypePrefArrivalDate" OnTextChanged="CalculateFamilyTotal"
                                            AutoPostBack="true"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender runat="server" TargetControlID="roomTypePrefArrivalDate"
                                            Format="MM/dd/yyyy" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="roomTypePrefDepartureDate" OnTextChanged="CalculateFamilyTotal"
                                            AutoPostBack="true"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender runat="server" TargetControlID="roomTypePrefDepartureDate"
                                            Format="MM/dd/yyyy" />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="roomTypePrefNumberOfNights"></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </fieldset>
                    <p>
                        <strong>
                            <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:EstimatedTotal %>"></asp:Literal>
                            <asp:Label runat="server" ID="estTotalCost"></asp:Label></strong></p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <asp:Button runat="server" ID="roomStepPrev" OnClick="ChangeToView0" Text="<%$ Resources:roomStepPrev %>" />
            <asp:Button runat="server" ID="roomStepNext" OnClick="ChangeToView2" Text="<%$ Resources:roomStepNext %>" />
        </asp:View>
        <asp:View ID="paymentStep" runat="server">
            <div class="paymentWrapDiv" id="paymentWrapper">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:LinkButton runat="server" ID="lbtnAdmin" Text="Are you an administrator?"></asp:LinkButton>
                        <ajaxToolkit:ModalPopupExtender runat="server" ID="modalAdmin" TargetControlID="lbtnAdmin"
                            PopupControlID="pnlAdminAuth" CancelControlID="btnCancelAdminAuth">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel runat="server" ID="pnlAdminAuth" CssClass="popup-panel">
                            <asp:Literal runat="server" Text="Enter the registrar bypass to enable 'other' payment method."></asp:Literal><br />
                            <asp:TextBox runat="server" ID="txtAdminAuth" MaxLength="15" TextMode="Password"></asp:TextBox><br />
                            <span style="color: Red;">
                                <asp:Literal runat="server" ID="litAuthError"></asp:Literal></span><br />
                            <asp:Button runat="server" ID="btnAdminAuth" Text="Authorize" OnClick="btnAdminAuth_Clicked" />
                            <asp:Button runat="server" ID="btnCancelAdminAuth" Text="Cancel" />
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:Panel runat="server" ID="pnlPaymentStepChoice" Visible="false">
                    <h4>
                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:ContributionDirections %>"></asp:Literal>
                    </h4>
                    <br />
                    <asp:RadioButtonList runat="server" AutoPostBack="true" ID="dlPaymentChoice" OnSelectedIndexChanged="PaymentMethodChanged">
                        <asp:ListItem Selected="True" Text="<%$ Resources:ContributionMethodChoiceByCC %>">
                        </asp:ListItem>
                        <asp:ListItem Text="<%$ Resources:ContributionMethodChoiceArranged %>">
                        </asp:ListItem>
                    </asp:RadioButtonList>
                </asp:Panel>
                <br />
                <br />
                <asp:Label runat="server" ID="extraPaymentInfo"></asp:Label>
                <asp:UpdatePanel runat="server" ID="paymentPanel">
                    <ContentTemplate>
                        <div class="paymentInfoDiv1">
                            <fieldset>
                                <legend>
                                    <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:PaymentInformation%>"></asp:Literal>
                                </legend>
                                <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:CCType %>"></asp:Literal>
                                <br />
                                <asp:DropDownList ID="dlCardChoice" runat="server">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="Visa" Text="<%$ Resources:Visa %>"></asp:ListItem>
                                    <asp:ListItem Value="Amex" Text="<%$ Resources:Amex %>"></asp:ListItem>
                                    <asp:ListItem Value="Discover" Text="<%$ Resources:DiscoverCard %>"></asp:ListItem>
                                    <asp:ListItem Value="MasterCard" Text="<%$ Resources:Mastercard %>"></asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <asp:Literal ID="Literal41" runat="server" Text="<%$ Resources:Currency %>"></asp:Literal><br />
                                <asp:DropDownList ID="dlCurrency" runat="server">
                                    <asp:ListItem Text="<%$ Resources:CurrencyUS %>" Value="USD"></asp:ListItem>
                                    <asp:ListItem Text="<%$ Resources:CurrencyEuro %>" Value="Euro"></asp:ListItem>
                                </asp:DropDownList>
                                <p>
                                    <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:CCNumber %>"></asp:Literal>
                                </p>
                                <p>
                                    <asp:TextBox ID="txtCardNum" MaxLength="16" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtCardNum"
                                        ValidationGroup="cardInfo" Display="Dynamic" ErrorMessage="<%$ Resources:CCValidation %>"></asp:RequiredFieldValidator></p>
                                <p>
                                    <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:ExpirationDate%>"></asp:Literal>
                                </p>
                                <p>
                                    <asp:TextBox ID="txtCardExpir" MaxLength="10" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtCardExpir"
                                        ValidationGroup="cardInfo" Display="Dynamic" ErrorMessage="<%$ Resources:ExpirationValidation %>"></asp:RequiredFieldValidator></p>
                                <p>
                                    <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:FirstNameOnCard %>"></asp:Literal>
                                </p>
                                <p>
                                    <asp:TextBox ID="txtCardFirstName" MaxLength="40" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtCardFirstName"
                                        ValidationGroup="cardInfo" Display="Dynamic" Text="<%$ Resources:FirstNameValidation %>"></asp:RequiredFieldValidator></p>
                                <p>
                                    <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:LastNameOnCard %>"></asp:Literal>
                                </p>
                                <p>
                                    <asp:TextBox ID="txtCardLastName" MaxLength="40" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtCardLastName"
                                        ValidationGroup="cardInfo" Display="Dynamic" Text="<%$ Resources:LastNameValidation %>"></asp:RequiredFieldValidator></p>
                            </fieldset>
                        </div>
                        <div class="paymentInfoDiv2">
                            <fieldset>
                                <legend>
                                    <asp:Literal ID="Literal25" runat="server" Text="<%$ Resources:BillingInfo%>"></asp:Literal>
                                </legend>
                                <p>
                                    <asp:Literal ID="Literal26" runat="server" Text="<%$ Resources:BillingAddress%>"></asp:Literal>
                                </p>
                                <p>
                                    <asp:TextBox ID="txtBillingAddress" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtBillingAddress"
                                        ValidationGroup="cardInfo" Display="Dynamic" Text="<%$ Resources:BillingAddressValidator %>"></asp:RequiredFieldValidator></p>
                                <p>
                                    <asp:Literal ID="Literal27" runat="server" Text="<%$ Resources:BillingCity%>"></asp:Literal>
                                </p>
                                <p>
                                    <asp:TextBox ID="txtBillingCity" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtBillingCity"
                                        ValidationGroup="cardInfo" Display="Dynamic" Text="<%$ Resources:BillingCityValidator %>"></asp:RequiredFieldValidator></p>
                                <p>
                                    <asp:Literal ID="Literal28" runat="server" Text="<%$ Resources:BillingZipcode %>"></asp:Literal>
                                </p>
                                <p>
                                    <asp:TextBox ID="txtBillingZip" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txtBillingZip"
                                        ValidationGroup="cardInfo" Display="Dynamic" Text="<%$ Resources:BillingZipcodeValidator %>"></asp:RequiredFieldValidator></p>
                                <p>
                                    <asp:Literal ID="Literal29" runat="server" Text="<%$ Resources:BillingState%>"></asp:Literal>
                                </p>
                                <p>
                                    <asp:TextBox ID="txtBillingState" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="txtBillingState"
                                        ValidationGroup="cardInfo" Display="Dynamic" Text="<%$ Resources:BillingStateValidator %>"></asp:RequiredFieldValidator></p>
                                <p>
                                    <asp:Literal ID="Literal30" runat="server" Text="<%$ Resources:BillingCountry%>"></asp:Literal>
                                </p>
                                <p>
                                    <asp:TextBox ID="txtBillingCountry" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="txtBillingCountry"
                                        ValidationGroup="cardInfo" Display="Dynamic" Text="<%$ Resources:BillingCountryValidator %>"></asp:RequiredFieldValidator></p>
                            </fieldset>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                <asp:Button runat="server" ID="paymentPreviousStep" OnClick="ChangeToView1" Text="<%$ Resources:paymentPreviousStep %>" />
                <asp:Button runat="server" ID="paymentNextStep" OnClick="ChangeToView3" ValidationGroup="cardInfo"
                    Text="<%$ Resources:paymentNextStep %>" />
            </div>
        </asp:View>
        <asp:View ID="reviewStep" runat="server">
            <h3>
                <asp:Literal ID="Literal31" runat="server" Text="<%$ Resources:ReviewYourInformation %>"></asp:Literal>
            </h3>
            <fieldset class="reviewFieldset">
                <legend>
                    <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:GroupSummary %>"></asp:Literal>
                </legend>
                <asp:Repeater runat="server" ID="reviewPersonRepeater" OnItemDataBound="FillReviewPersonRepeater">
                    <HeaderTemplate>
                        <table class="reviewTable">
                            <tr>
                                <th>
                                    <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:Name %>"></asp:Literal>
                                </th>
                                <th>
                                    <asp:Literal ID="Literal33" runat="server" Text="<%$ Resources:RoomPreference %>"></asp:Literal>
                                </th>
                                <th>
                                    <asp:Literal ID="Literal34" runat="server" Text="<%$ Resources:PricePerNight %>"></asp:Literal>
                                </th>
                                <th>
                                    <asp:Literal ID="Literal35" runat="server" Text="<%$ Resources:Discount %>"></asp:Literal>
                                </th>
                                <th>
                                    <asp:Literal ID="Literal36" runat="server" Text="<%$ Resources:ArrivalDate %>"></asp:Literal>
                                </th>
                                <th>
                                    <asp:Literal ID="Literal37" runat="server" Text="<%$ Resources:DepartureDate %>"></asp:Literal>
                                </th>
                                <th>
                                    <asp:Literal ID="Literal38" runat="server" Text="<%$ Resources:NumberOfNights %>"></asp:Literal>
                                </th>
                                <th>
                                    <asp:Literal ID="Literal39" runat="server" Text="<%$ Resources:Total %>"></asp:Literal>
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="personNameList"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="personRoomPref"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="personPricePerNight"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="personDiscount"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="personArrival"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="personDeparture"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="personNumNights"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="personTotal"></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <strong class="strongGrandTotal">
                    <asp:Literal ID="Literal40" runat="server" Text="<%$ Resources:GrandTotal %>"></asp:Literal>
                    <asp:Label runat="server" ID="grandTotal"></asp:Label></strong>
            </fieldset>
            <br />
            <asp:Button runat="server" ID="reviewStepPrev" OnClick="ChangeToView2" Text="<%$ Resources:reviewStepPrev %>" />
            <asp:Button runat="server" ID="reviewStepNext" OnClick="ChangeToView4" Text="<%$ Resources:reviewStepNext %>" />
        </asp:View>
        <asp:View runat="server" ID="confirmationStep">
            <asp:Label runat="server" ID="authStatus"></asp:Label>
            <br />
            <asp:Button runat="server" ID="backToPaymentButton" Visible="false" Text="<%$ Resources:backToPaymentButton %>"
                OnClick="ChangeToView2" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
