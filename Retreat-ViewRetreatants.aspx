<%@ Page Language="C#" MasterPageFile="~/ControlPanel.master" AutoEventWireup="true" CodeFile="Retreat-ViewRetreatants.aspx.cs" 
    Inherits="Retreat_ViewRetreatants" Title="<%$ Resources:PageTitle %>" %>
<%@ Register Src="Controls/PersonDetails.ascx" TagPrefix="monks" TagName="PersonDetailsPopup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>
    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:PageHeading %>"></asp:Literal>
</h1>
    
<p>
            <a href="Retreat-AdminOptions.aspx?retreatid=<%= Request.QueryString["retreatid"] %>">
            Return To Retreat Tools</a>
        </p>
<fieldset>
    <legend>
        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:PeopleAttendingThisRetreatList %>"></asp:Literal>
    </legend>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="MonkData" OrderBy="PAR_ContributionId" 
        Select="new ( PAR_ID, PAR_ContributionId, jkp_Contribution, jkp_Contribution.jkp_Person.Per_LastName as ContribPaidByLastName, PAR_PersonId, PAR_ArrivalTime, jkp_Contribution.Cont_AmountPaid as ContributionAmount, jkp_Contribution.Cont_PaymentMethodTypeId as ContributionTypeId, jkp_Room.Rm_Name as RoomName, PAR_DepartureTime, jkp_Room.jkp_Building.Bld_LocalName as Building, jkp_Room.jkp_Building.jkp_Hamlet.Ham_LocalName as Hamlet, jkp_Person.Per_FirstName as FirstName, jkp_Person.Per_LastName as LastName, jkp_Person.Per_Email as Email)" 
        TableName="jkp_PersonAttendingRetreats" Where="PAR_RetId == Guid(@PAR_RetId)">
        <WhereParameters>
            <asp:QueryStringParameter Name="PAR_RetId" QueryStringField="retreatid" 
                Type="Object" />
        </WhereParameters>
    </asp:LinqDataSource>
    <style type="text/css">
        .arrivalTimeField
        {
        	padding-right: 10px;
        }
        .gvRets td
        {
        	padding-left: 4px;
        	padding-right: 5px;
        }
        
    </style>
    <asp:GridView ID="gvRetreatants" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="PAR_ID" CssClass="gvRets" DataSourceID="LinqDataSource1" OnRowDataBound="gvRets_OnRowDataBound" AllowSorting="true" RowStyle-Height="50px" RowStyle-BorderColor="White">
        <Columns>
            <asp:TemplateField HeaderText="Person Details">
                <ItemTemplate>
                    <monks:PersonDetailsPopup ID="userDetails"  runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
            <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />            
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="Hamlet" HeaderText="Hamlet" SortExpression="jkp_Room.jkp_Building.jkp_Hamlet.Ham_LocalName" />            
            <asp:BoundField DataField="Building" HeaderText="Building" SortExpression="Building" />
            <asp:BoundField DataField="RoomName" HeaderText="Room" SortExpression="RoomName" />
            <asp:TemplateField HeaderText="Contrib Paid By" SortExpression="ContribPaidByLastName">
                <ItemTemplate>
                    <monks:PersonDetailsPopup ID="userContribPaidBy" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Payment Type" SortExpression="ContributionTypeId">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblPaymentType"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ContributionAmount" HeaderText="Contribution Amount" SortExpression="ContributionAmount" DataFormatString="{0:c}" />
            <asp:BoundField DataField="PAR_ArrivalTime" HeaderText="Arrival Time" 
                SortExpression="PAR_ArrivalTime" ItemStyle-CssClass="arrivalTimeField"  />
            <asp:BoundField DataField="PAR_DepartureTime" HeaderText="Departure Time" 
                SortExpression="PAR_DepartureTime" />
                
        </Columns>
    </asp:GridView>
    
    
    
    
    
</fieldset>

</asp:Content>

