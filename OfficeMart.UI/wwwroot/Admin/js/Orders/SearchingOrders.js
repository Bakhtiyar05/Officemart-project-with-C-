$(document).ready(function () {

    var _serach_btn = $("#submitSearch");

    _serach_btn.click(function () {

        var _body_tbl = $("#tableBody");
        var _beginDate = $("#beginDate").val();
        var _endDate = $("#endDate").val();
        var _pattern = $("#pattern").val();
        var _routeValue = $("#routeValue").val();
        var _requestUrl = "/Admin/Ajax/SearchOrders";

        $.ajax({
            url: _requestUrl,
            type: "GET",
            dataType: 'json',
            data: { beginDate: _beginDate, endDate: _endDate, pattern: _pattern, routeValue: _routeValue },
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                console.log(result)
                if (result.length != 0) {
                    var _counter = 1;
                    _body_tbl.empty();

                    result.forEach(function (elem) {
                        _body_tbl.append(` <tr data-expanded="true" id='trBody${_counter}'></tr>`)
                        var body_tr = $(`#trBody${_counter}`);
                        body_tr.append(`<td>${_counter}</td>`)
                        body_tr.append(`<td>${elem.orders[0].buyerName + " " + elem.orders[0].buyerSurname}</td>`)
                        body_tr.append(`<td>${elem.orders[0].buyerPhone}</td>`)
                        body_tr.append(`<td>${elem.orders[0].deliveryAddress}</td>`)
                        body_tr.append(`<td>${elem.orders[0].regDate}</td>`)
                        body_tr.append(`<td><a href='/Admin/Order/OrderDetail/${elem.orders[0].orderNumberId}' target="_blank"><i class="fa fa-info btn btn-info"></i></a></td>`)
                       
                        _counter++;
                    });
                }
            }
        })

    });
});
