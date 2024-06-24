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
            GetListData();
            //loadcounter();
            loadcounter();
            loadassetmanagers();
            GetSelectedAssetManagers();


            //Boolean user=  checkcustodian(txtFirstName.Text, txtSurname.Text, txtUsername.Text);
            //  if(user)
            //  {
            //      MsgBox("User Already Exists", this.Page, this);

            //  }
        }
        
    }
    public Boolean checkprice(  String surname)
    {
        Boolean existance = false;
        conn.Close();
        try
             
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM alternative_investments where investmentname='" + surname + "'   ", conn);
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
    public Boolean addprice(String type,String name)
    {
        Boolean added = false;
        try
        {
            conn.Close();
            conn.Open();
            String query = "INSERT INTO alternative_investments(investmenttype , investmentname) values('" + type + "','" + name + "')";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteReader(CommandBehavior.CloseConnection);
            added = true;
        }
        catch (Exception ex)
        {
            MsgBox("Error: " + ex.Message, this.Page, this);
            
        }
        return added;


    }
    private void GetListData()
    {
        conn.Close();
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select id,investmenttype as [Investment Type],investmentname as [Investment Name]  from alternative_investments where active='1' order by id desc  ", conn);
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
      SqlCommand  cmd = new SqlCommand("update  alternative_investments set active='0' where Id='" + idd + "' ", conn);
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
       
        Panel1.Visible = true;
        GetSelectedAssetManagers();

    }
    


    public void fetcheditadata(String id)
    {
        try
        {
            conn.Close();
            conn.Open();
            string Query = "SELECT * FROM alternative_investments WHERE id='" + id.ToString() + "' and active='1' ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)
            {
               
                cmbCounter.SelectedValue= dr["investmenttype"].ToString();
                txtBond.Text = dr["investmentname"].ToString();
                 txtID.Text= dr["id"].ToString();
                
                
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
        Boolean user = checkprice( txtBond.Text);
        if (user)
        {
            MsgBox("Investment  Already Exists", this.Page, this);
            

        }
        else
        {
            if (cmbCounter.SelectedItem.Value == "0")
            {
                MsgBox("Please Select Investment Type", this.Page, this);
                return;
            }
            if (txtBond.Text == "0")
            {
                MsgBox("Enter Investment Name", this.Page, this);
                return;
            }

            Boolean add = addprice( cmbCounter.SelectedItem.Value,txtBond.Text);
            if (add)
            {
                MsgBox("Investment  successfully added", this.Page, this);
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
        Panel1.Visible = true;
        
        txtBond.Text = "";
        
        Button2.Visible = false;
        Button1.Visible = true;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        String id = txtID.Text.ToString();
        Boolean edited = editcustodian( id);
        if (edited)
        {
            MsgBox("Update Successful", this.Page, this);
            GetListData();
            grdpanel.Visible = true;
            usersPanel.Visible = false;
        }
    }
    public Boolean editcustodian(string  id)
    {

        {
            SqlCommand cmd = new SqlCommand("update alternative_investments set investmenttype ='" + cmbCounter.SelectedItem.Value + "', investmentname='" + txtBond.Text + "' where id= '" + id + "'", conn);
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
        Response.Redirect("AlternativeInvestments.aspx");
    }

    protected void grdApps_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdApps.PageIndex = e.NewPageIndex;
        GetListData();
    }
    public void loadcounter()
    {

        
        cmbCounter.Items.Insert(0, new ListItem("Select Investment Type", "0"));


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
    protected void addAssetmanager_Click(object sender, EventArgs e)
    {
       

        List<string> li = new List<string>();
        foreach (ListItem item in cmbAssetManager.Items)
        {
            if (item.Selected)
            {
                li.Add(item.Value);

            }

        }
        recordinsert(li);
        GetSelectedAssetManagers();

    }
    public void recordinsert(List<string> li)
    {
        //try
        {

            StringBuilder stringbi = new StringBuilder(String.Empty);
            foreach (string item in li)
            {

                const string query = "insert into assetmanager_unlistedclassholdings(propertyclass,propertyname,assetmanager_id) values";
                stringbi.AppendFormat("{0}('AlternativeInvestment','" + txtBond.Text + "','{1}');", query, item);
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
            int a = cmd.ExecuteNonQuery();
            if (a >= 1)
            {
                MsgBox("asset managers  added", this.Page, this);
            }
        }

    }
    private void GetSelectedAssetManagers()
    {
        conn.Close();
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(" select b.id,a.surname  as AssetManager  from asset_managers a join assetmanager_unlistedclassholdings b on a.id=b.assetmanager_id  where b.propertyclass='AlternativeInvestment' and b.active=1 and b.propertyname='"+txtBond.Text+"'   ", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    grdAssetmanagers.DataSource = ds;
                    grdAssetmanagers.DataBind();
                }
            }
            else
            {
                grdAssetmanagers.DataSource = null;
                grdAssetmanagers.DataBind();
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
    }
    public void linkDiscardAssetManagers(object sender, System.EventArgs e)
    {
        string idd = ((LinkButton)sender).CommandArgument;
        SqlCommand cmd = new SqlCommand("delete from assetmanager_unlistedclassholdings  where Id='" + idd + "' ", conn);
        if ((conn.State == ConnectionState.Open))
            conn.Close();
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
        MsgBox("Delete Successful", this.Page, this);
        GetListData();
        GetSelectedAssetManagers();


    }

}