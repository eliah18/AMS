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
            loaddata();
            //loadlinkedassetclass();
            //loadusertypes();
            //loadcounter();
            // GetListDataa();

            try
            {
                if (Session["user"].ToString() == "")
                {

                    // MsgBox("hala", Page, this);
                    // GetListData();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/logins.aspx");
            }

            lbUsername.Text = "Logged in as" + " " + " " + (string)Session["username"] + "" + "" + (string)Session["role"];
            //loadYear();
            loadSubAccounts();

            txtID.Text = Request.QueryString["clientid"];

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
        String clientid = Request.QueryString["clientid"];
        String op = Request.QueryString["op"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"].Trim();
        String url = null;

        url = "ReportTypeView.aspx" + "?" + "clientid=" + clientid + "&year=" + year + "&quarter=" + quarter;
        Response.Redirect(url);
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
    
    


    protected void Button6_Click(object sender, EventArgs e)
    {


    


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
            String clientid = Request.QueryString["clientid"];
           
            String quarter = Request.QueryString["quarter"];
            String year = Request.QueryString["year"];


            SqlCommand cmd = new SqlCommand();
            conn.Open();
            if (cmbSubAccount.SelectedItem.Text == "View Consolidated")
            {

                 cmd = new SqlCommand("fetchAssetClassHoldingbackup ", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@clientid", clientid);
                cmd.Parameters.AddWithValue("@quarter", quarter);
                cmd.Parameters.AddWithValue("@year", year);
            }
            else
            {
                 cmd = new SqlCommand("fetchAssetClassHoldingbackuplatest ", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@clientid", clientid);
                cmd.Parameters.AddWithValue("@quarter", quarter);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@subaccount", cmbSubAccount.SelectedItem.Value.ToString());
            }
           
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
        //GetListDataa();
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
        
        GetListDataa();
        Panel4.Visible = true;
        Panel2.Visible = true;

        //Response.Redirect("AssetAllocation.aspx?clientid=" + HttpUtility.UrlEncode(txtID.Text.ToString())  + "&year=" + HttpUtility.UrlEncode(cmbYear.SelectedItem.Value) + "&quarter=" + HttpUtility.UrlEncode(rdQuarter.SelectedItem.Value));
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
    private void loadSubAccounts()
    {


        conn.Close();
        conn.Open();
        string com = "select *  from  subaccounts";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        cmbSubAccount.DataSource = dt;
        cmbSubAccount.DataBind();
        cmbSubAccount.DataTextField = "subaccountname";
        cmbSubAccount.DataValueField = "id";
        cmbSubAccount.DataBind();
        cmbSubAccount.Items.Insert(0, new ListItem("View Consolidated", "0"));




    }



    protected void grdApps_DataBound(object sender, EventArgs e)
    {
        int a = grdApps.Rows.Count - 1;

        // grdApps.Rows[a].Font.Bold = true;
        
    }
    public void loaddata()
    {
        txtQuarter.Text= Request.QueryString["quarter"];
        txtYear.Text = Request.QueryString["year"];
        conn.Close();
        conn.Open();

        string Query = " select * from Clients where client_number='"+ Request.QueryString["clientid"] + "'";
        SqlCommand cmd = new SqlCommand(Query, conn);
        SqlDataReader dr = cmd.ExecuteReader();

        if (dr.Read() == true)


        {
            //String taka = grdApps.SelectedRow.Cells[1].Text;
            //MsgBox(taka, this.Page, this);

            //txtFirstName.Text = dr["name"].ToString();
            //txtSurname.Text = dr["surname"].ToString();
            //txtBenchmark.Text = dr["benchmark"].ToString();
            //txtID.Text = dr["id"].ToString();
            //txtStrategy.Text = dr["strategy"].ToString();
            //txtPhilosophy.Text = dr["philosophy"].ToString();
            //txtContactDetails.Text = dr["contact_details"].ToString();
            //txtAddress.Text = dr["address"].ToString();
            //txtPrice.Text = "";
            txtFirstName.Text = dr["name"].ToString();
            txtSpecialNotes.Text = dr["special_notes"].ToString();
            //txtunli= dr["price"].ToString();
            //Button1.Visible = false;
            //Button2.Visible = true;

        }
    }

    protected void Button1_Click22(object sender, EventArgs e)

    {
        string strscript;
        strscript = "<script langauage=JavaScript>";
        strscript += "window.open('assetexposure.aspx?client_id=" + txtID.Text + "&Quarter=" + txtQuarter.Text + "&Subaccount=" + cmbSubAccount.SelectedItem.Value + "&Year=" + txtYear.Text + "');";
        strscript += "</script>";
        ClientScript.RegisterStartupScript(this.GetType(), "newwin", strscript);
    }

}

   
        
        
                

    

