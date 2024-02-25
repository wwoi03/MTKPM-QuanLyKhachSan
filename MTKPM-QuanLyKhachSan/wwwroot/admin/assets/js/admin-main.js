$(document).ready(function () {
    // Datepicker
    jQuery.datetimepicker.setLocale('vi');

    jQuery('.date-input').datetimepicker({
        i18n: {
            vi: {
                months: [
                    'Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4',
                    'Tháng 5', 'Tháng 6', 'Tháng 7', 'Tháng 8',
                    'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12',
                ],
                dayOfWeek: [
                    "CN.", "T2", "T3", "T4",
                    "T5", "T6", "T7.",
                ]
            }
        },
        minDate: 0,
        format: 'd.m.Y H:i'
    });

    main();

    function main() {
        switchPage();
        switchTabs();
        openDropdownMenu();
        closeDropdownMenu();
    }

    function switchPage() {
        $('.sidebar-page-item').each(function () {
            var sidebarPageLink = $(this).find('.sidebar-page-link');
            var sidebarPageItemActive = $('.sidebar-page-item.active');

            if (sidebarPageLink.attr('href') === window.location.pathname) {
                // đóng active hiện tại
                if (sidebarPageItemActive.length) {
                    sidebarPageItemActive.removeClass('active');
                }

                $(this).addClass('active');
            }
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
                });
            });
        }
    }

    // mở DropdownMenu khi bấm icon menu
    function openDropdownMenu() {
        // Sử dụng Event Delegation để gắn sự kiện click cho tất cả các phần tử
        $(document).on('click', '.icon-menu', function () {
            // Hành động bạn muốn thực hiện khi phần tử được click
            var card = $(this).closest('.custom-card')
            var dropdownMenu = card.find('.custom-dropdown-menu');
            var menuActive = $('.custom-dropdown-menu.active');

            // đóng menu hiện tại đang mở
            if (menuActive.length) {
                menuActive.removeClass('active');
            }

            // kiểm tra menu đang bấm có đang mở
            if (dropdownMenu.hasClass('active')) {
                dropdownMenu.removeClass('active');
            } else {
                dropdownMenu.addClass('active');
            }
        });
    }

    // đóng DropdownMenu khi bấm ra ngoài
    function closeDropdownMenu() {
        $(document).click(function (event) {
            // Kiểm tra nếu click không phải trên menu đang mở và không phải trên phần tử kích hoạt menu
            if (!$(event.target).closest('.custom-dropdown-menu').length && !$(event.target).hasClass('icon-menu')) {
                // Đóng tất cả các menu đang mở
                $('.custom-dropdown-menu.active').removeClass('active');
            }
        });
    }
});