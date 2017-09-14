class DocSoThanhChu {
    constructor() { }
    static dochangchuc(so, daydu) {
        let chuoi = "";
        let chuc = Math.floor(so / 10);
        let donvi = so % 10;
        let mangso = ['không', 'một', 'hai', 'ba', 'bốn', 'năm', 'sáu', 'bảy', 'tám', 'chín'];
        if (chuc > 1) {
            chuoi = " " + mangso[chuc] + " mươi";
            if (donvi === 1) {
                chuoi += " mốt";
            }
        } else if (chuc === 1) {
            chuoi = " mười";
            if (donvi === 1) {
                chuoi += " một";
            }
        } else if (daydu && donvi > 0) {
            chuoi = " lẻ";
        }
        if (donvi === 5 && chuc >= 1) {
            chuoi += " lăm";
        } else if (donvi > 1 || (donvi === 1 && chuc=== 0)) {
            chuoi += " " + mangso[donvi];
        }
        return chuoi;
    }
    static docblock(so, daydu) {
        let mangso = ['không', 'một', 'hai', 'ba', 'bốn', 'năm', 'sáu', 'bảy', 'tám', 'chín'];
        let chuoi = "";
        let tram = Math.floor(so / 100);
        so = so % 100;
        if (daydu || tram > 0) {
            chuoi = " " + mangso[tram] + " trăm";
            chuoi += this.dochangchuc(so, true);
        } else {
            chuoi = this.dochangchuc(so, false);
        }
        return chuoi;
    }
    static dochangtrieu(so, daydu) {
        let chuoi = "";
        let trieu = Math.floor(so / 1000000);
        so = so % 1000000;
        if (trieu > 0) {
            chuoi = this.docblock(trieu, daydu) + " triệu";
            daydu = true;
        }
        let nghin = Math.floor(so / 1000);
        so = so % 1000;
        if (nghin > 0) {
            chuoi += this.docblock(nghin, daydu) + " nghìn";
            daydu = true;
        }
        if (so > 0) {
            chuoi += this.docblock(so, daydu);
        }
        return chuoi;
    }
    static docso(so) {
        let mangso = ['không', 'một', 'hai', 'ba', 'bốn', 'năm', 'sáu', 'bảy', 'tám', 'chín'];
        if (so === 0) return mangso[0];
        let chuoi = "", hauto = "";
        do {
            let ty;
            ty = so % 1000000000;
            so = Math.floor(so / 1000000000);
            if (so > 0) {
                chuoi = this.dochangtrieu(ty, true) + hauto + chuoi;
            } else {
                chuoi = this.dochangtrieu(ty, false) + hauto + chuoi;
            }
            hauto = " tỷ";
        } while (so > 0);
        return chuoi;
    }
}
$(document).ready(function () {
    var idtablename = '#userindex,#ProductCategoryTable,#CategoryTable,#ContentTable,#DataTable';
    $(idtablename).DataTable({
        "lengthMenu": [5, 10, 25, 50, 75, 100]
    });

});
var obj = {
    init: function () {
        obj.registerEvents();
    },
    registerEvents: function () {


        $('*[class*="avatarToHover"]').hover(function () {
            let clArr = this.className.split(' ');
            console.log(clArr);
            let clName;
            for (let i = clArr.length - 1; i >= 0; i--) {
                if (clArr[i].startsWith('avatarToHover')) {
                    clName = clArr[i]; break;
                }
            }
            let pos = 'auto';
            let index = clName.indexOf('-');
            if (index !== -1) {
                pos = clName.substring(index + 1);
            }
            $(this).tooltip({
                animation: 'fade',
                placement: 'auto',
                container: 'body',
                html: true
            });
        });

        $('.add-cart').click(function () {
            var prodid = $(this).data('productid');
            var prodname = $(this).data('productName');
            $.ajax({
                url: "/Carts/AddItem",
                data: { productID: prodid, quantity: 1 },
                dataType: 'json',
                type: "POST",
                success: function (res) {
                    if (res.status) {
                        var sumcount = 0;
                        var total = 0;
                        var stringtext = '';
                        var listCartItem = JSON.parse(res.data);
                        listCartItem.forEach(c => { sumcount += c.Quantity; total += c.Quantity * c.Product.Price; });
                        listCartItem.forEach(function (item) {
                            let text = '<tr id="Row_' + item.Product.ID + '">' +
                                '<td>' + item.Product.Code + '</td>' +
                                '<td><img src="' + item.Product.Image + '" style="width:100px" /></td>' +
                                '<td>' + item.Product.Name + '</td>' +
                                '<td id = "Price_' + item.Product.ID + '" value = "' + item.Product.Price + '">' + (item.Product.Price === null ? 0 : item.Product.Price).toLocaleString() + '</td>' +
                                '<td><span>' +
                                '<a class = "glyphicon glyphicon-minus decrease-number" onclick = "DecreaseNumber(' + item.Product.ID + ')"></a>' +
                                '<input id = "Quantity_' + item.Product.ID + '" style="width:40px;text-align:center" type="text" class="txtQuantity" value="' + item.Quantity + '" />' +
                                '<a class = "glyphicon glyphicon-plus increase-number" onclick = "IncreaseNumber(' + item.Product.ID + ')"></a>' +
                                '</span></td>' +
                                '<td id = "Total_' + item.Product.ID + '">' + ((item.Product.Price === null ? 0 : item.Product.Price) * item.Quantity).toLocaleString() + '</td>' +
                                '<td>' +
                                '<a class = "glyphicon glyphicon-remove  btn-lg text-danger" onclick = "DeleteCartItem(' + item.Product.ID + ')"></a>' +
                                '</td>' +
                                '</tr >';
                            stringtext += text;
                        });
                        var dialog = bootbox.dialog({
                            title: 'Giỏ hàng của bạn',
                            message: '<table class="table table-bordered table-hover table-center text-center">' +
                            '<thead align="center">' +
                            '<tr>' +
                            '<th>Mã sản phẩm</th>' +
                            '<th>Hình ảnh</th>' +
                            '<th>Tên sản phẩm</th>' +
                            '<th>Đơn giá</th>' +
                            '<th>Số lượng</th>' +
                            '<th>Thành tiền</th>' +
                            '<th>#</th>' +
                            '</tr>' +
                            '</thead >' +
                            '<tbody>' +
                            stringtext +
                            '</tbody >' +
                            '<tfoot>' +
                            '<tr>' +
                            '<td colspan="4" class="text-right">' +
                            '<p class="text-danger font-weight-bold pull-left">TỔNG SỐ TIỀN: <span id="tong_tien_bang_chu">' + res.tongtienbangchu + '</span></p>' +
                            '</td>' +
                            '<td class="font-weight-bold" id="tong_so_luong">' + res.tongsl + '</td>' +
                            '<td class="font-weight-bold" id = "tong_tien">' + res.tongtien + '</td>' +
                            '<td></td>' +
                            '</tr>' +
                            '</tfoot>' +
                            '</table >',
                            buttons: {
                                update: {
                                    label: '<span><i class = "fa fa-refresh"></i>  Cập nhật giỏ hàng</span>',
                                    className: 'btn btn-success invisible',
                                    callback: function () {
                                        //Get list sản phẩm và số lượng
                                        let items = [];
                                        $.each($('tbody tr'), function (i, item) {
                                            let proid = Number($(item).prop('id').substring(4));
                                            let quantity = Number($(item).find('input').val());
                                            items.push({ productid: proid, quantity : quantity });
                                        });
                                        //Update lên server
                                        $.ajax({
                                            url: "/Carts/Update",
                                            type: "Post",
                                            data: {items:JSON.stringify(items)},
                                            dataType: "json",
                                            success: function (res) {
                                                if (res.status) {
                                                    AnHienButton(false);
                                                }
                                                
                                            }
                                        });
                                        return false;
                                    }
                                },
                                order: {
                                    label: '<span><i class = "fa fa-money"></i>  Thực hiện đặt hàng</span>',
                                    className: 'btn btn-danger',
                                    callback: function (response) { }
                                },
                                cancel: {
                                    label: '<span><i class = "glyphicon glyphicon-shopping-cart"></i>  Tiếp tục mua hàng</span>',
                                    className: 'btn btn-primary',
                                    callback: function (response) { }
                                }                               
                            },
                            size: 'large'
                        });

                    } else {
                        bootbox.alert(res.message);
                    }
                }
            });
        });
    }
};
$(document).ready(function () {
    obj.init();
});

//Hàm đổi giá trị từ true sang false và ngược lại
function changeBoolValue(idItem, urlAction) {
    //e.preventDefault();
    var checkbox = $(this);
    var id = idItem;
    console.log(id);
    var path = urlAction;
    console.log(path);
    $.ajax({
        url: path,
        data: { id: id },
        dataType: "json",
        type: "POST",
        success: function (response) {
            if (response.status) {
                if (response.value) {
                    alert(response.message);
                    checkbox.prop("checked", true);
                } else {
                    alert(response.message);
                    checkbox.prop("checked", false);
                }
            }
        }
    });
}
function DeleteCartItem(id) {
    //Hiện nút update
    AnHienButton(true);
    let rowid = "#Row_" + id;
    console.log(rowid);
    $.ajax({
        url: '/Carts/Delete',
        data: { productid: id },
        dataType: 'json',
        type: 'Post',
        success: function (res) {
            if (res.status) {
                $(rowid).remove();
                $('#tong_tien_bang_chu').html(res.tongtienbangchu);
                $('#tong_tien').html(res.tongtien);
                $('#tong_so_luong').html(res.tongsl);
            }
        }
    });
}
function IncreaseNumber(productid) {
    //Hiện nút update
    AnHienButton(true);
    //Tăng số lượng trong input
    let input = $('#Quantity_' + productid);
    let value = Number(input.val());
    value += 1;
    input.val(value);
    //Cập nhật lại thành tiền
    let priceElement = $('#Price_' + productid);
    let re = /,/gi;
    let price = Number(priceElement.html().replace(re, ''));
    let total = $('#Total_' + productid);
    let totalNumber = value * price;
    total.html(totalNumber.toLocaleString());
    //Cập nhật footer
    let tongslNumber = Number($('#tong_so_luong').html().replace(re, ''));
    let tongttNumber = Number($('#tong_tien').html().replace(re, ''));
    tongslNumber += 1; tongttNumber += price;
    $('#tong_so_luong').html(tongslNumber.toLocaleString());
    $('#tong_tien').html(tongttNumber.toLocaleString());
    $('#tong_tien_bang_chu').html(DocSoThanhChu.docso(tongttNumber));
    //Update lên server
    //$.ajax({
    //    url: '/Carts/Update',
    //    data: { productid: productid, quantity: value },
    //    dataType: "json",
    //    type: "Post",
    //    success: function (res) {
    //        if (res.status) {
    //            $('#tong_tien_bang_chu').html(res.tongtienbangchu);
    //            $('#tong_tien').html(res.tongtien);
    //            $('#tong_so_luong').html(res.tongsl);
    //        }
    //    }
    //});
}
function DecreaseNumber(productid) {
    //Hiện nút update
    AnHienButton(true);
    //Giảm số lượng phần input
    let input = $('#Quantity_' + productid);
    let value = Number(input.val());
    if (value > 1) { //Nếu nhỏ hơn 1 thì không thể giảm
        value -= 1;
        input.val(value);
        //Cập nhật lại thành tiền
        let priceElement = $('#Price_' + productid);
        let re = /,/gi;
        let price = Number(priceElement.html().replace(re, ''));
        let total = $('#Total_' + productid);
        let totalNumber = value * price;
        total.html(totalNumber.toLocaleString());
        //Cập nhật footer
        let tongslNumber = Number($('#tong_so_luong').html().replace(re, ''));
        let tongttNumber = Number($('#tong_tien').html().replace(re, ''));
        tongslNumber -= 1; tongttNumber -= price;
        $('#tong_so_luong').html(tongslNumber.toLocaleString());
        $('#tong_tien').html(tongttNumber.toLocaleString());
        $('#tong_tien_bang_chu').html(DocSoThanhChu.docso(tongttNumber));
    }

}
function AnHienButton(hienNutUpdate) {
    if (hienNutUpdate) {
        //Hiện nút update, ẩn 2 nút còn lại
        let updatebtn = $('[data-bb-handler="update"]');
        let orderbtn = $('[data-bb-handler="order"]');
        let cancelbtn = $('[data-bb-handler="cancel"]');
        updatebtn.removeClass('invisible');
        orderbtn.addClass('invisible');
        cancelbtn.addClass('invisible');
    } else {
        //Ẩn nút update, hiện 2 nút còn lại
        let updatebtn = $('[data-bb-handler="update"]');
        let orderbtn = $('[data-bb-handler="order"]');
        let cancelbtn = $('[data-bb-handler="cancel"]');
        updatebtn.addClass('invisible');
        orderbtn.removeClass('invisible');
        cancelbtn.removeClass('invisible');
    }
}