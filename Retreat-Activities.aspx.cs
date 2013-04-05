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

public partial class Retreat_Activities : System.Web.UI.Page
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
        if (!Page.IsPostBack)
        {
            LoadActivitiesList();
            LoadAllPageData();
        }
    }

    private void ResetCreateActivityForm()
    {
        txtCreateAssnName.Text = "";
        txtDescription.Text = "";
        txtCreateActStartTime.Text = "";
        txtCreateActEndTime.Text = "";
    }


    private void LoadActivitiesList()
    {
        var ActivitiesForRetreat = from a in db.jkp_Activities
                                    where a.Act_Ret_ID == RetreatId
                                    select a;
        dlAssnActivities.DataSource = ActivitiesForRetreat;
        dlAssnActivities.DataTextField = "Act_Name";
        dlAssnActivities.DataValueField = "Act_ID";
        dlAssnActivities.DataBind();
    }

    private void LoadAllPageData()
    {
        MonkData db = new MonkData();
        var personGoingToRetreat = from p in db.jkp_PersonAttendingRetreats
                                   where p.PAR_RetId == RetreatId
                                   select p;
        rpActivitiesForPeople.DataSource = personGoingToRetreat;
        rpActivitiesForPeople.DataBind();
    }

    public void rpActivitiesForPeople_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Monks.jkp_PersonAttendingRetreat par = (Monks.jkp_PersonAttendingRetreat)e.Item.DataItem;
            LinkButton lbtnAddActivity = (LinkButton)e.Item.FindControl("lbtnAddActivity");
            Literal litRetreatantName = (Literal)e.Item.FindControl("litRetreatantName");
            HiddenField hidPersonId = (HiddenField)e.Item.FindControl("hidPersonId");
            Repeater rpActivities = (Repeater)e.Item.FindControl("rpActivities");
            litRetreatantName.Text = par.jkp_Person.Per_FirstName + " " + par.jkp_Person.Per_LastName;
            rpActivities.DataSource = from p in par.jkp_Person.jkp_PersonActivities where p.jkp_Activity.Act_Ret_ID == RetreatId select p;
            rpActivities.DataBind();
            lbtnAddActivity.Attributes.Add("personid", par.jkp_Person.Per_ID.ToString());
            hidPersonId.Value = par.PAR_PersonId.ToString();

        }
    }

    public void rpActivities_OnItemDataBOund(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Monks.jkp_PersonActivity par = (Monks.jkp_PersonActivity)e.Item.DataItem;
            Literal litActivityName = (Literal)e.Item.FindControl("litActivityName");
            LinkButton lbtnActivityDelete = (LinkButton)e.Item.FindControl("lbtnActivityDelete");
            litActivityName.Text = par.jkp_Activity.Act_Name;
            lbtnActivityDelete.Attributes.Add("parid", par.PAct_ID.ToString());
        }
    }

    public void lbtnActivityDelete_Clicked(object sender, EventArgs e)
    {
        LinkButton lbtnActivityDelete = (LinkButton)sender;

        Guid parId = new Guid(lbtnActivityDelete.Attributes["parid"]);

        Monks.jkp_PersonActivity parToDel = db.jkp_PersonActivities.First(p => p.PAct_ID == parId);
        Guid personId = parToDel.PAct_Per_ID;
        db.jkp_PersonActivities.DeleteOnSubmit(parToDel);
        db.SubmitChanges();

        Repeater rpActivities = (Repeater)lbtnActivityDelete.Parent.Parent.Parent.Parent;
        BindPersonActivities(rpActivities, personId);
    }

    public void BindPersonActivities(Repeater rpToBindTo, Guid personId)
    {
        db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues);
        var personActivityRetreats = from p in db.jkp_PersonActivities
                                       where p.PAct_Per_ID == personId && p.jkp_Activity.Act_Ret_ID == RetreatId
                                       select p;
        rpToBindTo.DataSource = personActivityRetreats;
        rpToBindTo.DataBind();
    }

    public void lbtnAddActivity_Clicked(object sender, EventArgs e)
    {
        LinkButton lbtnAddActivity = (LinkButton)sender;

        btnAssnSave.Attributes.Remove("personid");
        btnAssnSave.Attributes.Add("personid", lbtnAddActivity.Attributes["personid"]);
        modalAssignPerson.Show();
        LoadActivitiesList();
    }

    public void btnAssnSave_Clicked(object sender, EventArgs e)
    {
        Button btnAssnSave = (Button)sender;
        Guid personIdToAddActivityTo = new Guid(btnAssnSave.Attributes["personid"]);
        
        var parAlreadyExisting = from p in db.jkp_PersonActivities
                                 where p.PAct_Act_ID == new Guid(dlAssnActivities.SelectedValue) && p.PAct_Per_ID == personIdToAddActivityTo
                                 select p;
        if(parAlreadyExisting.Count() > 0)
            return;
        
        Monks.jkp_PersonActivity assignRetreat = new Monks.jkp_PersonActivity();
        assignRetreat.PAct_Act_ID = new Guid(dlAssnActivities.SelectedValue);
        assignRetreat.PAct_ID = Guid.NewGuid();
        assignRetreat.PAct_Per_ID = personIdToAddActivityTo;        
        db.jkp_PersonActivities.InsertOnSubmit(assignRetreat);
        db.SubmitChanges();

        foreach(RepeaterItem rpPerson in rpActivitiesForPeople.Items)
        {
            HiddenField hidPersonId = (HiddenField) rpPerson.FindControl("hidPersonId");
            if(hidPersonId.Value == personIdToAddActivityTo.ToString())
            {
                Repeater rpActivities = (Repeater) hidPersonId.Parent.FindControl("rpActivities");
                BindPersonActivities(rpActivities, personIdToAddActivityTo);
                break;
            }
        }
         
        modalAssignPerson.Hide();
        ResetCreateActivityForm();
    }

    public void lbtnCreateActivity_Clicked(object sender, EventArgs e)
    {
        ResetCreateActivityForm();
        modalCreateActivity.Show();
    }

    public void btnAssnCancel_Clicked(object sender, EventArgs e)
    {
        modalAssignPerson.Hide();
    }

    public void btnCreateAssnCancel_Clicked(object sender, EventArgs e)
    {
        modalCreateActivity.Hide();
    }



    public void btnCreateAssnSave_Clicked(object sender, EventArgs e)
    {
        Monks.jkp_Activity Activity = new Monks.jkp_Activity();
        Activity.Act_ID = Guid.NewGuid();
        Activity.Act_Name = txtCreateAssnName.Text;
        Activity.Act_Description = txtDescription.Text;
        if(!String.IsNullOrEmpty(txtCreateActStartTime.Text))
        {
            DateTime timeToInsert;
            if(DateTime.TryParse(txtCreateActStartTime.Text, out timeToInsert))
                Activity.Act_StartTime = timeToInsert;
        }
        if(!String.IsNullOrEmpty(txtCreateActEndTime.Text))
        {
            DateTime timeToInsert;
            if(DateTime.TryParse(txtCreateActEndTime.Text, out timeToInsert))
                Activity.Act_EndTime = timeToInsert;
        }
        Activity.Act_Ret_ID = RetreatId;
        db.jkp_Activities.InsertOnSubmit(Activity);
        db.SubmitChanges();

        modalCreateActivity.Hide();
    }

}
