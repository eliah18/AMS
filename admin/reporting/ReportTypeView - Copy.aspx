<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Reporting/Reporting.master" AutoEventWireup="true" CodeFile="ReportTypeView.aspx.cs" Inherits="admin_parameters_User" %>

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
                          <h4 class="page-title center"> &nbsp;&nbsp;Reporting--Report Viewing</h4><br />
                       
                        
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
                                    <h4 class="card-title"> Reporting Category</h4>
                                    
                                    
                                      <div class="form-group row">
                                           <asp:Label ID="Label3" runat="server" Text="Choose Report View" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                              <asp:dropdownlist ID="rdReportingType"   class="form-control" runat="server">
                                                  <asp:ListItem Value="1"> Asset Class Holding</asp:ListItem>
                                                  <asp:ListItem Value="2"> Quarter to Quarter</asp:ListItem>
                                                   <asp:ListItem Value="3"> Portifolio Concentration</asp:ListItem>
                                                  <asp:ListItem Value="4"> PA OverWeight Structure</asp:ListItem>
                                                   <asp:ListItem Value="5">Money Market Counters</asp:ListItem>
                                                   <asp:ListItem Value="6">Exposure Graphs</asp:ListItem>
                                                   <asp:ListItem Value="7">Exposure Charts</asp:ListItem>
                                                   </asp:dropdownlist>
                                            </div>
                                          </div>
                                     <div class="center">
                                          
                                           </div>
                                            <div class="center">
                                                <asp:Button ID="Button6" runat="server" class="btn btn-primary" Text="Back" OnClick="Button3_Click" />
                                        <asp:Button ID="Button2" runat="server"  visble="true" class="btn btn-primary" Text="Next" OnClick="Button1_Click"   />
                                                  
                                          
                                 
                                
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

                                            
                                    
                                            
                                        </asp:Panel>
                            </div>
                        </div>
                                                </div>
                        </div>
                                                </div>
                                                
                                          


                                        
                                       
                                    
                                            
                                        
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
             

          
          
          </div>
    </div>
    


   <script type="text/javascript">
       var myLink = '<% =  nexturl %>';

  function newPage() {
    window.open(myLink);
  }


</script>
  
</asp:Content>

