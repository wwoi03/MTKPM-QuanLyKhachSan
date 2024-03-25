$(document).ready(function () {
    main();

    // main
    function main() {
        view();
        feature();
    }

    // view
    function view() {
        viewRoomWait();
        viewRoomChange();
        viewOrderMenu();
        viewEditRoomRent();
    }

    // chức năng
    function feature() {
        changeView();
        cleanRoom();
        requestCleanRoom();
        changeRoom();
        searchMenu();
        editRoomRent();
        orderMenu();
    }

    // xử lý chuyển view
    function changeView() {
        // bắt sự kiện click trên mỗi tab
        $('.nav-container .nav-item').on('click', function () {
            // Lấy id của item được click
            var view = $(this).attr('id');

            // Thay đổi hiển thị của FullCalendar tùy theo trạng thái mới
            switch (view) {
                case 'room-wait':
                    viewRoomWait();
                    break;
                case 'room-rent':
                    viewRoomRent();
                    break;
                case 'room-clean':
                    viewRoomClean();
                    break;
                case 'room-history':
                    viewRoomHistory();
                    break;
            }
        });
    }

    // render phòng chờ
    function viewRoomWait() {
        ajaxCall(
            'GET',
            '/Admin/AdminRentCheckOut/RoomWait',
            null,
            function (data) {
                $('#section-left-panel').html(data);
            }
        )
    }

    // render phòng đã đặt
    function viewRoomRent() {
        ajaxCall(
            'GET',
            '/Admin/AdminRentCheckOut/RoomRent',
            null,
            function (data) {
                $('#section-left-panel').html(data);
            }
        )
    }

    // render phòng cần dọn
    function viewRoomClean() {
        ajaxCall(
            'GET',
            '/Admin/AdminRentCheckOut/RoomClean',
            null,
            function (data) {
                $('#section-left-panel').html(data);
            }
        )
    }

    // render lịch sử phòng
    function viewRoomHistory() {
        ajaxCall(
            'GET',
            '/Admin/AdminRentCheckOut/RoomHistory',
            null,
            function (data) {
                $('#section-left-panel').html(data);
            }
        )
    }

    // view đổi phòng
    function viewRoomChange() {
        $('#section-left-panel').on('click', '.btn-change-room-view', function () {
            // lấy roomId
            var id = $(this).data('bookroom-details-id');

            ajaxCall(
                'GET',
                '/Admin/AdminRentCheckOut/ChangeRoom',
                { bookRoomDetailsId: id },
                function (data) {
                    $('.right-panel').html(data);
                }
            )
        });
    }

    // dọn phòng
    function cleanRoom() {
        $('#section-left-panel').on('click', '.btn-clean-room', null, function () {
            // lấy roomId
            var roomId = $(this).data('room-id');

            ajaxCall(
                'POST',
                '/Admin/AdminRentCheckOut/CleanRoom',
                { roomId: roomId },
                function (data) {
                    $('#section-left-panel').html(data);
                }
            )
        })
    }

    // yêu cầu dọn phòng
    function requestCleanRoom() {
        $('#section-left-panel').on('click', '.btn-request-clean-room', function () {
            // lấy roomId
            var roomId = $(this).data('room-id');

            ajaxCall(
                'POST',
                '/Admin/AdminRentCheckOut/RequestCleanRoom',
                { roomId: roomId },
                function (data) {
                    $('#section-left-panel').html(data);
                }
            )
        })
    }

    // đổi phòng
    function changeRoom() {
        $('.right-panel').on('click', '.btn-change-room', function () {
            // lấy roomId
            var roomIdOld = $(this).data('room-id-old');
            var roomIdNew = $(this).data('room-id-new');
            var isCleanRoom = $(this).data('is-clean-room');

            ajaxCall(
                'POST',
                '/Admin/AdminRentCheckOut/ChangeRoom',
                { roomIdOld: roomIdOld, roomIdNew: roomIdNew, isCleanRoom: isCleanRoom },
                function (data) {
                    $('.right-panel').html('');
                    $('#section-left-panel').html(data);
                }
            )
        });
    }

    // view thêm menu
    function viewOrderMenu() {
        $('#section-left-panel').on('click', '.btn-order-menu-view', function () {
            var id = $(this).data('bookroom-details-id');

            ajaxCall(
                'GET',
                '/Admin/AdminRentCheckOut/OrderMenu',
                { bookRoomDetailsId: id },
                function (data) {
                    $('.right-panel').html(data);
                }
            )
        });
    }

    // tìm kiếm  menu
    function searchMenu() {
        $('.right-panel').on('input', '#input-search-menu', function () {
            var value = $(this).val().toLowerCase();

            $('.custom-menu-list-item').each(function () {
                // Lấy nội dung của mục menu và chuyển đổi thành chữ thường
                var itemName = $(this).find('.menu-item-name').text().toLowerCase(); 

                // Kiểm tra xem giá trị tìm kiếm có tồn tại trong nội dung của mục menu không
                if (itemName.indexOf(value) === -1) {
                    $(this).removeClass('active'); // Nếu không tìm thấy, loại bỏ lớp 'active'
                } else {
                    $(this).addClass('active'); // Nếu tìm thấy, thêm lớp 'active'
                }
            });
        });
    }

    // thêm menu
    function orderMenu() {
        var orders = [];

        addMenu();
        deleteMenu();

        // thêm số lượng
        function addMenu() {
            $('.right-panel').on('click', '.btn-add-menu', function () {
                var parent = $(this).closest('.custom-menu-list-content');
                var serviceId = parent.data('service-id');
                var viewQuantity = $(this).closest('li').find('.custom-menu-list-quantity');

                var item = orders.find(item => item.ServiceId == serviceId);

                if (item) {
                    item.Quantity++;
                } else {
                    orders.push({
                        ServiceId: serviceId,
                        Quantity: 1
                    })
                }

                viewQuantity.html(item == undefined ? 1 : item.Quantity);
            });
        }

        // giảm số lượng
        function deleteMenu() {
            $('.right-panel').on('click', '.btn-delete-menu', function () {
                var parent = $(this).closest('.custom-menu-list-content');
                var serviceId = parent.data('service-id');
                var viewQuantity = $(this).closest('li').find('.custom-menu-list-quantity');

                var item = orders.find(item => item.ServiceId == serviceId);

                if (item) {
                    if (item.Quantity > 0) {
                        item.Quantity--;
                    };
                }

                viewQuantity.html(item == undefined ? 0 : item.Quantity);
            });
        }

        $('.right-panel').on('submit', '#form-order-menu', function (e) {
            e.preventDefault(); // Ngăn chặn việc tải lại trang
            var bookRoomDetailsId = $(this).data('bookroom-details-id');

            ajaxCall(
                'POST',
                '/Admin/AdminRentCheckOut/OrderMenu',
                { bookRoomDetailsId: bookRoomDetailsId, orders: orders},
                function (data) {
                    $('.right-panel').html("");

                    if (data.result == true) {
                        success({
                            title: data.mess,
                            showCancel: false
                        })

                        viewRoomRent();
                    } else {
                        error({
                            title: data.mess
                        })
                    }
                }
            )
        });
    }

    // view chỉnh sửa phòng thuê
    function viewEditRoomRent() {
        $('#section-left-panel').on('click', '.btn-edit-room-rent-view', function () {
            var bookRoomDetailsId = $(this).data('bookroom-details-id');

            ajaxCall(
                'GET',
                '/Admin/AdminRentCheckOut/EditBookRoomDetails',
                { bookRoomDetailsId: bookRoomDetailsId },
                function (data) {
                    $('.right-panel').html(data);
                }
            )
        });
    }

    // chỉnh sửa phòng
    function editRoomRent() {
        $('.right-panel').on('submit', '#form-book-room-details', function (e) {
            e.preventDefault(); // Ngăn chặn việc tải lại trang

            ajaxCall(
                'POST',
                '/Admin/AdminRentCheckOut/EditBookRoomDetails',
                $(this).serialize(),
                function (data) {
                    if (data.result == true) {
                        success({
                            title: "Thành công",
                            text: data.mess,
                        })

                        viewRoomRent();
                    } else {
                        error({
                            title: data.mess
                        })
                    }
                }
            )
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
});