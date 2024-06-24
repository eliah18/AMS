﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration.Provider;
using System.Configuration;
using System.IO;

public partial class admin_parameters_User : System.Web.UI.Page
{


    public string nexturl;
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

            //loadlinkedassetclass();
            //loadusertypes();
            //loadcounter();
            // GetListDataa();
            try {
                if (Session["user"].ToString() == "")
                {

                    // MsgBox("hala", Page, this);
                    // GetListData();
                }
            }
            catch(Exception ex)
            {
                Response.Redirect("~/logins.aspx");
            }
            

            lbUsername.Text = "Logged in as" + " " + " " + (string)Session["username"] + "" + "" + (string)Session["role"];
            loadYear();
            //Boolean user=  checkuser(txtFirstName.Text, txtSurname.Text, txtUsername.Text);
            //  if(user)
            //  {
            //      MsgBox("User Already Exists", this.Page, this);

            //  }
        }

    }
   
   
    public Boolean InsertInvestment(String clientid, String assetclassid, String assetmanagerid, String value , String year, String quarter)
    {
        Boolean added = false;
       // try
        {
            conn.Close();
            conn.Open();
			value=value.Replace(",","");
            String query = "INSERT INTO Investments(client_id ,Asset_class_id, Asset_manager_id ,value ,year,quarter ) values('" + clientid + "','" + assetclassid + "','" + assetmanagerid + "','" + value + "','" + year + "','" + quarter + "')";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteReader(CommandBehavior.CloseConnection);
            added = true;
        }
       // catch (Exception ex)
       // {
          //  MsgBox("Error: " + ex.Message, this.Page, this);
          //  throw;
        //}
        return added;


    }
    public Boolean UpdateInvestment(String id,String value)
    {
        Boolean added = false;
        try
        {
            conn.Close();
            conn.Open();
            String query = "update Investments set value='"+ value+"' where id='"+ id +"'";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteReader(CommandBehavior.CloseConnection);
            added = true;
        }
        catch (Exception ex)
        {
            MsgBox("Error: " + ex.Message, this.Page, this);
            throw;
        }
        return added;


    }











    //public void loadusertypes()
    //{

    //    string com = "Select * from assets_class where active ='1' and linkstatus='0'";
    //    SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
    //    DataTable dt = new DataTable();
    //    adpt.Fill(dt);
    //    rdAssetClass.DataSource = dt;
    //    rdAssetClass.DataBind();
    //    rdAssetClass.DataTextField = "asset_name";
    //    rdAssetClass.DataValueField = "ID";
    //    rdAssetClass.DataBind();
    //    rdAssetClass.Items.Insert(0,new ListItem("Select an asset", "0"));


    //}
    //public void loadcounter()
    //{

    //    string com = "Select * from para_company ";
    //    SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
    //    DataTable dt = new DataTable();
    //    adpt.Fill(dt);
    //    cmbCounter.DataSource = dt;
    //    cmbCounter.DataBind();
    //    cmbCounter.DataTextField = "fnam";
    //    cmbCounter.DataValueField = "company";
    //    cmbCounter.DataBind();
    //    cmbCounter .Items.Insert(0, new ListItem("Select Counter", "0"));


    //}
    //public void loadlinkedassetclass()
    //{

    //    string com = "Select * from assets_class where active ='1' and linkstatus='1'";
    //    SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
    //    DataTable dt = new DataTable();
    //    adpt.Fill(dt);
    //    cmbAssetClass.DataSource = dt;
    //    cmbAssetClass.DataBind();
    //    cmbAssetClass.DataTextField = "asset_name";
    //    cmbAssetClass.DataValueField = "id";
    //    cmbAssetClass.DataBind();
    //    cmbAssetClass.Items.Insert(0, new ListItem("Select Asset Class", "0"));


    //}

   
    public void loadYear()
    {
        conn.Close();
        conn.Open();
        //string com = " select year from years";
        string com = "select year from years where year<=(YEAR(getdate())) order by year desc";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        cmbYear.DataSource = dt;
        cmbYear.DataBind();
        cmbYear.DataTextField = "year";
        cmbYear.DataValueField = "year";
        cmbYear.DataBind();
        cmbYear.Items.Insert(0, new ListItem("Select Year", "0"));



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
    





    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {
            conn.Close();
            conn.Open();

            SqlCommand cmd = new SqlCommand("select name from clients where name like '%" + txtsearch.Text + "%'  ", conn);
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

            txtSpecialNotes.Text = dr["special_notes"].ToString();
            txtID.Text = dr["client_number"].ToString();
            // GetListData();
            
                //loadassetmanagers();
            




            // GetListData(txtID.Text);
            //Panel2.Visible = true;

            //loadassetmanagers();

        }
    }

    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    if(rdAssetMangers.SelectedItem.Text== "Select An Asset Manager")
    //    {
    //        MsgBox("select An Asset Manger" ,Page,this);
    //        return;
    //    }
    //    if(rdAssetClass.SelectedItem.Text == "Select an asset")
    //    {
    //        MsgBox("select An Asset Class", Page, this);
    //        return;
    //    }
    //    if (checkInvestment(txtID.Text, rdAssetClass.SelectedItem.Text, rdAssetMangers.SelectedItem.Text))
    //    {
    //        MsgBox("Investment Already  Added", Page, this);
    //    }
    //    else{
    //        if (InsertInvestment(txtID.Text, rdAssetClass.SelectedItem.Text, rdAssetMangers.SelectedItem.Text,txtValue.Text))
    //        {
    //            MsgBox("Investment Inserted Successfully", Page, this);
    //        }
    //        getIpsec();

    //    }
    //}





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


    }



    public Boolean updatedata(String id, String value)
    {
        bool added = false;
        conn.Close();
        conn.Open();
        SqlCommand cmd = new SqlCommand("update  Ipsecs set value='" + value + "' where Id='" + id + "' ", conn);
        if ((conn.State == ConnectionState.Open))
            conn.Close();
        conn.Open();

        int a = cmd.ExecuteNonQuery();
        conn.Close();
        if (a == 1)
        {
            added = true;
        }


        return added;
    }

    private void GetListData()
    {
        conn.Close();
        try
        {
            String date = cmbYear.Text.Trim();
            conn.Open();
            String query = "SELECT  a.id,a.id as investmentid,a. asset_name,'0' AS [Amount],case when a.linkstatus='1' then 'needButton' else 'NeedTextbox' end as ControlType FROM assets_class a where a.active=1 ";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {






                    repBusAssetsIB.DataSource = ds;
                    repBusAssetsIB.DataBind();


                    for (a = 0; a <= dt.Rows.Count; a++)
                    {
                        if (dt.Rows[a]["ControlType"].ToString() == "needButton")
                        {
                            repBusAssetsIB.Items[a].FindControl("res").Visible = false;
                            repBusAssetsIB.Items[a].FindControl("linked").Visible = true;
                        }
                        else
                        {
                            repBusAssetsIB.Items[a].FindControl("res").Visible = true;
                            repBusAssetsIB.Items[a].FindControl("linked").Visible = false;
                        }
                    }


                }
            }
            else
            {
                repBusAssetsIB.DataSource = null;
                repBusAssetsIB.DataBind();
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            //MsgBox(ex.Message, this.Page, this);
        }
    }
    private void GetFilteredData()
    {
        //conn.Close();
        //try
        //{
            
        //    String date = cmbYear.Text.Trim();
        //    conn.Open();
        //    String query = "SELECT  a.id,z.id as [investmentid],a. asset_name,z.value AS [Amount],case when linkstatus='1' then 'needButton' else 'NeedTextbox' end as ControlType FROM assets_class a left outer join Investments z on a.asset_name=z.Asset_class_id   where z.client_id='" + txtID.Text + "' and z.quarter='" + rdQuarter.SelectedItem.Value + "'   and z.year='" + date + "'  and REPLACE(LTRIM(RTRIM( z.Asset_manager_id) ), ' ', '')='" + rdAssetMangers.SelectedItem.Value + "' ";
        //    SqlCommand cmd = new SqlCommand(query, conn);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet("ds");
        //    da.Fill(ds);

        //    DataTable dt = ds.Tables[0];
        //    if (dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dt.Rows)
        //        {






        //            repBusAssetsIB.DataSource = ds;
        //            repBusAssetsIB.DataBind();

        //            repBusAssetsIB.Items[0].FindControl("res").Visible = false;
        //            repBusAssetsIB.Items[1].FindControl("res").Visible = false;
        //            repBusAssetsIB.Items[2].FindControl("res").Visible = false;
        //            repBusAssetsIB.Items[3].FindControl("res").Visible = false;
        //            repBusAssetsIB.Items[4].FindControl("res").Visible = false;
        //            repBusAssetsIB.Items[5].FindControl("res").Visible = false;
        //            repBusAssetsIB.Items[6].FindControl("res").Visible = false;
        //            repBusAssetsIB.Items[7].FindControl("res").Visible = false;
        //            repBusAssetsIB.Items[8].FindControl("linked").Visible = false;
        //            repBusAssetsIB.Items[9].FindControl("linked").Visible = false;
        //            repBusAssetsIB.Items[10].FindControl("linked").Visible = false;


        //        }
        //    }
        //    else
        //    {
        //        repBusAssetsIB.DataSource = null;
        //        repBusAssetsIB.DataBind();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    conn.Close();
        //    //MsgBox(ex.Message, this.Page, this);
        //}
    }


    protected void Button3_Click1(object sender, EventArgs e)
    {
        //if(insertlinked(txtID.Text, cmbCounter.SelectedItem.Value, txtPrice.Text, txtQuantity.Text, cmbAssetManagers.SelectedItem.Text, cmbAssetClass.SelectedItem.Text))
        //{


        //    Double totalamount = Convert.ToDouble(txtPrice.Text) * Convert.ToDouble(txtQuantity.Text);
        //    if(InsertInvestment(txtID.Text, cmbAssetClass.SelectedItem.Text, cmbAssetManagers.SelectedItem.Text, totalamount.ToString()))
        //    {
        //        MsgBox("investment saved", Page, this);

        //    }

        //}

    }
    public Boolean insertlinked(String clientid, String counter, String price, String quantity, String assetmanager, String Assetclass)
    {
        Boolean added = false;
        try
        {
            conn.Close();
            conn.Open();
            String query = "INSERT INTO linked_assetclasses( counter,assetclass,price, quantity ,client_id,assetmanager) values('" + counter + "','" + Assetclass + "','" + price + "','" + quantity + "' , '" + clientid + "','" + assetmanager + "')";
            SqlCommand cmd = new SqlCommand(query, conn);
            int a = cmd.ExecuteNonQuery();
            if (a == 1)
            {

                added = true;
            }
        }
        catch (Exception ex)
        {
            MsgBox("Error: " + ex.Message, this.Page, this);
            throw;
        }
        return added;

    }

    public DataTable getDetails()
    {
        String date = cmbYear.Text.Trim();
        conn.Close();
        conn.Open();
        String query = "SELECT  a.id,a.id as investmentid,a.asset_name,'0' AS[Amount],case when a.linkstatus = '1' then 'needButton' else 'NeedTextbox' end as ControlType FROM assets_class a where a.active = 1 ";
        SqlCommand cmd = new SqlCommand(query, conn);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet("ds");
        da.Fill(ds);

        DataTable dt = ds.Tables[0];
        return dt;
    }


    protected void Button6_Click(object sender, EventArgs e)
    {


     try{

            foreach (DataRow dr in getDetails().Rows)
            {









                for (a = 0; a <= getDetails().Rows.Count; a++)
                {
                    if (getDetails().Rows[a]["ControlType"].ToString() == "NeedTextbox")
                    {
                        TextBox txt = repBusAssetsIB.Items[a].FindControl("res") as TextBox;
                        Label lb = repBusAssetsIB.Items[a].FindControl("Label4") as Label;
                        Label updateid = repBusAssetsIB.Items[a].FindControl("Label7") as Label;
                        //InsertInvestment(txtID.Text, lb.Text, rdAssetMangers.SelectedItem.Value, txt.Text, cmbYear.Text, rdQuarter.SelectedItem.Value);
                       // UpdateInvestment(updateid.Text, txt.Text);
                    }

                }


                //for (int i = 0; i <= 10; i++)
                //{
                //    if (i == 0 || i == 1 || i == 2 || i == 3 || i == 4 || i == 5 || i == 6 || i == 7)
                //    {

                //    }
                //    else
                //    {
                //        TextBox txt = repBusAssetsIB.Items[i].FindControl("res") as TextBox;
                //        Label lb = repBusAssetsIB.Items[i].FindControl("Label4") as Label;
                //        Label updateid = repBusAssetsIB.Items[i].FindControl("Label7") as Label;
                //        //InsertInvestment(txtID.Text, lb.Text, rdAssetMangers.SelectedItem.Value, txt.Text, cmbYear.Text, rdQuarter.SelectedItem.Value);
                //        UpdateInvestment(updateid.Text, txt.Text);
                //    }

            }

        }
	 catch (Exception ex)
        {
           conn.Close();
           // MsgBox(ex.Message, this.Page, this);
        }
        MsgBox("Allocation Saved", Page, this);
        GetListData();
        rdQuarter.Enabled = true;
        cmbYear.Enabled = true;


    }

    protected void repBusAssetsIB_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        //TextBox txt = repBusAssetsIB.Items[0].FindControl("res") as TextBox;
        //TextBox moneymarket = repBusAssetsIB.Items[1].FindControl("res") as TextBox;
        //TextBox bonds = repBusAssetsIB.Items[2].FindControl("res") as TextBox;
        
        

    }

    public void lnkedit(object sender, System.EventArgs e)
    {
       
        
    }
  


    protected void rdAssetMangers_SelectedIndexChanged(object sender, EventArgs e)
    {
       

       
       
    }



    

    protected void Button5_Click(object sender, EventArgs e)
    {
        

    }
    private void GetListDataa()
    {
        conn.Close();
      //  try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("fetchAssetClassHoldingbackup ", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@clientid", txtID.Text);
            cmd.Parameters.AddWithValue("@quarter", rdQuarter.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@year", cmbYear.SelectedItem.Text);
           
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
                //getsum();
            }

            else
            {
                grdApps.DataSource = null;
                grdApps.DataBind();
            }
           
        }
        getTotals();
        //catch (Exception ex)
        //{
        //    conn.Close();
        //    MsgBox(ex.Message, this.Page, this);
        //}
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        GetListDataa();
    }
   

     double totalvalue = 0;
    int a = 1;
    protected void grdApps_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        
       
        //if (e.Row.RowType == DataControlRowType.DataRow)



        //{
        //    // MsgBox("hie", this.Page, this);
        //    //Add the value of column
           

        //    totalvalue += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Value".ToString()))/index;
           
        //    // MsgBox(totalvalue.ToString(), this.Page, this);
        //}
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{

        //    //MsgBox(totalvalue.ToString(), this.Page, this);
        //    //Find the control label in footer
        //   // Label lblamount = (Label)e.Row.FindControl("lblTotalValue");
        //    //Assign the total value to footer label control
        //    //lblamount.Text = "Total Value is : " + totalvalue;
        //    // grdApps.FooterRow.Cells[0].Text = "Total";
        //    //grdApps.FooterRow.Cells[2].Text = lblamount.Text;
        //    e.Row.Cells[0].Text = "Total Asset Class Holding";
        //    e.Row.Cells[1].Text = String.Format("{0:0}", "<b>" + totalvalue.ToString() + "</b>");
        //}
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        if (txtID.Text == "")
        {
            MsgBox("Please Select Client", this.Page, this);
            return;
        }
        if (cmbYear.SelectedItem.Text == "Select Year")
        {
            MsgBox("Please Select Year First", this.Page, this);
            return;
        }
        if (rdQuarter.SelectedValue == "SQ")
        {
            MsgBox("Please Select Quarter", this.Page, this);
            return;
        }
        
        Response.Redirect("ReportingDashboard.aspx?clientid=" + HttpUtility.UrlEncode(txtID.Text.ToString())  + "&year=" + HttpUtility.UrlEncode(cmbYear.SelectedItem.Value) + "&quarter=" + HttpUtility.UrlEncode(rdQuarter.SelectedItem.Value));
    //    Panel2.Visible = true;
    //    String date = cmbYear.Text.Trim();

       
    //    {
    //        Button2.Visible = true;
    //        operationid.Text = "insert";
    //        GetListData();
			 //Panel4.Visible = true;
    //         GetListDataa();
    //        Label16.Text = rdAssetMangers.Text;
    //        Label16.ForeColor = System.Drawing.Color.ForestGreen;
    //        Label16.Visible = true;
    //        rdQuarter.Enabled = false;
          
    //        cmbYear.Enabled = false;
    //    }

    }

    public void getTotals()
    {
        double sum = 0;
        foreach (GridViewRow row in grdApps.Rows)
        {


            sum = 0;
                for (int b=2;b < row.Cells.Count; b++)
                {
                if (row.Cells[b].Text=="" || row.Cells[b].Text == null || row.Cells[b].Text == "&nbsp;")
                {
                    row.Cells[b].Text = "0";
                }
                    String cellText = row.Cells[b].Text;

                
               
                   double figure = double.Parse(cellText);

                sum = sum + figure;

                //MsgBox(sum.ToString(), this.Page, this);
              
                row.Cells[1].Text = sum.ToString();
                row.Cells[1].Font.Bold = true;


                //String cellText = row.Cells[i].Text;
                //double figure = double.Parse(cellText);
                //double sum = 0;
                //sum = sum + figure;





            }
        }
       
    }
   

    protected void Button1_Click1(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
    private void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "AsseClassHolding" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        grdApps.GridLines = GridLines.Both;
        grdApps.HeaderStyle.Font.Bold = true;
        grdApps.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();

    }



    protected void grdApps_DataBound(object sender, EventArgs e)
    {
        int a = grdApps.Rows.Count - 1;

        // grdApps.Rows[a].Font.Bold = true;
        
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/parameters/maindashboard.aspx");
    }
}

   
        
        
                

    

