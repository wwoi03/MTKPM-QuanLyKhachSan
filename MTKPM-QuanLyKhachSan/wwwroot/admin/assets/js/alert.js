
// JavaScript code to display an alert
function alert() {
	$.ajax({
		type: "GET",
		url: "/Admin/ProxyBooking/Alert",
		success: function (data) {
			if (data !== "0")
				alert(data)
		},
		error: function () {

		}
	});
}
