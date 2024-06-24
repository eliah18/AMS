<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Reporting/Reporting.master" AutoEventWireup="true" CodeFile="AssetAllocationUpdate.aspx.cs" Inherits="admin_parameters_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
    <style>
.center {
  text-align: center;
 
}
.inside {
  background-color: #eeeeee;
 
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">

     <div class="inside">
                <div class="row">
                    <div class="col-12 d-flex no-block align-items-center">
                        <h4 class="page-title center"> &nbsp;&nbsp;Reporting--AssetClassHolding--Report Editing-- Details Confirmation </h4><br />
                        
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
                <div class="row inside" >
                    <div class="col-md-12">
                        <div class="card">
                             <%--<form class="form-horizontal">--%>
                                <div class="card-body">
                                    <h4 class="card-title"> Report Creation  Details</h4>
                                    <div class="form-group row">
                                        <asp:Label ID="Label1" runat="server" Text=" Client Name" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtFirstName" enabled="false" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                         <asp:Label ID="Label5" runat="server" Text="Asset Manager" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                          
                                             <asp:TextBox ID="txtID"  class="form-control" runat="server" Visible="false"></asp:TextBox>
                                               <asp:DropDownList ID="rdAssetMangers" OnSelectedIndexChanged="rdBonds_SelectedIndexChanged"  runat="server" class="select2 form-control custom-select" style="width: 100%; height:36px;" AutoPostBack="True" ></asp:DropDownList>
                                                   </asp:dropdownlist>
                                            <asp:TextBox ID="txtselectedAssetManager" Enabled="false" class="form-control" runat="server" Visible="false"></asp:TextBox>
                                             <asp:TextBox ID="txtAssetManager"   Visible="false"   class="form-control" runat="server"></asp:TextBox>
                                             <asp:TextBox id="operationid" visible="false" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                     <div class="form-group row">
                                        <asp:Label ID="Label14" runat="server" Text="Quarter" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                             <asp:TextBox ID="txtQuarter"  Enabled="false"  class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        <asp:Label ID="Label15" runat="server" Text="Reporting Year" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            
                                         
                                              <asp:TextBox ID="txtYear"  Enabled="false"  class="form-control" runat="server"></asp:TextBox>
                                          
                                            </div>

                                              
                                          
                                            </div>
                                    <div class="form-group row">
                                           <asp:Label ID="Label3" runat="server" Text="Sub Account" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                              <asp:dropdownlist ID="rdSubAccount"  class="form-control" runat="server">
                                                   </asp:dropdownlist>
                                            </div>
                                          </div>
                                     <div class="center">
                                          
                                           </div>
                                            <div class="center">
                                                 <asp:Button ID="Button6" runat="server" class="btn btn-primary" Text="Back" OnClick="Button3_Click" />
                                        <asp:Button ID="Button2" runat="server"  visble="true" class="btn btn-primary" Text="Confirm And Edit" OnClick="Button1_Click"   />
                                                  
                                          
                                 
                                
                                        </div>
                                    </div>
                            </div>
                        </div>
                    </div>
              </asp:Panel>
              
                                      
                                        



                                    
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

                                            
                                    
                                             <div class="row">
                                                 <div class="col-sm-3">
                                                 <h4 class="card-title">Reporting <asp:Label ID="Label16" Visible="false" runat="server" Text="Label"></asp:Label> </h4>
                                      <asp:Repeater ID="repBusAssetsIB" runat="server" OnItemCommand="repBusAssetsIB_ItemCommand">
                                            <HeaderTemplate>
                                                <table class="row table table-striped table-bordered">
                                                    <tr>
                                                        <th>
                                                            <asp:Label ID="Label99" runat="server" Text="Asset Class"></asp:Label>
                                                        </th>
                                                        <th>
                                                             <asp:Label ID="Label9" runat="server" Text="Value Entry"></asp:Label>
                                                        </th>
                                                    </tr>
                                            </HeaderTemplate>
                                             <ItemTemplate>
                                                <tr>
                                                    <td>
                                                         <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "asset_name")%>'></asp:Label>
                                                        <asp:Label ID="Label7" runat="server"  visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "investmentid")%>'></asp:Label>
                                                    </td>
                                                    <td>

                                                        <asp:TextBox CssClass="form-control input-sm" ID="res" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "amount")%>' ></asp:TextBox>
                                                        <asp:LinkButton id="linked" runat="server" OnClick="lnkedit" CommandArgument='<%# Eval("id") %>' Text="[Click Here]" ></asp:LinkButton>
                                                    </td>
                                                    
                                                    
                                                </tr>
                                                 
                                            </ItemTemplate>
                                           
                                            <FooterTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label26" runat="server" Text="Operation"></asp:Label>
                                                        </td>
                                                    <td>
                                                        
                                                        <asp:Button ID="Button3" Visible="true" runat="server" class="btn btn-primary" Text="Save" onclick="Button6_Click"  />
                                                        <asp:Button ID="Button5"  onclick="Button5_Click"           runat="server" class="btn btn-primary" visible="false" Text="Update"   />
                                                      
                                                        </td>
                                                    <tr>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                                     </div>
                                             
                                                  <div class="col-sm-9">
                                                      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                                                      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                          <ContentTemplate>
                                                <asp:Timer ID="Timer1"  Interval="3000" OnTick="Timer1_Tick"    runat="server"></asp:Timer>
                                                   <h4 class="card-title">Current Holdings Across All Allocated Asset Managers For Sub-Acount <% =rdSubAccount.SelectedItem.Text.ToString() %> </h4>
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
                                                            <asp:Button ID="Button4"  class="btn btn-primary" OnClick="Button1_Click22" runat="server" Text="View Detailed Report" />
                                                          </div>
                                                      

                                                      </div>
                                                
                                                 
                                                  </div>
                                        </asp:Panel>
                            </div>
                        </div>
                                             
                                                
                                          


                                        
                                       
                                    
                                            
                                        
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
             

          
          
          </div>
    </div>
    


   <script type="text/javascript">
       var myLink = '<% =  nexturl %>';

  function newPage() {
    window.open(myLink);
  }


</script>
  
</asp:Content>

