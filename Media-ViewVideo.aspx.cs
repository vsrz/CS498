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

public partial class Media_ViewVideo : System.Web.UI.Page
{
    public Guid VideoId
    {
        get { return new Guid(Request.QueryString["videoid"]); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        MonkData db = new MonkData();
        Monks.jkp_Video videoItem = db.jkp_Videos.First(p=>p.Vid_ID == VideoId);
        
        
        
        //litContents.Text = videoItem.Vid_Contents;
        //litCredits.Text = videoItem.Vid_Credits;
        // videoItem.Vid_DateAdded;
        //litDuration.Text = videoItem.Vid_Description;
        //if(videoItem.Vid_Disc_Number != null)
        //    litDiscNumber.Text = ((int)videoItem.Vid_Disc_Number).ToString();
        //if(videoItem.Vid_Duration != null)
        //    litDuration.Text = ((decimal)videoItem.Vid_Duration).ToString();
        //litGenre.Text = videoItem.Vid_Genre;
        //litLanguage.Text = videoItem.Vid_Language;
        hlVideoDownload.NavigateUrl = videoItem.Vid_Pathname;
        //videoItem.Vid_Size;
        //litSubject.Text = videoItem.Vid_Subject;
        //litVideoSummary.Text = videoItem.Vid_Summary;
        litVideoTitle.Text = videoItem.Vid_Title;
        //if(videoItem.Vid_Year != null)
        //    litYear.Text = videoItem.Vid_Year;
            

    }
}
