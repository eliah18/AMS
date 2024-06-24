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
    public Boolean InsertIpsec (String clientid, String assetclassid, String assetmanagerid , String value)
    {
        Boolean added = false;
        try
        {
            conn.Close();
            conn.Open();
            String query = "INSERT INTO Ipsecs(client_id ,Asset_class_id, Asset_manager_id ,value) values('" + clientid + "','" + assetclassid + "','" + assetmanagerid + "','" + value + "')";
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
        rdAssetClass.DataSource = dt;
        rdAssetClass.DataBind();
        rdAssetClass.DataTextField = "asset_name";
        rdAssetClass.DataValueField = "ID";
        rdAssetClass.DataBind();
        rdAssetClass.Items.Insert(0,new ListItem("Select an asset", "0"));
        

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
            
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if(rdAssetMangers.SelectedItem.Text== "Select An Asset Manager")
        {
            MsgBox("select An Asset Manger" ,Page,this);
            return;
        }
        if(rdAssetClass.SelectedItem.Text == "Select an asset")
        {
            MsgBox("select An Asset Class", Page, this);
            return;
        }
        if (checkIpsec(txtID.Text, rdAssetClass.SelectedItem.Text, rdAssetMangers.SelectedItem.Text))
        {
            MsgBox("Asset Class Ipsec Already  Added", Page, this);
        }
        else{
            if (InsertIpsec(txtID.Text, rdAssetClass.SelectedItem.Text, rdAssetMangers.SelectedItem.Text,txtValue.Text))
            {
                MsgBox("Asset Class Inserted Successfully", Page, this);
            }
            getIpsec();

        }
    }
    public void getIpsec()

    {
        conn.Close();
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select  id,asset_class_id as [Asset Class Name] ,Value from Ipsecs where client_id='" +  txtID.Text +"' and Asset_Manager_id ='"+rdAssetMangers.SelectedItem.Text +"' and active='0' order by id desc", conn);
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

    protected void rdAssetClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void rdAssetMangers_SelectedIndexChanged(object sender, EventArgs e)
    {
       // MsgBox("hie",Page,this);
        assetmanagerslabel.Text = "Client IPsec for AssetManager" + ' ' + rdAssetMangers.SelectedItem.Text;
        getIpsec();
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
        getIpsec();

    }
    public void lnkedit(object sender, System.EventArgs e)
    {
        string idd = ((LinkButton)sender).CommandArgument;
        fetcheditdata(idd);

        //updatedata(idd);

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
            rdAssetClass.DataValueField = dr["Asset_class_id"].ToString();
            rdAssetMangers.DataValueField = dr["Asset_manager_id"].ToString();
            
            txtupdateid.Text= dr["id"].ToString();
            txtValue.Text = dr["value"].ToString();
            Button2.Visible = true;
            Button1.Visible = false;
        }

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
       if( updatedata(txtupdateid.Text,txtValue.Text))
        {
            MsgBox("Update Successful", this.Page, this);
            getIpsec();

        }
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
}