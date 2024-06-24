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
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

public partial class admin_parameters_User : System.Web.UI.Page
{


    public string nexturl;
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
            
            loaddata();
            try
            {
                if (Session["user"].ToString() == "")
                {
                                        
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/logins.aspx");
            }

            lbUsername.Text = "Logged in as" + " " + " " + (string)Session["username"] + "" + "" + (string)Session["role"];          
        }

    }

    private static DataTable GetData(string query)
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        String clientid = Request.QueryString["clientid"];
        String op = Request.QueryString["op"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"].Trim();
        String url = null;
       
        url = "BarGraphTest13.aspx" + "?" + "clientid=" + clientid + "&year=" + year + "&quarter=" + quarter;
        myIframe.Visible = true;
        myIframe.Attributes.Add("src", url);
        //Response.Write("<script>window.open('" + url + "','_blank');</script>");
    }
    public void loaddata()
    {
        txtID.Text = Request.QueryString["clientid"];
        txtQuarter.Text = Request.QueryString["quarter"];
        txtYear.Text = Request.QueryString["year"];


        conn.Close();
        conn.Open();

        string Query = " select * from Clients where client_number='" + Request.QueryString["clientid"] + "'";
        SqlCommand cmd = new SqlCommand(Query, conn);
        SqlDataReader dr = cmd.ExecuteReader();

        if (dr.Read() == true)


        {
            txtFirstName.Text = dr["name"].ToString();
            txtSpecialNotes.Text = dr["special_notes"].ToString();
        }
    }

    //protected void Button1_Click1(object sender, EventArgs e)
    //{
    //    //ExportGridToExcel();
    //}


    protected void Button3_Click(object sender, EventArgs e)
    {
        String clientid = Request.QueryString["clientid"];
        String op = Request.QueryString["op"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"].Trim();
        String url = null;

        url = "ReportTypeView.aspx" + "?" + "clientid=" + clientid + "&year=" + year + "&quarter=" + quarter;
        Response.Redirect(url);
    }

    protected void rdAssetMangers_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
  

    protected void Button5_Click(object sender, EventArgs e)
    {
        
    } 
   
}

   
        
        
                

    

