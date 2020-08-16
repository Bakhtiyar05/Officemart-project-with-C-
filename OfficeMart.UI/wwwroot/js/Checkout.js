$(document).ready(() => {

    $(document).on("click", "#submit-checkout", function (e) {

        var counter = 0;
        var errorText = "Sahə tələbolunandır";

        var deliveryAddress = $("#deliveryAddress").val();
        var phone = $("#phone").val();
        var buyerName = $("#buyerName").val();
        var buyerSurname = $("#buyerSurname").val();
        var userId = $("#userId").val();

        if (deliveryAddress == "" || deliveryAddress == undefined || deliveryAddress == null) {
            counter++;
            $(".delivery-address-error-message").text(errorText);
        }
        else {
            $(".delivery-address-error-message").text("");
        }

        if (phone == "" || phone == undefined || phone == null) {
            counter++;
            $(".phone-error-message").text(errorText);
        }
        else {
            $(".phone-error-message").text("");
        }

        var buyerName = $("#buyerName").val();
        var buyerSurname = $("#buyerSurname").val();


        if (counter == 0) {
            var ids = [];
            var counts = [];
            $('.cart_list').children('li').each(function () {
                var ProId = $(this).attr("pro-id");
                var ProCount = $(this).attr("pro-count");
                ids.push(ProId);
                counts.push(ProCount);
            });

            if (ids.length > 0) {

                var url = '/Cart/Checkout';
                var form = $('<form action="' + url + '" method="post">' +
                    '<input type="text" name="ids" value="' + ids + '" />' +
                    '<input type="text" name="counts" value="' + counts + '" />' +
                    '<input type="text" name="BuyerName" value="' + buyerName + '"/>' +
                    '<input type="text" name="BuyerSurname" value="' + buyerSurname + '"/>' +
                    '<input type="text" name="DeliveryAddress" value="' + deliveryAddress + '"/>' +
                    '<input type="text" name="BuyerPhone" value="' + phone + '"/>' +
                    '<input type="text" name="UserId" value="' + userId + '"/>' +
                    '</form>');
                $('body').append(form);
                form.submit();
            }
        }
        
    });

    $(document).on("click", "#close-checkout", function (e) {

        $(".delivery-address-error-message").text("");
        $(".phone-error-message").text("");
    });

});