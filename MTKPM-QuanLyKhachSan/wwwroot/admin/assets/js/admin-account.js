$(document).ready(function () {
    main();

    // main
    function main() {
        view();
        feature();
    }

    // view
    function view() {
        createAccountView();
    }

    // chức năng
    function feature() {
        createAccount();
        clickPermission()
    }

    // view tạo tại khoản phụ
    function createAccountView() {
        $('#create').on('click', function () {
            ajaxCall(
                'GET',
                '/Admin/AdminAccount/CreateAccount',
                null,
                function (data) {
                    $('.right-panel').html(data);
                }
            )
        })
    }

    // tạo tại khoản phụ
    function createAccount() {
        $('.right-panel').on('submit', '#form-create-account', function (e) {
            e.preventDefault(); // Ngăn chặn việc tải lại trang

            ajaxCall(
                'POST',
                '/Admin/AdminAccount/CreateAccount',
                $(this).serialize(),
                function (data) {
                    if (data.result == true) {
                        success({
                            title: data.mess,
                            text: "",
                        });
                    } else {
                        error({
                            title: data.mess,
                        });
                    }
                }
            )
        });
    }

    // nhấn quyền
    function clickPermission() {
        /*$('.right-panel').on('click', '.item-permission', function (e) {
            var formData = $('#form-create-account').serialize();
            var permission = "CreateRoom";

            ajaxCall(
                'POST',
                '/Admin/AdminAccount/SetPermission',
                formData + '&permission=' + permission,
                function (data) {
                    $('.right-panel').html(data);
                }
            )
        });*/

        $('.right-panel').on('change', '.item-permission-checkbox', function (e) {
            if (this.checked) {
                $(this).attr('name', 'Permissions');
            } else {
                $(this).removeAttr('name');
            }
        });
    }

    // call ajax
    function ajaxCall(type, url, data, successCallback) {
        $.ajax({
            type: type,
            url: url,
            data: data,
            beforeSend: function () {
                $('#loaderBar').show();
            },
            complete: function () {
                $('#loaderBar').hide();
            },
            success: successCallback,
            error: function () {
                alert('Có lỗi xảy ra, vui lòng thử lại.');
            }
        });
    }
})