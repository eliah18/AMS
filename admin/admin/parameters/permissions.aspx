<%@ Page Title="" Language="C#" MasterPageFile="~/admin/parameters/Investor.master" AutoEventWireup="true" CodeFile="permissions.aspx.cs" Inherits="admin_parameters_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
     <div class="page-breadcrumb">
                <div class="row">
                    <div class="col-12 d-flex no-block align-items-center">
                        <h4 class="page-title">System  Permissions</h4><br />
                       
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
               <div class="col-12">
                        <div class="card">
                            <div class="card-body">
                                <div class="form-group row">
                                        <asp:Label ID="Label3" runat="server" Text="Usertype" class="col-sm-3 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <%--<asp:TextBox ID="txtRole"  class="form-control" runat="server"></asp:TextBox>--%>
                                            <asp:DropDownList ID="rdRole" runat="server" class="select2 form-control custom-select" style="width: 100%; height:36px;"></asp:DropDownList>
                                            </div>

                                        </div>
              <asp:GridView ID="grdApps" runat="server"  CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No Roles!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left" OnSelectedIndexChanged="grdApps_SelectedIndexChanged1" AutoGenerateColumns="False" AutoGenerateSelectButton="True">
                  <AlternatingRowStyle BackColor="White" CssClass="altrowstyle" />
                <EditRowStyle BackColor="#2461BF" />
                <EmptyDataRowStyle CssClass="text-warning text-center" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" CssClass="header info" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" CssClass="rowstyle" />
                <Columns>
                               <asp:BoundField HeaderText ="Rowid"  DataField ="id" />  
                              <asp:BoundField HeaderText ="Form url" DataField ="url" />  
                                <asp:BoundField HeaderText ="Modulename" DataField ="modulename" />
                                <asp:BoundField HeaderText ="Parent" DataField ="Parent" />  
    
    
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
                                    <h4 class="card-title">Details</h4>
                                    
                                    <div class="form-group row">
                                        <asp:Label ID="Label1" runat="server" Text="Name" class="col-sm-3 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtFirstName"  class="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtID"  class="form-control" runat="server" Visible="false"></asp:TextBox>
                                            </div>
                                        </div>
                                     
                                   
                            </div>
                             <div class="border-top">
                                    <div class="card-body">
                                        
                                          
                                 
                                 <asp:Button ID="Button3" runat="server" class="btn btn-primary" Text="Back" OnClick="Button3_Click" />
                                        </div>
                              
                                 </div>

                                <%-- </form>--%>
                                 </div>
                        </div>
                    </div>
              </asp:Panel>
          
          </div>

         
</asp:Content>

