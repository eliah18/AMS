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
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Globalization;

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
            loaddata();
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
          //  loadYear();
            //Boolean user=  checkuser(txtFirstName.Text, txtSurname.Text, txtUsername.Text);
            //  if(user)
            //  {
            //      MsgBox("User Already Exists", this.Page, this);

            //  }
        }


    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //MsgBox("Please Select Quarter", this.Page, this);        
        loadreport(txtID.Text, txtQuarter.Text, txtYear.Text);
        Label2.Visible = true;
        Chart1.Visible = true;
        //Button3.Visible = true;

        loadreport2(txtID.Text, txtPrevQuarter.Text, txtPrevQuarterYear.Text);
        Label7.Visible = true;
        Chart2.Visible = true;
        //Button4.Visible = true;
    }
    public void loaddata()
    {
        txtID.Text = Request.QueryString["clientid"];
        txtQuarter.Text = Request.QueryString["quarter"];
        txtYear.Text = Request.QueryString["year"];

        if (txtQuarter.Text == "4")
        {
            txtPrevQuarter.Text = "3";
            txtPrevQuarterYear.Text = txtYear.Text;
        }
        else if (txtQuarter.Text == "3")
        {
            txtPrevQuarter.Text = "2";
            txtPrevQuarterYear.Text = txtYear.Text;
        }
        else if (txtQuarter.Text == "2")
        {
            txtPrevQuarter.Text = "1";
            txtPrevQuarterYear.Text = txtYear.Text;
        }
        else if (txtQuarter.Text == "1")
        {
            txtPrevQuarterYear.Text = (Int32.Parse(txtYear.Text.ToString()) - 1).ToString();
            txtPrevQuarter.Text = "4";
        }

        conn.Close();
        conn.Open();

        string Query = " select * from Clients where client_number='" + Request.QueryString["clientid"] + "'";
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

    public void loadreport(String clientid, String quarter, String year)
    {
        //MsgBox("hie", this.Page, this);
        //try
        {           

            conn.Open();
            //SqlCommand cmd = new SqlCommand("Exposures ", conn);
            //cmd.CommandType = CommandType.StoredProcedure;
            String query = "select Asset_class_id, exposure from (select  asset_class_id,(sum(cast (value as float)))/(select sum(cast (value as float)) from [Investments] where client_id='" + clientid + "' and quarter='" + quarter + "' and year='" + year + "')*100  as exposure from  [Investments] where client_id='" + clientid + "' and  year='" + year + "' and quarter='" + quarter + "' group  by Asset_class_id) tt";
            SqlCommand cmd = new SqlCommand(query, conn);
            //cmd.Parameters.AddWithValue("@clientid", txtID.Text);
            //cmd.Parameters.AddWithValue("@quarter", quarter);
            //cmd.Parameters.AddWithValue("@year", year);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);
            DataTable ChartData = ds.Tables[0];

            //storing total rows count to loop on each Record
            string[] XPointMember = new string[ChartData.Rows.Count];
            decimal[] YPointMember = new decimal[ChartData.Rows.Count];
            //DataTable dt = ds.Tables[0];
            for (int count = 0; count < ChartData.Rows.Count; count++)
            {
                //storing Values for X axis
                XPointMember[count] = ChartData.Rows[count]["asset_class_id"].ToString();
                //storing values for Y Axis
                YPointMember[count] = Convert.ToDecimal(ChartData.Rows[count]["Exposure"]);                
            }
            //binding chart control
            Chart1.Series[0].Points.DataBindXY(XPointMember, YPointMember);

            //Setting width of line
            Chart1.Series[0].BorderWidth = 2;
            //setting Chart type 
            Chart1.Series[0].ChartType = SeriesChartType.Pie;


            foreach (Series charts in Chart1.Series)
            {
                foreach (DataPoint point in charts.Points)
                {
                    switch (point.AxisLabel)
                    {
                        case "Listed equities": point.Color = Color.RoyalBlue; break;
                        case "Unlisted Property": point.Color = Color.LightSlateGray; break;
                        case "Bonds and Debentures": point.Color = Color.Gold; break;
                        case "Cash": point.Color = Color.CadetBlue; break;
                        case "Foreign Equity": point.Color = Color.Green; break;
                        case "Guaranteed Fund": point.Color = Color.Coral; break;
                        case "Listed property": point.Color = Color.PaleGreen; break;
                        case "Money Market": point.Color = Color.PaleVioletRed; break;
                        case "Unlisted Equity": point.Color = Color.PaleTurquoise; break;
                        case "Alternative Investments": point.Color = Color.BlueViolet; break; 
                        case "Cash Nostro": point.Color = Color.Crimson; break;
                    }
                    point.Label = string.Format("{0:0.000000}%  {1}", point.YValues[0], point.AxisLabel);

                }
            }


            //Enabled 3D
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            //Seperate overlapping Labels
            Chart1.Series[0]["PieLabelStyle"] = "Outside";
            Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
            Chart1.ChartAreas[0].Area3DStyle.Inclination = 50;

            //Handle Empty Data
            TextAnnotation ta = new TextAnnotation();
            ta.Text = "No Data Yet";
            ta.X = 45;  // % of the..
            ta.Y = 45;  // chart size 
            ta.Font = new Font("font sans serif", 10f);
            ta.Visible = false;  // first we hide it
            Chart1.Annotations.Add(ta);
            ta.Visible = (!Chart1.Series[0].Points.Any());

            conn.Close();

        }
        
    }

    public void loadreport2(String clientid, String quarter, String year)
    {
        //MsgBox("hie", this.Page, this);
        //try
        {

            conn.Open();
            //SqlCommand cmd = new SqlCommand("Exposures ", conn);
            //cmd.CommandType = CommandType.StoredProcedure;
            String query = "select Asset_class_id, exposure from (select  asset_class_id,(sum(cast (value as float)))/(select sum(cast (value as float)) from [Investments] where client_id='" + clientid + "' and quarter='" + quarter + "' and year='" + year + "')*100  as exposure from  [Investments] where client_id='" + clientid + "' and  year='" + year + "' and quarter='" + quarter + "' group  by Asset_class_id) tt";
            SqlCommand cmd = new SqlCommand(query, conn);
            //cmd.Parameters.AddWithValue("@clientid", txtID.Text);
            //cmd.Parameters.AddWithValue("@quarter", quarter);
            //cmd.Parameters.AddWithValue("@year", year);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);
            DataTable ChartData = ds.Tables[0];

            //storing total rows count to loop on each Record
            string[] XPointMember = new string[ChartData.Rows.Count];
            //int[] YPointMember = new int[ChartData.Rows.Count];
            decimal[] YPointMember = new decimal[ChartData.Rows.Count];
            //DataTable dt = ds.Tables[0];
            for (int count = 0; count < ChartData.Rows.Count; count++)
            {
                //storing Values for X axis
                XPointMember[count] = ChartData.Rows[count]["asset_class_id"].ToString();
                //storing values for Y Axis
                YPointMember[count] = Convert.ToInt32(ChartData.Rows[count]["Exposure"]);

            }
            //binding chart control
            Chart2.Series[0].Points.DataBindXY(XPointMember, YPointMember);

            //Setting width of line
            Chart2.Series[0].BorderWidth = 2;
            //setting Chart type 
            Chart2.Series[0].ChartType = SeriesChartType.Pie;


            foreach (Series charts in Chart2.Series)
            {
                foreach (DataPoint point in charts.Points)
                {
                    switch (point.AxisLabel)
                    {
                        case "Listed equities": point.Color = Color.RoyalBlue; break;
                        case "Unlisted Property": point.Color = Color.LightSlateGray; break;
                        case "Bonds and Debentures": point.Color = Color.Gold; break;
                        case "Cash": point.Color = Color.CadetBlue; break;
                        case "Foreign Equity": point.Color = Color.Green; break;
                        case "Guaranteed Fund": point.Color = Color.Coral; break;
                        case "Listed property": point.Color = Color.PaleGreen; break;
                        case "Money Market": point.Color = Color.PaleVioletRed; break;
                        case "Unlisted Equity": point.Color = Color.PaleTurquoise; break;
                        case "Alternative Investments": point.Color = Color.BlueViolet; break;
                        case "Cash Nostro": point.Color = Color.Crimson; break;

                    }
                    point.Label = string.Format("{0:0.000000}%  {1}", point.YValues[0], point.AxisLabel);

                }
            }

            //Enabled 3D
            Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            //Seperate overlapping Labels
            Chart2.Series[0]["PieLabelStyle"] = "Outside";
            Chart2.ChartAreas[0].Area3DStyle.Enable3D = true;
            Chart2.ChartAreas[0].Area3DStyle.Inclination = 50;

            //Handle Empty Data
            TextAnnotation ta = new TextAnnotation();
            ta.Text = "No Data Yet";
            ta.X = 45;  // % of the..
            ta.Y = 45;  // chart size 
            ta.Font = new Font("font sans serif", 10f);
            ta.Visible = false;  // first we hide it
            Chart2.Annotations.Add(ta);
            ta.Visible = (!Chart2.Series[0].Points.Any());

            conn.Close();

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
        string FileName = "PortiffolioSnapshot" + DateTime.Now + ".xls";
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

    protected void BtnPrintPie1_Click1(object sender, EventArgs e)
    {
        //ExportGridToExcel();


        //Response.Clear();
        //Response.Buffer = true;
        //Response.ClearContent();
        //Response.ClearHeaders();
        //Response.Charset = "";
        //string FileName = "Q2Q_Y" + txtYear.Text.ToString() +"Q" + txtQuarter.Text.ToString() + "_"+ DateTime.Now + ".xls";
        //StringWriter strwritter = new StringWriter();
        //HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.ContentType = "application/vnd.ms-excel";
        //Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        //grdPortifolioSnapshot.GridLines = GridLines.Both;
        //grdPortifolioSnapshot.HeaderStyle.Font.Bold = true;
        //grdPortifolioSnapshot.RenderControl(htmltextwrtter);
        //Response.Write(strwritter.ToString());
        //Response.End();

       
            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=ChartExport.xls");
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.Charset = "";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(sw);
            //Chart1.RenderControl(hw);
            //string src = Regex.Match(sw.ToString(), "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
            //string img = string.Format("<img src = '{0}{1}' />", Request.Url.GetLeftPart(UriPartial.Authority), src);

            //Table table = new Table();
            //TableRow row = new TableRow();
            //row.Cells.Add(new TableCell());
            //row.Cells[0].Width = 200;
            //row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            //row.Cells[0].Controls.Add(new Label { Text = "Fruits Distribution (India)", ForeColor = Color.Red });
            //table.Rows.Add(row);
            //row = new TableRow();
            //row.Cells.Add(new TableCell());
            //row.Cells[0].Controls.Add(new Literal { Text = img });
            //table.Rows.Add(row);

            //sw = new StringWriter();
            //hw = new HtmlTextWriter(sw);
            //table.RenderControl(hw);
            //Response.Write(sw.ToString());
            //Response.Flush();
            //Response.End();
        
    }
    protected void BtnPrintPie2_Click1(object sender, EventArgs e)
    {
        //ExportGridToExcel();
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "Q2Q_Y" + txtPrevQuarterYear.Text.ToString() + "Q" + txtPrevQuarter.Text.ToString() + "_" + DateTime.Now + ".xls";
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


    //-------------------------------------------------------------------------------------------------------------------------------
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

}

   
        
        
                

    

