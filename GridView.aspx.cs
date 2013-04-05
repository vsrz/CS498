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
using System.Data.Linq.Mapping;
using System.Web.Compilation;
using System.Linq.Expressions;
using System.Xml;


public partial class GridView : LoggedInPage
{
    public Type DataType
    {
        get
        {
            return BuildManager.GetType("Monks." + Request.QueryString["typename"], true);
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        bool itemAssignedForOrderByList = false;
        bool checkFirstRadioButtonWhereClause = false;
        foreach (PropertyInfo pInfo in DataType.GetProperties())
        {
            object[] attributes = pInfo.GetCustomAttributes(false);
            if (attributes.Count() == 0)
            {
                continue;
            }

            if (attributes[0].GetType() != typeof(ColumnAttribute))
            {
                
                continue;
            }

            ColumnAttribute colAttrib = (ColumnAttribute)attributes[0];
            

            if (pInfo.PropertyType == typeof(string) )
            {
                RadioButton chkSearchItem = new RadioButton();
                chkSearchItem.Text = " " + GetFieldNameFromString(pInfo.Name) + " &nbsp; ";
                chkSearchItem.ID = pInfo.Name;
                if (checkFirstRadioButtonWhereClause == false)
                    chkSearchItem.Checked = true;
                else
                    chkSearchItem.Checked = false;
                chkSearchItem.GroupName = "WhereClauses";
                plcSearchFields.Controls.Add(chkSearchItem);
            }

            if (pInfo.PropertyType == typeof(DateTime) || pInfo.PropertyType == typeof(int) ||
                pInfo.PropertyType == typeof(DateTime?) || pInfo.PropertyType == typeof(int?) ||
                pInfo.PropertyType == typeof(string) && !colAttrib.DbType.Contains("NText") && !colAttrib.DbType.Contains("Text"))
            {


                RadioButton rbtnOrderByItem = new RadioButton();
                rbtnOrderByItem.Text = " " + GetFieldNameFromString(pInfo.Name) + " &nbsp; ";
                rbtnOrderByItem.ID = "orderby_" + pInfo.Name;
                string propertyName = GetFieldNameFromString(pInfo.Name);
                if ((propertyName.Contains("Created") || propertyName.Contains("Date Added")) && !itemAssignedForOrderByList && !Page.IsPostBack)
                {
                    itemAssignedForOrderByList = true;
                    rbtnOrderByItem.Checked = true;
                }
                rbtnOrderByItem.GroupName = "orderby_group";
                plcOrderByFields.Controls.Add(rbtnOrderByItem);
            }
        }
        if (!itemAssignedForOrderByList && !Page.IsPostBack && plcOrderByFields.Controls.Count > 0)
            ((RadioButton)plcOrderByFields.Controls[0]).Checked = true;



        CheckTablePermission();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        dataGrid.Style.Clear();
    }
    PropertyInfo primaryKeyProp;
    PropertyInfo displayFieldProperty = null;
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (!Page.IsPostBack)
        {
            litTitle.Text = GetFieldNameFromString(DataType.Name) + " Items";
            hlAddNewItem.NavigateUrl += "AddEdit.aspx?typename=" + DataType.Name;
            hlAddNewItem.Text = hlAddNewItem.Text + " " + GetFieldNameFromString(DataType.Name);
        }

        primaryKeyProp = GetPrimaryKeyProperty(DataType);

        string displayFieldPropertyName = string.Empty;
        // Get the optional value from the configuration file.
        if (GetDatabaseTable(DataType.Name) != null)
        {
            XmlNode xTableConfig = GetDatabaseTable(DataType.Name);
            XmlNode xTitleColumn = xTableConfig.SelectSingleNode("TitleColumn");
            if (xTitleColumn != null)
            {
                displayFieldPropertyName = xTitleColumn.InnerText;
            }
        }

        foreach (PropertyInfo pInfo in DataType.GetProperties())
        {
            if (!String.IsNullOrEmpty(displayFieldPropertyName))
            {
                displayFieldProperty = DataType.GetProperties().First(p => p.Name == displayFieldPropertyName);
                break;
            }

            if (displayFieldProperty == null)
                displayFieldProperty = pInfo;
            else if (pInfo.Name == displayFieldPropertyName) // This is the configured value from the configuration file.
            {
                displayFieldProperty = pInfo;
                break;
            }
            else if (pInfo.PropertyType == typeof(string))
            {
                displayFieldProperty = pInfo;
                break;  // Once we find a string column to use, just break out of the foreach.
            }
        }

        foreach (DataGridColumn column in dataGrid.Columns)
        {
            if (column.GetType() == typeof(BoundColumn))
            {
                BoundColumn boundColumn = (BoundColumn)column;
                boundColumn.DataField = displayFieldProperty.Name;
            }
        }

        object itemToCreateOrEdit = new object();
        MonkData db = new MonkData();
        object[] queryParams = { };



        string nameOfFieldToOrderBy = string.Empty;
        foreach (Control ctlButton in plcOrderByFields.Controls)
        {
            RadioButton rbtnItem = (RadioButton)ctlButton;
            if (rbtnItem.Checked)
            {
                nameOfFieldToOrderBy = rbtnItem.ID.Replace("orderby_", "");
                break;
            }
        }

        #region Building SQL strings to do sorting and viewing of grid
        //string likeConditions = "";
        //if (!String.IsNullOrEmpty(txtSearch.Text))
        //{
        //    foreach (Control cnltChkBox in plcSearchFields.Controls)
        //    {
        //        if (cnltChkBox.GetType() == typeof(CheckBox))
        //        {
        //            CheckBox chkBox = (CheckBox)cnltChkBox;
        //            if (chkBox.Checked)
        //            {
        //                if (!String.IsNullOrEmpty(likeConditions))
        //                {
        //                    likeConditions += " OR ";
        //                }

        //                likeConditions += chkBox.ID + " LIKE '%" + txtSearch.Text + "%'";
        //            }
        //        }
        //    }
        //}

        //string tableName = ((System.Data.Linq.Mapping.TableAttribute)DataType.GetCustomAttributes(typeof(System.Data.Linq.Mapping.TableAttribute), false).First()).Name;
        //string sqlExpression = "";
        //if (!String.IsNullOrEmpty(likeConditions))
        //    sqlExpression = "SELECT * FROM " + tableName;
        //else
        //    sqlExpression = "SELECT * FROM " + tableName;

        //string nameOfFieldToOrderBy = string.Empty;
        //foreach (Control ctlButton in plcOrderByFields.Controls)
        //{
        //    RadioButton rbtnItem = (RadioButton)ctlButton;
        //    if (rbtnItem.Checked)
        //    {
        //        nameOfFieldToOrderBy = rbtnItem.ID.Replace("orderby_", "");
        //        break;
        //    }
        //}
        //if (!String.IsNullOrEmpty(nameOfFieldToOrderBy))
        //    sqlExpression += " ORDER BY " + nameOfFieldToOrderBy + " " + (rbtnAscending.Checked ? "ASC" : "DESC");
        ////sqlDataSource.SelectCommand = sqlExpression;
        ////sqlDataSource.ConnectionString = db.Connection.ConnectionString;
        ////sqlDataSource.FilterExpression = likeConditions;

        //var getSQLForPaging = from a in db.aspnet_Memberships
        //                      select a;
        //getSQLForPaging = getSQLForPaging.Skip(5).Take(5);

        //object[] paramsForQuery = { };
        //dataGrid.DataSourceID = null;



        //var dataResultsCounting = db.ExecuteQuery(DataType, sqlExpression, paramsForQuery);
        //var dataResults = db.ExecuteQuery(DataType, sqlExpression, paramsForQuery);
        //System.Linq.Queryable queryableType;


        //Type[] typesForQuerableMethod = { DataType };
        //queryableType.GetType().GetMethod("Skip").MakeGenericMethod(typesForQuerableMethod).Invoke(null, { 5 });


        //dataGrid.DataSource = dataResults;

        //int resultCount = 0;
        //foreach(object c in dataResultsCounting)
        //    resultCount++;

        //MethodInfo countMethod = dataResults.GetType().GetMethod("Count");
        //dataGrid.VirtualItemCount = resultCount;

        //dataGrid.PageSize = int.Parse(dlResultCount.SelectedItem.Text);
        //dataGrid.CurrentPageIndex = currentPage;

        //dataGrid.DataBind(); 
        #endregion






        string tableName = ((System.Data.Linq.Mapping.TableAttribute)DataType.GetCustomAttributes(typeof(System.Data.Linq.Mapping.TableAttribute), false).First()).Name;

        string nameOfPropertyOnDataContextObject = db.GetType().GetProperties().Where(p => p.Name.StartsWith(DataType.Name.Substring(0, DataType.Name.Length - 2))).OrderBy(p => p.Name.Length).First().Name;


        linqDataSource.AutoPage = true;
        if (!String.IsNullOrEmpty(nameOfFieldToOrderBy))
            linqDataSource.OrderBy = nameOfFieldToOrderBy + " " + (rbtnAscending.Checked ? "ASC" : "DESC");

        linqDataSource.TableName = nameOfPropertyOnDataContextObject;
        linqDataSource.EnableUpdate = true;

        linqDataSource.WhereParameters.Clear();
        string likeConditions = "";
        if (!String.IsNullOrEmpty(txtSearch.Text))
        {
            linqDataSource.WhereParameters.Add("search_value", txtSearch.Text);
            //linqDataSource.SelectParameters.Add("search_value", txtSearch.Text);

            foreach (Control cnltChkBox in plcSearchFields.Controls)
            {
                if (cnltChkBox.GetType() == typeof(RadioButton))
                {
                    RadioButton chkBox = (RadioButton)cnltChkBox;
                    if (chkBox.Checked)
                    {
                        if (!String.IsNullOrEmpty(likeConditions))
                        {
                            likeConditions += " || ";
                        }

                        likeConditions += chkBox.ID + ".Contains(@search_value)";
                    }
                }
            }
        }

        linqDataSource.Where = "";


        if (!String.IsNullOrEmpty(likeConditions))
            linqDataSource.Where = likeConditions;

        dataGrid.PageSize = int.Parse(dlResultCount.Text);
        dataGrid.DataBind();

    }
    int currentPage = 0;
    public void pageIndexChanged(object sender, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dataGrid.CurrentPageIndex = e.NewPageIndex;
        currentPage = e.NewPageIndex;
    }

    public void dataGrid_OnItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            object dataItem = (object)e.Item.DataItem;
            HyperLink hlEdit = (HyperLink)e.Item.FindControl("hlEditItem");
            LinkButton lbtnDeleteItem = (LinkButton)e.Item.FindControl("lbtnDeleteItem");

            XmlNode databaseTable = GetDatabaseTable(DataType.Name);
            if (databaseTable == null || databaseTable.SelectSingleNode("EditLink/URLFormatString") == null)
                hlEdit.NavigateUrl = "AddEdit.aspx?typename=" + DataType.Name + "&itemid=" + primaryKeyProp.GetValue(dataItem, null).ToString();
            else
            {
                string editLinkFormatString = databaseTable.SelectSingleNode("EditLink/URLFormatString").InnerText;
                string editUrl = string.Format(editLinkFormatString, primaryKeyProp.GetValue(dataItem, null).ToString());
                hlEdit.NavigateUrl = editUrl;

            }
            lbtnDeleteItem.Attributes.Add("itemid", primaryKeyProp.GetValue(dataItem, null).ToString());



        }
    }

    private PropertyInfo GetPrimaryKeyProperty(Type dataTypeToFind)
    {
        PropertyInfo primaryKey;
        var primaryKeyColumns = from p in dataTypeToFind.GetProperties()
                                where p.GetCustomAttributes(typeof(ColumnAttribute), false).Where(a => ((ColumnAttribute)a).IsPrimaryKey == true).Count() > 0
                                select p;
        if (primaryKeyColumns.Count() == 0)
            throw new Exception("There was an error finding the primary key column on this object. Please make sure that the DataType " + DataType.ToString());
        primaryKey = primaryKeyColumns.First();
        return primaryKey;
    }
    protected void linqDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {

    }

    protected void btnSearch_Clicked(object sender, EventArgs e)
    {
        dataGrid.CurrentPageIndex = 0;
    }

    protected void dlResultCount_Changed(object sender, EventArgs e)
    {
        dataGrid.CurrentPageIndex = 0;

    }

    public void btnDelete_Clicked(object sender, EventArgs e)
    {
        LinkButton lbtnDelete = (LinkButton)sender;
        Guid itemIdToDelete = new Guid(lbtnDelete.Attributes["itemid"]);
        MonkData db = new MonkData();
        string tableName = ((System.Data.Linq.Mapping.TableAttribute)DataType.GetCustomAttributes(typeof(System.Data.Linq.Mapping.TableAttribute), false).First()).Name;
        primaryKeyProp = GetPrimaryKeyProperty(DataType);
        db.ExecuteCommand("DELETE FROM " + tableName + " WHERE " + primaryKeyProp.Name + " = '" + itemIdToDelete.ToString() + "'");
        db.SubmitChanges();
    }
}
