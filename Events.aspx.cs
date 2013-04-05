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

public partial class Events : DefaultPage
{


    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Add("CurrentDateTime", DateTime.Now);
    }

    protected override void OnPreRender(EventArgs e)
    {
        
        base.OnPreRender(e);
     
        if (!Page.IsPostBack)
        {
            // Load the dropdown list of sites before the GridView_UpdateDisplay() function is called
            // because it relies on there being something in the list of sites.
            ddlEventsSites.DataBind();
            ddlEventsSites.Items.Insert(0, ""); // default value, displays events from all sites first
            txtEventDate.Text = DateTime.Now.ToShortDateString();
            EventsListing_UpdateDisplay();
        }

    }

    public void EventsListing_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Monks.jkp_Retreat ret = (Monks.jkp_Retreat)e.Item.DataItem;
            HyperLink lblEventName = (HyperLink)e.Item.FindControl("lblEventName");
            Label lblStartDate = (Label)e.Item.FindControl("lblStartDate");
            Label lblEndDate = (Label)e.Item.FindControl("lblEndDate");
            Label lblDescription = (Label)e.Item.FindControl("lblDescription");

            // Hyperlink to Event Registration
            lblEventName.Text = ret.Ret_Name;
            lblEventName.NavigateUrl += ret.Ret_ID.ToString();

            // Start Date
            lblStartDate.Text = ret.Ret_StartDate.ToShortDateString();

            // End Date
            lblEndDate.Text = ret.Ret_EndDate.ToShortDateString();

            // Event Description
            lblDescription.Text = ShortenString(RemoveHTMLFromText(ret.Ret_Description.ToString()), 250);

        }
    }

    private void EventsListing_UpdateDisplay()
    {
        MonkData db = new MonkData();
        DateTime d = DateTime.Parse(txtEventDate.Text.ToString());

        System.Linq.IQueryable<Monks.jkp_Site> sites;
        System.Linq.IQueryable<Monks.jkp_Retreat> events;
        // if no site selected from dropdown, display events from all sites
        if (ddlEventsSites.SelectedItem.Text == "")
        {
            events = from e in db.jkp_Retreats
                     where e.Ret_EndDate >= d
                     orderby e.Ret_StartDate ascending
                     select e;
        }
        // List of retreats for selected site starting on DateSelected
        else
        {
            // List of sites
            sites = from q in db.jkp_Sites
                    where q.Site_EnglishName == ddlEventsSites.Text
                    select q;

            if (sites.Count() < 1)
            {
                throw new Exception("Not enough sites.");
            }

            events = from e in db.jkp_Retreats
                     where e.Ret_EndDate >= d && e.Ret_Site_ID == sites.First().Site_ID
                     orderby e.Ret_StartDate ascending
                     select e;
        }

        if (events.Count() == 0)
        {
            updBottom.Visible = false;
            litNoResultsReturned.Visible = true;
            return;
        }
        if (events.Count() > 0)
        {
            rpEventsListing.DataSource = events;
            rpEventsListing.DataBind();
        }
        updBottom.Visible = true;
        litNoResultsReturned.Visible = false;


    }

    public void txtEventDate_OnTextChanged(object sender, EventArgs e)
    {
        DateTime result;
        if (DateTime.TryParse(txtEventDate.Text, out result) && result >= DateTime.Now)
        {
            litEventDateError.Text = "";
            EventsListing_UpdateDisplay();
            return;
        }
        else
        {
            litEventDateError.Text = "Incorrect date specified.";
            txtEventDate.Text = DateTime.Now.ToShortDateString();
            EventsListing_UpdateDisplay();
            return;
        }

    }


    public void ddlEventsSites_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        EventsListing_UpdateDisplay();

    }


}
