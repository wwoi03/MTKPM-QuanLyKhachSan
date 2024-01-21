document.addEventListener('DOMContentLoaded', function () {
	main();

	// main
	function main() {
		selectRoom();
	}

	// hiển thị select room
	function selectRoom() {
		var selectRoomTypes = document.querySelector('#select-room-type')

		if (selectRoomTypes != null) {
			var roomTypeId = selectRoomTypes.value;

			if (roomTypeId != 0) {
				postRoomPartialView(roomTypeId);
			}

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
})