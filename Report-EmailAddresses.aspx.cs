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
using System.Data.Linq;
using System.Collections.Generic;
using System.IO;

public partial class Report_EmailAddresses : LoggedInPage
{
    public void EmailExportCSV(object sender, System.EventArgs e)
    {
        gvEmail.DataBind();
        // Create the CSV file to which grid data will be exported.
        StreamWriter sw = new StreamWriter(Server.MapPath("~/tmp/email_list.csv"), false);
        // First we will write the headers.
        int iColCount = gvEmail.Columns.Count;
        for (int i = 0; i < iColCount; i++)
        {
            sw.Write(gvEmail.Columns[i]);
            if (i < iColCount - 1)
            {
                sw.Write(",");
            }
        }
        sw.Write(sw.NewLine);
        // Now write all the rows.
        foreach (GridViewRow dr in gvEmail.Rows)
        {
            for (int i = 0; i < iColCount; i++)
            {
                if (!Convert.IsDBNull(dr.Cells[i]))
                {
                    
                    sw.Write(dr.Cells[i].Text.Replace("&nbsp;", " "));
                }
                if (i < iColCount - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
        }
        sw.Close();

        for (int i = 0; i < Request.Url.Segments.Count() - 1; i++)
        {
            hypEmailCSVDownload.NavigateUrl += Request.Url.Segments[i];
        }
        hypEmailCSVDownload.NavigateUrl += "tmp/email_list.csv";
        hypEmailCSVDownload.Visible = true;
    }

}
