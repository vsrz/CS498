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

public partial class Retreat_AddEdit : LoggedInPage
{
    public Guid? RetreatId
    {
        get
        {
            if (String.IsNullOrEmpty(Request.QueryString["retreatid"]))
            {
                return null;
            }
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
        if (!Page.IsPostBack)
        {
            var sites = db.jkp_Sites;
            dlSite.DataSource = sites;
            dlSite.DataTextField = "Site_EnglishName";
            dlSite.DataValueField = "Site_ID";
            dlSite.DataBind();
            ListItem blankItem = new ListItem("No Site Selected");
            dlSite.Items.Insert(0, blankItem);

            LoadDate(db);
        }
    }

    private void LoadDate(MonkData db)
    {
        if (RetreatId != null)
        {
            Monks.jkp_Retreat retreat = db.jkp_Retreats.First(p => p.Ret_ID == RetreatId);
            txtName.Text = retreat.Ret_Name;

            //txtArrivalTime.Text = (retreat.Ret_ArrivalTime).ToString("h:mm tt");

            //txtDepartureTime.Text =  retreat.Ret_DepartureTime.ToString("h:mm tt");

            if (retreat.Ret_EndDate != null)
                txtEndDate.Text = ((DateTime)retreat.Ret_EndDate).ToString("d"); // using "d" on a datetime object gives only the date as MM/DD/YYYY
            if (retreat.Ret_StartDate != null)
                txtStartDate.Text = ((DateTime)retreat.Ret_StartDate).ToString("d");
            txtDescription.Text = retreat.Ret_Description;
            //txtLanguage.Text = retreat.jkp_Language.Lang_DisplayName;
            foreach (ListItem lItem in dlSite.Items)
            {
                if (lItem.Value == retreat.Ret_Site_ID.ToString())
                    lItem.Selected = true;
            }

            hlEditRooms.NavigateUrl = "Retreat-AddEditRooms.aspx?retreatid=" + retreat.Ret_ID.ToString();
            hlEditRooms.Visible = true;

            hlAssignedRoomsToPeople.Visible = true;
            hlAssignedRoomsToPeople.NavigateUrl = "Retreat-AssignRooms.aspx?retreatid=" + retreat.Ret_ID.ToString();
        }
        else
        {
            var languages = db.jkp_Languages;

            foreach (Monks.jkp_Language lang in languages)
            {
                dlLanguage.Items.Add(new ListItem(lang.Lang_DisplayName.ToString(), lang.Lang_ID.ToString()));
            }
        }
    }

    public void btnSave_Clicked(object sender, EventArgs e)
    {
        MonkData db = new MonkData();
        Monks.jkp_Retreat retreat;
        if(RetreatId != null)
        {
            retreat = db.jkp_Retreats.First(p=>p.Ret_ID == RetreatId);
        }
        else
        {
            retreat = new Monks.jkp_Retreat();
            retreat.Ret_ID = Guid.NewGuid();
        }

        //if(!String.IsNullOrEmpty(txtArrivalTime.Text))        
        //    retreat.Ret_ArrivalTime = DateTime.Parse(txtArrivalTime.Text);
        //if(!String.IsNullOrEmpty(txtDepartureTime.Text))
        //    retreat.Ret_DepartureTime = DateTime.Parse(txtDepartureTime.Text);
        if(!String.IsNullOrEmpty(txtEndDate.Text))
            retreat.Ret_EndDate = DateTime.Parse(txtEndDate.Text);
        if(!String.IsNullOrEmpty(txtStartDate.Text))
            retreat.Ret_StartDate = DateTime.Parse(txtStartDate.Text);            
        retreat.Ret_Name = txtName.Text.Trim();
        retreat.Ret_Description = txtDescription.Text;
        if(!String.IsNullOrEmpty(dlSite.SelectedValue))
            retreat.Ret_Site_ID = new Guid( dlSite.SelectedValue);

        var selectedLanguage = db.jkp_Languages.SingleOrDefault(p => p.Lang_ID.ToString() == dlLanguage.SelectedValue);
        retreat.Ret_LanguageId = selectedLanguage.Lang_ID;
        Request.QueryString.Add("retreatid", retreat.Ret_ID.ToString());

        if(RetreatId == null)
            db.jkp_Retreats.InsertOnSubmit(retreat);
        db.SubmitChanges();
        mvAddEdit.ActiveViewIndex = 1;    
    }

   
}
