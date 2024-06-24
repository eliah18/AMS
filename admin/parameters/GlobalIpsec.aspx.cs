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
            loadAssetClasses();


            //Boolean user=  checkcustodian(txtFirstName.Text, cmbAssetClass.Text, txtUsername.Text);
            //  if(user)
            //  {
            //      MsgBox("User Already Exists", this.Page, this);

            //  }
        }
        
    }
    public Boolean checkcustodian(  String surname)
    {
        Boolean existance = false;
        conn.Close();
        try
             
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) from IpsecGlobalRegulatory  where AssetClass='" + surname + "'   ", conn);
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
    public Boolean addcustodian(String surname,String value)
    {
        Boolean added = false;
        try
        {
            conn.Close();
            conn.Open();
            String query = "INSERT INTO IpsecGlobalRegulatory(AssetClass,IpsecRegulatory) values('" + surname + "','" + value + "')";
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
            SqlCommand cmd = new SqlCommand("select id,AssetClass, IpsecRegulatory as Value from IpsecGlobalRegulatory order by id desc ", conn);
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
      SqlCommand  cmd = new SqlCommand("Delete from IpsecGlobalRegulatory where Id='" + idd + "' ", conn);
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
            string Query = "SELECT * FROM IpsecGlobalRegulatory WHERE id='" + id.ToString() + "' ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)
            {
               
                cmbAssetClass.Text= dr["AssetClass"].ToString();
                 txtID.Text= dr["id"].ToString();
               
                txtValue.Text = dr["IpsecRegulatory"].ToString();
                usersPanel.Visible = true;
                grdpanel.Visible = false;
                Button1.Visible = false;
                Button2.Visible = true;
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
        Boolean user = checkcustodian( cmbAssetClass.Text);
        if (user)
        {
            MsgBox("Asset Class Ipsec  Already Exists", this.Page, this);
            

        }
        else
        {
            Boolean add = addcustodian( cmbAssetClass.Text,txtValue.Text);
            if (add)
            {
                MsgBox("Global Ipsec successfully added", this.Page, this);
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
        
       
      
        txtValue.Text = "";
        Button2.Visible = false;
        Button1.Visible = true;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        String id = txtID.Text.ToString();
        Boolean edited = editcustodian( id);
        if (edited)
        {
            MsgBox("Update Successful", this.Page, this);
            GetListData();
            grdpanel.Visible = true;
            usersPanel.Visible = false;
        }
    }
    public Boolean editcustodian(string  id)
    {

        {
            SqlCommand cmd = new SqlCommand("update IpsecGlobalRegulatory set AssetClass='" + cmbAssetClass.Text + "',IpsecRegulatory='" + txtValue.Text + "' where id= '" + id + "'", conn);
            if ((conn.State == ConnectionState.Open))
                conn.Close();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            
        }
        return true;
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("Globallpsec.aspx");
    }

    protected void grdApps_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdApps.PageIndex = e.NewPageIndex;
        GetListData();
    }
    public void loadAssetClasses()
    {
        conn.Close();
        conn.Open();
        string com = " select * from assets_class where active='1'";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        cmbAssetClass.DataSource = dt;
        cmbAssetClass.DataBind();
        cmbAssetClass.DataTextField = "asset_name";
        cmbAssetClass.DataValueField = "asset_name";
        cmbAssetClass.DataBind();
        cmbAssetClass.Items.Insert(0, new ListItem("Select Asset Class", "0"));



    }
}