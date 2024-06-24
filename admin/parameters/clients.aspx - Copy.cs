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
using System.Text;

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
           
            //BindRepeater();
            GetListData();
            loadassetmanagers();
            loadcustodians();
            loadAdministrators();
            lbUsername.Text = "Logged in as" + " " + " " + (string)Session["username"] + "" + "" + (string)Session["role"];

            //Boolean user=  checkuser(txtFirstName.Text, txtSurname.Text, txtUsername.Text);
            //  if(user)
            //  {
            //      MsgBox("User Already Exists", this.Page, this);

            //  }
        }
        
    }
    public Boolean checkuser( String firstname)
    {
        Boolean existance = false;
        conn.Close();
        try
             
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM clients where name='" + firstname + "'   ", conn);
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
    public Boolean Adduser(String firstname,String date,String ratereturn,String specialnotes,String fees,String custodian,String contact,String address ,String administrators, String clientnumber)
    {
        Boolean added = false;
        try
        {
            conn.Close();
            conn.Open();
            String query = "INSERT INTO clients(name,date_of_take,rate_of_return,special_notes,fees,custodian,contact_details,address,administrators,client_number) values('" + firstname + "','" + date + "','" + ratereturn + "','" + specialnotes + "','" + fees + "','" + custodian + "','" + contact + "','" + address + "','" + administrators + "' ,'" + clientnumber + "')";
            SqlCommand cmd = new SqlCommand(query, conn);
            int a = cmd.ExecuteNonQuery();
            if (a == 1)
            {

                added = true;
            }
        }
        catch (Exception ex)
        {
            MsgBox("Error: " + ex.Message, this.Page, this);
            throw;
        }
        return added;


    }
    private void GetListData()
    {
        conn.Close();
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select id,name,date_of_take,rate_of_return,special_notes,fees from clients order by id desc ", conn);
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
    public void linkDiscard(object sender, System.EventArgs e)
    {
        string idd = ((LinkButton)sender).CommandArgument;
      SqlCommand  cmd = new SqlCommand("Delete from clients where Id='" + idd + "' ", conn);
        if ((conn.State == ConnectionState.Open))
            conn.Close();
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
        MsgBox("Delete Successful", this.Page, this);
        GetListData();

    }
    public void lnkedit(object sender, System.EventArgs e)
    {
        string idd = ((LinkButton)sender).CommandArgument;
        fetcheditadata(idd);

    }
    


    public void fetcheditadata(String id)
    {
        try
        {
            conn.Close();
            conn.Open();
            string Query = "SELECT * FROM clients WHERE id='" + id.ToString() + "' ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)
            {
                txtFirstName.Text = dr["name"].ToString();
                txtDate.Text= dr["date_of_take"].ToString();
                txtBenchmark.Text = dr["benchmark"].ToString();
                txtID.Text= dr["id"].ToString();
                txtFees.Text= dr["fees"].ToString();
                txtContactDetails.Text= dr["contact_details"].ToString();
                //txtIPS.Text = dr["IPS"].ToString();
                txtAddress.Text = dr["address"].ToString();

                usersPanel.Visible = true;
                grdpanel.Visible = false;
                Button1.Visible = false;
                Button2.Visible = true;
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        Button3.Visible = false;
        Boolean user = checkuser(txtFirstName.Text);
        if (user)
        {
            MsgBox("Client Already Exists", this.Page, this);

        }
        else
        {
            Boolean add = Adduser(txtFirstName.Text, txtDate.Text,txtReturn.Text,txtSpecialnotes.Text,txtFees.Text,cmbCustodians.SelectedItem.Text,txtContactDetails.Text,txtAddress.Text,cmbAdministrators.SelectedItem.Text, txtgenerator.Text);
            if (add)
            {
                MsgBox("Client  successfully added", this.Page, this);
                GetListData();
                grdpanel.Visible = true;
                usersPanel.Visible = false;
            }
        }
    }
    
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        grdpanel.Visible = false;
        usersPanel.Visible = true;
        txtFirstName.Text = "";
        txtFees.Text = "";
        txtBenchmark.Text = "";
        txtDate.Text = "";
        txtID.Text = "";
        txtSpecialnotes.Text = "";
        txtReturn.Text = "";
       // txtIPS.Text = "";
        txtContactDetails.Text = "";
        txtAddress.Text = "";
        Button2.Visible = false;
        Button1.Visible = true;
        
        generateclientid();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        String id = txtID.Text.ToString();
        Boolean edited = edituser( id);
        if (edited)
        {
            MsgBox("Update Successful", this.Page, this);
            GetListData();
            grdpanel.Visible = true;
            usersPanel.Visible = false;
        }
    }
    public Boolean edituser(string  id)
    {

        {
            SqlCommand cmd = new SqlCommand("update clients set name='" + txtFirstName.Text + "' ,date_of_take='" + txtDate.Text + "',rate_of_return='" + txtReturn.Text + "',special_notes='" + txtSpecialnotes.Text + "' ,fees='" + txtFees.Text + "' ,custodian= '" + cmbCustodians.SelectedItem.Text + "',contact_details='" + txtContactDetails.Text + "',address='" + txtAddress.Text + "',administrators='" + cmbAdministrators.SelectedItem.Text + "'  where id= '" + id + "'", conn);
            if ((conn.State == ConnectionState.Open))
                conn.Close();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            
        }
        return true;
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("clients.aspx");
    }

    protected void Unnamed2_Click(object sender, EventArgs e)
    {
        calendar1.Visible = true;

    }



    protected void calendar1_SelectionChanged(object sender, EventArgs e)
    {
        txtDate.Text = calendar1.SelectedDate.ToShortDateString();
        calendar1.Visible = false;
        //getassetclasses(txtgenerator.Text);
    }
    public void loadassetmanagers()
    {
        string com = "select  (surname ) as 'productname-price',id  from asset_managers";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        cmbAssetManager.DataSource = dt;
        cmbAssetManager.DataBind();
        cmbAssetManager.DataTextField = "productname-price";

        cmbAssetManager.DataValueField = "ID";
        cmbAssetManager.DataBind();


    }
    public void loadcustodians()
    {
        string com = "select * from custodians";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        cmbCustodians.DataSource = dt;
        cmbCustodians.DataBind();
        cmbCustodians.DataTextField = "name";

        cmbCustodians.DataValueField = "ID";
        cmbCustodians.DataBind();


    }
    public void loadAdministrators()
    {
        string com = "select * from administrators";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
       cmbAdministrators.DataSource = dt;
        cmbAdministrators .DataBind();
      cmbAdministrators.DataTextField = "name";

        cmbAdministrators.DataValueField = "ID";
        cmbAdministrators.DataBind();


    }
    public void generateclientid()
    {
        try
        {
            conn.Close();
            conn.Open();
            string Query = "select max(id)+ 1 as generator,id as optional from  Clients group by id ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)
            {
                //TextBox txtIdgenerator = new TextBox();
               
               
                    txtgenerator.Text = dr["generator"].ToString();

                }

            
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }

    protected void grdApps_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdApps.PageIndex = e.NewPageIndex;
        GetListData();
    }

    protected void addAssetmanager_Click(object sender, EventArgs e)
    {
        if (txtgenerator.Text == "")
        {
            txtgenerator.Text = "1";
        }

        List<string> li = new List<string>();
        foreach(ListItem item in cmbAssetManager.Items)
        {
            if (item.Selected)
            {
                li.Add(item.Value);

            }
           
        }
        recordinsert(li);
    }
    public void recordinsert(List<string> li)
    {
        //try
        {
            
            StringBuilder stringbi = new StringBuilder(String.Empty);
            foreach (string item in li)
            {
                
                const string query = "insert into client_assetmanager_relations(client_id,assetmanager_id) values";
                stringbi.AppendFormat("{0}('"+ txtgenerator.Text + "','{1}');", query,item);
            }
            conn.Close();
            conn.Open();
            //String query = "INSERT INTO clients(name,date_of_take,rate_of_return,special_notes,asset_manager,fees,custodian,ips,contact_details,address,administrators) values('" + firstname + "','" + date + "','" + ratereturn + "','" + specialnotes + "','" + assetmanager + "','" + fees + "','" + custodian + "','" + IPS + "','" + contact + "','" + address + "','" + administrators + "')";
            //SqlCommand cmd = new SqlCommand(query, conn);
            //cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //added = true;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = stringbi.ToString();
            cmd.Connection = conn;
          int a=  cmd.ExecuteNonQuery();
            if(a>=1)
            {
                MsgBox("asset managers  added", this.Page, this);
            }
        }
      

        //catch (Exception ex)
        //{
        //    MsgBox("Error: " + ex.Message, this.Page, this);
        //    throw;
        //}
    }
    //public void getassetclasses(String clientnumber)
    //{
    //    conn.Close();
    //    //try
    //    {
    //        // MsgBox("Error: ", this.Page, this);
    //        conn.Open();
    //        //String id;
    //        SqlCommand cmd = new SqlCommand("select * from  assets_class where active='1' ", conn);
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet("ds");
    //        da.Fill(ds);

    //        DataTable dt = ds.Tables[0];
    //        if (dt.Rows.Count > 0)
    //        {
    //            foreach (DataRow dr in dt.Rows)
    //            {

    //                // id = dr["id"].ToString();
    //                TextBox id = new TextBox();
    //                id.Text= dr["id"].ToString();

    //                TextBox assetname = new TextBox();
    //                assetname.Text = dr["asset_name"].ToString();

    //                insertclass(assetname.Text, id.Text, clientnumber);


    //                //tradeprice = Double.Parse(dr["TradePrice"].ToString());
    //                //tradeqty = int.Parse(dr["TradeQty"].ToString());
    //                //buyer = dr["Account1"].ToString();
    //                //seller = dr["Account2"].ToString();
    //                //conn.Close();
    //                conn.Dispose();
    //            }
    //        }
    //    }
    //}
    //public Boolean insertclass(String classname,String classid ,String clientnumber)
    //{
    //    Boolean added = false;
    //    try
    //    {

    //        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conpath"].ConnectionString);
    //        conn.Close();
    //        conn.Open();

    //        String query = " insert into client_assetclasses ( client_number , assetclass_name , assetclass_id ) values ('" + clientnumber + "' , '" + classname + "', '" + classid + "')";
    //        SqlCommand cmd = new SqlCommand(query, conn);
    //        cmd.ExecuteReader(CommandBehavior.CloseConnection);
    //        added = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        MsgBox("Error: " + ex.Message, this.Page, this);
    //        throw;
    //    }
    //    return added;


    //}

   


}