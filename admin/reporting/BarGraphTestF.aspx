<%@ Page Language="C#" AutoEventWireup="false" CodeFile="BarGraphTestF.aspx.cs" Inherits="index_Farai" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Intellego Bar Graph</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />
<%--    <link href="../assetsDashboard/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assetsDashboard/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css" />
    <link href="../assetsDashboard/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL STYLES -->
    <link href="../assetsDashboard/global/css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="../assetsDashboard/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME GLOBAL STYLES -->
    <!-- BEGIN THEME LAYOUT STYLES -->
    <link href="../assetsDashboard/layouts/layout/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="../assetsDashboard/layouts/layout/css/themes/light2.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../assetsDashboard/layouts/layout/css/custom.min.css" rel="stylesheet"/>--%>
    <!-- END THEME LAYOUT STYLES -->
        <!-- BEGIN GLOBAL MANDATORY STYLES -->

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


<%--    <!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">

<!-- Optional theme -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous">

<!-- Latest compiled and minified JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>--%>


<%--        <script type="text/javascript"> 

     

        var Tawk_API=Tawk_API||{}, Tawk_LoadStart=new Date();

        (function(){
        var s1=document.createElement("script"),s0=document.getElementsByTagName("script")[0];
        s1.async=true;
        s1.src='https://embed.tawk.to/5aba26f8d7591465c708f353/default';
        s1.charset='UTF-8';
        s1.setAttribute('crossorigin','*');
        s0.parentNode.insertBefore(s1,s0);
        })();
        </script>--%>
        <!--End of Tawk.to Script--> 
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
   /*background-color: transparent;
   border-radius: 50%;
   box-shadow: 0 6px 10px 0 #666;
   transition: all 0.1s ease-in-out;*/
 
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

#chartdiv {
  width: 100%;
  height: 500px;
}
</style>
</head>
<body class="page-header-fixed page-sidebar-closed-hide-logo page-container-bg-solid page-content-white" style="background:#ededed !important;">
    <form runat="server" id="form1">
        <%--<input type="hidden" value="<% =Session("Userrole") %>" id="user_role"/>
        <input type="hidden" value="<% =Session("Username") %>" id="user_id"/>--%>
        <div class="stayStill">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <asp:Timer ID="Timer1" runat="server" Enabled="false" Interval="120000">
        </asp:Timer>
        <!-- BEGIN HEADER -->
        <div class="page-header navbar navbar-fixed-top" style="background-color: #FFFFFF !important;
    height: 7.5vh !important;">
            <!-- BEGIN HEADER INNER -->
            <div class="page-header-inner " style="background-color:#FFFFFF !important;">
                <!-- BEGIN LOGO -->
                <div class="page-logo">
                    <a href="#">
                        <%--<img src="../img/crdbn.png" style="height:7.5vh !important;margin-left:10%;" alt="logo" class="logo-default" />--%>
                    </a>
                    <!--<div class="menu-toggler sidebar-toggler"> </div>-->
                </div>
                <!-- END LOGO -->
               
                <!-- BEGIN RESPONSIVE MENU TOGGLER -->
                <a href="javascript:;" class="menu-toggler responsive-toggler" data-toggle="collapse" data-target=".navbar-collapse"></a>
                <!-- END RESPONSIVE MENU TOGGLER -->
                <!-- BEGIN TOP NAVIGATION MENU -->
                <%--<div class="top-menu">
                    <ul class="nav navbar-nav pull-right">
                        <li class="dropdown dropdown-user">
                            <a href="javascript:;" class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">
                                <span class="glyphicon glyphicon-user"></span>
                                <span style="color:white;" class="username username-hide-on-mobile">
                                    <asp:Label ID="lblSess" runat="server" Text=""></asp:Label></span>
                                <i class="fa fa-angle-down"></i>
                            </a>
                            <ul class="dropdown-menu dropdown-menu-default">
                                <li>
                                    <a href="../Back.aspx">
                                        <i class="icon-user"></i>Main Page
                                    </a>
                                </li>
                                <%--<li class="divider"></li>--%>
                                <%--<li>
                                    <a href="../Login.aspx">
                                        <i class="icon-key"></i>Log Out
                                    </a>
                                </li>
                            </ul>
                        </li>
                        <!-- END USER LOGIN DROPDOWN -->
                    </ul>
                </div>--%>
                <!-- END TOP NAVIGATION MENU -->
            </div>
            <!-- END HEADER INNER -->
        </div>
        <div class="page-container">
            <!-- BEGIN CONTENT -->
            <div class="page-content-wrapper row">
                <!-- BEGIN CONTENT BODY -->
                <div class="page-content">
                    <div class="row" id="divRegStats" runat="server">
                        <div class="col-md-6 col-sm-3" style="height:140px;">
                            <div class="portlet light " >
                                <div class="portlet-body">
                                     <div id="popupLV">
                                         <div id="chartdiv" style="width:100%" class="CSSAnimationChart"></div>
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6 col-sm-3" style="height:140px;">
                            <div class="portlet light " >
                                <div class="portlet-body">
                                     <div id="popupLW">
                                         
                                         <div id="chartdiv1" style="width:100%" class="CSSAnimationChart"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>   
                    <%--<div class="row" id="div1" runat="server">                       
                        <div class="col-md-6 col-sm-3" style="height:140px;">
                            <div class="portlet light " >
                                <div class="portlet-body">
                                     <div id="popupLX">
                                         
                                         <div id="chartdiv2" style="width:100%" class="CSSAnimationChart"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div> --%>  
                </div>
                <!-- END CONTENT BODY -->
            </div>
            <!-- END CONTENT -->
        </div>
        <!-- END CONTAINER -->
       
        <hr/>
        <!-- BEGIN FOOTER -->
        <div class="page-footer" style="background-color:#FFFFFF !important;height:7vh !important">
            <div class="page-footer-inner">
                 <%--<a class="pageFootertxt" href="#" target="new"> &copy;2021 Escrow Systems.</a>--%>
                <%--<a href="http://themeforest.net/item/metronic-responsive-admin-dashboard-template/4021469?ref=keenthemes" title="Purchase Metronic just for 27$ and get lifetime updates for free" target="_blank">Purchase Metronic!</a>--%>
            </div>
            <div class="scroll-to-top">
                <i class="icon-arrow-up"></i>
            </div>
        </div>

        <!--[if lt IE 9]>
    <![endif]-->
<%--        <!-- BEGIN CORE PLUGINS -->
        <script src="../assetsDashboard/global/plugins/jquery.min.js" type="text/javascript"></script>
        <script src="../assetsDashboard/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
        <script src="../assetsDashboard/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
        <!-- END CORE PLUGINS -->
        <!-- BEGIN PAGE LEVEL PLUGINS -->
        <script src="../assetsDashboard/global/plugins/morris/raphael-min.js" type="text/javascript"></script>
        <script src="../assetsDashboard/global/plugins/morris/morris.min.js" type="text/javascript"></script>
        <script src="../assetsDashboard/global/plugins/counterup/jquery.waypoints.min.js" type="text/javascript"></script>
        <script src="../assetsDashboard/global/plugins/counterup/jquery.counterup.min.js" type="text/javascript"></script>
        <script src="../assetsDashboard/global/plugins/amcharts/amcharts/amcharts.js" type="text/javascript"></script>
        <script src="../assetsDashboard/global/plugins/amcharts/amcharts/serial.js" type="text/javascript"></script>
        <script src="../assetsDashboard/global/plugins/amcharts/amcharts/pie.js" type="text/javascript"></script>
        <script src="../assetsDashboard/global/plugins/amcharts/amcharts/radar.js" type="text/javascript"></script>
        <script src="../assetsDashboard/global/plugins/amcharts/amcharts/themes/light.js" type="text/javascript"></script>
        <script src="../assetsDashboard/global/plugins/amcharts/amcharts/themes/patterns.js" type="text/javascript"></script>
        <script src="../assetsDashboard/global/plugins/amcharts/amcharts/themes/chalk.js" type="text/javascript"></script>
        <script src="../assetsDashboard/global/plugins/amcharts/amstockcharts/amstock.js" type="text/javascript"></script>
        
        <script src="../assetsDashboard/global/scripts/core.js" type="text/javascript"></script>
        <script src="../assetsDashboard/global/scripts/charts.js" type="text/javascript"></script>
        <script src="../assetsDashboard/global/scripts/animated.js" type="text/javascript"></script>
        <!-- END PAGE LEVEL PLUGINS -->
        <!-- BEGIN THEME GLOBAL SCRIPTS -->
        <script src="../assetsDashboard/global/scripts/app.min.js" type="text/javascript"></script>
        <!-- END THEME GLOBAL SCRIPTS -->
        <!-- BEGIN PAGE LEVEL SCRIPTS -->
        <script src="../assetsDashboard/pages/scripts/dashboard.min.js" type="text/javascript"></script>
        <!-- END PAGE LEVEL SCRIPTS -->
        <!-- BEGIN THEME LAYOUT SCRIPTS -->
        <script src="../assetsDashboard/layouts/layout/scripts/layout.min.js" type="text/javascript"></script>
        <!-- END THEME LAYOUT SCRIPTS -->--%>

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
            function getVoresults() {
                //var dataString = { 'value': value };
                $.ajax({
                    url: "BarGraphDashboard.asmx/getChatindodata",
                    data: {},
                    //data: dataString,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (chartData) {
                        console.log(JSON.parse(chartData.d));
                        alert(JSON.parse(chartData.d));
                        var chart = AmCharts.makeChart("chartdiv", {
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
                            "categoryField": "Asset_class_id",
                            "rotate": true,
                            "startDuration": 1,
                            "categoryAxis": {
                                "gridPosition": "start",
                                "ignoreAxisWidth": false,
                                "autoWrap": true,
                                "title": "Asset Class",
                                "position": "left"
                            },
                            "trendLines": [],
                            "graphs": [                                                              

                                {
                                    "balloonText": "OMIG:[[value]]",
                                    "fillAlphas": 0.8,
                                    "id": "AmGraph-1",
                                    "lineAlpha": 0.2,
                                    "title": "OMIG",
                                    "type": "column",
                                    //"fillColorsField": "SForColor",
                                    "valueField": "OMIG",
                                    "labelText": ""
                                },
                                {
                                    "balloonText": "Platinum Asset Manager:[[value]]",
                                    "fillAlphas": 0.8,
                                    "id": "AmGraph-2",
                                    "lineAlpha": 0.2,
                                    "title": "Platinum_Asset_Manager",
                                    "type": "column",
                                    //"fillColorsField": "SAgainstColor",
                                    "valueField": "Platinum Asset Managers",
                                    "labelText": ""
                                },
                                {
                                    "balloonText": "Old Mutual Properties:[[value]]",
                                    "fillAlphas": 0.8,
                                    "id": "AmGraph-3",
                                    "lineAlpha": 0.2,
                                    "title": "OldMutual_Properties",
                                    "type": "column",
                                    //"fillColorsField": "SAbstainColor",
                                    "valueField": "Old Mutual Properties",
                                    "labelText": ""
                                },
                                {
                                    "balloonText": "Total :[[value]]",
                                    "fillAlphas": 0.8,
                                    "id": "AmGraph-3",
                                    "lineAlpha": 0.2,
                                    "title": "Total",
                                    "type": "column",
                                    //"fillColorsField": "SAbstainColor",
                                    "valueField": "Total",
                                    "labelText": ""
                                }
                                //,
                                //{
                                //    "balloonText": "Imara :[[value]]",
                                //    "fillAlphas": 0.8,
                                //    "id": "AmGraph-3",
                                //    "lineAlpha": 0.2,
                                //    "title": "Imara",
                                //    "type": "column",
                                //    //"fillColorsField": "SAbstainColor",
                                //    "valueField": "Imara",
                                //    "labelText": ""
                                //}
                            ],
                            "guides": [],
                            "dataProvider": JSON.parse(chartData.d),
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
                });
            }
            //---------

            function getVoresultsTryF() {
                //var dataString = { 'value': value };
                $.ajax({
                    url: "BarGraphDashboard.asmx/getChatindodata",
                    data: {},
                    //data: dataString,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (chartData) {
                        console.log(JSON.parse(chartData.d));
                        var chart = AmCharts.makeChart("chartdiv", {
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
                            "categoryField": "Asset_class_id",
                            "rotate": true,
                            "startDuration": 1,
                            "categoryAxis": {
                                "gridPosition": "start",
                                "ignoreAxisWidth": false,
                                "autoWrap": true,
                                "title": "Asset Class",
                                "position": "left"
                            },
                            "trendLines": [],
                            "graphs": [

                                {
                                    "balloonText": "OMIG:[[value]]",
                                    "fillAlphas": 0.8,
                                    "id": "AmGraph-1",
                                    "lineAlpha": 0.2,
                                    "title": "OMIG",
                                    "type": "column",
                                    //"fillColorsField": "SForColor",
                                    "valueField": "OMIG",
                                    "labelText": ""
                                },
                                {
                                    "balloonText": "Platinum Asset Manager:[[value]]",
                                    "fillAlphas": 0.8,
                                    "id": "AmGraph-2",
                                    "lineAlpha": 0.2,
                                    "title": "Platinum_Asset_Manager",
                                    "type": "column",
                                    //"fillColorsField": "SAgainstColor",
                                    "valueField": "Platinum Asset Managers",
                                    "labelText": ""
                                },
                                {
                                    "balloonText": "Old Mutual Properties:[[value]]",
                                    "fillAlphas": 0.8,
                                    "id": "AmGraph-3",
                                    "lineAlpha": 0.2,
                                    "title": "OldMutual_Properties",
                                    "type": "column",
                                    //"fillColorsField": "SAbstainColor",
                                    "valueField": "Old Mutual Properties",
                                    "labelText": ""
                                },
                                {
                                    "balloonText": "Total :[[value]]",
                                    "fillAlphas": 0.8,
                                    "id": "AmGraph-3",
                                    "lineAlpha": 0.2,
                                    "title": "Total",
                                    "type": "column",
                                    //"fillColorsField": "SAbstainColor",
                                    "valueField": "Total",
                                    "labelText": ""
                                }
                                //,
                                //{
                                //    "balloonText": "Imara :[[value]]",
                                //    "fillAlphas": 0.8,
                                //    "id": "AmGraph-3",
                                //    "lineAlpha": 0.2,
                                //    "title": "Imara",
                                //    "type": "column",
                                //    //"fillColorsField": "SAbstainColor",
                                //    "valueField": "Imara",
                                //    "labelText": ""
                                //}
                            ],
                            "guides": [],
                            "dataProvider": JSON.parse(chartData.d),
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
                });
            }

            //--------

            //F.CHatz
            GetAPIData().then(function (response) {
                if (response.ok) {
                    return response.json();
                }
                throw new Error("Response not OK");
            }).then(function (data) {
                var graphMap = {};
                var graphs = [];
                var dataMap = {};
                var dataProvider = [];

                //go through the data and create graphs based on the different sources
                //and fill in the dataMap object to remap/convert later
                data.forEach(function (dataItem) {
                    //create a new graph if we did not already create one
                    if (graphMap[dataItem.source] === undefined) {
                        graphMap[dataItem.source] = 1;
                        graphs.push({
                            valueField: dataItem.source,
                            title: dataItem.source,
                            type: 'column',
                            fillAlphas: 1,
                            balloonText: '<b>' + dataItem.source + '</b>: [[value]]'
                        });
                    }
                    //create a new object for the month if not already created
                    if (dataMap[dataItem.month] === undefined) {
                        dataMap[dataItem.month] = { month: dataItem.month };
                    }
                    //add new source information and set target information for that month.
                    dataMap[dataItem.month][dataItem.source] = dataItem.turnover;
                    dataMap[dataItem.month].target = dataItem.target;
                });
                //add the target line
                graphs.push({
                    valueField: 'target',
                    title: 'Target',
                    balloonText: '<b>Target</b>: [[value]]'
                });

                //convert dataMap to an array sorted by date
                Object.keys(dataMap).sort(function (lhs, rhs) {
                    return new Date(lhs) - new Date(rhs);
                }).forEach(function (month) {
                    dataProvider.push(dataMap[month]);
                });

                //create the chart using the data/graphs created
                AmCharts.makeChart('chartdiv2', {
                    // ... other properties omitted ...
                    dataProvider: dataProvider,
                    graphs: graphs,
                    // ... other properties omitted ...
                });
            }).catch(function (error) {
                alert(error.message);
            });
            //-------------------

        </script>
        <script type="text/javascript">
            $(document).ready(function () {
                getVoresults();
            });
            
            $(document).ready(function () {
                GetAPIData();
            });

            // 100 % STACKED
            am4core.ready(function () {

                // Themes begin
                am4core.useTheme(am4themes_animated);
                // Themes end

                var chart = am4core.create("chartdiv1", am4charts.XYChart);
                chart.hiddenState.properties.opacity = 0; // this creates initial fade-in

                chart.data = [
                    {
                        category: "One1",
                        value1: 1,
                        value2: 6,
                        value3: 3
                   
                    },
                    {
                        category: "Two",
                        value1: 2,
                        value2: 5,
                        value3: 3
                    },
                    {
                        category: "Three",
                        value1: 3,
                        value2: 5,
                        value3: 4
                    },
                    {
                        category: "Four",
                        value1: 4,
                        value2: 5,
                        value3: 6
                    },
                    {
                        category: "Five",
                        value1: 3,
                        value2: 5,
                        value3: 4
                    },
                    {
                        category: "Six",
                        value1: 2,
                        value2: 13,
                        value3: 1
                    }
                ];

                chart.colors.step = 2;
                chart.padding(30, 30, 10, 30);
                chart.legend = new am4charts.Legend();

                var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
                categoryAxis.dataFields.category = "category";
                categoryAxis.renderer.grid.template.location = 0;

                var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
                valueAxis.min = 0;
                valueAxis.max = 100;
                valueAxis.strictMinMax = true;
                valueAxis.calculateTotals = true;
                valueAxis.renderer.minWidth = 50;


                var series1 = chart.series.push(new am4charts.ColumnSeries());
                series1.columns.template.width = am4core.percent(80);
                series1.columns.template.tooltipText =
                    "{name}: {valueY.totalPercent.formatNumber('#.00')}%";
                series1.name = "Series 1";
                series1.dataFields.categoryX = "category";
                series1.dataFields.valueY = "value1";
                series1.dataFields.valueYShow = "totalPercent";
                series1.dataItems.template.locations.categoryX = 0.5;
                series1.stacked = true;
                series1.tooltip.pointerOrientation = "vertical";

                var bullet1 = series1.bullets.push(new am4charts.LabelBullet());
                bullet1.interactionsEnabled = false;
                bullet1.label.text = "{valueY.totalPercent.formatNumber('#.00')}%";
                bullet1.label.fill = am4core.color("#ffffff");
                bullet1.locationY = 0.5;

                var series2 = chart.series.push(new am4charts.ColumnSeries());
                series2.columns.template.width = am4core.percent(80);
                series2.columns.template.tooltipText =
                    "{name}: {valueY.totalPercent.formatNumber('#.00')}%";
                series2.name = "Series 2";
                series2.dataFields.categoryX = "category";
                series2.dataFields.valueY = "value2";
                series2.dataFields.valueYShow = "totalPercent";
                series2.dataItems.template.locations.categoryX = 0.5;
                series2.stacked = true;
                series2.tooltip.pointerOrientation = "vertical";

                var bullet2 = series2.bullets.push(new am4charts.LabelBullet());
                bullet2.interactionsEnabled = false;
                bullet2.label.text = "{valueY.totalPercent.formatNumber('#.00')}%";
                bullet2.locationY = 0.5;
                bullet2.label.fill = am4core.color("#ffffff");

                var series3 = chart.series.push(new am4charts.ColumnSeries());
                series3.columns.template.width = am4core.percent(80);
                series3.columns.template.tooltipText =
                    "{name}: {valueY.totalPercent.formatNumber('#.00')}%";
                series3.name = "Series 3";
                series3.dataFields.categoryX = "category";
                series3.dataFields.valueY = "value3";
                series3.dataFields.valueYShow = "totalPercent";
                series3.dataItems.template.locations.categoryX = 0.5;
                series3.stacked = true;
                series3.tooltip.pointerOrientation = "vertical";

                var bullet3 = series3.bullets.push(new am4charts.LabelBullet());
                bullet3.interactionsEnabled = false;
                bullet3.label.text = "{valueY.totalPercent.formatNumber('#.00')}%";
                bullet3.locationY = 0.5;
                bullet3.label.fill = am4core.color("#ffffff");

                chart.scrollbarX = new am4core.Scrollbar();

            }); // end am4core.ready()
        </script>
            </div>
    </form>
</body>
</html>