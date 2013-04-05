using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class ControlPanel_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        MonkData db = new MonkData();
        tdAdminLink.Visible = CanUserAccessPage("Admin-Home.aspx", false, ref db);
    }

    /// <summary>
    /// Gets the UserId for the currently logged in user. Only call if the user is logged in.
    /// </summary>
    protected Guid UserId
    {
        get
        {
            if (!this.Page.User.Identity.IsAuthenticated)
                throw new Exception("Only call the UserId when the user is logged in.");
            return GetUserIdByName(this.Page.User.Identity.Name);

        }
    }

    

    /// <summary>
    /// Gets the cached UserId
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Guid GetUserIdByName(string name)
    {

        object cacheItem = Cache.Get("username_" + name);

        if (cacheItem != null)
        {
            return (Guid)cacheItem;
        }
        else
        {
            Guid userId = (Guid)(Membership.GetUser(name).ProviderUserKey);
            Cache.Add("username_" + name, userId, null, DateTime.Now.AddDays(1), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            return userId;
        }

    }

    public bool CanUserAccessPage(string pagePath, bool redirectOnNoAccess, ref MonkData db )
    {
        

        var rolesForPage = from r in db.aspnet_PageUnderRoles
                           where r.FilePath == pagePath
                           select r;
        // We know there is a role applied to this page if there are more than 0 roles and we only want to check if roles are enabled.
        if (rolesForPage.Count() > 0 && bool.Parse(ConfigurationManager.AppSettings["EnableRoles"]))
        {

            var userInRoles = from p in db.aspnet_UsersInRoles
                              where p.UserId ==  UserId && p.aspnet_Role.aspnet_PageUnderRoles.Where(j => j.FilePath == pagePath).Count() > 0
                              select p;
            // If it's equal to zero the user doesn't have permission to access this page but we want to possibly
            // redirect them to a page telling them why they can't access it. 
            if (userInRoles.Count() == 0)
            {
                
                string rolesNeedToAccessPage = string.Empty;
                foreach (Monks.aspnet_PageUnderRole pageUnderRole in rolesForPage)
                {
                    if (!String.IsNullOrEmpty(rolesNeedToAccessPage))
                        rolesNeedToAccessPage += ", ";
                    rolesNeedToAccessPage += pageUnderRole.aspnet_Role.RoleName;
                }

                if(redirectOnNoAccess)
                    Response.Redirect("Access-Denied.aspx?reason=" + Uri.EscapeDataString(rolesNeedToAccessPage), true);

                return false;
            }
        }
        return true;
    }
}
