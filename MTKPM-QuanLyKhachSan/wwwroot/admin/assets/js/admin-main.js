// alert success
function success({ title = "", text = "", funcConfirm = null, funcCancel = null }) {
    Swal.fire({
        title: title,
        text: text,
        icon: "success",
        confirmButtonColor: "#1577BD",
        cancelButtonColor: "#999999",
        showCancelButton: true,
        reverseButtons: true,
    }).then((result) => {
        if (result.isConfirmed) {
            funcConfirm();
        } else {
            funcCancel();
        }
    });
}

// alert error
function error({ title = "" }) {
    Swal.fire({
        title: title,
        icon: "error",
        confirmButtonColor: "#1577BD",
    })
}

// Toast function
function toast({ title = "", message = "", type = "info", duration = 3000 }) {
    const main = document.getElementById("custom-toast");
    if (main) {
        const toast = document.createElement("div");

        // Auto remove toast
        const autoRemoveId = setTimeout(function () {
            main.removeChild(toast);
        }, duration + 1000);

        // Remove toast when clicked
        toast.onclick = function (e) {
            if (e.target.closest(".custom-toast__close")) {
                main.removeChild(toast);
                clearTimeout(autoRemoveId);
            }
        };

        const icons = {
            success: "fas fa-check-circle",
            info: "fas fa-info-circle",
            warning: "fas fa-exclamation-circle",
            error: "fas fa-exclamation-circle"
        };
        const icon = icons[type];
        const delay = (duration / 1000).toFixed(2);

        toast.classList.add("custom-toast", `custom-toast--${type}`);
        toast.style.animation = `slideInLeft ease .3s, fadeOut linear 1s ${delay}s forwards`;

        toast.innerHTML = `
                    <div class="custom-toast__icon">
                        <i class="${icon}"></i>
                    </div>
                    <div class="custom-toast__body">
                        <h3 class="custom-toast__title">${title}</h3>
                        <p class="custom-toast__msg">${message}</p>
                    </div>
                    <div class="custom-toast__close">
                        <i class="fas fa-times"></i>
                    </div>
                `;
        main.appendChild(toast);
    }
}

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

            

            // kiểm tra menu đang bấm có đang mở
            if (dropdownMenu.hasClass('active')) {
                dropdownMenu.removeClass('active');
            } else {
                // đóng menu hiện tại đang mở
                if (menuActive.length) {
                    menuActive.removeClass('active');
                }

                // mở menu đang bấm
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