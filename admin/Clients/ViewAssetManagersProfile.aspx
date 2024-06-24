<%@ Page Title="" Language="C#" MasterPageFile="~/admin/user/Modal.master" AutoEventWireup="true" CodeFile="ViewAssetManagersProfile.aspx.cs" Inherits="admin_ClientStatus_ClientFundsLinked" %>

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
                        <h4 class="page-title">Asset Manager </h4><br />
                       
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
                                        
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtFirstName"  class="form-control" runat="server" Visible="false"></asp:TextBox>
                                            </div>
                                        </div>
                                     <div class="form-group row">
                                        <asp:Label ID="Label2" runat="server" Text="Name" class="col-sm-2 text-right control-label col-form-label"    ></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtSurname"  class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                             <asp:TextBox ID="txtID"  class="form-control" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
                                            </div>
                                          <asp:Label ID="Label3" runat="server" Text="FundManager" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtBenchmark"  class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                            
                                            </div>
                                        </div>
                                     
                                    <div class="form-group row">
                                        <asp:Label ID="Label4" runat="server" Text="Strategy" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtStrategy"  class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                            
                                            </div>
                                         <asp:Label ID="Label1" runat="server" Text="Philosophy" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtPhilosophy"  class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                            
                                            </div>
                                        </div>
                                       
                                    <div class="form-group row">
                                        <asp:Label ID="Label5" runat="server" Text="Contact Details" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtContactDetails"  class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                            
                                            </div>
                                        <asp:Label ID="Label6" runat="server" Text="Addresss" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtAddress"  class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                            
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

