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
using System.Reflection;

public partial class Admin_AllTables : LoggedInPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        

        MonkData db = new MonkData();
        foreach(PropertyInfo tablePInfo in db.GetType().GetProperties())
        {
            Type[] genericTypes = tablePInfo.PropertyType.GetGenericArguments();
            if(genericTypes.Count() < 1)
                continue;

            if(!CanUserAccessTable(genericTypes[0].Name, false, ref db))
            {
                continue;
            }

            HyperLink hlTypeEdit = new HyperLink();
            hlTypeEdit.NavigateUrl = "AddEdit.aspx?typename=" + genericTypes[0].Name;
            hlTypeEdit.Text = "Add " + GetFieldNameFromString( tablePInfo.Name);
            plcAddItemsList.Controls.Add(hlTypeEdit);

            Literal litLineBreak = new Literal();
            litLineBreak.Text = "<br />";
            plcAddItemsList.Controls.Add(litLineBreak);

            HyperLink hlTypeGridView = new HyperLink();
            hlTypeGridView.NavigateUrl = "GridView.aspx?typename=" + genericTypes[0].Name;
            hlTypeGridView.Text = GetFieldNameFromString( tablePInfo.Name);
            plcViewItems.Controls.Add(hlTypeGridView);

            Literal litLineBreakGridView = new Literal();
            litLineBreakGridView.Text = "<br />";
            plcViewItems.Controls.Add(litLineBreakGridView);
        }

         
        
    }
}
