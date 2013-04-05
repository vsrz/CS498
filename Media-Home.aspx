<%@ Page Language="C#" MasterPageFile="~/ControlPanel.master" AutoEventWireup="true"
    CodeFile="Media-Home.aspx.cs" Inherits="Media_Home" Title="Media Homepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Media</h1>
    <div style="float: left; width: 320px;">
        <fieldset>
            <legend>Featured Videos</legend>
            
            <asp:Repeater runat="server" ID="rpVideos"  >                
                <ItemTemplate>
                    <div style="padding-bottom: 15px;">
                        <a href="<%# "Media-ViewVideo.aspx?videoid=" + Eval("Vid_ID").ToString() %>"><%# this.ShortenString( Eval("Vid_Title").ToString(), 30) %></a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            
            
            
        </fieldset>
    </div>
    <div style="float: left; width: 350px;">
        <fieldset>
            <legend>Featured Online Books</legend>
            
            
            
            <asp:Repeater runat="server" ID="rpBooks"  >                
                <ItemTemplate>
                    <div style="padding-bottom: 15px;">
                    <a href="<%# "Media-ViewBook.aspx?bookid=" + Eval("Bk_ID").ToString() %>"><%# this.ShortenString( Eval("Bk_Title").ToString(), 30) %></a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            
            
            
            
            
            
        </fieldset>
    </div>
    <div style="">
        <fieldset>
            <legend>Featured Audio Recordings</legend>
            
            
            <asp:Repeater runat="server" ID="rpAudioRecordings"  >                
                <ItemTemplate>
                    <div style="padding-bottom: 15px;">
                        <a href="<%# "Media-ViewAudio.aspx?audioid=" + Eval("Aud_ID").ToString() %>"><%# this.ShortenString( Eval("Aud_Title").ToString(), 40) %></a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            
            
            
            
        </fieldset>
    </div>
    <hr style="clear: both; display: block; visibility: hidden;" />
</asp:Content>
