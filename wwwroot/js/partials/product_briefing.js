// Change preview image
const subImageCol = $(".sub-image-item");
const classifyList = $(".classify-item");
const firstSubImage = subImageCol.first();
const quantityInp = $(".data-quantity input");
quantityInp.val(1)
const remainingQuantity = $('.remaining-quantity');
const sizeValue = $("#size");
const btnSizes = $(".size-item");
const form = $("form .btn");
const typeProductInp = $("input[name='TypeProductId']");
const sizeTypeInp = $("input[name='SizeType']");
let lastImage = null;
let lastSubImage = null;
let sizeLastChoose = null;
let lastChoose = null;

changeLastImage(firstSubImage);

//registry event
subImageCol.on("mouseover", subImageHandle);
classifyList.on("mouseover", changeClassifyPreviewImage);
classifyList.on("mouseout", returnPreviewImage);
btnSizes.on("click", handleChangeSize);
classifyList.on("click", classifyClickHandle);
quantityInp.on("input", handleQuantityOnInput);
quantityInp.on("change", handleQuantityInputChange);
$("#decrease").on("click", handleQuantityDecrease);
$("#increase").on("click", handleQuantityIncrease);
form.on("click", handleSubmit);

//Function

function handleSubmit(e) {
    if (typeProductInp.val() === "" || sizeTypeInp.val() === "") {
        e.preventDefault();
    }
}

function subImageHandle() {
    const target = $(this);
    changePreviewImage(target.find("img").attr("src"));
    changeLastImage(target);
    lastImage = target.find("img").attr("src");
}

function changeLastImage(e) {
    if (lastSubImage) {
        lastSubImage.removeClass("viewing");
    } else {
        lastImage = e.find("img").attr("src");
    }
    e.addClass("viewing");
    lastSubImage = e;
}

function changePreviewImage(e) {
    let attr = e;
    if (attr !== null && attr !== "") {
        $("#product-img-preview").attr({
            src: attr,
        });
    }
}

function handleQuantityOnInput() {
    let value = quantityInp.val();
    if (isNaN(parseInt(value[value.length - 1]))) {
        quantityInp.val(value.substring(0, value.length - 1));
    }
    if (parseInt(value) === 0) {
        quantityInp.val(1);
    } else if (value[0] === "0") {
        quantityInp.val(value.substring(1, value.length));
    }
    if (quantityInp.val() < 0) quantityInp.val(-quantityInp.val());
}

function handleQuantityInputChange() {
    if (quantityInp.val() === "") {
        quantityInp.val(1);
    }
    if (!checkQuantity(quantityInp.val())){
        quantityInp.val(0)
    }
}

function handleQuantityDecrease() {
    let value = quantityInp.val();
    if (value > 1) {
        quantityInp.val(quantityInp.val() - 1);
    }
}

function handleQuantityIncrease() {
    quantityInp.val(parseInt(quantityInp.val()) + 1);
}

function changeClassifyPreviewImage() {
    const target = $(this).find("img");
    const attr = target.attr("src");
    changePreviewImage(attr);
}

function returnPreviewImage() {
    if (lastImage) {
        changePreviewImage(lastImage);
    }
}

function classifyClickHandle(e) {
    const target = $(this);
    if (target.hasClass("viewing")){
       target.removeClass("viewing");
        typeProductInp.attr("value", "");
        return;
    }
    typeProductInp.attr("value", $(this).attr("id"));
    if (lastChoose) {
        lastChoose.removeClass("viewing");
    }
    changeClassifyPreviewImage(e);
    target.addClass("viewing");
    lastImage = target.find("img").attr("src");
    lastChoose = target;
    e.preventDefault();

    // TODO xử lý thay đổi số lượng sản phẩm còn lại khi thay đổi loại sản phẩm
}

function handleChangeSize() {
   
    if ($(this).hasClass("viewing")){
        $(this).removeClass("viewing")
        sizeTypeInp.attr("value", "");
        return
    }
    if (sizeLastChoose !== null) {
        sizeLastChoose.removeClass("viewing");
    }
    sizeTypeInp.attr("value", $(this).text().trim());
    sizeLastChoose = $(this);
    sizeLastChoose.addClass("viewing");
    const value = $(this).text().trim();
    sizeValue.val(value);
    const typeProductId = typeProductInp.attr('value');
    if (typeProductId) {
        const sizeText = $(this).text().trim();
        const id = typeProductId + sizeText;
        updateRemainingQuantity(id)
    }
}


function updateRemainingQuantity(id) {
    remainingQuantity.addClass('d-none');
    $(".remaining").find(`#${id}`).removeClass('d-none');
}


function checkQuantity(quan) {
    const quantity = $(".remaining-quantity:not(.d-none)").text().trim();
    return quan <= quantity;
}