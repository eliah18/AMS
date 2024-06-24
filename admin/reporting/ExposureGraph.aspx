<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Reporting/Reporting.master" AutoEventWireup="true" CodeFile="ExposureGraph.aspx.cs" Inherits="admin_parameters_User" %>

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
                       <%-- <h4 class="page-title">&nbsp;&nbsp;Reporting>>Client Selection>>Reporting Dashboard>>Report Viewing>>Asset Class Holding>>Q2Q Exposure Pie Charts</h4><br />--%>
                        <nav aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item">Reporting>>Client Selection>>Reporting Dashboard>>Report Viewing>>Q2Q Exposure Pie Charts</li>                                   
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
                                            <asp:TextBox ID="txtFirstName" enabled="false"  class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                         <asp:Label ID="Label4" runat="server" Text="Special Notes" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtSpecialNotes" enabled="false"  class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        
                                        
                                        </div>
                                    <div class="form-group row">
                                        <asp:Label ID="Label3" runat="server" Text="Selected Quarter" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtQuarter" enabled="false"  class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        <asp:Label ID="Label15" runat="server" Text="Selected Year" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                         <div class="col-sm-4">
                                            <asp:TextBox ID="txtYear" enabled="false"  class="form-control" runat="server"></asp:TextBox>
                                            </div>                                             
                                          
                                            </div>

                                      <div class="form-group row">
                                        <asp:Label ID="Label5" runat="server" Text="Previous Quarter" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtPrevQuarter" enabled="false"  class="form-control" runat="server" disabled="true" ></asp:TextBox>
                                            </div>
                                        <asp:Label ID="Label6" runat="server" Text="Previous Year" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">                                            
                                           <asp:TextBox ID="txtPrevQuarterYear" enabled="false"  class="form-control" runat="server" disabled="true" ></asp:TextBox>
                                            </div>
                                      </div> 
                                                                             



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
            <%--Pie for Current QYear--%>
             <div class="form-group row">
                  <div class="col-sm-12">
                        <div class="form-group row">
                        <asp:Label ID="Label2" runat="server" Visible="false" Text="">Exposure(%) For Quarter <% =txtQuarter.Text.ToString() + " Of Year " + txtYear.Text.ToString() %></asp:Label>   
                        </div>
                        <asp:Chart ID="Chart1" Visible="false" runat="server" BackColor="0, 0, 64" BackGradientStyle="LeftRight"
        BorderlineWidth="0" Height="600px" Palette="None" PaletteCustomColors="Maroon"
        Width="1000px" BorderlineColor="64, 0, 64">
                            <Titles>            
                                <asp:Title ShadowOffset="10" Name="Items" />
                            </Titles>
                            <Legends>
                                <%--<asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="True" Name="Default"
                                    LegendStyle="Row" Enabled="false" />--%>
                                <asp:Legend Alignment="Center" Docking="Left" IsTextAutoFit="True" Name="Default"
                                    LegendStyle="Column" Enabled="false" />
                            </Legends>
                            <Series>
                                <asp:Series Name="Default" />
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1"  BorderWidth="0"  />
                            </ChartAreas>
                        </asp:Chart>
                   </div>                 
            </div>
            <div class="center">
            <asp:Button ID="Button3" Visible="false"  class="btn btn-primary" OnClick="BtnPrintPie1_Click1" runat="server" Text="Export To Excel" />
            </div>
            <hr style="border:0px;height:5px;width:150px;" />
            <%--Pie for Previous QYear--%>
            <div class="form-group row">
                  <div class="col-sm-12">
                        <div class="form-group row">
                        <asp:Label ID="Label7" runat="server" Visible="false" Text="">Exposure(%) For Quarter <% =txtPrevQuarter.Text.ToString() + " Of Year " + txtPrevQuarterYear.Text.ToString() %></asp:Label>   
                        </div>
                        <asp:Chart ID="Chart2" Visible="false" runat="server" BackColor="0, 0, 64" BackGradientStyle="LeftRight"
        BorderlineWidth="0" Height="600px" Palette="None" PaletteCustomColors="Maroon"
        Width="1000px" BorderlineColor="64, 0, 64">
                            <Titles>            
                                <asp:Title ShadowOffset="10" Name="Items" />
                            </Titles>
                            <Legends>
                                <asp:Legend Alignment="Center" Docking="Left" IsTextAutoFit="True" Name="Default"
                                    LegendStyle="Column" Enabled="false" />
                            </Legends>
                            <Series>
                                <asp:Series Name="Default" />
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1"  BorderWidth="0"  />
                            </ChartAreas>
                        </asp:Chart>
                   </div>                
            </div>
            <div class="center">
                <asp:Button ID="Button4" Visible="false"  class="btn btn-primary" OnClick="BtnPrintPie2_Click1" runat="server" Text="Export To Excel" />
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

