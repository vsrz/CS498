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
using System.Collections.Generic;

public partial class Media_RoleManager : System.Web.UI.Page
{
    public Monks.Enums.MediaTypes MediaType
    {
        get { return (Monks.Enums.MediaTypes)Enum.Parse(typeof(Monks.Enums.MediaTypes), Request.QueryString["mediatype"]); }
    }

    public Guid ItemId
    {
        get { return new Guid(Request.QueryString["itemid"]); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (!Page.IsPostBack)
        {
            LoadPageData();
            MonkData db = new MonkData();
            var roles = db.aspnet_Roles;
            dlRolesToAssign.DataSource = roles;
            dlRolesToAssign.DataTextField = "RoleName";
            dlRolesToAssign.DataValueField = "RoleId";
            dlRolesToAssign.DataBind();

        }
    }

    private void LoadPageData()
    {
        MonkData db = new MonkData();
        List<Monks.jkp_MediaRole> mediaRoles = new List<Monks.jkp_MediaRole>();

        if (MediaType == Monks.Enums.MediaTypes.Audio)
        {
            Monks.jkp_Audio audio = db.jkp_Audios.First(p => p.Aud_ID == ItemId);
            litMediaType.Text = "Audio";
            litMediaTitle.Text = audio.Aud_Title;
            mediaRoles = db.jkp_MediaRoles.Where(p => p.AudioId == ItemId).ToList();
        }
        else if (MediaType == Monks.Enums.MediaTypes.Book)
        {
            Monks.jkp_Book book = db.jkp_Books.First(p => p.Bk_ID == ItemId);
            litMediaType.Text = "Book";
            litMediaTitle.Text = book.Bk_Title;
            mediaRoles = db.jkp_MediaRoles.Where(p => p.BookId == ItemId).ToList();
        }
        else if (MediaType == Monks.Enums.MediaTypes.Image)
        {
            Monks.jkp_Image image = db.jkp_Images.First(p => p.Img_ID == ItemId);
            litMediaType.Text = "Image";
            litMediaTitle.Text = image.Img_Title;
            mediaRoles = db.jkp_MediaRoles.Where(p => p.ImageId == ItemId).ToList();
        }
        else if (MediaType == Monks.Enums.MediaTypes.Video)
        {
            Monks.jkp_Video video = db.jkp_Videos.First(p => p.Vid_ID == ItemId);
            litMediaType.Text = "Video";
            litMediaTitle.Text = video.Vid_Title;
            mediaRoles = db.jkp_MediaRoles.Where(p => p.VideoId == ItemId).ToList();
        }

        rpAccessRoles.DataSource = mediaRoles;
        rpAccessRoles.DataBind();
    }

    public void rpAccessRoles_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Monks.jkp_MediaRole mediaRole = (Monks.jkp_MediaRole)e.Item.DataItem;
            Literal litRoleName = (Literal)e.Item.FindControl("litRoleName");
            LinkButton lbtnRemoveMediaRole = (LinkButton)e.Item.FindControl("lbtnRemoveMediaRole");
            litRoleName.Text = mediaRole.aspnet_Role.RoleName;
            lbtnRemoveMediaRole.Attributes.Add("mediaroleid", mediaRole.MediaRoleId.ToString());

        }
    }

    public void lbtnRemoveMediaRole_Clicked(object sender, EventArgs e)
    {
        LinkButton lbtnRemoveMediaRole = (LinkButton)sender;
        Guid mediaRoleId = new Guid(lbtnRemoveMediaRole.Attributes["mediaroleid"]);
        MonkData db = new MonkData();
        Monks.jkp_MediaRole mediaRole = db.jkp_MediaRoles.First(p => p.MediaRoleId == mediaRoleId);
        db.jkp_MediaRoles.DeleteOnSubmit(mediaRole);
        db.SubmitChanges();
        LoadPageData();
    }

    public void btnAddMediaRole_Clicked(object sender, EventArgs e)
    {
        if(dlRolesToAssign.Items.Count == 0)
            throw new Exception("Roles must be created first before trying to assign roles.");
        MonkData db = new MonkData();
        Monks.jkp_MediaRole mediaRole = new Monks.jkp_MediaRole();
        if(MediaType == Monks.Enums.MediaTypes.Audio)
            mediaRole.AudioId = ItemId;
        else if(MediaType == Monks.Enums.MediaTypes.Book)
            mediaRole.BookId = ItemId;
        else if(MediaType == Monks.Enums.MediaTypes.Image)
            mediaRole.ImageId = ItemId;
        else if(MediaType == Monks.Enums.MediaTypes.Video)
            mediaRole.VideoId = ItemId;
        mediaRole.RoleId = new Guid(dlRolesToAssign.SelectedValue);
        mediaRole.MediaRoleId = Guid.NewGuid();

        var existingSameRole = from d in db.jkp_MediaRoles
                               where object.Equals( d.AudioId, mediaRole.AudioId) && object.Equals( d.BookId, mediaRole.BookId) && object.Equals( d.ImageId, mediaRole.ImageId) && object.Equals(d.VideoId , mediaRole.VideoId)
                               && d.RoleId == mediaRole.RoleId
                               select d;
        if(existingSameRole.Count() > 0)
        {
            modalAddRole.Hide();
            return;
        }



        db.jkp_MediaRoles.InsertOnSubmit(mediaRole);
        db.SubmitChanges();
        LoadPageData();
        modalAddRole.Hide();
    }

    public void btnAddMediaRole_Cancel(object sender, EventArgs e)
    {
        modalAddRole.Hide();
    }
}
