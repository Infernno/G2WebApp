using System.ComponentModel.DataAnnotations;

namespace G2WebApp.Core.Data.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите имя пользователя")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите имя пользователя")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Введите класс")]
        public string Class { get; set; }

        public string Ip { get; set; }
    }
}
