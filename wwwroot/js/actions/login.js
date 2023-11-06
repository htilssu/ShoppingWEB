<<<<<<< HEAD
﻿
document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('btn-register').addEventListener('click', function (event) {
        event.preventDefault(); 

        // Lấy giá trị từ các trường
        const username = document.getElementById('username').value;
        const phone = document.getElementById('phone').value;
        const email = document.getElementById('email').value;
        const password = document.getElementById('password').value;
        const confirmPassword = document.getElementById('repassword').value;
        // Regex tên đăng nhập
        const usernamePattern = /^[a-zA-Z0-9]+$/;
        // Regex cho số điện thoại - 10 hoặc 11 chữ số, bắt đầu bằng số 0
        const phonePattern = /^(0\d{9,10})$/;
        // Regex cho email
        const emailPattern = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        // Regex cho mật khẩu - ít nhất 8 ký tự, 1 chữ cái viết hoa, 1 chữ cái viết thường, 1 số
        const passwordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$/;
        // Kiểm tra xem tên đăng nhập ,số điện thoại, email, và mật khẩu có hợp lệ không
        if (!usernamePattern.test(username)) {
            document.getElementById('username-error').style.display = 'block';
            document.getElementById('username').classList.add('error');
            return;
        } else {
            document.getElementById('username-error').style.display = 'none';
            document.getElementById('username').classList.remove('error');
        }
        if (!phonePattern.test(phone)) {
            document.getElementById('phone-error').style.display = 'block';
            document.getElementById('phone').classList.add('error');
            return;
        } else {
            document.getElementById('phone-error').style.display = 'none';
            document.getElementById('phone').classList.remove('error');
        }
        if (!emailPattern.test(email)) {
            document.getElementById('email-error').style.display = 'block';
            document.getElementById('email').classList.add('error');
            return;
        } else {
            document.getElementById('email-error').style.display = 'none';
            document.getElementById('email').classList.remove('error');
        }
        if (!passwordPattern.test(password)) {
            document.getElementById('password-error').style.display = 'block';
            document.getElementById('password').classList.add('error');
            return;
        } else {
            document.getElementById('password-error').style.display = 'none';
            document.getElementById('password').classList.remove('error');
        }
        
        window.location.href = https://localhost:7278/login
=======
﻿document.addEventListener("DOMContentLoaded", function () {
  document
    .getElementById("btn-register")
    .addEventListener("click", function (event) {
      // Lấy giá trị từ các trường
      const username = document.getElementById("username").value;
      const phone = document.getElementById("phone").value;
      const email = document.getElementById("email").value;
      const password = document.getElementById("password").value;
      const confirmPassword = document.getElementById("repassword").value;
      // Regex tên đăng nhập
      const usernamePattern = /^[a-zA-Z0-9]+$/;
      // Regex cho số điện thoại - 10 hoặc 11 chữ số, bắt đầu bằng số 0
      const phonePattern = /^(0\d{9,10})$/;
      // Regex cho email
      const emailPattern = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
      // Regex cho mật khẩu - ít nhất 8 ký tự, 1 chữ cái viết hoa, 1 chữ cái viết thường, 1 số
      const passwordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$/;
      // Kiểm tra xem tên đăng nhập ,số điện thoại, email, và mật khẩu có hợp lệ không
      if (!usernamePattern.test(username)) {
        document.getElementById("username-error").style.display = "block";
        document.getElementById("username").classList.add("error");
        event.preventDefault();
      } else {
        document.getElementById("username-error").style.display = "none";
        document.getElementById("username").classList.remove("error");
      }
      if (!phonePattern.test(phone)) {
        document.getElementById("phone-error").style.display = "block";
        document.getElementById("phone").classList.add("error");
        event.preventDefault();
      } else {
        document.getElementById("phone-error").style.display = "none";
        document.getElementById("phone").classList.remove("error");
      }
      if (!emailPattern.test(email)) {
        document.getElementById("email-error").style.display = "block";
        document.getElementById("email").classList.add("error");
        event.preventDefault();
      } else {
        document.getElementById("email-error").style.display = "none";
        document.getElementById("email").classList.remove("error");
      }
      if (!passwordPattern.test(password)) {
        document.getElementById("password-error").style.display = "block";
        document.getElementById("password").classList.add("error");
        event.preventDefault();
      } else {
        document.getElementById("password-error").style.display = "none";
        document.getElementById("password").classList.remove("error");
      }
      console.log(111);
>>>>>>> 44a96e142b0c200e7bb5ebdcf631fe425fbecc53
    });
});
