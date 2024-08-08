var user = {
    init: function (url) {
        user.changeStatusEvents(url)
    },

    changeStatusEvents: function (url) {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault()
            var btn = $(this)
            var id = btn.data('id')
            $.ajax({
                url: url,
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

user.init("/Admin/User/ChangeStatus")