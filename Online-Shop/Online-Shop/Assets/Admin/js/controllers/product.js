﻿var product = {
    init: function () {
        product.changeStatusEvents()
    },

    changeStatusEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault()
            var btn = $(this)
            var id = btn.data('id')
            $.ajax({
                url: "/Admin/Product/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "Post",
                success: function (response) {
                    if (response.status == true) {
                        btn.text("Vô hiệu hóa")
                    }
                    else {
                        btn.text("Kích hoạt")
                    }
                }
            })
        })
    }
}

product.init()