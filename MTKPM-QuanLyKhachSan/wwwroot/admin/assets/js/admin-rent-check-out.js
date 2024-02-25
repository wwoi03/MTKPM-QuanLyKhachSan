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
    }

    // chức năng
    function feature() {
        changeView();
        cleanRoom();
        requestCleanRoom();
        changeRoom();
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
            var roomId = $(this).data('room-id');

            ajaxCall(
                'GET',
                '/Admin/AdminRentCheckOut/ChangeRoom',
                { roomId: roomId },
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