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

public partial class Controls_PersonDetails : System.Web.UI.UserControl
{
    /// <summary>
    /// Should be a guid as a string.
    /// </summary>    
    public string PersonId
    {
        get;
        set;
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (String.IsNullOrEmpty(PersonId))
            throw new Exception("Must fill in the MembershipUserId property on the PersonDetails control");

        if (!Page.IsPostBack)
        {
            if (loadDataCalled == false)
                LoadData(new MonkData());

        }
    }

    bool loadDataCalled = false;

    /// <summary>
    /// Must call this function.
    /// </summary>
    /// <param name="db"></param>
    public void LoadData(MonkData db)
    {
        loadDataCalled = true;

        Monks.jkp_Person siteUser = db.jkp_Persons.First(p => p.Per_ID == new Guid(PersonId));
        if (siteUser.aspnet_Memberships.Count > 0)
        {
            litEmailAddress.Text = siteUser.aspnet_Memberships.First().Email;
            lbtnButtonToDisplayModal.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
        }

        litPersonName.Text = siteUser.Per_FirstName + " " + siteUser.Per_LastName;
        lbtnButtonToDisplayModal.Text = litPersonName.Text;
        litCellPhoneNumber.Text = siteUser.Per_CellPhoneNumber;
        litHomePhoneNumber.Text = siteUser.Per_HomePhoneNumber;
        if(siteUser.jkp_Address != null)
            litAddress.Text = siteUser.jkp_Address.Add_Street + ", " + siteUser.jkp_Address.Add_City + ", " + siteUser.jkp_Address.Add_State + " " + siteUser.jkp_Address.Add_PostalCode;
        


    }
}
