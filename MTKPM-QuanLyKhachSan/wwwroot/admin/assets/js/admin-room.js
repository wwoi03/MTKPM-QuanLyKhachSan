$(document).ready(function () {
    main();

    // main
    function main() {
        deleteRoom()
        detailsRoomView()
        editRoomView()
        editRoom()
    }
    function deleteRoom() {
        
    $('.left-panel').on('click', '.delete-room', function (e){
        var roomId = $(this).data('room-id');
        confirm({
            title: "Xác nhận xóa phòng?",
            funcConfirm: function () {
                ajaxCall(
                    'POST',
                    '/Admin/AdminRoom/DeleteRoom',
                    { roomId: roomId },
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

    function detailsRoomView() {  

        
        $('.left-panel').on('click', '.details-room', function (e) {
            var roomId = $(this).data('room-id');
           
             ajaxCall(
                'GET',
                '/Admin/AdminRoom/DetailsRoom',
                { roomId : roomId},
                 function (data) {
                    
                    $('.right-panel').html(data);
                }
            )
            
        });
           
           
    }

    function detailsRoom() {
        $('.right-panel').on('submit', '#form-details-room', function (e) {
            e.preventDefault(); // Ngăn chặn việc tải lại trang

            ajaxCall(
                'POST',
                '/Admin/AdminRoom/DetailsRoom',
                $(this).serialize(),
                function (data) {
                    if (data.result == true) {
                        success({
                            title: data.mess,
                            text: "",
                            funcConfirm: function () {
                                location.reload();
                            },
                            funcCancel: function () {
                                location.reload();
                            },
                        });
                    } else {
                        error({
                            title: data.mess,
                        });
                    }
                }
            )
        });
    }

    function editRoomView() {


        $('.left-panel').on('click', '.edit-room', function (e) {
            var roomId = $(this).data('room-id');
           
            ajaxCall(
                'GET',
                '/Admin/AdminRoom/EditRoom',
                { roomId: roomId },
                function (data) {
                   
                    $('.right-panel').html(data);
                }
            )
            
        });
    }
    function editRoom() {
        $('.right-panel').on('submit', '#form-edit-room', function (e) {
            e.preventDefault(); // Ngăn chặn việc tải lại trang
            console.log("ddd")
            ajaxCall(
                'POST',
                '/Admin/AdminRoom/EditRoom',
                $(this).serialize(),             
                function (data) {
                    console.log(data.result);
                    if (data.result == true) {
                        success({
                            title: data.mess,
                            text: "",
                            funcConfirm: function () {
                                location.reload();
                            },
                            funcCancel: function () {
                                location.reload();
                            },
                        });
                    } else {
                        error({
                            title: data.mess,
                        });
                    }
                }
            )
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
            alert('Phòng đang được đặt.');
        }
    });
}
})
