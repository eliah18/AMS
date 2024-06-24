<%@ Page Title="" Language="C#" MasterPageFile="~/admin/parameters/Investor.master" AutoEventWireup="true" CodeFile="ClientIpsecSetupView.aspx.cs" Inherits="admin_parameters_User" %>

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
                        <h4 class="page-title">  IPSEC View</h4><br />
                        
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
                <div class="row">
                    <div class="col-md-12">
                        <div class="card">
                             <%--<form class="form-horizontal">--%>
                                <div class="card-body">
                                    <h4 class="card-title">Client Search</h4>
                                    <div class="form-group row">
                                        <asp:Label ID="Label6" runat="server" Text="Search by Name/Surname" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtsearch"  class="form-control" runat="server"></asp:TextBox>
                                           
                                            </div>
                                        <div class="col-sm-4">
                                             <asp:Button ID="Button4" runat="server"  class="btn btn-primary" Text=">>" OnClick="Button4_Click"  />
                                            </div>
                                        </div>
                                        
                                        
                                        
                                        
                                    
                              
                                    <div class="form-group row">
                                        <asp:Label ID="Label8" runat="server" Text="Username" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:listbox ID="lstNamesSearch" autopostback="true" class="form-control" runat="server" OnSelectedIndexChanged="lstNamesSearch_SelectedIndexChanged"></asp:listbox>
                                            </div>
                                       
                                             
                                            </div>
                                     
                              
                                        </div>
                                   
                                    
                            </div>
                        
                                
                                 
                             

                                <%-- </form>--%>
                                 </div>
      </div>
                    
              </asp:Panel>
          
          <asp:Panel ID="usersPanel" Visible="true" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card">
                             <%--<form class="form-horizontal">--%>
                                <div class="card-body">
                                    <h4 class="card-title">Client Info</h4>
                                    <div class="form-group row">
                                        <asp:Label ID="Label1" runat="server" Text="Name" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtFirstName" enabled="false" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                         <asp:Label ID="Label2" runat="server" Text="Address" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtAddress" enabled="false" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    
                                   
                                    <div class="form-group row">
                                        <asp:Label ID="Label4" runat="server" Text="Contact Number" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtContactNumber" Enabled="false"  class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        <asp:Label ID="Label5" runat="server" Text="Special Notes" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtSpecialNotes" Enabled="false"  class="form-control" runat="server"></asp:TextBox>
                                             <asp:TextBox ID="txtID" visible="false"  class="form-control" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>
                                    <div class="border-top">
                                        <br />
                                        
                                    <h4 class="card-title">IPS  Asset Allocation/Mandates</h4>
                                         <div class="form-group row">
                                        <asp:Label ID="Label14" runat="server" Text="IPS level" class="col-sm-3 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-9">
                                         
                                             
                                            <asp:RadioButtonList ID="RadioButtonList1" RepeatDirection="Horizontal"  OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="true" CellPadding="4" CellSpacing="4" runat="server">
                                                <asp:ListItem Value="Client" selected="true">Client</asp:ListItem>
                                                <asp:ListItem Value="AssetManager">AssetManager</asp:ListItem>
                                            </asp:RadioButtonList>
                                
                                            </div>
                                        </div>
                                     <div class="form-group row">
                                        <asp:Label ID="Label10" runat="server" visible="false" Text="Asset Classes" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <%--<asp:TextBox ID="txtRole"  class="form-control" runat="server"></asp:TextBox>--%>
                                            <asp:DropDownList ID="cmbAsetClass" Visible="false" runat="server" class="select2 form-control custom-select" style="width: 100%; height:36px;"  OnSelectedIndexChanged="rdAssetClass_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                         <asp:Label ID="Label11" visible="false" runat="server" Text="Percentage(%)" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <%--<asp:TextBox ID="txtRole"  class="form-control" runat="server"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtAllocation" Visible="false"  min="0.00" max="100.00" step=".01" type="number" class="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtClientIpsecUpdateID" visible="false"  class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        
                                        </div>
                                        <div class="form-group row">
                                        
                                         <asp:Label ID="Label7" runat="server" Visible="false" Text="Asset Managers" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <%--<asp:TextBox ID="txtRole"  class="form-control" runat="server"></asp:TextBox>--%>
                                            <asp:DropDownList ID="rdAssetMangers" Visible="false" runat="server" class="select2 form-control custom-select" style="width: 100%; height:36px;" AutoPostBack="True" OnSelectedIndexChanged="rdAssetMangers_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        
                                        </div>
                                        
                                       
                                     <div class="center">     
                                          
                                
                                    
                                       
                                        <asp:Button ID="Button3" runat="server" class="btn btn-primary" Text="View" onclick="Button3_Click1"  />
                                           <asp:Button ID="Button5" runat="server" class="btn btn-primary" visible="false" Text="Update" onclick="Button5_Click"  />
                                 
                                
                                      
                                     </div>
                                        <br />
                                        
                                         <asp:Label ID="Label12" runat="server"  ></asp:Label>
                                       
                                         <div class="form-group row">

                                      
                                        <div class="col-sm-8">
                                             <div class="center">
                                            <asp:GridView ID="grdClientIpsecs" runat="server"  CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No IPSEC!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left" AllowPaging="True" >
                  <AlternatingRowStyle BackColor="White" CssClass="altrowstyle" />
                <EditRowStyle BackColor="#2461BF" />
                <EmptyDataRowStyle CssClass="text-warning text-center" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" CssClass="header info" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" CssClass="rowstyle" />
                                                
                  
              </asp:GridView>
                                            
                                             </div>
                                           </div>
                                             </div>
                                        <br />
                                        

                                         <asp:Label ID="Label13" runat="server"  ></asp:Label>
                                        
                                    <div>
                                        <br />
                                  
                                     
                                         
                                        
                                       
                                    
                                        <br />
                                        <div class="center"></div>
                                         <asp:Label ID="assetmanagerslabel" visible="false" runat="server"  ></asp:Label>
                                        </div>
                                         <div class="form-group row">

                                       
                                        <div class="col-sm-10">
                                            <asp:GridView ID="grdApps" runat="server"  CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No IPSEC!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left" AllowPaging="True" >
                  <AlternatingRowStyle BackColor="White" CssClass="altrowstyle" />
                <EditRowStyle BackColor="#2461BF" />
                <EmptyDataRowStyle CssClass="text-warning text-center" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" CssClass="header info" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" CssClass="rowstyle" />
                                               
               
                  
              </asp:GridView>
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


         
</asp:Content>

