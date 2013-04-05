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

public partial class Address_AddEdit : System.Web.UI.Page
{
    public Guid? AddressId
    {
        get
        {
            if (String.IsNullOrEmpty(Request.QueryString["addressid"]))
            {
                return null;
            }
            return new Guid(Request.QueryString["addressid"]);
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
            LoadDate(db);
        }
    }

    private void LoadDate(MonkData db)
    {
        if (AddressId != null)
        {
            Monks.jkp_Address address = db.jkp_Addresses.First(p => p.Add_ID == AddressId);
            txtAddress.Text = address.Add_Street;
            txtCity.Text = address.Add_City;
            txtCountry.Text = address.Add_Country;
            txtState.Text = address.Add_State;
            txtZip.Text = address.Add_PostalCode;
            
        }
    }

    public void btnSave_Clicked(object sender, EventArgs e)
    {
        MonkData db = new MonkData();
        Monks.jkp_Address address;
        if(AddressId != null)
        {
            address = db.jkp_Addresses.First(p=>p.Add_ID == AddressId);
        }
        else
        {
            address = new Monks.jkp_Address();
            address.Add_ID = Guid.NewGuid();
        }
        address.Add_Street = txtAddress.Text;
        address.Add_State = txtState.Text;
        address.Add_PostalCode = txtZip.Text;
        address.Add_Country = txtCountry.Text;
        address.Add_City = txtCity.Text;       
        
        if(AddressId == null)
            db.jkp_Addresses.InsertOnSubmit(address);
        db.SubmitChanges();
        mvAddEdit.ActiveViewIndex = 1;    
    }
}
