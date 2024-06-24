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

public partial class admin_parameters_User : System.Web.UI.Page
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

            loaddata();
            conn.Close();
            conn.Open();
            //MsgBox(lstNamesSearch.Text, Page, this);
            SqlCommand cmd = new SqlCommand("select * from clients where client_number='" + Request.QueryString["clientid"] + "'", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read() == true)
            {
                txtFirstName.Text = dr["name"].ToString();
                //txtAddress.Text = dr["address"].ToString();
                //txtContactNumber.Text = dr["contact_details"].ToString();
                txtSpecialNotes.Text = dr["special_notes"].ToString();
                txtID.Text = dr["client_number"].ToString();

                getComplianceMatrix();
                    grdApps.Visible = true;
            }

            dr.Close();
            loadVariables();
            loadassetmanagers();
            getComplianceMatrix();
            lbUsername.Text = "Logged in as" + " " + " " + (string)Session["username"] + "" + "" + (string)Session["role"];
                       
        }

    }
    protected void Button13_Click(object sender, EventArgs e)
    {
        String clientid = Request.QueryString["clientid"];
        String op = Request.QueryString["op"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"].Trim();
        String url = null;

        url = "ReportTypeView.aspx" + "?" + "clientid=" + clientid + "&year=" + year + "&quarter=" + quarter;
        Response.Redirect(url);
    }
    public void loaddata()
    {
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

    public void loadVariables()
    {
        conn.Close();
        conn.Open();
        string com = "select distinct variable from ComplianceMatrix where client_id='" + txtID.Text + "' and year='" + txtYear.Text + "' and quarter='" + txtQuarter.Text + "'";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);


        cmbAsetClass.DataSource = dt;
        cmbAsetClass.DataBind();
        cmbAsetClass.DataTextField = "variable";
        cmbAsetClass.DataValueField = "variable";
        cmbAsetClass.DataBind();
        cmbAsetClass.Items.Insert(0, new ListItem("Select A Variable", "0"));
        // cmbAsetClass.Items.Insert(0, new ListItem("Select an asset", "0"));


    }

    public void loadassetmanagers()
    {
        conn.Close();
        conn.Open();
        string com = "select distinct asset_manager from ComplianceMatrix where client_id='" + txtID.Text + "' and year='" + txtYear.Text + "' and quarter='" + txtQuarter.Text + "'";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        rdAssetMangers.DataSource = dt;
        rdAssetMangers.DataBind();
        rdAssetMangers.DataTextField = "asset_manager";
        rdAssetMangers.DataValueField = "asset_manager";
        rdAssetMangers.DataBind();
        rdAssetMangers.Items.Insert(0, new ListItem("Select An Asset Manager", "0"));

    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("User.aspx");
    }

    public void checkPecerntageLeft(String clientid)
    {
        conn.Close();
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select 1-sum(Value)  as PercentageLeft from Ipsecs where client_id='" + clientid + "' and active='1' and allocation_level='client'", conn);
            int percentageleft = int.Parse(cmd.ExecuteScalar().ToString());
            if (percentageleft < 1)
            {
                MsgBox("You have " + percentageleft + "% left", this.Page, this);
            }
            else if (percentageleft > 1)
            {
                MsgBox("Total Asset Class Percentage Exceeded. Please insert another percentage", this.Page, this);
            }
        }

        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
    }

    protected void getSumFromDBAssetManagers()
    {
        conn.Close();
        conn.Open();
        //MsgBox(lstNamesSearch.Text, Page, this);
        SqlCommand cmd = new SqlCommand("select isnull(sum(Value),0.00) as PercentageLeft  from Ipsecs where client_id='" + txtID.Text + "' and active='1' and allocation_level='assetmanager' and  Asset_manager_id='" + rdAssetMangers.SelectedItem.Text + "'", conn);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read() == true)
        {
            txtSumFromDB.Text = dr["PercentageLeft"].ToString();
            LblTotal.Text = dr["PercentageLeft"].ToString() + '%';
            LblTotal.Visible = true;
            Label3.Visible = true;
        }

        dr.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    public void getIpsec()

    {
        conn.Close();
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("DECLARE  @columns NVARCHAR(MAX) = '',@clientid NVARCHAR(MAX) = '" + txtID.Text + "',   @sql     NVARCHAR(MAX) = '';select  @columns+= QUOTENAME(surname) + ',' from( SELECT * FROM (select id,surname from asset_managers where id in( select assetmanager_id from [client_assetmanager_relations] where client_id=@clientid ) union select client_number,name from Clients where client_number=@clientid)y left join (select client_id,Asset_manager_id,value from ipsecs where client_id=@clientid and active='1')   r on  y.surname=r.Asset_manager_id )d SET @columns = LEFT(@columns, LEN(@columns) - 1);SET @sql ='select * from (select IPS.Asset_class_id as AssetClass,IPS.value ,IPS.Asset_manager_id  from (select id, surname from asset_managers  where id in(select assetmanager_id from[client_assetmanager_relations] where client_id = '+@clientid+') union select client_number,name from Clients where client_number = '+@clientid+' )r JOIN Ipsecs IPS on IPS.Asset_manager_id = R.surname and IPS.client_id = '+@clientid+')s PIVOT(sum(value) FOR  asset_manager_id IN('+@columns+')) AS pivot_table; 'EXECUTE sp_executesql @sql;", conn);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.AddWithValue("@ID", txtID.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    grdApps.DataSource = ds;
                    grdApps.DataBind();
                }
            }
            else
            {
                grdApps.DataSource = null;
                grdApps.DataBind();
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }
    public void getComplianceMatrix()

    {
        conn.Close();
        //try
        {

            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("DECLARE @cols AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX); select @cols = STUFF((SELECT distinct ',' + QUOTENAME(asset_manager) from (select[variable],AVG(scale) OVER (PARTITION BY variable) [overal],[asset_manager],[scale] from complianceMatrix where client_id = '" + txtID.Text + "' and year = '" + txtYear.Text + "' and quarter = '" + txtQuarter.Text + "') tt FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, ''); set @query = N'SELECT variable as Variable,' + @cols + N',overal as Overal from (select variable, overal, asset_manager, scale from (select[variable],AVG(scale) OVER (PARTITION BY variable) [overal],[asset_manager],[scale] from complianceMatrix where client_id = ''" + txtID.Text + "'' and year = ''" + txtYear.Text + "'' and quarter = ''" + txtQuarter.Text + "'') tt ) x pivot (sum(scale) for [asset_manager] in (' + @cols + N')) p group by variable,overal,' + @cols + N''; exec sp_executesql @query; ", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);

            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        grdClientIpsecs.DataSource = ds;
                        grdClientIpsecs.DataBind();
                    }
                }
                else
                {
                    grdClientIpsecs.DataSource = null;
                    grdClientIpsecs.DataBind();
                }                
            }
            else
            {
                //MsgBox("Y There Is No Data For " + txtFirstName.Text + " In Quarter " + txtQuarter.Text + " Of " + txtYear.Text, this.Page, this);
            }

        } 
        boldGridText();
        //catch (Exception ex)
        //{
        //    conn.Close();
        //    MsgBox(ex.Message, this.Page, this);
        //}

    }

    protected void rdAssetClass_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public void fetcheditdata(String id)
    {

        conn.Close();
        conn.Open();
        string Query = "SELECT * FROM Ipsecs WHERE id='" + id.ToString() + "' ";
        SqlCommand cmd = new SqlCommand(Query, conn);
        SqlDataReader dr = cmd.ExecuteReader();

        if (dr.Read() == true)
        {

            rdAssetMangers.DataValueField = dr["Asset_manager_id"].ToString();
            cmbAsetClass.Text = dr["[Asset_class_id"].ToString();
            txtAllocation.Text = dr["value"].ToString();

            string[] wholestring = dr["Corridor"].ToString().Split('-');
        }
        dr.Close();
    }

    public Boolean updatedata(String id, String value)
    {
        bool added = false;
        conn.Close();
        conn.Open();
        SqlCommand cmd = new SqlCommand("update  ComplianceMatrix set scale='" + value + "' where variable='" + cmbAsetClass.Text + "' and client_id='" + id + "' and year='" + txtYear.Text + "' and quarter='" + txtQuarter.Text + "' and asset_manager='" + rdAssetMangers.Text + "'", conn);
        if ((conn.State == ConnectionState.Open))
            conn.Close();
        conn.Open();

        int a = cmd.ExecuteNonQuery();
        conn.Close();
        if (a == 1)
        {
            added = true;
        }
        return added;
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        ///// F.Chatz3
        if (rdAssetMangers.SelectedItem.Text == "Select An Asset Manager")
        {
            rdAssetMangers.Focus();
            MsgBox("Please Select An Asset Manager", Page, this);
        }
         if (cmbAsetClass.SelectedItem.Text == "Select A Variable")
        {
            cmbAsetClass.Focus();
            MsgBox("Please Select A Variable", Page, this);
        } 
         if (txtAllocation.Text == "") 
        {
            txtAllocation.Focus();
            MsgBox("Please Select Scale", Page, this);
        }
        
            conn.Close();
            try
            {
                conn.Open();

                if (updatedata(txtID.Text, txtAllocation.Text))
                {
                    MsgBox("Matrix Scale Update Successful.", Page, this);

                    clearfields();
                    loaddata();
                    loadVariables();
                    loadassetmanagers();
                    getComplianceMatrix();

                }
                else
                {
                    MsgBox("Matrix Scale Update Failed.", Page, this);

                }
            }

            catch (Exception ex)
            {
                conn.Close();
                MsgBox("Update Exception: " + ex.Message, this.Page, this);
            }      
                
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }
    private void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        //string FileName = "ComplianceMatrix" + DateTime.Now + ".xls";
        string FileName = "ComplianceMatrix_For_" + txtYear.Text + "_Q" + txtQuarter.Text + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        grdClientIpsecs.GridLines = GridLines.Both;
        grdClientIpsecs.HeaderStyle.Font.Bold = true;
        grdClientIpsecs.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    public void clearfields()
    {
        txtFirstName.Text = "";
        txtAllocation.Text = "";
        txtAllocationOld.Text = "";
        cmbAsetClass.SelectedItem.Text = "Select An Asset Class";
    }
        
    protected void grdClientIpsecs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int j = 0; j < grdClientIpsecs.Rows.Count; j++)
            {

                for (int k = 0; k < grdClientIpsecs.Rows[j].Cells.Count; k++)
                {
                    if (grdClientIpsecs.Rows[j].Cells[k].Text == "0")
                    {
                        grdClientIpsecs.Rows[j].Cells[k].BackColor = System.Drawing.Color.Red;
                    }
                    else if (grdClientIpsecs.Rows[j].Cells[k].Text == "1")
                    {
                        grdClientIpsecs.Rows[j].Cells[k].BackColor = System.Drawing.Color.Red;
                    }
                    else if (grdClientIpsecs.Rows[j].Cells[k].Text == "2")
                    {
                        grdClientIpsecs.Rows[j].Cells[k].BackColor = System.Drawing.Color.Orange;
                    }
                    else if (grdClientIpsecs.Rows[j].Cells[k].Text == "3")
                    {
                        grdClientIpsecs.Rows[j].Cells[k].BackColor = System.Drawing.Color.Yellow;
                    }
                    else if (grdClientIpsecs.Rows[j].Cells[k].Text == "4")
                    {
                        grdClientIpsecs.Rows[j].Cells[k].BackColor = System.Drawing.Color.LightGreen;
                    }
                    else if (grdClientIpsecs.Rows[j].Cells[k].Text == "5")
                    {
                        grdClientIpsecs.Rows[j].Cells[k].BackColor = System.Drawing.Color.Green;
                    }
                }
            }
        }        
    }

    protected void grdClientIpsecs_PreRender(object sender, EventArgs e)
    {
        try
        {
            if (grdClientIpsecs.Rows.Count > 0)
            { 
            GridViewRow getRow = grdClientIpsecs.Rows[grdClientIpsecs.Rows.Count - 1];

            for (int k = 0; k < getRow.Cells.Count; k++)
            {
                if (getRow.Cells[k].Text == "0")
                {
                    getRow.Cells[k].BackColor = System.Drawing.Color.Red;
                }
                else if (getRow.Cells[k].Text == "1")
                {
                    getRow.Cells[k].BackColor = System.Drawing.Color.Red;
                }
                else if (getRow.Cells[k].Text == "2")
                {
                    getRow.Cells[k].BackColor = System.Drawing.Color.Orange;
                }
                else if (getRow.Cells[k].Text == "3")
                {
                    getRow.Cells[k].BackColor = System.Drawing.Color.Yellow;
                }
                else if (getRow.Cells[k].Text == "4")
                {
                    getRow.Cells[k].BackColor = System.Drawing.Color.LightGreen;
                }
                else if (getRow.Cells[k].Text == "5")
                {
                    getRow.Cells[k].BackColor = System.Drawing.Color.Green;
                }
            }
            }
            else
            {
                MsgBox("There Is No Data For " + txtFirstName.Text + " In Quarter " + txtQuarter.Text + " Of " + txtYear.Text, this.Page, this);
                Button5.Visible = false;
                    Button1.Visible = false;
            }
    }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
    }

    public void boldGridText()
    {
        foreach (GridViewRow row in grdClientIpsecs.Rows)
        {            
            for (int b = 1; b < row.Cells.Count; b++)
            {                
                row.Cells[b].Font.Bold = true;
            }
        }
    }
}