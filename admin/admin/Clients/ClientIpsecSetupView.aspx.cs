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
         
           
            loadusertypes();
           // getIpsecClient();

            lbUsername.Text ="Logged in as" +" "+ " " + (string)Session["username"] + ""+ ""+ (string)Session["role"];

            //Boolean user=  checkuser(txtFirstName.Text, txtSurname.Text, txtUsername.Text);
            //  if(user)
            //  {
            //      MsgBox("User Already Exists", this.Page, this);

            //  }
        }
        
    }
    public Boolean checkIpsec( String clientid, String assetclassid,String assetmanagerid)
    {
        Boolean existance = false;
        conn.Close();
        try
             
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Ipsecs where client_id='" + clientid + "' and  Asset_class_id='" + assetclassid + "' and Asset_manager_id ='" + assetmanagerid + "' and active='1' ", conn);
            int count = int.Parse(cmd.ExecuteScalar().ToString());
            if (count >= 1)
            {
                existance = true;
            }
           
        }


        
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
        return existance;




    }
    public Boolean checkIpsecClient(String clientid, String assetclassid)
    {
        Boolean existance = false;
        conn.Close();
        try

        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Ipsecs where client_id='" + clientid + "' and  Asset_class_id='" + assetclassid + "' and allocation_level='client' and active='1' ", conn);
            int count = int.Parse(cmd.ExecuteScalar().ToString());
            if (count >= 1)
            {
                existance = true;
            }

        }



        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
        return existance;




    }
    public Boolean InsertIpsec (String clientid, String assetclassid, String assetmanagerid , String value)
    {
        Boolean added = false;
        try
        {
            conn.Close();
            conn.Open();

            String query = "INSERT INTO Ipsecs(client_id ,Asset_class_id, Asset_manager_id ,value,allocation_level) values('" + clientid + "','" + assetclassid + "','" + assetmanagerid + "','" + value + "','assetmanager')";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteReader(CommandBehavior.CloseConnection);
            added = true;
        }
        catch (Exception ex)
        {
            MsgBox("Error: " + ex.Message, this.Page, this);
            throw;
        }
        return added;


    }
    public Boolean InsertIpsecClient(String clientid, String value ,String assetclass)
    {
        Boolean added = false;
        try
        {
            conn.Close();
            conn.Open();
            
            String query = "INSERT INTO Ipsecs(client_id ,value,allocation_level ,asset_class_id ,asset_manager_id) values('" + clientid + "','" + value + "','client', '"+ assetclass+"' ,'"+ txtFirstName.Text+"')";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteReader(CommandBehavior.CloseConnection);
            added = true;
        }
        catch (Exception ex)
        {
            MsgBox("Error: " + ex.Message, this.Page, this);
            throw;
        }
        return added;


    }










    public void loadusertypes()
    {
        
        string com = "Select * from assets_class where active ='1'";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        

        cmbAsetClass.DataSource = dt;
        cmbAsetClass.DataBind();
        cmbAsetClass.DataTextField = "asset_name";
        cmbAsetClass.DataValueField = "asset_name";
        cmbAsetClass.DataBind();
       // cmbAsetClass.Items.Insert(0, new ListItem("Select an asset", "0"));


    }
   
    public void loadassetmanagers()
    {
        conn.Close();
        conn.Open();
        string com = " select c.assetmanager_id as id,a.surname as name,c.client_id from client_assetmanager_relations c join clients q on c.client_id=q.client_number join asset_managers a on a.id=c.assetmanager_id where c.client_id='"+ txtID.Text+"'";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        rdAssetMangers.DataSource = dt;
        rdAssetMangers.DataBind();
        rdAssetMangers.DataTextField = "name";
        rdAssetMangers.DataValueField = "id";
        rdAssetMangers.DataBind();
        rdAssetMangers.Items.Insert(0, new ListItem("Select An Asset Manager", "0"));

    }


    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    String id = txtID.Text.ToString();
    //    Boolean edited = edituser(id);
    //    if (edited)
    //    {
    //        MsgBox("Update Successful", this.Page, this);


    //        usersPanel.Visible = false;
    //    }
    //}
    //public Boolean edituser(string id)
    //{

    //    {
    //        SqlCommand cmd = new SqlCommand("update SystemUsers set name='" + txtFirstName.Text + "' ,surname='" + txtSurname.Text + "',password='" + txtPassword.Text + "' ,role='" + rdRole.SelectedItem.Text + "', username='" + txtUsername.Text + "',role_id='" + rdRole.SelectedValue + "' where id='" + txtID.Text + "' ", conn);
    //        if ((conn.State == ConnectionState.Open))
    //            conn.Close();
    //        conn.Open();
    //        cmd.ExecuteNonQuery();
    //        conn.Close();

    //    }
    //    return true;
    //}
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("User.aspx");
    }



    

    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {
            conn.Close();
            conn.Open();
            
            SqlCommand cmd = new SqlCommand("select name from clients where name like '%" + txtsearch.Text +"%'  ", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("clients");
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                

                 lstNamesSearch.DataSource = ds;
                lstNamesSearch.DataTextField = "name";
                lstNamesSearch.DataValueField = "name";

                lstNamesSearch.DataBind();
                
            }
            else
            {
                lstNamesSearch.DataSource = null;
                lstNamesSearch.DataBind();
            }





            conn.Close();

        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
    }

    

    protected void lstNamesSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        conn.Close();
        conn.Open();
        //MsgBox(lstNamesSearch.Text, Page, this);
        SqlCommand cmd = new SqlCommand("select * from clients where name='" + lstNamesSearch.SelectedItem.Text + "'", conn);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read() == true)
        {
            txtFirstName.Text = dr["name"].ToString();
            txtAddress.Text = dr["address"].ToString();
            txtContactNumber.Text = dr["contact_details"].ToString();
            txtSpecialNotes.Text = dr["special_notes"].ToString();
            txtID.Text = dr["client_number"].ToString();
            loadassetmanagers();
           // getIpsec();
            
            grdApps.Visible = true;


        }
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
            SqlCommand cmd = new SqlCommand("DECLARE  @columns NVARCHAR(MAX) = '',@clientid NVARCHAR(MAX) = '"+txtID.Text+ "',   @sql     NVARCHAR(MAX) = '';select  @columns+= QUOTENAME(surname) + ',' from( SELECT * FROM (select id,surname from asset_managers where id in( select assetmanager_id from [client_assetmanager_relations] where client_id=@clientid ) union select client_number,name from Clients where client_number=@clientid)y left join (select client_id,Asset_manager_id,value from ipsecs where client_id=@clientid and active='1')   r on  y.surname=r.Asset_manager_id )d SET @columns = LEFT(@columns, LEN(@columns) - 1);SET @sql ='select * from (select IPS.Asset_class_id as AssetClass,IPS.value ,IPS.Asset_manager_id  from (select id, surname from asset_managers  where id in(select assetmanager_id from[client_assetmanager_relations] where client_id = '+@clientid+') union select client_number,name from Clients where client_number = '+@clientid+' )r JOIN Ipsecs IPS on IPS.Asset_manager_id = R.surname and IPS.client_id = '+@clientid+')s PIVOT(sum(value) FOR  asset_manager_id IN('+@columns+')) AS pivot_table; 'EXECUTE sp_executesql @sql;", conn);
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
    public void getIpsecClient()

    {
        conn.Close();
        try
        {

            SqlCommand cmd = new SqlCommand();
            conn.Open();
           
            if (RadioButtonList1.SelectedItem.Value== "Client")
               
            {
                
                cmd = new SqlCommand("select Asset_class_id as AssetClass,value  from ipsecs where client_id='"+txtID.Text+"' and allocation_level='client'  and active='1' union select 'total' as asset_class_id, sum(value) as value from ipsecs where client_id='"+txtID.Text+"' and allocation_level='client' ", conn);
            }
            else
            {
                Label12.Text = "IPS Mandate for " + " " + txtFirstName.Text + " " + "under Asset Manager" + " " + rdAssetMangers.SelectedItem.Text + "";
                //Label12.ForeColor = System.Drawing.Color.White;
                //Label12.BackColor = System.Drawing.Color.Black;
                cmd = new SqlCommand("select Asset_class_id as AssetClass,value  from ipsecs where client_id='"+txtID.Text+"'  and allocation_level='assetmanager'  and active='1' and Asset_manager_id='"+rdAssetMangers.SelectedItem.Text+"' union select 'total' as asset_class_id, sum(value) as value from ipsecs where client_id='"+txtID.Text+"' and allocation_level='assetmanager' and Asset_manager_id='"+ rdAssetMangers.SelectedItem.Text+"'  ", conn);
            }
            
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);

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
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

        
        

    }

    protected void rdAssetClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void rdAssetMangers_SelectedIndexChanged(object sender, EventArgs e)
    {
       // MsgBox("hie",Page,this);
        //assetmanagerslabel.Text = "Client IPsec for AssetManager" + ' ' + rdAssetMangers.SelectedItem.Text;
        //getIpsec();
    }
    public void linkDiscard(object sender, System.EventArgs e)
    {
        string idd = ((LinkButton)sender).CommandArgument;
        SqlCommand cmd = new SqlCommand("update  Ipsecs set active='0' where Id='" + idd + "' ", conn);
        if ((conn.State == ConnectionState.Open))
            conn.Close();
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
        MsgBox("Delete Successful", this.Page, this);
        //getIpsec();
        getIpsecClient();
    }
    public void lnkedit(object sender, System.EventArgs e)
    {
        string idd = ((LinkButton)sender).CommandArgument;
        fetcheditdata(idd);

        //updatedata(idd);

    }
    public void links(object sender, System.EventArgs e)
    {
        string idd = ((LinkButton)sender).CommandArgument;
        //MsgBox("hallo", this.Page, this);
        fectchclientipsecdata(idd);
        //updatedata(idd);

    }

    public void fectchclientipsecdata(String id)
    {
        conn.Close();
        conn.Open();
        string Query = "SELECT * FROM Ipsecs WHERE id='" + id.ToString() + "' ";
        SqlCommand cmd = new SqlCommand(Query, conn);
        SqlDataReader dr = cmd.ExecuteReader();

        if (dr.Read() == true)
        {
            cmbAsetClass.Text = dr["Asset_class_id"].ToString();
           

            txtClientIpsecUpdateID.Text = dr["id"].ToString();
            txtAllocation.Text = dr["value"].ToString();
            Button3.Visible = false;

            Button5.Visible = true;
        }

    }
    public void fetcheditdata( String id)
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
            txtClientIpsecUpdateID.Text= dr["[id"].ToString();
            txtAllocation.Text = dr["value"].ToString();
            //Button2.Visible = true;
            //Button1.Visible = false;
        }

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
       
    }
    public Boolean updatedata(String id,String value)
    {
        bool added = false;
        conn.Close();
        conn.Open();
        SqlCommand cmd = new SqlCommand("update  Ipsecs set value='" + value +"' where Id='" + id + "' ", conn);
        if ((conn.State == ConnectionState.Open))
            conn.Close();
        conn.Open();
        
       int a= cmd.ExecuteNonQuery();
        conn.Close();
        if (a == 1)
        {
            added = true;
        }
        
        
        return added;
    }

    protected void Button3_Click1(object sender, EventArgs e)
    {



        getIpsecClient();
            
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        if (updatedata(txtClientIpsecUpdateID.Text, txtAllocation.Text))
        {
            MsgBox("Update Successful", this.Page, this);
            //getIpsec();
            getIpsecClient();
            Button3.Visible = true;
            Button5.Visible = false;

        }

    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
       // MsgBox("tapinda", this.Page, this);
        if(RadioButtonList1.SelectedItem.Value== "AssetManager")
        {
            Label7.Visible = true;
            rdAssetMangers.Visible = true;
            txtAllocation.Visible = false;
           
        }
        else
        {
            Label7.Visible = false;
            rdAssetMangers.Visible = false;
            txtAllocation.Visible = false;
        }
    }
}