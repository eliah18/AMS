<%--<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Reporting/ReportingViews.master" AutoEventWireup="true" CodeFile="AssetClassHoldingView.aspx.cs" Inherits="admin_parameters_User" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Reporting/Reporting.master" AutoEventWireup="true" CodeFile="AssetClassHoldingView.aspx.cs" Inherits="admin_parameters_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
    <style>
.center {
  text-align: center;
 
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
     <div class="page-breadcrumb">
                <div class="row">
                    <div class="col-12 d-flex no-block align-items-center">
                        <nav aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item">Reporting>>Client Selection>>Reporting Dashboard>>Report Viewing>>Asset Class Holding</li>                                   
                            </ol>
                        </nav>
                        
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
          

             
           <asp:Panel ID="Panel1" Visible="true" runat="server">
                
              </asp:Panel>
          
          <asp:Panel ID="usersPanel" Visible="true" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card">
                             <%--<form class="form-horizontal">--%>
                                <div class="card-body">
                                    <h4 class="card-title"> General Details</h4>
                                    <div class="form-group row">
                                        <asp:Label ID="Label1" runat="server" Text="Name" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtFirstName" enabled="false" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                         <asp:Label ID="Label5" runat="server" Text="Special Notes" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtSpecialNotes"  Enabled="false"  class="form-control" runat="server"></asp:TextBox>
                                             <asp:TextBox ID="txtID"  class="form-control" runat="server" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtselectedAssetManager"  class="form-control" runat="server" Visible="false"></asp:TextBox>
                                             <asp:TextBox id="operationid" visible="false" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                     <div class="form-group row">
                                        <asp:Label ID="Label4" runat="server" Text="Quarter" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtQuarter" enabled="false" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                          <asp:Label ID="Label6" runat="server" Text="Year" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtYear" enabled="false" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                         

                                         </div>
                                    
                                        
                                    
                                     <div class="form-group row">
                                           <asp:Label ID="Label3" runat="server" Text="View Preference" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            
                                         
                                              <asp:dropdownlist ID="cmbSubAccount"   class="form-control" runat="server" ></asp:dropdownlist>
                                          
                                            </div>
                                         </div>
                                    
                                           </div>
                                            <div class="center">
                                                 <asp:Button ID="Button6" runat="server" class="btn btn-primary" Text="Back" OnClick="Button3_Click" />
                                        <asp:Button ID="Button2" runat="server"  visble="true" class="btn btn-primary" Text="View" OnClick="Button1_Click"   />
                                                 
                                          
                                 
                                
                                        </div>
                                    </div>
                            </div>
                        </div>
                    </div>
                                      
                                        



                                    
                                      <div class="border-top">
                                        <asp:Panel ID="Panel2" visible="false" runat="server">
                                          
                                       
                                            
                                   
                                            <div class="row">
                    <div class="col-md-12">
                        <div class="card">
                            <div class="card">
                             <%--<form class="form-horizontal">--%>
                                <div class="card-body">
                                    
                                      
                                        <br />
                                        <br />

                                            
                                    <asp:Panel ID="Panel4" Visible="false" runat="server">
                                             <div class="row">
                                                 
                                             
                                                  <div class="col-sm-12">
                                                      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                                                      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                          <ContentTemplate>
                                                <asp:Timer ID="Timer1"  Interval="3000" OnTick="Timer1_Tick"    runat="server"></asp:Timer>
                                                   <h4 class="card-title">Current Holdings Across All Allocated Asset Managers </h4>
                                                       <asp:GridView ID="grdApps" runat="server" OnDataBound="grdApps_DataBound"  CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No Investments!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left"  OnRowDataBound="grdApps_RowDataBound"  ShowFooter="false" AutoGenerateColumns="true">
                  <AlternatingRowStyle BackColor="White" CssClass="altrowstyle" />
                <EditRowStyle BackColor="#2461BF" />
                <EmptyDataRowStyle CssClass="text-warning text-center" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" CssClass="header info" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" CssClass="rowstyle" />
                                                         
                                                          
               
                   
              </asp:GridView>
                                                              </ContentTemplate>
                                                          </asp:UpdatePanel>
                                                      <div class="center">
                                                       <asp:Button ID="Button1"  class="btn btn-primary" OnClick="Button1_Click1" runat="server" Text="Export To Excel" />
                                                         <%-- <asp:Button ID="Button4"  class="btn btn-primary" OnClick="Button1_Click22" runat="server" Text="View Detailed Report" />--%>
                                                          </div>

                                                      </div>
                                                
                                                 
                                                  </div>
                                        </asp:Panel>
                            </div>
                        </div>
                                                </div>
                        </div>
                                                </div>
                                                
                                          


                                        
                                       
                                    
                                             </asp:Panel>
                                        
                                        <br />

                                         <asp:Panel ID="Panel3" visible="false" runat="server">
                                    <h4 class="card-title">linked Asset Classes</h4>
                                     <div class="form-group row">
                                        <asp:Label ID="Label2" runat="server" Text="Asset Classes" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <%--<asp:TextBox ID="txtRole"  class="form-control" runat="server"></asp:TextBox>--%>
                                            <asp:DropDownList ID="cmbAssetClass" runat="server" class="select2 form-control custom-select" style="width: 100%; height:36px;"  ></asp:DropDownList>
                                            </div>
                                         <asp:Label ID="Label10" runat="server" Text="Asset Managers" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        
                                        
                                        </div>
                                         <div class="form-group row">
                                              <asp:Label ID="Label12" runat="server" Text="Counter" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <%--<asp:TextBox ID="txtRole"  class="form-control" runat="server"></asp:TextBox>--%>
                                            <asp:DropDownList ID="cmbCounter" runat="server" class="select2 form-control custom-select" style="width: 100%; height:36px;"  ></asp:DropDownList>
                                            </div>
                                        <asp:Label ID="Label11" runat="server" Text="Quantity" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtQuantity"  class="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="TextBox2" visible="false"  class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                       
                                        </div>
                                             <div class="form-group row">
                                                 <asp:Label ID="Label13" runat="server" Text="Market Price" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <%--<asp:TextBox ID="txtRole"  class="form-control" runat="server"></asp:TextBox>--%>
                                            <asp:textbox ID="txtPrice" runat="server" class="form-control" style="width: 100%; height:36px;"  ></asp:textbox>
                                            </div>
                                                 </div>


                                             

                                        
                                       
                                     <div class="center">     
                                          
                                
                                    
                                       
                                        <asp:Button ID="Button3" runat="server" class="btn btn-primary" Text="Save" onclick="Button3_Click1"  />
                                           <asp:Button ID="Button5" runat="server" class="btn btn-primary" visible="false" Text="Update"   />
                                 
                                       
                                     </div>
                                             </asp:Panel>
                                             
                                      
                                       
                                            </div>
                                       
                                        </div>
                                        
                                            
                                        
                              
                                 </div>


                                        </div>
                                    
                            </div>
                          
                             

                                <%-- </form>--%>
      </div>
                        </div>
                    </div>
              </asp:Panel>

          
          
          </div>
    


   <script type="text/javascript">
       var myLink = '<% =  nexturl %>';

  function newPage() {
    window.open(myLink);
  }


</script>
  
</asp:Content>

