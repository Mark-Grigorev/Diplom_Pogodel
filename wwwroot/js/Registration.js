document.getElementById('submitBtn').addEventListener('click', function (event) {
    event.preventDefault();
    Registration();
});

function Registration() {
    console.log("Registration _ Cliced");
    var name = document.getElementById('Name').value;
    var phone = document.getElementById('Phone').value;
    var login = document.getElementById('Login').value;
    var password = document.getElementById('Password').value;
    var ErrorBox = document.getElementById('ErrorBox');

    $.ajax({
        type: 'POST',
        url: '../Account/Register',
        data: {
            Name: name,
            Phone: phone,
            Login: login,
            Password: password
        },
        dataType: 'json',
        success: function (data) {
            console.log(data);
            if (JSON.parse(data) === false) {
                ErrorBox.innerHTML = 'Не все данные введены!';
                setTimeout(function () { errorBox.innerHTML = ''; }, 2500);
            }
            else {
                location.reload();
            }
        },
        error: function (data) {
            console.log(data);
            alert('Вы зарегестрированы');
            
        }
    });
}