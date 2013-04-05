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
using System.Net;
using System.Net.Mail;

public partial class Event_Registration : LoggedInPage
{

    MonkData db = new MonkData();       //global db var

    //discounts by age 
    const double zero_five = 0;
    const double six_nine = 0.25;
    const double ten_fourteen = 0.5;
    const double fifteen_nineteen = 0.75;

    public Guid RetreatId
    {
        get
        {
            //do not allow access to registration without a retreat id in the querystring - redirect to events page
            if (String.IsNullOrEmpty(Request.QueryString["retreatid"]))
                Response.Redirect("events.aspx");
            return new Guid(Request.QueryString["retreatid"]);
        }
    }

    /* 
     * this internal class is used to fill the review step with a 
     * list of people that were checked off during the first step
     * of the registration process
    */
    internal class PastPerson
    {
        public String Person_fName { get; set; }
        public String Person_lName { get; set; }
        public int Person_age { get; set; }
        public DateTime Person_birthdate { get; set; }

        public PastPerson(String _person_fName, String _person_lName, int _person_age)
        {
            Person_fName = _person_fName;
            Person_lName = _person_lName;
            Person_age = _person_age;
        }

        public PastPerson(String _person_fName, String _person_lName, int _person_age, DateTime _person_birthdate)
        {
            Person_fName = _person_fName;
            Person_lName = _person_lName;
            Person_age = _person_age;
            Person_birthdate = _person_birthdate;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!Page.IsPostBack)
        {
            if (bool.Parse(ConfigurationManager.AppSettings["BillingDevMode"]))
            {
                txtCardNum.Text = "4165357162681594";
                dlCardChoice.SelectedValue = "Visa";
                txtCardExpir.Text = "06/08/2018";
                txtBillingAddress.Text = "1 Main St";
                txtBillingCity.Text = "San Jose";
                txtBillingState.Text = "CA";
                txtBillingZip.Text = "95131";
                txtCardFirstName.Text = "Paul";
                txtCardLastName.Text = "Mendoza";
                txtBillingCountry.Text = "US";
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        var user = (from v in db.aspnet_Users
                    where v.UserId == UserId
                    select v).Single();

        if ((from t in db.jkp_PersonAttendingRetreats
             where t.PAR_RetId == RetreatId && t.PAR_PersonId == user.aspnet_Membership.jkp_Person.Per_ID
             select t).Count() > 0) //user already registered for this retreat
        {
            h3retreatName.Visible = false;
            registrationView.Visible = false;

            lblRetreatError.Text = "You have already registered for this retreat.";
            redirectLink.Visible = true;
            lblRetreatError.Visible = true;
        }
        else
        {
            if (!Page.IsPostBack)
            {
                var retreat = db.jkp_Retreats.First(p => p.Ret_ID == RetreatId);

                lblRetreatName.Text = retreat.Ret_Name;
                retStart.Text = retreat.Ret_StartDate.Date.ToShortDateString();
                retEnd.Text = retreat.Ret_EndDate.Date.ToShortDateString();

                formUserName.InnerText = user.aspnet_Membership.jkp_Person.Per_FirstName + " " + user.aspnet_Membership.jkp_Person.Per_LastName;

                PastRetreatantsChecked(sender, e);
                for (int i = 0; i < 20; i++)
                {
                    dlHowManyPeople.Items.Add(new ListItem(i.ToString()));
                }
            }
        }
    }

    //functions to switch between views and process events
    public void ChangeToView0(object sender, EventArgs e)
    {
        registrationView.ActiveViewIndex = 0;
    }
    public void ChangeToView1(object sender, EventArgs e)
    {
        SetRoomTypes(sender, e);
        SetReviewPersonList(sender, e);
        CalculateFamilyTotal(sender, e);
        registrationView.ActiveViewIndex = 1;
    }
    public void ChangeToView2(object sender, EventArgs e)
    {
        AppendCostInfoReviewRepeater(sender, e);
        registrationView.ActiveViewIndex = 2;
    }
    public void ChangeToView3(object sender, EventArgs e)
    {
        registrationView.ActiveViewIndex = 3;
    }
    public void ChangeToView4(object sender, EventArgs e)
    {
        RegisterRetreatant(sender, e);
        registrationView.ActiveViewIndex = 4;
    }

    public void SetReviewPersonList(object sender, EventArgs e)
    {
        List<PastPerson> reviewRetreatants = new List<PastPerson>();

        var user = (from u in db.aspnet_Users
                    where u.UserId == UserId
                    select u).Single();

        if (user.aspnet_Membership.jkp_Person.Per_Birthdate.HasValue)
        {
            reviewRetreatants.Add(new PastPerson(user.aspnet_Membership.jkp_Person.Per_FirstName,
                                                 user.aspnet_Membership.jkp_Person.Per_LastName,
                                                 (DateTime.Now.Year - DateTime.Parse(user.aspnet_Membership.jkp_Person.Per_Birthdate.ToString()).Year),
                                                 DateTime.Parse(user.aspnet_Membership.jkp_Person.Per_Birthdate.ToString())));
        }
        else
        {
            reviewRetreatants.Add(new PastPerson(user.aspnet_Membership.jkp_Person.Per_FirstName.ToString(),
                                                 user.aspnet_Membership.jkp_Person.Per_LastName.ToString(),
                                                 21,
                                                 new DateTime(1980, 1,1)));
        }

        /*  
         * iterate through the past person repeater and add those with checkboxes
         * next to their name to the list of people which is used as the data
         * source for the review step
        */
        PastPerson person;
        foreach (RepeaterItem rpItem in rpPastRetreatants.Items)
        {
            CheckBox cb = (CheckBox)rpItem.FindControl("pastPersonCheckBox");
            if (cb.Checked)    //only add people to the list who have been checked
            {
                Literal per_fName = (Literal)rpItem.FindControl("pastPersonFirstName");
                Literal per_lName = (Literal)rpItem.FindControl("pastPersonLastName");
                int per_age = int.Parse(cb.Attributes["per_age"]);
                DateTime per_birthdate = DateTime.Parse(cb.Attributes["per_birthdate"]);

                person = new PastPerson(per_fName.Text, per_lName.Text, per_age, per_birthdate);
                reviewRetreatants.Add(person);
            }
        }

        foreach (RepeaterItem newPerson in rpAdditionalRetreatants.Items)
        {
            TextBox newPer_fName = (TextBox)newPerson.FindControl("txtAddFirstName");
            TextBox newPer_lName = (TextBox)newPerson.FindControl("txtAddLastName");
            TextBox newPer_birthdate = (TextBox)newPerson.FindControl("txtAddBirthdate");

            if (newPer_fName.Text != "" && newPer_lName.Text != "" && newPer_birthdate.Text != "")
            {
                person = new PastPerson(newPer_fName.Text, newPer_lName.Text, (DateTime.Now.Year - DateTime.Parse(newPer_birthdate.Text).Year), DateTime.Parse(newPer_birthdate.Text));
                reviewRetreatants.Add(person);
            }
        }

        if (reviewRetreatants.Count() > 0)
        {
            reviewPersonRepeater.DataSource = reviewRetreatants;
            roomPreferenceRepeater.DataSource = reviewRetreatants;
            reviewPersonRepeater.DataBind();
            roomPreferenceRepeater.DataBind();
        }
    }

    List<Monks.jkp_RoomType> roomTypes = new List<Monks.jkp_RoomType>();
    public void SetRoomTypes(object sender, EventArgs e)
    {
        var retreat = (from a in db.jkp_Retreats
                       where a.Ret_ID == RetreatId
                       select a).Single();

        // get all of the rooms at the retreat site. 
        var roomsAtRetreatSite = from t in db.jkp_Rooms
                                 where t.jkp_Building.jkp_Hamlet.jkp_Site.Site_ID == retreat.Ret_Site_ID
                                 select t;

        // get all of the rooms that exist during the retrat
        var roomsDuringTheRetreat = from r in roomsAtRetreatSite
                                    where r.Rm_ContructedDate <= retreat.Ret_StartDate && r.Rm_DestroyedDate >= retreat.Ret_EndDate
                                    select r;

        // get rooms that still have space in them.
        var roomsWithSpace = from r in roomsDuringTheRetreat
                             where r.jkp_PersonAttendingRetreats.Where(p => (p.PAR_ArrivalTime <= retreat.Ret_StartDate && p.PAR_DepartureTime >= retreat.Ret_EndDate)
                                 || (p.PAR_ArrivalTime <= retreat.Ret_StartDate && p.PAR_DepartureTime <= retreat.Ret_EndDate)
                                 || (p.PAR_ArrivalTime >= retreat.Ret_StartDate && p.PAR_DepartureTime >= retreat.Ret_EndDate)).Count() < r.Rm_TotalCapacity
                             select r;
        // group rooms by room type
        var roomTypeGrouping = from r in roomsWithSpace
                               where r.Rm_RmType_ID != null
                               group r by r.Rm_RmType_ID into g
                               select g;

        foreach (IGrouping<Guid?, Monks.jkp_Room> roomsWithType in roomTypeGrouping)
        {
            Monks.jkp_RoomType roomType = roomsWithType.First().jkp_RoomType;
            roomTypes.Add(roomType);
        }

        roomTypeRepeater.DataSource = roomTypes;
        roomTypeRepeater.DataBind(); //triggers roomTypeRepeaterBound function below        
    }

    public void roomTypeRepeaterBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Label roomType = (Label)e.Item.FindControl("lblRoomType");
            Label AC = (Label)e.Item.FindControl("lblAC");
            Label bathroom = (Label)e.Item.FindControl("lblBathroom");
            Label heating = (Label)e.Item.FindControl("lblHeating");
            Label bunkBed = (Label)e.Item.FindControl("lblBunkBed");
            Label singleBed = (Label)e.Item.FindControl("lblSingleBed");
            Label shower = (Label)e.Item.FindControl("lblShower");
            Label costPerNight = (Label)e.Item.FindControl("lblCostPerNight");
            Label famtotalCost = (Label)e.Item.FindControl("lblTotalCost");

            Monks.jkp_RoomType rt = (Monks.jkp_RoomType)e.Item.DataItem;

            roomType.Text = rt.RmType_Name;

            if ((bool)rt.RmType_AirCondition) AC.Text = "yes"; else AC.Text = "no";
            if ((bool)rt.RmType_Bathroom) bathroom.Text = "yes"; else bathroom.Text = "no";
            if ((bool)rt.RmType_Heating) heating.Text = "yes"; else heating.Text = "no";
            if ((bool)rt.RmType_UpperBunkBed || (bool)rt.RmType_LowerBunkBed) bunkBed.Text = "yes"; else bunkBed.Text = "no";
            if ((bool)rt.RmType_SingleBed) singleBed.Text = "yes"; else singleBed.Text = "no";
            if ((bool)rt.RmType_Shower) shower.Text = "yes"; else shower.Text = "no";

            costPerNight.Text = "$" + rt.jkp_Rate.Rt_BaseAmount.ToString();
        }
    }

    public void PersonRoomPrefRepeater(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Label personName = (Label)e.Item.FindControl("roomTypePrefPerson");
            Label lblNights = (Label)e.Item.FindControl("roomTypePrefNumberOfNights");
            DropDownList dlRoomPref = (DropDownList)e.Item.FindControl("dlRoomTypePref");
            TextBox arrivalDate = (TextBox)e.Item.FindControl("roomTypePrefArrivalDate");
            TextBox departureDate = (TextBox)e.Item.FindControl("roomTypePrefDepartureDate");

            PastPerson person = (PastPerson)e.Item.DataItem;
            personName.Text = person.Person_fName + " " + person.Person_lName;

            foreach (Monks.jkp_RoomType roomType in roomTypes)
            {
                int age = person.Person_age;
                double discountRate;

                if (age < 0)
                    discountRate = 1;
                else if (age <= 5)
                    discountRate = double.Parse(ConfigurationManager.AppSettings["Discount0to5"]);                
                else if (age <= 12)
                    discountRate = double.Parse(ConfigurationManager.AppSettings["Discount6to12"]);
                else if (age <= 19)
                    discountRate = double.Parse(ConfigurationManager.AppSettings["Discount13to19"]);
                else
                    discountRate = 1;
                
                var retreat = db.jkp_Retreats.First(p => p.Ret_ID == RetreatId);
                // get global rate modifier for retreat
                double retreatPriceModifier;
                if (retreat.Ret_GlobalRateModifier != null && retreat.Ret_GlobalRateModifier != 0)
                    retreatPriceModifier = double.Parse(retreat.Ret_GlobalRateModifier.ToString());
                else
                    retreatPriceModifier = 1;

                arrivalDate.Text = retreat.Ret_StartDate.ToShortDateString();
                departureDate.Text = retreat.Ret_EndDate.Date.ToShortDateString();
                int numNights = (retreat.Ret_EndDate - retreat.Ret_StartDate).Days;
                lblNights.Text = numNights.ToString();

                personName.Attributes.Add("per_discountRate", discountRate.ToString());
                double personPrice = double.Parse(roomType.jkp_Rate.Rt_BaseAmount.ToString()) * discountRate * retreatPriceModifier;
                dlRoomPref.Items.Add(new ListItem(roomType.RmType_Name.ToString() + " ($" + personPrice.ToString("0.00") + ") ",
                                                    roomType.RmType_ID.ToString()));
            }
        }
    }

    public void CalculateFamilyTotal(object sender, EventArgs e)
    {
        double familyTotalCost = 0;
        foreach (RepeaterItem rp in roomPreferenceRepeater.Items)
        {
            Label person = (Label)rp.FindControl("roomTypePrefPerson");
            TextBox arrivalDate = (TextBox)rp.FindControl("roomTypePrefArrivalDate");
            TextBox departureDate = (TextBox)rp.FindControl("roomTypePrefDepartureDate");
            Label numberNights = (Label)rp.FindControl("roomTypePrefNumberOfNights");
            DropDownList dlRoomPreference = (DropDownList)rp.FindControl("dlRoomTypePref");

            var roomType = db.jkp_RoomTypes.SingleOrDefault(p => p.RmType_ID.ToString() == dlRoomPreference.SelectedValue);
            if (roomType == null)
                throw new Exception("No rooms exist for this retreat. Please contact an administrator");
            int numNights = (DateTime.Parse(departureDate.Text) - DateTime.Parse(arrivalDate.Text)).Days;
            numberNights.Text = numNights.ToString();

            familyTotalCost += double.Parse(roomType.jkp_Rate.Rt_BaseAmount.ToString()) * double.Parse(person.Attributes["per_discountRate"].ToString()) * numNights;
        }
        estTotalCost.Text = familyTotalCost.ToString("0.00");
    }

    public void AppendCostInfoReviewRepeater(object sender, EventArgs e)
    {
        foreach (RepeaterItem rp in reviewPersonRepeater.Items)
        {
            Label perName = (Label)rp.FindControl("personNameList");
            Label roomPref = (Label)rp.FindControl("personRoomPref");
            Label pricePerNight = (Label)rp.FindControl("personPricePerNight");
            Label discount = (Label)rp.FindControl("personDiscount");
            Label arrival = (Label)rp.FindControl("personArrival");
            Label departure = (Label)rp.FindControl("personDeparture");
            Label numNights = (Label)rp.FindControl("personNumNights");            
            Label total = (Label)rp.FindControl("personTotal");
            int age = int.Parse(perName.Attributes["per_age"]);

            foreach (RepeaterItem rpItem in roomPreferenceRepeater.Items)
            {
                Label person = (Label)rpItem.FindControl("roomTypePrefPerson");
                TextBox arrivalDate = (TextBox)rpItem.FindControl("roomTypePrefArrivalDate");
                TextBox departureDate = (TextBox)rpItem.FindControl("roomTypePrefDepartureDate");
                DropDownList dlRoomPref = (DropDownList)rpItem.FindControl("dlRoomTypePref");

                if (perName.Text.ToString() == person.Text.ToString())
                {
                    var selectedRoomType = (from t in db.jkp_RoomTypes
                                            where t.RmType_ID.ToString() == dlRoomPref.SelectedValue
                                            select t).Single();

                    arrival.Text = arrivalDate.Text;
                    departure.Text = departureDate.Text;

                    int nights = (DateTime.Parse(departureDate.Text) - DateTime.Parse(arrivalDate.Text)).Days;
                    numNights.Text = nights.ToString();
                    roomPref.Text = selectedRoomType.RmType_Name.ToString();
                    roomPref.Attributes.Add("roomTypeID", selectedRoomType.RmType_ID.ToString());
                    pricePerNight.Text = "$" + selectedRoomType.jkp_Rate.Rt_BaseAmount.ToString();
                }
            }

            double disc;

            if (age < 0)
            {
                discount.Text = "N/A";
                disc = 1;
            }
            else if (age <= 5)
            {
                discount.Text = "100%";
                disc = double.Parse(ConfigurationManager.AppSettings["Discount0to5"]);
            }
            else if (age <= 12)
            {
                discount.Text = "50%";
                disc = double.Parse(ConfigurationManager.AppSettings["Discount6to12"]);
            }
            else if (age <= 19)
            {
                discount.Text = "25%";
                disc = double.Parse(ConfigurationManager.AppSettings["Discount13to19"]);
            }
            else
            {
                discount.Text = "N/A";
                disc = 1;
            }

            double preTotal = double.Parse(pricePerNight.Text.Remove(0, 1)) * disc * int.Parse(numNights.Text);
            total.Text = "$" + String.Format("{0:0.##}", preTotal);
        }
        grandTotal.Text = "$" + estTotalCost.Text;
    }

    public void PaymentMethodChanged(object sender, EventArgs e)
    {
        switch (dlPaymentChoice.SelectedIndex)
        {
            case 0:
                paymentPanel.Visible = true;
                extraPaymentInfo.Visible = false;
                break;
            default:
                paymentPanel.Visible = false;
                extraPaymentInfo.Visible = true;
                extraPaymentInfo.Text = "Your spot will be saved for thirty days. Please contact us "
                                        + "as soon as possible to make arrangements for your contribution.";
                break;
        }
    }

    public void UserSelectSingleStatus(object sender, EventArgs e)
    {
        //user answered 'no' to registering alone
        if (userSingleRetreatantButton.SelectedIndex == 1)
        {
            pnlHowManyAttending.Visible = true;
            dlHowManyPeople.Enabled = true;
        }
        else    
        //user is registering alone, make panel invisible
            pnlHowManyAttending.Visible = false;
    }

    public void btnAdminAuth_Clicked(object sender, EventArgs e)
    {
        if (txtAdminAuth.Text == ConfigurationManager.AppSettings["AdminRetreatRegistrationBypass"])
        {
            pnlPaymentStepChoice.Visible = true;
            litAuthError.Text = "";
            modalAdmin.Hide();
        }
        else
        {
            litAuthError.Text = "The bypass entered was incorrect. Please try again.";
            //modalAdmin.Show();
        }
    }

    public void SetNewPersonsAttending(object sender, EventArgs e)
    {
        List<int> itemsToDataBind = new List<int>();
        for (int i = 0; i < int.Parse(dlHowManyPeople.Text); i++)
        {
            itemsToDataBind.Add(i);
        }

        if (itemsToDataBind.Count() == 0)
            rpAdditionalRetreatants.Visible = false;
        else
        {
            rpAdditionalRetreatants.DataSource = itemsToDataBind;
            rpAdditionalRetreatants.DataBind();
            rpAdditionalRetreatants.Visible = true;
        }
    }

    public void SetLanguageDropdowns(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            DropDownList dlFirst = (DropDownList)e.Item.FindControl("dlFirstLangPref");
            DropDownList dlSecond = (DropDownList)e.Item.FindControl("dlSecondLangPref");

            var languages = db.jkp_Languages;

            foreach (Monks.jkp_Language lang in languages)
            {
                dlFirst.Items.Add(new ListItem(lang.Lang_DisplayName.ToString(), lang.Lang_ID.ToString()));
                dlSecond.Items.Add(new ListItem(lang.Lang_DisplayName.ToString(), lang.Lang_ID.ToString()));
            }
        }
    }

    public void PastRetreatantsChecked(object sender, EventArgs e)
    {
        var user = db.aspnet_Memberships.First(p => p.UserId == UserId);

        //list of people who were registered with the user (share a contribution id)
        var pastRetreatants = from d in db.jkp_PersonAttendingRetreats
                              where d.PAR_PersonId != user.PersonId && d.jkp_Contribution.Cont_Off_Per_ID == user.PersonId
                              group d by d.PAR_PersonId into g
                              select g;

        if (pastRetreatants.Count() > 0)
        {
            rpPastRetreatants.DataSource = pastRetreatants;
            rpPastRetreatants.DataBind();
        }
        else
        {
            famGroupErrorStatus.Text = "None";
        }
    }


    public void FillPastRetreatants(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            //set fields of the repeater with the item info
            IGrouping<Guid, Monks.jkp_PersonAttendingRetreat> pastRetreatantGrouping = (IGrouping<Guid, Monks.jkp_PersonAttendingRetreat>)e.Item.DataItem;
            Monks.jkp_PersonAttendingRetreat pastRetreatant = pastRetreatantGrouping.First();
            CheckBox pastRetreatantCheckBox = (CheckBox)e.Item.FindControl("pastPersonCheckBox");
            Literal pastRetreatantFirstName = (Literal)e.Item.FindControl("pastPersonFirstName");
            Literal pastRetreatantLastName = (Literal)e.Item.FindControl("pastPersonLastName");

            pastRetreatantCheckBox.Attributes.Add("per_id", pastRetreatant.PAR_PersonId.ToString());
            pastRetreatantCheckBox.Attributes.Add("per_age", (DateTime.Now.Year - pastRetreatant.jkp_Person.Per_Birthdate.Value.Year).ToString());
            pastRetreatantCheckBox.Attributes.Add("per_birthdate", pastRetreatant.jkp_Person.Per_Birthdate.ToString());
            pastRetreatantFirstName.Text = pastRetreatant.jkp_Person.Per_FirstName;
            pastRetreatantLastName.Text = pastRetreatant.jkp_Person.Per_LastName;
        }
        //after the list of past retreatants, prompt to register new persons
        rpPastRetreatants.Visible = true;
    }

    public void FillReviewPersonRepeater(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            //set fields of the repeater with the item info
            PastPerson pastRetreatant = (PastPerson)e.Item.DataItem;
            Label fullName = (Label)e.Item.FindControl("personNameList");

            fullName.Text = pastRetreatant.Person_fName + " " + pastRetreatant.Person_lName;
            if (pastRetreatant.Person_birthdate != null)
                fullName.Attributes.Add("per_birthdate", pastRetreatant.Person_birthdate.ToShortDateString());
            fullName.Attributes.Add("per_age", pastRetreatant.Person_age.ToString());
        }
    }

    public int DoPaypalTransaction(object sender, EventArgs e, ref string transID)
    {
        var user = (from u in db.aspnet_Memberships
                    where u.UserId == UserId
                    select u).Single();

        //set paypal params - currently hardcoding country, currency values
        PaypalHelpers.BillUserParams userParams = new PaypalHelpers.BillUserParams();
        DateTime cardExpir = DateTime.Parse(txtCardExpir.Text);
        userParams.CreditCardExpirMonth = cardExpir.Month;
        userParams.creditCardExpirYear = cardExpir.Year;
        userParams.CreditCardNumber = txtCardNum.Text;
        userParams.RecurringBillingAmount = (decimal)double.Parse(estTotalCost.Text);        
        userParams.PayerFirstName = txtCardFirstName.Text;
        userParams.PayerLastName = txtCardLastName.Text;
        userParams.ShippingCity = txtBillingCity.Text;
        userParams.ShippingCountry = txtBillingCountry.Text;
        userParams.ShippingCountryType = PayPalServices.CountryCodeType.US;
        userParams.ShippingState = txtBillingState.Text;
        userParams.ShippingZipCode = txtBillingZip.Text;
        userParams.ShippingAddress = txtBillingAddress.Text;
        userParams.PayerCountry = PayPalServices.CountryCodeType.US;
        userParams.CurrencyType = PayPalServices.CurrencyCodeType.USD;

        switch (dlCardChoice.SelectedValue)
        {
            case "Visa":
                userParams.CreditCardType = PayPalServices.CreditCardTypeType.Visa; break;
            case "Amex":
                userParams.CreditCardType = PayPalServices.CreditCardTypeType.Amex; break;
            case "Discover":
                userParams.CreditCardType = PayPalServices.CreditCardTypeType.Discover; break;
            case "Mastercard":
                userParams.CreditCardType = PayPalServices.CreditCardTypeType.MasterCard; break;
            default:
                userParams.CreditCardType = PayPalServices.CreditCardTypeType.Visa; break;
        }

        // payment in different currencies is not currently supported
        //switch (dlCurrency.SelectedValue)
        //{
        //    case "USD":
        //        userParams.CurrencyType = PayPalServices.CurrencyCodeType.USD; break;
        //    case "Euro":
        //        userParams.CurrencyType = PayPalServices.CurrencyCodeType.EUR; break;
        //    default:
        //        userParams.CurrencyType = PayPalServices.CurrencyCodeType.USD; break;
        //}

        String errors, transactionID;
        if (PaypalHelpers.BillUser(userParams, PayPalServices.PaymentActionCodeType.Sale, out errors, out transactionID))
            return 0;
        else
            return 1;
    }

    public void RegisterRetreatant(object sender, EventArgs e)
    {
        string transactionID = "";
        bool cardPayment = false;
        // if user chose to pay with a credit card, set a bool used in the rest of the function
        if (dlPaymentChoice.SelectedIndex == 0)
            cardPayment = true;

        if (cardPayment && DoPaypalTransaction(sender, e, ref transactionID) != 0)
        {
            authStatus.Text = "We are sorry, but your card did not authorize. Please go back to the payment step and review your information.";
            backToPaymentButton.Visible = true;
        }
        else
        {
            // make lists for all tuples to be added to db; 
            // is filled from single retreatant info and repeater info
            List<Monks.jkp_Person> personList = new List<Monks.jkp_Person>();
            List<Monks.jkp_PersonAttendingRetreat> PARList = new List<Monks.jkp_PersonAttendingRetreat>();

            //get user matching current logged in user's GUID
            var user = db.aspnet_Memberships.Single(p => p.UserId == UserId);
            var retreat = db.jkp_Retreats.Single(p => p.Ret_ID == RetreatId);

            Monks.jkp_Contribution contribution = new Monks.jkp_Contribution();

            // fill new contribution object with associated info from the person table
            contribution.Cont_ID = Guid.NewGuid();
            contribution.Cont_Off_Per_ID = user.jkp_Person.Per_ID;
            contribution.Cont_Rec_Per_ID = user.jkp_Person.Per_ID;
            contribution.Cont_Date = System.DateTime.Today;
            contribution.Cont_TotalPrice = (decimal)double.Parse(estTotalCost.Text);
            if (dlPaymentChoice.SelectedIndex == 0)
            {
                // credit card payment
                contribution.Cont_Currency = dlCurrency.SelectedValue;
                contribution.Cont_AmountPaid = (decimal)double.Parse(estTotalCost.Text);
                contribution.Cont_Transaction_ID = transactionID;
                contribution.Cont_PaymentMethodTypeId = (int)Monks.Enums.PaymentMethodType.PayPalCreditCard;
            }
            else if (dlPaymentChoice.SelectedIndex == 1)
            {
                // cash payment
                contribution.Cont_AmountPaid = (decimal)0.00;
                contribution.Cont_PaymentMethodTypeId = (int)Monks.Enums.PaymentMethodType.Cash;
            }

            Monks.jkp_PersonAttendingRetreat par;
            // go through every person in review person repeater and make the appropriate
            // PAR entries based on individual room choices
            foreach (RepeaterItem reviewPerson in reviewPersonRepeater.Items)
            {
                Label perName = (Label)reviewPerson.FindControl("personNameList");
                Label perRoomChoice = (Label)reviewPerson.FindControl("personRoomPref");
                Label perArrival = (Label)reviewPerson.FindControl("personArrival");
                Label perDeparture = (Label)reviewPerson.FindControl("personDeparture");

                par = new Monks.jkp_PersonAttendingRetreat();
                par.PAR_ID = Guid.NewGuid();
                par.PAR_ContributionId = contribution.Cont_ID;
                par.PAR_ArrivalTime = DateTime.Parse(perArrival.Text);
                par.PAR_DepartureTime = DateTime.Parse(perDeparture.Text);
                par.PAR_RetId = retreat.Ret_ID;
                par.PAR_RoomTypeId = new Guid(perRoomChoice.Attributes["roomTypeID"]);

                // get person from db using first name, last name, and birthdate
                String[] nameSplit = perName.Text.Split();
                String birthdate = perName.Attributes["per_birthdate"];
                var person = db.jkp_Persons.SingleOrDefault(r => r.Per_FirstName.ToLower() == nameSplit[0].ToLower()
                                                              && r.Per_LastName.ToLower() == nameSplit[1].ToLower()
                                                              && r.Per_Birthdate == DateTime.Parse(perName.Attributes["per_birthdate"]));

                if(person != null) // person was found in db
                    par.PAR_PersonId = person.Per_ID;
                else // person was not found in db, make new person entry
                {
                    Monks.jkp_Person newPerson = new Monks.jkp_Person();
                    newPerson.Per_ID = Guid.NewGuid();
                    newPerson.Per_FirstName = nameSplit[0];
                    newPerson.Per_LastName = nameSplit[1];
                    newPerson.Per_Birthdate = DateTime.Parse(perName.Attributes["per_birthdate"]);

                    foreach (RepeaterItem additionalPerson in rpAdditionalRetreatants.Items)
                    {
                        TextBox fName = (TextBox)additionalPerson.FindControl("txtAddFirstName");
                        TextBox lName = (TextBox)additionalPerson.FindControl("txtAddLastName");

                        if (nameSplit[0] == fName.Text && nameSplit[1] == lName.Text)
                        {
                            DropDownList gender = (DropDownList)additionalPerson.FindControl("dlAddGender");
                            DropDownList firstLangChoice = (DropDownList)additionalPerson.FindControl("dlFirstLangPref");
                            DropDownList secondLangChoice = (DropDownList)additionalPerson.FindControl("dlSecondLangPref");

                            if (gender.SelectedItem.Text == "male" || gender.SelectedItem.Text == "Male") 
                                newPerson.Per_Gender = 'M';
                            else 
                                newPerson.Per_Gender ='F';
                            newPerson.Per_PrimaryLanguage = firstLangChoice.SelectedItem.Text;
                            newPerson.Per_SecondaryLanguage = secondLangChoice.SelectedItem.Text;
                        }
                    }
                    par.PAR_PersonId = newPerson.Per_ID;
                    personList.Add(newPerson);
                 
                }
                PARList.Add(par);
            }

            //perform inserts        
            db.jkp_Persons.InsertAllOnSubmit(personList);
            db.jkp_Contributions.InsertOnSubmit(contribution);
            db.jkp_PersonAttendingRetreats.InsertAllOnSubmit(PARList);

            db.SubmitChanges();
            authStatus.Text = "Success! You have been successfully registered for this retreat";
            
            string messageSubject = "Deer Park Retreat Registration Confirmation";
            string messageBody = "This email is being sent as a confirmation of your successful retreat registration with Deer Park. Your confirmation number is " + PARList.First().PAR_ID.ToString() + ". Please keep this email for future reference.";
            //MailSender mSender = new MailSender(user.jkp_Person.Per_Email, messageSubject, messageBody);
            //mSender.SendMail();            

            //send confirmation email - need smtp info for deer park mail server
            //MailMessage mailConfirm = new MailMessage();
            //mailConfirm.From = new MailAddress("dpadmin@deerpark.org");
            //mailConfirm.To.Add(user.jkp_Person.Per_Email);
            //mailConfirm.Subject = "Deer Park Retreat Registration Confirmation";
            //mailConfirm.Body = "This email is being sent as a confirmation of your successful retreat registration with Deer Park. Your confirmation number is " + PARList.First().PAR_ID.ToString() + ". Please keep this email for future reference.";
            //mailConfirm.IsBodyHtml = true;
            //mailConfirm.Priority = MailPriority.Normal;
            //SmtpClient mailServer = new SmtpClient("smtp.gmail.com");
            //mailServer.Port = 995;
            //mailServer.Credentials = new NetworkCredential("absolution9099@gmail.com", "A0n%3ffk7b*");
            //mailServer.EnableSsl = true;
            //mailServer.Send(mailConfirm);

        }
    }
}
