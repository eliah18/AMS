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
            lbUsername.Text = "Logged in as" + " " + " " + (string)Session["username"] + "" + "" + (string)Session["role"];


            //Boolean user=  checkuser(txtFirstName.Text, txtSurname.Text, txtUsername.Text);
            //  if(user)
            //  {
            //      MsgBox("User Already Exists", this.Page, this);

            //  }
        }
        
    }
    public Boolean checkuser( String firstname, String surname)
    {
        Boolean existance = false;
        conn.Close();
        try
             
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM asset_managers where surname='" + firstname + "'  ", conn);
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
    public Boolean Adduser(String firstname,String surname,String benchmark,String strategy,String philosophy,String contactdetails,String address)
    {
        Boolean added = false;
        try
        {
            conn.Close();
            conn.Open();
            String query = "INSERT INTO asset_managers(name,surname,benchmark,strategy,philosophy,contact_details,address) values('" + firstname + "','" + surname + "','" + benchmark + "','" + strategy + "','" + philosophy + "','" + contactdetails + "','" + address + "')";
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
            String query = "DECLARE @cols NVARCHAR(MAX), @query NVARCHAR(MAX);SET @cols = STUFF((SELECT DISTINCT ','+QUOTENAME(c.[surname])  FROM asset_managers c left outer join client_assetmanager_relations b on c.id=b.assetmanager_id where b.client_id='1' FOR XML PATH(''), TYPE ).value('.', 'nvarchar(max)'), 1, 1, '');SET @query = 'SELECT [asset_name], '+@cols+'from (SELECT [id],[asset_name], 0 AS [amount],'''' AS [category]FROM [dbo].[assets_class])x pivot (max(amount) for category in ('+@cols+')) p';EXECUTE (@query);";
            SqlCommand cmd = new SqlCommand(query , conn);
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
      SqlCommand  cmd = new SqlCommand("Delete from asset_managers where Id='" + idd + "' ", conn);
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
            string Query = "SELECT * FROM asset_managers WHERE id='" + id.ToString() + "' ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)
            {
                txtFirstName.Text = dr["name"].ToString();
                txtSurname.Text= dr["surname"].ToString();
                txtBenchmark.Text = dr["benchmark"].ToString();
                txtID.Text= dr["id"].ToString();
                txtStrategy.Text= dr["strategy"].ToString();
                txtPhilosophy.Text= dr["philosophy"].ToString();
                txtContactDetails.Text = dr["contact_details"].ToString();
                txtAddress.Text = dr["address"].ToString();
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
        Boolean user = checkuser(txtFirstName.Text, txtSurname.Text);
        if (user)
        {
            MsgBox("Manager Already Exists", this.Page, this);

        }
        else
        {
            Boolean add = Adduser(txtFirstName.Text, txtSurname.Text,txtBenchmark.Text,txtStrategy.Text,txtPhilosophy.Text,txtContactDetails.Text,txtAddress.Text);
            if (add)
            {
                MsgBox("Manager  successfully added", this.Page, this);
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
        txtStrategy.Text = "";
        txtBenchmark.Text = "";
        txtAddress.Text = "";
        txtPhilosophy.Text = "";
        txtContactDetails.Text = "";
       
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
            SqlCommand cmd = new SqlCommand("update asset_managers set name='" + txtFirstName.Text + "' ,surname='" + txtSurname.Text + "',benchmark='" + txtBenchmark.Text + "',strategy='" + txtStrategy.Text + "', philosophy='" + txtPhilosophy.Text + "',contact_details='" + txtContactDetails.Text + "' ,address='" + txtAddress.Text + "' where id= '" + id + "'", conn);
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
        Response.Redirect("Assetmanagers.aspx");
    }

    protected void grdApps_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdApps.PageIndex = e.NewPageIndex;
        GetListData();
    }
}