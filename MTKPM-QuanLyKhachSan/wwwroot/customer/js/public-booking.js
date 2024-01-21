import { toast } from '../../lib/toast/toast.js';

document.addEventListener('DOMContentLoaded', function () {
	main();

	// main
	function main() {
		selectRoom();
		booking();
	}

	// hiển thị select room
	function selectRoom() {
		var selectRoomTypes = document.querySelector('#select-room-type')

		if (selectRoomTypes != null) {
			var roomTypeId = selectRoomTypes.value;

			// hiển thị mặc định
			if (roomTypeId != 0) {
				postRoomPartialView(roomTypeId);
			}

			// hiển thị khi change
			selectRoomTypes.addEventListener('change', function () {
				roomTypeId = selectRoomTypes.value;

				postRoomPartialView(roomTypeId);
			})
		}
	}

	// render select room
	function postRoomPartialView(roomTypeId) {
		$.ajax({
			type: "POST",
			url: "../../PublicRoom/RoomPartialView?roomTypeId=" + roomTypeId,
			success: function (data) {
				$("#render-room-partial-view").html(data);
			},
			error: function () {

			}
		})
	}

	// đặt phòng
	function booking() {
		var btnSubmit = $('#btn-submit');

		if (btnSubmit != null) {
			btnSubmit.click(function () {
				var bookingVM = GetBookingVM();

				$.ajax({
					type: "POST",
					url: "../../PublicRoom/Booking",
					data: bookingVM,
					success: function (data) {
						toast({
							title: "Thành công!",
							message: "Bạn đã đặt phòng thành công.",
							type: "success",
							duration: 3000
						});
					},
					error: function () {

					}
				})
			})
		}
	}

	// lấy dữ liệu từ model
	function GetBookingVM() {
		var bookingVM = {
			Name: $('#name').val(),
			Phone: $('#phone').val(),
			CheckIn: $('#checkin').val(),
			CheckOut: $('#checkout').val(),
			NumAdult: $('#numAdult').val(),
			NumChildren: $('#numChildren').val(),
			RoomId: $('#roomId').val(),
			Note: $('#note').val(),
		};

		return bookingVM;
    }
})