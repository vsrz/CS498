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

public partial class VisitingUs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        MonkData db = new MonkData();
        System.Linq.IQueryable<System.Linq.IGrouping<string, Monks.jkp_Site>> sites = from s in db.jkp_Sites
                                                                                      orderby s.Site_EnglishName
                                                                                      group s by s.jkp_Address.Add_Country into g
                                                                                      select g;
        rpCountriesOfSites.DataSource = sites;
        rpCountriesOfSites.DataBind();
    }

    public void rpCountriesOfSites_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            IGrouping<string, Monks.jkp_Site> groupOfSites = (IGrouping<string, Monks.jkp_Site>)e.Item.DataItem;
            Repeater rpSites = (Repeater)e.Item.FindControl("rpSites");
            Literal litCountryName = (Literal)e.Item.FindControl("litCountryName");

            if (groupOfSites.Where(p => p.jkp_Address != null).Count() != 0)
                litCountryName.Text = groupOfSites.First(p => p.jkp_Address != null).jkp_Address.Add_Country;

            rpSites.DataSource = groupOfSites;
            rpSites.DataBind();
        }
    }

    public void rpSites_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {


            Monks.jkp_Site siteToBind = (Monks.jkp_Site)e.Item.DataItem;
            HyperLink hlSiteListName = (HyperLink)e.Item.FindControl("hlSiteListName");
            hlSiteListName.Text = siteToBind.GetName;
            hlSiteListName.NavigateUrl = "Site-PublicDetails.aspx?siteid=" + siteToBind.Site_ID.ToString();
        }
    }
}
