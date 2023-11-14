let deleteImageArr = [];
let sizeArr = [];
collectSizeArr();
let typeProduct = [];
let btnClosePop = $(".btn-close__popup");
const btnSubmit = $("#check");
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
      imageItem.append(`<span class="delete__image d-flex justify-content-center align-items-center position-absolute top-0 start-100 translate-middle badge rounded-circle bg-danger">
                <i class="bi bi-x-lg"></i>
           </span>
            `);

      imageList.append(imageItem);
    };
    stream.readAsDataURL(file);
  }

  setTimeout(() => {
    $(".delete__image").on("click", handleDeleteImage);
  }, 1000);
}

function handleDeleteType() {
  const deleteTarget = $(this).closest(".type__item");
  const deleteId = deleteTarget.attr("id");
  deleteTarget.remove();
  productQuantityFormList.find(`#${deleteId}`).remove();
}

function handleShowQuantityForm() {
  productQuantityFormList.removeClass("d-none");
  const targetId = $(this).attr("id");

  productQuantityFormList.find(`#${targetId}`).removeClass("d-none");
}

function handleAddType() {
  const newTypeName = productTypeForm.find("input").first().val();
  for (const item of typeProduct) {
    if (item.name === newTypeName) return;
  }
  typeProduct.push({
    id: CreateUUID(),
    name: newTypeName,
    size: [...sizeArr],
  });

  const newType = typeProduct[typeProduct.length - 1];
  const indexProduct = typeProduct.length - 1;
  console.log(typeProduct);

  productQuantityFormList.append(`
  <div class="product-quantity__form d-none popup-form position-absolute" id="${newType.id}">
      <div class="quantity-form__header p-2">
          <div class="text-uppercase fw-bold">
              Thêm số lượng sản phẩm
          </div>
      </div>
      <div class="quantity-form__main p-2">
          <div class="row">
             <div> 
             <label class="form-label fw-bold fs-5">${newType.name}</label>
             <input value="${newType.name}" class="form-control" name="TypeProducts[${indexProduct}].TypeName" type="hidden">
             </div>
            <div>                                                       
                <label class="form-label">Chọn ảnh</label>
               <input name="TypeProducts[${indexProduct}].ImageFile" class="form-control" accept="image/png, image/jpeg, image/bmp, image/jpg, image/svg" type="file">
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
                                            
                                            
                                        </div>
  `);

  productListType.append(`
  <div class="type__item" id="${newType.id}">
        <div class="position-relative">
            <div class="btn showQuantity btn-outline-primary" id="${newType.id}">
                ${newType.name}
            </div>
            <span class="delete__type d-flex justify-content-center align-items-center position-absolute top-0 start-100 translate-middle badge rounded-circle bg-danger">
                <i class="bi bi-x-lg"></i>
            </span>
        </div>
    </div>
  `);

  productQuantityForm = $(".product-quantity__form");
  const lastForm = productQuantityForm.last();
  const row = lastForm.find(".row").eq(1);
  console.log(row);

  newType.size.forEach((item, index) => {
    row.append(
      `<div class="row" id="${item.id}">
<div class="col-6 mt-2">${item.size}
<input type="text" name="TypeProducts[${indexProduct}].Sizes[${index}].SizeType" value="${item.size}" hidden="hidden">
</div>
        
        <div class="col-6 ">
        <input class="form-control" name="TypeProducts[${indexProduct}].Sizes[${index}].Quantity" value="0"></div>
</div>`,
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
  const size = productTypeSizeForm.find("input").val();
  if (size !== "") {
    for (const item of sizeArr) {
      if (item.size === size) return;
    }
    sizeArr.push({
      id: CreateUUID(),
      size: size,
      quantity: 0,
    });
    console.log(sizeArr);
  } else {
    return;
  }

  productTypeSizeForm.find("input").val("");
  const newSize = sizeArr[sizeArr.length - 1];
  if (productQuantityForm.length !== 0) {
    productQuantityForm.each((index, item) => {
      $(item).find(".row").eq(1).append(`
<div class="row" id="${newSize.id}">
<div class="col-6 mt-2">${newSize.size}
<input type="text" name="TypeProducts[${index}].Sizes[${
        sizeArr.length - 1
      }].SizeType" value="${sizeArr[sizeArr.length - 1].size}" hidden="hidden">
</div>
        
        <div class="col-6 ">
        <input class="form-control" name="TypeProducts[${index}].Sizes[${
          sizeArr.length - 1
        }].Quantity" value="0"></div>
</div>
`);
    });
  }
  productTypeListSize.append(`
  <div class="size__item" id="${newSize.id}">
        <div class="position-relative">
            <div class="btn btn-outline-primary">
                ${newSize.size}
            </div>
            <span class="delete__size d-flex justify-content-center align-items-center position-absolute top-0 start-100 translate-middle badge rounded-circle bg-danger">
                <i class="bi bi-x-lg"></i>
            </span>
        </div>
    </div>
  `);

  $(".delete__size").on("click", handleDeleteSize);
}

function handleDeleteSize() {
  const sizeId = $(this).closest(".size__item").attr("id");
  productQuantityFormList.find(`#${sizeId}`).remove();
  sizeArr = sizeArr.filter((item) => item.id !== sizeId);
  typeProduct = typeProduct.map((item) => ({
    ...item,
    size: item.size.filter((sizeItem) => sizeItem.id !== sizeId),
  }));
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

setInterval(() => {
  checkValidate();
}, 1000);

function checkValidate() {
  if ($(".product-type-list__size").find(".size__item").length === 0) {
    btnSubmit.attr("disabled", "disabled");
    console.log(1);
    return;
  }
  if ($("#ProductName").val() === "") {
    btnSubmit.attr("disabled", "disabled");
    console.log(2);
    return;
  }

  if ($(".type__item").length === 0) {
    btnSubmit.attr("disabled", "disabled");
    console.log(111);
    return;
  }

  for (const item of $('input[type="file"]')) {
    if (item.files.length === 0) {
      console.log(4);
      btnSubmit.attr("disabled", "disabled");

      return;
    }
  }

  btnSubmit.removeAttr("disabled");
}

function renderSize() {
  productTypeListSize.empty();
  sizeArr.forEach((item) => {
    productTypeListSize.append(`
        < div

        class

        = "size__item"
        id = "${CreateUUID()}"
            >
            < div

        class

        = "position-relative" >
            < div

        class

        = "btn btn-outline-primary" >
            ${item.size}
            < /div>
        <span
            class="delete__size d-flex justify-content-center align-items-center position-absolute top-0 start-100 translate-middle badge rounded-circle bg-danger">
                <i class="bi bi-x-lg"></i>
            </span>
    </div>
    </div>
        `);
  });
  $(".delete__size").on("click", handleDeleteSize);
}

function CreateUUID() {
  return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, (c) =>
    (
      c ^
      (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (c / 4)))
    ).toString(16),
  );
}

function collectSizeArr() {
  const sizeItem = $(".product-type-list__size").find(".size__item");
  if (sizeItem.length !== 0) {
    sizeItem.each((index, item) => {
      sizeArr.push({
        id: CreateUUID(),
        size: $(item).find(".btn").text().trim(),
        quantity: 0,
      });
    });
  }
}
