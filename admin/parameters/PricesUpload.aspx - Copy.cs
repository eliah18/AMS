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
using System.Data.OleDb;
using System.Data.Common;

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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetListData();



            //Boolean user=  checkcustodian(txtFirstName.Text, txtSurname.Text, txtUsername.Text);
            //  if(user)
            //  {
            //      MsgBox("User Already Exists", this.Page, this);

            //  }
        }


    }
    public Boolean checkPrices(String pricedate)
    {
        Boolean existance = false;
        conn.Close();
        try

        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM prices where PriceDate='" + pricedate + "'   ", conn);
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

    public Boolean checkPriceByCounter(String pricedate,String Counter)
    {
        Boolean existance = false;
        conn.Close();
        try

        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM prices where PriceDate='" + pricedate + "'  and Company='"+Counter+"'  ", conn);
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
    public Boolean addcustodian(String surname, String contactdetails, String address)
    {
        Boolean added = false;
        try
        {
            conn.Close();
            conn.Open();
            String query = "INSERT INTO custodians(name,contact_details,address) values('" + surname + "','" + contactdetails + "','" + address + "')";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteReader(CommandBehavior.CloseConnection);
            added = true;
        }
        catch (Exception ex)
        {
            MsgBox("Error: " + ex.Message, this.Page, this);

        }
        return added;


    }
    private void GetListData()
    {
        conn.Close();
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select id,PriceDate,Company,convert(Double Precision,ClosingPrice) as [VWAP] from prices where convert(date,PriceDate)= convert(date,'" + txtDate.Text + "') order by id desc ", conn);
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
            }
            else
            {
                grdApps.DataSource = null;
                grdApps.DataBind();
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
    }
    public void linkDiscard(object sender, System.EventArgs e)
    {
        string idd = ((LinkButton)sender).CommandArgument;
        SqlCommand cmd = new SqlCommand("Delete from custodians where Id='" + idd + "' ", conn);
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
            string Query = "SELECT * FROM prices WHERE id='" + id.ToString() + "' ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)
            {


                usersPanel.Visible = false;
                grdpanel.Visible = true;
                txtPriceDate.Text = txtDate.Text;
                txtPriceId.Text = id.ToString();
                rdCounters.Visible = false;
                txtCounter.Visible = true;
               txtCounter.Text= dr["Company"].ToString();
                txtPrice.Text= dr["ClosingPrice"].ToString();

                Button1.Visible = false;
                Button2.Visible = false;
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }
    bool ReturnValue()
    {
        return false;
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        if (checkPrices(txtDate.Text))
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                DeleteExistingPrices();
                UploadData();
               
            }
            else
            {
                return;
            }


           

        }

        else
        {
            UploadData();
        }


    }
    protected void grdApps_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdApps.PageIndex = e.NewPageIndex;
        GetListData();
    }





    protected void LinkButton1_Click(object sender, EventArgs e)
    {

        if (txtDate.Text == "")
        {
            MsgBox("Please Select Date First", this.Page, this);
            return;
        }
       
        grdpanel.Visible = true;
        usersPanel.Visible = false;
        txtPriceDate.Text = txtDate.Text;
        loadcounters();
        rdCounters.Visible = true;
        txtCounter.Visible = false;
        txtCounter.Text = "";

        Button2.Visible = false;
        Button1.Visible = true;
    }
    private void loadcounters()
    {


        conn.Close();
        conn.Open();
        string com = "select * from para_company where company not in (select company  from prices where convert(date,pricedate)=convert(date,'"+txtPriceDate.Text+"'))";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        rdCounters.DataSource = dt;
        rdCounters.DataBind();
        rdCounters.DataTextField = "fnam";
        rdCounters.DataValueField = "company";
        rdCounters.DataBind();
        rdCounters.Items.Insert(0, new ListItem("Select Company", "0"));




    }

    protected void Button2_Click(object sender, EventArgs e)
    {

    }


    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("PricesUpload.aspx");
    }
    protected void Unnamed2_Click(object sender, EventArgs e)
    {
        //calendar1.Visible = true;

    }
    protected void calendar1_SelectionChanged(object sender, EventArgs e)
    {
        txtDate.Text = calendar1.SelectedDate.ToShortDateString();
        calendar1.Visible = false;
        GetListData();
        grdpanel.Visible = false;
        grdApps.Visible = true;
        
        //getassetclasses(txtgenerator.Text);
    }
    protected void pickdate_Click(object sender, EventArgs e)
    {
        calendar1.Visible = true;
    }
    public void UploadData()
    {


        DataTable dtExcel = new DataTable();
        if (FileUpload1.HasFile)

        {
            int count = 1;
            string excelconnectionString = "";

            string fileName2 = Path.GetFileName(FileUpload1.PostedFile.FileName);


            string fileLocation = Server.MapPath("~/uploads/uploads_" + DateTime.Now.ToString("ddMMyyyymmsss") + fileName2);
            FileUpload1.SaveAs(fileLocation);
            string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            if (fileExtension == ".xls")
                excelconnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=No;IMEX=1\"";
            else if (fileExtension == ".xlsx")
                excelconnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";
            else
            {
                MsgBox("Inavalid File", this.Page, this);
                return;
            }
            // Create OleDB Connection and OleDb Command

            OleDbConnection con = new OleDbConnection(excelconnectionString);
            string query = "Select * from [Sheet1$]  ";
            OleDbDataAdapter data = new OleDbDataAdapter(query, con);
            data.Fill(dtExcel);
            for (int i = 1; i < dtExcel.Rows.Count; i++)
            {

                {
                    conn.Close();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert ignore into prices(Company,ClosingPrice,PriceDate) values('" + dtExcel.Rows[i][1] + "', '" + dtExcel.Rows[i][2] + "', '" + txtDate.Text + "') ", conn);
                    if ((conn.State == ConnectionState.Open))
                    {
                        count = count + cmd.ExecuteNonQuery();
                    }
                  
                }

            }
            if (count == dtExcel.Rows.Count)
            {
                MsgBox("Upload Success", this.Page, this);
                GetListData();
                grdpanel.Visible = false;
                usersPanel.Visible = true;
            }
            else
            {
                MsgBox("Not Done", this.Page, this);

            }
        }
        else
        {

            MsgBox("Please Browser and Select File", this.Page, this);
        }

        Button3.Visible = false;
    }


    public Boolean DeleteExistingPrices()
    {
        bool existance = false;

        SqlCommand cmd = new SqlCommand("Delete from Prices where convert(date,PriceDate)= convert(date,'" + txtDate.Text + "') ", conn);
        if ((conn.State == ConnectionState.Open))
            conn.Close();
        conn.Open();
        if (cmd.ExecuteNonQuery() == 1)
        {
            existance = true;
        }
        conn.Close();




        return existance;
    }
    public Boolean AddNewPrice()
    {
        bool existance = false;

        SqlCommand cmd = new SqlCommand("insert into prices(Company,ClosingPrice,PriceDate) values('" + rdCounters.SelectedItem.Value + "', '" + txtPrice.Text + "', '" + txtPriceDate.Text + "') ", conn);
        if ((conn.State == ConnectionState.Open))
            conn.Close();
        conn.Open();
        if (cmd.ExecuteNonQuery() == 1)
        {
            existance = true;
        }
        conn.Close();




        return existance;
    }





    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        GetListData();
        grdpanel.Visible = false;
    }


    protected void Button6_Click(object sender, EventArgs e)
    {
        if (txtCounter.Text == "")
        {
            if (checkPriceByCounter(txtPriceDate.Text, rdCounters.SelectedItem.Value))
            {
                MsgBox("Price Already Added", this.Page, this);
                return;
            }
            if ( AddNewPrice())
            {
                MsgBox("Price Successfully Added", this.Page, this);
                grdpanel.Visible = false;
                usersPanel.Visible = true;
                GetListData();
                grdApps.Visible = true;

            }
        }
        else
        {
            if (updatePrice())
            {
                MsgBox("Price Successfully Updated", this.Page, this);
                grdpanel.Visible = false;
                usersPanel.Visible = true;
                GetListData();
                grdApps.Visible = true;

            }
        }
    }
    public Boolean updatePrice()
    {
        bool existance = false;

        SqlCommand cmd = new SqlCommand("update Prices  set ClosingPrice='"+txtPrice.Text+"'  where id='"+txtPriceId.Text+"' ", conn);
        if ((conn.State == ConnectionState.Open))
            conn.Close();
        conn.Open();
        if (cmd.ExecuteNonQuery() == 1)
        {
            existance = true;
        }
        conn.Close();




        return existance;
    }


}