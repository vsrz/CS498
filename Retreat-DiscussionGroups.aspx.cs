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

public partial class Retreat_DiscussionGroups : LoggedInPage
{
    MonkData db = new MonkData();
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

        if(!Page.IsPostBack)
        {
            LoadAllPageData();
        }
    }

    private void LoadAllPageData()
    {
        db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues);
        var discussionGroupsForRetreat = from p in db.jkp_DiscussionGroups
                                   where p.DGroup_RetreatID == RetreatId
                                   select p;
        rpDiscussionGroups.DataSource = discussionGroupsForRetreat;
        rpDiscussionGroups.DataBind();

        var personGoingToRetreat = from p in db.jkp_Persons
                                   where p.jkp_PersonAttendingRetreats.Where(j=>j.PAR_RetId == RetreatId).Count() > 0
                                   select p;

        dlAssnPersons.DataSource = personGoingToRetreat;
        dlAssnPersons.DataTextField = "DisplayName";
        dlAssnPersons.DataValueField = "Per_ID";
        dlAssnPersons.DataBind();
    }

    public void rpDiscussionGroups_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Monks.jkp_DiscussionGroup discussionGroup = (Monks.jkp_DiscussionGroup)e.Item.DataItem;
            Repeater rpPeopleInGroup = (Repeater)e.Item.FindControl("rpPeopleInGroup");
            Literal litDiscussionGroupName = (Literal)e.Item.FindControl("litDiscussionGroupName");
            HiddenField hidDiscussionGroupId = (HiddenField)e.Item.FindControl("hidDiscussionGroupId");
            LinkButton lbtnAddPersonToGroup = (LinkButton)e.Item.FindControl("lbtnAddPersonToGroup");

            litDiscussionGroupName.Text = discussionGroup.DGroup_Name;

            lbtnAddPersonToGroup.Attributes.Add("groupid", discussionGroup.DGroup_ID.ToString());
            hidDiscussionGroupId.Value = discussionGroup.DGroup_ID.ToString();

            rpPeopleInGroup.DataSource = discussionGroup.jkp_PersonDiscussionGroups;
            rpPeopleInGroup.DataBind();
        }
    }

    public void rpPeopleInGroup_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Monks.jkp_PersonDiscussionGroup perDis = (Monks.jkp_PersonDiscussionGroup)e.Item.DataItem;
            Literal litPersonName = (Literal)e.Item.FindControl("litPersonName");
            LinkButton lbtnRemovePersonFromGroup = (LinkButton)e.Item.FindControl("lbtnRemovePersonFromGroup");
            litPersonName.Text = perDis.jkp_Person.Per_FirstName + " " + perDis.jkp_Person.Per_LastName;
            lbtnRemovePersonFromGroup.Attributes.Add("personid", perDis.PDG_Per_ID.ToString());
            lbtnRemovePersonFromGroup.Attributes.Add("groupid", perDis.PDG_DGroup_ID.ToString());

        }
    }

    public void lbtnRemovePersonFromGroup_Clicked(object sender, EventArgs e)
    {
        LinkButton lbtnRemovePersonFromGroup = (LinkButton)sender;
        Guid personId = new Guid(lbtnRemovePersonFromGroup.Attributes["personid"]);

        Guid groupId = new Guid(lbtnRemovePersonFromGroup.Attributes["groupid"]);

        Monks.jkp_PersonDiscussionGroup perDiscusionGroup = db.jkp_PersonDiscussionGroups.First(p=>p.PDG_Per_ID == personId && p.PDG_DGroup_ID == groupId);
        db.jkp_PersonDiscussionGroups.DeleteOnSubmit(perDiscusionGroup);
        db.SubmitChanges();
        LoadAllPageData();
    }

    public void lbtnAddPersonToGroup_Clicked(object sender, EventArgs e)
    {
        LinkButton lbtnAddPersontoGroup = (LinkButton)sender;
        Guid groupId = new Guid( lbtnAddPersontoGroup.Attributes["groupid"]);
        Monks.jkp_DiscussionGroup disucssionGroup = db.jkp_DiscussionGroups.First(p=>p.DGroup_ID == groupId);
        litAssnGroupName.Text = disucssionGroup.DGroup_Name;
        if(!String.IsNullOrEmpty(btnAssnSave.Attributes["groupid"]))
        {
            btnAssnSave.Attributes.Remove("groupid");
        }
        btnAssnSave.Attributes.Add("groupid", disucssionGroup.DGroup_ID.ToString());
        modalAssignPerson.Show();
    }

    public void btnAssnSave_Clicked(object sender, EventArgs e)
    {
        
        Guid groupId = new Guid(btnAssnSave.Attributes["groupid"]);
        Monks.jkp_PersonDiscussionGroup pdGroup = new Monks.jkp_PersonDiscussionGroup();
        pdGroup.PDG_DGroup_ID = groupId;
        pdGroup.PDG_Per_ID = new Guid(dlAssnPersons.SelectedValue);
        pdGroup.PDG_ID = Guid.NewGuid();
        db.jkp_PersonDiscussionGroups.InsertOnSubmit(pdGroup);
        db.SubmitChanges();
        LoadAllPageData();
        modalAssignPerson.Hide();
    }

    public void btnAssnCancel_Clicked(object sender, EventArgs e)
    {
        modalAssignPerson.Hide();
    }

    public void lbtnCreateNewGroup_Clicked(object sender, EventArgs e)
    {
        modalCreateAssignment.Show();

    }

    public void btnCreateGroupSave_Clicked(object sender, EventArgs e)
    {
        Monks.jkp_DiscussionGroup dGroup = new Monks.jkp_DiscussionGroup();
        dGroup.DGroup_RetreatID = RetreatId;
        dGroup.DGroup_ID = Guid.NewGuid();
        dGroup.DGroup_Name = txtCreateGroupName.Text.Trim();
        dGroup.DGroup_Location = txtCreateGroupLocation.Text.Trim();
        
        db.jkp_DiscussionGroups.InsertOnSubmit(dGroup);
        db.SubmitChanges();
        LoadAllPageData();
        modalCreateAssignment.Hide();
    }

    public void btnCreateGroupCancel_Clicked(object sender, EventArgs e)
    {
        modalCreateAssignment.Hide();
    }


}
