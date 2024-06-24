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



    String assetmanager;
    String subaccount;
    public void MsgBox(String ex, Page pg, Object obj)
    {
        string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
        Type cstype = obj.GetType();
        ClientScriptManager cs = pg.ClientScript;
        cs.RegisterClientScriptBlock(cstype, s, s.ToString());
    }
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conpath"].ConnectionString);
    //SqlConnection conprice = new SqlConnection(ConfigurationManager.ConnectionStrings["conprice"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //F.Chatz
            String id = Request.QueryString["data"];
            fetchassetclass(id);
            this.assetmanager = Request.QueryString["asset"];
            String clientid= Request.QueryString["clientid"];
            String assetclass = Request.QueryString["data"];
            String quarter = Request.QueryString["quarter"];
            String year = Request.QueryString["year"];
            this.subaccount = Request.QueryString["subaccount"];
            String date = null;
            Double removeinvestmentvalue = 0;

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
            txtListedPropertyDate.Text = date;



            loadlabel(quarter, year, fetchAssetManager(assetmanager), fetchclientName(clientid));
            fetchcurrenttotalmoneymarket(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            fetchcurrenttotalbonds(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            fetchcurrentlistedpropertysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            fetchforeignequitysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            fetchlistedEquitysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            fetchcurrentunlistedpropertysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            fetchcurrentalternativeinvestmentsum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            panelstoshow(id);
            loadunlistedequity();
            GetListData();
            GetBonds();
            GetUnlistedEquity();
            GetListedProperty();
            GetunListedProperty();
            GetAlternativeInvestment();
            fetchunlistedequitysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            //F.Chatz
            FetchCurrentCashSum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            GetCash();
            FetchCurrentNostroSum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            GetNostro();
            FetchGFundSum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            GetGFund();

            loadcounters();
            // GetUnlistedProperty();
            GetForeignEquity();
            GetMoneymarkets();
            loadbond();
            loadalternativeinvestments();
            loadforeignequity();
            loadlistedproperty();
            loadlistedequities();
            loadunlistedproperties();
            Button1.Visible = false;
            //Session["naku"] = id;


        }

    }
    protected void calendar1_SelectionChanged(object sender, EventArgs e)
    {
        txtDate.Text = calendar1.SelectedDate.ToString("dd MMMM yyyy");
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
            this.subaccount= Request.QueryString["subaccount"];
            conn.Open();
            String query = "  select z.id,z.counter,z.price,z.quantity as quantity,z.value from linked_assetclasses z join para_company b on z.counter= b.company  where z.client_id='" + clientid + "' and z.assetclass='" + assetclass + "' and z.assetmanager='" + assetmanager + "' and z.year='" + year + "' and z.quarter='" + quarter + "' and z.subaccount='" + this.subaccount + "'  ";
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
            this.subaccount = Request.QueryString["subaccount"];
            conn.Close();
            conn.Open();
            String query = " select distinct( z.id)  as  id,z.counter,z.value from linked_assetclasses z join  bonds_and_debentures b on z.counter= b.bondname  where z.client_id='" + clientid + "' and z.assetclass='" + assetclass + "' and z.assetmanager='" + assetmanager + "' and z.year='" + year + "' and z.quarter='" + quarter + "' and z.subaccount='" + this.subaccount + "' ";
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
    private void loadunlistedproperties()
    {


        conn.Close();
        conn.Open();
        string com = "select * from unlisted_property where active='1' and  propertyname in (select propertyname from assetmanager_unlistedclassholdings where assetmanager_id= '"+ this.assetmanager+ "' and  propertyclass='UnlistedProperty')";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        rdUnlistedProperty.DataSource = dt;
        rdUnlistedProperty.DataBind();
        rdUnlistedProperty.DataTextField = "propertyname";
        rdUnlistedProperty.DataValueField = "propertyname";
        rdUnlistedProperty.DataBind();
        rdUnlistedProperty.Items.Insert(0, new ListItem("Select Property", "0"));




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
        string com = "select * from para_company  where symbol is null or symbol=''";
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
    private void loadalternativeinvestments()
    {


        conn.Close();
        conn.Open();
        string com = "select * from alternative_investments where active='1' and  investmentname in (select propertyname from assetmanager_unlistedclassholdings where assetmanager_id= '" + this.assetmanager + "' and  propertyclass='AlternativeInvestment')";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        rdAlternativeInvestments.DataSource = dt;
        rdAlternativeInvestments.DataBind();
        rdAlternativeInvestments.DataTextField = "investmentname";
        rdAlternativeInvestments.DataValueField = "investmentname";
        rdAlternativeInvestments.DataBind();
        rdAlternativeInvestments.Items.Insert(0, new ListItem("Select Investment", "0"));

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
    private void loadunlistedequity()
    {


        conn.Close();
        conn.Open();
        string com = "select * from unlisted_equity where active='1' and equityname in  (select propertyname from assetmanager_unlistedclassholdings where assetmanager_id= '" + this.assetmanager + "' and  propertyclass='UnlistedEquity')";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        rdUnlistedEquity.DataSource = dt;
        rdUnlistedEquity.DataBind();
        rdUnlistedEquity.DataTextField = "equityname";
        rdUnlistedEquity.DataValueField = "equityname";
        rdUnlistedEquity.DataBind();
        rdUnlistedEquity.Items.Insert(0, new ListItem("Select Equity", "0"));




    }

    public void linkDiscard(object sender, System.EventArgs e)
    {
        string idd = ((LinkButton)sender).CommandArgument;
        String assetmanager = Request.QueryString["asset"];
        String clientid = Request.QueryString["clientid"];
        String assetclass = Request.QueryString["data"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"];
        // Double value = fetchDeletValue(idd);
        removeinvestment(idd);
        SqlCommand cmd = new SqlCommand("Delete from linked_assetclasses where Id='" + idd + "' ", conn);
        if ((conn.State == ConnectionState.Open))
            conn.Close();
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
        MsgBox("Delete Successful", this.Page, this);
        GetListData();
        GetBonds();
        GetUnlistedEquity();
        GetListedProperty();
        GetunListedProperty();
        GetCash();
        GetNostro();
        GetGFund();
        GetAlternativeInvestment();
        GetMoneymarkets();
        fetchcurrenttotalmoneymarket(clientid, year, quarter, txtAssetclass.Text, assetmanager);
        fetchcurrenttotalbonds(clientid, year, quarter, txtAssetclass.Text, assetmanager);
        fetchcurrentlistedpropertysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
        fetchforeignequitysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
        fetchlistedEquitysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
        fetchcurrentunlistedpropertysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
        FetchCurrentCashSum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
        FetchCurrentNostroSum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
        FetchGFundSum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
        fetchcurrentalternativeinvestmentsum(clientid, year, quarter, txtAssetclass.Text, assetmanager);


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
    public double  fetchDeletValue(String id)
    {
        Double removeinvestmentvalue = 0;
       // try
        {
            conn.Close();
            conn.Open();
           
            string Query = "select value from linked_assetclasses  where  id='" + id.ToString() + "' ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)


            {

                removeinvestmentvalue = Convert.ToDouble(dr["value"].ToString());

            }
            
        }
        //catch (Exception ex)
        //{
        //    conn.Close();
        //    MsgBox(ex.Message, this.Page, this);
        //}

        return removeinvestmentvalue;



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
            this.subaccount = Request.QueryString["subaccount"];
            conn.Open();
            String query = " select z.id, m.counterparty as [Counter], z.value as Value  from linked_assetclasses z join money_market_counters m on z.counter=m.counterparty where z.client_id='" + clientid + "' and z.assetclass='" + assetclass + "' and z.assetmanager='" + assetmanager + "' and z.year='" + year + "' and z.quarter='" + quarter + "' and z.subaccount='"+this.subaccount+"'";
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
        else if (id == "6")
        {
            Panel2.Visible = false;
            Panel1.Visible = false;
            Panel3.Visible = false;
            Panel6.Visible = false;
            unlistedproperty.Visible = true;

        }
        else if (id == "8")
        {
            Panel2.Visible = false;
            Panel1.Visible = false;
            Panel3.Visible = false;
            Panel6.Visible = false;
            unlistedproperty.Visible = false;
            alternativeinvestments.Visible = true;
        }
        else if (id == "9") //F.Chatz Cash
        {
            Panel2.Visible = false;
            Panel1.Visible = false;
            Panel3.Visible = false;
            Panel6.Visible = false;
            cash.Visible = true;

        }
        else if (id == "10005") //F.Chatz Nostro
        {
            Panel2.Visible = false;
            Panel1.Visible = false;
            Panel3.Visible = false;
            Panel6.Visible = false;
            CashNostro.Visible = true;

        }
        else if (id == "10") //F.Chatz G Fund
        {
            Panel2.Visible = false;
            Panel1.Visible = false;
            Panel3.Visible = false;
            Panel6.Visible = false;
            GuaranteedFund.Visible = true;

        }
        else if (id == "7")
        {
            Panel2.Visible = false;
            Panel1.Visible = false;
            Panel3.Visible = false;
            Panel6.Visible = false;
            unlistedproperty.Visible = false;
            alternativeinvestments.Visible = false;
            unlistedequity.Visible = true;


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
            this.subaccount = Request.QueryString["subaccount"];
            conn.Open();
            String query = " select z.id,z.counter,z.value from linked_assetclasses z join foreign_equities b on z.counter= b.company  where z.client_id='" + clientid + "' and z.assetclass='" + assetclass + "' and z.assetmanager='" + assetmanager + "'  and z.year='" + year + "' and z.quarter='" + quarter + "' and z.subaccount='" + this.subaccount + "' ";
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
        //try
        {

            String assetmanager = Request.QueryString["asset"];
            String clientid = Request.QueryString["clientid"];
            String assetclass = Request.QueryString["data"];
            String quarter = Request.QueryString["quarter"];
            String year = Request.QueryString["year"];
            this.subaccount = Request.QueryString["subaccount"];
            conn.Open();
            String query = " select z.id,z.counter,z.price,z.quantity,FORMAT(z.value,'#,0.00') as value from linked_assetclasses z join para_company b on z.counter= b.company  where z.client_id='" + clientid + "' and z.assetclass='" + assetclass + "' and z.assetmanager='" + assetmanager + "' and z.year='" + year + "' and z.quarter='" + quarter + "' and z.subaccount='"+this.subaccount+"'  ";
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
        //catch (Exception ex)
        //{
        //    conn.Close();
        //    MsgBox(ex.Message, this.Page, this);
        //}
    }
    public Boolean insertlinked(String clientid, String counter, String price, String quantity, String assetmanager, String Assetclass, String type, String value, String year, String quarter)
    {
        this.subaccount = Request.QueryString["subaccount"];
        Boolean added = false;
        try
        {
            conn.Close();
            conn.Open();
            double total;
			value=value.Replace(",","");

            if (value == "")
            {
                total = Convert.ToDouble(price) * Convert.ToDouble(quantity);
            }
            else
            {
                total = Convert.ToDouble(value);
            }


            String query = "INSERT INTO linked_assetclasses( counter,assetclass,price, quantity ,client_id,assetmanager , type,value,year,quarter,subaccount) values('" + counter + "','" + Assetclass + "','" + price + "','" + quantity + "' , '" + clientid + "','" + assetmanager + "','" + type + "','" + total + "','" + year + "', '" + quarter + "','" + this.subaccount + "')";
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

    public Boolean insertlinked2(String clientid, String counter, String price, String quantity, String assetmanager, String Assetclass, String type, String value, String year, String quarter)
    {
        this.subaccount = Request.QueryString["subaccount"];
        Boolean added = false;
        try
        {
            conn.Close();
            conn.Open();
            double total;
            value = value.Replace(",", "");

            
                total = Convert.ToDouble(value);


            String query = "INSERT INTO linked_assetclasses( counter,assetclass,price,quantity,client_id,assetmanager,type,value,year,quarter,subaccount) values('" + counter + "','" + Assetclass + "','" + price + "','" + quantity + "', '" + clientid + "','" + assetmanager + "','" + type + "','" + total + "','" + year + "', '" + quarter + "','" + this.subaccount + "')";
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
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"];
        fetchcurrenttotalmoneymarket(clientid, year, quarter, txtAssetclass.Text, assetmanager);
        //MsgBox( year, Page, this);
        if ((rdCounters.SelectedItem.Text == "Select CounterParty"))
        {
            MsgBox("select counterparty", Page, this);
            return;
        }
        if (txtValue.Text.Length == 0)
        {
            MsgBox("Enter Money Market Value", Page, this);
            return;
        }
        if (checkLinked(clientid, year, quarter, assetclass, assetmanager, rdCounters.SelectedItem.Text))
        {
            MsgBox(" Money Market Value Already Exists", Page, this);
            return;
        }

        if (insertlinked(clientid, rdCounters.SelectedItem.Text, "0", "0", assetmanager, assetclass,txtMoneyMarketType.Text, txtValue.Text, year, quarter))
        {

            if (checkInvestment(clientid, year, quarter, txtAssetclass.Text, assetmanager))
            {
                UpdateInvestment(clientid, txtAssetclass.Text, assetmanager, txtValue.Text, year, quarter, llbmoneymarketvaluetotal.Text);
            }
            else
            {
                InsertInvestment(clientid, txtAssetclass.Text, assetmanager, txtValue.Text, year, quarter);
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
        this.subaccount= Request.QueryString["subaccount"];
        try
        {
            conn.Close();
            conn.Open();
			value=value.Replace(",","");
            String query = "INSERT INTO Investments(client_id ,Asset_class_id, Asset_manager_id ,value ,year,quarter,subaccount ) values('" + clientid + "','" + assetclassid + "','" + assetmanagerid + "','" + value + "','" + year + "','" + quarter + "','" + this.subaccount + "')";
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
    public Boolean UpdateInvestment(String clientid, String assetclassid, String assetmanagerid, String value, String year, String quarter, String previousinvestments)
    {
        Boolean added = false;
        // try
        {
            this.subaccount = Request.QueryString["subaccount"];
            Double figure = Double.Parse(value);
            Double previousvalue = Double.Parse(previousinvestments);
            Double newfigure = figure + previousvalue;
            conn.Close();
            conn.Open();

            String query = "update  Investments set value=  '" + newfigure + "' where client_id='" + clientid + "'and quarter= '" + quarter + "' and year='" + year + "'  and Asset_manager_id = '" + assetmanagerid + "' and  Asset_class_id='" + txtAssetclass.Text + "' and subaccount='"+this.subaccount+"'";
           // MsgBox(query, this.Page, this);
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteReader(CommandBehavior.CloseConnection);
            added = true;
        }
        //catch (Exception ex)
        //{
        //    MsgBox("Error: " + ex.Message, this.Page, this);
        //   throw;
        //}
        return added;


    }


    public Boolean checkInvestment(String clientid, String year, String quarter, String assetclass, String assetmanager)
    {
        Boolean existance = false;
        conn.Close();
        try

        {
            this.subaccount = Request.QueryString["subaccount"];
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Investments where client_id='" + clientid + "' and  year='" + year + "' and  quarter ='" + quarter + "' and Asset_manager_id ='" + assetmanager + "' and Asset_class_id='" + assetclass + "'  and subaccount='"+this.subaccount+"'", conn);
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
            this.subaccount = Request.QueryString["subaccount"];
            conn.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from linked_assetclasses where counter='" + counter + "' and assetclass='" + assetclass + "' and assetmanager='" + assetmanager + "' and year='" + year + "' and quarter='" + quarter + "' and client_id= '" + clientid + "' and subaccount='"+this.subaccount+"'", conn);
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
    //------------------------------------------------------------------------------------------------------

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
            SqlCommand cmd = new SqlCommand("update linked_assetclasses set  value='" + cma + "',quantity='"+txtQuantity.Text+"',price='"+txtPrice.Text+"' where id= '" + id + "'", conn);
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
            Double sum = Double.Parse(llbmoneymarketvaluetotal.Text) + Double.Parse(txtValue.Text) - Double.Parse(txtupdateoriginalvalue.Text);
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
    public Boolean removeinvestment(string id)
    {

        {
            Double sum = fetchDeletValue(id);
            //Double.Parse(txtupdateoriginalvalue.Text);
            if (sum <= 0)
            {
                sum = 0;
            }
            
            String assetmanager = Request.QueryString["asset"];
            String clientid = Request.QueryString["clientid"];
            String assetclass = Request.QueryString["data"];
            String quarter = Request.QueryString["quarter"];
            String year = Request.QueryString["year"];
            

           // double cma = Convert.ToDouble(txtValue.Text);
           SqlCommand cmd = new SqlCommand("update investments set value=value -'" + sum + "' where client_id= '" + clientid + "'  and Asset_class_id='" + txtAssetclass.Text + "' and  Asset_manager_id = '"+assetmanager+ "'  and  quarter='"+ quarter + "' and year = '"+year+"' ", conn);
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
            Double sum = Double.Parse(Lbtotallistedequity.Text) + value - Double.Parse(txtupdateoriginalvalue.Text);
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
            Double value = Double.Parse(txtListedPropertyPrice.Text) * Double.Parse(txtListedPropertyValue.Text);
            Double sum = Double.Parse(lbtotalpropertyvalue.Text) + value - Double.Parse(txtupdateoriginalvalue.Text);
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
    public Boolean editunlistedpropertysum(string id)
    {

        {
            Double sum = Double.Parse(Label23.Text) + Double.Parse(txtUnlistedPropertyValue.Text) - Double.Parse(txtupdateoriginalvalue.Text);
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

    public Boolean editcashsum(string id)
    {

        {
            Double sum = Double.Parse(Label42.Text) + Double.Parse(txtCashValue.Text) - Double.Parse(txtupdateoriginalvalue.Text);
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

    public Boolean editnostrosum(string id)
    {

        {
            Double sum = Double.Parse(Label62.Text) + Double.Parse(txtCashNostroValue.Text) - Double.Parse(txtupdateoriginalvalue.Text);
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

    public Boolean editGFundsum(string id)
    {

        {
            Double sum = Double.Parse(Label52.Text) + Double.Parse(txtGFundValue.Text) - Double.Parse(txtupdateoriginalvalue.Text);
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
    public Boolean editAlternativeInvestmentsum(string id)
    {

        {
            Double sum = Double.Parse(lbAlternativeInvestmentTotal.Text) + Double.Parse(txtAlternativeInvestmentValue.Text) - Double.Parse(txtupdateoriginalvalue.Text);
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
            double cma = Convert.ToDouble(txtListedPropertyValue.Text) * Convert.ToDouble(txtListedPropertyPrice.Text) ;
            SqlCommand cmd = new SqlCommand("update linked_assetclasses set  value='" + cma + "' , quantity='" + txtListedPropertyValue.Text + "',price ='" + txtListedPropertyPrice.Text + "' where id= '" + id + "'", conn);
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
        insertlinked("tinashe", "1234", "0", "0", "tinashe", "chivaura", "1235", "54775hy7", "89", "1234555hchvchfv");

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
        if (insertlinked(clientid, rdBonds.SelectedItem.Text, "0", "0", assetmanager, assetclass, txtbondtype.Text, txtBondvalue.Text, year, quarter))
        {
            if (checkInvestment(clientid, year, quarter, txtAssetclass.Text, assetmanager))
            {
                UpdateInvestment(clientid, txtAssetclass.Text, assetmanager, txtBondvalue.Text, year, quarter, Lbtotalbondsvalue.Text);
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

               // txtPrice.Text = dr["price"].ToString();
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
        fetchlistedequityprice(rdlistedequities.SelectedItem.Value);

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
                txtupdateoriginalvalue.Text = dr["value"].ToString();

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
            string Query = " select z.id,z.counter as counter_id,z.value,z.price,z.value,z.quantity,b.company as company_id from linked_assetclasses z join para_company b on z.counter= b.company    where  z.id='" + id.ToString() + "' ";
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
               // txtListedPropertyPrice.Text = dr["price"].ToString();
                //txtQuantity.Text = dr["quantity"].ToString();
                txtListedPropertyValue.Text = dr["quantity"].ToString();
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
        fetchlistedlistedpropertyprice(rdListedProperty.SelectedItem.Value);

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
        if (insertlinked(clientid, rdForeignEquity.SelectedItem.Text, "0", "0", assetmanager, assetclass, txtForeignEquityType.Text, txtForeignEquityValue.Text, year, quarter))
        {
            {
                if (checkInvestment(clientid, year, quarter, txtAssetclass.Text, assetmanager))
                {
                    UpdateInvestment(clientid, txtAssetclass.Text, assetmanager, txtForeignEquityValue.Text, year, quarter, lbforeignequitysum.Text);
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
        if (IsNumeric(txtListedPropertyPrice.Text)==false)
        {
            MsgBox(" Price should be in correct format", Page, this);
            return;
        }
        Double value = Convert.ToDouble(txtListedPropertyPrice.Text) * Convert.ToDouble(txtListedPropertyValue.Text);
        if (insertlinked(clientid, rdListedProperty.SelectedItem.Value, txtListedPropertyPrice.Text, txtListedPropertyValue.Text, assetmanager, assetclass, txtListedPropertyType.Text, value.ToString(), year, quarter))
        {
            if (checkInvestment(clientid, year, quarter, txtAssetclass.Text, assetmanager))
            {
                UpdateInvestment(clientid, txtAssetclass.Text, assetmanager, value.ToString(), year, quarter, lbtotalpropertyvalue.Text);
            }
            else
            {
                InsertInvestment(clientid, txtAssetclass.Text, assetmanager, value.ToString(), year, quarter);
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
        if (txtListedPropertyPrice.Text != "")
        {  
            if (txtListedPropertyValue.Text !="")
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
            else
            {
                MsgBox("Please insert Quantity", this.Page, this);
            }
        } else
        {
            MsgBox("Please insert Price", this.Page, this);
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
            MsgBox("Enter Listed Equity Quantity", Page, this);
            return;
        }
        if (checkLinked(clientid, year, quarter, assetclass, assetmanager, rdlistedequities.SelectedItem.Text))
        {
            MsgBox(" Listed Equity  Value Already Exists", Page, this);
            return;
        }
        Double value = Convert.ToDouble(txtPrice.Text) * Convert.ToDouble(txtQuantity.Text);

        if (insertlinked(clientid, rdlistedequities.SelectedItem.Value, txtPrice.Text, txtQuantity.Text, assetmanager, assetclass, txtlistedEquityType.Text, value.ToString(), year, quarter))
        {
            {
                if (checkInvestment(clientid, year, quarter, txtAssetclass.Text, assetmanager))
                {
                    UpdateInvestment(clientid, txtAssetclass.Text, assetmanager, value.ToString(), year, quarter, Lbtotallistedequity.Text);
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
        String names = null;

        conn.Close();
        conn.Open();
        SqlCommand cmd = new SqlCommand("select * from para_company  where  fnam ='" + rdlistedequities.SelectedItem.Text + "' ", conn);

        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read() == true)
        {
            txtlistedEquityType.Text = dr["index_type"].ToString();
        }
        //MsgBox("hie", Page, this);
        txtlistedEquityType.Text = "Other";
        fetchlistedequityprice(rdlistedequities.SelectedItem.Value);
		txtQuantity.Text="0";
    }

    protected void pickdate_Click(object sender, EventArgs e)
    {
        calendar1.Visible = true;
        
    }
    public void fetchlistedequityprice(String company)
    {
        // try
        {
            conn.Close();
            conn.Open();

            string Query = " select ClosingPrice as price,convert(date, format(max(PriceDate),'yyyyMMdd','en-us')) as PriceDate from prices   where  company='" + company.ToString() + "'  and CONVERT(date,PriceDate)=convert(date,'" + txtDate.Text + "' )group by ClosingPrice";
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
                txtPrice.Text = dr["price"].ToString();
                txtListedPropertyPrice.Text= dr["price"].ToString();
                //txtunli= dr["price"].ToString();
                //Button1.Visible = false;
                //Button2.Visible = true;

            }
        }
    }
         public void fetchlistedlistedpropertyprice(String company)
    {
        // try
        {
            conn.Close();
            conn.Open();

            string Query = " select ClosingPrice as price,convert(date, format(max(PriceDate),'yyyyMMdd','en-us')) as PriceDate from prices   where  company='" + company.ToString() + "'  and CONVERT(date,PriceDate)='" + txtListedPropertyDate.Text + "' group by ClosingPrice";
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
                txtListedPropertyPrice.Text = dr["price"].ToString();
                //Button1.Visible = false;
                //Button2.Visible = true;

            }
        }
        //catch (Exception ex)
        //{
        //    conn.Close();
        //    MsgBox(ex.Message, this.Page, this);
        //}

    }
    public void fetchinvestmentupdateid(String clientid, String year, String quarter, String assetclass, String assetmanager)
    {
        try
        {
            conn.Close();
            conn.Open();
            string Query = "   select * from  Investments where Asset_class_id='" + assetclass + "' and Asset_manager_id='" + assetmanager + "' and year='" + year + "' and quarter='" + quarter + "' and client_id='" + clientid + "'";
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

                txtUpdateInvestmentId.Text = dr["id"].ToString();
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
            this.subaccount = Request.QueryString["subaccount"];
            conn.Close();
            conn.Open();
            string Query = "   select id , value from  Investments where Asset_class_id='" + assetclass + "' and Asset_manager_id='" + assetmanager + "' and year='" + year + "' and quarter='" + quarter + "' and client_id = '" + clientid + "' and subaccount= '" + this.subaccount + "'";
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
                llbmoneymarketvaluetotal.Text = dr["value"].ToString();

                double b = Convert.ToDouble((dr["value"].ToString()));
                if (b >= 1) {
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
            this.subaccount = Request.QueryString["subaccount"];
            conn.Close();
            conn.Open();
            string Query = "   select id , value from  Investments where Asset_class_id='" + assetclass + "' and Asset_manager_id='" + assetmanager + "' and year='" + year + "' and quarter='" + quarter + "' and client_id = '" + clientid + "' and subaccount= '"+this.subaccount+"'";
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
            this.subaccount = Request.QueryString["subaccount"];
            conn.Close();
            conn.Open();
            string Query = "   select id , value from  Investments where Asset_class_id='" + assetclass + "' and Asset_manager_id='" + assetmanager + "' and year='" + year + "' and quarter='" + quarter + "' and client_id = '" + clientid + "' and subaccount='"+this.subaccount+"'";
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
            this.subaccount = Request.QueryString["subaccount"];
            conn.Close();
            conn.Open();
            string Query = "   select id , value from  Investments where Asset_class_id='" + assetclass + "' and Asset_manager_id='" + assetmanager + "' and year='" + year + "' and quarter='" + quarter + "' and client_id = '" + clientid + "' and subaccount='" + this.subaccount+"'";
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
        this.subaccount = Request.QueryString["subaccount"];
        conn.Close();
        conn.Open();
        string Query = "   select id ,  value from  Investments where Asset_class_id='" + assetclass.ToString() + "' and Asset_manager_id='" + assetmanager + "' and year='" + year + "' and quarter='" + quarter + "' and client_id = '" + clientid + "' and subaccount='"+this.subaccount+"'";
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
        // MsgBox(rdBonds.SelectedItem.Text, Page, this);

        String names = null;

        conn.Close();
        conn.Open();
        SqlCommand cmd = new SqlCommand("select * from bonds_and_debentures  where bondname='" + rdBonds.SelectedItem.Text + "' and active='1'", conn);

        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read() == true)
        {
            txtbondtype.Text = dr["bondtype"].ToString();
        }

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


        if (txtPrice.Text != "") //(txtQuantity.Text) * Convert.ToDouble(txtPrice.Text)
        {
            if (txtQuantity.Text != "")
            {    
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
            else
            {
                MsgBox("Please insert Quality", this.Page, this);
            }
        }
        else
        {
            MsgBox("Please insert Price", this.Page, this);
        }
    }

    protected void editlisted_Click1(object sender, EventArgs e)
    {
        
        string idd = ((LinkButton)sender).CommandArgument;
        fetchlistedequitydata(idd);

    }
    public void loadlabel(String quarter, String year, String assetmanager, String client)
    {
        lblabel.Text = client + " " + "Under" + " " + assetmanager + " " + "Year" + " " + year + " " + "Quater" + quarter;
    }


    public string fetchclientName(String clientid)
    {

        String names = null;

        conn.Close();
        conn.Open();
        SqlCommand cmd = new SqlCommand("select name from clients where client_number='" + clientid + "'", conn);

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
        SqlCommand cmd = new SqlCommand("select surname from asset_managers where id ='" + assetmanagerid + "'", conn);

        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read() == true)
        {
            names = dr["surname"].ToString();
        }



        return names;

    }

    protected void rdUnlistedProperty_SelectedIndexChanged(object sender, EventArgs e)
    {
        String names = null;

        conn.Close();
        conn.Open();
        SqlCommand cmd = new SqlCommand("select * from unlisted_property  where  propertyname='" + rdUnlistedProperty.SelectedItem.Text + "' and active='1'", conn);

        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read() == true)
        {
            txtUnlistedPropertyType.Text = dr["propertytype"].ToString();
        }

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        String assetmanager = Request.QueryString["asset"];
        String clientid = Request.QueryString["clientid"];
        String assetclass = Request.QueryString["data"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"];
        fetchcurrentunlistedpropertysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
        if (rdUnlistedProperty.SelectedItem.Text == "Select ")
        {
            MsgBox("Select Property", Page, this);
            return;
        }
        if (txtUnlistedPropertyValue.Text.Length == 0)
        {
            MsgBox("Enter Property Value", Page, this);
            return;
        }
        if (checkLinked(clientid, year, quarter, assetclass, assetmanager, rdUnlistedProperty.SelectedItem.Text))
        {
            MsgBox(" Property  Value Already Exists", Page, this);
            return;
        }
        if (insertlinked(clientid, rdUnlistedProperty.SelectedItem.Text, "0", "0", assetmanager, assetclass, txtUnlistedPropertyType.Text, txtUnlistedPropertyValue.Text, year, quarter))
        {
            {
                if (checkInvestment(clientid, year, quarter, txtAssetclass.Text, assetmanager))
                {
                    UpdateInvestment(clientid, txtAssetclass.Text, assetmanager, txtUnlistedPropertyValue.Text, year, quarter, Label23.Text);
                }
                else
                {
                    InsertInvestment(clientid, txtAssetclass.Text, assetmanager, txtUnlistedPropertyValue.Text, year, quarter);
                }
                MsgBox("Property  Saved", Page, this);
                fetchcurrentunlistedpropertysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
                GetunListedProperty();
                fetchcurrentunlistedpropertysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            }
        }
        //    sum = sum + Convert.ToDouble(value.Text);
        //}


        //if (InsertInvestment(clientid, assetclass, assetmanager, sum.ToString()))
        //{

    }
    public void fetchcurrentunlistedpropertysum(String clientid, String year, String quarter, String assetclass, String assetmanager)
    {
        try
        {
            this.subaccount = Request.QueryString["subaccount"];
            conn.Close();
            conn.Open();
            string Query = "   select id , value from  Investments where Asset_class_id='" + assetclass + "' and Asset_manager_id='" + assetmanager + "' and year='" + year + "' and quarter='" + quarter + "' and client_id = '" + clientid + "' and subaccount='"+this.subaccount+"'";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)


            {
                txtInvestmentId.Text = dr["id"].ToString();
                Label23.Text = dr["value"].ToString();
                double b = Convert.ToDouble((dr["value"].ToString()));
                if (b >= 1)
                {
                    Label23.ForeColor = System.Drawing.Color.White;
                    Label23.BackColor = System.Drawing.Color.ForestGreen;
                }
                //MsgBox(llbmoneymarketvaluetotal.Text, Page, this);
                else if (Label23.Text == "0")
                {
                    Label23.ForeColor = System.Drawing.Color.White;
                    Label23.BackColor = System.Drawing.Color.Red;
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
    private void GetunListedProperty()

    {
        conn.Close();
        try
        {

            String assetmanager = Request.QueryString["asset"];
            String clientid = Request.QueryString["clientid"];
            String assetclass = Request.QueryString["data"];
            String quarter = Request.QueryString["quarter"];
            String year = Request.QueryString["year"];
            this.subaccount = Request.QueryString["subaccount"];

            conn.Open();
            String query = " select z.id,z.counter,z.value from linked_assetclasses z join unlisted_property b on z.counter= b.propertyname  where z.client_id='" + clientid + "' and z.assetclass='" + assetclass + "' and z.assetmanager='" + assetmanager + "'  and z.year='" + year + "' and z.quarter='" + quarter + "' and z.subaccount='" + this.subaccount + "'  ";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {


                    grdUnlistedProperty.DataSource = ds;
                    grdUnlistedProperty.DataBind();

                }
            }
            else
            {
                grdUnlistedProperty.DataSource = null;
                grdUnlistedProperty.DataBind();
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
    }
    //F.Cash

    protected void Button13_Click(object sender, EventArgs e)
    {
        String assetmanager = Request.QueryString["asset"];
        String clientid = Request.QueryString["clientid"];
        String assetclass = Request.QueryString["data"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"];
        FetchCurrentCashSum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
        
        if (txtCashValue.Text.Length == 0)
        {
            MsgBox("Enter Cash Value", Page, this);
            return;
        }
        //if (checkLinked(clientid, year, quarter, assetclass, assetmanager, rdUnlistedProperty.SelectedItem.Text))
        //{
        //    MsgBox(" Property  Value Already Exists", Page, this);
        //    return;
        //}
        if (insertlinked2(clientid," ", "0", "0", assetmanager, assetclass," ", txtCashValue.Text, year, quarter))
        {
            {
                if (checkInvestment(clientid, year, quarter, txtAssetclass.Text, assetmanager))
                {
                    UpdateInvestment(clientid, txtAssetclass.Text, assetmanager, txtCashValue.Text, year, quarter, Label42.Text);
                }
                else
                {
                    InsertInvestment(clientid, txtAssetclass.Text, assetmanager, txtCashValue.Text, year, quarter);
                }
                MsgBox("Cash  Saved", Page, this);
                FetchCurrentCashSum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
                GetCash();
                FetchCurrentCashSum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            }
        }
        
    }

    public void FetchCurrentCashSum(String clientid, String year, String quarter, String assetclass, String assetmanager)
    {
        try
        {
            this.subaccount = Request.QueryString["subaccount"];
            conn.Close();
            conn.Open();
            string Query = "   select id , value from  Investments where Asset_class_id='" + assetclass + "' and Asset_manager_id='" + assetmanager + "' and year='" + year + "' and quarter='" + quarter + "' and client_id = '" + clientid + "' and subaccount='" + this.subaccount + "'";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)


            {
                
                txtInvestmentId.Text = dr["id"].ToString();
                Label42.Text = dr["value"].ToString();
                double b = Convert.ToDouble((dr["value"].ToString()));
                if (b >= 1)
                {
                    Label42.ForeColor = System.Drawing.Color.White;
                    Label42.BackColor = System.Drawing.Color.ForestGreen;
                }
                
                else if (Label42.Text == "0")
                {
                    Label42.ForeColor = System.Drawing.Color.White;
                    Label42.BackColor = System.Drawing.Color.Red;
                }

            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }

    private void GetCash()

    {
        conn.Close();
        try
        {

            String assetmanager = Request.QueryString["asset"];
            String clientid = Request.QueryString["clientid"];
            String assetclass = Request.QueryString["data"];
            String quarter = Request.QueryString["quarter"];
            String year = Request.QueryString["year"];
            this.subaccount = Request.QueryString["subaccount"];

            conn.Open();
            String query = " select z.id,z.value from linked_assetclasses z  where z.client_id='" + clientid + "' and z.assetclass='" + assetclass + "' and z.assetmanager='" + assetmanager + "'  and z.year='" + year + "' and z.quarter='" + quarter + "' and z.subaccount='" + this.subaccount + "'  ";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    grdCash.DataSource = ds;
                    grdCash.DataBind();
                }
            }
            else
            {
                grdCash.DataSource = null;
                grdCash.DataBind();
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
    }

    protected void Button100_Click(object sender, EventArgs e)
    {
        String assetmanager = Request.QueryString["asset"];
        String clientid = Request.QueryString["clientid"];
        String assetclass = Request.QueryString["data"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"];
        FetchCurrentNostroSum(clientid, year, quarter, txtAssetclass.Text, assetmanager);

        if (txtCashNostroValue.Text.Length == 0)
        {
            MsgBox("Enter Cash Nostro Value", Page, this);
            return;
        }
        if (insertlinked2(clientid, " ", "0", "0", assetmanager, assetclass, " ", txtCashNostroValue.Text, year, quarter))
        {
            {
                if (checkInvestment(clientid, year, quarter, txtAssetclass.Text, assetmanager))
                {
                    UpdateInvestment(clientid, txtAssetclass.Text, assetmanager, txtCashNostroValue.Text, year, quarter, Label42.Text);
                }
                else
                {
                    InsertInvestment(clientid, txtAssetclass.Text, assetmanager, txtCashNostroValue.Text, year, quarter);
                }
                MsgBox("Cash Nostro Saved", Page, this);
                FetchCurrentNostroSum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
                GetNostro();
                FetchCurrentNostroSum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            }
        }

    }

    public void FetchCurrentNostroSum(String clientid, String year, String quarter, String assetclass, String assetmanager)
    {
        try
        {
            this.subaccount = Request.QueryString["subaccount"];
            conn.Close();
            conn.Open();
            string Query = "   select id , value from  Investments where Asset_class_id='" + assetclass + "' and Asset_manager_id='" + assetmanager + "' and year='" + year + "' and quarter='" + quarter + "' and client_id = '" + clientid + "' and subaccount='" + this.subaccount + "'";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)


            {

                txtInvestmentId.Text = dr["id"].ToString();
                Label62.Text = dr["value"].ToString();
                double b = Convert.ToDouble((dr["value"].ToString()));
                if (b >= 1)
                {
                    Label62.ForeColor = System.Drawing.Color.White;
                    Label62.BackColor = System.Drawing.Color.ForestGreen;
                }

                else if (Label62.Text == "0")
                {
                    Label62.ForeColor = System.Drawing.Color.White;
                    Label62.BackColor = System.Drawing.Color.Red;
                }

            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }

    private void GetNostro()

    {
        conn.Close();
        try
        {

            String assetmanager = Request.QueryString["asset"];
            String clientid = Request.QueryString["clientid"];
            String assetclass = Request.QueryString["data"];
            String quarter = Request.QueryString["quarter"];
            String year = Request.QueryString["year"];
            this.subaccount = Request.QueryString["subaccount"];

            conn.Open();
            String query = " select z.id,z.value from linked_assetclasses z  where z.client_id='" + clientid + "' and z.assetclass='" + assetclass + "' and z.assetmanager='" + assetmanager + "'  and z.year='" + year + "' and z.quarter='" + quarter + "' and z.subaccount='" + this.subaccount + "'  ";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    grdCashNostro.DataSource = ds;
                    grdCashNostro.DataBind();
                }
            }
            else
            {
                grdCashNostro.DataSource = null;
                grdCashNostro.DataBind();
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
    }


    protected void Button15_Click(object sender, EventArgs e)
    {
        String assetmanager = Request.QueryString["asset"];
        String clientid = Request.QueryString["clientid"];
        String assetclass = Request.QueryString["data"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"];
        FetchGFundSum(clientid, year, quarter, txtAssetclass.Text, assetmanager);

        if (txtGFundValue.Text.Length == 0)
        {
            MsgBox("Enter Guaranteed Fund", Page, this);
            return;
        }
        
        if (insertlinked2(clientid, " ", "0", "0", assetmanager, assetclass, " ", txtGFundValue.Text, year, quarter))
        {
            {
                if (checkInvestment(clientid, year, quarter, txtAssetclass.Text, assetmanager))
                {
                    UpdateInvestment(clientid, txtAssetclass.Text, assetmanager, txtGFundValue.Text, year, quarter, Label52.Text);
                }
                else
                {
                    InsertInvestment(clientid, txtAssetclass.Text, assetmanager, txtGFundValue.Text, year, quarter);
                }
                MsgBox("Guaranteed Fund Saved", Page, this);
                FetchGFundSum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
                GetGFund();
                FetchGFundSum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            }
        }

    }

    public void FetchGFundSum(String clientid, String year, String quarter, String assetclass, String assetmanager)
    {
        try
        {
            this.subaccount = Request.QueryString["subaccount"];
            conn.Close();
            conn.Open();
            string Query = "   select id , value from  Investments where Asset_class_id='" + assetclass + "' and Asset_manager_id='" + assetmanager + "' and year='" + year + "' and quarter='" + quarter + "' and client_id = '" + clientid + "' and subaccount='" + this.subaccount + "'";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)


            {

                txtInvestmentId.Text = dr["id"].ToString();
                Label52.Text = dr["value"].ToString();
                double b = Convert.ToDouble((dr["value"].ToString()));
                if (b >= 1)
                {
                    Label52.ForeColor = System.Drawing.Color.White;
                    Label52.BackColor = System.Drawing.Color.ForestGreen;
                }

                else if (Label52.Text == "0")
                {
                    Label52.ForeColor = System.Drawing.Color.White;
                    Label52.BackColor = System.Drawing.Color.Red;
                }

            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }

    private void GetGFund()

    {
        conn.Close();
        try
        {

            String assetmanager = Request.QueryString["asset"];
            String clientid = Request.QueryString["clientid"];
            String assetclass = Request.QueryString["data"];
            String quarter = Request.QueryString["quarter"];
            String year = Request.QueryString["year"];
            this.subaccount = Request.QueryString["subaccount"];

            conn.Open();
            String query = " select z.id,z.value from linked_assetclasses z  where z.client_id='" + clientid + "' and z.assetclass='" + assetclass + "' and z.assetmanager='" + assetmanager + "'  and z.year='" + year + "' and z.quarter='" + quarter + "' and z.subaccount='" + this.subaccount + "'  ";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    grdGFund.DataSource = ds;
                    grdGFund.DataBind();
                }
            }
            else
            {
                grdGFund.DataSource = null;
                grdGFund.DataBind();
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
    }

    public void fetchUnPropertyData(String id)
    {
        try
        {
            conn.Close();
            conn.Open();
            string Query = "select z.id,z.counter as counter_id,z.value from linked_assetclasses z join  unlisted_property b on z.counter= b.propertyname  where  z.id='" + id.ToString() + "'  ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)


            {
                
                rdUnlistedProperty.SelectedItem.Text = dr["counter_id"].ToString();

                txtUnlistedPropertyValue.Text = dr["value"].ToString();
                txtupdateoriginalvalue.Text = dr["value"].ToString();

                txtUnlistedPropertyId.Text = dr["id"].ToString();


                Button2.Visible = false;
                Button4.Visible = true;

            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }
    public void fetchCashData(String id)
    {
        try
        {
            conn.Close();
            conn.Open();
            string Query = " select z.id,z.value from linked_assetclasses z where  z.id='" + id.ToString() + "' ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)


            {
                
                txtCashValue.Text = dr["value"].ToString();
                txtupdateoriginalvalue.Text = dr["value"].ToString();

                txtCashValueID.Text = dr["id"].ToString();


                Button9.Visible = false;
                Button10.Visible = true;

            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }

    public void fetchNostroData(String id)
    {
        try
        {
            conn.Close();
            conn.Open();
            string Query = " select z.id,z.value from linked_assetclasses z where  z.id='" + id.ToString() + "' ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)


            {

                txtCashNostroValue.Text = dr["value"].ToString();
                txtupdateoriginalvalue.Text = dr["value"].ToString();

                txtCashNostroValueID.Text = dr["id"].ToString();


                Button98.Visible = false;
                Button99.Visible = true;

            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }

    public void fetchGFundData(String id)
    {
        try
        {
            conn.Close();
            conn.Open();
            string Query = " select z.id,z.value from linked_assetclasses z where  z.id='" + id.ToString() + "' ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)


            {

                txtGFundValue.Text = dr["value"].ToString();
                txtupdateoriginalvalue.Text = dr["value"].ToString();

                txtGFundValueID.Text = dr["id"].ToString();


                Button11.Visible = false;
                Button12.Visible = true;

            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }

    }

    public void fetchAlternativeInvestmentData(String id)
    {
        try
        {
            conn.Close();
            conn.Open();
            string Query = "select z.id,z.counter as counter_id,z.value from linked_assetclasses z join alternative_investments b on z.counter= b.investmentname  where  z.id='" + id.ToString() + "'  ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)


            {
                rdAlternativeInvestments.SelectedItem.Text = dr["counter_id"].ToString();

                txtAlternativeInvestmentValue.Text = dr["value"].ToString();
                txtupdateoriginalvalue.Text = dr["value"].ToString();

                txtAlternativeInvestmentId.Text = dr["id"].ToString();


                //usersPanel.Visible = true;
                Button5.Visible = false;
                Button6.Visible = true;
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

    public void fetchUnlistedEquityData(String id)
    {
        try
        {
            conn.Close();
            conn.Open();
            string Query = "select z.id,z.counter as counter_id,z.value from linked_assetclasses z join unlisted_equity b on z.counter= b.equityname  where  z.id='" + id.ToString() + "'  ";
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)


            {
                rdUnlistedEquity.SelectedItem.Text = dr["counter_id"].ToString();

                txtUnlistedEquityValue.Text = dr["value"].ToString();
                txtupdateoriginalvalue.Text = dr["value"].ToString();

                txtUnlistedEquityId.Text = dr["id"].ToString();


                //usersPanel.Visible = true;
                Button7.Visible = false;
                Button8.Visible = true;
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

    protected void unlistedproperty_Click(object sender, EventArgs e)
    {
        // MsgBox("hie", this.Page, this);
        string idd = ((LinkButton)sender).CommandArgument;
        fetchUnPropertyData(idd);

    }
    protected void cash_Click(object sender, EventArgs e)
    {
        // MsgBox("hie", this.Page, this);
        string idd = ((LinkButton)sender).CommandArgument;
        fetchCashData(idd);

    }

    protected void CashNostro_Click(object sender, EventArgs e)
    {
        // MsgBox("hie", this.Page, this);
        string idd = ((LinkButton)sender).CommandArgument;
        fetchNostroData(idd);

    }

    protected void GFund_Click(object sender, EventArgs e)
    {
        // MsgBox("hie", this.Page, this);
        string idd = ((LinkButton)sender).CommandArgument;
        fetchGFundData(idd);

    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        String id = txtUnlistedPropertyId.Text.ToString();
        Boolean edited = editunlistedproperty(id);
        String assetmanager = Request.QueryString["asset"];
        String clientid = Request.QueryString["clientid"];
        String assetclass = Request.QueryString["data"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"];
        if (edited)
        {

            fetchinvestmentupdateid(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            if (editunlistedpropertysum(txtInvestmentId.Text))
            {
                MsgBox("Update Successful", this.Page, this);
                fetchcurrentunlistedpropertysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
                GetunListedProperty();
                Button2.Visible = true;
                Button4.Visible = false;
            }
            // MsgBox("Update Successful", this.Page, this);


        }

    }


    public Boolean editunlistedproperty(string id)
    {

        {
            double cma = Convert.ToDouble(txtUnlistedPropertyValue.Text);
            SqlCommand cmd = new SqlCommand("update linked_assetclasses set counter='" + rdUnlistedProperty.SelectedItem.Text + "' ,value='" + cma + "' where id= '" + id + "'", conn);
            if ((conn.State == ConnectionState.Open))
                conn.Close();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        return true;
    }

    protected void Button14_Click(object sender, EventArgs e)
    {
        String id = txtCashValueID.Text.ToString();
        Boolean edited = editcash(id);
        String assetmanager = Request.QueryString["asset"];
        String clientid = Request.QueryString["clientid"];
        String assetclass = Request.QueryString["data"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"];
        if (edited)
        {

            fetchinvestmentupdateid(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            if (editcashsum(txtInvestmentId.Text))
            {
                MsgBox("Update Successful", this.Page, this);
                FetchCurrentCashSum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
                GetCash();
                Button9.Visible = true;
                Button10.Visible = false;
            }
            // MsgBox("Update Successful", this.Page, this);


        }

    }

    protected void Button101_Click(object sender, EventArgs e)//hello6
    {
        String id = txtCashNostroValueID.Text.ToString();
        Boolean edited = editnostro(id);
        String assetmanager = Request.QueryString["asset"];
        String clientid = Request.QueryString["clientid"];
        String assetclass = Request.QueryString["data"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"];
        if (edited)
        {

            fetchinvestmentupdateid(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            if (editnostrosum(txtInvestmentId.Text))
            {
                MsgBox("Update Successful", this.Page, this);
                FetchCurrentNostroSum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
                GetNostro();
                Button98.Visible = true;
                Button99.Visible = false;
            }
            // MsgBox("Update Successful", this.Page, this);


        }

    }


    protected void Button16_Click(object sender, EventArgs e)
    {
        String id = txtGFundValueID.Text.ToString();
        Boolean edited = editGFund(id);
        String assetmanager = Request.QueryString["asset"];
        String clientid = Request.QueryString["clientid"];
        String assetclass = Request.QueryString["data"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"];
        if (edited)
        {

            fetchinvestmentupdateid(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            if (editGFundsum(txtInvestmentId.Text))
            {
                MsgBox("Update Successful", this.Page, this);
                FetchGFundSum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
                GetGFund();
                Button11.Visible = true;
                Button12.Visible = false;
            }
        }

    }

    public Boolean editcash(string id)
    {

        {
            double cma = Convert.ToDouble(txtCashValue.Text);
            SqlCommand cmd = new SqlCommand("update linked_assetclasses set value='" + cma + "' where id= '" + id + "'", conn);
            if ((conn.State == ConnectionState.Open))
                conn.Close();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        return true;
    }

    public Boolean editnostro(string id)
    {

        {
            double cma = Convert.ToDouble(txtCashNostroValue.Text);
            SqlCommand cmd = new SqlCommand("update linked_assetclasses set value='" + cma + "' where id= '" + id + "'", conn);
            if ((conn.State == ConnectionState.Open))
                conn.Close();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        return true;
    }

    public Boolean editGFund(string id)
    {

        {
            double cma = Convert.ToDouble(txtGFundValue.Text);
            SqlCommand cmd = new SqlCommand("update linked_assetclasses set value='" + cma + "' where id= '" + id + "'", conn);
            if ((conn.State == ConnectionState.Open))
                conn.Close();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        return true;
    }


    public Boolean editAlternativeInvestements(string id)
    {

        {
            double cma = Convert.ToDouble(txtAlternativeInvestmentValue.Text);
            SqlCommand cmd = new SqlCommand("update linked_assetclasses set counter='" + rdAlternativeInvestments.SelectedItem.Text + "' ,value='" + cma + "' where id= '" + id + "'", conn);
            if ((conn.State == ConnectionState.Open))
                conn.Close();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        return true;
    }
    public Boolean editUnlistedEquity(string id)
    {

        {
            double cma = Convert.ToDouble(txtUnlistedEquityValue.Text);
            SqlCommand cmd = new SqlCommand("update linked_assetclasses set counter='" + rdUnlistedEquity.SelectedItem.Text + "' ,value='" + cma + "' where id= '" + id + "'", conn);
            if ((conn.State == ConnectionState.Open))
                conn.Close();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        return true;
    }

    protected void rdAlternativeInvestments_SelectedIndexChanged(object sender, EventArgs e)
    {
        String names = null;

        conn.Close();
        conn.Open();
        SqlCommand cmd = new SqlCommand("select * from alternative_investments  where  investmentname ='" + rdAlternativeInvestments.SelectedItem.Text + "' and active='1'", conn);

        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read() == true)
        {
            txtAlternativeInvestmentType.Text = dr["investmenttype"].ToString();
        }

    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        String assetmanager = Request.QueryString["asset"];
        String clientid = Request.QueryString["clientid"];
        String assetclass = Request.QueryString["data"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"];
        fetchcurrentunlistedpropertysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
        if (rdAlternativeInvestments.SelectedItem.Text == "Select ")
        {
            MsgBox("Select Investment", Page, this);
            return;
        }
        if (txtAlternativeInvestmentValue.Text.Length == 0)
        {
            MsgBox("Enter Investment Value", Page, this);
            return;
        }
        if (checkLinked(clientid, year, quarter, assetclass, assetmanager, rdAlternativeInvestments.SelectedItem.Text))
        {
            MsgBox(" Investment Already Exists", Page, this);
            return;
        }
        if (insertlinked(clientid, rdAlternativeInvestments.SelectedItem.Text, "0", "0", assetmanager, assetclass, txtAlternativeInvestmentType.Text, txtAlternativeInvestmentValue.Text, year, quarter))
        {
            {
                if (checkInvestment(clientid, year, quarter, txtAssetclass.Text, assetmanager))
                {
                    UpdateInvestment(clientid, txtAssetclass.Text, assetmanager, txtAlternativeInvestmentValue.Text, year, quarter, Label23.Text);
                }
                else
                {
                    InsertInvestment(clientid, txtAssetclass.Text, assetmanager, txtAlternativeInvestmentValue.Text, year, quarter);
                }
                MsgBox("Investment  Saved", Page, this);
                fetchcurrentalternativeinvestmentsum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
                GetAlternativeInvestment();
                fetchcurrentalternativeinvestmentsum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            }
        }
        //    sum = sum + Convert.ToDouble(value.Text);
        //}


        //if (InsertInvestment(clientid, assetclass, assetmanager, sum.ToString()))
        //{
    }
    private void GetAlternativeInvestment()

    {
        conn.Close();
        try
        {

            String assetmanager = Request.QueryString["asset"];
            String clientid = Request.QueryString["clientid"];
            String assetclass = Request.QueryString["data"];
            String quarter = Request.QueryString["quarter"];
            String year = Request.QueryString["year"];
            this.subaccount = Request.QueryString["subaccount"];

            conn.Open();
            String query = " select z.id,z.counter,z.value from linked_assetclasses z join alternative_investments b on z.counter= b.investmentname  where z.client_id='" + clientid + "' and z.assetclass='" + assetclass + "' and z.assetmanager='" + assetmanager + "'  and z.year='" + year + "' and z.quarter='" + quarter + "' and z.subaccount='" + this.subaccount + "'   ";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {






                    grdAlternativeInvestments.DataSource = ds;
                    grdAlternativeInvestments.DataBind();

                }
            }
            else
            {
                grdAlternativeInvestments.DataSource = null;
                grdAlternativeInvestments.DataBind();
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
    }
    private void GetUnlistedEquity()

    {
        conn.Close();
        try
        {

            String assetmanager = Request.QueryString["asset"];
            String clientid = Request.QueryString["clientid"];
            String assetclass = Request.QueryString["data"];
            String quarter = Request.QueryString["quarter"];
            String year = Request.QueryString["year"];
            this.subaccount= Request.QueryString["subaccount"];
            conn.Open();
            String query = " select z.id,z.counter,z.value from linked_assetclasses z join unlisted_equity b on z.counter= b.equityname  where z.client_id='" + clientid + "' and z.assetclass='" + assetclass + "' and z.assetmanager='" + assetmanager + "'  and z.year='" + year + "' and z.quarter='" + quarter + "' and z.subaccount='" + this.subaccount + "'  ";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {






                    grdUnlistedEquity.DataSource = ds;
                    grdUnlistedEquity.DataBind();

                }
            }
            else
            {
               grdUnlistedEquity.DataSource = null;
                grdUnlistedEquity.DataBind();
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            MsgBox(ex.Message, this.Page, this);
        }
    }
    public void fetchcurrentalternativeinvestmentsum(String clientid, String year, String quarter, String assetclass, String assetmanager)
    {
        try
        {
            this.subaccount = Request.QueryString["subaccount"];
            conn.Close();
            conn.Open();
            string Query = "   select id , value from  Investments where Asset_class_id='" + assetclass + "' and Asset_manager_id='" + assetmanager + "' and year='" + year + "' and quarter='" + quarter + "' and client_id = '" + clientid + "' and subaccount='"+this.subaccount+"'";
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
                lbAlternativeInvestmentTotal.Text = dr["value"].ToString();
                double b = Convert.ToDouble((dr["value"].ToString()));
                if (b >= 1)
                {
                    lbAlternativeInvestmentTotal.ForeColor = System.Drawing.Color.White;
                    lbAlternativeInvestmentTotal.BackColor = System.Drawing.Color.ForestGreen;
                }
                //MsgBox(llbmoneymarketvaluetotal.Text, Page, this);
                else if (lbAlternativeInvestmentTotal.Text == "0")
                {
                    lbAlternativeInvestmentTotal.ForeColor = System.Drawing.Color.White;
                    lbAlternativeInvestmentTotal.BackColor = System.Drawing.Color.Red;
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


    protected void alternativeinvementsid_Click(object sender, EventArgs e)
    {
        // MsgBox("hie", this.Page, this);
        string idd = ((LinkButton)sender).CommandArgument;
        fetchAlternativeInvestmentData(idd);

    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        String id = txtAlternativeInvestmentId.Text.ToString();
        Boolean edited = editAlternativeInvestements(id);
        String assetmanager = Request.QueryString["asset"];
        String clientid = Request.QueryString["clientid"];
        String assetclass = Request.QueryString["data"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"];
        if (edited)
        {

            fetchinvestmentupdateid(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            if (editAlternativeInvestmentsum(txtInvestmentId.Text))
            {
                MsgBox("Update Successful", this.Page, this);
                fetchcurrentalternativeinvestmentsum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
                GetAlternativeInvestment();
                Button5.Visible = true;
                Button6.Visible = false;
            }
            // MsgBox("Update Successful", this.Page, this);


        }
    }

    protected void rdUnlistedEquity_SelectedIndexChanged(object sender, EventArgs e)
    {
        String names = null;

        conn.Close();
        conn.Open();
        SqlCommand cmd = new SqlCommand("select * from unlisted_equity  where  equityname='" + rdUnlistedEquity.SelectedItem.Text + "' and active='1'", conn);

        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read() == true)
        {
            txtUnlistedEquityType.Text = dr["equitytype"].ToString();
        }

    }

    protected void Button7_Click(object sender, EventArgs e)
    {

        String assetmanager = Request.QueryString["asset"];
        String clientid = Request.QueryString["clientid"];
        String assetclass = Request.QueryString["data"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"];
        fetchcurrentunlistedpropertysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
        if (rdUnlistedEquity.SelectedItem.Text == "Select ")
        {
            MsgBox("Select Equity", Page, this);
            return;
        }
        if (txtUnlistedEquityValue.Text.Length == 0)
        {
            MsgBox("Enter Equity Value", Page, this);
            return;
        }
        if (checkLinked(clientid, year, quarter, assetclass, assetmanager, rdUnlistedEquity.SelectedItem.Text))
        {
            MsgBox(" Equity Already Exists", Page, this);
            return;
        }
        if (insertlinked(clientid, rdUnlistedEquity.SelectedItem.Text, "0", "0", assetmanager, assetclass, txtUnlistedEquityType.Text, txtUnlistedEquityValue.Text, year, quarter))
        {
            {
                if (checkInvestment(clientid, year, quarter, txtAssetclass.Text, assetmanager))
                {
                    UpdateInvestment(clientid, txtAssetclass.Text, assetmanager, txtUnlistedEquityValue.Text, year, quarter, lbtotalUnlistedEquty.Text);
                }
                else
                {
                    InsertInvestment(clientid, txtAssetclass.Text, assetmanager, txtUnlistedEquityValue.Text, year, quarter);
                }
                MsgBox("Investment  Saved", Page, this);
                fetchcurrentalternativeinvestmentsum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
                GetUnlistedEquity();
                fetchunlistedequitysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            }
        }
        //    sum = sum + Convert.ToDouble(value.Text);
        //}


        //if (InsertInvestment(clientid, assetclass, assetmanager, sum.ToString()))
        //{

    }
    public void fetchunlistedequitysum(String clientid, String year, String quarter, String assetclass, String assetmanager)
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
                lbtotalUnlistedEquty.Text = dr["value"].ToString();
                double b = Convert.ToDouble((dr["value"].ToString()));
                if (b >= 1)
                {
                    lbtotalUnlistedEquty.ForeColor = System.Drawing.Color.White;
                    lbtotalUnlistedEquty.BackColor = System.Drawing.Color.ForestGreen;
                }
                //MsgBox(llbmoneymarketvaluetotal.Text, Page, this);
                else if (lbtotalUnlistedEquty.Text == "0")
                {
                    lbtotalUnlistedEquty.ForeColor = System.Drawing.Color.White;
                    lbtotalUnlistedEquty.BackColor = System.Drawing.Color.Red;
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


    protected void unlistedequityid_Click(object sender, EventArgs e)
    {
        string idd = ((LinkButton)sender).CommandArgument;
        fetchUnlistedEquityData(idd);
    }

    protected void Button8_Click(object sender, EventArgs e)
    {
        String id = txtUnlistedEquityId.Text.ToString();
        Boolean edited = editUnlistedEquity(id);
        String assetmanager = Request.QueryString["asset"];
        String clientid = Request.QueryString["clientid"];
        String assetclass = Request.QueryString["data"];
        String quarter = Request.QueryString["quarter"];
        String year = Request.QueryString["year"];
        if (edited)
        {

            fetchinvestmentupdateid(clientid, year, quarter, txtAssetclass.Text, assetmanager);
            if (editUnlistedEquitysum(txtInvestmentId.Text))
            {
                MsgBox(txtInvestmentId.Text, this.Page, this);
                fetchunlistedequitysum(clientid, year, quarter, txtAssetclass.Text, assetmanager);
                GetUnlistedEquity();
                Button7.Visible = true;
                Button8.Visible = false;
            }
            // MsgBox("Update Successful", this.Page, this);


        }
    }
    public Boolean editUnlistedEquitysum(string id)
    {

        {
            Double sum = Double.Parse(lbtotalUnlistedEquty.Text) + Double.Parse(txtUnlistedEquityValue.Text) - Double.Parse(txtupdateoriginalvalue.Text);
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

   

    protected void rdCounters_SelectedIndexChanged(object sender, EventArgs e)
    {
        String names = null;

        conn.Close();
        conn.Open();
        SqlCommand cmd = new SqlCommand("select * from money_market_counters  where  counterparty ='" + rdCounters.SelectedItem.Text + "' and active='1'", conn);

        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read() == true)
        {
            txtMoneyMarketType.Text = dr["type"].ToString();
        }
    }
    protected void rdListedProperty_SelectedIndexChanged(object sender, EventArgs e)
    {
        String names = null;

        conn.Close();
        conn.Open();
        SqlCommand cmd = new SqlCommand("select * from para_company  where  fnam ='" + rdListedProperty.SelectedItem.Text + "' ", conn);

        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read() == true)
        {
            //txtListedPropertyType.Text = dr["index_type"].ToString();

            txtListedPropertyType.Text = "Other";
        }

        fetchlistedlistedpropertyprice(rdListedProperty.SelectedItem.Value);

    }

    protected void rdForeignEquity_SelectedIndexChanged(object sender, EventArgs e)
    {
        String names = null;

        conn.Close();
        conn.Open();
        SqlCommand cmd = new SqlCommand("select * from foreign_equities   where  company  ='" + rdForeignEquity.SelectedItem.Text + "' ", conn);

        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read() == true)
        {
            txtForeignEquityType.Text = dr["sector"].ToString();
        }

    }
    protected void grdlistedEquity_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdlistedEquity.PageIndex = e.NewPageIndex;
        GetListData();
    }
    public bool IsNumeric(string text)
    {
        double test=0.0;
        return double.TryParse(text, out test);
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        calendar2.Visible = true;
    }

    protected void calendar2_SelectionChanged(object sender, EventArgs e)
    {
        txtListedPropertyDate.Text = calendar2.SelectedDate.ToString("dd MMMM yyyy");
        calendar2.Visible = false;
    }

    //F.Chatz 
    protected void grdApps_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            if (e.Row.Cells[4].Text.Replace("&nbsp;", " ") != " ")
            {
                e.Row.Cells[4].Text = Decimal.Parse(e.Row.Cells[4].Text).ToString("n");
            }

        }

    }

    protected void grdlistedEquity_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[6].Text.Replace("&nbsp;", " ") != " ")
            {
                e.Row.Cells[6].Text = Decimal.Parse(e.Row.Cells[6].Text).ToString("n");
            }
        }

    }

    protected void grdbonds_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[4].Text.Replace("&nbsp;", " ") != " ")
            {
                e.Row.Cells[4].Text = Decimal.Parse(e.Row.Cells[4].Text).ToString("n");
            }
        }

    }

    protected void grdListedProperty_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[5].Text.Replace("&nbsp;", " ") != " ")
            {
                e.Row.Cells[5].Text = Decimal.Parse(e.Row.Cells[5].Text).ToString("n");
            }
        }

    }

    protected void grdforeignequity_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[4].Text.Replace("&nbsp;", " ") != " ")
            {
                e.Row.Cells[4].Text = Decimal.Parse(e.Row.Cells[4].Text).ToString("n");
            }
        }
    }

    protected void grdUnlistedProperty_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[4].Text.Replace("&nbsp;", " ") != " ")
            {
                e.Row.Cells[4].Text = Decimal.Parse(e.Row.Cells[4].Text).ToString("n");
            }
        }
    }

    protected void grdCash_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[3].Text.Replace("&nbsp;", " ") != " ")
            {
                e.Row.Cells[3].Text = Decimal.Parse(e.Row.Cells[3].Text).ToString("n");
            }
        }
    }

    protected void grdCashNostro_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[3].Text.Replace("&nbsp;", " ") != " ")
            {
                e.Row.Cells[3].Text = Decimal.Parse(e.Row.Cells[3].Text).ToString("n");
            }
        }
    }

    protected void grdGFund_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[3].Text.Replace("&nbsp;", " ") != " ")
            {
                e.Row.Cells[3].Text = Decimal.Parse(e.Row.Cells[3].Text).ToString("n");
            }
        }
    }

    protected void grdUnlistedEquity_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[4].Text.Replace("&nbsp;", " ") != " ")
            {
                e.Row.Cells[4].Text = Decimal.Parse(e.Row.Cells[4].Text).ToString("n");
            }

            if (e.Row.Cells[5].Text.Replace("&nbsp;", " ") != " ")
            {
                e.Row.Cells[5].Text = Decimal.Parse(e.Row.Cells[5].Text).ToString("n");
            }
        }
    }

    protected void grdAlternativeInvestments_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[4].Text.Replace("&nbsp;", " ") != " ")
            {
                e.Row.Cells[4].Text = Decimal.Parse(e.Row.Cells[4].Text).ToString("n");
            }

            if (e.Row.Cells[5].Text.Replace("&nbsp;", " ") != " ")
            {
                e.Row.Cells[5].Text = Decimal.Parse(e.Row.Cells[5].Text).ToString("n");
            }
        }
    }

}

