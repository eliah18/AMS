<%@ Page Title="" Language="C#" MasterPageFile="~/admin/parameters/Parameter.master" AutoEventWireup="true" CodeFile="PricesUpload.aspx.cs" Inherits="admin_parameters_User" %>

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
                        <h4 class="page-title">Historical Prices Upload </h4><br />
                        <asp:LinkButton ID="LinkButton1" runat="server"  Visible="false" OnClick="LinkButton1_Click">[Add] </asp:LinkButton>
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
          
          <asp:Panel ID="usersPanel" Visible="true" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card">
                             <%--<form class="form-horizontal">--%>
                                <div class="card-body">
                                    <h4 class="card-title">Prices  <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton1_Click">[Add]</asp:LinkButton></h4> 
                                     <div class="form-group row">    

                                          
                                   <asp:Label ID="Label12" runat="server" Text="Date" class="col-sm-2 text-right control-label col-form-label" ></asp:Label> 
                                         <div class="col-sm-4">          
                     <asp:TextBox ID="txtDate"  Class="form-control" OnTextChanged="txtDate_TextChanged" runat="server"    AutoPostBack="true"></asp:TextBox>
                    <asp:LinkButton ID="pickdate" runat="server" onclick="pickdate_Click" class="col-sm-3 text-right control-label col-form-label">Pick Date</asp:LinkButton>
                                            <asp:Calendar id="calendar1" runat="server" visible="false" OnSelectionChanged="calendar1_SelectionChanged"></asp:Calendar> 
                </div>
                                         </div>

                                    
                                    
                                   
                                      <div class="form-group row">
                                        <asp:Label ID="Label4" runat="server" Text="Upload File" class="col-sm-3 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-8">
                                            
                                            <asp:FileUpload ID="FileUpload1" class="col-sm-8 text-left control-label col-form-label"    runat="server" />
                                            </div>
                                        </div>
                                   
                            </div>
                            <div class="center">
                             <div class="border-top">
                                    <div class="card-body">
                                        <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Save" OnClick="Button1_Click"  onClientClick="Confirm()"/>
                                           <asp:Button ID="Button2" runat="server" class="btn btn-primary" visible="false" Text="Save" OnClick="Button2_Click" />
                                 
                                 <asp:Button ID="Button3" runat="server" class="btn btn-primary" Text="Back" OnClick="Button3_Click" />
                                        </div>
                              
                                 </div>
                                </div>
                            <div class="col-sm-10">
                            
                            <asp:GridView ID="grdApps" Visible="false" runat="server"  CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No Prices!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left" AllowPaging="True"  OnPageIndexChanging="grdApps_PageIndexChanging">
                  <AlternatingRowStyle BackColor="White" CssClass="altrowstyle" />
                <EditRowStyle BackColor="#2461BF" />
                <EmptyDataRowStyle CssClass="text-warning text-center" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" CssClass="header info" Font-Bold="True" ForeColor="White" />

                <RowStyle BackColor="#EFF3FB" CssClass="rowstyle" />
                                 <Columns>
                   
              
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
                            

                                <%-- </form>--%>
                                 </div>
                        </div>
                    </div>
              </asp:Panel>
          <asp:Panel ID="grdpanel" Visible="false" runat="server">
           <div class="row">
               <div class="col-8">
                        <div class="card">
                            <div class="card-body">
                                 <h4 class="card-title">Prices Update</h4> 
                                <div class="form-group row">
                                        <asp:Label ID="Label1" runat="server" Text="Price Date" class="col-sm-3 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtPriceDate"   ReadOnly="true"  Enabled="false"  class="form-control" runat="server"></asp:TextBox>
                                             
                                            </div>
                                        </div>
                                 <div class="form-group row">
                                        <asp:Label ID="Label2" runat="server" Text="Counter" class="col-sm-3 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtCounter" Visible="false"  ReadOnly="true" Enabled="false" class="form-control" runat="server"></asp:TextBox>
                                           
                                             <asp:DropDownList runat="server" id="rdCounters" class="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                       <div class="form-group row">
                                        <asp:Label ID="Label3" runat="server" Text="Price" class="col-sm-3 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtPrice"  class="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtPriceId"  Visible="false" runat="server"></asp:TextBox>
                                             
                                            </div>
                                        </div>
                                
                                 <div class="center">
                             <div class="border-top">
                                    <div class="card-body">
                                       
                                 
                                 <asp:Button ID="Button6" runat="server" class="btn btn-primary" Text="Save" onclick="Button6_Click" />
                                        </div>
                              
                                 </div>
                                </div>
              
          </div>
                            </div>
                   </div>
          </div>
              </asp:Panel>
          
          </div>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Prices For The Selected Date  Already Exit.Are You Sure You Want To Override The Prices?")) {
                confirm_value.value = "Yes";
            }
            else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

         
</asp:Content>

