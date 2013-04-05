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

public partial class Retreat_AddEditRooms : LoggedInPage
{
    public Guid RetreatId
    {
        get
        {
            return new Guid(Request.QueryString["retreatid"]);
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    MonkData db1 = new MonkData();
    public Monks.jkp_Retreat Retreat
    {
        get
        {
            
            if(retreat == null)
                retreat = db1.jkp_Retreats.First(p => p.Ret_ID == RetreatId);
            return retreat;
        }
    }

    Monks.jkp_Retreat retreat;
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (!Page.IsPostBack)
        {
            MonkData db = new MonkData();
            retreat = db.jkp_Retreats.First(p => p.Ret_ID == RetreatId);
            litRetreatName.Text = retreat.Ret_Name;
            if (retreat.Ret_StartDate != null)
                litStartDate.Text = ((DateTime)retreat.Ret_StartDate).ToString("d");

            if(retreat.jkp_Site != null)
                litSite.Text = retreat.jkp_Site.GetName;
            

            var roomRates = db.jkp_Rates;
            if(roomRates.Count() < 1)
            {
                Response.Redirect("GridView.aspx?typename=jkp_Rate");
                return;
            }
            


            RedrawRoomTypes(db);
            RedrawHamletList(db);
            RedrawBuildingsList(db);
            RedrawRoomsForRetreat(db);

            
            txtCreateRoomConstructionDate.Text = retreat.Ret_StartDate.ToShortDateString() + " 12:00:00 AM";
            txtCreateRoomDestructionDate.Text = retreat.Ret_EndDate.ToShortDateString() + " 11:59:00 PM";
            
            
            
        }
    }


    public void RedrawRoomTypes(MonkData db)
    {
        var roomTypes = from rt in db.jkp_RoomTypes
                        select rt;

        dlRoomType.DataSource = roomTypes;
        dlRoomType.DataTextField = "FullRoomTypeNameWithRate";
        dlRoomType.DataValueField = "RmType_ID";
        dlRoomType.DataBind();
        if (dlRoomType.Items.Count > 0)
        {
            dlRoomType.SelectedIndex = 0;
            FillSelectedRoomTypeDetails();
        }        
    }

    public void dlRoomTypes_Changed(object sender, EventArgs e)
    {
        FillSelectedRoomTypeDetails();
    }

    public void FillSelectedRoomTypeDetails()
    {
        MonkData db = new MonkData();
        var roomType = db.jkp_RoomTypes.First(p => p.RmType_ID == new Guid(dlRoomType.SelectedValue));

        chkCreateRoomTypeAirConditioning.Checked = (bool)roomType.RmType_AirCondition;
        chkCreateRoomTypeBathroom.Checked = (bool)roomType.RmType_Bathroom;
        chkCreateRoomTypeLowerBunkBed.Checked = (bool)roomType.RmType_Heating;
        chkCreateRoomTypeShower.Checked = (bool)roomType.RmType_Shower;
        chkCreateRoomTypeSingleBed.Checked = (bool)roomType.RmType_SingleBed;
        chkCreateRoomTypeUpperBunkBed.Checked = (bool)roomType.RmType_UpperBunkBed;

    }


    public void btnCreateHamlet_Clicked(object sender, EventArgs e)
    {
        MonkData db = new MonkData();
        Monks.jkp_Retreat retreat = db.jkp_Retreats.First(p => p.Ret_ID == RetreatId);
        Monks.jkp_Hamlet hamlet = new Monks.jkp_Hamlet();
        hamlet.Ham_ID = Guid.NewGuid();
        hamlet.Ham_LocalName = txtHamletLocalName.Text;
        hamlet.Ham_EnglishName = txtHamletEnglishName.Text;
        hamlet.Ham_VietnameseName = txtHamletVietName.Text;        
        hamlet.Ham_Site_ID = retreat.Ret_Site_ID;
        db.jkp_Hamlets.InsertOnSubmit(hamlet);
        db.SubmitChanges();
        RedrawHamletList(db);

        txtHamletEnglishName.Text = "";
        txtHamletLocalName.Text = "";
        txtHamletVietName.Text = "";
    }

    protected void RedrawHamletList(MonkData db)
    {
        var retreat = db.jkp_Retreats.First(p=>p.Ret_ID == RetreatId);
        var hamletsForRetreat = db.jkp_Hamlets.Where(p => p.Ham_Site_ID == retreat.Ret_Site_ID);
        dlHamlets.DataSource = hamletsForRetreat;
        dlHamlets.DataTextField = "Ham_LocalName";
        dlHamlets.DataValueField = "Ham_ID";
        dlHamlets.DataBind();
    }

    public void btnCreateBuilding_Clicked(object sender, EventArgs e)
    {
        MonkData db = new MonkData();
        Guid hamletId = new Guid(dlHamlets.SelectedValue);
        Monks.jkp_Building building = new Monks.jkp_Building();
        building.Bld_EnglishName = txtBuildingEnglishName.Text;
        building.Bld_LocalName = txtBuildingLocalName.Text;
        building.Bld_VietnameseName = txtBuildingVietnameseName.Text;
        building.Bld_Ham_ID = hamletId;
        building.Bld_ID = Guid.NewGuid();
        db.jkp_Buildings.InsertOnSubmit(building);
        db.SubmitChanges();
        RedrawBuildingsList(db);
        txtBuildingEnglishName.Text = "";
        txtBuildingLocalName.Text = "";
        txtBuildingVietnameseName.Text = "";
    }

    protected void RedrawBuildingsList(MonkData db)
    {
        var retreat = db.jkp_Retreats.First(p=>p.Ret_ID == RetreatId);
        var buildingsFromBuildingsForRetreat = db.jkp_Buildings.Where(p => p.jkp_Hamlet.Ham_Site_ID == retreat.Ret_Site_ID);
        dlBuildings.DataSource = buildingsFromBuildingsForRetreat;
        dlBuildings.DataTextField = "Bld_LocalName";
        dlBuildings.DataValueField = "Bld_ID";
        dlBuildings.DataBind();
    }

    public void btnCreateRoom_Clicked(object sender, EventArgs e)
    {
        MonkData db = new MonkData();
        Guid buildingId = new Guid(dlBuildings.SelectedValue);

        // Check to see if there are any rooms with this same name already on the building.
        var roomsWithSameNameInBuilding = from r in db.jkp_Rooms
                                          where r.Rm_Bld_ID == buildingId && r.Rm_Name == txtCreateRoomsName.Text.Trim().ToUpper()
                                          select r;
        if(roomsWithSameNameInBuilding.Count() > 0)
            return; // Duplicate room found.

        Monks.jkp_Room room = new Monks.jkp_Room();
        room.Rm_Bld_ID = buildingId;
        room.Rm_ID = Guid.NewGuid();
        if (!String.IsNullOrEmpty(txtCreateRoomLevelNumber.Text))
            room.Rm_Level = int.Parse(txtCreateRoomLevelNumber.Text);
        room.Rm_Name = txtCreateRoomsName.Text.Trim().ToUpper();
        room.Rm_RmType_ID = new Guid( dlRoomType.SelectedValue);
        room.Rm_TotalCapacity = int.Parse(txtCreateRoomCapacity.Text);
        room.Rm_ContructedDate = DateTime.Parse(txtCreateRoomConstructionDate.Text);
        if(!String.IsNullOrEmpty(txtCreateRoomDestructionDate.Text))
            room.Rm_DestroyedDate = DateTime.Parse(txtCreateRoomDestructionDate.Text);

        

        db.jkp_Rooms.InsertOnSubmit(room);
        
        db.SubmitChanges();
        RedrawRoomsForRetreat(db);
        txtCreateRoomsName.Text = "";
        modalRoom.Hide();
        
        

    }

    public void btnCreateRoomCancel_Clicked(object sender, EventArgs e)
    {
        modalRoom.Hide();
    }

    public void RedrawRoomsForRetreat(MonkData db)
    {
        var retreat = db.jkp_Retreats.First(p=>p.Ret_ID == RetreatId);
        // TODO
        var rooms = from r in db.jkp_Rooms
                    where r.jkp_Building.jkp_Hamlet.Ham_Site_ID == retreat.Ret_Site_ID                    
                    select r;
        var roomsOrderedByDateTime = rooms.OrderBy(p=>p.Rm_DestroyedDate.Value - p.Rm_ContructedDate.Value);
        litTotalRooms.Text = rooms.Count().ToString();
        if(rooms.Count() > 0)
            litRetreatCapacity.Text = rooms.Sum(p=>p.Rm_TotalCapacity).ToString();
        
        rpRooms.DataSource = roomsOrderedByDateTime;
        rpRooms.DataBind();
    }

    public void rpRooms_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Monks.jkp_Room room = (Monks.jkp_Room)e.Item.DataItem;
            Literal litExistingRoomBuildingName = (Literal)e.Item.FindControl("litExistingRoomBuildingName");
            Literal litExistingRoomName = (Literal)e.Item.FindControl("litExistingRoomName");
            LinkButton lbtnEditRoom = (LinkButton)e.Item.FindControl("lbtnEditRoom");
            LinkButton lbtnRoomDelete = (LinkButton)e.Item.FindControl("lbtnRoomDelete");
            Literal litCapacity = (Literal)e.Item.FindControl("litCapacity");
            Literal litRoomConstructionDate = (Literal)e.Item.FindControl("litRoomConstructionDate");
            Literal litRoomDestructionDate = (Literal)e.Item.FindControl("litRoomDestructionDate");
            Literal litRoomType = (Literal)e.Item.FindControl("litRoomType");
            Literal litRoomUseage = (Literal)e.Item.FindControl("litRoomUseage");

            if(room.jkp_PersonAttendingRetreats.Count > 0)
            {
                //lbtnRoomDelete.Style.Add(HtmlTextWriterStyle.Color, "Gray");
                //lbtnRoomDelete.Enabled = false;
                //lbtnRoomDelete.Text = "Cannot delete room. Already in use.";
                lbtnRoomDelete.Visible = false;
            }

            

            litRoomType.Text = room.jkp_RoomType.FullRoomTypeNameWithRate;

            litExistingRoomBuildingName.Text = room.jkp_Building.Bld_LocalName;
            litExistingRoomName.Text = room.Rm_Name;
            lbtnEditRoom.Attributes.Add("roomid", room.Rm_ID.ToString());
            lbtnRoomDelete.Attributes.Add("roomid", room.Rm_ID.ToString());
            litCapacity.Text = room.Rm_TotalCapacity.ToString();

            if(room.Rm_ContructedDate.HasValue)
                litRoomConstructionDate.Text = room.Rm_ContructedDate.Value.ToShortDateString();
            if(room.Rm_DestroyedDate.HasValue)
                litRoomDestructionDate.Text = room.Rm_DestroyedDate.Value.ToShortDateString();

            litRoomUseage.Text = room.jkp_PersonAttendingRetreats.Where(p=>(p.PAR_ArrivalTime <= Retreat.Ret_StartDate && p.PAR_DepartureTime >= Retreat.Ret_EndDate) || (p.PAR_ArrivalTime <= Retreat.Ret_StartDate && p.PAR_DepartureTime <= Retreat.Ret_EndDate) || (p.PAR_ArrivalTime >= Retreat.Ret_StartDate && p.PAR_DepartureTime >= Retreat.Ret_EndDate)).Count().ToString();
        }
    }

    public void lbtnRoomDelete_Clicked(object sender, EventArgs e)
    {
        LinkButton lbtnRoomDelete = (LinkButton)sender;
        Guid roomId = new Guid(lbtnRoomDelete.Attributes["roomid"]);
        MonkData db = new MonkData();
        Monks.jkp_Room roomToDelete = db.jkp_Rooms.First(p=>p.Rm_ID == roomId);
        db.jkp_Rooms.DeleteOnSubmit(roomToDelete);
        db.SubmitChanges();
        RedrawRoomsForRetreat(db);
    }

}
