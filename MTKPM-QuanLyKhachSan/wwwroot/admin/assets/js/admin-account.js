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
        viewEditAccount();
    }

    // chức năng
    function feature() {
        createAccount();
        clickPermission();
        clickRole();
        lockAccount();
        unlockAccount();
    }

    // view tạo tại khoản phụ
    function createAccountView() {
        $('#create').on('click', function () {
            ajaxCall(
                'GET',
                '/Admin/AdminAccountProxy/CreateAccount',
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
                '/Admin/AdminAccountProxy/CreateAccount',
                $(this).serialize(),
                function (data) {
                    if (data.result == true) {
                        success({
                            title: data.mess,
                            text: "",
                            funcConfirm: function () {
                                location.reload();
                            },
                            funcCancel: function () {
                                location.reload();
                            },
                        });
                    } else if (data.result == false) {
                        error({
                            title: data.mess,
                        });
                    } else {
                        $('.right-panel').html(data);
                    }
                }
            )
        });
    }

    // view tạo tại khoản phụ
    function viewEditAccount() {
        $('.left-panel').on('click', '.account-edit', function () {
            var employeeId = $(this).data('employee-id');

            ajaxCall(
                'GET',
                '/Admin/AdminAccountProxy/EditAccount',
                { employeeId: employeeId },
                function (data) {
                    $('.right-panel').html(data);
                }
            )
        })
    }

    // nhấn quyền
    function clickPermission() {
        $('.right-panel').on('change', '.item-permission-checkbox', function (e) {
            if (this.checked) {
                $(this).attr('name', 'Permissions');
            } else {
                $(this).removeAttr('name');
            }
        });
    }

    // chọn role
    function clickRole() {
        $('.right-panel').on('click', '.setting-account-item-title', function (e) {
            var container = $(this).closest('.setting-account-item-container');
            var titleElements = container.find('.item-permission-title');
            var childElements = container.find('.item-permission-checkbox');

            // Đánh dấu tất cả các input trong container là checked
            if (titleElements[0].checked) {
                childElements.prop('checked', true).attr('name', 'Permissions');
            }
            else {
                childElements.prop('checked', false).removeAttr('name');
            }
        });
    }

    // khóa tài khoản
    function lockAccount() {
        $('.left-panel').on('click', '.lock-account', function (e) {
            var employeeId = $(this).data('employee-id');
            var username = $(this).data('username');

            confirm({
                title: "Xác nhận khóa tài khoản?",
                text: username,
                funcConfirm: function () {
                    ajaxCall(
                        'POST',
                        '/Admin/AdminAccountProxy/LockAccount',
                        { employeeId: employeeId },
                        function (data) {
                            if (data.result == true) {
                                success({
                                    title: data.mess,
                                    text: username,
                                    funcConfirm: function () {
                                        location.reload();
                                    },
                                    showCancel: false
                                });
                            } else if (data.result == false) {
                                error({
                                    title: data.mess,
                                });
                            } else {
                                $('.right-panel').html(data);
                            }
                        }
                    )
                },
            });
        });
    }

    // khóa tài khoản
    function unlockAccount() {
        $('.left-panel').on('click', '.unlock-account', function (e) {
            var employeeId = $(this).data('employee-id');
            var username = $(this).data('username');

            confirm({
                title: "Xác nhận mở khóa tài khoản?",
                text: username,
                funcConfirm: function () {
                    ajaxCall(
                        'POST',
                        '/Admin/AdminAccountProxy/UnLockAccount',
                        { employeeId: employeeId },
                        function (data) {
                            if (data.result == true) {
                                success({
                                    title: data.mess,
                                    text: username,
                                    funcConfirm: function () {
                                        location.reload();
                                    },
                                    showCancel: false
                                });
                            } else if (data.result == false) {
                                error({
                                    title: data.mess,
                                });
                            } else {
                                $('.right-panel').html(data);
                            }
                        }
                    )
                },
            });
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

/*$('.right-panel').on('click', '.item-permission', function (e) {
    var formData = $('#form-create-account').serialize();
    var permission = "CreateRoom";

    ajaxCall(
        'POST',
        '/Admin/AdminAccountProxy/SetPermission',
        formData + '&permission=' + permission,
        function (data) {
            $('.right-panel').html(data);
        }
    )
});*/