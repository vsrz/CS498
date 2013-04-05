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

namespace Monks
{
    /// <summary>
    /// Summary description for aspnet_Membership
    /// </summary>
    public partial class aspnet_Membership
    {

        public string ListDisplayText
        {
            get
            {
                if(jkp_Person == null)
                    return this.Email + " - No Person Object Associated.";
                return this.aspnet_User.UserName + " - " + this.jkp_Person.Per_FirstName + " " + this.jkp_Person.Per_LastName + " - " + this.Email + " - Last Login: " + this.LastLoginDate.ToShortDateString();
            }
        }

    }
}
