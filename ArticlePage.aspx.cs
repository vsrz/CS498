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

public partial class ArticlePage : DefaultPage
{
    public Guid? ArticleId
    {
        get
        {
            if (String.IsNullOrEmpty(Request.QueryString["articleid"]))
                return null;
            return new Guid(Request.QueryString["articleid"]);
        }
    }

    public Guid ArticlePageId
    {
        get
        {
            return new Guid(Request.QueryString["pageid"]);
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
            
            Monks.jkp_ArticlePage artPage = db.jkp_ArticlePages.First(p => p.ArtP_ID == ArticlePageId);
            litContent.Text = artPage.ArtP_DefaultBodyText;
            litPageTitle.Text = GetResource(artPage.ArtP_ResxHeaderName);
            this.Page.Title = GetResource(artPage.ArtP_ResxTitle);

            litCategoryName.Text = artPage.jkp_ArticleCategory.ArtCat_Name;
            rpArticles.DataSource = artPage.jkp_ArticleCategory.jkp_Articles;
            rpArticles.DataBind();
        }

        if(ArticleId != null)
        {
            Monks.jkp_Article article = db.jkp_Articles.First(p=>p.Art_ID == ArticleId);
            litPageTitle.Text = article.Art_Title;
            this.Page.Title = article.Art_Title;
            litContent.Text = article.Art_Content;
            litAuthor.Text = article.jkp_Person.Per_FirstName + " " + article.jkp_Person.Per_LastName;
            spanAuthorName.Visible = true;

        }
    }

    public void rpArticles_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Monks.jkp_Article article = (Monks.jkp_Article)e.Item.DataItem;
            HyperLink hlArticle = (HyperLink)e.Item.FindControl("hlArticle");
            hlArticle.Text = article.Art_LinkTitle;
            hlArticle.NavigateUrl = "ArticlePage.aspx?pageid=" + ArticlePageId.ToString() + "&articleid=" + article.Art_ID.ToString();

        }
    }
}
