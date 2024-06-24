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
            GetListData();
        }
    }
    private void GetListData()
    {
        conn.Close();
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select id,moduleid ,userid,modulename,parent_id,parent from module_permissions_users  where userid='"+Session["roleid"] +"' ", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);
            ds.Relations.Add("childrows",  ds.Tables[0].Columns["parent_id"], ds.Tables[0].Columns["parent"]);
            foreach(DataRow level1DataRow in ds.Tables[0].Rows)
            {
                if (string.IsNullOrEmpty(level1DataRow["parent"].ToString())){
                    TreeNode parentTreeNode = new TreeNode();
                    parentTreeNode.Text = level1DataRow["modulename"].ToString();
                    parentTreeNode.NavigateUrl = level1DataRow["moduleid"].ToString();
                    DataRow[] childrows = level1DataRow.GetChildRows("childrows");
                    foreach (DataRow level2DataRow in childrows)
                    {
                        TreeNode childTreeNode = new TreeNode();
                        childTreeNode.Text = level2DataRow["modulename"].ToString();
                        childTreeNode.NavigateUrl = level2DataRow["moduleid"].ToString();
                        parentTreeNode.ChildNodes.Add(childTreeNode);
                    }
                    TreeView1.Nodes.Add(parentTreeNode);
                }
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
    }

    }
