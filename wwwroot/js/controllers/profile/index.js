const btnPickAvatar = $("#pick-image");
const inpAvatar = $(".avatar-input");
const previewImage = $(".avatar__image");
const btnSubmitProfile  = $(".btn-submit");

btnPickAvatar.on("click", handlePickAvatar);
inpAvatar.on("change", handleChangeAvatar);
btnSubmitProfile.on("click", handleSubmitProfileChange)


function handleSubmitProfileChange(e) {
  if (!checkValidate()){
    e.preventDefault();
  }
}

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



const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
const phoneRegex = /^0\d{9}$/;

function checkValidate(){
  const email = $("#Email").text().trim();
  if (!emailRegex.check(email)){
    return false;
  }
  const phoneNumber = $("#PhoneNumber").text().trim();
  return phoneRegex.test(phoneNumber);
}
