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
    public void loadreport(String client,String selectedQuarter, String selectedYear)
    {
        //MsgBox("hie", this.Page, this);
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("declare @Quarter nvarchar(50)='" + selectedQuarter + "'; declare @Year nvarchar(50)='" + selectedYear + "'; declare @client nvarchar(50)='" + client + "';select surname as Portfolio,Unprescibed as [Total Portfolio],concat((Unprescibed/TotalUnprescibed)*100, ' %') as [Portfolio Percentage],Prescibed as [Prescribed Assets],concat((Prescibed/Unprescibed)*100, ' %') as [Prescribed Assets Percentage] from (select *,sum(Unprescibed) over (PARTITION BY client_id,year,quarter) TotalUnprescibed from (select unpre.surname,unpre.client_id,unpre.year,unpre.quarter,case when unpre.Unprescibed is null then 0 else unpre.Unprescibed end as Unprescibed,case when pre.Prescibed is null then 0 else pre.Prescibed end as Prescibed from (select distinct surname,client_id,year,quarter,Unprescibed from (select *,sum(value) over (PARTITION BY surname) as Unprescibed from (select am.surname,ac.asset_name,lac.value,lac.type,lac.client_id,lac.year,lac.quarter from linked_assetclasses lac left join asset_managers am on am.id = lac.assetmanager join assets_class ac on ac.id=lac.assetclass where lac.type<>'Prescribed' and quarter=@Quarter and year=@Year and client_id=@client) abc) qwe)unpre left join (select distinct surname,type,client_id,year,quarter,Prescibed from (select *,sum(value) over (PARTITION BY surname) as Prescibed from (select am.surname,ac.asset_name,lac.value,lac.type,lac.client_id,lac.year,lac.quarter from linked_assetclasses lac left join asset_managers am on am.id = lac.assetmanager join assets_class ac on ac.id=lac.assetclass where lac.type='Prescribed' and quarter=@Quarter and year=@Year and client_id=@client) abc) rty )pre on unpre.surname=pre.surname) tot ) tot2 UNION select 'Total', sum(TotalPortfolio), concat(sum(PortfolioPercentage), '%'), sum(PrescribedAssets), concat(sum(PrescribedAssetsPercentage), '%') from(select surname, Unprescibed as TotalPortfolio, (Unprescibed / TotalUnprescibed) * 100 as PortfolioPercentage, Prescibed as PrescribedAssets, (Prescibed / Unprescibed) * 100 as PrescribedAssetsPercentage from(select *, sum(Unprescibed) over(PARTITION BY client_id, year, quarter) TotalUnprescibed from(select unpre.surname, unpre.client_id, unpre.year, unpre.quarter,case when unpre.Unprescibed is null then 0 else unpre.Unprescibed end as Unprescibed,case when pre.Prescibed is null then 0 else pre.Prescibed end as Prescibed from(select distinct surname, client_id, year, quarter, Unprescibed from(select *, sum(value) over(PARTITION BY surname) as Unprescibed from(select am.surname, ac.asset_name, lac.value, lac.type, lac.client_id, lac.year, lac.quarter from linked_assetclasses lac left join asset_managers am on am.id = lac.assetmanager join assets_class ac on ac.id = lac.assetclass where lac.type <> 'Prescribed' and quarter = @Quarter and year = @Year and client_id = @client) abc) qwe)unpre left join(select distinct surname, type, client_id, year, quarter, Prescibed from(select *, sum(value) over(PARTITION BY surname) as Prescibed from(select am.surname, ac.asset_name, lac.value, lac.type, lac.client_id, lac.year, lac.quarter from linked_assetclasses lac left join asset_managers am on am.id = lac.assetmanager join assets_class ac on ac.id = lac.assetclass where lac.type = 'Prescribed' and quarter = @Quarter and year = @Year and client_id = @client) abc) rty)pre on unpre.surname = pre.surname) tot ) tot2 ) tot3", conn);

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
        string FileName = "PrescribedAssetsRequirement" + DateTime.Now + ".xls";
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
        loadreport(txtID.Text, txtQuarter.Text, txtYear.Text);
     
        //Label3.Visible = true;
        Button1.Visible = true;
        //Label2.Visible = true;
    }
    public void loaddata()
    {
        txtID.Text= Request.QueryString["clientid"];
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

   
        
        
                

    

