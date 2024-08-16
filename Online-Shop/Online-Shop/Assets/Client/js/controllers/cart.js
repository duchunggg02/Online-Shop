var cart = {
    init: function () {
        cart.regEvents()
    },
    regEvents: function () {
        $('#btn_Continue').off('click').on('click', function () {
            window.location.href = "/"

        })

        $('#btn_Update').off('click').on('click', function () {
            var productQuantityInput = $('.input_quantity')

            var cartList = []
            $.each(productQuantityInput, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    Product: {
                        ID: $(item).data('id')
                    }
                })
            })

            $.ajax({
                url: "/Cart/Update",
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Cart"
                    }
                }
            })
        })

        $('#btn_Delete').off('click').on('click', function () {

            $.ajax({
                url: "/Cart/DeleteAll",
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Cart"
                    }
                }
            })
        })
    }
}

cart.init()
