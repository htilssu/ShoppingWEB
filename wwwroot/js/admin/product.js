let deleteImageArr = [];
let sizeArr = [];
let typeArr = [];
let btnClosePop = $(".btn-close__popup");
const btnSubmit = $('button[type="submit"]');
const btnShowSizePop = $(".btn-show__size");
const btnShowTypePop = $(".btn-show__type");
const btnAddSize = $(".btn-add__size");
const btnAddType = $(".btn-add__type");
const btnSubmitEdit = $(".submit-edit").first();
const productTypeListSize = $(".product-type-list__size");
const productQuantityFormList = $(".product-quantity-form__list");
const productTypeSizeForm = $(".product-type-size__form");
const imageList = $(".image-list");
const imageSelector = $("input[name='ImageFile']");
const btnDeleteSize = $(".delete__size");
let btnDeleteType = $(".delete__type");
let btnDeleteImage = $(".delete__image");
const productListType = $(".product-type-list");
const productTypeForm = $(".product-type__form");
let productQuantityForm = $(".product-quantity__form");
let btnShowQuantity = $(".showQuantity");
let btnSaveQuantity = $(".btn-save__quantity");

btnShowQuantity.on("click", handleShowQuantityForm);
btnClosePop.on("click", handleCloseProductPopup);
btnShowSizePop.on("click", handleShowProductSizePop);
btnAddSize.on("click", handleAddSize);
btnDeleteSize.on("click", handleDeleteSize);
btnDeleteType.on("click", handleDeleteType);
btnAddType.on("click", handleAddType);
btnShowTypePop.on("click", handleShowProductTypePopup);
imageSelector.on("change", handleChangeListImage);
btnDeleteImage.on("click", handleDeleteImage);
btnSubmitEdit.on("click", handleSubmitEdit);
btnSaveQuantity.on("click", handleSaveQuantity);

function handleSaveQuantity() {
  $(this).closest(".product-quantity-form__list").addClass("d-none");
  $(this).closest(".popup-form").addClass("d-none");
}

function handleSubmitEdit() {
  deleteImageArr.forEach((item, index) => {
    $.ajax({
      url: `https://localhost:7278/api/image?id=${item}`,
      type: "DELETE",
      xhrFields: {
        withCredentials: true,
      },
    });
  });
}

function handleDeleteImage() {
  const parentDiv = $(this).closest(".col-2");

  if (parentDiv.hasClass("addition-image")) {
    const targetBase64 = parentDiv.find("img").attr("src");
    const files = imageSelector[0].files;
    const dt = new DataTransfer();
    for (const file of files) {
      const reader = new FileReader();
      reader.onload = (e) => {
        if (e.target.result !== targetBase64) {
          dt.items.add(file);
          imageSelector[0].files = dt.files;
        } else {
          imageSelector[0].files = dt.files;
        }
      };

      reader.readAsDataURL(file);
    }

    parentDiv.remove();
  }
  deleteImageArr.push(parentDiv.find("img").first().attr("src"));
  parentDiv.remove();
}

function handleChangeListImage(ev) {
  const files = ev.target.files;

  imageList.find("img").each((index, item) => {
    if (!$(item).attr("src").includes("upload")) {
      $(item).remove();
    }
  });
  for (const file of files) {
    const stream = new FileReader();
    stream.onload = (e) => {
      const imageItem = $("<div>");
      const image = $("<img>");
      image.addClass("w-100");
      imageItem.addClass("col-2 mt-2 position-relative addition-image");
      image.attr("src", e.target.result);
      imageItem.append(image);
      imageItem.append(
        `<span class="delete__image d-flex justify-content-center align-items-center position-absolute top-0 start-100 translate-middle badge rounded-circle bg-danger">
                <i class="bi bi-x-lg"></i>
           </span>
            `,
      );

      imageList.append(imageItem);
    };
    stream.readAsDataURL(file);
  }

  setTimeout(() => {
    $(".delete__image").on("click", handleDeleteImage);
  }, 1000);
}

function handleDeleteType() {
  productQuantityForm.each((index, item) => {
    if (
      $(item).find("label").first().text().trim() ===
      $(this).closest(".type__item").find(".btn").text().trim()
    ) {
      $(item).remove();
    }
  });
  $(this).closest(".type__item").remove();
}

function handleShowQuantityForm() {
  productQuantityFormList.removeClass("d-none");
  productQuantityForm.each((index, item) => {
    if ($(item).find("label").first().text().trim() === $(this).text().trim()) {
      $(item).removeClass("d-none");
    }
  });
}

function handleAddType() {
  const sizeList = $(".size__item");
  // TODO prevent user add if dont have any size
  productListType.append(`
  <div class="type__item">
        <div class="position-relative">
            <div class="btn showQuantity btn-outline-primary">
                ${productTypeForm.find("input").first().val()}
            </div>
            <span class="delete__type d-flex justify-content-center align-items-center position-absolute top-0 start-100 translate-middle badge rounded-circle bg-danger">
                <i class="bi bi-x-lg"></i>
            </span>
        </div>
    </div>
  `);

  productQuantityFormList.append(
    `<div class="product-quantity__form d-none popup-form position-absolute">
      <div class="quantity-form__header p-2">
          <div class="text-uppercase fw-bold">
              Thêm số lượng sản phẩm
          </div>
      </div>
      <div class="quantity-form__main p-2">
          <div class="row">
             <div> 
             <label class="form-label fw-bold fs-5">${$(this)
               .closest(".popup-form")
               .find("input")
               .val()}</label>
             <input value="${$(this)
               .closest(".popup-form")
               .find("input")
               .val()}" class="form-control" name="TypeProducts[${
               productQuantityForm.length
             }].TypeName" type="hidden">
             </div>
            <div>                                                       
                <label class="form-label">Chọn ảnh</label>
              
         </div>
</div>
        
          <div class="row mt-2">
             <div class="col-6">Size</div>
          <div class="col-6">Số Lượng</div>
          </div>
                                               
                                              
</div>
                                                    <div class="quantity-form__action p-2">
                                                <div class="d-flex justify-content-end gap-3">
                                                    <div class="btn btn-primary btn-close__popup">Hủy</div>
                                                    <div class="btn btn-success btn-save__quantity">Lưu</div>
                                                </div>
                                            </div>
                                            </div>
                                            
                                            
                                        </div>`,
  );

  productQuantityForm = $(".product-quantity__form");
  const imgTypeProduct = productTypeForm
    .find("input")
    .eq(1)
    .clone()
    .attr("name", `TypeProducts[${productQuantityForm.length - 1}].ImageFile`);
  const lastForm = productQuantityForm.last();
  const rowHide = lastForm.find(".row").eq(0).find("div").last();
  rowHide.append(imgTypeProduct);

  const row = lastForm.find(".row").eq(1);

  sizeList.map((index, item) => {
    row.append(
      `<div class="col-6 mt-2">${$(item)
        .find(".btn.btn-outline-primary")
        .text()
        .trim()}
<input type="text" name="TypeProducts[${
        productQuantityForm.length - 1
      }].Sizes[${index}].SizeType" value="${$(item)
        .find(".btn.btn-outline-primary")
        .text()
        .trim()}" hidden="hidden">
</div>
        
        <div class="col-6 ">
        <input class="form-control" name="TypeProducts[${
          productQuantityForm.length - 1
        }].Sizes[${index}].Quantity" value="0"></div>`,
    );
  });
  btnDeleteType = $(".delete__type");
  btnShowQuantity = $(".showQuantity");
  btnClosePop = $(".btn-close__popup");
  productQuantityForm = $(".product-quantity__form");
  btnShowQuantity.on("click", handleShowQuantityForm);
  btnClosePop.on("click", handleCloseProductPopup);
  productTypeForm.find("input").val("");
  btnDeleteType.on("click", handleDeleteType);
  $(".btn-save__quantity").on("click", function () {
    $(this).closest(".product-quantity-form__list").addClass("d-none");
    $(this).closest(".popup-form").addClass("d-none");
  });
}

function handleAddSize() {
  productTypeListSize.append(`
     <div class="size__item">
        <div class="position-relative">
            <div class="btn btn-outline-primary">
                ${productTypeSizeForm.find("input").val()}
            </div>
            <span class="delete__size d-flex justify-content-center align-items-center position-absolute top-0 start-100 translate-middle badge rounded-circle bg-danger">
                <i class="bi bi-x-lg"></i>
            </span>
        </div>
    </div>
  `);
  const rowSize = productTypeSizeForm.find("input").val("");
  $(".delete__size").on("click", handleDeleteSize);
}

function handleDeleteSize() {
  const size = $(this).attr("id");
  productQuantityFormList.find(`.${size}`).remove();
  $(this).closest(".size__item").remove();
}

function handleShowProductSizePop() {
  btnClosePop.closest(".product-type-size__form").removeClass("d-none");
}

function handleShowProductTypePopup() {
  btnClosePop.closest(".product-type__form").removeClass("d-none");
}

function handleCloseProductPopup() {
  $(this).closest(".product-quantity-form__list").addClass("d-none");
  $(this).closest(".popup-form").addClass("d-none");
  productTypeForm.find("input").val("");
  productTypeSizeForm.find("input").val("");
}

function checkValidate() {
  if ($(".product-type-list__size").find(".size__item").length === 0) {
    return;
  }
  if ($(".product-type-list").find(".btn").length === 0) {
    return;
  }
  $('input[type="text"]').each((index, item) => {
    if ($(item).val() === "") return;
  });

  $('input[type="file"]').each((index, item) => {
    if (item.values.length === 0) {
      return;
    }
  });

  btnSubmit.removeAttr("disable");
}
