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
using System.Reflection;
using System.Xml;

public partial class Admin_Home : LoggedInPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XmlNode xNode = GetDatabaseTable("jkp_Retreat");
        
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        

        MonkData db = new MonkData();

        // Master Admin Page
        fieldsetViewAllTables.Visible = CanUserAccessPage("Admin-AllTables.aspx", false, ref db);

        // Retreats
        bool createNewRetreat = CanUserAccessTable("jkp_Retreat", false, ref db);
        bool manageARetreat = CanUserAccessTable("jkp_Retreat", false, ref db);

        liAddRetreat.Visible = createNewRetreat;
        liViewRetreats.Visible = manageARetreat;

        if(createNewRetreat == false && manageARetreat == false)
        {
            fieldsetRetreats.Visible = false;
        }

       
        // Site and Rooms
        bool siteAdmin = CanUserAccessTable("jkp_Site", false, ref db);
        bool hamletAdmin = CanUserAccessTable("jkp_Hamlet", false, ref db);
        bool buildingAdmin = CanUserAccessTable("jkp_Building", false, ref db);
        bool roomAdmin = CanUserAccessTable("jkp_Room", false, ref db);
        bool roomTypeAdmin = CanUserAccessTable("jkp_RoomType", false, ref db);
        bool roomRateAdmin = CanUserAccessTable("jkp_RoomRate", false, ref db);

        liSiteAdmin.Visible = siteAdmin;
        liHamletAdmin.Visible = hamletAdmin;
        liBuildingAdmin.Visible = buildingAdmin;
        liRoomsAdmin.Visible = roomAdmin;
        liRoomTypeAdmin.Visible = roomTypeAdmin;
        liRateAdmin.Visible = roomRateAdmin;

        if(!siteAdmin && !hamletAdmin && !buildingAdmin && !roomAdmin && !roomTypeAdmin && !roomRateAdmin)
        {
            fieldsetLocationAdmin.Visible = false;
        }


        // User Administration
        bool manageRoleRights = CanUserAccessPage("Admin-ManageRolesRights.aspx", false, ref db);
        bool viewEditUsers = CanUserAccessTable("aspnet_Membership", false, ref db);

        litManageRoleRights.Visible = manageRoleRights;
        litViewUsers.Visible = viewEditUsers;

        if(!manageRoleRights && !viewEditUsers)
        {
            fieldsetUserAdmin.Visible = false;
        }

        // Reporting
        bool emailAddressReport = CanUserAccessPage("Report-EmailAddresses.aspx", false, ref db);
        
        lisReportEmailAddresses.Visible = emailAddressReport;
        
        if(!emailAddressReport)
        {
            fieldsetReports.Visible = false;
        }

        // Articles
        bool articlePageView = CanUserAccessPage("Admin-ArticlePages.aspx", false, ref db);
        bool ariticleAdmin = CanUserAccessTable("jkp_Article", false, ref db);
        bool aricleCategoryAdmin = CanUserAccessTable("jkp_ArticleCategory", false, ref db);
        bool addNewArticleAdmin = CanUserAccessTable("jkp_Article", false, ref db);        
        bool articlePageAdmin = CanUserAccessTable("jkp_ArticlePage", false, ref db);
        
        liArticlePagesListing.Visible = articlePageView;
        liArticles.Visible = articlePageAdmin;
        liArticleCategories.Visible = aricleCategoryAdmin;
        liArticleAdd.Visible = addNewArticleAdmin;
        liArticlePagesAdd.Visible = articlePageAdmin;

        if(!articlePageView && !articlePageAdmin && !aricleCategoryAdmin && !addNewArticleAdmin && !articlePageAdmin)
        {
            fieldsetArticles.Visible = false;
        }



        //  Media
        bool bookAdmin = CanUserAccessTable("jkp_Book", false, ref db);
        bool audioAdmin = CanUserAccessTable("jkp_Audio", false, ref db);
        bool videoAdmin = CanUserAccessTable("jkp_Video", false, ref db);
        bool imageAdmin = CanUserAccessTable("jkp_Image", false, ref db);

        liBooks.Visible = bookAdmin;
        liAudio.Visible = audioAdmin;
        liVideos.Visible = videoAdmin;
        liImages.Visible = imageAdmin;



        
    }

    
}
