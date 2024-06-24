using System;
using System.Collections.Generic;
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

public partial class admin_ClientStatus_ClientFundsLinked : System.Web.UI.Page
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


                String id = Request.QueryString["data"];
                fetchassetclass(id);
                String assetmanager = Request.QueryString["asset"];
                String clientid = Request.QueryString["clientid"];
                String assetclass = Request.QueryString["data"];
                String quarter = Request.QueryString["quarter"];
                String year = Request.QueryString["year"];
                String date = null;
                if (quarter == "1")
                {
                    date = "31 March" + " " + year;
                    //MsgBox(date, this.Page, this);
                }
                else if (quarter == "2") {
                    date = "30 June" + " " + year;
                }
                else if (quarter == "2")
                {
                    date = "30 June" + " " + year;
                }
                else if (quarter == "3")
                {
                    date = "30 September" + " " + year;
                }
                else
                {
                    date = "31 December " + " " + year;
                }

                txtDate.Text = date;



                loadlabel(quarter, year, fetchAssetManager(assetmanager), fetchclientName(clientid));
                fetchcurrenttotalmoneymarket(clientid, year, quarter, txtAssetclass.Text, assetmanager);
                fetchcurrenttotalbonds(clientid, year, quarter, txtAssetclass.Text, assetmanager);
                fetchcurrentlistedpropertysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
                fetchforeignequitysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
                fetchlistedEquitysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            panelstoshow(id);
            GetListData();
            GetBonds();
            GetListedProperty();
            loadcounters();
           // GetUnlistedProperty();
            GetForeignEquity();
            GetMoneymarkets();
            loadbond();
            loadforeignequity();
            loadlistedproperty();
            loadlistedequities();
            Button1.Visible = false;
                //Session["naku"] = id;

            
        }
      
    }
    protected void calendar1_SelectionChanged(object sender, EventArgs e)
    {
        txtDate.Text = calendar1.SelectedDate.ToShortDateString();
        calendar1.Visible = false;
        //getassetclasses(txtgenerator.Text);
    }
    private void GetListData()
    {
        conn.Close();
        try
        {
            String assetmanager = Request.QueryString["asset"];
            String clientid = Request.QueryString["clientid"];
            String assetclass = Request.QueryString["data"];
            String quarter = Request.QueryString["quarter"];
            String year = Request.QueryString["year"];
            conn.Open();
            String query = "  select z.id,z.counter,z.price,z.quantity as quantity,z.value from linked_assetclasses z join para_company b on z.counter= b.company  where z.client_id='" + clientid + "' and z.assetclass='" + assetclass + "' and z.assetmanager='" + assetmanager + "' and z.year='"+ year +"' and z.quarter='"+ quarter+"'   ";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {






                    grdlistedEquity.DataSource = ds;
                    grdlistedEquity.DataBind();

                }
            }
            else
            {
                grdlistedEquity.DataSource = null;
                grdlistedEquity.DataBind();
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
    }
    private void GetBonds()
    {
        {
            String assetmanager = Request.QueryString["asset"];
            String clientid = Request.QueryString["clientid"];
            String assetclass = Request.QueryString["data"];
            String quarter = Request.QueryString["quarter"];
            String year = Request.QueryString["year"];
            conn.Close();
            conn.Open();
            String query = " select z.id,z.counter,z.value from linked_assetclasses z join  bonds_and_debentures b on z.counter= b.bondname  where z.client_id='" + clientid + "' and z.assetclass='" + assetclass + "' and z.assetmanager='" + assetmanager + "' and z.year='" + year + "' and z.quarter='" + quarter + "' ";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {






                    grdbonds.DataSource = ds;
                    grdbonds.DataBind();

                }
            }
            else
            {
                grdbonds.DataSource = null;
                grdbonds.DataBind();
            }
        }
    
        
    }
    private void loadcounters()
    {


        conn.Close();
        conn.Open();
        string com = "select * from money_market_counters";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        rdCounters.DataSource = dt;
        rdCounters.DataBind();
        rdCounters.DataTextField = "counterparty";
        rdCounters.DataValueField = "counterparty";
        rdCounters.DataBind();
        rdCounters.Items.Insert(0, new ListItem("Select CounterParty", "0"));




    }
    private void loadlistedproperty()
    {


        conn.Close();
        conn.Open();
        string com = "select * from para_company where symbol='listedProperty'";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        rdListedProperty.DataSource = dt;
        rdListedProperty.DataBind();
        rdListedProperty.DataTextField = "fnam";
        rdListedProperty.DataValueField = "company";
        rdListedProperty.DataBind();
        rdListedProperty.Items.Insert(0, new ListItem("Select Company", "0"));

       




    }
    private void loadlistedequities()
    {


        conn.Close();
        conn.Open();
        string com = "select * from para_company ";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        rdlistedequities.DataSource = dt;
        rdlistedequities.DataBind();
        rdlistedequities.DataTextField = "fnam";
        rdlistedequities.DataValueField = "company";
        rdlistedequities.DataBind();
        rdlistedequities.Items.Insert(0, new ListItem("Select Company", "0"));






    }
    private void loadforeignequity()
    {


        conn.Close();
        conn.Open();
        string com = "select * from foreign_equities";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        rdForeignEquity.DataSource = dt;
        rdForeignEquity.DataBind();
        rdForeignEquity.DataTextField = "company";
        rdForeignEquity.DataValueField = "company";
        rdForeignEquity.DataBind();
        rdForeignEquity.Items.Insert(0, new ListItem("Select Counter", "0"));




    }
    private void loadbond()
    {


        conn.Close();
        conn.Open();
        string com = "select * from bonds_and_debentures";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        rdBonds.DataSource = dt;
        rdBonds.DataBind();
        rdBonds.DataTextField = "bondname";
        rdBonds.DataValueField = "id";
        rdBonds.DataBind();
        rdBonds.Items.Insert(0, new ListItem("Select Bond", "0"));




    }
    
    public void linkDiscard(object sender, System.EventArgs e)
    {
        string idd = ((LinkButton)sender).CommandArgument;
        SqlCommand cmd = new SqlCommand("Delete from linked_assetclasses where Id='" + idd + "' ", conn);
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
        String assetmanager = Request.QueryString["asset"];
        String clientid = Request.QueryString["clientid"];
        String assetclass = Request.QueryString["data"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"];
        //checkInvestment(clientid, year, quarter, txtAssetclass.Text, assetmanager);
        

    }



    public void fetcheditadata(String id)
    {
        try
        {
            conn.Close();
            conn.Open();
            string Query = "select z.id,z.counter as [counter_id], m.counterparty as [Counter], z.value as Value  from linked_assetclasses z join money_market_counters m on z.counter=m.counterparty  where  z.id='" + id.ToString() + "' ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)


            {
                //String taka = grdApps.SelectedRow.Cells[1].Text;
                //MsgBox(taka, this.Page, this);

                //txtFirstName.Text = dr["name"].ToString();
                //txtSurname.Text = dr["surname"].ToString();
                //txtBenchmark.Text = dr["benchmark"].ToString();
                //txtID.Text = dr["id"].ToString();
                //txtStrategy.Text = dr["strategy"].ToString();
                //txtPhilosophy.Text = dr["philosophy"].ToString();
                //txtContactDetails.Text = dr["contact_details"].ToString();
                //txtAddress.Text = dr["address"].ToString();

                rdCounters.SelectedValue = dr["Counter"].ToString();

                txtValue.Text = dr["value"].ToString();
                txtupdateoriginalvalue.Text = dr["value"].ToString();


                txtID.Text = dr["id"].ToString();


                //usersPanel.Visible = true;
                btnmoneymarket.Visible = false;
                editmoneymarket.Visible = true;
                //Button1.Visible = false;
                //Button2.Visible = true;

            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }
    private void GetMoneymarkets()
    {
        conn.Close();
        try
        {
            String assetmanager = Request.QueryString["asset"];
            String clientid = Request.QueryString["clientid"];
            String assetclass = Request.QueryString["data"];
            String quarter = Request.QueryString["quarter"];
            String year = Request.QueryString["year"];
            conn.Open();
            String query = " select z.id, m.counterparty as [Counter], z.value as Value  from linked_assetclasses z join money_market_counters m on z.counter=m.counterparty where z.client_id='"+ clientid +"' and z.assetclass='"+ assetclass+"' and z.assetmanager='"+ assetmanager+ "' and z.year='" + year + "' and z.quarter='" + quarter + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
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
    public void panelstoshow(String id)
    {
        if (id == "3")
        {
            Panel1.Visible = true;
            Panel2.Visible = false;
            Panel3.Visible = false;
        }
        else if (id == "2")
        {
            Panel2.Visible = true;
            Panel1.Visible = false;
            Panel3.Visible = false;

        }
        else if (id == "1")
        {
            Panel2.Visible = false;
            Panel1.Visible = false;
            Panel3.Visible = true;
            editmoneymarket.Visible = false;

        }
        else if (id == "4")
        {
            Panel2.Visible = false;
            Panel1.Visible = false;
            Panel3.Visible = false;
            Panel5.Visible = true;


        }
        else if (id == "5")
        {
            Panel2.Visible = false;
            Panel1.Visible = false;
            Panel3.Visible = false;
            Panel6.Visible = true;


        }



    }
    private void GetForeignEquity()

    {
        conn.Close();
        try
        {

            String assetmanager = Request.QueryString["asset"];
            String clientid = Request.QueryString["clientid"];
            String assetclass = Request.QueryString["data"];
            String quarter = Request.QueryString["quarter"];
            String year = Request.QueryString["year"];
            conn.Open();
            String query = " select z.id,z.counter,z.value from linked_assetclasses z join foreign_equities b on z.counter= b.company  where z.client_id='" + clientid + "' and z.assetclass='" + assetclass + "' and z.assetmanager='" + assetmanager + "'  and z.year='" + year + "' and z.quarter='" + quarter + "' ";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {






                    grdforeignequity.DataSource = ds;
                    grdforeignequity.DataBind();

                }
            }
            else
            {
                grdforeignequity.DataSource = null;
                grdforeignequity.DataBind();
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
    }
    private void GetListedProperty()

    {
        conn.Close();
        try
        {

            String assetmanager = Request.QueryString["asset"];
            String clientid = Request.QueryString["clientid"];
            String assetclass = Request.QueryString["data"];
            String quarter = Request.QueryString["quarter"];
            String year = Request.QueryString["year"];
            conn.Open();
            String query = " select z.id,z.counter,z.value from linked_assetclasses z join para_company b on z.counter= b.company  where z.client_id='" + clientid + "' and z.assetclass='" + assetclass + "' and z.assetmanager='" + assetmanager + "' and z.year='" + year + "' and z.quarter='" + quarter + "'  ";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {






                    grdListedProperty.DataSource = ds;
                    grdListedProperty.DataBind();

                }
            }
            else
            {
                grdListedProperty.DataSource = null;
                grdListedProperty.DataBind();
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
    }
    public Boolean insertlinked(String clientid, String counter, String price, String quantity, String assetmanager, String Assetclass, String type, String value , String year, String quarter)
    {
        Boolean added = false;
        try
        {
            conn.Close();
            conn.Open();
            double total;

            if (value == "")
            {
                total = Convert.ToDouble(price) * Convert.ToDouble(quantity);
            }
            else
            {
                total = Convert.ToDouble(value);
            }


            String query = "INSERT INTO linked_assetclasses( counter,assetclass,price, quantity ,client_id,assetmanager , type,value,year,quarter) values('" + counter + "','" + Assetclass + "','" + price + "','" + quantity + "' , '" + clientid + "','" + assetmanager + "','" + type + "','" + total + "','" + year + "', '" + quarter + "')";
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


    protected void Unnamed_Click(object sender, EventArgs e)
    {

        String assetmanager = Request.QueryString["asset"];
        String clientid = Request.QueryString["clientid"];
        String assetclass = Request.QueryString["data"];
        String quarter= Request.QueryString["quarter"];
        String year = Request.QueryString["year"];
        fetchcurrenttotalmoneymarket(clientid, year, quarter, txtAssetclass.Text, assetmanager);
        //MsgBox( year, Page, this);
        if ((rdCounters.SelectedItem.Text== "Select CounterParty"))
        {
            MsgBox("select counterparty", Page, this);
            return;
        }
        if (txtValue.Text.Length == 0)
        {
            MsgBox("Enter Money Market Value", Page, this);
            return;
        }
        if(checkLinked(clientid,year,quarter,assetclass,assetmanager,rdCounters.SelectedItem.Text))
        {
            MsgBox(" Money Market Value Already Exists", Page, this);
            return;
        }

      if( insertlinked(clientid, rdCounters.SelectedItem.Text, "0", "0", assetmanager, assetclass, "", txtValue.Text,year,quarter))
        { 

            if(checkInvestment(clientid,year,quarter, txtAssetclass.Text,assetmanager))
            {
                UpdateInvestment(clientid, txtAssetclass.Text, assetmanager, txtValue.Text, year, quarter);  
            }
            else
            {
                InsertInvestment(clientid, txtAssetclass.Text, assetmanager, txtValue.Text,year,quarter); 
            }
        MsgBox("Money Market Saved", Page, this);
            fetchcurrenttotalmoneymarket(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            GetMoneymarkets();
            //Button1.Visible = true;
        }
      
    }
    public Boolean InsertInvestment(String clientid, String assetclassid, String assetmanagerid, String value, String year, String quarter)
    {
        Boolean added = false;
        try
        {
            conn.Close();
            conn.Open();
            String query = "INSERT INTO Investments(client_id ,Asset_class_id, Asset_manager_id ,value ,year,quarter ) values('" + clientid + "','" + assetclassid + "','" + assetmanagerid + "','" + value + "','" + year + "','" + quarter + "')";
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
    public Boolean UpdateInvestment(String clientid, String assetclassid, String assetmanagerid, String value, String year, String quarter)
    {
        Boolean added = false;
        try
        {
            Double figure = Double.Parse(value);
            Double previousvalue = Double.Parse(txtUpdateInvestmentId.Text);
            Double newfigure = figure + previousvalue;
            conn.Close();
            conn.Open();
            String query = "update  Investments set value=  '"+ newfigure + "' where client_id='"+ clientid+ "'and quarter= '" + quarter + "' and year='"+ year + "'  and Asset_manager_id = '" + assetmanagerid + "' and  Asset_class_id='"+ txtAssetclass.Text+"'";
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


    public Boolean checkInvestment(String clientid, String year, String quarter, String assetclass,String assetmanager)
    {
        Boolean existance = false;
        conn.Close();
        try

        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Investments where client_id='" + clientid + "' and  year='" + year + "' and  quarter ='" + quarter + "' and Asset_manager_id ='" + assetmanager + "' and Asset_class_id='" + assetclass+ "' ", conn);
            int count = int.Parse(cmd.ExecuteScalar().ToString());
            if (count >= 1)
            {
                existance = true;

                fetchinvestmentupdateid(clientid, year, quarter, assetclass, assetmanager);
            }

        }



        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
        return existance;




    }
    public Boolean checkLinked(String clientid, String year, String quarter, String assetclass, String assetmanager, String counter)
    {
        Boolean existance = false;
        conn.Close();
        try

        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from linked_assetclasses where counter='"+counter +"' and assetclass='" +assetclass +"' and assetmanager='" +assetmanager+"' and year='"+ year +"' and quarter='" + quarter+ "' and client_id= '" + clientid + "'", conn);
            int count = int.Parse(cmd.ExecuteScalar().ToString());
            if (count >= 1)
            {
                existance = true;

                //fetchinvestmentupdateid(clientid, year, quarter, assetclass, assetmanager);
            }

        }



        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
        return existance;




    }















    protected void editmoneymarket_Click(object sender, EventArgs e)
    {
        String id = txtID.Text.ToString();
        Boolean edited = editmoneymarkets(id);
        String assetmanager = Request.QueryString["asset"];
        String clientid = Request.QueryString["clientid"];
        String assetclass = Request.QueryString["data"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"];

        if (edited)

        {
            fetchinvestmentupdateid(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            if (editmoneymoneymarketsum(txtInvestmentId.Text))
            {
                MsgBox("Update Successful", this.Page, this);
                fetchcurrenttotalmoneymarket(clientid, year, quarter, txtAssetclass.Text, assetmanager);
                GetMoneymarkets();
                btnmoneymarket.Visible = true;
                editmoneymarket.Visible = false;
            }
         

        }
    }
    public Boolean editmoneymarkets(string id)
    {

        {
            double cma = Convert.ToDouble(txtValue.Text);
            SqlCommand cmd = new SqlCommand("update linked_assetclasses set counter='" + rdCounters.SelectedItem.Text + "' ,value='" + cma + "' where id= '" + id + "'", conn);
            if ((conn.State == ConnectionState.Open))
                conn.Close();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        return true;
    }
    public Boolean editlistedequities(string id)
    {

        {
            double cma = Convert.ToDouble(txtQuantity.Text) * Convert.ToDouble(txtPrice.Text);
            SqlCommand cmd = new SqlCommand("update linked_assetclasses set  value='" + cma + "' where id= '" + id + "'", conn);
            if ((conn.State == ConnectionState.Open))
                conn.Close();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        return true;
    }

    public Boolean editmoneymoneymarketsum(string id)
    {

        {
            Double sum = Double.Parse(llbmoneymarketvaluetotal.Text) +  Double.Parse(txtValue.Text) - Double.Parse(txtupdateoriginalvalue.Text) ;
            //Double.Parse(txtupdateoriginalvalue.Text);
            if (sum <= 0)
            {
                sum = 0;
            }

            double cma = Convert.ToDouble(txtValue.Text);
            SqlCommand cmd = new SqlCommand("update investments set value='" + sum + "' where id= '" + id + "'", conn);
            if ((conn.State == ConnectionState.Open))
                conn.Close();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        return true;
    }
    public Boolean editlistedequitiessum(string id)
    {

        {
            Double value = Double.Parse(txtPrice.Text) * Double.Parse(txtQuantity.Text);
            Double sum = Double.Parse(llbmoneymarketvaluetotal.Text) + value - Double.Parse(txtupdateoriginalvalue.Text);
            //Double.Parse(txtupdateoriginalvalue.Text);
            if (sum <= 0)
            {
                sum = 0;
            }

            //double cma = Convert.ToDouble(txtValue.Text);
            SqlCommand cmd = new SqlCommand("update investments set value='" + sum + "' where id= '" + id + "'", conn);
            if ((conn.State == ConnectionState.Open))
                conn.Close();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        return true;
    }
    public Boolean editlistedpropertysum(string id)
    {

        {
            Double sum = Double.Parse(lbtotalpropertyvalue.Text) + Double.Parse(txtListedPropertyValue.Text) - Double.Parse(txtupdateoriginalvalue.Text);
            //Double.Parse(txtupdateoriginalvalue.Text);
            if (sum <= 0)
            {
                sum = 0;
            }

           // double cma = Convert.ToDouble(txtValue.Text);
            SqlCommand cmd = new SqlCommand("update investments set value='" + sum + "' where id= '" + id + "'", conn);
            if ((conn.State == ConnectionState.Open))
                conn.Close();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        return true;
    }
    public Boolean editforeignequitysum(string id)
    {

        {
            Double sum = Double.Parse(lbforeignequitysum.Text) + Double.Parse(txtForeignEquityValue.Text) - Double.Parse(txtupdateoriginalvalue.Text);
            //Double.Parse(txtupdateoriginalvalue.Text);
            if (sum <= 0)
            {
                sum = 0;
            }

            // double cma = Convert.ToDouble(txtValue.Text);
            SqlCommand cmd = new SqlCommand("update investments set value='" + sum + "' where id= '" + id + "'", conn);
            if ((conn.State == ConnectionState.Open))
                conn.Close();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        return true;
    }
    public Boolean editbondsum(string id)
    {

        {
            Double sum = Double.Parse(Lbtotalbondsvalue.Text) + Double.Parse(txtBondvalue.Text) - Double.Parse(txtupdateoriginalvalue.Text);
            //Double.Parse(txtupdateoriginalvalue.Text);
            if (sum <= 0)
            {
                sum = 0;
            }

            //double cma = Convert.ToDouble(txtValue.Text);
            SqlCommand cmd = new SqlCommand("update investments set value='" + sum + "' where id= '" + id + "'", conn);
            if ((conn.State == ConnectionState.Open))
                conn.Close();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        return true;
    }
    public Boolean editbond(string id)
    {

        {
            double cma = Convert.ToDouble(txtBondvalue.Text);
            SqlCommand cmd = new SqlCommand("update linked_assetclasses set counter='" + rdBonds.SelectedItem.Text + "' ,value='" + cma + "' where id= '" + id + "'", conn);
            if ((conn.State == ConnectionState.Open))
                conn.Close();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        return true;
    }
    public Boolean editforeignequities(string id)
    {

        {
            double cma = Convert.ToDouble(txtForeignEquityValue.Text);
            SqlCommand cmd = new SqlCommand("update linked_assetclasses set counter='" + rdForeignEquity.SelectedItem.Text + "' ,value='" + cma + "' where id= '" + id + "'", conn);
            if ((conn.State == ConnectionState.Open))
                conn.Close();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        return true;
    }
    public Boolean editlistedproperty(string id)
    {

        {
            double cma = Convert.ToDouble(txtListedPropertyValue.Text);
            SqlCommand cmd = new SqlCommand("update linked_assetclasses set counter='" + rdListedProperty.SelectedItem.Text + "' ,value='" + cma + "' where id= '" + id + "'", conn);
            if ((conn.State == ConnectionState.Open))
                conn.Close();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        return true;
    }





    protected void Button1_Click(object sender, EventArgs e)
    {
        insertlinked("tinashe", "1234", "0", "0", "tinashe", "chivaura","1235", "54775hy7", "89", "1234555hchvchfv");
        
    }

    protected void bonds_Click(object sender, EventArgs e)
    {
        String assetmanager = Request.QueryString["asset"];
        String clientid = Request.QueryString["clientid"];
        String assetclass = Request.QueryString["data"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"];

        fetchcurrenttotalbonds(clientid, year, quarter, txtAssetclass.Text, assetmanager);
        if ((rdBonds.SelectedItem.Text == "Select Bond"))
        {
            MsgBox("Select Bond", Page, this);
            return;
        }
        if (txtBondvalue.Text.Length == 0)
        {
            MsgBox("Enter Bond Value", Page, this);
            return;
        }
        if (checkLinked(clientid, year, quarter, assetclass, assetmanager, rdBonds.SelectedItem.Text))
        {
            MsgBox(" Bond  Value Already Exists", Page, this);
            return;
        }
        if ( insertlinked(clientid, rdBonds.SelectedItem.Text, "0", "0", assetmanager, assetclass,rdBonds.SelectedValue, txtBondvalue.Text, year, quarter))
        {
            if (checkInvestment(clientid, year, quarter, txtAssetclass.Text, assetmanager))
            {
                UpdateInvestment(clientid, txtAssetclass.Text, assetmanager, txtBondvalue.Text, year, quarter);
            }
            else
            {
                InsertInvestment(clientid, txtAssetclass.Text, assetmanager, txtBondvalue.Text, year, quarter);
            }
            MsgBox("Bond Saved", Page, this);
            fetchcurrenttotalbonds(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            GetBonds();
        }
        //    sum = sum + Convert.ToDouble(value.Text);
        //}


        //if (InsertInvestment(clientid, assetclass, assetmanager, sum.ToString()))
        //{

        
        MsgBox("Bond Saved", Page, this);
        GetBonds();
    }

    protected void bondsedit_Click(object sender, EventArgs e)
    {
        String id = txtBondId.Text.ToString();
       
       
        String assetmanager = Request.QueryString["asset"];
        String clientid = Request.QueryString["clientid"];
        String assetclass = Request.QueryString["data"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"];
        Boolean edited = editbond(id);
        if (edited)

        {
            fetchinvestmentupdateid(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            if (editbondsum(txtInvestmentId.Text))
            {
                MsgBox("Update Successful", this.Page, this);
                fetchcurrenttotalbonds(clientid, year, quarter, txtAssetclass.Text, assetmanager);
                GetBonds();
                bonds.Visible = true;
                bondsedit.Visible = false;
            }


        }

    }

    protected void bondedit_Click(object sender, EventArgs e)
    {
        string idd = ((LinkButton)sender).CommandArgument;
        fetchedboddata(idd);
    }
    public void fetchedboddata(String id)
    {
        try
        {
            conn.Close();
            conn.Open();
            string Query = "select z.id,z.counter as counter_id,z.value from linked_assetclasses z join  bonds_and_debentures b on z.counter= b.bondname   where  z.id='" + id.ToString() + "' ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)


            {
                //String taka = grdApps.SelectedRow.Cells[1].Text;
                //MsgBox(taka, this.Page, this);

                //txtFirstName.Text = dr["name"].ToString();
                //txtSurname.Text = dr["surname"].ToString();
                //txtBenchmark.Text = dr["benchmark"].ToString();
                //txtID.Text = dr["id"].ToString();
                //txtStrategy.Text = dr["strategy"].ToString();
                //txtPhilosophy.Text = dr["philosophy"].ToString();
                //txtContactDetails.Text = dr["contact_details"].ToString();
                //txtAddress.Text = dr["address"].ToString();

                rdBonds.SelectedItem.Text = dr["counter_id"].ToString();

                txtBondvalue.Text = dr["value"].ToString();
                txtupdateoriginalvalue.Text = dr["value"].ToString();

                txtBondId.Text = dr["id"].ToString();


                //usersPanel.Visible = true;
                bonds.Visible = false;
                bondsedit.Visible = true;
                //Button1.Visible = false;
                //Button2.Visible = true;

            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }
    public void fetchlistedequitydata(String id)
    {
        try
        {
            conn.Close();
            conn.Open();
            string Query = "select z.id,z.counter as counter_id,z.value,z.price,z.quantity from linked_assetclasses z    where  z.id='" + id.ToString() + "' ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)


            {
                //String taka = grdApps.SelectedRow.Cells[1].Text;
                //MsgBox(taka, this.Page, this);

                //txtFirstName.Text = dr["name"].ToString();
                //txtSurname.Text = dr["surname"].ToString();
                //txtBenchmark.Text = dr["benchmark"].ToString();
                //txtID.Text = dr["id"].ToString();
                //txtStrategy.Text = dr["strategy"].ToString();
                //txtPhilosophy.Text = dr["philosophy"].ToString();
                //txtContactDetails.Text = dr["contact_details"].ToString();
                //txtAddress.Text = dr["address"].ToString();

                rdlistedequities.SelectedItem.Text = dr["counter_id"].ToString();
                rdlistedequities.SelectedItem.Value = dr["counter_id"].ToString();

                txtPrice.Text = dr["price"].ToString();
                txtQuantity.Text = dr["quantity"].ToString();
                txtupdateoriginalvalue.Text = dr["value"].ToString();

                txtListedid.Text = dr["id"].ToString();


                //usersPanel.Visible = true;
                addlistedequity.Visible = false;
                Button3.Visible = true;
                //Button1.Visible = false;
                //Button2.Visible = true;

            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }
    public void fetchedforeigneqitydata(String id)
    {
        try
        {
            conn.Close();
            conn.Open();
            string Query = " select z.id,z.counter as counter_id,z.value from linked_assetclasses z join foreign_equities b on z.counter= b.company    where  z.id='" + id.ToString() + "' ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)


            {
                //String taka = grdApps.SelectedRow.Cells[1].Text;
                //MsgBox(taka, this.Page, this);

                //txtFirstName.Text = dr["name"].ToString();
                //txtSurname.Text = dr["surname"].ToString();
                //txtBenchmark.Text = dr["benchmark"].ToString();
                //txtID.Text = dr["id"].ToString();
                //txtStrategy.Text = dr["strategy"].ToString();
                //txtPhilosophy.Text = dr["philosophy"].ToString();
                //txtContactDetails.Text = dr["contact_details"].ToString();
                //txtAddress.Text = dr["address"].ToString();

                rdForeignEquity.SelectedItem.Text = dr["counter_id"].ToString();

                txtForeignEquityValue.Text = dr["value"].ToString();
                txtupdateoriginalvalue.Text= dr["value"].ToString();

                txtForeignEquityId.Text = dr["id"].ToString();


                //usersPanel.Visible = true;
                foreignequityadd.Visible = false;
                editforeignequity.Visible = true;
                //Button1.Visible = false;
                //Button2.Visible = true;

            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }
    public void fetchedlistedpropertydata(String id)
    {
        try
        {
            conn.Close();
            conn.Open();
            string Query = " select z.id,z.counter as counter_id,z.value,b.company as company_id from linked_assetclasses z join para_company b on z.counter= b.company    where  z.id='" + id.ToString() + "' ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)


            {
                //String taka = grdApps.SelectedRow.Cells[1].Text;
                //MsgBox(taka, this.Page, this);

                //txtFirstName.Text = dr["name"].ToString();
                //txtSurname.Text = dr["surname"].ToString();
                //txtBenchmark.Text = dr["benchmark"].ToString();
                //txtID.Text = dr["id"].ToString();
                //txtStrategy.Text = dr["strategy"].ToString();
                //txtPhilosophy.Text = dr["philosophy"].ToString();
                //txtContactDetails.Text = dr["contact_details"].ToString();
                //txtAddress.Text = dr["address"].ToString();

                rdListedProperty.SelectedItem.Text = dr["company_id"].ToString();
                rdListedProperty.SelectedItem.Value = dr["counter_id"].ToString();

                txtListedPropertyValue.Text = dr["value"].ToString();
                txtupdateoriginalvalue.Text = dr["value"].ToString();

                txtListedPropertyId.Text = dr["id"].ToString();


                //usersPanel.Visible = true;
                addlistedproperty.Visible = false;
                editlisted.Visible = true;
                //Button1.Visible = false;
                //Button2.Visible = true;

            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }

    protected void foreignequityadd_Click(object sender, EventArgs e)
    {
        String assetmanager = Request.QueryString["asset"];
        String clientid = Request.QueryString["clientid"];
        String assetclass = Request.QueryString["data"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"];
        fetchforeignequitysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
        if (rdForeignEquity.SelectedItem.Text == "Select Counter")
        {
            MsgBox("Select Counter", Page, this);
            return;
        }
        if (txtForeignEquityValue.Text.Length == 0)
        {
            MsgBox("Enter Foreign Equity Value", Page, this);
            return;
        }
        if (checkLinked(clientid, year, quarter, assetclass, assetmanager, rdForeignEquity.SelectedItem.Text))
        {
            MsgBox(" Foreign Equity  Value Already Exists", Page, this);
            return;
        }
        if (insertlinked(clientid, rdForeignEquity.SelectedItem.Text, "0", "0", assetmanager, assetclass, "", txtForeignEquityValue.Text, year, quarter))
        {
            {
                if (checkInvestment(clientid, year, quarter, txtAssetclass.Text, assetmanager))
                {
                    UpdateInvestment(clientid, txtAssetclass.Text, assetmanager, txtForeignEquityValue.Text, year, quarter);
                }
                else
                {
                    InsertInvestment(clientid, txtAssetclass.Text, assetmanager, txtForeignEquityValue.Text, year, quarter);
                }
                MsgBox("Equity  Saved", Page, this);
                fetchforeignequitysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
                GetForeignEquity();
                fetchcurrentlistedpropertysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            }
        }
        //    sum = sum + Convert.ToDouble(value.Text);
        //}


        //if (InsertInvestment(clientid, assetclass, assetmanager, sum.ToString()))
        //{
       

    }

    protected void editforeignequity_Click(object sender, EventArgs e)
    {
        String id = txtForeignEquityId.Text.ToString();
        Boolean edited = editforeignequities(id);
        String assetmanager = Request.QueryString["asset"];
        String clientid = Request.QueryString["clientid"];
        String assetclass = Request.QueryString["data"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"];
        if (edited)
        {

            fetchinvestmentupdateid(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            if (editforeignequitysum(txtInvestmentId.Text))
            {
                MsgBox("Update Successful", this.Page, this);
                fetchforeignequitysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
                GetForeignEquity();
                foreignequityadd.Visible = true;
                editforeignequity.Visible = false;
            }
           // MsgBox("Update Successful", this.Page, this);
           

        }

    }



    protected void foreigns_Click(object sender, EventArgs e)
    {
        string idd = ((LinkButton)sender).CommandArgument;
        fetchedforeigneqitydata(idd);
    }

    protected void addlistedproperty_Click(object sender, EventArgs e)
    {
        String assetmanager = Request.QueryString["asset"];
        String clientid = Request.QueryString["clientid"];
        String assetclass = Request.QueryString["data"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"];
        fetchcurrentlistedpropertysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
        if ((rdListedProperty.SelectedItem.Text == "Select Counter"))
        {
            MsgBox("Select Counter", Page, this);
            return;
        }
        if (txtListedPropertyValue.Text.Length == 0)
        {
            MsgBox("Enter Listed Property Value", Page, this);
            return;
        }
        if (checkLinked(clientid, year, quarter, assetclass, assetmanager, rdListedProperty.SelectedItem.Text))
        {
            MsgBox(" Listed Property  Value Already Exists", Page, this);
            return;
        }
        if(insertlinked(clientid, rdListedProperty.SelectedItem.Value, "0", "0", assetmanager, assetclass, "", txtListedPropertyValue.Text, year, quarter))
        {
            if (checkInvestment(clientid, year, quarter, txtAssetclass.Text, assetmanager))
            {
                UpdateInvestment(clientid, txtAssetclass.Text, assetmanager, txtListedPropertyValue.Text, year, quarter);
            }
            else
            {
                InsertInvestment(clientid, txtAssetclass.Text, assetmanager, txtListedPropertyValue.Text, year, quarter);
            }
            MsgBox("Listed Property Saved", Page, this);
            GetListedProperty();
            fetchcurrentlistedpropertysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
           
        }
        //    sum = sum + Convert.ToDouble(value.Text);
        //}


        //if (InsertInvestment(clientid, assetclass, assetmanager, sum.ToString()))
        //{
       
    }

    protected void editlisted_Click(object sender, EventArgs e)
    {
        String id = txtListedPropertyId.Text.ToString();
        Boolean edited = editlistedproperty(id);
        String assetmanager = Request.QueryString["asset"];
        String clientid = Request.QueryString["clientid"];
        String assetclass = Request.QueryString["data"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"];
        if (edited)
        {
            fetchinvestmentupdateid(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            if (editlistedpropertysum(txtInvestmentId.Text))
            {
                MsgBox("Update Successful", this.Page, this);
                fetchcurrentlistedpropertysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
                GetListedProperty();
                addlistedproperty.Visible = true;
                editlisted.Visible = false;
            }
            //MsgBox("Update Successful", this.Page, this);
           

        }

    }

    protected void listaproperty_Click(object sender, EventArgs e)
    {
        string idd = ((LinkButton)sender).CommandArgument;
        fetchedlistedpropertydata(idd);

    }

    protected void addlistedequity_Click(object sender, EventArgs e)
    {
        String assetmanager = Request.QueryString["asset"];
        String clientid = Request.QueryString["clientid"];
        String assetclass = Request.QueryString["data"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"];
        fetchlistedEquitysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
        if (rdlistedequities.SelectedItem.Text == "Select Company")
        {
            MsgBox("Select Counter", Page, this);
            return;
        }
        if (txtQuantity.Text.Length == 0)
        {
            MsgBox("Enter Listed Equity Value", Page, this);
            return;
        }
        if (checkLinked(clientid, year, quarter, assetclass, assetmanager, rdlistedequities.SelectedItem.Text))
        {
            MsgBox(" Listed Equity  Value Already Exists", Page, this);
            return;
        }
        Double value = Convert.ToDouble(txtPrice.Text) * Convert.ToDouble(txtQuantity.Text);

        if(insertlinked(clientid, rdlistedequities.SelectedItem.Value, txtPrice.Text, txtQuantity.Text, assetmanager, assetclass, "", value.ToString(), year, quarter))
        {
            {
                if (checkInvestment(clientid, year, quarter, txtAssetclass.Text, assetmanager))
                {
                    UpdateInvestment(clientid, txtAssetclass.Text, assetmanager, value.ToString(), year, quarter);
                }
                else
                {
                    InsertInvestment(clientid, txtAssetclass.Text, assetmanager, value.ToString(), year, quarter);
                }
                MsgBox("Equity  Saved", Page, this);
                fetchlistedEquitysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
                GetListData();
                fetchcurrentlistedpropertysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            }
        }
        //    sum = sum + Convert.ToDouble(value.Text);
        //}


        //if (InsertInvestment(clientid, assetclass, assetmanager, sum.ToString()))
        //{
        //MsgBox("Equity Saved", Page, this);
       

    }

    protected void rdlistedequities_SelectedIndexChanged(object sender, EventArgs e)
    {
        //MsgBox("hie", Page, this);
        fetchlistedequityprice(rdlistedequities.SelectedItem.Value);
    }

    protected void pickdate_Click(object sender, EventArgs e)
    {
        calendar1.Visible = true;
    }
    public void fetchlistedequityprice(String company)
    {
        try
        {
            conn.Close();
            conn.Open();
            string Query = " select * from listed_equities    where  company='" + company.ToString() + "'  and CONVERT(date,[date])='" + txtDate.Text + "'";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)


            {
                //String taka = grdApps.SelectedRow.Cells[1].Text;
                //MsgBox(taka, this.Page, this);

                //txtFirstName.Text = dr["name"].ToString();
                //txtSurname.Text = dr["surname"].ToString();
                //txtBenchmark.Text = dr["benchmark"].ToString();
                //txtID.Text = dr["id"].ToString();
                //txtStrategy.Text = dr["strategy"].ToString();
                //txtPhilosophy.Text = dr["philosophy"].ToString();
                //txtContactDetails.Text = dr["contact_details"].ToString();
                //txtAddress.Text = dr["address"].ToString();
                //txtPrice.Text = "";
               txtPrice.Text=dr["price"].ToString();
                //Button1.Visible = false;
                //Button2.Visible = true;

            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }
    public void fetchinvestmentupdateid(String clientid,String year, String quarter,String assetclass,String assetmanager)
    {
        try
        {
            conn.Close();
            conn.Open();
            string Query = "   select value from  Investments where Asset_class_id='"+ assetclass +"' and Asset_manager_id='"+ assetmanager+"' and year='"+ year+"' and quarter='"+ quarter + "' and client_id='" + clientid+ "'";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)


            {
                //String taka = grdApps.SelectedRow.Cells[1].Text;
                //MsgBox(taka, this.Page, this);

                //txtFirstName.Text = dr["name"].ToString();
                //txtSurname.Text = dr["surname"].ToString();
                //txtBenchmark.Text = dr["benchmark"].ToString();
                //txtID.Text = dr["id"].ToString();
                //txtStrategy.Text = dr["strategy"].ToString();
                //txtPhilosophy.Text = dr["philosophy"].ToString();
                //txtContactDetails.Text = dr["contact_details"].ToString();
                //txtAddress.Text = dr["address"].ToString();

                txtUpdateInvestmentId.Text = dr["value"].ToString();
                //Button1.Visible = false;
                //Button2.Visible = true;

            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }
    public void fetchcurrenttotalmoneymarket(String clientid, String year, String quarter, String assetclass, String assetmanager)
    {
        try
        {
            conn.Close();
            conn.Open();
            string Query = "   select id , value from  Investments where Asset_class_id='" + assetclass + "' and Asset_manager_id='" + assetmanager + "' and year='" + year + "' and quarter='" + quarter + "' and client_id = '"+ clientid +"'";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)


            {
                //String taka = grdApps.SelectedRow.Cells[1].Text;
                //MsgBox(taka, this.Page, this);

                //txtFirstName.Text = dr["name"].ToString();
                //txtSurname.Text = dr["surname"].ToString();
                //txtBenchmark.Text = dr["benchmark"].ToString();
                //txtID.Text = dr["id"].ToString();
                //txtStrategy.Text = dr["strategy"].ToString();
                //txtPhilosophy.Text = dr["philosophy"].ToString();
                //txtContactDetails.Text = dr["contact_details"].ToString();
                //txtAddress.Text = dr["address"].ToString();
                txtInvestmentId.Text= dr["id"].ToString();
                llbmoneymarketvaluetotal.Text = dr["value"].ToString();
               
                double b = Convert.ToDouble((dr["value"].ToString()));
                if (b>=1) { 
                    llbmoneymarketvaluetotal.ForeColor = System.Drawing.Color.White;
                    llbmoneymarketvaluetotal.BackColor = System.Drawing.Color.ForestGreen;
                }
                //MsgBox(llbmoneymarketvaluetotal.Text, Page, this);
                else if (llbmoneymarketvaluetotal.Text == "0")
                {
                    llbmoneymarketvaluetotal.ForeColor = System.Drawing.Color.White;
                    llbmoneymarketvaluetotal.BackColor = System.Drawing.Color.Red;
                }
                //Button1.Visible = false;
                //Button2.Visible = true;

            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }
    public void fetchcurrenttotalbonds(String clientid, String year, String quarter, String assetclass, String assetmanager)
    {
        try
        {
            conn.Close();
            conn.Open();
            string Query = "   select id , value from  Investments where Asset_class_id='" + assetclass + "' and Asset_manager_id='" + assetmanager + "' and year='" + year + "' and quarter='" + quarter + "' and client_id = '" + clientid + "'";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)


            {
                //String taka = grdApps.SelectedRow.Cells[1].Text;
                //MsgBox(taka, this.Page, this);

                //txtFirstName.Text = dr["name"].ToString();
                //txtSurname.Text = dr["surname"].ToString();
                //txtBenchmark.Text = dr["benchmark"].ToString();
                //txtID.Text = dr["id"].ToString();
                //txtStrategy.Text = dr["strategy"].ToString();
                //txtPhilosophy.Text = dr["philosophy"].ToString();
                //txtContactDetails.Text = dr["contact_details"].ToString();
                //txtAddress.Text = dr["address"].ToString();
                txtInvestmentId.Text = dr["id"].ToString();
                Lbtotalbondsvalue.Text = dr["value"].ToString();
                double b = Convert.ToDouble((dr["value"].ToString()));
                if (b >= 1)
                {
                    Lbtotalbondsvalue.ForeColor = System.Drawing.Color.White;
                    Lbtotalbondsvalue.BackColor = System.Drawing.Color.ForestGreen;
                }
                //MsgBox(llbmoneymarketvaluetotal.Text, Page, this);
                else if (llbmoneymarketvaluetotal.Text == "0")
                {
                    Lbtotalbondsvalue.ForeColor = System.Drawing.Color.White;
                    Lbtotalbondsvalue.BackColor = System.Drawing.Color.Red;
                }
                //Button1.Visible = false;
                //Button2.Visible = true;

            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }
    public void fetchcurrentlistedpropertysum(String clientid, String year, String quarter, String assetclass, String assetmanager)
    {
        try
        {
            conn.Close();
            conn.Open();
            string Query = "   select id , value from  Investments where Asset_class_id='" + assetclass + "' and Asset_manager_id='" + assetmanager + "' and year='" + year + "' and quarter='" + quarter + "' and client_id = '" + clientid + "'";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)


            {
                //String taka = grdApps.SelectedRow.Cells[1].Text;
                //MsgBox(taka, this.Page, this);

                //txtFirstName.Text = dr["name"].ToString();
                //txtSurname.Text = dr["surname"].ToString();
                //txtBenchmark.Text = dr["benchmark"].ToString();
                //txtID.Text = dr["id"].ToString();
                //txtStrategy.Text = dr["strategy"].ToString();
                //txtPhilosophy.Text = dr["philosophy"].ToString();
                //txtContactDetails.Text = dr["contact_details"].ToString();
                //txtAddress.Text = dr["address"].ToString();
                txtInvestmentId.Text = dr["id"].ToString();
                lbtotalpropertyvalue.Text = dr["value"].ToString();
                double b = Convert.ToDouble((dr["value"].ToString()));
                if (b >= 1)
                {
                    lbtotalpropertyvalue.ForeColor = System.Drawing.Color.White;
                    lbtotalpropertyvalue.BackColor = System.Drawing.Color.ForestGreen;
                }
                //MsgBox(llbmoneymarketvaluetotal.Text, Page, this);
                else if (lbtotalpropertyvalue.Text == "0")
                {
                    lbtotalpropertyvalue.ForeColor = System.Drawing.Color.White;
                    lbtotalpropertyvalue.BackColor = System.Drawing.Color.Red;
                }
                //Button1.Visible = false;
                //Button2.Visible = true;

            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }
    public void fetchforeignequitysum(String clientid, String year, String quarter, String assetclass, String assetmanager)
    {
        try
        {
            conn.Close();
            conn.Open();
            string Query = "   select id , value from  Investments where Asset_class_id='" + assetclass + "' and Asset_manager_id='" + assetmanager + "' and year='" + year + "' and quarter='" + quarter + "' and client_id = '" + clientid + "'";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)


            {
                //String taka = grdApps.SelectedRow.Cells[1].Text;
                //MsgBox(taka, this.Page, this);

                //txtFirstName.Text = dr["name"].ToString();
                //txtSurname.Text = dr["surname"].ToString();
                //txtBenchmark.Text = dr["benchmark"].ToString();
                //txtID.Text = dr["id"].ToString();
                //txtStrategy.Text = dr["strategy"].ToString();
                //txtPhilosophy.Text = dr["philosophy"].ToString();
                //txtContactDetails.Text = dr["contact_details"].ToString();
                //txtAddress.Text = dr["address"].ToString();
                txtInvestmentId.Text = dr["id"].ToString();
                lbforeignequitysum.Text = dr["value"].ToString();
                //int determinecolor = int.Parse(dr["value"].ToString());

                double b = Convert.ToDouble((dr["value"].ToString()));
                if (b >= 1)
                {
                    lbforeignequitysum.ForeColor = System.Drawing.Color.White;
                    lbforeignequitysum.BackColor = System.Drawing.Color.ForestGreen;
                }
                //MsgBox(llbmoneymarketvaluetotal.Text, Page, this);
                else if (lbforeignequitysum.Text == "0")
                {
                    lbforeignequitysum.ForeColor = System.Drawing.Color.White;
                    lbforeignequitysum.BackColor = System.Drawing.Color.Red;
                }
                //Button1.Visible = false;
                //Button2.Visible = true;

            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }
    public void fetchlistedEquitysum(String clientid, String year, String quarter, String assetclass, String assetmanager)
    {
        
            conn.Close();
            conn.Open();
            string Query = "   select id , value from  Investments where Asset_class_id='" + assetclass.ToString() + "' and Asset_manager_id='" + assetmanager + "' and year='" + year + "' and quarter='" + quarter + "' and client_id = '" + clientid + "'";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)


            {
                //String taka = grdApps.SelectedRow.Cells[1].Text;
                //MsgBox(taka, this.Page, this);

                //txtFirstName.Text = dr["name"].ToString();
                //txtSurname.Text = dr["surname"].ToString();
                //txtBenchmark.Text = dr["benchmark"].ToString();
                //txtID.Text = dr["id"].ToString();
                //txtStrategy.Text = dr["strategy"].ToString();
                //txtPhilosophy.Text = dr["philosophy"].ToString();
                //txtContactDetails.Text = dr["contact_details"].ToString();
                //txtAddress.Text = dr["address"].ToString();
                txtInvestmentId.Text = dr["id"].ToString();
                Lbtotallistedequity.Text = dr["value"].ToString();
            double b = Convert.ToDouble((dr["value"].ToString()));
            if (b >= 1)
            {
                Lbtotallistedequity.ForeColor = System.Drawing.Color.White;
                    Lbtotallistedequity.BackColor = System.Drawing.Color.ForestGreen;
                }
                //MsgBox(llbmoneymarketvaluetotal.Text, Page, this);
                else if (Lbtotallistedequity.Text == "0")
                {
                    Lbtotallistedequity.ForeColor = System.Drawing.Color.White;
                    Lbtotallistedequity.BackColor = System.Drawing.Color.Red;
                }
                //Button1.Visible = false;
                //Button2.Visible = true;

     }

    }
    public void fetchassetclass(String id)
    {
        try
        {
            conn.Close();
            conn.Open();
            string Query = " select * from assets_class   where  id='" + id + "' ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)


            {
                //String taka = grdApps.SelectedRow.Cells[1].Text;
                //MsgBox(taka, this.Page, this);

                //txtFirstName.Text = dr["name"].ToString();
                //txtSurname.Text = dr["surname"].ToString();
                //txtBenchmark.Text = dr["benchmark"].ToString();
                //txtID.Text = dr["id"].ToString();
                //txtStrategy.Text = dr["strategy"].ToString();
                //txtPhilosophy.Text = dr["philosophy"].ToString();
                //txtContactDetails.Text = dr["contact_details"].ToString();
                //txtAddress.Text = dr["address"].ToString();

                txtAssetclass.Text = dr["asset_name"].ToString();
                //Button1.Visible = false;
                //Button2.Visible = true;

            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }

    protected void rdBonds_SelectedIndexChanged(object sender, EventArgs e)
    {
        //MsgBox(rdBonds.SelectedItem.Text, Page, this);

       
        //conn.Close();
        //conn.Open();
        //string Query = "select z.id,z.counter as counter_id,z.value,z.price,z.quantity from linked_assetclasses z    where  z.id='" + id.ToString() + "' ";
        //SqlCommand cmd = new SqlCommand(Query, conn);
        //SqlDataReader dr = cmd.ExecuteReader();

        //if (dr.Read() == true)


        //{

        //    txtbondtype.Text= dr["name"].ToString();


        //}
            //String taka = grdApps.SelectedRow.Cells[1].Text;
            //MsgBox(taka, this.Page, this);

            //txtFirstName.Text = dr["name"].ToString();

        }

    protected void Button3_Click(object sender, EventArgs e)
    {
        String id = txtListedid.Text.ToString();
        Boolean edited = editlistedequities(id);
        String assetmanager = Request.QueryString["asset"];
        String clientid = Request.QueryString["clientid"];
        String assetclass = Request.QueryString["data"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"];

        if (edited)

        {
            fetchinvestmentupdateid(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            if (editlistedequitiessum(txtInvestmentId.Text))
            {
                MsgBox("Update Successful", this.Page, this);
                fetchlistedEquitysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
                GetListData();
                addlistedequity.Visible = true;
                Button3.Visible = false;
            }


        }

    }

    protected void editlisted_Click1(object sender, EventArgs e)
    {
        string idd = ((LinkButton)sender).CommandArgument;
        fetchlistedequitydata(idd);
       
    }
    public void loadlabel(String quarter, String year, String assetmanager, String client)
    {
        lblabel.Text =  client + " " + "Under" + " " + assetmanager + " " + "Year" + " " +year +" " + "Quater" + quarter;
    }


    public string  fetchclientName( String clientid)
    {

        String names=null;

        conn.Close();
        conn.Open();
        SqlCommand cmd= new SqlCommand("select name from clients where client_number='"+ clientid+"'" ,conn);

        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read() == true)
        {
            names = dr["name"].ToString();
        }



            return names;

    }
    public string fetchAssetManager(String assetmanagerid)
    {

        String names = null;

        conn.Close();
        conn.Open();
        SqlCommand cmd = new SqlCommand("select surname from asset_managers where id ='" + assetmanagerid+ "'", conn);

        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read() == true)
        {
            names = dr["surname"].ToString();
        }



        return names;

    }
}

