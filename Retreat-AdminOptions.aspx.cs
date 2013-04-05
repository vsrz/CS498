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
using System.Collections.Generic;

public partial class Retreat_AdminOptions : System.Web.UI.Page
{
    public Guid RetreatId
    {
        get { return new Guid(Request.QueryString["retreatid"]); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        hlActAddEditRooms.NavigateUrl = "Retreat-AddEditRooms.aspx?retreatid=" + RetreatId.ToString();
        hlActAssignPeople.NavigateUrl = "Retreat-AssignRooms.aspx?retreatid=" + RetreatId.ToString();
        hlActViewRetreatants.NavigateUrl = "Retreat-ViewRetreatants.aspx?retreatid=" + RetreatId.ToString();
        hlActEditRetreatDetals.NavigateUrl = "AddEdit.aspx?typename=jkp_Retreat&itemid=" + RetreatId.ToString();
        hlActAssignments.NavigateUrl = "Retreat-Assignments.aspx?retreatid=" + RetreatId.ToString();
        hlActActivites.NavigateUrl = "Retreat-Activities.aspx?retreatid=" + RetreatId.ToString();
        hlActBuilder.NavigateUrl = "Retreat-Builder.aspx?retreatid=" + RetreatId.ToString();
        hlActDiscussionGroups.NavigateUrl = "Retreat-DiscussionGroups.aspx?retreatid=" + RetreatId.ToString();
        hlContributions.NavigateUrl = "Retreat-Contributions.aspx?retreatid=" + RetreatId.ToString();

        MonkData db = new MonkData();
        Monks.jkp_Retreat retreat = db.jkp_Retreats.First(p=>p.Ret_ID == RetreatId);
        litRetreatName.Text = retreat.Ret_Name;





    }
}
