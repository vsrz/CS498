using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Retreat_GuestInformation : LoggedInPage
{
    public Guid RetreatId
    {
        get
        {
            return new Guid(Request.QueryString["retreatid"]);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        MonkData db = new MonkData();
        Monks.jkp_Retreat ret = db.jkp_Retreats.First(p=>p.Ret_ID == RetreatId);
        Monks.aspnet_Membership membershipPerson = db.aspnet_Memberships.First(p=>p.UserId == UserId);
        Monks.jkp_PersonAttendingRetreat par = ret.jkp_PersonAttendingRetreats.First(p=>p.PAR_PersonId == membershipPerson.PersonId);


        litAttendeeInformation.Text = ret.Ret_InformationForAttendingRetreat;
        litRetreatName.Text = ret.Ret_Name;
        if(par.jkp_Contribution.Cont_TotalPrice.HasValue)
            litContributionAmount.Text = par.jkp_Contribution.Cont_TotalPrice.Value.ToString("0.00");
        litContributionPaidBy.Text = par.jkp_Contribution.jkp_Person.DisplayName;

        if(par.jkp_Contribution.Cont_TotalPrice.HasValue && par.jkp_Contribution.Cont_AmountPaid.HasValue)
        {
            decimal amountRemainingToBePaid = par.jkp_Contribution.Cont_TotalPrice.Value - par.jkp_Contribution.Cont_AmountPaid.Value;
            if(amountRemainingToBePaid > 0)
            {
                pnlAmountRemainingToBePaid.Visible = true;
                litAmountRemainingToBePaid.Text = amountRemainingToBePaid.ToString("0.00");
            }
        }

        if(par.PAR_RoomId == null)
        {
            litAssignedRoom.Text = "No Room Assigned Yet";

        }
        else
        {
            litAssignedRoom.Text = "Hamlet: " + par.jkp_Room.jkp_Building.jkp_Hamlet.Ham_LocalName + " - Building: " + par.jkp_Room.jkp_Building.Bld_LocalName + " Room: " +  par.jkp_Room.Rm_Name;
        }


        litCostOfSignupForMyself.Text = par.PAR_InititalCostOfSpotAtSignup.ToString("0.00");

    }
}
