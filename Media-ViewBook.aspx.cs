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

public partial class Media_ViewBook : System.Web.UI.Page
{
    public Guid BookId
    {
        get { return new Guid(Request.QueryString["bookid"]); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        MonkData db = new MonkData();
        Monks.jkp_Book book = db.jkp_Books.First(p=>p.Bk_ID == BookId);
        litTitle.Text = book.Bk_Title;
    }
}
