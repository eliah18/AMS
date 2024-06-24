using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;

/// <summary>
/// Summary description for getGraphdata
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class getGraphdata : System.Web.Services.WebService
{

    public getGraphdata()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }
    string cnstr = System.Configuration.ConfigurationManager.ConnectionStrings["conpath"].ConnectionString;
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]


    public string GetRegData(string clientid, string quarter, string year)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = cnstr;
            using (SqlCommand cmd = new SqlCommand("Exposures", conn))
            {
                //cmd.CommandText = "dbo.Exposures;
                //cmd = new SqlCommand("Exposures", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@clientid", clientid);
                cmd.Parameters.AddWithValue("@quarter", quarter);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Connection = conn;
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
    }
}
