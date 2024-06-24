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
           

            //Boolean user=  checkasset(txtFirstName.Text, txtSurname.Text, txtUsername.Text);
            //  if(user)
            //  {
            //      MsgBox("User Already Exists", this.Page, this);

            //  }
        }
        
    }
    public Boolean checkasset( String firstname)
    {
        Boolean existance = false;
        conn.Close();
        try
             
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM assets_class where asset_name='" + firstname + "'   ", conn);
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
    public Boolean addasset(String firstname )
    {
        Boolean added = false;
        try
        {
            conn.Close();
            conn.Open();
            String query = "INSERT INTO assets_class(asset_name ) values('" + firstname + "')";
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
            SqlCommand cmd = new SqlCommand("select id,asset_name as Name from assets_class order by id desc ", conn);
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
      SqlCommand  cmd = new SqlCommand("Delete from assets_class where Id='" + idd + "' ", conn);
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
        //try
        //{
            conn.Close();
            conn.Open();
            string Query = "SELECT * FROM assets_class WHERE id='" + id.ToString() + "' ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)
            {
                txtFirstName.Text = dr["asset_name"].ToString();
               
                txtID.Text= dr["id"].ToString();
               //s rdType.Text = dr["type"].ToString();

                usersPanel.Visible = true;
                grdpanel.Visible = false;
                Button1.Visible = false;
                Button2.Visible = true;
            }
        //}
        //catch (Exception ex)
        //{
        //    conn.Close();
        //    MsgBox(ex.Message, this.Page, this);
        //}

    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        Button3.Visible = false;
        Boolean user = checkasset(txtFirstName.Text);
        if (user)
        {
            MsgBox("Asset Class Already Exists", this.Page, this);

        }
        else
        {
            Boolean add = addasset(txtFirstName.Text );
            if (add)
            {
                MsgBox("Asset Class  successfully added", this.Page, this);
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
        
        Button2.Visible = false;
        Button1.Visible = true;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        String id = txtID.Text.ToString();
        Boolean edited = edituser( id);
        if (edited)
        {
            MsgBox("Update Successful", this.Page, this);
            GetListData();
            grdpanel.Visible = true;
            usersPanel.Visible = false;
        }
    }
    public Boolean edituser(string  id)
    {

        {
            SqlCommand cmd = new SqlCommand("update assets_class set asset_name='" + txtFirstName.Text + "'   where id= '" + id + "'", conn);
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
        Response.Redirect("asset_classtypes.aspx");
    }
}