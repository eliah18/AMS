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
            loadYear();
            //Boolean user=  checkuser(txtFirstName.Text, txtSurname.Text, txtUsername.Text);
            //  if(user)
            //  {
            //      MsgBox("User Already Exists", this.Page, this);

            //  }
        }


    }
  











    

   
    public void loadreport(String clientid,String quarter, String year)
    {
        //MsgBox("hie", this.Page, this);
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("PrescribedAssetClass ", conn);
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
                    grdPortifolioSnapshot.DataSource = ds;
                    grdPortifolioSnapshot.DataBind();
                }
            }
            else
            {
                grdPortifolioSnapshot.DataSource = null;
                grdPortifolioSnapshot.DataBind();
            }
            //getTotalsFrom();
            //getTotalsTo();
            //getTotalsChange();
            //getTotal();
            //getTotalExposure();
            //getTotalTarget();
           // getTotalIpsecLimit();
            Button1.Visible = true;
        }

        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
    }
    public void getTotalExposure()
    {
        {
            double sum = 0;
            for (int i = 0; i < grdPortifolioSnapshot.Rows.Count; ++i)
            {
               // sum += Convert.ToDouble(grdPortifolioSnapshot.Rows[i].Cells[2].Text);

                //MsgBox(grdQuarterToQuarter.Rows.Count.ToString(), this.Page, this);
            }
            grdPortifolioSnapshot.Rows[grdPortifolioSnapshot.Rows.Count-1].Cells[2].Text = "100%";
            //'txtsum.Text = sum.ToString();

        }

    }
    public void getTotalTarget()
    {
        {
            double sum = 0;
            for (int i = 0; i < grdPortifolioSnapshot.Rows.Count; ++i)
            {
                //sum += Convert.ToDouble(grdPortifolioSnapshot.Rows[i].Cells[4].Text);

                //MsgBox(grdQuarterToQuarter.Rows.Count.ToString(), this.Page, this);
            }
            grdPortifolioSnapshot.Rows[grdPortifolioSnapshot.Rows.Count-1].Cells[4].Text = "100%";
            //'txtsum.Text = sum.ToString();

        }

    }

    public void getTotal()
    {
        {
            double sum = 0;
            for (int i = 0; i < grdPortifolioSnapshot.Rows.Count; ++i)
            {
                sum += Convert.ToDouble(grdPortifolioSnapshot.Rows[i].Cells[1].Text);

                //MsgBox(grdQuarterToQuarter.Rows.Count.ToString(), this.Page, this);
            }
            grdPortifolioSnapshot.Rows[grdPortifolioSnapshot.Rows.Count-1].Cells[1].Text = sum.ToString();
            //'txtsum.Text = sum.ToString();

        }

    }
    public void getTotalIpsecLimit()
    {
        {
            double sum = 0;
            for (int i = 0; i < grdPortifolioSnapshot.Rows.Count; ++i)
            {
                sum += Convert.ToDouble(grdPortifolioSnapshot.Rows[i].Cells[5].Text);

                //MsgBox(grdQuarterToQuarter.Rows.Count.ToString(), this.Page, this);
            }
            grdPortifolioSnapshot.Rows[grdPortifolioSnapshot.Rows.Count-1].Cells[5].Text = sum.ToString()+"%";
            //'txtsum.Text = sum.ToString();

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
        string FileName = "PortiffolioSnapshot" + DateTime.Now.ToString("ddMMMyyyyhhfff") + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        grdPortifolioSnapshot.GridLines = GridLines.Both;
        grdPortifolioSnapshot.HeaderStyle.Font.Bold = true;
        grdPortifolioSnapshot.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();

    }
    public void loadYear()
    {
        conn.Close();
        conn.Open();
        string com = " select year from years";
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
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("User.aspx");
    }





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

           
            txtID.Text = dr["client_number"].ToString();
            // GetListData();
            
               // loadassetmanagers();
            




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




   

    protected void repBusAssetsIB_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        //TextBox txt = repBusAssetsIB.Items[0].FindControl("res") as TextBox;
        //TextBox moneymarket = repBusAssetsIB.Items[1].FindControl("res") as TextBox;
        //TextBox bonds = repBusAssetsIB.Items[2].FindControl("res") as TextBox;
        
        

    }

    public void lnkedit(object sender, System.EventArgs e)
    {
       
       // string idd = ((LinkButton)sender).CommandArgument;
        

       //// String assetmanager = rdAssetMangers.SelectedItem.Value;
       // String clientid = txtID.Text;
       // String date = cmbYear.Text;
       // String quarter = rdQuarter.SelectedItem.Value;


       // string strPopup = "<script language='javascript' ID='script1'>"
           
       // // Passing intId to popup window.
       // + "window.open('ClientFundsLinked.aspx?data=" + HttpUtility.UrlEncode(idd.ToString()) + "&asset= " + HttpUtility.UrlEncode(assetmanager.ToString())  + "&clientid=" + HttpUtility.UrlEncode(clientid.ToString())  + "&year=" + HttpUtility.UrlEncode(date.ToString()) + "&quarter=" + HttpUtility.UrlEncode(quarter.ToString())

       // + "','new window', 'top=90, left=200, width=1000, height=1000, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=yes, scrollbars=y, toolbar=yes, status=yes, center=yes')"

       // + "</script>";

       // ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
    }
  


    protected void rdAssetMangers_SelectedIndexChanged(object sender, EventArgs e)
    {
       

       
       
    }



    

    protected void Button5_Click(object sender, EventArgs e)
    {
        

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
        if (cmbYear.SelectedItem.Text == "Select Year")
        {
            MsgBox("Please Select Year First", this.Page, this);
            return;
        }
        if (rdQuarter.SelectedValue == "")
        {
            MsgBox("Please Select Quarter", this.Page, this);
            return;
        }
        //MsgBox("Please Select Quarter", this.Page, this);

        String date = cmbYear.Text.Trim();
        loadreport(txtID.Text, rdQuarter.SelectedItem.Value,cmbYear.Text);
      
    }
}

   
        
        
                

    

