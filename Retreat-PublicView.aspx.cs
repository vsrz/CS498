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

public partial class Retreat_PublicView : System.Web.UI.Page
{
    public Guid RetreatId
    {
        get { return new Guid(Request.QueryString["retreatid"]); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        MonkData db = new MonkData();
        Monks.jkp_Retreat retreat = db.jkp_Retreats.First(p => p.Ret_ID == RetreatId);
        litRetreatName.Text = retreat.Ret_Name;
        litDescription.Text = retreat.Ret_Description;

        if (retreat.Ret_StartDate != null)
            litStartDate.Text = ((DateTime)retreat.Ret_StartDate).ToShortDateString();
        if (retreat.Ret_EndDate != null)
            litEndDate.Text = ((DateTime)retreat.Ret_EndDate).ToShortDateString();

        if (retreat.jkp_Site != null)
        {
            if (!String.IsNullOrEmpty(retreat.jkp_Site.Site_LocalName))
                litSite.Text = retreat.jkp_Site.Site_LocalName;
            else if (!String.IsNullOrEmpty(retreat.jkp_Site.Site_EnglishName))
                litSite.Text = retreat.jkp_Site.Site_EnglishName;

            if (retreat.jkp_Site.jkp_Address != null)
                litAddress.Text = retreat.jkp_Site.jkp_Address.Add_Street + "<br />" + retreat.jkp_Site.jkp_Address.Add_City + ", " + retreat.jkp_Site.jkp_Address.Add_State + " " + retreat.jkp_Site.jkp_Address.Add_PostalCode;

        }

        litRemainingSpots.Text = (retreat.Ret_TotalCapacity - retreat.jkp_PersonAttendingRetreats.Count()).ToString();
        //var roomsForRetreat = from r in db.jkp_Rooms
        //                      where r.Rm_ContructedDate <= retreat.Ret_StartDate && r.Rm_DestroyedDate >= retreat.Ret_EndDate
        //                      select r;

        //if (roomsForRetreat.Count() > 0)
        //{
            
        //    //litRemainingSpots.Text = (capacity - retreat.jkp_PersonAttendingRetreats.Count()).ToString();
        //}
        //else
        //{
        //    litRemainingSpots.Text = "No more spots left";
        //}


        litLanguage.Text = retreat.jkp_Language.Lang_DisplayName;

        if( retreat.Ret_EndDate <= DateTime.Now)
        {
            hlRegisterForReteat.Visible = false;
                        
        }
        else if(retreat.Ret_StartDate <= DateTime.Now && retreat.Ret_AllowRegistrationDuringRetreat)
        {
            hlRegisterForReteat.Visible = true;
        }
        else if(retreat.Ret_StartDate <= DateTime.Now && !retreat.Ret_AllowRegistrationDuringRetreat)
        {
            hlRegisterForReteat.Visible = false;
        }
        else if(retreat.Ret_StartDate > DateTime.Now)
        {
            hlRegisterForReteat.Visible = true;
        }

        
    }

    public void RegisterButtonClicked(object sender, EventArgs e)
    {
        Response.Redirect("Retreat-Registration.aspx?retreatid=" + RetreatId);
    }
}
