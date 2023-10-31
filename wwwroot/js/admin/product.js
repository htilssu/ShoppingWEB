const closeBtnPro = $(".close-popup");
const showProductTypePop = $(".add-product-type");

closeBtnPro.on("click", handleCloseProductPopup);
showProductTypePop.on("click", handleShowProductTypePopup);

function handleShowProductTypePopup() {
  console.log("hi");
  closeBtnPro.closest(".product-type-popup").removeClass("d-none");
}

function handleCloseProductPopup() {
  $(this).closest(".product-type-popup").addClass("d-none");
}
