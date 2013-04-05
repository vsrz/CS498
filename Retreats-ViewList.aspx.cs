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

public partial class Retreats_ViewList : LoggedInPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Add("CurrentDateTime", DateTime.Now);
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        MonkData db = new MonkData();

        var person = db.aspnet_Memberships.First(p => p.UserId == UserId);


        var peopleWhoAttendedWithMe = from d in db.jkp_PersonAttendingRetreats
                                      where d.PAR_PersonId != person.PersonId && d.jkp_Contribution.Cont_Off_Per_ID == person.PersonId
                                      group d by d.PAR_PersonId into g
                                      select g;

        rpAttendedRetreatWith.DataSource = peopleWhoAttendedWithMe;
        rpAttendedRetreatWith.DataBind();

        var personPAR = from q in db.jkp_PersonAttendingRetreats
                        where q.PAR_PersonId == person.PersonId
                        select q;

        rpRetreatsAttended.DataSource = personPAR;
        rpRetreatsAttended.DataBind();

    }

    public void rpAttendedRetreatWith_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            IGrouping<Guid, Monks.jkp_PersonAttendingRetreat> peopleAttendingRetreat = (IGrouping<Guid, Monks.jkp_PersonAttendingRetreat>)e.Item.DataItem;
            Monks.jkp_PersonAttendingRetreat par = peopleAttendingRetreat.First();
            Literal litPersonAttendingRetreat = (Literal)e.Item.FindControl("litPersonAttendingRetreat");
            Literal litRetreatName = (Literal)e.Item.FindControl("litRetreatName");

            litPersonAttendingRetreat.Text = par.jkp_Person.Per_FirstName + " " + par.jkp_Person.Per_LastName;
            litRetreatName.Text = par.jkp_Retreat.Ret_Name;
        }
    }

    public void rpRetreatsAttended_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Monks.jkp_PersonAttendingRetreat par = (Monks.jkp_PersonAttendingRetreat)e.Item.DataItem;
            HyperLink linkRetreatName = (HyperLink)e.Item.FindControl("linkRetreatName");
            HyperLink hlSignupInformation = (HyperLink)e.Item.FindControl("hlSignupInformation");
            linkRetreatName.Text = par.jkp_Retreat.Ret_Name;
            linkRetreatName.NavigateUrl = "Retreat-PublicView.aspx?retreatid=" + par.jkp_Retreat.Ret_ID.ToString();
            hlSignupInformation.NavigateUrl = "Retreat-GuestInformation.aspx?retreatid=" + par.PAR_RetId.ToString();
        }
    }
}
