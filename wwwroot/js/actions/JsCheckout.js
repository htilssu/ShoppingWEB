﻿// Gắn sự kiện click cho nút "Thay Đổi"
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
    const addresses = document.querySelectorAll('.o-nharieng, .o-vanphong');
    addresses.forEach(address => {
        address.classList.remove('selected-address');
    });
    // Thêm lớp CSS 'selected-address' cho địa chỉ được chọn
    element.classList.add('selected-address');
}

//----Xu Ly backend-----
function Erorr(){
    confirm("Xin lỗi bạn! Tôi chưa thêm chức năng cho sự kiện này...");
}
function Erorr_ship(){
    confirm("Không thể Thay Đổi phương thức Vận Chuyển.");
}
function YeuCau(){
    confirm("Yêu Cầu Thành Công.");
}


// Hàm JavaScript để chọn Phương thức thanh toán
function selectPay(element) {
    const addresses = document.querySelectorAll('.button-checkout');
    addresses.forEach(address => {
        address.classList.remove('selected-address'); //Loại bỏ lớp CSS 'selected-address' từ tất cả các địa chỉ
    });
    // Thêm lớp CSS 'selected-address' cho địa chỉ được chọn
    element.classList.add('selected-address');
}

var btnThanhToan = $("#btnThanhToan");
btnThanhToan.on("click", function (e) {
    // Kiểm tra xem có phương thức thanh toán nào được chọn không
    var selectedAddress = document.querySelector('.selected-address');

    // Nếu không có phương thức thanh toán
    if (!selectedAddress) {
        e.preventDefault();
        alert("Vui lòng chọn Phương Thức Thanh Toán trước khi Thanh Toán.");
    }
    //Ngược lại:
    else {
        var confirmation = confirm("Xác nhận Thanh Toán ?");
        if (confirmation) {
            alert("Thanh toán Thành Công");
        } else {
            alert("Thanh toán bị Hủy!");
        }
    }
});

