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
            loadcountries();
            loadassetmanagers();
            loadcustodians();
            cmbCustodian.SelectedItem.Text = "";
            cmbCountry.SelectedItem.Text = "";
            cmbAssetManager.SelectedItem.Text = "";


            //Boolean user=  checkinvestor(txtFirstName.Text, txtSurname.Text, txtUsername.Text);
            //  if(user)
            //  {
            //      MsgBox("User Already Exists", this.Page, this);

            //  }
        }

    }
    public Boolean checkinvestor(String firstname, String surname)
    {
        Boolean existance = false;
        conn.Close();
        try

        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM asset_managers where name='" + firstname + "' and  surname='" + surname + "'  ", conn);
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
    public Boolean addinvestor(String firstname, String surname, String nationaid, String address, String City, String Country, String ratereturn, String replacementratio, String assetmanager, String assetmanagerbenchmark, String custodian)
    {
        Boolean added = false;
        try
        {
            conn.Close();
            conn.Open();
            String query = "INSERT INTO investors(name,surname,Address,City,national_id,country,rate_of_return,replacement_ratio,asset_manager_id,asset_manager_benchmark,custodian_id) values('" + firstname + "','" + surname + "','" + address + "','" + City + "','" + nationaid + "','" + Country + "','" + ratereturn + "','" + replacementratio + "','" + assetmanager + "','" + assetmanagerbenchmark + "','" + custodian + "')";
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
    private void GetListData()
    {
        conn.Close();
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select id,name,surname,rate_of_return,replacement_ratio from investors order by id desc ", conn);
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
        SqlCommand cmd = new SqlCommand("Delete from investors where Id='" + idd + "' ", conn);
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
            string Query = "SELECT * FROM investors WHERE id='" + id.ToString() + "' ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)
            {
                txtFirstName.Text = dr["name"].ToString();
                txtSurname.Text = dr["surname"].ToString();
                txtID.Text = dr["id"].ToString();
                txtCity.Text = dr["city"].ToString();
                txtAddress.Text = dr["address"].ToString();
                txtNationalid.Text = dr["national_id"].ToString();
                txtReturn.Text = dr["rate_of_return"].ToString();
                cmbCountry.SelectedItem.Text= dr["country"].ToString();
                cmbCustodian.SelectedItem.Text = dr["custodian_id"].ToString();

                usersPanel.Visible = true;
                grdpanel.Visible = false;
                Button1.Visible = false;
                Button2.Visible = true;
                Button3.Visible = true;
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
        Button3.Visible = false;
        Boolean user = checkinvestor(txtFirstName.Text, txtSurname.Text);
        

        if (user)
        {
            MsgBox("Investor Already Exists", this.Page, this);

        }
        else if (assetmanagervalidation()){

            MsgBox("Select Asset Manager", this.Page, this);
        }
        else if (custodianvalidation())
        {

            MsgBox("Select Custodian", this.Page, this);
        }
        else if (countryvalidation())
        {

            MsgBox("Select Country", this.Page, this);
        }

        else
        {
            Boolean add = addinvestor(txtFirstName.Text, txtSurname.Text, txtAddress.Text, txtCity.Text, txtNationalid.Text, cmbCountry.SelectedItem.Text, txtReturn.Text, txtReplacementRatio.Text, cmbAssetManager.SelectedValue, txtAssetManagerBenchmark.Text, cmbCustodian.SelectedItem.Text);
            if (add)
            {
                MsgBox("Investor successfully added", this.Page, this);
                GetListData();
                grdpanel.Visible = true;
                usersPanel.Visible = false;
            }
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        grdpanel.Visible = false;
        usersPanel.Visible = true;
        txtFirstName.Text = "";
        txtSurname.Text = "";
        Button2.Visible = false;
        Button1.Visible = true;
        Button3.Visible = true;
        txtID.Text = "";
        txtCity.Text = "";
        txtAddress.Text = "";
        txtNationalid.Text = "";
        txtReturn.Text = "";
        cmbCountry.SelectedItem.Text = "";
        cmbCustodian.SelectedItem.Text = "";


    }

    protected void Button2_Click(object sender, EventArgs e)

    {
        String id = txtID.Text.ToString();
        Boolean edited = editinvestor(id);
        if (edited)
        {
            MsgBox("Update Successful", this.Page, this);
            GetListData();
            grdpanel.Visible = true;
            usersPanel.Visible = false;
        }
    }
    public Boolean editinvestor(string id)
    {
        Button3.Visible = false;
        Boolean edita = false;

        if (assetmanagervalidation())
        {

            MsgBox("Select Asset Manager", this.Page, this);
        }
        else if (custodianvalidation())
        {

            MsgBox("Select Custodian", this.Page, this);
        }
        else if (countryvalidation())
        {

            MsgBox("Select Country", this.Page, this);
        }
        else

        {
            SqlCommand cmd = new SqlCommand("update investors set name='" + txtFirstName.Text + "' ,surname='" + txtSurname.Text + "',address='" + txtAddress.Text + "' ,City='" + txtCity.Text + "',national_id='" + txtNationalid.Text + "',country='" + txtNationalid.Text + "',rate_of_return='" + txtReturn.Text + "',replacement_ratio='" + txtReplacementRatio.Text + "',asset_manager_id='" + cmbAssetManager.SelectedItem.Text + "',asset_manager_benchmark ='" + txtAssetManagerBenchmark.Text + "',custodian_id='" + cmbCustodian.SelectedItem.Text + "' where id= '" + id + "'", conn);
            if ((conn.State == ConnectionState.Open))
                conn.Close();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            edita = true;

        }
        return edita;
    }
    public void loadcountries()
    {
        string com = "Select * from para_country";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        cmbCountry.DataSource = dt;
        cmbCountry.DataBind();
        cmbCountry.DataTextField = "country";
        cmbCountry.DataValueField = "fnam";
        cmbCountry.DataBind();


    }
    public void loadassetmanagers()
    {
        string com = "select  (name + '-' + surname ) as 'productname-price',id  from asset_managers";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        cmbAssetManager.DataSource = dt;
        cmbAssetManager.DataBind();
        cmbAssetManager.DataTextField = "productname-price";

        cmbAssetManager.DataValueField = "ID";
        cmbAssetManager.DataBind();


    }
    public void loadcustodians()
    {
        string com = "select  id,name from custodians";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        cmbCustodian.DataSource = dt;
        cmbCustodian.DataBind();
        cmbCustodian.DataTextField = "name";

        cmbCustodian.DataValueField = "id";
        cmbCustodian.DataBind();


    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("investors.aspx");
    }

    protected void cmbAssetManager_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        
            try
            {
                conn.Close();
                conn.Open();
                string Query = "SELECT * FROM asset_managers WHERE id='" + cmbAssetManager.SelectedItem.Value + "' ";
                SqlCommand cmd = new SqlCommand(Query, conn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read() == true)
                {
                    txtAssetManagerBenchmark.Text = dr["benchmark"].ToString();



                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MsgBox(ex.Message, this.Page, this);
            }
        }
    public bool assetmanagervalidation()
    {
        Boolean status = false;
        if (cmbAssetManager.SelectedItem.Text=="")
        {
            status = true;
        }
        return status;
    }
    public bool custodianvalidation()
    {
        Boolean status = false;
        if (cmbCustodian.SelectedItem.Text == "")
        {
            status = true;
        }
        return status;
    }
    public bool countryvalidation()
    {
        Boolean status = false;
        if (cmbCountry.SelectedItem.Text == "")
        {
            status = true;
        }
        return status;
    }

}
