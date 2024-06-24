using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class logins : System.Web.UI.Page
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
             
        }

    }
    public Boolean  AutheticateUser(String username, String password)
    {
        Boolean existance = false;
        try
        {
            conn.Close();
            conn.Open();
            string Query = "SELECT * FROM SystemUsers WHERE username='" +username  + "' and password= '" + password + "'";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)
            {
                existance = true;
               Session["username"]= dr["username"].ToString();
                Session["role"] = dr["role"].ToString();
                txtusertype.Text = dr["role_id"].ToString();
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
        return existance;

    }











    protected void signin_Click(object sender, EventArgs e)
    {
        if (AutheticateUser(txtUsername.Text, txtPassword.Text))
        {
            //MsgBox("user exists", this.Page, this);
            int id = int.Parse(txtusertype.Text);
            Session["roleid"] = id;
            Session["user"] = txtUsername.Text;
            Session["sum"] = 0;
            Session["clientid"] = 0;
            if (id == 1)
            {
                Response.Redirect("~/admin/parameters/MainDashboard.aspx");
            }
            else
            {
                Response.Redirect("~/admin/user/usersHome.aspx");
            }
           
            
        }
    }
}