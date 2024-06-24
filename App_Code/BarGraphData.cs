//using System;
using System.Collections.Generic;
//using System.Web;
using System.Web.Services;
//using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
///=====================
//using System.Diagnostics;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Runtime.CompilerServices;
//using System.Security;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.VisualBasic;


/// <summary>
/// Summary description for BarGraphData
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

//[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
[ScriptService]
//public class BarGraphData : System.Web.Services.WebService
public class BarGraphData : System.Web.Services.WebService
{
    //[WebMethod()]
    //public string HelloWorld()
    //{
    //    return "Hello World";
    //}

    [WebMethod] 
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)] 
    public string GetBarChartData()
    {
        // Dim customers As New List(Of String)()
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conpath"].ConnectionString))
        {
            //conn.ConnectionString = ConfigurationManager.ConnectionStrings("conpath").ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = " DECLARE @cols AS NVARCHAR(MAX),@selectCols AS NVARCHAR(MAX),@query AS NVARCHAR(MAX),@client as nvarchar(50),@year as nvarchar(50),@quarter as nvarchar(50); set @client = '7' set @year = '2020' set @quarter = '1'; SELECT @selectCols = STUFF((SELECT distinct ', ISNULL(' + QUOTENAME([surname]) + ', 0) AS ' + QUOTENAME([surname]) FROM(select surname, Asset_class_id, value, TotalAcrossManagers, TotalPerManager,case when ExposurePerMan is null then 0 else ExposurePerMan end as ExposurePerMan,ExposureAcrossManagers from(select surname, Asset_class_id, value, TotalAcrossManagers, TotalPerManager, (case when isnull(VALUE,1)= 0 then 1 else isnull(VALUE, 1) end /case when isnull(TotalPerManager,1)= 0 then 1 else isnull(TotalPerManager, 1) end)*100 AS ExposurePerMan,(case when isnull(TotalAcrossManagers, 1) = 0 then 1 else isnull(TotalAcrossManagers, 1) end / case when isnull(GrandTotal,1)= 0 then 1 else isnull(GrandTotal, 1) end)*100 AS ExposureAcrossManagers FROM(select *, SUM(VALUE) OVER(PARTITION BY Asset_class_id) AS TotalAcrossManagers, SUM(VALUE) OVER(PARTITION BY surname) AS TotalPerManager, sum(VALUE) OVER(PARTITION BY client_id) AS GrandTotal from(select surname, Asset_class_id, sum(value) value, client_id, year from(SELECT i.*, am.surname FROM Investments i left join asset_managers am on am.id = i.Asset_manager_id)tt where tt.quarter = @quarter and year = @year and client_id = @client group by surname, asset_class_id, client_id, year) covids) TT) xx ) tt FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,1,'') SELECT @cols = STUFF((SELECT distinct ',' + QUOTENAME([surname]) FROM(select surname, Asset_class_id, value, TotalAcrossManagers, TotalPerManager,case when ExposurePerMan is null then 0 else ExposurePerMan end as ExposurePerMan,ExposureAcrossManagers from(select surname, Asset_class_id, value, TotalAcrossManagers, TotalPerManager, (case when isnull(VALUE,1)= 0 then 1 else isnull(VALUE, 1) end /case when isnull(TotalPerManager,1)= 0 then 1 else isnull(TotalPerManager, 1) end)*100 AS ExposurePerMan,(case when isnull(TotalAcrossManagers, 1) = 0 then 1 else isnull(TotalAcrossManagers, 1) end / case when isnull(GrandTotal,1)= 0 then 1 else isnull(GrandTotal, 1) end)*100 AS ExposureAcrossManagers FROM(select *, SUM(VALUE) OVER(PARTITION BY Asset_class_id) AS TotalAcrossManagers, SUM(VALUE) OVER(PARTITION BY surname) AS TotalPerManager, sum(VALUE) OVER(PARTITION BY client_id) AS GrandTotal from(select surname, Asset_class_id, sum(value) value, client_id, year from(SELECT i.*, am.surname FROM Investments i left join asset_managers am on am.id = i.Asset_manager_id)tt where tt.quarter = @quarter and year = @year and client_id = @client group by surname, asset_class_id, client_id, year) covids) TT) xx ) tt FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,1,'') SET @query = 'SELECT distinct Asset_class_id,' + @selectCols + N',ExposureAcrossManagers as Total FROM (select surname,Asset_class_id,case when ExposurePerMan is null then 0 else ExposurePerMan end as ExposurePerMan,ExposureAcrossManagers from(select surname, Asset_class_id, value, TotalAcrossManagers, TotalPerManager, (case when isnull(VALUE, 1) = 0 then 1 else isnull(VALUE, 1) end /case when isnull(TotalPerManager,1)= 0 then 1 else isnull(TotalPerManager, 1) end)*100 AS ExposurePerMan,(case when isnull(TotalAcrossManagers, 1) = 0 then 1 else isnull(TotalAcrossManagers, 1) end / case when isnull(GrandTotal,1)= 0 then 1 else isnull(GrandTotal, 1) end)*100 AS ExposureAcrossManagers FROM(select *, SUM(VALUE) OVER(PARTITION BY Asset_class_id) AS TotalAcrossManagers, SUM(VALUE) OVER(PARTITION BY surname) AS TotalPerManager, sum(VALUE) OVER(PARTITION BY client_id) AS GrandTotal from(select surname, Asset_class_id, sum(value) value, client_id, year from(SELECT i.*, am.surname FROM Investments i left join asset_managers am on am.id = i.Asset_manager_id)tt where tt.quarter = ''1'' and year = ''2020'' and client_id = ''7'' group by surname, asset_class_id, client_id, year) covids) TT ) tt) x pivot(Sum ([ExposurePerMan]) for [surname] in (' + @cols + ')) p' execute(@query) ";
                cmd.Connection = conn;
                // conn.Open()

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                // Using sdr As SqlDataReader = cmd.ExecuteReader()
                // While sdr.Read()
                // customers.Add(String.Format("{0}--{1}", sdr("display"), sdr("CUSTOMER_NUMBER")))

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                        row.Add(col.ColumnName,dr[col]);
                    rows.Add(row);
                }
                return serializer.Serialize(rows);
            }
        }
    }
}
