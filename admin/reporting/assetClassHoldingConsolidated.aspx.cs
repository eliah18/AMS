using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;

public partial class admin_reporting_assetClassHolding_Consolidated : System.Web.UI.Page
{
    ReportDocument cryRpt = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        String client_id = Request.QueryString["client_id"];
        String Quarter = Request.QueryString["Quarter"];
        //String Subaccount = Request.QueryString["Subaccount"];
        String Year = Request.QueryString["Year"];

        {

            cryRpt.Load(Server.MapPath(@"rptassetClassHoldingConsolidated.rpt"));

            cryRpt.SetParameterValue("client_id", client_id);
            cryRpt.SetParameterValue("Quarter", Quarter);
            //cryRpt.SetParameterValue("Subaccount", Subaccount);
            cryRpt.SetParameterValue("Year", Year);
            CrystalReportViewer.ReportSource = cryRpt;
        }

    }
    protected void Page_unLoad(object sender, EventArgs e)
    {
        cryRpt.Close();
        cryRpt.Dispose();

    }
}


