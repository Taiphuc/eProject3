$.getJSON("../Admin/Home/Chart", { year: 2021 },
    function (data) {
        chart(data)
    }
);
$('select').on('change', function () {
    $.getJSON("../Admin/Home/Chart", { year: this.value },
        function (data) {
            chart(data)
        }
    );
});
function chart(data) {
    Highcharts.chart('container', {
        chart: {
            type: 'line'
        },
        title: {
            text: 'Biểu đồ thống kê'
        },
        subtitle: {
            text: 'Gas Shop'
        },
        xAxis: {
            categories: ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6', 'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12']
        },
        yAxis: {
            title: {
                text: 'VNĐ'
            }
        },
        plotOptions: {
            line: {
                dataLabels: {
                    enabled: true
                },
                enableMouseTracking: false
            }
        },
        series: [{
            name: 'Tiền nhập',
            data: data.data.stock
        }, {
            name: 'Tiền bán',
            data: data.data.price
        }]
    });
}
$.getJSON("../Admin/Home/Chart", { year: 2020 },
    function (data) {
        console.log(data.data.stock)     
    }
);