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

public partial class Site_PublicDetails : System.Web.UI.Page
{
    public Guid SiteId
    {
        get { return new Guid(Request.QueryString["siteid"]); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        MonkData db = new MonkData();
        Monks.jkp_Site site = db.jkp_Sites.First(p=>p.Site_ID == SiteId);
        litSiteName.Text = site.GetName;
        litTitleSiteName.Text = site.GetName;
        
        if(!String.IsNullOrEmpty(site.Site_Website))
        {
            pnlSiteName.Visible = true;
            hlWebsite.NavigateUrl = site.Site_Website;
        }

        if(site.jkp_Address != null)
        {
            litAddress.Text = site.jkp_Address.Add_Street + " <br />" + 
                site.jkp_Address.Add_City + ", " + site.jkp_Address.Add_State + " " + site.jkp_Address.Add_PostalCode + "<br />" + site.jkp_Address.Add_Country;
        }

        litDescription.Text = site.Site_Description;

    }
}
