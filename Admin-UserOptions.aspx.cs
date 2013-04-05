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

public partial class Admin_UserOptions : LoggedInPage
{
    public Guid UserId
    {
        get { return new Guid(Request.QueryString["userid"]); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        hlEditMembership.NavigateUrl = "AddEdit.aspx?typename=aspnet_Membership&itemid=" + UserId.ToString();
        hlEditUserDetails.NavigateUrl = "AddEdit.aspx?typename=aspnet_User&itemid=" + UserId.ToString();
        hlEditRoles.NavigateUrl = "Admin-GivePersonRoles.aspx?userid=" + UserId.ToString();
        
        MonkData db = new MonkData();
        Monks.jkp_Person person = db.aspnet_Memberships.First(p=>p.UserId == UserId).jkp_Person;
        litUserName.Text = person.Per_FirstName + " " + person.Per_LastName;

        hlLoginAsUser.NavigateUrl = "Admin-LoginAsUser.aspx?username=" + person.aspnet_Memberships.First().aspnet_User.UserName;
    }
}
