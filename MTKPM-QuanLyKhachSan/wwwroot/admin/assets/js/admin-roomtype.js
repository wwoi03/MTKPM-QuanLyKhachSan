$(document).ready(function () {
    main();

    // main
    function main() {
        deleteRoomType()
    }








function deleteRoomType() {

    $('.left-panel').on('click', '.delete-roomtype', function (e) {
        var roomTypeId = $(this).data('roomtype-id');
        confirm({
            title: "Xác nhận xóa loại phòng?",
            funcConfirm: function () {
                ajaxCall(
                    'POST',
                    '/Admin/AdminRoomTye/DeleteRoomType',
                    { roomTypeId: roomTypeId },
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
            },
        });
    });
}


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
            alert('Thể loại phòng này không thể xóa.');
        }
    });
}
})