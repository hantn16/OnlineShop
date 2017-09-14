var product = {
    Init: function () {
        product.RegisterEvents();

    },
    RegisterEvents: function () {
        $('.btn-images').off('click').on('click', function (e) {
            e.preventDefault();
            $('#ImageManager').modal('show');
            $('#hidProductID').val($(this).data('id'));
            product.LoadImages();
        });
        //Gán sự kiện cho nút chọn ảnh
        $('#btnChooseImage').off('click').on('click', function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (url) {
                $('#ImageList').append('<div style = "float:left"><img src="' + url + '" width="100" /><a href="#" class="btnDelImage"><i class = "fa fa-times"></i></a></div>');
                $('.btnDelImage').off('click').on('click', function (e) {
                    e.preventDefault();
                    $(this).parent().remove();
                });
            };
            finder.popup();
        });
        //Gán sự kiện cho nút lưu ảnh
        $('#btnSaveImage').off('click').on('click', function () {
            var images = [];
            var id = $('#hidProductID').val();
            $.each($('#ImageList img'), function (i, item) {
                images.push($(item).prop('src'));
            });
            $.ajax({
                url: '/Admin/Products/SaveImages',
                type: 'POST',
                data: {
                    id: id,
                    images: JSON.stringify(images)
                },
                datatype: 'json',
                success: function (response) {
                    if (response.status) {
                        $('#ImageManager').modal('hide');
                        $('#ImageList').html('');
                        alert("Lưu thành công");
                    }
                }
            });
        });
    },
    //Hàm load ảnh đã có nên để edit
    LoadImages: function () {
        $.ajax({
            url: '/Admin/Products/LoadImages',
            type: 'GET',
            data: {
                id: $('#hidProductID').val()
            },
            datatype: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    $.each(data, function (i, item) {
                        html += '<div style = "float:left"><img src="' + item + '" width="100" /><a href="#" class="btnDelImage"><i class = "fa fa-times"></i></a></div>';
                    });
                    $('#ImageList').html(html);
                    //Gán lại sự kiện delete ảnh
                    $('.btnDelImage').off('click').on('click', function (e) {
                        e.preventDefault();
                        $(this).parent().remove();
                    });
                    //Chạy lại hàm registerEvents
                    product.RegisterEvents();
                }

            }
        });
    }
};
product.Init();
