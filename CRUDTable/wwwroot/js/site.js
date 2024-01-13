    document.addEventListener('DOMContentLoaded', function () {
        var passInput = document.getElementById('pass');
    var submitButton = document.getElementById('submitButton1');

    submitButton.addEventListener('click', function () {
            if (!validatePassword(passInput.value)) {
        alert("Mật khẩu không hợp lệ. Yêu cầu ít nhất 8 ký tự, 1 ký tự hoa, và 1 ký tự đặc biệt.");
    event.preventDefault(); // Ngăn chặn form được gửi đi nếu mật khẩu không hợp lệ
            }
        });

        function validatePassword(password) {
            
                var passwordRegex = /^(?=.*[A-Z])(?=.*[!@#$%^&*])(.{8,})$/;
        return passwordRegex.test(password);
        }
    });

document.addEventListener('DOMContentLoaded', function () {
    var passInput = document.getElementById('newpass');
    var submitButton = document.getElementById('submitButton');

    submitButton.addEventListener('click', function () {
        var password = passInput.value;

        // Bỏ qua kiểm tra mật khẩu nếu người dùng để trống mật khẩu
        if (password.trim() !== '' && !validatePassword(password)) {
            alert("Mật khẩu không hợp lệ. Yêu cầu ít nhất 8 ký tự, 1 ký tự hoa, và 1 ký tự đặc biệt.");
            event.preventDefault(); // Ngăn chặn form được gửi đi nếu mật khẩu không hợp lệ
        }
    });

    function validatePassword(password) {
        var passwordRegex = /^(?=.*[A-Z])(?=.*[!@#$%^&*])(.{8,})$/;
        return passwordRegex.test(password);
    }
});
