document.addEventListener('DOMContentLoaded', function () {
	main();

	function main() {
		$.ajax({
			type: "GET",
			url: "/Admin/AdminBooking/GetBooking",
			success: function (data) {
				var dataResources = JSON.parse(data.resources);
				var dataEvents = JSON.parse(data.events);

				renderFullCallendar(dataResources, dataEvents);
			},
			error: function () {

            }
		});
	}

	

	// hiển thị giao diện Calendar
	function renderFullCallendar(dataResources, dataEvents) {
		var calendarEl = document.getElementById('calendar');

		var calendar = new FullCalendar.Calendar(calendarEl, {
			schedulerLicenseKey: 'GPL-My-Project-Is-Open-Source',
			now: new Date(),
			aspectRatio: 1.8,
			scrollTime: '00:00',
			editable: true,
			selectTable: true,
			locale: 'vi',
			initialView: 'resourceTimelineMonth',
			initialDate: new Date(),
			resources: dataResources,
			events: dataEvents,
			resourceAreaHeaderContent: 'Phòng',
			resourceAreaWidth: '10%', // Chỉnh độ rộng của khu vực resource
			headerToolbar: {
				left: 'prev,next today',
				center: 'title',
				right: 'btnExportFileExcel btnBooking'
				//right: 'resourceTimelineMonth,resourceTimelineThreeDays,timeGridWeek,dayGridMonth'
			},
			views: {
				resourceTimelineMonth: {
					slotLabelFormat: [
						{ weekday: 'short', day: '2-digit' }, // lower level of text
					],
					initialDate: new Date().toISOString()
				},
			},
			buttonText: {
				today: 'Hôm nay',
			},
			customButtons: {
				btnBooking: {
					text: 'Đặt phòng',
					click: function () {
						booking();
					}
				},
				btnExportFileExcel: {
					text: 'Xuất excel',
					click: function () {
						var title = prompt('Room name');
						if (title) {
							calendar.addResource({ title: title });
						}
					}
				}
			},
			resourceLabelDidMount: function (arg) {
				var resource = arg.resource;

				arg.el.addEventListener('click', function () {
					if (confirm('Are you sure you want to delete ' + resource.title + '?')) {
						resource.remove();
					}
				});
			},
			eventClick: function (info) {
				bookingDetails(info.event.id);
			},
			dateClick: function (info) {
				console.log(info.resource.id)
			},
		});

		switchTabs(calendar);

		calendar.render();
	}

	// đặt phòng
	function booking() {
		$.ajax({
			type: "GET",
			url: "/Admin/AdminBooking/Booking",
			success: function (data) {
				$(".right-panel").html(data);

				addRoomBooking();
			},
			error: function () {

            }
		});
	}

	// chi tiết đặt phòng
	function bookingDetails(bookRoomDetailsId) {
		$.ajax({
			type: "GET",
			url: "/Admin/AdminBooking/BookingDetails?bookRoomDetailsId=" + bookRoomDetailsId,
			success: function (data) {
				$(".right-panel").html(data);

				editBooking();
				addRoomBooking();
			},
			error: function () {

			}
		});
	}

	// chỉnh sửa đặt phòng
	function editBooking() {
		document.getElementById('form-book-room').addEventListener('submit', function (e) {
			e.preventDefault(); // Ngăn chặn việc tải lại trang

			$.ajax({
				type: "POST",
				url: "/Admin/AdminBooking/EditBooking",
				data: $(this).serialize(),
				success: function (data) {
					if (data.result == true) 
						success(data.mess, "", null);
					else {
						error(data.mess);
						editBooking();
					}
				},
				error: function () {

				}
			});
        })
	}

	// thêm phòng
	function addRoomBooking() {
		var roomContainer = document.querySelector('.custom-room-container');

		document.querySelector('.panel-form-add-room').addEventListener('click', function () {
			roomContainer.classList.add('active');
		})

		document.querySelector('.custom-overlay').addEventListener('click', function () {
			roomContainer.classList.remove('active');
		});

		switchTabRoom();
	}

	// chuyển tab room
	function switchTabRoom() {
		var tabActive = document.querySelector(".custom-tab-link.active");
		var navLine = document.querySelector(".custom-tab-container .nav-line");

		LineUpdate(tabActive);

		// Chuyển line
		function LineUpdate(tab) {
			navLine.style.left = tab.offsetLeft + "px";
			navLine.style.width = tab.offsetWidth + "px";
		}

		// xử lý nhấn chuyển tab
		document.querySelectorAll('.custom-tab-link').forEach(function (tab, index) {
			tab.addEventListener('click', function () {
				LineUpdate(this);
				tab.classList.remove("active");
				document.querySelector(".custom-tab-content.active").classList.remove("active");
				document.querySelectorAll(".custom-tab-content")[index].classList.add("active");
			});
		});
	}

	// M: Xử lý chuyển tab
	function switchTabs(calendar) {
		var tabActive = document.querySelector(".nav-item.active");
		var navLine = document.querySelector(".panel-navbar .nav-line");

		LineUpdate(tabActive);

		// xử lý nhấn chuyển tab
		$(".nav-container .nav-item").click(function () {
			document.querySelector(".nav-item.active").classList.remove("active");

			LineUpdate(this);

			this.classList.add("active");
		});

		// Bắt sự kiện click của các item trong navbar
		$(".nav-container li").click(function () {
			// Lấy id của item được click
			var view = $(this).attr("id");

			// Th	ay đổi hiển thị của FullCalendar tùy theo trạng thái mới
			if (view === "month-view") {
				calendar.changeView('resourceTimelineMonth');
			} else if (view === "week-view") {
				calendar.changeView('dayGridWeek');
			} else if (view === "day-view") {
				calendar.changeView('listDay');
			}
		});

		// Chuyển line
		function LineUpdate(tab) {
			navLine.style.left = tab.offsetLeft + "px";
			navLine.style.width = tab.offsetWidth + "px";
		}
	}

	// alert success
	function success(title, text, functionResult) {
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
				functionResult();
			} else {
				location.reload();
			}
		});
	}

	// alert error
	function error(title) {
		Swal.fire({
			title: title,
			icon: "error",
			confirmButtonColor: "#1577BD",
		})
	}
});