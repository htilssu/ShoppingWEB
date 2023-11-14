const clearAllButton = document.getElementById('clearAll');
const checkboxInputs = document.querySelectorAll('.category-group-item-check');
var priceFromInput = document.getElementById("priceFrom");
var priceToInput = document.getElementById("priceTo");
var applyFilterButton = document.getElementById("applyFilter");

// Hàm áp dụng bộ lọc
function applyPriceFilter() {
    var priceFromValue = parseFloat(priceFromInput.value);
    var priceToValue = parseFloat(priceToInput.value);

    if (isNaN(priceFromValue) || isNaN(priceToValue) || priceFromValue > priceToValue) {
        // Xử lý khi giá trị không hợp lệ
    } else {
        // Thực hiện logic áp dụng bộ lọc ở đây
        console.log("Giá từ: " + priceFromValue + " đến: " + priceToValue);

        // Đặt các logic áp dụng bộ lọc khác ở đây
    }
}

// Kiểm tra giá trị nhập liệu để ngăn người dùng nhập các ký tự không phải số
priceFromInput.addEventListener("input", function () {
    priceFromInput.value = priceFromInput.value.replace(/[^0-9]/g, "");
});

priceToInput.addEventListener("input", function () {
    priceToInput.value = priceToInput.value.replace(/[^0-9]/g, "");
});

// Xử lý sự kiện khi nút "Áp dụng" được nhấn
applyFilterButton.addEventListener("click", applyPriceFilter);

// Hàm xóa tất cả các checkbox đã chọn và áp dụng bộ lọc
function clearAll() {
    checkboxInputs.forEach(input => {
        input.checked = false;
    });

    // Xóa giá trị của ô nhập priceFrom và priceTo
    priceFromInput.value = "";
    priceToInput.value = "";

    // Áp dụng bộ lọc sau khi xóa tất cả checkbox và giá trị ô nhập
    applyPriceFilter();
}

clearAllButton.addEventListener('click', clearAll);