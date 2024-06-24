<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Reporting/Reporting.master" AutoEventWireup="true" CodeFile="BarGraph.aspx.cs" Inherits="admin_parameters_User" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
    <style>
.center {
  text-align: center;
 
}
</style>

    
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="fonts/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL STYLES -->
    <link href="assets/global/css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME GLOBAL STYLES -->
    <!-- BEGIN THEME LAYOUT STYLES -->
    <link href="assets/layouts/layout/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/layouts/layout/css/themes/light2.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="assets/layouts/layout/css/custom.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME LAYOUT STYLES -->
    <link rel="shortcut icon" href="favicon.ico" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
     <div class="page-breadcrumb">
                <div class="row">
                    <div class="col-12 d-flex no-block align-items-center">
                        <h4 class="page-title"> Asset Class Holding Bar Charts</h4><br />
                        
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

                                     <%-- <div class="form-group row">
                                        <asp:Label ID="Label5" runat="server" Text="Previous Quarter" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtPrevQuarter" enabled="false"  class="form-control" runat="server" disabled="true" ></asp:TextBox>
                                            </div>
                                        <asp:Label ID="Label6" runat="server" Text="Previous Year" class="col-sm-2 text-right control-label col-form-label"></asp:Label>
                                        <div class="col-sm-4">                                            
                                           <asp:TextBox ID="txtPrevQuarterYear" enabled="false"  class="form-control" runat="server" disabled="true" ></asp:TextBox>
                                            </div>
                                      </div> --%>


                                     <div class="form-group row">
                                        <asp:TextBox ID="txtID"  class="form-control" runat="server" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtselectedAssetManager"  class="form-control" runat="server" Visible="false"></asp:TextBox>
                                             <asp:TextBox id="operationid" visible="false" runat="server"></asp:TextBox>
                                        
                                        </div>         
                                 
                                   
            <div class="center">
                    <asp:Button ID="Button6" runat="server" class="btn btn-primary" Text="Back" OnClick="Button3_Click" />
            <%--<asp:Button ID="Button2" runat="server"  visble="true" class="btn btn-primary" Text="View" OnClick="Button1_Click"   />--%>
                                          
                                
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
                        <asp:Label ID="Label2" runat="server" Visible="true" Text=""> Quarter <% =txtQuarter.Text.ToString() + " Of Year " + txtYear.Text.ToString() %></asp:Label>   
                        </div>

                      <%--AM Chatz start--%>

                       <div class="col-md-6 col-sm-6">
                            <div class="portlet light ">
                                <div class="portlet-title">
                                    <div class="caption font-red">
                                        <span class="caption-subject bold uppercase">Q2Q</span>
                                        <span class="caption-helper">Exposure Analysis </span>
                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <%--<iframe name="myIframe" id="myIframe" runat="server">--%>
                                        <div id="dataChart" class="CSSAnimationChart"></div>
                                    <%--</iframe>--%>
                                    
                                </div>
                            </div>
                        </div>

                       <%--AM Chatz end--%>


                        <%--<asp:Chart ID="Chart1" Visible="false" runat="server" BackColor="0, 0, 64" BackGradientStyle="LeftRight"
        BorderlineWidth="0" Height="600px" Palette="None" PaletteCustomColors="Maroon"
        Width="1000px" BorderlineColor="64, 0, 64">
                            <Titles>            
                                <asp:Title ShadowOffset="10" Name="Items" />
                            </Titles>
                            <Legends>
                                <%--<asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="True" Name="Default"
                                    LegendStyle="Row" Enabled="false" />--%><%--
                                <asp:Legend Alignment="Center" Docking="Left" IsTextAutoFit="True" Name="Default"
                                    LegendStyle="Column" Enabled="false" />
                            </Legends>
                            <Series>
                                <asp:Series Name="Default" />
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1"  BorderWidth="0"  />
                            </ChartAreas>
                        </asp:Chart>--%>
                      <%--<asp:Chart ID="Chart1" runat="server" Height="300px" Width="400px">
                        <Titles>
                            <asp:Title ShadowOffset="3" Name="Items" />
                        </Titles>
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                                LegendStyle="Row" />
                        </Legends>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
                        </ChartAreas>
                       </asp:Chart>--%>
                   </div>                 
            </div>
            <div class="center">
            <asp:Button ID="Button3" Visible="false"  class="btn btn-primary" OnClick="BtnPrintPie1_Click1" runat="server" Text="Export To Excel" />
            </div>
            <hr style="border:0px;height:5px;width:150px;" />
            <%--Pie for Previous QYear--%>
            <%--<div class="form-group row">
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
            </div>--%>
            <div class="center">
                <asp:Button ID="Button4" Visible="false"  class="btn btn-primary" OnClick="BtnPrintPie2_Click1" runat="server" Text="Export To Excel" />
            </div>


            <div class="center">
                <asp:Button ID="Button1" Visible="false"  class="btn btn-primary" OnClick="Button1_Click1" runat="server" Text="Export To Excel" />                
            </div>                                    
                              
                                       
                                       
        </asp:Panel>
                                        
                                        <br />

                                       
                                            
                                       
                                            </div>



                                       
                                        <%--</div>
                                        
                                            
                                        
                              
                                 </div>


                                        </div>
                                    
                            </div>
                          
                             

                                <%-- </form>--%>
      <%--</div>
                        </div>
                    </div>
              </asp:Panel>

          
          
          </div>
    --%>


         <!--[if lt IE 9]>
    <![endif]-->
        <!-- BEGIN CORE PLUGINS -->
        <script src="assets/global/plugins/jquery.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
        <!-- END CORE PLUGINS -->
        <!-- BEGIN PAGE LEVEL PLUGINS -->
        <script src="assets/global/plugins/morris/raphael-min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/morris/morris.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/counterup/jquery.waypoints.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/counterup/jquery.counterup.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/amcharts/amcharts/amcharts.js" type="text/javascript"></script>
        <script src="assets/global/plugins/amcharts/amcharts/serial.js" type="text/javascript"></script>
        <script src="assets/global/plugins/amcharts/amcharts/pie.js" type="text/javascript"></script>
        <script src="assets/global/plugins/amcharts/amcharts/radar.js" type="text/javascript"></script>
        <script src="assets/global/plugins/amcharts/amcharts/themes/light.js" type="text/javascript"></script>
        <script src="assets/global/plugins/amcharts/amcharts/themes/patterns.js" type="text/javascript"></script>
        <script src="assets/global/plugins/amcharts/amcharts/themes/chalk.js" type="text/javascript"></script>
        <script src="assets/global/plugins/amcharts/amstockcharts/amstock.js" type="text/javascript"></script>
        <script type="text/javascript" src="assets/global/plugins/jquery-idle-timeout/jquery.idletimeout.js"></script>
        <script type="text/javascript" src="assets/global/plugins/jquery-idle-timeout/jquery.idletimer.js"></script>
        <script type="text/javascript" src="assets/pages/scripts/ui-idletimeout.min.js"></script>
        <!-- END PAGE LEVEL PLUGINS -->
        <!-- BEGIN THEME GLOBAL SCRIPTS -->
        <script src="assets/global/scripts/app.min.js" type="text/javascript"></script>
        <!-- END THEME GLOBAL SCRIPTS -->
        <!-- BEGIN PAGE LEVEL SCRIPTS -->
        <script src="assets/pages/scripts/dashboard.min.js" type="text/javascript"></script>
        <!-- END PAGE LEVEL SCRIPTS -->
        <!-- BEGIN THEME LAYOUT SCRIPTS -->
        <script src="assets/layouts/layout/scripts/layout.min.js" type="text/javascript"></script>
        <!-- END THEME LAYOUT SCRIPTS -->
        <script type="text/javascript">
            $(document).ready(function () {
                $.ajax({
                    url: "BarGraphData.asmx/GetBarChartData",
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (chartData) {
                        //debugger;
                        var chart = AmCharts.makeChart("dataChart", {
                            "type": "serial",
                            "addClassNames": true,
                            "theme": "light",
                            "path": "assets/global/plugins/amcharts/ammap/images/",
                            "autoMargins": true,
                            //"marginLeft": 50,
                            //"marginRight": 8,
                            //"marginTop": 10,
                            //"marginBottom": 26,
                            "balloon": {
                                "adjustBorderColor": false,
                                "horizontalPadding": 10,
                                "verticalPadding": 8,
                                "color": "#ffffff"
                            },
                            "dataProvider": JSON.parse(chartData.d),
                            "valueAxes": [{
                                "axisAlpha": 0,
                                "position": "left",
                                //"title": "Amount [US$]"//,
                                "title": "Exposure"//,
                                //"minimum": 0,
                                //"maximum": 8000
                            }],
                            "startDuration": 1,
                            "graphs": [{
                                "alphaField": "alpha",
                                "balloonText": "<span style='font-size:12px;'>[[title]] for week ended [[category]]:<br><span style='font-size:20px;'>[[value]]</span> [[additional]]</span>",
                                "fillAlphas": 1,
                                "title": "Value of Total Exposures",
                                "type": "column",
                                //"valueField": "DisbAmt"
                                "valueField": "Total"
                            }, {
                                "id": "graph2",
                                "balloonText": "<span style='font-size:12px;'>[[title]] for week ended [[category]]:<br><span style='font-size:20px;'>[[value]]</span> [[additional]]</span>",
                                "bullet": "round",
                                "lineThickness": 3,
                                "bulletSize": 7,
                                "bulletBorderAlpha": 1,
                                "bulletColor": "#FFFFFF",
                                "useLineColorForBulletBorder": true,
                                "bulletBorderThickness": 3,
                                "fillAlphas": 0,
                                "lineAlpha": 1,
                                "title": "Value of OMIG",
                                "valueField": "OMIG"
                            }],
                            "categoryField": "Asset_class_id",
                            "categoryAxis": {
                                "gridPosition": "start",
                                "axisAlpha": 0,
                                "tickLength": 0,
                                "title": "Asset class",
                                "labelRotation": 45
                            },
                            "export": {
                                "enabled": true
                            }
                        });
                    },
                    error: function (xhr, status, error) {
                        alert(status);
                    }
                });
            });
            //$(document).ready(function () {
            //    $.ajax({
            //        url: "BarGraphData.asmx/GetBarGraphData",
            //        data: {},
            //        contentType: "application/json; charset=utf-8",
            //        dataType: "json",
            //        success: function (chartData) {
            //            //debugger;
            //            var chart = AmCharts.makeChart("countChart", {
            //                "type": "serial",
            //                "addClassNames": true,
            //                "theme": "light",
            //                "path": "assets/global/plugins/amcharts/ammap/images/",
            //                "autoMargins": true,
            //                //"marginLeft": 30,
            //                //"marginRight": 8,
            //                //"marginTop": 10,
            //                //"marginBottom": 26,
            //                "balloon": {
            //                    "adjustBorderColor": false,
            //                    "horizontalPadding": 10,
            //                    "verticalPadding": 8,
            //                    "color": "#ffffff"
            //                },
            //                "dataProvider": JSON.parse(chartData.d),
            //                "valueAxes": [{
            //                    "axisAlpha": 0,
            //                    "position": "left",
            //                    "title": "Number of transactions",
            //                    "gridCount": "1"
            //                    //"minimum": 0,
            //                    //"maximum": 100
            //                }],
            //                "startDuration": 1,
            //                "graphs": [{
            //                    "alphaField": "alpha",
            //                    "balloonText": "<span style='font-size:12px;'>[[title]] for week ended [[category]]:<br><span style='font-size:20px;'>[[value]]</span> [[additional]]</span>",
            //                    "fillAlphas": 1,
            //                    //"title": "Number of Disbursements",
            //                    //"type": "column",
            //                    //"valueField": "DisbCount"
            //                    "title": "Value of Total Exposures",
            //                    "type": "column",
            //                    "valueField": "Total"
            //                }, {
            //                    "id": "graph2",
            //                    "balloonText": "<span style='font-size:12px;'>[[title]] for week ended [[category]]:<br><span style='font-size:20px;'>[[value]]</span> [[additional]]</span>",
            //                    "bullet": "round",
            //                    "lineThickness": 3,
            //                    "bulletSize": 7,
            //                    "bulletBorderAlpha": 1,
            //                    "bulletColor": "#FFFFFF",
            //                    "useLineColorForBulletBorder": true,
            //                    "bulletBorderThickness": 3,
            //                    "fillAlphas": 0,
            //                    "lineAlpha": 1,
            //                    //"title": "Number of Repayments",
            //                    //"valueField": "RepayCount"
            //                    "title": "Value of OMIG",
            //                    "valueField": "OMIG"
            //                }],
            //                "categoryField": "WeekEnding",
            //                "categoryAxis": {
            //                    "gridPosition": "start",
            //                    "axisAlpha": 0,
            //                    "tickLength": 0,
            //                    "title": "Week ended",
            //                    "labelRotation": 45
            //                },
            //                "export": {
            //                    "enabled": true
            //                }
            //            });
            //        },
            //        error: function (xhr, status, error) {
            //            alert(status);
            //        }
            //    });
            //});
        </script>
  
</asp:Content>

