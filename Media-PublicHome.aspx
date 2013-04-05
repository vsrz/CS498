<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Media-PublicHome.aspx.cs" Inherits="Media_PublicHome" Title="<%$ Resources:litPageTitle %>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="MediaPublic-Main" style="height: 800px; width: 930px;">
        <h2 style="font-size: xx-large; color: brown;">
            <asp:Literal ID="lblTitle" runat="server" Text="<%$ Resources:lblMediaTitle %>"></asp:Literal>
            <br />
            <br />
        </h2>
        <asp:UpdatePanel runat="server" ID="udpBody">
            <ContentTemplate>
                <center>
                    <div id="Media_Public_Left_Panel" style="width: 225px; height: inherit; float: left;
                        margin-right: 3px;">
                        <fieldset style="padding-top: 5px; padding-bottom: 20px;">
                            <legend>
                                <asp:Literal ID="lblFeaturedMedia" runat="server" Text="<%$ Resources:lblFeaturedMedia %>"></asp:Literal>
                            </legend>
                            <div style="height: 15%; margin-top: 15px;">
                                <asp:ImageButton ID="Image1" runat="server" ImageUrl="dpimages/video48.png" OnClick="btnVideos_Click" />
                                <br />
                                <asp:LinkButton ID="lblVideo" runat="server" Text="Video" OnClick="btnVideos_Click">
                                </asp:LinkButton>
                                <br />
                            </div>
                            <div style="height: 15%; margin-top: 40px;">
                                <asp:ImageButton ID="Image2" runat="server" ImageUrl="dpimages/speaker48.png" OnClick="btnAudio_Click" />
                                <br />
                                <asp:LinkButton ID="Literal1" runat="server" Text="Audio Recordings" OnClick="btnAudio_Click">
                                </asp:LinkButton>
                                <br />
                            </div>
                            <div style="height: 15%; margin-top: 40px;">
                                <asp:ImageButton ID="Image3" runat="server" ImageUrl="dpimages/document48.png" OnClick="btnBooks_Click" />
                                <br />
                                <asp:LinkButton ID="Literal2" runat="server" Text="Books and Text" OnClick="btnBooks_Click">
                                </asp:LinkButton>
                                <br />
                            </div>
                        </fieldset>
                    </div>
                </center>
                <div id="Media_Public_Right_Panel" style="width: 700px; height: inherit; float: right;
                    margin-left: 2px;">
                    <fieldset runat="server" id="fldVideos" visible="true" style="padding-left: 20px;
                        padding-right: 15px;">
                        <legend>
                            <asp:Literal ID="lblVideos" runat="server" Text="<%$ Resources:lblVideos %>">
                            </asp:Literal>
                        </legend>
                        <br />
                        <h2>
                            <asp:Literal runat="server" ID="litVideoTitle"></asp:Literal></h2>
                             Download Link:
                                <asp:HyperLink runat="server" ID="hlVideo"></asp:HyperLink>
                                <br />
                                <center>
                                    <asp:Image runat="server" ID="imgVideoPreview" />
                                </center>
                        <ul>
                           
                            <li style="visibility: hidden;">Author:
                                <asp:Literal runat="server" ID="litVideoAuthor"></asp:Literal>
                            </li>
                            <li>Catalog number:
                                <asp:Literal runat="server" ID="litVideoID"></asp:Literal></li>
                            <li>Subject:
                                <asp:Literal runat="server" ID="litVideoSubject"></asp:Literal></li>
                            <li>Date:
                                <asp:Literal runat="server" ID="litVideoDate"></asp:Literal>
                            </li>
                            <li>Filesize:
                                <asp:Literal runat="server" ID="litVideoFilesize"></asp:Literal></li>
                            <li>Genre:
                                <asp:Literal runat="server" ID="litVideoGenre"></asp:Literal></li>
                            <li>Language:
                                <asp:Literal runat="server" ID="litVideoLanguage"></asp:Literal></li>
                            <li>Disc Number:
                                <asp:Literal runat="server" ID="litVideoDiscNumber"></asp:Literal></li>
                            <li>Contents:
                                <asp:Literal runat="server" ID="litVideoContents"></asp:Literal></li>
                            <li>Credits:
                                <asp:Literal runat="server" ID="litVideoCredits"></asp:Literal></li>
                            <li>Comment:
                                <asp:Literal runat="server" ID="litVideoComment"></asp:Literal></li>
                            <li>Description:
                                <asp:Literal runat="server" ID="litVideoDescription"></asp:Literal></li>
                            <li>Preview:
                                <asp:Image runat="server" ID="imgVideoPreviewUrl"></asp:Image>
                            </li>
                        </ul>
                    </fieldset>
                    <fieldset style="padding-left: 20px; padding-right: 15px;" runat="server" id="fldAudio"
                        visible="false">
                        <legend>
                            <asp:Literal ID="lblAudio" runat="server" Text="Featured Audio Clip">
                            </asp:Literal>
                        </legend>
                        <br />
                        <h2>
                            <asp:Literal runat="server" ID="litAudioTitle"></asp:Literal></h2>
                            Download Link:
                                <asp:HyperLink runat="server" ID="hlAudio">Download</asp:HyperLink>
                                <br />  
                                <center>
                                    <asp:Image runat="server" ID="imgAudioPreview" />
                                </center>                              
                        <ul>
                            
                            <li>Author:
                                <asp:Literal runat="server" ID="litAudioAuthor"></asp:Literal></li>
                            <li>Subject:
                                <asp:Literal runat="server" ID="litAudioSubject"></asp:Literal></li>
                            <li>Date Added:
                                <asp:Literal runat="server" ID="litAudioDateAdded"></asp:Literal></li>
                            <li>Last Modified:
                                <asp:Literal runat="server" ID="litAudioDateModified"></asp:Literal></li>
                            <li>Year:
                                <asp:Literal runat="server" ID="litAudioYear"></asp:Literal></li>
                            <li>Genre:
                                <asp:Literal runat="server" ID="litAudioGenre"></asp:Literal></li>
                            <li>Language:
                                <asp:Literal runat="server" ID="litAudioLanguage"></asp:Literal></li>
                            <li>Publisher:
                                <asp:Literal runat="server" ID="litAudioPublisher"></asp:Literal></li>
                            <li>Content:
                                <asp:Literal runat="server" ID="litAudioContents"></asp:Literal></li>
                            <li>Media Type:
                                <asp:Literal runat="server" ID="litAudioFileTypeId"></asp:Literal></li>
                            <li>Filesize:
                                <asp:Literal runat="server" ID="litAudioFileSize"></asp:Literal></li>
                        </ul>
                    </fieldset>
                    <fieldset style="padding-left: 20px; padding-right: 15px;" runat="server" id="fldBooks"
                        visible="false">
                        <legend>
                            <asp:Literal ID="lblBooks" runat="server" Text="Featured Book Or Text">
                            </asp:Literal>
                        </legend>
                        <br />
                        
                        <h2>
                            <asp:Literal runat="server" ID="litBookTitle"></asp:Literal>
                            &nbsp;
                            <asp:Literal runat="server" ID="litBookEdition"></asp:Literal>
                        </h2>
                        Download Link:
                        <asp:HyperLink runat="server" ID="hlBook">Download</asp:HyperLink>
                        <br />
                        <center>
                                    <asp:Image runat="server" ID="imgBookPreview" />
                                </center>
                        <ul>
                            <li>Author:
                                <asp:Literal runat="server" ID="litBookAuthor"></asp:Literal></li>
                            <li>ID:
                                <asp:Literal runat="server" ID="litBookID"></asp:Literal></li>
                            <li>Subject:
                                <asp:Literal runat="server" ID="litBookSubject"></asp:Literal></li>
                            <li>Date Added:
                                <asp:Literal runat="server" ID="litBookDateAdded"></asp:Literal></li>
                            <li>Last Modified:
                                <asp:Literal runat="server" ID="litBookDateModified"></asp:Literal></li>
                            <li>Year:
                                <asp:Literal runat="server" ID="litBookYear"></asp:Literal></li>
                            <li>Genre:
                                <asp:Literal runat="server" ID="litBookGenre"></asp:Literal></li>
                            <li>Language:
                                <asp:Literal runat="server" ID="litBookLanguage"></asp:Literal></li>
                            <li>Publisher:
                                <asp:Literal runat="server" ID="litBookPublisher"></asp:Literal></li>
                            <li>Content:
                                <asp:Literal runat="server" ID="litBookContents"></asp:Literal></li>
                            <li>Media Type:
                                <asp:Literal runat="server" ID="litBookFileTypeId"></asp:Literal></li>
                            <li>Filesize:
                                <asp:Literal runat="server" ID="litBookFilesize"></asp:Literal></li>
                            <li>Preview:
                                <asp:Image runat="server" ID="litBookPreviewUrl" />
                            </li>
                        </ul>
                    </fieldset>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
