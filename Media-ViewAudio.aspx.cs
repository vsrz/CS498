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

public partial class Media_ViewAudio : System.Web.UI.Page
{
    public Guid AudioId
    {
        get { return new Guid(Request.QueryString["audioid"]); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        MonkData db = new MonkData();
        Monks.jkp_Audio audio = db.jkp_Audios.First(p=>p.Aud_ID == AudioId);
        litAudioItem.Text = audio.Aud_Title;
        if(!String.IsNullOrEmpty(audio.Aud_Pathname))
        {
            hlDownload.NavigateUrl = audio.Aud_Pathname;
        }
        else
            hlDownload.NavigateUrl = "#";
    }
}
