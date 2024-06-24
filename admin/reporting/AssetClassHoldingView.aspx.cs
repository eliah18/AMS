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

                //cmd = new SqlCommand("fetchAssetClassHoldingbackup ", conn);
                cmd = new SqlCommand("fetchAssetClassHoldingbackupFChatz ", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@clientid", clientid);
                cmd.Parameters.AddWithValue("@quarter", quarter);
                cmd.Parameters.AddWithValue("@year", year);
            }
            else
            {
                //cmd = new SqlCommand("fetchAssetClassHoldingbackuplatest ", conn);
                cmd = new SqlCommand("fetchAssetClassHoldingbackupSubFChatz ", conn);
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
                int sum = 0;

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
        getTotals();
        //catch (Exception ex)
        //{
        //    conn.Close();
        //    MsgBox(ex.Message, this.Page, this);
        //}
    }

    
    public void getTotals()
    {
        double sum = 0;
        double AmountTotal = 0;
        int rowCount = 0;
        string tot;
        foreach (GridViewRow row in grdApps.Rows)
        {
            // For AMOUNTS
            sum = 0;
            for (int b = 1; b < row.Cells.Count - 2; b += 2)
            {
                if (row.Cells[b].Text == "" || row.Cells[b].Text == null || row.Cells[b].Text == "&nbsp;")
                {
                    row.Cells[b].Text = "0";
                }
                String cellText = row.Cells[b].Text;

                double figure = double.Parse(cellText);

                sum = sum + figure;

                int colCount1 = grdApps.Rows[0].Cells.Count - 2;

                row.Cells[colCount1].Text = sum.ToString();
                row.Cells[colCount1].Font.Bold = true;
                row.Cells[colCount1].Text = Decimal.Parse(row.Cells[colCount1].Text).ToString("n");

                foreach (TableCell cell in row.Cells)
                {
                    cell.Attributes.CssStyle["text-align"] = "right";
                }

            }
            
            rowCount = grdApps.Rows.Count - 1;
            for (int b = 1; b < row.Cells.Count - 1; b ++)
            {
                if (row.Cells[b].Text == "" || row.Cells[b].Text == null || row.Cells[b].Text == "&nbsp;")
                {
                    row.Cells[b].Text = "0";
                }

               //Get Total of TotaHolding
                if (b == row.Cells.Count - 2)
                {
                    if (row.RowIndex == rowCount)
                    {
                        String cellText1 = row.Cells[b].Text;

                        AmountTotal = double.Parse(cellText1);
                    }

                    getExposures(AmountTotal);
                }             
                
                foreach (TableCell cell in row.Cells)
                {
                    cell.Attributes.CssStyle["text-align"] = "right";
                }
            }

            for (int b = 0; b < row.Cells.Count - 1; b ++)
            {
                if (row.Cells[b].Text == "z Total Portifolio")
                {
                    row.Cells[b].Text = "Total Portifolio";
                }                
            }
        }
        
    }

    public void getExposures(Double amntT)
    {
        double Exposure = 0;
        foreach (GridViewRow row in grdApps.Rows)
        {         

            for (int b = 1; b < row.Cells.Count - 1; b += 2)
            {
                if (row.Cells[b].Text == "" || row.Cells[b].Text == null || row.Cells[b].Text == "&nbsp;")
                {
                    row.Cells[b].Text = "0";
                }

                if (b == row.Cells.Count - 2)
                {
                    String cellText = row.Cells[b].Text;

                    double amount = double.Parse(cellText);

                    Exposure = (amount / amntT) * 100;

                    int colCount2 = grdApps.Rows[0].Cells.Count - 1;

                    row.Cells[colCount2].Text = Exposure.ToString("F6");
                    row.Cells[colCount2].Font.Bold = true;
                }
            }
        }
    }



    protected void Timer1_Tick(object sender, EventArgs e)
    {
        GetListDataa();
    }
   

     double totalvalue = 0;
    int a = 1;
    Int32 tot = 0;
    protected void grdApps_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int j = 1; j < e.Row.Cells.Count; j++)
            {
                if (e.Row.Cells[j].Text.Replace("&nbsp;", " ") != " ")
                {
                    e.Row.Cells[j].Text = Decimal.Parse(e.Row.Cells[j].Text).ToString("n");
                }
            }

        } 

        
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
            
            txtFirstName.Text = dr["name"].ToString();
            txtSpecialNotes.Text = dr["special_notes"].ToString();
        }
    }

    //protected void Button1_Click22(object sender, EventArgs e)

    //{
    //    if (cmbSubAccount.SelectedItem.Text == "View Consolidated")
    //    {
    //        //MsgBox("txtID=" + txtID.Text + "&Quarter=" + txtQuarter.Text + "&Subaccount=" + cmbSubAccount.SelectedItem.Value + "&Year=" + txtYear.Text, this.Page, this);
    //        string strscript;
    //        strscript = "<script langauage=JavaScript>";
    //        strscript += "window.open('assetClassHoldingConsolidated.aspx?client_id=" + txtID.Text + "&Quarter=" + txtQuarter.Text + "&Year=" + txtYear.Text + "');";
    //        strscript += "</script>";
    //        ClientScript.RegisterStartupScript(this.GetType(), "newwin", strscript);
    //    }
    //    else
    //    {
    //        //MsgBox("txtID=" + txtID.Text + "&Quarter=" + txtQuarter.Text + "&Subaccount=" + cmbSubAccount.SelectedItem.Value + "&Year=" + txtYear.Text, this.Page, this);
    //        string strscript;
    //        strscript = "<script langauage=JavaScript>";
    //        strscript += "window.open('assetClassHoldingSub.aspx?client_id=" + txtID.Text + "&Quarter=" + txtQuarter.Text + "&Subaccount=" + cmbSubAccount.SelectedItem.Value + "&Year=" + txtYear.Text + "');";
    //        strscript += "</script>";
    //        ClientScript.RegisterStartupScript(this.GetType(), "newwin", strscript);
    //    }
    //}

}

   
        
        
                

    

