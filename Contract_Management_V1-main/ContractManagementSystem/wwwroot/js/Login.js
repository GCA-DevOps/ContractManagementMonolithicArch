//function togglePasswordVisibility() {
//    var passwordField = document.getElementById("yourPassword");
//    var passwordToggleBtn = document.querySelector(".password-toggle-btn");

//    if (passwordField.type === "password") {
//        passwordField.type = "text";
//        passwordToggleBtn.innerHTML = '<i class="fas fa-eye-slash"></i>';
//        passwordToggleBtn.innerHTML = '<i class="fas fa-eye"></i>';
//    }    } else {
//        passwordField.type = "password";

//}

//document.getElementById('loginButton').addEventListener('click', function (event) {
//    event.preventDefault(); // Prevent the default form submission behavior

//    var form = document.getElementById('loginForm');
//    if (form.checkValidity()) {
//        var actionUrl = form.getAttribute('action');
//        var formData = new FormData(form);

//        fetch(actionUrl, {
//            method: 'POST',
//            body: formData
//        })
//            .then(response => {
//                if (response.ok) {
//                    // Redirect or do something on successful login
//                    window.location.href = 'RedirectPage';
//                } else {
//                    // Display error message for incorrect credentials
//                    alert('Invalid email or password');
//                    // Optionally, reset the form
//                    form.reset();
//                }
//            })
//            .catch(error => {
//                console.error('Error:', error);
//                // Display error message for any network or server error
//                alert('An error occurred, please try again later');
//            });
//    } else {
//        form.reportValidity();
//    }
//});
