$(document).ready(function () {
    main();

    function main() {
        changeView();
        roomWait();
    }

    // xử lý chuyển view
    function changeView() {
        // bắt sự kiện click trên mỗi tab
        $('.nav-container .nav-item').on('click', function () {
            // Lấy id của item được click
            var view = $(this).attr("id");

            // Thay đổi hiển thị của FullCalendar tùy theo trạng thái mới
            if (view === "room-wait") {
                roomWait();
            } else if (view === "room-rent") {
                roomRent();
            } else if (view === "room-clean") {
                roomClean();
            } else if (view === "room-history") {
                roomHistory();
            }
        });
    }

    // render phòng chờ
    function roomWait() {
        $.ajax({
            type: "GET",
            url: "/Admin/AdminRentCheckOut/RoomWait",
            beforeSend: function () {
                // Hành động trước khi gửi yêu cầu, ví dụ: hiển thị hình ảnh loading
                $('#loaderBar').show();
            },
            complete: function () {
                // Hành động sau khi yêu cầu hoàn thành, ví dụ: ẩn hình ảnh loading
                $('#loaderBar').hide();
            },
            success: function (data) {
                $('#section-left-panel').html(data);

                requestCleanRoom();
            },
            error: function () {

            }
        });
    }

    // render phòng đã đặt
    function roomRent() {
        $.ajax({
            type: "GET",
            url: "/Admin/AdminRentCheckOut/RoomRent",
            beforeSend: function () {
                // Hành động trước khi gửi yêu cầu, ví dụ: hiển thị hình ảnh loading
                $('#loaderBar').show();
            },
            complete: function () {
                // Hành động sau khi yêu cầu hoàn thành, ví dụ: ẩn hình ảnh loading
                $('#loaderBar').hide();
            },
            success: function (data) {
                $('#section-left-panel').html(data);

                requestCleanRoom();
                changeRoom();
            },
            error: function () {

            }
        });
    }

    // render phòng đã đặt
    function roomClean() {
        $.ajax({
            type: "GET",
            url: "/Admin/AdminRentCheckOut/RoomClean",
            beforeSend: function () {
                // Hành động trước khi gửi yêu cầu, ví dụ: hiển thị hình ảnh loading
                $('#loaderBar').show();
            },
            complete: function () {
                // Hành động sau khi yêu cầu hoàn thành, ví dụ: ẩn hình ảnh loading
                $('#loaderBar').hide();
            },
            success: function (data) {
                $('#section-left-panel').html(data);

                cleanRoom();
            },
            error: function () {

            }
        });
    }

    // render phòng đã đặt
    function roomHistory() {
        $.ajax({
            type: "GET",
            url: "/Admin/AdminRentCheckOut/RoomHistory",
            beforeSend: function () {
                // Hành động trước khi gửi yêu cầu, ví dụ: hiển thị hình ảnh loading
                $('#loaderBar').show();
            },
            complete: function () {
                // Hành động sau khi yêu cầu hoàn thành, ví dụ: ẩn hình ảnh loading
                $('#loaderBar').hide();
            },
            success: function (data) {
                $('#section-left-panel').html(data);
            },
            error: function () {

            }
        });
    }

    // dọn phòng
    function cleanRoom() {
        $('.btn-clean-room').on('click', function () {
            // lấy roomId
            var roomId = $(this).attr('data-roomId');

            $.ajax({
                type: "POST",
                url: "/Admin/AdminRentCheckOut/CleanRoom",
                data: { roomId: roomId },
                beforeSend: function () {
                    $('#loaderBar').show();
                },
                complete: function () {
                    $('#loaderBar').hide();
                },
                success: function (data) {
                    $('#section-left-panel').html(data);
                },
                error: function () {

                }
            });
        })
    }

    // yêu cầu dọn phòng
    function requestCleanRoom() {
        $('.btn-request-clean-room').on('click', function () {
            // lấy roomId
            var roomId = $(this).attr('data-roomId');

            $.ajax({
                type: "POST",
                url: "/Admin/AdminRentCheckOut/RequestCleanRoom",
                data: { roomId: roomId },
                beforeSend: function () {
                    $('#loaderBar').show();
                },
                complete: function () {
                    $('#loaderBar').hide();
                },
                success: function (data) {
                    $('#section-left-panel').html(data);
                },
                error: function () {

                }
            });
        })
    }

    // đổi phòng
    function changeRoom() {
        $('.btn-change-room').on('click', function () {
            // lấy roomId
            var roomId = $(this).attr('data-roomId');

            $.ajax({
                type: "GET",
                url: "/Admin/AdminRentCheckOut/ChangeRoom",
                data: { roomId: roomId },
                beforeSend: function () {
                    $('#loaderBar').show();
                },
                complete: function () {
                    $('#loaderBar').hide();
                },
                success: function (data) {
                    $('.right-panel').html(data);

                    postChangeRoom1();
                    postChangeRoom2();
                },
                error: function () {

                }
            });
        });

        function postChangeRoom1() {
            $('.btn-change-room1').on('click', function () {
                // lấy roomId
                var roomIdOld = $(this).attr('data-roomIdOld');
                var roomIdNew = $(this).attr('data-roomIdNew');

                $.ajax({
                    type: "POST",
                    url: "/Admin/AdminRentCheckOut/ChangeRoom",
                    data: { roomIdOld: roomIdOld, roomIdNew: roomIdNew },
                    beforeSend: function () {
                        $('#loaderBar').show();
                    },
                    complete: function () {
                        $('#loaderBar').hide();
                    },
                    success: function (data) {
                        $('.right-panel').html("");

                        $('#section-left-panel').html(data);
                    },
                    error: function () {

                    }
                });
            });
        }

        function postChangeRoom2() {
            $('.btn-change-room2').on('click', function () {
                // lấy roomId
                var roomIdOld = $(this).attr('data-roomIdOld');
                var roomIdNew = $(this).attr('data-roomIdNew');

                $.ajax({
                    type: "POST",
                    url: "/Admin/AdminRentCheckOut/ChangeRoom",
                    data: { roomIdOld: roomIdOld, roomIdNew: roomIdNew, isCleanRoom: true },
                    beforeSend: function () {
                        $('#loaderBar').show();
                    },
                    complete: function () {
                        $('#loaderBar').hide();
                    },
                    success: function (data) {
                        $('.right-panel').html("");

                        $('#section-left-panel').html(data);
                    },
                    error: function () {

                    }
                });
            });
        }
    }
});