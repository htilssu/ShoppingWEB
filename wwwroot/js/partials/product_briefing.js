const subImageCol = $(".sub-image-item");
subImageCol.on('mouseover', changePreviewImage)

let lastImage = null
let firstSubImage = subImageCol.first();
changeLastImage(firstSubImage)

function changeLastImage(e) {
    if (lastImage === firstSubImage) {
        lastImage.removeClass('viewing');
    } else if (lastImage !== null) {
        lastImage.classList.remove("viewing")
    }

    if (e === firstSubImage) {
        e.addClass("viewing")
        lastImage = e;
    } else {
        e.closest('.sub-image-item').classList.add("viewing")
        lastImage = e.closest('.sub-image-item');
    }
}


function changePreviewImage(e) {
    let attr = e.target.getAttribute('src');
    if (attr !== null && attr !== "") {
        changeLastImage(e.target)
        $("#product-img-preview").attr({
            src: e.target.getAttribute('src')
        })
    }
}