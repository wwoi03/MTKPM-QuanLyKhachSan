document.addEventListener('DOMContentLoaded', function () {
    main();

    function main() {
        changeView();
        roomWait();
    }

    // xử lý chuyển view
    function changeView() {
        var navItem = document.querySelectorAll(".nav-container .nav-item");

        // bắt sự kiện click trên mỗi tab
        navItem.forEach((item, index) => {
            item.addEventListener('click', function () {
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
        var btnCleanRooms = document.querySelectorAll('.btn-clean-room');

        btnCleanRooms.forEach((item, index) => {
            item.addEventListener('click', function () {
                // lấy roomId
                var roomId = item.getAttribute('data-roomId');

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

                        cleanRoom();
                    },
                    error: function () {

                    }
                });
            })
        });
    }

    // yêu cầu dọn phòng
    function requestCleanRoom() {
        var btnRequestCleanRooms = document.querySelectorAll('.btn-request-clean-room');

        btnRequestCleanRooms.forEach((item, index) => {
            item.addEventListener('click', function () {
                // lấy roomId
                var roomId = item.getAttribute('data-roomId');

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

                        requestCleanRoom();
                    },
                    error: function () {

                    }
                });
            })
        });
    }
});