<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Reporting/Reporting.master" AutoEventWireup="true" CodeFile="AssetAllocation.aspx.cs" Inherits="admin_parameters_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .center {
            text-align: center;
        }

        .inside {
            background-color: #eeeeee;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">

    <div class="inside">
        <div class="row">
            <div class="col-12 d-flex no-block align-items-center">
                <h4 class="page-title center">&nbsp;&nbsp;Reporting--AssetClassHolding--Report Creation-- Details Confirmation</h4>
                <br />

                <div class="ml-auto text-right">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item">
                                <asp:Label ID="lbUsername" runat="server"></asp:Label></li>
                            <li class="breadcrumb-item"><a href="~/logout.aspx" runat="server">Logout</a></li>
                        </ol>
                    </nav>
                </div>
            </div>
        </div>
    </div>
    <div class="col-xs-3">
        <%--                    <asp:HyperLink ID="HyperLink112" runat="server"
                        NavigateUrl="~/Credit/rptLoanBook.aspx" Target="_blank">Loan Book</asp:HyperLink>--%>

        <%--<a data-target="#loanbookModal1" role="button" class="" data-toggle="modal" id="loanbookModal">Loan Book</a>--%>
        <%--  <asp:HyperLink ID="HyperLink4" runat="server" Target="_blank" NavigateUrl="~/Credit/rptOperationIndicator.aspx">>Loan Book</asp:HyperLink>
               
        --%>
    </div>

    <div class="container-fluid">





        <asp:Panel ID="usersPanel" Visible="true" runat="server">
            <div class="row inside">
                <div class="col-md-12">
                    <div class="card">
                        <%--<form class="form-horizontal">--%>
                        <div class="card-body">
                            <h4 class="card-title">Report Creation  Details</h4>
                            <div class="form-group row">
                                <asp:Label ID="Label1" runat="server" Text=" Client Name" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtFirstName" Enabled="false" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <%--<asp:Label ID="Label5" runat="server" Text="Asset Manager" class="col-sm-2 text-right control-label col-form-label"></asp:Label>--%>
                                <%--<asp:Label ID="Label3" runat="server" Text="Sub Account" class="col-sm-2 text-right control-label col-form-label"></asp:Label>--%>
                                 <asp:Label ID="Label14" runat="server" Text="Quarter" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                <div class="col-sm-4">

                                    <asp:TextBox ID="txtID" class="form-control" runat="server" Visible="false"></asp:TextBox>

                                    <%--<asp:DropDownList ID="rdAssetMangers" OnSelectedIndexChanged="rdBonds_SelectedIndexChanged" runat="server" class="select2 form-control custom-select" Style="width: 100%; height: 36px;" AutoPostBack="True"></asp:DropDownList>--%>
                                    <%--<asp:DropDownList ID="rdSubAccount" class="form-control" runat="server">
                                    </asp:DropDownList>--%>
                                    <asp:TextBox ID="txtQuarter" Enabled="false" class="form-control" runat="server"></asp:TextBox>
                                    <div class="col-sm-4">
                                    
                                </div>
                                    </asp:dropdownlist>
                                            <asp:TextBox ID="txtselectedAssetManager" Enabled="false" class="form-control" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtAssetManager" Visible="false" class="form-control" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="operationid" Visible="false" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                               <%-- <asp:Label ID="Label14" runat="server" Text="Quarter" class="col-sm-2 text-right control-label col-form-label"></asp:Label>--%>
                                <%--<asp:Label ID="Label3" runat="server" Text="Sub Account" class="col-sm-2 text-right control-label col-form-label"></asp:Label>--%>
                                <asp:Label ID="Label5" runat="server" Text="Asset Manager" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                <div class="col-sm-4">
                                    <%--<asp:TextBox ID="txtQuarter" Enabled="false" class="form-control" runat="server"></asp:TextBox>--%>
                                    <%--<asp:DropDownList ID="rdSubAccount" class="form-control" runat="server">
                                    </asp:DropDownList>--%>
                                    <asp:DropDownList ID="rdAssetMangers" OnSelectedIndexChanged="rdBonds_SelectedIndexChanged" runat="server" class="select2 form-control custom-select" Style="width: 100%; height: 36px;" AutoPostBack="True"></asp:DropDownList>
                                </div>
                                <asp:Label ID="Label15" runat="server" Text="Reporting Year" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                <div class="col-sm-4">

                                    <asp:TextBox ID="txtYear" Enabled="false" class="form-control" runat="server"></asp:TextBox>

                                </div>

                            </div>
                            <div class="form-group row">
                                <%--<asp:Label ID="Label3" runat="server" Text="Sub Account" class="col-sm-2 text-right control-label col-form-label"></asp:Label>--%>
                                <%--<asp:Label ID="Label5" runat="server" Text="Asset Manager" class="col-sm-2 text-right control-label col-form-label"></asp:Label>--%>
                                <asp:Label ID="Label3" runat="server" Text="Sub Account" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                <div class="col-sm-4">
                                    <%--<asp:DropDownList ID="rdSubAccount" class="form-control" runat="server">
                                    </asp:DropDownList>--%>
                                    <%--<asp:DropDownList ID="rdAssetMangers" OnSelectedIndexChanged="rdBonds_SelectedIndexChanged" runat="server" class="select2 form-control custom-select" Style="width: 100%; height: 36px;" AutoPostBack="True"></asp:DropDownList>--%>
                                    <asp:DropDownList ID="rdSubAccount" class="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                                
                            </div>
                            <div class="center">
                            </div>
                            <div class="center">
                                <asp:Button ID="Button6" runat="server" class="btn btn-primary" Text="Back" OnClick="Button3_Click" />
                                <asp:Button ID="Button2" runat="server" visble="true" class="btn btn-primary" Text="Confirm And  Allocate" OnClick="Button1_Click" />




                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>










        <div class="border-top">
            <asp:Panel ID="Panel2" Visible="false" runat="server">




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
                                            <h4 class="card-title">Reporting
                                                <asp:Label ID="Label16" Visible="false" runat="server" Text="Label"></asp:Label>
                                            </h4>
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
                                                            <asp:Label ID="Label7" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "investmentid")%>'></asp:Label>
                                                        </td>
                                                        <td>

                                                            <asp:TextBox CssClass="form-control input-sm" ID="res" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "amount")%>'></asp:TextBox>
                                                            <asp:LinkButton ID="linked" runat="server" OnClick="lnkedit" CommandArgument='<%# Eval("id") %>' Text="[Click Here]"></asp:LinkButton>
                                                        </td>


                                                    </tr>

                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label26" runat="server" Text="Operation"></asp:Label>
                                                        </td>
                                                        <td>

                                                            <asp:Button ID="Button3" Visible="true" runat="server" class="btn btn-primary" Text="Save" OnClick="Button6_Click" />
                                                            <asp:Button ID="Button5" OnClick="Button5_Click" runat="server" class="btn btn-primary" Visible="false" Text="Update" />

                                                        </td>
                                                        <tr>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>








                                        <div class="col-sm-9">
                                       <%--     <div class="row">
                                                <a data-target="#loanbookModal1" role="button" class="" data-toggle="modal" id="LOANCALCMOD">CALCULATOR</a>



                                            </div>--%>


                                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <contenttemplate>
            <asp:Timer ID="Timer1"  Interval="3000" OnTick="Timer1_Tick"    runat="server"></asp:Timer>
             <h4 class="card-title">Current Holdings Across Allocated Asset Managers For Sub-Account <% =rdSubAccount.SelectedItem.Text.ToString() + " Under " + rdAssetMangers.SelectedItem.Text.ToString() %> </h4>
            <asp:GridView ID="grdApps" runat="server"  OnDataBound="grdApps_DataBound"  CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No Investments!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left"   OnRowDataBound="grdApps_RowDataBound"  ShowFooter="false" AutoGenerateColumns="true">
            <AlternatingRowStyle BackColor="White" CssClass="altrowstyle" />
            <EditRowStyle BackColor="#2461BF" />
            <EmptyDataRowStyle CssClass="text-warning text-center" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" CssClass="header info" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" CssClass="rowstyle" />
            </asp:GridView>
         </contenttemplate>
                                            </asp:UpdatePanel>
                                            <div class="center">
                                                <asp:Button ID="Button1" class="btn btn-primary" OnClick="Button1_Click1" runat="server" Text="Export To Excel" />
                                                <asp:Button ID="Button4"  class="btn btn-primary" OnClick="Button1_Click22" runat="server" Text="View Detailed Report" />

                                            </div>

                                        </div>


                                    </div>
            </asp:Panel>
        </div>
    </div>
    </div>
                        </div>
                                                </div>
                                                
                                          


                                        
                                       
                                    
                                            
                                        
                                        <br />

    <asp:Panel ID="Panel3" Visible="false" runat="server">
        <h4 class="card-title">linked Asset Classes</h4>
        <div class="form-group row">
            <asp:Label ID="Label2" runat="server" Text="Asset Classes" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
            <div class="col-sm-4">
                <%--<asp:TextBox ID="txtRole"  class="form-control" runat="server"></asp:TextBox>--%>
                <asp:DropDownList ID="cmbAssetClass" runat="server" class="select2 form-control custom-select" Style="width: 100%; height: 36px;"></asp:DropDownList>
            </div>
            <asp:Label ID="Label10" runat="server" Text="Asset Managers" class="col-sm-2 text-right control-label col-form-label"></asp:Label>


        </div>
        <div class="form-group row">
            <asp:Label ID="Label12" runat="server" Text="Counter" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
            <div class="col-sm-4">
                <%--<asp:TextBox ID="txtRole"  class="form-control" runat="server"></asp:TextBox>--%>
                <asp:DropDownList ID="cmbCounter" runat="server" class="select2 form-control custom-select" Style="width: 100%; height: 36px;"></asp:DropDownList>
            </div>
            <asp:Label ID="Label11" runat="server" Text="Quantity" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtQuantity" class="form-control" runat="server"></asp:TextBox>
                <asp:TextBox ID="TextBox2" Visible="false" class="form-control" runat="server"></asp:TextBox>
            </div>

        </div>
        <div class="form-group row">
            <asp:Label ID="Label13" runat="server" Text="Market Price" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
            <div class="col-sm-4">
                <%--<asp:TextBox ID="txtRole"  class="form-control" runat="server"></asp:TextBox>--%>
                <asp:TextBox ID="txtPrice" runat="server" class="form-control" Style="width: 100%; height: 36px;"></asp:TextBox>
            </div>
        </div>






        <div class="center">




            <asp:Button ID="Button3" runat="server" class="btn btn-primary" Text="Save" OnClick="Button3_Click1" />
            <asp:Button ID="Button5" runat="server" class="btn btn-primary" Visible="false" Text="Update" />


        </div>
    </asp:Panel>
    <div id="loanbookModal1" class="modal fade" role="dialog">
      <%--  <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">

                    <h4 class="modal-title">CALCULATOR</h4>
                    <button class="btn btn-primary btn-lg" data-toggle="modal" data-target="#myModal">
  Calculator in Modal
</button>--%>

<%--<div id="myModal" class="modal fade">--%>
   <%-- <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Modal title</h4>
            </div>
            <div class="modal-body">
             <div class="container">
    <div class="row">
        <div class="col-sm-8 col-sm-offset-2">
            <div class="calculator">
                <input id="inputcalculation" type="text" class="calculator-input form-control" />
                <div class="calculator-controls">
                     <div class="row">
                        <div class="col-xs-3"><a href="#" class="btn btn-sm">7</a></div>
                        <div class="col-xs-3"><a href="#" class="btn btn-sm">8</a></div>
                        <div class="col-xs-3"><a href="#" class="btn btn-sm">9</a></div>
                        <div class="col-xs-3"><a href="#" class="btn btn-sm operator">+</a></div>
                    </div>
                     <div class="row">
                        <div class="col-xs-3"><a href="#" class="btn btn-sm">4</a></div>
                        <div class="col-xs-3"><a href="#" class="btn btn-sm">5</a></div>
                        <div class="col-xs-3"><a href="#" class="btn btn-sm">6</a></div>
                        <div class="col-xs-3"><a href="#" class="btn btn-sm operator">-</a></div>
                    </div>
                     <div class="row">
                        <div class="col-xs-3"><a href="#" class="btn btn-sm">1</a></div>
                        <div class="col-xs-3"><a href="#" class="btn btn-sm">2</a></div>
                        <div class="col-xs-3"><a href="#" class="btn btn-sm">3</a></div>
                        <div class="col-xs-3"><a href="#" class="btn btn-sm operator">*</a></div>
                    </div>
                    <div class="row">
                        <div class="col-xs-3"><a href="#" class="btn btn-sm">0</a></div>
                        <div class="col-xs-3"><a href="#" class="btn btn-sm">.</a></div>
                        <div class="col-xs-3"><a href="#" class="btn btn-sm calculate">=</a></div>
                        <div class="col-xs-3"><a href="#" class="btn btn-sm operator">/</a></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                <button id="btncalculate" type="button" class="btn btn-primary">Save changes</button>
            </div>
        </div>
        <!-- /.modal-content -->
    </div>
    <!-- /.modal-dialog -->
</div>--%>
<!-- /.modal -->
                    <%--<button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body panel-body small">

                    <div class="form-group row">
                        <asp:Label ID="Label6" runat="server" Text="VAL 1" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtVal1"  class="form-control" runat="server"></asp:TextBox>
                        </div>
                        <asp:Label ID="Label8" runat="server" Text="VAL 2" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                        <div class="col-sm-4">


                            <asp:TextBox ID="txtVal2" class="form-control" runat="server"></asp:TextBox>

                        </div>



                    </div>

                    <div class="form-group row">
                        <asp:Label ID="Label18" runat="server" Text="  " class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                        <asp:Button ID="ButtonAdd" runat="server" OnClick="ButtonAdd_Click" Text="+" Width="36px" />
                        <asp:Button ID="ButtonSub" runat="server" OnClick="ButtonSub_Click" Text="-" Width="36px" />
                        <asp:Button ID="ButtonMul" runat="server" OnClick="ButtonMul_Click" Text="*" Width="36px" />
                        <asp:Button ID="ButtonDiv" runat="server" OnClick="ButtonDiv_Click" Text="/" Width="36px" />--%>

                        <%--<div class="col-sm-3">
                                    <asp:TextBox ID="TextBox4"  class="form-control" runat="server"></asp:TextBox>
                                </div>
                                 <div class="col-sm-3">
                                    <asp:TextBox ID="TextBox6"  class="form-control" runat="server"></asp:TextBox>
                                </div>
                                 <div class="col-sm-3">
                                    <asp:TextBox ID="TextBox7"  class="form-control" runat="server"></asp:TextBox>
                                </div>
                                 <div class="col-sm-3">
                                    <asp:TextBox ID="TextBox8"  class="form-control" runat="server"></asp:TextBox>
                                </div>--%>
                    </div>
                   <%-- <div class="form-group row">
                        <asp:Label ID="Label17" runat="server" Text="Total" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtAnswer" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>--%>
           

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

