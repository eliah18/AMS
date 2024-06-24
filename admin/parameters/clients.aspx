<%@ Page Title="" Language="C#" MasterPageFile="~/admin/parameters/Investor.master" AutoEventWireup="true" CodeFile="clients.aspx.cs" Inherits="admin_parameters_User" %>
<%@ Import Namespace ="System.Data" %>
<%@ Import Namespace ="System.Data.SqlClient" %>
<%@ Import Namespace ="System.Configuration.Provider" %>
<%@ Import Namespace ="System.Text" %>
<%@ Import Namespace ="System.Configuration" %>





<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
.center {
  text-align: center;
 
}
</style>
    <script runat = "server">
        String cs = ConfigurationManager.ConnectionStrings["conpath"].ConnectionString;


        protected DataSet getAssesttypes()

        {

            // String cs = ConfigurationManager.ConnectionStrings["conpath"].ConnectionString;
            SqlConnection con = new SqlConnection((cs));
            con.Open();

            SqlDataAdapter da= new SqlDataAdapter("select * from administrators ",con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            return ds;


        }
        
        public SqlDataReader getdata()
        {
            SqlConnection con = new SqlConnection((cs));
            con.Open();

            string Query = "SELECT name from administrators ";
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }
        public DataTable selectedassestmanagers()

        {
            SqlConnection con = new SqlConnection((cs));
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand(" select name from administrators ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet("ds");
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            return dt;

        }
        public SqlCommand selectedMnagers()

        {
            SqlConnection con = new SqlConnection((cs));
            con.Open();
            SqlCommand cmd = new SqlCommand(" select * from administrators where id='3' ", con);
            


            return cmd;

        }







      </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
     <div class="page-breadcrumb">
                <div class="row">
                    <div class="col-12 d-flex no-block align-items-center">
                        <h4 class="page-title"> Clients </h4><br />
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">[Add]</asp:LinkButton>
                        <div class="ml-auto text-right">
                            <nav aria-label="breadcrumb">
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><asp:Label ID="lbUsername" runat="server" ></asp:Label></li>
                                   
                                     <li class="breadcrumb-item"><a href="~/logout.aspx" runat="server">Logout</a></li>
                                </ol>
                            </nav>
                        </div>
                    </div>
                </div>
            </div>
      <div class="container-fluid">
          <asp:Panel ID="grdpanel" Visible="true" runat="server">
           <div class="row">
               <div class="col-10">
                        <div class="card">
                            <div class="card-body">
              <asp:GridView ID="grdApps" runat="server"  CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No Users!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left" AllowPaging="True" OnPageIndexChanging="grdApps_PageIndexChanging">
                  <AlternatingRowStyle BackColor="White" CssClass="altrowstyle" />
                <EditRowStyle BackColor="#2461BF" />
                <EmptyDataRowStyle CssClass="text-warning text-center" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" CssClass="header info" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" CssClass="rowstyle" />
               
                   <Columns>
                   
              
                   <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton OnClientClick="if (!confirm('Are you sure you want delete?')) return false;" ID="lnkDiscard" runat="server" CommandArgument="<%# Bind('id') %>" OnClick="linkDiscard">Delete</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton  ID="lnkedit" runat="server" CommandArgument="<%# Bind('id') %>" OnClick="lnkedit">Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkdelete" runat="server" CommandName="Delete" >Delete</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
              </asp:GridView>
          </div>
                            </div>
                   </div>
          </div>
              </asp:Panel>
          <asp:Panel ID="usersPanel" Visible="false" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card">
                             <%--<form class="form-horizontal">--%>
                                <div class="card-body">
                                    <h4 class="card-title">Clients Info</h4>
                                    <div class="form-group row">
                                        <asp:Label ID="Label1" runat="server" Text="Client Name" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtFirstName"  class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        <asp:Label ID="Label13" runat="server" Text="Date of Take" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            
                                           <asp:TextBox ID="txtDate"   class="form-control" runat="server" AutoPostBack="True"></asp:TextBox>
                                          <asp:LinkButton runat="server" OnClick="Unnamed2_Click" class="col-sm-3 text-right control-label col-form-label">Pick Date</asp:LinkButton>
                                            <asp:Calendar id="calendar1" runat="server" visible="false" OnSelectionChanged="calendar1_SelectionChanged"></asp:Calendar> 
                                            </div>
                                        </div>
                                    
                                     <div class="form-group row">
                                        <asp:Label ID="Label3" runat="server" Text="Principal Officer" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtBenchmark"  class="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtID"  class="form-control" runat="server" Visible="false"></asp:TextBox>
                                            </div>
                                          <asp:Label ID="Label4" runat="server" Text="Fees per Client" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtFees"  class="form-control" runat="server"></asp:TextBox>
                                             <asp:TextBox ID="txtgenerator"  class="form-control" runat="server" Visible="false"></asp:TextBox>
                                            </div>
                                        </div>
                                   
                                    <div class="form-group row">
                                        <asp:Label ID="Label5" runat="server" Text="Special Notes" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtSpecialnotes"  class="form-control" runat="server"></asp:TextBox>
                                            
                                            </div>
                                          <asp:Label ID="Label6" runat="server" Text="Rate of Return" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtReturn"  class="form-control" runat="server"></asp:TextBox>
                                            
                                            </div>
                                        </div>
                                    <div class="form-group row">
                                        <asp:Label ID="Label11" runat="server" Text="Contact Details" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtContactDetails"  class="form-control" runat="server"></asp:TextBox>
                                            
                                            </div>
                                         <asp:Label ID="Label12" runat="server" Text="Address" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtAddress"  class="form-control" runat="server"></asp:TextBox>
                                            
                                            </div>

                                        </div>
                                     
                                    
                                     
                                     
                                    
                                     
                                    <div class="form-group row">
                                        <asp:Label ID="Label9" runat="server" Text="Administrators" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cmbAdministrators" runat="server"    class="form-control"  AutoPostBack="True" ></asp:DropDownList>
                                            
                                            </div>
                                           <asp:Label ID="Label2" runat="server" Text="Actuary" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cmbActuary" runat="server"    class="form-control"  AutoPostBack="True" ></asp:DropDownList>
                                            
                                            </div>
                                          
                                        
                                        </div>
                                     <div class="form-group row">
                                        <asp:Label ID="Label8" runat="server" Text="Asset Manager" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <%--<asp:DropDownList ID="cmbAssetManager" runat="server"    class="form-control"  AutoPostBack="True" ></asp:DropDownList>--%>
                                            <asp:ListBox ID="cmbAssetManager" class="form-control" runat="server"  AutoPostBack ="false"  SelectionMode="Multiple"></asp:ListBox>
                                            <br />
                                            <asp:Button ID="addAssetmanager" class="btn btn-primary" onclick="addAssetmanager_Click" runat="server" Text="Add"   /> 
                                            </div>
                                         
                                         <asp:Label ID="Label7" runat="server" Text="Custodian" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cmbCustodians" runat="server"    class="form-control"  AutoPostBack="true" ></asp:DropDownList>
                                            
                                            </div>
                                        </div>
                                    <asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <div class="form-group row">

                                         <div class="col-sm-2">
                                             </div>
                                        
                                        <div class="col-sm-2">
                                            <asp:Label ID="LbselecdtedAssetmanagers" Visible="false" runat="server" Text="Selected Managers" ></asp:Label>
                                             <asp:GridView ID="grdAssetmanagers" runat="server"  CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No Users!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left" AllowPaging="True" >
                  <AlternatingRowStyle BackColor="White" CssClass="altrowstyle" />
                <EditRowStyle BackColor="#2461BF" />
                <EmptyDataRowStyle CssClass="text-warning text-center" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" CssClass="header info" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" CssClass="rowstyle" />
               
                   <Columns>
                   
              
                   <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton OnClientClick="if (!confirm('Are you sure you want remove this assetmanager?')) return false;" ID="lnkDiscard" runat="server" CommandArgument="<%# Bind('id') %>" OnClick="linkDiscardAssetManagers">Remove</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton  ID="lnkedit" runat="server" CommandArgument="<%# Bind('Name') %>" OnClick="viewprofile">View AssetManager Profile</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                      
                    <%-- <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkdelete" runat="server" CommandName="Delete" >Delete</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
              </asp:GridView>
                                            
                                            </div>
                                         

                                        </div>
                                        </asp:Panel>
                                     
                                   
                                   
                            </div>
                             
                           


          
                             <div class="border-top">
                                 
                                    <div class="card-body"  >
                                        <div class="center">
                                        <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Save" OnClick="Button1_Click" />
                                           <asp:Button ID="Button2" runat="server" class="btn btn-primary" visible="false" Text="Save" OnClick="Button2_Click" />
                                 
                                 <asp:Button ID="Button3" runat="server" class="btn btn-primary" Text="Back" OnClick="Button3_Click" />
                                        </div>
                                        </div>
                              
                                 </div>

                                <%-- </form>--%>
                                 </div>
                        </div>
                    </div>
              </asp:Panel>
          
          </div>

         
</asp:Content>

