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
            
            try
            {
                if (Session["user"].ToString() == "")
                {

                    // MsgBox("hala", Page, this);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/logins.aspx");
            }

            lbUsername.Text = "Logged in as" + " " + " " + (string)Session["username"] + "" + "" + (string)Session["role"];
          
        }


    }
    //loadreport(txtID.Text, txtQuarter.Text, txtYear.Text, txtPrevQuarter.Text, txtPrevQuarterYear.Text);
    public void loadreport(String client,String selectedQuarter, String selectedYear, String prevQuarter,String prevYear)
    {
        //MsgBox("hie", this.Page, this);
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("declare @firstQuarter nvarchar(50)='"+ @prevQuarter  + "'; declare @secondQuarter nvarchar(50)='"+ @selectedQuarter + "'; declare @firstYear nvarchar(50)='"+ @prevYear + "'; declare @secondYear nvarchar(50)='"+ @selectedYear + "'; declare @client nvarchar(50)='"+ @client + "'; select * from (select a.manager [Asset Manager],a.TotalPerMan [Previous Quarter],b.TotalPerMan-a.TotalPerMan [Investment Gain/Loss], b.TotalPerMan [Selected Quarter],a.TotWeight as [Weight In Total For Previous Quarter ],b.TotWeight [Weight In Total For Selected Quarter] from (select *, concat((cast(round((TotalPerMan / TotalPerYearnQ) * 100, 2) as numeric(18, 2))), ' %') as TotWeight from(select *, sum(TotalPerMan) over(PARTITION BY quarter) as TotalPerYearnQ from(select distinct manager, Asset_manager_id, TotalPerMan, client_id, year, quarter from(select * from(select a.surname as manager, i.Asset_manager_id, sum(i.value) over(PARTITION by quarter, Asset_manager_id) as TotalPerMan, i.client_id, i.year, i.quarter from Investments i join asset_managers a on i.Asset_manager_id = a.id where (quarter = @firstQuarter and year = @firstYear and client_id = @client)) x) y) y2) z)A join (select*, concat((cast(round((TotalPerMan/ TotalPerYearnQ)*100,2) as numeric(18, 2))), ' %') as TotWeight from(select *, sum(TotalPerMan) over(PARTITION BY quarter) as TotalPerYearnQ from(select distinct manager, Asset_manager_id, TotalPerMan, client_id, year, quarter from(select * from(select a.surname as manager, i.Asset_manager_id, sum(i.value) over(PARTITION by quarter, Asset_manager_id) as TotalPerMan, i.client_id, i.year, i.quarter from Investments i join asset_managers a on i.Asset_manager_id = a.id where (quarter = @secondQuarter and year = @secondYear and client_id = @client)) x) y) y2) z)B on A.manager = B.manager) OG UNION select 'Total' Total,sum(PreviousQuarter) PreviousQuarter, sum(InvestmentGainLoss) InvestmentGainLoss, sum(SelectedQuarter) SelectedQuarter,concat(cast(round(sum(WeightPreviousQuarter), 2) as numeric(18, 2)), ' %')WeightPreviousQuarter,concat(cast(round(sum(WeightSelectedQuarter), 2) as numeric(18, 2)), ' %') WeightSelectedQuarter from(select a.manager[Manager], a.TotalPerMan[PreviousQuarter], b.TotalPerMan-a.TotalPerMan[InvestmentGainLoss], b.TotalPerMan[SelectedQuarter],a.TotWeight[WeightPreviousQuarter],b.TotWeight[WeightSelectedQuarter] from(select *, ((TotalPerMan / TotalPerYearnQ) * 100) as TotWeight from(select *, sum(TotalPerMan) over(PARTITION BY quarter) as TotalPerYearnQ from(select distinct manager, Asset_manager_id, TotalPerMan, client_id, year, quarter from(select * from(select a.surname as manager, i.Asset_manager_id, sum(i.value) over(PARTITION by quarter, Asset_manager_id) as TotalPerMan, i.client_id, i.year, i.quarter from Investments i join asset_managers a on i.Asset_manager_id = a.id where (quarter = @firstQuarter and year = @firstYear and client_id = @client)) x) y) y2) z)A join(select*, ((TotalPerMan/ TotalPerYearnQ)*100) as TotWeight from(select *, sum(TotalPerMan) over(PARTITION BY quarter) as TotalPerYearnQ from(select distinct manager, Asset_manager_id, TotalPerMan, client_id, year, quarter from(select * from(select a.surname as manager, i.Asset_manager_id, sum(i.value) over(PARTITION by quarter, Asset_manager_id) as TotalPerMan, i.client_id, i.year, i.quarter from Investments i join asset_managers a on i.Asset_manager_id = a.id where (quarter = @secondQuarter and year = @secondYear and client_id = @client)) x) y) y2) z)B on A.manager = B.manager) TOT", conn);
           
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    grdAssetManagerPortifolio.DataSource = ds;
                    grdAssetManagerPortifolio.DataBind();
                }

            }
            else
            {
                grdAssetManagerPortifolio.DataSource = null;
                grdAssetManagerPortifolio.DataBind();
            }
            //int total = 0; ;
            //grdAssetManagerPortifolio.FooterRow.Cells[0].Text = "Total";
            //grdAssetManagerPortifolio.FooterRow.Cells[1].Font.Bold = true;
            //grdAssetManagerPortifolio.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            
            //getTotal();
        }
        
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
    }

    public void getTotals()
    {
        double sum = 0;
        foreach (GridViewRow row in grdAssetManagerPortifolio.Rows)
        {


            sum = 0;
            for (int b = 2; b < row.Cells.Count; b++)
            {
                if (row.Cells[b].Text == "" || row.Cells[b].Text == null || row.Cells[b].Text == "&nbsp;")
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

    public void getTotalExposure()
    {
        {
            double sum = 0;
            for (int i = 0; i < grdAssetManagerPortifolio.Rows.Count; ++i)
            {
               // sum += Convert.ToDouble(grdPortifolioSnapshot.Rows[i].Cells[2].Text);

                //MsgBox(grdQuarterToQuarter.Rows.Count.ToString(), this.Page, this);
            }
            grdAssetManagerPortifolio.Rows[grdAssetManagerPortifolio.Rows.Count-1].Cells[2].Text = "100%";
            //'txtsum.Text = sum.ToString();

        }

    }
    public void getTotalTarget()
    {
        {
            double sum = 0;
            for (int i = 0; i < grdAssetManagerPortifolio.Rows.Count; ++i)
            {
                //sum += Convert.ToDouble(grdPortifolioSnapshot.Rows[i].Cells[4].Text);

                //MsgBox(grdQuarterToQuarter.Rows.Count.ToString(), this.Page, this);
            }
            grdAssetManagerPortifolio.Rows[grdAssetManagerPortifolio.Rows.Count-1].Cells[4].Text = "100%";
            //'txtsum.Text = sum.ToString();

        }

    }

    public void getTotal()
    {
        {
            //double sum = 0;
            //for (int i = 0; i < grdAssetManagerPortifolio.Rows.Count; ++i)
            //{
            //    sum += Convert.ToDouble(grdAssetManagerPortifolio.Rows[i].Cells[1].Text);

            //    //MsgBox(grdQuarterToQuarter.Rows.Count.ToString(), this.Page, this);
            //}
            //grdAssetManagerPortifolio.Rows[grdAssetManagerPortifolio.Rows.Count-1].Cells[1].Text = sum.ToString();
            ////'txtsum.Text = sum.ToString();
            
            

        }

    }
    public void getTotalIpsecLimit()
    {
        {
            double sum = 0;
            for (int i = 0; i < grdAssetManagerPortifolio.Rows.Count; ++i)
            {
                sum += Convert.ToDouble(grdAssetManagerPortifolio.Rows[i].Cells[5].Text);

                //MsgBox(grdQuarterToQuarter.Rows.Count.ToString(), this.Page, this);
            }
            grdAssetManagerPortifolio.Rows[grdAssetManagerPortifolio.Rows.Count-1].Cells[5].Text = sum.ToString()+"%";
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
        string FileName = "AssetManagerPortfolio" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        grdAssetManagerPortifolio.GridLines = GridLines.Both;
        grdAssetManagerPortifolio.HeaderStyle.Font.Bold = true;
        grdAssetManagerPortifolio.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();

    }
   
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

        
        loadreport(txtID.Text, txtQuarter.Text, txtYear.Text,txtPrevQuarter.Text,txtPrevQuarterYear.Text);
     
        //Label3.Visible = true;
        Button1.Visible = true;
        //Label2.Visible = true;


    }
    public void loaddata()
    {
        txtID.Text= Request.QueryString["clientid"];
        txtQuarter.Text = Request.QueryString["quarter"];
        txtYear.Text = Request.QueryString["year"];

        if (txtQuarter.Text=="4")
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
            txtPrevQuarterYear.Text = (Int32.Parse(txtYear.Text.ToString())-1).ToString();
            txtPrevQuarter.Text = "4";
        }


        conn.Close();
        conn.Open();

        string Query = " select * from Clients where client_number='" + Request.QueryString["clientid"] + "'";
        SqlCommand cmd = new SqlCommand(Query, conn);
        SqlDataReader dr = cmd.ExecuteReader();

        if (dr.Read() == true)


        {            
            txtFirstName.Text = dr["name"].ToString();
            txtSpecialNotes.Text = dr["special_notes"].ToString();
        }
    }

    protected void txtQuarter_TextChanged(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loaddata();
        }
    }

    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loaddata();
        }
    }
}

   
        
        
                

    

