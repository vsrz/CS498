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

public partial class MasterPage : System.Web.UI.MasterPage
{

    public string GetResource(string resourceName)
    {
        string virtualPath = this.Page.Request.Path;
        try
        {
            return (string)this.GetLocalResourceObject( resourceName.Replace(" ", ""));
        }
        catch
        {
            return "";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!Page.IsPostBack)
        {
            //this code block dynamically generates the top navigation from the database
            MonkData db = new MonkData();

            //separate 'Home' link from the list of menu items
            var homeLink = from u1 in db.aspnet_MenuItems
                           where u1.LinkText == "Home"
                           select u1;
            //get the rest of the menu items
            var menuItemList = from u2 in db.aspnet_MenuItems
                               where u2.LinkText != "Home"
                               orderby u2.LinkText
                               select u2;

            topNavul.Controls.Clear();

            //want 'Home' link first
            topNavul.InnerHtml += "<li><a href=\"" + homeLink.First().LinkLocation + "\">"
                                        + GetResource(homeLink.First().LinkText) + "</a></li>";

            //then append the rest of the menu items
            foreach (Monks.aspnet_MenuItem menuItemElement in menuItemList)
            {
                string textForLink = GetResource(menuItemElement.LinkText);
                topNavul.InnerHtml += "<li><a href=\"" + menuItemElement.LinkLocation + "\">"
                                        + textForLink + "</a></li>";
            }
        }

        topNavul.Controls.Add(lgv);
    }
}
