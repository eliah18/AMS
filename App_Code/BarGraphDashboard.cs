using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for BarGraphDashboard
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]

public class BarGraphDashboard : System.Web.Services.WebService
{
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
    public BarGraphDashboard()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
   
    public string getChatindodata(String clientid, String quarter, String year)
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conpath"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "DECLARE @cols AS NVARCHAR(MAX),@selectCols AS NVARCHAR(MAX),@query AS NVARCHAR(MAX),@client as nvarchar(50),@year as nvarchar(50),@quarter as nvarchar(50); set @client = '" + clientid + "' set @year = '" + year + "' set @quarter = '" + quarter + "'; select surname as Asset_manager, Asset_class_id,case when ExposurePerMan is null then 0 else ExposurePerMan end as ExposurePerMan,ExposureAcrossManagers as Total from (select surname, Asset_class_id, value, TotalAcrossManagers, TotalPerManager, (case when isnull(VALUE,1)= 0 then 1 else isnull(VALUE, 1) end /case when isnull(TotalPerManager,1)= 0 then 1 else isnull(TotalPerManager, 1) end)*100 AS ExposurePerMan,(case when isnull(TotalAcrossManagers, 1) = 0 then 1 else isnull(TotalAcrossManagers, 1) end / case when isnull(GrandTotal,1)= 0 then 1 else isnull(GrandTotal, 1) end)*100 AS ExposureAcrossManagers FROM(select *, SUM(VALUE) OVER(PARTITION BY Asset_class_id) AS TotalAcrossManagers, SUM(VALUE) OVER(PARTITION BY surname) AS TotalPerManager, sum(VALUE) OVER(PARTITION BY client_id) AS GrandTotal from(select surname, Asset_class_id, sum(value) value, client_id, year from(SELECT i.*, am.surname FROM Investments i left join asset_managers am on am.id = i.Asset_manager_id)tt where tt.quarter = @quarter and year = @year and client_id = @client group by surname, asset_class_id, client_id, year) covids) TT) xx ";
                cmd.Connection = conn;
                //var adp = new SqlDataAdapter(cmd);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                        row.Add(col.ColumnName, dr[col]);
                    rows.Add(row);
                }
                return serializer.Serialize(rows);
            }
        }
      //}
      //  catch (Exception ex)
      //  {
      //      //ErrorLogging.WriteLogFile(" --- page_load()", ex.ToString());
      //      msgbox("Failed to load Page: " + ex.ToString());
      //  }
    }

}
