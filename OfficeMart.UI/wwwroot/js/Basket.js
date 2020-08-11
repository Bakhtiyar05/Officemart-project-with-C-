$(function () {

    $(document).on("click", ".prod-addbtn", function (e) {
        e.preventDefault();
        var clickedId = $(this).attr("id");
        var imgSRC = $(`#${clickedId}`).attr("src");
        var productName = $(`#product-name-${clickedId}`).text();
        var totalPrice = $(`#total-value-${clickedId}`).val();
        var totalCount = $(`#count-${clickedId}`).val();

        var element = `<li id="${clickedId}">

                                        <a href="#" class="remove">&times;</a>

                                        <a href="#">

                                            <img src="${imgSRC}" alt="">

                                            ${productName}

                                        </a>

                                        <span class="quantity">${totalCount} &times; ₼${totalPrice}</span>

                                    </li>`;

        $(".cart_list").append(element);
    });

});