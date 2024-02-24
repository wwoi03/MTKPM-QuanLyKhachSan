main();

function main() {
    closeDropdownMenu();
    openDropdownMenu();
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
