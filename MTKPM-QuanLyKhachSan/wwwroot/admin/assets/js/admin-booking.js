$(document).ready(function () {
	main();

	// main
	function main() {
		view();
		feature();
	}

	// view
	function view() {
		viewFullCanlandar();
		viewChooseRoom();
	}

	// chức năng
	function feature() {
		booking();
		editBooking();
		addRoomBooking();
	}

	// xử lý chuyển view
	function changeView(calendar) {
		// bắt sự kiện click trên mỗi tab
		$('.nav-container .nav-item').on('click', function () {
			// Lấy id của item được click
			var view = $(this).attr('id');

			// Thay đổi hiển thị của FullCalendar tùy theo trạng thái mới
			switch (view) {
				case 'month-view':
					calendar.changeView('resourceTimelineMonth');
					break;
				case 'week-view':
					calendar.changeView('dayGridWeek');
					break;
				case 'day-view':
					calendar.changeView('listDay');
					break;
			}
		});
	}

	// view 
	function viewFullCanlandar() {
		ajaxCall(
			'GET',
			'/Admin/AdminBookingProxy/GetBooking',
			null,
			function (data) {
				var dataResources = JSON.parse(data.resources);
				var dataEvents = JSON.parse(data.events);

				renderFullCallendar(dataResources, dataEvents);
			}
		)
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
						viewBooking();
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
				viewBookingDetails(info.event.id);
			},
			dateClick: function (info) {
				console.log(info.resource.id)
			},
		});

		changeView(calendar);

		calendar.render();
	}

	// view đặt phòng
	function viewBooking() {
		ajaxCall(
			'GET',
			'/Admin/AdminBookingProxy/Booking',
			null,
			function (data) {
				$(".right-panel").html(data);
			}
		)
	}

	// đặt phòng
	function booking() {
		$('.right-panel').on('submit', '#form-book-room', function (e) {
			e.preventDefault(); // Ngăn chặn việc tải lại trang

			ajaxCall(
				'POST',
				'/Admin/AdminBookingProxy/Booking',
				$(this).serialize(),
				function (data) {
					if (data.result == true) {
						success({
							title: data.mess,
							funcConfirm: function () {
								location.reload();
							},
							showCancel: false
						});
					} else {
						error({
							title: data.mess,
						});
					}
				}
			)
		})
	}

	// chi tiết đặt phòng
	function viewBookingDetails(bookRoomDetailsId) {
		ajaxCall(
			'GET',
			'/Admin/AdminBookingProxy/BookingDetails',
			{ bookRoomDetailsId : bookRoomDetailsId },
			function (data) {
				$(".right-panel").html(data);
			}
		)
	}

	// chỉnh sửa đặt phòng
	function editBooking() {
		$('.right-panel').on('submit', '#form-book-room', function (e) {
			e.preventDefault(); // Ngăn chặn việc tải lại trang

			ajaxCall(
				'POST',
				'/Admin/AdminBookingProxy/EditBooking',
				$(this).serialize(),
				function (data) {
					if (data.result == true) {
						success({
							title: data.mess,
							funcConfirm: function () {
								location.reload();
							},
							showCancel: false
						});
					} else {
						error({
							title: data.mess,
						});
					}
				}
			)
		})
	}

	// view chọn phòng
	function viewChooseRoom() {
		$('.right-panel').on('click', '.panel-form-add-room', function (e) {
			ajaxCall(
				'GET',
				'/Admin/AdminBookingProxy/ChooseRoom',
				null,
				function (data) {
					$('#choose-room').html(data);

					var roomContainer = document.querySelector('.custom-room-container');

					roomContainer.classList.add('active');

					document.querySelector('.custom-overlay').addEventListener('click', function () {
						roomContainer.classList.remove('active');
					});

					switchTabRoom();
				}
			)
		})
	}

	// chọn phòng
	function addRoomBooking() {
		$('#choose-room').on('click', '.panel-form-roomtype-render-item', function (e) {
			// Thêm phòng mới vào danh sách
			var newRoom =
				`<div class="col-2-4 panel-form-roomtype-render-item choose">
					${$(this).text()}
					<i class="fa-solid fa-circle-xmark"></i>
					<input type="hidden" name="RoomIds" value="${$(this).data('room-id')}" />
				</div>`;

			$('.panel-form-roomtype-render-list.show-room').append(newRoom);

			// Xóa phần tử hiện tại mà người dùng đã click vào
			$(this).remove();
		})
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