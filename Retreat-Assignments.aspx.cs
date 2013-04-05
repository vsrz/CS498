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

public partial class Retreat_Assignments : System.Web.UI.Page
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
            LoadAssignmentsList();

            LoadAllPageData();
        }


    }

    private void LoadAssignmentsList()
    {
        var assignmentsForRetreat = from a in db.jkp_Assignments
                                    where a.Assn_RetreatId == RetreatId
                                    select a;
        dlAssnAssignments.DataSource = assignmentsForRetreat;
        dlAssnAssignments.DataTextField = "Assn_Name";
        dlAssnAssignments.DataValueField = "Assn_ID";
        dlAssnAssignments.DataBind();
    }

    private void LoadAllPageData()
    {
        MonkData db = new MonkData();
        var personGoingToRetreat = from p in db.jkp_PersonAttendingRetreats
                                   where p.PAR_RetId == RetreatId
                                   select p;
        rpAssignmentsForPeople.DataSource = personGoingToRetreat;
        rpAssignmentsForPeople.DataBind();
    }

    public void rpAssignmentsForPeople_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Monks.jkp_PersonAttendingRetreat par = (Monks.jkp_PersonAttendingRetreat)e.Item.DataItem;
            LinkButton lbtnAddAssignment = (LinkButton)e.Item.FindControl("lbtnAddAssignment");
            Literal litRetreatantName = (Literal)e.Item.FindControl("litRetreatantName");
            HiddenField hidPersonId = (HiddenField)e.Item.FindControl("hidPersonId");
            Repeater rpAssignments = (Repeater)e.Item.FindControl("rpAssignments");
            litRetreatantName.Text = par.jkp_Person.Per_FirstName + " " + par.jkp_Person.Per_LastName;
            rpAssignments.DataSource = from p in par.jkp_Person.jkp_PersonAssignmentRetreats where p.PAR_Ret_ID == RetreatId select p;
            rpAssignments.DataBind();
            lbtnAddAssignment.Attributes.Add("personid", par.jkp_Person.Per_ID.ToString());
            hidPersonId.Value = par.PAR_PersonId.ToString();

        }
    }

    public void rpAssignments_OnItemDataBOund(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Monks.jkp_PersonAssignmentRetreat par = (Monks.jkp_PersonAssignmentRetreat)e.Item.DataItem;
            Literal litAssignmentName = (Literal)e.Item.FindControl("litAssignmentName");
            LinkButton lbtnAssignmentDelete = (LinkButton)e.Item.FindControl("lbtnAssignmentDelete");
            litAssignmentName.Text = par.jkp_Assignment.Assn_Name;
            lbtnAssignmentDelete.Attributes.Add("parid", par.PAR_ID.ToString());
        }
    }

    public void lbtnAssignmentDelete_Clicked(object sender, EventArgs e)
    {
        LinkButton lbtnAssignmentDelete = (LinkButton)sender;

        Guid parId = new Guid(lbtnAssignmentDelete.Attributes["parid"]);

        Monks.jkp_PersonAssignmentRetreat parToDel = db.jkp_PersonAssignmentRetreats.First(p => p.PAR_ID == parId);
        Guid personId = parToDel.PAR_Per_ID;
        db.jkp_PersonAssignmentRetreats.DeleteOnSubmit(parToDel);
        db.SubmitChanges();

        Repeater rpAssignments = (Repeater)lbtnAssignmentDelete.Parent.Parent.Parent.Parent;
        BindPersonActivities(rpAssignments, personId);

        

    }

    public void BindPersonActivities(Repeater rpToBindTo, Guid personId)
    {
        db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues);
        var personAssignmentRetreats = from p in db.jkp_PersonAssignmentRetreats
                                       where p.PAR_Per_ID == personId && p.PAR_Ret_ID == RetreatId
                                       select p;
        rpToBindTo.DataSource = personAssignmentRetreats;
        rpToBindTo.DataBind();
    }

    public void lbtnAddAssignment_Clicked(object sender, EventArgs e)
    {
        LinkButton lbtnAddAssignment = (LinkButton)sender;

        btnAssnSave.Attributes.Remove("personid");
        btnAssnSave.Attributes.Add("personid", lbtnAddAssignment.Attributes["personid"]);
        modalAssignPerson.Show();
        LoadAssignmentsList();
    }

    public void btnAssnSave_Clicked(object sender, EventArgs e)
    {
        Button btnAssnSave = (Button)sender;
        Guid personIdToAddAssignmentTo = new Guid(btnAssnSave.Attributes["personid"]);
        
        var parAlreadyExisting = from p in db.jkp_PersonAssignmentRetreats
                                 where p.PAR_Assn_ID == new Guid(dlAssnAssignments.SelectedValue) && p.PAR_Per_ID == personIdToAddAssignmentTo && p.PAR_Ret_ID == RetreatId
                                 select p;
        if(parAlreadyExisting.Count() > 0)
            return;
        
        Monks.jkp_PersonAssignmentRetreat assignRetreat = new Monks.jkp_PersonAssignmentRetreat();
        assignRetreat.PAR_Assn_ID = new Guid(dlAssnAssignments.SelectedValue);
        assignRetreat.PAR_ID = Guid.NewGuid();
        assignRetreat.PAR_Per_ID = personIdToAddAssignmentTo;
        assignRetreat.PAR_Ret_ID = RetreatId;
        db.jkp_PersonAssignmentRetreats.InsertOnSubmit(assignRetreat);
        db.SubmitChanges();

        foreach(RepeaterItem rpPerson in rpAssignmentsForPeople.Items)
        {
            HiddenField hidPersonId = (HiddenField) rpPerson.FindControl("hidPersonId");
            if(hidPersonId.Value == personIdToAddAssignmentTo.ToString())
            {
                Repeater rpAssignments = (Repeater) hidPersonId.Parent.FindControl("rpAssignments");
                BindPersonActivities(rpAssignments, personIdToAddAssignmentTo);
                break;
            }
        }
         
        modalAssignPerson.Hide();
    }

    public void lbtnCreateAssignment_Clicked(object sender, EventArgs e)
    {

        modalCreateAssignment.Show();
    }

    public void btnAssnCancel_Clicked(object sender, EventArgs e)
    {
        modalAssignPerson.Hide();
    }

    public void btnCreateAssnCancel_Clicked(object sender, EventArgs e)
    {
        modalCreateAssignment.Hide();
    }



    public void btnCreateAssnSave_Clicked(object sender, EventArgs e)
    {
        Monks.jkp_Assignment assignment = new Monks.jkp_Assignment();
        assignment.Assn_ID = Guid.NewGuid();
        assignment.Assn_Name = txtCreateAssnName.Text;
        assignment.Assn_Description = txtDescription.Text;
        assignment.Assn_RetreatId = RetreatId;
        db.jkp_Assignments.InsertOnSubmit(assignment);
        db.SubmitChanges();

        modalCreateAssignment.Hide();

    }


}
