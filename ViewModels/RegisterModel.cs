using System.ComponentModel.DataAnnotations;

namespace Diplom_Pogodel.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Вы не представились")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указан Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}