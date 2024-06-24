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

public partial class admin_ClientStatus_ClientFundsLinked : System.Web.UI.Page
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
           
            lbUsername.Text = "Logged in as" + " " + " " + (string)Session["username"] + "" + "" + (string)Session["role"];
            String name= Request.QueryString["name"];
            fetcheditadata(name);

            //Boolean user=  checkuser(txtFirstName.Text, txtSurname.Text, txtUsername.Text);
            //  if(user)
            //  {
            //      MsgBox("User Already Exists", this.Page, this);

            //  }
        }
        
    }
    
  
    
   
    


    public void fetcheditadata(String name)
    {
        try
        {
            conn.Close();
            conn.Open();
            string Query = "SELECT * FROM asset_managers WHERE surname ='" + name.ToString() + "' ";
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
               
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }


   
}