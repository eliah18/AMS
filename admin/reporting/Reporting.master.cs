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

public partial class admin_parameters_Investor : System.Web.UI.MasterPage
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
            if ( Session["roleid"] ==null)
            {
                Response.Redirect("~/logins.aspx");
            }
            //GetListData();
        }
    }

    protected void ReportingDashboard_OnSelectedNodeChanged(object sender, EventArgs e)
    {
        
        String clientid = Request.QueryString["clientid"];
        String op = Request.QueryString["op"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"].Trim();
        String url = null;

        if (TreeView1.SelectedNode.Text.ToString() == "Main Dashboard")
        {
            url = "~/admin/parameters/MainDashboard.aspx";
        } 
        else if (TreeView1.SelectedNode.Text.ToString() == "Reporting Dashboard")
        {
            url = "~/admin/Reporting/ReportingDashboard.aspx" + "?" + "clientid=" + clientid + "&year=" + year + "&quarter=" + quarter;
        }
        else if (TreeView1.SelectedNode.Text.ToString() == "Client Selection")
        {
            url = "~/admin/Reporting/ClientsInvestments.aspx";
        }

        Response.Redirect(url);
    }


}
