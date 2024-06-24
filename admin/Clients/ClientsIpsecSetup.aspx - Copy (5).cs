using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration.Provider;
using System.Configuration;

public partial class admin_parameters_User : System.Web.UI.Page
{
    public void MsgBox(String ex, Page pg, Object obj)
    {
        string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
        Type cstype = obj.GetType();
        ClientScriptManager cs = pg.ClientScript;
        cs.RegisterClientScriptBlock(cstype, s, s.ToString());
    }
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conpath"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
         
           
            loadusertypes();
            getIpsecClient();

            lbUsername.Text ="Logged in as" +" "+ " " + (string)Session["username"] + ""+ ""+ (string)Session["role"];

            //Boolean user=  checkuser(txtFirstName.Text, txtSurname.Text, txtUsername.Text);
            //  if(user)
            //  {
            //      MsgBox("User Already Exists", this.Page, this);

            //  }
        }
        
    }
    public Boolean checkIpsec( String clientid, String assetclassid,String assetmanagerid)
    {
        Boolean existance = false;
        conn.Close();
        try
             
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Ipsecs where client_id='" + clientid + "' and  Asset_class_id='" + assetclassid + "' and Asset_manager_id ='" + assetmanagerid + "' and active='1' ", conn);
            int count = int.Parse(cmd.ExecuteScalar().ToString());
            if (count >= 1)
            {
                existance = true;
            }
           
        }


        
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
        return existance;




    }
    public Boolean checkIpsecClient(String clientid, String assetclassid)
    {
        Boolean existance = false;
        conn.Close();
        try

        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Ipsecs where client_id='" + clientid + "' and  Asset_class_id='" + assetclassid + "' and allocation_level='client' and active='1' ", conn);
            int count = int.Parse(cmd.ExecuteScalar().ToString());
            if (count >= 1)
            {
                existance = true;
            }

        }



        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
        return existance;

    }
    public void InsertIpsec (String clientid, String assetclassid, String assetmanagerid , String value)
    {
        decimal sumFrmDB;
        decimal userPercentage;
        decimal percentageleft;
        //decimal userPercentageOld;
        try
        {
            conn.Close();
            conn.Open();

            String query = "INSERT INTO Ipsecs(client_id ,Asset_class_id, Asset_manager_id ,value,allocation_level , Corridor) values('" + clientid + "','" + assetclassid + "','" + assetmanagerid + "','" + value + "','assetmanager' ,'" + txtCorridorTop.Text + "-" + txtCorridorBottom.Text + "')";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteReader(CommandBehavior.CloseConnection);

            getSumFromDBAssetManagers();
            sumFrmDB = Convert.ToDecimal(txtSumFromDB.Text);
            userPercentage = Convert.ToDecimal(txtAllocation.Text);
            percentageleft = 100 - sumFrmDB;
            MsgBox("Asset Allocation Successfully Captured " + "You have " + percentageleft + " % left", Page, this);
        }
        catch (Exception ex)
        {
            MsgBox("Error: " + ex.Message, this.Page, this);
            throw;
        }


    }
    public void InsertIpsecClient(String clientid, String value ,String assetclass)
    {
        decimal sumFrmDB;
        decimal userPercentage;
        decimal percentageleft;
        //decimal userPercentageOld;
        try
        {
            conn.Close();
            conn.Open();
            
            String query = "INSERT INTO Ipsecs(client_id ,value,allocation_level ,asset_class_id ,asset_manager_id ,Corridor) values('" + clientid + "','" + value + "','client', '"+ assetclass+"' ,'"+ txtFirstName.Text+ "',  '" + txtCorridorTop.Text + "-" + txtCorridorBottom.Text + "')";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ////MsgBox("Insert exit ", Page, this);
            ////MsgBox("sumFrmDB is => " + sumFrmDB , Page, this);
            //userPercentage = Convert.ToDecimal(txtAllocation.Text);
            ////MsgBox("userPercentage is => " + userPercentage, Page, this);

            getSumFromDBClient();
            sumFrmDB = Convert.ToDecimal(txtSumFromDB.Text);
            userPercentage = Convert.ToDecimal(txtAllocation.Text);
            //MsgBox("userPercentage is => " + userPercentage, Page, this);
            //MsgBox("sumFrmDB is => " + sumFrmDB, Page, this);
            percentageleft = 100 - sumFrmDB; 

            MsgBox("Asset Allocation Successfully Captured " + "You have " + percentageleft + " % left", Page, this);
        }
        catch (Exception ex)
        {
            MsgBox("Error: " + ex.Message, this.Page, this);
            throw;
        }
    }



    public void loadusertypes()
    {
        
        string com = "Select * from assets_class where active ='1'";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        

        cmbAsetClass.DataSource = dt;
        cmbAsetClass.DataBind();
        cmbAsetClass.DataTextField = "asset_name";
        cmbAsetClass.DataValueField = "asset_name";
        cmbAsetClass.DataBind();
        cmbAsetClass.Items.Insert(0, new ListItem("Select An Asset Class", "0"));
        // cmbAsetClass.Items.Insert(0, new ListItem("Select an asset", "0"));


    }
   
    public void loadassetmanagers()
    {
        conn.Close();
        conn.Open();
        string com = " select c.assetmanager_id as id,a.surname as name,c.client_id from client_assetmanager_relations c join clients q on c.client_id=q.client_number join asset_managers a on a.id=c.assetmanager_id where c.client_id='"+ txtID.Text+"'";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        rdAssetMangers.DataSource = dt;
        rdAssetMangers.DataBind();
        rdAssetMangers.DataTextField = "name";
        rdAssetMangers.DataValueField = "id";
        rdAssetMangers.DataBind();
        rdAssetMangers.Items.Insert(0, new ListItem("Select An Asset Manager", "0"));

    }


    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    String id = txtID.Text.ToString();
    //    Boolean edited = edituser(id);
    //    if (edited)
    //    {
    //        MsgBox("Update Successful", this.Page, this);


    //        usersPanel.Visible = false;
    //    }
    //}
    //public Boolean edituser(string id)
    //{

    //    {
    //        SqlCommand cmd = new SqlCommand("update SystemUsers set name='" + txtFirstName.Text + "' ,surname='" + txtSurname.Text + "',password='" + txtPassword.Text + "' ,role='" + rdRole.SelectedItem.Text + "', username='" + txtUsername.Text + "',role_id='" + rdRole.SelectedValue + "' where id='" + txtID.Text + "' ", conn);
    //        if ((conn.State == ConnectionState.Open))
    //            conn.Close();
    //        conn.Open();
    //        cmd.ExecuteNonQuery();
    //        conn.Close();

    //    }
    //    return true;
    //}
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("User.aspx");
    }

    public void checkPecerntageLeft(String clientid)
    {
        conn.Close();
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select 1-sum(Value)  as PercentageLeft from Ipsecs where client_id='" + clientid + "' and active='1' and allocation_level='client'", conn);
            int percentageleft = int.Parse(cmd.ExecuteScalar().ToString());
            if (percentageleft < 1)
            {
                MsgBox("You have " + percentageleft + "% left", this.Page, this);
            }
            else if (percentageleft > 1)
            {
                MsgBox("Total Asset Class Percentage Exceeded. Please insert another percentage", this.Page, this);
            }
        }

        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
    }


    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {
            conn.Close();
            conn.Open();
            
            SqlCommand cmd = new SqlCommand("select name from clients where name like '%" + txtsearch.Text +"%'  ", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("clients");
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                

                 lstNamesSearch.DataSource = ds;
                lstNamesSearch.DataTextField = "name";
                lstNamesSearch.DataValueField = "name";

                lstNamesSearch.DataBind();
                
            }
            else
            {
                lstNamesSearch.DataSource = null;
                lstNamesSearch.DataBind();
            }





            conn.Close();

        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
    }

    

    protected void lstNamesSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        conn.Close();
        conn.Open();
        //MsgBox(lstNamesSearch.Text, Page, this);
        SqlCommand cmd = new SqlCommand("select * from clients where name='" + lstNamesSearch.SelectedItem.Text + "'", conn);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read() == true)
        {
            txtFirstName.Text = dr["name"].ToString();
            txtAddress.Text = dr["address"].ToString();
            txtContactNumber.Text = dr["contact_details"].ToString();
            txtSpecialNotes.Text = dr["special_notes"].ToString();
            txtID.Text = dr["client_number"].ToString();
            loadassetmanagers();
            //getSumFromDB();
            // getIpsec();
            //MsgBox(" txtSumFromDB is:  " + txtSumFromDB.Text, Page, this);
            if (RadioButtonList1.SelectedItem.Text == "Client")
            {
                getIpsecClient();
            }
            else
            {
                getIpsecAssetManager2();
            }
                
            grdApps.Visible = true;
        }
    }

    protected void getSumFromDBClient()
    {
        conn.Close();
        conn.Open();
        //MsgBox(lstNamesSearch.Text, Page, this);
        SqlCommand cmd = new SqlCommand("select isnull(sum(Value),0.00) as PercentageLeft  from Ipsecs where client_id='" + txtID.Text + "' and active='1' and allocation_level='client'", conn);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read() == true)
        {
            txtSumFromDB.Text = dr["PercentageLeft"].ToString();
           
        }
    }

    protected void getSumFromDBAssetManagers()
    {
        conn.Close();
        conn.Open();
        //MsgBox(lstNamesSearch.Text, Page, this);
        SqlCommand cmd = new SqlCommand("select isnull(sum(Value),0.00) as PercentageLeft  from Ipsecs where client_id='" + txtID.Text + "' and active='1' and allocation_level='assetmanager'", conn);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read() == true)
        {
            txtSumFromDB.Text = dr["PercentageLeft"].ToString();

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       
    }
    public void getIpsec()

    {
        conn.Close();
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("DECLARE  @columns NVARCHAR(MAX) = '',@clientid NVARCHAR(MAX) = '"+txtID.Text+ "',   @sql     NVARCHAR(MAX) = '';select  @columns+= QUOTENAME(surname) + ',' from( SELECT * FROM (select id,surname from asset_managers where id in( select assetmanager_id from [client_assetmanager_relations] where client_id=@clientid ) union select client_number,name from Clients where client_number=@clientid)y left join (select client_id,Asset_manager_id,value from ipsecs where client_id=@clientid and active='1')   r on  y.surname=r.Asset_manager_id )d SET @columns = LEFT(@columns, LEN(@columns) - 1);SET @sql ='select * from (select IPS.Asset_class_id as AssetClass,IPS.value ,IPS.Asset_manager_id  from (select id, surname from asset_managers  where id in(select assetmanager_id from[client_assetmanager_relations] where client_id = '+@clientid+') union select client_number,name from Clients where client_number = '+@clientid+' )r JOIN Ipsecs IPS on IPS.Asset_manager_id = R.surname and IPS.client_id = '+@clientid+')s PIVOT(sum(value) FOR  asset_manager_id IN('+@columns+')) AS pivot_table; 'EXECUTE sp_executesql @sql;", conn);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.AddWithValue("@ID", txtID.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    grdApps.DataSource = ds;
                    grdApps.DataBind();
                }
            }
            else
            {
                grdApps.DataSource = null;
                grdApps.DataBind();
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }
    public void getIpsecClient()

    {
        conn.Close();
        try
        {
           
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            if (RadioButtonList1.SelectedItem.Text == "Client")
            {
                cmd = new SqlCommand("(select id,asset_class_id as [Asset Class Name] ,concat(value,' %') as [Value],Corridor as Range from Ipsecs where client_id='" + txtID.Text + "' and active='1' and allocation_level='client') union (select '0' + cast(id as varchar) as id, asset_name as [Asset Class Name], '0' as Value, '0' as Range from assets_class where asset_name not in (select asset_class_id from Ipsecs where client_id = '" + txtID.Text + "' and active = '1' and allocation_level = 'client')) union (select '0' as id, ' ' as [Asset Class Name], concat(sum(Value), '%') as TotalValue, ' ' as Range from Ipsecs where client_id = '" + txtID.Text + "' and active = '1' and allocation_level = 'client') order by id desc", conn);
            }
            //else if(RadioButtonList1.SelectedItem.Text== "Asset Manager")
            //{
            //    MsgBox(RadioButtonList1.SelectedItem.Text, this.Page, this);
            //    cmd = new SqlCommand("select  id,asset_class_id as [Asset Class Name] ,Value,Corridor as Range  from Ipsecs where client_id='" + txtID.Text + "' and  Asset_manager_id='" + rdAssetMangers.SelectedItem.Text + "' and active='1' order by id desc", conn);
            //}
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    grdClientIpsecs.DataSource = ds;


                    //grdClientIpsecs.Columns[4].FooterText = ds.Tables[1].Rows[0].ToString();


                    grdClientIpsecs.DataBind();
                }
            }
            else
            {
                grdClientIpsecs.DataSource = null;
                grdClientIpsecs.DataBind();
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }


    //protected void grdClientIpsecs_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
    //    {
    //        e.Row.Cells[0].Visible = false;//this is your templatefield column.
    //        e.Row.Cells[1].Visible = false;//this is your templatefield column.
    //    }
    //}

    //protected void grdClientIpsecs_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        GridView grid = (GridView)sender;

    //        if (e.Row.RowIndex == (grid.Rows.Count - 1))
    //        {
    //            //TableCell lastRow = e.Row.Cells[grid.Rows.Count - 1];
    //            //lastRow.FindControl("lnkDiscard").Visible = false;
    //            //lastRow.FindControl("lnkedit").Visible = false;
    //        }
    //    }
    //}

    //int totalvalue = 0;
    //protected void grdClientIpsecs_RowDataBound1(object sender, GridViewRowEventArgs e)
    //{
    //    //Check if the current row is datarow or not
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        //Add the value of column
    //        totalvalue += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Value"));
    //    }
    //    if (e.Row.RowType == DataControlRowType.Footer)
    //    {
    //        //Find the control label in footer 
    //        Label lblamount = (Label)e.Row.FindControl("lblTotalValue");
    //        //Assign the total value to footer label control
    //        lblamount.Text = "Total Value is : " + totalvalue.ToString();
    //    }
    //}

    //protected void grdClientIpsecs_OnRowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.EmptyDataRow)
    //    {
    //        string deb = e.Row.Cells[6].Text.ToString();
    //        string debTotal = " ";
    //         debTotal = debTotal + deb;
    //        e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
    //    } else if (e.Row.RowType == DataControlRowType.Footer)
    //    { e.Row.Cells[0].Text = "Totals"; 
    //            e.Row.Cells[0].ColumnSpan = 5;
    //            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
    //            e.Row.Cells[2].Visible = false;
    //        e.Row.Cells[3].Visible = false;
    //        e.Row.Cells[4].Visible = false;
    //        e.Row.Cells[5].Visible = false;

    //    }
    //}

    //protected void grdClientIpsecs_OnRowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    decimal GrandTotalSales = 0;
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {

    //        decimal tmptotalsales = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Value").ToString());
    //        GrandTotalSales += tmptotalsales;
    //    }
    //    if (e.Row.RowType == DataControlRowType.Footer)
    //    {
    //        e.Row.Cells[0].Font.Bold = true;
    //        e.Row.Cells[3].Font.Bold = true;
    //        e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
    //        e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
    //        e.Row.Cells[0].Text = "Total Sales";
    //        e.Row.Cells[3].Text = GrandTotalSales.ToString();
    //    }
    //}

     ​//int total = 0;
//protected void grdClientIpsecs_OnRowDataBound(object sender, GridViewRowEventArgs e)
//    {
// ​if (e.Row.RowType == DataControlRowType.DataRow)
//  {
//   ​total += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Value"));
//  }
// ​if (e.Row.RowType == DataControlRowType.Footer)
// {
//   ​Label lblAmount = (Label)e.Row.FindControl("amountLabe");
//   ​lblAmount.Text = total.ToString();
// ​}
//    }

    public void getIpsecAssetManager()

    {
        if (rdAssetMangers.SelectedItem.Text != "Select An Asset Manager") {
        conn.Close();
        try
        {

            conn.Open();
            SqlCommand cmd = new SqlCommand();
            if (RadioButtonList1.SelectedItem.Text == "Asset Manager")
            {
                cmd = new SqlCommand("select cast(id as varchar) as id,asset_class_id as [Asset Class Name] ,concat(Value,' %') as [Value],Corridor as Range from Ipsecs where client_id='" + txtID.Text + "' and  Asset_manager_id='" + rdAssetMangers.SelectedItem.Text + "' and active='1' union (select cast(id as varchar) as id, asset_name as [Asset Class Name], '0 %' as Value, '0' as Range from assets_class where asset_name not in (select asset_class_id from Ipsecs where client_id = '" + txtID.Text + "' and active = '1' and allocation_level = 'assetmanager')) union (select ' ' as id, ' ' as [Asset Class Name], concat(sum(Value), '%') as TotalValue, ' ' as Range from Ipsecs where client_id = '" + txtID.Text + "' and active = '1' and allocation_level = 'assetmanager') order by id desc", conn);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    grdClientIpsecs.DataSource = ds;
                    grdClientIpsecs.DataBind();
                }
            }
            else
            {
                grdClientIpsecs.DataSource = null;
                grdClientIpsecs.DataBind();
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
        }
        else
        {
            MsgBox("Please Select Asset Manager", this.Page, this);
        }
    }

    public void getIpsecAssetManager2()

    {
           conn.Close();
            try
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand();
                if (RadioButtonList1.SelectedItem.Text == "Asset Manager")
                {
                    cmd = new SqlCommand("select cast(id as varchar) as id,asset_class_id as [Asset Class Name] ,concat(Value,' %') as [Value],Corridor as Range from Ipsecs where client_id='" + txtID.Text + "' and  Asset_manager_id='" + rdAssetMangers.SelectedItem.Text + "' and active='1' union (select cast(id as varchar) as id, asset_name as [Asset Class Name], '0 %' as Value, '0' as Range from assets_class where asset_name not in (select asset_class_id from Ipsecs where client_id = '" + txtID.Text + "' and active = '1' and allocation_level = 'assetmanager')) union (select ' ' as id, ' ' as [Asset Class Name], concat(sum(Value), '%') as TotalValue, ' ' as Range from Ipsecs where client_id = '" + txtID.Text + "' and active = '1' and allocation_level = 'assetmanager') order by id desc", conn);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet("ds");
                da.Fill(ds);

                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        grdClientIpsecs.DataSource = ds;
                        grdClientIpsecs.DataBind();
                    }
                }
                else
                {
                    grdClientIpsecs.DataSource = null;
                    grdClientIpsecs.DataBind();
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MsgBox(ex.Message, this.Page, this);
            }
        
        
    }

    protected void rdAssetClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void rdAssetMangers_SelectedIndexChanged(object sender, EventArgs e)
    {
        getIpsecAssetManager();

        // MsgBox("hie",Page,this);
        //assetmanagerslabel.Text = "Client IPsec for AssetManager" + ' ' + rdAssetMangers.SelectedItem.Text;
        //getIpsec();
    }
    public void linkDiscard(object sender, System.EventArgs e)
    {
        string idd = ((LinkButton)sender).CommandArgument;
        SqlCommand cmd = new SqlCommand("update  Ipsecs set active='0' where Id='" + idd + "' ", conn);
        if ((conn.State == ConnectionState.Open))
            conn.Close();
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
        MsgBox("Delete Successful", this.Page, this);
        //getIpsec();
        getIpsecClient();
    }
    public void lnkedit(object sender, System.EventArgs e)
    {
        string idd = ((LinkButton)sender).CommandArgument;
        fetcheditdata(idd);

        //Label10.Visible = false;
        //cmbAsetClass.Visible = false;

        //Label113.Visible = true;
        //txtAssetclass4Edit.Visible = true;

        //updatedata(idd);

    }
    public void links(object sender, System.EventArgs e)
    {
        string idd = ((LinkButton)sender).CommandArgument;
        //MsgBox("hallo", this.Page, this);
        fectchclientipsecdata(idd);
        //updatedata(idd);

    }

   
    public void fectchclientipsecdata(String id)
    {
        conn.Close();
        conn.Open();
        string Query = "SELECT * FROM Ipsecs WHERE id='" + id.ToString() + "' ";
        SqlCommand cmd = new SqlCommand(Query, conn);
        SqlDataReader dr = cmd.ExecuteReader();

        if (dr.Read() == true)
        {
            cmbAsetClass.Text = dr["Asset_class_id"].ToString();
            
            //txtAssetclass4Edit.Text = dr["Asset_class_id"].ToString();

            //Label10.Visible = false;
            //cmbAsetClass.Visible = false;

            //Label113.Visible = true;
            //txtAssetclass4Edit.Visible = true;

            txtClientIpsecUpdateID.Text = dr["id"].ToString();
            txtAllocation.Text = dr["value"].ToString();
            txtAllocationOld.Text = dr["value"].ToString();
            //txtCorridor.Text = dr["Corridor"].ToString();

            string[] wholestring = dr["Corridor"].ToString().Split('-');
            txtCorridorTop.Text = wholestring[0];
            txtCorridorBottom.Text = wholestring[1];

            //MsgBox(txtCorridorTop.Text, Page, this);
            //MsgBox(wholestring[1], Page, this);
            Button3.Visible = false;


            Button5.Visible = true;
        }

    }
    public void fetcheditdata( String id)
    {

        conn.Close();
        conn.Open();
        string Query = "SELECT * FROM Ipsecs WHERE id='" + id.ToString() + "' ";
        SqlCommand cmd = new SqlCommand(Query, conn);
        SqlDataReader dr = cmd.ExecuteReader();

        if (dr.Read() == true)
        {
           
            rdAssetMangers.DataValueField = dr["Asset_manager_id"].ToString();
            cmbAsetClass.Text = dr["[Asset_class_id"].ToString();
            txtClientIpsecUpdateID.Text= dr["[id"].ToString();
            txtAllocation.Text = dr["value"].ToString();
            //txtCorridor.Text = dr["Corridor"].ToString();

            string []  wholestring = dr["Corridor"].ToString().Split('-') ;
            txtCorridorTop.Text = wholestring[0];
            txtCorridorBottom.Text = wholestring[1];

            //Button2.Visible = true;
            //Button1.Visible = false;
        }

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
       
    }
    public Boolean updatedata(String id,String value)
    {
        bool added = false;
        conn.Close();
        conn.Open();
        SqlCommand cmd = new SqlCommand("update  Ipsecs set value='" + value + "',Corridor='" + txtCorridorTop.Text + "-" + txtCorridorBottom.Text + "' where Id='" + id + "' ", conn);
        if ((conn.State == ConnectionState.Open))
            conn.Close();
        conn.Open();
        
       int a= cmd.ExecuteNonQuery();
        conn.Close();
        if (a == 1)
        {
            added = true;
        }
        
        
        return added;
    }

    protected void Button3_Click1(object sender, EventArgs e)
    {
        decimal sumFrmDB;
        decimal userPercentage;
        decimal percentageleft;
        decimal percentagerequired;
        //decimal userPercentageOld;
        if (cmbAsetClass.SelectedItem.Text == "Select An Asset Class")
        {
            MsgBox("Select An Asset Class", Page, this);
            return;
        }
        if (RadioButtonList1.SelectedItem.Value == "Client") {
            if (checkIpsecClient(txtID.Text, cmbAsetClass.SelectedItem.Text))
            {
                MsgBox("Asset Class Ipsec Already  Added", Page, this);
            }
            else
            {
                ///// F.Chatz
                conn.Close();
                try
                {
                    conn.Open();
                        //conn.Open();
                        SqlCommand cmd = new SqlCommand("select isnull(sum(Value),0.00) as SumClassPercentage  from Ipsecs where client_id='" + txtID.Text + "' and active='1' and allocation_level='client'", conn);
                        SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read() == true)
                    {

                        sumFrmDB = Convert.ToDecimal(dr["SumClassPercentage"]);
                    }
                    else
                    {
                        sumFrmDB = 0;
                    }

                    //sumFrmDB = Convert.ToDecimal(txtSumFromDB.Text);
                    userPercentage = Convert.ToDecimal(txtAllocation.Text);
                    //userPercentageOld = Convert.ToDecimal(txtAllocationOld.Text);
                    percentageleft = 100 - (sumFrmDB + userPercentage); // - userPercentageOld);
                    //percentageleft = 100 - (sumFrmDB + userPercentage);
                    percentagerequired = 100 - sumFrmDB;

                if (percentageleft >= 0)
                    {
                        InsertIpsecClient(txtID.Text, txtAllocation.Text, cmbAsetClass.SelectedItem.Text);
                        getIpsecClient();
                        clearfields();
                    }

                    else 
                    {
                        MsgBox("Total Asset Class Percentage Exceeded. Please enter a percentage equal or lower than "+ percentagerequired + " %", this.Page, this);
                                
                    }

                }

                catch (Exception ex)
                {
                    conn.Close();
                    MsgBox("Client Exception: " + ex.Message, this.Page, this);
                }

            }


        }
        else
        {
            if (checkIpsec(txtID.Text, cmbAsetClass.SelectedItem.Text, rdAssetMangers.SelectedItem.Text))
            {
                MsgBox("Asset Class Ipsec Already  Added", Page, this);
            }
            else
            {
                ///// F.Chatz2
                conn.Close();
                //try
                //{
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select isnull(sum(Value),0.00) as SumClassPercentage  from Ipsecs where client_id='" + txtID.Text + "' and active='1' and allocation_level='assetmanager'", conn);
                    SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read() == true)
                {

                    sumFrmDB = Convert.ToDecimal(dr["SumClassPercentage"]);
                } 
                else
                {
                    sumFrmDB = 0;
                }

                    //sumFrmDB = Convert.ToDecimal(dr["SumClassPercentage"]);
                    userPercentage = Convert.ToDecimal(txtAllocation.Text);
                    //MsgBox("txtAllocationOld.Text +>" + txtAllocationOld.Text, this.Page, this);
                    //userPercentageOld = Convert.ToDecimal(txtAllocationOld.Text);
                    percentageleft = 100 - (sumFrmDB + userPercentage); // - userPercentageOld);
                    //percentageleft = 100 - (sumFrmDB + userPercentage);
                    percentagerequired = 100 - sumFrmDB ;

                    if (percentageleft >= 0)
                    {
                        InsertIpsec(txtID.Text, cmbAsetClass.SelectedItem.Text, rdAssetMangers.SelectedItem.Text, txtAllocation.Text);
                        getIpsecAssetManager();
                        clearfields();

                    }
                    else
                    {
                        MsgBox("Total Asset Class Percentage Exceeded. Please enter a percentage equal or lower than " + percentagerequired + " %", this.Page, this);
                    }
                    dr.Close();
                //}

                //catch (Exception ex)
                //{
                //    conn.Close();
                //    MsgBox("Asset Manager Exception: " + ex.Message, this.Page, this);
                //}


            }
        }
    }

    //puplic string newText = " ";
    //public void txtAllocation_TextChanged(object sender, EventArgs e)
    //{
    //    string newText = txtAllocation.Text;
    //    //Compare OldText and newText here
    //}
    protected void Button5_Click(object sender, EventArgs e)
    {
        decimal sumFrmDB=0;
        decimal userPercentage;
        decimal userPercentageOld;
        decimal percentageleft;
        decimal percentagerequired;

        ///// F.Chatz3
        conn.Close();
        try
        {
            conn.Open();
            if (RadioButtonList1.SelectedItem.Value == "Client")
            {
                SqlCommand cmd = new SqlCommand("select isnull(sum(Value),0.00) as SumClassPercentage  from Ipsecs where client_id='" + txtID.Text + "' and active='1' and allocation_level='client'", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read() == true)
                {

                    sumFrmDB = Convert.ToDecimal(dr["SumClassPercentage"]);
                }
                else
                {
                    sumFrmDB = 0;
                }
                dr.Close();
            } else 
            {
                SqlCommand cmd = new SqlCommand("select isnull(sum(Value),0.00) as SumClassPercentage  from Ipsecs where client_id='" + txtID.Text + "' and active='1' and allocation_level='assetmanager'", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read() == true)
                {

                    sumFrmDB = Convert.ToDecimal(dr["SumClassPercentage"]);
                }
                else
                {
                    sumFrmDB = 0;
                }
                dr.Close();
            }

            //sumFrmDB = Convert.ToDecimal(dr["SumClassPercentage"]);
            userPercentage = Convert.ToDecimal(txtAllocation.Text);
            userPercentageOld = Convert.ToDecimal(txtAllocationOld.Text);
            percentageleft = 100 - (sumFrmDB + userPercentage - userPercentageOld);
            percentagerequired = 100 - sumFrmDB;

            if (percentageleft >= 0)
            {
                if (updatedata(txtClientIpsecUpdateID.Text, txtAllocation.Text))
                {

                    //getIpsec();
                    if (RadioButtonList1.SelectedItem.Value == "Client")
                    {
                        getIpsecClient();
                    }
                    else
                    {
                        getIpsecAssetManager();
                    }

                    //MsgBox("sumFrmDB " + sumFrmDB, Page, this);
                    //MsgBox("userPercentage " + userPercentage, Page, this);
                    //MsgBox("userPercentageOld " + userPercentageOld, Page, this);
                    //MsgBox("Left " + (100 - (sumFrmDB + userPercentage - userPercentageOld)), Page, this);
                    MsgBox("Asset Update Successfully Captured. " + "You have " + percentageleft + " % left", Page, this);

                    clearfields();
                    Button3.Visible = true;
                    Button5.Visible = false;

                }

            }
            else
            {
                MsgBox("Total Asset Class Percentage Exceeded. Please enter a percentage equal or lower than " + percentagerequired + " %", this.Page, this);
            }
            
        }

        catch (Exception ex)
        {
            conn.Close();
            MsgBox("Update Exception: " + ex.Message, this.Page, this);
        }

        //----------------


    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
       // MsgBox("tapinda", this.Page, this);
        if(RadioButtonList1.SelectedItem.Value== "AssetManager")
        {
            Label7.Visible = true;
            rdAssetMangers.Visible = true;
            //getIpsecClient();
            loadassetmanagers();
            grdClientIpsecs.DataSource = null;
            grdClientIpsecs.DataBind();
            loadusertypes();
        }
        else
        {
            Label7.Visible = false;
            rdAssetMangers.Visible = false;
            getIpsecClient();
            loadusertypes();
        }
    }
    public void clearfields()
    {
        
        txtCorridorTop.Text = "";
        txtCorridorBottom.Text = "";
        txtFirstName.Text = "";
        txtAllocation.Text = "";
        txtAllocationOld.Text = "";
        cmbAsetClass.SelectedItem.Text = "Select An Asset Class";

    }
}