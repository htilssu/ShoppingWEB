const selectAllCheckbox = document.getElementById('selectAllCheckbox');
const productCheckboxes = document.querySelectorAll('.product-checkbox');

productCheckboxes.forEach(checkbox => {
    checkbox.addEventListener('change', () => {
        updateSelectedProductsAndTotalPrice();
    });
});

// Cấu trúc dữ liệu để lưu trữ thông tin sản phẩm.
const products = {
    1: { price: 1500, quantity: 1, remainingQuantity: 9 },
    2: { price: 1700, quantity: 1, remainingQuantity: 19 },
    3: { price: 1500, quantity: 1, remainingQuantity: 20 },
    4: { price: 1300, quantity: 1, remainingQuantity: 3 },
    5: { price: 1200, quantity: 1, remainingQuantity: 25 },
    6: { price: 1400, quantity: 1, remainingQuantity: 30 },
};

function dieuchinh_sl(productNumber, change) {
    const soluong = document.getElementById(`quantity-${productNumber}`);
    const sotien = document.getElementById(`sotien-${productNumber}`);
    const remainingQuantityElement = document.getElementById(`remaining-quantity-${productNumber}`);

    // Số lượng còn lại là số lượng hiện tại trừ đi sự thay đổi.
    const remainingQuantity = products[productNumber].quantity + change;

    // Kiểm tra xem số lượng tăng có vượt quá số lượng còn lại không.
    if (remainingQuantity >= 0 && remainingQuantity <= products[productNumber].remainingQuantity) {
        products[productNumber].quantity += change;

        // Cập nhật giao diện.
        soluong.value = products[productNumber].quantity;
        sotien.textContent = `$${(products[productNumber].quantity * products[productNumber].price).toLocaleString()}`;

        // Cập nhật tổng số tiền
        updateSelectedProductsAndTotalPrice();
    }
}

selectAllCheckbox.addEventListener('change', () => {
    productCheckboxes.forEach(checkbox => {
        checkbox.checked = selectAllCheckbox.checked;
    });
    updateSelectedProductsAndTotalPrice();
});

function updateSelectedProductsAndTotalPrice() {
    let selectedCount = 0;
    let totalPrice = 0;
    // Đếm số sản phẩm đã chọn và tính tổng số tiền cho các sản phẩm được chọn
    productCheckboxes.forEach((checkbox, index) => {
        if (checkbox.checked) {
            selectedCount++;

            // Lấy giá của sản phẩm từ cấu trúc dữ liệu
            const product = products[index + 1];
            totalPrice += product.price * product.quantity;
        }
    });

    // Cập nhật nội dung các thẻ div
    document.getElementById('selected-products').textContent = `Tổng thanh toán(${selectedCount} sản phẩm):`;
    document.getElementById('total-price').textContent = `$${totalPrice.toFixed(2)}`;
}

/*--------------------------FreeShip-----------------------------*/
// JavaScript để hiển thị bảng thông báo khi di chuyển chuột đến "Tìm hiểu thêm"
const learnMoreElements = document.querySelectorAll('.learn-more');
const popups = document.querySelectorAll('.popup');

learnMoreElements.forEach((learnMoreElement, index) => {
    learnMoreElement.addEventListener('mouseover', () => {
        // Lấy vị trí của "Tìm hiểu thêm" để đặt vị trí cho bảng thông báo tương ứng
        const rect = learnMoreElement.getBoundingClientRect();
        popups[index].style.top = rect.bottom + 'px';
        popups[index].style.left = rect.left + 'px';
        popups[index].classList.remove('hidden');
    });

    learnMoreElement.addEventListener('mouseout', () => {
        popups[index].classList.add('hidden');
    });
});