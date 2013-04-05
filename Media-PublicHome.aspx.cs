using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Media_PublicHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        MonkData db = new MonkData();
        int videoCount = db.jkp_Videos.Where(p=>p.Vid_Featured).Count();

        if (videoCount > 0)
        {
            // Load Video
            Monks.jkp_Video video = db.jkp_Videos.Skip(new Random().Next(videoCount - 1)).First();
            litVideoAuthor.Text = "Unnamed";
            litVideoComment.Text = video.Vid_Comment;
            litVideoContents.Text = video.Vid_Contents;
            litVideoCredits.Text = video.Vid_Credits;
            litVideoDate.Text = video.Vid_DateAdded;
            litVideoDescription.Text = video.Vid_Description;
            if (video.Vid_Disc_Number.HasValue)
                litVideoDiscNumber.Text = video.Vid_Disc_Number.Value.ToString();
            if (video.Vid_Size.HasValue)
                litVideoFilesize.Text = video.Vid_Size.Value.ToString();
            litVideoGenre.Text = video.Vid_Genre;
            litVideoLanguage.Text = video.Vid_Language;
            litVideoSubject.Text = video.Vid_Subject;
            litVideoTitle.Text = video.Vid_Title;
            hlVideo.Text = "Download";
            hlVideo.NavigateUrl = video.Vid_Pathname;

            if (!String.IsNullOrEmpty(video.Vid_PreviewSmallUrl))
            {
                imgVideoPreview.ImageUrl = video.Vid_PreviewSmallUrl;
                imgVideoPreview.Visible = true;
            }
            else if (!String.IsNullOrEmpty(video.Vid_PreviewMediumUrl))
            {
                imgVideoPreview.ImageUrl = video.Vid_PreviewMediumUrl;
                imgVideoPreview.Visible = true;
            }
            else
                imgVideoPreview.Visible = false;
        }


        int audioCount = db.jkp_Audios.Where(p=>p.Aud_Featured).Count();
        if (audioCount > 0)
        {
            Monks.jkp_Audio audio = db.jkp_Audios.Skip(new Random().Next(audioCount - 1)).First();
            litAudioAuthor.Text = "Unnamed";
            litAudioContents.Text = audio.Aud_Contents;
            if (audio.Aud_DateAdded.HasValue)
                litAudioDateAdded.Text = audio.Aud_DateAdded.Value.ToShortDateString();
            if (audio.Aud_DateModified.HasValue)
                litAudioDateModified.Text = audio.Aud_DateModified.Value.ToShortDateString();
            if (audio.Aud_Size.HasValue)
                litAudioFileSize.Text = audio.Aud_Size.Value.ToString();
            if (audio.jkp_FileType != null)
                litAudioFileTypeId.Text = audio.jkp_FileType.For_Name;
            litAudioGenre.Text = audio.Aud_Genre;
            litAudioLanguage.Text = audio.Aud_Language;
            litAudioPublisher.Text = "Undefined";
            litAudioSubject.Text = audio.Aud_Subject;
            litAudioTitle.Text = audio.Aud_Title;
            if (audio.Aud_Track_Number.HasValue)
                litAudioYear.Text = audio.Aud_Track_Number.Value.ToString();
            hlAudio.NavigateUrl = audio.Aud_Pathname;

            if (!String.IsNullOrEmpty(audio.Aud_PreviewSmallUrl))
            {
                imgAudioPreview.ImageUrl = audio.Aud_PreviewSmallUrl;
                imgAudioPreview.Visible = true;
            }
            else if (!String.IsNullOrEmpty(audio.Aud_PreviewLargeUrl))
            {
                imgAudioPreview.ImageUrl = audio.Aud_PreviewLargeUrl;
                imgAudioPreview.Visible = true;
            }
            else
                imgAudioPreview.Visible = false;
        }



        int bookCount = db.jkp_Books.Where(p=>p.Bk_Featured).Count();
        if (bookCount > 0)
        {

            Monks.jkp_Book book = db.jkp_Books.Skip(new Random().Next(bookCount - 1)).First();
            litBookAuthor.Text = "Unnamed";
            litBookContents.Text = book.Bk_Contents;
            if (book.Bk_DateAdded.HasValue)
                litBookDateAdded.Text = book.Bk_DateAdded.Value.ToShortDateString();
            if (book.Bk_DateModified.HasValue)
                litBookDateModified.Text = book.Bk_DateModified.Value.ToShortDateString();
            litBookEdition.Text = book.Bk_Edition;
            if (book.Bk_Size.HasValue)
                litBookFilesize.Text = book.Bk_Size.Value.ToString();
            if (book.jkp_FileType != null)
                litBookFileTypeId.Text = book.jkp_FileType.For_Name;
            litBookGenre.Text = book.Bk_Genre;
            litBookLanguage.Text = book.Bk_Language;
            litBookPublisher.Text = book.Bk_Publisher;
            litBookSubject.Text = book.Bk_Subject;
            litBookTitle.Text = book.Bk_Title;
            if (book.Bk_Year.HasValue)
                litBookYear.Text = book.Bk_Year.Value.ToString();
            hlBook.NavigateUrl = book.Bk_Pathname;

            if (!String.IsNullOrEmpty(book.Bk_PreviewSmallUrl))
            {
                imgBookPreview.ImageUrl = book.Bk_PreviewSmallUrl;
                imgBookPreview.Visible = true;
            }
            else if (!String.IsNullOrEmpty(book.Bk_PreviewMediumUrl))
            {
                imgBookPreview.ImageUrl = book.Bk_PreviewMediumUrl;
                imgBookPreview.Visible = true;
            }
            else
                imgBookPreview.Visible = false;
        }

    }

    private void showPanel(int i)
    {

        switch (i)
        {
            case 0:
                fldAudio.Visible = false;
                fldBooks.Visible = false;
                fldVideos.Visible = true;
                break;
            case 1:
                fldVideos.Visible = false;
                fldBooks.Visible = false;
                fldAudio.Visible = true;
                break;
            case 2:
                fldVideos.Visible = false;
                fldAudio.Visible = false;
                fldBooks.Visible = true;
                break;
        }
    }

    public void btnVideos_Click(object sender, EventArgs e)
    {
        showPanel(0);
    }

    public void btnAudio_Click(object sender, EventArgs e)
    {
        showPanel(1);
    }

    public void btnBooks_Click(object sender, EventArgs e)
    {
        showPanel(2);
    }

}
