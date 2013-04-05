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

public partial class Retreat_Contributions : LoggedInPage
{
    MonkData db = new MonkData();
    public Guid RetreatId
    {
        get
        {
            return new Guid(Request.QueryString["retreatid"]);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {



    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!Page.IsPostBack)
        {
            LoadContributions();
        }
    }

    public void LoadContributions()
    {
        Monks.jkp_Retreat ret = db.jkp_Retreats.First(p => p.Ret_ID == RetreatId);
        lblRetreatName.Text = ret.Ret_Name;
        var contributions = from c in db.jkp_Contributions
                            where c.jkp_PersonAttendingRetreats.Where(p => p.PAR_RetId == RetreatId).Count() > 0
                            select c;
        rpContributions.DataSource = contributions;
        rpContributions.DataBind();
    }

    public void rpContributions_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Monks.jkp_Contribution contrib = (Monks.jkp_Contribution)e.Item.DataItem;
            Label lblPaidByUser = (Label)e.Item.FindControl("lblPaidByUser");
            Literal litDate = (Literal)e.Item.FindControl("litDate");
            Literal litPeoplePaidFor = (Literal)e.Item.FindControl("litPeoplePaidFor");
            Literal litAmountDue = (Literal)e.Item.FindControl("litAmountDue");
            Literal litAmountPaid = (Literal)e.Item.FindControl("litAmountPaid");
            LinkButton lbtnEditContribution = (LinkButton)e.Item.FindControl("lbtnEditContribution");
            LinkButton lbtnMarkAsPaid = (LinkButton)e.Item.FindControl("lbtnMarkAsPaid");
            Literal litRefundedAmount = (Literal)e.Item.FindControl("litRefundedAmount");
            Label litAmountRemainingToBePaid = (Label)e.Item.FindControl("litAmountRemainingToBePaid");
            LinkButton lbtnRefund = (LinkButton)e.Item.FindControl("lbtnRefund");
            lblPaidByUser.Text = contrib.jkp_Person.Per_FirstName + " " + contrib.jkp_Person.Per_LastName;
            litDate.Text = contrib.Cont_Date.Value.ToShortDateString();

            var peopleAttendingWithThisContribution = contrib.jkp_PersonAttendingRetreats;
            foreach (Monks.jkp_PersonAttendingRetreat par in peopleAttendingWithThisContribution)
            {
                if (!String.IsNullOrEmpty(litPeoplePaidFor.Text))
                    litPeoplePaidFor.Text += ", ";
                litPeoplePaidFor.Text += par.jkp_Person.Per_FirstName + " " + par.jkp_Person.Per_LastName;
            }

            litAmountDue.Text = contrib.Cont_TotalPrice.Value.ToString("0.00");
            litAmountPaid.Text = contrib.Cont_AmountPaid.Value.ToString("0.00");
            decimal amountRemainingToBePaid = (contrib.Cont_TotalPrice.Value - contrib.Cont_AmountPaid.Value);
            litAmountRemainingToBePaid.Text = amountRemainingToBePaid.ToString("0.00");

            if(amountRemainingToBePaid > 0)
            {
                litAmountRemainingToBePaid.Style.Add(HtmlTextWriterStyle.Color, "Red");
            }


            if (contrib.Cont_RefundedAmount.HasValue)
                litRefundedAmount.Text = contrib.Cont_RefundedAmount.Value.ToString("0.00");
            else
                litRefundedAmount.Text = "0.00";

            lbtnEditContribution.Attributes.Add("contid", contrib.Cont_ID.ToString());
            lbtnMarkAsPaid.Attributes.Add("contid", contrib.Cont_ID.ToString());
            lbtnRefund.Attributes.Add("contid", contrib.Cont_ID.ToString());

            if (contrib.Cont_RefundedAmount.HasValue)
                litRefundedAmount.Text = contrib.Cont_RefundedAmount.Value.ToString("0.00");
        }
    }

    public void btnMarkAsPaidSave_Clicked(object sender, EventArgs e)
    {
        Guid contribId = new Guid(btnMarkAsPaidSave.Attributes["contid"]);
        MonkData db = new MonkData();
        Monks.jkp_Contribution contrib = db.jkp_Contributions.First(p => p.Cont_ID == contribId);

        if (!contrib.Cont_AmountPaid.HasValue || contrib.Cont_AmountPaid.Value == 0)
            contrib.Cont_AmountPaid = decimal.Parse(txtAmountPaid.Text);
        else
            contrib.Cont_AmountPaid += decimal.Parse(txtAmountPaid.Text);

        db.SubmitChanges();
        modalMarkAsPaid.Hide();
        LoadContributions();
    }

    public void btnMarkAsPaidCancel_Clicked(object sender, EventArgs e)
    {
        modalMarkAsPaid.Hide();
    }

    public void lbtnMarkAsPaid_Clicked(object sender, EventArgs e)
    {
        LinkButton lbtnMarkAsPaid = (LinkButton)sender;
        Guid contribId = new Guid(lbtnMarkAsPaid.Attributes["contid"]);
        MonkData db = new MonkData();
        Monks.jkp_Contribution contrib = db.jkp_Contributions.First(p => p.Cont_ID == contribId);
        if (contrib.Cont_TotalPrice.HasValue)
            litMPAmountDue.Text = contrib.Cont_TotalPrice.Value.ToString("0.00");
        if (contrib.Cont_AmountPaid.HasValue)
            litMPAmountPaidSoFar.Text = contrib.Cont_AmountPaid.Value.ToString("0.00");
        btnMarkAsPaidSave.Attributes.Remove("contid");
        btnMarkAsPaidSave.Attributes.Add("contid", contribId.ToString());
        modalMarkAsPaid.Show();
    }

    public void lbtnRefund_Clicked(object sender, EventArgs e)
    {
        LinkButton lbtnRefund = (LinkButton)sender;
        var contrib = db.jkp_Contributions.SingleOrDefault(p => p.Cont_ID.ToString() == lbtnRefund.Attributes["contid"]);

        litAmountAvailableToRefund.Text = contrib.Cont_AmountPaid.Value.ToString("0.00");

        Button refundSave = (Button)btnRefundSave;
        refundSave.Visible = true;
        refundSave.Attributes.Remove("contid");
        refundSave.Attributes.Add("contid", contrib.Cont_ID.ToString());

        if (contrib.Cont_PaymentMethodTypeId == (int)Monks.Enums.PaymentMethodType.PayPalCreditCard)
            litContributionType.Text = "Credit Card";
        else
        {
            litContributionType.Text = "Cash/Check/Other";
            lblRefundErrors.Text = "Payment was not made with a credit card. Refund will be recorded in the system, but must be issued manually.";
        }

        modalRefundPayment.Show();
    }

    public void btnRefundSave_Clicked(object sender, EventArgs e)
    {
        Button refundButton = (Button)sender;
        var contrib = db.jkp_Contributions.SingleOrDefault(p => p.Cont_ID.ToString() == refundButton.Attributes["contid"]);

        if (!contrib.Cont_RefundedAmount.HasValue || contrib.Cont_RefundedAmount.Value == 0)
            contrib.Cont_RefundedAmount = decimal.Parse(txtRefundAmount.Text);
        else
            contrib.Cont_RefundedAmount += decimal.Parse(txtRefundAmount.Text);

        // if contribution was made by credit card through paypal and is less than a year old, call paypal API and issue a refund with the stored transaction ID
        if (contrib.Cont_PaymentMethodTypeId.Value == (int)Monks.Enums.PaymentMethodType.PayPalCreditCard && (contrib.Cont_Date.Value.AddYears(1) > DateTime.Now))
        {
            string errors = "";
            if (DoPaypalRefund(ref contrib, contrib.Cont_Transaction_ID.ToString(), ref errors))
            {
                contrib.Cont_AmountPaid -= contrib.Cont_RefundedAmount;
                db.SubmitChanges();
                LoadContributions();
                modalRefundPayment.Hide();
                return;
            }
            else
                lblRefundErrors.Text = errors;
        }
        else
        {
            contrib.Cont_AmountPaid -= contrib.Cont_RefundedAmount;
            db.SubmitChanges();
            LoadContributions();
        }
    }

    public void btnRefundCancel_Clicked(object sender, EventArgs e)
    {
        modalRefundPayment.Hide();
    }

    public void lbtnEditContribution_Clicked(object sender, EventArgs e)
    {
        LinkButton editContrib = (LinkButton)sender;
        var contrib = db.jkp_Contributions.SingleOrDefault(p => p.Cont_ID.ToString() == editContrib.Attributes["contid"]);

        contributionAmount.Text = contrib.Cont_TotalPrice.Value.ToString("0.00");
        btnEditContribSave.Attributes.Remove("contid");
        btnEditContribSave.Attributes.Add("contid", contrib.Cont_ID.ToString());
        modalEditContribution.Show();
    }

    public void btnEditContribSave_Clicked(object sender, EventArgs e)
    {
        Button editContribSave = (Button)sender;
        var contrib = db.jkp_Contributions.SingleOrDefault(p => p.Cont_ID.ToString() == editContribSave.Attributes["contid"]);

        contrib.Cont_TotalPrice = decimal.Parse(txtEditContribAmount.Text);
        db.SubmitChanges();
        LoadContributions();
    }

    public void btnEditContribCancel_Clicked(object sender, EventArgs e)
    {
        modalEditContribution.Hide();
    }

    private static bool DoPaypalRefund(ref Monks.jkp_Contribution contrib, string transID, ref string errors)
    {
        string returnedID;
        return PaypalHelpers.DoRefund(transID, PayPalServices.RefundType.Partial, contrib.Cont_RefundedAmount.Value, "", out returnedID, ref errors);
    }
}
