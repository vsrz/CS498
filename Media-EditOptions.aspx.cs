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

public partial class Media_EditOptions : System.Web.UI.Page
{
    public Monks.Enums.MediaTypes MediaType
    {
        get { return (Monks.Enums.MediaTypes)Enum.Parse(typeof(Monks.Enums.MediaTypes), Request.QueryString["mediatype"]); }
    }

    public Guid ItemId
    {
        get { return new Guid(Request.QueryString["itemid"]); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        MonkData db = new MonkData();
        if (MediaType == Monks.Enums.MediaTypes.Audio)
        {
            Monks.jkp_Audio audio = db.jkp_Audios.First(p => p.Aud_ID == ItemId);
            litPageTitle.Text = "Audio: " + audio.Aud_Title;

            hlEditDetails.NavigateUrl = "AddEdit.aspx?typename=jkp_Audio&itemid=" + ItemId.ToString();
            hlEditRoles.NavigateUrl = "Media-RoleManager.aspx?mediatype=Audio&itemid=" + ItemId.ToString();
        }
        else if(MediaType == Monks.Enums.MediaTypes.Book)
        {
            Monks.jkp_Book book = db.jkp_Books.First(p=>p.Bk_ID == ItemId);
            litPageTitle.Text = "Book: " + book.Bk_Title;
            hlEditDetails.NavigateUrl = "AddEdit.aspx?typename=jkp_Book&itemid=" + ItemId.ToString();
            hlEditRoles.NavigateUrl = "Media-RoleManager.aspx?mediatype=Book&itemid=" + ItemId.ToString();
        }
        else if(MediaType == Monks.Enums.MediaTypes.Image)
        {
            Monks.jkp_Image image = db.jkp_Images.First(p=>p.Img_ID == ItemId);
            litPageTitle.Text = "Image: " + image.Img_Title;
            hlEditDetails.NavigateUrl = "AddEdit.aspx?typename=jkp_Image&itemid=" + ItemId.ToString();
            hlEditRoles.NavigateUrl = "Media-RoleManager.aspx?mediatype=Image&itemid=" + ItemId.ToString();
        }
        else if(MediaType == Monks.Enums.MediaTypes.Video)
        {
            Monks.jkp_Video video = db.jkp_Videos.First(p=>p.Vid_ID == ItemId);
            litPageTitle.Text = "Image: " + video.Vid_Title;
            hlEditDetails.NavigateUrl = "AddEdit.aspx?typename=jkp_Video&itemid=" + ItemId.ToString();
            hlEditRoles.NavigateUrl = "Media-RoleManager.aspx?mediatype=Video&itemid=" + ItemId.ToString();
        }
    }
}
