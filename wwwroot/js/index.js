﻿//slide category to right
$(".next-item-btn-right").on("click", () => {
    $(".item-list").css({
        transform: 'translateX(-21%)'
    })
})

//slide category to left
$(".next-item-btn-left").on("click", () => {
    $(".item-list").css({
        transform: 'translateX(0)'
    })
})