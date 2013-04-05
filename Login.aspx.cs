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

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Login_Success(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["returnUrl"]))
        {
            Response.Redirect(Page.ResolveUrl(Uri.UnescapeDataString(Request.QueryString["returnUrl"])));
        }
        else
        {
            Response.Redirect(Page.ResolveUrl("ControlPanel.aspx"));
        }
    }
}
