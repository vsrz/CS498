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

public partial class Account_Home : LoggedInPage
{
    MonkData db = new MonkData();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Add("UserId", UserId);
        Monks.jkp_Person jPerson = db.aspnet_Memberships.First(p => p.UserId == UserId).jkp_Person;

        Session.Add("PersonId", jPerson.Per_ID);
        Session.Add("PersonAddressId", jPerson.Per_Add_ID.Value.ToString());
        if (jPerson.jkp_Address == null)
        {
            Monks.jkp_Address address = new Monks.jkp_Address();
            address.Add_ID = Guid.NewGuid();
            db.jkp_Addresses.InsertOnSubmit(address);
            jPerson.jkp_Address = address;
            db.SubmitChanges();
            jPerson = db.aspnet_Memberships.First(p => p.UserId == UserId).jkp_Person;
        }

        //get list of PCR tuples matching the person ID
        var userAttendingRetreatList = from u in db.jkp_PersonAttendingRetreats
                                       where u.PAR_PersonId == jPerson.Per_ID
                                       select u;

        List<Monks.jkp_Retreat> retreats = new List<Monks.jkp_Retreat>();
        foreach (Monks.jkp_PersonAttendingRetreat retreat in userAttendingRetreatList)
        {
            retreats.Add(retreat.jkp_Retreat);
        }



        rpRetreats.DataSource = retreats;
        rpRetreats.DataBind();

        var userInRoles = db.aspnet_UsersInRoles.Where(p => p.UserId == UserId);

        if (userInRoles.Count() == 0)
            fieldsetRoles.Visible = false;
        else
        {
            rpRolesIn.DataSource = userInRoles;
            rpRolesIn.DataBind();
        }

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        Monks.jkp_Person jPerson = db.aspnet_Memberships.First(p => p.UserId == UserId).jkp_Person;


        DetailsView2.DataBind();
        DropDownList dlGender = (DropDownList)DetailsView2.FindControl("genderSelection");
        if (dlGender != null)
            if (jPerson.Per_Gender == 'M')
                dlGender.SelectedIndex = 0;
            else
                dlGender.SelectedIndex = 1;

        detailsAddress.DataBind();

        DropDownList dlSanghas = (DropDownList)detailsAddress.FindControl("dlSanghas");

        dlSanghas.DataSource = db.jkp_Sanghas;
        dlSanghas.DataTextField = "San_Name";
        dlSanghas.DataValueField = "San_ID";
        dlSanghas.DataBind();


        // Select the correct sangha if we have an address and we have a sangha assigned.
        if (dlSanghas.Items.Count > 0 && jPerson.jkp_Address != null && jPerson.jkp_Address.Add_San_ID.HasValue)
        {
            foreach (ListItem listItemsSangahas in dlSanghas.Items)
            {
                if (listItemsSangahas.Value == jPerson.jkp_Address.Add_San_ID.Value.ToString())
                {
                    listItemsSangahas.Selected = true;
                    break;
                }
            }
        }


    }

    public void rpRolesIn_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Monks.aspnet_UsersInRole userInRole = (Monks.aspnet_UsersInRole)e.Item.DataItem;
            Literal litRole = (Literal)e.Item.FindControl("litRole");
            litRole.Text = userInRole.aspnet_Role.RoleName;

        }
    }

    public void DetailsView2_OnItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
    {

        DropDownList dlGender = (DropDownList)DetailsView2.FindControl("genderSelection");
        string valueOfGender = dlGender.SelectedItem.Text;

        MonkData db = new MonkData();

        Monks.jkp_Person jPerson = db.aspnet_Memberships.First(p => p.UserId == UserId).jkp_Person;
        jPerson.Per_Gender = valueOfGender[0];
        db.SubmitChanges();

        DetailsView2.DataBind();




    }

    public void detailsAddress_OnItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
    {
        DropDownList dlSanghas = (DropDownList)detailsAddress.FindControl("dlSanghas");
        string idStringOfSangha = dlSanghas.SelectedValue;
        MonkData db = new MonkData();

        Monks.jkp_Person jPerson = db.aspnet_Memberships.First(p => p.UserId == UserId).jkp_Person;
        jPerson.jkp_Address.Add_San_ID = new Guid(idStringOfSangha);
        db.SubmitChanges();

        detailsAddress.DataBind();
    }


}
