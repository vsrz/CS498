<%@ Page Language="C#" MasterPageFile="~/ControlPanel.master" AutoEventWireup="true"
    CodeFile="Account-Home.aspx.cs" Inherits="Account_Home" Title="<%$ Resources:litPageTitle %>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        My Account
    </h1>
    <br />
    <%--
    <h3>
        My Retreats</h3>
   <p>
        Currently registered for: <strong><span runat="server" id="collectionCount"></span>
        </strong>retreats</p>--%>
    <span runat="server" id="retreatList"></span>
    <div style="float: left; width: 450px;">
        <fieldset>
            <legend>
                <asp:Literal ID="Literal1" Text="<%$ Resources:lblAccountInformation %>" runat="server"></asp:Literal></legend>
            <asp:UpdatePanel runat="server" ID="upanAccountInfo">
                <ContentTemplate>
                    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="MonkData"
                        EnableUpdate="True" TableName="jkp_Persons" Where="Per_ID == @Per_ID">
                        <WhereParameters>
                            <asp:SessionParameter Name="Per_ID" SessionField="PersonId" Type="Object" />
                        </WhereParameters>
                    </asp:LinqDataSource>
                    <asp:DetailsView ID="DetailsView2" runat="server" Height="50px" Width="440px" AutoGenerateRows="False"
                        DataKeyNames="Per_ID" OnItemUpdated="DetailsView2_OnItemUpdated" DataSourceID="LinqDataSource1"
                        BorderColor="White" BorderStyle="None">
                        <Fields>
                            <asp:TemplateField HeaderText="First Name">
                                <ItemTemplate>
                                    <%# Eval("Per_FirstName") %></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" ID="dtvPerFirstName"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="dtvPerFirstName" ID="rvPerFirstName"
                                        Display="Dynamic">*First name required</asp:RequiredFieldValidator>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Last Name">
                                <ItemTemplate>
                                    <%# Eval("Per_LastName") %></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" ID="dtvPerLastName"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="dtvPerLastName" ID="rvPerLastName"
                                        Display="Dynamic">*Last name required</asp:RequiredFieldValidator>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Per_DharmaName" HeaderText="Dharma Name" SortExpression="Per_DharmaName" />
                            <asp:BoundField DataField="Per_LineageName" HeaderText="Lineage Name" SortExpression="Per_LineageName" />
                            <asp:BoundField DataField="Per_Gender" HeaderText="Gender" SortExpression="Per_Gender"
                                Visible="false" />
                            <asp:TemplateField HeaderText="Gender">
                                <ItemTemplate>
                                    <%# Eval("Per_Gender") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList runat="server" ID="genderSelection" AppendDataBoundItems="true">
                                        <asp:ListItem>M</asp:ListItem>
                                        <asp:ListItem>F</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Per_Nationality" HeaderText="Nationality" SortExpression="Per_Nationality" />
                            <asp:BoundField DataField="Per_PrimaryLanguage" HeaderText="Primary Language" SortExpression="Per_PrimaryLanguage" />
                            <asp:BoundField DataField="Per_SecondaryLanguage" HeaderText="Secondary Language"
                                SortExpression="Per_SecondaryLanguage" />
                            <asp:BoundField DataField="Per_PlaceOfBirth" HeaderText="Place Of Birth" SortExpression="Per_PlaceOfBirth" />
                            <asp:TemplateField HeaderText="Birth Date">
                                <ItemTemplate>
                                    <%# ((DateTime)Eval("Per_Birthdate")).ToShortDateString() %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" ID="txtBirthDate" Text='<%# Bind("Per_Birthdate", "{0:d}") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="rvPerBirthdate" ControlToValidate="txtBirthDate"
                                        Display="Dynamic">*Required (MM/DD/YYYY)</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ID="regBirthDate" ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$"
                                        ControlToValidate="txtBirthDate">Format: MM/DD/YYYY</asp:RegularExpressionValidator>                                    
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Per_HomePhoneNumber" HeaderText="Home Phone Number" SortExpression="Per_HomePhoneNumber" />
                            <asp:BoundField DataField="Per_CellPhoneNumber" HeaderText="Cell Phone Number" SortExpression="Per_CellPhoneNumber" />
                            <asp:BoundField DataField="Per_WorkPhoneNumber" HeaderText="Work Phone Number" SortExpression="Per_WorkPhoneNumber" />
                            <asp:BoundField DataField="Per_Fax" HeaderText="Fax" SortExpression="Per_Fax" />
                            <asp:BoundField DataField="Per_Email" HeaderText="Email" SortExpression="Per_Email" />
                            <asp:BoundField DataField="Per_SanghaRole" HeaderText="Sangha Role" SortExpression="Per_SanghaRole" />
                            <asp:CommandField ShowEditButton="True" ButtonType="Button" EditText="Edit" />
                        </Fields>
                    </asp:DetailsView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
        <fieldset>
            <legend>
                <asp:Literal ID="Literal2" Text="<%$ Resources:lblAddress %>" runat="server"></asp:Literal></legend>
            <asp:UpdatePanel runat="server" ID="upanAddress">
                <ContentTemplate>
                    <asp:LinqDataSource ID="LinqDataSource2" runat="server" ContextTypeName="MonkData"
                        EnableUpdate="true" TableName="jkp_Addresses" Where="Add_ID == Guid( @PersonAddressId)">
                        <WhereParameters>
                            <asp:SessionParameter Name="PersonAddressId" SessionField="PersonAddressId" Type="Object" />
                        </WhereParameters>
                    </asp:LinqDataSource>
                    <asp:DetailsView ID="detailsAddress" runat="server" Height="50px" Width="380px" AutoGenerateRows="False"
                        DataKeyNames="Add_ID" OnItemUpdated="detailsAddress_OnItemUpdated" DataSourceID="LinqDataSource2"
                        BorderColor="White" BorderStyle="None">
                        <Fields>
                            <asp:BoundField DataField="Add_Street" HeaderText="Street" SortExpression="Add_Street" />
                            <asp:BoundField DataField="Add_City" HeaderText="City" SortExpression="Add_City" />
                            <asp:BoundField DataField="Add_State" HeaderText="State" SortExpression="Add_State" />
                            <asp:BoundField DataField="Add_Country" HeaderText="Country" SortExpression="Add_Country" />
                            <asp:BoundField DataField="Add_PostalCode" HeaderText="PostalCode" SortExpression="Add_PostalCode" />
                            <asp:TemplateField HeaderText="Sangha">
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="dlSanghas" Width="200px">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList runat="server" ID="dlSanghas" Width="200px">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" ButtonType="Button" EditText="Edit" />
                        </Fields>
                    </asp:DetailsView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
    </div>
    <div style="">
        <fieldset runat="server" id="fieldsetRoles">
            <legend>
                <asp:Literal ID="Literal3" Text="<%$ Resources:lblSiteRoles %>" runat="server"></asp:Literal></legend>
            <asp:Repeater runat="server" ID="rpRolesIn" OnItemDataBound="rpRolesIn_OnItemDataBound">
                <HeaderTemplate>
                    <ol>
                </HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <asp:Literal runat="server" ID="litRole"></asp:Literal>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ol>
                </FooterTemplate>
            </asp:Repeater>
        </fieldset>
        <fieldset>
            <legend>
                <asp:Literal ID="Literal4" Text="<%$ Resources:lblRetreatRegistration %>" runat="server"></asp:Literal></legend>
            <asp:Repeater runat="server" ID="rpRetreats">
                <HeaderTemplate>
                    <table>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="width: 250px;">
                            <a href="Retreat-PublicView.aspx?retreatid=<%# Eval("Ret_Id") %>">
                                <%# Eval("Ret_Name") %></a>
                        </td>
                        <td>
                            <a href="Retreat-GuestInformation.aspx?retreatid=<%# Eval("Ret_Id") %>">Signup Information</a>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </fieldset>
        <fieldset runat="server" id="fieldsetPayments" visible="false">
            <legend>
                <asp:Literal ID="Literal5" Text="<%$ Resources:lblPayments %>" runat="server"></asp:Literal></legend>
        </fieldset>
    </div>
    <hr style="clear: both; display: block; visibility: hidden;" />
</asp:Content>
