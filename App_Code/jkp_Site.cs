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
    /// Summary description for jkp_Site
    /// </summary>
    public partial class jkp_Site
    {
        /// <summary>
        /// Looks for the first available name starting with the local name. Not data bound.
        /// </summary>
        public string GetName
        {
            get
            {
                
                if(!String.IsNullOrEmpty(this.Site_LocalName))
                    return this.Site_LocalName;
                else if(!String.IsNullOrEmpty(this.Site_EnglishName))
                    return this.Site_EnglishName;
                else
                    return this.Site_VietnameseName;
            }
        }



    }
}
