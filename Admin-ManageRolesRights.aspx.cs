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
using System.IO;
using System.Reflection;
using System.Data.Linq;

public partial class Admin_ManageRolesRights : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (!Page.IsPostBack)
        {
            LoadPageData();
        }
    }

    public void btnSaveNewRole_Clicked(object sender, EventArgs e)
    {
        MonkData db = new MonkData();
        Monks.aspnet_Role role = new Monks.aspnet_Role();

        var app = (from u in db.aspnet_Applications
                   where u.ApplicationName == "DeerPark"
                   select u).Single();
        role.ApplicationId = app.ApplicationId;
        role.Description = txtDescription.Text;
        role.LoweredRoleName = txtRoleName.Text.ToLower();
        role.RoleName = txtRoleName.Text;
        role.RoleId = Guid.NewGuid();
        db.aspnet_Roles.InsertOnSubmit(role);
        db.SubmitChanges();

        txtRoleName.Text = "";
        txtDescription.Text = "";

        LoadPageData();


    }

    public void rpRoles_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Monks.aspnet_Role role = (Monks.aspnet_Role)e.Item.DataItem;
            LinkButton lbtnRole = (LinkButton)e.Item.FindControl("lbtnRole");
            LinkButton lbtnDeleteRole = (LinkButton)e.Item.FindControl("lbtnDeleteRole");
            lbtnRole.Text = role.RoleName;
            lbtnRole.Attributes.Add("roleid", role.RoleId.ToString());
            lbtnDeleteRole.Attributes.Add("roleid", role.RoleId.ToString());

        }
    }



    public void LoadPageData()
    {
        MonkData db = new MonkData();
        // Force it to refresh it's list of items in it's list.
        var roles = db.aspnet_Roles;
        rpRoles.DataSource = roles;
        rpRoles.DataBind();

        fieldObjectPermissions.Visible = false;
        fieldPagePermissions.Visible = false;
        fieldRoleDetails.Visible = false;
        pnlCreatePageRole.Visible = false;
        pnlTablePermission.Visible = false;
        
    }

    public void lbtnEditRoleAssignments_clicked(object sender, EventArgs e)
    {
        
        LinkButton lbtnRole = (LinkButton)sender;
        Guid roleId = new Guid(lbtnRole.Attributes["roleid"]);
        DisplayRoleDetails(roleId);

        
    }

    private void DisplayRoleDetails(Guid roleId)
    {
        MonkData db = new MonkData();
        Monks.aspnet_Role role = db.aspnet_Roles.First(p => p.RoleId == roleId);
        litRoleName.Text = role.RoleName;
        hidRoleId.Value = role.RoleId.ToString();


        fieldObjectPermissions.Visible = true;
        fieldPagePermissions.Visible = true;
        fieldRoleDetails.Visible = true;



        DirectoryInfo dInfo = new DirectoryInfo(Server.MapPath(""));
        FileInfo[] pages = dInfo.GetFiles("*.aspx");

        dlPages.DataSource = pages;
        dlPages.DataTextField = "Name";
        dlPages.DataBind();

        var tableNames = db.GetType().GetProperties().Where(p => p.PropertyType.Name.Contains("Table"));
        dlTables.DataSource = tableNames;
        dlTables.DataTextField = "Name";
        dlTables.DataBind();

        pnlCreatePageRole.Visible = true;
        pnlTablePermission.Visible = true;


        var pagePermissions = db.aspnet_PageUnderRoles.Where(p => p.RoleId == roleId).OrderBy(p=>p.FilePath);
        rpPagePermissions.DataSource = pagePermissions;
        rpPagePermissions.DataBind();

        var permissionsOnObjects = db.aspnet_TableUnderRoles.Where(p => p.RoleId == roleId).OrderBy(p=>p.TableName);
        rpTablePermissions.DataSource = permissionsOnObjects;
        rpTablePermissions.DataBind();
    }

    public void btnSavePageAccessRole_Clicked(object sender, EventArgs e)
    {
        MonkData db = new MonkData();
        Guid roleId = new Guid(hidRoleId.Value);
        var pagesUnderRole = db.aspnet_PageUnderRoles.Where(p=>p.RoleId == roleId && p.FilePath == dlPages.SelectedValue);
        if(pagesUnderRole.Count() > 0)
        {
            // Role was already found that was defined for this page.
            return;
        }

        Monks.aspnet_PageUnderRole pageUnderRole = new Monks.aspnet_PageUnderRole();
        pageUnderRole.FilePath = dlPages.SelectedValue.Trim();
        pageUnderRole.PageId = Guid.NewGuid();
        pageUnderRole.RoleId = roleId;

        db.aspnet_PageUnderRoles.InsertOnSubmit(pageUnderRole);
        db.SubmitChanges();

        var pagePermissions = db.aspnet_PageUnderRoles.Where(p=>p.RoleId == roleId);
        rpPagePermissions.DataSource = pagePermissions;
        rpPagePermissions.DataBind();
        

        var tablePermissions = db.aspnet_TableUnderRoles.Where(p=>p.RoleId == roleId);
        rpTablePermissions.DataSource = tablePermissions;
        rpTablePermissions.DataBind();
        
    }

    public void LoadRoles(MonkData db)
    {
        Guid roleId = new Guid(hidRoleId.Value);
        var pagePermissions = db.aspnet_PageUnderRoles.Where(p=>p.RoleId == roleId);
        rpPagePermissions.DataSource = pagePermissions;
        rpPagePermissions.DataBind();
        

        var tablePermissions = db.aspnet_TableUnderRoles.Where(p=>p.RoleId == roleId);
        rpTablePermissions.DataSource = tablePermissions;
        rpTablePermissions.DataBind();
    }

    public void lbtnDeletePageRole_Clicked(object sender, EventArgs e)
    {
        LinkButton lbtnDeletePageRole = (LinkButton)sender;
        Guid pageRoleId = new Guid(lbtnDeletePageRole.Attributes["pageroleid"]);
        MonkData db = new MonkData();
        Monks.aspnet_PageUnderRole pageUnderRole = db.aspnet_PageUnderRoles.First(p=>p.PageId == pageRoleId);
        db.aspnet_PageUnderRoles.DeleteOnSubmit(pageUnderRole);
        db.SubmitChanges();
        LoadRoles(db);
    }

    public void rpPageRoles_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Monks.aspnet_PageUnderRole pageUnderRole = (Monks.aspnet_PageUnderRole)e.Item.DataItem;
            Literal litPageTitle = (Literal)e.Item.FindControl("litPageTitle");
            LinkButton lbtnDeletePageRole = (LinkButton)e.Item.FindControl("lbtnDeletePageRole");
            litPageTitle.Text = pageUnderRole.FilePath;
            lbtnDeletePageRole.Attributes.Add("pageroleid", pageUnderRole.PageId.ToString());
        }
    }

    public void rpTablePermissions_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Monks.aspnet_TableUnderRole tableUnderRole = (Monks.aspnet_TableUnderRole)e.Item.DataItem;
            Literal litTableName = (Literal)e.Item.FindControl("litTableName");
            LinkButton lbtnDeleteTableRole = (LinkButton)e.Item.FindControl("lbtnDeleteTableRole");
            litTableName.Text = tableUnderRole.TableName;
            lbtnDeleteTableRole.Attributes.Add("tableroleid", tableUnderRole.TableUnderRoleId.ToString());            
        }
    }

    public void lbtnDeleteTableRole_Clicked(object sender, EventArgs e)
    {
        LinkButton lbtnDeleteTableRole = (LinkButton)sender;
        Guid tableUnderRoleId = new Guid(lbtnDeleteTableRole.Attributes["tableroleid"]);
        MonkData db = new MonkData();
        Monks.aspnet_TableUnderRole tableUnderRole = db.aspnet_TableUnderRoles.First(p=>p.TableUnderRoleId == tableUnderRoleId);
        db.aspnet_TableUnderRoles.DeleteOnSubmit(tableUnderRole);
        db.SubmitChanges();
        LoadRoles(db);
    }

    public void btnSaveTablePermission_Clicked(object sender, EventArgs e)
    {
        MonkData db = new MonkData();
        Guid roleId = new Guid(hidRoleId.Value);
        var tablesWithSameName = from t in db.aspnet_TableUnderRoles
                                 where t.TableName == dlTables.SelectedValue && t.RoleId == roleId
                                 select t;
        if(tablesWithSameName.Count() > 0)
            return; // Found a table role assignment that was alredy created.
        Monks.aspnet_TableUnderRole tableUnderRole = new Monks.aspnet_TableUnderRole();
        tableUnderRole.RoleId = roleId;
        tableUnderRole.TableName = dlTables.SelectedValue;
        tableUnderRole.TableUnderRoleId = Guid.NewGuid();
        db.aspnet_TableUnderRoles.InsertOnSubmit(tableUnderRole);
        db.SubmitChanges();

        LoadRoles(db);
        
    }

    public void lbtnDeleteRole_Clicked(object sender, EventArgs e)
    {
        LinkButton lbtnDeleteRole = (LinkButton)sender;
        Guid roleId = new Guid(lbtnDeleteRole.Attributes["roleid"]);
        MonkData db = new MonkData();
        Monks.aspnet_Role role = db.aspnet_Roles.First(p=>p.RoleId == roleId);
        db.aspnet_Roles.DeleteOnSubmit(role);
        
        db.SubmitChanges();
        LoadPageData();
    }
}
