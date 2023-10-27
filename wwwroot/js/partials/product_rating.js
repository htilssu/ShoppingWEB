const ratingFilters = $(".product-rating-filter__item");
const previewSubImage = $(".posted-content__sub-image img");
const pageItems = $(".list_number");
const previousPage = $(".previous-page");
const nextPage = $(".next-page");
let currentPage = 0;

//TODO handle request to server go to next page
previousPage.on("click", handlePreviousPage);
nextPage.on("click", handleNextPage);
pageItems.on("click", handleSelectPage);
ratingFilters.on("click", handleFilterChange);
previewSubImage.on("click", handleChangePreview);

ratingFilters.first().addClass("viewing");
pageItems.first().addClass("current-page");

function handleFilterChange() {
  ratingFilters.each((i, e) => {
    if ($(e).hasClass("viewing")) $(e).removeClass("viewing");
  });

  $(this).addClass("viewing");
}

// TODO setup handle
function handleChangePreview() {}

function handleSelectPage() {
  const item = $(this);
  currentPage = item.text().trim() - 1;
  changeCurrentPage(currentPage);
}

function handleNextPage() {
  if (currentPage === pageItems.last().text().trim() - 1) return;
  currentPage += 1;
  changeCurrentPage(currentPage);
}

function handlePreviousPage() {
  if (currentPage === 0) {
    return;
  }
  currentPage -= 1;
  changeCurrentPage(currentPage);
}

function changeCurrentPage(index) {
  pageItems.each((i, e) => {
    const item = $(e);
    if (i === index) item.addClass("current-page");
    else item.removeClass("current-page");
  });
}
