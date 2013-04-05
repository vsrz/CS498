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

public partial class Admin_ArticlePages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        MonkData db = new MonkData();
        var articlePages = from a in db.jkp_ArticlePages
                           select a;
        rpArticlePages.DataSource = articlePages;
        rpArticlePages.DataBind();
    }

    public void rpArticlePages_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Monks.jkp_ArticlePage artPage = (Monks.jkp_ArticlePage)e.Item.DataItem;
            Literal litPageTitle = (Literal)e.Item.FindControl("litPageTitle");
            Literal litCategory = (Literal)e.Item.FindControl("litCategory");
            Literal litPageUrl = (Literal)e.Item.FindControl("litPageUrl");
            litPageTitle.Text = artPage.ArtP_ResxTitle;
            litCategory.Text = artPage.jkp_ArticleCategory.ArtCat_Name;
            litPageUrl.Text = "ArticlePage.aspx?pageid=" + artPage.ArtP_ID.ToString();

        }
    }
}
