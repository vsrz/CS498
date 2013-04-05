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
    /// Summary description for jkp_RoomType
    /// </summary>
    public partial class jkp_RoomType
    {
        /// <summary>
        /// Gets the name of the room with it's rate.
        /// </summary>
        public string FullRoomTypeNameWithRate
        {
            get
            {
                
                    return this.RmType_Name + " - $" + this.jkp_Rate.Rt_BaseAmount.ToString();
                
            }
        }
    }
}
