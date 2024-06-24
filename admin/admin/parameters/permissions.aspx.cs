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
            loadusertypes();
            lbUsername.Text = "Logged in as" + " " + " " + (string)Session["username"] + "" + "" + (string)Session["role"];


            //Boolean user=  checkrole(txtFirstName.Text, txtSurname.Text, txtUsername.Text);
            //  if(user)
            //  {
            //      MsgBox("User Already Exists", this.Page, this);

            //  }
        }
        
    }
   
  
    private void GetListData()
    {
        conn.Close();
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select id,+'~/'+ parent +'/'+location+'/'+form as url ,modulename,location as Parent from modulenames order by id desc ", conn);
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
    public Boolean checkpermission(String modulename,String user)
    {
        Boolean existance = false;
        conn.Close();
        try

        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM module_permissions_users where moduleid='" + modulename + "' and userid= '" + user + "'   ", conn);
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














    public void loadusertypes()
    {
        string com = "Select * from roles";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        rdRole.DataSource = dt;
        rdRole.DataBind();
        rdRole.DataTextField = "role_name";
        rdRole.DataValueField = "ID";
        rdRole.DataBind();

    }
   

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("permissions.aspx");
    }

    protected void grdApps_SelectedIndexChanged1(object sender, EventArgs e)
    {

        String message;
        if (checkpermission(grdApps.SelectedRow.Cells[2].Text, rdRole.SelectedValue.ToString()))
        {

            message = "Permission already exists for the user" + rdRole.SelectedItem.Text;
            MsgBox(message, this.Page, this);
        }
        else
        {
            String parent = rdRole.SelectedValue;
            if(rdRole.SelectedItem.Text=="admin")
            {

            }
            if (checkparent(grdApps.SelectedRow.Cells[4].Text, rdRole.SelectedValue.ToString(), parent))
            {
                //MsgBox("Permission parent exists", this.Page, this);
            }
            else
            {
                SqlCommand command = new SqlCommand("insert into module_permissions_users(modulename,userid) values('" + grdApps.SelectedRow.Cells[4].Text + "','" + rdRole.SelectedValue.ToString() + "')", conn);
                if ((conn.State == ConnectionState.Open))
                    conn.Close();
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
            MsgBox("Permission added successfully", this.Page, this);

            SqlCommand cmd = new SqlCommand("insert into module_permissions_users (moduleid,userid,modulename,parent) values('" + grdApps.SelectedRow.Cells[2].Text + "','" + rdRole.SelectedValue.ToString() + "','" + grdApps.SelectedRow.Cells[3].Text  + "',(select  parent_id from [module_permissions_users] where modulename='" + grdApps.SelectedRow.Cells[4].Text + "' and userid='"+rdRole.SelectedValue +"'))", conn);
            if ((conn.State == ConnectionState.Open))
                conn.Close();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
           
        }
    }
    
    public Boolean checkparent(String modulename,String user ,String parent)
    {
        Boolean existance = false;
        conn.Close();
        try

        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM module_permissions_users where  modulename= '" + modulename + "' and userid= '" + user + "' and parent is   null ", conn);
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
}