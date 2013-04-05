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
    /// Summary description for jkp_Contribution
    /// </summary>
    public partial class jkp_Contribution
    {
        public string DisplayName
        {
            get
            {
                return this.jkp_Person.DisplayName + " of $" + (this.Cont_AmountPaid.HasValue ?  this.Cont_AmountPaid.Value.ToString("0.00") : "0.00") + " on " + this.Cont_Date.Value.ToShortDateString() + "   - ContribId: " + this.Cont_ID.ToString();
            }
        }
    }
}
