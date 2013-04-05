<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Copyright.aspx.cs" Inherits="Copyright" Title="<%$ Resources:litPagetitle %>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        <asp:Literal runat="server" ID="Literal1" Text=" <%$ Resources:Heading_Group1 %>">
        </asp:Literal>
        <br />
        <asp:Literal runat="server" ID="Literal4" Text=" <%$ Resources:Heading_DateRange1 %>">
        </asp:literal>
    </h1>    
    <p style="width: 420px; font-size: large; font-family: Arial;">        
        <asp:Literal runat="server" ID="Literal2" Text=" <%$ Resources:Author1 %>">
        </asp:Literal>
        <br />
        <asp:Literal runat="server" ID="Literal5" Text=" <%$ Resources:Author2 %>">
        </asp:Literal>
        <br />  
        <asp:Literal runat="server" ID="Literal6" Text=" <%$ Resources:Author3 %>">
        </asp:Literal>
        <br />
        <br />
        <asp:Literal runat="server" ID="LitT" Text="Advisor: Dr. Ahmad Hadaegh">
        </asp:Literal>
        <br />
    </p>
    <h2>
        <i>ASSIGNMENT OF RIGHTS FOR THE PROJECT</i><br /><br />
    </h2>
    <p>
        
        This project was created by students as part of an independent study course granted by the California State University.<br /><br />
        The copyright of such intellectual property is governed by California State University System (CSU) Intellectual Property guidelines. Copyright ownership rests with the creator of intellectual property, which in this case is the student.  In order to utilize the student-created work for other educational activities, the undersigned student grants to CSUSM the non-exclusive right to use (i.e., copy, distribute, display, create, perform, transmit and create derivative works for nonprofit educational purpose) the student-created work and any associated documentation, data files or libraries developed for this course project.<br /><br />
        The student agrees and acknowledges that this assignment has been made voluntarily and without any expectation of academic credit at CSUSM.<br /><br />
        The student warrants that he or she has full power and authority to make this agreement and that the work does not infringe any copyright, violate any property rights, or contain any scandalous, libelous, or unlawful matter.<br /><br />
        This agreement shall be construed and interpreted according to the laws of the State of California and shall be binding upon the parties hereto, their heirs, successors, assigns, and personal representatives.<br /><br />                
        <br />                          
    </p>
    <p>
        <b>
            <asp:Literal runat="server" ID="Literal3" Text=" <%$ Resources:Assignment1 %>">
            </asp:Literal>
        </b>
    </p>
    <center>
        <p>
            "Aqua Glass" Icon set provided by <a href="http://www.dezinerfolio.com/2007/07/17/aqua-gloss-icons-the-psd">http://www.dezinerfolio.com/2007/07/17/aqua-gloss-icons-the-psd</a>
            Additional copyrights may apply.
        </p>
    </center>
</asp:Content>
    