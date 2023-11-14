// Gắn sự kiện click cho nút "Thay Đổi"
changeAddressButton.addEventListener("click", function (e) {
    // Hiển thị form nhập thông tin mới khi nút "Thay Đổi" được nhấn
    changeAddressForm.style.display = "block";
    e.stopPropagation(); // Ngăn sự kiện click truyền qua form
});

// Gắn sự kiện click cho nút "Hủy"
cancelButton.addEventListener("click", function () {
    // Ẩn form khi nút "Hủy" được nhấn
    changeAddressForm.style.display = "none";
});

// Gắn sự kiện click cho nút "Xác nhận"
saveAddressButton.addEventListener("click", function () {
    // Lấy giá trị tên, số điện thoại và địa chỉ mới từ input
    const newName = document.getElementById("newName").value;
    const newPhoneNumber = document.getElementById("newPhoneNumber").value;
    const newAddress = document.getElementById("newAddress").value;

    // Kiểm tra và hiển thị thông báo yêu cầu nhập và đúng cú pháp
    if (newName.trim() === "" || /[^A-Za-z]/.test(newName)) {
        document.getElementById("nameError").style.display = "block";
        return; // Ngăn form gửi đi nếu trường tên trống
    }
    else {
        document.getElementById("nameError").style.display = "none";
    }

    if (newPhoneNumber.trim() === "" || /\D/.test(newPhoneNumber)) {
        document.getElementById("phoneNumberError").style.display = "block";
        return; // Ngăn form gửi đi nếu trường số điện thoại trống
    }
    else {
        document.getElementById("phoneNumberError").style.display = "none";
    }

    if (newAddress.trim() === "") {
        document.getElementById("addressError").style.display = "block";
        return; // Ngăn form gửi đi nếu trường địa chỉ mới trống
    }
    else {
        document.getElementById("addressError").style.display = "none";
    }

    // Cập nhật thông tin hiển thị
    document.querySelector(".position2 b").textContent = newName + " (" + newPhoneNumber + ")";
    document.querySelector(".position2-1").textContent = newAddress;

    // Ẩn form
    changeAddressForm.style.display = "none";
});

// Gắn sự kiện click cho toàn bộ màn hình để đóng form khi bất kỳ nơi nào được nhấn
document.addEventListener("click", function () {
    changeAddressForm.style.display = "none";
});

// Ngăn sự kiện click truyền qua form
changeAddressForm.addEventListener("click", function (e) {
    e.stopPropagation();
});

//--------------------------------
// Hàm JavaScript để chọn địa chỉ
function selectAddress(element) {
    // Loại bỏ lớp CSS 'selected-address' từ tất cả các địa chỉ
    const addresses = document.querySelectorAll('.o-nharieng, .o-vanphong, .button-checkout');
    addresses.forEach(address => {
        address.classList.remove('selected-address');
    });

    // Thêm lớp CSS 'selected-address' cho địa chỉ được chọn
    element.classList.add('selected-address');
}

//----Xu Ly backend-----
function updateTotalPrice(totalPrice) {
    //tính tổng số tiền cho các bao hiem được chọn
    const productCheckedList = $(".product-checkbox:checked");
    productCheckedList.each((index, checkbox) => {
        // Lấy giá của bao hiem sản phẩm 
        const parent = $(checkbox).closest("tr");
        const price = parent.find("#sotienbaohiem");
        const value = price.text().replaceAll(",", "")

        totalPrice += value - 0;
    });

    // Cập nhật nội dung các thẻ div
    document.getElementById('total-price').textContent = `₫${new Intl.NumberFormat().format(totalPrice)}`;
}
function Erorr(){
    confirm("Xin lỗi bạn! Tôi chưa thêm chức năng cho sự kiện này...");
}
function YeuCau(){
    confirm("Yêu Cầu Thành Công.");
}

function thanhToan() {
    if (selectedPay !== null) {
        console.log(`Đang thực hiện thanh toán bằng phương thức: ${selectedPay}`);
        // Thêm logic xử lý thanh toán của bạn tại đây
    }
    else {
        confirm('Vui lòng chọn Phương Thức Thanh Toán !');
    }
}