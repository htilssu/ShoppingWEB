const fileChoose = $("#imageFile");
const deleteBtn = $(".delete");
const closeBtn = $(".cancel");

fileChoose.on("change", handleChangeImage);
deleteBtn.on("click", handleConfirmDelete);
closeBtn.on("click", handleClosePopup);

function handleClosePopup() {
  const parent = $(this).closest(".delete-confirm-pop");
  console.log(parent);
  parent.addClass("d-none");
}

function handleConfirmDelete() {
  const trTag = $(this).closest("tr");
  trTag.find(".delete-confirm-pop").removeClass("d-none");
}

function handleChangeImage(e) {
  const display = $("#display-img");
  const file = e.target.value;
  console.log(file);
  if (file) {
    const fileReader = new FileReader();
    fileReader.onload = function (e) {
      display.attr({
        src: e.target.result,
      });
    };

    fileReader.readAsDataURL(e.target.files[0]);
  }
}
