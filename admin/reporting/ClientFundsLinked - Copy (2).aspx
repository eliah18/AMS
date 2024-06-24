<%@ Page Title="" Language="C#" MasterPageFile="~/admin/user/Modal.master" AutoEventWireup="true" CodeFile="ClientFundsLinked.aspx.cs" Inherits="admin_ClientStatus_ClientFundsLinked" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div class="page-breadcrumb">
                <div class="row">
                    <div class="col-12 d-flex no-block align-items-center">
                        <h4 class="page-title">Reporting--AssetClassHolding--Report Creation-- Linked Asset Classes Capture <br /> <br /> <asp:Label ID="lblabel" runat="server" Text="Label"></asp:Label> </h4>
                        
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
          <asp:Panel ID="Panel1" Visible="false" runat="server">
                <div class="row">
                    <div class="col-md-10">
                        <div class="card">
                             <%--<form class="form-horizontal">--%>
                                <div class="card-body">
                                    <h4 class="card-title">Client Listed Equity</h4>
                                     <div class="form-group row">

                                          
                                   <asp:Label ID="Label12" runat="server" Text="Date" class="col-sm-2 text-right control-label col-form-label" ></asp:Label> 
                                         <div class="col-sm-4">          
                     <asp:TextBox ID="txtDate"  Class="form-control" runat="server"></asp:TextBox>
                    <asp:LinkButton ID="pickdate" runat="server" onclick="pickdate_Click" class="col-sm-3 text-right control-label col-form-label">Pick Date</asp:LinkButton>
                                            <asp:Calendar id="calendar1" runat="server" visible="false" OnSelectionChanged="calendar1_SelectionChanged"></asp:Calendar> 
                </div>
                                         </div>


                                     <div class="form-group row">
                                        <asp:Label ID="Label2" runat="server" Text="Counter" class="col-sm-2 text-right control-label col-form-label" ></asp:Label>
                                        <div class="col-sm-4">
                                           <asp:DropDownList ID="rdlistedequities" OnSelectedIndexChanged="rdlistedequities_SelectedIndexChanged"  runat="server" class="form-control"  AutoPostBack="True" ></asp:DropDownList>
                                            </div>
                                        
                                        </div>
                                    <div class="form-group row">
                                        <asp:Label ID="Label34" runat="server"  Text="Type" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtlistedEquityType"  readonly="true" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                   
                                    <div class="form-group row">
                                        <asp:Label ID="Label10" runat="server"  Text="Price" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtPrice" class="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtListedEquityId"   visible="false" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        
                                        </div>
                                    <div class="form-group row">
                                        <asp:Label ID="Label11" runat="server" Text="Quantity" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtQuantity"  class="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtListedid" Visible="false"  class="form-control" runat="server"></asp:TextBox>
                                            
                                            </div>
                                        
                                        </div>
                                      <div class="center">
                                          <asp:Label ID="Label17" runat="server" Text="Total Listed Equity Value" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                          <asp:Label ID="Lbtotallistedequity" runat="server" Text="0" class="col-sm-2 text-right control-label col-form-label"></asp:Label>


                                     <asp:Button Text="Add" ID="addlistedequity" onclick="addlistedequity_Click" class="btn btn-primary" runat="server" />
                                          <br/>
                                     <asp:Button ID="Button3" runat="server" Visible="false" onclick="Button3_Click" class="btn btn-primary" Text="update" />
                                         
                                          </div>
                                    
                                    
                                    <asp:GridView ID="grdlistedEquity" runat="server"  CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No Investment!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left" AllowPaging="True" OnPageIndexChanging="grdlistedEquity_PageIndexChanging" OnRowDataBound="grdlistedEquity_RowDataBound">
                  <AlternatingRowStyle BackColor="White" CssClass="altrowstyle" />
                <EditRowStyle BackColor="#2461BF" />
                <EmptyDataRowStyle CssClass="text-warning text-center" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" CssClass="header info" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" CssClass="rowstyle" />
                                                <Columns>
                   
                   <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton OnClientClick="if (!confirm('Are you sure you want delete?')) return false;" ID="lnkDiscard" runat="server" CommandArgument="<%# Bind('id') %>" OnClick="linkDiscard">Remove</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                 
                       <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton  ID="editlisted" runat="server" CommandArgument="<%# Bind('id') %>" onclick="editlisted_Click1">Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                </Columns>
               
                  
              </asp:GridView>
                                  
                              
                                        </div>
                                   
                                    
                            </div>
                        
                                
                                 
                             

                                <%-- </form>--%>
                                 </div>
      </div>
                    
              </asp:Panel>

        <asp:Panel ID="Panel2" Visible="false" runat="server">
                <div class="row">
                    <div class="col-md-9">
                        <div class="card">
                             <%--<form class="form-horizontal">--%>
                                <div class="card-body">
                                    <h4 class="card-title">Client Bonds </h4>
                                    <div class="form-group row">
                                        <asp:Label ID="Label5" runat="server" Text="Counter" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-7">
                                          <asp:DropDownList ID="rdBonds" OnSelectedIndexChanged="rdBonds_SelectedIndexChanged"  runat="server" class="select2 form-control custom-select" style="width: 100%; height:36px;" AutoPostBack="True" ></asp:DropDownList>
                                            
                                            </div>
                                       
                                        </div>
                                    <div class="form-group row">
                                        <asp:Label ID="Label18" runat="server" Text="Type" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">

                                            <asp:TextBox ID="txtbondtype" readonly="true" Enabled="false" class="form-control" runat="server"></asp:TextBox>
                                           
                                            </div>
                                        
                                        </div>
                                    <div class="form-group row">
                                        <asp:Label ID="Label6" runat="server" Text="Value" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">

                                            <asp:TextBox ID="txtBondvalue"  class="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtBondId"  visible="false" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        
                                        </div>

                                      <div class="center">
                                          <asp:Label ID="Label14" runat="server" Text="Total Bonds Value" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                          <asp:Label ID="Lbtotalbondsvalue" runat="server" Text="0" class="col-sm-2 text-right control-label col-form-label"></asp:Label>

                                     <asp:Button Text="Add" ID="bonds" onclick="bonds_Click" class="btn btn-primary" runat="server" />
                                          <br/>
                                     <asp:Button ID="bondsedit" runat="server" Visible="false" onclick="bondsedit_Click" class="btn btn-primary" Text="update" />
                                          
                                          </div>
                                    
                                    
                                    <asp:GridView ID="grdbonds" runat="server"  CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No Investment!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left" AllowPaging="True" OnRowDataBound="grdbonds_RowDataBound">
                  <AlternatingRowStyle BackColor="White" CssClass="altrowstyle" />
                <EditRowStyle BackColor="#2461BF" />
                <EmptyDataRowStyle CssClass="text-warning text-center" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" CssClass="header info" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" CssClass="rowstyle" />
                                                <Columns>
                   
              
                   <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton OnClientClick="if (!confirm('Are you sure you want delete?')) return false;" ID="lnkDiscard" runat="server" CommandArgument="<%# Bind('id') %>" OnClick="linkDiscard">Remove</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton  ID="bondedit" runat="server" CommandArgument="<%# Bind('id') %>" onclick="bondedit_Click">Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                </Columns>
               
                  
              </asp:GridView>
                                    
                              
                                                                           </div>
                                    
                            </div>
                        
                                
                                 
                             

                                <%-- </form>--%>
                                 </div>
      </div>
                    
              </asp:Panel>
        <asp:Panel ID="Panel3" Visible="false" runat="server">
                <div class="row">
                    <div class="col-md-10">
                        <div class="card">
                             <%--<form class="form-horizontal">--%>
                                <div class="card-body">
                                    <h4 class="card-title">Client Money Market </h4>
                                     
                                     <div class="form-group row">
                                        <asp:Label ID="Label3" runat="server" Text="Counter" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-7">
                                           <asp:DropDownList ID="rdCounters" OnSelectedIndexChanged="rdCounters_SelectedIndexChanged" runat="server" class="select2 form-control custom-select" style="width: 100%; height:36px;" AutoPostBack="True" ></asp:DropDownList>
                                            </div>
                                        
                                        </div>
                                    <div class="form-group row">
                                        <asp:Label ID="Label33" runat="server" Text="Type" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtMoneyMarketType" ReadOnly="true" disabled="false"  class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    <div class="form-group row">
                                        <asp:Label ID="Label1" runat="server" Text="Value" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtValue"  class="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtID"  visible="false" class="form-control" runat="server"></asp:TextBox>
                                             <asp:TextBox ID="txtAssetclass"  visible="false" class="form-control" runat="server"></asp:TextBox>
                                              <asp:TextBox ID="txtUpdateInvestmentId"  visible="false" class="form-control" runat="server"></asp:TextBox>
                                             <asp:TextBox ID="txtUpdateInvestmentValue"  visible="false" class="form-control" runat="server"></asp:TextBox>
                                             <asp:TextBox ID="txtupdateoriginalvalue"  visible="false" class="form-control" runat="server"></asp:TextBox>
                                               <asp:TextBox ID="txtInvestmentId"  visible="false" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                       
                                        </div>
                                      <div class="center">
                                        <asp:Label ID="Label13" runat="server" Text="Total Money Market Value" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                          <asp:Label ID="llbmoneymarketvaluetotal" runat="server" Text="0" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                     <asp:Button Text="Add" ID="btnmoneymarket" OnClick="Unnamed_Click"  class="btn btn-primary" runat="server" />
                                          
                                     <asp:Button ID="editmoneymarket"  runat="server" OnClick="editmoneymarket_Click" class="btn btn-primary" Text="Update" />
                                          
                                          <asp:Button ID="Button1" Class= "btn btn-primary"   OnClientClick="refreshParent();" OnClick="Button1_Click" runat="server" Text="Done" />
                                          </div>
                                    
                                    
                                    <asp:GridView ID="grdApps" runat="server"  CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No Money Market!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left" AllowPaging="True" OnRowDataBound="grdApps_RowDataBound">
                  <AlternatingRowStyle BackColor="White" CssClass="altrowstyle" />
                <EditRowStyle BackColor="#2461BF" />
                <EmptyDataRowStyle CssClass="text-warning text-center" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" CssClass="header info" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" CssClass="rowstyle" />
                                                <Columns>
                   
              
                   <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton OnClientClick="if (!confirm('Are you sure you want delete?')) return false;" ID="lnkDiscard" runat="server" CommandArgument="<%# Bind('id') %>" OnClick="linkDiscard">Remove</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton  ID="lnkedit" runat="server" CommandArgument="<%# Bind('id') %>" OnClick="lnkedit">Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                </Columns>
               
                  
              </asp:GridView>
                                    
                                    
                              
                                        </div>


                                   
                                    
                            </div>
                        
                                
                                 
                             

                                <%-- </form>--%>
                                 </div>
      </div>
                    
              </asp:Panel>
        
         <asp:Panel ID="Panel5" Visible="false" runat="server">
                <div class="row">
                    <div class="col-md-9">
                        <div class="card">
                             <%--<form class="form-horizontal">--%>
                                <div class="card-body">
                                    <h4 class="card-title">Client Listed Property</h4>
                                     <div class="form-group row">

                                          
                                   <asp:Label ID="Label37" runat="server" Text="Date" class="col-sm-2 text-right control-label col-form-label" ></asp:Label> 
                                         <div class="col-sm-4">          
                     <asp:TextBox ID="txtListedPropertyDate"  Class="form-control" runat="server"></asp:TextBox>
                    <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" class="col-sm-3 text-right control-label col-form-label">Pick Date</asp:LinkButton>
                                            <asp:Calendar id="calendar2" runat="server" visible="false" OnSelectionChanged="calendar2_SelectionChanged"></asp:Calendar> 
                </div>
                                         </div>
                                    <div class="form-group row">
                                        <asp:Label ID="Label4" runat="server" Text="Counter" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-7">
                                           <asp:DropDownList ID="rdListedProperty" OnSelectedIndexChanged="rdListedProperty_SelectedIndexChanged" runat="server" class="select2 form-control custom-select" style="width: 100%; height:36px;" AutoPostBack="True" ></asp:DropDownList>
                                            </div>
                                        
                                        </div>
                                    <div class="form-group row">
                                        <asp:Label ID="Label35" runat="server"  Text="Type" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtListedPropertyType"  Enabled="false" readonly="true" class="form-control" runat="server"></asp:TextBox>
                                          
                                            </div>
                                        </div>
                                        
                                     <div class="form-group row">
                                        <asp:Label ID="Label32" runat="server"  Text="Price" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtListedPropertyPrice"  readonly="false" class="form-control" runat="server"></asp:TextBox>
                                          
                                            </div>
                                        
                                        </div>
                                    <div class="form-group row">
                                        <asp:Label ID="Label9" runat="server" Text="Quantity" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtListedPropertyValue"  class="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtListedPropertyId"  visible="false" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        
                                        </div>
                                    
                                      <div class="center">
                                          <asp:Label ID="Label15" runat="server" Text="Total Listed Property Value" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                          <asp:Label ID="lbtotalpropertyvalue" runat="server" Text="0" class="col-sm-2 text-right control-label col-form-label"></asp:Label>

                                     <asp:Button Text="Add" ID="addlistedproperty" onclick="addlistedproperty_Click" class="btn btn-primary" runat="server" />
                                          <br/>
                                     <asp:Button ID="editlisted" runat="server" Visible="false" onclick="editlisted_Click" class="btn btn-primary" Text="update" />
                                          
                                          </div>
                                    
                                    
                                    <asp:GridView ID="grdListedProperty" runat="server"  CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No Investment!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left" AllowPaging="True" OnRowDataBound="grdListedProperty_RowDataBound">
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
                            <asp:LinkButton  ID="listaproperty" runat="server" CommandArgument="<%# Bind('id') %>" OnClick="listaproperty_Click" >Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                </Columns>
               
                  
              </asp:GridView>
                              
                                        </div>
                                   
                                    
                            </div>
                        
                                
                                 
                             

                                <%-- </form>--%>
                                 </div>
      </div>
                    
              </asp:Panel>
         <asp:Panel ID="Panel6" Visible="false" runat="server">
                <div class="row">
                    <div class="col-md-8">
                        <div class="card">
                             <%--<form class="form-horizontal">--%>
                                <div class="card-body">
                                    <h4 class="card-title">Client Foreign Equity </h4>
                                    <div class="form-group row">
                                        <asp:Label ID="Label7" runat="server" Text="Counter" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-7">
                                           <asp:DropDownList ID="rdForeignEquity" OnSelectedIndexChanged="rdForeignEquity_SelectedIndexChanged" runat="server" class="select2 form-control custom-select" style="width: 100%; height:36px;" AutoPostBack="True" ></asp:DropDownList>
                                            </div>
                                        
                                        </div>
                                     <div class="form-group row">
                                        <asp:Label ID="Label36" runat="server" Text="Type" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtForeignEquityType" readonly="true" Enabled="false"  class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                         </div>
                                    <div class="form-group row">
                                        <asp:Label ID="Label8" runat="server" Text="Value" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtForeignEquityValue"  class="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtForeignEquityId"  visible="false" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        
                                        </div>
                                      <div class="center">
                                          <asp:Label ID="Label16" runat="server" Text="Total Foreign Equity Value" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                          <asp:Label ID="lbforeignequitysum" runat="server" Text="0" class="col-sm-2 text-right control-label col-form-label"></asp:Label>

                                     <asp:Button Text="Add" ID="foreignequityadd" onclick="foreignequityadd_Click" class="btn btn-primary" runat="server" />
                                          <br/>
                                     <asp:Button ID="editforeignequity" runat="server" Visible="false" onclick="editforeignequity_Click" class="btn btn-primary" Text="update" />
                                          
                                          </div>
                                    
                                    
                                    <asp:GridView ID="grdforeignequity" runat="server"  CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No Investment!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left" AllowPaging="True" OnRowDataBound="grdforeignequity_RowDataBound">
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
                            <asp:LinkButton  ID="foreigns" runat="server" CommandArgument="<%# Bind('id') %>" onclick="foreigns_Click">Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                </Columns>
               
                  
              </asp:GridView>
                                    
                              
                                        </div>
                                   
                                    
                            </div>
                        
                                
                                 
                             

                                <%-- </form>--%>
                                 </div>
      </div>
                    
              </asp:Panel>
        <asp:Panel ID="unlistedproperty" Visible="false" runat="server">
                <div class="row">
                    <div class="col-md-9">
                        <div class="card">
                             <%--<form class="form-horizontal">--%>
                                <div class="card-body">
                                    <h4 class="card-title">Client Unlisted Property </h4>
                                    <div class="form-group row">
                                        <asp:Label ID="Label19" runat="server" Text="Counter" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-7">
                                          <asp:DropDownList ID="rdUnlistedProperty"  OnSelectedIndexChanged="rdUnlistedProperty_SelectedIndexChanged" runat="server" class="select2 form-control custom-select" style="width: 100%; height:36px;" AutoPostBack="True" ></asp:DropDownList>
                                            
                                            </div>
                                       
                                        </div>
                                    <div class="form-group row">
                                        <asp:Label ID="Label20" runat="server" Text="Type" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">

                                            <asp:TextBox ID="txtUnlistedPropertyType" readonly="true" Enabled="false" class="form-control" runat="server"></asp:TextBox>
                                           
                                            </div>
                                        
                                        </div>
                                    <div class="form-group row">
                                        <asp:Label ID="Label21" runat="server" Text="Value" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">

                                            <asp:TextBox ID="txtUnlistedPropertyValue"  class="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtUnlistedPropertyId"  visible="false" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        
                                        </div>

                                      <div class="center">
                                          <asp:Label ID="Label22" runat="server" Text="Total  Value" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                          <asp:Label ID="Label23" runat="server" Text="0" class="col-sm-2 text-right control-label col-form-label"></asp:Label>

                                     <asp:Button Text="Add" ID="Button2"  onclick="Button2_Click" class="btn btn-primary" runat="server" />
                                          <br/>
                                     <asp:Button ID="Button4" OnClick="Button4_Click" runat="server" Visible="false"  class="btn btn-primary" Text="update" />
                                          
                                          </div>
                                    
                                    
                                    <asp:GridView ID="grdUnlistedProperty" runat="server"  CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No Investment!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left" AllowPaging="True" OnRowDataBound="grdUnlistedProperty_RowDataBound">
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
                            <asp:LinkButton  ID="unlistedproperty" runat="server" CommandArgument="<%# Bind('id') %>" onclick="unlistedproperty_Click">Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                </Columns>
               
                  
              </asp:GridView>
                                    
                              
                                        </div>
                                   
                                    
                            </div>
                        
                                
                                 
                             

                                <%-- </form>--%>
                                 </div>
      </div>
                    
              </asp:Panel>

        <asp:Panel ID="CashNostro" Visible="false" runat="server">
                <div class="row">
                    <div class="col-md-9">
                        <div class="card">
                             <%--<form class="form-horizontal">--%>
                                <div class="card-body">
                                    <h4 class="card-title">Cash Nostro </h4>
                                    <div class="form-group row">
                                        <asp:Label ID="Label60" runat="server" Text="Value" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">

                                            <asp:TextBox ID="txtCashNostroValue"  class="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtCashNostroValueID"  visible="false" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        
                                        </div>

                                      <div class="center">
                                          <asp:Label ID="Label61" runat="server" Text="Total  Value" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                          <asp:Label ID="Label62" runat="server" Text="0" class="col-sm-2 text-right control-label col-form-label"></asp:Label>

                                     <asp:Button Text="Add" ID="Button98"  onclick="Button100_Click" class="btn btn-primary" runat="server" />
                                          <br/>
                                     <asp:Button ID="Button99" OnClick="Button101_Click" runat="server" Visible="false"  class="btn btn-primary" Text="update" />
                                          
                                          </div>
                                    
                                    
                                    <asp:GridView ID="grdCashNostro" runat="server"  CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No Investment!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left" AllowPaging="True" OnRowDataBound="grdCashNostro_RowDataBound">
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
                            <asp:LinkButton  ID="CashNostro" runat="server" CommandArgument="<%# Bind('id') %>" onclick="CashNostro_Click">Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                </Columns>
               
                  
              </asp:GridView>
                                        </div>
                                   
                            </div>
                        
                                <%-- </form>--%>
                                 </div>
      </div>
                    
              </asp:Panel>

              <asp:Panel ID="cash" Visible="false" runat="server">
                <div class="row">
                    <div class="col-md-9">
                        <div class="card">
                             <%--<form class="form-horizontal">--%>
                                <div class="card-body">
                                    <h4 class="card-title">Cash </h4>
                                    <div class="form-group row">
                                        <asp:Label ID="Label40" runat="server" Text="Value" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">

                                            <asp:TextBox ID="txtCashValue"  class="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtCashValueID"  visible="false" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        
                                        </div>

                                      <div class="center">
                                          <asp:Label ID="Label41" runat="server" Text="Total  Value" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                          <asp:Label ID="Label42" runat="server" Text="0" class="col-sm-2 text-right control-label col-form-label"></asp:Label>

                                     <asp:Button Text="Add" ID="Button9"  onclick="Button13_Click" class="btn btn-primary" runat="server" />
                                          <br/>
                                     <asp:Button ID="Button10" OnClick="Button14_Click" runat="server" Visible="false"  class="btn btn-primary" Text="update" />
                                          
                                          </div>
                                    
                                    
                                    <asp:GridView ID="grdCash" runat="server"  CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No Investment!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left" AllowPaging="True" OnRowDataBound="grdCash_RowDataBound">
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
                            <asp:LinkButton  ID="cash" runat="server" CommandArgument="<%# Bind('id') %>" onclick="cash_Click">Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                </Columns>
                  
              </asp:GridView>
                                    
                                        </div>
                                   
                            </div>
                        
                                <%-- </form>--%>
                                 </div>
      </div>
                    
              </asp:Panel>

        <asp:Panel ID="GuaranteedFund" Visible="false" runat="server">
                <div class="row">
                    <div class="col-md-9">
                        <div class="card">
                             <%--<form class="form-horizontal">--%>
                                <div class="card-body">
                                    <h4 class="card-title">Guaranteed Fund </h4>
                                    <div class="form-group row">
                                        <asp:Label ID="Label38" runat="server" Text="Value" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">

                                            <asp:TextBox ID="txtGFundValue"  class="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtGFundValueID"  visible="false" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        
                                        </div>

                                      <div class="center">
                                          <asp:Label ID="Label50" runat="server" Text="Total  Value" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                          <asp:Label ID="Label52" runat="server" Text="0" class="col-sm-2 text-right control-label col-form-label"></asp:Label>

                                     <asp:Button Text="Add" ID="Button11"  onclick="Button15_Click" class="btn btn-primary" runat="server" />
                                          <br/>
                                     <asp:Button ID="Button12" OnClick="Button16_Click" runat="server" Visible="false"  class="btn btn-primary" Text="update" />
                                          
                                          </div>
                                    
                                    
                                    <asp:GridView ID="grdGFund" runat="server"  CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No Investment!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left" AllowPaging="True" OnRowDataBound="grdGFund_RowDataBound">
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
                            <asp:LinkButton  ID="GFund" runat="server" CommandArgument="<%# Bind('id') %>" onclick="GFund_Click">Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                </Columns>
                  
              </asp:GridView>
                                    
                                        </div>
                                   
                            </div>
                        
                                <%-- </form>--%>
                                 </div>
      </div>
                    
              </asp:Panel>

         <asp:Panel ID="alternativeinvestments" Visible="false" runat="server">
                <div class="row">
                    <div class="col-md-9">
                        <div class="card">
                             <%--<form class="form-horizontal">--%>
                                <div class="card-body">
                                    <h4 class="card-title">Client Alternative Investments </h4>
                                    <%--<div class="form-group row">
                                        <asp:Label ID="Label24" runat="server" Text="Counter" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-7">
                                          <asp:DropDownList ID="rdAlternativeInvestments"   runat="server" class="select2 form-control custom-select" style="width: 100%; height:36px;" AutoPostBack="True" OnSelectedIndexChanged="rdAlternativeInvestments_SelectedIndexChanged" ></asp:DropDownList>
                                            
                                            </div>
                                       
                                        </div>--%>
                                    <%--<div class="form-group row">
                                        <asp:Label ID="Label25" runat="server" Text="Type" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">

                                            <asp:TextBox ID="txtAlternativeInvestmentType" readonly="true" Enabled="false" class="form-control" runat="server"></asp:TextBox>
                                           
                                            </div>
                                        
                                        </div>--%>
                                    <div class="form-group row">
                                        <asp:Label ID="Label26" runat="server" Text="Value" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">

                                            <asp:TextBox ID="txtAlternativeInvestmentValue"  class="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtAlternativeInvestmentId"  visible="false" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        
                                        </div>

                                      <div class="center">
                                          <asp:Label ID="Label27" runat="server" Text="Total  Value" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                          <asp:Label ID="lbAlternativeInvestmentTotal" runat="server" Text="0" class="col-sm-2 text-right control-label col-form-label"></asp:Label>

                                     <asp:Button Text="Add" ID="Button5"  onclick="Button5_Click" class="btn btn-primary" runat="server" />
                                          <br/>
                                     <asp:Button ID="Button6" OnClick="Button6_Click" runat="server" Visible="false"  class="btn btn-primary" Text="update" />
                                          
                                          </div>
                                    
                                    
                                    <asp:GridView ID="grdAlternativeInvestments" runat="server"  CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No Investment!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left" AllowPaging="True" OnRowDataBound="grdAlternativeInvestments_RowDataBound">
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
                            <asp:LinkButton  ID="alternativeinvementsid" runat="server" CommandArgument="<%# Bind('id') %>" onclick="alternativeinvementsid_Click">Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                </Columns>
               
                  
              </asp:GridView>
                                    
                              
                                        </div>
                                   
                                    
                            </div>
                        
                                
                                 
                             

                                <%-- </form>--%>
                                 </div>
      </div>
                    
              </asp:Panel>
        <asp:Panel ID="unlistedequity" Visible="false" runat="server">
                <div class="row">
                    <div class="col-md-9">
                        <div class="card">
                             <%--<form class="form-horizontal">--%>
                                <div class="card-body">
                                    <h4 class="card-title">Client Unlisted Equity </h4>
                                    <%--<div class="form-group row">
                                        <asp:Label ID="Label28" runat="server" Text="Counter" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-7">
                                          <asp:DropDownList ID="rdUnlistedEquity" OnSelectedIndexChanged="rdUnlistedEquity_SelectedIndexChanged"  runat="server" class="select2 form-control custom-select" style="width: 100%; height:36px;" AutoPostBack="True" ></asp:DropDownList>
                                            
                                            </div>
                                       
                                        </div>--%>
                                    <%--<div class="form-group row">
                                        <asp:Label ID="Label29" runat="server" Text="Type" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">

                                            <asp:TextBox ID="txtUnlistedEquityType" readonly="true" Enabled="false" class="form-control" runat="server"></asp:TextBox>
                                           
                                            </div>
                                        
                                        </div>--%>
                                    <div class="form-group row">
                                        <asp:Label ID="Label30" runat="server" Text="Value" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">

                                            <asp:TextBox ID="txtUnlistedEquityValue"  class="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtUnlistedEquityId"  visible="false" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        
                                        </div>

                                      <div class="center">
                                          <asp:Label ID="Label31" runat="server" Text="Total  Value" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                          <asp:Label ID="lbtotalUnlistedEquty" runat="server" Text="0" class="col-sm-2 text-right control-label col-form-label"></asp:Label>

                                     <asp:Button Text="Add" ID="Button7"  onclick="Button7_Click" class="btn btn-primary" runat="server" />
                                          <br/>
                                     <asp:Button ID="Button8" OnClick="Button8_Click" runat="server" Visible="false"  class="btn btn-primary" Text="update" />
                                          
                                          </div>
                                    
                                    
                                    <asp:GridView ID="grdUnlistedEquity" runat="server"  CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No Investment!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left" AllowPaging="True" OnRowDataBound="grdUnlistedEquity_RowDataBound">
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
                            <asp:LinkButton  ID="unlistedequityid" runat="server" CommandArgument="<%# Bind('id') %>" onclick="unlistedequityid_Click">Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                </Columns>
               
                  
              </asp:GridView>
                                    
                              
                                        </div>
                                   
                                    
                            </div>
                        
                                
                                 
                             

                                <%-- </form>--%>
                                 </div>
      </div>
                    
              </asp:Panel>

       
        </div>
   
<script type="text/javascript">
    $(function () {
        $("[id*=txtDate]").datepicker({
            showOn: 'button',
            buttonImageOnly: true,
            buttonImage: 'images/calendar.png'
        });
    });
</script>
    <script type="text/javascript">
        function refreshParent() {
            window.opener.location.href = window.opener.location.href;
            if (window.opener.progressWindow) {
                window.opener.progressWindow.close()
            }
            window.close();

        }

    </script>
    
</asp:Content>

