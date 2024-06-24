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
    public void MsgBox(String ex, Page pg, Object obj)
    {
        string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
        Type cstype = obj.GetType();
        ClientScriptManager cs = pg.ClientScript;
        cs.RegisterClientScriptBlock(cstype, s, s.ToString());
    }
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conpath"].ConnectionString);
    SqlConnection conprice = new SqlConnection(ConfigurationManager.ConnectionStrings["conprice"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          
            


            //Boolean user=  checkcustodian(txtFirstName.Text, txtSurname.Text, txtUsername.Text);
            //  if(user)
            //  {
            //      MsgBox("User Already Exists", this.Page, this);

            //  }
        }
        
    }
    public Boolean checkprice(  String surname)
    {
        Boolean existance = false;
        conn.Close();
        try
             
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM listed_equities where company='" + surname + "'   ", conn);
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
    protected void calendar1_SelectionChanged(object sender, EventArgs e)
    {
        txtDate.Text = calendar1.SelectedDate.ToShortDateString();
        calendar1.Visible = false;
        //getassetclasses(txtgenerator.Text);
    }
    protected void pickdate_Click(object sender, EventArgs e)
    {
        calendar1.Visible = true;
    }
    public Boolean addprice(String date)
    {
        conn.Close();
        //try
        {
            // MsgBox("Error: ", this.Page, this);
            conn.Open();
            //String id;
            //SqlCommand cmd = new SqlCommand("(select company_name,price_best,max(price_date) as pricedate from [testcds_ROUTER].[dbo].[prices] where convert(date,price_date) =convert(date,'"+date+"') group by company_name,price_best) ", conprice);
            //SqlCommand cmd = new SqlCommand("select tt. *, db.price_best from (select company_name, max(price_date) price_date from [testcds_ROUTER].[dbo].[prices] where convert(date,price_date) =convert(date,'" + date + "') group by company_name) tt left join [testcds_ROUTER].[dbo].[prices] db on tt.price_date = db.price_date and tt.company_name = db.company_name order by company_name asc", conprice);
            SqlCommand cmd = new SqlCommand(" DECLARE @Date date = '" + date + "' SELECT B.Company_name,B.price_close ,B.price_date FROM[testcds_ROUTER].[dbo].[prices] B JOIN (SELECT MAX(A.PriceID) AS LastID, A.Company_Name FROM[testcds_ROUTER].[dbo].[prices] A WHERE CONVERT(DATE, A.price_date)=@Date and A.Company_name not like '%1%' and Company_name not like '%1%' GROUP BY A.Company_Name) C ON B.PriceID=C.LastID ORDER BY B.Company_name", conprice);

SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    //id = dr["id"].ToString();





                    InsertPrice(dr["company_name"].ToString(), dr["price_best"].ToString(), dr["price_date"].ToString());




                    conn.Close();
                    conn.Dispose();
                    // var test = getaccrualinterest(vaulttype, numberofdays, total, cdsnumber);
                    

                    //difference = Convert.ToDateTime(DateTime.Now - datemodified);





                }
            }
        }


        return true;


    }
    public Boolean InsertPrice(String company,String vwap,String pricedate)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conpath"].ConnectionString);
        Boolean result = false;

        {
            SqlCommand cmd = new SqlCommand("IF NOT EXISTS(SELECT TOP 1 P.* FROM Prices P where P.Company='"+ company + "' and convert(date,P.PriceDate)='"+ pricedate + "') BEGIN insert into Prices(PriceDate,Company,ClosingPrice) Values ('" + pricedate+"','"+company+"','"+vwap+ "') END ELSE BEGIN UPDATE Prices set ClosingPrice='"+ vwap +"' where Company='" + company + "' and convert(date,PriceDate)='" + pricedate + "' END", conn);
            if ((conn.State == ConnectionState.Open))
                conn.Close();
            conn.Open();
            if (cmd.ExecuteNonQuery() == 1)
            {
                result = true;
            }
            conn.Close();

        }
        return result;
    }
    private void GetListData()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conpath"].ConnectionString);
        conn.Close();
        //try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select  company,closingprice  as [vwap] ,PriceDate from prices where  convert(date,PriceDate)=convert(date,'" + txtDate.Text+ "') order by company ", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
               
                    grdApps.DataSource = ds;
                    grdApps.DataBind();
                
            }
            else
            {
                grdApps.DataSource = null;
                grdApps.DataBind();
            }
        }
        //catch (Exception ex)
        //{
        //    conn.Close();
        //    MsgBox(ex.Message, this.Page, this);
        //}
    }
    public void linkDiscard(object sender, System.EventArgs e)
    {
        string idd = ((LinkButton)sender).CommandArgument;
      SqlCommand  cmd = new SqlCommand("Delete from listed_equities where Id='" + idd + "' ", conn);
        if ((conn.State == ConnectionState.Open))
            conn.Close();
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
        MsgBox("Delete Successful", this.Page, this);
        GetListData();


    }
    public void lnkedit(object sender, System.EventArgs e)
    {
        string idd = ((LinkButton)sender).CommandArgument;
        fetcheditadata(idd);

    }
    


    public void fetcheditadata(String id)
    {
        try
        {
            conn.Close();
            conn.Open();
            string Query = "SELECT * FROM listed_equities WHERE id='" + id.ToString() + "' ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)
            {
               
              
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }


    protected void Button1_Click(object sender, EventArgs e)
    {
           // addprice(txtDate.Text);
       
                GetListData();
                grdpanel.Visible = true;
                usersPanel.Visible = false;
            }
    protected void grdApps_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdApps.PageIndex = e.NewPageIndex;
        GetListData();
    }

    protected void BtnView_Click(object sender, EventArgs e)
    {
        try
        {
            GetListData();
        }
        catch (Exception ex)
        {

        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        addprice(txtDate.Text);
        MsgBox("Prices Successfully Fetched", this.Page, this);
        GetListData();
        Button3.Visible = false;
        // MsgBox("prices fetched", Page, this);

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
        
    }
    
    private void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "Prices" + DateTime.Now + ".xls";
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

    protected void grdApps_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
    
    
   
    
   

   
   
