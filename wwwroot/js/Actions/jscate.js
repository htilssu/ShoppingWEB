const clearAllButton = document.getElementById('clearAll');
const checkboxInputs = document.querySelectorAll('.category-group-item-check');
var priceFromInput = document.getElementById("priceFrom");
var priceToInput = document.getElementById("priceTo");
var applyFilterButton = document.getElementById("applyFilter");
clearAllButton.addEventListener('click', clearAll);
// Xử lý sự kiện khi nút "Áp dụng" được nhấn
applyFilterButton.addEventListener("click", function () {
    var priceFromValue = parseFloat(priceFromInput.value);
    var priceToValue = parseFloat(priceToInput.value);

    if (isNaN(priceFromValue) || isNaN(priceToValue) || priceFromValue > priceToValue) {

    } else {
        // Thực hiện logic áp dụng bộ lọc ở đây
        console.log("Giá từ: " + priceFromValue + " đến: " + priceToValue);
    }
});

// Kiểm tra giá trị nhập liệu để ngăn người dùng nhập các ký tự không phải số
priceFromInput.addEventListener("input", function () {
    priceFromInput.value = priceFromInput.value.replace(/[^0-9]/g, "");
});

priceToInput.addEventListener("input", function () {
    priceToInput.value = priceToInput.value.replace(/[^0-9]/g, "");
});

applyFilterButton.addEventListener('click', applyPriceFilter);


// Hàm xóa tất cả các checkbox đã chọn
function clearAll() {
    checkboxInputs.forEach(input => {
        input.checked = false;
    });
    // Thực hiện logic sau khi xóa tất cả checkbox ở đây
}
