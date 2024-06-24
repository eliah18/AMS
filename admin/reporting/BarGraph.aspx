<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Reporting/Reporting.master" AutoEventWireup="true" CodeFile="BarGraph.aspx.cs" Inherits="admin_parameters_User" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
    <style>
.center {
  text-align: center;
 
}
</style>

    
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <%--<link href="fonts/css/font-awesome.min.css" rel="stylesheet" type="text/css" />--%>
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
                <%--<h4 class="page-title"> Asset Class Holding Bar Chart</h4><br />--%>
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item">Reporting>>Client Selection>>Reporting Dashboard>>Report Viewing>>Exposure Bar Chart</li>  
                    </ol>
                </nav>
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
                                     <div class="form-group row">
                                        <asp:TextBox ID="txtID"  class="form-control" runat="server" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtselectedAssetManager"  class="form-control" runat="server" Visible="false"></asp:TextBox>
                                             <asp:TextBox id="operationid" visible="false" runat="server"></asp:TextBox>  
                                     </div>         
                                 
                                   
                                    <div class="center">
                                            <asp:Button ID="Button6" runat="server" class="btn btn-primary" Text="Back" OnClick="Button3_Click" />
                                    <asp:Button ID="Button2" runat="server"  visble="true" class="btn btn-primary" Text="View" OnClick="Button1_Click"   />
                                    </div>
                                            <br />            
                                     <div class="form-group row">
                                        <div class="col-sm-12">                       
                                            <div class="portlet light ">
                                                <div class="portlet-body">
                                                    <%--<div id="popupLW">                                         
                                                            <div id="chartdiv14" style="width:100%" class="CSSAnimationChart"></div>
                                                    </div>--%>
                                                    <iframe name="myIframe" id="myIframe" width="1000" height="500" runat="server" visible="false">  
                                                    </iframe>                                    
                                                </div>
                                            </div>
                                        </div>                 
                                    </div>
                                    <%--<div class="center">
                                    <asp:Button ID="Button3" Visible="false"  class="btn btn-primary" OnClick="BtnPrintBarChart_Click1" runat="server" Text="Export To Excel" />
                                    </div>  --%>                       
                                </div>
                            </div>
                        </div>
                    </div>
        </asp:Panel>                                        
        <br />                            
                                       
      </div>

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
        <script src="https://cdn.amcharts.com/lib/4/core.js"></script>
        <script src="https://cdn.amcharts.com/lib/4/charts.js"></script>
        <script src="https://cdn.amcharts.com/lib/4/themes/animated.js"></script>
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
            function getResults13() {

                $.ajax({
                    url: "BarGraphDashboard.asmx/getChatindodata",
                    data: {},
                    //data: dataString,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (chartData) {

                        let mydata = [];
                        mydata = JSON.parse(chartData.d);
                        console.log(mydata);

                        mydata.forEach(function (data) {

                            let ObjectArray = []
                            var Asset_managerX
                            var Asset_class_idX
                            var ExposurePerManX
                            var TotalX

                            ObjectArray = Object.values(data)
                            //console.log(ObjectArray[0])

                            Asset_managerX = 'Total'
                            Asset_class_idX = ObjectArray[1]
                            ExposurePerManX = ObjectArray[3]
                            TotalX = 0

                            mydata.push({ Asset_manager: Asset_managerX, Asset_class_id: Asset_class_idX, ExposurePerMan: ExposurePerManX, Total: TotalX });

                        });
                        //console.log(mydata)

                        //DYNAMIC CHART FILLING
                        var graphMap = {};
                        var graphs = [];
                        var dataMap = {};
                        var dataProvider = [];

                        //go through the data and create graphs based on the different sources
                        //and fill in the dataMap object to remap/convert later
                        mydata.forEach(function (dataItem) {
                            //create a new graph if we did not already create one
                            if (graphMap[dataItem.Asset_class_id] === undefined) {
                                graphMap[dataItem.Asset_class_id] = 1;
                                //console.log(dataItem.Asset_class_id)
                                graphs.push({
                                    valueField: dataItem.Asset_class_id,
                                    title: dataItem.Asset_class_id,
                                    type: 'column',
                                    fillAlphas: 1,
                                    balloonText: '<b>' + dataItem.Asset_class_id + '</b>: [[value]]'
                                });
                            }

                            //create a new object for the asset manager if not already created
                            if (dataMap[dataItem.Asset_manager] === undefined) {
                                dataMap[dataItem.Asset_manager] = { Asset_manager: dataItem.Asset_manager };
                            }

                            //add new source information and set target information for that asset manager.
                            dataMap[dataItem.Asset_manager][dataItem.Asset_class_id] = dataItem.ExposurePerMan;
                            //dataMap[dataItem.Asset_manager].Total = dataItem.Total;
                        });

                        Object.keys(dataMap).forEach(function (Asset_manager) {
                            dataProvider.push(dataMap[Asset_manager]);
                        });

                        var chart = AmCharts.makeChart("chartdiv14", {
                            "type": "serial",
                            "theme": "light",
                            "legend": {
                                "maxColumns": 1,
                                "divId": "legenddiv",
                                "position": "right",
                                "useGraphSettings": true,
                                "clickable": false,
                                "focusable": false,
                                "enabled": true,
                                "markerSize": 10
                            },
                            "categoryField": "Asset_manager",
                            "rotate": false,
                            "startDuration": 1,
                            "categoryAxis": {
                                "gridPosition": "start",
                                "ignoreAxisWidth": false,
                                "autoWrap": true,
                                "title": "Asset Manager",
                                "position": "left"
                            },
                            "trendLines": [],
                            "graphs": graphs,
                            "guides": [],
                            "dataProvider": dataProvider,
                            "valueAxes": [
                                {
                                    "id": "ValueAxis-1",
                                    "position": "top",
                                    "title": "Value",
                                    "axisAlpha": 0,
                                    //"minimum": 0,
                                    //"maximum": 100,
                                    //"strictMinMax": "true"
                                }
                            ],
                            "allLabels": [],
                            "balloon": {},
                            "titles": [],
                            "export": {
                                "enabled": true
                            }

                        });

                    },

                    error: function (xhr, status, error) {
                        //alert(status);
                        console.log(status);
                    }

                })

            }
            
        </script>

    <script type="text/javascript">

        $(document).ready(function () {
            getResults13();
        });
    </script>
  
</asp:Content>

