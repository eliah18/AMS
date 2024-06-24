using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_reporting_ClientInvestment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        String year = Request.QueryString["year"];
        String quarter = Request.QueryString["quarter"];
        String clientid = Request.QueryString["clientid"];
        ReportDocument cryRpt = new ReportDocument();
        {
            cryRpt.Load(Server.MapPath(@"Snap.rpt"));


           
            cryRpt.SetParameterValue("pclientid", clientid);
            cryRpt.SetParameterValue("pquarter", quarter);
            cryRpt.SetParameterValue("pyear", year);
            CrystalReportViewer1.ReportSource = cryRpt;
        }

    }
}