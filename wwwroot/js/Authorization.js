/*document.forms['LoginForm'].onsubmit = function (e) {
    e.preventDefault();
    Login();
    
}*/
document.getElementById('submitBtn').addEventListener('click', function (event) {
    event.preventDefault();
    Login();
});

function Login() {
    console.log("Login button clicked!");
    var login = document.getElementById('Login').value;
    var password = document.getElementById('Password').value;
    var ErrorBox = document.getElementById('ErrorBox');

    $.ajax({
        type: 'POST',
        url: '../Account/Authorization',
        data: {
            Login: login,
            Password: password
        },
        dataType: 'json',
        success: function (data) {
            console.log(data);
            if (JSON.parse(data) === false) {
                console.log("Данные не доставлены - Json.Parse(Data) == false")
                
                ErrorBox.innerHTML = 'Неверный логин и/или пароль!';
                document.getElementById('password').value = '';       // HTML - id login & password
                setTimeout(function () { errorLabel.innerHTML = ''; }, 2500);
            }
            else {
                console.log("Данные доставлены");
                location.reload();
            }
        },
        error: function (data) {
            console.log(data);
            alert('Ошибка');
        }
    });
}