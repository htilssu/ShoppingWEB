// Change preview image
const subImageCol = $(".sub-image-item");
subImageCol.on('mouseover', (e) => {
    changePreviewImage(e.target.getAttribute('src'))
    changeLastImage(e.target)
})

let lastImage = null
let lastSubImage = null;
let firstSubImage = subImageCol.first();
changeLastImage(firstSubImage)

function changeLastImage(e) {
    if (lastSubImage === firstSubImage) {
        lastSubImage.removeClass('viewing');
    } else if (lastSubImage !== null) {
        lastSubImage.classList.remove("viewing")
    }

    if (e === firstSubImage) {
        e.addClass("viewing")
        lastSubImage = e;
    } else {
        if (e.closest('.sub-image-item')) {
            e.closest('.sub-image-item').classList.add("viewing")
            lastSubImage = e.closest('.sub-image-item');
        }
    }
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

function changeClassifyPreviewImage(e) {
    const target = e.target.closest('button').querySelector('img');
    const attr = target.getAttribute('src');
    changePreviewImage(attr)
}

function returnPreviewImage(e) {
    const target = lastImage;
    if (target === firstSubImage) {
        const attr = target.find('img').attr('src')
        changePreviewImage(attr)

    } else {
        if (target !== null) {
            const attr = target.querySelector('img').getAttribute('src')
            changePreviewImage(attr)
        }

    }
}


//registry mouseover event
classifyList.on('mouseover', changeClassifyPreviewImage)
classifyList.on('mouseout', returnPreviewImage)


//choose type
let lastChoose = null;

classifyList.on('click', (e) => {
    if (lastChoose !== null) {
        lastChoose.classList.remove('viewing')
    }
    changeClassifyPreviewImage(e)
    const button = e.target.closest('button');
    button.classList.add('viewing')
    lastImage = button;
    lastChoose = button;
    e.preventDefault()

})

//Size item 
const sizeItemList = $('.size-item');
let sizeLastChoose = null;

sizeItemList.on('click', (e) => {
    if (sizeLastChoose !== null) {
        sizeLastChoose.classList.remove('viewing')
    }
    changeClassifyPreviewImage(e)

    sizeLastChoose = e.target.closest('.size-item');
    sizeLastChoose.classList.add('viewing')
})