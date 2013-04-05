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
using FreeTextBoxControls;
using System.Xml;

public partial class AddEdit : LoggedInPage
{


    public Type DataType
    {
        get
        {
            return BuildManager.GetType("Monks." + Request.QueryString["typename"], true);
        }
    }

    public Guid? ItemId
    {
        get
        {
            if (String.IsNullOrEmpty(Request.QueryString["itemid"]))
                return null;
            return new Guid(Request.QueryString["itemid"]);
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        CreateFields();
        litSavedTitle.Text = CreateFieldNameFromString(DataType.Name);
        litTitle.Text = CreateFieldNameFromString(DataType.Name);
        litSuccessItemType.Text = CreateFieldNameFromString(DataType.Name);
        this.Title += CreateFieldNameFromString(DataType.Name);
        CheckTablePermission();
    }



    public void CreateFields()
    {
        string tableName = ((System.Data.Linq.Mapping.TableAttribute)DataType.GetCustomAttributes(typeof(System.Data.Linq.Mapping.TableAttribute), false).First()).Name;

        MonkData db = new MonkData();

        PropertyInfo[] properties = DataType.GetProperties();
        // Look at each property on the object type
        foreach (PropertyInfo prop in properties)
        {
            Type propertyType = prop.PropertyType;

            object[] attributes = prop.GetCustomAttributes(false);
            if (attributes.Count() > 0)
                if (attributes[0].GetType() == typeof(ColumnAttribute))
                {
                    ColumnAttribute column = (ColumnAttribute)attributes[0];

                    if (column.DbType.Contains("Text"))
                    {
                        CreateFieldName(prop);
                        CreateLineBreak();
                        // Show cool edit textbox
                        FreeTextBoxControls.FreeTextBox textBox = new FreeTextBox();
                        textBox.Width = new Unit(920);
                        textBox.Height = new Unit(350);
                        textBox.ID = prop.Name;
                        plcForm.Controls.Add(textBox);
                        CreateLineBreak();


                    }
                    else if (column.DbType.Contains("Decimal"))
                    {
                        CreateFieldName(prop);
                        CreateLineBreak();
                        TextBox txtDecimal = new TextBox();
                        if (!Page.IsPostBack)
                            txtDecimal.Text = "0.00";
                        txtDecimal.ID = prop.Name;
                        plcForm.Controls.Add(txtDecimal);
                        RequiredFieldValidator reqDecimal = new RequiredFieldValidator();
                        reqDecimal.ControlToValidate = prop.Name;
                        reqDecimal.Text = "Required Decimal Number";
                        reqDecimal.ID = "reqDecimal_" + prop.Name;
                        reqDecimal.ValidationGroup = "AddEdit";
                        plcForm.Controls.Add(reqDecimal);
                        CreateLineBreak();
                    }
                    else if (column.DbType.Contains("Bit"))
                    {
                        CheckBox chkBox = new CheckBox();
                        chkBox.ID = prop.Name;
                        plcForm.Controls.Add(chkBox);
                        CreateFieldName(prop);
                        CreateLineBreak();

                    }
                    else if (column.DbType.Contains("VarChar") || column.DbType.Contains("NChar"))
                    {


                        int maxLength = 8000;
                        string number = column.DbType.Replace("NVarChar", "").Replace("NChar", "").Replace("VarChar", "").Replace("(", "").Replace(")", "").Replace(" NOT NULL", "");

                        if (number.Contains("N128"))
                        {

                        }

                        if (number.Contains("MAX") || number.Contains("max"))
                        {
                            maxLength = 8000;
                        }
                        else
                        {
                            maxLength = int.Parse(number);
                        }

                        CreateFieldName(prop);
                        CreateLineBreak();
                        // Get the max length of the varchar
                        TextBox txtBox = new TextBox();
                        txtBox.ID = prop.Name;
                        txtBox.MaxLength = maxLength;
                        plcForm.Controls.Add(txtBox);

                        CreateLineBreak();
                    }
                    else if (column.DbType == "UniqueIdentifier")
                    {
                        // Don't show GUID columns
                    }
                    else if (column.DbType.Contains("DateTime"))
                    {
                        CreateFieldName(prop);
                        CreateLineBreak();
                        // Get the max length of the varchar
                        TextBox txtBox = new TextBox();
                        txtBox.ID = prop.Name;

                        if (ItemId == null && !Page.IsPostBack)
                            txtBox.Text = DateTime.Now.ToShortDateString();

                        plcForm.Controls.Add(txtBox);

                        // Validate date information in box.
                        RegularExpressionValidator regVal = new RegularExpressionValidator();
                        regVal.ID = "reg" + prop.Name;
                        regVal.ControlToValidate = txtBox.ID;
                        regVal.Text = "Must be in the format of '1:01 AM' or 23:52:01";
                        regVal.ValidationExpression = @"(?=\d)^(?:(?!(?:10\D(?:0?[5-9]|1[0-4])\D(?:1582))|(?:0?9\D(?:0?[3-9]|1[0-3])\D(?:1752)))((?:0?[13578]|1[02])|(?:0?[469]|11)(?!\/31)(?!-31)(?!\.31)|(?:0?2(?=.?(?:(?:29.(?!000[04]|(?:(?:1[^0-6]|[2468][^048]|[3579][^26])00))(?:(?:(?:\d\d)(?:[02468][048]|[13579][26])(?!\x20BC))|(?:00(?:42|3[0369]|2[147]|1[258]|09)\x20BC))))))|(?:0?2(?=.(?:(?:\d\D)|(?:[01]\d)|(?:2[0-8])))))([-.\/])(0?[1-9]|[12]\d|3[01])\2(?!0000)((?=(?:00(?:4[0-5]|[0-3]?\d)\x20BC)|(?:\d{4}(?!\x20BC)))\d{4}(?:\x20BC)?)(?:$|(?=\x20\d)\x20))?((?:(?:0?[1-9]|1[012])(?::[0-5]\d){0,2}(?:\x20[aApP][mM]))|(?:[01]\d|2[0-3])(?::[0-5]\d){1,2})?$";
                        regVal.ValidationGroup = "AddEdit";
                        plcForm.Controls.Add(regVal);

                        // Add ajax popup calendar for date selection
                        AjaxControlToolkit.CalendarExtender popupCal = new AjaxControlToolkit.CalendarExtender();
                        popupCal.ID = "popCal" + prop.Name;
                        popupCal.TargetControlID = txtBox.ID;
                        popupCal.Format = "MM/dd/yyyy";
                        plcForm.Controls.Add(popupCal);

                        CreateLineBreak();
                    }
                    else if (column.DbType.Contains("Int"))
                    {
                        int maxLength = 10;
                        CreateFieldName(prop);
                        CreateLineBreak();
                        TextBox txtBox = new TextBox();
                        txtBox.ID = prop.Name;
                        txtBox.MaxLength = maxLength;
                        if (!Page.IsPostBack)
                            txtBox.Text = "0";

                        plcForm.Controls.Add(txtBox);
                        RequiredFieldValidator reqInt = new RequiredFieldValidator();
                        reqInt.ID = "reqInt_" + prop.Name;
                        reqInt.Text = "Required integer number";
                        reqInt.ControlToValidate = txtBox.ID;
                        reqInt.ValidationGroup = "AddEdit";
                        plcForm.Controls.Add(reqInt);

                        CreateLineBreak();
                    }

                }
                else if (attributes[0].GetType() == typeof(AssociationAttribute))
                {
                    // Load a dropdown list
                    AssociationAttribute assocAttrib = (AssociationAttribute)attributes[0];
                    if (!String.IsNullOrEmpty(assocAttrib.ThisKey) && assocAttrib.IsForeignKey)
                    {
                        // The field that points to another table. 
                        PropertyInfo sourcePropertyInfo = DataType.GetProperties().First(p => p.Name == assocAttrib.ThisKey);

                        // Create a dropdown list.
                        DropDownList dropList = new DropDownList();
                        dropList.ID = prop.Name;

                        // Get the type that is in the association that we're going to use.
                        Type typeOfItems;

                        typeOfItems = BuildManager.GetType("Monks." + prop.PropertyType.Name, true);


                        // Get the primary key for that type
                        PropertyInfo primaryKeyOfItems = GetPrimaryKeyProperty(typeOfItems);

                        // Get the property from the table 
                        var matchingProperties = db.GetType().GetProperties().Where(p => p.Name.StartsWith(typeOfItems.Name.Substring(0, typeOfItems.Name.Length - 1))).OrderBy(p => p.Name.Length);

                        if (matchingProperties.Count() < 1)
                            continue;

                        IEnumerable itemsInTable = (IEnumerable)matchingProperties.First().GetValue(db, null);
                        if (sourcePropertyInfo.PropertyType.Name.Contains("Nullable"))
                            dropList.Items.Add(new ListItem("null", "null"));

                        string tableNameReferenceTable = ((System.Data.Linq.Mapping.TableAttribute)prop.PropertyType.GetCustomAttributes(typeof(System.Data.Linq.Mapping.TableAttribute), false).First()).Name;


                        XmlNode xmlReferenceTableRules = GetDatabaseTable(prop.PropertyType.Name);

                        XmlNode xmlDisplayPropertyName = null;
                        if (xmlReferenceTableRules != null)
                            xmlDisplayPropertyName = xmlReferenceTableRules.SelectSingleNode("TitleColumn");


                        // Loop over each item in the table
                        foreach (object item in itemsInTable)
                        {
                            // If the column is a string, create the object.
                            foreach (PropertyInfo itemPInfo in typeOfItems.GetProperties())
                            {
                                if (xmlDisplayPropertyName != null)
                                {
                                    if (xmlDisplayPropertyName.InnerText == itemPInfo.Name)
                                    {
                                        ListItem dListItem = new ListItem();

                                        dListItem.Text = itemPInfo.GetValue(item, null).ToString().Replace("&nbsp;", " - ");
                                        dListItem.Value = primaryKeyOfItems.GetValue(item, null).ToString();

                                        dropList.Items.Add(dListItem);
                                        break;
                                    }
                                }
                                else if (itemPInfo.PropertyType == typeof(string))
                                {
                                    ListItem dListItem = new ListItem();
                                    if (itemPInfo.GetValue(item, null) == null)
                                        continue;

                                    dListItem.Text = itemPInfo.GetValue(item, null).ToString();
                                    dListItem.Value = primaryKeyOfItems.GetValue(item, null).ToString();

                                    dropList.Items.Add(dListItem);
                                    break;
                                }

                            }
                        }
                        CreateFieldName(prop);
                        CreateLineBreak();
                        plcForm.Controls.Add(dropList);

                        HyperLink hlNewItem = new HyperLink();
                        hlNewItem.Style.Add(HtmlTextWriterStyle.PaddingLeft, "10px");
                        hlNewItem.NavigateUrl = "AddEdit.aspx?typename=" + typeOfItems.Name;
                        hlNewItem.Text = "Add New " + CreateFieldNameFromString(typeOfItems.Name);
                        plcForm.Controls.Add(hlNewItem);

                        Literal litSpace = new Literal();
                        litSpace.Text = " | ";
                        plcForm.Controls.Add(litSpace);

                        LinkButton lbtnViewCurrentSelectedItem = new LinkButton();
                        lbtnViewCurrentSelectedItem.Text = "View Selected Item";
                        lbtnViewCurrentSelectedItem.ID = "lbtnViewSelectedItem_" + prop.Name;
                        lbtnViewCurrentSelectedItem.Click += new EventHandler(lbtnViewCurrentSelectedItem_Clicked);
                        plcForm.Controls.Add(lbtnViewCurrentSelectedItem);

                        Literal litSpace2 = new Literal();
                        litSpace2.Text = " | ";
                        plcForm.Controls.Add(litSpace2);


                        HyperLink hlGridViewForDropDown = new HyperLink();
                        hlGridViewForDropDown.Text = "Search";
                        hlGridViewForDropDown.NavigateUrl = "GridView.aspx?typename=" + prop.PropertyType.Name;
                        plcForm.Controls.Add(hlGridViewForDropDown);

                        CreateLineBreak();
                    }
                }
        }
    }

    public void lbtnViewCurrentSelectedItem_Clicked(object sender, EventArgs e)
    {
        LinkButton lbtnViewCurrentSelectedItem = (LinkButton)sender;
        string propertyName = lbtnViewCurrentSelectedItem.ID.Replace("lbtnViewSelectedItem_", "");
        DropDownList dlListOfItems = (DropDownList)plcForm.FindControl(propertyName);
        Guid selectedID = new Guid(dlListOfItems.SelectedValue);
        string propertyTypeName = DataType.GetProperty(propertyName).PropertyType.Name;

        Response.Redirect("AddEdit.aspx?typename=" + propertyTypeName + "&itemid=" + selectedID.ToString());

    }

    private void CreateLineBreak()
    {
        Literal litLineBreak = new Literal();
        litLineBreak.Text = "<br />";
        plcForm.Controls.Add(litLineBreak);
    }

    private void CreateFieldName(PropertyInfo prop)
    {
        Literal litFieldName = new Literal();
        string propertyName = prop.Name;

        string newFieldName = CreateFieldNameFromString(propertyName);
        litFieldName.Text = newFieldName;
        litFieldName.Text += " ";

        if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
        {
            litFieldName.Text += "(DateTime)";
        }
        else if(prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
        {
            litFieldName.Text += "(int)";
        }
        else if(prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(decimal?))
        {
            litFieldName.Text += "(decimal)";
        }
        plcForm.Controls.Add(litFieldName);
    }

    private static string CreateFieldNameFromString(string propertyName)
    {
        string fieldName = string.Empty;
        if (propertyName.Contains("_"))
            fieldName = propertyName.Substring(propertyName.IndexOf("_") + 1);
        else
            fieldName = propertyName;
        string newFieldName = string.Empty;
        foreach (char fieldNameChar in fieldName.ToCharArray())
        {
            if (!String.IsNullOrEmpty(newFieldName) && Char.IsUpper(fieldNameChar))
                newFieldName += " ";
            newFieldName += fieldNameChar;

        }
        return newFieldName;
    }


    protected void Page_Load(object sender, EventArgs e)
    {


    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);



        LoadData();



    }

    private void LoadData()
    {
        // Find the primary key property
        PropertyInfo primaryKey = GetPrimaryKeyProperty(DataType);

        if (ItemId != null)
        {

            object itemToCreateOrEdit = new object();
            MonkData db = new MonkData();
            object[] queryParams = { };
            string tableName = ((System.Data.Linq.Mapping.TableAttribute)DataType.GetCustomAttributes(typeof(System.Data.Linq.Mapping.TableAttribute), false).First()).Name;
            // Load the object from the database.
            IEnumerable itemsFromDB = db.ExecuteQuery(DataType, "select * from " + tableName + " where " + primaryKey.Name + " = '" + ItemId.ToString() + "'", queryParams);
            foreach (object item in itemsFromDB)
            {
                itemToCreateOrEdit = item;
            }


            foreach (PropertyInfo prop in itemToCreateOrEdit.GetType().GetProperties())
            {
                foreach (Control cntl in plcForm.Controls)
                {
                    if (cntl.ID == prop.Name) // Look to see if this is the property
                    {
                        if (cntl.GetType() == typeof(TextBox))
                        {
                            TextBox txtControl = (TextBox)cntl;
                            if (prop.GetValue(itemToCreateOrEdit, null) != null)
                                txtControl.Text = prop.GetValue(itemToCreateOrEdit, null).ToString();
                        }
                        else if (cntl.GetType() == typeof(FreeTextBox))
                        {
                            FreeTextBox txtControl = (FreeTextBox)cntl;
                            if (prop.GetValue(itemToCreateOrEdit, null) != null)
                                txtControl.Text = prop.GetValue(itemToCreateOrEdit, null).ToString();
                        }
                        else if (cntl.GetType() == typeof(CheckBox))
                        {
                            CheckBox chkBox = (CheckBox)cntl;
                            if (prop.GetValue(itemToCreateOrEdit, null) != null)
                                chkBox.Checked = (bool)prop.GetValue(itemToCreateOrEdit, null);
                        }
                        else if (cntl.GetType() == typeof(DropDownList))
                        {
                            DropDownList dList = (DropDownList)cntl;

                            PropertyInfo primaryKeyOfReferencedTable = GetPrimaryKeyProperty(prop.PropertyType);


                            if (prop.GetValue(itemToCreateOrEdit, null) != null)
                            {
                                ListItem itemToSelect = dList.Items.FindByValue(primaryKeyOfReferencedTable.GetValue(prop.GetValue(itemToCreateOrEdit, null), null).ToString());
                                if (itemToSelect == null)
                                {
                                    ListItem nullListItem = dList.Items.FindByValue("null");
                                    if (nullListItem != null)
                                        nullListItem.Selected = true;
                                }
                                else
                                    itemToSelect.Selected = true;
                            }
                            else
                            {

                                ListItem itemToSelect = dList.Items.FindByValue("null");
                                if (itemToSelect != null)
                                    itemToSelect.Selected = true;
                            }
                        }
                    }
                }

            }
        }
    }

    public void btnSave_Clicked(object sender, EventArgs e)
    {


        object itemToCreateOrEdit;
        MonkData db = new MonkData();

        // Find the primary key property
        PropertyInfo primaryKey = GetPrimaryKeyProperty(DataType);

        if (ItemId == null)
        {
            // Dynamically get the datatype
            Type[] emptyConstructorArgs = { };
            itemToCreateOrEdit = DataType.GetConstructor(emptyConstructorArgs).Invoke(null);
        }
        else
        {
            // Load the object from the database.

            Type[] emptyConstructorArgs = { };
            string tableName = ((System.Data.Linq.Mapping.TableAttribute)DataType.GetCustomAttributes(typeof(System.Data.Linq.Mapping.TableAttribute), false).First()).Name;

            IEnumerable itemsFromDB = db.ExecuteQuery(DataType, "select * from " + tableName + " where " + primaryKey.Name + " = '" + ItemId.ToString() + "'", emptyConstructorArgs);
            IEnumerator dbItemsEnumberator = itemsFromDB.GetEnumerator();
            dbItemsEnumberator.MoveNext();
            itemToCreateOrEdit = dbItemsEnumberator.Current;
        }

        // Load all the values from the form into the object
        foreach (Control cntItem in plcForm.Controls)
        {
            var propertiesFound = from p in DataType.GetProperties()
                                  where p.Name == cntItem.ID
                                  select p;
            if (propertiesFound.Count() < 1)
                continue;
            PropertyInfo pInfo = propertiesFound.First();
            object[] attributesOfProperty = pInfo.GetCustomAttributes(false);
            if (attributesOfProperty[0].GetType() == typeof(ColumnAttribute))
            {
                ColumnAttribute columnAttrib = (ColumnAttribute)attributesOfProperty[0];
                if (columnAttrib.DbType.Contains("VarChar") || columnAttrib.DbType.Contains("NChar"))
                {
                    TextBox txtControl = (TextBox)cntItem;
                    pInfo.SetValue(itemToCreateOrEdit, txtControl.Text, null);
                }
                else if (columnAttrib.DbType.Contains("Decimal"))
                {
                    TextBox txtControl = (TextBox)cntItem;
                    pInfo.SetValue(itemToCreateOrEdit, decimal.Parse(txtControl.Text), null);
                }
                else if (columnAttrib.DbType.Contains("DateTime"))
                {
                    TextBox txtControl = (TextBox)cntItem;
                    if (!String.IsNullOrEmpty(txtControl.Text))
                    {
                        DateTime time; 
                        
                        // Check if the datetime supplied is actually parseable.
                        if(!DateTime.TryParse(txtControl.Text, out time))
                        {
                            throw new Exception("Failed to parse the datetime in the textbox to the proper datetime format. An example is 12/31/2008 12:59:59 AM. The field was " + GetFieldNameFromString(pInfo.Name));
                        }
                        
                        pInfo.SetValue(itemToCreateOrEdit, time, null);
                    }
                    else if (columnAttrib.CanBeNull)
                        pInfo.SetValue(itemToCreateOrEdit, null, null);
                    else if (!columnAttrib.CanBeNull)
                        throw new Exception("Tried to insert a null datetime into a field tha required a datetime. Field was " + GetFieldNameFromString(pInfo.Name));
                }
                else if (columnAttrib.DbType.Contains("Text"))
                {
                    FreeTextBox txtControl = (FreeTextBox)cntItem;
                    pInfo.SetValue(itemToCreateOrEdit, txtControl.Text, null);
                }
                else if (columnAttrib.DbType.Contains("Bit"))
                {
                    CheckBox chkBox = (CheckBox)cntItem;
                    pInfo.SetValue(itemToCreateOrEdit, chkBox.Checked, null);
                }
                else if (columnAttrib.DbType.Contains("Int"))
                {
                    TextBox txtControl = (TextBox)cntItem;

                    int parsedValue = 0;
                    if(!int.TryParse(txtControl.Text, out parsedValue))
                    {
                        throw new Exception("Failed converting the text in the textbox to an integer. The property name was " + GetFieldNameFromString(pInfo.Name));
                    }
                    

                    pInfo.SetValue(itemToCreateOrEdit, parsedValue, null);
                }
            }
            else if (attributesOfProperty[0].GetType() == typeof(AssociationAttribute))
            {
                AssociationAttribute assocAttrib = (AssociationAttribute)attributesOfProperty[0];

                if (cntItem.GetType() == typeof(DropDownList))
                {
                    DropDownList dList = (DropDownList)cntItem;
                    PropertyInfo propIDOfOtherTableItem = DataType.GetProperties().First(p => p.Name == assocAttrib.ThisKey);
                    if (dList.SelectedValue == "null" || dList.SelectedValue == "")
                    {
                        propIDOfOtherTableItem.SetValue(itemToCreateOrEdit, null, null);
                    }
                    else
                    {
                        Guid selectedGuidOfItem = new Guid(dList.SelectedValue);
                        propIDOfOtherTableItem.SetValue(itemToCreateOrEdit, selectedGuidOfItem, null);
                    }
                }
            }

        }
        // Save the object...

        if (ItemId == null)
        {
            if (primaryKey.PropertyType != typeof(Guid))
                throw new Exception("Primary key of " + DataType.Name + " is not a Guid.");
            primaryKey.SetValue(itemToCreateOrEdit, Guid.NewGuid(), null);

            object[] itemToInsert = { itemToCreateOrEdit };
            //                           Where finds the table we need to examine    // Find the one that's shortest   //Get prop vlaue     // Get the method to insert  // Insert
            var propertyInfosForDatabaseWhereName = db.GetType().GetProperties().Where(p => p.Name.StartsWith(DataType.Name.Substring(0, DataType.Name.Length - 1)));
            var propertyObjectOfTableFromDB = propertyInfosForDatabaseWhereName.OrderBy(p => p.Name.Length).First().GetValue(db, null);
            propertyObjectOfTableFromDB.GetType().GetMethod("InsertOnSubmit").Invoke(propertyObjectOfTableFromDB, itemToInsert);

        }

        db.SubmitChanges();
        mvAddEdit.ActiveViewIndex = 1;

        hlViewItem.NavigateUrl = "AddEdit.aspx?typename=" + DataType.Name + "&itemid=" + primaryKey.GetValue(itemToCreateOrEdit, null).ToString();
        hlReturnToList.NavigateUrl = "GridView.aspx?typename=" + DataType.Name;
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
}
