using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.Linq.SqlClient;

public partial class Media : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void btnSearchMedia_Clicked(object sender, EventArgs e)
    {
        MonkData db = new MonkData();

        if(rbtnAudio.Checked)
        {
            var audioBooks = from a in db.jkp_Audios
                             where SqlMethods.Like( a.Aud_Title , "%" + txtMediaSearch.Text + "%")
                             select a;
            gridSearchResults.DataSource = audioBooks;
            gridSearchResults.DataBind();
        }
        else if(rbtnBooks.Checked)
        {
            var books = from b in db.jkp_Books
                        where SqlMethods.Like(b.Bk_Title, "%" + txtMediaSearch.Text + "%")
                        select b;
            gridSearchResults.DataSource = books;
            gridSearchResults.DataBind();
        }
        else if(rbtnVideo.Checked)
        {
            var videos = from v in db.jkp_Videos
                         where SqlMethods.Like(v.Vid_Title, "%" + txtMediaSearch.Text + "%")
                         select v;
            gridSearchResults.DataSource = videos;
            gridSearchResults.DataBind();
        }
    }

    public void dataGrid_OnItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            object dataItem = e.Item.DataItem;
            HyperLink hlItemTitle = (HyperLink)e.Item.FindControl("hlItemTitle");
            if(dataItem.GetType() == typeof(Monks.jkp_Audio))
            {
                Monks.jkp_Audio audioItem = (Monks.jkp_Audio)dataItem;
                hlItemTitle.Text = audioItem.Aud_Title;
                hlItemTitle.NavigateUrl = "Media-ViewAudio.aspx?audioid=" + audioItem.Aud_ID.ToString();
            }
            else if(dataItem.GetType() == typeof(Monks.jkp_Book))
            {
                Monks.jkp_Book bookItem = (Monks.jkp_Book)dataItem;
                hlItemTitle.Text = bookItem.Bk_Title;
                hlItemTitle.NavigateUrl = "Media-ViewBook.aspx?bookid=" + bookItem.Bk_ID.ToString();
            }
            else if(dataItem.GetType() == typeof(Monks.jkp_Video))
            {
                Monks.jkp_Video videoItem = (Monks.jkp_Video)dataItem;
                hlItemTitle.Text = videoItem.Vid_Title;
                hlItemTitle.NavigateUrl = "Media-ViewVideo.aspx?videoid=" + videoItem.Vid_ID.ToString();
            }
        }
    }

}
