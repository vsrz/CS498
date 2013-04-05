<%@ Page Language="C#" MasterPageFile="~/ControlPanel.master" AutoEventWireup="true"
    CodeFile="GridView.aspx.cs" Inherits="GridView" Title="<%$ Resources:PageTitle %>" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <h1>
        <asp:Literal runat="server" ID="litTitle" Text="Items"></asp:Literal>
    </h1>
    <asp:HyperLink runat="server" ID="hlAddNewItem" Text="<%$ Resources:AddNew %>">        
    </asp:HyperLink>
    <div id="grid-view">
        
        <asp:UpdatePanel runat="server" ID="upanGrid">
            <ContentTemplate>
                <fieldset>
                    <legend>
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Search %>"></asp:Literal>
                    </legend>
                    <asp:TextBox runat="server" ID="txtSearch" style="width: 700px;"></asp:TextBox>
                    <asp:Button runat="server" ID="btnSerach" OnClick="btnSearch_Clicked" Text="<%$ Resources:btnSearch %>" UseSubmitBehavior="true" />
                    <br />
                    <b>
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Fields %>"></asp:Literal>
                    </b>
                    <br />
                    <div id="searchFields">
                        <asp:PlaceHolder runat="server" ID="plcSearchFields"></asp:PlaceHolder>
                    </div>
                    <br />
                    <b>
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:OrderBy %>"></asp:Literal>
                    </b>
                    <asp:PlaceHolder runat="server" ID="plcOrderByFields"></asp:PlaceHolder>
                    <br />
                    <b>
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:OrderDirection %>"></asp:Literal>
                    </b>
                    <asp:RadioButton runat="server" ID="rbtnAscending" GroupName="OrderDirection" />
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:Ascending %>"></asp:Literal>
                    <asp:RadioButton runat="server" ID="rbtnDescending" GroupName="OrderDirection" Checked="true" /> 
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:Descending %>"></asp:Literal>
                    <br />
                    <b>
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:NumberOfResults %>"></asp:Literal>
                    </b><br />
                    <asp:DropDownList runat="server" ID="dlResultCount" OnSelectedIndexChanged="dlResultCount_Changed"
                        AutoPostBack="true">
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                        <asp:ListItem>1000</asp:ListItem>
                    </asp:DropDownList>
                </fieldset>
                <asp:LinqDataSource runat="server" ID="linqDataSource" ContextTypeName="MonkData"
                    OnSelecting="linqDataSource_Selecting" AutoPage="true">
                </asp:LinqDataSource>
                <asp:SqlDataSource runat="server" ID="sqlDataSource" DataSourceMode="DataSet"></asp:SqlDataSource>
                <asp:DataGrid runat="server" ID="dataGrid" AllowPaging="True" AutoGenerateColumns="false"
                    OnItemDataBound="dataGrid_OnItemDataBound" BorderStyle="None" BorderColor="White"
                    ItemStyle-CssClass="view-items-grid-item" HeaderStyle-Font-Bold="true" PageSize="5"
                    OnPageIndexChanged="pageIndexChanged" PagerStyle-NextPageText="Next" PagerStyle-PrevPageText="Previous"
                    DataSourceID="linqDataSource">
                    <Columns>
                        <asp:BoundColumn HeaderStyle-Width="700" DataFormatString="{0}" HeaderText="Title Of Item">
                        </asp:BoundColumn>
                        <asp:TemplateColumn>
                            <HeaderTemplate>
                                <b>
                                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:ClickToEditItem %>"></asp:Literal>
                                </b>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="hlEditItem" runat="server" Text="Edit"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDeleteItem" runat="server" CausesValidation="false" OnClick="btnDelete_Clicked"
                                    Text="<%$ Resources:lbtnDeleteItem %>"></asp:LinkButton>
                                <ajaxToolkit:ConfirmButtonExtender runat="server" ID="confirmDelete" ConfirmText="<%$ Resources:confirmDelete %>"
                                    TargetControlID="lbtnDeleteItem">
                                </ajaxToolkit:ConfirmButtonExtender>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
