using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forgot_Password : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    MailSender mailSender = new MailSender();
    public void btnEmailPassword_Clicked(object sender, EventArgs e)
    {
        MonkData db = new MonkData();
        var membersWithEmail = db.aspnet_Memberships.Where(p=>p.Email == txtEmailAddress.Text);
        
        if(membersWithEmail.Count() > 0)
        { 
            Monks.aspnet_Membership member = membersWithEmail.First();
            mailSender.messageBody = "Your password that you requested is " + member.Password;
            mailSender.recipient = member.Email;
            mailSender.subject = "Forgot your Password";
            mvForgot.ActiveViewIndex = 1;
        }
        else
        {

        }
    }
}
