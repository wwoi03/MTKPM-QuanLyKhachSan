(function ($) {
    "use strict";

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

    switchTabs();

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
})(jQuery);