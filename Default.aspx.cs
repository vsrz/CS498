using System;
using System.Collections;
using System.Collections.Generic;
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

public partial class _Default : DefaultPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        image1.ImageUrl = Random_Image(); //set image filepath to a random image
        dateTime.Value = System.DateTime.Today.ToString();
        if(!Page.IsPostBack)
        {
            MonkData db = new MonkData();
            var languages = from l in db.jkp_Languages
                            select l;
            dlLanguages.DataSource = languages;
            dlLanguages.DataTextField = "Lang_DisplayName";
            dlLanguages.DataValueField = "Lang_BrowserCode";
            dlLanguages.DataBind();

            // culture codes http://msdn.microsoft.com/en-us/library/system.globalization.cultureinfo.aspx
            if(Session["Culture"] != null)
            {
                 dlLanguages.Items.FindByValue(Session["Culture"].ToString()).Selected = true;
            }

            var retreats = from r in db.jkp_Retreats
                           //where r.Ret_EndDate > DateTime.Now
                           orderby r.Ret_StartDate ascending
                           select r;
            rpRetreats.DataSource = retreats.Take(10);
            rpRetreats.DataBind();

            

        }
    }

    public void dlLanguages_Picked(object sender, EventArgs e)
    {
        string selectedLanguage = dlLanguages.SelectedValue;
        Page.Culture = selectedLanguage;
        
        Session.Remove("Culture");
        Session.Add("Culture", selectedLanguage);
        Server.Transfer("Default.aspx", false);
        if(Request.Cookies["language"] != null)
        Request.Cookies.Add(new HttpCookie("language", selectedLanguage));
    }

    protected string Random_Image()
    {

        //Monks.jkp_RoomTypeRate roomTypeRate = new Monks.jkp_RoomTypeRate();
        //roomTypeRate.jkp_RoomType.RmType_Name
        /*
         * all images in /random are labeled sequentially
         * like 'image1.jpg', 'image2.jpg'. randomer generates 
         * a random int and appends it to the filepath and returns
        */

        string dir = "dpimages/random/image";
        Random randomer = new Random();
        int randNum = randomer.Next(1, 5);
        return dir += randNum + ".jpg";            
    }
}
