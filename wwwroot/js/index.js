const itemListCategory = $(".item-list .ct-item");
const overFlowCount = Math.ceil(itemListCategory.length / 2) - 9;

//slide category to right
$(".next-item-btn-right").on("click", () => {
  $(".item-list").css({
    transform: `translateX(calc(-100% / 14 * ${overFlowCount}))`,
  });
});

//slide category to left
$(".next-item-btn-left").on("click", () => {
  $(".item-list").css({
    transform: "translateX(0)",
  });
});
