var cart = {
    init: function () {
        cart.regEvents()
    },
    regEvents: function () {

        //continue
        $('#btn_Continue').off('click').on('click', function () {
            window.location.href = "/"

        })

        //update gio hang
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

        //xoa gio hang
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

        //xoa san pham khoi gio hang
        $('.btn_del').off('click').on('click', function () {
            $.ajax({
                data: { id: $(this).data('id') },
                url: "/Cart/Delete",
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
