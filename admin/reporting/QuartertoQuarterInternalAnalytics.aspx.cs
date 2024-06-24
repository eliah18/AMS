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
            loadQuarterToValues(fetchCurrentQuarter());
            loadQuarterFromValues(fetchCurrentQuarter());
            loadcurrentyearvalues(fetchCurrentYear());


            //Boolean user=  checkuser(txtFirstName.Text, txtSurname.Text, txtUsername.Text);
            //  if(user)
            //  {
            //      MsgBox("User Already Exists", this.Page, this);

            //  }
        }


    }
  











    

    public void loadassetmanagers()
    {
        conn.Close();
        conn.Open();
        string com = " select c.assetmanager_id as id,a.surname as name,c.client_id from client_assetmanager_relations c join clients q on c.client_id=q.client_number join asset_managers a on a.id=c.assetmanager_id where c.client_id='" + txtID.Text + "' AND c.active='1'";
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
    public void loadreport(String clientid,String quarter, String year, String  quarterto,String yearto)
    {
        //MsgBox("hie", this.Page, this);
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select a.Asset_class_id as [Asset Name],isnull(a.totalfrom, 0) as [Total From], round((isnull(b.total-a.totalfrom,0)/a.totalfrom)*100,2 ) as [Change(%)],isnull(b.total,0) as [Total To] from (select Asset_class_id,sum(value) as totalfrom from Investments where client_id='"+txtID.Text+"' and year='"+cmbYear.SelectedItem.Text+"' and quarter='"+rdQuarter.Text+"' group by Asset_class_id ) a left join (select asset_class_id, sum(value) as total from Investments where client_id = '"+txtID.Text+"' and year = '"+cmbYearTo.Text+"' and quarter = '"+rdQuarterTo.Text+"' group by Asset_class_id) b on a.Asset_class_id = b.Asset_class_id union all select top 1 'Total Portifolio' as assetclass,'0' as totalfrom,'0' as variance,'0' as totalto from investments  ", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    grdQuarterToQuarter.DataSource = ds;
                    grdQuarterToQuarter.DataBind();
                }
            }
            else
            {
                grdQuarterToQuarter.DataSource = null;
                grdQuarterToQuarter.DataBind();
            }
            getTotalsFrom();
            getTotalsTo();
            getTotalsChange();
            Button1.Visible = true;
        }
        
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }
    public void getTotalsFrom()
    {
        {
            double sum = 0;
            for (int i = 0; i < grdQuarterToQuarter.Rows.Count; ++i)
            {
                sum += Convert.ToDouble(grdQuarterToQuarter.Rows[i].Cells[1].Text);

                //MsgBox(grdQuarterToQuarter.Rows.Count.ToString(), this.Page, this);
            }
            grdQuarterToQuarter.Rows[grdQuarterToQuarter.Rows.Count-1].Cells[1].Text = sum.ToString();

            //txtsum.Text = sum.ToString();
            
        }

    }
    public void getTotalsTo()
    {
        {
            double sum = 0;
            for (int i = 0; i < grdQuarterToQuarter.Rows.Count; ++i)
            {
                sum += Convert.ToDouble(grdQuarterToQuarter.Rows[i].Cells[3].Text);

                //MsgBox(grdQuarterToQuarter.Rows.Count.ToString(), this.Page, this);
            }
            grdQuarterToQuarter.Rows[grdQuarterToQuarter.Rows.Count-1].Cells[3].Text = sum.ToString();
            //'txtsum.Text = sum.ToString();

        }

    }
    public void getTotalsChange()
    {
        {
            double sum = 0;
            for (int i = 0; i < grdQuarterToQuarter.Rows.Count; ++i)
            {
                sum += Convert.ToDouble(grdQuarterToQuarter.Rows[i].Cells[2].Text);

                //MsgBox(grdQuarterToQuarter.Rows.Count.ToString(), this.Page, this);
            }
            grdQuarterToQuarter.Rows[grdQuarterToQuarter.Rows.Count-1].Cells[2].Text = sum.ToString();
            //txtsum.Text = sum.ToString();

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
        string FileName = "QuarterToQuarter" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        grdQuarterToQuarter.GridLines = GridLines.Both;
        grdQuarterToQuarter.HeaderStyle.Font.Bold = true;
        grdQuarterToQuarter.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();

    }

    protected void grdQuarterToQuarter_RowDataBound(object sender, GridViewRowEventArgs e)
    {

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

        cmbYearTo.DataSource = dt;
        cmbYearTo.DataBind();
        cmbYearTo.DataTextField = "year";
        cmbYearTo.DataValueField = "year";
        cmbYearTo.DataBind();
        cmbYearTo.Items.Insert(0, new ListItem("Select Year", "0"));



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
            
                loadassetmanagers();
            




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
       
        string idd = ((LinkButton)sender).CommandArgument;
        

        String assetmanager = rdAssetMangers.SelectedItem.Value;
        String clientid = txtID.Text;
        String date = cmbYear.Text;
        String quarter = rdQuarter.SelectedItem.Value;


        string strPopup = "<script language='javascript' ID='script1'>"
           
        // Passing intId to popup window.
        + "window.open('ClientFundsLinked.aspx?data=" + HttpUtility.UrlEncode(idd.ToString()) + "&asset= " + HttpUtility.UrlEncode(assetmanager.ToString())  + "&clientid=" + HttpUtility.UrlEncode(clientid.ToString())  + "&year=" + HttpUtility.UrlEncode(date.ToString()) + "&quarter=" + HttpUtility.UrlEncode(quarter.ToString())

        + "','new window', 'top=90, left=200, width=1000, height=1000, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=yes, scrollbars=y, toolbar=yes, status=yes, center=yes')"

        + "</script>";

        ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
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
        loadreport(txtID.Text, rdQuarter.SelectedItem.Value,cmbYear.Text, rdQuarterTo.SelectedItem.Value, cmbYearTo.SelectedItem.Value);
      
    }



    protected void grdQuarterToQuarter_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdQuarterToQuarter.PageIndex = e.NewPageIndex;
        loadreport(txtID.Text, rdQuarter.SelectedItem.Value, cmbYear.Text, rdQuarterTo.SelectedItem.Value, cmbYearTo.SelectedItem.Value);

    }


    public int fetchCurrentQuarter()
    {
        int currentquarter = 0;
        // try
        {
            conn.Close();
            conn.Open();

            string Query = "SELECT DATENAME(qq, getdate()) as  reportquarter ";
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

                currentquarter = Convert.ToInt32(dr["reportquarter"].ToString());

                //txtValue.Text = dr["value"].ToString();
                //txtupdateoriginalvalue.Text = dr["value"].ToString();


                //txtID.Text = dr["id"].ToString();


                //usersPanel.Visible = true;
                //btnmoneymarket.Visible = false;
                //editmoneymarket.Visible = true;
                //Button1.Visible = false;
                //Button2.Visible = true;

            }

        }
        //catch (Exception ex)
        //{
        //    conn.Close();
        //    MsgBox(ex.Message, this.Page, this);
        //}

        return currentquarter;



    }
    public int fetchCurrentYear()
    {
        int currentyear = 0;
        // try
        {
            conn.Close();
            conn.Open();

            string Query = "SELECT DATENAME(qq, getdate()) as  reportquarter,DATEPART(year, getdate()) as year ";
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

                currentyear = Convert.ToInt32(dr["year"].ToString());

                //txtValue.Text = dr["value"].ToString();
                //txtupdateoriginalvalue.Text = dr["value"].ToString();


                //txtID.Text = dr["id"].ToString();


                //usersPanel.Visible = true;
                //btnmoneymarket.Visible = false;
                //editmoneymarket.Visible = true;
                //Button1.Visible = false;
                //Button2.Visible = true;

            }

        }
        //catch (Exception ex)
        //{
        //    conn.Close();
        //    MsgBox(ex.Message, this.Page, this);
        //}

        return currentyear;



    }


    public void loadQuarterToValues(int quarter)
    {
        switch (quarter)
        {
            case 1:
                rdQuarterTo.SelectedValue = "1";
                rdQuarterTo.Text = "First Quarter";
                break;

            case 2:
                rdQuarterTo.SelectedValue = "2";
                rdQuarterTo.Text = "Second  Quarter";
                break;


            case 3:
                rdQuarterTo.SelectedValue = "3";
                rdQuarterTo.Text = "Third  Quarter";
                break;


            case 4:
                rdQuarterTo.SelectedValue = "4";
                rdQuarterTo.Text = "Fourth  Quarter";
                break;


        }


    }
    public void loadcurrentyearvalues(int year)
    {

        int yearfrom = year;
        if (fetchCurrentQuarter()==1)
        {
            yearfrom = year - 1;
        }

        cmbYearTo.SelectedValue = year.ToString();
        cmbYearTo.Text= year.ToString();



        cmbYear.SelectedValue = yearfrom.ToString();
        cmbYear.Text = yearfrom.ToString();



    }
    public void loadQuarterFromValues(int quarter)
    {
        int quarterfrom = 0;
        if (quarter == 1)
        {
            quarterfrom = 4;
        }
        else
        {
            quarterfrom = quarter - 1;
        }

        switch (quarterfrom)
        {
            case 1:
                rdQuarter.SelectedValue = "1";
                rdQuarterTo.Text = "First Quarter";
                break;

            case 2:
                rdQuarter.SelectedValue = "2";
                rdQuarterTo.Text = "Second  Quarter";
                break;


            case 3:
                rdQuarter.SelectedValue = "3";
                rdQuarter.Text = "Third  Quarter";
                break;


            case 4:
                rdQuarter.SelectedValue = "4";
                rdQuarter.Text = "Fourth  Quarter";
                break;


        }


    }
}








