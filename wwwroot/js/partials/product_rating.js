const ratingFilters = $(".product-rating-filter__item");

ratingFilters.on("click", handleFilterChange);

ratingFilters.first().addClass("viewing");

function handleFilterChange(e) {
  ratingFilters.each(function (index, element) {
    if ($(element).hasClass("viewing")) $(element).removeClass("viewing");
  });

  $(this).addClass("viewing");
}
