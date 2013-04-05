using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml.Serialization;

/// <summary>
/// Paypal helpers is used for contacting paypal. Look at https://cms.paypal.com/us/cgi-bin/?cmd=_render-content&content_ID=developer/e_howto_api_soap_r_CreateRecurringPayments
/// for documentation on all of the fields for the different calls.
/// </summary>
public class PaypalHelpers
{
    

    /// <summary>
    /// 
    /// </summary>
    public static string APIVersion
    {
        get { return ConfigurationManager.AppSettings["PayPalAPIVersion"]; }
    }

    public static string APIUsername
    {
        get { return ConfigurationManager.AppSettings["PayPalAPIUsername"]; }
    }

    public static string APIPassword
    {
        get { return ConfigurationManager.AppSettings["PayPalAPIPassword"]; }
    }

    public static string APISignature
    {
        get { return ConfigurationManager.AppSettings["PayPalSignature"]; }
    }

    public static string APIUrl
    {
        get { return ConfigurationManager.AppSettings["PayPalAPIUrl"]; }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="transactionIdToRefund"></param>
    /// <param name="refundType">If the type of partial, then you need to give an amount for the amount to refund. If it's full, the full amount will be refunded</param>
    /// <param name="amountToRefund">Only used for partial refunds.</param>
    /// <param name="refundMemo">The memo to include with the refunded amount.</param>
    /// <param name="transactionIdOfRefund">The transaction ID of the refund that was just processed. This is returned to us from PayPal.</param>
    /// <returns></returns>
    public static bool DoRefund(string transactionIdToRefund, PayPalServices.RefundType refundType, decimal amountToRefund, string refundMemo, out string transactionIdOfRefund, ref string errorMessages)
    {
        PayPalServices.UserIdPasswordType user = new PayPalServices.UserIdPasswordType();
        user.Username = APIUsername;
        user.Password = APIPassword;
        user.Signature = APISignature;

        PayPalServices.PayPalAPISoapBinding PPInterface = new PayPalServices.PayPalAPISoapBinding();
        PPInterface.RequesterCredentials = new PayPalServices.CustomSecurityHeaderType();
        PPInterface.RequesterCredentials.Credentials = user;
        PPInterface.RequestEncoding = System.Text.Encoding.UTF8;
        PPInterface.Url = APIUrl;

        PayPalServices.RefundTransactionReq refund = new PayPalServices.RefundTransactionReq();
        refund.RefundTransactionRequest = new PayPalServices.RefundTransactionRequestType();
        if (refundType == PayPalServices.RefundType.Partial)
        {
            PayPalServices.BasicAmountType refundBasicAmount = new PayPalServices.BasicAmountType();
            refundBasicAmount.currencyID = PayPalServices.CurrencyCodeType.USD;
            refundBasicAmount.Value = amountToRefund.ToString("0.00");

            refund.RefundTransactionRequest.Amount = refundBasicAmount;
        }
        refund.RefundTransactionRequest.Memo = refundMemo;
        refund.RefundTransactionRequest.RefundType = refundType;
        refund.RefundTransactionRequest.TransactionID = transactionIdToRefund;
        refund.RefundTransactionRequest.Version = APIVersion;


        PayPalServices.RefundTransactionResponseType response = PPInterface.RefundTransaction(refund);

        if (response.Ack == PayPalServices.AckCodeType.Success)
        {
            transactionIdOfRefund = response.RefundTransactionID;
            errorMessages = string.Empty;
            return true;
        }

        string errorsMessages = "Status: " + response.Ack.ToString() + " <br/>";
        foreach (PayPalServices.ErrorType error in response.Errors)
        {
            errorsMessages += error.ErrorCode + " - " + error.LongMessage + " " + error.ShortMessage + "<br/>";
        }
        errorMessages = errorsMessages;
        transactionIdOfRefund = string.Empty;
        return false;
    }

    /// <summary>
    /// Run a new transaction off of a previously retrieved transaction Id
    /// </summary>
    /// <param name="b"></param>
    /// <param name="transactionId">Reference transaction id</param>
    /// <param name="TransactionId">Transaction ID of the order that was just processed.</param>
    /// <returns></returns>
    public static bool DoReferenceTransaction(BillUserParams b, string transactionId, out string TransactionId, out string ErrorMessages)
    {
        PayPalServices.UserIdPasswordType user = new PayPalServices.UserIdPasswordType();
        user.Username = APIUsername;
        user.Password = APIPassword;
        user.Signature = APISignature;

        PayPalServices.PayPalAPIAASoapBinding PPInterface = new PayPalServices.PayPalAPIAASoapBinding();
        PPInterface.RequesterCredentials = new PayPalServices.CustomSecurityHeaderType();
        PPInterface.RequesterCredentials.Credentials = user;
        PPInterface.RequestEncoding = System.Text.Encoding.UTF8;
        PPInterface.Url = APIUrl;

        PayPalServices.DoReferenceTransactionReq trans = new PayPalServices.DoReferenceTransactionReq();
        trans.DoReferenceTransactionRequest = new PayPalServices.DoReferenceTransactionRequestType();
        trans.DoReferenceTransactionRequest.Version = APIVersion;
        trans.DoReferenceTransactionRequest.DoReferenceTransactionRequestDetails = new PayPalServices.DoReferenceTransactionRequestDetailsType();
        trans.DoReferenceTransactionRequest.DoReferenceTransactionRequestDetails.ReferenceID = transactionId;
        trans.DoReferenceTransactionRequest.DoReferenceTransactionRequestDetails.PaymentAction = PayPalServices.PaymentActionCodeType.Sale;

        PayPalServices.BasicAmountType subAmount = new PayPalServices.BasicAmountType();
        subAmount.currencyID = b.CurrencyType;
        subAmount.Value = b.RecurringBillingAmount.ToString("0.00");

        PayPalServices.PaymentDetailsType paymentDetails = new PayPalServices.PaymentDetailsType();
        paymentDetails.InvoiceID = b.InvoiceId;
        paymentDetails.ItemTotal = subAmount;
        paymentDetails.OrderDescription = b.SubscriptionDescription;
        paymentDetails.OrderTotal = subAmount;
        trans.DoReferenceTransactionRequest.DoReferenceTransactionRequestDetails.PaymentDetails = paymentDetails;

        trans.DoReferenceTransactionRequest.DoReferenceTransactionRequestDetails.IPAddress = b.IPAddress;

        PayPalServices.DoReferenceTransactionResponseType response = PPInterface.DoReferenceTransaction(trans);

        if (response.Ack == PayPalServices.AckCodeType.Success)
        {
            TransactionId = response.DoReferenceTransactionResponseDetails.TransactionID;
            ErrorMessages = string.Empty;
            return true;
        }

        string errorsMessages = "Status: " + response.Ack.ToString() + " <br/>";
        foreach (PayPalServices.ErrorType error in response.Errors)
        {
            errorsMessages += error.ErrorCode + " - " + error.LongMessage + " " + error.ShortMessage + "<br/>";
        }
        ErrorMessages = errorsMessages;
        TransactionId = string.Empty;
        return false;
    }

    public static bool VoidPendingCharge(string transactionId, out string errorMessages)
    {
        PayPalServices.UserIdPasswordType user = new PayPalServices.UserIdPasswordType();
        user.Username = APIUsername;
        user.Password = APIPassword;
        user.Signature = APISignature;

        PayPalServices.PayPalAPIAASoapBinding PPInterface = new PayPalServices.PayPalAPIAASoapBinding();
        PPInterface.RequesterCredentials = new PayPalServices.CustomSecurityHeaderType();
        PPInterface.RequesterCredentials.Credentials = user;
        PPInterface.RequestEncoding = System.Text.Encoding.UTF8;
        PPInterface.Url = APIUrl;

        PayPalServices.DoVoidReq voidReq = new PayPalServices.DoVoidReq();
        voidReq.DoVoidRequest = new PayPalServices.DoVoidRequestType();
        voidReq.DoVoidRequest.Version = APIVersion;
        voidReq.DoVoidRequest.AuthorizationID = transactionId;
        
        PayPalServices.DoVoidResponseType response = PPInterface.DoVoid(voidReq);
        if(response.Ack == PayPalServices.AckCodeType.Success)
        {
           errorMessages = string.Empty;
           return true;
        }
        string errorsMessages = "Status: " + response.Ack.ToString() + " <br/>";
        foreach (PayPalServices.ErrorType error in response.Errors)
        {
            errorsMessages += error.ErrorCode + " - " + error.LongMessage + " " + error.ShortMessage + "<br/>";
        }

        errorMessages = errorsMessages;
        return false;
    }


    /// <summary>
    /// Bills the user for something
    /// </summary>
    /// <param name="b"></param>
    /// <param name="ErrorMessages">If there were any error messages, they'll be in this varable.</param>
    /// <returns>Determines if this was a success.</returns>
    public static bool BillUser(BillUserParams b, PayPalServices.PaymentActionCodeType PaymentActionType, out string ErrorMessages, out string TransactionId)
    {
        PayPalServices.UserIdPasswordType user = new PayPalServices.UserIdPasswordType();
        user.Username = APIUsername;
        user.Password = APIPassword;
        user.Signature = APISignature;

        PayPalServices.PayPalAPIAASoapBinding PPInterface = new PayPalServices.PayPalAPIAASoapBinding();
        PPInterface.RequesterCredentials = new PayPalServices.CustomSecurityHeaderType();
        PPInterface.RequesterCredentials.Credentials = user;
        PPInterface.RequestEncoding = System.Text.Encoding.UTF8;
        PPInterface.Url = APIUrl;

        PayPalServices.DoDirectPaymentReq paymentReq = new PayPalServices.DoDirectPaymentReq();

        PayPalServices.PersonNameType personNameType = new PayPalServices.PersonNameType();
        personNameType.FirstName = b.PayerFirstName;
        personNameType.LastName = b.PayerLastName;

        PayPalServices.AddressType payerBillingAddress = new PayPalServices.AddressType();
        payerBillingAddress.CityName = b.ShippingCity;
        payerBillingAddress.CountryName = b.ShippingCountry;
        payerBillingAddress.Country = b.ShippingCountryType;
        payerBillingAddress.CountrySpecified = true;
        payerBillingAddress.StateOrProvince = b.ShippingState;
        payerBillingAddress.Street1 = b.ShippingAddress;
        payerBillingAddress.PostalCode = b.ShippingZipCode;
        payerBillingAddress.Phone = b.PhoneNumber;
        payerBillingAddress.Name = b.PayerFirstName + " " + b.PayerLastName;

        PayPalServices.PayerInfoType payerInfo = new PayPalServices.PayerInfoType();
        payerInfo.Address = payerBillingAddress;
        payerInfo.ContactPhone = b.PhoneNumber;
        payerInfo.Payer = b.EmailAddress;  // Max length 127 characters
        payerInfo.PayerBusiness = b.BusinessName; // Max length 127 characters
        payerInfo.PayerCountry = b.PayerCountry;
        payerInfo.PayerCountrySpecified = true;
        payerInfo.PayerName = personNameType;

        PayPalServices.CreditCardDetailsType cc = new PayPalServices.CreditCardDetailsType();
        cc.CardOwner = payerInfo;
        cc.CreditCardNumber = b.CreditCardNumber;
        cc.CreditCardType = b.CreditCardType;
        cc.CreditCardTypeSpecified = true;
        cc.CVV2 = b.CVV2;
        cc.ExpMonth = b.CreditCardExpirMonth;
        cc.ExpMonthSpecified = true;
        cc.ExpYear = b.creditCardExpirYear;
        cc.ExpYearSpecified = true;
        cc.IssueNumber = string.Empty;


        paymentReq.DoDirectPaymentRequest = new PayPalServices.DoDirectPaymentRequestType();
        paymentReq.DoDirectPaymentRequest.Version = APIVersion;
        paymentReq.DoDirectPaymentRequest.DoDirectPaymentRequestDetails = new PayPalServices.DoDirectPaymentRequestDetailsType();
        paymentReq.DoDirectPaymentRequest.DoDirectPaymentRequestDetails.CreditCard = cc;
        paymentReq.DoDirectPaymentRequest.DoDirectPaymentRequestDetails.IPAddress = b.IPAddress;
        paymentReq.DoDirectPaymentRequest.DoDirectPaymentRequestDetails.PaymentAction = PaymentActionType;


        PayPalServices.BasicAmountType subAmount = new PayPalServices.BasicAmountType();
        subAmount.currencyID = b.CurrencyType;
        subAmount.Value = b.RecurringBillingAmount.ToString("0.00");

        PayPalServices.PaymentDetailsType paymentDetails = new PayPalServices.PaymentDetailsType();
        paymentDetails.InvoiceID = b.InvoiceId;
        paymentDetails.ItemTotal = subAmount;
        paymentDetails.OrderDescription = b.SubscriptionDescription;
        paymentDetails.OrderTotal = subAmount;

        paymentReq.DoDirectPaymentRequest.DoDirectPaymentRequestDetails.PaymentDetails = paymentDetails;
        PayPalServices.DoDirectPaymentResponseType response = PPInterface.DoDirectPayment(paymentReq);

        if (response.Ack == PayPalServices.AckCodeType.Success)
        {
            TransactionId = response.TransactionID;
            ErrorMessages = string.Empty;
            return true;
        }

        // Build the error message to display to the user.
        string errorsMessages;
        
        if (response.Ack == PayPalServices.AckCodeType.SuccessWithWarning)
        {
            errorsMessages = "Status: " + "Error" + " <br/>";
        }
        else
        {
            errorsMessages = "Status: " + response.Ack.ToString() + " <br/>";
        }

        foreach (PayPalServices.ErrorType error in response.Errors)
        {
            errorsMessages += error.ErrorCode + " - " + error.LongMessage + " " + error.ShortMessage + "<br/>";
        }
        ErrorMessages = errorsMessages;
        TransactionId = string.Empty;
        return false;
    }

    

    public class BillUserParams
    {
        public string CreditCardNumber { get; set; }
        public int CreditCardExpirMonth { get; set; }
        public int creditCardExpirYear { get; set; }
        public PayPalServices.CreditCardTypeType CreditCardType { get; set; }
        public string CVV2 { get; set; }
        public int CreditCardStartMonth { get; set; }
        public int CreditCardStartYear { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingZipCode { get; set; }
        public string ShippingCountry { get; set; }
        public PayPalServices.CountryCodeType ShippingCountryType { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string BusinessName { get; set; }
        public PayPalServices.CountryCodeType PayerCountry { get; set; }
        public string PayerFirstName { get; set; }
        public string PayerLastName { get; set; }
        public string SubscriptionReferenceId { get; set; }
        public PayPalServices.AutoBillType AutoBillType { get; set; }
        public string SubscriptionDescription { get; set; }
        public int? MaxFailedAttempts { get; set; }
        public PayPalServices.CurrencyCodeType CurrencyType { get; set; }
        public string IPAddress { get; set; }
        public string InvoiceId { get; set; }
        public decimal RecurringBillingAmount { get; set; }

    }


    /// <summary>
    /// Create a subscription in PayPal
    /// </summary>
    /// <returns>Returns the profile ID</returns>
    public static string CreateSubscription(CreateRecurringBillingParams b
        )
    {

        PayPalServices.UserIdPasswordType user = new PayPalServices.UserIdPasswordType();
        user.Username = APIUsername;
        user.Password = APIPassword;
        user.Signature = APISignature;
        #region Old Paypal code
        //PayPalServices.PayPalAPISoapBinding PPInterface  = new PayPalServices.PayPalAPISoapBinding();
        //PPInterface.RequesterCredentials = new PayPalServices.CustomSecurityHeaderType();
        //PPInterface.Url = APIUrl;
        //PPInterface.RequesterCredentials.Credentials = user; 
        #endregion

        PayPalServices.PayPalAPIAASoapBinding PPInterface = new PayPalServices.PayPalAPIAASoapBinding();
        PPInterface.RequesterCredentials = new PayPalServices.CustomSecurityHeaderType();
        PPInterface.RequesterCredentials.Credentials = user;
        PPInterface.Url = APIUrl;
        PPInterface.RequestEncoding = System.Text.Encoding.UTF8;

        PayPalServices.CreateRecurringPaymentsProfileReq recurring = new PayPalServices.CreateRecurringPaymentsProfileReq();

        recurring.CreateRecurringPaymentsProfileRequest = new PayPalServices.CreateRecurringPaymentsProfileRequestType();
        recurring.CreateRecurringPaymentsProfileRequest.Version = APIVersion;
        recurring.CreateRecurringPaymentsProfileRequest.CreateRecurringPaymentsProfileRequestDetails = new PayPalServices.CreateRecurringPaymentsProfileRequestDetailsType();


        PayPalServices.AddressType payerShippingAddress = new PayPalServices.AddressType();
        payerShippingAddress.CityName = b.ShippingCity;
        payerShippingAddress.CountryName = b.ShippingCountry;
        payerShippingAddress.CountrySpecified = true;
        payerShippingAddress.StateOrProvince = b.ShippingState;
        payerShippingAddress.Street1 = b.ShippingAddress;
        payerShippingAddress.PostalCode = b.ShippingZipCode;
        payerShippingAddress.Phone = b.PhoneNumber;
        payerShippingAddress.Name = b.PayerFirstName + " " + b.PayerLastName;


        PayPalServices.PersonNameType personNameType = new PayPalServices.PersonNameType();
        personNameType.FirstName = b.PayerFirstName;
        personNameType.LastName = b.PayerLastName;

        PayPalServices.AddressType payerBillingAddress = new PayPalServices.AddressType();
        payerBillingAddress.CityName = b.ShippingCity;
        payerBillingAddress.CountryName = b.ShippingCountry;
        payerBillingAddress.CountrySpecified = true;
        payerBillingAddress.StateOrProvince = b.ShippingState;
        payerBillingAddress.Street1 = b.ShippingAddress;
        payerBillingAddress.PostalCode = b.ShippingZipCode;
        payerBillingAddress.Phone = b.PhoneNumber;
        payerBillingAddress.Name = b.PayerFirstName + " " + b.PayerLastName;

        PayPalServices.PayerInfoType payerInfo = new PayPalServices.PayerInfoType();
        payerInfo.Address = payerBillingAddress;
        payerInfo.ContactPhone = b.PhoneNumber;
        payerInfo.Payer = b.EmailAddress;  // Max length 127 characters
        payerInfo.PayerBusiness = b.BusinessName; // Max length 127 characters
        payerInfo.PayerCountry = b.PayerCountry;
        payerInfo.PayerCountrySpecified = true;
        payerInfo.PayerName = personNameType;




        PayPalServices.CreditCardDetailsType cc = new PayPalServices.CreditCardDetailsType();
        cc.CardOwner = payerInfo;
        cc.CreditCardNumber = b.CreditCardNumber;
        cc.CreditCardType = b.CreditCardType;
        cc.CreditCardTypeSpecified = true;
        cc.CVV2 = b.CVV2;
        cc.ExpMonth = b.CreditCardExpirMonth;
        cc.ExpMonthSpecified = true;
        cc.ExpYear = b.creditCardExpirYear;
        cc.ExpYearSpecified = true;
        cc.IssueNumber = string.Empty;

        recurring.CreateRecurringPaymentsProfileRequest.CreateRecurringPaymentsProfileRequestDetails.CreditCard = cc;

        PayPalServices.RecurringPaymentsProfileDetailsType recDetails = new PayPalServices.RecurringPaymentsProfileDetailsType();
        recDetails.BillingStartDate = DateTime.Now.Add(new TimeSpan(0, 0, 1, 0));
        recDetails.ProfileReference = b.SubscriptionReferenceId;
        recDetails.SubscriberName = b.PayerFirstName + " " + b.PayerLastName;
        recDetails.SubscriberShippingAddress = payerShippingAddress;

        recurring.CreateRecurringPaymentsProfileRequest.CreateRecurringPaymentsProfileRequestDetails.RecurringPaymentsProfileDetails = recDetails;

        PayPalServices.BasicAmountType subAmount = new PayPalServices.BasicAmountType();
        subAmount.currencyID = b.CurrencyType;
        subAmount.Value = b.RecurringBillingAmount.ToString("0.00");

        // Billing period
        PayPalServices.BillingPeriodDetailsType subBillingPeriod = new PayPalServices.BillingPeriodDetailsType();
        subBillingPeriod.Amount = subAmount;
        subBillingPeriod.BillingFrequency = b.BillingFrequency;
        subBillingPeriod.BillingPeriod = b.BillingPeriod;


        PayPalServices.BasicAmountType trialAmount = new PayPalServices.BasicAmountType();
        trialAmount.currencyID = b.CurrencyType;
        trialAmount.Value = b.TrialPrice.ToString("0.00");

        // Trial period
        PayPalServices.BillingPeriodDetailsType trialBillingPeriod = new PayPalServices.BillingPeriodDetailsType();
        trialBillingPeriod.Amount = trialAmount;
        trialBillingPeriod.BillingFrequency = b.TrialLength;
        trialBillingPeriod.BillingPeriod = b.TrialBillingPeriodType;
        trialBillingPeriod.TotalBillingCycles = 1;
        trialBillingPeriod.TotalBillingCyclesSpecified = true;


        PayPalServices.ScheduleDetailsType schedule = new PayPalServices.ScheduleDetailsType();
        schedule.AutoBillOutstandingAmount = b.AutoBillType;
        schedule.AutoBillOutstandingAmountSpecified = true;
        schedule.Description = b.SubscriptionDescription;
        if (b.MaxFailedAttempts != null)
        {
            schedule.MaxFailedPayments = (int)b.MaxFailedAttempts;
            schedule.MaxFailedPaymentsSpecified = true;
        }
        schedule.PaymentPeriod = subBillingPeriod;
        schedule.TrialPeriod = trialBillingPeriod;

        PayPalServices.ActivationDetailsType activationDetails = new PayPalServices.ActivationDetailsType();
        activationDetails.FailedInitialAmountAction = PayPalServices.FailedPaymentActionType.CancelOnFailure;
        activationDetails.FailedInitialAmountActionSpecified = true;
        activationDetails.InitialAmount = subAmount;

        schedule.ActivationDetails = activationDetails;

        recurring.CreateRecurringPaymentsProfileRequest.CreateRecurringPaymentsProfileRequestDetails.ScheduleDetails = schedule;

        //return Helpers.SerializeSoapObject(recurring, typeof(PayPalServices.CreateRecurringPaymentsProfileReq));

        PayPalServices.CreateRecurringPaymentsProfileResponseType response = PPInterface.CreateRecurringPaymentsProfile(recurring);
        if (response.Ack == PayPalServices.AckCodeType.Success)
        {
            string profileId = response.CreateRecurringPaymentsProfileResponseDetails.ProfileID;
            return profileId;
        }


        string errorsMessages = "Status: " + response.Ack.ToString();
        foreach (PayPalServices.ErrorType error in response.Errors)
        {
            errorsMessages += error.ErrorCode + " - " + error.LongMessage + " - Short Message:" + error.ShortMessage + " - Severity Code:" + error.SeverityCode + "<br/>";
        }

        return response.Ack.ToString() + "<br/>" + errorsMessages;
    }


    public class CreateRecurringBillingParams
    {
        public string CreditCardNumber { get; set; }
        public int CreditCardExpirMonth { get; set; }
        public int creditCardExpirYear { get; set; }
        public PayPalServices.CreditCardTypeType CreditCardType { get; set; }
        public string CVV2 { get; set; }
        public int CreditCardStartMonth { get; set; }
        public int CreditCardStartYear { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingZipCode { get; set; }
        public string ShippingCountry { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string BusinessName { get; set; }
        public PayPalServices.CountryCodeType PayerCountry { get; set; }
        public string PayerFirstName { get; set; }
        public string PayerLastName { get; set; }
        public string SubscriptionReferenceId { get; set; }
        public PayPalServices.AutoBillType AutoBillType { get; set; }
        public string SubscriptionDescription { get; set; }
        public int? MaxFailedAttempts { get; set; }
        public PayPalServices.CurrencyCodeType CurrencyType { get; set; }
        public decimal RecurringBillingAmount { get; set; }
        public PayPalServices.BillingPeriodType BillingPeriod { get; set; }
        public int BillingFrequency { get; set; }
        public decimal TrialPrice { get; set; }
        public PayPalServices.BillingPeriodType TrialBillingPeriodType { get; set; }
        public int TrialLength { get; set; }

    }



}


