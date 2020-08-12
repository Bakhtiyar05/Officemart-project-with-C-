$(function () {

    $(document).on("click", ".prod-addbtn", function (e) {
        e.preventDefault();
        var clickedId = $(this).attr("id");
        if ($(".cart_list li").length > 0) {
            var isDuplicate = false;
            $('.cart_list').children('li').each(function () {
                var currentLiId = $(this).attr('id');

                if (currentLiId == `basketed-li-${clickedId}`) {
                    $(this).remove();
                    appendBask(clickedId);
                    isDuplicate = true;
                    return false;
                }
            });
            if (!isDuplicate) {
                appendBask(clickedId);
            }
        } else {
            appendBask(clickedId);
        }
        getkBasketCount();
    });

    $(document).on("click", ".remove", function (e) {
        e.preventDefault();
        var clickedId = $(this).attr("del-id");
        $("li").remove(`#basketed-li-${clickedId}`);
        getkBasketCount();
    });

    function appendBask(clickedId) {
        var imgSRC = $(`#baseimage-${clickedId}`).attr("src");
        var productName = $(`#product-name-${clickedId}`).text();
        var totalPrice = $(`#total-value-${clickedId}`).val();
        var totalCount = $(`#count-${clickedId}`).val();

        var element = `<li id="basketed-li-${clickedId}">

                                        <a del-id="${clickedId}" class="remove">&times;</a>

                                        <a href="#">

                                            <img src="${imgSRC}" alt="">

                                            ${productName}

                                        </a>

                                        <span class="quantity">${totalCount} &times; ₼${totalPrice}</span>

                                    </li>`;

        $(".cart_list").append(element);
    }

    function getkBasketCount() {
        var count = $(".cart_list li").length;
        $("#basket-count").text(count);
    }

});