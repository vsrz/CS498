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
using System.Data.Linq;
using System.IO;
using System.Text;

public partial class Retreat_AssignRooms : LoggedInPage
{
    MonkData db = new MonkData();
    public Guid RetreatId
    {
        get { return new Guid(Request.QueryString["retreatid"]); }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        DataLoadOptions dlo = new DataLoadOptions();
        dlo.LoadWith<Monks.jkp_Room>(p => p.jkp_Building);
        dlo.LoadWith<Monks.jkp_Room>(p => p.jkp_RoomType);
        dlo.LoadWith<Monks.jkp_Building>(p => p.jkp_Hamlet);
        dlo.LoadWith<Monks.jkp_PersonAttendingRetreat>(p => p.jkp_Person);
        //dlo.LoadWith<Monks.jkp_PersonAttendingRetreat>(p=>p.jkp_Room);
        db.LoadOptions = dlo;
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);



        if (!Page.IsPostBack)
        {

            UpdateAssignmentLists();

        }

    }

    public void UpdateAssignmentLists()
    {
        db.Refresh(RefreshMode.OverwriteCurrentValues);
        Monks.jkp_Retreat retreat = db.jkp_Retreats.First(p => p.Ret_ID == RetreatId);
        litRetreatName.Text = retreat.Ret_Name;
        var roomsForRetreat = from r in db.jkp_Rooms
                              where r.Rm_ContructedDate <= retreat.Ret_StartDate && r.Rm_DestroyedDate >= retreat.Ret_EndDate
                              select r;
        litRooms.Text = roomsForRetreat.Count().ToString();
        if (roomsForRetreat.Count() > 0)
            litTotalCapacity.Text = retreat.Ret_TotalCapacity.ToString();

        litRoomsFilled.Text = roomsForRetreat.Where(q => q.Rm_TotalCapacity == q.jkp_PersonAttendingRetreats.Count()).Count().ToString();

        if (roomsForRetreat.Count() > 0)
        {
            litRemainingSpace.Text = (retreat.Ret_TotalCapacity - retreat.jkp_PersonAttendingRetreats.Count()).ToString();
            litRemainingToBeAssigned.Text = retreat.jkp_PersonAttendingRetreats.Where(p => p.PAR_RoomId == null).Count().ToString();

            int personsInRooms = 0;
            foreach (Monks.jkp_Room room in roomsForRetreat)
            {
                foreach (Monks.jkp_PersonAttendingRetreat par in room.jkp_PersonAttendingRetreats)
                {
                    personsInRooms++;
                }                
                litSpotsFilledSoFar.Text = personsInRooms.ToString();
            }
        }

        var peopleWithoutRooms = from par in retreat.jkp_PersonAttendingRetreats
                                 where par.PAR_RoomId == null
                                 orderby par.PAR_ContributionId descending
                                 select par;

        rpUnAssignedPeople.DataSource = peopleWithoutRooms;
        rpUnAssignedPeople.DataBind();

        var peopleWithRooms = from par in retreat.jkp_PersonAttendingRetreats
                              where par.PAR_RoomId != null
                              orderby par.PAR_ContributionId descending
                              select par;
        
        rpAssignedToRooms.DataSource = peopleWithRooms;
        rpAssignedToRooms.DataBind();
    }

    
    List<Monks.jkp_Room> vacantRooms;

    public List<Monks.jkp_Room> GetVacantRooms()
    {
        var retreat = (from a in db.jkp_Retreats
                      where a.Ret_ID == RetreatId
                      select a).Single();

        // Get all of the rooms at the retreat site. 
        var roomsAtRetreatSite = from t in db.jkp_Rooms
                    where t.jkp_Building.jkp_Hamlet.jkp_Site.Site_ID == retreat.Ret_Site_ID
                    select t;

        // Get all of the rooms that exist during the retrat
        var roomsDuringTheRetreat = from r in roomsAtRetreatSite
                                    where r.Rm_ContructedDate <= retreat.Ret_StartDate && r.Rm_DestroyedDate >= retreat.Ret_EndDate
                                    select r;

        // Get rooms that still have space in them.
        var roomsWithSpace = from r in roomsDuringTheRetreat
                             where r.jkp_PersonAttendingRetreats.Where(p=>(p.PAR_ArrivalTime <= retreat.Ret_StartDate && p.PAR_DepartureTime >= retreat.Ret_EndDate) || (p.PAR_ArrivalTime <= retreat.Ret_StartDate && p.PAR_DepartureTime <= retreat.Ret_EndDate) || (p.PAR_ArrivalTime >= retreat.Ret_StartDate && p.PAR_DepartureTime >= retreat.Ret_EndDate)).Count() < r.Rm_TotalCapacity
                             select r;
        
        return roomsWithSpace.ToList();
    }


    public void rpRooms_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Monks.jkp_PersonAttendingRetreat par = (Monks.jkp_PersonAttendingRetreat)e.Item.DataItem;
            LinkButton lbtnPersonName = (LinkButton)e.Item.FindControl("lbtnPersonName");
            Label lblGender = (Label)e.Item.FindControl("lblPersonGender");
            Label lblAge = (Label)e.Item.FindControl("lblPersonAge");
            LinkButton lbtnRequestedRoomType = (LinkButton)e.Item.FindControl("lbtnRequestedRoomType");
            DropDownList dlRoomsToSelect = (DropDownList)e.Item.FindControl("dlRoomsToSelect");
            LinkButton lbtnViewDetailsOfRoom = (LinkButton)e.Item.FindControl("lbtnViewDetailsOfRoom");
            Button btnSaveRoomSelection = (Button)e.Item.FindControl("btnSaveRoomSelection");
            HtmlTableRow trAssignPerson = (HtmlTableRow)e.Item.FindControl("trAssignPerson");

            int colorNumber = par.PAR_ContributionId.GetHashCode();
            
            System.Drawing.Color color = System.Drawing.Color.FromArgb(colorNumber);
             trAssignPerson.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#" + color.R.ToString("X") + color.G.ToString("X") + color.B.ToString("X"));

            // We want the link text to be the opposite color as the brightness of the displayed color.
            float brightness = color.GetBrightness();
            if(brightness < .5)
            {
                lbtnPersonName.Style.Add(HtmlTextWriterStyle.Color, "White");
                lbtnRequestedRoomType.Style.Add(HtmlTextWriterStyle.Color, "White");
                lbtnViewDetailsOfRoom.Style.Add(HtmlTextWriterStyle.Color, "White");
                trAssignPerson.Style.Add(HtmlTextWriterStyle.Color, "White");
            }
            else
            {
                trAssignPerson.Style.Add(HtmlTextWriterStyle.Color, "Black");
            }


           

            
            lbtnPersonName.Text = par.jkp_Person.Per_FirstName + " " + par.jkp_Person.Per_LastName;
            if (par.jkp_Person.Per_Gender == 'M') 
                lblGender.Text = "male";
            else
                lblGender.Text = "female";

            lblAge.Text = (DateTime.Now.Year - par.jkp_Person.Per_Birthdate.Value.Year).ToString();
            lbtnRequestedRoomType.Text = par.jkp_RoomType.RmType_Name;

            if (vacantRooms == null)
            {
                vacantRooms = GetVacantRooms();
            }

            foreach (Monks.jkp_Room room in vacantRooms)
            {
                string hamletName = string.Empty;
                if (!String.IsNullOrEmpty(room.jkp_Building.jkp_Hamlet.Ham_LocalName))
                {
                    hamletName = room.jkp_Building.jkp_Hamlet.Ham_LocalName;
                }
                else if (!String.IsNullOrEmpty(room.jkp_Building.jkp_Hamlet.Ham_EnglishName))
                {
                    hamletName = room.jkp_Building.jkp_Hamlet.Ham_EnglishName;
                }
                else if (!String.IsNullOrEmpty(room.jkp_Building.jkp_Hamlet.Ham_VietnameseName))
                {
                    hamletName = room.jkp_Building.jkp_Hamlet.Ham_VietnameseName;
                }

                string buildingName = string.Empty;
                if (!String.IsNullOrEmpty(room.jkp_Building.Bld_LocalName))
                {
                    buildingName = room.jkp_Building.Bld_LocalName;
                }
                else if (!String.IsNullOrEmpty(room.jkp_Building.Bld_EnglishName))
                {
                    buildingName = room.jkp_Building.Bld_EnglishName;
                }
                else if (!String.IsNullOrEmpty(room.jkp_Building.Bld_VietnameseName))
                {
                    buildingName = room.jkp_Building.Bld_VietnameseName;
                }


                ListItem liRoom = new ListItem();
                liRoom.Text = hamletName + " - " + room.Rm_Name  + " - " + room.jkp_RoomType.RmType_Name + " - " + buildingName + " (" + (room.Rm_TotalCapacity - room.jkp_PersonAttendingRetreats.Count()) + ")";
                liRoom.Value = room.Rm_ID.ToString();
                dlRoomsToSelect.Items.Add(liRoom);


            }

            btnSaveRoomSelection.Attributes.Add("parid", par.PAR_ID.ToString());
            
        }
    }

    public void rpAssignedRooms_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Monks.jkp_PersonAttendingRetreat par = (Monks.jkp_PersonAttendingRetreat)e.Item.DataItem;
            LinkButton lbtnPersonName = (LinkButton)e.Item.FindControl("lbtnPersonName");
            LinkButton lbtnUnAssignViewDetailsOfRoom = (LinkButton)e.Item.FindControl("lbtnUnAssignViewDetailsOfRoom");
            Button btnUnassignRoom = (Button)e.Item.FindControl("btnUnassignRoom");
            Literal litAssignedRoomName = (Literal)e.Item.FindControl("litAssignedRoomName");
            HtmlTableRow trAssignRoom = (HtmlTableRow)e.Item.FindControl("trAssignRoom");

            lbtnPersonName.Text = par.jkp_Person.Per_FirstName + " " + par.jkp_Person.Per_LastName;
            lbtnPersonName.Attributes.Add("per_age", (DateTime.Now.Year - par.jkp_Person.Per_Birthdate.Value.Year).ToString());
            lbtnPersonName.Attributes.Add("per_fLangPref", par.jkp_Person.Per_PrimaryLanguage);
            lbtnPersonName.Attributes.Add("per_sLangPref", par.jkp_Person.Per_SecondaryLanguage);
            if (par.jkp_Person.Per_Gender == 'M')
                lbtnPersonName.Attributes.Add("per_gender", "male");
            else
                lbtnPersonName.Attributes.Add("per_gender", "female");
            lbtnUnAssignViewDetailsOfRoom.Attributes.Add("roomid", par.PAR_RoomId.ToString());
            btnUnassignRoom.Attributes.Add("parid", par.PAR_ID.ToString());
            litAssignedRoomName.Text = par.jkp_Room.jkp_Building.jkp_Hamlet.Ham_LocalName + " - " + par.jkp_Room.jkp_Building.Bld_LocalName + " - " + par.jkp_Room.Rm_Name;


            // Coloring stuff
            int colorNumber = par.PAR_ContributionId.GetHashCode();
            
            System.Drawing.Color color = System.Drawing.Color.FromArgb(colorNumber);
            trAssignRoom.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#" + color.R.ToString("X") + color.G.ToString("X") + color.B.ToString("X"));

            // We want the link text to be the opposite color as the brightness of the displayed color.
            float brightness = color.GetBrightness();
            if(brightness < .5)
            {
                lbtnPersonName.Style.Add(HtmlTextWriterStyle.Color, "White");
                lbtnUnAssignViewDetailsOfRoom.Style.Add(HtmlTextWriterStyle.Color, "White");
                
                trAssignRoom.Style.Add(HtmlTextWriterStyle.Color, "White");
            }
            else
            {
                trAssignRoom.Style.Add(HtmlTextWriterStyle.Color, "Black");
            }


        }
    }

    // export room assignments to csv file
    public void GetCSVRoomAssignments(object sender, EventArgs e)
    {
        var retreat = db.jkp_Retreats.Single(p => p.Ret_ID == RetreatId);
        string filename = retreat.Ret_Name.ToString().Replace('.', '_') + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Year.ToString() + ".csv";
        try
        {
            string dirPath = System.IO.Path.Combine(Server.MapPath(""), "tmp");
            DirectoryInfo dinfo = new DirectoryInfo(dirPath);
            if (!dinfo.Exists)
                dinfo.Create();

            foreach (FileInfo f in dinfo.GetFiles())
            {
                // delete all export files after 24 hours has passed
                if ((f.CreationTime.Add(new TimeSpan(24, 0, 0)) < DateTime.Now))
                {
                    f.Delete();
                }
            }

            string filepath = System.IO.Path.Combine(dirPath, filename);
            StreamWriter sw = new StreamWriter(filepath, false);
            sw.Write("Name,Gender,Age,First Language Pref.,Second Language Pref.,Room Name\r\n");   // headers for csv file
            foreach (RepeaterItem assignment in rpAssignedToRooms.Items)
            {
                LinkButton personName = (LinkButton)assignment.FindControl("lbtnPersonName");
                Literal roomName = (Literal)assignment.FindControl("litAssignedRoomName");

                sw.Write(personName.Text + "," + personName.Attributes["per_gender"] + "," + personName.Attributes["per_age"] +  "," +  personName.Attributes["per_fLangPref"] + "," + personName.Attributes["per_sLangPref"] + "," + roomName.Text + "\r\n");
            }
            sw.Close();

            for (int i = 0; i < Request.Url.Segments.Count() - 1; i++) 
            {
                csvFileLink.NavigateUrl += Request.Url.Segments[i];
            }
            csvFileLink.NavigateUrl += "tmp/" + filename;
            csvFileLink.Visible = true;
        }
        catch(Exception u)
        { 
            error.Text = u.ToString(); 
        }  
    }

    public void btnSaveRoomSelection_Clicked(object sender, EventArgs e)
    {

        Button btnSaveRoomSelection = (Button)sender;
        DropDownList dlRoomsToSelect = (DropDownList)btnSaveRoomSelection.Parent.FindControl("dlRoomsToSelect");
        Guid roomId = new Guid(dlRoomsToSelect.SelectedValue);
        Guid personAttendingRetId = new Guid(btnSaveRoomSelection.Attributes["parid"]);
        Monks.jkp_PersonAttendingRetreat par = db.jkp_PersonAttendingRetreats.First(p => p.PAR_ID == personAttendingRetId);
        par.PAR_RoomId = roomId;
        db.SubmitChanges();
        UpdateAssignmentLists();
    }

    public void lbtnViewDetailsOfRoom_Clicked(object sender, EventArgs e)
    {
        LinkButton btnViewRoomDetails = (LinkButton)sender;
        Guid selectedRoomId;
        if (String.IsNullOrEmpty(btnViewRoomDetails.Attributes["roomid"]))
        {

            DropDownList dlRoomsToSelect = (DropDownList)btnViewRoomDetails.Parent.FindControl("dlRoomsToSelect");
            selectedRoomId = new Guid(dlRoomsToSelect.SelectedValue);
        }
        else
        {
            selectedRoomId = new Guid(btnViewRoomDetails.Attributes["roomid"]);
        }
        Monks.jkp_Room room = db.jkp_Rooms.First(p => p.Rm_ID == selectedRoomId);
        litHamletName.Text = room.jkp_Building.jkp_Hamlet.Ham_LocalName;
        litBuildingName.Text = room.jkp_Building.Bld_LocalName;
        litRoomName.Text = room.Rm_Name;
        litRoomType.Text = room.jkp_RoomType.RmType_Name;
        chkCreateRoomTypeAirConditioning.Checked = (bool)room.jkp_RoomType.RmType_AirCondition;
        chkCreateRoomTypeBathroom.Checked = (bool)room.jkp_RoomType.RmType_Bathroom;
        chkCreateRoomTypeLowerBunkBed.Checked = (bool)room.jkp_RoomType.RmType_LowerBunkBed;

        chkCreateRoomTypeShower.Checked = (bool)room.jkp_RoomType.RmType_Shower;
        chkCreateRoomTypeSingleBed.Checked = (bool)room.jkp_RoomType.RmType_SingleBed;

        litFloor.Text = room.Rm_Level.ToString();
        litRoomTotalCapacity.Text = room.Rm_TotalCapacity.ToString();
        litRoomRemainingSpace.Text = (room.Rm_TotalCapacity - room.jkp_PersonAttendingRetreats.Count()).ToString();

        rpPeopleCurrentlyInRoom.DataSource = room.jkp_PersonAttendingRetreats;
        rpPeopleCurrentlyInRoom.DataBind();

        modalRoom.Show();
    }

    public void btnCloseRoomDetails_Clicked(object sender, EventArgs e)
    {
        modalRoom.Hide();
    }

    public void btnUnassignRoom_Clicked(object sender, EventArgs e)
    {
        Button btnUnassignRoom = (Button)sender;
        Guid personAttendingRetreatId = new Guid(btnUnassignRoom.Attributes["parid"]);
        Monks.jkp_PersonAttendingRetreat par = db.jkp_PersonAttendingRetreats.First(p => p.PAR_ID == personAttendingRetreatId);
        par.PAR_RoomId = null;
        db.SubmitChanges();
        UpdateAssignmentLists();

    }
    int countOfPeopleInRoom = 0;
    public void rpPeopleCurrentlyInRoom_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            countOfPeopleInRoom++;
            Monks.jkp_PersonAttendingRetreat par = (Monks.jkp_PersonAttendingRetreat)e.Item.DataItem;
            Literal litPersonInRoomName = (Literal)e.Item.FindControl("litPersonInRoomName");
            Button lbtnRemovePersonFromRoom = (Button)e.Item.FindControl("lbtnRemovePersonFromRoom");
            Literal litCountOfPeople = (Literal)e.Item.FindControl("litCountOfPeople");
            litPersonInRoomName.Text = par.jkp_Person.Per_FirstName + " " + par.jkp_Person.Per_LastName;
            lbtnRemovePersonFromRoom.Attributes.Add("parid", par.PAR_ID.ToString());
            litCountOfPeople.Text = countOfPeopleInRoom.ToString();

        }
    }


}
