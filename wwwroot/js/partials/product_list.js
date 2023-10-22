const productItem = $(".a");

productItem.on("mouseover", showSimilar)
productItem.on("mouseout", hideSimilar)

function showSimilar(e) {
    $(this).find(".area-product-hover").addClass("d-flex")
    $(this).find(".area-product-hover").removeClass("d-none")
}

function hideSimilar(e) {
    $(this).find(".area-product-hover").removeClass("d-flex")
    $(this).find(".area-product-hover").addClass("d-none")
}