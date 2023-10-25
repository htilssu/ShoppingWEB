const ratingFilters = $(".product-rating-filter__item")

ratingFilters.first().addClass("viewing")

function handleFilterChange(e) {
    ratingFilters.each(function (index, element) {
        if ($(element).hasClass("viewing")) $(element).removeClass("viewing")
    })

    $(this).addClass("viewing")
}

ratingFilters.on("click", handleFilterChange)