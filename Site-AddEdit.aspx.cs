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

public partial class Sites_AddEdit : LoggedInPage
{
    public Guid? SiteId
    {
        get
        {
            if (String.IsNullOrEmpty(Request.QueryString["siteid"]))
            {
                return null;
            }
            return new Guid(Request.QueryString["siteid"]);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        MonkData db = new MonkData();
        if (!Page.IsPostBack)
        {
            LoadDate(db);
        }
    }

    private void LoadDate(MonkData db)
    {
        if (SiteId != null)
        {
            Monks.jkp_Site site = db.jkp_Sites.First(p => p.Site_ID == SiteId);
            txtEnglishName.Text = site.Site_EnglishName;
            txtDescription.Text = site.Site_Description;
            
        }
    }

    public void btnSave_Clicked(object sender, EventArgs e)
    {
        MonkData db = new MonkData();
        Monks.jkp_Site site;
        if(SiteId != null)
        {
            site = db.jkp_Sites.First(p=>p.Site_ID == SiteId);
        }
        else
        {
            site = new Monks.jkp_Site();
            site.Site_ID = Guid.NewGuid();
        }

        site.Site_EnglishName = txtEnglishName.Text;
        site.Site_Description = txtDescription.Text;
        
        if(SiteId == null)
            db.jkp_Sites.InsertOnSubmit(site);
        db.SubmitChanges();
        mvAddEdit.ActiveViewIndex = 1;    
    }
}
