<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Clients/ClientStatus.master" AutoEventWireup="true" CodeFile="QuartertoQuarter.aspx.cs" Inherits="admin_parameters_User" %>

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
                        <h4 class="page-title"> Quarterly  to  Quarter</h4><br />
                        
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
                                        <asp:Label ID="Label8" runat="server" Text="Suggested List"  class="col-sm-2 text-right control-label col-form-label"></asp:Label>
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
                                    <h4 class="card-title">Details</h4>
                                    <div class="form-group row">
                                        <asp:Label ID="Label1" runat="server" Text="Name" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtFirstName"  class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        
                                         <asp:Label ID="Label3" runat="server" Text="Asset Managers" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                             
                                      
                                            <asp:dropdownlist ID="rdAssetMangers"     autopostback="false" class="form-control" runat="server" OnSelectedIndexChanged="rdAssetMangers_SelectedIndexChanged"></asp:dropdownlist>
                                            
                                            
                                           
                                            </div>
                                        </div>
                                   
                                     <div class="form-group row">
                                        <asp:Label ID="Label14" runat="server" Text="From (Quarter)" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                             <asp:dropdownlist ID="rdQuarter"  class="form-control" runat="server">
                                                 <asp:ListItem Value="SQ">Select Quarter</asp:ListItem>
                                                 <asp:ListItem Value="1">First Quarter</asp:ListItem>
                                                  <asp:ListItem Value="2">Second  Quarter</asp:ListItem>
                                                 <asp:ListItem Value="3">Third  Quarter</asp:ListItem>
                                                  <asp:ListItem Value="4">Fourth  Quarter</asp:ListItem>
                                             </asp:dropdownlist>
                                            </div>
                                        <asp:Label ID="Label15" runat="server" Text="From (Year)" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            
                                         
                                              <asp:dropdownlist ID="cmbYear"    class="form-control" runat="server" ></asp:dropdownlist>
                                          
                                            </div>

                                              
                                          
                                            </div>
                                     <br />
                                    <b />
                                      <div class="form-group row">
                                        <asp:Label ID="Label2" runat="server" Text="To (Quarter)" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                             <asp:dropdownlist ID="rdQuarterTo"  class="form-control" runat="server">
                                                 <asp:ListItem Value="SQ">Select Quarter</asp:ListItem>
                                                 <asp:ListItem Value="1">First Quarter</asp:ListItem>
                                                  <asp:ListItem Value="2">Second  Quarter</asp:ListItem>
                                                 <asp:ListItem Value="3">Third  Quarter</asp:ListItem>
                                                  <asp:ListItem Value="4">Fourth  Quarter</asp:ListItem>
                                             </asp:dropdownlist>
                                            </div>
                                        <asp:Label ID="Label4" runat="server" Text="To (Year)" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            
                                         
                                              <asp:dropdownlist ID="cmbYearTo"    class="form-control" runat="server" ></asp:dropdownlist>
                                          
                                            </div>

                                              
                                          
                                            </div>
                                      
                                      
                                        



                                     <div class="form-group row">
                                        <asp:TextBox ID="txtID"  class="form-control" runat="server" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtselectedAssetManager"  class="form-control" runat="server" Visible="false"></asp:TextBox>
                                             <asp:TextBox id="operationid" visible="false" runat="server"></asp:TextBox>
                                        
                                        </div>
                                    
                                     
                                 
                                   
                                        <div class="center">
                                        <asp:Button ID="Button2" runat="server"  visble="true" class="btn btn-primary" Text="View  Report" OnClick="Button1_Click"   />
                                          
                                 
                                
                                        </div>
                                        
                              
                                 
                            
                                    
                                   
                                   
                                        
                                    
                                       
                                             </asp:Panel>

                                        
                                        <br />

                                       
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

