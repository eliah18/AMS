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
            try
            {
                if (Session["user"].ToString() == "")
                {

                    // MsgBox("hala", Page, this);
                    // GetListData();

                    ///----------------------------
                    
                    ////Fetch the Statistical data from database.
                    //string query = "SELECT ShipCountry, DATEPART(Year, OrderDate) [Year], COUNT(OrderId) [Total]";
                    //query += " FROM Orders WHERE ShipCountry IN ('France', 'Germany', 'Brazil')";
                    //query += " GROUP BY ShipCountry, DATEPART(Year, OrderDate)";
                    //DataTable dt = GetData(query);

                    ////Get the DISTINCT Countries.
                    //List<string> countries = (from p in dt.AsEnumerable()
                    //                            select p.Field<string>("ShipCountry")).Distinct().ToList();

                    ////Loop through the Countries.
                    //foreach (string country in countries)
                    //{

                    //    //Get the Year for each Country.
                    //    int[] x = (from p in dt.AsEnumerable()
                    //                where p.Field<string>("ShipCountry") == country
                    //                orderby p.Field<int>("Year") ascending
                    //                select p.Field<int>("Year")).ToArray();

                    //    //Get the Total of Orders for each Country.
                    //    int[] y = (from p in dt.AsEnumerable()
                    //                where p.Field<string>("ShipCountry") == country
                    //                orderby p.Field<int>("Year") ascending
                    //                select p.Field<int>("Total")).ToArray();

                    //    //Add Series to the Chart.
                    //    Chart1.Series.Add(new Series(country));
                    //    Chart1.Series[country].IsValueShownAsLabel = true;
                    //    Chart1.Series[country].ChartType = SeriesChartType.StackedBar;
                    //    Chart1.Series[country].Points.DataBindXY(x, y);
                    //}

                    //Chart1.Legends[0].Enabled = true;
                        
                    ///--------------------------------------------
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

    private static DataTable GetData(string query)
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
        }
    }


    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    //MsgBox("Please Select Quarter", this.Page, this);        
    //    //loadreport(txtID.Text, txtQuarter.Text, txtYear.Text);
    //    Label2.Visible = true;
    //    Chart1.Visible = true;
    //    //Button3.Visible = true;

    //}
    public void loaddata()
    {
        txtID.Text = Request.QueryString["clientid"];
        txtQuarter.Text = Request.QueryString["quarter"];
        txtYear.Text = Request.QueryString["year"];


        conn.Close();
        conn.Open();

        string Query = " select * from Clients where client_number='" + Request.QueryString["clientid"] + "'";
        SqlCommand cmd = new SqlCommand(Query, conn);
        SqlDataReader dr = cmd.ExecuteReader();

        if (dr.Read() == true)


        {
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
            //String query = "select Asset_class_id,cast(round(exposure,4) as numeric(18,4)) as exposure from (select  asset_class_id,(sum(value))/(select sum(value) from [Investments] where client_id='" + clientid + "' and quarter='" + quarter + "' and year='" + year + "')*100  as exposure from  [Investments] where client_id='" + clientid + "' and  year='" + year + "' and quarter='" + quarter + "' group  by Asset_class_id) tt";
            String query = " DECLARE @cols AS NVARCHAR(MAX),@selectCols AS NVARCHAR(MAX),@query AS NVARCHAR(MAX),@client as nvarchar(50),@year as nvarchar(50),@quarter as nvarchar(50); set @client='" + clientid + "' set @year='" + year + "' set @quarter='" + quarter + "'; SELECT  @selectCols = STUFF ((SELECT distinct ', ISNULL(' + QUOTENAME([surname]) + ', 0) AS ' + QUOTENAME([surname]) FROM(select surname, Asset_class_id, value, TotalAcrossManagers, TotalPerManager,case when ExposurePerMan is null then 0 else ExposurePerMan end as ExposurePerMan,ExposureAcrossManagers from(select surname, Asset_class_id, value, TotalAcrossManagers, TotalPerManager, (case when isnull(VALUE,1)= 0 then 1 else isnull(VALUE, 1) end /case when isnull(TotalPerManager,1)= 0 then 1 else isnull(TotalPerManager, 1) end)*100 AS ExposurePerMan,(case when isnull(TotalAcrossManagers, 1) = 0 then 1 else isnull(TotalAcrossManagers, 1) end / case when isnull(GrandTotal,1)= 0 then 1 else isnull(GrandTotal, 1) end)*100 AS ExposureAcrossManagers FROM(select *, SUM(VALUE) OVER(PARTITION BY Asset_class_id) AS TotalAcrossManagers, SUM(VALUE) OVER(PARTITION BY surname) AS TotalPerManager, sum(VALUE) OVER(PARTITION BY client_id) AS GrandTotal from(select surname, Asset_class_id, sum(value) value, client_id, year from(SELECT i.*, am.surname FROM Investments i left join asset_managers am on am.id = i.Asset_manager_id)tt where tt.quarter = @quarter and year = @year and client_id = @client group by surname, asset_class_id, client_id, year) covids) TT) xx ) tt FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,1,'') SELECT @cols = STUFF((SELECT distinct ',' + QUOTENAME([surname]) FROM(select surname, Asset_class_id, value, TotalAcrossManagers, TotalPerManager,case when ExposurePerMan is null then 0 else ExposurePerMan end as ExposurePerMan,ExposureAcrossManagers from(select surname, Asset_class_id, value, TotalAcrossManagers, TotalPerManager, (case when isnull(VALUE,1)= 0 then 1 else isnull(VALUE, 1) end /case when isnull(TotalPerManager,1)= 0 then 1 else isnull(TotalPerManager, 1) end)*100 AS ExposurePerMan,(case when isnull(TotalAcrossManagers, 1) = 0 then 1 else isnull(TotalAcrossManagers, 1) end / case when isnull(GrandTotal,1)= 0 then 1 else isnull(GrandTotal, 1) end)*100 AS ExposureAcrossManagers FROM(select *, SUM(VALUE) OVER(PARTITION BY Asset_class_id) AS TotalAcrossManagers, SUM(VALUE) OVER(PARTITION BY surname) AS TotalPerManager, sum(VALUE) OVER(PARTITION BY client_id) AS GrandTotal from(select surname, Asset_class_id, sum(value) value, client_id, year from(SELECT i.*, am.surname FROM Investments i left join asset_managers am on am.id = i.Asset_manager_id)tt where tt.quarter = @quarter and year = @year and client_id = @client group by surname, asset_class_id, client_id, year) covids) TT) xx ) tt FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,1,'') SET @query = 'SELECT distinct Asset_class_id,' + @selectCols + ' FROM (select surname,Asset_class_id,case when ExposurePerMan is null then 0 else ExposurePerMan end as ExposurePerMan,ExposureAcrossManagers from(select surname, Asset_class_id, value, TotalAcrossManagers, TotalPerManager, (case when isnull(VALUE, 1) = 0 then 1 else isnull(VALUE, 1) end /case when isnull(TotalPerManager,1)= 0 then 1 else isnull(TotalPerManager, 1) end)*100 AS ExposurePerMan,(case when isnull(TotalAcrossManagers, 1) = 0 then 1 else isnull(TotalAcrossManagers, 1) end / case when isnull(GrandTotal,1)= 0 then 1 else isnull(GrandTotal, 1) end)*100 AS ExposureAcrossManagers FROM(select *, SUM(VALUE) OVER(PARTITION BY Asset_class_id) AS TotalAcrossManagers, SUM(VALUE) OVER(PARTITION BY surname) AS TotalPerManager, sum(VALUE) OVER(PARTITION BY client_id) AS GrandTotal from(select surname, Asset_class_id, sum(value) value, client_id, year from(SELECT i.*, am.surname FROM Investments i left join asset_managers am on am.id = i.Asset_manager_id)tt where tt.quarter = ''" + quarter + "'' and year = ''" + year + "'' and client_id = ''" + clientid + "'' group by surname, asset_class_id, client_id, year) covids) TT ) tt) x pivot(Sum ([ExposurePerMan]) for [surname] in (' + @cols + ')) p' execute(@query)";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);
            DataTable ChartData = ds.Tables[0];

            //storing total rows count to loop on each Record
            string[] XPointMember = new string[ChartData.Rows.Count];
            int[] YPointMember = new int[ChartData.Rows.Count];
            //DataTable dt = ds.Tables[0];
            for (int count = 0; count < ChartData.Rows.Count; count++)
            {
                //storing Values for X axis
                XPointMember[count] = ChartData.Rows[count]["asset_class_id"].ToString();
                //storing values for Y Axis
                YPointMember[count] = Convert.ToInt32(ChartData.Rows[count]["Exposure"]);

            }
            ////binding chart control
            //Chart1.Series[0].Points.DataBindXY(XPointMember, YPointMember);

            ////Setting width of line
            //Chart1.Series[0].BorderWidth = 2;
            ////setting Chart type 
            //Chart1.Series[0].ChartType = SeriesChartType.Pie;


            //foreach (Series charts in Chart1.Series)
            //{
            //    foreach (DataPoint point in charts.Points)
            //    {
            //        switch (point.AxisLabel)
            //        {
            //            case "listed equities": point.Color = Color.RoyalBlue; break;
            //            case "Unlisted Property": point.Color = Color.SaddleBrown; break;
            //            case "Bonds and Debentures": point.Color = Color.Yellow; break;
            //            case "Cash": point.Color = Color.HotPink; break;
            //            case "Foreign Equity": point.Color = Color.Orange; break;
            //            case "Guaranteed Fund": point.Color = Color.Green; break;
            //            case "listed property": point.Color = Color.Gray; break;
            //            case "Money Market": point.Color = Color.Red; break;
            //            case "Unlisted Equity": point.Color = Color.Purple; break;
            //            case "Alternative Investments": point.Color = Color.Silver; break;

            //        }
            //        point.Label = string.Format("{0:0} - {1}", point.YValues[0], point.AxisLabel);

            //    }
            //}


            ////Enabled 3D
            //Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            ////Seperate overlapping Labels
            //Chart1.Series[0]["PieLabelStyle"] = "Outside";
            //Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
            //Chart1.ChartAreas[0].Area3DStyle.Inclination = 10;

            ////Handle Empty Data
            //TextAnnotation ta = new TextAnnotation();
            //ta.Text = "No Data Yet";
            //ta.X = 45;  // % of the..
            //ta.Y = 45;  // chart size 
            //ta.Font = new Font("font sans serif", 10f);
            //ta.Visible = false;  // first we hide it
            //Chart1.Annotations.Add(ta);
            //ta.Visible = (!Chart1.Series[0].Points.Any());

            //conn.Close();

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
    }
    protected void BtnPrintPie2_Click1(object sender, EventArgs e)
    {
        //ExportGridToExcel();
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

   
        
        
                

    

