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

public partial class Admin_GivePersonRoles : LoggedInPage
{
    public Guid PageUserId
    {
        get { return new Guid(Request.QueryString["userid"]); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        
        if(!Page.IsPostBack)
        {
            LoadPageData();

        }
        
        
    }

    public void LoadPageData()
    {
        MonkData db = new MonkData();
        
        Monks.jkp_Person person = db.aspnet_Memberships.First(p=>p.UserId == PageUserId).jkp_Person;
        litPersonName.Text = person.Per_FirstName + " " + person.Per_LastName;

        rpPersonRoles.DataSource = db.aspnet_UsersInRoles.Where(p=>p.UserId == PageUserId);
        rpPersonRoles.DataBind();

        dlRolesToAssign.DataSource = db.aspnet_Roles;
        dlRolesToAssign.DataTextField = "RoleName";
        dlRolesToAssign.DataValueField = "RoleId";
        dlRolesToAssign.DataBind();
    }

    public void rpPersonRoles_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Monks.aspnet_UsersInRole userInRole = (Monks.aspnet_UsersInRole)e.Item.DataItem;
            Literal litRoleName = (Literal)e.Item.FindControl("litRoleName");
            LinkButton lbtnRemoveRole = (LinkButton)e.Item.FindControl("lbtnRemoveRole");
            litRoleName.Text = userInRole.aspnet_Role.RoleName;
            lbtnRemoveRole.Attributes.Add("roleid", userInRole.RoleId.ToString());

        }
    }

    public void btnAddRole_Cancel(object sender, EventArgs e)
    {
        
        modalAddRole.Hide();
        
    }

    public void btnAddRole_Clicked(object sender, EventArgs e)
    {
        modalAddRole.Hide();
        MonkData db = new MonkData();
        Monks.aspnet_UsersInRole userInRole = new Monks.aspnet_UsersInRole();
        userInRole.RoleId = new Guid(dlRolesToAssign.SelectedValue);
        userInRole.UserId = PageUserId;

        var usersInRolesSame = from u in db.aspnet_UsersInRoles
                               where u.UserId == PageUserId && u.RoleId == userInRole.RoleId
                               select u;
        if(usersInRolesSame.Count() > 0)
            return;
        db.aspnet_UsersInRoles.InsertOnSubmit(userInRole);
        db.SubmitChanges();

        LoadPageData();
    }

    public void lbtnRemoveRole_Clicked(object sender, EventArgs e)
    {
        MonkData db = new MonkData();
        LinkButton lbtnRemoveRole = (LinkButton)sender;
        Guid roleId = new Guid( lbtnRemoveRole.Attributes["roleid"]);
        Monks.aspnet_UsersInRole userInRole = db.aspnet_UsersInRoles.First(p=>p.RoleId == roleId && p.UserId == PageUserId);
        db.aspnet_UsersInRoles.DeleteOnSubmit(userInRole);
        db.SubmitChanges();

        LoadPageData();
    }
}
