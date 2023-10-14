// Change preview image
const subImageCol = $(".sub-image-item");
subImageCol.on('mouseover', subImageHandle)

function subImageHandle() {
    const target = $(this)
    changePreviewImage(target.find('img').attr('src'))
    changeLastImage(target)
    lastImage = target.find('img').attr('src');
}


let lastImage = null
let lastSubImage = null;
let firstSubImage = subImageCol.first();
changeLastImage(firstSubImage)

function changeLastImage(e) {

    if (lastSubImage) {
        lastSubImage.removeClass('viewing')
    } else {
        lastImage = e.find('img').attr('src')
    }
    e.addClass('viewing')
    lastSubImage = e;
}


function changePreviewImage(e) {
    let attr = e;
    if (attr !== null && attr !== "") {
        $("#product-img-preview").attr({
            src: attr
        })
    }
}

// Quantity action
let quantityInp = $(".data-quantity input");
quantityInp.on('input', () => {
    let value = quantityInp.val();
    if (isNaN(parseInt(value[value.length - 1]))) {
        quantityInp.val(value.substring(0, value.length - 1))
    }
    if (parseInt(value) === 0) {
        quantityInp.val(1)
    } else if (value[0] === '0') {
        quantityInp.val(value.substring(1, value.length))
    }
    if (quantityInp.val() < 0) quantityInp.val(-quantityInp.val())
})
quantityInp.on('change', () => {
    if (quantityInp.val() === "") {
        quantityInp.val(1);
    }
})

// /registry
$("#decrease").on('click', () => {
    let value = quantityInp.val();
    if (value > 1) {

        quantityInp.val(quantityInp.val() - 1);
    }
})
$("#increase").on('click', () => {
    quantityInp.val(parseInt(quantityInp.val()) + 1);
})


//View classify 
const classifyList = $(".classify-item");

function changeClassifyPreviewImage() {
    const target = $(this).find('img');
    const attr = target.attr('src');
    changePreviewImage(attr)
}

function returnPreviewImage() {
    console.log(lastImage)
    if (lastImage) {
        changePreviewImage(lastImage)
    }
}


//registry mouseover event
classifyList.on('mouseover', changeClassifyPreviewImage)
classifyList.on('mouseout', returnPreviewImage)


//choose type
let lastChoose = null;

classifyList.on('click', classifyClickHandle)

function classifyClickHandle(e) {
    const target = $(this)
    if (lastChoose) {
        lastChoose.removeClass('viewing')
    }
    changeClassifyPreviewImage(e)
    target.addClass('viewing')
    lastImage = target.find('img').attr('src')
    lastChoose = target;
    e.preventDefault()
}

//Size item 
const sizeItemList = $('.size-item');
let sizeLastChoose = null;

sizeItemList.on('click', (e) => {
    if (sizeLastChoose !== null) {
        sizeLastChoose.classList.remove('viewing')
    }

    sizeLastChoose = e.target.closest('.size-item');
    sizeLastChoose.classList.add('viewing')
})