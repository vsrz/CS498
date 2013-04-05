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
    /// Summary description for jkp_Person
    /// </summary>
    public partial class jkp_Person
    {
        public string DisplayName
        {
           get { return this.Per_FirstName + " " + this.Per_LastName; }
        }

        public string SearchDisplayName
        {
            get
            {
                return DisplayName + " &nbsp; &nbsp; " + this.Per_Email + " &nbsp; &nbsp; Gender: " + this.Per_Gender;
            }
        }
    }
}
