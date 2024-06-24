<%--<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Reporting/ReportingViews.master" AutoEventWireup="true" CodeFile="ComplianceMatrixCreate.aspx.cs" Inherits="admin_parameters_User" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Reporting/Reporting.master" AutoEventWireup="true" CodeFile="ComplianceMatrixCreate.aspx.cs" Inherits="admin_parameters_User" %>

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
                        <%--<h4 class="page-title">&nbsp;&nbsp;Reporting>>Client Selection>>Reporting Dashboard>>Report Creation>>Compliance Matrix</h4><br />--%>
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item">Reporting>>Client Selection>>Reporting Dashboard>>Report Creation>>Compliance Matrix</li>   
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
          

             
<%--           <asp:Panel ID="Panel1" Visible="true" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card">
                             <form class="form-horizontal">
                                <div class="card-body">
                                    <h4 class="card-title"> General Details</h4>
                                    <div class="form-group row">
                                        <asp:Label ID="Label6" runat="server" Text="Name" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="TextBox1" enabled="false" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                         <asp:Label ID="Label8" runat="server" Text="Special Notes" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="TextBox2"  Enabled="false"  class="form-control" runat="server"></asp:TextBox>
                                             <asp:TextBox ID="TextBox3"  class="form-control" runat="server" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtselectedAssetManager"  class="form-control" runat="server" Visible="false"></asp:TextBox>
                                             <asp:TextBox id="operationid" visible="false" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                     <div class="form-group row">
                                        <asp:Label ID="Label19" runat="server" Text="Quarter" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtQuarter" enabled="false" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                          <asp:Label ID="Label20" runat="server" Text="Year" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtYear" enabled="false" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                         

                                         </div>
                                    
                                        
                                    
                                     <div class="form-group row">
                                           <asp:Label ID="Label21" runat="server" Text="View Preference" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            
                                         
                                              <asp:dropdownlist ID="cmbSubAccount"   class="form-control" runat="server" ></asp:dropdownlist>
                                          
                                            </div>
                                         </div>
                                    
                                           </div>
                                   
                            </div>
                        
                                 </form>
                                 </div>
      </div>
                    
              </asp:Panel>--%>
          
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
                                        <asp:Label ID="Label5" runat="server" Text="Special Notes" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtSpecialNotes" Enabled="false"  class="form-control" runat="server"></asp:TextBox>
                                             <asp:TextBox ID="txtID" visible="false"  class="form-control" runat="server" ></asp:TextBox>
                                            <asp:TextBox ID="txtSumFromDB" visible="false"  class="form-control" runat="server" ></asp:TextBox>
                                            </div>
                                         <%--<asp:Label ID="Label2" runat="server" Text="Address" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtAddress" enabled="false" class="form-control" runat="server"></asp:TextBox>
                                            </div>--%>
                                        </div>
                                    
                                   
                                    <%--<div class="form-group row">
                                        <asp:Label ID="Label4" runat="server" Text="Contact Number" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtContactNumber" Enabled="false"  class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                    </div>--%>

                                       <div class="form-group row">
                                        <asp:Label ID="Label19" runat="server" Text="Quarter" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtQuarter" enabled="false" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                          <asp:Label ID="Label20" runat="server" Text="Year" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtYear" enabled="false" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                         

                                         </div>
                                    <div class="border-top">
                                        <br />
                                        
                                    <h4 class="card-title"></h4>

                                        <div class="form-group row">

                                            <asp:Label ID="Label10" runat="server" Text="Variable" visible="true" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <%--<asp:TextBox ID="txtRole"  class="form-control" runat="server"></asp:TextBox>--%>
                                            <asp:DropDownList ID="cmbAsetClass" runat="server" visible="true" class="select2 form-control custom-select" style="width: 100%; height:36px;"  OnSelectedIndexChanged="rdAssetClass_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:TextBox ID="txtAllocationOld" visible="false"  class="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtCheckCount" visible="false"  class="form-control" runat="server"></asp:TextBox>
                                            </div>

                                            <asp:Label ID="Label7" runat="server" Text="Asset Manager" visible="true" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <%--<asp:TextBox ID="txtRole"  class="form-control" runat="server"></asp:TextBox>--%>
                                            <asp:DropDownList ID="rdAssetMangers" runat="server" visible="true" class="select2 form-control custom-select" style="width: 100%; height:36px;" AutoPostBack="True"></asp:DropDownList>
                                            <%--<asp:TextBox ID="AssetMangersID" visible="false"  class="form-control" runat="server"></asp:TextBox>--%>
                                            </div>   

                                        </div>
                                     <%--<div class="form-group row">--%>
                                        <%--<asp:Label ID="Label10" runat="server" Text="Asset Classes" class="col-sm-2 text-right control-label col-form-label"></asp:Label>--%>
                                        <%--<div class="col-sm-4">--%>
                                            <%--<asp:TextBox ID="txtRole"  class="form-control" runat="server"></asp:TextBox>--%>
                                            <%--<asp:DropDownList ID="cmbAsetClass" runat="server" class="select2 form-control custom-select" style="width: 100%; height:36px;"  OnSelectedIndexChanged="rdAssetClass_SelectedIndexChanged"></asp:DropDownList>
                                            </div>--%>

                                         
                                                                                  
                                         <%--</div>--%>
                                    
                                        <div class="form-group row">                                        
                                            <asp:Label ID="Label11" runat="server" Text="Scale" visible="true" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                            <div class="col-sm-4">
<%--                                            <asp:TextBox ID="txtRole"  class="form-control" runat="server"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtAllocation" visible="true"  min="0" max="5" step="1" type="number" class="form-control" runat="server"></asp:TextBox>                                            
                                            </div>  
                                            <asp:Label ID="Label6" runat="server" Text="" visible="true" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                            <div class="col-sm-4">
                                            <div class="center">                                           
                                        <%--<asp:Button ID="Button3" runat="server" class="btn btn-primary" Text="Save" onclick="Button3_Click1"  />--%>
                                                                         
                                            </div>
                                            </div>

                                        </div>
                                    
                                       <div class="center">
                                       </div>
                                        <div class="center">  
                                            <asp:Button ID="Button6" runat="server" class="btn btn-primary" Text="Back" OnClick="Button4_Click" />
                                            <asp:Button ID="BtnSetMatrices" runat="server" class="btn btn-primary" visible="true" Text="Add Matrice" onclick="BtnSetMatrices_Click"  /> 
                                        <%--<asp:Button ID="Button5" runat="server" class="btn btn-primary" visible="true" Text="Update" onclick="Button5_Click"  /> --%>
                                            <%--<asp:Button ID="Button1" class="btn btn-primary" OnClick="Button1_Click1" runat="server" Text="Export To Excel" />--%>
                                        </div>                                            
                                   
                                        
                                            <%--NEW CORRIDORS--%>
                                            <div class="form-group row">                                                
                                        <br />
                                        <div class="center">
                                            </div>
                                         <asp:Label ID="Label12" runat="server"  ></asp:Label>                                       
                                         <%--<div class="form-group row">--%>
                                      
                                        <%--<div class="col-sm-10">--%>
                                             <div class="center">
                <asp:GridView ID="grdClientIpsecs" runat="server" CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No Compliance Matrix!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left" AllowPaging="False" OnRowDataBound="grdClientIpsecs_RowDataBound" OnPreRender="grdClientIpsecs_PreRender">
                  <AlternatingRowStyle BackColor="White" CssClass="altrowstyle" />
                <EditRowStyle BackColor="#2461BF" />
                <EmptyDataRowStyle CssClass="text-warning text-center" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" CssClass="header info" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" CssClass="rowstyle" />
                                                <Columns>
              
                   <%--<asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton OnClientClick="if (!confirm('Are you sure you want delete?')) return false;" ID="lnkDiscard" runat="server" CommandArgument="<%# Bind('variable') %>" OnClick="linkDiscard">Delete</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                       <%--<asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton  ID="lnkedit" runat="server" CommandArgument="<%# Bind('variable') %>" OnClick="links">Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                <%--<asp:TemplateField HeaderText="TotalQuantity" ItemStyle-Width="150px">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblQuantity" Text='<%# Eval("Value") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label runat="server" ID="lblTotalValue" Text='<%# Eval("Value") %>'></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>--%>
                    <%-- <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkdelete" runat="server" CommandName="Delete" >Delete</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
               
                  
              </asp:GridView>
                                                 <div class="form-group row">
                                                     <asp:Label ID="Label17" runat="server" Text=" " class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                                     <asp:Label ID="Label16" runat="server" Text=" " class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                                     
                                        <asp:Label ID="Label3" runat="server" Text="Total Value" visible="false" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                                      <asp:Label ID="Label18" runat="server" Text=" " class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                                     <asp:Label ID="LblTotal" runat="server" Text=" " Visible="false" class="col-sm-2 text-left control-label col-form-label"></asp:Label>

                                        <div class="col-sm-4">
                                            <%--<asp:TextBox ID="txtTotalSum" Enabled="false" visible="true" class="form-control" runat="server"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                            
                                             </div>
                                           <%--</div>--%>
                                             <%--</div>--%>
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

       <%--<script type="text/javascript">
           var myLink = '<% =  nexturl %>';

           function newPage() {
               window.open(myLink);
           }--%>


       </script>
         
</asp:Content>

