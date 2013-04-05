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
using System.Security.Cryptography;
using System.Text;
using System.Resources;
using System.Threading;
using System.Globalization;
using System.Reflection;

public partial class Signup : DefaultPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!Page.IsPostBack)
        {
            // This code is only called on the first page load because it's not a postback.
            mvSignup.ActiveViewIndex = 0;
            ddlCountry.DataSource = GetCountryNames;
            ddlCountry.DataBind();

            MonkData db = new MonkData();
            var languages = from q in db.jkp_Languages
                            select q.Lang_DisplayName;
            dlFirstLang.DataSource = languages;
            dlSecondLang.DataSource = languages;
            dlFirstLang.DataBind();
            dlSecondLang.DataBind();
        }
    }

    /// <summary>
    /// Event to check  if the username exists in the system already and it shows an alert to the user.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckUsername_Clicked(object sender, EventArgs e)
    {
        CheckUsername();
    }

    /// <summary>
    /// Checks if the username already exists in the system.
    /// </summary>
    /// <returns>True if the email address already exists. False if not.</returns>
    private bool CheckUsername()
    {
        MonkData db = new MonkData();
        // Check if a user already exists in the site with the same username.
        var existingUsers = from u in db.aspnet_Users
                            where u.LoweredUserName == txtUsername.Text.ToLower().Trim()
                            select u;

        // Show the error if a user already does exist with the same username.
        if (existingUsers.Count() > 0)
            lblUsernameAlreadyExists.Visible = true;
        else
            lblUsernameAlreadyExists.Visible = false;


        // Trigger the AJAX update panel to update. The only reason we have to trigger it is because if you look at the UI code, you'll notice
        // that property on the upanUsername control UpdateMode is "Conditional" so that means it will only update when we tell it to. It makes AJAX calls faster.
        upanUsername.Update();

        return existingUsers.Count() > 0;
    }
    /// <summary>
    /// Checks if the email address already exists.
    /// </summary>
    /// <returns>True if the email already exists. False if not.</returns>
    private bool CheckEmail()
    {
        MonkData db = new MonkData();
        var existingUsers = from u in db.aspnet_Memberships
                            where u.Email == txtEmailAddress1.Text.ToLower().Trim()
                            select u;

        return existingUsers.Count() > 0;
    }

    /// <summary>
    /// Event clicked when the user signs up.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSignup_Clicked(object sender, EventArgs e)
    {
        if (CheckUsername() || CheckEmail())
            return;

        btnSignup.Enabled = false;
        MonkData db = new MonkData();
        // Get the application from the ASP.NET Membership object. When we create the user, we'll need to know what aspnet_Application object they will use. 
        // This is the only spot in the entire site where we'll care about the asp.net application.
        var app = (from u in db.aspnet_Applications
                   where u.ApplicationName == "DeerPark"
                   select u).Single();
        string salt = GenerateSalt();  // Gets the salt that will be used for encoding the password if encoding the password is turned on.

        // Load up all of the LINQ objects for a new user
        bool personExists = false; // use a bool to see if the corresponding person entry exists. if so, do not perform insert to db
        Monks.jkp_Person jPerson = Create_jkp_Person(ref personExists, ref db);
        if (personExists)
        {
            jPerson.Per_Email = txtEmailAddress1.Text;
        }
        Monks.jkp_Address jAddress = Create_jkp_Address();
        Monks.aspnet_Membership curMembership = Create_aspnet_Membership(app, salt);        
        Monks.aspnet_User curUser = Create_aspnet_User(curMembership);

        // Attach the Person ID to the aspnet_Membership object
        curMembership.PersonId = jPerson.Per_ID;
        jPerson.Per_Add_ID = jAddress.Add_ID;

        // Setup the inserts. 
        db.jkp_Addresses.InsertOnSubmit(jAddress);
        
        // do not perform insert to the person table if an entry was already found for them
        if (!personExists)
        {
            db.jkp_Persons.InsertOnSubmit(jPerson);
        }

        db.aspnet_Users.InsertOnSubmit(curUser);
        db.aspnet_Memberships.InsertOnSubmit(curMembership);
        // Submit the inserts. This is a transactional insert. If one fails, they all fail. 
        db.SubmitChanges();

        // Show the success view.
        mvSignup.ActiveViewIndex = 1;

        FormsAuthentication.SetAuthCookie(txtUsername.Text, false);
    }

    private Monks.aspnet_User Create_aspnet_User(Monks.aspnet_Membership curUser)
    {
        Monks.aspnet_User user = new Monks.aspnet_User();
        user.ApplicationId = curUser.ApplicationId;
        user.IsAnonymous = false;
        user.UserId = curUser.UserId;
        user.UserName = txtUsername.Text.Trim();
        user.LoweredUserName = txtUsername.Text.ToLower().Trim();
        user.LastActivityDate = DateTime.Now;
        return user;
    }

    private Monks.aspnet_Membership Create_aspnet_Membership(Monks.aspnet_Application app, string salt)
    {
        Monks.aspnet_Membership curUser = new Monks.aspnet_Membership();
        curUser.ApplicationId = app.ApplicationId;
        curUser.UserId = Guid.NewGuid();
        curUser.Password = txtPassword1.Text.Trim(); //EncodePassword(txtPassword1.Text.Trim().ToLower(), 1, salt);
        curUser.PasswordFormat = 0; //1; // 0 means the password is in the clear. If set it to one we need to hash the password but then we can't see it if we need to fix something for the user.
        curUser.PasswordSalt = salt;
        curUser.Email = txtEmailAddress1.Text.ToLower().Trim();
        curUser.LoweredEmail = txtEmailAddress1.Text.ToLower().Trim();
        curUser.PasswordQuestion = txtQuestion.Text;
        curUser.PasswordAnswer = txtAnswer.Text; //EncodePassword(txtAnswer.Text.Trim().ToLower(CultureInfo.InvariantCulture), 1, salt);
        curUser.IsApproved = true;
        curUser.IsLockedOut = false;
        curUser.CreateDate = DateTime.Now;
        curUser.LastLoginDate = DateTime.Now;
        curUser.LastPasswordChangedDate = DateTime.Now;
        curUser.LastLockoutDate = DateTime.Parse("1/1/1754 12:00:00 AM");
        curUser.FailedPasswordAttemptCount = 0;
        curUser.FailedPasswordAttemptWindowStart = DateTime.Parse("1/1/1754 12:00:00 AM");
        curUser.FailedPasswordAnswerAttemptWindowStart = DateTime.Parse("1/1/1754 12:00:00 AM");
        curUser.FailedPasswordAnswerAttemptCount = 0;
        curUser.Comment = "";

        return curUser;
    }

    private Monks.jkp_Person Create_jkp_Person(ref bool personExists,ref MonkData db)
    {
        // see if a person object already exists in the database based on combination of first name, last name, and birthdate
        Monks.jkp_Person personToAddMembership = db.jkp_Persons.SingleOrDefault(p => p.Per_FirstName == txtFirstName.Text && p.Per_LastName == txtLastName.Text && p.Per_Birthdate.Value == DateTime.Parse(txtBirthdate.Text));

        if (personToAddMembership == null)
        {
            Monks.jkp_Person jPerson = new Monks.jkp_Person();
            jPerson.Per_FirstName = txtFirstName.Text;
            jPerson.Per_LastName = txtLastName.Text;
            jPerson.Per_Birthdate = DateTime.Parse(txtBirthdate.Text);
            jPerson.Per_PrimaryLanguage = dlFirstLang.SelectedItem.Text;
            jPerson.Per_SecondaryLanguage = dlSecondLang.SelectedItem.Text;
            if (ddlGender.SelectedItem.Text == "Male")
                jPerson.Per_Gender = 'M';
            else
                jPerson.Per_Gender = 'F';
            jPerson.Per_Email = txtEmailAddress1.Text;
            jPerson.Per_ID = Guid.NewGuid();

            personExists = false;
            return jPerson;
        }
        else
        {
            personExists = true;
            return personToAddMembership;
        }
    }

    /// <summary>
    /// Creates a jkp_address object for a user
    /// </summary>
    /// <returns></returns>
    private Monks.jkp_Address Create_jkp_Address()
    {
        Monks.jkp_Address jAddress = new Monks.jkp_Address();
        jAddress.Add_City = txtCity.Text;
        jAddress.Add_Street = txtAddress.Text;
        jAddress.Add_State = txtState.Text;
        jAddress.Add_PostalCode = txtPostalCode.Text;
        jAddress.Add_Country = ddlCountry.SelectedItem.Text;
        jAddress.Add_ID = Guid.NewGuid();
        return jAddress;
    }
       

    #region Password Encoding logic for the membership
    protected string GenerateSalt()
    {
        byte[] buf = new byte[16];
        (new RNGCryptoServiceProvider()).GetBytes(buf);
        return Convert.ToBase64String(buf);
    }
    protected string EncodePassword(string pass, int passwordFormat, string salt)
    {
        if (passwordFormat == 0) // MembershipPasswordFormat.Clear
            return pass;

        byte[] bIn = Encoding.Unicode.GetBytes(pass);
        byte[] bSalt = Convert.FromBase64String(salt);
        byte[] bAll = new byte[bSalt.Length + bIn.Length];
        byte[] bRet = null;

        System.Buffer.BlockCopy(bSalt, 0, bAll, 0, bSalt.Length);
        System.Buffer.BlockCopy(bIn, 0, bAll, bSalt.Length, bIn.Length);
        if (passwordFormat == 1)
        { // MembershipPasswordFormat.Hashed
            HashAlgorithm s = HashAlgorithm.Create(Membership.HashAlgorithmType);
            bRet = s.ComputeHash(bAll);
        }
        else
        {
            //bRet = EncryptPassword(bAll);
        }

        return Convert.ToBase64String(bRet);
    }
    #endregion
}
