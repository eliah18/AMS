<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BarGraphDataPage.aspx.cs" Inherits="index" %>

<%--<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">--%>
<head runat="server">
    <meta charset="utf-8" />
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="fontsG/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="assetsG/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assetsG/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css" />
    <link href="assetsG/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL STYLES -->
    <link href="assetsG/global/css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="assetsG/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME GLOBAL STYLES -->
    <!-- BEGIN THEME LAYOUT STYLES -->
    <link href="assetsG/layouts/layout/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="assetsG/layouts/layout/css/themes/light2.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="assetsG/layouts/layout/css/custom.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME LAYOUT STYLES -->
    <link rel="shortcut icon" href="favicon.ico" />

</head>
<body class="page-sidebar-closed-hide-logo page-container-bg-solid page-content-white">
    <form runat="server" id="form1">
        <div class="page-container">
            <!-- BEGIN CONTENT -->
            <div class="page-content-wrapper">
                <!-- BEGIN CONTENT BODY -->
                <div class="page-content">
                    <div class="row">
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
                        <%--<div class="col-md-6 col-sm-6">
                            <div class="portlet light ">
                                <div class="portlet-title">
                                    <div class="caption font-green">
                                        <span class="caption-subject bold uppercase">Asset Manager Perfomance</span>
                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <div id="countChart" class="CSSAnimationChart"></div>
                                </div>
                            </div>
                        </div>--%>
                    </div>
                </div>
                <!-- END CONTENT BODY -->
            </div>
            <!-- END CONTENT -->
        </div>
        <!-- END CONTAINER -->
        <!-- END FOOTER -->
        <%--<div class="modal fade" id="idle-timeout-dialog" data-backdrop="static">
            <div class="modal-dialog modal-small">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Your session is about to expire.</h4>
                    </div>
                    <div class="modal-body">
                        <p>
                            <i class="fa fa-warning text-danger"></i>You session will be logged out in

                            <span id="idle-timeout-counter"></span>&nbsp; seconds.
                        </p>
                        <p>Do you want to continue your session? </p>
                    </div>
                    <div class="modal-footer">
                        <button id="idle-timeout-dialog-logout" type="button" class="btn btn-warning btn-sm text-uppercase">No, Logout</button>
                        <button id="idle-timeout-dialog-keepalive" type="button" class="btn btn-success btn-sm text-uppercase" data-dismiss="modal">Yes, Keep Working</button>
                    </div>
                </div>
            </div>
        </div>--%>
        <!--[if lt IE 9]>
    <![endif]-->
        <!-- BEGIN CORE PLUGINS -->
        <script src="assetsG/global/plugins/jquery.min.js" type="text/javascript"></script>
        <script src="assetsG/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
        <script src="assetsG/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
        <!-- END CORE PLUGINS -->
        <!-- BEGIN PAGE LEVEL PLUGINS -->
        <script src="assetsG/global/plugins/morris/raphael-min.js" type="text/javascript"></script>
        <script src="assetsG/global/plugins/morris/morris.min.js" type="text/javascript"></script>
        <script src="assetsG/global/plugins/counterup/jquery.waypoints.min.js" type="text/javascript"></script>
        <script src="assetsG/global/plugins/counterup/jquery.counterup.min.js" type="text/javascript"></script>
        <script src="assetsG/global/plugins/amcharts/amcharts/amcharts.js" type="text/javascript"></script>
        <script src="assetsG/global/plugins/amcharts/amcharts/serial.js" type="text/javascript"></script>
        <script src="assetsG/global/plugins/amcharts/amcharts/pie.js" type="text/javascript"></script>
        <script src="assetsG/global/plugins/amcharts/amcharts/radar.js" type="text/javascript"></script>
        <script src="assetsG/global/plugins/amcharts/amcharts/themes/light.js" type="text/javascript"></script>
        <script src="assetsG/global/plugins/amcharts/amcharts/themes/patterns.js" type="text/javascript"></script>
        <script src="assetsG/global/plugins/amcharts/amcharts/themes/chalk.js" type="text/javascript"></script>
        <script src="assetsG/global/plugins/amcharts/amstockcharts/amstock.js" type="text/javascript"></script>
        <%--        <script type="text/javascript" src="assetsG/global/plugins/jquery-idle-timeout/jquery.idletimeout.js"></script>
        <script type="text/javascript" src="assetsG/global/plugins/jquery-idle-timeout/jquery.idletimer.js"></script>
        <script type="text/javascript" src="assetsG/pages/scripts/ui-idletimeout.min.js"></script>--%>
        <!-- END PAGE LEVEL PLUGINS -->
        <!-- BEGIN THEME GLOBAL SCRIPTS -->
        <script src="assetsG/global/scripts/app.min.js" type="text/javascript"></script>
        <!-- END THEME GLOBAL SCRIPTS -->
        <!-- BEGIN PAGE LEVEL SCRIPTS -->
        <script src="assetsG/pages/scripts/dashboard.min.js" type="text/javascript"></script>
        <!-- END PAGE LEVEL SCRIPTS -->
        <!-- BEGIN THEME LAYOUT SCRIPTS -->
        <script src="assetsG/layouts/layout/scripts/layout.min.js" type="text/javascript"></script>
        <!-- END THEME LAYOUT SCRIPTS -->
        <script type="text/javascript">
            //$(document).ready(function () {
            //    const queryString = window.location.search;
            //    console.log(queryString);
            //    const urlParams = new URLSearchParams(queryString);
            //    //const quarter1 = urlParams.get('quarter')
            //    //const clientid1 = urlParams.get('clientid')
            //    //const year1 = urlParams.get('year')
            //    //console.log(quarter1, clientid1, year1);
            //    const quarter1 = 100
            //    const clientid1 = 700
            //    const year1 = 202000
            //    //console.log(quarter1, clientid1, year1);
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
            //        url: "DashboardData.asmx/GetWeeklyData",
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
            //                    "title": "Number of Disbursements",
            //                    "type": "column",
            //                    "valueField": "DisbCount"
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
            //                    "title": "Number of Repayments",
            //                    "valueField": "RepayCount"
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
    </form>
</body>
