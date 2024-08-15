var cart = {
    init: function () {
        cart.regEvents()
    },
    regEvents: function () {
        $('#btn_Continue').off('click').on('click', function () {
            window.location.href = "/"

        })

        $('#btn_Update').off('click').on('click', function () {
            var listProduct = $('.input_quantity');

            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    Product: {
                        ID: $(item).data('id')
                    }
                })
            }



        })
    }
}

cart.init()
