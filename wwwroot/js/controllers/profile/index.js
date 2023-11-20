const btnPickAvatar = $("#pick-image");
const inpAvatar = $(".avatar-input");
const previewImage = $(".avatar__image");

btnPickAvatar.on("click", handlePickAvatar);
inpAvatar.on("change", handleChangeAvatar);

function handleChangeAvatar() {
  const current = $(this);
  const file = current[0].files[0];
  const fileReader = new FileReader();
  fileReader.onload = function (e) {
    previewImage.css(
      "background",
      `center / cover no-repeat url(${e.target.result})`,
    );
  };
  fileReader.readAsDataURL(file);
  current.closest("form").submit();
}
function handlePickAvatar() {
  inpAvatar.click();
}
