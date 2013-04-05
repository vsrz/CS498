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
/// Summary description for jkp_Retreat
/// </summary>
public partial class jkp_Retreat
{
	public string LargeRetreatDescription
    {
        get
        {
            return this.Ret_Name + " at " + this.jkp_Site.GetName + " from " + this.Ret_StartDate.ToShortDateString() + " until " + this.Ret_EndDate.ToShortDateString() + " with " + this.jkp_PersonAttendingRetreats.Count().ToString() + " guests";
        }
    }
}
}
