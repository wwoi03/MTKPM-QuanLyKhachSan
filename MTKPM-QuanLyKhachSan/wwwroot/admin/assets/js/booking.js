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

	// M: Xử lý chuyển tab
	function switchTabs(calendar) {
		var tabActive = document.querySelector(".nav-item.active");
		var navLine = document.querySelector(".panel-navbar .nav-line");

		// Chuyển line
		function LineUpdate(tab) {
			navLine.style.left = tab.offsetLeft + "px";
			navLine.style.width = tab.offsetWidth + "px";
		}

		LineUpdate(tabActive);

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
			locale: 'vi',
			initialView: 'resourceTimelineMonth',
			initialDate: new Date(),
			resourceAreaHeaderContent: 'Phòng',
			headerToolbar: {
				left: 'prev,next today',
				center: 'title',
				right: 'btnExportFileExcel btnBooking'
				//right: 'resourceTimelineMonth,resourceTimelineThreeDays,timeGridWeek,dayGridMonth'
			},
			buttonText: {
				today: 'Hôm nay',
			},
			resourceAreaWidth: '10%', // Chỉnh độ rộng của khu vực resource
			customButtons: {
				btnBooking: {
					text: 'Đặt phòng',
					click: function () {
						var title = prompt('Room name');
						if (title) {
							calendar.addResource({ title: title });
						}
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
				alert(
					'Title: ' + info.event.title + '\n' +
					'Start: ' + info.event.start + '\n' +
					'End: ' + info.event.end);

				// change the border color just for fun
				info.el.style.borderColor = 'red';
			},
			resources: dataResources,
			events: dataEvents,
		});

		switchTabs(calendar);

		calendar.render();
	}
});

/*
 [
	{ id: '101', title: '101' },
	{ id: '102', title: '102', eventColor: 'green' },
	{ id: '103', title: '103', eventColor: 'orange' },
	{ id: '104', title: '104', eventColor: 'blue' },
	{ id: '105', title: '105' },
	{ id: '106', title: '106', eventColor: 'red' },
	{ id: '107', title: '107' },
	{ id: '108', title: '108' },
	{ id: '109', title: '109' },
	{ id: '110', title: '110' },
	{ id: '111', title: '111' },
	{ id: '112', title: '112' },
	{ id: '113', title: '113' },
	{ id: '114', title: '114' },
	{ id: '115', title: '115' },
	{ id: '116', title: '116' },
	{ id: '117', title: '117' },
	{ id: '118', title: '118' },
	{ id: '119', title: '119' },
	{ id: '120', title: '120' },
	{ id: '121', title: '121' },
	{ id: '122', title: '122' },
	{ id: '123', title: '123' },
	{ id: '124', title: '124' },
	{ id: '125', title: '125' },
	{ id: '126', title: '126' }
],


[
	{ id: '1', resourceId: '102', start: '2024-01-02', end: '2024-01-02', title: 'Đào Công Tuấn' },
	{ id: '2', resourceId: '103', start: '2024-01-02', end: '2024-01-02', title: 'Nguyễn Thành An' },
	{ id: '3', resourceId: '104', start: '2024-01-01', end: '2024-01-03', title: 'Nguyễn Văn Xên' },
	{ id: '4', resourceId: '105', start: '2024-01-02', end: '2024-01-02', title: 'Diệp Minh Quân' },
	{ id: '5', resourceId: '108', start: '2024-01-02', end: '2024-01-04', title: 'Bùi Thanh Tùng' }
]
 
 */