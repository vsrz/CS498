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

public partial class Retreat_Builder : System.Web.UI.Page
{
    public Guid RetreatId
    {
        get { return new Guid(Request.QueryString["retreatid"]); }
    }

    MonkData db = new MonkData();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!Page.IsPostBack)
        {

            var sites = db.jkp_Sites;
            dlSites.DataSource = sites;
            dlSites.DataTextField = "Site_EnglishName";
            dlSites.DataValueField = "Site_ID";
            dlSites.DataBind();

            LoadRetreatsFromSelectedSite();

            var retreat = db.jkp_Retreats.First(p => p.Ret_ID == RetreatId);
            litRetreatName.Text = retreat.Ret_Name;
            if (retreat.jkp_Site != null)
                if (!string.IsNullOrEmpty(retreat.jkp_Site.Site_LocalName))
                    litSite.Text = retreat.jkp_Site.Site_LocalName;
                else
                    litSite.Text = retreat.jkp_Site.Site_EnglishName;
            //litHamlets.Text = retreat.jkp_Site.jkp_Hamlets.Count().ToString();
            //litBuildings.Text = retreat.jkp_Site.jkp_Hamlets.Sum(p => p.jkp_Buildings.Count).ToString();
            //litRooms.Text = retreat.jkp_Site.jkp_Hamlets.Sum(p => p.jkp_Buildings.Sum(j => j.jkp_Rooms.Count())).ToString();
            litAssingments.Text = retreat.jkp_Assignments.Count().ToString();
            litAcitivities.Text = retreat.jkp_Activities.Count().ToString();
        }
    }

    public void btnGetRetreats_Clicked(object sender, EventArgs e)
    {
        LoadRetreatsFromSelectedSite();

    }

    public void LoadRetreatsFromSelectedSite()
    {
        if (dlSites.Items.Count == 0)
            return;


        Guid siteId = new Guid(dlSites.SelectedValue);
        var retreatsFromSite = db.jkp_Retreats.Where(p => p.Ret_Site_ID == siteId && p.Ret_ID != RetreatId);
        rpRetreats.DataSource = retreatsFromSite;
        rpRetreats.DataBind();
    }

    public void rpRetreats_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Monks.jkp_Retreat retreat = (Monks.jkp_Retreat)e.Item.DataItem;
            Literal litRetName = (Literal)e.Item.FindControl("litRetName");
            //Literal litRetHamletsCount = (Literal)e.Item.FindControl("litRetHamletsCount");
            //Literal litRetRoomsCount = (Literal)e.Item.FindControl("litRetRoomsCount");
            Literal litAssignmentTypes = (Literal)e.Item.FindControl("litAssignmentTypes");
            Literal litActivityTypes = (Literal)e.Item.FindControl("litActivityTypes");
            LinkButton lbtnCopyRooms = (LinkButton)e.Item.FindControl("lbtnCopyRooms");
            LinkButton lbtnCopyAssignments = (LinkButton)e.Item.FindControl("lbtnCopyAssignments");
            LinkButton lbtnCopyActivities = (LinkButton)e.Item.FindControl("lbtnCopyActivities");

            litRetName.Text = retreat.Ret_Name;
            //litRetHamletsCount.Text = retreat.jkp_Site.jkp_Hamlets.Count().ToString();
            //litRetRoomsCount.Text = retreat.jkp_Site.jkp_Hamlets.Sum(p => p.jkp_Buildings.Sum(j => j.jkp_Rooms.Count)).ToString();
            litAssignmentTypes.Text = retreat.jkp_Assignments.Count().ToString();
            litActivityTypes.Text = retreat.jkp_Activities.Count().ToString();

            //lbtnCopyRooms.Attributes.Add("retreatid", retreat.Ret_ID.ToString());
            lbtnCopyActivities.Attributes.Add("retreatid", retreat.Ret_ID.ToString());
            lbtnCopyAssignments.Attributes.Add("retreatid", retreat.Ret_ID.ToString());

        }
    }

    public void lbtnCopyRooms_Clicked(object sender, EventArgs e)
    {
        LinkButton lbtnCopyRooms = (LinkButton)sender;
        Guid retreatId = new Guid(lbtnCopyRooms.Attributes["retreatid"]);
        Monks.jkp_Retreat retreat = db.jkp_Retreats.First(p => p.Ret_ID == retreatId);

        List<Monks.jkp_Hamlet> newHamlets = new List<Monks.jkp_Hamlet>();
        List<Monks.jkp_Building> newBuildings = new List<Monks.jkp_Building>();
        List<Monks.jkp_Room> newRooms = new List<Monks.jkp_Room>();
        List<Monks.jkp_RoomRate> newRoomRates = new List<Monks.jkp_RoomRate>();
        foreach (Monks.jkp_Hamlet hamletInRetreat in retreat.jkp_Site.jkp_Hamlets)
        {
            Monks.jkp_Hamlet newHamlet = new Monks.jkp_Hamlet();
            newHamlet.Ham_Add_ID = hamletInRetreat.Ham_Add_ID;
            newHamlet.Ham_Comment = hamletInRetreat.Ham_Comment;
            newHamlet.Ham_Description = hamletInRetreat.Ham_Description;
            newHamlet.Ham_ID = Guid.NewGuid();
            newHamlet.Ham_LocalName = hamletInRetreat.Ham_LocalName;
            
            newHamlet.Ham_Site_ID = retreat.Ret_Site_ID;
            newHamlet.Ham_VietnameseName = hamletInRetreat.Ham_VietnameseName;
            newHamlet.Ham_EnglishName = hamletInRetreat.Ham_EnglishName;
            newHamlets.Add(newHamlet);

            foreach (Monks.jkp_Building building in hamletInRetreat.jkp_Buildings)
            {
                Monks.jkp_Building newBuilding = new Monks.jkp_Building();
                newBuilding.Bld_Comment = building.Bld_Comment;
                newBuilding.Bld_Description = building.Bld_Description;
                newBuilding.Bld_Directions = building.Bld_Directions;
                newBuilding.Bld_EnglishName = building.Bld_EnglishName;
                newBuilding.Bld_Ham_ID = newHamlet.Ham_ID;
                newBuilding.Bld_ID = Guid.NewGuid();
                newBuilding.Bld_LocalName = building.Bld_LocalName;
                newBuilding.Bld_VietnameseName = building.Bld_VietnameseName;
                newBuildings.Add(newBuilding);

                foreach (Monks.jkp_Room room in building.jkp_Rooms)
                {
                    Monks.jkp_Room newRoom = new Monks.jkp_Room();
                    newRoom.Rm_Bld_ID = newBuilding.Bld_ID;
                    newRoom.Rm_Comment = room.Rm_Comment;
                    newRoom.Rm_ID = Guid.NewGuid();
                    newRoom.Rm_Level = room.Rm_Level;
                    newRoom.Rm_Name = room.Rm_Name;
                    newRoom.Rm_RmType_ID = room.Rm_RmType_ID;
                    newRoom.Rm_TotalCapacity = room.Rm_TotalCapacity;
                    newRooms.Add(newRoom);

                    foreach (Monks.jkp_RoomRate roomRate in room.jkp_RoomRates)
                    {
                        Monks.jkp_RoomRate newRoomRate = new Monks.jkp_RoomRate();
                        newRoomRate.RR_EndDate = roomRate.RR_EndDate;
                        newRoomRate.RR_StartDate = roomRate.RR_StartDate;
                        newRoomRate.RR_Rm_ID = newRoom.Rm_ID;
                        newRoomRate.RR_Rt_ID = roomRate.RR_Rt_ID;
                        newRoomRates.Add(newRoomRate);
                    }

                }
            }
        }

        db.jkp_Hamlets.InsertAllOnSubmit(newHamlets);
        db.jkp_Buildings.InsertAllOnSubmit(newBuildings);
        db.jkp_Rooms.InsertAllOnSubmit(newRooms);
        db.jkp_RoomRates.InsertAllOnSubmit(newRoomRates);
        db.SubmitChanges();
        db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues);
        LoadRetreatsFromSelectedSite();

        mvCopy.ActiveViewIndex = 1;
    }

    public void lbtnCopyAssignments_Clicked(object sender, EventArgs e)
    {
        LinkButton lbtnCopyAssignments = (LinkButton)sender;
        Guid retreatId = new Guid(lbtnCopyAssignments.Attributes["retreatid"]);
        Monks.jkp_Retreat retreat = db.jkp_Retreats.First(p => p.Ret_ID == retreatId);

        foreach (Monks.jkp_Assignment assignment in retreat.jkp_Assignments)
        {
            Monks.jkp_Assignment newAssignment = new Monks.jkp_Assignment();
            newAssignment.Assn_Comment = assignment.Assn_Comment;
            newAssignment.Assn_Description = assignment.Assn_Description;
            newAssignment.Assn_ID = Guid.NewGuid();
            newAssignment.Assn_Name = assignment.Assn_Name;
            newAssignment.Assn_RetreatId = RetreatId;
            db.jkp_Assignments.InsertOnSubmit(newAssignment);
        }
        db.SubmitChanges();
        mvCopy.ActiveViewIndex = 1;
    }

    public void lbtnCopyActivities_Clicked(object sender, EventArgs e)
    {
        LinkButton lbtnCopyActivities = (LinkButton)sender;
        Guid retreatId = new Guid(lbtnCopyActivities.Attributes["retreatid"]);
        Monks.jkp_Retreat retreat = db.jkp_Retreats.First(p => p.Ret_ID == retreatId);
        foreach (Monks.jkp_Activity activity in retreat.jkp_Activities)
        {
            Monks.jkp_Activity newActivitiy = new Monks.jkp_Activity();
            newActivitiy.Act_Date = retreat.Ret_StartDate;
            newActivitiy.Act_Description = activity.Act_Description;
            newActivitiy.Act_EndTime = retreat.Ret_StartDate;
            newActivitiy.Act_ID = Guid.NewGuid();
            newActivitiy.Act_Language = activity.Act_Language;
            newActivitiy.Act_Name = activity.Act_Name;
            newActivitiy.Act_Ret_ID = RetreatId;
            newActivitiy.Act_StartTime = activity.Act_StartTime;
            db.jkp_Activities.InsertOnSubmit(newActivitiy);

        }
        db.SubmitChanges();
        mvCopy.ActiveViewIndex = 1;
    }
}
