using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Admin_LoginAsUser : LoggedInPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthentication.SetAuthCookie(Request.QueryString["username"], false);
        Response.Redirect("Account-Home.aspx");
    }
}
