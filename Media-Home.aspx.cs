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

public partial class Media_Home : LoggedInPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        MonkData db = new MonkData();
        rpAudioRecordings.DataSource = (from a in db.jkp_Audios orderby a.Aud_DateAdded descending select a).Take(10);
        rpAudioRecordings.DataBind();
        rpBooks.DataSource = (from b in db.jkp_Books orderby b.Bk_DateAdded descending select b).Take(10);
        rpBooks.DataBind();
        rpVideos.DataSource = (from b in db.jkp_Videos orderby b.Vid_DateAdded descending select b).Take(10);
        rpVideos.DataBind();
    }

    

    
}
