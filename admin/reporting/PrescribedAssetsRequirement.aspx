<%--<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Reporting/ReportingViews.master" AutoEventWireup="true" CodeFile="PrescribedAssetsRequirement.aspx.cs" Inherits="admin_parameters_User" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Reporting/Reporting.master" AutoEventWireup="true" CodeFile="PrescribedAssetsRequirement.aspx.cs" Inherits="admin_parameters_User" %>

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
                        <%--<h4 class="page-title">&nbsp;&nbsp;Reporting>>Client Selection>>Reporting Dashboard>>Report Viewing>>Asset Class Holding>>Prescribed Assets Requirement</h4><br />--%>
                        <nav aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item">Reporting>>Client Selection>>Reporting Dashboard>>Report Viewing>>Prescribed Assets Requirement</li>                                   
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
                                            <asp:TextBox ID="txtFirstName"  enabled="false"  class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                         <asp:Label ID="Label2" runat="server"  enabled="false" Text="Special Notes" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtSpecialNotes"  enabled="false"  class="form-control" runat="server"></asp:TextBox>
                                            </div>                                       
                                        </div>
                                     <div class="form-group row">
                                        <asp:Label ID="Label14" runat="server" Text="Selected Quarter" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtQuarter" enabled="false"  class="form-control" runat="server" OnTextChanged="txtQuarter_TextChanged"></asp:TextBox>
                                            </div>
                                        <asp:Label ID="Label15" runat="server" Text="Year" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">                                            
                                           <asp:TextBox ID="txtYear" enabled="false"  class="form-control" runat="server" OnTextChanged="txtYear_TextChanged"></asp:TextBox>
                                            </div>
                                      </div>   <%-- 

                                    <div class="form-group row">
                                        <asp:Label ID="Label4" runat="server" Text="Previous Quarter" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtPrevQuarter" enabled="false"  class="form-control" runat="server" disabled="true" ></asp:TextBox>
                                            </div>
                                        <asp:Label ID="Label5" runat="server" Text="Year" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">                                            
                                           <asp:TextBox ID="txtPrevQuarterYear" enabled="false"  class="form-control" runat="server" disabled="true" ></asp:TextBox>
                                            </div>
                                      </div> --%>


                                     <div class="form-group row">
                                        <asp:TextBox ID="txtID"  class="form-control" runat="server" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtselectedAssetManager"  class="form-control" runat="server" Visible="false"></asp:TextBox>
                                             <asp:TextBox id="operationid" visible="false" runat="server"></asp:TextBox>
                                        
                                        </div>
                                    
                                     
                                 
                                   
                                        <div class="center">
                                            <asp:Button ID="Button6" runat="server" class="btn btn-primary" Text="Back" OnClick="Button3_Click" />
                                        <asp:Button ID="Button2" runat="server"  visble="true" class="btn btn-primary" Text="View" OnClick="Button1_Click"   />
                                          
                                 
                                
                                        </div>
                                    <br />
                                     <div class="form-group row">

                                      
                                        <div class="col-sm-12">
                                             <div class="center">
                                                  <%--<asp:Label ID="Label3" runat="server" Text="Asset Manager Portfolio Movements" Visible="false" ForeColor="Black"></asp:Label>--%>
                                            <asp:GridView ID="grdAssetManagerPortifolio" runat="server"   CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No Portifolio!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left" AllowPaging="false">
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
                                     <div class="center">
                                                       <asp:Button ID="Button1" Visible="false"  class="btn btn-primary" OnClick="Button1_Click1" runat="server" Text="Export To Excel" />
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

