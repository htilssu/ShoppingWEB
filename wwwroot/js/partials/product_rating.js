const ratingFilters = $(".product-rating-filter__item");
const previewSubImage = $(".posted-content__sub-image img");

ratingFilters.on("click", handleFilterChange);

previewSubImage.on("click", handleChangePreview);

ratingFilters.first().addClass("viewing");

function handleFilterChange(e) {
  ratingFilters.each(function (index, element) {
    if ($(element).hasClass("viewing")) $(element).removeClass("viewing");
  });

  $(this).addClass("viewing");
}

function handleChangePreview(e) {}
