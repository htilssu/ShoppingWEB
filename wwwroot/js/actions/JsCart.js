//-----Tang Giam So Luong----
const quantityInp = $(".soluong")
quantityInp.on('change', handleChangeQuantity)
quantityInp.on('input', handleChangeQuantity)
function handleChangeQuantity() {
    console.log(1);
    if ($(this).val() < 0){
        $(this).val(1)
    }
    const parentTr = $(this).closest("tr")
    var spMaxQuantity = parentTr.find(".max-quantity");
    if ($(this).val() > spMaxQuantity.text().trim()-0)  
    {
        $(this).val(spMaxQuantity.text().trim())
    }
    const dongia = parentTr.find(".dongia > div").eq(0)
    const value = dongia.text().replaceAll(",", "")
    const sotientra = parentTr.find("#sotientra")
    const total = value * $(this).val();
    sotientra.text(new Intl.NumberFormat().format(total))

    updateQuantityPost(parentTr.find("#cartItemId").attr("value"), $(this).val())
    updateSelectedProductsAndTotalPrice();
    
}

var btnDcre = $(".decrement-soluong"); //truyen vao 1 doi tuong
btnDcre.on('click', DreQuantity) //dang ky su kien, ten ham
function DreQuantity() {    
    const parentTr = $(this).closest("tr")
    const parent = parentTr.find("td").eq(2) // chon phan tu td thu 3 duoc tim tu the tr (so luong)
    const intput = parent.find("input");
    intput.val(intput.val() - 1 > 0 ? intput.val() - 1 : 1) //lay cha, xong tim con 

    const dongia = parentTr.find(".dongia > div").eq(0)  //the div thu nhat
    const value = dongia.text().replaceAll(",", "")
    const sotientra = parentTr.find("#sotientra")
    const total = value * intput.val();
    if (isNaN(total)) {
        sotientra.text(0)
    } else {
        sotientra.text(new Intl.NumberFormat().format(total))
    }

    updateQuantityPost(parentTr.find("#cartItemId").attr("value"), intput.val())
    updateSelectedProductsAndTotalPrice();
    
}

var btnIncre = $(".increment-soluong");
btnIncre.on('click', IncreQuantity)

function IncreQuantity() {
    const parentTr = $(this).closest("tr")
    const parent = parentTr.find("td").eq(2)  //tim ra cha
    var spMaxQuantity = parentTr.find(".max-quantity"); //lay ra con
    const intput = parent.find("input");
    intput.val(intput.val() - 0 + 1 <= spMaxQuantity.text().trim()-0 ? intput.val() - 0 + 1 : spMaxQuantity.text().trim())

    const dongia = parentTr.find(".dongia > div").eq(0)
    const value = dongia.text().replaceAll(",", "")
    const sotientra = parentTr.find("#sotientra")
    const total = value * intput.val();
    sotientra.text(new Intl.NumberFormat().format(total))

    updateQuantityPost(parentTr.find("#cartItemId").attr("value"), intput.val())
    updateSelectedProductsAndTotalPrice();
    
}
//update du lieu len database
function updateQuantityPost(cartItemId, quantity) {
    $.ajax(document.location.href + "/UpdateQuantity", 
        {
        method: "POST",
        data: {cartItemId, quantity},
        xhrFields: {withCredentials: true}
    })
}

//----------SeLect------
const selectAllCheckbox = document.getElementById("selectAllCheckbox");
const selectAllCheckbox1 = document.getElementById("selectAllCheckbox1");

const productCheckboxes = $(".product-checkbox");

productCheckboxes.on("change", (e) => {
    if (e.target.checked === false) {  //neu 1 sanpham khong chon thi xoa selectAll
        selectAllCheckbox.checked = selectAllCheckbox1.checked = false;
    }
    
    updateSelectedProductsAndTotalPrice();
});


selectAllCheckbox.addEventListener("change", () => {  //select All tren
    productCheckboxes.each((index,checkbox) => {
        checkbox.checked = selectAllCheckbox.checked;
    });
    selectAllCheckbox1.checked = selectAllCheckbox.checked;
    updateSelectedProductsAndTotalPrice();
});
selectAllCheckbox1.addEventListener("change", () => {  //select All duoi
    productCheckboxes.each((INDEX   ,checkbox) => {
        checkbox.checked = selectAllCheckbox1.checked;
    });
    selectAllCheckbox.checked = selectAllCheckbox1.checked;
    updateSelectedProductsAndTotalPrice();
});

function updateSelectedProductsAndTotalPrice() {
    let selectedCount = 0;  //so san pham duoc chon
    let totalPrice = 0;
    // Đếm số sản phẩm đã chọn và tính tổng số tiền cho các sản phẩm được chọn

    const productCheckedList = $(".product-checkbox:checked");
    productCheckedList.each((index, checkbox) => {

        selectedCount++;

        // Lấy giá của sản phẩm 
        const parent = $(checkbox).closest("tr");
        const price = parent.find("#sotientra");
        const value = price.text().replaceAll(",", "")

        totalPrice += value - 0;
    });

    // Cập nhật nội dung các thẻ div
    document.getElementById('selected-products').textContent = `Tổng thanh toán(${selectedCount} sản phẩm):`;
    document.getElementById('total-price').textContent = `₫${new Intl.NumberFormat().format(totalPrice)}`;
}

//------Kiem tra dieu kien Mua Hang-------0
var btnMua = $("#checkoutButton");
btnMua.on('click', MuaHang)
function MuaHang(e) {
    const productCheckedList = $(".product-checkbox:checked");
    if (productCheckedList.length === 0)  //neu chua chon san pham nao thi khong cho mua hang
    {
        e.preventDefault();
        confirm("Bạn Chưa chọn Sản Phẩm nào! Vui lòng chọn để mua! ");
    }
    const selectedProducts = [];
    productCheckedList.each(function () {
        const parent = $(this).closest("tr");
        const productId = parent.find("#cartItemId").attr("value");
        console.log(productId)
        const productQuantity = parent.find("#quantity-1").text();
        const productTotalPrice = parent.find("#sotientra").text();

        e.preventDefault()
        e.stopPropagation()
        // Thêm thông tin sản phẩm vào mảng
        selectedProducts.push({
            id: productId,
            quantity: productQuantity,
            price: productTotalPrice
            // Thêm các thông tin khác nếu cần
        });
    });

    // Chuyển dữ liệu sang trang thanh toán (checkout)
    // Sử dụng AJAX hoặc chuyển hướng trình duyệt, tùy thuộc vào yêu cầu của bạn

    // Ví dụ sử dụng chuyển hướng trình duyệt
    window.location.href = `/Checkout?selectedProducts=${encodeURIComponent(JSON.stringify(selectedProducts))}`;
}


/*--------------------------FreeShip-----------------------------*/
// JavaScript để hiển thị bảng thông báo khi di chuyển chuột đến "Tìm hiểu thêm"
const learnMoreElements = document.querySelectorAll(".learn-more");
const popups = document.querySelectorAll(".popup");

learnMoreElements.forEach((learnMoreElement, index) => {
    learnMoreElement.addEventListener("mouseover", () => {
        // Lấy vị trí của "Tìm hiểu thêm" để đặt vị trí cho bảng thông báo tương ứng
        const rect = learnMoreElement.getBoundingClientRect();
        popups[index].classList.remove("hidden");
    });

    learnMoreElement.addEventListener("mouseout", () => {
        popups[index].classList.add("hidden");
    });
});
