/**
 Template Name: Appzia Admin
 Dashboard
 */


!function ($) {
    "use strict";

    var Dashboard = function () {
    };


        //creates area chart
        Dashboard.prototype.createAreaChart = function (element, pointSize, lineWidth, data, xkey, ykeys, labels, lineColors) {
            Morris.Area({
                element: element,
                pointSize: 3,
                lineWidth: 1,
                data: data,
                xkey: xkey,
                ykeys: ykeys,
                labels: labels,
                resize: true,
                gridLineColor: '#3d4956',
                hideHover: 'auto',
                lineColors: lineColors
            });
        },
        //creates Bar chart
        Dashboard.prototype.createBarChart = function (element, data, xkey, ykeys, labels, lineColors) {
            Morris.Bar({
                element: element,
                data: data,
                xkey: xkey,
                ykeys: ykeys,
                labels: labels,
                gridLineColor: '#3d4956',
                barSizeRatio: 0.4,
                resize: true,
                hideHover: 'auto',
                barColors: lineColors
            });
        },

        //creates Donut chart
        Dashboard.prototype.createDonutChart = function (element, data, colors) {
            Morris.Donut({
                element: element,
                data: data,
                resize: true,
                colors: colors,
                backgroundColor: '#2f3e47',
                labelColor: '#fff'
            });
        },

        Dashboard.prototype.init = function () {

            //creating area chart
            var $areaData = [
                {y: '1', a: 10, b: 20},
                {y: '2', a: 75, b: 65},
                {y: '3', a: 50, b: 40},
                {y: '4', a: 75, b: 65},
                {y: '5', a: 50, b: 40},
                {y: '6', a: 75, b: 65},
                {y: '7', a: 90, b: 60},
                {y: '8', a: 90, b: 75}
            ];
            this.createAreaChart('morris-area-example', 0, 0, $areaData, 'y', ['a', 'b'], ['ZSE', 'FINSEC'], ['#00a3ff', '#04a2b3']);

            //creating bar chart
            var $barData = [
                {y: 'Jan', a: 100, b: 90},
                {y: 'Feb', a: 75, b: 65},
                {y: 'Mar', a: 50, b: 40},
                {y: 'Apr', a: 75, b: 65},
                {y: 'May', a: 50, b: 40},
                {y: 'Jun', a: 75, b: 65},
                {y: 'Jul', a: 50, b: 9},
                {y: 'Aug', a: 10, b: 5 },
                 { y: 'Sep', a: 80, b: 65 },
            { y: 'Oct', a: 20, b: 14 },
    { y: 'Nov', a: 40, b: 95 },
    { y: 'Dec', a: 80, b: 25 }

            ];
            this.createBarChart('morris-bar-example', $barData, 'y', ['a', 'b'], ['Series A', 'Series B'], ['#04a2b3', '#00a3ff']);

            //creating donut chart
            var $donutData = [
                {label: "Telecel", value: 29},
                {label: "Econet", value: 28},
                {label: "CBZ Touch", value: 5}
            ];
            this.createDonutChart('morris-donut-example', $donutData, ['#dcdcdc', '#e66060', '#04a2b3']);

        },
        //init
        $.Dashboard = new Dashboard, $.Dashboard.Constructor = Dashboard
}(window.jQuery),

//initializing
    function ($) {
        "use strict";
        $.Dashboard.init();
    }(window.jQuery);