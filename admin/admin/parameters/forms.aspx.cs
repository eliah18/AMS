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
            loadfolders();
            lbUsername.Text = "Logged in as" + " " + " " + (string)Session["username"] + "" + "" + (string)Session["role"];





            //Boolean user=  checkpermission(txtModuleName.Text, txtForm.Text, txtUsername.Text);
            //  if(user)
            //  {
            //      MsgBox("User Already Exists", this.Page, this);

            //  }
        }
        
    }
    public Boolean checkpermission( String modulename)
    {
        Boolean existance = false;
        conn.Close();
        try
             
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM modulenames where modulename='" + modulename + "'    ", conn);
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
    public Boolean addpermission(String modulename,String form,String location,String parent)
    {
        Boolean added = false;
        try
        {
            conn.Close();
            conn.Open();
            String query = "INSERT INTO modulenames(modulename,form,location,parent) values('" + modulename + "','" + form + "','" + location + "','" + parent + "')";
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
            SqlCommand cmd = new SqlCommand("select id,modulename,form,location  from modulenames order by id desc ", conn);
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
      SqlCommand  cmd = new SqlCommand("Delete from modulenames where Id='" + idd + "' ", conn);
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
            string Query = "SELECT * FROM modulenames WHERE id='" + id.ToString() + "' ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)
            {
                txtModuleName.Text = dr["modulename"].ToString();
                txtForm.Text= dr["form"].ToString();
                rdLocation.SelectedItem.Text = dr["location"].ToString();
               
                txtID.Text= dr["id"].ToString();
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
        Boolean user = checkpermission(txtModuleName.Text);
        if (user)
        {
            MsgBox("Form Already Exists", this.Page, this);

        }
       
        else if (locationvalidation())
        {

            MsgBox("Select Location", this.Page, this);
        }

        else
        {
            String parent="admin";
            
            Boolean add = addpermission(txtModuleName.Text, txtForm.Text,rdLocation.SelectedItem.Text,parent);
            if (add)
            {
                MsgBox("Form  successfully added", this.Page, this);
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
        txtModuleName.Text = "";
        txtForm.Text = "";
       
       
        Button2.Visible = false;
        Button1.Visible = true;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        String id = txtID.Text.ToString();

        Boolean edited = editpermission( id);
        if (edited)
        {
            MsgBox("Update Successful", this.Page, this);
            GetListData();
            grdpanel.Visible = true;
            usersPanel.Visible = false;
        }
    }
    public Boolean editpermission(string  id)
    {

        Boolean edita = false;
        
         if (locationvalidation())
        {

            MsgBox("Select Location", this.Page, this);
        }
         else

        {
            SqlCommand cmd = new SqlCommand("update modulenames set modulename='" + txtModuleName.Text + "' ,form='" + txtForm.Text + "', location='" + rdLocation.SelectedItem.ToString() + "' where id= '" + id + "'", conn);
            if ((conn.State == ConnectionState.Open))
                conn.Close();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            edita = true;
        }
        return edita;
    }
    public bool locationvalidation()
    {
        Boolean status = false;
        if (rdLocation.SelectedItem.Text == "")
        {
            status = true;
        }
        return status;
    }
    

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("forms.aspx");
    }
    public void loadfolders()
    {
        string com = "Select * from folders";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        rdLocation.DataSource = dt;
        rdLocation.DataBind();
        rdLocation.DataTextField = "folder_name";
        rdLocation.DataValueField = "ID";
        rdLocation.DataBind();

    }
}