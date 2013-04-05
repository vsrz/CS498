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
using System.Reflection;

public partial class Retreat_ViewRetreatants : LoggedInPage
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
    MonkData db = new MonkData();
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        
        var retreatants = from d in db.jkp_PersonAttendingRetreats
                          where d.PAR_RetId == RetreatId
                          orderby d.PAR_ContributionId descending
                          select new
                          {
                              Person_Name = d.jkp_Person.Per_FirstName + " " + d.jkp_Person.Per_LastName,
                             
                              //Email = d.jkp_Person.Per_Email,
                              Hamlet = d.jkp_Room.jkp_Building.jkp_Hamlet.Ham_LocalName,
                              Building = d.jkp_Room.jkp_Building.Bld_LocalName,
                              Room = d.jkp_Room.Rm_Name,
                              Contribution = "$" + d.jkp_Contribution.Cont_AmountPaid.ToString(),
                              //Currency = d.jkp_Contribution.Cont_Currency,
                              Contrib_Date = d.jkp_Contribution.Cont_Date,
                              
                              Contrib_Paid_By = d.jkp_Contribution.jkp_Person.Per_FirstName + " " + d.jkp_Contribution.jkp_Person.Per_LastName,
                              //Contributor_Email = d.jkp_Contribution.jkp_Person.Per_Email,
                              Payment_Method =  Enum.ToObject(typeof(Monks.Enums.PaymentMethodType),  (int)d.jkp_Contribution.Cont_PaymentMethodTypeId).ToString()
                          };
        
        //gvRetreatants.DataSource = retreatants;        
        //gvRetreatants.DataBind();

        
        

        
        
    }

    public void gvRets_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            var item = e.Row.DataItem;
            Controls_PersonDetails perDetails = (Controls_PersonDetails)e.Row.FindControl("userDetails");
            Controls_PersonDetails perContrib = (Controls_PersonDetails)e.Row.FindControl("userContribPaidBy");
            Label lblPaymentType = (Label)e.Row.FindControl("lblPaymentType");
            perDetails.PersonId = item.GetType().GetProperty("PAR_PersonId").GetValue(item, null).ToString();
            perDetails.LoadData(db);
            PropertyInfo contribPropType = item.GetType().GetProperty("jkp_Contribution");
            var contrib = contribPropType.GetValue(item, null);
            perContrib.PersonId = contrib.GetType().GetProperty("Cont_Off_Per_ID").GetValue(contrib, null).ToString();
            perContrib.LoadData(db);

            int valueOfContribTypeId = (int)item.GetType().GetProperty("ContributionTypeId").GetValue(item, null);
            lblPaymentType.Text = Enum.ToObject(typeof(Monks.Enums.PaymentMethodType),  valueOfContribTypeId).ToString();
            
            
        }
    }
}
