﻿<%@ Page Title="" Language="C#" MasterPageFile="~/admin/parameters/Parameter.master" AutoEventWireup="true" CodeFile="AlternativeInvestments.aspx.cs" Inherits="admin_parameters_User" %>

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
                        <h4 class="page-title">Alternative Investments Capturing </h4><br />
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">[Add]</asp:LinkButton>
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
              <asp:GridView ID="grdApps" runat="server"  CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No Investments!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left" AllowPaging="True" OnPageIndexChanging="grdApps_PageIndexChanging">
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
                            <asp:LinkButton  ID="lnkedit" runat="server" CommandArgument="<%# Bind('id') %>" OnClick="lnkedit">Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkdelete" runat="server" CommandName="Delete" >Delete</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
              </asp:GridView>
          </div>
                            </div>
                   </div>
          </div>
              </asp:Panel>
          <asp:Panel ID="usersPanel" Visible="false" runat="server">
                <div class="row">
                    <div class="col-md-8">
                        <div class="card">
                             <%--<form class="form-horizontal">--%>
                                <div class="card-body">
                                    <h4 class="card-title">Alternative Investments</h4>
                                    
                                     <div class="form-group row">
                                        <asp:Label ID="Label2" runat="server" Text="Investment Type" class="col-sm-3 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="cmbCounter"  class="form-control" runat="server">
                                                <asp:ListItem Value="Prescribed">Prescribed</asp:ListItem>
                                                <asp:ListItem Value="Other">Other</asp:ListItem>
                                            </asp:DropDownList>
                                             <asp:TextBox ID="txtID"  class="form-control" runat="server" Visible="false"></asp:TextBox>
                                            </div>
                                        </div>
                                     <div class="form-group row">
                                        <asp:Label ID="Label1" runat="server" Text="Investment Name" class="col-sm-3 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtBond"  class="form-control" runat="server"></asp:TextBox>
                                             
                                            </div>
                                        </div>
                                     <div class="form-group row">
                                        <asp:Label ID="Label8" runat="server" Text="Asset Manager" class="col-sm-3 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-9">
                                            <%--<asp:DropDownList ID="cmbAssetManager" runat="server"    class="form-control"  AutoPostBack="True" ></asp:DropDownList>--%>
                                            <asp:ListBox ID="cmbAssetManager" class="form-control" runat="server"  AutoPostBack ="false"  SelectionMode="Multiple"></asp:ListBox>
                                            <br />
                                            <asp:Button ID="addAssetmanager" class="btn btn-primary" onclick="addAssetmanager_Click" runat="server" Text="Add"   /> 
                                            </div>
                                         
                                        
                                        </div>
                                    <asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <div class="form-group row">

                                         <div class="col-sm-2">
                                             </div>
                                        
                                        <div class="col-sm-8">
                                            <asp:Label ID="LbselecdtedAssetmanagers" Visible="true" runat="server" Text="Selected Managers" ></asp:Label>
                                             <asp:GridView ID="grdAssetmanagers" runat="server"  CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No AssetManagers" ForeColor="#333333" GridLines="None" HorizontalAlign="Left" AllowPaging="True" >
                  <AlternatingRowStyle BackColor="White" CssClass="altrowstyle" />
                <EditRowStyle BackColor="#2461BF" />
                <EmptyDataRowStyle CssClass="text-warning text-center" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" CssClass="header info" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" CssClass="rowstyle" />
               
                   <Columns>
                   
              
                   <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton OnClientClick="if (!confirm('Are you sure you want remove this assetmanager?')) return false;" ID="lnkAssetManagerDiscard" runat="server" CommandArgument="<%# Bind('id') %>" OnClick="linkDiscardAssetManagers">Remove</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                       
                      
                    <%-- <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkdelete" runat="server" CommandName="Delete" >Delete</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
              </asp:GridView>
                                            
                                            </div>
                                         

                                        </div>
                                        </asp:Panel>
                                   
                                   
                            </div>
                             <div class="border-top">
                                 <div class="center">
                                    <div class="card-body">
                                        <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Save" OnClick="Button1_Click" />
                                           <asp:Button ID="Button2" runat="server" class="btn btn-primary" visible="false" Text="Save" OnClick="Button2_Click" />
                                 
                                 <asp:Button ID="Button3" runat="server" class="btn btn-primary" Text="Back" OnClick="Button3_Click" />
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

