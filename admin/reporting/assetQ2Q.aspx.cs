using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;

public partial class admin_reporting_assetQ2Q : System.Web.UI.Page
{
    ReportDocument cryRpt = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {

        String client_id = Request.QueryString["client_id"];
        String fromquarter = Request.QueryString["prevQuarter"];
        String fromyear = Request.QueryString["prevYear"];
        String toquarter = Request.QueryString["toQuarter"];
        String toyear = Request.QueryString["toYear"];

        {

            cryRpt.Load(Server.MapPath(@"rptQ2Q.rpt"));

            cryRpt.SetParameterValue(0, client_id);
            cryRpt.SetParameterValue(1,fromquarter);
            cryRpt.SetParameterValue(2,fromyear);
            cryRpt.SetParameterValue(3,toquarter);
            cryRpt.SetParameterValue(4,toyear);
            CrystalReportViewer.ReportSource = cryRpt;
        }

    }
    protected void Page_unLoad(object sender, EventArgs e)
    {
        cryRpt.Close();
        cryRpt.Dispose();

    }
}


