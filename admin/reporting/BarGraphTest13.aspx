<%@ Page Language="C#" AutoEventWireup="false" CodeFile="BarGraphTest13.aspx.cs" Inherits="index_Farai" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Intellego Bar Graph</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />

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

    <link rel="shortcut icon" href="favicon.ico" />


    <script
      src="https://code.jquery.com/jquery-3.2.1.js"
      integrity="sha256-DZAnKJ/6XZ9si04Hgrsxu/8s717jcIzLy3oi35EouyE="
      crossorigin="anonymous">
        
      </script>
     <style type="text/css"> 
         .modal 
         {
             background: none !important;
             }
             
         .gridViewPager td
                {
	                padding-left: 4px;
	                padding-right: 4px;
	                padding-top: 1px;
	                padding-bottom: 2px;
                }
         .auto-style11 {
            background-image: linear-gradient(to bottom, #2d8cff, #2d8cff);
            color: white;
            font-weight:bold;
            font-family:Arimo, Helvetica, Arial, sans-serif;
            text-shadow:none;
            border: thin solid #2d8cff;
            -webkit-transition-duration: 0.4s; /* Safari */
            transition-duration: 0.4s;
            cursor:pointer;
        }
          .blink {
              animation: blinker 12.0s linear infinite;
              /*color: #1c87c9;
              font-size: 30px;
              font-weight: bold;
              font-family: sans-serif;*/
              }
              @keyframes blinker {  
              50% { opacity: 0; }
              }
     </style><%--
    <script lang="javascript" type="text/javascript">30mins
    setTimeout(function () { alert("Session has Timed, kindly login again"); window.location = '<%= LoginURL %>'; }, 2160000 * 180);
   
    </script>--%>
    <style>
    #chartdivGauge {
      width: 40%;
      height: 28vh;
    }
    button.closex {
        background: #d73e4d;
        background: rgba(215, 62, 77, 0.75);
        border: 0 none !important;
        color: #fff7cc;
        display: inline-block;
        float: right;
        font-size: 34px;
        height: 40px;
        line-height: 1;
        margin: 0px 1px;
        opacity: 1;
        text-align: center;
        text-shadow: none;
        -webkit-transition: background 0.2s ease-in-out;
        transition: background 0.2s ease-in-out;
        vertical-align: top;
        width: 46px;
    }
    .modal {
            background: #000;
        }
    .modal.livefeeds {
            background: rgba(255, 255, 255, 0.00) !important;
        }
    
.fab {
   width: 50px;
   height: 50px;
 
   font-size: 50px;
   color: red;
   text-align: center;
   line-height: 70px;
 
   position: fixed;
   right: 50px;
   bottom: 50px;
   margin-bottom: -20px;
   margin-right:60px;
   
}
 
.fab:hover {
   /*box-shadow: 0 6px 14px 0 #666;*/
   transform: scale(1.05);
  
}

#chartdivX {
  width: 100%;
  height: 500px;
}

#chartdiv13 {
  width: 100%;
  height: 500px;
}

</style>
</head>
<body >
    <form runat="server" id="form1">
        <div class="page-container">
            <div class="page-content-wrapper row">
                <div class="page-content">
                    
                    <div class="row" id="divRegStats" runat="server">
                        <div style="height:100%; height:100%">
                            <div class="portlet light " >
                                <div class="portlet-body">                                                                             
                                   <div id="chartdiv14" style="width:100%; height:100%"  class="CSSAnimationChart"></div>   
                                </div>
                            </div>
                        </div>
                    </div>   
                    
                </div>
            </div>
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
       <%-- <script type="text/javascript" src="assets/global/plugins/jquery-idle-timeout/jquery.idletimeout.js"></script>
        <script type="text/javascript" src="assets/global/plugins/jquery-idle-timeout/jquery.idletimer.js"></script>
        <script type="text/javascript" src="assets/pages/scripts/ui-idletimeout.min.js"></script>--%>
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

                const queryString = window.location.search;
                const urlParams = new URLSearchParams(queryString);
                var clientid = urlParams.get('clientid');
                var quarter = urlParams.get('quarter');
                var year = urlParams.get('year');

               var params = {'clientid':clientid,'quarter':quarter,'year':year}
                console.log(params)
                $.ajax({
                    type: "GET",
                    url: "BarGraphDashboard.asmx/getChatindodata",
                    data: params,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (chartData) {

                        let mydata = [];
                        mydata = JSON.parse(chartData.d);
                        mydata.forEach(function (data) {
                            let ObjectArray = []
                            var Asset_managerX
                            var Asset_class_idX
                            var ExposurePerManX
                            var TotalX

                            ObjectArray = Object.values(data)
                            Asset_managerX = 'Total'
                            Asset_class_idX = ObjectArray[1]
                            ExposurePerManX = ObjectArray[3]
                            TotalX = 0

                        mydata.push({ Asset_manager: Asset_managerX, Asset_class_id: Asset_class_idX, ExposurePerMan: ExposurePerManX, Total: TotalX });
                        
                        }); //console.log(mydata)
                        
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
                                    "axisAlpha": 0
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
           
    </form>
</body>
</html>