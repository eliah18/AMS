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
            GetListData(txtSearch.Text);

            loadcounter();
            //Boolean user=  checkcustodian(txtFirstName.Text, txtSurname.Text, txtUsername.Text);
            //  if(user)
            //  {
            //      MsgBox("User Already Exists", this.Page, this);

            //  }
        }
        
    }
    public Boolean checkcustodian(  String surname)
    {
        Boolean existance = false;
        conn.Close();
        try
             
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM para_company where fnam='" + surname + "'   ", conn);
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
    public Boolean addcustodian(String surname,String contactdetails,String property)
    {
        Boolean added = false;
        try
        {
            conn.Close();
            conn.Open();
            String query = "INSERT INTO para_company(fnam,company,symbol,Index_Type) values('" + surname + "','" + contactdetails + "','" + property + "','"+ cmbCounter.SelectedItem.Text +"')";
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
    private void GetListData(string company)
    {
        conn.Close();
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select id,Company,fnam as [Full Company Name] from para_company where Company like '%"+company+"%' order by id desc", conn);
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
      SqlCommand  cmd = new SqlCommand("Delete from para_company where Id='" + idd + "' ", conn);
        if ((conn.State == ConnectionState.Open))
            conn.Close();
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
        MsgBox("Delete Successful", this.Page, this);
        GetListData(txtSearch.Text);


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
            string Query = "SELECT * FROM para_company WHERE id='" + id.ToString() + "' ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)
            {
               
                txtSurname.Text= dr["fnam"].ToString();
                 txtID.Text= dr["id"].ToString();
               
                txtContactDetails.Text = dr["company"].ToString();
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

    protected void buttonsearch_Click(object sender, EventArgs e)
    {
        {
            if (txtSearch.Text != "")
            {
                string search = txtSearch.Text;
                string selectCommand = "SELECT* FROM para_company WHERE Company= " + search;




                grdApps.DataSource = selectCommand;
                grdApps.DataBind();
                txtSearch.Focus();


            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        String property=null;
        if (chkproperty.Checked == true)
        {
            property = "listedProperty";
        }
        Button3.Visible = false;
        Boolean user = checkcustodian( txtSurname.Text);
        if (user)
        {
            MsgBox("Counter Already Exists", this.Page, this);
            

        }
        else
        {
            Boolean add = addcustodian( txtSurname.Text,txtContactDetails.Text , property);
            if (add)
            {
                MsgBox("Counter successfully added", this.Page, this);
                GetListData(txtSearch.Text);
                grdpanel.Visible = true;
                usersPanel.Visible = false;
            }
        }
    }
    
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        grdpanel.Visible = false;
        usersPanel.Visible = true;
        
        txtSurname.Text = "";
       
        txtContactDetails.Text = "";
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
            GetListData(txtSearch.Text);
            grdpanel.Visible = true;
            usersPanel.Visible = false;
        }
    }
    public Boolean editcustodian(string  id)
    {

        {

            String property = null;
            if (chkproperty.Checked == true)
            {
                property = "listedProperty";
            }
            SqlCommand cmd = new SqlCommand("update para_company set fnam='" + txtSurname.Text + "',company='" + txtContactDetails.Text + "',symbol='" + property + "',Index_Type='"+cmbCounter.SelectedItem.Text+"'  where id= '" + id + "'", conn);
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
        Response.Redirect("Counters.aspx");
    }

    protected void grdApps_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdApps.PageIndex = e.NewPageIndex;
        GetListData(txtSearch.Text);
    }
    public void loadcounter()
    {


        cmbCounter.Items.Insert(0, new ListItem("Select Type", "0"));


    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        GetListData(txtSearch.Text);
    }
}