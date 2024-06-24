<%@ Page Title="" Language="C#" MasterPageFile="~/admin/parameters/Parameter.master" AutoEventWireup="true" CodeFile="EquitiesCapture.aspx.cs" Inherits="admin_parameters_User" EnableEventValidation="false" %>

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
                        <h4 class="page-title">Equities Capturing </h4><br />
                        
                        <div class="ml-auto text-right">
                            <nav aria-label="breadcrumb">
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="#">Home</a></li>
                                    <li class="breadcrumb-item active" aria-current="page">Library</li>
                                </ol>
                            </nav>
                        </div>
                    </div>
                </div>
            </div>
      <div class="container-fluid">
          <asp:Panel ID="grdpanel" Visible="true" runat="server">
           <div class="row">
               <div class="col-8">
                        <div class="card">
                            <div class="card-body">
                                <div class="form-group row">    

                                          
                                   <asp:Label ID="Label12" runat="server" Text="Date" class="col-sm-2 text-right control-label col-form-label" ></asp:Label> 
                                         <div class="col-sm-4">          
                     <asp:TextBox ID="txtDate"  Class="form-control" runat="server"></asp:TextBox>
                    <asp:LinkButton ID="pickdate" runat="server" onclick="pickdate_Click" class="col-sm-3 text-right control-label col-form-label">Pick Date</asp:LinkButton>
                                            <asp:Calendar id="calendar1" runat="server" visible="false" OnSelectionChanged="calendar1_SelectionChanged"></asp:Calendar> 
                </div>
                                     <div class="col-sm-4"> 
                                         <asp:Button ID="BtnView" onclick="BtnView_Click" class= "btn btn-primary" runat="server" Text="View" />
                                         </div>
                                         </div>
                               <div class="center">
                                <div class="border-top">
                                    <div class="card-body">
                                        <asp:Button ID="Button2" onclick="Button2_Click" class= "btn btn-primary" runat="server" Text="Fetch Prices" />
                                        </div>
                              
                                 </div>
                                   </div>






              <asp:GridView ID="grdApps" runat="server" CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No Prices!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left" AllowPaging="True" OnPageIndexChanging="grdApps_PageIndexChanging" OnSelectedIndexChanged="grdApps_SelectedIndexChanged" >
                  
                  <AlternatingRowStyle BackColor="White" CssClass="altrowstyle" />
               <%-- <EditRowStyle BackColor="#2461BF" />--%>
                <EmptyDataRowStyle CssClass="text-warning text-center" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" CssClass="header info" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" CssClass="rowstyle" />

                  <Columns>
                            <%--<asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False"
                                        CommandName="Delete" OnClientClick="return isDelete();" Text="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                           <%-- <asp:TemplateField ShowHeader="False">
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False"
                                        CommandName="Update" Text="Update"></asp:LinkButton>
                                    &nbsp;<asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False"
                                        CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False"
                                        CommandName="Edit" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                           <%-- <asp:TemplateField HeaderText="Role ID">
                                <EditItemTemplate>
                                    <asp:Label ID="lblRoleIDEdit" runat="server"><%#Eval("RoleID")%></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblRoleID" runat="server"><%#Eval("RoleID")%></asp:Label>
                                    <asp:TextBox ID="txtRoleIDEdit" runat="server" Text='<%# Bind("RoleID")%>'
                                        Visible="False"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Role Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRoleNameEdit" runat="server" Text='<%# Bind("RoleName")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblRoleName" runat="server"><%#Eval("RoleName")%></asp:Label>
                                    <asp:TextBox ID="txtRoleName" runat="server" Text='<%# Bind("RoleName")%>'
                                        Visible="False"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                          <%--  <asp:TemplateField HeaderText="Dashboard View">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDashboardViewEdit" runat="server" Text='<%# Bind("DASHBOARD")%>'
                                        Visible="False"></asp:TextBox>
                                    <asp:DropDownList ID="cmbDashboardViewEdit" runat="server">
                                        <asp:ListItem Value="" Text=""></asp:ListItem>
                                        <asp:ListItem Value="Individual" Text="Individual"></asp:ListItem>
                                        <asp:ListItem Value="Branch" Text="Branch"></asp:ListItem>
                                        <asp:ListItem Value="Overall" Text="Overall"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDashboardView" runat="server"><%#Eval("DASHBOARD")%></asp:Label>
                                    <asp:TextBox ID="txtDashboardView" runat="server" Text='<%# Bind("DASHBOARD")%>'
                                        Visible="False"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                          <%--  <asp:TemplateField HeaderText="User Status">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkStatusEdit" runat="server"
                                        Checked='<%# Bind("USER_STATUS")%>' />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkStatus" runat="server" Checked='<%# Bind("USER_STATUS")%>'
                                        Enabled="False" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>



               
<%--                   <Columns>
                   
              
                   <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton OnClientClick="if (!confirm('Are you sure you want delete?')) return false;" ID="lnkDiscard" runat="server" CommandArgument="<%# Bind('id') %>" OnClick="linkDiscard">Delete</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton  ID="lnkedit" runat="server" CommandArgument="<%# Bind('id') %>" OnClick="lnkedit">Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                </Columns>--%>
              </asp:GridView>
          </div>
                             <div class="center">
                                <div class="border-top">
                                    <div class="card-body">
                                        <asp:Button ID="Button3" onclick="Button3_Click" Visible="false" class= "btn btn-primary" runat="server" Text="Export Prices" />
                                        </div>
                              
                                 </div>
                                   </div>

                            </div>
                   </div>
          </div>
              </asp:Panel>
          <asp:Panel ID="usersPanel" Visible="false" runat="server">
                <div class="row">
                    <div class="col-md-6">
                        <div class="card">
                             <%--<form class="form-horizontal">--%>
                                <div class="card-body">
                                    <h4 class="card-title">Equities</h4>
                                    
                                     
                                     <div class="form-group row">
                                        <asp:Label ID="Label1" runat="server" Text="Price" class="col-sm-3 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtPricedate"  class="form-control" runat="server"></asp:TextBox>
                                             
                                            </div>
                                        </div>
                                   
                                   
                            </div>
                             <div class="border-top">
                                    <div class="card-body">
                                        <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Fetchprice" OnClick="Button1_Click" />
                                          
                                 
                               
                                        </div>
                              
                                 </div>

                                <%-- </form>--%>
                                 </div>
                        </div>
                    </div>
              </asp:Panel>
          
          </div>

         
</asp:Content>

