"use strict";

$(document).on("click", "#checkPrice", function (e) {
    e.preventDefault();
    $(".orderErrorMessage").addClass("text-danger").removeClass("text-success").text("")
    if (!$("#isOrderPolicyAccepted").hasClass("active")) {
        $(".orderErrorMessage").text("Sifariş şərtlərini oxuyun və qəbul edin.")
        return
    }
    let cartProducts = [];

    $('.prod-li').each(function () {
        let productCode = $(this).attr('data-code');
        let price = parseFloat($('#price-' + productCode).val());
        let quantity = parseInt($('#count-' + productCode).val());
        cartProducts.push({
            ProductCode: productCode,
            Quantity: quantity
        });
    });

    if (cartProducts.length == 0) {
        $(".orderErrorMessage").text("Səbət boşdur.")
        return
    }
    $(".main-wrapper-for-cart").addClass("d-none")
    $(".loading-wrapper-for-cart").removeClass("d-none")
    fetch(`/Cart/Order`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(cartProducts)
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                $(".allprods.prod-litems").html('')
                $(".orderErrorMessage").removeClass("text-danger").addClass("text-success").text("Sifariş uğurla təsdiq olundu")
                $(".cart_list").html('')
                $(".woocommerce.allprods.prod-litems.section-list").html('')
                getkBasketCount()
                localStorage.removeItem('basketList');
            }
            else {
                $(".orderErrorMessage").text(data.message)
            }
            $(".main-wrapper-for-cart").removeClass("d-none")
            $(".loading-wrapper-for-cart").addClass("d-none")
        })
});

$(".section-sb-current a.search").on("click", function (e) {
    e.preventDefault()
    alert("Handler for `click` called.");
});

$(".section-sb-current .section-sb-list li a").on("click", function (e) {
    e.preventDefault()
    if ($(this).hasClass("subcategoried")) {
        if ($(this).next().hasClass("active")) {
            $(this).next().removeClass("active").hide("fast")
            $(this).children().last().removeClass("active")
        }
        else {
            $(".section-sb-current .section-sb-list li ul.subCategories.active").removeClass("active").hide("fast")
            $(".section-sb-current .section-sb-list li a i.active").removeClass("active")

            $(this).next().addClass("active").show("fast")
            $(this).children().last().addClass("active")
        }
    }
    else {
        if ($(this).prev().prop('checked')) {
            $(this).prev().prop('checked', !$(this).prev().prop('checked'))
            $(this).prev().removeClass("active")
        }
        else {
            $(".section-sb-current .section-sb-list input:checked").prop('checked', false)
            $(".section-sb-current .section-sb-list input.active").removeClass("active")
            $(this).prev().prop('checked', !$(this).prev().prop('checked'))
            $(this).prev().addClass("active")
        }
        $(".errorMessageForCategory").addClass("d-none")
    }
});

$(".section-sb-current .section-sb-list input").on("click", function () {
    $('.section-sb-current .section-sb-list input[type="checkbox"]').not(this).prop('checked', false);
    $(".errorMessageForCategory").addClass("d-none")
    if ($(this).hasClass('active')) {
        $(this).removeClass("active")
    }
    else {
        $(".section-sb-current .section-sb-list input.active").removeClass("active")
        $(this).addClass("active")
    }
});

$("#isRegisterPolicyAccepted").on("click", function () {
    $(this).toggleClass('active')
});

$("#isOrderPolicyAccepted").on("click", function () {
    $(this).toggleClass('active')
});



$(".searchByCategories").on("click", function (e) {
    e.preventDefault()
    var $input = $('.section-sb-current .section-sb-list input[type="checkbox"]:checked')
    if ($input.length > 0) {
        $(".errorMessageForCategory").addClass("d-none")
        window.location.href = $input.next().attr('href')
    }
    else {
        
        $(".errorMessageForCategory").removeClass("d-none")
    }
})

$(document).ready(function () {
    var decodedUrl = decodeURIComponent(window.location.href)
    if (decodedUrl.includes("/Məhsul/Məhsullarımız?categoryGUID")) {
        var guid = decodedUrl.split("categoryGUID=")[1]
        var $input = $(`.productCategories a[data-category-GUID='${guid}']`).prev()
        $input.prop('checked', true)
        $input.addClass("active")
        if ($input.parents(".subCategories").length > 0) {
            $input.parents(".subCategories").addClass("active")
            $input.parents(".subCategories").show("fast")
        }
    }
});

$(document).ready(function () {
    $("#h-search").on("submit", function (event) {
        const searchValue = $("#search").val();
        $(this).attr("action", $(this).attr("action") + "?search=" + encodeURIComponent(searchValue));
    });
});

function debounce(func, delay) {
    let timeout;
    return function (...args) {
        clearTimeout(timeout);
        timeout = setTimeout(() => func.apply(this, args), delay);
    };
}



$(document).ready(function () {
    if ($(".prod-litems").length > 0) {
        var firstTime = true
        const handleScroll = debounce((isProductsPerPage) => {
            console.log("a")
            const scrollTop = $(window).scrollTop();
            const clientHeight = $(window).innerHeight();
            const scrollHeight = document.documentElement.scrollHeight;
            const isAtBottom = (scrollTop + clientHeight) >= (scrollHeight - 700);
            const productsPerPage = parseInt($(".section-top-sort select").val(), 10);
            var skippedProducts = parseInt($("#skipedProducts").val(), 10);
            const productsCount = parseInt($("#productsCount").val(), 10);
            const basketList = localStorage.getItem('basketList');
            var productIds = []
            if (basketList) {
                var beautifiedHTML = basketList
                    .replace(/\\/g, '')
                    .replace(/\n\s+/g, '\n')
                    .trim()

                var tempDiv = document.createElement('div');
                tempDiv.innerHTML = beautifiedHTML;
                var listItems = tempDiv.querySelectorAll('li');
                listItems.forEach(function (item) {
                    var productIdFromHtml = item.getAttribute('pro-id');
                    var quantityFromHtml = item.querySelector('.quantity') ? parseInt(item.querySelector('.quantity').innerText.split("×")[0].trim()) : 1;
                    if (productIdFromHtml && quantityFromHtml) {
                        var productId = productIdFromHtml;
                        var quantityValue = quantityFromHtml;
                        productIds.push({ "productId": productId, "quantity": quantityValue })
                    }
                });
            }

            if (skippedProducts < productsCount) {
                $(".loading-wrapper").removeClass("d-none");
            }
            else {
                $(".loading-wrapper").addClass("d-none");
            }

            if (isAtBottom || isProductsPerPage || firstTime) {
                firstTime = false
                var guid = ""
                const search = $("#searchInput").val()
                var decodedUrl = decodeURIComponent(window.location.href)
                if (decodedUrl.includes("/Məhsul/Məhsullarımız?categoryGUID")) {
                    guid = decodedUrl.split("categoryGUID=")[1]
                }

                if (skippedProducts < productsCount) {

                    $(".loading-wrapper").removeClass("d-none");
                    fetch(`/Ajax/GetProducts?categoryGUID=${guid}&search=${encodeURIComponent(search)}`, {
                        method: "POST",
                        headers: {
                            "Content-Type": "application/json"
                        },
                        body: JSON.stringify({ "skip": skippedProducts, "productsPerPage": productsPerPage })
                    })
                        .then(response => response.json())
                        .then(data => {
                            var cartProducts = []
                            $('.cart_list').children('li').each(function () {
                                var ProId = $(this).attr("pro-id");
                                var ProCount = parseInt($(this).attr("pro-count"))
                                var Price = parseFloat($(this).find('.per-product-money').text())
                                var TotalPrice = (ProCount * Price)

                                cartProducts.push({ "GUID": ProId, "Count": ProCount, "Price": Price, "TotalPrice": TotalPrice })
                            });

                            $.each(data.products, function (index, product) {
                                var cartProduct = cartProducts.find(p => p.GUID === product.guid)
                                var totalPrice = product.price
                                var count = 1
                                if (cartProduct) {
                                    totalPrice = cartProduct.TotalPrice
                                    count = cartProduct.Count
                                }
                                const productHtml = `
                                    <article class="prod-li sectls">
                                        <div class="prod-li-inner">
                                            <a class="prod-li-img" href="/Məhsul_Haqqında/${product.guid}">
                                                <img id="baseimage-${product.guid}" src="${product.imageUrl}" alt="${product.name}" />
                                            </a>
                                            <div class="prod-li-cont">
                                                <div class="prod-li-ttl-wrap">
                                                    <p><a href="/M%C9%99hsul/M%C9%99hsullar%C4%B1m%C4%B1z?categoryGUID=${product.categoryGUID}">${product.categoryName}</a></p>
                                                    <h3>
                                                        <a id="product-name-${product.guid}" href="/Məhsul_Haqqında/${product.guid}">
                                                            ${product.name}
                                                        </a>
                                                    </h3>
                                                </div>
                                                <div class="prod-li-prices">
                                                    <div class="prod-li-price-wrap">
                                                        <p>Qiyməti</p>
                                                        <p class="prod-li-price">₼ ${product.price}</p>
                                                        <input type="hidden" id="price-${product.guid}" value="${product.price}" />
                                                    </div>
                                                    <div class="prod-li-qnt-wrap">
                                                        <p class="qnt-wrap prod-li-qnt">
                                                            <a data-id="${product.guid}" class="qnt-plus prod-li-plus">
                                                                <i class="icon ion-arrow-up-b"></i>
                                                            </a>
                                                            <input type="text" id="count-${product.guid}" readonly value="${count}" />
                                                            <a data-id="${product.guid}" class="qnt-minus prod-li-minus">
                                                                <i class="icon ion-arrow-down-b"></i>
                                                            </a>
                                                        </p>
                                                    </div>
                                                    <div class="prod-li-total-wrap">
                                                        <p>Cəm</p>
                                                        <p id="total-${product.guid}" class="prod-li-total">₼ ${totalPrice}</p>
                                                        <input type="hidden" id="total-value-${product.guid}" value="${totalPrice}" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="prod-li-info">
                                                <p class="prod-li-add" style="cursor: pointer;">
                                                    <a id="${product.guid}" class="button hover-label prod-addbtn">
                                                        <i class="icon ion-android-cart"></i>
                                                        <span>Səbətə əlavə et</span>
                                                    </a>
                                                </p>
                                            </div>
                                        </div>
                                    </article>`;
                                $(".prod-litems").append(productHtml);
                            });
                            skippedProducts += productsPerPage
                            $("#skipedProducts").val(skippedProducts);
                            if (skippedProducts >= productsCount) {
                                $(".loading-wrapper").addClass("d-none");
                            }

                        })
                        .catch(error => {
                            console.error("Error sending POST:", error);
                        });
                }
                else {
                    $(".loading-wrapper").addClass("d-none");
                }
            }
        }, 300);
        
        $(window).on("scroll", function () {
            handleScroll(false);
        });

        $(".products-per-page select").on("change", function () {
            $("#productsPerPage").val($(this).val())
            $(".prod-litems .prod-li").remove()
            $("#skipedProducts").val(0)
            handleScroll(true)
        })

        //handleScroll(true)
        
        //setTimeout(function () {
        //    handleScroll(true)
        //}, 500);
        handleScroll(true)
    }
});


$(document).ready(function () {
    $("#main .owl-carousel").owlCarousel({
        loop: true,
        margin: 10,
        nav: false,
        dots: true,
        margin: 25,
        responsive: {
            0: {
                items: 1
            }
        }
    });
});

function getkBasketCount() {
    var count = $(".cart_list li").length;
    $("#basket-count").text(count);
    calcPrice();
}

function calcPrice() {
    var totalPrice = 0;

    $('.cart_list').children('li').each(function () {
        var quantity = parseInt($(this).attr("pro-count"))
        var price = parseFloat($(this).find('.per-product-money').text().replace(',', '.'))
        totalPrice = totalPrice + (quantity * price)
    });

    $(".sum-money").text(`₼ ${totalPrice.toFixed(2)}`);
}


function clickForBasket(id, isArrow = false) {
    if ($(".cart_list li").length > 0) {
        var isDuplicate = false;
        $('.cart_list').children('li').each(function () {
            var currentLiId = $(this).attr('id');
            if (currentLiId == `basketed-li-${id}`) {
                $(this).remove();
                appendBask(id);
                isDuplicate = true;
                return false;
            }
        });
        if (!isDuplicate && !isArrow) {
            appendBask(id);
        }
    } else if (!isArrow) {
        appendBask(id);
    }
    getkBasketCount();
}

function appendBask(clickedId) {
    if (clickedId) {
        var imgSRC = $(`#baseimage-${clickedId}`).attr("src");
        var productName = $(`#product-name-${clickedId}`).text();
        var price = $(`#price-${clickedId}`).val();
        var totalCount = $(`#count-${clickedId}`).val();
        var categoryGuid = $(`a#product-name-${clickedId}`).parent().prev().children().first().attr("href").split("=")[1]
        var categoryName = $(`a#product-name-${clickedId}`).parent().prev().children().first().text()

        var element = `<li pro-count=${totalCount} pro-id=${clickedId} id="basketed-li-${clickedId}" categoryName="${categoryName}" categoryGUID="${categoryGuid}">

                                        <a del-id="${clickedId}" class="remove">&times;</a>

                                        <a href="/Məhsul_Haqqında/${clickedId}">

                                            <img src="${imgSRC}" alt="">

                                            ${productName}

                                        </a>

                                        <span class="quantity">${totalCount} &times; ₼<span class="per-product-money"> ${price}</span></span>

                                    </li>`;

        $(".cart_list").append(element);
        setStorage();
    }
}
function setStorage() {
    var baskList = $(".cart_list").html();
    var isExist = localStorage.getItem('basketList');
    if (isExist) {
        localStorage.removeItem('basketList');
    }
    localStorage.setItem('basketList', JSON.stringify(baskList));
}

$(function () {

    $(document).on("click", ".prod-addbtn", function (e) {
        e.preventDefault();
        var clickedId = $(this).attr("id");

        clickForBasket(clickedId);
    });

    $(document).on("click", ".remove", function (e) {
        e.preventDefault();
        var clickedId = $(this).attr("del-id");
        $("article").remove(`#bask-remove-${clickedId}`)
        $("li").remove(`#basketed-li-${clickedId}`);
        getkBasketCount();
        setStorage();
    });
});

function multishop_initslider(refresh, parent) {

    var slider_thumbs = '#prod-thumbs';
    var slider_main = '#prod-slider';

    if (refresh) {
        jQuery('#prod-thumbs').removeData("flexslider");
        jQuery('#prod-thumbs .slides').find("li").off();
        jQuery('#prod-slider').removeData("flexslider");
    }

    if (parent) {
        slider_thumbs = parent + ' ' + slider_thumbs;
        slider_main = parent + ' ' + slider_main;
    }

    jQuery(slider_thumbs).flexslider({
        animation: "slide",
        controlNav: false,
        animationLoop: false,
        slideshow: false,
        itemWidth: 97,
        itemMargin: 0,
        minItems: 5,
        maxItems: 5,
        asNavFor: slider_main,
        start: function (slider) {
            jQuery(slider_thumbs).resize();
        }
    });
    jQuery(slider_main).flexslider({
        animation: "fade",
        animationSpeed: 500,
        slideshow: false,
        animationLoop: false,
        smoothHeight: false,
        controlNav: false,
        sync: slider_thumbs,
        after: function (slider) {
            jQuery('.prod-slider-count .count-cur').text(slider.currentSlide + 1);
        }
    });
}

jQuery(document).ready(function ($) {
    var lsPoduct = localStorage.getItem('basketList');
    if (lsPoduct) {
        $(".cart_list").html(JSON.parse(lsPoduct));
        
        getkBasketCount();
    }
    // Modal Form
    $('.callback').fancybox({
        padding: 0,
        content: $('#modal-form'),
        helpers: {
            overlay: {
                locked: false
            }
        },
        tpl: {
            closeBtn: '<a title="Close" class="fancybox-item fancybox-close modal-form-close" href="javascript:;"></a>'
        }
    });

    $(document).on("click", "#btnCheckout", function (e) {
        e.preventDefault();

        let data = []

        $('.cart_list').children('li').each(function () {
            var ProductCode = $(this).attr("pro-id");
            var Quantity = parseInt($(this).attr("pro-count"))
            var Name = $(this).find('a').last().text().trim();
            var ImageUrl = $(this).find('a').last().find('img').attr('src');
            var Price = parseFloat($(this).find('.per-product-money').text().replace(',', '.'))
            var CategoryName = $(this).attr("categoryname");
            var CategoryGUID = $(this).attr("categoryguid");
            var TotalPrice = (Quantity * Price)

            data.push({ "ProductCode": ProductCode, "Quantity": Quantity, "Name": Name, "ImageUrl": ImageUrl, "Price": Price, "TotalPrice": TotalPrice, "CategoryName": CategoryName, "CategoryGUID": CategoryGUID })
        });

        if (data.length > 0) {
            var jsonData = JSON.stringify(data);
            var encodedJsonData = encodeURIComponent(jsonData);
            var url = `/Cart/Checkout?productsJson=${encodedJsonData}`;
            window.location.href = url;
        }
    });

    function getCookie(name) {
        const value = `; ${document.cookie}`;
        const parts = value.split(`; ${name}=`);
        if (parts.length === 2) return parts.pop().split(';').shift();
    }

    $('.prod-li-add').css('cursor', 'pointer');
    $('.LoginRegUser').css('cursor', 'pointer');
    $('.resetPass').css('cursor', 'pointer');

    
    // Fancybox Images
    $('.fancy-img').fancybox({
        padding: 0,
        margin: [60, 50, 20, 50],
        helpers: {
            overlay: {
                locked: false
            },
            thumbs: {
                width: 60,
                height: 60
            }
        },
        tpl: {
            closeBtn: '<a title="Close" class="fancybox-item fancybox-close modal-form-close2" href="javascript:;"></a>',
            prev: '<a title="Previous" class="fancybox-nav fancybox-prev modal-prev" href="javascript:;"><span></span></a>',
            next: '<a title="Next" class="fancybox-nav fancybox-next modal-next" href="javascript:;"><span></span></a>',
        }
    });

    // Modal Videos
    $(".multishop-gallery").on('click', ".multishop-gallery-video", function () {
        $.fancybox({
            'padding': 0,
            'autoScale': false,
            'transitionIn': 'none',
            'transitionOut': 'none',
            'href': this.href.replace(new RegExp("watch\\?v=", "i"), 'v/index.html'),
            'type': 'swf',
            'swf': {
                'wmode': 'transparent',
                'allowfullscreen': 'true'
            },
            tpl: {
                closeBtn: '<a title="Close" class="fancybox-item fancybox-close modal-form-close2" href="javascript:;"></a>'
            }
        });
        return false;
    });


    // Dropdown
    if ($('.dropdown-wrap').length > 0) {
        $('.dropdown-wrap').on('click', '.dropdown-title', function () {
            $('.dropdown-list').slideUp(200);
            if ($(this).hasClass('opened')) {
                $(this).removeClass('opened');
            } else {
                $('.dropdown-wrap .dropdown-title').removeClass('opened');
                $(this).addClass('opened');
                $(this).next('.dropdown-list').slideDown(200);
            }
            return false;
        });
        $('.cont').on('click', '.dropdown-wrap-range', function () {
            return false;
        });
        $('.dropdown-wrap .dropdown-list li').on('click', 'a', function () {
            $(this).closest('.dropdown-wrap').find('.dropdown-title').text($(this).text());
            if ($(this).attr('href') == '#') {
                $('.dropdown-list').slideUp(200);
                $('.dropdown-wrap .dropdown-title').removeClass('opened');
                return false;
            }
        });
    }

    if ($('.dropdown-wrap').length > 0 || $('#b-crumbs-menu').length > 0) {
        $('body').on('click', function () {
            if ($('#b-crumbs-menu').length > 0) {
                $('.b-crumbs-menulist').fadeOut(200);
                $('#b-crumbs-menu').removeClass('opened');
            }
            if ($('.dropdown-wrap').length > 0) {
                $('.dropdown-list').slideUp(200);
                $('.dropdown-wrap .dropdown-title').removeClass('opened');
            }
        });
    }

    // Top Menu Seacrh
    $('.site-header').on('click', '#h-search-btn', function () {
        if ($(this).hasClass('opened')) {
            $(this).removeClass('opened');
            if ($('#h-search').length > 0) {
                $('#h-search').fadeOut();
            } else if ($('#h-search').length > 0) {
                $('#h-search').fadeOut();
            }
        } else {
            $(this).addClass('opened');
            if ($('#h-search').length > 0) {
                $('#h-search').fadeIn();
            } else if ($('#h-search').length > 0) {
                $('#h-search').fadeIn();
            }
        }
        return false;
    });


    // Top Menu
    $('.site-header').on('click', '#h-menu-btn', function () {
        $(this).toggleClass('opened');
        $('.cmm-toggle-wrapper .cmm-toggle').click();

        if ($('#h-menu').length > 0) {
            $('#h-menu').slideToggle();
        }

        return false;
    });

    // Top SubMenu
    $('.mainmenu nav > ul').on('click', '.menu-item-has-children > a', function () {
        if ($(window).width() < 751) {
            if ($(this).hasClass('opened')) {
                $(this).removeClass('opened');
                $(this).next('ul').slideUp();
            } else {
                $(this).addClass('opened');
                $(this).next('ul').slideDown();
            }
            return false;
        }
    });


    // Filter Button
    if ($('#section-filter-toggle-btn').length > 0 && $('.section-filter .woof_redraw_zone').length > 0) {
        $('.section-filter-toggle').on('click', '#section-filter-toggle-btn', function () {
            if ($(this).parent().hasClass('filter_hidden')) {
                $(this).text($(this).data('hidetext')).parent().removeClass('filter_hidden');
                document.cookie = "filter_toggle=filter_opened; expires=Thu, 31 Dec 2020 23:59:59 GMT; path=/;";
            } else {
                $(this).text($(this).data('showtext')).parent().addClass('filter_hidden');
                document.cookie = "filter_toggle=filter_hidden; expires=Thu, 31 Dec 2020 23:59:59 GMT; path=/;";
            }

            return false;
        });
    }

    // Sticky sidebar
    if ($('#section-sb').length > 0 && $('#section-list-withsb').length > 0) {
        $('#section-sb, #section-list-withsb').theiaStickySidebar({
            additionalMarginTop: 30
        });
    }


    // Product Tabs
    $('body').on('click', '.prod-tabs li a', function () {
        if ($(this).parent().hasClass('prod-tabs-addreview') || $(this).parent().hasClass('active') || $(this).attr('data-prodtab') == '')
            return false;
        $('.prod-tabs li').removeClass('active');
        $(this).parent().addClass('active');

        // mobile
        $('.prod-tab-mob').removeClass('active');
        $('.prod-tab-mob[data-prodtab-num=' + $(this).parent().data('prodtab-num') + ']').addClass('active');

        $('.prod-tab-cont .prod-tab').hide();
        $($(this).attr('data-prodtab')).fadeIn();

        return false;
    });

    // Product Tabs (mobile)
    $('body').on('click', '.prod-tab-cont .prod-tab-mob', function () {
        if ($(this).hasClass('active') || $(this).attr('data-prodtab') == '')
            return false;
        $('.prod-tab-cont .prod-tab-mob').removeClass('active');
        $(this).addClass('active');

        // main
        $('.prod-tabs li').removeClass('active');
        $('.prod-tabs li[data-prodtab-num=' + $(this).data('prodtab-num') + ']').addClass('active');

        $('.prod-tab-cont .prod-tab').slideUp();
        $($(this).attr('data-prodtab')).slideDown();
        return false;
    });

    $('body').on('click', '.prod-tabs-addreview', function () {
        if ($('.prod-tabs li.active a').attr('data-prodtab') == '#prod-tab-3') {
            $('html, body').animate({ scrollTop: ($('.prod-tabs-wrap').offset().top - 10) }, 700);
        } else {
            $('.prod-tabs li').removeClass('active');
            $('#prod-reviews').addClass('active');
            $('.prod-tab-cont .prod-tab').hide();
            $('.prod-tab-cont .prod-tab.prod-reviews').fadeIn();
            $('html, body').animate({ scrollTop: ($('.prod-tabs-wrap').offset().top - 10) }, 700);
        }

        $('#review_form_wrapper').fadeIn();

        return false;
    });

    if ($('.prod-tab #commentform #submit').length > 0) {
        var filterEmail = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,6})+$/;
        $('body').on('click', '.prod-tab #commentform #submit', function () {
            var errors = false;
            if (!$(this).parents('#commentform').find('#rating').val()) {
                $('.prod-tab-addreview').addClass('error');
                errors = true;
            }
            $(this).parents('#commentform').find('input[type=text], input[type=email], textarea').each(function () {
                if ($(this).attr('id') == 'email') {
                    if (!filterEmail.test($(this).val())) {
                        $(this).addClass("redborder");
                        errors++;
                    }
                    else {
                        $(this).removeClass("redborder");
                    }
                    return;
                }
                if ($(this).val() == '') {
                    $(this).addClass('redborder');
                    errors++;
                } else {
                    $(this).removeClass('redborder');
                }
            });

            if (errors)
                return false;
        });
    }

    // Show Properties
    $('.prod-cont').on('click', '#prod-showprops', function () {
        if ($('.prod-tabs li.active a').attr('data-prodtab') == '#prod-tab-2') {
            $('html, body').animate({ scrollTop: ($('.prod-tabs-wrap').offset().top - 10) }, 700);
        } else {
            $('.prod-tabs li').removeClass('active');
            $('#prod-props').addClass('active');
            $('.prod-tab-cont .prod-tab').hide();
            $('#prod-tab-2').fadeIn();
            $('html, body').animate({ scrollTop: ($('.prod-tabs-wrap').offset().top - 10) }, 700);
        }
        return false;
    });

    // Show Description
    $('.prod-cont').on('click', '#prod-showdesc', function () {
        if ($('.prod-tabs li.active a').attr('data-prodtab') == '#prod-tab-1') {
            $('html, body').animate({ scrollTop: ($('.prod-tabs-wrap').offset().top - 10) }, 700);
        } else {
            $('.prod-tabs li').removeClass('active');
            $('#prod-desc').addClass('active');
            $('.prod-tab-cont .prod-tab').hide();
            $('#prod-tab-1').fadeIn();
            $('html, body').animate({ scrollTop: ($('.prod-tabs-wrap').offset().top - 10) }, 700);
        }
        return false;
    });



    // Post Add Comment Form
    $('.post-comments').on('click', '#post-comments-add', function () {
        $('#post-addcomment-form').slideDown();
        $(this).fadeOut();
        return false;
    });



    // Select Styler
    if ($('.prod-add .variations select').length > 0) {
        $('.prod-add .variations select').chosen({
            disable_search_threshold: 10
        });
    }
    if ($('.section-top .products-per-page select').length > 0) {
        $('.section-top .products-per-page select').chosen({
            disable_search_threshold: 10
        });
    }
    if ($('.blog-sb-widget:not(.WOOF_Widget) select').length > 0) {
        $('.blog-sb-widget:not(.WOOF_Widget) select').chosen({
            disable_search_threshold: 10
        });
    }
    if ($('.shipping-calculator-form select').length > 0) {
        $('.shipping-calculator-form select').chosen({
            disable_search_threshold: 10
        });
    }


    // Product List Show Information
    $('body').on('click', '.prod-li-information', function () {
        var product_info = $(this).parents('.prod-li').find('.prod-li-informations');
        $(this).toggleClass('opened');
        if (product_info.length) {
            product_info.slideToggle();
        }
        return false;
    });


    // Widgets Mobile Toggle
    if ($('.section-sb .blog-sb-widget .widgettitle').length > 0) {
        $('.section-sb .blog-sb-widget').on('click', '.widgettitle', function () {
            if ($(window).width() <= 991) {
                if ($(this).hasClass('opened')) {
                    $(this).removeClass('opened').next().slideUp();
                } else {
                    $(this).addClass('opened').next().slideDown();
                }
            }
            return false;
        });
    }

});

(function ($) {
    jQuery(window).load(function () {

        // Product Slider
        if ($('#prod-slider').length > 0) {
            multishop_initslider(false, '');

            $('#prod-slider').on('click', '.prod-slider-zoom', function () {
                $('#prod-slider .slides .flex-active-slide .fancy-img').click();
                return false;
            });
        }

        // Slider "About Us"
        if ($('.content_carousel').length > 0) {
            $('.content_carousel').each(function () {
                if ($(this).data('slideshow_speed') != '') {
                    var slideshow_speed = $(this).data('slideshow_speed');
                } else {
                    var slideshow_speed = '7000';
                }
                if ($(this).data('animation_speed') != '') {
                    var animation_speed = $(this).data('animation_speed');
                } else {
                    var animation_speed = '600';
                }
                if ($(this).data('navigation') == true) {
                    var navigation = true;
                } else {
                    var navigation = false;
                }
                if ($(this).data('pagination') == true) {
                    var pagination = true;
                } else {
                    var pagination = false;
                }
                if ($(this).data('stop_on_hover') == true) {
                    var stop_on_hover = true;
                } else {
                    var stop_on_hover = false;
                }
                $('.content_carousel').flexslider({
                    pauseOnHover: stop_on_hover,
                    animationSpeed: animation_speed,
                    slideshowSpeed: slideshow_speed,
                    useCSS: false,
                    directionNav: navigation,
                    controlNav: pagination,
                    animation: "fade",
                    slideshow: false,
                    animationLoop: true,
                    smoothHeight: true
                });
            });
        }


        // Blog sliders
        if ($('.blog-slider').length > 0) {
            $('.blog-slider').flexslider({
                animation: "fade",
                animationSpeed: 500,
                slideshow: false,
                animationLoop: false,
                directionNav: false,
                smoothHeight: false,
                controlNav: true,
            });
        }
        if ($('.post-slider').length > 0) {
            $('.post-slider').flexslider({
                animation: "fade",
                animationSpeed: 500,
                slideshow: false,
                animationLoop: false,
                directionNav: false,
                smoothHeight: true,
                controlNav: true,
            });
        }

        // Slider "Testimonials"
        if ($('.testimonials-car').length > 0) {
            $('.testimonials-car').each(function () {
                var testimonials_slider;
                if ($(this).data('slideshow_speed') != '') {
                    var slideshow_speed = $(this).data('slideshow_speed');
                } else {
                    var slideshow_speed = '7000';
                }
                if ($(this).data('animation_speed') != '') {
                    var animation_speed = $(this).data('animation_speed');
                } else {
                    var animation_speed = '600';
                }
                if ($(this).data('navigation') == true) {
                    var navigation = true;
                } else {
                    var navigation = false;
                }
                if ($(this).data('pagination') == true) {
                    var pagination = true;
                } else {
                    var pagination = false;
                }
                if ($(this).data('stop_on_hover') == true) {
                    var stop_on_hover = true;
                } else {
                    var stop_on_hover = false;
                }
                if ($(this).hasClass('style-1')) {
                    var items = 1;
                    var item_margin = 0;
                } else {
                    var items = 2;
                    if ($(window).width() < 751) {
                        items = 1;
                    }
                    var item_margin = 68;
                }
                $(this).flexslider({
                    pauseOnHover: stop_on_hover,
                    animationLoop: true,
                    animation: 'slide',
                    animationSpeed: animation_speed,
                    slideshowSpeed: slideshow_speed,
                    useCSS: false,
                    directionNav: navigation,
                    controlNav: pagination,
                    slideshow: false,
                    itemMargin: item_margin,
                    itemWidth: 2000,
                    maxItems: items,
                    minItems: items,
                    start: function (slider) {
                        testimonials_slider = slider;
                    }
                });
                $(window).resize(function () {
                    if ($(window).width() < 751) {
                        testimonials_slider.vars.minItems = 1;
                        testimonials_slider.vars.maxItems = 1;
                    } else {
                        testimonials_slider.vars.minItems = items;
                        testimonials_slider.vars.maxItems = items;
                    }
                });
            });
        }



        // Quantity
        $('body').on('click', '.qnt-wrap a', function () {
            var qnt = $(this).parent().find('input').val();
            var clickedId = $(this).attr('data-id');
            var price = $(`#price-${clickedId}`).val();
            
            if ($(this).hasClass('qnt-plus')) {
                qnt++;
            } else if ($(this).hasClass('qnt-minus')) {
                qnt--;
            }

            if (qnt > 0) {
                $(this).parent().find('input').attr('value', qnt);

                $(`#total-${clickedId}`).text(`₼ ${(price.replace(',', '.') * qnt).toFixed(2)}`);
                $(`#total-value-${clickedId}`).val((price.replace(',', '.') * qnt).toFixed(2));
            }
            var id = $(this).attr("data-id");
            clickForBasket(id, true);
            return false;
        });



        // Masonry Grids
        if ($('#blog-grid').length > 0) {
            $('#blog-grid').isotope({
                itemSelector: '.blog-grid-i',
            });
        }
        if ($('#gallery-grid').length > 0) {

            var $grid = $('#gallery-grid').isotope({
                itemSelector: '.gallery-grid-i',
            });
            $('#gallery-sections').on('click', 'a', function () {
                var filterValue = $(this).attr('data-section');
                $grid.isotope({ filter: filterValue });
            });
            $('#gallery-sections').each(function (i, buttonGroup) {
                var $buttonGroup = $(buttonGroup);
                $buttonGroup.on('click', 'a', function () {
                    $buttonGroup.find('.active').removeClass('active');
                    $(this).addClass('active');
                    return false;
                });
            });

        }
        if ($('.multishop-gallery').length > 0) {

            var $grid = $('.multishop-gallery').isotope({
                itemSelector: '.gallery-grid-i',
            });
            $('.multishop-gallery-sections').on('click', 'a', function () {
                var filterValue = $(this).attr('data-section');
                $grid.isotope({ filter: filterValue });
            });
            $('.multishop-gallery-sections').each(function (i, buttonGroup) {
                var $buttonGroup = $(buttonGroup);
                $buttonGroup.on('click', 'a', function () {
                    $buttonGroup.find('.active').removeClass('active');
                    $(this).addClass('active');
                    return false;
                });
            });

        }
        if ($('#about-gallery').length > 0) {
            $('#about-gallery').isotope({
                itemSelector: '.grid-item',
                columnWidth: '.grid-sizer',
                percentPosition: true
            });
        }



        // Quick View
        $('.quick-view').fancybox({
            content: $('.quick-view-modal'),
            padding: 0,
            helpers: {
                overlay: {
                    locked: false
                }
            },
            afterLoad: function () {
                multishop_initslider(true);
            }
        });


        // Sticky header
        if ($('.header-sticky').length > 0) {
            $(window).scroll(function () {
                var topbar = false;
                var topbar_ht = $('.site-header-before').height();
                if ($('.site-header-before').length > 0 && $('.site-header-before').css('display') !== 'none') {
                    topbar = true;
                }
                if (topbar) {
                    $('body').css('margin-top', '0px');
                    if (topbar_ht < $(window).scrollTop()) {
                        $('.header-sticky .site-header').addClass('header_sticky');
                        $('.site-header-before').css('margin-bottom', $('.header-sticky .site-header').outerHeight(true));
                    } else {
                        $('.header-sticky .site-header').removeClass('header_sticky');
                        $('.site-header-before').css('margin-bottom', '0px');
                    }
                } else {
                    $('.header-sticky .site-header').addClass('header_sticky');
                    $('body').css('margin-top', $('.header-sticky .site-header').outerHeight(true));
                }
            });
        }

        if ($('.prod-i').length > 0) {
            $('.prod-i').each(function () {
                var prod = $(this);
                var prod_img = prod.find('.prod-i-img');
                var prod_cont = prod.find('.prod-i-bot');
                if (prod.outerHeight() + 1 < prod_img.outerHeight() + prod_cont.outerHeight() || prod.outerHeight() - 1 > prod_img.outerHeight() + prod_cont.outerHeight()) {
                    prod_img.outerHeight(prod.outerHeight() - prod_cont.outerHeight());
                }
            });
            $(window).resize(function () {
                $('.prod-i').each(function () {
                    var prod = $(this);
                    var prod_img = prod.find('.prod-i-img');
                    var prod_cont = prod.find('.prod-i-bot');
                    if (prod.outerHeight() + 1 < prod_img.outerHeight() + prod_cont.outerHeight() || prod.outerHeight() - 1 > prod_img.outerHeight() + prod_cont.outerHeight()) {
                        prod_img.outerHeight(prod.outerHeight() - prod_cont.outerHeight());
                    }
                });
            });
        }

    });




    // Forms Validation
    var filterEmail = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,6})+$/;
    $('.form-validate').submit(function () {
        var errors = 0;
        $(this).find('[data-required="text"]').each(function () {
            if ($(this).attr('data-required-email') == 'email') {
                if (!filterEmail.test($(this).val())) {
                    $(this).addClass("redborder");
                    errors++;
                }
                else {
                    $(this).removeClass("redborder");
                }
                return;
            }
            if ($(this).val() == '') {
                $(this).addClass('redborder');
                errors++;
            } else {
                $(this).removeClass('redborder');
            }
        });
        if (errors === 0) {
            var form1 = $(this);
            form1.submit();
        }
        return false;
    });
    $('.form-validate').find('[data-required="text"]').blur(function () {
        if ($(this).attr('data-required-email') == 'email' && ($(this).hasClass("redborder"))) {
            if (filterEmail.test($(this).val()))
                $(this).removeClass("redborder");
            return;
        }
        if ($(this).val() != "" && ($(this).hasClass("redborder")))
            $(this).removeClass("redborder");
    });




    // Filter Toggle
    jQuery('.woof_front_toggle').on('click', function () {
        if (jQuery(this).data('condition') == 'opened') {
            jQuery(this).removeClass('woof_front_toggle_opened');
            jQuery(this).addClass('woof_front_toggle_closed');
            jQuery(this).data('condition', 'closed');
            if (woof_toggle_type == 'text') {
                jQuery(this).text(woof_toggle_closed_text);
            } else {
                jQuery(this).find('img').prop('src', woof_toggle_closed_image);
            }
        } else {
            jQuery(this).addClass('woof_front_toggle_opened');
            jQuery(this).removeClass('woof_front_toggle_closed');
            jQuery(this).data('condition', 'opened');
            if (woof_toggle_type == 'text') {
                jQuery(this).text(woof_toggle_opened_text);
            } else {
                jQuery(this).find('img').prop('src', woof_toggle_opened_image);
            }
        }

        //jQuery(this).parents('.woof_container_inner').find('.woof_block_html_items').toggle(500);
        jQuery(this).parents('.woof_container_inner').find('.woof_block_html_items').toggleClass('woof_closed_block');
        return false;
    });


})(jQuery);


// Compare List
(function ($) {
    $.fn.setDraggable = function () {
        var compares = $(this),
            html = $('html');

        compares.each(function () {
            var compare = $(this),
                tables = compare.find('.wccm-table'),
                wrappers = compare.find('.wccm-table-wrapper'),
                dragging = false,
                maxshift = wrappers.width() - tables.width(),
                offset = 0,
                shift = 0;

            $(window).resize(function () {
                maxshift = wrappers.width() - tables.width();
                if (maxshift < 0) {
                    wrappers.css('cursor', 'move');
                } else {
                    wrappers.css('cursor', 'default');
                    tables.css('margin-left', '0');
                }
            });

            if (maxshift < 0) {
                wrappers.css('cursor', 'move');
                shift = parseInt(tables.css('margin-left'));
            }

            tables.mousedown(function (e) {
                var node = e.target.nodeName;

                if (maxshift < 0 && node != 'IMG' && node != 'A') {
                    dragging = true;
                    offset = e.screenX;
                    shift = parseInt(tables.css('margin-left'));
                    wrappers.css('cursor', 'default');
                }
            });

            html.mouseup(function () {
                dragging = false;
                if (maxshift < 0) {
                    wrappers.css('cursor', 'move');
                }
            });

            html.mousemove(function (e) {
                var move = shift - (offset - e.screenX);
                if (dragging && maxshift <= move && move <= 0) {
                    tables.css('margin-left', move + 'px');
                }
            });
        });

        return compares;
    };

    $(document).ready(function () {
        $('.wccm-compare-table').setDraggable();
    });

    //$(document).ready(function () {
    //    var basketList = localStorage.getItem('basketList');
    //    if (basketList) {
    //        var tempDiv = $('<div>').html(JSON.parse(basketList));
    //        var listItems = tempDiv.find('li');
    //        listItems.each(function () {
    //            var item = $(this);
    //            var productId = item.attr('pro-id');
    //            var quantityElement = item.find('.quantity');
    //            var quantity = quantityElement.length ? parseInt(quantityElement.text().split("×")[0].trim()) : 1;
    //            if (productId && quantity) {
    //                $(`#count-${productId}`).val(quantity);
    //                var price = parseFloat($(`#price-${productId}`).val().replace(',', '.'))
    //                $(`#total-${productId}`).text(`₼ ${(price * quantity).toFixed(2)}`)
    //                $(`#total-value-${productId}`).val(`${(price * quantity).toFixed(2)}`)
    //            }
    //        });
    //    }

    //    document.getElementById("count-fd16ac36-30f2-11ec-a27c-000c295bf306").value = 3
    //    console.log(document.getElementById("count-fd16ac36-30f2-11ec-a27c-000c295bf306"))
    //});

})(jQuery);
