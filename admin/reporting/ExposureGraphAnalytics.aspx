<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Clients/ClientStatus.master" AutoEventWireup="true" CodeFile="ExposureGraphAnalytics.aspx.cs" Inherits="admin_parameters_User" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

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
                        <h4 class="page-title">Cliengt Asset Manager Exposures</h4><br />
                        
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
                                            <asp:TextBox ID="txtFirstName"  class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        
                                        
                                        </div>
                                     <div class="form-group row">
                                        <asp:Label ID="Label14" runat="server" Text="Quarter" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                             <asp:dropdownlist ID="rdQuarter"  class="form-control" runat="server">
                                                 <asp:ListItem Value="SQ">Select Quarter</asp:ListItem>
                                                 <asp:ListItem Value="1">First Quarter</asp:ListItem>
                                                  <asp:ListItem Value="2">Second  Quarter</asp:ListItem>
                                                 <asp:ListItem Value="3">Third  Quarter</asp:ListItem>
                                                  <asp:ListItem Value="4">Fourth  Quarter</asp:ListItem>
                                             </asp:dropdownlist>
                                            </div>
                                        <asp:Label ID="Label15" runat="server" Text="Year of Take" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            
                                         
                                              <asp:dropdownlist ID="cmbYear"    class="form-control" runat="server" ></asp:dropdownlist>
                                          
                                            </div>

                                              
                                          
                                            </div>
                                      
                                        



                                     <div class="form-group row">
                                        <asp:TextBox ID="txtID"  class="form-control" runat="server" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtselectedAssetManager"  class="form-control" runat="server" Visible="false"></asp:TextBox>
                                             <asp:TextBox id="operationid" visible="false" runat="server"></asp:TextBox>
                                        
                                        </div>
                                    
                                     
                                 
                                   
                                        <div class="center">
                                        <asp:Button ID="Button2" runat="server"  visble="true" class="btn btn-primary" Text="View" OnClick="Button1_Click"   />
                                          
                                 
                                
                                        </div>
                                    <br />
                                     <div class="form-group row">

                                      
                                        <div class="col-sm-12">
                                             <div class="center">
                                            <asp:GridView ID="grdPortifolioSnapshot" runat="server"  visible="false" CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No Portifolio!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left" AllowPaging="false" >
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
                                     <div class="form-group row">

                                      
                                        <div class="col-sm-12">

                                            <asp:Label ID="Label2" runat="server" Visible="false" Text="Exposures(%)"></asp:Label>        
                                                 <asp:Chart ID="Chart1" Visible="false" runat="server" BackColor="0, 0, 64" BackGradientStyle="LeftRight"
        BorderlineWidth="0" Height="800px" Palette="None" PaletteCustomColors="Maroon"
        Width="1420px" BorderlineColor="64, 0, 64">
        <Titles>            <asp:Title ShadowOffset="10" Name="Items" />
        </Titles>
        <Legends>
            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                LegendStyle="Row" />
        </Legends>
        <Series>
            <asp:Series Name="Default" />
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1" BorderWidth="0"  />
        </ChartAreas>
    </asp:Chart>

                                                
                                            
                                            
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

