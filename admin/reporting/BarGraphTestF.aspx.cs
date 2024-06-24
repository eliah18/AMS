using System;
using System.Data.SqlClient;

partial class index_Farai : System.Web.UI.Page
{
    private SqlDataAdapter adp;
    private SqlConnection con = new SqlConnection();
    //private string cnstr = System.Configuration.ConfigurationManager.AppSettings("connpath");
    //public string LoginURL = System.Configuration.ConfigurationManager.AppSettings("LoginURL");
    public int SessionTimoutTime;
    public void msgbox(string strMessage)
    {
        // finishes server processing, returns to client.
        string strScript = "<script language=JavaScript>";
        strScript += "window.alert(\"" + strMessage + "\");";
        strScript += "</script>";
        System.Web.UI.WebControls.Label lbl = new System.Web.UI.WebControls.Label();
        lbl.Text = strScript;
        //System.Web.UI.Control.Page.Controls.Add(lbl);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //System.Web.UI.Control.Page.MaintainScrollPositionOnPostBack = true;
            //if (!System.Web.UI.Page.IsPostBack)
            //{
            //}

            //msgbox("Success");
        }
        catch (Exception ex)
        {
            //ErrorLogging.WriteLogFile(" --- page_load()", ex.ToString());
            msgbox("Failed to load Page: " + ex.ToString());
        }
    }
}
