document.addEventListener('DOMContentLoaded', function () {
    main();

    function main() {
        roomWait();
        switchTabs();
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

                closeDropdownMenu();
                openDropdownMenu();
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

                closeDropdownMenu();
                openDropdownMenu();
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

                closeDropdownMenu();
                openDropdownMenu();
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

                closeDropdownMenu();
                openDropdownMenu();
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
                    type: "GET",
                    url: "/Admin/AdminRentCheckOut/CleanRoom",
                    data: { roomId: roomId },
                    beforeSend: function () {
                        $('#loaderBar').show();
                    },
                    complete: function () {
                        $('#loaderBar').hide();
                    },
                    success: function (data) {
                        if (data) {
                            roomClean();
                        }
                    },
                    error: function () {

                    }
                });
            })
        });
    }

    // M: Xử lý chuyển tab
    function switchTabs() {
        // Xử animation chuyển tab
        var tabActive = document.querySelector(".nav-item.active");
        var navLine = document.querySelector(".panel-navbar .nav-line");
        var navItem = document.querySelectorAll(".nav-container .nav-item");

        if (tabActive != null) {
            // Cập nhật lại đường line khi chuyển tab
            function LineUpdate(tab) {
                navLine.style.left = tab.offsetLeft + "px";
                navLine.style.width = tab.offsetWidth + "px";
            }

            // line cho tab đầu tiên
            LineUpdate(tabActive);

            // bắt sự kiện click trên mỗi tab
            navItem.forEach((item, index) => {
                item.addEventListener('click', function () {
                    document.querySelector(".nav-item.active").classList.remove("active");
                    LineUpdate(this);
                    this.classList.add("active");

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
    }

    // đóng DropdownMenu khi bấm ra ngoài
    function closeDropdownMenu() {
        // Lấy danh sách tất cả các card trong tài liệu
        const cards = document.querySelectorAll(".custom-card");

        // Lắng nghe sự kiện click trên toàn bộ tài liệu
        document.addEventListener('click', function (event) {
            // Kiểm tra xem sự kiện click có xảy ra bên ngoài dropdown-menu không
            cards.forEach((card, index) => {
                const dropdownMenu = card.querySelector(".custom-dropdown-menu");
                const iconMenu = card.querySelector(".icon-menu");

                if (
                    !event.target.classList.contains('icon-menu') && // Không phải icon-menu
                    !dropdownMenu.contains(event.target) && // Không nằm trong dropdown-menu
                    dropdownMenu.classList.contains('active') // Dropdown-menu đang mở
                ) {
                    dropdownMenu.classList.remove('active'); // Đóng dropdown-menu
                }
            });
        });
    }

    // mở DropdownMenu khi bấm icon menu
    function openDropdownMenu() {
        // Lấy danh sách tất cả các card trong tài liệu
        const cards = document.querySelectorAll(".custom-card");

        // Lặp qua từng card để thêm sự kiện khi icon-menu được bấm
        cards.forEach((card, index) => {
            const iconMenu = card.querySelector(".icon-menu");
            const dropdownMenu = card.querySelector(".custom-dropdown-menu");

            // Thêm sự kiện click cho icon-menu
            iconMenu.addEventListener('click', function (event) {
                // Xác định trạng thái hiện tại của dropdown-menu
                const isActive = dropdownMenu.classList.contains('active');

                // Đóng tất cả các dropdown-menu khác nếu chúng đang mở
                cards.forEach((otherCard, otherIndex) => {
                    const otherDropdownMenu = otherCard.querySelector(".custom-dropdown-menu");
                    if (otherIndex !== index && otherDropdownMenu.classList.contains('active')) {
                        otherDropdownMenu.classList.remove('active');
                    }
                });

                // Nếu dropdown-menu đang mở, đóng nó; ngược lại, mở nó
                if (isActive) {
                    dropdownMenu.classList.remove('active');
                } else {
                    dropdownMenu.classList.add('active');
                }
            });
        });
    }
});